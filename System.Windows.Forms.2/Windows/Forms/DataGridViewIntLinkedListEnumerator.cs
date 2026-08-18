using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x02000203 RID: 515
	internal class DataGridViewIntLinkedListEnumerator : IEnumerator
	{
		// Token: 0x0600217C RID: 8572 RVA: 0x0009E094 File Offset: 0x0009C294
		public DataGridViewIntLinkedListEnumerator(DataGridViewIntLinkedListElement headElement)
		{
			this.headElement = headElement;
			this.reset = true;
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x0600217D RID: 8573 RVA: 0x0009E0AA File Offset: 0x0009C2AA
		object IEnumerator.Current
		{
			get
			{
				return this.current.Int;
			}
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x0009E0BC File Offset: 0x0009C2BC
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

		// Token: 0x0600217F RID: 8575 RVA: 0x0009E0F5 File Offset: 0x0009C2F5
		void IEnumerator.Reset()
		{
			this.reset = true;
			this.current = null;
		}

		// Token: 0x04000E00 RID: 3584
		private DataGridViewIntLinkedListElement headElement;

		// Token: 0x04000E01 RID: 3585
		private DataGridViewIntLinkedListElement current;

		// Token: 0x04000E02 RID: 3586
		private bool reset;
	}
}
