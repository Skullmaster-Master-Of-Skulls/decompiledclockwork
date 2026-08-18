using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000479 RID: 1145
	public sealed class X509EnhancedKeyUsageExtension : X509Extension
	{
		// Token: 0x06002A7B RID: 10875 RVA: 0x000C1A85 File Offset: 0x000BFC85
		public X509EnhancedKeyUsageExtension() : base("2.5.29.37")
		{
			this.m_enhancedKeyUsages = new OidCollection();
			this.m_decoded = true;
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x000C1AA4 File Offset: 0x000BFCA4
		public X509EnhancedKeyUsageExtension(OidCollection enhancedKeyUsages, bool critical) : base("2.5.29.37", X509EnhancedKeyUsageExtension.EncodeExtension(enhancedKeyUsages), critical)
		{
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x000C1AB8 File Offset: 0x000BFCB8
		public X509EnhancedKeyUsageExtension(AsnEncodedData encodedEnhancedKeyUsages, bool critical) : base("2.5.29.37", encodedEnhancedKeyUsages.RawData, critical)
		{
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06002A7E RID: 10878 RVA: 0x000C1ACC File Offset: 0x000BFCCC
		public OidCollection EnhancedKeyUsages
		{
			get
			{
				if (!this.m_decoded)
				{
					this.DecodeExtension();
				}
				OidCollection oidCollection = new OidCollection();
				foreach (Oid oid in this.m_enhancedKeyUsages)
				{
					oidCollection.Add(oid);
				}
				return oidCollection;
			}
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x000C1B13 File Offset: 0x000BFD13
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			this.m_decoded = false;
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x000C1B24 File Offset: 0x000BFD24
		private void DecodeExtension()
		{
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = null;
			if (!CAPI.DecodeObject(new IntPtr(36L), this.m_rawData, out safeLocalAllocHandle, out num))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			CAPIBase.CERT_ENHKEY_USAGE cert_ENHKEY_USAGE = (CAPIBase.CERT_ENHKEY_USAGE)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPIBase.CERT_ENHKEY_USAGE));
			this.m_enhancedKeyUsages = new OidCollection();
			int num2 = 0;
			while ((long)num2 < (long)((ulong)cert_ENHKEY_USAGE.cUsageIdentifier))
			{
				IntPtr ptr = Marshal.ReadIntPtr(new IntPtr((long)cert_ENHKEY_USAGE.rgpszUsageIdentifier + (long)(num2 * Marshal.SizeOf(typeof(IntPtr)))));
				string oid = Marshal.PtrToStringAnsi(ptr);
				Oid oid2 = new Oid(oid, OidGroup.ExtensionOrAttribute, false);
				this.m_enhancedKeyUsages.Add(oid2);
				num2++;
			}
			this.m_decoded = true;
			safeLocalAllocHandle.Dispose();
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x000C1BF4 File Offset: 0x000BFDF4
		private unsafe static byte[] EncodeExtension(OidCollection enhancedKeyUsages)
		{
			if (enhancedKeyUsages == null)
			{
				throw new ArgumentNullException("enhancedKeyUsages");
			}
			SafeLocalAllocHandle safeLocalAllocHandle = X509Utils.CopyOidsToUnmanagedMemory(enhancedKeyUsages);
			byte[] result = null;
			using (safeLocalAllocHandle)
			{
				CAPIBase.CERT_ENHKEY_USAGE cert_ENHKEY_USAGE = default(CAPIBase.CERT_ENHKEY_USAGE);
				cert_ENHKEY_USAGE.cUsageIdentifier = (uint)enhancedKeyUsages.Count;
				cert_ENHKEY_USAGE.rgpszUsageIdentifier = safeLocalAllocHandle.DangerousGetHandle();
				if (!CAPI.EncodeObject("2.5.29.37", new IntPtr((void*)(&cert_ENHKEY_USAGE)), out result))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
			return result;
		}

		// Token: 0x0400263F RID: 9791
		private OidCollection m_enhancedKeyUsages;

		// Token: 0x04002640 RID: 9792
		private bool m_decoded;
	}
}
