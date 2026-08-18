using System;

namespace ClockWorkAPI.AT
{
	// Token: 0x020000A0 RID: 160
	public class VendorContact
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x0003037C File Offset: 0x0002F37C
		public int VendorContactId
		{
			get
			{
				return this.vendorContactId;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x00030394 File Offset: 0x0002F394
		// (set) Token: 0x06000806 RID: 2054 RVA: 0x000303AC File Offset: 0x0002F3AC
		public ObjectStatus Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x000303C8 File Offset: 0x0002F3C8
		public void SetDetails(int vendorContactId, string contactName, string title, string phone1, string phone2, string email, string note, string username, string password)
		{
			this.vendorContactId = vendorContactId;
			this.contactName = contactName;
			this.contactTitle = title;
			this.contactPhone1 = phone1;
			this.contactPhone2 = phone2;
			this.contactEmail = email;
			this.contactNote = note;
			this.contactUsername = username;
			this.contactPassword = password;
		}

		// Token: 0x04000405 RID: 1029
		private ObjectStatus status = ObjectStatus.Unknown;

		// Token: 0x04000406 RID: 1030
		private int vendorContactId;

		// Token: 0x04000407 RID: 1031
		private string contactName;

		// Token: 0x04000408 RID: 1032
		private string contactTitle;

		// Token: 0x04000409 RID: 1033
		private string contactPhone1;

		// Token: 0x0400040A RID: 1034
		private string contactPhone2;

		// Token: 0x0400040B RID: 1035
		private string contactEmail;

		// Token: 0x0400040C RID: 1036
		private string contactNote;

		// Token: 0x0400040D RID: 1037
		private string contactUsername;

		// Token: 0x0400040E RID: 1038
		private string contactPassword;
	}
}
