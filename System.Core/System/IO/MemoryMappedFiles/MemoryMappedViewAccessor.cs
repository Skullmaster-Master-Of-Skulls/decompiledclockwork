using System;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.IO.MemoryMappedFiles
{
	// Token: 0x020000A6 RID: 166
	public sealed class MemoryMappedViewAccessor : UnmanagedMemoryAccessor
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x0000DB64 File Offset: 0x0000BD64
		[SecurityCritical]
		internal MemoryMappedViewAccessor(MemoryMappedView view)
		{
			this.m_view = view;
			base.Initialize(this.m_view.ViewHandle, this.m_view.PointerOffset, this.m_view.Size, MemoryMappedFile.GetFileAccess(this.m_view.Access));
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0000DBB5 File Offset: 0x0000BDB5
		public SafeMemoryMappedViewHandle SafeMemoryMappedViewHandle
		{
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				if (this.m_view == null)
				{
					return null;
				}
				return this.m_view.ViewHandle;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0000DBCC File Offset: 0x0000BDCC
		public long PointerOffset
		{
			get
			{
				if (this.m_view == null)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_ViewIsNull"));
				}
				return this.m_view.PointerOffset;
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000DBF4 File Offset: 0x0000BDF4
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.m_view != null && !this.m_view.IsClosed)
				{
					this.Flush();
				}
			}
			finally
			{
				try
				{
					if (this.m_view != null)
					{
						this.m_view.Dispose();
					}
				}
				finally
				{
					base.Dispose(disposing);
				}
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000DC5C File Offset: 0x0000BE5C
		[SecurityCritical]
		public void Flush()
		{
			if (!base.IsOpen)
			{
				throw new ObjectDisposedException("MemoryMappedViewAccessor", SR.GetString("ObjectDisposed_ViewAccessorClosed"));
			}
			if (this.m_view != null)
			{
				this.m_view.Flush((IntPtr)base.Capacity);
			}
		}

		// Token: 0x04000529 RID: 1321
		private MemoryMappedView m_view;
	}
}
