using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Globalization;
using System.Linq;
using System.Web.Http.Internal;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Properties;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000126 RID: 294
	internal static class ModelBindingHelper
	{
		// Token: 0x06000717 RID: 1815 RVA: 0x000176DC File Offset: 0x000158DC
		internal static TModel CastOrDefault<TModel>(object model)
		{
			if (!(model is TModel))
			{
				return default(TModel);
			}
			return (TModel)((object)model);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00017701 File Offset: 0x00015901
		internal static string CreateIndexModelName(string parentName, int index)
		{
			return ModelBindingHelper.CreateIndexModelName(parentName, index.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00017715 File Offset: 0x00015915
		internal static string CreateIndexModelName(string parentName, string index)
		{
			if (parentName.Length != 0)
			{
				return parentName + "[" + index + "]";
			}
			return "[" + index + "]";
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00017741 File Offset: 0x00015941
		internal static string CreatePropertyModelName(string prefix, string propertyName)
		{
			if (string.IsNullOrEmpty(prefix))
			{
				return propertyName ?? string.Empty;
			}
			if (string.IsNullOrEmpty(propertyName))
			{
				return prefix ?? string.Empty;
			}
			return prefix + "." + propertyName;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00017775 File Offset: 0x00015975
		internal static string ConcatenateKeys(string prefix, string suffix)
		{
			if (string.IsNullOrEmpty(suffix))
			{
				return prefix;
			}
			if (!suffix.StartsWith("[", StringComparison.Ordinal))
			{
				return prefix + "." + suffix;
			}
			return prefix + suffix;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x000177A4 File Offset: 0x000159A4
		internal static IModelBinder GetPossibleBinderInstance(Type closedModelType, Type openModelType, Type openBinderType)
		{
			Type[] typeArgumentsIfMatch = TypeHelper.GetTypeArgumentsIfMatch(closedModelType, openModelType);
			if (typeArgumentsIfMatch == null)
			{
				return null;
			}
			return (IModelBinder)Activator.CreateInstance(openBinderType.MakeGenericType(typeArgumentsIfMatch));
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x000177D0 File Offset: 0x000159D0
		internal static object[] RawValueToObjectArray(object rawValue)
		{
			if (rawValue is string)
			{
				return new object[]
				{
					rawValue
				};
			}
			object[] array = rawValue as object[];
			if (array != null)
			{
				return array;
			}
			IEnumerable enumerable = rawValue as IEnumerable;
			if (enumerable != null)
			{
				return enumerable.Cast<object>().ToArray<object>();
			}
			return new object[]
			{
				rawValue
			};
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00017820 File Offset: 0x00015A20
		internal static void ReplaceEmptyStringWithNull(ModelMetadata modelMetadata, ref object model)
		{
			if (model is string && modelMetadata.ConvertEmptyStringToNull && string.IsNullOrWhiteSpace(model as string))
			{
				model = null;
			}
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00017844 File Offset: 0x00015A44
		internal static bool TryGetProviderFromAttribute(Type modelType, ModelBinderAttribute modelBinderAttribute, out ModelBinderProvider provider)
		{
			if (modelBinderAttribute.BinderType == null)
			{
				provider = null;
				return false;
			}
			if (typeof(ModelBinderProvider).IsAssignableFrom(modelBinderAttribute.BinderType))
			{
				provider = (ModelBinderProvider)Activator.CreateInstance(modelBinderAttribute.BinderType);
			}
			else
			{
				if (!typeof(IModelBinder).IsAssignableFrom(modelBinderAttribute.BinderType))
				{
					throw Error.InvalidOperation(SRResources.ModelBinderProviderCollection_InvalidBinderType, new object[]
					{
						modelBinderAttribute.BinderType,
						typeof(ModelBinderProvider),
						typeof(IModelBinder)
					});
				}
				Type type = modelBinderAttribute.BinderType.IsGenericTypeDefinition ? modelBinderAttribute.BinderType.MakeGenericType(modelType.GetGenericArguments()) : modelBinderAttribute.BinderType;
				IModelBinder modelBinder = (IModelBinder)Activator.CreateInstance(type);
				provider = new SimpleModelBinderProvider(modelType, modelBinder)
				{
					SuppressPrefixCheck = modelBinderAttribute.SuppressPrefixCheck
				};
			}
			return true;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001792C File Offset: 0x00015B2C
		internal static bool TryGetProviderFromAttributes(Type modelType, out ModelBinderProvider provider)
		{
			ModelBinderAttribute modelBinderAttribute = ModelBindingHelper.GetModelBinderAttribute(modelType);
			if (modelBinderAttribute == null)
			{
				provider = null;
				return false;
			}
			return ModelBindingHelper.TryGetProviderFromAttribute(modelType, modelBinderAttribute, out provider);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00017950 File Offset: 0x00015B50
		private static ModelBinderAttribute GetModelBinderAttribute(Type modelType)
		{
			ModelBinderAttribute modelBinderAttribute;
			if (!ModelBindingHelper._modelBinderAttributeCache.TryGetValue(modelType, out modelBinderAttribute))
			{
				modelBinderAttribute = TypeDescriptorHelper.Get(modelType).GetAttributes().OfType<ModelBinderAttribute>().FirstOrDefault<ModelBinderAttribute>();
				ModelBindingHelper._modelBinderAttributeCache.TryAdd(modelType, modelBinderAttribute);
			}
			return modelBinderAttribute;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00017990 File Offset: 0x00015B90
		internal static void ValidateBindingContext(ModelBindingContext bindingContext)
		{
			if (bindingContext == null)
			{
				throw Error.ArgumentNull("bindingContext");
			}
			if (bindingContext.ModelMetadata == null)
			{
				throw Error.Argument("bindingContext", SRResources.ModelBinderUtil_ModelMetadataCannotBeNull, new object[0]);
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x000179C0 File Offset: 0x00015BC0
		internal static void ValidateBindingContext(ModelBindingContext bindingContext, Type requiredType, bool allowNullModel)
		{
			ModelBindingHelper.ValidateBindingContext(bindingContext);
			if (bindingContext.ModelType != requiredType)
			{
				throw Error.Argument("bindingContext", SRResources.ModelBinderUtil_ModelTypeIsWrong, new object[]
				{
					bindingContext.ModelType,
					requiredType
				});
			}
			if (!allowNullModel && bindingContext.Model == null)
			{
				throw Error.Argument("bindingContext", SRResources.ModelBinderUtil_ModelCannotBeNull, new object[]
				{
					requiredType
				});
			}
			if (bindingContext.Model != null && !requiredType.IsInstanceOfType(bindingContext.Model))
			{
				throw Error.Argument("bindingContext", SRResources.ModelBinderUtil_ModelInstanceIsWrong, new object[]
				{
					bindingContext.Model.GetType(),
					requiredType
				});
			}
		}

		// Token: 0x04000203 RID: 515
		private static readonly ConcurrentDictionary<Type, ModelBinderAttribute> _modelBinderAttributeCache = new ConcurrentDictionary<Type, ModelBinderAttribute>();
	}
}
