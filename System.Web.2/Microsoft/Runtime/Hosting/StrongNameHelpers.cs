using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Runtime.Hosting
{
	// Token: 0x02000010 RID: 16
	internal static class StrongNameHelpers
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003130 File Offset: 0x00001330
		private static IClrStrongName StrongName
		{
			[SecurityCritical]
			get
			{
				if (StrongNameHelpers.s_StrongName == null)
				{
					StrongNameHelpers.s_StrongName = (IClrStrongName)RuntimeEnvironment.GetRuntimeInterfaceAsObject(new Guid("B79B0ACD-F5CD-409b-B5A5-A16244610B92"), new Guid("9FD93CCF-3280-4391-B3A9-96E1CDE77C8D"));
				}
				return StrongNameHelpers.s_StrongName;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003161 File Offset: 0x00001361
		private static IClrStrongNameUsingIntPtr StrongNameUsingIntPtr
		{
			[SecurityCritical]
			get
			{
				return (IClrStrongNameUsingIntPtr)StrongNameHelpers.StrongName;
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000316D File Offset: 0x0000136D
		[SecurityCritical]
		public static int StrongNameErrorInfo()
		{
			return StrongNameHelpers.ts_LastStrongNameHR;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003174 File Offset: 0x00001374
		[SecurityCritical]
		public static void StrongNameFreeBuffer(IntPtr pbMemory)
		{
			StrongNameHelpers.StrongNameUsingIntPtr.StrongNameFreeBuffer(pbMemory);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003184 File Offset: 0x00001384
		[SecurityCritical]
		public static bool StrongNameGetPublicKey(string pwzKeyContainer, IntPtr pbKeyBlob, int cbKeyBlob, out IntPtr ppbPublicKeyBlob, out int pcbPublicKeyBlob)
		{
			int num = StrongNameHelpers.StrongNameUsingIntPtr.StrongNameGetPublicKey(pwzKeyContainer, pbKeyBlob, cbKeyBlob, out ppbPublicKeyBlob, out pcbPublicKeyBlob);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				ppbPublicKeyBlob = IntPtr.Zero;
				pcbPublicKeyBlob = 0;
				return false;
			}
			return true;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000031BC File Offset: 0x000013BC
		[SecurityCritical]
		public static bool StrongNameKeyDelete(string pwzKeyContainer)
		{
			int num = StrongNameHelpers.StrongName.StrongNameKeyDelete(pwzKeyContainer);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				return false;
			}
			return true;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000031E4 File Offset: 0x000013E4
		[SecurityCritical]
		public static bool StrongNameKeyGen(string pwzKeyContainer, int dwFlags, out IntPtr ppbKeyBlob, out int pcbKeyBlob)
		{
			int num = StrongNameHelpers.StrongName.StrongNameKeyGen(pwzKeyContainer, dwFlags, out ppbKeyBlob, out pcbKeyBlob);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				ppbKeyBlob = IntPtr.Zero;
				pcbKeyBlob = 0;
				return false;
			}
			return true;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003218 File Offset: 0x00001418
		[SecurityCritical]
		public static bool StrongNameKeyInstall(string pwzKeyContainer, IntPtr pbKeyBlob, int cbKeyBlob)
		{
			int num = StrongNameHelpers.StrongNameUsingIntPtr.StrongNameKeyInstall(pwzKeyContainer, pbKeyBlob, cbKeyBlob);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				return false;
			}
			return true;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003240 File Offset: 0x00001440
		[SecurityCritical]
		public static bool StrongNameSignatureGeneration(string pwzFilePath, string pwzKeyContainer, IntPtr pbKeyBlob, int cbKeyBlob)
		{
			IntPtr zero = IntPtr.Zero;
			int num = 0;
			return StrongNameHelpers.StrongNameSignatureGeneration(pwzFilePath, pwzKeyContainer, pbKeyBlob, cbKeyBlob, ref zero, out num);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003264 File Offset: 0x00001464
		[SecurityCritical]
		public static bool StrongNameSignatureGeneration(string pwzFilePath, string pwzKeyContainer, IntPtr pbKeyBlob, int cbKeyBlob, ref IntPtr ppbSignatureBlob, out int pcbSignatureBlob)
		{
			int num = StrongNameHelpers.StrongNameUsingIntPtr.StrongNameSignatureGeneration(pwzFilePath, pwzKeyContainer, pbKeyBlob, cbKeyBlob, ppbSignatureBlob, out pcbSignatureBlob);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				pcbSignatureBlob = 0;
				return false;
			}
			return true;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003298 File Offset: 0x00001498
		[SecurityCritical]
		public static bool StrongNameSignatureSize(IntPtr pbPublicKeyBlob, int cbPublicKeyBlob, out int pcbSize)
		{
			int num = StrongNameHelpers.StrongNameUsingIntPtr.StrongNameSignatureSize(pbPublicKeyBlob, cbPublicKeyBlob, out pcbSize);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				pcbSize = 0;
				return false;
			}
			return true;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000032C4 File Offset: 0x000014C4
		[SecurityCritical]
		public static bool StrongNameSignatureVerification(string pwzFilePath, int dwInFlags, out int pdwOutFlags)
		{
			int num = StrongNameHelpers.StrongName.StrongNameSignatureVerification(pwzFilePath, dwInFlags, out pdwOutFlags);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				pdwOutFlags = 0;
				return false;
			}
			return true;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000032F0 File Offset: 0x000014F0
		[SecurityCritical]
		public static bool StrongNameSignatureVerificationEx(string pwzFilePath, bool fForceVerification, out bool pfWasVerified)
		{
			int num = StrongNameHelpers.StrongName.StrongNameSignatureVerificationEx(pwzFilePath, fForceVerification, out pfWasVerified);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				pfWasVerified = false;
				return false;
			}
			return true;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000331C File Offset: 0x0000151C
		[SecurityCritical]
		public static bool StrongNameTokenFromPublicKey(IntPtr pbPublicKeyBlob, int cbPublicKeyBlob, out IntPtr ppbStrongNameToken, out int pcbStrongNameToken)
		{
			int num = StrongNameHelpers.StrongNameUsingIntPtr.StrongNameTokenFromPublicKey(pbPublicKeyBlob, cbPublicKeyBlob, out ppbStrongNameToken, out pcbStrongNameToken);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				ppbStrongNameToken = IntPtr.Zero;
				pcbStrongNameToken = 0;
				return false;
			}
			return true;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003350 File Offset: 0x00001550
		[SecurityCritical]
		public static bool StrongNameSignatureSize(byte[] bPublicKeyBlob, int cbPublicKeyBlob, out int pcbSize)
		{
			int num = StrongNameHelpers.StrongName.StrongNameSignatureSize(bPublicKeyBlob, cbPublicKeyBlob, out pcbSize);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				pcbSize = 0;
				return false;
			}
			return true;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000337C File Offset: 0x0000157C
		[SecurityCritical]
		public static bool StrongNameTokenFromPublicKey(byte[] bPublicKeyBlob, int cbPublicKeyBlob, out IntPtr ppbStrongNameToken, out int pcbStrongNameToken)
		{
			int num = StrongNameHelpers.StrongName.StrongNameTokenFromPublicKey(bPublicKeyBlob, cbPublicKeyBlob, out ppbStrongNameToken, out pcbStrongNameToken);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				ppbStrongNameToken = IntPtr.Zero;
				pcbStrongNameToken = 0;
				return false;
			}
			return true;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000033B0 File Offset: 0x000015B0
		[SecurityCritical]
		public static bool StrongNameGetPublicKey(string pwzKeyContainer, byte[] bKeyBlob, int cbKeyBlob, out IntPtr ppbPublicKeyBlob, out int pcbPublicKeyBlob)
		{
			int num = StrongNameHelpers.StrongName.StrongNameGetPublicKey(pwzKeyContainer, bKeyBlob, cbKeyBlob, out ppbPublicKeyBlob, out pcbPublicKeyBlob);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				ppbPublicKeyBlob = IntPtr.Zero;
				pcbPublicKeyBlob = 0;
				return false;
			}
			return true;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000033E8 File Offset: 0x000015E8
		[SecurityCritical]
		public static bool StrongNameKeyInstall(string pwzKeyContainer, byte[] bKeyBlob, int cbKeyBlob)
		{
			int num = StrongNameHelpers.StrongName.StrongNameKeyInstall(pwzKeyContainer, bKeyBlob, cbKeyBlob);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				return false;
			}
			return true;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003410 File Offset: 0x00001610
		[SecurityCritical]
		public static bool StrongNameSignatureGeneration(string pwzFilePath, string pwzKeyContainer, byte[] bKeyBlob, int cbKeyBlob)
		{
			IntPtr zero = IntPtr.Zero;
			int num = 0;
			return StrongNameHelpers.StrongNameSignatureGeneration(pwzFilePath, pwzKeyContainer, bKeyBlob, cbKeyBlob, ref zero, out num);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003434 File Offset: 0x00001634
		[SecurityCritical]
		public static bool StrongNameSignatureGeneration(string pwzFilePath, string pwzKeyContainer, byte[] bKeyBlob, int cbKeyBlob, ref IntPtr ppbSignatureBlob, out int pcbSignatureBlob)
		{
			int num = StrongNameHelpers.StrongName.StrongNameSignatureGeneration(pwzFilePath, pwzKeyContainer, bKeyBlob, cbKeyBlob, ppbSignatureBlob, out pcbSignatureBlob);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				pcbSignatureBlob = 0;
				return false;
			}
			return true;
		}

		// Token: 0x04000067 RID: 103
		[ThreadStatic]
		private static int ts_LastStrongNameHR;

		// Token: 0x04000068 RID: 104
		[SecurityCritical]
		[ThreadStatic]
		private static IClrStrongName s_StrongName;
	}
}
