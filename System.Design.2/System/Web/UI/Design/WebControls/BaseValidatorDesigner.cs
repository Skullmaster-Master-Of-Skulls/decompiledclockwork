using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000A0 RID: 160
	[SupportsPreviewControl(true)]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class BaseValidatorDesigner : PreviewControlDesigner
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x00016C20 File Offset: 0x00014E20
		protected override Control CreateViewControl()
		{
			BaseValidator baseValidator = (BaseValidator)base.CreateViewControl();
			baseValidator.ForeColor = ((BaseValidator)base.Component).ForeColor;
			return baseValidator;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00016C50 File Offset: 0x00014E50
		public override string GetDesignTimeHtml()
		{
			BaseValidator baseValidator = (BaseValidator)base.ViewControl;
			baseValidator.IsValid = false;
			string errorMessage = baseValidator.ErrorMessage;
			ValidatorDisplay display = baseValidator.Display;
			bool flag = display == ValidatorDisplay.None || (errorMessage.Trim().Length == 0 && baseValidator.Text.Trim().Length == 0);
			if (flag)
			{
				baseValidator.ErrorMessage = "[" + baseValidator.ID + "]";
				baseValidator.Display = ValidatorDisplay.Static;
			}
			string designTimeHtml = base.GetDesignTimeHtml();
			if (flag)
			{
				baseValidator.ErrorMessage = errorMessage;
				baseValidator.Display = display;
			}
			return designTimeHtml;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00016CE8 File Offset: 0x00014EE8
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			if (((BaseValidator)base.Component).RenderingCompatibility < new Version(4, 0))
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
