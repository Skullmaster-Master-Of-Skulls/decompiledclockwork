using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200067B RID: 1659
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ValidatedControlConverter : ControlIDConverter
	{
		// Token: 0x060051BE RID: 20926 RVA: 0x0014A8C8 File Offset: 0x001498C8
		protected override bool FilterControl(Control control)
		{
			ValidationPropertyAttribute validationPropertyAttribute = (ValidationPropertyAttribute)TypeDescriptor.GetAttributes(control)[typeof(ValidationPropertyAttribute)];
			return validationPropertyAttribute != null && validationPropertyAttribute.Name != null;
		}
	}
}
