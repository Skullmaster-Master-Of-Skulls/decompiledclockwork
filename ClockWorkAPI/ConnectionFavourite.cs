using System;

namespace ClockWorkAPI
{
	// Token: 0x0200007C RID: 124
	public class ConnectionFavourite
	{
		// Token: 0x0600065D RID: 1629 RVA: 0x00023FF8 File Offset: 0x00022FF8
		public ConnectionFavourite()
		{
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00024003 File Offset: 0x00023003
		public ConnectionFavourite(string name, string connectionString, string password)
		{
			this.name = name;
			this.connectionString = connectionString;
			this.password = password;
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00024024 File Offset: 0x00023024
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x0002403C File Offset: 0x0002303C
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00024048 File Offset: 0x00023048
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x00024060 File Offset: 0x00023060
		public string ConnectionString
		{
			get
			{
				return this.connectionString;
			}
			set
			{
				this.connectionString = value;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0002406C File Offset: 0x0002306C
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x00024084 File Offset: 0x00023084
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x0400033E RID: 830
		private string name;

		// Token: 0x0400033F RID: 831
		private string connectionString;

		// Token: 0x04000340 RID: 832
		private string password;
	}
}
