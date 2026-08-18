using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200128E RID: 4750
	public class TreeListDataColumns : Dictionary<string, PropertyDescriptor>
	{
		// Token: 0x0600C62F RID: 50735 RVA: 0x002C3948 File Offset: 0x002C1B48
		public TreeListDataColumns(object item)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(item);
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (TreeListTypeHelper.IsBindableType(propertyDescriptor.PropertyType))
				{
					base.Add(propertyDescriptor.Name, propertyDescriptor);
				}
			}
		}
	}
}
