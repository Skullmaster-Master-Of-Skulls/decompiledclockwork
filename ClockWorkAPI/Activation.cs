using System;
using System.Collections.Generic;
using System.Data;
using EncryptionClassLibrary;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x02000062 RID: 98
	public class Activation
	{
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0001CC1C File Offset: 0x0001BC1C
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x0001CC34 File Offset: 0x0001BC34
		public int PeoplePreviousYearsId
		{
			get
			{
				return this.peoplePreviousYearsId;
			}
			set
			{
				this.peoplePreviousYearsId = value;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0001CC40 File Offset: 0x0001BC40
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x0001CC58 File Offset: 0x0001BC58
		public int Pid
		{
			get
			{
				return this.pid;
			}
			set
			{
				this.pid = value;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0001CC64 File Offset: 0x0001BC64
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x0001CC7C File Offset: 0x0001BC7C
		public string FirstName
		{
			get
			{
				return this.firstName;
			}
			set
			{
				this.firstName = value;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x0001CC88 File Offset: 0x0001BC88
		// (set) Token: 0x0600057F RID: 1407 RVA: 0x0001CCA0 File Offset: 0x0001BCA0
		public string LastName
		{
			get
			{
				return this.lastName;
			}
			set
			{
				this.lastName = value;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0001CCAC File Offset: 0x0001BCAC
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x0001CCC4 File Offset: 0x0001BCC4
		public string Student_no
		{
			get
			{
				return this.student_no;
			}
			set
			{
				this.student_no = value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0001CCD0 File Offset: 0x0001BCD0
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x0001CCE8 File Offset: 0x0001BCE8
		public DateTime ActivationDate
		{
			get
			{
				return this.activationDate;
			}
			set
			{
				this.activationDate = value;
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001CCF2 File Offset: 0x0001BCF2
		public Activation()
		{
			this.Clear();
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001CD04 File Offset: 0x0001BD04
		private void Clear()
		{
			this.pid = 0;
			this.firstName = "";
			this.lastName = "";
			this.student_no = "";
			this.activationDate = DateTime.Now;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001CD3C File Offset: 0x0001BD3C
		public Activation(DataRow dr)
		{
			if (dr["personid"] == DBNull.Value)
			{
				this.Clear();
			}
			else
			{
				DataTable table = dr.Table;
				this.pid = (int)dr["personid"];
				this.peoplePreviousYearsId = ((table != null && table.Columns.Contains("peoplepreviousyearsid")) ? ((dr["peoplepreviousyearsid"] == DBNull.Value) ? 0 : ((int)dr["peoplepreviousyearsid"])) : 0);
				this.firstName = ((table != null && table.Columns.Contains("firstname")) ? dr["firstname"].ToString() : "");
				this.lastName = ((table != null && table.Columns.Contains("lastname")) ? dr["lastname"].ToString() : "");
				this.student_no = ((table != null && table.Columns.Contains("student_no")) ? dr["student_no"].ToString() : "");
				this.activationDate = ((dr["dateactive"] == DBNull.Value) ? DateTime.Now : ((DateTime)dr["dateactive"]));
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001CEA0 File Offset: 0x0001BEA0
		public bool DeleteActivationFromDatabase(UnivDataAdapter da)
		{
			bool result;
			if (this.peoplePreviousYearsId < 1)
			{
				result = false;
			}
			else
			{
				string commandText = "DELETE FROM peoplepreviousyears WHERE peoplepreviousyearsid=@id;\r\nSELECT personid FROM peoplepreviousyears WHERE peoplepreviousyearsid=@id";
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@id", this.peoplePreviousYearsId);
				DataTable dataTable = new DataTable();
				da.Fill(dataTable);
				result = (dataTable.Rows.Count < 1);
			}
			return result;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001CF28 File Offset: 0x0001BF28
		public static List<Activation> LoadActivations(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int pid, bool showAllStudents)
		{
			string commandText = "SELECT pp.peoplepreviousyearsid,pp.personid\r\n        ,p.firstname,p.lastname,p.student_no\r\n        ,pp.dateactive\r\nFROM    peoplepreviousyears pp LEFT JOIN people p ON p.personid=pp.personid\r\nWHERE   pp.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1)\r\n        AND p.isactive=1\r\n        AND (@showallstudents=1 OR pp.personid=@pid)";
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@showallstudents", showAllStudents);
			da.SelectCommand.Parameters.Add("@pid", pid);
			DataTable dataTable = new DataTable();
			string text;
			da.Fill(dataTable, out text);
			dataTable = tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
			{
				"firstname",
				"lastname",
				"student_no"
			});
			List<Activation> list = new List<Activation>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dr = (DataRow)obj;
				Activation item = new Activation(dr);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x04000219 RID: 537
		private int peoplePreviousYearsId;

		// Token: 0x0400021A RID: 538
		private int pid;

		// Token: 0x0400021B RID: 539
		private string firstName;

		// Token: 0x0400021C RID: 540
		private string lastName;

		// Token: 0x0400021D RID: 541
		private string student_no;

		// Token: 0x0400021E RID: 542
		private DateTime activationDate;
	}
}
