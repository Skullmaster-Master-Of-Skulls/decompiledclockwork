using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000465 RID: 1125
	public sealed class PublicKey
	{
		// Token: 0x060029B4 RID: 10676 RVA: 0x000BD196 File Offset: 0x000BB396
		private PublicKey()
		{
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x000BD19E File Offset: 0x000BB39E
		public PublicKey(Oid oid, AsnEncodedData parameters, AsnEncodedData keyValue)
		{
			this.m_oid = new Oid(oid);
			this.m_encodedParameters = new AsnEncodedData(parameters);
			this.m_encodedKeyValue = new AsnEncodedData(keyValue);
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x000BD1CA File Offset: 0x000BB3CA
		internal PublicKey(PublicKey publicKey)
		{
			this.m_oid = new Oid(publicKey.m_oid);
			this.m_encodedParameters = new AsnEncodedData(publicKey.m_encodedParameters);
			this.m_encodedKeyValue = new AsnEncodedData(publicKey.m_encodedKeyValue);
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x060029B7 RID: 10679 RVA: 0x000BD205 File Offset: 0x000BB405
		internal uint AlgorithmId
		{
			get
			{
				if (this.m_aiPubKey == 0U)
				{
					this.m_aiPubKey = X509Utils.OidToAlgId(this.m_oid.Value);
				}
				return this.m_aiPubKey;
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x060029B8 RID: 10680 RVA: 0x000BD22B File Offset: 0x000BB42B
		private byte[] CspBlobData
		{
			get
			{
				if (this.m_cspBlobData == null)
				{
					PublicKey.DecodePublicKeyObject(this.AlgorithmId, this.m_encodedKeyValue.RawData, this.m_encodedParameters.RawData, out this.m_cspBlobData);
				}
				return this.m_cspBlobData;
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x060029B9 RID: 10681 RVA: 0x000BD264 File Offset: 0x000BB464
		public AsymmetricAlgorithm Key
		{
			get
			{
				if (this.m_key == null)
				{
					uint algorithmId = this.AlgorithmId;
					if (algorithmId != 8704U)
					{
						if (algorithmId != 9216U && algorithmId != 41984U)
						{
							throw new NotSupportedException(SR.GetString("NotSupported_KeyAlgorithm"));
						}
						RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider();
						rsacryptoServiceProvider.ImportCspBlob(this.CspBlobData);
						this.m_key = rsacryptoServiceProvider;
					}
					else
					{
						DSACryptoServiceProvider dsacryptoServiceProvider = new DSACryptoServiceProvider();
						dsacryptoServiceProvider.ImportCspBlob(this.CspBlobData);
						this.m_key = dsacryptoServiceProvider;
					}
				}
				return this.m_key;
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x060029BA RID: 10682 RVA: 0x000BD2E4 File Offset: 0x000BB4E4
		public Oid Oid
		{
			get
			{
				return new Oid(this.m_oid);
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x060029BB RID: 10683 RVA: 0x000BD2F1 File Offset: 0x000BB4F1
		public AsnEncodedData EncodedKeyValue
		{
			get
			{
				return this.m_encodedKeyValue;
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x060029BC RID: 10684 RVA: 0x000BD2F9 File Offset: 0x000BB4F9
		public AsnEncodedData EncodedParameters
		{
			get
			{
				return this.m_encodedParameters;
			}
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x000BD304 File Offset: 0x000BB504
		private static void DecodePublicKeyObject(uint aiPubKey, byte[] encodedKeyValue, byte[] encodedParameters, out byte[] decodedData)
		{
			decodedData = null;
			IntPtr zero = IntPtr.Zero;
			if (aiPubKey <= 9216U)
			{
				if (aiPubKey == 8704U)
				{
					zero = new IntPtr(38L);
					goto IL_6F;
				}
				if (aiPubKey != 9216U)
				{
					goto IL_5F;
				}
			}
			else if (aiPubKey != 41984U)
			{
				if (aiPubKey - 43521U > 1U)
				{
					goto IL_5F;
				}
				throw new NotSupportedException(SR.GetString("NotSupported_KeyAlgorithm"));
			}
			zero = new IntPtr(19L);
			goto IL_6F;
			IL_5F:
			throw new NotSupportedException(SR.GetString("NotSupported_KeyAlgorithm"));
			IL_6F:
			SafeLocalAllocHandle safeLocalAllocHandle = null;
			uint num = 0U;
			if (!CAPI.DecodeObject(zero, encodedKeyValue, out safeLocalAllocHandle, out num))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			if ((int)zero == 19)
			{
				decodedData = new byte[num];
				Marshal.Copy(safeLocalAllocHandle.DangerousGetHandle(), decodedData, 0, decodedData.Length);
			}
			else if ((int)zero == 38)
			{
				SafeLocalAllocHandle safeLocalAllocHandle2 = null;
				uint num2 = 0U;
				if (!CAPI.DecodeObject(new IntPtr(39L), encodedParameters, out safeLocalAllocHandle2, out num2))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				decodedData = PublicKey.ConstructDSSPubKeyCspBlob(safeLocalAllocHandle, safeLocalAllocHandle2);
				safeLocalAllocHandle2.Dispose();
			}
			safeLocalAllocHandle.Dispose();
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x000BD40C File Offset: 0x000BB60C
		private static byte[] ConstructDSSPubKeyCspBlob(SafeLocalAllocHandle decodedKeyValue, SafeLocalAllocHandle decodedParameters)
		{
			CAPIBase.CRYPTOAPI_BLOB cryptoapi_BLOB = (CAPIBase.CRYPTOAPI_BLOB)Marshal.PtrToStructure(decodedKeyValue.DangerousGetHandle(), typeof(CAPIBase.CRYPTOAPI_BLOB));
			CAPIBase.CERT_DSS_PARAMETERS cert_DSS_PARAMETERS = (CAPIBase.CERT_DSS_PARAMETERS)Marshal.PtrToStructure(decodedParameters.DangerousGetHandle(), typeof(CAPIBase.CERT_DSS_PARAMETERS));
			uint cbData = cert_DSS_PARAMETERS.p.cbData;
			if (cbData == 0U)
			{
				throw new CryptographicException(-2146893803);
			}
			uint capacity = 16U + cbData + 20U + cbData + cbData + 24U;
			MemoryStream memoryStream = new MemoryStream((int)capacity);
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(6);
			binaryWriter.Write(2);
			binaryWriter.Write(0);
			binaryWriter.Write(8704U);
			binaryWriter.Write(827544388U);
			binaryWriter.Write(cbData * 8U);
			byte[] array = new byte[cert_DSS_PARAMETERS.p.cbData];
			Marshal.Copy(cert_DSS_PARAMETERS.p.pbData, array, 0, array.Length);
			binaryWriter.Write(array);
			uint cbData2 = cert_DSS_PARAMETERS.q.cbData;
			if (cbData2 == 0U || cbData2 > 20U)
			{
				throw new CryptographicException(-2146893803);
			}
			byte[] array2 = new byte[cert_DSS_PARAMETERS.q.cbData];
			Marshal.Copy(cert_DSS_PARAMETERS.q.pbData, array2, 0, array2.Length);
			binaryWriter.Write(array2);
			if (20U > cbData2)
			{
				binaryWriter.Write(new byte[20U - cbData2]);
			}
			cbData2 = cert_DSS_PARAMETERS.g.cbData;
			if (cbData2 == 0U || cbData2 > cbData)
			{
				throw new CryptographicException(-2146893803);
			}
			byte[] array3 = new byte[cert_DSS_PARAMETERS.g.cbData];
			Marshal.Copy(cert_DSS_PARAMETERS.g.pbData, array3, 0, array3.Length);
			binaryWriter.Write(array3);
			if (cbData > cbData2)
			{
				binaryWriter.Write(new byte[cbData - cbData2]);
			}
			cbData2 = cryptoapi_BLOB.cbData;
			if (cbData2 == 0U || cbData2 > cbData)
			{
				throw new CryptographicException(-2146893803);
			}
			byte[] array4 = new byte[cryptoapi_BLOB.cbData];
			Marshal.Copy(cryptoapi_BLOB.pbData, array4, 0, array4.Length);
			binaryWriter.Write(array4);
			if (cbData > cbData2)
			{
				binaryWriter.Write(new byte[cbData - cbData2]);
			}
			binaryWriter.Write(uint.MaxValue);
			binaryWriter.Write(new byte[20]);
			return memoryStream.ToArray();
		}

		// Token: 0x040025BC RID: 9660
		private AsnEncodedData m_encodedKeyValue;

		// Token: 0x040025BD RID: 9661
		private AsnEncodedData m_encodedParameters;

		// Token: 0x040025BE RID: 9662
		private Oid m_oid;

		// Token: 0x040025BF RID: 9663
		private uint m_aiPubKey;

		// Token: 0x040025C0 RID: 9664
		private byte[] m_cspBlobData;

		// Token: 0x040025C1 RID: 9665
		private AsymmetricAlgorithm m_key;
	}
}
