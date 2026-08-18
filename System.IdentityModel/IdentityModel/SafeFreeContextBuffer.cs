using System;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.ServiceModel.Diagnostics;
using Microsoft.Win32.SafeHandles;

namespace System.IdentityModel
{
	// Token: 0x02000098 RID: 152
	internal sealed class SafeFreeContextBuffer : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x00006319 File Offset: 0x00004519
		private SafeFreeContextBuffer() : base(true)
		{
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00012ED4 File Offset: 0x000110D4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void Set(IntPtr value)
		{
			this.handle = value;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00012EEC File Offset: 0x000110EC
		internal static int EnumeratePackages(out int pkgnum, out SafeFreeContextBuffer pkgArray)
		{
			int num = SafeFreeContextBuffer.EnumerateSecurityPackagesW(out pkgnum, out pkgArray);
			if (num != 0)
			{
				Utility.CloseInvalidOutSafeHandle(pkgArray);
				pkgArray = null;
			}
			return num;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00012F11 File Offset: 0x00011111
		internal static SafeFreeContextBuffer CreateEmptyHandle()
		{
			return new SafeFreeContextBuffer();
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00012F18 File Offset: 0x00011118
		public unsafe static int QueryContextAttributes(SafeDeleteContext phContext, ContextAttribute contextAttribute, byte* buffer, SafeHandle refHandle)
		{
			int num = -2146893055;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				phContext.DangerousAddRef(ref flag);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (flag)
				{
					phContext.DangerousRelease();
					flag = false;
				}
				if (!(ex is ObjectDisposedException))
				{
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					num = SafeFreeContextBuffer.QueryContextAttributesW(ref phContext._handle, contextAttribute, (void*)buffer);
					phContext.DangerousRelease();
				}
				if (num == 0 && refHandle != null)
				{
					if (refHandle is SafeFreeContextBuffer)
					{
						if (contextAttribute == ContextAttribute.SessionKey)
						{
							IntPtr value = Marshal.ReadIntPtr(new IntPtr((void*)buffer), SecPkgContext_SessionKey.SessionkeyOffset);
							((SafeFreeContextBuffer)refHandle).Set(value);
						}
						else
						{
							((SafeFreeContextBuffer)refHandle).Set(*(IntPtr*)buffer);
						}
					}
					else
					{
						((SafeFreeCertContext)refHandle).Set(*(IntPtr*)buffer);
					}
				}
				if (num != 0 && refHandle != null)
				{
					refHandle.SetHandleAsInvalid();
				}
			}
			return num;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00012FF0 File Offset: 0x000111F0
		protected override bool ReleaseHandle()
		{
			return SafeFreeContextBuffer.FreeContextBuffer(this.handle) == 0;
		}

		// Token: 0x06000505 RID: 1285
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("security.dll", ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern int QueryContextAttributesW(ref SSPIHandle contextHandle, [In] ContextAttribute attribute, [In] void* buffer);

		// Token: 0x06000506 RID: 1286
		[DllImport("security.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern int EnumerateSecurityPackagesW(out int pkgnum, out SafeFreeContextBuffer handle);

		// Token: 0x06000507 RID: 1287
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("security.dll", ExactSpelling = true, SetLastError = true)]
		private static extern int FreeContextBuffer([In] IntPtr contextBuffer);

		// Token: 0x0400045E RID: 1118
		private const string SECURITY = "security.dll";
	}
}
