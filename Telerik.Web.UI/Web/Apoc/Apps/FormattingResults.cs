using System;
using System.Collections;
using Telerik.Web.Apoc.Fo.Pagination;

namespace Telerik.Web.Apoc.Apps
{
	// Token: 0x02001375 RID: 4981
	internal class FormattingResults
	{
		// Token: 0x0600CFED RID: 53229 RVA: 0x002E12E8 File Offset: 0x002DF4E8
		public int GetPageCount()
		{
			return this.pageCount;
		}

		// Token: 0x0600CFEE RID: 53230 RVA: 0x002E12F0 File Offset: 0x002DF4F0
		public ArrayList GetPageSequences()
		{
			return this.pageSequences;
		}

		// Token: 0x0600CFEF RID: 53231 RVA: 0x002E12F8 File Offset: 0x002DF4F8
		public void Reset()
		{
			this.pageCount = 0;
			if (this.pageSequences != null)
			{
				this.pageSequences.Clear();
			}
		}

		// Token: 0x0600CFF0 RID: 53232 RVA: 0x002E1314 File Offset: 0x002DF514
		public void HaveFormattedPageSequence(PageSequence pageSequence)
		{
			this.pageCount += pageSequence.PageCount;
			if (this.pageSequences == null)
			{
				this.pageSequences = new ArrayList();
			}
			this.pageSequences.Add(new PageSequenceResults(pageSequence.GetProperty("id").GetString(), pageSequence.PageCount));
		}

		// Token: 0x040037BA RID: 14266
		private int pageCount;

		// Token: 0x040037BB RID: 14267
		private ArrayList pageSequences;
	}
}
