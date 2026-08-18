using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000130 RID: 304
	[SupportsPreviewControl(true)]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ValidationSummaryDesigner : PreviewControlDesigner
	{
		// Token: 0x06000B06 RID: 2822 RVA: 0x00047728 File Offset: 0x00045928
		protected override Control CreateViewControl()
		{
			ValidationSummary validationSummary = (ValidationSummary)base.CreateViewControl();
			validationSummary.ForeColor = ((ValidationSummary)base.Component).ForeColor;
			return validationSummary;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00047758 File Offset: 0x00045958
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			if (((ValidationSummary)base.Component).RenderingCompatibility < new Version(4, 0))
			{
				return;
			}
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties["ForeColor"];
			properties["ForeColor"] = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, new Attribute[]
			{
				new DefaultValueAttribute(typeof(Color), "")
			});
		}
	}
}
