using System;

namespace OracleInternal.NotificationServices
{
	// Token: 0x0200017F RID: 383
	internal sealed class Constants
	{
		// Token: 0x0400110A RID: 4362
		public const string ONS_EMPTY_SUBSCRIPTION = "\"eventType=_XNOP\"";

		// Token: 0x0400110B RID: 4363
		public const string ONS_REGISTER_NOTIFICATION = "ONSregister";

		// Token: 0x0400110C RID: 4364
		public const string ONS_STATUS_NOTIFICATION = "ONSstatus";

		// Token: 0x0400110D RID: 4365
		public const string ONS_RPC_PING_NOTIFICATION = "ONS_RPC_PING";

		// Token: 0x0400110E RID: 4366
		public const string ONS_RPC_PING_REPLY_NOTIFICATION = "ONS_RPC_PINGREPLY";

		// Token: 0x0400110F RID: 4367
		public const string ONS_RPC_REQUEST_NOTIFICATION = "ONS_RPC_REQUEST";

		// Token: 0x04001110 RID: 4368
		public const string ONS_REGISTER_ID_PREFIX = "sONSrpc";

		// Token: 0x04001111 RID: 4369
		public const string ONS_REGISTER_ID = "ONSregisterID";

		// Token: 0x04001112 RID: 4370
		public const string ONS_SUBSCRIBER_ID = "SubscriberID";

		// Token: 0x04001113 RID: 4371
		public const string ONS_REGISTER_GROUP = "ONSregisterGroup";

		// Token: 0x04001114 RID: 4372
		public const string ONS_DIRECT_ROUTE = "DirectRoute";

		// Token: 0x04001115 RID: 4373
		public const string ONS_BACK_ROUTE = "BackRoute";

		// Token: 0x04001116 RID: 4374
		public const string ONS_TRACE_ROUTE = "TraceRoute";

		// Token: 0x04001117 RID: 4375
		public const string ONS_DIRECT_SOURCE = "DirectSource";

		// Token: 0x04001118 RID: 4376
		public const string ONS_BROADCAST_ID = "ONSbroadcastID";

		// Token: 0x04001119 RID: 4377
		public const string ONS_RESULT = "Result";

		// Token: 0x0400111A RID: 4378
		public const string ONS_MESSAGE = "Message";

		// Token: 0x0400111B RID: 4379
		public const string ONS_RPC_REQUEST = "ONSrpcRequest";

		// Token: 0x0400111C RID: 4380
		public const long RPC_SERVER_REGISTRATION_TIMEOUT = 120000L;
	}
}
