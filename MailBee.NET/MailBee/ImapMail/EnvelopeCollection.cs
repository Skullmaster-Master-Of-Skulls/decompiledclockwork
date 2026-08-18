using System;
using System.Collections;

namespace MailBee.ImapMail
{
	// Token: 0x02000174 RID: 372
	public class EnvelopeCollection : CollectionBase
	{
		// Token: 0x17000413 RID: 1043
		public Envelope this[int index]
		{
			get
			{
				return (Envelope)base.List[index];
			}
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0003295D File Offset: 0x0003195D
		public void Add(Envelope item)
		{
			if (item == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.List.Add(item);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00032978 File Offset: 0x00031978
		public void Add(EnvelopeCollection items)
		{
			if (items == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			foreach (object obj in items)
			{
				Envelope value = (Envelope)obj;
				base.List.Add(value);
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x000329E0 File Offset: 0x000319E0
		public void Reverse()
		{
			base.InnerList.Reverse();
		}
	}
}
