using System;
using System.Net.Cache;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x02000530 RID: 1328
	internal sealed class SafeUnlockUrlCacheEntryFile : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060028A5 RID: 10405 RVA: 0x000A8014 File Offset: 0x000A7014
		private SafeUnlockUrlCacheEntryFile(string keyString) : base(true)
		{
			this.m_KeyString = keyString;
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x000A8024 File Offset: 0x000A7024
		protected unsafe override bool ReleaseHandle()
		{
			fixed (char* keyString = this.m_KeyString)
			{
				UnsafeNclNativeMethods.SafeNetHandles.UnlockUrlCacheEntryFileW(keyString, 0);
			}
			base.SetHandle(IntPtr.Zero);
			this.m_KeyString = null;
			return true;
		}

		// Token: 0x060028A7 RID: 10407 RVA: 0x000A8064 File Offset: 0x000A7064
		internal unsafe static _WinInetCache.Status GetAndLockFile(string key, byte* entryPtr, ref int entryBufSize, out SafeUnlockUrlCacheEntryFile handle)
		{
			if (ValidationHelper.IsBlankString(key))
			{
				throw new ArgumentNullException("key");
			}
			handle = new SafeUnlockUrlCacheEntryFile(key);
			IntPtr intPtr2;
			IntPtr intPtr = intPtr2 = key;
			if (intPtr != 0)
			{
				intPtr2 = (IntPtr)((int)intPtr + RuntimeHelpers.OffsetToStringData);
			}
			char* key2 = intPtr2;
			return SafeUnlockUrlCacheEntryFile.MustRunGetAndLockFile(key2, entryPtr, ref entryBufSize, handle);
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x000A80A8 File Offset: 0x000A70A8
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

		// Token: 0x04002799 RID: 10137
		private string m_KeyString;
	}
}
