using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008E7 RID: 2279
	internal struct Triple<T1, T2, T3>
	{
		// Token: 0x060052CA RID: 21194 RVA: 0x0012A6E4 File Offset: 0x001296E4
		internal Triple(T1 first, T2 second, T3 third)
		{
			this._first = first;
			this._second = second;
			this._third = third;
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x060052CB RID: 21195 RVA: 0x0012A6FB File Offset: 0x001296FB
		public T1 Item1
		{
			get
			{
				return this._first;
			}
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x060052CC RID: 21196 RVA: 0x0012A703 File Offset: 0x00129703
		public T2 Item2
		{
			get
			{
				return this._second;
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x060052CD RID: 21197 RVA: 0x0012A70B File Offset: 0x0012970B
		public T3 Item3
		{
			get
			{
				return this._third;
			}
		}

		// Token: 0x04002AAB RID: 10923
		private readonly T1 _first;

		// Token: 0x04002AAC RID: 10924
		private readonly T2 _second;

		// Token: 0x04002AAD RID: 10925
		private readonly T3 _third;
	}
}
