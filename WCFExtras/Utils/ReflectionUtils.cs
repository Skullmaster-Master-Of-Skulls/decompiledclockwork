using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace WCFExtras.Utils
{
	// Token: 0x02000004 RID: 4
	internal static class ReflectionUtils
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000022E4 File Offset: 0x000004E4
		internal static object GetValue(object obj, string propertyName)
		{
			MemberInfo memberInfo = obj.GetType().GetMember(propertyName, MemberTypes.Field | MemberTypes.Property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)[0];
			object value;
			if (memberInfo.MemberType == MemberTypes.Field)
			{
				value = ((FieldInfo)memberInfo).GetValue(obj);
			}
			else
			{
				value = ((PropertyInfo)memberInfo).GetValue(obj, null);
			}
			return value;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002338 File Offset: 0x00000538
		internal static void SetValue(object obj, string propertyName, object value)
		{
			MemberInfo memberInfo = obj.GetType().GetMember(propertyName, MemberTypes.Field | MemberTypes.Property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)[0];
			if (memberInfo.MemberType == MemberTypes.Field)
			{
				((FieldInfo)memberInfo).SetValue(obj, value);
			}
			else
			{
				((PropertyInfo)memberInfo).SetValue(obj, value, null);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002388 File Offset: 0x00000588
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

		// Token: 0x0600001A RID: 26 RVA: 0x00002440 File Offset: 0x00000640
		internal static DataContractAttribute GetDataContractAttribute(Type type)
		{
			return ReflectionUtils.GetAttribute<DataContractAttribute>(type);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002458 File Offset: 0x00000658
		private static T GetAttribute<T>(MemberInfo memberInfo) where T : class
		{
			object[] customAttributes = memberInfo.GetCustomAttributes(typeof(T), false);
			T result;
			if (customAttributes.Length > 0)
			{
				result = (T)((object)customAttributes[0]);
			}
			else
			{
				result = default(T);
			}
			return result;
		}
	}
}
