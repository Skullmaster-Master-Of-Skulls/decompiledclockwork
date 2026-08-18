using System;
using System.Collections;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000095 RID: 149
	public class RecipientInformationStore
	{
		// Token: 0x060004CD RID: 1229 RVA: 0x0001A6AC File Offset: 0x000196AC
		public RecipientInformationStore(ICollection recipientInfos)
		{
			foreach (object obj in recipientInfos)
			{
				RecipientInformation recipientInformation = (RecipientInformation)obj;
				RecipientID recipientID = recipientInformation.RecipientID;
				ArrayList arrayList = (ArrayList)this.table[recipientID];
				if (arrayList == null)
				{
					arrayList = (this.table[recipientID] = new ArrayList(1));
				}
				arrayList.Add(recipientInformation);
			}
			this.all = new ArrayList(recipientInfos);
		}

		// Token: 0x170000D2 RID: 210
		public RecipientInformation this[RecipientID selector]
		{
			get
			{
				return this.GetFirstRecipient(selector);
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001A760 File Offset: 0x00019760
		public RecipientInformation GetFirstRecipient(RecipientID selector)
		{
			ArrayList arrayList = (ArrayList)this.table[selector];
			if (arrayList != null)
			{
				return (RecipientInformation)arrayList[0];
			}
			return null;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0001A790 File Offset: 0x00019790
		public int Count
		{
			get
			{
				return this.all.Count;
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0001A79D File Offset: 0x0001979D
		public ICollection GetRecipients()
		{
			return new ArrayList(this.all);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001A7AC File Offset: 0x000197AC
		public ICollection GetRecipients(RecipientID selector)
		{
			ArrayList arrayList = (ArrayList)this.table[selector];
			if (arrayList != null)
			{
				return new ArrayList(arrayList);
			}
			return new ArrayList();
		}

		// Token: 0x0400026B RID: 619
		private readonly ArrayList all;

		// Token: 0x0400026C RID: 620
		private readonly Hashtable table = new Hashtable();
	}
}
