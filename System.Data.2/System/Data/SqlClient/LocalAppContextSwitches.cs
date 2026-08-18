using System;
using System.Runtime.CompilerServices;

namespace System.Data.SqlClient
{
	// Token: 0x02000230 RID: 560
	internal static class LocalAppContextSwitches
	{
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060022DB RID: 8923 RVA: 0x000F1994 File Offset: 0x000F0D94
		public static bool MakeReadAsyncBlocking
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Data.SqlClient.MakeReadAsyncBlocking", ref LocalAppContextSwitches._makeReadAsyncBlocking);
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060022DC RID: 8924 RVA: 0x000F19B0 File Offset: 0x000F0DB0
		public static bool UseMinimumLoginTimeout
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Data.SqlClient.UseOneSecFloorInTimeoutCalculationDuringLogin", ref LocalAppContextSwitches._useMinimumLoginTimeout);
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060022DD RID: 8925 RVA: 0x000F19CC File Offset: 0x000F0DCC
		public static bool DisableTNIRByDefault
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Data.SqlClient.DisableTNIRByDefaultInConnectionString", ref LocalAppContextSwitches._disableTNIRByDefault);
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060022DE RID: 8926 RVA: 0x000F19E8 File Offset: 0x000F0DE8
		public static bool SendCancellationAfterBulkCopySuccess
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Data.SqlClient.SendCancellationAfterBulkCopySuccess", ref LocalAppContextSwitches._sendCancellationAfterBulkCopySuccess);
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060022DF RID: 8927 RVA: 0x000F1A04 File Offset: 0x000F0E04
		public static bool UseCultureInfoKazakhCodePage
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Data.SqlClient.UseCultureInfoKazakhCodePage", ref LocalAppContextSwitches._useCultureInfoKazakhCodePage);
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060022E0 RID: 8928 RVA: 0x000F1A20 File Offset: 0x000F0E20
		public static bool CleanupParserOnAllFailures
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Data.SqlClient.CleanupParserOnAllFailures", ref LocalAppContextSwitches._cleanupParserOnAllFailures);
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060022E1 RID: 8929 RVA: 0x000F1A3C File Offset: 0x000F0E3C
		public static bool DisableHardenedQueryTimeouts
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Data.SqlClient.DisableHardenedQueryTimeouts", ref LocalAppContextSwitches._disableHardenedQueryTimeouts);
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060022E2 RID: 8930 RVA: 0x000F1A58 File Offset: 0x000F0E58
		public static bool DisablePooledConnectionResetOnTransientError
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Data.SqlClient.DisablePooledConnectionResetOnTransientError", ref LocalAppContextSwitches._disablePooledConnectionResetOnTransientError);
			}
		}

		// Token: 0x0400151D RID: 5405
		internal const string MakeReadAsyncBlockingString = "Switch.System.Data.SqlClient.MakeReadAsyncBlocking";

		// Token: 0x0400151E RID: 5406
		private static int _makeReadAsyncBlocking;

		// Token: 0x0400151F RID: 5407
		internal const string UseMinimumLoginTimeoutString = "Switch.System.Data.SqlClient.UseOneSecFloorInTimeoutCalculationDuringLogin";

		// Token: 0x04001520 RID: 5408
		private static int _useMinimumLoginTimeout;

		// Token: 0x04001521 RID: 5409
		internal const string DisableTNIRByDefaultString = "Switch.System.Data.SqlClient.DisableTNIRByDefaultInConnectionString";

		// Token: 0x04001522 RID: 5410
		private static int _disableTNIRByDefault;

		// Token: 0x04001523 RID: 5411
		internal const string SendCancellationAfterBulkCopySuccessString = "Switch.System.Data.SqlClient.SendCancellationAfterBulkCopySuccess";

		// Token: 0x04001524 RID: 5412
		private static int _sendCancellationAfterBulkCopySuccess;

		// Token: 0x04001525 RID: 5413
		internal const string UseCultureInfoKazakhCodePageString = "Switch.System.Data.SqlClient.UseCultureInfoKazakhCodePage";

		// Token: 0x04001526 RID: 5414
		private static int _useCultureInfoKazakhCodePage;

		// Token: 0x04001527 RID: 5415
		internal const string CleanupParserOnAllFailuresString = "Switch.System.Data.SqlClient.CleanupParserOnAllFailures";

		// Token: 0x04001528 RID: 5416
		private static int _cleanupParserOnAllFailures;

		// Token: 0x04001529 RID: 5417
		internal const string DisableHardenedQueryTimeoutsString = "Switch.System.Data.SqlClient.DisableHardenedQueryTimeouts";

		// Token: 0x0400152A RID: 5418
		private static int _disableHardenedQueryTimeouts;

		// Token: 0x0400152B RID: 5419
		internal const string DisablePooledConnectionResetOnTransientErrorString = "Switch.System.Data.SqlClient.DisablePooledConnectionResetOnTransientError";

		// Token: 0x0400152C RID: 5420
		private static int _disablePooledConnectionResetOnTransientError;
	}
}
