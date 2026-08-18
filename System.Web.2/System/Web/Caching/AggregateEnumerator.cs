using System;
using System.Collections;

namespace System.Web.Caching
{
	// Token: 0x02000874 RID: 2164
	internal class AggregateEnumerator : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x060065F2 RID: 26098 RVA: 0x0016777A File Offset: 0x0016597A
		internal AggregateEnumerator(IDictionaryEnumerator[] enumerators)
		{
			this._enumerators = enumerators;
		}

		// Token: 0x060065F3 RID: 26099 RVA: 0x0016778C File Offset: 0x0016598C
		public bool MoveNext()
		{
			bool flag;
			for (;;)
			{
				flag = this._enumerators[this._iCurrent].MoveNext();
				if (flag || this._iCurrent == this._enumerators.Length - 1)
				{
					break;
				}
				this._iCurrent++;
			}
			return flag;
		}

		// Token: 0x060065F4 RID: 26100 RVA: 0x001677D4 File Offset: 0x001659D4
		public void Reset()
		{
			for (int i = 0; i <= this._iCurrent; i++)
			{
				this._enumerators[i].Reset();
			}
			this._iCurrent = 0;
		}

		// Token: 0x17001C8C RID: 7308
		// (get) Token: 0x060065F5 RID: 26101 RVA: 0x00167806 File Offset: 0x00165A06
		public object Current
		{
			get
			{
				return this._enumerators[this._iCurrent].Current;
			}
		}

		// Token: 0x17001C8D RID: 7309
		// (get) Token: 0x060065F6 RID: 26102 RVA: 0x0016781A File Offset: 0x00165A1A
		public object Key
		{
			get
			{
				return this._enumerators[this._iCurrent].Key;
			}
		}

		// Token: 0x17001C8E RID: 7310
		// (get) Token: 0x060065F7 RID: 26103 RVA: 0x0016782E File Offset: 0x00165A2E
		public object Value
		{
			get
			{
				return this._enumerators[this._iCurrent].Value;
			}
		}

		// Token: 0x17001C8F RID: 7311
		// (get) Token: 0x060065F8 RID: 26104 RVA: 0x00167842 File Offset: 0x00165A42
		public DictionaryEntry Entry
		{
			get
			{
				return this._enumerators[this._iCurrent].Entry;
			}
		}

		// Token: 0x0400348A RID: 13450
		private IDictionaryEnumerator[] _enumerators;

		// Token: 0x0400348B RID: 13451
		private int _iCurrent;
	}
}
