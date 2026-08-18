using System;

namespace TechnoPro.Common.Public.Entities.AppointmentSync
{
	// Token: 0x020004DF RID: 1247
	public class SyncApplicationConnection
	{
		// Token: 0x06002596 RID: 9622 RVA: 0x0002845D File Offset: 0x0002665D
		public SyncApplicationConnection()
		{
			this.UserCredentials = new SyncApplicationConnection.Credentials();
			this.ServiceCredentials = new SyncApplicationConnection.ServiceAccountCredentials();
		}

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06002597 RID: 9623 RVA: 0x0002847F File Offset: 0x0002667F
		// (set) Token: 0x06002598 RID: 9624 RVA: 0x00028487 File Offset: 0x00026687
		public SyncApplicationConnection.Credentials UserCredentials { get; set; }

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06002599 RID: 9625 RVA: 0x00028490 File Offset: 0x00026690
		// (set) Token: 0x0600259A RID: 9626 RVA: 0x00028498 File Offset: 0x00026698
		public SyncApplicationConnection.ServiceAccountCredentials ServiceCredentials { get; set; }

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x0600259B RID: 9627 RVA: 0x000284A1 File Offset: 0x000266A1
		// (set) Token: 0x0600259C RID: 9628 RVA: 0x000284A9 File Offset: 0x000266A9
		public string ApplicationUrl { get; set; }

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x0600259D RID: 9629 RVA: 0x000284B2 File Offset: 0x000266B2
		// (set) Token: 0x0600259E RID: 9630 RVA: 0x000284BA File Offset: 0x000266BA
		public string ApplicationVersion { get; set; }

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x0600259F RID: 9631 RVA: 0x000284C3 File Offset: 0x000266C3
		// (set) Token: 0x060025A0 RID: 9632 RVA: 0x000284CB File Offset: 0x000266CB
		public bool UseAutoDiscoverUrl { get; set; }

		// Token: 0x02000612 RID: 1554
		public class Credentials
		{
			// Token: 0x06003160 RID: 12640 RVA: 0x00044FE0 File Offset: 0x000431E0
			public Credentials()
			{
				this.Username = string.Empty;
				this.Password = string.Empty;
			}

			// Token: 0x170013FB RID: 5115
			// (get) Token: 0x06003161 RID: 12641 RVA: 0x00045002 File Offset: 0x00043202
			// (set) Token: 0x06003162 RID: 12642 RVA: 0x0004500A File Offset: 0x0004320A
			public string Username { get; set; }

			// Token: 0x170013FC RID: 5116
			// (get) Token: 0x06003163 RID: 12643 RVA: 0x00045013 File Offset: 0x00043213
			// (set) Token: 0x06003164 RID: 12644 RVA: 0x0004501B File Offset: 0x0004321B
			public string Password { get; set; }
		}

		// Token: 0x02000613 RID: 1555
		public class ServiceAccountCredentials
		{
			// Token: 0x170013FD RID: 5117
			// (get) Token: 0x06003165 RID: 12645 RVA: 0x00045024 File Offset: 0x00043224
			// (set) Token: 0x06003166 RID: 12646 RVA: 0x0004502C File Offset: 0x0004322C
			public string ServiceClientId { get; set; }

			// Token: 0x170013FE RID: 5118
			// (get) Token: 0x06003167 RID: 12647 RVA: 0x00045035 File Offset: 0x00043235
			// (set) Token: 0x06003168 RID: 12648 RVA: 0x0004503D File Offset: 0x0004323D
			public string ServiceAccountEmail { get; set; }

			// Token: 0x170013FF RID: 5119
			// (get) Token: 0x06003169 RID: 12649 RVA: 0x00045046 File Offset: 0x00043246
			// (set) Token: 0x0600316A RID: 12650 RVA: 0x0004504E File Offset: 0x0004324E
			public string ServiceAccountPKCS12Filename { get; set; }
		}
	}
}
