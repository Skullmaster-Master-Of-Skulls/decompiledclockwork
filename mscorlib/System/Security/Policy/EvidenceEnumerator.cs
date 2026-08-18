using System;
using System.Collections;

namespace System.Security.Policy
{
	// Token: 0x020004A1 RID: 1185
	internal sealed class EvidenceEnumerator : IEnumerator
	{
		// Token: 0x06002F16 RID: 12054 RVA: 0x0009F8A2 File Offset: 0x0009E8A2
		public EvidenceEnumerator(Evidence evidence)
		{
			this.m_evidence = evidence;
			this.Reset();
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x0009F8B8 File Offset: 0x0009E8B8
		public bool MoveNext()
		{
			if (this.m_enumerator == null)
			{
				return false;
			}
			if (this.m_enumerator.MoveNext())
			{
				return true;
			}
			if (this.m_first)
			{
				this.m_enumerator = this.m_evidence.GetAssemblyEnumerator();
				this.m_first = false;
				return this.m_enumerator != null && this.m_enumerator.MoveNext();
			}
			return false;
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06002F18 RID: 12056 RVA: 0x0009F915 File Offset: 0x0009E915
		public object Current
		{
			get
			{
				if (this.m_enumerator == null)
				{
					return null;
				}
				return this.m_enumerator.Current;
			}
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x0009F92C File Offset: 0x0009E92C
		public void Reset()
		{
			this.m_first = true;
			if (this.m_evidence != null)
			{
				this.m_enumerator = this.m_evidence.GetHostEnumerator();
				return;
			}
			this.m_enumerator = null;
		}

		// Token: 0x040017F8 RID: 6136
		private bool m_first;

		// Token: 0x040017F9 RID: 6137
		private Evidence m_evidence;

		// Token: 0x040017FA RID: 6138
		private IEnumerator m_enumerator;
	}
}
