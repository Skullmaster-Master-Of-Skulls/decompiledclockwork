using System;
using MailBee.SmtpMail;

namespace a.d
{
	// Token: 0x02000454 RID: 1108
	internal interface o : a9
	{
		// Token: 0x06002706 RID: 9990
		bool l5();

		// Token: 0x06002707 RID: 9991
		void l6(SmtpMessageSenderSubmittedEventArgs A_0);

		// Token: 0x06002708 RID: 9992
		bool l7();

		// Token: 0x06002709 RID: 9993
		void l8(SmtpMessageRecipientSubmittedEventArgs A_0);

		// Token: 0x0600270A RID: 9994
		bool l9();

		// Token: 0x0600270B RID: 9995
		void ma(SmtpMessageDataChunkSentEventArgs A_0);

		// Token: 0x0600270C RID: 9996
		bool mb();

		// Token: 0x0600270D RID: 9997
		void mc(SmtpMessageSubmittedToServerEventArgs A_0);

		// Token: 0x0600270E RID: 9998
		bool md();

		// Token: 0x0600270F RID: 9999
		void me(SmtpMessageSentEventArgs A_0);

		// Token: 0x06002710 RID: 10000
		bool mf();

		// Token: 0x06002711 RID: 10001
		void mg(SmtpMessageNotSentEventArgs A_0);

		// Token: 0x06002712 RID: 10002
		bool mh();

		// Token: 0x06002713 RID: 10003
		void mi(SmtpTransientErrorOccurredEventArgs A_0);

		// Token: 0x06002714 RID: 10004
		bool mj();

		// Token: 0x06002715 RID: 10005
		void mk(SmtpMergingMessageEventArgs A_0);

		// Token: 0x06002716 RID: 10006
		bool l3();

		// Token: 0x06002717 RID: 10007
		void l4(SmtpSendingMessageEventArgs A_0);

		// Token: 0x06002718 RID: 10008
		bool ml();

		// Token: 0x06002719 RID: 10009
		void mm(SmtpSubmittingMessageToPickupFolderEventArgs A_0);

		// Token: 0x0600271A RID: 10010
		bool mn();

		// Token: 0x0600271B RID: 10011
		void mo(SmtpMessageSubmittedToPickupFolderEventArgs A_0);

		// Token: 0x0600271C RID: 10012
		bool mp();

		// Token: 0x0600271D RID: 10013
		void mq(SmtpFinishingJobEventArgs A_0);

		// Token: 0x0600271E RID: 10014
		bool mt();

		// Token: 0x0600271F RID: 10015
		void mu(SmtpMessageDirectSendDoneEventArgs A_0);

		// Token: 0x06002720 RID: 10016
		bool mr();

		// Token: 0x06002721 RID: 10017
		void ms(SmtpMessageMXLookupDoneEventArgs A_0);
	}
}
