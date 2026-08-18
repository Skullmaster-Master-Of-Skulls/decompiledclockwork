using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020001FA RID: 506
	internal abstract class SafeFreeCredentials : SafeHandle
	{
		// Token: 0x0600132D RID: 4909 RVA: 0x0006499F File Offset: 0x00062B9F
		protected SafeFreeCredentials() : base(IntPtr.Zero, true)
		{
			this._handle = default(SSPIHandle);
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x000649B9 File Offset: 0x00062BB9
		public override bool IsInvalid
		{
			get
			{
				return base.IsClosed || this._handle.IsZero;
			}
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x000649D0 File Offset: 0x00062BD0
		public static int AcquireCredentialsHandle(SecurDll dll, string package, CredentialUse intent, ref AuthIdentity authdata, out SafeFreeCredentials outCredential)
		{
			int num = -1;
			if (dll == SecurDll.SECURITY)
			{
				outCredential = new SafeFreeCredential_SECURITY();
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					goto IL_52;
				}
				finally
				{
					long num2;
					num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.AcquireCredentialsHandleW(null, package, (int)intent, null, ref authdata, null, null, ref outCredential._handle, out num2);
				}
				goto IL_2F;
				IL_52:
				if (num != 0)
				{
					outCredential.SetHandleAsInvalid();
				}
				return num;
			}
			IL_2F:
			throw new ArgumentException(SR.GetString("net_invalid_enum", new object[]
			{
				"SecurDll"
			}), "Dll");
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00064A4C File Offset: 0x00062C4C
		public static int AcquireDefaultCredential(SecurDll dll, string package, CredentialUse intent, out SafeFreeCredentials outCredential)
		{
			int num = -1;
			if (dll == SecurDll.SECURITY)
			{
				outCredential = new SafeFreeCredential_SECURITY();
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					goto IL_54;
				}
				finally
				{
					long num2;
					num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.AcquireCredentialsHandleW(null, package, (int)intent, null, IntPtr.Zero, null, null, ref outCredential._handle, out num2);
				}
				goto IL_31;
				IL_54:
				if (num != 0)
				{
					outCredential.SetHandleAsInvalid();
				}
				return num;
			}
			IL_31:
			throw new ArgumentException(SR.GetString("net_invalid_enum", new object[]
			{
				"SecurDll"
			}), "Dll");
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00064AC8 File Offset: 0x00062CC8
		public static int AcquireCredentialsHandle(string package, CredentialUse intent, ref SafeSspiAuthDataHandle authdata, out SafeFreeCredentials outCredential)
		{
			int num = -1;
			outCredential = new SafeFreeCredential_SECURITY();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				long num2;
				num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.AcquireCredentialsHandleW(null, package, (int)intent, null, authdata, null, null, ref outCredential._handle, out num2);
			}
			if (num != 0)
			{
				outCredential.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x00064B1C File Offset: 0x00062D1C
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
				if (dll == SecurDll.SECURITY)
				{
					outCredential = new SafeFreeCredential_SECURITY();
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						goto IL_7F;
					}
					finally
					{
						long num2;
						num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.AcquireCredentialsHandleW(null, package, (int)intent, null, ref authdata, null, null, ref outCredential._handle, out num2);
					}
				}
				throw new ArgumentException(SR.GetString("net_invalid_enum", new object[]
				{
					"SecurDll"
				}), "Dll");
			}
			finally
			{
				authdata.certContextArray = certContextArray;
			}
			IL_7F:
			if (num != 0)
			{
				outCredential.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00064BD0 File Offset: 0x00062DD0
		public static int AcquireCredentialsHandle(SecurDll dll, string package, CredentialUse intent, ref SecureCredential2 authdata, out SafeFreeCredentials outCredential)
		{
			int num = -1;
			if (dll == SecurDll.SECURITY)
			{
				outCredential = new SafeFreeCredential_SECURITY();
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					goto IL_52;
				}
				finally
				{
					long num2;
					num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.AcquireCredentialsHandleW(null, package, (int)intent, null, ref authdata, null, null, ref outCredential._handle, out num2);
				}
				goto IL_2F;
				IL_52:
				if (num != 0)
				{
					outCredential.SetHandleAsInvalid();
				}
				return num;
			}
			IL_2F:
			throw new ArgumentException(SR.GetString("net_invalid_enum", new object[]
			{
				"SecurDll"
			}), "Dll");
		}

		// Token: 0x04001554 RID: 5460
		internal SSPIHandle _handle;
	}
}
