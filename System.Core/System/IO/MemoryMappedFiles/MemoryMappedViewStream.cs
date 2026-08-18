using System;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.IO.MemoryMappedFiles
{
	// Token: 0x020000A7 RID: 167
	public sealed class MemoryMappedViewStream : UnmanagedMemoryStream
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x0000DC9C File Offset: 0x0000BE9C
		[SecurityCritical]
		internal MemoryMappedViewStream(MemoryMappedView view)
		{
			this.m_view = view;
			base.Initialize(this.m_view.ViewHandle, this.m_view.PointerOffset, this.m_view.Size, MemoryMappedFile.GetFileAccess(this.m_view.Access));
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0000DCED File Offset: 0x0000BEED
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

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000DD04 File Offset: 0x0000BF04
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("NotSupported_MMViewStreamsFixedLength"));
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000DD15 File Offset: 0x0000BF15
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

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000DD3C File Offset: 0x0000BF3C
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

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000DDA4 File Offset: 0x0000BFA4
		[SecurityCritical]
		public override void Flush()
		{
			if (!this.CanSeek)
			{
				__Error.StreamIsClosed();
			}
			if (this.m_view != null)
			{
				this.m_view.Flush((IntPtr)base.Capacity);
			}
		}

		// Token: 0x0400052A RID: 1322
		private MemoryMappedView m_view;
	}
}
