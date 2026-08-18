using System;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities.DocumentForPrint
{
	// Token: 0x020002DE RID: 734
	public class DocumentPrintItem
	{
		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x0001B806 File Offset: 0x00019A06
		// (set) Token: 0x06001618 RID: 5656 RVA: 0x0001B80E File Offset: 0x00019A0E
		public eDocumentPrintItemType ItemType { get; set; }

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x0001B817 File Offset: 0x00019A17
		// (set) Token: 0x0600161A RID: 5658 RVA: 0x0001B81F File Offset: 0x00019A1F
		public string[] ColumnText { get; set; }

		// Token: 0x0600161B RID: 5659 RVA: 0x0001B828 File Offset: 0x00019A28
		public DocumentPrintItem()
		{
			this.ItemType = eDocumentPrintItemType.Regular;
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x0001B83A File Offset: 0x00019A3A
		public DocumentPrintItem(eDocumentPrintItemType itemType)
		{
			this.ItemType = itemType;
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0001B84C File Offset: 0x00019A4C
		public DocumentPrintItem(eDocumentPrintItemType itemType, string[] columnText)
		{
			this.ItemType = itemType;
			this.ColumnText = columnText;
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x0001B866 File Offset: 0x00019A66
		public DocumentPrintItem(string[] columnText)
		{
			this.ItemType = eDocumentPrintItemType.Regular;
			this.ColumnText = columnText;
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x0001B880 File Offset: 0x00019A80
		public override string ToString()
		{
			return string.Format("{0}: {1}", Enum.GetName(typeof(eDocumentPrintItemType), this.ItemType), (this.ColumnText == null) ? "" : string.Join(" | ", this.ColumnText));
		}
	}
}
