using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000477 RID: 1143
	public sealed class X509KeyUsageExtension : X509Extension
	{
		// Token: 0x06002A6B RID: 10859 RVA: 0x000C16D4 File Offset: 0x000BF8D4
		public X509KeyUsageExtension() : base("2.5.29.15")
		{
			this.m_decoded = true;
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x000C16E8 File Offset: 0x000BF8E8
		public X509KeyUsageExtension(X509KeyUsageFlags keyUsages, bool critical) : base("2.5.29.15", X509KeyUsageExtension.EncodeExtension(keyUsages), critical)
		{
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x000C16FC File Offset: 0x000BF8FC
		public X509KeyUsageExtension(AsnEncodedData encodedKeyUsage, bool critical) : base("2.5.29.15", encodedKeyUsage.RawData, critical)
		{
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06002A6E RID: 10862 RVA: 0x000C1710 File Offset: 0x000BF910
		public X509KeyUsageFlags KeyUsages
		{
			get
			{
				if (!this.m_decoded)
				{
					this.DecodeExtension();
				}
				return (X509KeyUsageFlags)this.m_keyUsages;
			}
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x000C1726 File Offset: 0x000BF926
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			this.m_decoded = false;
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x000C1738 File Offset: 0x000BF938
		private void DecodeExtension()
		{
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = null;
			if (!CAPI.DecodeObject(new IntPtr(14L), this.m_rawData, out safeLocalAllocHandle, out num))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			CAPIBase.CRYPTOAPI_BLOB cryptoapi_BLOB = (CAPIBase.CRYPTOAPI_BLOB)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPIBase.CRYPTOAPI_BLOB));
			if (cryptoapi_BLOB.cbData > 4U)
			{
				cryptoapi_BLOB.cbData = 4U;
			}
			byte[] array = new byte[4];
			if (cryptoapi_BLOB.pbData != IntPtr.Zero)
			{
				Marshal.Copy(cryptoapi_BLOB.pbData, array, 0, (int)cryptoapi_BLOB.cbData);
			}
			this.m_keyUsages = BitConverter.ToUInt32(array, 0);
			this.m_decoded = true;
			safeLocalAllocHandle.Dispose();
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x000C17E4 File Offset: 0x000BF9E4
		private unsafe static byte[] EncodeExtension(X509KeyUsageFlags keyUsages)
		{
			CAPIBase.CRYPT_BIT_BLOB crypt_BIT_BLOB = default(CAPIBase.CRYPT_BIT_BLOB);
			crypt_BIT_BLOB.cbData = 2U;
			crypt_BIT_BLOB.pbData = new IntPtr((void*)(&keyUsages));
			crypt_BIT_BLOB.cUnusedBits = 0U;
			byte[] result = null;
			if (!CAPI.EncodeObject("2.5.29.15", new IntPtr((void*)(&crypt_BIT_BLOB)), out result))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return result;
		}

		// Token: 0x04002639 RID: 9785
		private uint m_keyUsages;

		// Token: 0x0400263A RID: 9786
		private bool m_decoded;
	}
}
