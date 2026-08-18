using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001099 RID: 4249
	internal class AdvancedEnumerator : IEnumerator
	{
		// Token: 0x0600ACB6 RID: 44214 RVA: 0x00251F9B File Offset: 0x0025019B
		public AdvancedEnumerator(IEnumerator originalEnumerator, object firstDataItem)
		{
			this.originalEnumerator = originalEnumerator;
			this.firstDataItem = firstDataItem;
		}

		// Token: 0x0600ACB7 RID: 44215 RVA: 0x00251FBF File Offset: 0x002501BF
		public bool MoveNext()
		{
			if (this.isFirstMove && this.firstDataItem != null)
			{
				this.isFirstMove = false;
				return true;
			}
			this.currentIsFirst = false;
			return this.originalEnumerator.MoveNext();
		}

		// Token: 0x0600ACB8 RID: 44216 RVA: 0x00251FEC File Offset: 0x002501EC
		public void Reset()
		{
			try
			{
				this.originalEnumerator.Reset();
				this.firstDataItem = null;
			}
			catch
			{
				this.isFirstMove = true;
				this.currentIsFirst = true;
				this.isForwardOnly = true;
			}
		}

		// Token: 0x170037CF RID: 14287
		// (get) Token: 0x0600ACB9 RID: 44217 RVA: 0x00252038 File Offset: 0x00250238
		public object Current
		{
			get
			{
				if (this.currentIsFirst && this.firstDataItem != null)
				{
					return this.firstDataItem;
				}
				return this.originalEnumerator.Current;
			}
		}

		// Token: 0x04002DC6 RID: 11718
		private IEnumerator originalEnumerator;

		// Token: 0x04002DC7 RID: 11719
		private object firstDataItem;

		// Token: 0x04002DC8 RID: 11720
		private bool isFirstMove = true;

		// Token: 0x04002DC9 RID: 11721
		private bool currentIsFirst = true;

		// Token: 0x04002DCA RID: 11722
		internal bool isForwardOnly;
	}
}
