using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200000C RID: 12
	internal static class LocalAppContext
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000212D File Offset: 0x0000032D
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002134 File Offset: 0x00000334
		private static bool DisableCaching { get; set; }

		// Token: 0x06000016 RID: 22 RVA: 0x0000213C File Offset: 0x0000033C
		static LocalAppContext()
		{
			LocalAppContext.s_canForwardCalls = LocalAppContext.SetupDelegate();
			AppContextDefaultValues.PopulateDefaultValues();
			LocalAppContext.DisableCaching = LocalAppContext.IsSwitchEnabled("TestSwitch.LocalAppContext.DisableCaching");
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002170 File Offset: 0x00000370
		public static bool IsSwitchEnabled(string switchName)
		{
			bool result;
			if (LocalAppContext.s_canForwardCalls && LocalAppContext.TryGetSwitchFromCentralAppContext(switchName, out result))
			{
				return result;
			}
			return LocalAppContext.IsSwitchEnabledLocal(switchName);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000219C File Offset: 0x0000039C
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

		// Token: 0x06000019 RID: 25 RVA: 0x000021EC File Offset: 0x000003EC
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

		// Token: 0x0600001A RID: 26 RVA: 0x00002279 File Offset: 0x00000479
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool GetCachedSwitchValue(string switchName, ref int switchValue)
		{
			return switchValue >= 0 && (switchValue > 0 || LocalAppContext.GetCachedSwitchValueInternal(switchName, ref switchValue));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002290 File Offset: 0x00000490
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

		// Token: 0x0600001C RID: 28 RVA: 0x000022BC File Offset: 0x000004BC
		internal static void DefineSwitchDefault(string switchName, bool initialValue)
		{
			LocalAppContext.s_switchMap[switchName] = initialValue;
		}

		// Token: 0x040000AA RID: 170
		private static LocalAppContext.TryGetSwitchDelegate TryGetSwitchFromCentralAppContext;

		// Token: 0x040000AB RID: 171
		private static bool s_canForwardCalls;

		// Token: 0x040000AC RID: 172
		private static Dictionary<string, bool> s_switchMap = new Dictionary<string, bool>();

		// Token: 0x040000AD RID: 173
		private static readonly object s_syncLock = new object();

		// Token: 0x020000C6 RID: 198
		// (Invoke) Token: 0x060007C7 RID: 1991
		private delegate bool TryGetSwitchDelegate(string switchName, out bool value);
	}
}
