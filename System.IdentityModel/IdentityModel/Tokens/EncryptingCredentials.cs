using System;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200011F RID: 287
	public class EncryptingCredentials
	{
		// Token: 0x060007DE RID: 2014 RVA: 0x00004469 File Offset: 0x00002669
		public EncryptingCredentials()
		{
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x000211BC File Offset: 0x0001F3BC
		public EncryptingCredentials(SecurityKey key, SecurityKeyIdentifier keyIdentifier, string algorithm)
		{
			if (key == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
			}
			if (keyIdentifier == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifier");
			}
			if (string.IsNullOrEmpty(algorithm))
			{
				throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("algorithm");
			}
			this._algorithm = algorithm;
			this._key = key;
			this._keyIdentifier = keyIdentifier;
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0002121D File Offset: 0x0001F41D
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x00021225 File Offset: 0x0001F425
		public string Algorithm
		{
			get
			{
				return this._algorithm;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("value");
				}
				this._algorithm = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x00021241 File Offset: 0x0001F441
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x00021249 File Offset: 0x0001F449
		public SecurityKey SecurityKey
		{
			get
			{
				return this._key;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._key = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x00021265 File Offset: 0x0001F465
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x0002126D File Offset: 0x0001F46D
		public SecurityKeyIdentifier SecurityKeyIdentifier
		{
			get
			{
				return this._keyIdentifier;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._keyIdentifier = value;
			}
		}

		// Token: 0x04000AE0 RID: 2784
		private string _algorithm;

		// Token: 0x04000AE1 RID: 2785
		private SecurityKey _key;

		// Token: 0x04000AE2 RID: 2786
		private SecurityKeyIdentifier _keyIdentifier;
	}
}
