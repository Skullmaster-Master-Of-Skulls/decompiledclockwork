using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Internal;
using System.Web.Http.Metadata;
using System.Web.Http.Properties;
using System.Web.Http.Validation;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x0200017E RID: 382
	public class MutableObjectModelBinder : IModelBinder
	{
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x00020920 File Offset: 0x0001EB20
		// (set) Token: 0x060009EE RID: 2542 RVA: 0x00020928 File Offset: 0x0001EB28
		internal ModelMetadataProvider MetadataProvider { private get; set; }

		// Token: 0x060009EF RID: 2543 RVA: 0x00020934 File Offset: 0x0001EB34
		internal static bool CanBindType(Type modelType)
		{
			return !TypeHelper.HasStringConverter(modelType) && !(modelType == typeof(ComplexModelDto));
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00020968 File Offset: 0x0001EB68
		public virtual bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			ModelBindingHelper.ValidateBindingContext(bindingContext);
			if (!bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName))
			{
				return false;
			}
			if (!MutableObjectModelBinder.CanBindType(bindingContext.ModelType))
			{
				return false;
			}
			this.EnsureModel(actionContext, bindingContext);
			IEnumerable<ModelMetadata> metadataForProperties = this.GetMetadataForProperties(actionContext, bindingContext);
			ComplexModelDto dto = this.CreateAndPopulateDto(actionContext, bindingContext, metadataForProperties);
			this.ProcessDto(actionContext, bindingContext, dto);
			bindingContext.ValidationNode.ValidateAllProperties = true;
			return true;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x000209D0 File Offset: 0x0001EBD0
		protected virtual bool CanUpdateProperty(ModelMetadata propertyMetadata)
		{
			return MutableObjectModelBinder.CanUpdatePropertyInternal(propertyMetadata);
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x000209D8 File Offset: 0x0001EBD8
		internal static bool CanUpdatePropertyInternal(ModelMetadata propertyMetadata)
		{
			return !propertyMetadata.IsReadOnly || MutableObjectModelBinder.CanUpdateReadOnlyProperty(propertyMetadata.ModelType);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x000209EF File Offset: 0x0001EBEF
		private static bool CanUpdateReadOnlyProperty(Type propertyType)
		{
			return !propertyType.IsValueType && !propertyType.IsArray && !(propertyType == typeof(string));
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00020A2C File Offset: 0x0001EC2C
		private ComplexModelDto CreateAndPopulateDto(HttpActionContext actionContext, ModelBindingContext bindingContext, IEnumerable<ModelMetadata> propertyMetadatas)
		{
			ModelMetadataProvider modelMetadataProvider = this.MetadataProvider ?? actionContext.GetMetadataProvider();
			ComplexModelDto originalDto = new ComplexModelDto(bindingContext.ModelMetadata, propertyMetadatas);
			ModelBindingContext modelBindingContext = new ModelBindingContext(bindingContext)
			{
				ModelMetadata = modelMetadataProvider.GetMetadataForType(() => originalDto, typeof(ComplexModelDto)),
				ModelName = bindingContext.ModelName
			};
			actionContext.Bind(modelBindingContext);
			return (ComplexModelDto)modelBindingContext.Model;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00020AAC File Offset: 0x0001ECAC
		protected virtual object CreateModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			return Activator.CreateInstance(bindingContext.ModelType);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00020B20 File Offset: 0x0001ED20
		internal static EventHandler<ModelValidatedEventArgs> CreateNullCheckFailedHandler(ModelMetadata modelMetadata, object incomingValue)
		{
			return delegate(object sender, ModelValidatedEventArgs e)
			{
				ModelValidationNode modelValidationNode = (ModelValidationNode)sender;
				ModelStateDictionary modelState = e.ActionContext.ModelState;
				if (modelState.IsValidField(modelValidationNode.ModelStateKey))
				{
					string text = ModelBinderConfig.ValueRequiredErrorMessageProvider(e.ActionContext, modelMetadata, incomingValue);
					if (text != null)
					{
						modelState.AddModelError(modelValidationNode.ModelStateKey, text);
					}
				}
			};
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00020B4D File Offset: 0x0001ED4D
		protected virtual void EnsureModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			if (bindingContext.Model == null)
			{
				bindingContext.ModelMetadata.Model = this.CreateModel(actionContext, bindingContext);
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00020E0C File Offset: 0x0001F00C
		protected virtual IEnumerable<ModelMetadata> GetMetadataForProperties(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			HashSet<string> requiredProperties;
			HashSet<string> skipProperties;
			Dictionary<string, ModelValidator> dictionary;
			MutableObjectModelBinder.GetRequiredPropertiesCollection(actionContext, bindingContext, out requiredProperties, out dictionary, out skipProperties);
			return from propertyMetadata in bindingContext.ModelMetadata.Properties
			let propertyName = propertyMetadata.PropertyName
			let shouldUpdateProperty = requiredProperties.Contains(propertyName) || !skipProperties.Contains(propertyName)
			where shouldUpdateProperty && this.CanUpdateProperty(propertyMetadata)
			select propertyMetadata;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00020EAC File Offset: 0x0001F0AC
		private static object GetPropertyDefaultValue(PropertyDescriptor propertyDescriptor)
		{
			DefaultValueAttribute defaultValueAttribute = propertyDescriptor.Attributes.OfType<DefaultValueAttribute>().FirstOrDefault<DefaultValueAttribute>();
			if (defaultValueAttribute == null)
			{
				return null;
			}
			return defaultValueAttribute.Value;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00020EE0 File Offset: 0x0001F0E0
		internal static void GetRequiredPropertiesCollection(HttpActionContext actionContext, ModelBindingContext bindingContext, out HashSet<string> requiredProperties, out Dictionary<string, ModelValidator> requiredValidators, out HashSet<string> skipProperties)
		{
			requiredProperties = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			requiredValidators = new Dictionary<string, ModelValidator>(StringComparer.OrdinalIgnoreCase);
			skipProperties = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			ICustomTypeDescriptor customTypeDescriptor = TypeDescriptorHelper.Get(bindingContext.ModelType);
			PropertyDescriptorCollection properties = customTypeDescriptor.GetProperties();
			HttpBindingBehaviorAttribute httpBindingBehaviorAttribute = customTypeDescriptor.GetAttributes().OfType<HttpBindingBehaviorAttribute>().SingleOrDefault<HttpBindingBehaviorAttribute>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				string name = propertyDescriptor.Name;
				ModelMetadata metadata = bindingContext.PropertyMetadata[name];
				ModelValidator modelValidator = (from v in actionContext.GetValidators(metadata)
				where v.IsRequired
				select v).FirstOrDefault<ModelValidator>();
				requiredValidators[name] = modelValidator;
				HttpBindingBehaviorAttribute httpBindingBehaviorAttribute2 = propertyDescriptor.Attributes.OfType<HttpBindingBehaviorAttribute>().SingleOrDefault<HttpBindingBehaviorAttribute>();
				HttpBindingBehaviorAttribute httpBindingBehaviorAttribute3 = httpBindingBehaviorAttribute2 ?? httpBindingBehaviorAttribute;
				if (httpBindingBehaviorAttribute3 != null)
				{
					switch (httpBindingBehaviorAttribute3.Behavior)
					{
					case HttpBindingBehavior.Never:
						skipProperties.Add(name);
						break;
					case HttpBindingBehavior.Required:
						requiredProperties.Add(name);
						break;
					}
				}
				else if (modelValidator != null)
				{
					requiredProperties.Add(name);
				}
			}
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00021048 File Offset: 0x0001F248
		internal void ProcessDto(HttpActionContext actionContext, ModelBindingContext bindingContext, ComplexModelDto dto)
		{
			HashSet<string> hashSet;
			Dictionary<string, ModelValidator> dictionary;
			HashSet<string> hashSet2;
			MutableObjectModelBinder.GetRequiredPropertiesCollection(actionContext, bindingContext, out hashSet, out dictionary, out hashSet2);
			hashSet.ExceptWith(from r in dto.Results
			select r.Key.PropertyName);
			foreach (string text in hashSet)
			{
				string text2 = ModelBindingHelper.CreatePropertyModelName(bindingContext.ValidationNode.ModelStateKey, text);
				ModelMetadata modelMetadata = bindingContext.PropertyMetadata[text];
				modelMetadata.Model = null;
				ModelValidator validator = dictionary[text];
				if (!MutableObjectModelBinder.RunValidator(validator, bindingContext, modelMetadata, text2))
				{
					bindingContext.ModelState.AddModelError(text2, Error.Format(SRResources.MissingRequiredMember, new object[]
					{
						text
					}));
				}
			}
			foreach (KeyValuePair<ModelMetadata, ComplexModelDtoResult> keyValuePair in dto.Results)
			{
				ModelMetadata key = keyValuePair.Key;
				ComplexModelDtoResult value = keyValuePair.Value;
				if (value != null)
				{
					this.SetProperty(actionContext, bindingContext, key, value, dictionary[key.PropertyName]);
					bindingContext.ValidationNode.ChildNodes.Add(value.ValidationNode);
				}
			}
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x000211B8 File Offset: 0x0001F3B8
		protected virtual void SetProperty(HttpActionContext actionContext, ModelBindingContext bindingContext, ModelMetadata propertyMetadata, ComplexModelDtoResult dtoResult, ModelValidator requiredValidator)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptorHelper.Get(bindingContext.ModelType).GetProperties().Find(propertyMetadata.PropertyName, true);
			if (propertyDescriptor == null || propertyDescriptor.IsReadOnly)
			{
				return;
			}
			object obj = dtoResult.Model ?? MutableObjectModelBinder.GetPropertyDefaultValue(propertyDescriptor);
			propertyMetadata.Model = obj;
			if (obj == null)
			{
				string modelStateKey = dtoResult.ValidationNode.ModelStateKey;
				if (bindingContext.ModelState.IsValidField(modelStateKey))
				{
					MutableObjectModelBinder.RunValidator(requiredValidator, bindingContext, propertyMetadata, modelStateKey);
				}
			}
			if (obj == null)
			{
				if (!TypeHelper.TypeAllowsNullValue(propertyDescriptor.PropertyType))
				{
					goto IL_B8;
				}
			}
			try
			{
				propertyDescriptor.SetValue(bindingContext.Model, obj);
				return;
			}
			catch (Exception exception)
			{
				string modelStateKey2 = dtoResult.ValidationNode.ModelStateKey;
				if (bindingContext.ModelState.IsValidField(modelStateKey2))
				{
					bindingContext.ModelState.AddModelError(modelStateKey2, exception);
				}
				return;
			}
			IL_B8:
			string modelStateKey3 = dtoResult.ValidationNode.ModelStateKey;
			if (bindingContext.ModelState.IsValidField(modelStateKey3))
			{
				dtoResult.ValidationNode.Validated += MutableObjectModelBinder.CreateNullCheckFailedHandler(propertyMetadata, obj);
			}
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x000212C0 File Offset: 0x0001F4C0
		private static bool RunValidator(ModelValidator validator, ModelBindingContext bindingContext, ModelMetadata propertyMetadata, string modelStateKey)
		{
			bool result = false;
			if (validator != null)
			{
				foreach (ModelValidationResult modelValidationResult in validator.Validate(propertyMetadata, bindingContext.Model))
				{
					bindingContext.ModelState.AddModelError(modelStateKey, modelValidationResult.Message);
					result = true;
				}
			}
			return result;
		}
	}
}
