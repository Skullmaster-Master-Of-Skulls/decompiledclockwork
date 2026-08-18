using System;

namespace System.Security.Util
{
	// Token: 0x02000489 RID: 1161
	internal struct TokenBasedSetEnumerator
	{
		// Token: 0x06002E22 RID: 11810 RVA: 0x0009AD5F File Offset: 0x00099D5F
		public bool MoveNext()
		{
			return this._tb != null && this._tb.MoveNext(ref this);
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x0009AD77 File Offset: 0x00099D77
		public void Reset()
		{
			this.Index = -1;
			this.Current = null;
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x0009AD87 File Offset: 0x00099D87
		public TokenBasedSetEnumerator(TokenBasedSet tb)
		{
			this.Index = -1;
			this.Current = null;
			this._tb = tb;
		}

		// Token: 0x040017B7 RID: 6071
		public object Current;

		// Token: 0x040017B8 RID: 6072
		public int Index;

		// Token: 0x040017B9 RID: 6073
		private TokenBasedSet _tb;
	}
}
