using System;

namespace System.Web.Configuration
{
	// Token: 0x0200070D RID: 1805
	internal sealed class ImpersonateTokenRef : IDisposable
	{
		// Token: 0x06005708 RID: 22280 RVA: 0x00130387 File Offset: 0x0012E587
		internal ImpersonateTokenRef(IntPtr token)
		{
			this._handle = token;
		}

		// Token: 0x17001929 RID: 6441
		// (get) Token: 0x06005709 RID: 22281 RVA: 0x00130396 File Offset: 0x0012E596
		internal IntPtr Handle
		{
			get
			{
				return this._handle;
			}
		}

		// Token: 0x0600570A RID: 22282 RVA: 0x001303A0 File Offset: 0x0012E5A0
		~ImpersonateTokenRef()
		{
			if (this._handle != IntPtr.Zero)
			{
				UnsafeNativeMethods.CloseHandle(this._handle);
				this._handle = IntPtr.Zero;
			}
		}

		// Token: 0x0600570B RID: 22283 RVA: 0x001303F0 File Offset: 0x0012E5F0
		void IDisposable.Dispose()
		{
			if (this._handle != IntPtr.Zero)
			{
				UnsafeNativeMethods.CloseHandle(this._handle);
				this._handle = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x04002E38 RID: 11832
		private IntPtr _handle;
	}
}
