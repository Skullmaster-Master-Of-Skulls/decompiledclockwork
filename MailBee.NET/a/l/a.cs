using System;
using MailBee.EwsMail;
using Microsoft.Exchange.WebServices.Data;

namespace a.l
{
	// Token: 0x02000227 RID: 551
	internal static class a
	{
		// Token: 0x0600128B RID: 4747 RVA: 0x0005217C File Offset: 0x0005117C
		public static PropertySet a(EwsItemParts A_0, ExchangeVersion A_1)
		{
			PropertySet propertySet = new PropertySet(BasePropertySet.FirstClassProperties);
			if ((A_0 & EwsItemParts.MailMessageRecipients) > EwsItemParts.IdOnly)
			{
				propertySet.Add(EmailMessageSchema.ToRecipients);
				propertySet.Add(EmailMessageSchema.CcRecipients);
				propertySet.Add(EmailMessageSchema.BccRecipients);
				propertySet.Add(EmailMessageSchema.ReplyTo);
			}
			if ((A_0 & EwsItemParts.MailMessageBody) > EwsItemParts.IdOnly)
			{
				propertySet.RequestedBodyType = new BodyType?(BodyType.HTML);
				propertySet.Add(ItemSchema.Body);
				propertySet.Add(d.e);
				if (A_1 >= ExchangeVersion.Exchange2013)
				{
					propertySet.Add(ItemSchema.TextBody);
				}
				else
				{
					propertySet.Add(d.c);
				}
			}
			if ((A_0 & EwsItemParts.MailMessageAttachments) > EwsItemParts.IdOnly)
			{
				propertySet.Add(ItemSchema.Attachments);
			}
			if ((A_0 & EwsItemParts.MailMessageRawData) > EwsItemParts.IdOnly)
			{
				propertySet.Add(ItemSchema.MimeContent);
			}
			return propertySet;
		}
	}
}
