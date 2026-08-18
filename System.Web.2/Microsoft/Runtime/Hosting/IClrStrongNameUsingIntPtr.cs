using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Runtime.Hosting
{
	// Token: 0x02000011 RID: 17
	[SecurityCritical]
	[ComConversionLoss]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("9FD93CCF-3280-4391-B3A9-96E1CDE77C8D")]
	[ComImport]
	internal interface IClrStrongNameUsingIntPtr
	{
		// Token: 0x0600006D RID: 109
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int GetHashFromAssemblyFile([MarshalAs(UnmanagedType.LPStr)] [In] string pszFilePath, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref int piHashAlg, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [Out] byte[] pbHash, [MarshalAs(UnmanagedType.U4)] [In] int cchHash, [MarshalAs(UnmanagedType.U4)] out int pchHash);

		// Token: 0x0600006E RID: 110
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int GetHashFromAssemblyFileW([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzFilePath, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref int piHashAlg, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [Out] byte[] pbHash, [MarshalAs(UnmanagedType.U4)] [In] int cchHash, [MarshalAs(UnmanagedType.U4)] out int pchHash);

		// Token: 0x0600006F RID: 111
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int GetHashFromBlob([In] IntPtr pbBlob, [MarshalAs(UnmanagedType.U4)] [In] int cchBlob, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref int piHashAlg, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] [Out] byte[] pbHash, [MarshalAs(UnmanagedType.U4)] [In] int cchHash, [MarshalAs(UnmanagedType.U4)] out int pchHash);

		// Token: 0x06000070 RID: 112
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int GetHashFromFile([MarshalAs(UnmanagedType.LPStr)] [In] string pszFilePath, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref int piHashAlg, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [Out] byte[] pbHash, [MarshalAs(UnmanagedType.U4)] [In] int cchHash, [MarshalAs(UnmanagedType.U4)] out int pchHash);

		// Token: 0x06000071 RID: 113
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int GetHashFromFileW([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzFilePath, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref int piHashAlg, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [Out] byte[] pbHash, [MarshalAs(UnmanagedType.U4)] [In] int cchHash, [MarshalAs(UnmanagedType.U4)] out int pchHash);

		// Token: 0x06000072 RID: 114
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int GetHashFromHandle([In] IntPtr hFile, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref int piHashAlg, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [Out] byte[] pbHash, [MarshalAs(UnmanagedType.U4)] [In] int cchHash, [MarshalAs(UnmanagedType.U4)] out int pchHash);

		// Token: 0x06000073 RID: 115
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.U4)]
		int StrongNameCompareAssemblies([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzAssembly1, [MarshalAs(UnmanagedType.LPWStr)] [In] string pwzAssembly2, [MarshalAs(UnmanagedType.U4)] out int dwResult);

		// Token: 0x06000074 RID: 116
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameFreeBuffer([In] IntPtr pbMemory);

		// Token: 0x06000075 RID: 117
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameGetBlob([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzFilePath, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] byte[] pbBlob, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref int pcbBlob);

		// Token: 0x06000076 RID: 118
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameGetBlobFromImage([In] IntPtr pbBase, [MarshalAs(UnmanagedType.U4)] [In] int dwLength, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [Out] byte[] pbBlob, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref int pcbBlob);

		// Token: 0x06000077 RID: 119
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameGetPublicKey([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzKeyContainer, [In] IntPtr pbKeyBlob, [MarshalAs(UnmanagedType.U4)] [In] int cbKeyBlob, out IntPtr ppbPublicKeyBlob, [MarshalAs(UnmanagedType.U4)] out int pcbPublicKeyBlob);

		// Token: 0x06000078 RID: 120
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.U4)]
		int StrongNameHashSize([MarshalAs(UnmanagedType.U4)] [In] int ulHashAlg, [MarshalAs(UnmanagedType.U4)] out int cbSize);

		// Token: 0x06000079 RID: 121
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameKeyDelete([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzKeyContainer);

		// Token: 0x0600007A RID: 122
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameKeyGen([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzKeyContainer, [MarshalAs(UnmanagedType.U4)] [In] int dwFlags, out IntPtr ppbKeyBlob, [MarshalAs(UnmanagedType.U4)] out int pcbKeyBlob);

		// Token: 0x0600007B RID: 123
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameKeyGenEx([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzKeyContainer, [MarshalAs(UnmanagedType.U4)] [In] int dwFlags, [MarshalAs(UnmanagedType.U4)] [In] int dwKeySize, out IntPtr ppbKeyBlob, [MarshalAs(UnmanagedType.U4)] out int pcbKeyBlob);

		// Token: 0x0600007C RID: 124
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameKeyInstall([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzKeyContainer, [In] IntPtr pbKeyBlob, [MarshalAs(UnmanagedType.U4)] [In] int cbKeyBlob);

		// Token: 0x0600007D RID: 125
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameSignatureGeneration([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzFilePath, [MarshalAs(UnmanagedType.LPWStr)] [In] string pwzKeyContainer, [In] IntPtr pbKeyBlob, [MarshalAs(UnmanagedType.U4)] [In] int cbKeyBlob, [In] [Out] IntPtr ppbSignatureBlob, [MarshalAs(UnmanagedType.U4)] out int pcbSignatureBlob);

		// Token: 0x0600007E RID: 126
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameSignatureGenerationEx([MarshalAs(UnmanagedType.LPWStr)] [In] string wszFilePath, [MarshalAs(UnmanagedType.LPWStr)] [In] string wszKeyContainer, [In] IntPtr pbKeyBlob, [MarshalAs(UnmanagedType.U4)] [In] int cbKeyBlob, [In] [Out] IntPtr ppbSignatureBlob, [MarshalAs(UnmanagedType.U4)] out int pcbSignatureBlob, [MarshalAs(UnmanagedType.U4)] [In] int dwFlags);

		// Token: 0x0600007F RID: 127
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameSignatureSize([In] IntPtr pbPublicKeyBlob, [MarshalAs(UnmanagedType.U4)] [In] int cbPublicKeyBlob, [MarshalAs(UnmanagedType.U4)] out int pcbSize);

		// Token: 0x06000080 RID: 128
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.U4)]
		int StrongNameSignatureVerification([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzFilePath, [MarshalAs(UnmanagedType.U4)] [In] int dwInFlags, [MarshalAs(UnmanagedType.U4)] out int dwOutFlags);

		// Token: 0x06000081 RID: 129
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.U4)]
		int StrongNameSignatureVerificationEx([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzFilePath, [MarshalAs(UnmanagedType.I1)] [In] bool fForceVerification, [MarshalAs(UnmanagedType.I1)] out bool fWasVerified);

		// Token: 0x06000082 RID: 130
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.U4)]
		int StrongNameSignatureVerificationFromImage([In] IntPtr pbBase, [MarshalAs(UnmanagedType.U4)] [In] int dwLength, [MarshalAs(UnmanagedType.U4)] [In] int dwInFlags, [MarshalAs(UnmanagedType.U4)] out int dwOutFlags);

		// Token: 0x06000083 RID: 131
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameTokenFromAssembly([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzFilePath, out IntPtr ppbStrongNameToken, [MarshalAs(UnmanagedType.U4)] out int pcbStrongNameToken);

		// Token: 0x06000084 RID: 132
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameTokenFromAssemblyEx([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzFilePath, out IntPtr ppbStrongNameToken, [MarshalAs(UnmanagedType.U4)] out int pcbStrongNameToken, out IntPtr ppbPublicKeyBlob, [MarshalAs(UnmanagedType.U4)] out int pcbPublicKeyBlob);

		// Token: 0x06000085 RID: 133
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameTokenFromPublicKey([In] IntPtr pbPublicKeyBlob, [MarshalAs(UnmanagedType.U4)] [In] int cbPublicKeyBlob, out IntPtr ppbStrongNameToken, [MarshalAs(UnmanagedType.U4)] out int pcbStrongNameToken);
	}
}
