using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x02000531 RID: 1329
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeRegistryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060028A9 RID: 10409 RVA: 0x000A80F8 File Offset: 0x000A70F8
		private SafeRegistryHandle() : base(true)
		{
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x000A8101 File Offset: 0x000A7101
		internal static uint RegOpenKeyEx(IntPtr key, string subKey, uint ulOptions, uint samDesired, out SafeRegistryHandle resultSubKey)
		{
			return UnsafeNclNativeMethods.RegistryHelper.RegOpenKeyEx(key, subKey, ulOptions, samDesired, out resultSubKey);
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x000A810E File Offset: 0x000A710E
		internal uint RegOpenKeyEx(string subKey, uint ulOptions, uint samDesired, out SafeRegistryHandle resultSubKey)
		{
			return UnsafeNclNativeMethods.RegistryHelper.RegOpenKeyEx(this, subKey, ulOptions, samDesired, out resultSubKey);
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x000A811B File Offset: 0x000A711B
		internal uint RegCloseKey()
		{
			base.Close();
			return this.resClose;
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x000A812C File Offset: 0x000A712C
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
			uint num4 = num3;
			if (num4 == 3U)
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

		// Token: 0x060028AE RID: 10414 RVA: 0x000A819C File Offset: 0x000A719C
		internal uint RegNotifyChangeKeyValue(bool watchSubTree, uint notifyFilter, SafeWaitHandle regEvent, bool async)
		{
			return UnsafeNclNativeMethods.RegistryHelper.RegNotifyChangeKeyValue(this, watchSubTree, notifyFilter, regEvent, async);
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x000A81A9 File Offset: 0x000A71A9
		internal static uint RegOpenCurrentUser(uint samDesired, out SafeRegistryHandle resultKey)
		{
			if (ComNetOS.IsWin9x)
			{
				return UnsafeNclNativeMethods.RegistryHelper.RegOpenKeyEx(UnsafeNclNativeMethods.RegistryHelper.HKEY_CURRENT_USER, null, 0U, samDesired, out resultKey);
			}
			return UnsafeNclNativeMethods.RegistryHelper.RegOpenCurrentUser(samDesired, out resultKey);
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x000A81C8 File Offset: 0x000A71C8
		protected override bool ReleaseHandle()
		{
			if (!this.IsInvalid)
			{
				this.resClose = UnsafeNclNativeMethods.RegistryHelper.RegCloseKey(this.handle);
			}
			base.SetHandleAsInvalid();
			return true;
		}

		// Token: 0x0400279A RID: 10138
		private uint resClose;
	}
}
