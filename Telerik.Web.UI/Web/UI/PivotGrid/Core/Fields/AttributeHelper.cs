using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x020006B1 RID: 1713
	internal static class AttributeHelper
	{
		// Token: 0x06003DB8 RID: 15800 RVA: 0x000C6E64 File Offset: 0x000C5064
		internal static string GetValueForDisplayName(PropertyInfo propertyInfo)
		{
			string valueForDisplayNameFromAttributes = AttributeHelper.GetValueForDisplayNameFromAttributes(propertyInfo);
			if (valueForDisplayNameFromAttributes != null)
			{
				return valueForDisplayNameFromAttributes;
			}
			return propertyInfo.Name;
		}

		// Token: 0x06003DB9 RID: 15801 RVA: 0x000C6E84 File Offset: 0x000C5084
		internal static bool GetValueForAutoGenerateField(PropertyInfo propertyInfo)
		{
			bool? valueForAutoGenerateFieldFromAttributes = AttributeHelper.GetValueForAutoGenerateFieldFromAttributes(propertyInfo);
			return valueForAutoGenerateFieldFromAttributes == null || valueForAutoGenerateFieldFromAttributes.Value;
		}

		// Token: 0x06003DBA RID: 15802 RVA: 0x000C6EAC File Offset: 0x000C50AC
		private static string GetValueForDisplayNameFromAttributes(PropertyInfo propertyInfo)
		{
			object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), true);
			if (customAttributes == null)
			{
				return null;
			}
			DisplayAttribute displayAttribute = customAttributes.OfType<DisplayAttribute>().FirstOrDefault<DisplayAttribute>();
			if (displayAttribute != null)
			{
				return displayAttribute.GetName();
			}
			DisplayNameAttribute displayNameAttribute = propertyInfo.GetCustomAttributes(typeof(DisplayNameAttribute), true).OfType<DisplayNameAttribute>().FirstOrDefault<DisplayNameAttribute>();
			if (displayNameAttribute != null)
			{
				return displayNameAttribute.DisplayName;
			}
			return null;
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x000C6F10 File Offset: 0x000C5110
		private static bool? GetValueForAutoGenerateFieldFromAttributes(PropertyInfo propertyInfo)
		{
			object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), true);
			if (customAttributes == null)
			{
				return null;
			}
			DisplayAttribute displayAttribute = customAttributes.OfType<DisplayAttribute>().FirstOrDefault<DisplayAttribute>();
			if (displayAttribute != null)
			{
				return displayAttribute.GetAutoGenerateField();
			}
			BrowsableAttribute browsableAttribute = propertyInfo.GetCustomAttributes(typeof(BrowsableAttribute), true).OfType<BrowsableAttribute>().FirstOrDefault<BrowsableAttribute>();
			if (browsableAttribute != null)
			{
				return new bool?(browsableAttribute.Browsable);
			}
			return null;
		}
	}
}
