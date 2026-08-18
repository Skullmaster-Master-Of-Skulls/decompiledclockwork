using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Databases;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.People
{
	// Token: 0x02000072 RID: 114
	public class PeopleGroupDAO : IPeopleGroupDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00017358 File Offset: 0x00015558
		// (set) Token: 0x060002BD RID: 701 RVA: 0x00017360 File Offset: 0x00015560
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x060002BE RID: 702 RVA: 0x00017369 File Offset: 0x00015569
		public PeopleGroupDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0001739A File Offset: 0x0001559A
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x000173A2 File Offset: 0x000155A2
		public OperationContext OpContext { get; set; }

		// Token: 0x060002C1 RID: 705 RVA: 0x000173AC File Offset: 0x000155AC
		public static Group GetGroupFromRecord(IDataReader record)
		{
			object obj = record["groupid"];
			object obj2 = record["description"];
			string fullDescription = record.ContainsColumn("fulldescription") ? record["fulldescription"].ToString().Trim() : "";
			int orderNum = (record.ContainsColumn("ordernum") && !(record["ordernum"] is DBNull)) ? ((int)record["ordernum"]) : 0;
			Group group = new Group
			{
				GroupId = ((obj != DBNull.Value) ? ((int)obj) : 0),
				Description = ((obj2 != DBNull.Value) ? ((string)obj2) : ""),
				FullDescription = fullDescription,
				OrderNum = orderNum
			};
			bool flag = PeopleDAO.ReaderContainsColumn(record, "viewappsvisible");
			if (flag)
			{
				object obj3 = record["viewappsvisible"];
				group.VisibleInCalendar = (obj3 != DBNull.Value && Convert.ToBoolean(obj3));
			}
			return group;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x000174C0 File Offset: 0x000156C0
		public IList<int> GetGroupIdsByPersonId(int PersonId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId)
			};
			IList<int> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT DISTINCT groupid FROM peoplegroups WHERE personid=@pid ORDER BY groupid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<int> list = new List<int>();
					while (dataReader.Read())
					{
						list.Add((int)dataReader["groupid"]);
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00017560 File Offset: 0x00015760
		[DebuggerStepThrough]
		public Task<IList<int>> GetGroupIdsByPersonIdAsync(int PersonId)
		{
			PeopleGroupDAO.<GetGroupIdsByPersonIdAsync>d__11 <GetGroupIdsByPersonIdAsync>d__ = new PeopleGroupDAO.<GetGroupIdsByPersonIdAsync>d__11();
			<GetGroupIdsByPersonIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<int>>.Create();
			<GetGroupIdsByPersonIdAsync>d__.<>4__this = this;
			<GetGroupIdsByPersonIdAsync>d__.PersonId = PersonId;
			<GetGroupIdsByPersonIdAsync>d__.<>1__state = -1;
			<GetGroupIdsByPersonIdAsync>d__.<>t__builder.Start<PeopleGroupDAO.<GetGroupIdsByPersonIdAsync>d__11>(ref <GetGroupIdsByPersonIdAsync>d__);
			return <GetGroupIdsByPersonIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000175AC File Offset: 0x000157AC
		public IList<Group> LoadGroupsById(params int[] GroupIds)
		{
			bool flag = GroupIds == null || GroupIds.Length < 1;
			IList<Group> result;
			if (flag)
			{
				result = new List<Group>();
			}
			else
			{
				DbParameter[] array = new DbParameter[1];
				array[0] = this.DatabaseManager.GetParameter("@gids", DbType.String, string.Join(",", GroupIds.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray()));
				DbParameter[] parameters = array;
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT g.groupid,g.description,g.viewAppsVisible,g.fulldescription,g.ordernum,g.isprimary \r\nFROM groups g \r\nWHERE g.groupid IN (SELECT orderid AS groupid FROM splitorderids(@gids,',')) ORDER BY g.description", parameters))
				{
					bool flag2 = dataReader == null;
					if (flag2)
					{
						result = null;
					}
					else
					{
						List<Group> list = new List<Group>();
						while (dataReader.Read())
						{
							Group groupFromRecord = PeopleGroupDAO.GetGroupFromRecord(dataReader);
							bool flag3 = groupFromRecord != null;
							if (flag3)
							{
								list.Add(groupFromRecord);
							}
						}
						result = list;
					}
				}
			}
			return result;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000176A0 File Offset: 0x000158A0
		public IList<Group> LoadAllGroups()
		{
			IList<Group> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT g.groupid,g.description,g.viewAppsVisible,g.fulldescription,g.ordernum,g.isprimary \r\nFROM groups g \r\n ORDER BY g.description"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<Group> list = new List<Group>();
					while (dataReader.Read())
					{
						Group groupFromRecord = PeopleGroupDAO.GetGroupFromRecord(dataReader);
						bool flag2 = groupFromRecord != null;
						if (flag2)
						{
							list.Add(groupFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00017720 File Offset: 0x00015920
		public int LoadGroupMemberCount(int groupId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@groupid", DbType.Int32, groupId)
			};
			object obj = databaseLayer.ExecuteScalar("SELECT COUNT(personid) AS ct FROM peoplegroups WHERE groupid=@groupid", parameters);
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
	}
}
