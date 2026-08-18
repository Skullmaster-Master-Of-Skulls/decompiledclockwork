using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020011A5 RID: 4517
	public class GridPanelItemsStyle : TableStyle
	{
		// Token: 0x17003BF4 RID: 15348
		// (get) Token: 0x0600B9A3 RID: 47523 RVA: 0x00292684 File Offset: 0x00290884
		// (set) Token: 0x0600B9A4 RID: 47524 RVA: 0x0029269F File Offset: 0x0029089F
		[Bindable(true)]
		[DefaultValue(2)]
		public override int CellPadding
		{
			get
			{
				int cellPadding = base.CellPadding;
				if (cellPadding == -1)
				{
					return 2;
				}
				return cellPadding;
			}
			set
			{
				if (value == 2)
				{
					base.CellPadding = -1;
					return;
				}
				base.CellPadding = value;
			}
		}

		// Token: 0x17003BF5 RID: 15349
		// (get) Token: 0x0600B9A5 RID: 47525 RVA: 0x002926B4 File Offset: 0x002908B4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsDefault
		{
			get
			{
				return this.IsEmpty;
			}
		}
	}
}
