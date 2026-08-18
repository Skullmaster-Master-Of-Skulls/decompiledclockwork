using System;
using System.IdentityModel;

namespace System.ServiceModel.Security
{
	// Token: 0x020002B7 RID: 695
	internal class SendSecurityHeaderElement
	{
		// Token: 0x060015F1 RID: 5617 RVA: 0x00053BCB File Offset: 0x00051DCB
		public SendSecurityHeaderElement(string id, ISecurityElement item)
		{
			this.id = id;
			this.item = item;
			this.markedForEncryption = false;
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x00053BE8 File Offset: 0x00051DE8
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x00053BF0 File Offset: 0x00051DF0
		public ISecurityElement Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x00053BF8 File Offset: 0x00051DF8
		// (set) Token: 0x060015F5 RID: 5621 RVA: 0x00053C00 File Offset: 0x00051E00
		public bool MarkedForEncryption
		{
			get
			{
				return this.markedForEncryption;
			}
			set
			{
				this.markedForEncryption = value;
			}
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00053C09 File Offset: 0x00051E09
		public bool IsSameItem(ISecurityElement item)
		{
			return this.item == item || this.item.Equals(item);
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00053C22 File Offset: 0x00051E22
		public void Replace(string id, ISecurityElement item)
		{
			this.item = item;
			this.id = id;
		}

		// Token: 0x04001B93 RID: 7059
		private string id;

		// Token: 0x04001B94 RID: 7060
		private ISecurityElement item;

		// Token: 0x04001B95 RID: 7061
		private bool markedForEncryption;
	}
}
