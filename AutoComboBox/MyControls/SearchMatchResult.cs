using System;

namespace AutoComboBox.MyControls
{
	// Token: 0x020000E8 RID: 232
	[Serializable]
	public class SearchMatchResult
	{
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x00045EC8 File Offset: 0x00044EC8
		public string OriginalSearchText
		{
			get
			{
				return this.originalSearchText;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00045EE0 File Offset: 0x00044EE0
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x00045EF8 File Offset: 0x00044EF8
		public object SearchResult
		{
			get
			{
				return this.searchResult;
			}
			set
			{
				this.searchResult = value;
			}
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00045F02 File Offset: 0x00044F02
		public SearchMatchResult(string originalSearchText, object searchResult)
		{
			this.originalSearchText = originalSearchText;
			this.searchResult = searchResult;
		}

		// Token: 0x0400068D RID: 1677
		private string originalSearchText;

		// Token: 0x0400068E RID: 1678
		private object searchResult;
	}
}
