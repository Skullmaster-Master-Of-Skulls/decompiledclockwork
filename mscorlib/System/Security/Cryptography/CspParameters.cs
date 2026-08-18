using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace System.Security.Cryptography
{
	// Token: 0x02000873 RID: 2163
	[ComVisible(true)]
	public sealed class CspParameters
	{
		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x06004ECE RID: 20174 RVA: 0x00110DCD File Offset: 0x0010FDCD
		// (set) Token: 0x06004ECF RID: 20175 RVA: 0x00110DD8 File Offset: 0x0010FDD8
		public CspProviderFlags Flags
		{
			get
			{
				return (CspProviderFlags)this.m_flags;
			}
			set
			{
				uint num = 2147483775U;
				if ((value & (CspProviderFlags)(~(CspProviderFlags)num)) != CspProviderFlags.NoFlags)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
					{
						(int)value
					}), "value");
				}
				this.m_flags = (uint)value;
			}
		}

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06004ED0 RID: 20176 RVA: 0x00110E20 File Offset: 0x0010FE20
		// (set) Token: 0x06004ED1 RID: 20177 RVA: 0x00110E28 File Offset: 0x0010FE28
		public CryptoKeySecurity CryptoKeySecurity
		{
			get
			{
				return this.m_cryptoKeySecurity;
			}
			set
			{
				this.m_cryptoKeySecurity = value;
			}
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06004ED2 RID: 20178 RVA: 0x00110E31 File Offset: 0x0010FE31
		// (set) Token: 0x06004ED3 RID: 20179 RVA: 0x00110E39 File Offset: 0x0010FE39
		public SecureString KeyPassword
		{
			get
			{
				return this.m_keyPassword;
			}
			set
			{
				this.m_keyPassword = value;
				this.m_parentWindowHandle = IntPtr.Zero;
			}
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06004ED4 RID: 20180 RVA: 0x00110E4D File Offset: 0x0010FE4D
		// (set) Token: 0x06004ED5 RID: 20181 RVA: 0x00110E55 File Offset: 0x0010FE55
		public IntPtr ParentWindowHandle
		{
			get
			{
				return this.m_parentWindowHandle;
			}
			set
			{
				this.m_parentWindowHandle = value;
				this.m_keyPassword = null;
			}
		}

		// Token: 0x06004ED6 RID: 20182 RVA: 0x00110E65 File Offset: 0x0010FE65
		public CspParameters() : this(Utils.DefaultRsaProviderType, null, null)
		{
		}

		// Token: 0x06004ED7 RID: 20183 RVA: 0x00110E74 File Offset: 0x0010FE74
		public CspParameters(int dwTypeIn) : this(dwTypeIn, null, null)
		{
		}

		// Token: 0x06004ED8 RID: 20184 RVA: 0x00110E7F File Offset: 0x0010FE7F
		public CspParameters(int dwTypeIn, string strProviderNameIn) : this(dwTypeIn, strProviderNameIn, null)
		{
		}

		// Token: 0x06004ED9 RID: 20185 RVA: 0x00110E8A File Offset: 0x0010FE8A
		public CspParameters(int dwTypeIn, string strProviderNameIn, string strContainerNameIn) : this(dwTypeIn, strProviderNameIn, strContainerNameIn, CspProviderFlags.NoFlags)
		{
		}

		// Token: 0x06004EDA RID: 20186 RVA: 0x00110E96 File Offset: 0x0010FE96
		public CspParameters(int providerType, string providerName, string keyContainerName, CryptoKeySecurity cryptoKeySecurity, SecureString keyPassword) : this(providerType, providerName, keyContainerName)
		{
			this.m_cryptoKeySecurity = cryptoKeySecurity;
			this.m_keyPassword = keyPassword;
		}

		// Token: 0x06004EDB RID: 20187 RVA: 0x00110EB1 File Offset: 0x0010FEB1
		public CspParameters(int providerType, string providerName, string keyContainerName, CryptoKeySecurity cryptoKeySecurity, IntPtr parentWindowHandle) : this(providerType, providerName, keyContainerName)
		{
			this.m_cryptoKeySecurity = cryptoKeySecurity;
			this.m_parentWindowHandle = parentWindowHandle;
		}

		// Token: 0x06004EDC RID: 20188 RVA: 0x00110ECC File Offset: 0x0010FECC
		internal CspParameters(int providerType, string providerName, string keyContainerName, CspProviderFlags flags)
		{
			this.ProviderType = providerType;
			this.ProviderName = providerName;
			this.KeyContainerName = keyContainerName;
			this.KeyNumber = -1;
			this.Flags = flags;
		}

		// Token: 0x06004EDD RID: 20189 RVA: 0x00110EF8 File Offset: 0x0010FEF8
		internal CspParameters(CspParameters parameters)
		{
			this.ProviderType = parameters.ProviderType;
			this.ProviderName = parameters.ProviderName;
			this.KeyContainerName = parameters.KeyContainerName;
			this.KeyNumber = parameters.KeyNumber;
			this.Flags = parameters.Flags;
			this.m_cryptoKeySecurity = parameters.m_cryptoKeySecurity;
			this.m_keyPassword = parameters.m_keyPassword;
			this.m_parentWindowHandle = parameters.m_parentWindowHandle;
		}

		// Token: 0x040028B5 RID: 10421
		public int ProviderType;

		// Token: 0x040028B6 RID: 10422
		public string ProviderName;

		// Token: 0x040028B7 RID: 10423
		public string KeyContainerName;

		// Token: 0x040028B8 RID: 10424
		public int KeyNumber;

		// Token: 0x040028B9 RID: 10425
		private uint m_flags;

		// Token: 0x040028BA RID: 10426
		private CryptoKeySecurity m_cryptoKeySecurity;

		// Token: 0x040028BB RID: 10427
		private SecureString m_keyPassword;

		// Token: 0x040028BC RID: 10428
		private IntPtr m_parentWindowHandle;
	}
}
