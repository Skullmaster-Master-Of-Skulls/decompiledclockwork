using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl
{
	// Token: 0x02000016 RID: 22
	public class CampusDAO : ICampusDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00004442 File Offset: 0x00002642
		// (set) Token: 0x06000080 RID: 128 RVA: 0x0000444A File Offset: 0x0000264A
		public OperationContext OpContext { get; set; }

		// Token: 0x06000081 RID: 129 RVA: 0x00004453 File Offset: 0x00002653
		public CampusDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004468 File Offset: 0x00002668
		public IList<SchoolCampus> GetCampusList()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<SchoolCampus> list = new List<SchoolCampus>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("Select CampusId,CampusName,CampusDescription from CampusLookup where IsActive=1 order by OrderNum"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						SchoolCampus campusFromReader = CampusDAO.GetCampusFromReader(dataReader);
						bool flag2 = campusFromReader != null;
						if (flag2)
						{
							list.Add(campusFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004500 File Offset: 0x00002700
		public int CreateCampus(SchoolCampus campus)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@campusid", DbType.Int32, 0),
				databaseLayer.GetParameter("@campusname", DbType.String, campus.CampusName ?? string.Empty),
				databaseLayer.GetParameter("@campusdescription", DbType.String, campus.Description ?? string.Empty)
			};
			databaseLayer.ExecuteNonQuery("insert into CampusLookup\r\n\t\t\t(CampusName\r\n\t\t\t,CampusDescription)\r\nvalues\r\n\t\t\t(@campusname\r\n\t\t\t,@campusdescription)\r\nset @campusid = SCOPE_IDENTITY()", array);
			return campus.CampusId = ((array[0].Value is DBNull) ? 0 : ((int)array[0].Value));
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000045B8 File Offset: 0x000027B8
		public void UpdateCampus(SchoolCampus campus)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@campusid", DbType.Int32, campus.CampusId),
				databaseLayer.GetParameter("@campusname", DbType.String, campus.CampusName ?? string.Empty),
				databaseLayer.GetParameter("@campusdescription", DbType.String, campus.Description ?? string.Empty)
			};
			databaseLayer.ExecuteNonQuery("update CampusLookup\r\nset\t CampusName = @campusname\r\n\t,CampusDescription = @campusdescription\r\nwhere CampusId=@campusid", parameters);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000464C File Offset: 0x0000284C
		public void DeleteCampus(int campusId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			databaseLayer.ExecuteNonQuery("delete from CampusLookup where CampusId=@campusid", parameters);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000046A0 File Offset: 0x000028A0
		public static SchoolCampus GetCampusFromReader(IDataReader reader)
		{
			bool flag = !reader.ContainsColumn("CampusId");
			SchoolCampus result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (reader["CampusId"] is DBNull) ? 0 : ((int)reader["CampusId"]);
				bool flag2 = num <= 0;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = new SchoolCampus
					{
						CampusId = num,
						CampusName = (string)reader["CampusName"],
						Description = (string)reader["CampusDescription"]
					};
				}
			}
			return result;
		}
	}
}
