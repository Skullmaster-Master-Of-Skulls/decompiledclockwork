using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A4C RID: 2636
	internal sealed class SelfSignedCertificate : IDisposable
	{
		// Token: 0x06006830 RID: 26672
		[DllImport("Crypt32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		private static extern CertificateHandle CertCreateSelfSignCertificate(KeyContainerHandle hProv, CryptoApiBlob.InteropHelper pSubjectIssuerBlob, SelfSignedCertificate.SelfSignFlags dwFlags, IntPtr pKeyProvInfo, IntPtr pSignatureAlgorithm, [In] ref SystemTime pStartTime, [In] ref SystemTime pEndTime, IntPtr pExtensions);

		// Token: 0x06006831 RID: 26673
		[DllImport("Crypt32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		private static extern CertificateStoreHandle CertOpenStore(IntPtr lpszStoreProvider, int dwMsgAndCertEncodingType, IntPtr hCryptProv, int dwFlags, IntPtr pvPara);

		// Token: 0x06006832 RID: 26674
		[DllImport("Crypt32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		private static extern bool CertAddCertificateContextToStore(CertificateStoreHandle hCertStore, CertificateHandle pCertContext, SelfSignedCertificate.AddDisposition dwAddDisposition, out StoreCertificateHandle ppStoreContext);

		// Token: 0x06006833 RID: 26675
		[DllImport("advapi32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		private static extern bool CryptAcquireContext(out KeyContainerHandle phProv, string pszContainer, string pszProvider, SelfSignedCertificate.ProviderType dwProvType, SelfSignedCertificate.ContextFlags dwFlags);

		// Token: 0x06006834 RID: 26676
		[DllImport("advapi32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		private static extern bool CryptGenKey(KeyContainerHandle hProv, SelfSignedCertificate.AlgorithmType algId, SelfSignedCertificate.KeyFlags dwFlags, out KeyHandle phKey);

		// Token: 0x06006835 RID: 26677
		[DllImport("Crypt32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool PFXExportCertStoreEx(CertificateStoreHandle hStore, IntPtr pPFX, string password, IntPtr pvReserved, SelfSignedCertificate.PfxExportFlags dwFlags);

		// Token: 0x06006836 RID: 26678
		[DllImport("Crypt32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		private static extern bool CertSetCertificateContextProperty(CertificateHandle context, int propId, int flags, KeyHandle pv);

		// Token: 0x06006837 RID: 26679 RVA: 0x00184A00 File Offset: 0x00182C00
		private CriticalAllocHandle GetProviderInfo()
		{
			return CriticalAllocHandleBlob.FromBlob<SelfSignedCertificate.CRYPT_KEY_PROV_INFO>(new SelfSignedCertificate.CRYPT_KEY_PROV_INFO
			{
				container = this.keyContainerName,
				providerType = 12,
				paramsCount = 0,
				keySpec = 1
			});
		}

		// Token: 0x06006838 RID: 26680 RVA: 0x00184A3C File Offset: 0x00182C3C
		private static CriticalAllocHandle GetSha1AlgorithmId()
		{
			SelfSignedCertificate.Sha1AlgorithmId id = new SelfSignedCertificate.Sha1AlgorithmId();
			return CriticalAllocHandleBlob.FromBlob<SelfSignedCertificate.CRYPT_ALGORITHM_IDENTIFIER>(id);
		}

		// Token: 0x06006839 RID: 26681 RVA: 0x00184A58 File Offset: 0x00182C58
		public static SelfSignedCertificate Create(string name, string password)
		{
			return SelfSignedCertificate.Create(name, password, DateTime.UtcNow, DateTime.UtcNow.AddYears(2), Guid.NewGuid().ToString());
		}

		// Token: 0x0600683A RID: 26682 RVA: 0x00184A94 File Offset: 0x00182C94
		public static SelfSignedCertificate Create(string name, string password, DateTime start, DateTime expire, string containerName)
		{
			SelfSignedCertificate selfSignedCertificate = new SelfSignedCertificate(password, containerName);
			selfSignedCertificate.GenerateKeys();
			selfSignedCertificate.CreateCertContext(name, start, expire);
			selfSignedCertificate.GetX509Certificate();
			return selfSignedCertificate;
		}

		// Token: 0x0600683B RID: 26683 RVA: 0x00184AC4 File Offset: 0x00182CC4
		private void CreateCertContext(string name, DateTime start, DateTime expire)
		{
			CriticalAllocHandle providerInfo = this.GetProviderInfo();
			CriticalAllocHandle sha1AlgorithmId = SelfSignedCertificate.GetSha1AlgorithmId();
			SystemTime systemTime = new SystemTime(start);
			SystemTime systemTime2 = new SystemTime(expire);
			CertificateName certificateName = new CertificateName(name);
			using (CryptoApiBlob cryptoApiBlob = certificateName.GetCryptoApiBlob())
			{
				using (providerInfo)
				{
					using (sha1AlgorithmId)
					{
						this.cert = SelfSignedCertificate.CertCreateSelfSignCertificate(this.keyContainer, cryptoApiBlob.GetMemoryForPinning(), SelfSignedCertificate.SelfSignFlags.None, providerInfo, sha1AlgorithmId, ref systemTime, ref systemTime2, IntPtr.Zero);
						if (this.cert.IsInvalid)
						{
							PeerExceptionHelper.ThrowInvalidOperation_PeerCertGenFailure(PeerExceptionHelper.GetLastException());
						}
						if (!SelfSignedCertificate.CertSetCertificateContextProperty(this.cert, 1, 0, this.key))
						{
							PeerExceptionHelper.ThrowInvalidOperation_PeerCertGenFailure(PeerExceptionHelper.GetLastException());
						}
					}
				}
			}
		}

		// Token: 0x0600683C RID: 26684 RVA: 0x00184BB8 File Offset: 0x00182DB8
		public X509Certificate2 GetX509Certificate()
		{
			if (this.x509Cert == null)
			{
				this.Export();
				this.x509Cert = new X509Certificate2(this.exportedBytes, this.password);
			}
			return this.x509Cert;
		}

		// Token: 0x0600683D RID: 26685 RVA: 0x00184BE8 File Offset: 0x00182DE8
		private void Export()
		{
			using (CertificateStoreHandle certificateStoreHandle = SelfSignedCertificate.CertOpenStore(new IntPtr(2), 0, IntPtr.Zero, 0, IntPtr.Zero))
			{
				StoreCertificateHandle storeCertificateHandle;
				if (!SelfSignedCertificate.CertAddCertificateContextToStore(certificateStoreHandle, this.cert, SelfSignedCertificate.AddDisposition.ReplaceExisting, out storeCertificateHandle))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					Utility.CloseInvalidOutSafeHandle(storeCertificateHandle);
					PeerExceptionHelper.ThrowInvalidOperation_PeerCertGenFailure(new Win32Exception(lastWin32Error));
				}
				using (storeCertificateHandle)
				{
					CryptoApiBlob cryptoApiBlob = new CryptoApiBlob();
					CryptoApiBlob.InteropHelper memoryForPinning = cryptoApiBlob.GetMemoryForPinning();
					GCHandle gchandle = GCHandle.Alloc(memoryForPinning, GCHandleType.Pinned);
					try
					{
						if (!SelfSignedCertificate.PFXExportCertStoreEx(certificateStoreHandle, gchandle.AddrOfPinnedObject(), this.password, IntPtr.Zero, SelfSignedCertificate.PfxExportFlags.ReportNoPrivateKey | SelfSignedCertificate.PfxExportFlags.ReportNotAbleToExportPrivateKey | SelfSignedCertificate.PfxExportFlags.ExportPrivateKeys))
						{
							PeerExceptionHelper.ThrowInvalidOperation_PeerCertGenFailure(PeerExceptionHelper.GetLastException());
						}
						int size = memoryForPinning.size;
						gchandle.Free();
						cryptoApiBlob.AllocateBlob(size);
						memoryForPinning = cryptoApiBlob.GetMemoryForPinning();
						gchandle = GCHandle.Alloc(memoryForPinning, GCHandleType.Pinned);
						if (!SelfSignedCertificate.PFXExportCertStoreEx(certificateStoreHandle, gchandle.AddrOfPinnedObject(), this.password, IntPtr.Zero, SelfSignedCertificate.PfxExportFlags.ReportNoPrivateKey | SelfSignedCertificate.PfxExportFlags.ReportNotAbleToExportPrivateKey | SelfSignedCertificate.PfxExportFlags.ExportPrivateKeys))
						{
							PeerExceptionHelper.ThrowInvalidOperation_PeerCertGenFailure(PeerExceptionHelper.GetLastException());
						}
						this.exportedBytes = cryptoApiBlob.GetBytes();
					}
					finally
					{
						gchandle.Free();
						if (cryptoApiBlob != null)
						{
							cryptoApiBlob.Dispose();
						}
					}
				}
			}
		}

		// Token: 0x0600683E RID: 26686 RVA: 0x00184D2C File Offset: 0x00182F2C
		private void GenerateKeys()
		{
			if (!SelfSignedCertificate.CryptAcquireContext(out this.keyContainer, this.keyContainerName, null, SelfSignedCertificate.ProviderType.RsaSecureChannel, SelfSignedCertificate.ContextFlags.NewKeySet | SelfSignedCertificate.ContextFlags.Silent))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				Utility.CloseInvalidOutSafeHandle(this.keyContainer);
				this.keyContainer = null;
				PeerExceptionHelper.ThrowInvalidOperation_PeerCertGenFailure(new Win32Exception(lastWin32Error));
			}
			if (!SelfSignedCertificate.CryptGenKey(this.keyContainer, SelfSignedCertificate.AlgorithmType.KeyExchange, SelfSignedCertificate.KeyFlags.Exportable2k, out this.key))
			{
				int lastWin32Error2 = Marshal.GetLastWin32Error();
				Utility.CloseInvalidOutSafeHandle(this.key);
				this.key = null;
				PeerExceptionHelper.ThrowInvalidOperation_PeerCertGenFailure(new Win32Exception(lastWin32Error2));
			}
		}

		// Token: 0x0600683F RID: 26687 RVA: 0x00184DB0 File Offset: 0x00182FB0
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.cert != null)
				{
					this.cert.Dispose();
				}
				if (this.key != null)
				{
					this.key.Dispose();
				}
				if (this.keyContainer != null)
				{
					this.keyContainer.Dispose();
				}
				if (this.keyContainerName != null)
				{
					SelfSignedCertificate.CryptAcquireContext(out this.keyContainer, this.keyContainerName, null, SelfSignedCertificate.ProviderType.RsaSecureChannel, SelfSignedCertificate.ContextFlags.DeleteKeySet);
					Utility.CloseInvalidOutSafeHandle(this.keyContainer);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06006840 RID: 26688 RVA: 0x00184E29 File Offset: 0x00183029
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06006841 RID: 26689 RVA: 0x00184E32 File Offset: 0x00183032
		private SelfSignedCertificate(string password, string containerName)
		{
			this.password = password;
			this.keyContainerName = containerName;
		}

		// Token: 0x04003BBA RID: 15290
		private const int CERT_KEY_SPEC_PROP_ID = 1;

		// Token: 0x04003BBB RID: 15291
		private const int CERT_KEY_PROV_INFO_PROP_ID = 2;

		// Token: 0x04003BBC RID: 15292
		private CertificateHandle cert;

		// Token: 0x04003BBD RID: 15293
		private KeyContainerHandle keyContainer;

		// Token: 0x04003BBE RID: 15294
		private KeyHandle key;

		// Token: 0x04003BBF RID: 15295
		private string keyContainerName;

		// Token: 0x04003BC0 RID: 15296
		private string password;

		// Token: 0x04003BC1 RID: 15297
		private byte[] exportedBytes;

		// Token: 0x04003BC2 RID: 15298
		private X509Certificate2 x509Cert;

		// Token: 0x04003BC3 RID: 15299
		private const int CERT_STORE_PROV_MEMORY = 2;

		// Token: 0x04003BC4 RID: 15300
		private const int DefaultLifeSpanInYears = 2;

		// Token: 0x02000E7E RID: 3710
		[Flags]
		private enum SelfSignFlags
		{
			// Token: 0x04004B38 RID: 19256
			None = 0,
			// Token: 0x04004B39 RID: 19257
			NoSign = 1,
			// Token: 0x04004B3A RID: 19258
			NoKeyInfo = 2
		}

		// Token: 0x02000E7F RID: 3711
		private enum AddDisposition
		{
			// Token: 0x04004B3C RID: 19260
			New = 1,
			// Token: 0x04004B3D RID: 19261
			UseExisting,
			// Token: 0x04004B3E RID: 19262
			ReplaceExisting,
			// Token: 0x04004B3F RID: 19263
			Always,
			// Token: 0x04004B40 RID: 19264
			ReplaceExistingInheritProperties
		}

		// Token: 0x02000E80 RID: 3712
		[Flags]
		private enum PfxExportFlags
		{
			// Token: 0x04004B42 RID: 19266
			ReportNoPrivateKey = 1,
			// Token: 0x04004B43 RID: 19267
			ReportNotAbleToExportPrivateKey = 2,
			// Token: 0x04004B44 RID: 19268
			ExportPrivateKeys = 4
		}

		// Token: 0x02000E81 RID: 3713
		private enum ProviderType
		{
			// Token: 0x04004B46 RID: 19270
			RsaFull = 1,
			// Token: 0x04004B47 RID: 19271
			RsaSignature,
			// Token: 0x04004B48 RID: 19272
			Dss,
			// Token: 0x04004B49 RID: 19273
			Fortezza,
			// Token: 0x04004B4A RID: 19274
			MsExchange,
			// Token: 0x04004B4B RID: 19275
			Ssl,
			// Token: 0x04004B4C RID: 19276
			RsaSecureChannel = 12,
			// Token: 0x04004B4D RID: 19277
			DssDiffieHellman,
			// Token: 0x04004B4E RID: 19278
			EcDsaSignature,
			// Token: 0x04004B4F RID: 19279
			EcNraSignature,
			// Token: 0x04004B50 RID: 19280
			EcDsaFull,
			// Token: 0x04004B51 RID: 19281
			EcNraFull,
			// Token: 0x04004B52 RID: 19282
			DiffieHellmanSecureChannel,
			// Token: 0x04004B53 RID: 19283
			SpyrusLynks = 20,
			// Token: 0x04004B54 RID: 19284
			RandomNumberGenerator,
			// Token: 0x04004B55 RID: 19285
			IntelSec,
			// Token: 0x04004B56 RID: 19286
			ReplaceOwf,
			// Token: 0x04004B57 RID: 19287
			RsaAes
		}

		// Token: 0x02000E82 RID: 3714
		[Flags]
		private enum ContextFlags : uint
		{
			// Token: 0x04004B59 RID: 19289
			VerifyContext = 4026531840U,
			// Token: 0x04004B5A RID: 19290
			NewKeySet = 8U,
			// Token: 0x04004B5B RID: 19291
			DeleteKeySet = 16U,
			// Token: 0x04004B5C RID: 19292
			MachineKeySet = 32U,
			// Token: 0x04004B5D RID: 19293
			Silent = 64U
		}

		// Token: 0x02000E83 RID: 3715
		private enum AlgorithmType
		{
			// Token: 0x04004B5F RID: 19295
			KeyExchange = 1,
			// Token: 0x04004B60 RID: 19296
			Signature
		}

		// Token: 0x02000E84 RID: 3716
		private enum KeyFlags
		{
			// Token: 0x04004B62 RID: 19298
			Exportable = 1,
			// Token: 0x04004B63 RID: 19299
			UserProtected,
			// Token: 0x04004B64 RID: 19300
			CreateSalt = 4,
			// Token: 0x04004B65 RID: 19301
			UpdateKey = 8,
			// Token: 0x04004B66 RID: 19302
			NoSalt = 16,
			// Token: 0x04004B67 RID: 19303
			PreGenerate = 64,
			// Token: 0x04004B68 RID: 19304
			Online = 128,
			// Token: 0x04004B69 RID: 19305
			Sf = 256,
			// Token: 0x04004B6A RID: 19306
			CreateIv = 512,
			// Token: 0x04004B6B RID: 19307
			KeyExchangeKey = 1024,
			// Token: 0x04004B6C RID: 19308
			DataKey = 2048,
			// Token: 0x04004B6D RID: 19309
			Volatile = 4096,
			// Token: 0x04004B6E RID: 19310
			SgcKey = 8192,
			// Token: 0x04004B6F RID: 19311
			Archivable = 16384,
			// Token: 0x04004B70 RID: 19312
			Exportable2k = 134217729
		}

		// Token: 0x02000E85 RID: 3717
		[Serializable]
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public class CRYPT_KEY_PROV_INFO
		{
			// Token: 0x04004B71 RID: 19313
			public string container;

			// Token: 0x04004B72 RID: 19314
			public string provName;

			// Token: 0x04004B73 RID: 19315
			public int providerType;

			// Token: 0x04004B74 RID: 19316
			public int flags;

			// Token: 0x04004B75 RID: 19317
			public int paramsCount;

			// Token: 0x04004B76 RID: 19318
			public IntPtr param;

			// Token: 0x04004B77 RID: 19319
			public int keySpec;
		}

		// Token: 0x02000E86 RID: 3718
		[Serializable]
		public struct CRYPT_OBJID_BLOB
		{
			// Token: 0x04004B78 RID: 19320
			public int count;

			// Token: 0x04004B79 RID: 19321
			public IntPtr parameters;
		}

		// Token: 0x02000E87 RID: 3719
		[Serializable]
		[StructLayout(LayoutKind.Sequential)]
		public class CRYPT_ALGORITHM_IDENTIFIER
		{
			// Token: 0x060083FB RID: 33787 RVA: 0x001E80EC File Offset: 0x001E62EC
			public CRYPT_ALGORITHM_IDENTIFIER(string id)
			{
				this.pszObjId = id;
			}

			// Token: 0x04004B7A RID: 19322
			public string pszObjId;

			// Token: 0x04004B7B RID: 19323
			public SelfSignedCertificate.CRYPT_OBJID_BLOB Parameters;
		}

		// Token: 0x02000E88 RID: 3720
		[Serializable]
		[StructLayout(LayoutKind.Sequential)]
		public class Sha1AlgorithmId : SelfSignedCertificate.CRYPT_ALGORITHM_IDENTIFIER
		{
			// Token: 0x060083FC RID: 33788 RVA: 0x001E80FB File Offset: 0x001E62FB
			public Sha1AlgorithmId() : base("1.2.840.113549.1.1.5")
			{
			}

			// Token: 0x04004B7C RID: 19324
			private const string AlgId = "1.2.840.113549.1.1.5";
		}
	}
}
