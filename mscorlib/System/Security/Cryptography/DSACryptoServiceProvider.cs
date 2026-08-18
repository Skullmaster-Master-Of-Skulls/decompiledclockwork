using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x0200087F RID: 2175
	[ComVisible(true)]
	public sealed class DSACryptoServiceProvider : DSA, ICspAsymmetricAlgorithm
	{
		// Token: 0x06004F37 RID: 20279 RVA: 0x00113A4F File Offset: 0x00112A4F
		public DSACryptoServiceProvider() : this(0, new CspParameters(13, null, null, DSACryptoServiceProvider.s_UseMachineKeyStore))
		{
		}

		// Token: 0x06004F38 RID: 20280 RVA: 0x00113A66 File Offset: 0x00112A66
		public DSACryptoServiceProvider(int dwKeySize) : this(dwKeySize, new CspParameters(13, null, null, DSACryptoServiceProvider.s_UseMachineKeyStore))
		{
		}

		// Token: 0x06004F39 RID: 20281 RVA: 0x00113A7D File Offset: 0x00112A7D
		public DSACryptoServiceProvider(CspParameters parameters) : this(0, parameters)
		{
		}

		// Token: 0x06004F3A RID: 20282 RVA: 0x00113A88 File Offset: 0x00112A88
		public DSACryptoServiceProvider(int dwKeySize, CspParameters parameters)
		{
			if (dwKeySize < 0)
			{
				throw new ArgumentOutOfRangeException("dwKeySize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this._parameters = Utils.SaveCspParameters(CspAlgorithmType.Dss, parameters, DSACryptoServiceProvider.s_UseMachineKeyStore, ref this._randomKeyContainer);
			this.LegalKeySizesValue = new KeySizes[]
			{
				new KeySizes(512, 1024, 64)
			};
			this._dwKeySize = dwKeySize;
			this._sha1 = new SHA1CryptoServiceProvider();
			if (!this._randomKeyContainer || Environment.GetCompatibilityFlag(CompatibilityFlag.EagerlyGenerateRandomAsymmKeys))
			{
				this.GetKeyPair();
			}
		}

		// Token: 0x06004F3B RID: 20283 RVA: 0x00113B18 File Offset: 0x00112B18
		private void GetKeyPair()
		{
			if (this._safeKeyHandle == null)
			{
				lock (this)
				{
					if (this._safeKeyHandle == null)
					{
						Utils.GetKeyPairHelper(CspAlgorithmType.Dss, this._parameters, this._randomKeyContainer, this._dwKeySize, ref this._safeProvHandle, ref this._safeKeyHandle);
					}
				}
			}
		}

		// Token: 0x06004F3C RID: 20284 RVA: 0x00113B7C File Offset: 0x00112B7C
		protected override void Dispose(bool disposing)
		{
			if (this._safeKeyHandle != null && !this._safeKeyHandle.IsClosed)
			{
				this._safeKeyHandle.Dispose();
			}
			if (this._safeProvHandle != null && !this._safeProvHandle.IsClosed)
			{
				this._safeProvHandle.Dispose();
			}
		}

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x06004F3D RID: 20285 RVA: 0x00113BCC File Offset: 0x00112BCC
		[ComVisible(false)]
		public bool PublicOnly
		{
			get
			{
				this.GetKeyPair();
				byte[] array = Utils._GetKeyParameter(this._safeKeyHandle, 2U);
				return array[0] == 1;
			}
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06004F3E RID: 20286 RVA: 0x00113BF2 File Offset: 0x00112BF2
		[ComVisible(false)]
		public CspKeyContainerInfo CspKeyContainerInfo
		{
			get
			{
				this.GetKeyPair();
				return new CspKeyContainerInfo(this._parameters, this._randomKeyContainer);
			}
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x06004F3F RID: 20287 RVA: 0x00113C0C File Offset: 0x00112C0C
		public override int KeySize
		{
			get
			{
				this.GetKeyPair();
				byte[] array = Utils._GetKeyParameter(this._safeKeyHandle, 1U);
				this._dwKeySize = ((int)array[0] | (int)array[1] << 8 | (int)array[2] << 16 | (int)array[3] << 24);
				return this._dwKeySize;
			}
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x06004F40 RID: 20288 RVA: 0x00113C4F File Offset: 0x00112C4F
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x06004F41 RID: 20289 RVA: 0x00113C52 File Offset: 0x00112C52
		public override string SignatureAlgorithm
		{
			get
			{
				return "http://www.w3.org/2000/09/xmldsig#dsa-sha1";
			}
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x06004F42 RID: 20290 RVA: 0x00113C59 File Offset: 0x00112C59
		// (set) Token: 0x06004F43 RID: 20291 RVA: 0x00113C63 File Offset: 0x00112C63
		public static bool UseMachineKeyStore
		{
			get
			{
				return DSACryptoServiceProvider.s_UseMachineKeyStore == CspProviderFlags.UseMachineKeyStore;
			}
			set
			{
				DSACryptoServiceProvider.s_UseMachineKeyStore = (value ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags);
			}
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x06004F44 RID: 20292 RVA: 0x00113C74 File Offset: 0x00112C74
		// (set) Token: 0x06004F45 RID: 20293 RVA: 0x00113CD4 File Offset: 0x00112CD4
		public bool PersistKeyInCsp
		{
			get
			{
				if (this._safeProvHandle == null)
				{
					lock (this)
					{
						if (this._safeProvHandle == null)
						{
							this._safeProvHandle = Utils.CreateProvHandle(this._parameters, this._randomKeyContainer);
						}
					}
				}
				return Utils._GetPersistKeyInCsp(this._safeProvHandle);
			}
			set
			{
				bool persistKeyInCsp = this.PersistKeyInCsp;
				if (value == persistKeyInCsp)
				{
					return;
				}
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
				if (!value)
				{
					KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Delete);
					keyContainerPermission.AccessEntries.Add(accessEntry);
				}
				else
				{
					KeyContainerPermissionAccessEntry accessEntry2 = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Create);
					keyContainerPermission.AccessEntries.Add(accessEntry2);
				}
				keyContainerPermission.Demand();
				Utils._SetPersistKeyInCsp(this._safeProvHandle, value);
			}
		}

		// Token: 0x06004F46 RID: 20294 RVA: 0x00113D40 File Offset: 0x00112D40
		public override DSAParameters ExportParameters(bool includePrivateParameters)
		{
			this.GetKeyPair();
			if (includePrivateParameters)
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
				KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Export);
				keyContainerPermission.AccessEntries.Add(accessEntry);
				keyContainerPermission.Demand();
			}
			DSACspObject dsacspObject = new DSACspObject();
			int blobType = includePrivateParameters ? 7 : 6;
			Utils._ExportKey(this._safeKeyHandle, blobType, dsacspObject);
			return DSACryptoServiceProvider.DSAObjectToStruct(dsacspObject);
		}

		// Token: 0x06004F47 RID: 20295 RVA: 0x00113D9F File Offset: 0x00112D9F
		[ComVisible(false)]
		public byte[] ExportCspBlob(bool includePrivateParameters)
		{
			this.GetKeyPair();
			return Utils.ExportCspBlobHelper(includePrivateParameters, this._parameters, this._safeKeyHandle);
		}

		// Token: 0x06004F48 RID: 20296 RVA: 0x00113DBC File Offset: 0x00112DBC
		public override void ImportParameters(DSAParameters parameters)
		{
			DSACspObject cspObject = DSACryptoServiceProvider.DSAStructToObject(parameters);
			if (this._safeKeyHandle != null && !this._safeKeyHandle.IsClosed)
			{
				this._safeKeyHandle.Dispose();
			}
			this._safeKeyHandle = SafeKeyHandle.InvalidHandle;
			if (DSACryptoServiceProvider.IsPublic(parameters))
			{
				Utils._ImportKey(Utils.StaticDssProvHandle, 8704, CspProviderFlags.NoFlags, cspObject, ref this._safeKeyHandle);
				return;
			}
			KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
			KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Import);
			keyContainerPermission.AccessEntries.Add(accessEntry);
			keyContainerPermission.Demand();
			if (this._safeProvHandle == null)
			{
				this._safeProvHandle = Utils.CreateProvHandle(this._parameters, this._randomKeyContainer);
			}
			Utils._ImportKey(this._safeProvHandle, 8704, this._parameters.Flags, cspObject, ref this._safeKeyHandle);
		}

		// Token: 0x06004F49 RID: 20297 RVA: 0x00113E84 File Offset: 0x00112E84
		[ComVisible(false)]
		public void ImportCspBlob(byte[] keyBlob)
		{
			Utils.ImportCspBlobHelper(CspAlgorithmType.Dss, keyBlob, DSACryptoServiceProvider.IsPublic(keyBlob), ref this._parameters, this._randomKeyContainer, ref this._safeProvHandle, ref this._safeKeyHandle);
		}

		// Token: 0x06004F4A RID: 20298 RVA: 0x00113EAC File Offset: 0x00112EAC
		public byte[] SignData(Stream inputStream)
		{
			byte[] rgbHash = this._sha1.ComputeHash(inputStream);
			return this.SignHash(rgbHash, null);
		}

		// Token: 0x06004F4B RID: 20299 RVA: 0x00113ED0 File Offset: 0x00112ED0
		public byte[] SignData(byte[] buffer)
		{
			byte[] rgbHash = this._sha1.ComputeHash(buffer);
			return this.SignHash(rgbHash, null);
		}

		// Token: 0x06004F4C RID: 20300 RVA: 0x00113EF4 File Offset: 0x00112EF4
		public byte[] SignData(byte[] buffer, int offset, int count)
		{
			byte[] rgbHash = this._sha1.ComputeHash(buffer, offset, count);
			return this.SignHash(rgbHash, null);
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x00113F18 File Offset: 0x00112F18
		public bool VerifyData(byte[] rgbData, byte[] rgbSignature)
		{
			byte[] rgbHash = this._sha1.ComputeHash(rgbData);
			return this.VerifyHash(rgbHash, null, rgbSignature);
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x00113F3B File Offset: 0x00112F3B
		public override byte[] CreateSignature(byte[] rgbHash)
		{
			return this.SignHash(rgbHash, null);
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x00113F45 File Offset: 0x00112F45
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			return this.VerifyHash(rgbHash, null, rgbSignature);
		}

		// Token: 0x06004F50 RID: 20304 RVA: 0x00113F50 File Offset: 0x00112F50
		public byte[] SignHash(byte[] rgbHash, string str)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (this.PublicOnly)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NoPrivateKey"));
			}
			int calgHash = X509Utils.OidToAlgId(str);
			if (rgbHash.Length != this._sha1.HashSize / 8)
			{
				throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Cryptography_InvalidHashSize"), new object[]
				{
					"SHA1",
					this._sha1.HashSize / 8
				}));
			}
			this.GetKeyPair();
			if (!this.CspKeyContainerInfo.RandomlyGenerated)
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
				KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Sign);
				keyContainerPermission.AccessEntries.Add(accessEntry);
				keyContainerPermission.Demand();
			}
			return Utils._SignValue(this._safeKeyHandle, this._parameters.KeyNumber, 8704, calgHash, rgbHash, 0);
		}

		// Token: 0x06004F51 RID: 20305 RVA: 0x00114038 File Offset: 0x00113038
		public bool VerifyHash(byte[] rgbHash, string str, byte[] rgbSignature)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (rgbSignature == null)
			{
				throw new ArgumentNullException("rgbSignature");
			}
			int calgHash = X509Utils.OidToAlgId(str);
			if (rgbHash.Length != this._sha1.HashSize / 8)
			{
				throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Cryptography_InvalidHashSize"), new object[]
				{
					"SHA1",
					this._sha1.HashSize / 8
				}));
			}
			this.GetKeyPair();
			return Utils._VerifySign(this._safeKeyHandle, 8704, calgHash, rgbHash, rgbSignature, 0);
		}

		// Token: 0x06004F52 RID: 20306 RVA: 0x001140D4 File Offset: 0x001130D4
		private static DSAParameters DSAObjectToStruct(DSACspObject dsaCspObject)
		{
			return new DSAParameters
			{
				P = dsaCspObject.P,
				Q = dsaCspObject.Q,
				G = dsaCspObject.G,
				Y = dsaCspObject.Y,
				J = dsaCspObject.J,
				X = dsaCspObject.X,
				Seed = dsaCspObject.Seed,
				Counter = dsaCspObject.Counter
			};
		}

		// Token: 0x06004F53 RID: 20307 RVA: 0x00114154 File Offset: 0x00113154
		private static DSACspObject DSAStructToObject(DSAParameters dsaParams)
		{
			return new DSACspObject
			{
				P = dsaParams.P,
				Q = dsaParams.Q,
				G = dsaParams.G,
				Y = dsaParams.Y,
				J = dsaParams.J,
				X = dsaParams.X,
				Seed = dsaParams.Seed,
				Counter = dsaParams.Counter
			};
		}

		// Token: 0x06004F54 RID: 20308 RVA: 0x001141D0 File Offset: 0x001131D0
		private static bool IsPublic(DSAParameters dsaParams)
		{
			return dsaParams.X == null;
		}

		// Token: 0x06004F55 RID: 20309 RVA: 0x001141DC File Offset: 0x001131DC
		private static bool IsPublic(byte[] keyBlob)
		{
			if (keyBlob == null)
			{
				throw new ArgumentNullException("keyBlob");
			}
			return keyBlob[0] == 6 && (keyBlob[11] == 49 || keyBlob[11] == 51) && keyBlob[10] == 83 && keyBlob[9] == 83 && keyBlob[8] == 68;
		}

		// Token: 0x040028F0 RID: 10480
		private int _dwKeySize;

		// Token: 0x040028F1 RID: 10481
		private CspParameters _parameters;

		// Token: 0x040028F2 RID: 10482
		private bool _randomKeyContainer;

		// Token: 0x040028F3 RID: 10483
		private SafeProvHandle _safeProvHandle;

		// Token: 0x040028F4 RID: 10484
		private SafeKeyHandle _safeKeyHandle;

		// Token: 0x040028F5 RID: 10485
		private SHA1CryptoServiceProvider _sha1;

		// Token: 0x040028F6 RID: 10486
		private static CspProviderFlags s_UseMachineKeyStore;
	}
}
