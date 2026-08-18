using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000203 RID: 515
	internal sealed class SQLMessage
	{
		// Token: 0x060020D5 RID: 8405 RVA: 0x000DE02C File Offset: 0x000DD42C
		private SQLMessage()
		{
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x000DE040 File Offset: 0x000DD440
		internal static string CultureIdError()
		{
			return Res.GetString("SQL_CultureIdError");
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x000DE058 File Offset: 0x000DD458
		internal static string EncryptionNotSupportedByClient()
		{
			return Res.GetString("SQL_EncryptionNotSupportedByClient");
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x000DE070 File Offset: 0x000DD470
		internal static string EncryptionNotSupportedByServer()
		{
			return Res.GetString("SQL_EncryptionNotSupportedByServer");
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x000DE088 File Offset: 0x000DD488
		internal static string OperationCancelled()
		{
			return Res.GetString("SQL_OperationCancelled");
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x000DE0A0 File Offset: 0x000DD4A0
		internal static string SevereError()
		{
			return Res.GetString("SQL_SevereError");
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x000DE0B8 File Offset: 0x000DD4B8
		internal static string SSPIInitializeError()
		{
			return Res.GetString("SQL_SSPIInitializeError");
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x000DE0D0 File Offset: 0x000DD4D0
		internal static string SSPIGenerateError()
		{
			return Res.GetString("SQL_SSPIGenerateError");
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x000DE0E8 File Offset: 0x000DD4E8
		internal static string Timeout()
		{
			return Res.GetString("SQL_Timeout_Execution");
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x000DE100 File Offset: 0x000DD500
		internal static string Timeout_PreLogin_Begin()
		{
			return Res.GetString("SQL_Timeout_PreLogin_Begin");
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x000DE118 File Offset: 0x000DD518
		internal static string Timeout_PreLogin_InitializeConnection()
		{
			return Res.GetString("SQL_Timeout_PreLogin_InitializeConnection");
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x000DE130 File Offset: 0x000DD530
		internal static string Timeout_PreLogin_SendHandshake()
		{
			return Res.GetString("SQL_Timeout_PreLogin_SendHandshake");
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x000DE148 File Offset: 0x000DD548
		internal static string Timeout_PreLogin_ConsumeHandshake()
		{
			return Res.GetString("SQL_Timeout_PreLogin_ConsumeHandshake");
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x000DE160 File Offset: 0x000DD560
		internal static string Timeout_Login_Begin()
		{
			return Res.GetString("SQL_Timeout_Login_Begin");
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x000DE178 File Offset: 0x000DD578
		internal static string Timeout_Login_ProcessConnectionAuth()
		{
			return Res.GetString("SQL_Timeout_Login_ProcessConnectionAuth");
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x000DE190 File Offset: 0x000DD590
		internal static string Timeout_PostLogin()
		{
			return Res.GetString("SQL_Timeout_PostLogin");
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x000DE1A8 File Offset: 0x000DD5A8
		internal static string Timeout_FailoverInfo()
		{
			return Res.GetString("SQL_Timeout_FailoverInfo");
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x000DE1C0 File Offset: 0x000DD5C0
		internal static string Timeout_RoutingDestination()
		{
			return Res.GetString("SQL_Timeout_RoutingDestinationInfo");
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x000DE1D8 File Offset: 0x000DD5D8
		internal static string Duration_PreLogin_Begin(long PreLoginBeginDuration)
		{
			return Res.GetString("SQL_Duration_PreLogin_Begin", new object[]
			{
				PreLoginBeginDuration
			});
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x000DE200 File Offset: 0x000DD600
		internal static string Duration_PreLoginHandshake(long PreLoginBeginDuration, long PreLoginHandshakeDuration)
		{
			return Res.GetString("SQL_Duration_PreLoginHandshake", new object[]
			{
				PreLoginBeginDuration,
				PreLoginHandshakeDuration
			});
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x000DE230 File Offset: 0x000DD630
		internal static string Duration_Login_Begin(long PreLoginBeginDuration, long PreLoginHandshakeDuration, long LoginBeginDuration)
		{
			return Res.GetString("SQL_Duration_Login_Begin", new object[]
			{
				PreLoginBeginDuration,
				PreLoginHandshakeDuration,
				LoginBeginDuration
			});
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x000DE268 File Offset: 0x000DD668
		internal static string Duration_Login_ProcessConnectionAuth(long PreLoginBeginDuration, long PreLoginHandshakeDuration, long LoginBeginDuration, long LoginAuthDuration)
		{
			return Res.GetString("SQL_Duration_Login_ProcessConnectionAuth", new object[]
			{
				PreLoginBeginDuration,
				PreLoginHandshakeDuration,
				LoginBeginDuration,
				LoginAuthDuration
			});
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x000DE2AC File Offset: 0x000DD6AC
		internal static string Duration_PostLogin(long PreLoginBeginDuration, long PreLoginHandshakeDuration, long LoginBeginDuration, long LoginAuthDuration, long PostLoginDuration)
		{
			return Res.GetString("SQL_Duration_PostLogin", new object[]
			{
				PreLoginBeginDuration,
				PreLoginHandshakeDuration,
				LoginBeginDuration,
				LoginAuthDuration,
				PostLoginDuration
			});
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x000DE2F8 File Offset: 0x000DD6F8
		internal static string UserInstanceFailure()
		{
			return Res.GetString("SQL_UserInstanceFailure");
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x000DE310 File Offset: 0x000DD710
		internal static string PreloginError()
		{
			return Res.GetString("Snix_PreLogin");
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x000DE328 File Offset: 0x000DD728
		internal static string ExClientConnectionId()
		{
			return Res.GetString("SQL_ExClientConnectionId");
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x000DE340 File Offset: 0x000DD740
		internal static string ExErrorNumberStateClass()
		{
			return Res.GetString("SQL_ExErrorNumberStateClass");
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x000DE358 File Offset: 0x000DD758
		internal static string ExOriginalClientConnectionId()
		{
			return Res.GetString("SQL_ExOriginalClientConnectionId");
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x000DE370 File Offset: 0x000DD770
		internal static string ExRoutingDestination()
		{
			return Res.GetString("SQL_ExRoutingDestination");
		}
	}
}
