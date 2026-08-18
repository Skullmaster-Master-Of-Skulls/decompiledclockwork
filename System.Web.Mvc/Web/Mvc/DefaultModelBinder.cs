using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020001AD RID: 429
	public class DefaultModelBinder : IModelBinder
	{
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x0001F217 File Offset: 0x0001D417
		// (set) Token: 0x06000BF2 RID: 3058 RVA: 0x0001F232 File Offset: 0x0001D432
		protected internal ModelBinderDictionary Binders
		{
			get
			{
				if (this._binders == null)
				{
					this._binders = ModelBinders.Binders;
				}
				return this._binders;
			}
			set
			{
				this._binders = value;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0001F23B File Offset: 0x0001D43B
		// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x0001F24B File Offset: 0x0001D44B
		public static string ResourceClassKey
		{
			get
			{
				return DefaultModelBinder._resourceClassKey ?? string.Empty;
			}
			set
			{
				DefaultModelBinder._resourceClassKey = value;
			}
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0001F253 File Offset: 0x0001D453
		private static void AddValueRequiredMessageToModelState(ControllerContext controllerContext, ModelStateDictionary modelState, string modelStateKey, Type elementType, object value)
		{
			if (value == null && !TypeHelpers.TypeAllowsNullValue(elementType) && modelState.IsValidField(modelStateKey))
			{
				modelState.AddModelError(modelStateKey, DefaultModelBinder.GetValueRequiredResource(controllerContext));
			}
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0001F278 File Offset: 0x0001D478
		internal void BindComplexElementalModel(ControllerContext controllerContext, ModelBindingContext bindingContext, object model)
		{
			ModelBindingContext bindingContext2 = this.CreateComplexElementalModelBindingContext(controllerContext, bindingContext, model);
			if (this.OnModelUpdating(controllerContext, bindingContext2))
			{
				this.BindProperties(controllerContext, bindingContext2);
				this.OnModelUpdated(controllerContext, bindingContext2);
			}
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0001F2D4 File Offset: 0x0001D4D4
		internal object BindComplexModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			object model = bindingContext.Model;
			Type modelType = bindingContext.ModelType;
			if (model == null && modelType.IsArray)
			{
				Type elementType = modelType.GetElementType();
				Type modelType2 = typeof(List<>).MakeGenericType(new Type[]
				{
					elementType
				});
				object collection = this.CreateModel(controllerContext, bindingContext, modelType2);
				ModelBindingContext bindingContext2 = new ModelBindingContext
				{
					ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => collection, modelType2),
					ModelName = bindingContext.ModelName,
					ModelState = bindingContext.ModelState,
					PropertyFilter = bindingContext.PropertyFilter,
					ValueProvider = bindingContext.ValueProvider
				};
				IList list = (IList)this.UpdateCollection(controllerContext, bindingContext2, elementType);
				if (list == null)
				{
					return null;
				}
				Array array = Array.CreateInstance(elementType, list.Count);
				list.CopyTo(array, 0);
				return array;
			}
			else
			{
				if (model == null)
				{
					model = this.CreateModel(controllerContext, bindingContext, modelType);
				}
				Type type = TypeHelpers.ExtractGenericInterface(modelType, typeof(IDictionary<, >));
				if (type != null)
				{
					Type[] genericArguments = type.GetGenericArguments();
					Type keyType = genericArguments[0];
					Type valueType = genericArguments[1];
					ModelBindingContext modelBindingContext = new ModelBindingContext();
					modelBindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, modelType);
					modelBindingContext.ModelName = bindingContext.ModelName;
					modelBindingContext.ModelState = bindingContext.ModelState;
					modelBindingContext.PropertyFilter = bindingContext.PropertyFilter;
					modelBindingContext.ValueProvider = bindingContext.ValueProvider;
					ModelBindingContext bindingContext3 = modelBindingContext;
					return this.UpdateDictionary(controllerContext, bindingContext3, keyType, valueType);
				}
				Type type2 = TypeHelpers.ExtractGenericInterface(modelType, typeof(IEnumerable<>));
				if (type2 != null)
				{
					Type type3 = type2.GetGenericArguments()[0];
					Type type4 = typeof(ICollection<>).MakeGenericType(new Type[]
					{
						type3
					});
					if (type4.IsInstanceOfType(model))
					{
						ModelBindingContext modelBindingContext2 = new ModelBindingContext();
						modelBindingContext2.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, modelType);
						modelBindingContext2.ModelName = bindingContext.ModelName;
						modelBindingContext2.ModelState = bindingContext.ModelState;
						modelBindingContext2.PropertyFilter = bindingContext.PropertyFilter;
						modelBindingContext2.ValueProvider = bindingContext.ValueProvider;
						ModelBindingContext bindingContext4 = modelBindingContext2;
						return this.UpdateCollection(controllerContext, bindingContext4, type3);
					}
				}
				this.BindComplexElementalModel(controllerContext, bindingContext, model);
				return model;
			}
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0001F59C File Offset: 0x0001D79C
		public virtual object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			if (bindingContext == null)
			{
				throw new ArgumentNullException("bindingContext");
			}
			bool flag = false;
			if (!string.IsNullOrEmpty(bindingContext.ModelName) && !bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName))
			{
				if (!bindingContext.FallbackToEmptyPrefix)
				{
					return null;
				}
				bindingContext = new ModelBindingContext
				{
					ModelMetadata = bindingContext.ModelMetadata,
					ModelState = bindingContext.ModelState,
					PropertyFilter = bindingContext.PropertyFilter,
					ValueProvider = bindingContext.ValueProvider
				};
				flag = true;
			}
			if (!flag)
			{
				bool flag2 = DefaultModelBinder.ShouldPerformRequestValidation(controllerContext, bindingContext);
				ValueProviderResult value = bindingContext.UnvalidatedValueProvider.GetValue(bindingContext.ModelName, !flag2);
				if (value != null)
				{
					return this.BindSimpleModel(controllerContext, bindingContext, value);
				}
			}
			if (!bindingContext.ModelMetadata.IsComplexType)
			{
				return null;
			}
			return this.BindComplexModel(controllerContext, bindingContext);
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0001F66C File Offset: 0x0001D86C
		private void BindProperties(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			PropertyDescriptorCollection modelProperties = this.GetModelProperties(controllerContext, bindingContext);
			Predicate<string> propertyFilter = bindingContext.PropertyFilter;
			for (int i = 0; i < modelProperties.Count; i++)
			{
				PropertyDescriptor propertyDescriptor = modelProperties[i];
				if (DefaultModelBinder.ShouldUpdateProperty(propertyDescriptor, propertyFilter))
				{
					this.BindProperty(controllerContext, bindingContext, propertyDescriptor);
				}
			}
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0001F6D4 File Offset: 0x0001D8D4
		protected virtual void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor)
		{
			string text = DefaultModelBinder.CreateSubPropertyName(bindingContext.ModelName, propertyDescriptor.Name);
			if (!bindingContext.ValueProvider.ContainsPrefix(text))
			{
				return;
			}
			IModelBinder binder = this.Binders.GetBinder(propertyDescriptor.PropertyType);
			object value = propertyDescriptor.GetValue(bindingContext.Model);
			ModelMetadata modelMetadata = bindingContext.PropertyMetadata[propertyDescriptor.Name];
			modelMetadata.Model = value;
			ModelBindingContext bindingContext2 = new ModelBindingContext
			{
				ModelMetadata = modelMetadata,
				ModelName = text,
				ModelState = bindingContext.ModelState,
				ValueProvider = bindingContext.ValueProvider
			};
			object propertyValue = this.GetPropertyValue(controllerContext, bindingContext2, propertyDescriptor, binder);
			modelMetadata.Model = propertyValue;
			ModelState modelState = bindingContext.ModelState[text];
			if (modelState == null || modelState.Errors.Count == 0)
			{
				if (this.OnPropertyValidating(controllerContext, bindingContext, propertyDescriptor, propertyValue))
				{
					this.SetProperty(controllerContext, bindingContext, propertyDescriptor, propertyValue);
					this.OnPropertyValidated(controllerContext, bindingContext, propertyDescriptor, propertyValue);
					return;
				}
			}
			else
			{
				this.SetProperty(controllerContext, bindingContext, propertyDescriptor, propertyValue);
				foreach (ModelError modelError in (from err in modelState.Errors
				where string.IsNullOrEmpty(err.ErrorMessage) && err.Exception != null
				select err).ToList<ModelError>())
				{
					for (Exception ex = modelError.Exception; ex != null; ex = ex.InnerException)
					{
						if (ex is FormatException || ex is OverflowException)
						{
							string displayName = modelMetadata.GetDisplayName();
							string valueInvalidResource = DefaultModelBinder.GetValueInvalidResource(controllerContext);
							string errorMessage = string.Format(CultureInfo.CurrentCulture, valueInvalidResource, new object[]
							{
								modelState.Value.AttemptedValue,
								displayName
							});
							modelState.Errors.Remove(modelError);
							modelState.Errors.Add(errorMessage);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0001F8CC File Offset: 0x0001DACC
		internal object BindSimpleModel(ControllerContext controllerContext, ModelBindingContext bindingContext, ValueProviderResult valueProviderResult)
		{
			bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);
			if (bindingContext.ModelType.IsInstanceOfType(valueProviderResult.RawValue))
			{
				return valueProviderResult.RawValue;
			}
			if (bindingContext.ModelType != typeof(string))
			{
				if (bindingContext.ModelType.IsArray)
				{
					return DefaultModelBinder.ConvertProviderResult(bindingContext.ModelState, bindingContext.ModelName, valueProviderResult, bindingContext.ModelType);
				}
				Type type = TypeHelpers.ExtractGenericInterface(bindingContext.ModelType, typeof(IEnumerable<>));
				if (type != null)
				{
					object obj = this.CreateModel(controllerContext, bindingContext, bindingContext.ModelType);
					Type type2 = type.GetGenericArguments()[0];
					Type destinationType = type2.MakeArrayType();
					object newContents = DefaultModelBinder.ConvertProviderResult(bindingContext.ModelState, bindingContext.ModelName, valueProviderResult, destinationType);
					Type type3 = typeof(ICollection<>).MakeGenericType(new Type[]
					{
						type2
					});
					if (type3.IsInstanceOfType(obj))
					{
						DefaultModelBinder.CollectionHelpers.ReplaceCollection(type2, obj, newContents);
					}
					return obj;
				}
			}
			return DefaultModelBinder.ConvertProviderResult(bindingContext.ModelState, bindingContext.ModelName, valueProviderResult, bindingContext.ModelType);
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0001F9ED File Offset: 0x0001DBED
		private static bool CanUpdateReadonlyTypedReference(Type type)
		{
			return !type.IsValueType && !type.IsArray && !(type == typeof(string));
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0001FA18 File Offset: 0x0001DC18
		private static object ConvertProviderResult(ModelStateDictionary modelState, string modelStateKey, ValueProviderResult valueProviderResult, Type destinationType)
		{
			object result;
			try
			{
				object obj = valueProviderResult.ConvertTo(destinationType);
				result = obj;
			}
			catch (Exception exception)
			{
				modelState.AddModelError(modelStateKey, exception);
				result = null;
			}
			return result;
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0001FA84 File Offset: 0x0001DC84
		internal ModelBindingContext CreateComplexElementalModelBindingContext(ControllerContext controllerContext, ModelBindingContext bindingContext, object model)
		{
			BindAttribute bindAttr = (BindAttribute)this.GetTypeDescriptor(controllerContext, bindingContext).GetAttributes()[typeof(BindAttribute)];
			Predicate<string> propertyFilter = (bindAttr != null) ? ((string propertyName) => bindAttr.IsPropertyAllowed(propertyName) && bindingContext.PropertyFilter(propertyName)) : bindingContext.PropertyFilter;
			return new ModelBindingContext
			{
				ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, bindingContext.ModelType),
				ModelName = bindingContext.ModelName,
				ModelState = bindingContext.ModelState,
				PropertyFilter = propertyFilter,
				ValueProvider = bindingContext.ValueProvider
			};
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0001FB60 File Offset: 0x0001DD60
		protected virtual object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
		{
			Type type = modelType;
			if (modelType.IsGenericType)
			{
				Type genericTypeDefinition = modelType.GetGenericTypeDefinition();
				if (genericTypeDefinition == typeof(IDictionary<, >))
				{
					type = typeof(Dictionary<, >).MakeGenericType(modelType.GetGenericArguments());
				}
				else if (genericTypeDefinition == typeof(IEnumerable<>) || genericTypeDefinition == typeof(ICollection<>) || genericTypeDefinition == typeof(IList<>))
				{
					type = typeof(List<>).MakeGenericType(modelType.GetGenericArguments());
				}
			}
			object result;
			try
			{
				result = Activator.CreateInstance(type);
			}
			catch (MissingMethodException originalException)
			{
				MissingMethodException ex = TypeHelpers.EnsureDebuggableException(originalException, type.FullName);
				if (ex != null)
				{
					throw ex;
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0001FC28 File Offset: 0x0001DE28
		protected static string CreateSubIndexName(string prefix, int index)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}[{1}]", new object[]
			{
				prefix,
				index
			});
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0001FC5C File Offset: 0x0001DE5C
		protected static string CreateSubIndexName(string prefix, string index)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}[{1}]", new object[]
			{
				prefix,
				index
			});
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0001FC88 File Offset: 0x0001DE88
		protected internal static string CreateSubPropertyName(string prefix, string propertyName)
		{
			if (string.IsNullOrEmpty(prefix))
			{
				return propertyName;
			}
			if (string.IsNullOrEmpty(propertyName))
			{
				return prefix;
			}
			return prefix + "." + propertyName;
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0001FCC0 File Offset: 0x0001DEC0
		protected IEnumerable<PropertyDescriptor> GetFilteredModelProperties(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			PropertyDescriptorCollection modelProperties = this.GetModelProperties(controllerContext, bindingContext);
			Predicate<string> propertyFilter = bindingContext.PropertyFilter;
			return from PropertyDescriptor property in modelProperties
			where DefaultModelBinder.ShouldUpdateProperty(property, propertyFilter)
			select property;
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0001FD00 File Offset: 0x0001DF00
		private static void GetIndexes(ModelBindingContext bindingContext, out bool stopOnIndexNotFound, out IEnumerable<string> indexes)
		{
			string key = DefaultModelBinder.CreateSubPropertyName(bindingContext.ModelName, "index");
			ValueProviderResult value = bindingContext.ValueProvider.GetValue(key);
			if (value != null)
			{
				string[] array = value.ConvertTo(typeof(string[])) as string[];
				if (array != null)
				{
					stopOnIndexNotFound = false;
					indexes = array;
					return;
				}
			}
			stopOnIndexNotFound = true;
			indexes = DefaultModelBinder.GetZeroBasedIndexes();
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0001FD58 File Offset: 0x0001DF58
		protected virtual PropertyDescriptorCollection GetModelProperties(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			return this.GetTypeDescriptor(controllerContext, bindingContext).GetProperties();
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0001FD68 File Offset: 0x0001DF68
		protected virtual object GetPropertyValue(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, IModelBinder propertyBinder)
		{
			object obj = propertyBinder.BindModel(controllerContext, bindingContext);
			if (bindingContext.ModelMetadata.ConvertEmptyStringToNull && object.Equals(obj, string.Empty))
			{
				return null;
			}
			return obj;
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0001FD9C File Offset: 0x0001DF9C
		protected virtual ICustomTypeDescriptor GetTypeDescriptor(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			return TypeDescriptorHelper.Get(bindingContext.ModelType);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0001FDAC File Offset: 0x0001DFAC
		private static string GetUserResourceString(ControllerContext controllerContext, string resourceName)
		{
			string result = null;
			if (!string.IsNullOrEmpty(DefaultModelBinder.ResourceClassKey) && controllerContext != null && controllerContext.HttpContext != null)
			{
				result = (controllerContext.HttpContext.GetGlobalResourceObject(DefaultModelBinder.ResourceClassKey, resourceName, CultureInfo.CurrentUICulture) as string);
			}
			return result;
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0001FDEF File Offset: 0x0001DFEF
		private static string GetValueInvalidResource(ControllerContext controllerContext)
		{
			return DefaultModelBinder.GetUserResourceString(controllerContext, "PropertyValueInvalid") ?? MvcResources.DefaultModelBinder_ValueInvalid;
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0001FE05 File Offset: 0x0001E005
		private static string GetValueRequiredResource(ControllerContext controllerContext)
		{
			return DefaultModelBinder.GetUserResourceString(controllerContext, "PropertyValueRequired") ?? MvcResources.DefaultModelBinder_ValueRequired;
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0001FEF8 File Offset: 0x0001E0F8
		private static IEnumerable<string> GetZeroBasedIndexes()
		{
			int i = 0;
			for (;;)
			{
				yield return i.ToString(CultureInfo.InvariantCulture);
				i++;
			}
			yield break;
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0001FF0E File Offset: 0x0001E10E
		protected static bool IsModelValid(ModelBindingContext bindingContext)
		{
			if (bindingContext == null)
			{
				throw new ArgumentNullException("bindingContext");
			}
			if (string.IsNullOrEmpty(bindingContext.ModelName))
			{
				return bindingContext.ModelState.IsValid;
			}
			return bindingContext.ModelState.IsValidField(bindingContext.ModelName);
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0001FF48 File Offset: 0x0001E148
		protected virtual void OnModelUpdated(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
			foreach (ModelValidationResult modelValidationResult in ModelValidator.GetModelValidator(bindingContext.ModelMetadata, controllerContext).Validate(null))
			{
				string key = DefaultModelBinder.CreateSubPropertyName(bindingContext.ModelName, modelValidationResult.MemberName);
				if (!dictionary.ContainsKey(key))
				{
					dictionary[key] = bindingContext.ModelState.IsValidField(key);
				}
				if (dictionary[key])
				{
					bindingContext.ModelState.AddModelError(key, modelValidationResult.Message);
				}
			}
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0001FFF0 File Offset: 0x0001E1F0
		protected virtual bool OnModelUpdating(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			return true;
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0001FFF3 File Offset: 0x0001E1F3
		protected virtual void OnPropertyValidated(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, object value)
		{
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0001FFF5 File Offset: 0x0001E1F5
		protected virtual bool OnPropertyValidating(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, object value)
		{
			return true;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00020000 File Offset: 0x0001E200
		protected virtual void SetProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, object value)
		{
			ModelMetadata modelMetadata = bindingContext.PropertyMetadata[propertyDescriptor.Name];
			modelMetadata.Model = value;
			string key = DefaultModelBinder.CreateSubPropertyName(bindingContext.ModelName, modelMetadata.PropertyName);
			if (value == null && bindingContext.ModelState.IsValidField(key))
			{
				ModelValidator modelValidator = (from v in ModelValidatorProviders.Providers.GetValidators(modelMetadata, controllerContext)
				where v.IsRequired
				select v).FirstOrDefault<ModelValidator>();
				if (modelValidator != null)
				{
					foreach (ModelValidationResult modelValidationResult in modelValidator.Validate(bindingContext.Model))
					{
						bindingContext.ModelState.AddModelError(key, modelValidationResult.Message);
					}
				}
			}
			bool flag = value == null && !TypeHelpers.TypeAllowsNullValue(propertyDescriptor.PropertyType);
			if (!propertyDescriptor.IsReadOnly && !flag)
			{
				try
				{
					propertyDescriptor.SetValue(bindingContext.Model, value);
				}
				catch (Exception exception)
				{
					if (bindingContext.ModelState.IsValidField(key))
					{
						bindingContext.ModelState.AddModelError(key, exception);
					}
				}
			}
			if (flag && bindingContext.ModelState.IsValidField(key))
			{
				bindingContext.ModelState.AddModelError(key, DefaultModelBinder.GetValueRequiredResource(controllerContext));
			}
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00020160 File Offset: 0x0001E360
		private static bool ShouldPerformRequestValidation(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			return controllerContext == null || controllerContext.Controller == null || bindingContext == null || bindingContext.ModelMetadata == null || (controllerContext.Controller.ValidateRequest && bindingContext.ModelMetadata.RequestValidationEnabled);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00020194 File Offset: 0x0001E394
		private static bool ShouldUpdateProperty(PropertyDescriptor property, Predicate<string> propertyFilter)
		{
			return (!property.IsReadOnly || DefaultModelBinder.CanUpdateReadonlyTypedReference(property.PropertyType)) && propertyFilter(property.Name);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x000201C0 File Offset: 0x0001E3C0
		internal object UpdateCollection(ControllerContext controllerContext, ModelBindingContext bindingContext, Type elementType)
		{
			bool flag;
			IEnumerable<string> enumerable;
			DefaultModelBinder.GetIndexes(bindingContext, out flag, out enumerable);
			IModelBinder binder = this.Binders.GetBinder(elementType);
			List<object> list = new List<object>();
			foreach (string index in enumerable)
			{
				string text = DefaultModelBinder.CreateSubIndexName(bindingContext.ModelName, index);
				if (!bindingContext.ValueProvider.ContainsPrefix(text))
				{
					if (flag)
					{
						break;
					}
				}
				else
				{
					ModelBindingContext bindingContext2 = new ModelBindingContext
					{
						ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, elementType),
						ModelName = text,
						ModelState = bindingContext.ModelState,
						PropertyFilter = bindingContext.PropertyFilter,
						ValueProvider = bindingContext.ValueProvider
					};
					object obj = binder.BindModel(controllerContext, bindingContext2);
					DefaultModelBinder.AddValueRequiredMessageToModelState(controllerContext, bindingContext.ModelState, text, elementType, obj);
					list.Add(obj);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			object model = bindingContext.Model;
			DefaultModelBinder.CollectionHelpers.ReplaceCollection(elementType, model, list);
			return model;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x000202DC File Offset: 0x0001E4DC
		internal object UpdateDictionary(ControllerContext controllerContext, ModelBindingContext bindingContext, Type keyType, Type valueType)
		{
			bool flag;
			IEnumerable<string> enumerable;
			DefaultModelBinder.GetIndexes(bindingContext, out flag, out enumerable);
			IModelBinder binder = this.Binders.GetBinder(keyType);
			IModelBinder binder2 = this.Binders.GetBinder(valueType);
			List<KeyValuePair<object, object>> list = new List<KeyValuePair<object, object>>();
			foreach (string index in enumerable)
			{
				string prefix = DefaultModelBinder.CreateSubIndexName(bindingContext.ModelName, index);
				string text = DefaultModelBinder.CreateSubPropertyName(prefix, "key");
				string text2 = DefaultModelBinder.CreateSubPropertyName(prefix, "value");
				if (!bindingContext.ValueProvider.ContainsPrefix(text) || !bindingContext.ValueProvider.ContainsPrefix(text2))
				{
					if (flag)
					{
						break;
					}
				}
				else
				{
					ModelBindingContext bindingContext2 = new ModelBindingContext
					{
						ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, keyType),
						ModelName = text,
						ModelState = bindingContext.ModelState,
						ValueProvider = bindingContext.ValueProvider
					};
					object obj = binder.BindModel(controllerContext, bindingContext2);
					DefaultModelBinder.AddValueRequiredMessageToModelState(controllerContext, bindingContext.ModelState, text, keyType, obj);
					if (keyType.IsInstanceOfType(obj))
					{
						list.Add(DefaultModelBinder.CreateEntryForModel(controllerContext, bindingContext, valueType, binder2, text2, obj));
					}
				}
			}
			if (list.Count == 0)
			{
				IEnumerableValueProvider enumerableValueProvider = bindingContext.ValueProvider as IEnumerableValueProvider;
				if (enumerableValueProvider != null)
				{
					IDictionary<string, string> keysFromPrefix = enumerableValueProvider.GetKeysFromPrefix(bindingContext.ModelName);
					foreach (KeyValuePair<string, string> keyValuePair in keysFromPrefix)
					{
						list.Add(DefaultModelBinder.CreateEntryForModel(controllerContext, bindingContext, valueType, binder2, keyValuePair.Value, keyValuePair.Key));
					}
				}
			}
			object model = bindingContext.Model;
			DefaultModelBinder.CollectionHelpers.ReplaceDictionary(keyType, valueType, model, list);
			return model;
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x000204BC File Offset: 0x0001E6BC
		private static KeyValuePair<object, object> CreateEntryForModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type valueType, IModelBinder valueBinder, string modelName, object modelKey)
		{
			ModelBindingContext bindingContext2 = new ModelBindingContext
			{
				ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, valueType),
				ModelName = modelName,
				ModelState = bindingContext.ModelState,
				PropertyFilter = bindingContext.PropertyFilter,
				ValueProvider = bindingContext.ValueProvider
			};
			object value = valueBinder.BindModel(controllerContext, bindingContext2);
			DefaultModelBinder.AddValueRequiredMessageToModelState(controllerContext, bindingContext.ModelState, modelName, valueType, value);
			return new KeyValuePair<object, object>(modelKey, value);
		}

		// Token: 0x04000339 RID: 825
		private static string _resourceClassKey;

		// Token: 0x0400033A RID: 826
		private ModelBinderDictionary _binders;

		// Token: 0x020001AE RID: 430
		private static class CollectionHelpers
		{
			// Token: 0x06000C1A RID: 3098 RVA: 0x00020538 File Offset: 0x0001E738
			[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
			public static void ReplaceCollection(Type collectionType, object collection, object newContents)
			{
				MethodInfo methodInfo = DefaultModelBinder.CollectionHelpers._replaceCollectionMethod.MakeGenericMethod(new Type[]
				{
					collectionType
				});
				methodInfo.Invoke(null, new object[]
				{
					collection,
					newContents
				});
			}

			// Token: 0x06000C1B RID: 3099 RVA: 0x00020574 File Offset: 0x0001E774
			private static void ReplaceCollectionImpl<T>(ICollection<T> collection, IEnumerable newContents)
			{
				collection.Clear();
				if (newContents != null)
				{
					foreach (object obj in newContents)
					{
						T item = (obj is T) ? ((T)((object)obj)) : default(T);
						collection.Add(item);
					}
				}
			}

			// Token: 0x06000C1C RID: 3100 RVA: 0x000205EC File Offset: 0x0001E7EC
			[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
			public static void ReplaceDictionary(Type keyType, Type valueType, object dictionary, object newContents)
			{
				MethodInfo methodInfo = DefaultModelBinder.CollectionHelpers._replaceDictionaryMethod.MakeGenericMethod(new Type[]
				{
					keyType,
					valueType
				});
				methodInfo.Invoke(null, new object[]
				{
					dictionary,
					newContents
				});
			}

			// Token: 0x06000C1D RID: 3101 RVA: 0x0002062C File Offset: 0x0001E82C
			private static void ReplaceDictionaryImpl<TKey, TValue>(IDictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<object, object>> newContents)
			{
				dictionary.Clear();
				foreach (KeyValuePair<object, object> keyValuePair in newContents)
				{
					if (keyValuePair.Key is TKey)
					{
						TKey key = (TKey)((object)keyValuePair.Key);
						TValue value = (keyValuePair.Value is TValue) ? ((TValue)((object)keyValuePair.Value)) : default(TValue);
						dictionary[key] = value;
					}
				}
			}

			// Token: 0x0400033D RID: 829
			private static readonly MethodInfo _replaceCollectionMethod = typeof(DefaultModelBinder.CollectionHelpers).GetMethod("ReplaceCollectionImpl", BindingFlags.Static | BindingFlags.NonPublic);

			// Token: 0x0400033E RID: 830
			private static readonly MethodInfo _replaceDictionaryMethod = typeof(DefaultModelBinder.CollectionHelpers).GetMethod("ReplaceDictionaryImpl", BindingFlags.Static | BindingFlags.NonPublic);
		}
	}
}
