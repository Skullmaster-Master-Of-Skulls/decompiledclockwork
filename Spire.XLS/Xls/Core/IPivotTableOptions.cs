using System;

namespace Spire.Xls.Core
{
	// Token: 0x0200058B RID: 1419
	public interface IPivotTableOptions
	{
		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x060055FC RID: 22012
		// (set) Token: 0x060055FD RID: 22013
		bool ShowAsteriskTotals { get; set; }

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x060055FE RID: 22014
		// (set) Token: 0x060055FF RID: 22015
		string ColumnHeaderCaption { get; set; }

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06005600 RID: 22016
		// (set) Token: 0x06005601 RID: 22017
		string RowHeaderCaption { get; set; }

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06005602 RID: 22018
		// (set) Token: 0x06005603 RID: 22019
		bool ShowCustomSortList { get; set; }

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06005604 RID: 22020
		// (set) Token: 0x06005605 RID: 22021
		bool ShowFieldList { get; set; }

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06005606 RID: 22022
		// (set) Token: 0x06005607 RID: 22023
		bool IsDataEditable { get; set; }

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06005608 RID: 22024
		// (set) Token: 0x06005609 RID: 22025
		bool EnableFieldProperties { get; set; }

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x0600560A RID: 22026
		// (set) Token: 0x0600560B RID: 22027
		uint Indent { get; set; }

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x0600560C RID: 22028
		// (set) Token: 0x0600560D RID: 22029
		string ErrorString { get; set; }

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x0600560E RID: 22030
		// (set) Token: 0x0600560F RID: 22031
		bool DisplayErrorString { get; set; }

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06005610 RID: 22032
		// (set) Token: 0x06005611 RID: 22033
		bool MergeLabels { get; set; }

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06005612 RID: 22034
		// (set) Token: 0x06005613 RID: 22035
		int PageFieldWrapCount { get; set; }

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06005614 RID: 22036
		// (set) Token: 0x06005615 RID: 22037
		PivotPageAreaFieldsOrderType PageFieldsOrder { get; set; }

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06005616 RID: 22038
		// (set) Token: 0x06005617 RID: 22039
		bool DisplayNullString { get; set; }

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06005618 RID: 22040
		// (set) Token: 0x06005619 RID: 22041
		string NullString { get; set; }

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x0600561A RID: 22042
		// (set) Token: 0x0600561B RID: 22043
		bool PreserveFormatting { get; set; }

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x0600561C RID: 22044
		// (set) Token: 0x0600561D RID: 22045
		bool ShowTooltips { get; set; }

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x0600561E RID: 22046
		// (set) Token: 0x0600561F RID: 22047
		bool DisplayFieldCaptions { get; set; }

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06005620 RID: 22048
		// (set) Token: 0x06005621 RID: 22049
		bool PrintTitles { get; set; }

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06005622 RID: 22050
		// (set) Token: 0x06005623 RID: 22051
		bool IsSaveData { get; set; }

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06005624 RID: 22052
		// (set) Token: 0x06005625 RID: 22053
		PivotTableLayoutType RowLayout { get; set; }

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06005626 RID: 22054
		// (set) Token: 0x06005627 RID: 22055
		bool ShowDrillIndicators { get; set; }
	}
}
