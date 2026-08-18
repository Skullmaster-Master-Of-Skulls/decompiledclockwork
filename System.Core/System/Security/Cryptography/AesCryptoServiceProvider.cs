using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x020000DD RID: 221
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class AesCryptoServiceProvider : Aes
	{
		// Token: 0x060006CA RID: 1738 RVA: 0x000160B4 File Offset: 0x000142B4
		[SecurityCritical]
		public AesCryptoServiceProvider()
		{
			string providerName = "Microsoft Enhanced RSA and AES Cryptographic Provider";
			if (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor == 1)
			{
				providerName = "Microsoft Enhanced RSA and AES Cryptographic Provider (Prototype)";
			}
			this.m_cspHandle = CapiNative.AcquireCsp(null, providerName, CapiNative.ProviderType.RsaAes, CapiNative.CryptAcquireContextFlags.VerifyContext, true);
			this.FeedbackSizeValue = 8;
			int keySizeValue = 0;
			KeySizes[] array = AesCryptoServiceProvider.FindSupportedKeySizes(this.m_cspHandle, out keySizeValue);
			if (array.Length != 0)
			{
				this.KeySizeValue = keySizeValue;
				return;
			}
			throw new PlatformNotSupportedException(SR.GetString("Cryptography_PlatformNotSupported"));
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x00016140 File Offset: 0x00014340
		// (set) Token: 0x060006CC RID: 1740 RVA: 0x00016184 File Offset: 0x00014384
		public override byte[] Key
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_key == null || this.m_key.IsInvalid || this.m_key.IsClosed)
				{
					this.GenerateKey();
				}
				return CapiNative.ExportSymmetricKey(this.m_key);
			}
			[SecuritySafeCritical]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				byte[] array = (byte[])value.Clone();
				if (!base.ValidKeySize(array.Length * 8))
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidKeySize"));
				}
				SafeCapiKeyHandle key = CapiNative.ImportSymmetricKey(this.m_cspHandle, AesCryptoServiceProvider.GetAlgorithmId(array.Length * 8), array);
				if (this.m_key != null)
				{
					this.m_key.Dispose();
				}
				this.m_key = key;
				this.KeySizeValue = array.Length * 8;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x00016204 File Offset: 0x00014404
		// (set) Token: 0x060006CE RID: 1742 RVA: 0x0001620C File Offset: 0x0001440C
		public override int KeySize
		{
			get
			{
				return base.KeySize;
			}
			[SecuritySafeCritical]
			set
			{
				base.KeySize = value;
				if (this.m_key != null)
				{
					this.m_key.Dispose();
				}
			}
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00016228 File Offset: 0x00014428
		[SecuritySafeCritical]
		public override ICryptoTransform CreateDecryptor()
		{
			if (this.m_key == null || this.m_key.IsInvalid || this.m_key.IsClosed)
			{
				throw new CryptographicException(SR.GetString("Cryptography_DecryptWithNoKey"));
			}
			return this.CreateDecryptor(this.m_key, this.IVValue);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001627C File Offset: 0x0001447C
		[SecuritySafeCritical]
		public override ICryptoTransform CreateDecryptor(byte[] key, byte[] iv)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!base.ValidKeySize(key.Length * 8))
			{
				throw new ArgumentException(SR.GetString("Cryptography_InvalidKeySize"), "key");
			}
			if (iv != null && iv.Length * 8 != this.BlockSizeValue)
			{
				throw new ArgumentException(SR.GetString("Cryptography_InvalidIVSize"), "iv");
			}
			byte[] array = (byte[])key.Clone();
			byte[] iv2 = null;
			if (iv != null)
			{
				iv2 = (byte[])iv.Clone();
			}
			ICryptoTransform result;
			using (SafeCapiKeyHandle safeCapiKeyHandle = CapiNative.ImportSymmetricKey(this.m_cspHandle, AesCryptoServiceProvider.GetAlgorithmId(array.Length * 8), array))
			{
				result = this.CreateDecryptor(safeCapiKeyHandle, iv2);
			}
			return result;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00016338 File Offset: 0x00014538
		[SecurityCritical]
		private ICryptoTransform CreateDecryptor(SafeCapiKeyHandle key, byte[] iv)
		{
			return new CapiSymmetricAlgorithm(this.BlockSizeValue, this.FeedbackSizeValue, this.m_cspHandle, key, iv, this.Mode, this.PaddingValue, EncryptionMode.Decrypt);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00016360 File Offset: 0x00014560
		[SecuritySafeCritical]
		public override ICryptoTransform CreateEncryptor()
		{
			if (this.m_key == null || this.m_key.IsInvalid || this.m_key.IsClosed)
			{
				this.GenerateKey();
			}
			if (this.Mode != CipherMode.ECB && this.IVValue == null)
			{
				this.GenerateIV();
			}
			return this.CreateEncryptor(this.m_key, this.IVValue);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x000163C0 File Offset: 0x000145C0
		[SecuritySafeCritical]
		public override ICryptoTransform CreateEncryptor(byte[] key, byte[] iv)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!base.ValidKeySize(key.Length * 8))
			{
				throw new ArgumentException(SR.GetString("Cryptography_InvalidKeySize"), "key");
			}
			if (iv != null && iv.Length * 8 != this.BlockSizeValue)
			{
				throw new ArgumentException(SR.GetString("Cryptography_InvalidIVSize"), "iv");
			}
			byte[] array = (byte[])key.Clone();
			byte[] iv2 = null;
			if (iv != null)
			{
				iv2 = (byte[])iv.Clone();
			}
			ICryptoTransform result;
			using (SafeCapiKeyHandle safeCapiKeyHandle = CapiNative.ImportSymmetricKey(this.m_cspHandle, AesCryptoServiceProvider.GetAlgorithmId(array.Length * 8), array))
			{
				result = this.CreateEncryptor(safeCapiKeyHandle, iv2);
			}
			return result;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001647C File Offset: 0x0001467C
		[SecurityCritical]
		private ICryptoTransform CreateEncryptor(SafeCapiKeyHandle key, byte[] iv)
		{
			return new CapiSymmetricAlgorithm(this.BlockSizeValue, this.FeedbackSizeValue, this.m_cspHandle, key, iv, this.Mode, this.PaddingValue, EncryptionMode.Encrypt);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000164A4 File Offset: 0x000146A4
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this.m_key != null)
					{
						this.m_key.Dispose();
					}
					if (this.m_cspHandle != null)
					{
						this.m_cspHandle.Dispose();
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000164F4 File Offset: 0x000146F4
		[SecurityCritical]
		private static KeySizes[] FindSupportedKeySizes(SafeCspHandle csp, out int defaultKeySize)
		{
			if (AesCryptoServiceProvider.s_supportedKeySizes == null)
			{
				List<KeySizes> list = new List<KeySizes>();
				int num = 0;
				CapiNative.PROV_ENUMALGS providerParameterStruct = CapiNative.GetProviderParameterStruct<CapiNative.PROV_ENUMALGS>(csp, CapiNative.ProviderParameter.EnumerateAlgorithms, CapiNative.ProviderParameterFlags.RestartEnumeration);
				while (providerParameterStruct.aiAlgId != CapiNative.AlgorithmId.None)
				{
					switch (providerParameterStruct.aiAlgId)
					{
					case CapiNative.AlgorithmId.Aes128:
						list.Add(new KeySizes(128, 128, 0));
						if (128 > num)
						{
							num = 128;
						}
						break;
					case CapiNative.AlgorithmId.Aes192:
						list.Add(new KeySizes(192, 192, 0));
						if (192 > num)
						{
							num = 192;
						}
						break;
					case CapiNative.AlgorithmId.Aes256:
						list.Add(new KeySizes(256, 256, 0));
						if (256 > num)
						{
							num = 256;
						}
						break;
					}
					providerParameterStruct = CapiNative.GetProviderParameterStruct<CapiNative.PROV_ENUMALGS>(csp, CapiNative.ProviderParameter.EnumerateAlgorithms, CapiNative.ProviderParameterFlags.None);
				}
				AesCryptoServiceProvider.s_supportedKeySizes = list.ToArray();
				AesCryptoServiceProvider.s_defaultKeySize = num;
			}
			defaultKeySize = AesCryptoServiceProvider.s_defaultKeySize;
			return AesCryptoServiceProvider.s_supportedKeySizes;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x000165F0 File Offset: 0x000147F0
		[SecuritySafeCritical]
		public override void GenerateKey()
		{
			SafeCapiKeyHandle safeCapiKeyHandle = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (!CapiNative.UnsafeNativeMethods.CryptGenKey(this.m_cspHandle, AesCryptoServiceProvider.GetAlgorithmId(this.KeySizeValue), CapiNative.KeyFlags.Exportable, out safeCapiKeyHandle))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				if (safeCapiKeyHandle != null && !safeCapiKeyHandle.IsInvalid)
				{
					safeCapiKeyHandle.SetParentCsp(this.m_cspHandle);
				}
			}
			if (this.m_key != null)
			{
				this.m_key.Dispose();
			}
			this.m_key = safeCapiKeyHandle;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00016670 File Offset: 0x00014870
		[SecuritySafeCritical]
		public override void GenerateIV()
		{
			byte[] array = new byte[this.BlockSizeValue / 8];
			if (!CapiNative.UnsafeNativeMethods.CryptGenRandom(this.m_cspHandle, array.Length, array))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			this.IVValue = array;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x000166AE File Offset: 0x000148AE
		private static CapiNative.AlgorithmId GetAlgorithmId(int keySize)
		{
			if (keySize == 128)
			{
				return CapiNative.AlgorithmId.Aes128;
			}
			if (keySize == 192)
			{
				return CapiNative.AlgorithmId.Aes192;
			}
			if (keySize != 256)
			{
				return CapiNative.AlgorithmId.None;
			}
			return CapiNative.AlgorithmId.Aes256;
		}

		// Token: 0x040005D3 RID: 1491
		private static volatile KeySizes[] s_supportedKeySizes;

		// Token: 0x040005D4 RID: 1492
		private static volatile int s_defaultKeySize;

		// Token: 0x040005D5 RID: 1493
		[SecurityCritical]
		private SafeCspHandle m_cspHandle;

		// Token: 0x040005D6 RID: 1494
		[SecurityCritical]
		private SafeCapiKeyHandle m_key;
	}
}
