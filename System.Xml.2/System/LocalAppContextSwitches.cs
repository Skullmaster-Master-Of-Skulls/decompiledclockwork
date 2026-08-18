using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200005B RID: 91
	internal static class LocalAppContextSwitches
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000D227 File Offset: 0x0000B427
		public static bool DontThrowOnInvalidSurrogatePairs
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Xml.DontThrowOnInvalidSurrogatePairs", ref LocalAppContextSwitches._dontThrowOnInvalidSurrogatePairs);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000D238 File Offset: 0x0000B438
		public static bool IgnoreEmptyKeySequences
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Xml.IgnoreEmptyKeySequences", ref LocalAppContextSwitches._ignoreEmptyKeySequences);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000D249 File Offset: 0x0000B449
		public static bool IgnoreKindInUtcTimeSerialization
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Xml.IgnoreKindInUtcTimeSerialization", ref LocalAppContextSwitches._ignoreKindInUtcTimeSerialization);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000D25A File Offset: 0x0000B45A
		public static bool EnableTimeSpanSerialization
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Xml.EnableTimeSpanSerialization", ref LocalAppContextSwitches._enableTimeSpanSerialization);
			}
		}

		// Token: 0x04000157 RID: 343
		private static int _dontThrowOnInvalidSurrogatePairs;

		// Token: 0x04000158 RID: 344
		private static int _ignoreEmptyKeySequences;

		// Token: 0x04000159 RID: 345
		private static int _ignoreKindInUtcTimeSerialization;

		// Token: 0x0400015A RID: 346
		private static int _enableTimeSpanSerialization;
	}
}
