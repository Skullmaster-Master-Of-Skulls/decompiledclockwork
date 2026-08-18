using System;
using System.Data.SqlClient;

namespace TechnoPro.Common.Public.Entities.Database
{
	// Token: 0x02000194 RID: 404
	public class DbConnectionInfo : BusinessBase<string>
	{
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x000133B8 File Offset: 0x000115B8
		public override string Id
		{
			get
			{
				return string.Format("{0}->{1}", this.DataSource, this.InitialCatalog);
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x000133E0 File Offset: 0x000115E0
		// (set) Token: 0x06000A32 RID: 2610 RVA: 0x00013400 File Offset: 0x00011600
		public string DataSource
		{
			get
			{
				return this.ConnectionStringBuilder.DataSource;
			}
			set
			{
				bool flag = !string.IsNullOrEmpty(value) && this.DataSource != value;
				if (flag)
				{
					this.ConnectionStringBuilder.DataSource = value;
				}
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x00013438 File Offset: 0x00011638
		// (set) Token: 0x06000A34 RID: 2612 RVA: 0x00013458 File Offset: 0x00011658
		public string InitialCatalog
		{
			get
			{
				return this.ConnectionStringBuilder.InitialCatalog;
			}
			set
			{
				bool flag = !string.IsNullOrEmpty(value) && this.InitialCatalog != value;
				if (flag)
				{
					this.ConnectionStringBuilder.InitialCatalog = value;
				}
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x00013490 File Offset: 0x00011690
		// (set) Token: 0x06000A36 RID: 2614 RVA: 0x000134B0 File Offset: 0x000116B0
		public string UserId
		{
			get
			{
				return this.ConnectionStringBuilder.UserID;
			}
			set
			{
				bool flag = !string.IsNullOrEmpty(value) && this.UserId != value;
				if (flag)
				{
					this.ConnectionStringBuilder.UserID = value;
				}
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x000134E8 File Offset: 0x000116E8
		// (set) Token: 0x06000A38 RID: 2616 RVA: 0x00013508 File Offset: 0x00011708
		public string Password
		{
			get
			{
				return this.ConnectionStringBuilder.Password;
			}
			set
			{
				bool flag = !string.IsNullOrEmpty(value) && this.Password != value;
				if (flag)
				{
					this.ConnectionStringBuilder.Password = value;
				}
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x00013540 File Offset: 0x00011740
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x00013560 File Offset: 0x00011760
		public bool IntegratedSecurity
		{
			get
			{
				return this.ConnectionStringBuilder.IntegratedSecurity;
			}
			set
			{
				bool flag = this.IntegratedSecurity != value;
				if (flag)
				{
					this.ConnectionStringBuilder.IntegratedSecurity = value;
				}
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0001358C File Offset: 0x0001178C
		// (set) Token: 0x06000A3C RID: 2620 RVA: 0x000135AC File Offset: 0x000117AC
		public string ConnectionString
		{
			get
			{
				return this.ConnectionStringBuilder.ToString();
			}
			set
			{
				bool flag = value != null && this.ConnectionString != value;
				if (flag)
				{
					string text = value;
					bool flag2 = text.StartsWith("provider=", StringComparison.OrdinalIgnoreCase);
					if (flag2)
					{
						int num = text.IndexOf(';');
						bool flag3 = num > 0;
						if (flag3)
						{
							text = text.Substring(num + 1);
						}
					}
					this.ConnectionStringBuilder.ConnectionString = text;
				}
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00013610 File Offset: 0x00011810
		// (set) Token: 0x06000A3E RID: 2622 RVA: 0x00013618 File Offset: 0x00011818
		public string DbEncryptionPassword { get; set; }

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x00013621 File Offset: 0x00011821
		// (set) Token: 0x06000A40 RID: 2624 RVA: 0x00013629 File Offset: 0x00011829
		protected SqlConnectionStringBuilder ConnectionStringBuilder { get; set; }

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x00013632 File Offset: 0x00011832
		// (set) Token: 0x06000A42 RID: 2626 RVA: 0x0001363A File Offset: 0x0001183A
		public bool NoDirectDbAccess { get; set; }

		// Token: 0x06000A43 RID: 2627 RVA: 0x00013643 File Offset: 0x00011843
		public DbConnectionInfo()
		{
			this.ConnectionStringBuilder = new SqlConnectionStringBuilder();
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00013659 File Offset: 0x00011859
		public DbConnectionInfo(string cs, string k)
		{
			this.ConnectionStringBuilder = new SqlConnectionStringBuilder();
			this.ConnectionString = cs;
			this.DbEncryptionPassword = k;
		}
	}
}
