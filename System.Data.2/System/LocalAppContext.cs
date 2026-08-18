using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000080 RID: 128
	internal static class LocalAppContext
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x0004946C File Offset: 0x0004886C
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x00049480 File Offset: 0x00048880
		private static bool DisableCaching { get; set; }

		// Token: 0x06000605 RID: 1541 RVA: 0x00049494 File Offset: 0x00048894
		static LocalAppContext()
		{
			LocalAppContext.s_canForwardCalls = LocalAppContext.SetupDelegate();
			AppContextDefaultValues.PopulateDefaultValues();
			LocalAppContext.DisableCaching = LocalAppContext.IsSwitchEnabled("TestSwitch.LocalAppContext.DisableCaching");
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x000494D4 File Offset: 0x000488D4
		public static bool IsSwitchEnabled(string switchName)
		{
			bool result;
			if (LocalAppContext.s_canForwardCalls && LocalAppContext.TryGetSwitchFromCentralAppContext(switchName, out result))
			{
				return result;
			}
			return LocalAppContext.IsSwitchEnabledLocal(switchName);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00049500 File Offset: 0x00048900
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

		// Token: 0x06000608 RID: 1544 RVA: 0x0004955C File Offset: 0x0004895C
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

		// Token: 0x06000609 RID: 1545 RVA: 0x000495EC File Offset: 0x000489EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool GetCachedSwitchValue(string switchName, ref int switchValue)
		{
			return switchValue >= 0 && (switchValue > 0 || LocalAppContext.GetCachedSwitchValueInternal(switchName, ref switchValue));
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00049610 File Offset: 0x00048A10
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

		// Token: 0x0600060B RID: 1547 RVA: 0x0004963C File Offset: 0x00048A3C
		internal static void DefineSwitchDefault(string switchName, bool initialValue)
		{
			LocalAppContext.s_switchMap[switchName] = initialValue;
		}

		// Token: 0x04000266 RID: 614
		private static LocalAppContext.TryGetSwitchDelegate TryGetSwitchFromCentralAppContext;

		// Token: 0x04000267 RID: 615
		private static bool s_canForwardCalls;

		// Token: 0x04000268 RID: 616
		private static Dictionary<string, bool> s_switchMap = new Dictionary<string, bool>();

		// Token: 0x04000269 RID: 617
		private static readonly object s_syncLock = new object();

		// Token: 0x02000344 RID: 836
		// (Invoke) Token: 0x060033DB RID: 13275
		private delegate bool TryGetSwitchDelegate(string switchName, out bool value);
	}
}
