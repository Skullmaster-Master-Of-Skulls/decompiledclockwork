using System;
using System.Collections;

namespace System.Security.Cryptography
{
	// Token: 0x0200044F RID: 1103
	public sealed class AsnEncodedDataEnumerator : IEnumerator
	{
		// Token: 0x060028DF RID: 10463 RVA: 0x000BB10A File Offset: 0x000B930A
		private AsnEncodedDataEnumerator()
		{
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x000BB112 File Offset: 0x000B9312
		internal AsnEncodedDataEnumerator(AsnEncodedDataCollection asnEncodedDatas)
		{
			this.m_asnEncodedDatas = asnEncodedDatas;
			this.m_current = -1;
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x060028E1 RID: 10465 RVA: 0x000BB128 File Offset: 0x000B9328
		public AsnEncodedData Current
		{
			get
			{
				return this.m_asnEncodedDatas[this.m_current];
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x060028E2 RID: 10466 RVA: 0x000BB13B File Offset: 0x000B933B
		object IEnumerator.Current
		{
			get
			{
				return this.m_asnEncodedDatas[this.m_current];
			}
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x000BB14E File Offset: 0x000B934E
		public bool MoveNext()
		{
			if (this.m_current == this.m_asnEncodedDatas.Count - 1)
			{
				return false;
			}
			this.m_current++;
			return true;
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000BB176 File Offset: 0x000B9376
		public void Reset()
		{
			this.m_current = -1;
		}

		// Token: 0x04002286 RID: 8838
		private AsnEncodedDataCollection m_asnEncodedDatas;

		// Token: 0x04002287 RID: 8839
		private int m_current;
	}
}
