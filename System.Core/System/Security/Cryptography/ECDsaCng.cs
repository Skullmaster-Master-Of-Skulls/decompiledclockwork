using System;
using System.IO;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x020000FF RID: 255
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ECDsaCng : ECDsa
	{
		// Token: 0x06000862 RID: 2146 RVA: 0x0001C51D File Offset: 0x0001A71D
		public ECDsaCng() : this(521)
		{
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001C52A File Offset: 0x0001A72A
		public ECDsaCng(int keySize)
		{
			this.m_hashAlgorithm = CngAlgorithm.Sha256;
			base..ctor();
			if (!NCryptNative.NCryptSupported)
			{
				throw new PlatformNotSupportedException(SR.GetString("Cryptography_PlatformNotSupported"));
			}
			this.LegalKeySizesValue = ECDsaCng.s_legalKeySizes;
			this.KeySize = keySize;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0001C566 File Offset: 0x0001A766
		public ECDsaCng(ECCurve curve)
		{
			this.m_hashAlgorithm = CngAlgorithm.Sha256;
			base..ctor();
			this.GenerateKey(curve);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0001C580 File Offset: 0x0001A780
		[SecuritySafeCritical]
		public ECDsaCng(CngKey key)
		{
			this.m_hashAlgorithm = CngAlgorithm.Sha256;
			base..ctor();
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!ECDsaCng.IsEccAlgorithmGroup(key.AlgorithmGroup))
			{
				throw new ArgumentException(SR.GetString("Cryptography_ArgECDsaRequiresECDsaKey"), "key");
			}
			if (!NCryptNative.NCryptSupported)
			{
				throw new PlatformNotSupportedException(SR.GetString("Cryptography_PlatformNotSupported"));
			}
			this.LegalKeySizesValue = ECDsaCng.s_legalKeySizes;
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
			using (SafeNCryptKeyHandle handle = key.Handle)
			{
				this.Key = CngKey.Open(handle, key.IsEphemeral ? CngKeyHandleOpenOptions.EphemeralKey : CngKeyHandleOpenOptions.None);
			}
			CodeAccessPermission.RevertAssert();
			this.KeySizeValue = this.m_key.KeySize;
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x0001C64C File Offset: 0x0001A84C
		// (set) Token: 0x06000867 RID: 2151 RVA: 0x0001C654 File Offset: 0x0001A854
		public CngAlgorithm HashAlgorithm
		{
			get
			{
				return this.m_hashAlgorithm;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_hashAlgorithm = value;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x0001C674 File Offset: 0x0001A874
		// (set) Token: 0x06000869 RID: 2153 RVA: 0x0001C718 File Offset: 0x0001A918
		public CngKey Key
		{
			get
			{
				if (this.m_key != null && this.m_key.KeySize != this.KeySize)
				{
					this.m_key.Dispose();
					this.m_key = null;
				}
				if (this.m_key == null)
				{
					CngAlgorithm algorithm = null;
					int keySize = this.KeySize;
					if (keySize != 256)
					{
						if (keySize != 384)
						{
							if (keySize == 521)
							{
								algorithm = CngAlgorithm.ECDsaP521;
							}
						}
						else
						{
							algorithm = CngAlgorithm.ECDsaP384;
						}
					}
					else
					{
						algorithm = CngAlgorithm.ECDsaP256;
					}
					CngKeyCreationParameters creationParameters = new CngKeyCreationParameters
					{
						ExportPolicy = new CngExportPolicies?(CngExportPolicies.AllowPlaintextExport)
					};
					this.m_key = CngKey.Create(algorithm, null, creationParameters);
				}
				return this.m_key;
			}
			private set
			{
				if (!ECDsaCng.IsEccAlgorithmGroup(value.AlgorithmGroup))
				{
					throw new ArgumentException(SR.GetString("Cryptography_ArgECDsaRequiresECDsaKey"));
				}
				if (this.m_key != null)
				{
					this.m_key.Dispose();
				}
				this.m_key = value;
				this.KeySizeValue = this.m_key.KeySize;
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001C770 File Offset: 0x0001A970
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this.m_key != null)
				{
					this.m_key.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001C7AC File Offset: 0x0001A9AC
		public override void FromXmlString(string xmlString)
		{
			throw new NotImplementedException(SR.GetString("Cryptography_ECXmlSerializationFormatRequired"));
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001C7C0 File Offset: 0x0001A9C0
		public void FromXmlString(string xml, ECKeyXmlFormat format)
		{
			if (xml == null)
			{
				throw new ArgumentNullException("xml");
			}
			if (format != ECKeyXmlFormat.Rfc4050)
			{
				throw new ArgumentOutOfRangeException("format");
			}
			bool flag;
			ECParameters parameters = Rfc4050KeyFormatter.FromXml(xml, out flag);
			this.ImportParameters(parameters);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0001C7F9 File Offset: 0x0001A9F9
		public byte[] SignData(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return this.SignData(data, 0, data.Length);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0001C814 File Offset: 0x0001AA14
		[SecuritySafeCritical]
		public byte[] SignData(byte[] data, int offset, int count)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (offset < 0 || offset > data.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > data.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			byte[] result;
			using (BCryptHashAlgorithm bcryptHashAlgorithm = new BCryptHashAlgorithm(this.HashAlgorithm, "Microsoft Primitive Provider"))
			{
				bcryptHashAlgorithm.HashCore(data, offset, count);
				byte[] hash = bcryptHashAlgorithm.HashFinal();
				result = this.SignHash(hash);
			}
			return result;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0001C8A4 File Offset: 0x0001AAA4
		[SecuritySafeCritical]
		public byte[] SignData(Stream data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			byte[] result;
			using (BCryptHashAlgorithm bcryptHashAlgorithm = new BCryptHashAlgorithm(this.HashAlgorithm, "Microsoft Primitive Provider"))
			{
				bcryptHashAlgorithm.HashStream(data);
				byte[] hash = bcryptHashAlgorithm.HashFinal();
				result = this.SignHash(hash);
			}
			return result;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001C904 File Offset: 0x0001AB04
		[SecuritySafeCritical]
		public override byte[] SignHash(byte[] hash)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
			KeyContainerPermission keyContainerPermission = this.Key.BuildKeyContainerPermission(KeyContainerPermissionFlags.Sign);
			if (keyContainerPermission != null)
			{
				keyContainerPermission.Demand();
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
			byte[] result;
			using (SafeNCryptKeyHandle handle = this.Key.Handle)
			{
				CodeAccessPermission.RevertAssert();
				result = NCryptNative.SignHash(handle, hash);
			}
			return result;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001C97C File Offset: 0x0001AB7C
		public override string ToXmlString(bool includePrivateParameters)
		{
			throw new NotImplementedException(SR.GetString("Cryptography_ECXmlSerializationFormatRequired"));
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0001C990 File Offset: 0x0001AB90
		public string ToXmlString(ECKeyXmlFormat format)
		{
			if (format != ECKeyXmlFormat.Rfc4050)
			{
				throw new ArgumentOutOfRangeException("format");
			}
			ECParameters parameters = this.ExportParameters(false);
			return Rfc4050KeyFormatter.ToXml(parameters, false);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0001C9BA File Offset: 0x0001ABBA
		public bool VerifyData(byte[] data, byte[] signature)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return this.VerifyData(data, 0, data.Length, signature);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001C9D8 File Offset: 0x0001ABD8
		[SecuritySafeCritical]
		public bool VerifyData(byte[] data, int offset, int count, byte[] signature)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (offset < 0 || offset > data.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > data.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			bool result;
			using (BCryptHashAlgorithm bcryptHashAlgorithm = new BCryptHashAlgorithm(this.HashAlgorithm, "Microsoft Primitive Provider"))
			{
				bcryptHashAlgorithm.HashCore(data, offset, count);
				byte[] hash = bcryptHashAlgorithm.HashFinal();
				result = this.VerifyHash(hash, signature);
			}
			return result;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0001CA78 File Offset: 0x0001AC78
		[SecuritySafeCritical]
		public bool VerifyData(Stream data, byte[] signature)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			bool result;
			using (BCryptHashAlgorithm bcryptHashAlgorithm = new BCryptHashAlgorithm(this.HashAlgorithm, "Microsoft Primitive Provider"))
			{
				bcryptHashAlgorithm.HashStream(data);
				byte[] hash = bcryptHashAlgorithm.HashFinal();
				result = this.VerifyHash(hash, signature);
			}
			return result;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0001CAE8 File Offset: 0x0001ACE8
		[SecuritySafeCritical]
		public override bool VerifyHash(byte[] hash, byte[] signature)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
			bool result;
			using (SafeNCryptKeyHandle handle = this.Key.Handle)
			{
				CodeAccessPermission.RevertAssert();
				result = NCryptNative.VerifySignature(handle, hash, signature);
			}
			return result;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0001CB54 File Offset: 0x0001AD54
		public override void GenerateKey(ECCurve curve)
		{
			curve.Validate();
			if (this.m_key != null)
			{
				this.m_key.Dispose();
				this.m_key = null;
			}
			CngKey cngKey = CngKey.Create(curve, (string name) => CngKey.EcdsaCurveNameToAlgorithm(name));
			this.m_key = cngKey;
			this.KeySizeValue = cngKey.KeySize;
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x0001CBBB File Offset: 0x0001ADBB
		private SafeNCryptKeyHandle KeyHandle
		{
			[SecuritySafeCritical]
			get
			{
				return this.Key.Handle;
			}
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0001CBC8 File Offset: 0x0001ADC8
		protected override byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
		{
			byte[] result;
			using (BCryptHashAlgorithm bcryptHashAlgorithm = new BCryptHashAlgorithm(new CngAlgorithm(hashAlgorithm.Name), "Microsoft Primitive Provider"))
			{
				bcryptHashAlgorithm.HashCore(data, offset, count);
				result = bcryptHashAlgorithm.HashFinal();
			}
			return result;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001CC1C File Offset: 0x0001AE1C
		protected override byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
		{
			byte[] result;
			using (BCryptHashAlgorithm bcryptHashAlgorithm = new BCryptHashAlgorithm(new CngAlgorithm(hashAlgorithm.Name), "Microsoft Primitive Provider"))
			{
				bcryptHashAlgorithm.HashStream(data);
				result = bcryptHashAlgorithm.HashFinal();
			}
			return result;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001CC6C File Offset: 0x0001AE6C
		private static bool IsEccAlgorithmGroup(CngAlgorithmGroup algorithmGroup)
		{
			return algorithmGroup == CngAlgorithmGroup.ECDsa || algorithmGroup == CngAlgorithmGroup.ECDiffieHellman;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001CC88 File Offset: 0x0001AE88
		public override void ImportParameters(ECParameters parameters)
		{
			this.Key = ECCng.ImportECDsaParameters(ref parameters);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001CC97 File Offset: 0x0001AE97
		public override ECParameters ExportExplicitParameters(bool includePrivateParameters)
		{
			return ECCng.ExportExplicitParameters(this.Key, includePrivateParameters);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001CCA5 File Offset: 0x0001AEA5
		public override ECParameters ExportParameters(bool includePrivateParameters)
		{
			return ECCng.ExportParameters(this.Key, includePrivateParameters);
		}

		// Token: 0x04000675 RID: 1653
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(256, 384, 128),
			new KeySizes(521, 521, 0)
		};

		// Token: 0x04000676 RID: 1654
		private CngKey m_key;

		// Token: 0x04000677 RID: 1655
		private CngAlgorithm m_hashAlgorithm;
	}
}
