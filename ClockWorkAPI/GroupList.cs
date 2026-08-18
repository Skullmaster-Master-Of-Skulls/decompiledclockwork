using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ClockWorkAPI
{
	// Token: 0x0200005E RID: 94
	public class GroupList : List<Group>
	{
		// Token: 0x06000535 RID: 1333 RVA: 0x00019E1C File Offset: 0x00018E1C
		public Group GetGroup(int groupId)
		{
			foreach (Group group in this)
			{
				if (group.GroupId == groupId)
				{
					return group;
				}
			}
			return null;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00019E88 File Offset: 0x00018E88
		public Group AddGroup(DataRow dr)
		{
			Group group = new Group(dr);
			base.Add(group);
			return group;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00019EAC File Offset: 0x00018EAC
		public string GroupIdsCommaSeparated
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (Group group in this)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(group.GroupId.ToString());
				}
				return stringBuilder.ToString();
			}
		}
	}
}
