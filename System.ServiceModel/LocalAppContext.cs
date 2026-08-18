using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200001A RID: 26
	internal static class LocalAppContext
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000668E File Offset: 0x0000488E
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00006695 File Offset: 0x00004895
		private static bool DisableCaching { get; set; }

		// Token: 0x060000CB RID: 203 RVA: 0x0000669D File Offset: 0x0000489D
		static LocalAppContext()
		{
			LocalAppContext.s_canForwardCalls = LocalAppContext.SetupDelegate();
			AppContextDefaultValues.PopulateDefaultValues();
			LocalAppContext.DisableCaching = LocalAppContext.IsSwitchEnabled("TestSwitch.LocalAppContext.DisableCaching");
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000066D4 File Offset: 0x000048D4
		public static bool IsSwitchEnabled(string switchName)
		{
			bool result;
			if (LocalAppContext.s_canForwardCalls && LocalAppContext.TryGetSwitchFromCentralAppContext(switchName, out result))
			{
				return result;
			}
			return LocalAppContext.IsSwitchEnabledLocal(switchName);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00006700 File Offset: 0x00004900
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

		// Token: 0x060000CE RID: 206 RVA: 0x00006750 File Offset: 0x00004950
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

		// Token: 0x060000CF RID: 207 RVA: 0x000067DD File Offset: 0x000049DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool GetCachedSwitchValue(string switchName, ref int switchValue)
		{
			return switchValue >= 0 && (switchValue > 0 || LocalAppContext.GetCachedSwitchValueInternal(switchName, ref switchValue));
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000067F4 File Offset: 0x000049F4
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

		// Token: 0x060000D1 RID: 209 RVA: 0x00006820 File Offset: 0x00004A20
		internal static void DefineSwitchDefault(string switchName, bool initialValue)
		{
			LocalAppContext.s_switchMap[switchName] = initialValue;
		}

		// Token: 0x0400009C RID: 156
		private static LocalAppContext.TryGetSwitchDelegate TryGetSwitchFromCentralAppContext;

		// Token: 0x0400009D RID: 157
		private static bool s_canForwardCalls;

		// Token: 0x0400009E RID: 158
		private static Dictionary<string, bool> s_switchMap = new Dictionary<string, bool>();

		// Token: 0x0400009F RID: 159
		private static readonly object s_syncLock = new object();

		// Token: 0x02000ABE RID: 2750
		// (Invoke) Token: 0x06006E19 RID: 28185
		private delegate bool TryGetSwitchDelegate(string switchName, out bool value);
	}
}
