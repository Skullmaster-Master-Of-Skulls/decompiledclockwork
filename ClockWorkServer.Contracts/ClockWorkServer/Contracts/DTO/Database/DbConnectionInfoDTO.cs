using System;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Database
{
	// Token: 0x0200073F RID: 1855
	[DataContract(Namespace = "http://tpro.ca")]
	public class DbConnectionInfoDTO
	{
		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06002660 RID: 9824 RVA: 0x00011C68 File Offset: 0x0000FE68
		// (set) Token: 0x06002661 RID: 9825 RVA: 0x00011C70 File Offset: 0x0000FE70
		[DataMember]
		public string DataSource { get; set; }

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06002662 RID: 9826 RVA: 0x00011C79 File Offset: 0x0000FE79
		// (set) Token: 0x06002663 RID: 9827 RVA: 0x00011C81 File Offset: 0x0000FE81
		[DataMember]
		public string InitialCatalog { get; set; }

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06002664 RID: 9828 RVA: 0x00011C8A File Offset: 0x0000FE8A
		// (set) Token: 0x06002665 RID: 9829 RVA: 0x00011C92 File Offset: 0x0000FE92
		[DataMember]
		public string UserId { get; set; }

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06002666 RID: 9830 RVA: 0x00011C9B File Offset: 0x0000FE9B
		// (set) Token: 0x06002667 RID: 9831 RVA: 0x00011CA3 File Offset: 0x0000FEA3
		[DataMember]
		public string Password { get; set; }

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06002668 RID: 9832 RVA: 0x00011CAC File Offset: 0x0000FEAC
		// (set) Token: 0x06002669 RID: 9833 RVA: 0x00011CB4 File Offset: 0x0000FEB4
		[DataMember]
		public bool IntegratedSecurity { get; set; }

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x0600266A RID: 9834 RVA: 0x00011CBD File Offset: 0x0000FEBD
		// (set) Token: 0x0600266B RID: 9835 RVA: 0x00011CC5 File Offset: 0x0000FEC5
		[DataMember]
		public string DbEncryptionPassword { get; set; }

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x0600266C RID: 9836 RVA: 0x00011CCE File Offset: 0x0000FECE
		// (set) Token: 0x0600266D RID: 9837 RVA: 0x00011CD6 File Offset: 0x0000FED6
		private SqlConnectionStringBuilder ConnectionStringBuilder { get; set; }

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x0600266E RID: 9838 RVA: 0x00011CDF File Offset: 0x0000FEDF
		// (set) Token: 0x0600266F RID: 9839 RVA: 0x00011CE7 File Offset: 0x0000FEE7
		[DataMember]
		public bool NoDirectDbAccess { get; set; }

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06002670 RID: 9840 RVA: 0x00011CF0 File Offset: 0x0000FEF0
		// (set) Token: 0x06002671 RID: 9841 RVA: 0x00011D80 File Offset: 0x0000FF80
		public string ConnectionString
		{
			get
			{
				bool flag = this.ConnectionStringBuilder == null;
				if (flag)
				{
					this.ConnectionStringBuilder = new SqlConnectionStringBuilder();
				}
				this.ConnectionStringBuilder.DataSource = this.DataSource;
				this.ConnectionStringBuilder.InitialCatalog = this.InitialCatalog;
				this.ConnectionStringBuilder.UserID = this.UserId;
				this.ConnectionStringBuilder.Password = this.Password;
				this.ConnectionStringBuilder.IntegratedSecurity = this.IntegratedSecurity;
				return this.ConnectionStringBuilder.ConnectionString;
			}
			set
			{
				bool flag = this.ConnectionStringBuilder == null;
				if (flag)
				{
					this.ConnectionStringBuilder = new SqlConnectionStringBuilder();
				}
				this.ConnectionStringBuilder.ConnectionString = value;
				this.DataSource = this.ConnectionStringBuilder.DataSource;
				this.InitialCatalog = this.ConnectionStringBuilder.InitialCatalog;
				this.UserId = this.ConnectionStringBuilder.UserID;
				this.Password = this.ConnectionStringBuilder.Password;
				this.IntegratedSecurity = this.ConnectionStringBuilder.IntegratedSecurity;
			}
		}
	}
}
