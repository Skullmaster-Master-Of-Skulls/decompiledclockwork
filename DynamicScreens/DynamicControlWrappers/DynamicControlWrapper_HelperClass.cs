using System;
using System.Data;
using UnivOleDb;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x0200006E RID: 110
	public class DynamicControlWrapper_HelperClass
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00042980 File Offset: 0x00041980
		public DynamicListGroupCollection ListGroups
		{
			get
			{
				return this.listGroups;
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00042998 File Offset: 0x00041998
		public void ReloadGroups()
		{
			if (this.groups != null)
			{
				this.groups.Clear();
				this.groups = null;
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x000429C8 File Offset: 0x000419C8
		public void ReloadLookupListGroups()
		{
			this.listGroups = new DynamicListGroupCollection(this.da);
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x000429DC File Offset: 0x000419DC
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x00042AD8 File Offset: 0x00041AD8
		public NameIntValueCollection Groups
		{
			get
			{
				if (this.groups == null)
				{
					this.da.SelectCommand.CommandText = "SELECT groupid,description FROM groups ORDER BY description";
					DataTable dataTable = new DataTable();
					this.da.Fill(dataTable);
					this.groups = new NameIntValueCollection();
					foreach (object obj in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						int val = (int)dataRow["groupid"];
						string name = (string)dataRow["description"];
						NameIntValue nameIntValue = new NameIntValue(name, val);
						this.groups.Add(nameIntValue);
					}
				}
				return this.groups;
			}
			set
			{
				this.groups = value;
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00042AE2 File Offset: 0x00041AE2
		public DynamicControlWrapper_HelperClass(UnivDataAdapter da)
		{
			this.da = da;
			this.listGroups = new DynamicListGroupCollection(da);
		}

		// Token: 0x04000394 RID: 916
		private UnivDataAdapter da;

		// Token: 0x04000395 RID: 917
		private DynamicListGroupCollection listGroups;

		// Token: 0x04000396 RID: 918
		private NameIntValueCollection groups = null;
	}
}
