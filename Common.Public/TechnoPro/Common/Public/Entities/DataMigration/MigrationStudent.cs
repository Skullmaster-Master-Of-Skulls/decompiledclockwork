using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace TechnoPro.Common.Public.Entities.DataMigration
{
	// Token: 0x02000405 RID: 1029
	public class MigrationStudent : BusinessBase<string>, IMigrationDataItems
	{
		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x00023A98 File Offset: 0x00021C98
		// (set) Token: 0x06001F89 RID: 8073 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public virtual string StudentNumber
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

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06001F8A RID: 8074 RVA: 0x00023AB0 File Offset: 0x00021CB0
		// (set) Token: 0x06001F8B RID: 8075 RVA: 0x00023AB8 File Offset: 0x00021CB8
		public IList<int> ClockWorkGroupIds { get; set; }

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06001F8C RID: 8076 RVA: 0x00023AC1 File Offset: 0x00021CC1
		// (set) Token: 0x06001F8D RID: 8077 RVA: 0x00023AC9 File Offset: 0x00021CC9
		public string FirstName { get; set; }

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06001F8E RID: 8078 RVA: 0x00023AD2 File Offset: 0x00021CD2
		// (set) Token: 0x06001F8F RID: 8079 RVA: 0x00023ADA File Offset: 0x00021CDA
		public string MiddleName { get; set; }

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06001F90 RID: 8080 RVA: 0x00023AE3 File Offset: 0x00021CE3
		// (set) Token: 0x06001F91 RID: 8081 RVA: 0x00023AEB File Offset: 0x00021CEB
		public string LastName { get; set; }

		// Token: 0x06001F92 RID: 8082 RVA: 0x00023AF4 File Offset: 0x00021CF4
		public static MigrationStudent GetMigrationStudentFromDataRow(DataRow dr, string groupIdsCommaSeparatedColName = null)
		{
			bool flag = dr == null;
			MigrationStudent result;
			if (flag)
			{
				result = null;
			}
			else
			{
				MigrationStudent migrationStudent = new MigrationStudent
				{
					StudentNumber = ((dr["student_no"] is DBNull) ? "" : dr["student_no"].ToString().Trim().ToUpper()),
					FirstName = ((dr["firstname"] is DBNull) ? "" : dr["firstname"].ToString().Trim()),
					LastName = ((dr["lastname"] is DBNull) ? "" : dr["lastname"].ToString().Trim()),
					MiddleName = ((!dr.Table.Columns.Contains("middlename") || dr["middlename"] is DBNull) ? "" : dr["middlename"].ToString().Trim()),
					ClockWorkGroupIds = new List<int>()
				};
				bool flag2 = !string.IsNullOrEmpty(groupIdsCommaSeparatedColName) && !(dr[groupIdsCommaSeparatedColName] is DBNull);
				if (flag2)
				{
					string text = dr[groupIdsCommaSeparatedColName].ToString().Trim();
					string[] source = text.Split(new char[]
					{
						','
					}, StringSplitOptions.RemoveEmptyEntries);
					foreach (string text2 in from s in source
					select s.Trim())
					{
						int item;
						bool flag3 = text2.Length > 0 && int.TryParse(text2, out item) && !migrationStudent.ClockWorkGroupIds.Contains(item);
						if (flag3)
						{
							migrationStudent.ClockWorkGroupIds.Add(item);
						}
					}
				}
				result = migrationStudent;
			}
			return result;
		}
	}
}
