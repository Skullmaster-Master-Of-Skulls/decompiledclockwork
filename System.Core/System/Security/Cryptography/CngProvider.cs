using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x020000EE RID: 238
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public sealed class CngProvider : IEquatable<CngProvider>
	{
		// Token: 0x0600077E RID: 1918 RVA: 0x00018764 File Offset: 0x00016964
		public CngProvider(string provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (provider.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Cryptography_InvalidProviderName", new object[]
				{
					provider
				}), "provider");
			}
			this.m_provider = provider;
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x000187B3 File Offset: 0x000169B3
		public string Provider
		{
			get
			{
				return this.m_provider;
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x000187BB File Offset: 0x000169BB
		public static bool operator ==(CngProvider left, CngProvider right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x000187CC File Offset: 0x000169CC
		public static bool operator !=(CngProvider left, CngProvider right)
		{
			if (left == null)
			{
				return right != null;
			}
			return !left.Equals(right);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x000187E0 File Offset: 0x000169E0
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CngProvider);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x000187EE File Offset: 0x000169EE
		public bool Equals(CngProvider other)
		{
			return other != null && this.m_provider.Equals(other.Provider);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00018806 File Offset: 0x00016A06
		public override int GetHashCode()
		{
			return this.m_provider.GetHashCode();
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00018813 File Offset: 0x00016A13
		public override string ToString()
		{
			return this.m_provider.ToString();
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x00018820 File Offset: 0x00016A20
		public static CngProvider MicrosoftSmartCardKeyStorageProvider
		{
			get
			{
				if (CngProvider.s_msSmartCardKsp == null)
				{
					CngProvider.s_msSmartCardKsp = new CngProvider("Microsoft Smart Card Key Storage Provider");
				}
				return CngProvider.s_msSmartCardKsp;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x00018849 File Offset: 0x00016A49
		public static CngProvider MicrosoftSoftwareKeyStorageProvider
		{
			get
			{
				if (CngProvider.s_msSoftwareKsp == null)
				{
					CngProvider.s_msSoftwareKsp = new CngProvider("Microsoft Software Key Storage Provider");
				}
				return CngProvider.s_msSoftwareKsp;
			}
		}

		// Token: 0x04000627 RID: 1575
		private static volatile CngProvider s_msSmartCardKsp;

		// Token: 0x04000628 RID: 1576
		private static volatile CngProvider s_msSoftwareKsp;

		// Token: 0x04000629 RID: 1577
		private string m_provider;
	}
}
