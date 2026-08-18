using System;
using System.Web.Configuration;

namespace System.Web.Security.Cryptography
{
	// Token: 0x020005FE RID: 1534
	internal sealed class AspNetCryptoServiceProvider : ICryptoServiceProvider
	{
		// Token: 0x06004D86 RID: 19846 RVA: 0x0010D5CC File Offset: 0x0010B7CC
		internal AspNetCryptoServiceProvider(MachineKeySection machineKeySection = null, ICryptoAlgorithmFactory cryptoAlgorithmFactory = null, IMasterKeyProvider masterKeyProvider = null, IDataProtectorFactory dataProtectorFactory = null, KeyDerivationFunction keyDerivationFunction = null)
		{
			this._machineKeySection = machineKeySection;
			this._cryptoAlgorithmFactory = cryptoAlgorithmFactory;
			this._masterKeyProvider = masterKeyProvider;
			this._dataProtectorFactory = dataProtectorFactory;
			this._keyDerivationFunction = keyDerivationFunction;
			this.IsDefaultProvider = (machineKeySection != null && machineKeySection.CompatibilityMode >= MachineKeyCompatibilityMode.Framework45);
			this._isDataProtectorEnabled = (machineKeySection != null && !string.IsNullOrWhiteSpace(machineKeySection.DataProtectorType));
		}

		// Token: 0x170016C5 RID: 5829
		// (get) Token: 0x06004D87 RID: 19847 RVA: 0x0010D636 File Offset: 0x0010B836
		internal static AspNetCryptoServiceProvider Instance
		{
			get
			{
				return AspNetCryptoServiceProvider._singleton.Value;
			}
		}

		// Token: 0x170016C6 RID: 5830
		// (get) Token: 0x06004D88 RID: 19848 RVA: 0x0010D642 File Offset: 0x0010B842
		// (set) Token: 0x06004D89 RID: 19849 RVA: 0x0010D64A File Offset: 0x0010B84A
		internal bool IsDefaultProvider { get; private set; }

		// Token: 0x06004D8A RID: 19850 RVA: 0x0010D654 File Offset: 0x0010B854
		public ICryptoService GetCryptoService(Purpose purpose, CryptoServiceOptions options = CryptoServiceOptions.None)
		{
			ICryptoService wrapped;
			if (this._isDataProtectorEnabled && options == CryptoServiceOptions.None)
			{
				wrapped = this.GetDataProtectorCryptoService(purpose);
			}
			else
			{
				wrapped = this.GetNetFXCryptoService(purpose, options);
			}
			return new HomogenizingCryptoServiceWrapper(wrapped);
		}

		// Token: 0x06004D8B RID: 19851 RVA: 0x0010D685 File Offset: 0x0010B885
		private DataProtectorCryptoService GetDataProtectorCryptoService(Purpose purpose)
		{
			return new DataProtectorCryptoService(this._dataProtectorFactory, purpose);
		}

		// Token: 0x06004D8C RID: 19852 RVA: 0x0010D694 File Offset: 0x0010B894
		private NetFXCryptoService GetNetFXCryptoService(Purpose purpose, CryptoServiceOptions options)
		{
			CryptographicKey derivedEncryptionKey = purpose.GetDerivedEncryptionKey(this._masterKeyProvider, this._keyDerivationFunction);
			CryptographicKey derivedValidationKey = purpose.GetDerivedValidationKey(this._masterKeyProvider, this._keyDerivationFunction);
			return new NetFXCryptoService(this._cryptoAlgorithmFactory, derivedEncryptionKey, derivedValidationKey, options == CryptoServiceOptions.CacheableOutput);
		}

		// Token: 0x06004D8D RID: 19853 RVA: 0x0010D6D8 File Offset: 0x0010B8D8
		private static AspNetCryptoServiceProvider GetSingletonCryptoServiceProvider()
		{
			MachineKeySection applicationConfig = MachineKeySection.GetApplicationConfig();
			return new AspNetCryptoServiceProvider(applicationConfig, new MachineKeyCryptoAlgorithmFactory(applicationConfig), new MachineKeyMasterKeyProvider(applicationConfig, null, null, null, null), new MachineKeyDataProtectorFactory(applicationConfig), new KeyDerivationFunction(SP800_108.DeriveKey));
		}

		// Token: 0x0400295A RID: 10586
		private static readonly Lazy<AspNetCryptoServiceProvider> _singleton = new Lazy<AspNetCryptoServiceProvider>(new Func<AspNetCryptoServiceProvider>(AspNetCryptoServiceProvider.GetSingletonCryptoServiceProvider));

		// Token: 0x0400295B RID: 10587
		private readonly ICryptoAlgorithmFactory _cryptoAlgorithmFactory;

		// Token: 0x0400295C RID: 10588
		private readonly IDataProtectorFactory _dataProtectorFactory;

		// Token: 0x0400295D RID: 10589
		private readonly bool _isDataProtectorEnabled;

		// Token: 0x0400295E RID: 10590
		private KeyDerivationFunction _keyDerivationFunction;

		// Token: 0x0400295F RID: 10591
		private readonly MachineKeySection _machineKeySection;

		// Token: 0x04002960 RID: 10592
		private readonly IMasterKeyProvider _masterKeyProvider;
	}
}
