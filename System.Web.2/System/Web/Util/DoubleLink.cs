using System;

namespace System.Web.Util
{
	// Token: 0x020001F4 RID: 500
	internal class DoubleLink
	{
		// Token: 0x060018D9 RID: 6361 RVA: 0x0004CC80 File Offset: 0x0004AE80
		internal DoubleLink()
		{
			this._prev = this;
			this._next = this;
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0004CCA3 File Offset: 0x0004AEA3
		internal DoubleLink(object item) : this()
		{
			this.Item = item;
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x0004CCB2 File Offset: 0x0004AEB2
		internal DoubleLink Next
		{
			get
			{
				return this._next;
			}
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0004CCBA File Offset: 0x0004AEBA
		internal void InsertAfter(DoubleLink after)
		{
			this._prev = after;
			this._next = after._next;
			after._next = this;
			this._next._prev = this;
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0004CCE2 File Offset: 0x0004AEE2
		internal void InsertBefore(DoubleLink before)
		{
			this._prev = before._prev;
			this._next = before;
			before._prev = this;
			this._prev._next = this;
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0004CD0C File Offset: 0x0004AF0C
		internal void Remove()
		{
			this._prev._next = this._next;
			this._next._prev = this._prev;
			this._prev = this;
			this._next = this;
		}

		// Token: 0x04001793 RID: 6035
		internal DoubleLink _next;

		// Token: 0x04001794 RID: 6036
		internal DoubleLink _prev;

		// Token: 0x04001795 RID: 6037
		internal object Item;
	}
}
