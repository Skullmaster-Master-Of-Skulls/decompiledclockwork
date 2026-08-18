using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x02000018 RID: 24
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CryptographicAttributeObjectEnumerator : IEnumerator
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x000044A9 File Offset: 0x000026A9
		private CryptographicAttributeObjectEnumerator()
		{
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004992 File Offset: 0x00002B92
		internal CryptographicAttributeObjectEnumerator(CryptographicAttributeObjectCollection attributes)
		{
			this.m_attributes = attributes;
			this.m_current = -1;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000049A8 File Offset: 0x00002BA8
		public CryptographicAttributeObject Current
		{
			get
			{
				return this.m_attributes[this.m_current];
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000049A8 File Offset: 0x00002BA8
		object IEnumerator.Current
		{
			get
			{
				return this.m_attributes[this.m_current];
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000049BB File Offset: 0x00002BBB
		public bool MoveNext()
		{
			if (this.m_current == this.m_attributes.Count - 1)
			{
				return false;
			}
			this.m_current++;
			return true;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000049E3 File Offset: 0x00002BE3
		public void Reset()
		{
			this.m_current = -1;
		}

		// Token: 0x04000380 RID: 896
		private CryptographicAttributeObjectCollection m_attributes;

		// Token: 0x04000381 RID: 897
		private int m_current;
	}
}
