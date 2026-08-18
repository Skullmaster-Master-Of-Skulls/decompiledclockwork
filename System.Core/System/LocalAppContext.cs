using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000036 RID: 54
	internal static class LocalAppContext
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00003C21 File Offset: 0x00001E21
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00003C28 File Offset: 0x00001E28
		private static bool DisableCaching { get; set; }

		// Token: 0x06000154 RID: 340 RVA: 0x00003C30 File Offset: 0x00001E30
		static LocalAppContext()
		{
			LocalAppContext.s_canForwardCalls = LocalAppContext.SetupDelegate();
			AppContextDefaultValues.PopulateDefaultValues();
			LocalAppContext.DisableCaching = LocalAppContext.IsSwitchEnabled("TestSwitch.LocalAppContext.DisableCaching");
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00003C64 File Offset: 0x00001E64
		public static bool IsSwitchEnabled(string switchName)
		{
			bool result;
			if (LocalAppContext.s_canForwardCalls && LocalAppContext.TryGetSwitchFromCentralAppContext(switchName, out result))
			{
				return result;
			}
			return LocalAppContext.IsSwitchEnabledLocal(switchName);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00003C90 File Offset: 0x00001E90
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

		// Token: 0x06000157 RID: 343 RVA: 0x00003CE0 File Offset: 0x00001EE0
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

		// Token: 0x06000158 RID: 344 RVA: 0x00003D6D File Offset: 0x00001F6D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool GetCachedSwitchValue(string switchName, ref int switchValue)
		{
			return switchValue >= 0 && (switchValue > 0 || LocalAppContext.GetCachedSwitchValueInternal(switchName, ref switchValue));
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00003D84 File Offset: 0x00001F84
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

		// Token: 0x0600015A RID: 346 RVA: 0x00003DB0 File Offset: 0x00001FB0
		internal static void DefineSwitchDefault(string switchName, bool initialValue)
		{
			LocalAppContext.s_switchMap[switchName] = initialValue;
		}

		// Token: 0x040000DF RID: 223
		private static LocalAppContext.TryGetSwitchDelegate TryGetSwitchFromCentralAppContext;

		// Token: 0x040000E0 RID: 224
		private static bool s_canForwardCalls;

		// Token: 0x040000E1 RID: 225
		private static Dictionary<string, bool> s_switchMap = new Dictionary<string, bool>();

		// Token: 0x040000E2 RID: 226
		private static readonly object s_syncLock = new object();

		// Token: 0x020002F8 RID: 760
		// (Invoke) Token: 0x06001A48 RID: 6728
		private delegate bool TryGetSwitchDelegate(string switchName, out bool value);
	}
}
