using System;
using System.Collections;

namespace MailBee.Mime
{
	// Token: 0x0200055F RID: 1375
	public class MailMessageCollection : CollectionBase
	{
		// Token: 0x17000547 RID: 1351
		public MailMessage this[int index]
		{
			get
			{
				return (MailMessage)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x000DAC7C File Offset: 0x000D9C7C
		public void Add(MailMessage message)
		{
			if (message == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.List.Add(message);
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x000DAC98 File Offset: 0x000D9C98
		public void Add(MailMessageCollection messages)
		{
			if (messages == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			foreach (object obj in messages)
			{
				MailMessage value = (MailMessage)obj;
				base.List.Add(value);
			}
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x000DAD00 File Offset: 0x000D9D00
		public void Reverse()
		{
			base.InnerList.Reverse();
		}
	}
}
