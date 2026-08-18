using System;
using System.Collections;

namespace MailBee.BounceMail
{
	// Token: 0x0200007D RID: 125
	public class DsnRecipientCollection : CollectionBase
	{
		// Token: 0x17000257 RID: 599
		public DsnRecipient this[int index]
		{
			get
			{
				return (DsnRecipient)base.List[index];
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000AC7F File Offset: 0x00009C7F
		internal DsnRecipientCollection()
		{
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000AC87 File Offset: 0x00009C87
		internal void a(DsnRecipient A_0)
		{
			base.List.Add(A_0);
		}
	}
}
