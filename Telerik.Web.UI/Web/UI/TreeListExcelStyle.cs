using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001222 RID: 4642
	public class TreeListExcelStyle : TableItemStyle
	{
		// Token: 0x17003DBE RID: 15806
		// (get) Token: 0x0600BF80 RID: 49024 RVA: 0x002A78D1 File Offset: 0x002A5AD1
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual bool IsDefault
		{
			get
			{
				return this.IsEmpty;
			}
		}

		// Token: 0x0600BF81 RID: 49025 RVA: 0x002A78D9 File Offset: 0x002A5AD9
		public override void CopyFrom(Style s)
		{
			base.CopyFrom(s);
		}
	}
}
