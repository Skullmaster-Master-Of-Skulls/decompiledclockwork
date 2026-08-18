using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x0200051E RID: 1310
	internal abstract class SafeFreeCredentials : SafeHandle
	{
		// Token: 0x06002854 RID: 10324 RVA: 0x000A5F8B File Offset: 0x000A4F8B
		protected SafeFreeCredentials() : base(IntPtr.Zero, true)
		{
			this._handle = default(SSPIHandle);
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06002855 RID: 10325 RVA: 0x000A5FA5 File Offset: 0x000A4FA5
		public override bool IsInvalid
		{
			get
			{
				return base.IsClosed || this._handle.IsZero;
			}
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x000A5FBC File Offset: 0x000A4FBC
		public static int AcquireCredentialsHandle(SecurDll dll, string package, CredentialUse intent, ref AuthIdentity authdata, out SafeFreeCredentials outCredential)
		{
			int num = -1;
			switch (dll)
			{
			case SecurDll.SECURITY:
				outCredential = new SafeFreeCredential_SECURITY();
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					goto IL_8D;
				}
				finally
				{
					long num2;
					num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.AcquireCredentialsHandleW(null, package, (int)intent, null, ref authdata, null, null, ref outCredential._handle, out num2);
				}
				break;
			case SecurDll.SECUR32:
				break;
			default:
				goto IL_68;
			}
			outCredential = new SafeFreeCredential_SECUR32();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				goto IL_8D;
			}
			finally
			{
				long num2;
				num = UnsafeNclNativeMethods.SafeNetHandles_SECUR32.AcquireCredentialsHandleA(null, package, (int)intent, null, ref authdata, null, null, ref outCredential._handle, out num2);
			}
			IL_68:
			throw new ArgumentException(SR.GetString("net_invalid_enum", new object[]
			{
				"SecurDll"
			}), "Dll");
			IL_8D:
			if (num != 0)
			{
				outCredential.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x000A6080 File Offset: 0x000A5080
		public static int AcquireDefaultCredential(SecurDll dll, string package, CredentialUse intent, out SafeFreeCredentials outCredential)
		{
			int num = -1;
			switch (dll)
			{
			case SecurDll.SECURITY:
				outCredential = new SafeFreeCredential_SECURITY();
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					goto IL_91;
				}
				finally
				{
					long num2;
					num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.AcquireCredentialsHandleW(null, package, (int)intent, null, IntPtr.Zero, null, null, ref outCredential._handle, out num2);
				}
				break;
			case SecurDll.SECUR32:
				break;
			default:
				goto IL_6C;
			}
			outCredential = new SafeFreeCredential_SECUR32();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				goto IL_91;
			}
			finally
			{
				long num2;
				num = UnsafeNclNativeMethods.SafeNetHandles_SECUR32.AcquireCredentialsHandleA(null, package, (int)intent, null, IntPtr.Zero, null, null, ref outCredential._handle, out num2);
			}
			IL_6C:
			throw new ArgumentException(SR.GetString("net_invalid_enum", new object[]
			{
				"SecurDll"
			}), "Dll");
			IL_91:
			if (num != 0)
			{
				outCredential.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x000A6148 File Offset: 0x000A5148
		public unsafe static int AcquireCredentialsHandle(SecurDll dll, string package, CredentialUse intent, ref SecureCredential authdata, out SafeFreeCredentials outCredential)
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
				switch (dll)
				{
				case SecurDll.SECURITY:
					outCredential = new SafeFreeCredential_SECURITY();
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						goto IL_BB;
					}
					finally
					{
						long num2;
						num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.AcquireCredentialsHandleW(null, package, (int)intent, null, ref authdata, null, null, ref outCredential._handle, out num2);
					}
					break;
				case SecurDll.SECUR32:
					goto IL_93;
				case SecurDll.SCHANNEL:
					break;
				default:
					goto IL_93;
				}
				outCredential = new SafeFreeCredential_SCHANNEL();
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					goto IL_BB;
				}
				finally
				{
					long num2;
					num = UnsafeNclNativeMethods.SafeNetHandles_SCHANNEL.AcquireCredentialsHandleA(null, package, (int)intent, null, ref authdata, null, null, ref outCredential._handle, out num2);
				}
				IL_93:
				throw new ArgumentException(SR.GetString("net_invalid_enum", new object[]
				{
					"SecurDll"
				}), "Dll");
				IL_BB:;
			}
			finally
			{
				authdata.certContextArray = certContextArray;
			}
			if (num != 0)
			{
				outCredential.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x04002784 RID: 10116
		internal SSPIHandle _handle;
	}
}
