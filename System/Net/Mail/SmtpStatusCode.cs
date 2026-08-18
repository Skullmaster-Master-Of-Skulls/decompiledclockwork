using System;

namespace System.Net.Mail
{
	// Token: 0x020006D9 RID: 1753
	public enum SmtpStatusCode
	{
		// Token: 0x04003141 RID: 12609
		SystemStatus = 211,
		// Token: 0x04003142 RID: 12610
		HelpMessage = 214,
		// Token: 0x04003143 RID: 12611
		ServiceReady = 220,
		// Token: 0x04003144 RID: 12612
		ServiceClosingTransmissionChannel,
		// Token: 0x04003145 RID: 12613
		Ok = 250,
		// Token: 0x04003146 RID: 12614
		UserNotLocalWillForward,
		// Token: 0x04003147 RID: 12615
		CannotVerifyUserWillAttemptDelivery,
		// Token: 0x04003148 RID: 12616
		StartMailInput = 354,
		// Token: 0x04003149 RID: 12617
		ServiceNotAvailable = 421,
		// Token: 0x0400314A RID: 12618
		MailboxBusy = 450,
		// Token: 0x0400314B RID: 12619
		LocalErrorInProcessing,
		// Token: 0x0400314C RID: 12620
		InsufficientStorage,
		// Token: 0x0400314D RID: 12621
		ClientNotPermitted = 454,
		// Token: 0x0400314E RID: 12622
		CommandUnrecognized = 500,
		// Token: 0x0400314F RID: 12623
		SyntaxError,
		// Token: 0x04003150 RID: 12624
		CommandNotImplemented,
		// Token: 0x04003151 RID: 12625
		BadCommandSequence,
		// Token: 0x04003152 RID: 12626
		MustIssueStartTlsFirst = 530,
		// Token: 0x04003153 RID: 12627
		CommandParameterNotImplemented = 504,
		// Token: 0x04003154 RID: 12628
		MailboxUnavailable = 550,
		// Token: 0x04003155 RID: 12629
		UserNotLocalTryAlternatePath,
		// Token: 0x04003156 RID: 12630
		ExceededStorageAllocation,
		// Token: 0x04003157 RID: 12631
		MailboxNameNotAllowed,
		// Token: 0x04003158 RID: 12632
		TransactionFailed,
		// Token: 0x04003159 RID: 12633
		GeneralFailure = -1
	}
}
