using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc.Html
{
	// Token: 0x02000040 RID: 64
	public static class EnumHelper
	{
		// Token: 0x06000138 RID: 312 RVA: 0x00005B70 File Offset: 0x00003D70
		public static bool IsValidForEnumHelper(Type type)
		{
			bool result = false;
			if (type != null)
			{
				Type type2 = Nullable.GetUnderlyingType(type) ?? type;
				if (type2.IsEnum)
				{
					result = !EnumHelper.HasFlagsInternal(type2);
				}
			}
			return result;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005BA7 File Offset: 0x00003DA7
		public static bool IsValidForEnumHelper(ModelMetadata metadata)
		{
			return metadata != null && EnumHelper.IsValidForEnumHelper(metadata.ModelType);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005BBC File Offset: 0x00003DBC
		public static IList<SelectListItem> GetSelectList(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (!EnumHelper.IsValidForEnumHelper(type))
			{
				throw Error.Argument("type", MvcResources.EnumHelper_InvalidParameterType, new object[]
				{
					type.FullName
				});
			}
			IList<SelectListItem> list = new List<SelectListItem>();
			Type type2 = Nullable.GetUnderlyingType(type) ?? type;
			if (type2 != type)
			{
				list.Add(new SelectListItem
				{
					Text = string.Empty,
					Value = string.Empty
				});
			}
			foreach (FieldInfo fieldInfo in type2.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField))
			{
				object rawConstantValue = fieldInfo.GetRawConstantValue();
				list.Add(new SelectListItem
				{
					Text = EnumHelper.GetDisplayName(fieldInfo),
					Value = rawConstantValue.ToString()
				});
			}
			return list;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005CA0 File Offset: 0x00003EA0
		public static IList<SelectListItem> GetSelectList(ModelMetadata metadata)
		{
			if (metadata == null)
			{
				throw Error.ArgumentNull("metadata");
			}
			if (metadata.ModelType == null)
			{
				throw Error.Argument("metadata", MvcResources.EnumHelper_InvalidMetadataParameter, new object[0]);
			}
			if (!EnumHelper.IsValidForEnumHelper(metadata))
			{
				throw Error.Argument("metadata", MvcResources.EnumHelper_InvalidParameterType, new object[]
				{
					metadata.ModelType.FullName
				});
			}
			return EnumHelper.GetSelectList(metadata.ModelType);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005D18 File Offset: 0x00003F18
		public static IList<SelectListItem> GetSelectList(Type type, Enum value)
		{
			IList<SelectListItem> selectList = EnumHelper.GetSelectList(type);
			Type type2 = (value == null) ? null : value.GetType();
			if (type2 != null && type2 != type && type2 != Nullable.GetUnderlyingType(type))
			{
				throw Error.Argument("value", MvcResources.EnumHelper_InvalidValueParameter, new object[]
				{
					type2.FullName,
					type.FullName
				});
			}
			if (value == null && selectList.Count != 0 && string.IsNullOrEmpty(selectList[0].Value))
			{
				selectList[0].Selected = true;
			}
			else
			{
				string text = (value == null) ? "0" : value.ToString("d");
				bool flag = false;
				int num = selectList.Count - 1;
				while (!flag && num >= 0)
				{
					SelectListItem selectListItem = selectList[num];
					selectListItem.Selected = (text == selectListItem.Value);
					flag |= selectListItem.Selected;
					num--;
				}
				if (!flag)
				{
					if (selectList.Count != 0 && string.IsNullOrEmpty(selectList[0].Value))
					{
						selectList[0].Selected = true;
						selectList[0].Value = text;
					}
					else
					{
						selectList.Insert(0, new SelectListItem
						{
							Selected = true,
							Text = string.Empty,
							Value = text
						});
					}
				}
			}
			return selectList;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005E78 File Offset: 0x00004078
		public static IList<SelectListItem> GetSelectList(ModelMetadata metadata, Enum value)
		{
			if (metadata == null)
			{
				throw Error.ArgumentNull("metadata");
			}
			if (metadata.ModelType == null)
			{
				throw Error.Argument("metadata", MvcResources.EnumHelper_InvalidMetadataParameter, new object[0]);
			}
			if (!EnumHelper.IsValidForEnumHelper(metadata))
			{
				throw Error.Argument("metadata", MvcResources.EnumHelper_InvalidParameterType, new object[]
				{
					metadata.ModelType.FullName
				});
			}
			return EnumHelper.GetSelectList(metadata.ModelType, value);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005EF4 File Offset: 0x000040F4
		internal static bool HasFlags(Type type)
		{
			Type type2 = Nullable.GetUnderlyingType(type) ?? type;
			return EnumHelper.HasFlagsInternal(type2);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005F14 File Offset: 0x00004114
		private static bool HasFlagsInternal(Type type)
		{
			FlagsAttribute customAttribute = type.GetCustomAttribute(false);
			return customAttribute != null;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005F30 File Offset: 0x00004130
		private static string GetDisplayName(FieldInfo field)
		{
			DisplayAttribute customAttribute = field.GetCustomAttribute(false);
			if (customAttribute != null)
			{
				string name = customAttribute.GetName();
				if (!string.IsNullOrEmpty(name))
				{
					return name;
				}
			}
			return field.Name;
		}
	}
}
