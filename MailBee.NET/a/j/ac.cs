using System;
using System.Runtime.InteropServices;
using System.Security;

namespace a.j
{
	// Token: 0x020001D3 RID: 467
	internal class ac
	{
		// Token: 0x06000F15 RID: 3861 RVA: 0x000389C9 File Offset: 0x000379C9
		private ac()
		{
		}

		// Token: 0x06000F16 RID: 3862
		[SecuritySafeCritical]
		[DllImport("secur32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int AcquireCredentialsHandle(string A_0, string A_1, int A_2, IntPtr A_3, ref f A_4, IntPtr A_5, IntPtr A_6, ref v A_7, ref b A_8);

		// Token: 0x06000F17 RID: 3863
		[SecuritySafeCritical]
		[DllImport("secur32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int AcquireCredentialsHandle(string A_0, string A_1, int A_2, IntPtr A_3, IntPtr A_4, IntPtr A_5, IntPtr A_6, ref v A_7, ref b A_8);

		// Token: 0x06000F18 RID: 3864
		[SecuritySafeCritical]
		[DllImport("secur32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int ApplyControlToken(ref v A_0, ref an A_1);

		// Token: 0x06000F19 RID: 3865
		[SecuritySafeCritical]
		[DllImport("secur32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int InitializeSecurityContext(ref v A_0, IntPtr A_1, IntPtr A_2, int A_3, int A_4, int A_5, IntPtr A_6, int A_7, out v A_8, out an A_9, out uint A_10, out b A_11);

		// Token: 0x06000F1A RID: 3866
		[SecuritySafeCritical]
		[DllImport("secur32", CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern int InitializeSecurityContext(ref v A_0, IntPtr A_1, string A_2, int A_3, int A_4, int A_5, IntPtr A_6, int A_7, out v A_8, out an A_9, out uint A_10, out b A_11);

		// Token: 0x06000F1B RID: 3867
		[DllImport("secur32", CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern int InitializeSecurityContext(ref v A_0, ref v A_1, string A_2, int A_3, int A_4, int A_5, ref an A_6, int A_7, IntPtr A_8, ref an A_9, out uint A_10, out b A_11);

		// Token: 0x06000F1C RID: 3868
		[SecuritySafeCritical]
		[DllImport("secur32", CharSet = CharSet.Unicode)]
		public static extern int QueryContextAttributes(ref v A_0, uint A_1, out p A_2);

		// Token: 0x06000F1D RID: 3869
		[SecuritySafeCritical]
		[DllImport("secur32", CharSet = CharSet.Unicode)]
		public static extern int QueryContextAttributes(ref v A_0, uint A_1, out w A_2);

		// Token: 0x06000F1E RID: 3870
		[SecuritySafeCritical]
		[DllImport("secur32", CharSet = CharSet.Unicode)]
		public static extern int QueryContextAttributes(ref v A_0, uint A_1, ref IntPtr A_2);

		// Token: 0x06000F1F RID: 3871
		[SecuritySafeCritical]
		[DllImport("secur32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int FreeContextBuffer(IntPtr A_0);

		// Token: 0x06000F20 RID: 3872
		[SecuritySafeCritical]
		[DllImport("secur32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int DeleteSecurityContext(ref v A_0);

		// Token: 0x06000F21 RID: 3873
		[SecuritySafeCritical]
		[DllImport("secur32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int FreeCredentialsHandle(ref v A_0);

		// Token: 0x06000F22 RID: 3874
		[SecuritySafeCritical]
		[DllImport("secur32", SetLastError = true)]
		public static extern int EncryptMessage(ref v A_0, uint A_1, ref an A_2, uint A_3);

		// Token: 0x06000F23 RID: 3875
		[SecuritySafeCritical]
		[DllImport("secur32", SetLastError = true)]
		public static extern int DecryptMessage(ref v A_0, ref an A_1, uint A_2, IntPtr A_3);

		// Token: 0x06000F24 RID: 3876
		[DllImport("secur32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int QueryCredentialsAttributes(ref v A_0, int A_1, ref j A_2);
	}
}
