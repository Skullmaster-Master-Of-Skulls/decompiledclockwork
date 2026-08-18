using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000082 RID: 130
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class AlgorithmIdentifier
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x00017F8D File Offset: 0x0001618D
		public AlgorithmIdentifier()
		{
			this.Reset(Oid.FromOidValue("1.2.840.113549.3.7", OidGroup.EncryptionAlgorithm), 0, new byte[0]);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00017FAD File Offset: 0x000161AD
		public AlgorithmIdentifier(Oid oid)
		{
			this.Reset(oid, 0, new byte[0]);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00017FC3 File Offset: 0x000161C3
		public AlgorithmIdentifier(Oid oid, int keyLength)
		{
			this.Reset(oid, keyLength, new byte[0]);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00017FDC File Offset: 0x000161DC
		[SecurityCritical]
		internal AlgorithmIdentifier(CAPI.CERT_PUBLIC_KEY_INFO keyInfo)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPI.CERT_PUBLIC_KEY_INFO))));
			Marshal.StructureToPtr(keyInfo, safeLocalAllocHandle.DangerousGetHandle(), false);
			int keyLength = (int)CAPI.CAPISafe.CertGetPublicKeyLength(65537U, safeLocalAllocHandle.DangerousGetHandle());
			byte[] array = new byte[keyInfo.Algorithm.Parameters.cbData];
			if (array.Length != 0)
			{
				Marshal.Copy(keyInfo.Algorithm.Parameters.pbData, array, 0, array.Length);
			}
			Marshal.DestroyStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPI.CERT_PUBLIC_KEY_INFO));
			safeLocalAllocHandle.Dispose();
			this.Reset(Oid.FromOidValue(keyInfo.Algorithm.pszObjId, OidGroup.PublicKeyAlgorithm), keyLength, array);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00018098 File Offset: 0x00016298
		[SecurityCritical]
		internal AlgorithmIdentifier(CAPI.CRYPT_ALGORITHM_IDENTIFIER algorithmIdentifier)
		{
			int keyLength = 0;
			uint num = 0U;
			SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle;
			byte[] array = new byte[0];
			uint num2 = X509Utils.OidToAlgId(algorithmIdentifier.pszObjId);
			if (num2 == 26114U)
			{
				if (algorithmIdentifier.Parameters.cbData > 0U)
				{
					if (!CAPI.DecodeObject(new IntPtr(41L), algorithmIdentifier.Parameters.pbData, algorithmIdentifier.Parameters.cbData, out invalidHandle, out num))
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
					CAPI.CRYPT_RC2_CBC_PARAMETERS crypt_RC2_CBC_PARAMETERS = (CAPI.CRYPT_RC2_CBC_PARAMETERS)Marshal.PtrToStructure(invalidHandle.DangerousGetHandle(), typeof(CAPI.CRYPT_RC2_CBC_PARAMETERS));
					uint dwVersion = crypt_RC2_CBC_PARAMETERS.dwVersion;
					if (dwVersion != 52U)
					{
						if (dwVersion != 58U)
						{
							if (dwVersion == 160U)
							{
								keyLength = 40;
							}
						}
						else
						{
							keyLength = 128;
						}
					}
					else
					{
						keyLength = 56;
					}
					if (crypt_RC2_CBC_PARAMETERS.fIV)
					{
						array = (byte[])crypt_RC2_CBC_PARAMETERS.rgbIV.Clone();
					}
				}
			}
			else if (num2 == 26625U || num2 == 26113U || num2 == 26115U)
			{
				if (algorithmIdentifier.Parameters.cbData > 0U)
				{
					if (!CAPI.DecodeObject(new IntPtr(25L), algorithmIdentifier.Parameters.pbData, algorithmIdentifier.Parameters.cbData, out invalidHandle, out num))
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
					if ((ulong)num > (ulong)((long)Marshal.SizeOf(typeof(CAPI.CRYPTOAPI_BLOB))))
					{
						CAPI.CRYPTOAPI_BLOB cryptoapi_BLOB = (CAPI.CRYPTOAPI_BLOB)Marshal.PtrToStructure(invalidHandle.DangerousGetHandle(), typeof(CAPI.CRYPTOAPI_BLOB));
						if (num2 == 26625U)
						{
							if (cryptoapi_BLOB.cbData > 0U)
							{
								array = new byte[cryptoapi_BLOB.cbData];
								Marshal.Copy(cryptoapi_BLOB.pbData, array, 0, array.Length);
							}
						}
						else
						{
							array = new byte[num];
							Marshal.Copy(invalidHandle.DangerousGetHandle(), array, 0, array.Length);
							Array.Clear(array, 4, (int)((long)array.Length - (long)((ulong)cryptoapi_BLOB.cbData) - 4L));
						}
					}
				}
				if (num2 == 26625U)
				{
					keyLength = 128 - array.Length * 8;
				}
				else if (num2 == 26113U)
				{
					keyLength = 64;
				}
				else
				{
					keyLength = 192;
				}
			}
			else if (algorithmIdentifier.Parameters.cbData > 0U)
			{
				array = new byte[algorithmIdentifier.Parameters.cbData];
				Marshal.Copy(algorithmIdentifier.Parameters.pbData, array, 0, array.Length);
			}
			this.Reset(Oid.FromOidValue(algorithmIdentifier.pszObjId, OidGroup.All), keyLength, array);
			invalidHandle.Dispose();
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x000182F7 File Offset: 0x000164F7
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x000182FF File Offset: 0x000164FF
		public Oid Oid
		{
			get
			{
				return this.m_oid;
			}
			set
			{
				this.m_oid = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00018308 File Offset: 0x00016508
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x00018310 File Offset: 0x00016510
		public int KeyLength
		{
			get
			{
				return this.m_keyLength;
			}
			set
			{
				this.m_keyLength = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00018319 File Offset: 0x00016519
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x00018321 File Offset: 0x00016521
		public byte[] Parameters
		{
			get
			{
				return this.m_parameters;
			}
			set
			{
				this.m_parameters = value;
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001832A File Offset: 0x0001652A
		private void Reset(Oid oid, int keyLength, byte[] parameters)
		{
			this.m_oid = oid;
			this.m_keyLength = keyLength;
			this.m_parameters = parameters;
		}

		// Token: 0x0400050A RID: 1290
		private Oid m_oid;

		// Token: 0x0400050B RID: 1291
		private int m_keyLength;

		// Token: 0x0400050C RID: 1292
		private byte[] m_parameters;
	}
}
