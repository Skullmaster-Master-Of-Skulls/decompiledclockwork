using System;
using System.Security.Cryptography;
using System.Web.Configuration;

namespace System.Web.Security.Cryptography
{
	// Token: 0x0200060C RID: 1548
	internal sealed class MachineKeyCryptoAlgorithmFactory : ICryptoAlgorithmFactory
	{
		// Token: 0x06004DC1 RID: 19905 RVA: 0x0010DF33 File Offset: 0x0010C133
		public MachineKeyCryptoAlgorithmFactory(MachineKeySection machineKeySection)
		{
			this._machineKeySection = machineKeySection;
		}

		// Token: 0x06004DC2 RID: 19906 RVA: 0x0010DF42 File Offset: 0x0010C142
		public SymmetricAlgorithm GetEncryptionAlgorithm()
		{
			if (this._encryptionAlgorithmFactory == null)
			{
				this._encryptionAlgorithmFactory = this.GetEncryptionAlgorithmFactory();
			}
			return this._encryptionAlgorithmFactory();
		}

		// Token: 0x06004DC3 RID: 19907 RVA: 0x0010DF63 File Offset: 0x0010C163
		private Func<SymmetricAlgorithm> GetEncryptionAlgorithmFactory()
		{
			return this.GetGenericAlgorithmFactory<SymmetricAlgorithm>("decryption", this._machineKeySection.GetDecryptionAttributeSkipValidation(), delegate(string algorithmName)
			{
				if (algorithmName == "AES" || algorithmName == "Auto")
				{
					return new Func<SymmetricAlgorithm>(CryptoAlgorithms.CreateAes);
				}
				if (algorithmName == "DES")
				{
					return new Func<SymmetricAlgorithm>(CryptoAlgorithms.CreateDES);
				}
				if (!(algorithmName == "3DES"))
				{
					return null;
				}
				return new Func<SymmetricAlgorithm>(CryptoAlgorithms.CreateTripleDES);
			}, "Wrong_decryption_enum");
		}

		// Token: 0x06004DC4 RID: 19908 RVA: 0x0010DF9F File Offset: 0x0010C19F
		public KeyedHashAlgorithm GetValidationAlgorithm()
		{
			if (this._validationAlgorithmFactory == null)
			{
				this._validationAlgorithmFactory = this.GetValidationAlgorithmFactory();
			}
			return this._validationAlgorithmFactory();
		}

		// Token: 0x06004DC5 RID: 19909 RVA: 0x0010DFC0 File Offset: 0x0010C1C0
		private Func<KeyedHashAlgorithm> GetValidationAlgorithmFactory()
		{
			return this.GetGenericAlgorithmFactory<KeyedHashAlgorithm>("validation", this._machineKeySection.GetValidationAttributeSkipValidation(), delegate(string algorithmName)
			{
				if (algorithmName == "SHA1")
				{
					return new Func<KeyedHashAlgorithm>(CryptoAlgorithms.CreateHMACSHA1);
				}
				if (algorithmName == "HMACSHA256")
				{
					return new Func<KeyedHashAlgorithm>(CryptoAlgorithms.CreateHMACSHA256);
				}
				if (algorithmName == "HMACSHA384")
				{
					return new Func<KeyedHashAlgorithm>(CryptoAlgorithms.CreateHMACSHA384);
				}
				if (!(algorithmName == "HMACSHA512"))
				{
					return null;
				}
				return new Func<KeyedHashAlgorithm>(CryptoAlgorithms.CreateHMACSHA512);
			}, "Wrong_validation_enum_FX45");
		}

		// Token: 0x06004DC6 RID: 19910 RVA: 0x0010DFFC File Offset: 0x0010C1FC
		private Func<TResult> GetGenericAlgorithmFactory<TResult>(string configAttributeName, string configAttributeValue, Func<string, Func<TResult>> switchStatement, string errorResourceString) where TResult : class, IDisposable
		{
			Func<TResult> func;
			if (configAttributeValue != null && configAttributeValue.StartsWith("alg:", StringComparison.Ordinal))
			{
				string algorithmName = configAttributeValue.Substring("alg:".Length);
				func = delegate()
				{
					TResult result;
					using (new ApplicationImpersonationContext())
					{
						result = (TResult)((object)CryptoConfig.CreateFromName(algorithmName));
					}
					return result;
				};
			}
			else
			{
				func = switchStatement(configAttributeValue);
			}
			Exception innerException = null;
			try
			{
				if (func != null)
				{
					TResult tresult = func();
					if (tresult != null)
					{
						tresult.Dispose();
						return func;
					}
				}
			}
			catch (Exception ex)
			{
				innerException = ex;
			}
			throw ConfigUtil.MakeConfigurationErrorsException(SR.GetString(errorResourceString), innerException, this._machineKeySection.ElementInformation.Properties[configAttributeName]);
		}

		// Token: 0x04002971 RID: 10609
		private Func<SymmetricAlgorithm> _encryptionAlgorithmFactory;

		// Token: 0x04002972 RID: 10610
		private readonly MachineKeySection _machineKeySection;

		// Token: 0x04002973 RID: 10611
		private Func<KeyedHashAlgorithm> _validationAlgorithmFactory;
	}
}
