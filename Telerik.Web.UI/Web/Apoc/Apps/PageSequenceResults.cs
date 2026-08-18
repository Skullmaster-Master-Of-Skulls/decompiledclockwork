using System;

namespace Telerik.Web.Apoc.Apps
{
	// Token: 0x02001376 RID: 4982
	internal class PageSequenceResults
	{
		// Token: 0x0600CFF1 RID: 53233 RVA: 0x002E136E File Offset: 0x002DF56E
		public PageSequenceResults(string id, int pageCount)
		{
			this.id = id;
			this.pageCount = pageCount;
		}

		// Token: 0x0600CFF2 RID: 53234 RVA: 0x002E1384 File Offset: 0x002DF584
		public string GetID()
		{
			return this.id;
		}

		// Token: 0x0600CFF3 RID: 53235 RVA: 0x002E138C File Offset: 0x002DF58C
		public int GetPageCount()
		{
			return this.pageCount;
		}

		// Token: 0x040037BC RID: 14268
		private string id;

		// Token: 0x040037BD RID: 14269
		private int pageCount;
	}
}
