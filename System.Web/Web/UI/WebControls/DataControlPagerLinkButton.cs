using System;
using System.Drawing;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000536 RID: 1334
	[SupportsEventValidation]
	internal class DataControlPagerLinkButton : DataControlLinkButton
	{
		// Token: 0x060041BB RID: 16827 RVA: 0x001101C1 File Offset: 0x0010F1C1
		internal DataControlPagerLinkButton(IPostBackContainer container) : base(container)
		{
		}

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x060041BC RID: 16828 RVA: 0x001101CA File Offset: 0x0010F1CA
		// (set) Token: 0x060041BD RID: 16829 RVA: 0x001101CD File Offset: 0x0010F1CD
		public override bool CausesValidation
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("CannotSetValidationOnPagerButtons"));
			}
		}

		// Token: 0x060041BE RID: 16830 RVA: 0x001101E0 File Offset: 0x0010F1E0
		protected override void SetForeColor()
		{
			if (!base.ControlStyle.IsSet(4))
			{
				Control control = this;
				for (int i = 0; i < 6; i++)
				{
					control = control.Parent;
					Color foreColor = ((WebControl)control).ForeColor;
					if (foreColor != Color.Empty)
					{
						this.ForeColor = foreColor;
						return;
					}
				}
			}
		}
	}
}
