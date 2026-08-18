using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Routing;

namespace System.Web.WebPages
{
	// Token: 0x02000056 RID: 86
	internal static class TypeHelper
	{
		// Token: 0x0600020E RID: 526 RVA: 0x00008678 File Offset: 0x00006878
		public static RouteValueDictionary ObjectToDictionary(object value)
		{
			RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
			if (value != null)
			{
				foreach (PropertyHelper propertyHelper in PropertyHelper.GetProperties(value))
				{
					routeValueDictionary.Add(propertyHelper.Name, propertyHelper.GetValue(value));
				}
			}
			return routeValueDictionary;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000086BC File Offset: 0x000068BC
		public static RouteValueDictionary ObjectToDictionaryUncached(object value)
		{
			RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
			if (value != null)
			{
				foreach (PropertyHelper propertyHelper in PropertyHelper.GetProperties(value))
				{
					routeValueDictionary.Add(propertyHelper.Name, propertyHelper.GetValue(value));
				}
			}
			return routeValueDictionary;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00008700 File Offset: 0x00006900
		public static void AddAnonymousObjectToDictionary(IDictionary<string, object> dictionary, object value)
		{
			RouteValueDictionary routeValueDictionary = TypeHelper.ObjectToDictionary(value);
			foreach (KeyValuePair<string, object> item in routeValueDictionary)
			{
				dictionary.Add(item);
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00008758 File Offset: 0x00006958
		public static bool IsAnonymousType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false) && type.IsGenericType && type.Name.Contains("AnonymousType") && (type.Name.StartsWith("<>", StringComparison.OrdinalIgnoreCase) || type.Name.StartsWith("VB$", StringComparison.OrdinalIgnoreCase)))
			{
				TypeAttributes attributes = type.Attributes;
				return 0 == 0;
			}
			return false;
		}
	}
}
