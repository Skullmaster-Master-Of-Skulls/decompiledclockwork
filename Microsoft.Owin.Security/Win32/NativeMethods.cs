using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.Win32
{
	// Token: 0x02000038 RID: 56
	[Localizable(false)]
	internal static class NativeMethods
	{
		// Token: 0x060000EB RID: 235
		[DllImport("crypt32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CryptEncodeObject(uint dwCertEncodingType, IntPtr lpszStructType, ref NativeMethods.CERT_PUBLIC_KEY_INFO pvStructInfo, byte[] pbEncoded, ref uint pcbEncoded);

		// Token: 0x0400005A RID: 90
		public const int X509_ASN_ENCODING = 1;

		// Token: 0x0400005B RID: 91
		public const int X509_PUBLIC_KEY_INFO = 8;

		// Token: 0x02000039 RID: 57
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPT_BLOB
		{
			// Token: 0x0400005C RID: 92
			public int cbData;

			// Token: 0x0400005D RID: 93
			public IntPtr pbData;
		}

		// Token: 0x0200003A RID: 58
		internal struct CERT_CONTEXT
		{
			// Token: 0x0400005E RID: 94
			public int dwCertEncodingType;

			// Token: 0x0400005F RID: 95
			public IntPtr pbCertEncoded;

			// Token: 0x04000060 RID: 96
			public int cbCertEncoded;

			// Token: 0x04000061 RID: 97
			public IntPtr pCertInfo;

			// Token: 0x04000062 RID: 98
			public IntPtr hCertStore;
		}

		// Token: 0x0200003B RID: 59
		internal struct CRYPT_ALGORITHM_IDENTIFIER
		{
			// Token: 0x04000063 RID: 99
			public string pszObjId;

			// Token: 0x04000064 RID: 100
			public NativeMethods.CRYPT_BLOB Parameters;
		}

		// Token: 0x0200003C RID: 60
		internal struct CRYPT_BIT_BLOB
		{
			// Token: 0x04000065 RID: 101
			public int cbData;

			// Token: 0x04000066 RID: 102
			public IntPtr pbData;

			// Token: 0x04000067 RID: 103
			public int cUnusedBits;
		}

		// Token: 0x0200003D RID: 61
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_PUBLIC_KEY_INFO
		{
			// Token: 0x04000068 RID: 104
			public NativeMethods.CRYPT_ALGORITHM_IDENTIFIER Algorithm;

			// Token: 0x04000069 RID: 105
			public NativeMethods.CRYPT_BIT_BLOB PublicKey;
		}

		// Token: 0x0200003E RID: 62
		[StructLayout(LayoutKind.Sequential)]
		internal class CERT_INFO
		{
			// Token: 0x0400006A RID: 106
			public int dwVersion;

			// Token: 0x0400006B RID: 107
			public NativeMethods.CRYPT_BLOB SerialNumber;

			// Token: 0x0400006C RID: 108
			public NativeMethods.CRYPT_ALGORITHM_IDENTIFIER SignatureAlgorithm;

			// Token: 0x0400006D RID: 109
			public NativeMethods.CRYPT_BLOB Issuer;

			// Token: 0x0400006E RID: 110
			public System.Runtime.InteropServices.ComTypes.FILETIME NotBefore;

			// Token: 0x0400006F RID: 111
			public System.Runtime.InteropServices.ComTypes.FILETIME NotAfter;

			// Token: 0x04000070 RID: 112
			public NativeMethods.CRYPT_BLOB Subject;

			// Token: 0x04000071 RID: 113
			public NativeMethods.CERT_PUBLIC_KEY_INFO SubjectPublicKeyInfo;

			// Token: 0x04000072 RID: 114
			public NativeMethods.CRYPT_BIT_BLOB IssuerUniqueId;

			// Token: 0x04000073 RID: 115
			public NativeMethods.CRYPT_BIT_BLOB SubjectUniqueId;

			// Token: 0x04000074 RID: 116
			public int cExtension;

			// Token: 0x04000075 RID: 117
			public IntPtr rgExtension;
		}
	}
}
