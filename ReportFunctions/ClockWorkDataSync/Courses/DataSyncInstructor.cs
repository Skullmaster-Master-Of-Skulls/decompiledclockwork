using System;
using System.Data;
using System.Text;

namespace ReportFunctions.ClockWorkDataSync.Courses
{
	// Token: 0x02000043 RID: 67
	public class DataSyncInstructor
	{
		// Token: 0x060003DF RID: 991 RVA: 0x000448F4 File Offset: 0x000438F4
		public DataSyncInstructor()
		{
			this.IsPrimary = false;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00044908 File Offset: 0x00043908
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x0004491F File Offset: 0x0004391F
		public int Id { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00044928 File Offset: 0x00043928
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x0004493F File Offset: 0x0004393F
		public string Name { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00044948 File Offset: 0x00043948
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x0004495F File Offset: 0x0004395F
		public string FirstName { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00044968 File Offset: 0x00043968
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x0004497F File Offset: 0x0004397F
		public string LastName { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00044988 File Offset: 0x00043988
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x0004499F File Offset: 0x0004399F
		public string Username { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x000449A8 File Offset: 0x000439A8
		// (set) Token: 0x060003EB RID: 1003 RVA: 0x000449BF File Offset: 0x000439BF
		public string Email { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x000449C8 File Offset: 0x000439C8
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x000449DF File Offset: 0x000439DF
		public string Phone { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x000449E8 File Offset: 0x000439E8
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x000449FF File Offset: 0x000439FF
		public bool IsPrimary { get; set; }

		// Token: 0x060003F0 RID: 1008 RVA: 0x00044A08 File Offset: 0x00043A08
		public override bool Equals(object obj)
		{
			bool result;
			if (obj == null)
			{
				result = false;
			}
			else if (obj is DataSyncInstructor)
			{
				DataSyncInstructor dataSyncInstructor = (DataSyncInstructor)obj;
				if (!string.IsNullOrEmpty(this.Username) || !string.IsNullOrEmpty(dataSyncInstructor.Username))
				{
					result = this.Username.Equals(dataSyncInstructor.Username, StringComparison.OrdinalIgnoreCase);
				}
				else if (!string.IsNullOrEmpty(this.Email) || !string.IsNullOrEmpty(dataSyncInstructor.Email))
				{
					result = this.Email.Equals(dataSyncInstructor.Email, StringComparison.OrdinalIgnoreCase);
				}
				else
				{
					result = ((!string.IsNullOrEmpty(this.Name) || !string.IsNullOrEmpty(dataSyncInstructor.Name)) && this.Name.Equals(dataSyncInstructor.Name, StringComparison.OrdinalIgnoreCase));
				}
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00044AEC File Offset: 0x00043AEC
		public DataSyncInstructor(DataRow dr, string colname_id, string colname_name, string colname_email, string colname_username, string colname_phone)
		{
			DataTable table = dr.Table;
			this.Id = ((!string.IsNullOrEmpty(colname_id) && table.Columns.Contains(colname_id)) ? ((dr[colname_id] == DBNull.Value) ? 0 : ((int)dr[colname_id])) : 0);
			this.Name = (table.Columns.Contains(colname_name) ? dr[colname_name].ToString() : "");
			this.Email = (table.Columns.Contains(colname_email) ? dr[colname_email].ToString() : "");
			this.Username = (table.Columns.Contains(colname_username) ? dr[colname_username].ToString() : "");
			this.Phone = (table.Columns.Contains(colname_phone) ? dr[colname_phone].ToString() : "");
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00044BEC File Offset: 0x00043BEC
		public string ToStringHtml()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0} . <a href='mailto:{1}'>{1}</a>", this.Name, this.Email);
			if (!string.IsNullOrEmpty(this.Username))
			{
				stringBuilder.AppendFormat(" . {0}", this.Username);
			}
			if (!string.IsNullOrEmpty(this.Phone))
			{
				stringBuilder.AppendFormat(" . {0}", this.Phone);
			}
			return stringBuilder.ToString();
		}
	}
}
