using System;

namespace System.Windows.Forms
{
	// Token: 0x02000204 RID: 516
	internal class DataGridViewIntLinkedListElement
	{
		// Token: 0x06002180 RID: 8576 RVA: 0x0009E105 File Offset: 0x0009C305
		public DataGridViewIntLinkedListElement(int integer)
		{
			this.integer = integer;
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06002181 RID: 8577 RVA: 0x0009E114 File Offset: 0x0009C314
		// (set) Token: 0x06002182 RID: 8578 RVA: 0x0009E11C File Offset: 0x0009C31C
		public int Int
		{
			get
			{
				return this.integer;
			}
			set
			{
				this.integer = value;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06002183 RID: 8579 RVA: 0x0009E125 File Offset: 0x0009C325
		// (set) Token: 0x06002184 RID: 8580 RVA: 0x0009E12D File Offset: 0x0009C32D
		public DataGridViewIntLinkedListElement Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x04000E03 RID: 3587
		private int integer;

		// Token: 0x04000E04 RID: 3588
		private DataGridViewIntLinkedListElement next;
	}
}
