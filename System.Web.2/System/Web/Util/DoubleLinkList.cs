using System;

namespace System.Web.Util
{
	// Token: 0x020001F5 RID: 501
	internal class DoubleLinkList : DoubleLink
	{
		// Token: 0x060018DF RID: 6367 RVA: 0x0004CD4B File Offset: 0x0004AF4B
		internal DoubleLinkList()
		{
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x0004CD53 File Offset: 0x0004AF53
		internal bool IsEmpty()
		{
			return this._next == this;
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0004CD5E File Offset: 0x0004AF5E
		internal virtual void InsertHead(DoubleLink entry)
		{
			entry.InsertAfter(this);
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0004CD67 File Offset: 0x0004AF67
		internal virtual void InsertTail(DoubleLink entry)
		{
			entry.InsertBefore(this);
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0004CD70 File Offset: 0x0004AF70
		internal DoubleLinkListEnumerator GetEnumerator()
		{
			return new DoubleLinkListEnumerator(this);
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x0004CD78 File Offset: 0x0004AF78
		internal int Length
		{
			get
			{
				int num = 0;
				DoubleLinkListEnumerator enumerator = this.GetEnumerator();
				while (enumerator.MoveNext())
				{
					num++;
				}
				return num;
			}
		}
	}
}
