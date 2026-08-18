using System;
using System.Collections.Generic;
using Microsoft.Exchange.WebServices.Data;

namespace MailBee.EwsMail
{
	// Token: 0x02000525 RID: 1317
	public class EwsItemList : List<EwsItem>
	{
		// Token: 0x06002B4E RID: 11086 RVA: 0x000CC6CE File Offset: 0x000CB6CE
		internal EwsItemList(FindItemsResults<Item> A_0)
		{
			this.a = A_0;
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06002B4F RID: 11087 RVA: 0x000CC6DD File Offset: 0x000CB6DD
		public bool MoreAvailable
		{
			get
			{
				return this.a != null && this.a.MoreAvailable;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06002B50 RID: 11088 RVA: 0x000CC6F4 File Offset: 0x000CB6F4
		public int NextPageOffset
		{
			get
			{
				if (this.a == null || this.a.NextPageOffset == null)
				{
					return -1;
				}
				return this.a.NextPageOffset.Value;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06002B51 RID: 11089 RVA: 0x000CC733 File Offset: 0x000CB733
		public int TotalCount
		{
			get
			{
				if (this.a != null)
				{
					return this.a.TotalCount;
				}
				return 0;
			}
		}

		// Token: 0x04001DDE RID: 7646
		private FindItemsResults<Item> a;
	}
}
