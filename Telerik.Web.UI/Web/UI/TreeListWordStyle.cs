using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200095E RID: 2398
	public class TreeListWordStyle : TableItemStyle
	{
		// Token: 0x17001E14 RID: 7700
		// (get) Token: 0x06005B38 RID: 23352 RVA: 0x00115A27 File Offset: 0x00113C27
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual bool IsDefault
		{
			get
			{
				return this.IsEmpty;
			}
		}

		// Token: 0x06005B39 RID: 23353 RVA: 0x00115A2F File Offset: 0x00113C2F
		public override void CopyFrom(Style s)
		{
			base.CopyFrom(s);
		}
	}
}
