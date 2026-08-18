using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.People
{
	// Token: 0x02000070 RID: 112
	public class AdminGroupDAO : IAdminGroupDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		public AdminGroupDAO()
		{
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0001684C File Offset: 0x00014A4C
		public AdminGroupDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0001685E File Offset: 0x00014A5E
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x00016866 File Offset: 0x00014A66
		public OperationContext OpContext { get; set; }

		// Token: 0x060002AA RID: 682 RVA: 0x00016870 File Offset: 0x00014A70
		public int CreateGroup(Group group)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@groupid", DbType.Int32, 0),
				databaseLayer.GetParameter("@description", DbType.String, group.Description ?? ""),
				databaseLayer.GetParameter("@ordernum", DbType.Int32, group.OrderNum),
				databaseLayer.GetParameter("@fulldescription", DbType.String, group.FullDescription ?? ""),
				databaseLayer.GetParameter("@visibleincalendar", DbType.Boolean, group.VisibleInCalendar)
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO Groups ([description],isPrimary,viewAppsVisible,fullDescription,ordernum) \r\nVALUES (@description,0,@visibleincalendar,@fulldescription,@ordernum)\r\nSET @groupid = (SELECT CAST(SCOPE_IDENTITY() AS int) AS groupid)", array);
			DbParameter dbParameter = array[0];
			object obj = (dbParameter != null) ? dbParameter.Value : null;
			bool flag = obj == null || obj is DBNull;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = (int)obj;
			}
			return result;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00016964 File Offset: 0x00014B64
		public void UpdateGroup(Group group)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@groupid", DbType.Int32, group.GroupId),
				databaseLayer.GetParameter("@description", DbType.String, group.Description ?? ""),
				databaseLayer.GetParameter("@ordernum", DbType.Int32, group.OrderNum),
				databaseLayer.GetParameter("@fulldescription", DbType.String, group.FullDescription ?? ""),
				databaseLayer.GetParameter("@visibleincalendar", DbType.Boolean, group.VisibleInCalendar)
			};
			databaseLayer.ExecuteNonQuery("UPDATE Groups SET [description]=@description,fullDescription=@fulldescription,ordernum=@ordernum,viewAppsVisible=@visibleincalendar WHERE groupid=@groupid", parameters);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00016A30 File Offset: 0x00014C30
		public void DeleteGroup(int groupId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@groupid", DbType.Int32, groupId)
			};
			databaseLayer.ExecuteNonQuery("DELETE FROM Groups WHERE GroupId=@groupid AND NOT EXISTS(SELECT groupid FROM peoplegroups WHERE groupid=@groupid)", parameters);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00016A84 File Offset: 0x00014C84
		public void UpdateGroupOrder(int groupId, int newOrderNum)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@groupid", DbType.Int32, groupId),
				databaseLayer.GetParameter("@ordernum", DbType.Int32, newOrderNum)
			};
			databaseLayer.ExecuteNonQuery("UPDATE Groups SET ordernum=@ordernum WHERE groupid=@groupid", parameters);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00016AEC File Offset: 0x00014CEC
		public void UpdateGroupContainerTitle(string oldContainerTitle, string newContainerTitle)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@oldFullDescription", DbType.String, oldContainerTitle),
				databaseLayer.GetParameter("@newFullDescription", DbType.String, newContainerTitle)
			};
			databaseLayer.ExecuteNonQuery("UPDATE Groups SET FullDescription=@newFullDescription WHERE FullDescription=@oldFullDescription", parameters);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00016B4C File Offset: 0x00014D4C
		public void AddMembersToGroup(int groupId, int[] pids)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[2];
			array[0] = databaseLayer.GetParameter("@gid", DbType.Int32, groupId);
			array[1] = databaseLayer.GetParameter("@pids", DbType.String, string.Join(",", (from g in pids.Distinct<int>()
			select g.ToString()).ToArray<string>()));
			DbParameter[] parameters = array;
			databaseLayer.ExecuteNonQuery("DECLARE @isPrimaryGroup bit = CASE WHEN @gid>0 AND @gid<5 THEN CAST(1 AS BIT) ELSE CAST(0 AS bit) END\r\nINSERT INTO peoplegroups(personid,groupid,isprimarygroup)\r\n\tSELECT orderid AS personid,@gid,@isPrimaryGroup FROM SplitOrderIDs(@pids,',') WHERE orderid>0 AND NOT orderid IN (SELECT personid AS orderid FROM peoplegroups WHERE groupid=@gid)", parameters);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00016BE8 File Offset: 0x00014DE8
		public void RemoveMembersFromGroup(int groupId, int[] pids)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[2];
			array[0] = databaseLayer.GetParameter("@gid", DbType.Int32, groupId);
			array[1] = databaseLayer.GetParameter("@pids", DbType.String, string.Join(",", (from g in pids.Distinct<int>()
			select g.ToString()).ToArray<string>()));
			DbParameter[] parameters = array;
			databaseLayer.ExecuteNonQuery("DELETE FROM peoplegroups WHERE groupid=@gid AND personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))", parameters);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00016C84 File Offset: 0x00014E84
		public IList<PersonBase> LoadGroupMembers(bool onlyShowDeleted, params int[] gids)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[2];
			array[0] = databaseLayer.GetParameter("@gids", DbType.String, string.Join(",", (from g in gids ?? new int[0]
			select g.ToString()).ToArray<string>()));
			array[1] = databaseLayer.GetParameter("@personIsActive", DbType.Boolean, !onlyShowDeleted);
			DbParameter[] parameters = array;
			IList<PersonBase> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT   DISTINCT p.personid,p.Firstname,p.Lastname,p.Student_no,pg.groupid,g.Description AS GroupMemberships \r\nFROM    people p LEFT JOIN peoplegroups pg ON pg.personid=p.personid \r\n        LEFT JOIN groups g ON g.groupid=pg.groupid \r\nWHERE   p.isactive=@personIsActive\r\n        AND (@gids='' \r\n                OR p.personid IN (SELECT personid FROM peoplegroups WHERE groupid IN (SELECT orderid AS groupid FROM splitorderids(@gids,',')))\r\n        ) \r\nORDER BY p.personid,g.description", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<PersonBase> list = new List<PersonBase>();
					bool flag2 = !dataReader.Read();
					if (flag2)
					{
						result = list;
					}
					else
					{
						IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
						for (;;)
						{
							PersonBase personBaseFromReader = PeopleDAO.GetPersonBaseFromReader<PersonBase>("", dataReader, this.OpContext, batchDecryptor);
							bool flag3 = personBaseFromReader == null || personBaseFromReader.PersonId < 1;
							if (!flag3)
							{
								bool flag4 = personBaseFromReader.Groups == null;
								if (flag4)
								{
									personBaseFromReader.Groups = new List<Group>();
								}
								list.Add(personBaseFromReader);
								int personId = personBaseFromReader.PersonId;
								bool flag5 = false;
								for (;;)
								{
									bool flag6 = !dataReader.Read();
									if (flag6)
									{
										goto Block_11;
									}
									int num = (dataReader["personid"] is DBNull) ? 0 : ((int)dataReader["personid"]);
									bool flag7 = num != personId;
									if (flag7)
									{
										break;
									}
									Group groupFromRecord = PeopleGroupDAO.GetGroupFromRecord(dataReader);
									bool flag8 = groupFromRecord == null;
									if (!flag8)
									{
										personBaseFromReader.Groups.Add(groupFromRecord);
									}
								}
								IL_1B4:
								bool flag9 = flag5;
								if (flag9)
								{
									break;
								}
								continue;
								goto IL_1B4;
								Block_11:
								flag5 = true;
								goto IL_1B4;
							}
						}
						result = list;
					}
				}
			}
			return result;
		}
	}
}
