using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x020001AD RID: 429
	internal class DataGridViewCellLinkedListEnumerator : IEnumerator
	{
		// Token: 0x06001E4E RID: 7758 RVA: 0x0008F3F3 File Offset: 0x0008D5F3
		public DataGridViewCellLinkedListEnumerator(DataGridViewCellLinkedListElement headElement)
		{
			this.headElement = headElement;
			this.reset = true;
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001E4F RID: 7759 RVA: 0x0008F409 File Offset: 0x0008D609
		object IEnumerator.Current
		{
			get
			{
				return this.current.DataGridViewCell;
			}
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x0008F416 File Offset: 0x0008D616
		bool IEnumerator.MoveNext()
		{
			if (this.reset)
			{
				this.current = this.headElement;
				this.reset = false;
			}
			else
			{
				this.current = this.current.Next;
			}
			return this.current != null;
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x0008F44F File Offset: 0x0008D64F
		void IEnumerator.Reset()
		{
			this.reset = true;
			this.current = null;
		}

		// Token: 0x04000CD0 RID: 3280
		private DataGridViewCellLinkedListElement headElement;

		// Token: 0x04000CD1 RID: 3281
		private DataGridViewCellLinkedListElement current;

		// Token: 0x04000CD2 RID: 3282
		private bool reset;
	}
}
