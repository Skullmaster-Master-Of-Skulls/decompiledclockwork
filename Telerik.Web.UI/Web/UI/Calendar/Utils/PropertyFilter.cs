using System;
using System.ComponentModel;
using Telerik.Web.UI.Design.DatePickerAttributes;

namespace Telerik.Web.UI.Calendar.Utils
{
	// Token: 0x02001007 RID: 4103
	internal class PropertyFilter
	{
		// Token: 0x0600A050 RID: 41040 RVA: 0x0023A8A8 File Offset: 0x00238AA8
		internal static PropertyDescriptorCollection Filter(PropertyDescriptorCollection properties)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (PropertyFilter.IsDatePickerBrowsable(propertyDescriptor) && !PropertyFilter.IsHiddenProperty(propertyDescriptor))
				{
					propertyDescriptorCollection.Add(propertyDescriptor);
				}
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x0600A051 RID: 41041 RVA: 0x0023A91C File Offset: 0x00238B1C
		private static bool IsHiddenProperty(PropertyDescriptor current)
		{
			string[] array = new string[]
			{
				"ID"
			};
			return Array.IndexOf<string>(array, current.Name) >= 0;
		}

		// Token: 0x0600A052 RID: 41042 RVA: 0x0023A94C File Offset: 0x00238B4C
		private static bool IsDatePickerBrowsable(PropertyDescriptor descriptor)
		{
			return !descriptor.Attributes.Matches(new DatePickerBrowsableAttribute(false));
		}
	}
}
