using System;
using System.Drawing;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003B9 RID: 953
	[SupportsEventValidation]
	internal class DataControlPagerLinkButton : DataControlLinkButton
	{
		// Token: 0x06002E00 RID: 11776 RVA: 0x00096239 File Offset: 0x00094439
		internal DataControlPagerLinkButton(IPostBackContainer container) : base(container)
		{
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06002E01 RID: 11777 RVA: 0x00007722 File Offset: 0x00005922
		// (set) Token: 0x06002E02 RID: 11778 RVA: 0x00096242 File Offset: 0x00094442
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

		// Token: 0x06002E03 RID: 11779 RVA: 0x00096254 File Offset: 0x00094454
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
