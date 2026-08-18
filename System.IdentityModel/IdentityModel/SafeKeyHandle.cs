using System;
using System.ComponentModel;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.ServiceModel.Diagnostics;
using Microsoft.Win32.SafeHandles;

namespace System.IdentityModel
{
	// Token: 0x02000070 RID: 112
	internal class SafeKeyHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000363 RID: 867 RVA: 0x00006319 File Offset: 0x00004519
		private SafeKeyHandle() : base(true)
		{
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00006322 File Offset: 0x00004522
		private SafeKeyHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000D5BC File Offset: 0x0000B7BC
		internal static SafeKeyHandle InvalidHandle
		{
			get
			{
				return new SafeKeyHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000D5C8 File Offset: 0x0000B7C8
		protected override bool ReleaseHandle()
		{
			bool result = NativeMethods.CryptDestroyKey(this.handle);
			if (this.provHandle != null)
			{
				this.provHandle.DangerousRelease();
				this.provHandle = null;
			}
			return result;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000D5FC File Offset: 0x0000B7FC
		internal unsafe static SafeKeyHandle SafeCryptImportKey(SafeProvHandle provHandle, void* pbDataPtr, int cbData)
		{
			bool flag = false;
			int num = 0;
			SafeKeyHandle safeKeyHandle = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				provHandle.DangerousAddRef(ref flag);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (flag)
				{
					provHandle.DangerousRelease();
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
					flag = NativeMethods.CryptImportKey(provHandle, pbDataPtr, (uint)cbData, IntPtr.Zero, 0U, out safeKeyHandle);
					if (!flag)
					{
						num = Marshal.GetLastWin32Error();
						provHandle.DangerousRelease();
					}
					else
					{
						safeKeyHandle.provHandle = provHandle;
					}
				}
			}
			if (!flag)
			{
				Utility.CloseInvalidOutSafeHandle(safeKeyHandle);
				string text = (num != 0) ? new Win32Exception(num).Message : string.Empty;
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("AESCryptImportKeyFailed", new object[]
				{
					text
				})));
			}
			return safeKeyHandle;
		}

		// Token: 0x04000364 RID: 868
		private SafeProvHandle provHandle;
	}
}
