using System;
using System.Collections;

namespace log4net.Util
{
	// Token: 0x02000109 RID: 265
	public sealed class NullEnumerator : IEnumerator
	{
		// Token: 0x060007A2 RID: 1954 RVA: 0x00017C18 File Offset: 0x00015E18
		private NullEnumerator()
		{
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00017C20 File Offset: 0x00015E20
		public static NullEnumerator Instance
		{
			get
			{
				return NullEnumerator.s_instance;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x00017C27 File Offset: 0x00015E27
		public object Current
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00017C2E File Offset: 0x00015E2E
		public bool MoveNext()
		{
			return false;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00017C31 File Offset: 0x00015E31
		public void Reset()
		{
		}

		// Token: 0x040002D4 RID: 724
		private static readonly NullEnumerator s_instance = new NullEnumerator();
	}
}
