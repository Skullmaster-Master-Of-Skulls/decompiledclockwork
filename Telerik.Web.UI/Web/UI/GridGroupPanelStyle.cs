using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020011A4 RID: 4516
	public class GridGroupPanelStyle : TableStyle
	{
		// Token: 0x17003BF2 RID: 15346
		// (get) Token: 0x0600B99F RID: 47519 RVA: 0x00292644 File Offset: 0x00290844
		// (set) Token: 0x0600B9A0 RID: 47520 RVA: 0x0029265F File Offset: 0x0029085F
		[Bindable(true)]
		[DefaultValue(5)]
		public override int CellSpacing
		{
			get
			{
				int cellSpacing = base.CellSpacing;
				if (cellSpacing == -1)
				{
					return 5;
				}
				return cellSpacing;
			}
			set
			{
				if (value == 5)
				{
					base.CellSpacing = -1;
					return;
				}
				base.CellSpacing = value;
			}
		}

		// Token: 0x17003BF3 RID: 15347
		// (get) Token: 0x0600B9A1 RID: 47521 RVA: 0x00292674 File Offset: 0x00290874
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsDefault
		{
			get
			{
				return this.IsEmpty;
			}
		}
	}
}
