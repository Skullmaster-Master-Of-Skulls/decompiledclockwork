using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000060 RID: 96
	internal static class LocalAppContext
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000D794 File Offset: 0x0000B994
		// (set) Token: 0x0600035F RID: 863 RVA: 0x0000D79B File Offset: 0x0000B99B
		private static bool DisableCaching { get; set; }

		// Token: 0x06000360 RID: 864 RVA: 0x0000D7A3 File Offset: 0x0000B9A3
		static LocalAppContext()
		{
			LocalAppContext.s_canForwardCalls = LocalAppContext.SetupDelegate();
			AppContextDefaultValues.PopulateDefaultValues();
			LocalAppContext.DisableCaching = LocalAppContext.IsSwitchEnabled("TestSwitch.LocalAppContext.DisableCaching");
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000D7D8 File Offset: 0x0000B9D8
		public static bool IsSwitchEnabled(string switchName)
		{
			bool result;
			if (LocalAppContext.s_canForwardCalls && LocalAppContext.TryGetSwitchFromCentralAppContext(switchName, out result))
			{
				return result;
			}
			return LocalAppContext.IsSwitchEnabledLocal(switchName);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000D804 File Offset: 0x0000BA04
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

		// Token: 0x06000363 RID: 867 RVA: 0x0000D854 File Offset: 0x0000BA54
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

		// Token: 0x06000364 RID: 868 RVA: 0x0000D8E1 File Offset: 0x0000BAE1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool GetCachedSwitchValue(string switchName, ref int switchValue)
		{
			return switchValue >= 0 && (switchValue > 0 || LocalAppContext.GetCachedSwitchValueInternal(switchName, ref switchValue));
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000D8F8 File Offset: 0x0000BAF8
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

		// Token: 0x06000366 RID: 870 RVA: 0x0000D924 File Offset: 0x0000BB24
		internal static void DefineSwitchDefault(string switchName, bool initialValue)
		{
			LocalAppContext.s_switchMap[switchName] = initialValue;
		}

		// Token: 0x04000187 RID: 391
		private static LocalAppContext.TryGetSwitchDelegate TryGetSwitchFromCentralAppContext;

		// Token: 0x04000188 RID: 392
		private static bool s_canForwardCalls;

		// Token: 0x04000189 RID: 393
		private static Dictionary<string, bool> s_switchMap = new Dictionary<string, bool>();

		// Token: 0x0400018A RID: 394
		private static readonly object s_syncLock = new object();

		// Token: 0x02000306 RID: 774
		// (Invoke) Token: 0x06002D9D RID: 11677
		private delegate bool TryGetSwitchDelegate(string switchName, out bool value);
	}
}
