using System;
using System.Globalization;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000474 RID: 1140
	public sealed class X509ChainPolicy
	{
		// Token: 0x06002A53 RID: 10835 RVA: 0x000C13DB File Offset: 0x000BF5DB
		public X509ChainPolicy()
		{
			this.Reset();
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06002A54 RID: 10836 RVA: 0x000C13E9 File Offset: 0x000BF5E9
		public OidCollection ApplicationPolicy
		{
			get
			{
				return this.m_applicationPolicy;
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06002A55 RID: 10837 RVA: 0x000C13F1 File Offset: 0x000BF5F1
		public OidCollection CertificatePolicy
		{
			get
			{
				return this.m_certificatePolicy;
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06002A56 RID: 10838 RVA: 0x000C13F9 File Offset: 0x000BF5F9
		// (set) Token: 0x06002A57 RID: 10839 RVA: 0x000C1401 File Offset: 0x000BF601
		public X509RevocationMode RevocationMode
		{
			get
			{
				return this.m_revocationMode;
			}
			set
			{
				if (value < X509RevocationMode.NoCheck || value > X509RevocationMode.Offline)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Arg_EnumIllegalVal"), new object[]
					{
						"value"
					}));
				}
				this.m_revocationMode = value;
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06002A58 RID: 10840 RVA: 0x000C143A File Offset: 0x000BF63A
		// (set) Token: 0x06002A59 RID: 10841 RVA: 0x000C1442 File Offset: 0x000BF642
		public X509RevocationFlag RevocationFlag
		{
			get
			{
				return this.m_revocationFlag;
			}
			set
			{
				if (value < X509RevocationFlag.EndCertificateOnly || value > X509RevocationFlag.ExcludeRoot)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Arg_EnumIllegalVal"), new object[]
					{
						"value"
					}));
				}
				this.m_revocationFlag = value;
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06002A5A RID: 10842 RVA: 0x000C147B File Offset: 0x000BF67B
		// (set) Token: 0x06002A5B RID: 10843 RVA: 0x000C1483 File Offset: 0x000BF683
		public X509VerificationFlags VerificationFlags
		{
			get
			{
				return this.m_verificationFlags;
			}
			set
			{
				if (value < X509VerificationFlags.NoFlag || value > X509VerificationFlags.AllFlags)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Arg_EnumIllegalVal"), new object[]
					{
						"value"
					}));
				}
				this.m_verificationFlags = value;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06002A5C RID: 10844 RVA: 0x000C14C0 File Offset: 0x000BF6C0
		// (set) Token: 0x06002A5D RID: 10845 RVA: 0x000C14C8 File Offset: 0x000BF6C8
		public DateTime VerificationTime
		{
			get
			{
				return this.m_verificationTime;
			}
			set
			{
				this.m_verificationTime = value;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06002A5E RID: 10846 RVA: 0x000C14D1 File Offset: 0x000BF6D1
		// (set) Token: 0x06002A5F RID: 10847 RVA: 0x000C14D9 File Offset: 0x000BF6D9
		public TimeSpan UrlRetrievalTimeout
		{
			get
			{
				return this.m_timeout;
			}
			set
			{
				this.m_timeout = value;
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06002A60 RID: 10848 RVA: 0x000C14E2 File Offset: 0x000BF6E2
		public X509Certificate2Collection ExtraStore
		{
			get
			{
				return this.m_extraStore;
			}
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x000C14EC File Offset: 0x000BF6EC
		public void Reset()
		{
			this.m_applicationPolicy = new OidCollection();
			this.m_certificatePolicy = new OidCollection();
			this.m_revocationMode = X509RevocationMode.Online;
			this.m_revocationFlag = X509RevocationFlag.ExcludeRoot;
			this.m_verificationFlags = X509VerificationFlags.NoFlag;
			this.m_verificationTime = DateTime.Now;
			this.m_timeout = new TimeSpan(0, 0, 0);
			this.m_extraStore = new X509Certificate2Collection();
		}

		// Token: 0x04002625 RID: 9765
		private OidCollection m_applicationPolicy;

		// Token: 0x04002626 RID: 9766
		private OidCollection m_certificatePolicy;

		// Token: 0x04002627 RID: 9767
		private X509RevocationMode m_revocationMode;

		// Token: 0x04002628 RID: 9768
		private X509RevocationFlag m_revocationFlag;

		// Token: 0x04002629 RID: 9769
		private DateTime m_verificationTime;

		// Token: 0x0400262A RID: 9770
		private TimeSpan m_timeout;

		// Token: 0x0400262B RID: 9771
		private X509Certificate2Collection m_extraStore;

		// Token: 0x0400262C RID: 9772
		private X509VerificationFlags m_verificationFlags;
	}
}
