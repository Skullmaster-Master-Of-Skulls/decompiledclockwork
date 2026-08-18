using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.People
{
	// Token: 0x02000071 RID: 113
	public class GroupDAO : IGroupDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x00016E88 File Offset: 0x00015088
		public GroupDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00016E9A File Offset: 0x0001509A
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x00016EA2 File Offset: 0x000150A2
		public OperationContext OpContext { get; set; }

		// Token: 0x060002B5 RID: 693 RVA: 0x00016EAC File Offset: 0x000150AC
		public Group LoadGroupById(int GroupId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@gid", DbType.Int32, GroupId)
			};
			Group result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT g.groupid,g.description,g.viewAppsVisible,g.fulldescription,g.ordernum,g.isprimary \r\nFROM groups g \r\n WHERE g.groupid=@gid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = PeopleGroupDAO.GetGroupFromRecord(dataReader);
				}
			}
			return result;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00016F40 File Offset: 0x00015140
		public Group LoadGroupByTitle(string GroupTitle)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@grouptitle", DbType.String, GroupTitle ?? "")
			};
			Group result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT g.groupid,g.description,g.viewAppsVisible,g.fulldescription,g.ordernum,g.isprimary \r\nFROM groups g \r\n WHERE g.description=@grouptitle", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = PeopleGroupDAO.GetGroupFromRecord(dataReader);
				}
			}
			return result;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00016FD8 File Offset: 0x000151D8
		public int CreateGroupByTitle(string GroupTitle)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@gid", DbType.Int32, 0),
				databaseLayer.GetParameter("@grouptitle", DbType.String, GroupTitle ?? "")
			};
			databaseLayer.ExecuteNonQuery("IF EXISTS(SELECT groupid FROM groups WHERE description=@grouptitle)\r\n    SET @gid=(SELECT TOP 1 groupid FROM groups WHERE description=@grouptitle)\r\nELSE \r\nBEGIN\r\n    INSERT INTO groups (description,isprimary,viewappsvisible,fulldescription,ordernum) VALUES (@grouptitle,0,0,'',9999)\r\n    SET @gid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS gid)\r\nEND", array);
			bool flag = array[0].Value == null || array[0].Value == DBNull.Value;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = (int)array[0].Value;
			}
			return result;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00017074 File Offset: 0x00015274
		public int TryToLoadGroupOrCreateFirstIfNoneFound(params string[] groupTitles)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@gid", DbType.Int32, 0),
				databaseLayer.GetParameter("@grouptitle", DbType.String, groupTitles[0]),
				databaseLayer.GetParameter("@grouptitles", DbType.String, string.Join(",", groupTitles))
			};
			databaseLayer.ExecuteNonQuery("SELECT orderid AS [description] INTO #t1 FROM splitstrings2(@grouptitles,',')\r\n\r\nSET @gid = (SELECT TOP 1 groupid FROM groups WHERE [description] IN (SELECT [description] FROM #t1))\r\n\r\nIF (@gid IS NULL OR @gid<1)\r\nBEGIN\r\n    INSERT INTO groups (description,isprimary,viewappsvisible,fulldescription,ordernum) VALUES (@grouptitle,0,0,'',9999)\r\n    SET @gid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS gid)\r\nEND", array);
			bool flag = array[0].Value == null || array[0].Value == DBNull.Value;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = (int)array[0].Value;
			}
			return result;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00017124 File Offset: 0x00015324
		public IList<GroupContainer> LoadAllGroupContainers()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<GroupContainer> list = new List<GroupContainer>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT 0 AS ordernum,'Main' AS fulldescription\r\nUNION\r\nSELECT DISTINCT 1 AS ordernum,fulldescription FROM groups WHERE NOT fulldescription IS NULL AND NOT fulldescription=''\r\nORDER BY ordernum,fulldescription"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return null;
				}
				while (dataReader.Read())
				{
					string fullDescription = dataReader["fulldescription"].ToString().Trim();
					list.Add(new GroupContainer
					{
						FullDescription = fullDescription
					});
				}
			}
			return list;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x000171CC File Offset: 0x000153CC
		private GroupForEdit GetGroupForEditFromRecord(IDataReader record)
		{
			return new GroupForEdit
			{
				GroupId = ((record["groupid"] is DBNull) ? 0 : ((int)record["groupid"])),
				Description = record["description"].ToString().Trim(),
				IsPrimary = (!(record["isprimary"] is DBNull) && (bool)record["isprimary"]),
				ViewAppsVisible = (!(record["viewappsvisible"] is DBNull) && (bool)record["viewappsvisible"]),
				FullDescription = record["fulldescription"].ToString().Trim(),
				OrderNum = ((record["ordernum"] is DBNull) ? 0 : ((int)record["ordernum"]))
			};
		}

		// Token: 0x060002BB RID: 699 RVA: 0x000172D0 File Offset: 0x000154D0
		public IList<GroupForEdit> LoadAllGroupForEdits()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IList<GroupForEdit> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT groupid,description,isprimary,viewappsvisible,fulldescription,ordernum FROM groups ORDER BY ordernum,description"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<GroupForEdit> list = new List<GroupForEdit>();
					while (dataReader.Read())
					{
						list.Add(this.GetGroupForEditFromRecord(dataReader));
					}
					result = list;
				}
			}
			return result;
		}
	}
}
