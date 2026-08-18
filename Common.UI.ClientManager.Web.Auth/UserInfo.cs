using System;
using System.Data;
using System.Data.Common;
using Databases;

namespace TechnoPro.Common.UI.ClientManager.Web.Auth
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public class UserInfo
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000049C4 File Offset: 0x00002BC4
		// (set) Token: 0x06000063 RID: 99 RVA: 0x000049DC File Offset: 0x00002BDC
		public int StaffPid
		{
			get
			{
				return this.staffPid;
			}
			set
			{
				this.staffPid = value;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000049E8 File Offset: 0x00002BE8
		public bool IsUserInGroup_LoadFromDbFirstTimeIfNecessary(int pid, GroupMembership gm, object conn)
		{
			return this.IsUserInGroup_LoadFromDbFirstTimeIfNecessary(pid, gm);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004A04 File Offset: 0x00002C04
		public bool IsUserInGroup_LoadFromDbFirstTimeIfNecessary(int pid, GroupMembership gm)
		{
			bool flag = (gm & this.groupMemberships) == gm;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = (this.loadedGroupMemberships & gm) == gm;
				if (flag2)
				{
					result = false;
				}
				else
				{
					this.loadedGroupMemberships |= gm;
					int num;
					if (gm != GroupMembership.student)
					{
						if (gm != GroupMembership.staff)
						{
							if (gm != GroupMembership.admin)
							{
								num = 0;
							}
							else
							{
								num = 10;
							}
						}
						else
						{
							num = 2;
						}
					}
					else
					{
						num = 1;
					}
					DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
					DbParameter[] parameters = new DbParameter[]
					{
						clockWork.GetParameter("@pid", DbType.Int32, pid),
						clockWork.GetParameter("@gid", DbType.Int32, num)
					};
					DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_Select_GroupMembership, parameters);
					result = (dataTable.Rows.Count > 0);
				}
			}
			return result;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00004ADC File Offset: 0x00002CDC
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00004AF4 File Offset: 0x00002CF4
		public GroupMembership AuthenticationGroupMembership
		{
			get
			{
				return this.authenticationGroupMembership;
			}
			set
			{
				this.authenticationGroupMembership = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00004B00 File Offset: 0x00002D00
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00004B18 File Offset: 0x00002D18
		public AuthenticationMethod AuthenticationMethod
		{
			get
			{
				return this.authenticationMethod;
			}
			set
			{
				this.authenticationMethod = value;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004B24 File Offset: 0x00002D24
		public UserInfo(string username, string displayName, string email, params GroupMembership[] groupMemberships)
		{
			this.username = username;
			this.email = email;
			this.displayName = displayName;
			this.groupMemberships = GroupMembership.unknown;
			foreach (GroupMembership groupMembership in groupMemberships)
			{
				this.AddGroupMembership(groupMembership);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00004B98 File Offset: 0x00002D98
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00004BB0 File Offset: 0x00002DB0
		public string StudentNumber
		{
			get
			{
				return this.studentNumber;
			}
			set
			{
				this.studentNumber = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00004BBC File Offset: 0x00002DBC
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00004BD4 File Offset: 0x00002DD4
		public string Username
		{
			get
			{
				return this.username;
			}
			set
			{
				this.username = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00004BE0 File Offset: 0x00002DE0
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00004BF8 File Offset: 0x00002DF8
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00004C04 File Offset: 0x00002E04
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00004C1C File Offset: 0x00002E1C
		public string Email
		{
			get
			{
				return this.email;
			}
			set
			{
				this.email = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00004C28 File Offset: 0x00002E28
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00004C40 File Offset: 0x00002E40
		public GroupMembership GroupMemberships
		{
			get
			{
				return this.groupMemberships;
			}
			set
			{
				this.groupMemberships = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00004C4C File Offset: 0x00002E4C
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00004C64 File Offset: 0x00002E64
		public int ClockworkPid
		{
			get
			{
				return this.clockworkPid;
			}
			set
			{
				this.clockworkPid = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00004C70 File Offset: 0x00002E70
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00004C88 File Offset: 0x00002E88
		public int ClockworkNid
		{
			get
			{
				return this.clockworkNid;
			}
			set
			{
				this.clockworkNid = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00004C94 File Offset: 0x00002E94
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00004CAC File Offset: 0x00002EAC
		public int ClockworkIid
		{
			get
			{
				return this.clockworkIid;
			}
			set
			{
				this.clockworkIid = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00004CB8 File Offset: 0x00002EB8
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00004CD0 File Offset: 0x00002ED0
		public int ClockworkAltContactId
		{
			get
			{
				return this.clockworkAltContactId;
			}
			set
			{
				this.clockworkAltContactId = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00004CDC File Offset: 0x00002EDC
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00004CF4 File Offset: 0x00002EF4
		public int ExternalClockWorkPid
		{
			get
			{
				return this.externalClockWorkPid;
			}
			set
			{
				this.externalClockWorkPid = value;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004D00 File Offset: 0x00002F00
		public bool IsMember(GroupMembership groupMembership)
		{
			return (this.groupMemberships & groupMembership) == groupMembership;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004D1D File Offset: 0x00002F1D
		public void ClearGroupMemberships()
		{
			this.groupMemberships = GroupMembership.unknown;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004D27 File Offset: 0x00002F27
		public void AddGroupMembership(GroupMembership groupMembership)
		{
			this.groupMemberships |= groupMembership;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004D38 File Offset: 0x00002F38
		public void RemoveGroupMembership(GroupMembership groupMembership)
		{
			bool flag = this.IsMember(groupMembership);
			if (flag)
			{
				this.groupMemberships ^= groupMembership;
			}
		}

		// Token: 0x0400001B RID: 27
		private string username;

		// Token: 0x0400001C RID: 28
		private string displayName;

		// Token: 0x0400001D RID: 29
		private string email;

		// Token: 0x0400001E RID: 30
		private GroupMembership groupMemberships;

		// Token: 0x0400001F RID: 31
		private int clockworkPid;

		// Token: 0x04000020 RID: 32
		private int clockworkNid;

		// Token: 0x04000021 RID: 33
		private int clockworkIid;

		// Token: 0x04000022 RID: 34
		private int externalClockWorkPid;

		// Token: 0x04000023 RID: 35
		private int clockworkAltContactId = 0;

		// Token: 0x04000024 RID: 36
		private string studentNumber;

		// Token: 0x04000025 RID: 37
		private int staffPid = 0;

		// Token: 0x04000026 RID: 38
		private AuthenticationMethod authenticationMethod = null;

		// Token: 0x04000027 RID: 39
		private GroupMembership authenticationGroupMembership = GroupMembership.unknown;

		// Token: 0x04000028 RID: 40
		private GroupMembership loadedGroupMemberships = GroupMembership.unknown;
	}
}
