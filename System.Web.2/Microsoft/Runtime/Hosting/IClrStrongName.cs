using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Runtime.Hosting
{
	// Token: 0x02000012 RID: 18
	[SecurityCritical]
	[ComConversionLoss]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("9FD93CCF-3280-4391-B3A9-96E1CDE77C8D")]
	[ComImport]
	internal interface IClrStrongName
	{
		// Token: 0x06000086 RID: 134
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int GetHashFromAssemblyFile([MarshalAs(UnmanagedType.LPStr)] [In] string pszFilePath, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref int piHashAlg, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [Out] byte[] pbHash, [MarshalAs(UnmanagedType.U4)] [In] int cchHash, [MarshalAs(UnmanagedType.U4)] out int pchHash);

		// Token: 0x06000087 RID: 135
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int GetHashFromAssemblyFileW([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzFilePath, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref int piHashAlg, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [Out] byte[] pbHash, [MarshalAs(UnmanagedType.U4)] [In] int cchHash, [MarshalAs(UnmanagedType.U4)] out int pchHash);

		// Token: 0x06000088 RID: 136
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int GetHashFromBlob([In] IntPtr pbBlob, [MarshalAs(UnmanagedType.U4)] [In] int cchBlob, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref int piHashAlg, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] [Out] byte[] pbHash, [MarshalAs(UnmanagedType.U4)] [In] int cchHash, [MarshalAs(UnmanagedType.U4)] out int pchHash);

		// Token: 0x06000089 RID: 137
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int GetHashFromFile([MarshalAs(UnmanagedType.LPStr)] [In] string pszFilePath, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref int piHashAlg, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [Out] byte[] pbHash, [MarshalAs(UnmanagedType.U4)] [In] int cchHash, [MarshalAs(UnmanagedType.U4)] out int pchHash);

		// Token: 0x0600008A RID: 138
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int GetHashFromFileW([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzFilePath, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref int piHashAlg, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [Out] byte[] pbHash, [MarshalAs(UnmanagedType.U4)] [In] int cchHash, [MarshalAs(UnmanagedType.U4)] out int pchHash);

		// Token: 0x0600008B RID: 139
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int GetHashFromHandle([In] IntPtr hFile, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref int piHashAlg, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [Out] byte[] pbHash, [MarshalAs(UnmanagedType.U4)] [In] int cchHash, [MarshalAs(UnmanagedType.U4)] out int pchHash);

		// Token: 0x0600008C RID: 140
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.U4)]
		int StrongNameCompareAssemblies([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzAssembly1, [MarshalAs(UnmanagedType.LPWStr)] [In] string pwzAssembly2, [MarshalAs(UnmanagedType.U4)] out int dwResult);

		// Token: 0x0600008D RID: 141
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameFreeBuffer([In] IntPtr pbMemory);

		// Token: 0x0600008E RID: 142
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameGetBlob([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzFilePath, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] byte[] pbBlob, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref int pcbBlob);

		// Token: 0x0600008F RID: 143
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameGetBlobFromImage([In] IntPtr pbBase, [MarshalAs(UnmanagedType.U4)] [In] int dwLength, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [Out] byte[] pbBlob, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref int pcbBlob);

		// Token: 0x06000090 RID: 144
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameGetPublicKey([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzKeyContainer, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] byte[] pbKeyBlob, [MarshalAs(UnmanagedType.U4)] [In] int cbKeyBlob, out IntPtr ppbPublicKeyBlob, [MarshalAs(UnmanagedType.U4)] out int pcbPublicKeyBlob);

		// Token: 0x06000091 RID: 145
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.U4)]
		int StrongNameHashSize([MarshalAs(UnmanagedType.U4)] [In] int ulHashAlg, [MarshalAs(UnmanagedType.U4)] out int cbSize);

		// Token: 0x06000092 RID: 146
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameKeyDelete([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzKeyContainer);

		// Token: 0x06000093 RID: 147
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameKeyGen([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzKeyContainer, [MarshalAs(UnmanagedType.U4)] [In] int dwFlags, out IntPtr ppbKeyBlob, [MarshalAs(UnmanagedType.U4)] out int pcbKeyBlob);

		// Token: 0x06000094 RID: 148
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameKeyGenEx([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzKeyContainer, [MarshalAs(UnmanagedType.U4)] [In] int dwFlags, [MarshalAs(UnmanagedType.U4)] [In] int dwKeySize, out IntPtr ppbKeyBlob, [MarshalAs(UnmanagedType.U4)] out int pcbKeyBlob);

		// Token: 0x06000095 RID: 149
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameKeyInstall([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzKeyContainer, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] byte[] pbKeyBlob, [MarshalAs(UnmanagedType.U4)] [In] int cbKeyBlob);

		// Token: 0x06000096 RID: 150
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameSignatureGeneration([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzFilePath, [MarshalAs(UnmanagedType.LPWStr)] [In] string pwzKeyContainer, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [In] byte[] pbKeyBlob, [MarshalAs(UnmanagedType.U4)] [In] int cbKeyBlob, [In] [Out] IntPtr ppbSignatureBlob, [MarshalAs(UnmanagedType.U4)] out int pcbSignatureBlob);

		// Token: 0x06000097 RID: 151
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameSignatureGenerationEx([MarshalAs(UnmanagedType.LPWStr)] [In] string wszFilePath, [MarshalAs(UnmanagedType.LPWStr)] [In] string wszKeyContainer, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [In] byte[] pbKeyBlob, [MarshalAs(UnmanagedType.U4)] [In] int cbKeyBlob, [In] [Out] IntPtr ppbSignatureBlob, [MarshalAs(UnmanagedType.U4)] out int pcbSignatureBlob, [MarshalAs(UnmanagedType.U4)] [In] int dwFlags);

		// Token: 0x06000098 RID: 152
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameSignatureSize([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] byte[] pbPublicKeyBlob, [MarshalAs(UnmanagedType.U4)] [In] int cbPublicKeyBlob, [MarshalAs(UnmanagedType.U4)] out int pcbSize);

		// Token: 0x06000099 RID: 153
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.U4)]
		int StrongNameSignatureVerification([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzFilePath, [MarshalAs(UnmanagedType.U4)] [In] int dwInFlags, [MarshalAs(UnmanagedType.U4)] out int dwOutFlags);

		// Token: 0x0600009A RID: 154
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.U4)]
		int StrongNameSignatureVerificationEx([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzFilePath, [MarshalAs(UnmanagedType.I1)] [In] bool fForceVerification, [MarshalAs(UnmanagedType.I1)] out bool fWasVerified);

		// Token: 0x0600009B RID: 155
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.U4)]
		int StrongNameSignatureVerificationFromImage([In] IntPtr pbBase, [MarshalAs(UnmanagedType.U4)] [In] int dwLength, [MarshalAs(UnmanagedType.U4)] [In] int dwInFlags, [MarshalAs(UnmanagedType.U4)] out int dwOutFlags);

		// Token: 0x0600009C RID: 156
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameTokenFromAssembly([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzFilePath, out IntPtr ppbStrongNameToken, [MarshalAs(UnmanagedType.U4)] out int pcbStrongNameToken);

		// Token: 0x0600009D RID: 157
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameTokenFromAssemblyEx([MarshalAs(UnmanagedType.LPWStr)] [In] string pwzFilePath, out IntPtr ppbStrongNameToken, [MarshalAs(UnmanagedType.U4)] out int pcbStrongNameToken, out IntPtr ppbPublicKeyBlob, [MarshalAs(UnmanagedType.U4)] out int pcbPublicKeyBlob);

		// Token: 0x0600009E RID: 158
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int StrongNameTokenFromPublicKey([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] byte[] pbPublicKeyBlob, [MarshalAs(UnmanagedType.U4)] [In] int cbPublicKeyBlob, out IntPtr ppbStrongNameToken, [MarshalAs(UnmanagedType.U4)] out int pcbStrongNameToken);
	}
}
