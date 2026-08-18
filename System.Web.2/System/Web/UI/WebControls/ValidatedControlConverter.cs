using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000509 RID: 1289
	public class ValidatedControlConverter : ControlIDConverter
	{
		// Token: 0x060040FC RID: 16636 RVA: 0x000D48F0 File Offset: 0x000D2AF0
		protected override bool FilterControl(Control control)
		{
			ValidationPropertyAttribute validationPropertyAttribute = (ValidationPropertyAttribute)TypeDescriptor.GetAttributes(control)[typeof(ValidationPropertyAttribute)];
			return validationPropertyAttribute != null && validationPropertyAttribute.Name != null;
		}
	}
}
