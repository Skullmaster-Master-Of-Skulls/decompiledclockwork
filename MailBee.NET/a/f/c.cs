using System;
using MailBee.ImapMail;

namespace a.f
{
	// Token: 0x020000B9 RID: 185
	internal interface c : a9
	{
		// Token: 0x060006DA RID: 1754
		bool nj();

		// Token: 0x060006DB RID: 1755
		void nk(ImapEnvelopeDownloadedEventArgs A_0);

		// Token: 0x060006DC RID: 1756
		bool nl();

		// Token: 0x060006DD RID: 1757
		void nm(ImapEnvelopeDataChunkReceivedEventArgs A_0);

		// Token: 0x060006DE RID: 1758
		bool nn();

		// Token: 0x060006DF RID: 1759
		void no(ImapServerStatusEventArgs A_0);

		// Token: 0x060006E0 RID: 1760
		bool np();

		// Token: 0x060006E1 RID: 1761
		void nq(ImapMessageStatusEventArgs A_0);

		// Token: 0x060006E2 RID: 1762
		bool nr();

		// Token: 0x060006E3 RID: 1763
		void ns(ImapIdlingEventArgs A_0);
	}
}
