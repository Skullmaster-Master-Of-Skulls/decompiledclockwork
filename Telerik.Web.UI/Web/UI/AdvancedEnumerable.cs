using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001098 RID: 4248
	internal class AdvancedEnumerable : IEnumerable
	{
		// Token: 0x0600ACB4 RID: 44212 RVA: 0x00251F5E File Offset: 0x0025015E
		public AdvancedEnumerable(IEnumerator originalEnumerator, object firstDataItem)
		{
			this.originalEnumerator = originalEnumerator;
			this.firstDataItem = firstDataItem;
		}

		// Token: 0x0600ACB5 RID: 44213 RVA: 0x00251F74 File Offset: 0x00250174
		public IEnumerator GetEnumerator()
		{
			if (this.currEnumerator == null)
			{
				this.currEnumerator = new AdvancedEnumerator(this.originalEnumerator, this.firstDataItem);
			}
			return this.currEnumerator;
		}

		// Token: 0x04002DC3 RID: 11715
		internal IEnumerator originalEnumerator;

		// Token: 0x04002DC4 RID: 11716
		private object firstDataItem;

		// Token: 0x04002DC5 RID: 11717
		private IEnumerator currEnumerator;
	}
}
