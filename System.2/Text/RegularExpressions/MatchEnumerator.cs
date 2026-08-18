using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200069F RID: 1695
	[Serializable]
	internal class MatchEnumerator : IEnumerator
	{
		// Token: 0x06003F2C RID: 16172 RVA: 0x00107CB4 File Offset: 0x00105EB4
		internal MatchEnumerator(MatchCollection matchcoll)
		{
			this._matchcoll = matchcoll;
		}

		// Token: 0x06003F2D RID: 16173 RVA: 0x00107CC4 File Offset: 0x00105EC4
		public bool MoveNext()
		{
			if (this._done)
			{
				return false;
			}
			this._match = this._matchcoll.GetMatch(this._curindex);
			this._curindex++;
			if (this._match == null)
			{
				this._done = true;
				return false;
			}
			return true;
		}

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06003F2E RID: 16174 RVA: 0x00107D12 File Offset: 0x00105F12
		public object Current
		{
			get
			{
				if (this._match == null)
				{
					throw new InvalidOperationException(SR.GetString("EnumNotStarted"));
				}
				return this._match;
			}
		}

		// Token: 0x06003F2F RID: 16175 RVA: 0x00107D32 File Offset: 0x00105F32
		public void Reset()
		{
			this._curindex = 0;
			this._done = false;
			this._match = null;
		}

		// Token: 0x04002E09 RID: 11785
		internal MatchCollection _matchcoll;

		// Token: 0x04002E0A RID: 11786
		internal Match _match;

		// Token: 0x04002E0B RID: 11787
		internal int _curindex;

		// Token: 0x04002E0C RID: 11788
		internal bool _done;
	}
}
