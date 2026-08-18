using System;

namespace Telerik.Web.UI.Barcode.PDF417ClassLibrary
{
	// Token: 0x0200009F RID: 159
	internal class TextModeDefinitionEntry
	{
		// Token: 0x06000623 RID: 1571 RVA: 0x00010DBC File Offset: 0x0000EFBC
		internal TextModeDefinitionEntry(int asciiValue, int group, int rowIndex)
		{
			this.TypeIndex = group;
			this.EntryValue = asciiValue;
			this.RowIndex = rowIndex;
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00010DD9 File Offset: 0x0000EFD9
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x00010DE1 File Offset: 0x0000EFE1
		internal int TypeIndex { get; set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00010DEA File Offset: 0x0000EFEA
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x00010DF2 File Offset: 0x0000EFF2
		internal int EntryValue { get; set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00010DFB File Offset: 0x0000EFFB
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x00010E03 File Offset: 0x0000F003
		internal int RowIndex { get; set; }
	}
}
