using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.Design;

namespace AjaxControlToolkit.Design
{
	// Token: 0x02000197 RID: 407
	internal class TabPanelDesigner : ControlDesigner
	{
		// Token: 0x06000BBC RID: 3004 RVA: 0x0001E9D0 File Offset: 0x0001CBD0
		protected override void PreFilterProperties(IDictionary properties)
		{
			PropertyDescriptor[] array = new PropertyDescriptor[]
			{
				(PropertyDescriptor)properties["HeaderTemplate"],
				(PropertyDescriptor)properties["ContentTemplate"]
			};
			foreach (PropertyDescriptor propertyDescriptor in array)
			{
				if (propertyDescriptor != null)
				{
					properties[propertyDescriptor.Name] = TypeDescriptor.CreateProperty(typeof(TabPanel), propertyDescriptor, new Attribute[]
					{
						new TemplateContainerAttribute(typeof(TabPanel))
					});
				}
			}
			base.PreFilterProperties(properties);
		}
	}
}
