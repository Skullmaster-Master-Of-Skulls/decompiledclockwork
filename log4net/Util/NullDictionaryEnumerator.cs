using System;
using System.Collections;

namespace log4net.Util
{
	// Token: 0x02000108 RID: 264
	public sealed class NullDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x06000799 RID: 1945 RVA: 0x00017BDC File Offset: 0x00015DDC
		private NullDictionaryEnumerator()
		{
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x00017BE4 File Offset: 0x00015DE4
		public static NullDictionaryEnumerator Instance
		{
			get
			{
				return NullDictionaryEnumerator.s_instance;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x00017BEB File Offset: 0x00015DEB
		public object Current
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00017BF2 File Offset: 0x00015DF2
		public bool MoveNext()
		{
			return false;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00017BF5 File Offset: 0x00015DF5
		public void Reset()
		{
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00017BF7 File Offset: 0x00015DF7
		public object Key
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x00017BFE File Offset: 0x00015DFE
		public object Value
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x00017C05 File Offset: 0x00015E05
		public DictionaryEntry Entry
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x040002D3 RID: 723
		private static readonly NullDictionaryEnumerator s_instance = new NullDictionaryEnumerator();
	}
}
