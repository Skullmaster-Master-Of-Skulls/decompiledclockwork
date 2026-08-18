using System;
using Org.BouncyCastle.X509;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200063A RID: 1594
	public class PdfPublicKeyRecipient
	{
		// Token: 0x060035F6 RID: 13814 RVA: 0x0014F4A9 File Offset: 0x0014E4A9
		public PdfPublicKeyRecipient(X509Certificate certificate, int permission)
		{
			this.certificate = certificate;
			this.permission = permission;
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x060035F7 RID: 13815 RVA: 0x0014F4BF File Offset: 0x0014E4BF
		public X509Certificate Certificate
		{
			get
			{
				return this.certificate;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x060035F8 RID: 13816 RVA: 0x0014F4C7 File Offset: 0x0014E4C7
		public int Permission
		{
			get
			{
				return this.permission;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x060035FA RID: 13818 RVA: 0x0014F4D8 File Offset: 0x0014E4D8
		// (set) Token: 0x060035F9 RID: 13817 RVA: 0x0014F4CF File Offset: 0x0014E4CF
		protected internal byte[] Cms
		{
			get
			{
				return this.cms;
			}
			set
			{
				this.cms = value;
			}
		}

		// Token: 0x04002446 RID: 9286
		private X509Certificate certificate;

		// Token: 0x04002447 RID: 9287
		private int permission;

		// Token: 0x04002448 RID: 9288
		protected byte[] cms;
	}
}
