using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000207 RID: 519
	internal enum ParsingErrorState
	{
		// Token: 0x04001387 RID: 4999
		Undefined,
		// Token: 0x04001388 RID: 5000
		FedAuthInfoLengthTooShortForCountOfInfoIds,
		// Token: 0x04001389 RID: 5001
		FedAuthInfoLengthTooShortForData,
		// Token: 0x0400138A RID: 5002
		FedAuthInfoFailedToReadCountOfInfoIds,
		// Token: 0x0400138B RID: 5003
		FedAuthInfoFailedToReadTokenStream,
		// Token: 0x0400138C RID: 5004
		FedAuthInfoInvalidOffset,
		// Token: 0x0400138D RID: 5005
		FedAuthInfoFailedToReadData,
		// Token: 0x0400138E RID: 5006
		FedAuthInfoDataNotUnicode,
		// Token: 0x0400138F RID: 5007
		FedAuthInfoDoesNotContainStsurlAndSpn,
		// Token: 0x04001390 RID: 5008
		FedAuthInfoNotReceived,
		// Token: 0x04001391 RID: 5009
		FedAuthNotAcknowledged,
		// Token: 0x04001392 RID: 5010
		FedAuthFeatureAckContainsExtraData,
		// Token: 0x04001393 RID: 5011
		FedAuthFeatureAckUnknownLibraryType,
		// Token: 0x04001394 RID: 5012
		UnrequestedFeatureAckReceived,
		// Token: 0x04001395 RID: 5013
		UnknownFeatureAck,
		// Token: 0x04001396 RID: 5014
		InvalidTdsTokenReceived,
		// Token: 0x04001397 RID: 5015
		SessionStateLengthTooShort,
		// Token: 0x04001398 RID: 5016
		SessionStateInvalidStatus,
		// Token: 0x04001399 RID: 5017
		CorruptedTdsStream,
		// Token: 0x0400139A RID: 5018
		ProcessSniPacketFailed,
		// Token: 0x0400139B RID: 5019
		FedAuthRequiredPreLoginResponseInvalidValue,
		// Token: 0x0400139C RID: 5020
		TceUnknownVersion,
		// Token: 0x0400139D RID: 5021
		TceInvalidVersion,
		// Token: 0x0400139E RID: 5022
		TceInvalidOrdinalIntoCipherInfoTable
	}
}
