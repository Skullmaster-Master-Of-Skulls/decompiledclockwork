using System;
using System.Collections;
using System.Data;
using DynamicScreens.DynamicControlWrappers.TypeConverters;
using UnivOleDb;

namespace DynamicScreens
{
	// Token: 0x02000040 RID: 64
	public class DynamicListGroupCollection : CollectionBase
	{
		// Token: 0x060003C2 RID: 962 RVA: 0x000335E8 File Offset: 0x000325E8
		public int Add(DynamicListGroup listGroup)
		{
			return base.List.Add(listGroup);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00033608 File Offset: 0x00032608
		public DynamicListGroup Add(string description)
		{
			DynamicListGroup dynamicListGroup = new DynamicListGroup(description);
			this.Add(dynamicListGroup);
			return dynamicListGroup;
		}

		// Token: 0x17000115 RID: 277
		public DynamicListGroup this[int index]
		{
			get
			{
				return (DynamicListGroup)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00033660 File Offset: 0x00032660
		public DynamicListGroupCollection(UnivDataAdapter da)
		{
			string commandText;
			if (DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(da, DatabaseVersionManager.ClockWorkFeature.LookupGroupVisible))
			{
				commandText = "SELECT lg.lookupgroupid,lg.description,lg.sortby,lg.childlist,ll.lookuplistid,ll.lookuptext,ll.ordernum,ll.lookupvalue,ll.visible,ll.children FROM lookupgroups lg LEFT JOIN lookuplists ll ON ll.lookupgroupid=lg.lookupgroupid WHERE isvisible=1 ORDER BY lg.sortby,lg.description,lg.lookupgroupid,ll.ordernum,ll.lookuptext,ll.lookuplistid";
			}
			else
			{
				commandText = "SELECT lg.lookupgroupid,lg.description,lg.sortby,lg.childlist,ll.lookuplistid,ll.lookuptext,ll.ordernum,ll.lookupvalue,ll.visible,ll.children FROM lookupgroups lg LEFT JOIN lookuplists ll ON ll.lookupgroupid=lg.lookupgroupid ORDER BY lg.sortby,lg.description,lg.lookupgroupid,ll.ordernum,ll.lookuptext,ll.lookuplistid";
			}
			da.SelectCommand.CommandText = commandText;
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			HE_GlobalVars.listGroups = new DataTable();
			HE_GlobalVars.listGroups.Columns.Add("groupid", typeof(int));
			HE_GlobalVars.listGroups.Columns.Add("description");
			int i = 0;
			while (i < dataTable.Rows.Count)
			{
				DataRow dataRow = dataTable.Rows[i];
				int num = (int)dataRow["lookupgroupid"];
				int num2 = i++;
				while (i < dataTable.Rows.Count)
				{
					DataRow dataRow2 = dataTable.Rows[i];
					int num3 = (int)dataRow2["lookupgroupid"];
					if (num3 != num)
					{
						break;
					}
					i++;
				}
				DynamicListGroup dynamicListGroup = new DynamicListGroup(dataRow);
				for (int j = num2; j < i; j++)
				{
					dynamicListGroup.Add(new DynamicListItem(dataTable.Rows[j]));
				}
				this.Add(dynamicListGroup);
				HE_GlobalVars.listGroups.Rows.Add(new object[]
				{
					dynamicListGroup.LookupGroupId,
					dynamicListGroup.Description
				});
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00033800 File Offset: 0x00032800
		public DynamicListGroup FindListGroup(int listGroupId)
		{
			foreach (object obj in base.List)
			{
				DynamicListGroup dynamicListGroup = (DynamicListGroup)obj;
				if (dynamicListGroup.LookupGroupId == listGroupId)
				{
					return dynamicListGroup;
				}
			}
			return null;
		}
	}
}
