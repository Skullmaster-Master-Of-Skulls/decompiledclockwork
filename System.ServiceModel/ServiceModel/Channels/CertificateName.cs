using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A4B RID: 2635
	internal sealed class CertificateName
	{
		// Token: 0x0600682B RID: 26667
		[DllImport("Crypt32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CertStrToName(CertificateName.CertEncodingType dwCertEncodingType, [MarshalAs(UnmanagedType.LPTStr)] string pszX500, CertificateName.StringType dwStrType, IntPtr pvReserved, [In] [Out] byte[] pbEncoded, [In] [Out] ref int pcbEncoded, [MarshalAs(UnmanagedType.LPTStr)] ref StringBuilder ppszError);

		// Token: 0x0600682C RID: 26668 RVA: 0x00184965 File Offset: 0x00182B65
		public CertificateName(string dn)
		{
			this.dn = dn;
		}

		// Token: 0x170018ED RID: 6381
		// (get) Token: 0x0600682D RID: 26669 RVA: 0x00184974 File Offset: 0x00182B74
		public string DistinguishedName
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x0600682E RID: 26670 RVA: 0x0018497C File Offset: 0x00182B7C
		public CryptoApiBlob GetCryptoApiBlob()
		{
			byte[] encodedName = this.GetEncodedName();
			return new CryptoApiBlob(encodedName);
		}

		// Token: 0x0600682F RID: 26671 RVA: 0x00184998 File Offset: 0x00182B98
		private byte[] GetEncodedName()
		{
			int num = 0;
			StringBuilder stringBuilder = null;
			CertificateName.CertStrToName(CertificateName.CertEncodingType.X509AsnEncoding | CertificateName.CertEncodingType.PKCS7AsnEncoding, this.DistinguishedName, CertificateName.StringType.OIDNameString | CertificateName.StringType.ReverseFlag, IntPtr.Zero, null, ref num, ref stringBuilder);
			byte[] array = new byte[num];
			if (!CertificateName.CertStrToName(CertificateName.CertEncodingType.X509AsnEncoding | CertificateName.CertEncodingType.PKCS7AsnEncoding, this.DistinguishedName, CertificateName.StringType.OIDNameString | CertificateName.StringType.ReverseFlag, IntPtr.Zero, array, ref num, ref stringBuilder))
			{
				PeerExceptionHelper.ThrowInvalidOperation_PeerCertGenFailure(PeerExceptionHelper.GetLastException());
			}
			return array;
		}

		// Token: 0x04003BB9 RID: 15289
		private string dn;

		// Token: 0x02000E7C RID: 3708
		[Flags]
		private enum CertEncodingType
		{
			// Token: 0x04004B28 RID: 19240
			X509AsnEncoding = 1,
			// Token: 0x04004B29 RID: 19241
			PKCS7AsnEncoding = 65536
		}

		// Token: 0x02000E7D RID: 3709
		[Flags]
		private enum StringType
		{
			// Token: 0x04004B2B RID: 19243
			SimpleNameString = 1,
			// Token: 0x04004B2C RID: 19244
			OIDNameString = 2,
			// Token: 0x04004B2D RID: 19245
			X500NameString = 3,
			// Token: 0x04004B2E RID: 19246
			CommaFlag = 67108864,
			// Token: 0x04004B2F RID: 19247
			SemicolonFlag = 1073741824,
			// Token: 0x04004B30 RID: 19248
			CRLFFlag = 134217728,
			// Token: 0x04004B31 RID: 19249
			NoPlusFlag = 536870912,
			// Token: 0x04004B32 RID: 19250
			NoQuotingFlag = 268435456,
			// Token: 0x04004B33 RID: 19251
			ReverseFlag = 33554432,
			// Token: 0x04004B34 RID: 19252
			DisableIE4UTF8Flag = 65536,
			// Token: 0x04004B35 RID: 19253
			EnableT61UnicodeFlag = 131072,
			// Token: 0x04004B36 RID: 19254
			EnableUTF8UnicodeFlag = 262144
		}
	}
}
