using System;
using System.Collections;

namespace MailBee.BounceMail
{
	// Token: 0x02000082 RID: 130
	public class RecipientStatusCollection : CollectionBase
	{
		// Token: 0x1700026B RID: 619
		public RecipientStatus this[int index]
		{
			get
			{
				return (RecipientStatus)base.List[index];
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000B3DD File Offset: 0x0000A3DD
		internal RecipientStatusCollection()
		{
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000B3E5 File Offset: 0x0000A3E5
		internal void a(RecipientStatus A_0)
		{
			base.List.Add(A_0);
		}
	}
}
