using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Configuration;

namespace System.Web.Security.Cryptography
{
	// Token: 0x0200060D RID: 1549
	internal sealed class MachineKeyMasterKeyProvider : IMasterKeyProvider
	{
		// Token: 0x06004DC7 RID: 19911 RVA: 0x0010E0B0 File Offset: 0x0010C2B0
		internal MachineKeyMasterKeyProvider(MachineKeySection machineKeySection, string applicationId = null, string applicationName = null, CryptographicKey autogenKeys = null, KeyDerivationFunction keyDerivationFunction = null)
		{
			this._machineKeySection = machineKeySection;
			this._applicationId = applicationId;
			this._applicationName = applicationName;
			this._autogenKeys = autogenKeys;
			this._keyDerivationFunction = keyDerivationFunction;
		}

		// Token: 0x170016C9 RID: 5833
		// (get) Token: 0x06004DC8 RID: 19912 RVA: 0x0010E0DD File Offset: 0x0010C2DD
		internal string ApplicationName
		{
			get
			{
				if (this._applicationName == null)
				{
					this._applicationName = (HttpRuntime.AppDomainAppVirtualPath ?? Process.GetCurrentProcess().MainModule.ModuleName);
				}
				return this._applicationName;
			}
		}

		// Token: 0x170016CA RID: 5834
		// (get) Token: 0x06004DC9 RID: 19913 RVA: 0x0010E10B File Offset: 0x0010C30B
		internal string ApplicationId
		{
			get
			{
				if (this._applicationId == null)
				{
					this._applicationId = HttpRuntime.AppDomainAppId;
				}
				return this._applicationId;
			}
		}

		// Token: 0x170016CB RID: 5835
		// (get) Token: 0x06004DCA RID: 19914 RVA: 0x0010E126 File Offset: 0x0010C326
		internal CryptographicKey AutogenKeys
		{
			get
			{
				if (this._autogenKeys == null)
				{
					this._autogenKeys = new CryptographicKey(HttpRuntime.s_autogenKeys);
				}
				return this._autogenKeys;
			}
		}

		// Token: 0x170016CC RID: 5836
		// (get) Token: 0x06004DCB RID: 19915 RVA: 0x0010E146 File Offset: 0x0010C346
		internal KeyDerivationFunction KeyDerivationFunction
		{
			get
			{
				if (this._keyDerivationFunction == null)
				{
					this._keyDerivationFunction = new KeyDerivationFunction(SP800_108.DeriveKey);
				}
				return this._keyDerivationFunction;
			}
		}

		// Token: 0x06004DCC RID: 19916 RVA: 0x0010E168 File Offset: 0x0010C368
		private static void AddSpecificPurposeString(IList<string> specificPurposes, string key, string value)
		{
			specificPurposes.Add(key + ": " + value);
		}

		// Token: 0x06004DCD RID: 19917 RVA: 0x0010E17C File Offset: 0x0010C37C
		private CryptographicKey GenerateCryptographicKey(string configAttributeName, string configAttributeValue, int autogenKeyOffset, int autogenKeyCount, string errorResourceString)
		{
			byte[] array = CryptoUtil.HexToBinary(configAttributeValue);
			if (array != null && array.Length != 0)
			{
				return new CryptographicKey(array);
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (configAttributeValue != null)
			{
				foreach (string a in configAttributeValue.Split(new char[]
				{
					','
				}))
				{
					if (!(a == "AutoGenerate"))
					{
						if (!(a == "IsolateApps"))
						{
							if (!(a == "IsolateByAppId"))
							{
								throw ConfigUtil.MakeConfigurationErrorsException(SR.GetString(errorResourceString), null, this._machineKeySection.ElementInformation.Properties[configAttributeName]);
							}
							flag3 = true;
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag = true;
					}
				}
			}
			if (!flag)
			{
				throw ConfigUtil.MakeConfigurationErrorsException(SR.GetString(errorResourceString), null, this._machineKeySection.ElementInformation.Properties[configAttributeName]);
			}
			CryptographicKey keyDerivationKey = this.AutogenKeys.ExtractBits(autogenKeyOffset, autogenKeyCount);
			List<string> list = new List<string>();
			if (flag2)
			{
				MachineKeyMasterKeyProvider.AddSpecificPurposeString(list, "IsolateApps", this.ApplicationName);
			}
			if (flag3)
			{
				MachineKeyMasterKeyProvider.AddSpecificPurposeString(list, "IsolateByAppId", this.ApplicationId);
			}
			Purpose purpose = new Purpose("MachineKeyDerivation", list.ToArray());
			return this.KeyDerivationFunction(keyDerivationKey, purpose);
		}

		// Token: 0x06004DCE RID: 19918 RVA: 0x0010E2BD File Offset: 0x0010C4BD
		public CryptographicKey GetEncryptionKey()
		{
			if (this._encryptionKey == null)
			{
				this._encryptionKey = this.GenerateCryptographicKey("decryptionKey", this._machineKeySection.DecryptionKey, 0, 256, "Invalid_decryption_key");
			}
			return this._encryptionKey;
		}

		// Token: 0x06004DCF RID: 19919 RVA: 0x0010E2F4 File Offset: 0x0010C4F4
		public CryptographicKey GetValidationKey()
		{
			if (this._validationKey == null)
			{
				this._validationKey = this.GenerateCryptographicKey("validationKey", this._machineKeySection.ValidationKey, 256, 256, "Invalid_validation_key");
			}
			return this._validationKey;
		}

		// Token: 0x04002974 RID: 10612
		private const int AUTOGEN_ENCRYPTION_OFFSET = 0;

		// Token: 0x04002975 RID: 10613
		private const int AUTOGEN_ENCRYPTION_KEYLENGTH = 256;

		// Token: 0x04002976 RID: 10614
		private const int AUTOGEN_VALIDATION_OFFSET = 256;

		// Token: 0x04002977 RID: 10615
		private const int AUTOGEN_VALIDATION_KEYLENGTH = 256;

		// Token: 0x04002978 RID: 10616
		private const string AUTOGEN_KEYDERIVATION_PRIMARYPURPOSE = "MachineKeyDerivation";

		// Token: 0x04002979 RID: 10617
		private const string AUTOGEN_KEYDERIVATION_ISOLATEAPPS_SPECIFICPURPOSE = "IsolateApps";

		// Token: 0x0400297A RID: 10618
		private const string AUTOGEN_KEYDERIVATION_ISOLATEBYAPPID_SPECIFICPURPOSE = "IsolateByAppId";

		// Token: 0x0400297B RID: 10619
		private string _applicationId;

		// Token: 0x0400297C RID: 10620
		private string _applicationName;

		// Token: 0x0400297D RID: 10621
		private CryptographicKey _autogenKeys;

		// Token: 0x0400297E RID: 10622
		private CryptographicKey _encryptionKey;

		// Token: 0x0400297F RID: 10623
		private KeyDerivationFunction _keyDerivationFunction;

		// Token: 0x04002980 RID: 10624
		private readonly MachineKeySection _machineKeySection;

		// Token: 0x04002981 RID: 10625
		private CryptographicKey _validationKey;
	}
}
