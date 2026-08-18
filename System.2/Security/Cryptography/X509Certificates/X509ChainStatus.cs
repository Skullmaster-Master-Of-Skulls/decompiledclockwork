using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x0200046C RID: 1132
	public struct X509ChainStatus
	{
		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06002A28 RID: 10792 RVA: 0x000C0775 File Offset: 0x000BE975
		// (set) Token: 0x06002A29 RID: 10793 RVA: 0x000C077D File Offset: 0x000BE97D
		public X509ChainStatusFlags Status
		{
			get
			{
				return this.m_status;
			}
			set
			{
				this.m_status = value;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06002A2A RID: 10794 RVA: 0x000C0786 File Offset: 0x000BE986
		// (set) Token: 0x06002A2B RID: 10795 RVA: 0x000C079C File Offset: 0x000BE99C
		public string StatusInformation
		{
			get
			{
				if (this.m_statusInformation == null)
				{
					return string.Empty;
				}
				return this.m_statusInformation;
			}
			set
			{
				this.m_statusInformation = value;
			}
		}

		// Token: 0x040025FE RID: 9726
		private X509ChainStatusFlags m_status;

		// Token: 0x040025FF RID: 9727
		private string m_statusInformation;
	}
}
