using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200193D RID: 6461
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ListBoxPostBackCommand
	{
		// Token: 0x17004B7A RID: 19322
		// (get) Token: 0x0600F9E7 RID: 63975 RVA: 0x003856D5 File Offset: 0x003838D5
		// (set) Token: 0x0600F9E8 RID: 63976 RVA: 0x003856DD File Offset: 0x003838DD
		public ListBoxCommand Type { get; set; }

		// Token: 0x17004B7B RID: 19323
		// (get) Token: 0x0600F9E9 RID: 63977 RVA: 0x003856E6 File Offset: 0x003838E6
		// (set) Token: 0x0600F9EA RID: 63978 RVA: 0x003856EE File Offset: 0x003838EE
		public int Offset { get; set; }

		// Token: 0x17004B7C RID: 19324
		// (get) Token: 0x0600F9EB RID: 63979 RVA: 0x003856F7 File Offset: 0x003838F7
		// (set) Token: 0x0600F9EC RID: 63980 RVA: 0x003856FF File Offset: 0x003838FF
		public string SourceListBox { get; set; }

		// Token: 0x17004B7D RID: 19325
		// (get) Token: 0x0600F9ED RID: 63981 RVA: 0x00385708 File Offset: 0x00383908
		// (set) Token: 0x0600F9EE RID: 63982 RVA: 0x00385710 File Offset: 0x00383910
		public string DestinationListBox { get; set; }

		// Token: 0x17004B7E RID: 19326
		// (get) Token: 0x0600F9EF RID: 63983 RVA: 0x00385719 File Offset: 0x00383919
		// (set) Token: 0x0600F9F0 RID: 63984 RVA: 0x00385721 File Offset: 0x00383921
		public int DestinationIndex { get; set; }

		// Token: 0x17004B7F RID: 19327
		// (get) Token: 0x0600F9F1 RID: 63985 RVA: 0x0038572A File Offset: 0x0038392A
		// (set) Token: 0x0600F9F2 RID: 63986 RVA: 0x00385732 File Offset: 0x00383932
		public int NumberOfItems { get; set; }

		// Token: 0x17004B80 RID: 19328
		// (get) Token: 0x0600F9F3 RID: 63987 RVA: 0x0038573B File Offset: 0x0038393B
		// (set) Token: 0x0600F9F4 RID: 63988 RVA: 0x00385743 File Offset: 0x00383943
		public string HtmlElementId { get; set; }

		// Token: 0x17004B81 RID: 19329
		// (get) Token: 0x0600F9F5 RID: 63989 RVA: 0x0038574C File Offset: 0x0038394C
		// (set) Token: 0x0600F9F6 RID: 63990 RVA: 0x00385754 File Offset: 0x00383954
		public int ItemIndex { get; set; }

		// Token: 0x17004B82 RID: 19330
		// (get) Token: 0x0600F9F7 RID: 63991 RVA: 0x0038575D File Offset: 0x0038395D
		// (set) Token: 0x0600F9F8 RID: 63992 RVA: 0x00385765 File Offset: 0x00383965
		public bool CheckAllChecked { get; set; }

		// Token: 0x17004B83 RID: 19331
		// (get) Token: 0x0600F9F9 RID: 63993 RVA: 0x0038576E File Offset: 0x0038396E
		// (set) Token: 0x0600F9FA RID: 63994 RVA: 0x00385776 File Offset: 0x00383976
		public ListBoxDropPosition DropPosition { get; set; }
	}
}
