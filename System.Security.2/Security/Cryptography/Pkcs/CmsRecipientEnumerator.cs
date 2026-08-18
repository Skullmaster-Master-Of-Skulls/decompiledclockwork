using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x0200006D RID: 109
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CmsRecipientEnumerator : IEnumerator
	{
		// Token: 0x0600043F RID: 1087 RVA: 0x000044A9 File Offset: 0x000026A9
		private CmsRecipientEnumerator()
		{
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00016852 File Offset: 0x00014A52
		internal CmsRecipientEnumerator(CmsRecipientCollection recipients)
		{
			this.m_recipients = recipients;
			this.m_current = -1;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x00016868 File Offset: 0x00014A68
		public CmsRecipient Current
		{
			get
			{
				return this.m_recipients[this.m_current];
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00016868 File Offset: 0x00014A68
		object IEnumerator.Current
		{
			get
			{
				return this.m_recipients[this.m_current];
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001687B File Offset: 0x00014A7B
		public bool MoveNext()
		{
			if (this.m_current == this.m_recipients.Count - 1)
			{
				return false;
			}
			this.m_current++;
			return true;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000168A3 File Offset: 0x00014AA3
		public void Reset()
		{
			this.m_current = -1;
		}

		// Token: 0x040004BF RID: 1215
		private CmsRecipientCollection m_recipients;

		// Token: 0x040004C0 RID: 1216
		private int m_current;
	}
}
