using System;
using System.IO;
using System.Web.Caching;
using System.Web.Util;

namespace System.Web.Security
{
	// Token: 0x020005DB RID: 1499
	internal class FileSecurityDescriptorWrapper : IDisposable
	{
		// Token: 0x06004BB7 RID: 19383 RVA: 0x00101F34 File Offset: 0x00100134
		~FileSecurityDescriptorWrapper()
		{
			this.FreeSecurityDescriptor();
		}

		// Token: 0x06004BB8 RID: 19384 RVA: 0x00101F60 File Offset: 0x00100160
		internal FileSecurityDescriptorWrapper(string strFile)
		{
			this._FileName = FileUtil.RemoveTrailingDirectoryBackSlash(strFile);
			this._securityDescriptor = UnsafeNativeMethods.GetFileSecurityDescriptor(this._FileName);
		}

		// Token: 0x06004BB9 RID: 19385 RVA: 0x00101F88 File Offset: 0x00100188
		internal bool IsAccessAllowed(IntPtr iToken, int iAccess)
		{
			if (iToken == IntPtr.Zero)
			{
				return true;
			}
			if (this._SecurityDescriptorBeingFreed)
			{
				return this.IsAccessAllowedUsingNewSecurityDescriptor(iToken, iAccess);
			}
			this._Lock.AcquireReaderLock();
			try
			{
				try
				{
					if (!this._SecurityDescriptorBeingFreed)
					{
						if (this._securityDescriptor == IntPtr.Zero)
						{
							return true;
						}
						if (this._securityDescriptor == UnsafeNativeMethods.INVALID_HANDLE_VALUE)
						{
							return false;
						}
						return UnsafeNativeMethods.IsAccessToFileAllowed(this._securityDescriptor, iToken, iAccess) != 0;
					}
				}
				finally
				{
					this._Lock.ReleaseReaderLock();
				}
			}
			catch
			{
				throw;
			}
			return this.IsAccessAllowedUsingNewSecurityDescriptor(iToken, iAccess);
		}

		// Token: 0x06004BBA RID: 19386 RVA: 0x00102040 File Offset: 0x00100240
		private bool IsAccessAllowedUsingNewSecurityDescriptor(IntPtr iToken, int iAccess)
		{
			if (iToken == IntPtr.Zero)
			{
				return true;
			}
			IntPtr fileSecurityDescriptor = UnsafeNativeMethods.GetFileSecurityDescriptor(this._FileName);
			if (fileSecurityDescriptor == IntPtr.Zero)
			{
				return true;
			}
			if (fileSecurityDescriptor == UnsafeNativeMethods.INVALID_HANDLE_VALUE)
			{
				return false;
			}
			bool result;
			try
			{
				try
				{
					result = (UnsafeNativeMethods.IsAccessToFileAllowed(fileSecurityDescriptor, iToken, iAccess) != 0);
				}
				finally
				{
					UnsafeNativeMethods.FreeFileSecurityDescriptor(fileSecurityDescriptor);
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004BBB RID: 19387 RVA: 0x001020BC File Offset: 0x001002BC
		internal void OnCacheItemRemoved(string key, object value, CacheItemRemovedReason reason)
		{
			this.FreeSecurityDescriptor();
		}

		// Token: 0x06004BBC RID: 19388 RVA: 0x001020C4 File Offset: 0x001002C4
		internal void FreeSecurityDescriptor()
		{
			if (!this.IsSecurityDescriptorValid())
			{
				return;
			}
			this._SecurityDescriptorBeingFreed = true;
			this._Lock.AcquireWriterLock();
			try
			{
				try
				{
					if (this.IsSecurityDescriptorValid())
					{
						IntPtr securityDescriptor = this._securityDescriptor;
						this._securityDescriptor = UnsafeNativeMethods.INVALID_HANDLE_VALUE;
						UnsafeNativeMethods.FreeFileSecurityDescriptor(securityDescriptor);
					}
				}
				finally
				{
					this._Lock.ReleaseWriterLock();
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x06004BBD RID: 19389 RVA: 0x00102140 File Offset: 0x00100340
		internal bool IsSecurityDescriptorValid()
		{
			return this._securityDescriptor != UnsafeNativeMethods.INVALID_HANDLE_VALUE && this._securityDescriptor != IntPtr.Zero;
		}

		// Token: 0x06004BBE RID: 19390 RVA: 0x00102168 File Offset: 0x00100368
		internal string GetCacheDependencyPath()
		{
			if (this._securityDescriptor == UnsafeNativeMethods.INVALID_HANDLE_VALUE)
			{
				return null;
			}
			if (this._securityDescriptor != IntPtr.Zero)
			{
				return this._FileName;
			}
			return FileUtil.GetFirstExistingDirectory(FileSecurityDescriptorWrapper.AppRoot, this._FileName);
		}

		// Token: 0x17001645 RID: 5701
		// (get) Token: 0x06004BBF RID: 19391 RVA: 0x001021B4 File Offset: 0x001003B4
		private static string AppRoot
		{
			get
			{
				string text = FileSecurityDescriptorWrapper._AppRoot;
				if (text == null)
				{
					InternalSecurityPermissions.AppPathDiscovery.Assert();
					text = Path.GetFullPath(HttpRuntime.AppDomainAppPathInternal);
					text = FileUtil.RemoveTrailingDirectoryBackSlash(text);
				}
				return text;
			}
		}

		// Token: 0x06004BC0 RID: 19392 RVA: 0x001021E7 File Offset: 0x001003E7
		void IDisposable.Dispose()
		{
			this.FreeSecurityDescriptor();
			GC.SuppressFinalize(this);
		}

		// Token: 0x040028C9 RID: 10441
		private IntPtr _securityDescriptor;

		// Token: 0x040028CA RID: 10442
		internal bool _AnonymousAccessChecked;

		// Token: 0x040028CB RID: 10443
		internal bool _AnonymousAccess;

		// Token: 0x040028CC RID: 10444
		private bool _SecurityDescriptorBeingFreed;

		// Token: 0x040028CD RID: 10445
		private string _FileName;

		// Token: 0x040028CE RID: 10446
		private ReadWriteSpinLock _Lock;

		// Token: 0x040028CF RID: 10447
		private static string _AppRoot;
	}
}
