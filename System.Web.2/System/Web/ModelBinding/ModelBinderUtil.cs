using System;
using System.Collections;
using System.Globalization;
using System.Linq;

namespace System.Web.ModelBinding
{
	// Token: 0x0200067C RID: 1660
	internal static class ModelBinderUtil
	{
		// Token: 0x060050A7 RID: 20647 RVA: 0x001162F0 File Offset: 0x001144F0
		public static TModel CastOrDefault<TModel>(object model)
		{
			if (!(model is TModel))
			{
				return default(TModel);
			}
			return (TModel)((object)model);
		}

		// Token: 0x060050A8 RID: 20648 RVA: 0x00116315 File Offset: 0x00114515
		public static string CreateIndexModelName(string parentName, int index)
		{
			return ModelBinderUtil.CreateIndexModelName(parentName, index.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x060050A9 RID: 20649 RVA: 0x00116329 File Offset: 0x00114529
		public static string CreateIndexModelName(string parentName, string index)
		{
			if (parentName.Length != 0)
			{
				return parentName + "[" + index + "]";
			}
			return "[" + index + "]";
		}

		// Token: 0x060050AA RID: 20650 RVA: 0x00116355 File Offset: 0x00114555
		public static string CreatePropertyModelName(string prefix, string propertyName)
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

		// Token: 0x060050AB RID: 20651 RVA: 0x0011638C File Offset: 0x0011458C
		public static IModelBinder GetPossibleBinderInstance(Type closedModelType, Type openModelType, Type openBinderType)
		{
			Type[] typeArgumentsIfMatch = TypeHelpers.GetTypeArgumentsIfMatch(closedModelType, openModelType);
			if (typeArgumentsIfMatch == null)
			{
				return null;
			}
			return (IModelBinder)Activator.CreateInstance(openBinderType.MakeGenericType(typeArgumentsIfMatch));
		}

		// Token: 0x060050AC RID: 20652 RVA: 0x001163B8 File Offset: 0x001145B8
		public static object[] RawValueToObjectArray(object rawValue)
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

		// Token: 0x060050AD RID: 20653 RVA: 0x00116404 File Offset: 0x00114604
		public static void ReplaceEmptyStringWithNull(ModelMetadata modelMetadata, ref object model)
		{
			if (modelMetadata.ConvertEmptyStringToNull && ModelBinderUtil.StringIsEmptyOrWhitespace(model as string))
			{
				model = null;
			}
		}

		// Token: 0x060050AE RID: 20654 RVA: 0x00116420 File Offset: 0x00114620
		private static bool StringIsEmptyOrWhitespace(string s)
		{
			if (s == null)
			{
				return false;
			}
			if (s.Length != 0)
			{
				for (int i = 0; i < s.Length; i++)
				{
					if (!char.IsWhiteSpace(s[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060050AF RID: 20655 RVA: 0x0011645C File Offset: 0x0011465C
		public static void ValidateBindingContext(ModelBindingContext bindingContext)
		{
			if (bindingContext == null)
			{
				throw new ArgumentNullException("bindingContext");
			}
			if (bindingContext.ModelMetadata == null)
			{
				throw Error.ModelBinderUtil_ModelMetadataCannotBeNull();
			}
		}

		// Token: 0x060050B0 RID: 20656 RVA: 0x0011647C File Offset: 0x0011467C
		public static void ValidateBindingContext(ModelBindingContext bindingContext, Type requiredType, bool allowNullModel)
		{
			ModelBinderUtil.ValidateBindingContext(bindingContext);
			if (bindingContext.ModelType != requiredType)
			{
				throw Error.ModelBinderUtil_ModelTypeIsWrong(bindingContext.ModelType, requiredType);
			}
			if (!allowNullModel && bindingContext.Model == null)
			{
				throw Error.ModelBinderUtil_ModelCannotBeNull(requiredType);
			}
			if (bindingContext.Model != null && !requiredType.IsInstanceOfType(bindingContext.Model))
			{
				throw Error.ModelBinderUtil_ModelInstanceIsWrong(bindingContext.Model.GetType(), requiredType);
			}
		}
	}
}
