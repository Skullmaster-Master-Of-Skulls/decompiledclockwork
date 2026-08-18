using System;
using System.Data;
using UnivOleDb;

namespace DynamicScreens
{
	// Token: 0x0200006C RID: 108
	public class StudentData
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00042604 File Offset: 0x00041604
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x0004261C File Offset: 0x0004161C
		public bool IsEmpty
		{
			get
			{
				return this.isEmpty;
			}
			set
			{
				this.isEmpty = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00042628 File Offset: 0x00041628
		public DataTable MainInfo
		{
			get
			{
				return this.mainInfo;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00042640 File Offset: 0x00041640
		public DataTable OtherInfo
		{
			get
			{
				return this.otherInfo;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00042658 File Offset: 0x00041658
		public DataTable DateTimeInfo
		{
			get
			{
				return this.dateTimeInfo;
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00042670 File Offset: 0x00041670
		public StudentData(UnivDataAdapter da, int personid, string cids)
		{
			this.LoadStudentData(da, personid, cids);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0004268C File Offset: 0x0004168C
		public StudentData()
		{
			this.mainInfo = null;
			this.otherInfo = null;
			this.dateTimeInfo = null;
			this.isEmpty = true;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000426BC File Offset: 0x000416BC
		public string LoadStudentData(UnivDataAdapter da, int personid, string controlids)
		{
			this.mainInfo = new DataTable("maininfo");
			this.otherInfo = new DataTable("otherinfo");
			this.dateTimeInfo = new DataTable("datetimeinfo");
			da.SelectCommand.CommandText = "SELECT dataID,screenNum,personID,controlID,controlValue FROM maininfops WHERE controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')) AND personid=@personid";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@cids", controlids);
			da.SelectCommand.Parameters.Add("@personid", personid);
			string result;
			da.Fill(this.mainInfo, out result);
			da.SelectCommand.CommandText = "SELECT dataID,screenNum,personID,controlID,controlValue FROM otherinfops WHERE controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')) AND personid=@personid";
			da.Fill(this.otherInfo, out result);
			da.SelectCommand.CommandText = "SELECT dataID,screenNum,personID,controlID,controlValue FROM datetimeinfops WHERE controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')) AND personid=@personid";
			da.Fill(this.dateTimeInfo, out result);
			this.isEmpty = false;
			return result;
		}

		// Token: 0x04000390 RID: 912
		private DataTable mainInfo;

		// Token: 0x04000391 RID: 913
		private DataTable otherInfo;

		// Token: 0x04000392 RID: 914
		private DataTable dateTimeInfo;

		// Token: 0x04000393 RID: 915
		private bool isEmpty = true;
	}
}
