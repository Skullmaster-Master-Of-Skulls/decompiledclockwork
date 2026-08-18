using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x02000206 RID: 518
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeRegistryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600136C RID: 4972 RVA: 0x00066268 File Offset: 0x00064468
		private SafeRegistryHandle() : base(true)
		{
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00066271 File Offset: 0x00064471
		internal static uint RegOpenKeyEx(IntPtr key, string subKey, uint ulOptions, uint samDesired, out SafeRegistryHandle resultSubKey)
		{
			return UnsafeNclNativeMethods.RegistryHelper.RegOpenKeyEx(key, subKey, ulOptions, samDesired, out resultSubKey);
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0006627E File Offset: 0x0006447E
		internal uint RegOpenKeyEx(string subKey, uint ulOptions, uint samDesired, out SafeRegistryHandle resultSubKey)
		{
			return UnsafeNclNativeMethods.RegistryHelper.RegOpenKeyEx(this, subKey, ulOptions, samDesired, out resultSubKey);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0006628B File Offset: 0x0006448B
		internal uint RegCloseKey()
		{
			base.Close();
			return this.resClose;
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0006629C File Offset: 0x0006449C
		internal uint QueryValue(string name, out object data)
		{
			data = null;
			byte[] array = null;
			uint num = 0U;
			uint num3;
			uint num2;
			for (;;)
			{
				num2 = UnsafeNclNativeMethods.RegistryHelper.RegQueryValueEx(this, name, IntPtr.Zero, out num3, array, ref num);
				if (num2 != 234U && (array != null || num2 != 0U))
				{
					break;
				}
				array = new byte[num];
			}
			if (num2 != 0U)
			{
				return num2;
			}
			if (num3 == 3U)
			{
				if ((ulong)num != (ulong)((long)array.Length))
				{
					byte[] src = array;
					array = new byte[num];
					Buffer.BlockCopy(src, 0, array, 0, (int)num);
				}
				data = array;
				return 0U;
			}
			return 50U;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00066306 File Offset: 0x00064506
		internal uint RegNotifyChangeKeyValue(bool watchSubTree, uint notifyFilter, SafeWaitHandle regEvent, bool async)
		{
			return UnsafeNclNativeMethods.RegistryHelper.RegNotifyChangeKeyValue(this, watchSubTree, notifyFilter, regEvent, async);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00066313 File Offset: 0x00064513
		internal static uint RegOpenCurrentUser(uint samDesired, out SafeRegistryHandle resultKey)
		{
			return UnsafeNclNativeMethods.RegistryHelper.RegOpenCurrentUser(samDesired, out resultKey);
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0006631C File Offset: 0x0006451C
		protected override bool ReleaseHandle()
		{
			if (!this.IsInvalid)
			{
				this.resClose = UnsafeNclNativeMethods.RegistryHelper.RegCloseKey(this.handle);
			}
			base.SetHandleAsInvalid();
			return true;
		}

		// Token: 0x04001562 RID: 5474
		private uint resClose;
	}
}
