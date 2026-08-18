using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000F82 RID: 3970
	internal class ListBoxCallbackArgument
	{
		// Token: 0x1700301A RID: 12314
		// (get) Token: 0x06009813 RID: 38931 RVA: 0x00220B63 File Offset: 0x0021ED63
		// (set) Token: 0x06009814 RID: 38932 RVA: 0x00220B6B File Offset: 0x0021ED6B
		public int StartIndex { get; set; }

		// Token: 0x1700301B RID: 12315
		// (get) Token: 0x06009815 RID: 38933 RVA: 0x00220B74 File Offset: 0x0021ED74
		// (set) Token: 0x06009816 RID: 38934 RVA: 0x00220B7C File Offset: 0x0021ED7C
		public int Count { get; set; }

		// Token: 0x1700301C RID: 12316
		// (get) Token: 0x06009817 RID: 38935 RVA: 0x00220B85 File Offset: 0x0021ED85
		// (set) Token: 0x06009818 RID: 38936 RVA: 0x00220B8D File Offset: 0x0021ED8D
		public bool CheckAllCheck { get; set; }

		// Token: 0x1700301D RID: 12317
		// (get) Token: 0x06009819 RID: 38937 RVA: 0x00220B96 File Offset: 0x0021ED96
		// (set) Token: 0x0600981A RID: 38938 RVA: 0x00220B9E File Offset: 0x0021ED9E
		public IDictionary<string, object> UserContext { get; set; }
	}
}
