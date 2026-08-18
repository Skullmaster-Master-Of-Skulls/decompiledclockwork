using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000003 RID: 3
	internal static class LocalAppContext
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002239 File Offset: 0x00000439
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002240 File Offset: 0x00000440
		private static bool DisableCaching { get; set; }

		// Token: 0x06000007 RID: 7 RVA: 0x00002248 File Offset: 0x00000448
		static LocalAppContext()
		{
			LocalAppContext.s_canForwardCalls = LocalAppContext.SetupDelegate();
			AppContextDefaultValues.PopulateDefaultValues();
			LocalAppContext.DisableCaching = LocalAppContext.IsSwitchEnabled("TestSwitch.LocalAppContext.DisableCaching");
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000227C File Offset: 0x0000047C
		public static bool IsSwitchEnabled(string switchName)
		{
			bool result;
			if (LocalAppContext.s_canForwardCalls && LocalAppContext.TryGetSwitchFromCentralAppContext(switchName, out result))
			{
				return result;
			}
			return LocalAppContext.IsSwitchEnabledLocal(switchName);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022A8 File Offset: 0x000004A8
		private static bool IsSwitchEnabledLocal(string switchName)
		{
			Dictionary<string, bool> obj = LocalAppContext.s_switchMap;
			bool flag3;
			bool flag2;
			lock (obj)
			{
				flag2 = LocalAppContext.s_switchMap.TryGetValue(switchName, out flag3);
			}
			return flag2 && flag3;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022F8 File Offset: 0x000004F8
		private static bool SetupDelegate()
		{
			Type type = typeof(object).Assembly.GetType("System.AppContext");
			if (type == null)
			{
				return false;
			}
			MethodInfo method = type.GetMethod("TryGetSwitch", BindingFlags.Static | BindingFlags.Public, null, new Type[]
			{
				typeof(string),
				typeof(bool).MakeByRefType()
			}, null);
			if (method == null)
			{
				return false;
			}
			LocalAppContext.TryGetSwitchFromCentralAppContext = (LocalAppContext.TryGetSwitchDelegate)Delegate.CreateDelegate(typeof(LocalAppContext.TryGetSwitchDelegate), method);
			return true;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002385 File Offset: 0x00000585
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool GetCachedSwitchValue(string switchName, ref int switchValue)
		{
			return switchValue >= 0 && (switchValue > 0 || LocalAppContext.GetCachedSwitchValueInternal(switchName, ref switchValue));
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000239C File Offset: 0x0000059C
		private static bool GetCachedSwitchValueInternal(string switchName, ref int switchValue)
		{
			if (LocalAppContext.DisableCaching)
			{
				return LocalAppContext.IsSwitchEnabled(switchName);
			}
			bool flag = LocalAppContext.IsSwitchEnabled(switchName);
			switchValue = (flag ? 1 : -1);
			return flag;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023C8 File Offset: 0x000005C8
		internal static void DefineSwitchDefault(string switchName, bool initialValue)
		{
			LocalAppContext.s_switchMap[switchName] = initialValue;
		}

		// Token: 0x04000001 RID: 1
		private static LocalAppContext.TryGetSwitchDelegate TryGetSwitchFromCentralAppContext;

		// Token: 0x04000002 RID: 2
		private static bool s_canForwardCalls;

		// Token: 0x04000003 RID: 3
		private static Dictionary<string, bool> s_switchMap = new Dictionary<string, bool>();

		// Token: 0x04000004 RID: 4
		private static readonly object s_syncLock = new object();

		// Token: 0x02000057 RID: 87
		// (Invoke) Token: 0x0600036B RID: 875
		private delegate bool TryGetSwitchDelegate(string switchName, out bool value);
	}
}
