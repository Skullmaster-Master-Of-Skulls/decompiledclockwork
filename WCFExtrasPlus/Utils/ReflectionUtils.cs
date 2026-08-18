using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace WCFExtrasPlus.Utils
{
	// Token: 0x02000010 RID: 16
	internal static class ReflectionUtils
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002F98 File Offset: 0x00001198
		internal static object GetValue(object obj, string propertyName)
		{
			MemberInfo memberInfo = obj.GetType().GetMember(propertyName, MemberTypes.Field | MemberTypes.Property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)[0];
			if (memberInfo.MemberType == MemberTypes.Field)
			{
				return ((FieldInfo)memberInfo).GetValue(obj);
			}
			return ((PropertyInfo)memberInfo).GetValue(obj, null);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002FDC File Offset: 0x000011DC
		internal static void SetValue(object obj, string propertyName, object value)
		{
			MemberInfo memberInfo = obj.GetType().GetMember(propertyName, MemberTypes.Field | MemberTypes.Property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)[0];
			if (memberInfo.MemberType == MemberTypes.Field)
			{
				((FieldInfo)memberInfo).SetValue(obj, value);
				return;
			}
			((PropertyInfo)memberInfo).SetValue(obj, value, null);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003024 File Offset: 0x00001224
		internal static Dictionary<string, MemberInfo> GetEnumMembers(Type type)
		{
			bool flag = ReflectionUtils.GetDataContractAttribute(type) != null;
			FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
			Dictionary<string, MemberInfo> dictionary = new Dictionary<string, MemberInfo>(fields.Length);
			foreach (FieldInfo memberInfo in fields)
			{
				if (flag)
				{
					EnumMemberAttribute attribute = ReflectionUtils.GetAttribute<EnumMemberAttribute>(memberInfo);
					if (attribute != null)
					{
						string text = attribute.Value;
						if (string.IsNullOrEmpty(text))
						{
							text = memberInfo.Name;
						}
						dictionary.Add(text, memberInfo);
					}
				}
				else
				{
					dictionary.Add(memberInfo.Name, memberInfo);
				}
			}
			return dictionary;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000030AE File Offset: 0x000012AE
		internal static DataContractAttribute GetDataContractAttribute(Type type)
		{
			return ReflectionUtils.GetAttribute<DataContractAttribute>(type);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000030B8 File Offset: 0x000012B8
		private static T GetAttribute<T>(MemberInfo memberInfo) where T : class
		{
			object[] customAttributes = memberInfo.GetCustomAttributes(typeof(T), false);
			if (customAttributes.Length > 0)
			{
				return (T)((object)customAttributes[0]);
			}
			return default(T);
		}
	}
}
