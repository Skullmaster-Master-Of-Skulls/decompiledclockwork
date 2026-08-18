using System;
using System.Security.Cryptography.Pkcs;
using MailBee.Mime;

namespace MailBee.Security
{
	// Token: 0x02000122 RID: 290
	public class SmimeResult
	{
		// Token: 0x06000947 RID: 2375 RVA: 0x0002B9EC File Offset: 0x0002A9EC
		internal SmimeResult()
		{
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x0002B9F4 File Offset: 0x0002A9F4
		public MailMessage DecryptedMessage
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x0002B9FC File Offset: 0x0002A9FC
		public Certificate DecryptionCertificate
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x0002BA04 File Offset: 0x0002AA04
		public Certificate SignatureCertificate
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x0002BA0C File Offset: 0x0002AA0C
		public MessageVerificationFlags VerificationResult
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0002BA14 File Offset: 0x0002AA14
		public SignedCms SignedCmsResult
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x0002BA1C File Offset: 0x0002AA1C
		public EnvelopedCms EnvelopedCmsResult
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x04000752 RID: 1874
		internal MailMessage a;

		// Token: 0x04000753 RID: 1875
		internal Certificate b;

		// Token: 0x04000754 RID: 1876
		internal Certificate c;

		// Token: 0x04000755 RID: 1877
		internal MessageVerificationFlags d;

		// Token: 0x04000756 RID: 1878
		internal SignedCms e;

		// Token: 0x04000757 RID: 1879
		internal EnvelopedCms f;
	}
}
