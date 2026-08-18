using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.IdentityModel
{
	// Token: 0x02000096 RID: 150
	internal class SafeFreeCredentials : SafeHandle
	{
		// Token: 0x060004F0 RID: 1264 RVA: 0x00012D08 File Offset: 0x00010F08
		protected SafeFreeCredentials() : base(IntPtr.Zero, true)
		{
			this._handle = default(SSPIHandle);
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00012D22 File Offset: 0x00010F22
		public override bool IsInvalid
		{
			get
			{
				return base.IsClosed || this._handle.IsZero;
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00012D3C File Offset: 0x00010F3C
		public static int AcquireCredentialsHandle(string package, CredentialUse intent, ref AuthIdentityEx authdata, out SafeFreeCredentials outCredential)
		{
			int num = -1;
			outCredential = new SafeFreeCredentials();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				long num2;
				num = SafeFreeCredentials.AcquireCredentialsHandleW(null, package, (int)intent, null, ref authdata, null, null, ref outCredential._handle, out num2);
				if (num != 0)
				{
					outCredential.SetHandleAsInvalid();
				}
			}
			return num;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00012D90 File Offset: 0x00010F90
		public static int AcquireDefaultCredential(string package, CredentialUse intent, ref AuthIdentityEx authIdentity, out SafeFreeCredentials outCredential)
		{
			int num = -1;
			outCredential = new SafeFreeCredentials();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				long num2;
				num = SafeFreeCredentials.AcquireCredentialsHandleW(null, package, (int)intent, null, ref authIdentity, null, null, ref outCredential._handle, out num2);
				if (num != 0)
				{
					outCredential.SetHandleAsInvalid();
				}
			}
			return num;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00012DE4 File Offset: 0x00010FE4
		public unsafe static int AcquireCredentialsHandle(string package, CredentialUse intent, ref SecureCredential authdata, out SafeFreeCredentials outCredential)
		{
			int num = -1;
			IntPtr certContextArray = authdata.certContextArray;
			try
			{
				IntPtr certContextArray2 = new IntPtr((void*)(&certContextArray));
				if (certContextArray != IntPtr.Zero)
				{
					authdata.certContextArray = certContextArray2;
				}
				outCredential = new SafeFreeCredentials();
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					long num2;
					num = SafeFreeCredentials.AcquireCredentialsHandleW(null, package, (int)intent, null, ref authdata, null, null, ref outCredential._handle, out num2);
					if (num != 0)
					{
						outCredential.SetHandleAsInvalid();
					}
				}
			}
			finally
			{
				authdata.certContextArray = certContextArray;
			}
			return num;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00012E70 File Offset: 0x00011070
		public static int AcquireCredentialsHandle(string package, CredentialUse intent, ref IntPtr ppAuthIdentity, out SafeFreeCredentials outCredential)
		{
			int num = -1;
			outCredential = new SafeFreeCredentials();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				long num2;
				num = SafeFreeCredentials.AcquireCredentialsHandleW(null, package, (int)intent, null, ppAuthIdentity, null, null, ref outCredential._handle, out num2);
				if (num != 0)
				{
					outCredential.SetHandleAsInvalid();
				}
			}
			return num;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00012EC4 File Offset: 0x000110C4
		protected override bool ReleaseHandle()
		{
			return SafeFreeCredentials.FreeCredentialsHandle(ref this._handle) == 0;
		}

		// Token: 0x060004F7 RID: 1271
		[DllImport("security.Dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern int AcquireCredentialsHandleW([In] string principal, [In] string moduleName, [In] int usage, [In] void* logonID, [In] ref AuthIdentityEx authdata, [In] void* keyCallback, [In] void* keyArgument, ref SSPIHandle handlePtr, out long timeStamp);

		// Token: 0x060004F8 RID: 1272
		[DllImport("security.Dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern int AcquireCredentialsHandleW([In] string principal, [In] string moduleName, [In] int usage, [In] void* logonID, [In] IntPtr zero, [In] void* keyCallback, [In] void* keyArgument, ref SSPIHandle handlePtr, out long timeStamp);

		// Token: 0x060004F9 RID: 1273
		[DllImport("security.Dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern int AcquireCredentialsHandleW([In] string principal, [In] string moduleName, [In] int usage, [In] void* logonID, [In] ref SecureCredential authData, [In] void* keyCallback, [In] void* keyArgument, ref SSPIHandle handlePtr, out long timeStamp);

		// Token: 0x060004FA RID: 1274
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("security.Dll", ExactSpelling = true, SetLastError = true)]
		internal static extern int FreeCredentialsHandle(ref SSPIHandle handlePtr);

		// Token: 0x04000459 RID: 1113
		private const string SECURITY = "security.Dll";

		// Token: 0x0400045A RID: 1114
		internal SSPIHandle _handle;
	}
}
