using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClockWorkWebAPIWeb.CustomControls
{
	// Token: 0x02000019 RID: 25
	public class MyRequiredFieldValidator : RequiredFieldValidator
	{
		// Token: 0x06000143 RID: 323 RVA: 0x000105E2 File Offset: 0x0000E7E2
		public MyRequiredFieldValidator()
		{
			base.EnableClientScript = false;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000105FC File Offset: 0x0000E7FC
		protected override bool ControlPropertiesValid()
		{
			Control control = this.FindControl(base.ControlToValidate);
			bool flag = control != null;
			bool result;
			if (flag)
			{
				while (control != null)
				{
					WebControl webControl = (WebControl)control;
					string text = webControl.Style[HtmlTextWriterStyle.Display];
					bool flag2 = !string.IsNullOrEmpty(text) && text.ToLower().Equals("none");
					bool flag3 = flag2;
					if (flag3)
					{
						this.isHidden = true;
						break;
					}
					control = control.Parent;
				}
				this.isHidden = false;
				result = base.ControlPropertiesValid();
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00010694 File Offset: 0x0000E894
		protected override bool EvaluateIsValid()
		{
			bool flag = this.isHidden;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = base.EvaluateIsValid();
				result = flag2;
			}
			return result;
		}

		// Token: 0x04000081 RID: 129
		private bool isHidden = false;
	}
}
