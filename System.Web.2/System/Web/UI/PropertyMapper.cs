using System;
using System.ComponentModel;
using System.Reflection;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x020002F4 RID: 756
	internal sealed class PropertyMapper
	{
		// Token: 0x0600230A RID: 8970 RVA: 0x000030B5 File Offset: 0x000012B5
		private PropertyMapper()
		{
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x00072110 File Offset: 0x00070310
		internal static MemberInfo GetMemberInfo(Type ctrlType, string name, out string nameForCodeGen)
		{
			Type type = ctrlType;
			PropertyInfo propertyInfo = null;
			FieldInfo fieldInfo = null;
			string text = PropertyMapper.MapNameToPropertyName(name);
			nameForCodeGen = null;
			int i = 0;
			while (i < text.Length)
			{
				int num = text.IndexOf('.', i);
				string text2;
				if (num < 0)
				{
					text2 = text.Substring(i);
					i = text.Length;
				}
				else
				{
					text2 = text.Substring(i, num - i);
					i = num + 1;
				}
				BindingFlags bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
				try
				{
					propertyInfo = TargetFrameworkUtil.GetProperty(type, text2, bindingFlags, null, null, false);
				}
				catch (AmbiguousMatchException)
				{
					bindingFlags |= BindingFlags.DeclaredOnly;
					propertyInfo = TargetFrameworkUtil.GetProperty(type, text2, bindingFlags, null, null, false);
				}
				if (propertyInfo == null)
				{
					fieldInfo = TargetFrameworkUtil.GetField(type, text2, bindingFlags);
					if (fieldInfo == null)
					{
						nameForCodeGen = null;
						break;
					}
				}
				text2 = null;
				if (propertyInfo != null)
				{
					type = propertyInfo.PropertyType;
					text2 = propertyInfo.Name;
				}
				else
				{
					type = fieldInfo.FieldType;
					text2 = fieldInfo.Name;
				}
				if (!PropertyMapper.IsTypeCLSCompliant(type))
				{
					throw new HttpException(SR.GetString("Property_Not_ClsCompliant", new object[]
					{
						name,
						ctrlType.FullName,
						type.FullName
					}));
				}
				if (text2 != null)
				{
					if (nameForCodeGen == null)
					{
						nameForCodeGen = text2;
					}
					else
					{
						nameForCodeGen = nameForCodeGen + "." + text2;
					}
				}
			}
			if (propertyInfo != null)
			{
				return propertyInfo;
			}
			return fieldInfo;
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x00072268 File Offset: 0x00070468
		private static bool IsTypeCLSCompliant(Type type)
		{
			return !(type == typeof(sbyte)) && !(type == typeof(TypedReference)) && !(type == typeof(ushort)) && !(type == typeof(uint)) && !(type == typeof(ulong)) && !(type == typeof(UIntPtr));
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x000722E4 File Offset: 0x000704E4
		internal static string MapNameToPropertyName(string attrName)
		{
			return attrName.Replace('-', '.');
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x000722F0 File Offset: 0x000704F0
		internal static object LocatePropertyObject(object obj, string mappedName, out string propertyName, bool inDesigner)
		{
			object obj2 = obj;
			Type type = obj.GetType();
			propertyName = null;
			int i = 0;
			while (i < mappedName.Length)
			{
				int num = mappedName.IndexOf('.', i);
				if (num < 0)
				{
					break;
				}
				propertyName = mappedName.Substring(i, num - i);
				i = num + 1;
				obj2 = FastPropertyAccessor.GetProperty(obj2, propertyName, inDesigner);
				if (obj2 == null)
				{
					return null;
				}
			}
			if (i > 0)
			{
				propertyName = mappedName.Substring(i);
			}
			else
			{
				propertyName = mappedName;
			}
			return obj2;
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x00072358 File Offset: 0x00070558
		internal static PropertyDescriptor GetMappedPropertyDescriptor(object obj, string mappedName, out object childObject, out string propertyName, bool inDesigner)
		{
			childObject = PropertyMapper.LocatePropertyObject(obj, mappedName, out propertyName, inDesigner);
			if (childObject == null)
			{
				return null;
			}
			PropertyDescriptorCollection properties = TargetFrameworkUtil.GetProperties(childObject);
			return properties[propertyName];
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x00072388 File Offset: 0x00070588
		internal static void SetMappedPropertyValue(object obj, string mappedName, object value, bool inDesigner)
		{
			string propName;
			object obj2 = PropertyMapper.LocatePropertyObject(obj, mappedName, out propName, inDesigner);
			if (obj2 == null)
			{
				return;
			}
			FastPropertyAccessor.SetProperty(obj2, propName, value, inDesigner);
		}

		// Token: 0x04001C99 RID: 7321
		private const char PERSIST_CHAR = '-';

		// Token: 0x04001C9A RID: 7322
		private const char OM_CHAR = '.';

		// Token: 0x04001C9B RID: 7323
		private const string STR_OM_CHAR = ".";
	}
}
