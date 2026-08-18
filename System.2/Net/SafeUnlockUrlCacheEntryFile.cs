using System;
using System.Net.Cache;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x02000205 RID: 517
	internal sealed class SafeUnlockUrlCacheEntryFile : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001368 RID: 4968 RVA: 0x00066184 File Offset: 0x00064384
		private SafeUnlockUrlCacheEntryFile(string keyString) : base(true)
		{
			this.m_KeyString = keyString;
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x00066194 File Offset: 0x00064394
		protected unsafe override bool ReleaseHandle()
		{
			fixed (string keyString = this.m_KeyString)
			{
				char* ptr = keyString;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				UnsafeNclNativeMethods.SafeNetHandles.UnlockUrlCacheEntryFileW(ptr, 0);
			}
			base.SetHandle(IntPtr.Zero);
			this.m_KeyString = null;
			return true;
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x000661D4 File Offset: 0x000643D4
		internal unsafe static _WinInetCache.Status GetAndLockFile(string key, byte* entryPtr, ref int entryBufSize, out SafeUnlockUrlCacheEntryFile handle)
		{
			if (ValidationHelper.IsBlankString(key))
			{
				throw new ArgumentNullException("key");
			}
			handle = new SafeUnlockUrlCacheEntryFile(key);
			char* ptr = key;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return SafeUnlockUrlCacheEntryFile.MustRunGetAndLockFile(ptr, entryPtr, ref entryBufSize, handle);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00066218 File Offset: 0x00064418
		private unsafe static _WinInetCache.Status MustRunGetAndLockFile(char* key, byte* entryPtr, ref int entryBufSize, SafeUnlockUrlCacheEntryFile handle)
		{
			_WinInetCache.Status result = _WinInetCache.Status.Success;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				if (!UnsafeNclNativeMethods.SafeNetHandles.RetrieveUrlCacheEntryFileW(key, entryPtr, ref entryBufSize, 0))
				{
					result = (_WinInetCache.Status)Marshal.GetLastWin32Error();
					handle.SetHandleAsInvalid();
				}
				else
				{
					handle.SetHandle((IntPtr)1);
				}
			}
			return result;
		}

		// Token: 0x04001561 RID: 5473
		private string m_KeyString;
	}
}
