using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x020002F4 RID: 756
	public interface IBoundColumn : IMarkableStateManager, IStateManager
	{
		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x060019F7 RID: 6647
		// (set) Token: 0x060019F8 RID: 6648
		string DataField { get; set; }

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x060019F9 RID: 6649
		// (set) Token: 0x060019FA RID: 6650
		string DataFormatString { get; set; }

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x060019FB RID: 6651
		// (set) Token: 0x060019FC RID: 6652
		DataType DataType { get; set; }

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x060019FD RID: 6653
		// (set) Token: 0x060019FE RID: 6654
		string HeaderText { get; set; }

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x060019FF RID: 6655
		// (set) Token: 0x06001A00 RID: 6656
		string UniqueName { get; set; }

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06001A01 RID: 6657
		// (set) Token: 0x06001A02 RID: 6658
		bool AllowSorting { get; set; }

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06001A03 RID: 6659
		// (set) Token: 0x06001A04 RID: 6660
		bool Visible { get; set; }

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06001A05 RID: 6661
		// (set) Token: 0x06001A06 RID: 6662
		Unit Width { get; set; }

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06001A07 RID: 6663
		IColumnValidation Validation { get; }
	}
}
