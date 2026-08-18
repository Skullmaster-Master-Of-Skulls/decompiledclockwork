using System;

namespace System.Net.Mail
{
	// Token: 0x02000295 RID: 661
	public enum SmtpStatusCode
	{
		// Token: 0x04001873 RID: 6259
		SystemStatus = 211,
		// Token: 0x04001874 RID: 6260
		HelpMessage = 214,
		// Token: 0x04001875 RID: 6261
		ServiceReady = 220,
		// Token: 0x04001876 RID: 6262
		ServiceClosingTransmissionChannel,
		// Token: 0x04001877 RID: 6263
		Ok = 250,
		// Token: 0x04001878 RID: 6264
		UserNotLocalWillForward,
		// Token: 0x04001879 RID: 6265
		CannotVerifyUserWillAttemptDelivery,
		// Token: 0x0400187A RID: 6266
		StartMailInput = 354,
		// Token: 0x0400187B RID: 6267
		ServiceNotAvailable = 421,
		// Token: 0x0400187C RID: 6268
		MailboxBusy = 450,
		// Token: 0x0400187D RID: 6269
		LocalErrorInProcessing,
		// Token: 0x0400187E RID: 6270
		InsufficientStorage,
		// Token: 0x0400187F RID: 6271
		ClientNotPermitted = 454,
		// Token: 0x04001880 RID: 6272
		CommandUnrecognized = 500,
		// Token: 0x04001881 RID: 6273
		SyntaxError,
		// Token: 0x04001882 RID: 6274
		CommandNotImplemented,
		// Token: 0x04001883 RID: 6275
		BadCommandSequence,
		// Token: 0x04001884 RID: 6276
		MustIssueStartTlsFirst = 530,
		// Token: 0x04001885 RID: 6277
		CommandParameterNotImplemented = 504,
		// Token: 0x04001886 RID: 6278
		MailboxUnavailable = 550,
		// Token: 0x04001887 RID: 6279
		UserNotLocalTryAlternatePath,
		// Token: 0x04001888 RID: 6280
		ExceededStorageAllocation,
		// Token: 0x04001889 RID: 6281
		MailboxNameNotAllowed,
		// Token: 0x0400188A RID: 6282
		TransactionFailed,
		// Token: 0x0400188B RID: 6283
		GeneralFailure = -1
	}
}
