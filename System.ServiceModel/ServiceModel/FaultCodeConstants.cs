using System;

namespace System.ServiceModel
{
	// Token: 0x02000176 RID: 374
	internal class FaultCodeConstants
	{
		// Token: 0x02000AF1 RID: 2801
		public static class Namespaces
		{
			// Token: 0x04003F42 RID: 16194
			public const string NetDispatch = "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/dispatcher";

			// Token: 0x04003F43 RID: 16195
			public const string Transactions = "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/transactions";
		}

		// Token: 0x02000AF2 RID: 2802
		public static class Codes
		{
			// Token: 0x04003F44 RID: 16196
			public const string DeserializationFailed = "DeserializationFailed";

			// Token: 0x04003F45 RID: 16197
			public const string SessionTerminated = "SessionTerminated";

			// Token: 0x04003F46 RID: 16198
			public const string InternalServiceFault = "InternalServiceFault";

			// Token: 0x04003F47 RID: 16199
			public const string TransactionHeaderMalformed = "TransactionHeaderMalformed";

			// Token: 0x04003F48 RID: 16200
			public const string TransactionHeaderMissing = "TransactionHeaderMissing";

			// Token: 0x04003F49 RID: 16201
			public const string TransactionUnmarshalingFailed = "TransactionUnmarshalingFailed";

			// Token: 0x04003F4A RID: 16202
			public const string TransactionIsolationLevelMismatch = "TransactionIsolationLevelMismatch";

			// Token: 0x04003F4B RID: 16203
			public const string TransactionAborted = "TransactionAborted";

			// Token: 0x04003F4C RID: 16204
			public const string IssuedTokenFlowNotAllowed = "IssuedTokenFlowNotAllowed";
		}

		// Token: 0x02000AF3 RID: 2803
		public static class Actions
		{
			// Token: 0x04003F4D RID: 16205
			public const string NetDispatcher = "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/dispatcher/fault";

			// Token: 0x04003F4E RID: 16206
			public const string Transactions = "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/transactions/fault";
		}
	}
}
