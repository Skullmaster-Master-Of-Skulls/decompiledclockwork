using System;
using System.Collections;

namespace System.Web.Util
{
	// Token: 0x020001F6 RID: 502
	internal class DoubleLinkListEnumerator : IEnumerator
	{
		// Token: 0x060018E5 RID: 6373 RVA: 0x0004CD9D File Offset: 0x0004AF9D
		internal DoubleLinkListEnumerator(DoubleLinkList list)
		{
			this._list = list;
			this._current = list;
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x0004CDB3 File Offset: 0x0004AFB3
		public void Reset()
		{
			this._current = this._list;
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x0004CDC1 File Offset: 0x0004AFC1
		public bool MoveNext()
		{
			if (this._current.Next == this._list)
			{
				this._current = null;
				return false;
			}
			this._current = this._current.Next;
			return true;
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x0004CDF1 File Offset: 0x0004AFF1
		public object Current
		{
			get
			{
				if (this._current == null || this._current == this._list)
				{
					throw new InvalidOperationException();
				}
				return this._current.Item;
			}
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x0004CE1A File Offset: 0x0004B01A
		internal DoubleLink GetDoubleLink()
		{
			return this._current;
		}

		// Token: 0x04001796 RID: 6038
		private DoubleLinkList _list;

		// Token: 0x04001797 RID: 6039
		private DoubleLink _current;
	}
}
