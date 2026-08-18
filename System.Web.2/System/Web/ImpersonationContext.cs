using System;
using System.Runtime.InteropServices;

namespace System.Web
{
	// Token: 0x020000D3 RID: 211
	internal class ImpersonationContext : IDisposable
	{
		// Token: 0x06000DF3 RID: 3571 RVA: 0x000030B5 File Offset: 0x000012B5
		internal ImpersonationContext()
		{
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0002780E File Offset: 0x00025A0E
		internal ImpersonationContext(IntPtr token)
		{
			this.ImpersonateToken(new HandleRef(this, token));
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00027824 File Offset: 0x00025A24
		~ImpersonationContext()
		{
			this.Dispose(false);
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00027854 File Offset: 0x00025A54
		void IDisposable.Dispose()
		{
			this.Undo();
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0002785C File Offset: 0x00025A5C
		private void Dispose(bool disposing)
		{
			if (this._savedToken.Handle != IntPtr.Zero)
			{
				try
				{
				}
				finally
				{
					UnsafeNativeMethods.CloseHandle(this._savedToken.Handle);
					this._savedToken = new HandleRef(this, IntPtr.Zero);
				}
			}
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x000278B8 File Offset: 0x00025AB8
		protected void ImpersonateToken(HandleRef token)
		{
			try
			{
				this._savedToken = new HandleRef(this, ImpersonationContext.GetCurrentToken());
				if (this._savedToken.Handle != IntPtr.Zero && UnsafeNativeMethods.RevertToSelf() != 0)
				{
					this._reverted = true;
				}
				if (token.Handle != IntPtr.Zero)
				{
					if (UnsafeNativeMethods.SetThreadToken(IntPtr.Zero, token.Handle) == 0)
					{
						throw new HttpException(SR.GetString("Cannot_impersonate"));
					}
					this._impersonating = true;
				}
			}
			catch
			{
				this.RestoreImpersonation();
				throw;
			}
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00027954 File Offset: 0x00025B54
		private void RestoreImpersonation()
		{
			if (this._impersonating)
			{
				UnsafeNativeMethods.RevertToSelf();
				this._impersonating = false;
			}
			if (this._savedToken.Handle != IntPtr.Zero)
			{
				if (this._reverted && UnsafeNativeMethods.SetThreadToken(IntPtr.Zero, this._savedToken.Handle) == 0)
				{
					throw new HttpException(SR.GetString("Cannot_impersonate"));
				}
				this._reverted = false;
			}
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x000279C3 File Offset: 0x00025BC3
		internal void Undo()
		{
			this.RestoreImpersonation();
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x000279D8 File Offset: 0x00025BD8
		private static IntPtr GetCurrentToken()
		{
			IntPtr zero = IntPtr.Zero;
			if (UnsafeNativeMethods.OpenThreadToken(UnsafeNativeMethods.GetCurrentThread(), 131084, true, ref zero) == 0 && Marshal.GetLastWin32Error() != 1008)
			{
				throw new HttpException(SR.GetString("Cannot_impersonate"));
			}
			return zero;
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x00027A1C File Offset: 0x00025C1C
		internal static bool CurrentThreadTokenExists
		{
			get
			{
				bool result = false;
				try
				{
				}
				finally
				{
					IntPtr currentToken = ImpersonationContext.GetCurrentToken();
					if (currentToken != IntPtr.Zero)
					{
						result = true;
						UnsafeNativeMethods.CloseHandle(currentToken);
					}
				}
				return result;
			}
		}

		// Token: 0x04000523 RID: 1315
		private HandleRef _savedToken;

		// Token: 0x04000524 RID: 1316
		private bool _reverted;

		// Token: 0x04000525 RID: 1317
		private bool _impersonating;
	}
}
