using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000007 RID: 7
	internal static class LocalAppContext
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000022D4 File Offset: 0x000004D4
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000022DB File Offset: 0x000004DB
		private static bool DisableCaching { get; set; }

		// Token: 0x0600000C RID: 12 RVA: 0x000022E3 File Offset: 0x000004E3
		static LocalAppContext()
		{
			LocalAppContext.s_canForwardCalls = LocalAppContext.SetupDelegate();
			AppContextDefaultValues.PopulateDefaultValues();
			LocalAppContext.DisableCaching = LocalAppContext.IsSwitchEnabled("TestSwitch.LocalAppContext.DisableCaching");
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002318 File Offset: 0x00000518
		public static bool IsSwitchEnabled(string switchName)
		{
			bool result;
			if (LocalAppContext.s_canForwardCalls && LocalAppContext.TryGetSwitchFromCentralAppContext(switchName, out result))
			{
				return result;
			}
			return LocalAppContext.IsSwitchEnabledLocal(switchName);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002344 File Offset: 0x00000544
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

		// Token: 0x0600000F RID: 15 RVA: 0x00002394 File Offset: 0x00000594
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

		// Token: 0x06000010 RID: 16 RVA: 0x00002421 File Offset: 0x00000621
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool GetCachedSwitchValue(string switchName, ref int switchValue)
		{
			return switchValue >= 0 && (switchValue > 0 || LocalAppContext.GetCachedSwitchValueInternal(switchName, ref switchValue));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002438 File Offset: 0x00000638
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

		// Token: 0x06000012 RID: 18 RVA: 0x00002464 File Offset: 0x00000664
		internal static void DefineSwitchDefault(string switchName, bool initialValue)
		{
			LocalAppContext.s_switchMap[switchName] = initialValue;
		}

		// Token: 0x04000058 RID: 88
		private static LocalAppContext.TryGetSwitchDelegate TryGetSwitchFromCentralAppContext;

		// Token: 0x04000059 RID: 89
		private static bool s_canForwardCalls;

		// Token: 0x0400005A RID: 90
		private static Dictionary<string, bool> s_switchMap = new Dictionary<string, bool>();

		// Token: 0x0400005B RID: 91
		private static readonly object s_syncLock = new object();

		// Token: 0x0200008A RID: 138
		// (Invoke) Token: 0x0600053E RID: 1342
		private delegate bool TryGetSwitchDelegate(string switchName, out bool value);
	}
}
