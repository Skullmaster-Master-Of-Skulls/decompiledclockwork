using System;
using MailBee.Mime;

namespace a.d
{
	// Token: 0x0200043E RID: 1086
	internal class a
	{
		// Token: 0x0600258B RID: 9611 RVA: 0x000A7E92 File Offset: 0x000A6E92
		private a()
		{
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x000A7E9C File Offset: 0x000A6E9C
		public static EmailAddressCollection a(MailMessage A_0, ref string A_1)
		{
			if (A_0.Builder.SetDateOnSend)
			{
				A_0.Date = DateTime.Now;
			}
			if (A_0.Builder.SetMessageIDOnSend && (A_0.MessageID == string.Empty || A_0.MessageID == A_1))
			{
				A_0.SetUniqueMessageID(null);
			}
			if (A_0.Builder.SetMessageIDOnSend || !A_0.NeedToReparse)
			{
				A_1 = A_0.MessageID;
			}
			EmailAddressCollection emailAddressCollection = null;
			if (A_0.Builder.RemoveBccOnSend && A_0.Headers.Exists("Bcc") && A_0.Bcc.Count > 0)
			{
				emailAddressCollection = new EmailAddressCollection();
				emailAddressCollection.Add(A_0.Bcc);
				A_0.Bcc.Clear();
			}
			return emailAddressCollection;
		}
	}
}
