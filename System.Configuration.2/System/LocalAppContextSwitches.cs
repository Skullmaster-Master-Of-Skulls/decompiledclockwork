using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200000B RID: 11
	internal static class LocalAppContextSwitches
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000020FA File Offset: 0x000002FA
		public static bool AllowUserConfigFilesToLoadWhenSearchingForWellKnownSqlClientFactories
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Configuration.AllowUserConfigFilesToLoadWhenSearchingForWellKnownSqlClientFactories", ref LocalAppContextSwitches._allowUserConfigFilesToLoadWhenSearchingForWellKnownSqlClientFactories);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000210B File Offset: 0x0000030B
		public static bool AllowUserConfigFilesToLoadWhenSearchingForDatasetSerializationAllowedTypes
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Configuration.AllowUserConfigFilesToLoadWhenSearchingForDatasetSerializationAllowedTypes", ref LocalAppContextSwitches._allowUserConfigFilesToLoadWhenSearchingForDatasetSerializationAllowedTypes);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000211C File Offset: 0x0000031C
		public static bool AllowUserConfigFilesToLoadWhenSearchingForMarkupSerializationAllowedTypes
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Configuration.AllowUserConfigFilesToLoadWhenSearchingForMarkupSerializationAllowedTypes", ref LocalAppContextSwitches._allowUserConfigFilesToLoadWhenSearchingForMarkupSerializationAllowedTypes);
			}
		}

		// Token: 0x040000A4 RID: 164
		private static int _allowUserConfigFilesToLoadWhenSearchingForWellKnownSqlClientFactories;

		// Token: 0x040000A5 RID: 165
		internal const string AllowUserConfigFilesToLoadWhenSearchingForWellKnownSqlClientFactoriesName = "Switch.System.Configuration.AllowUserConfigFilesToLoadWhenSearchingForWellKnownSqlClientFactories";

		// Token: 0x040000A6 RID: 166
		private static int _allowUserConfigFilesToLoadWhenSearchingForDatasetSerializationAllowedTypes;

		// Token: 0x040000A7 RID: 167
		internal const string AllowUserConfigFilesToLoadWhenSearchingForDatasetSerializationAllowedTypesName = "Switch.System.Configuration.AllowUserConfigFilesToLoadWhenSearchingForDatasetSerializationAllowedTypes";

		// Token: 0x040000A8 RID: 168
		private static int _allowUserConfigFilesToLoadWhenSearchingForMarkupSerializationAllowedTypes;

		// Token: 0x040000A9 RID: 169
		internal const string AllowUserConfigFilesToLoadWhenSearchingForMarkupSerializationAllowedTypesName = "Switch.System.Configuration.AllowUserConfigFilesToLoadWhenSearchingForMarkupSerializationAllowedTypes";
	}
}
