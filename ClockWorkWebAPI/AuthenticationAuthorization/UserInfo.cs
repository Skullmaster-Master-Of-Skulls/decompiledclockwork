using System;
using System.Data;
using System.Data.Common;
using Databases;

namespace ClockWorkWebAPI.AuthenticationAuthorization
{
	// Token: 0x0200007B RID: 123
	[Serializable]
	public class UserInfo
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00028900 File Offset: 0x00026B00
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x00028918 File Offset: 0x00026B18
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

		// Token: 0x0600061F RID: 1567 RVA: 0x00028924 File Offset: 0x00026B24
		public bool IsUserInGroup_LoadFromDbFirstTimeIfNecessary(int pid, GroupMembership gm, db conn)
		{
			return this.IsUserInGroup_LoadFromDbFirstTimeIfNecessary(pid, gm);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00028940 File Offset: 0x00026B40
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
					DbParameter[] array = new DbParameter[2];
					array[0] = clockWork.Parameter;
					array[0].ParameterName = "@pid";
					array[0].DbType = DbType.Int32;
					array[0].Value = pid;
					array[1] = clockWork.Parameter;
					array[1].ParameterName = "@gid";
					array[1].DbType = DbType.Int32;
					array[1].Value = num;
					DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_Select_GroupMembership, array);
					result = (dataTable.Rows.Count > 0);
				}
			}
			return result;
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00028A58 File Offset: 0x00026C58
		// (set) Token: 0x06000622 RID: 1570 RVA: 0x00028A70 File Offset: 0x00026C70
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

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x00028A7C File Offset: 0x00026C7C
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x00028A94 File Offset: 0x00026C94
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

		// Token: 0x06000625 RID: 1573 RVA: 0x00028AA0 File Offset: 0x00026CA0
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

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00028B14 File Offset: 0x00026D14
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x00028B2C File Offset: 0x00026D2C
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

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00028B38 File Offset: 0x00026D38
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x00028B50 File Offset: 0x00026D50
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

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x00028B5C File Offset: 0x00026D5C
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x00028B74 File Offset: 0x00026D74
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

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x00028B80 File Offset: 0x00026D80
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x00028B98 File Offset: 0x00026D98
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

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x00028BA4 File Offset: 0x00026DA4
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x00028BBC File Offset: 0x00026DBC
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

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00028BC8 File Offset: 0x00026DC8
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x00028BE0 File Offset: 0x00026DE0
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

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00028BEC File Offset: 0x00026DEC
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x00028C04 File Offset: 0x00026E04
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

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00028C10 File Offset: 0x00026E10
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x00028C28 File Offset: 0x00026E28
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

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00028C34 File Offset: 0x00026E34
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x00028C4C File Offset: 0x00026E4C
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

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00028C58 File Offset: 0x00026E58
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x00028C70 File Offset: 0x00026E70
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

		// Token: 0x0600063A RID: 1594 RVA: 0x00028C7C File Offset: 0x00026E7C
		public bool IsMember(GroupMembership groupMembership)
		{
			return (this.groupMemberships & groupMembership) == groupMembership;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00028C99 File Offset: 0x00026E99
		public void ClearGroupMemberships()
		{
			this.groupMemberships = GroupMembership.unknown;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00028CA3 File Offset: 0x00026EA3
		public void AddGroupMembership(GroupMembership groupMembership)
		{
			this.groupMemberships |= groupMembership;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00028CB4 File Offset: 0x00026EB4
		public void RemoveGroupMembership(GroupMembership groupMembership)
		{
			bool flag = this.IsMember(groupMembership);
			if (flag)
			{
				this.groupMemberships ^= groupMembership;
			}
		}

		// Token: 0x04000332 RID: 818
		private string username;

		// Token: 0x04000333 RID: 819
		private string displayName;

		// Token: 0x04000334 RID: 820
		private string email;

		// Token: 0x04000335 RID: 821
		private GroupMembership groupMemberships;

		// Token: 0x04000336 RID: 822
		private int clockworkPid;

		// Token: 0x04000337 RID: 823
		private int clockworkNid;

		// Token: 0x04000338 RID: 824
		private int clockworkIid;

		// Token: 0x04000339 RID: 825
		private int externalClockWorkPid;

		// Token: 0x0400033A RID: 826
		private int clockworkAltContactId = 0;

		// Token: 0x0400033B RID: 827
		private string studentNumber;

		// Token: 0x0400033C RID: 828
		private int staffPid = 0;

		// Token: 0x0400033D RID: 829
		private AuthenticationMethod authenticationMethod = null;

		// Token: 0x0400033E RID: 830
		private GroupMembership authenticationGroupMembership = GroupMembership.unknown;

		// Token: 0x0400033F RID: 831
		private GroupMembership loadedGroupMemberships = GroupMembership.unknown;
	}
}
