using System;
using System.Collections.Generic;

namespace System.util
{
	// Token: 0x020004F7 RID: 1271
	public class ListIterator<T>
	{
		// Token: 0x06002B82 RID: 11138 RVA: 0x00107AD3 File Offset: 0x00106AD3
		public ListIterator(IList<T> col)
		{
			this.col = col;
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x00107AE9 File Offset: 0x00106AE9
		public bool HasNext()
		{
			return this.cursor != this.col.Count;
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x00107B04 File Offset: 0x00106B04
		public T Next()
		{
			T result = this.col[this.cursor];
			this.lastRet = this.cursor++;
			return result;
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x00107B3C File Offset: 0x00106B3C
		public T Previous()
		{
			int index = this.cursor - 1;
			T result = this.col[index];
			this.lastRet = (this.cursor = index);
			return result;
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x00107B70 File Offset: 0x00106B70
		public void Remove()
		{
			if (this.lastRet == -1)
			{
				throw new InvalidOperationException();
			}
			this.col.RemoveAt(this.lastRet);
			if (this.lastRet < this.cursor)
			{
				this.cursor--;
			}
			this.lastRet = -1;
		}

		// Token: 0x04001E28 RID: 7720
		private IList<T> col;

		// Token: 0x04001E29 RID: 7721
		private int cursor;

		// Token: 0x04001E2A RID: 7722
		private int lastRet = -1;
	}
}
