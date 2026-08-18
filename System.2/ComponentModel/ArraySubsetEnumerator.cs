using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000510 RID: 1296
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal class ArraySubsetEnumerator : IEnumerator
	{
		// Token: 0x06003126 RID: 12582 RVA: 0x000DEEF0 File Offset: 0x000DD0F0
		public ArraySubsetEnumerator(Array array, int count)
		{
			this.array = array;
			this.total = count;
			this.current = -1;
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x000DEF0D File Offset: 0x000DD10D
		public bool MoveNext()
		{
			if (this.current < this.total - 1)
			{
				this.current++;
				return true;
			}
			return false;
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x000DEF30 File Offset: 0x000DD130
		public void Reset()
		{
			this.current = -1;
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06003129 RID: 12585 RVA: 0x000DEF39 File Offset: 0x000DD139
		public object Current
		{
			get
			{
				if (this.current == -1)
				{
					throw new InvalidOperationException();
				}
				return this.array.GetValue(this.current);
			}
		}

		// Token: 0x0400290A RID: 10506
		private Array array;

		// Token: 0x0400290B RID: 10507
		private int total;

		// Token: 0x0400290C RID: 10508
		private int current;
	}
}
