using System;
using System.Data.SqlClient;

namespace System.Data.Common
{
	// Token: 0x020002EB RID: 747
	internal static class DbConnectionStringDefaults
	{
		// Token: 0x04001CF0 RID: 7408
		internal const string Driver = "";

		// Token: 0x04001CF1 RID: 7409
		internal const string Dsn = "";

		// Token: 0x04001CF2 RID: 7410
		internal const bool AdoNetPooler = false;

		// Token: 0x04001CF3 RID: 7411
		internal const string FileName = "";

		// Token: 0x04001CF4 RID: 7412
		internal const int OleDbServices = -13;

		// Token: 0x04001CF5 RID: 7413
		internal const string Provider = "";

		// Token: 0x04001CF6 RID: 7414
		internal const bool Unicode = false;

		// Token: 0x04001CF7 RID: 7415
		internal const bool OmitOracleConnectionName = false;

		// Token: 0x04001CF8 RID: 7416
		internal const ApplicationIntent ApplicationIntent = ApplicationIntent.ReadWrite;

		// Token: 0x04001CF9 RID: 7417
		internal const string ApplicationName = ".Net SqlClient Data Provider";

		// Token: 0x04001CFA RID: 7418
		internal const bool AsynchronousProcessing = false;

		// Token: 0x04001CFB RID: 7419
		internal const string AttachDBFilename = "";

		// Token: 0x04001CFC RID: 7420
		internal const int ConnectTimeout = 15;

		// Token: 0x04001CFD RID: 7421
		internal const bool ConnectionReset = true;

		// Token: 0x04001CFE RID: 7422
		internal const bool ContextConnection = false;

		// Token: 0x04001CFF RID: 7423
		internal const string CurrentLanguage = "";

		// Token: 0x04001D00 RID: 7424
		internal const string DataSource = "";

		// Token: 0x04001D01 RID: 7425
		internal const bool Encrypt = false;

		// Token: 0x04001D02 RID: 7426
		internal const bool Enlist = true;

		// Token: 0x04001D03 RID: 7427
		internal const string FailoverPartner = "";

		// Token: 0x04001D04 RID: 7428
		internal const string InitialCatalog = "";

		// Token: 0x04001D05 RID: 7429
		internal const bool IntegratedSecurity = false;

		// Token: 0x04001D06 RID: 7430
		internal const int LoadBalanceTimeout = 0;

		// Token: 0x04001D07 RID: 7431
		internal const bool MultipleActiveResultSets = false;

		// Token: 0x04001D08 RID: 7432
		internal const bool MultiSubnetFailover = false;

		// Token: 0x04001D09 RID: 7433
		internal static readonly bool TransparentNetworkIPResolution = !LocalAppContextSwitches.DisableTNIRByDefault;

		// Token: 0x04001D0A RID: 7434
		internal const int MaxPoolSize = 100;

		// Token: 0x04001D0B RID: 7435
		internal const int MinPoolSize = 0;

		// Token: 0x04001D0C RID: 7436
		internal const string NetworkLibrary = "";

		// Token: 0x04001D0D RID: 7437
		internal const int PacketSize = 8000;

		// Token: 0x04001D0E RID: 7438
		internal const string Password = "";

		// Token: 0x04001D0F RID: 7439
		internal const bool PersistSecurityInfo = false;

		// Token: 0x04001D10 RID: 7440
		internal const bool Pooling = true;

		// Token: 0x04001D11 RID: 7441
		internal const bool TrustServerCertificate = false;

		// Token: 0x04001D12 RID: 7442
		internal const string TypeSystemVersion = "Latest";

		// Token: 0x04001D13 RID: 7443
		internal const string UserID = "";

		// Token: 0x04001D14 RID: 7444
		internal const bool UserInstance = false;

		// Token: 0x04001D15 RID: 7445
		internal const bool Replication = false;

		// Token: 0x04001D16 RID: 7446
		internal const string WorkstationID = "";

		// Token: 0x04001D17 RID: 7447
		internal const string TransactionBinding = "Implicit Unbind";

		// Token: 0x04001D18 RID: 7448
		internal const int ConnectRetryCount = 1;

		// Token: 0x04001D19 RID: 7449
		internal const int ConnectRetryInterval = 10;

		// Token: 0x04001D1A RID: 7450
		internal static readonly SqlAuthenticationMethod Authentication = SqlAuthenticationMethod.NotSpecified;

		// Token: 0x04001D1B RID: 7451
		internal static readonly SqlConnectionColumnEncryptionSetting ColumnEncryptionSetting = SqlConnectionColumnEncryptionSetting.Disabled;

		// Token: 0x04001D1C RID: 7452
		internal const string EnclaveAttestationUrl = "";

		// Token: 0x04001D1D RID: 7453
		internal const PoolBlockingPeriod PoolBlockingPeriod = PoolBlockingPeriod.Auto;
	}
}
