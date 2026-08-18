using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Web.ModelBinding
{
	// Token: 0x02000680 RID: 1664
	public class MutableObjectModelBinder : IModelBinder
	{
		// Token: 0x1700173E RID: 5950
		// (get) Token: 0x060050D4 RID: 20692 RVA: 0x00116ADC File Offset: 0x00114CDC
		// (set) Token: 0x060050D5 RID: 20693 RVA: 0x00116AF7 File Offset: 0x00114CF7
		internal ModelMetadataProvider MetadataProvider
		{
			get
			{
				if (this._metadataProvider == null)
				{
					this._metadataProvider = ModelMetadataProviders.Current;
				}
				return this._metadataProvider;
			}
			set
			{
				this._metadataProvider = value;
			}
		}

		// Token: 0x060050D6 RID: 20694 RVA: 0x00116B00 File Offset: 0x00114D00
		public virtual bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ModelBinderUtil.ValidateBindingContext(bindingContext);
			this.EnsureModel(modelBindingExecutionContext, bindingContext);
			IEnumerable<ModelMetadata> metadataForProperties = this.GetMetadataForProperties(modelBindingExecutionContext, bindingContext);
			ComplexModel complexModel = this.CreateAndPopulateComplexModel(modelBindingExecutionContext, bindingContext, metadataForProperties);
			this.ProcessComplexModel(modelBindingExecutionContext, bindingContext, complexModel);
			bindingContext.ValidationNode.ValidateAllProperties = true;
			return true;
		}

		// Token: 0x060050D7 RID: 20695 RVA: 0x00116B49 File Offset: 0x00114D49
		protected virtual bool CanUpdateProperty(ModelMetadata propertyMetadata)
		{
			return MutableObjectModelBinder.CanUpdatePropertyInternal(propertyMetadata);
		}

		// Token: 0x060050D8 RID: 20696 RVA: 0x00116B51 File Offset: 0x00114D51
		internal static bool CanUpdatePropertyInternal(ModelMetadata propertyMetadata)
		{
			return !propertyMetadata.IsReadOnly || MutableObjectModelBinder.CanUpdateReadOnlyProperty(propertyMetadata.ModelType);
		}

		// Token: 0x060050D9 RID: 20697 RVA: 0x00116B68 File Offset: 0x00114D68
		private static bool CanUpdateReadOnlyProperty(Type propertyType)
		{
			return !propertyType.IsValueType && !propertyType.IsArray && !(propertyType == typeof(string));
		}

		// Token: 0x060050DA RID: 20698 RVA: 0x00116B94 File Offset: 0x00114D94
		private ComplexModel CreateAndPopulateComplexModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext, IEnumerable<ModelMetadata> propertyMetadatas)
		{
			ComplexModel originalComplexModel = new ComplexModel(bindingContext.ModelMetadata, propertyMetadatas);
			ModelBindingContext modelBindingContext = new ModelBindingContext(bindingContext)
			{
				ModelMetadata = this.MetadataProvider.GetMetadataForType(() => originalComplexModel, typeof(ComplexModel)),
				ModelName = bindingContext.ModelName
			};
			IModelBinder requiredBinder = bindingContext.ModelBinderProviders.GetRequiredBinder(modelBindingExecutionContext, modelBindingContext);
			requiredBinder.BindModel(modelBindingExecutionContext, modelBindingContext);
			return (ComplexModel)modelBindingContext.Model;
		}

		// Token: 0x060050DB RID: 20699 RVA: 0x00116C17 File Offset: 0x00114E17
		protected virtual object CreateModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			return SecurityUtils.SecureCreateInstance(bindingContext.ModelType);
		}

		// Token: 0x060050DC RID: 20700 RVA: 0x00116C24 File Offset: 0x00114E24
		internal static EventHandler<ModelValidatedEventArgs> CreateNullCheckFailedHandler(ModelBindingExecutionContext modelBindingExecutionContext, ModelMetadata modelMetadata, object incomingValue)
		{
			return delegate(object sender, ModelValidatedEventArgs e)
			{
				ModelValidationNode modelValidationNode = (ModelValidationNode)sender;
				ModelStateDictionary modelState = e.ModelBindingExecutionContext.ModelState;
				if (modelState.IsValidField(modelValidationNode.ModelStateKey))
				{
					string text = ModelBinderErrorMessageProviders.ValueRequiredErrorMessageProvider(modelBindingExecutionContext, modelMetadata, incomingValue);
					if (text != null)
					{
						modelState.AddModelError(modelValidationNode.ModelStateKey, text);
					}
				}
			};
		}

		// Token: 0x060050DD RID: 20701 RVA: 0x00116C58 File Offset: 0x00114E58
		protected virtual void EnsureModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			if (bindingContext.Model == null)
			{
				bindingContext.ModelMetadata.Model = this.CreateModel(modelBindingExecutionContext, bindingContext);
			}
		}

		// Token: 0x060050DE RID: 20702 RVA: 0x00116C78 File Offset: 0x00114E78
		protected virtual IEnumerable<ModelMetadata> GetMetadataForProperties(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			HashSet<string> requiredProperties;
			HashSet<string> skipProperties;
			MutableObjectModelBinder.GetRequiredPropertiesCollection(bindingContext.ModelType, out requiredProperties, out skipProperties);
			return from propertyMetadata in bindingContext.ModelMetadata.Properties
			let propertyName = propertyMetadata.PropertyName
			let shouldUpdateProperty = requiredProperties.Contains(propertyName) || !skipProperties.Contains(propertyName)
			where shouldUpdateProperty && this.CanUpdateProperty(propertyMetadata)
			select propertyMetadata;
		}

		// Token: 0x060050DF RID: 20703 RVA: 0x00116D20 File Offset: 0x00114F20
		private static object GetPropertyDefaultValue(PropertyDescriptor propertyDescriptor)
		{
			DefaultValueAttribute defaultValueAttribute = propertyDescriptor.Attributes.OfType<DefaultValueAttribute>().FirstOrDefault<DefaultValueAttribute>();
			if (defaultValueAttribute == null)
			{
				return null;
			}
			return defaultValueAttribute.Value;
		}

		// Token: 0x060050E0 RID: 20704 RVA: 0x00116D4C File Offset: 0x00114F4C
		internal static void GetRequiredPropertiesCollection(Type modelType, out HashSet<string> requiredProperties, out HashSet<string> skipProperties)
		{
			requiredProperties = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			skipProperties = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			ICustomTypeDescriptor customTypeDescriptor = TypeDescriptorHelper.Get(modelType);
			PropertyDescriptorCollection properties = customTypeDescriptor.GetProperties();
			BindingBehaviorAttribute bindingBehaviorAttribute = customTypeDescriptor.GetAttributes().OfType<BindingBehaviorAttribute>().SingleOrDefault<BindingBehaviorAttribute>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				BindingBehaviorAttribute bindingBehaviorAttribute2 = propertyDescriptor.Attributes.OfType<BindingBehaviorAttribute>().SingleOrDefault<BindingBehaviorAttribute>();
				BindingBehaviorAttribute bindingBehaviorAttribute3 = bindingBehaviorAttribute2 ?? bindingBehaviorAttribute;
				if (bindingBehaviorAttribute3 != null)
				{
					BindingBehavior behavior = bindingBehaviorAttribute3.Behavior;
					if (behavior != BindingBehavior.Never)
					{
						if (behavior == BindingBehavior.Required)
						{
							requiredProperties.Add(propertyDescriptor.Name);
						}
					}
					else
					{
						skipProperties.Add(propertyDescriptor.Name);
					}
				}
			}
		}

		// Token: 0x060050E1 RID: 20705 RVA: 0x00116E28 File Offset: 0x00115028
		internal void ProcessComplexModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext, ComplexModel complexModel)
		{
			HashSet<string> collection;
			HashSet<string> hashSet;
			MutableObjectModelBinder.GetRequiredPropertiesCollection(bindingContext.ModelType, out collection, out hashSet);
			HashSet<string> hashSet2 = new HashSet<string>(collection);
			hashSet2.ExceptWith(from r in complexModel.Results
			select r.Key.PropertyName);
			string text = hashSet2.FirstOrDefault<string>();
			if (text != null)
			{
				string fieldName = ModelBinderUtil.CreatePropertyModelName(bindingContext.ModelName, text);
				throw Error.BindingBehavior_ValueNotFound(fieldName);
			}
			foreach (KeyValuePair<ModelMetadata, ComplexModelResult> keyValuePair in complexModel.Results)
			{
				ModelMetadata key = keyValuePair.Key;
				ComplexModelResult value = keyValuePair.Value;
				if (value != null)
				{
					this.SetProperty(modelBindingExecutionContext, bindingContext, key, value);
					bindingContext.ValidationNode.ChildNodes.Add(value.ValidationNode);
				}
			}
		}

		// Token: 0x060050E2 RID: 20706 RVA: 0x00116F14 File Offset: 0x00115114
		protected virtual void SetProperty(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext, ModelMetadata propertyMetadata, ComplexModelResult complexModelResult)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptorHelper.Get(bindingContext.ModelType).GetProperties().Find(propertyMetadata.PropertyName, true);
			if (propertyDescriptor == null || propertyDescriptor.IsReadOnly)
			{
				return;
			}
			object obj = complexModelResult.Model ?? MutableObjectModelBinder.GetPropertyDefaultValue(propertyDescriptor);
			propertyMetadata.Model = obj;
			if (obj == null)
			{
				string modelStateKey = complexModelResult.ValidationNode.ModelStateKey;
				if (bindingContext.ModelState.IsValidField(modelStateKey))
				{
					ModelValidator modelValidator = (from v in ModelValidatorProviders.Providers.GetValidators(propertyMetadata, modelBindingExecutionContext)
					where v.IsRequired
					select v).FirstOrDefault<ModelValidator>();
					if (modelValidator != null)
					{
						foreach (ModelValidationResult modelValidationResult in modelValidator.Validate(bindingContext.Model))
						{
							bindingContext.ModelState.AddModelError(modelStateKey, modelValidationResult.Message);
						}
					}
				}
			}
			if (obj != null || TypeHelpers.TypeAllowsNullValue(propertyDescriptor.PropertyType))
			{
				try
				{
					propertyDescriptor.SetValue(bindingContext.Model, obj);
					return;
				}
				catch (Exception exception)
				{
					string modelStateKey2 = complexModelResult.ValidationNode.ModelStateKey;
					if (bindingContext.ModelState.IsValidField(modelStateKey2))
					{
						bindingContext.ModelState.AddModelError(modelStateKey2, exception);
					}
					return;
				}
			}
			string modelStateKey3 = complexModelResult.ValidationNode.ModelStateKey;
			if (bindingContext.ModelState.IsValidField(modelStateKey3))
			{
				complexModelResult.ValidationNode.Validated += MutableObjectModelBinder.CreateNullCheckFailedHandler(modelBindingExecutionContext, propertyMetadata, obj);
			}
		}

		// Token: 0x04002AD7 RID: 10967
		private ModelMetadataProvider _metadataProvider;
	}
}
