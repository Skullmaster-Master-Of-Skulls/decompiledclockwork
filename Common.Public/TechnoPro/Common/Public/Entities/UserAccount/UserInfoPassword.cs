using System;

namespace TechnoPro.Common.Public.Entities.UserAccount
{
	// Token: 0x02000138 RID: 312
	public class UserInfoPassword : BusinessBase<int, string>
	{
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x00010698 File Offset: 0x0000E898
		// (set) Token: 0x06000770 RID: 1904 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int PersonId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x000106B0 File Offset: 0x0000E8B0
		// (set) Token: 0x06000772 RID: 1906 RVA: 0x000106C8 File Offset: 0x0000E8C8
		public virtual string UserName
		{
			get
			{
				return this.SecondId;
			}
			set
			{
				this.SecondId = value;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x000106D3 File Offset: 0x0000E8D3
		// (set) Token: 0x06000774 RID: 1908 RVA: 0x000106DB File Offset: 0x0000E8DB
		public string Password { get; set; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x000106E4 File Offset: 0x0000E8E4
		// (set) Token: 0x06000776 RID: 1910 RVA: 0x000106EC File Offset: 0x0000E8EC
		public bool RequiresPasswordChange { get; set; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x000106F5 File Offset: 0x0000E8F5
		// (set) Token: 0x06000778 RID: 1912 RVA: 0x000106FD File Offset: 0x0000E8FD
		public DateTime LastPasswordChangeDate { get; set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x00010706 File Offset: 0x0000E906
		// (set) Token: 0x0600077A RID: 1914 RVA: 0x0001070E File Offset: 0x0000E90E
		public DateTime? PasswordExpiryDate { get; set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x00010717 File Offset: 0x0000E917
		// (set) Token: 0x0600077C RID: 1916 RVA: 0x0001071F File Offset: 0x0000E91F
		public bool IsEncrypted { get; set; }
	}
}
