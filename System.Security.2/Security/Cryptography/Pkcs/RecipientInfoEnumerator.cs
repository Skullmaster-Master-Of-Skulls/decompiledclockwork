using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x0200007B RID: 123
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class RecipientInfoEnumerator : IEnumerator
	{
		// Token: 0x060004A5 RID: 1189 RVA: 0x000044A9 File Offset: 0x000026A9
		private RecipientInfoEnumerator()
		{
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000178CA File Offset: 0x00015ACA
		internal RecipientInfoEnumerator(RecipientInfoCollection RecipientInfos)
		{
			this.m_recipientInfos = RecipientInfos;
			this.m_current = -1;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x000178E0 File Offset: 0x00015AE0
		public RecipientInfo Current
		{
			get
			{
				return this.m_recipientInfos[this.m_current];
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x000178E0 File Offset: 0x00015AE0
		object IEnumerator.Current
		{
			get
			{
				return this.m_recipientInfos[this.m_current];
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000178F3 File Offset: 0x00015AF3
		public bool MoveNext()
		{
			if (this.m_current == this.m_recipientInfos.Count - 1)
			{
				return false;
			}
			this.m_current++;
			return true;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001791B File Offset: 0x00015B1B
		public void Reset()
		{
			this.m_current = -1;
		}

		// Token: 0x040004F4 RID: 1268
		private RecipientInfoCollection m_recipientInfos;

		// Token: 0x040004F5 RID: 1269
		private int m_current;
	}
}
