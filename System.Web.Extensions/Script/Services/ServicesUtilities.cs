using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Script.Services
{
	// Token: 0x020000F6 RID: 246
	internal static class ServicesUtilities
	{
		// Token: 0x06000D07 RID: 3335 RVA: 0x0002BC02 File Offset: 0x00029E02
		internal static string GetClientTypeName(string name)
		{
			return name.Replace('+', '_');
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0002BC10 File Offset: 0x00029E10
		internal static string GetClientTypeFromServerType(WebServiceData webServiceData, Type type)
		{
			if (webServiceData.ClientTypeNameDictionary.ContainsKey(type))
			{
				return webServiceData.ClientTypeNameDictionary[type];
			}
			if (type.IsEnum)
			{
				return ServicesUtilities.GetClientTypeName(type.FullName);
			}
			if (type == typeof(string) || type == typeof(char))
			{
				return "String";
			}
			if (type.IsPrimitive)
			{
				if (type == typeof(bool))
				{
					return "Boolean";
				}
				return "Number";
			}
			else
			{
				if (type.IsValueType)
				{
					if (type == typeof(DateTime))
					{
						return "Date";
					}
					if (type == typeof(Guid))
					{
						return "String";
					}
					if (type == typeof(decimal))
					{
						return "Number";
					}
				}
				if (typeof(IDictionary).IsAssignableFrom(type))
				{
					return "Object";
				}
				if (type.IsGenericType)
				{
					Type left = type;
					if (!type.IsGenericTypeDefinition)
					{
						left = type.GetGenericTypeDefinition();
					}
					if (left == typeof(IDictionary<, >))
					{
						return "Object";
					}
				}
				if (type.IsArray || typeof(IEnumerable).IsAssignableFrom(type))
				{
					return "Array";
				}
				return "";
			}
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0002BD5C File Offset: 0x00029F5C
		internal static Type UnwrapNullableType(Type type)
		{
			if (type.IsGenericType && !type.IsGenericTypeDefinition)
			{
				Type genericTypeDefinition = type.GetGenericTypeDefinition();
				if (genericTypeDefinition == typeof(Nullable<>))
				{
					return type.GetGenericArguments()[0];
				}
			}
			return type;
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0002BD9C File Offset: 0x00029F9C
		internal static string XmlSerializeObjectToString(object obj)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
			MemoryStream memoryStream = new MemoryStream();
			string result;
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8))
			{
				xmlSerializer.Serialize(xmlTextWriter, obj);
				memoryStream.Position = 0L;
				using (StreamReader streamReader = new StreamReader(memoryStream))
				{
					result = streamReader.ReadToEnd();
				}
			}
			return result;
		}
	}
}
