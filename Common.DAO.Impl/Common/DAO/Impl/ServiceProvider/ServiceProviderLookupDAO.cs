using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.ServiceProvider;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.DAO.Impl.ServiceProvider
{
	// Token: 0x0200005B RID: 91
	public class ServiceProviderLookupDAO : IServiceProviderLookupDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600023D RID: 573 RVA: 0x00013546 File Offset: 0x00011746
		public ServiceProviderLookupDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00013576 File Offset: 0x00011776
		// (set) Token: 0x0600023F RID: 575 RVA: 0x0001357E File Offset: 0x0001177E
		public OperationContext OpContext { get; set; }

		// Token: 0x06000240 RID: 576 RVA: 0x00013588 File Offset: 0x00011788
		public SPUrgencyLevelType GetUrgencyLevelTypeFromRecord(IDataReader record, string prefix)
		{
			bool flag = prefix == null;
			if (flag)
			{
				prefix = "";
			}
			string name = prefix + "spurgencyleveltypeid";
			bool flag2 = record == null || record[name] == DBNull.Value;
			SPUrgencyLevelType result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				result = new SPUrgencyLevelType
				{
					SPUrgencyLevelTypeId = (int)record[name],
					Title = (string)record[prefix + "urgencytitle"],
					Description = (string)record[prefix + "urgencydescription"],
					Urgency = (int)record[prefix + "urgencylevel"]
				};
			}
			return result;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00013640 File Offset: 0x00011840
		public SPRequestStatusType GetRequestStatusTypeFromRecord(IDataReader record, string prefix)
		{
			bool flag = prefix == null;
			if (flag)
			{
				prefix = "";
			}
			string name = prefix + "sprequeststatustypeid";
			bool flag2 = record == null || record[name] == DBNull.Value;
			SPRequestStatusType result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				result = new SPRequestStatusType
				{
					SPRequestStatusTypeId = (int)record[name],
					AssignmentIsRequired = Convert.ToBoolean(record[prefix + "rsassignmentisrequired"]),
					Title = (string)record[prefix + "rstitle"],
					Description = (string)record[prefix + "rsdescription"],
					UrgencyLevel = this.GetUrgencyLevelTypeFromRecord(record, prefix + "rs")
				};
			}
			return result;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00013714 File Offset: 0x00011914
		public SPRequestAssignmentStatusType GetRequestAssignmentStatusTypeFromRecord(IDataReader record, string prefix)
		{
			bool flag = prefix == null;
			if (flag)
			{
				prefix = "";
			}
			string name = prefix + "sprequestAssignmentstatustypeid";
			bool flag2 = record == null || record[name] == DBNull.Value;
			SPRequestAssignmentStatusType result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				result = new SPRequestAssignmentStatusType
				{
					SPRequestAssignmentStatusTypeId = (int)record[name],
					AssignmentIsCompleted = Convert.ToBoolean(record[prefix + "asassignmentiscompleted"]),
					Title = (string)record[prefix + "astitle"],
					Description = (string)record[prefix + "asdescription"],
					UrgencyLevel = this.GetUrgencyLevelTypeFromRecord(record, prefix + "as")
				};
			}
			return result;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000137E8 File Offset: 0x000119E8
		public SPRequestStatusType LoadRequestStatusTypeById(int SPRequestStatusTypeId)
		{
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT r.sprequeststatustypeid,r.rstitle,r.rsdescription,r.rsassignmentisrequired,r.rsspurgencyleveltypeid,u.urgencytitle AS rsurgencytitle,u.urgencydescription AS rsurgencydescription,u.urgencylevel AS rsurgencylevel FROM sprequeststatustype r LEFT JOIN spurgencyleveltype u ON u.spurgencyleveltypeid=r.rsspurgencyleveltypeid WHERE sprequeststatustypeid=@sprequeststatustypeid", new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@sprequeststatustypeid", DbType.Int32, SPRequestStatusTypeId)
			}))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return null;
				}
				if (dataReader.Read())
				{
					return this.GetRequestStatusTypeFromRecord(dataReader, "");
				}
			}
			return null;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00013878 File Offset: 0x00011A78
		public IList<SPRequestStatusType> LoadActiveRequestStatusTypes()
		{
			IList<SPRequestStatusType> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT r.sprequeststatustypeid,r.rstitle,r.rsdescription,r.rsassignmentisrequired,r.rsspurgencyleveltypeid,u.urgencytitle AS rsurgencytitle,u.urgencydescription AS rsurgencydescription,u.urgencylevel AS rsurgencylevel FROM sprequeststatustype r LEFT JOIN spurgencyleveltype u ON u.spurgencyleveltypeid=r.rsspurgencyleveltypeid ORDER BY r.rstitle"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<SPRequestStatusType> list = new List<SPRequestStatusType>();
					while (dataReader.Read())
					{
						SPRequestStatusType requestStatusTypeFromRecord = this.GetRequestStatusTypeFromRecord(dataReader, "");
						bool flag2 = requestStatusTypeFromRecord != null;
						if (flag2)
						{
							list.Add(requestStatusTypeFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00003998 File Offset: 0x00001B98
		public void DeleteRequestStatusType(int SPRequestStatusTypeId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00003998 File Offset: 0x00001B98
		public void UpdateRequestStatusType(SPRequestStatusType StatusType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00003998 File Offset: 0x00001B98
		public int CreateRequestStatusType(SPRequestStatusType StatusType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000138FC File Offset: 0x00011AFC
		public SPRequestAssignmentStatusType LoadRequestAssignmentStatusTypeById(int SPRequestAssignmentStatusTypeId)
		{
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT a.SPRequestAssignmentStatusTypeId,a.astitle,a.asdescription,a.asassignmentiscompleted,a.asspurgencyleveltypeid,u.urgencytitle AS asurgencytitle,u.urgencydescription AS asurgencydescription,u.urgencylevel AS asurgencylevel FROM sprequestassignmentstatustype a LEFT JOIN spurgencyleveltype u ON u.spurgencyleveltypeid=a.asspurgencyleveltypeid WHERE a.sprequeststatustypeid=@SPRequestAssignmentStatusTypeId", new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@sprequestassignmentstatustypeid", DbType.Int32, SPRequestAssignmentStatusTypeId)
			}))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return null;
				}
				if (dataReader.Read())
				{
					return this.GetRequestAssignmentStatusTypeFromRecord(dataReader, "");
				}
			}
			return null;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0001398C File Offset: 0x00011B8C
		public IList<SPRequestAssignmentStatusType> LoadActiveRequestAssignmentStatusTypes()
		{
			IList<SPRequestAssignmentStatusType> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT a.SPRequestAssignmentStatusTypeId,a.astitle,a.asdescription,a.asassignmentiscompleted,a.asspurgencyleveltypeid,u.urgencytitle AS asurgencytitle,u.urgencydescription AS asurgencydescription,u.urgencylevel AS asurgencylevel FROM sprequestassignmentstatustype a LEFT JOIN spurgencyleveltype u ON u.spurgencyleveltypeid=a.asspurgencyleveltypeid ORDER BY a.astitle"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<SPRequestAssignmentStatusType> list = new List<SPRequestAssignmentStatusType>();
					while (dataReader.Read())
					{
						SPRequestAssignmentStatusType requestAssignmentStatusTypeFromRecord = this.GetRequestAssignmentStatusTypeFromRecord(dataReader, "");
						bool flag2 = requestAssignmentStatusTypeFromRecord != null;
						if (flag2)
						{
							list.Add(requestAssignmentStatusTypeFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00003998 File Offset: 0x00001B98
		public void DeleteRequestAssignmentStatusType(int SPRequestAssignmentStatusTypeId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00003998 File Offset: 0x00001B98
		public void UpdateRequestAssignmentStatusType(SPRequestAssignmentStatusType AssignmentStatusType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00003998 File Offset: 0x00001B98
		public int CreateRequestAssignmentStatusType(SPRequestAssignmentStatusType AssignmentStatusType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00013A10 File Offset: 0x00011C10
		public SPUrgencyLevelType LoadUrgencyLevelTypeById(int SPUrgencyLevelTypeId)
		{
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT spurgencyleveltypeid,urgencytitle,urgencydescription,urgencylevel FROM spurgencyleveltype WHERE spurgencyleveltypeid=@spurgencyleveltypeid", new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@spurgencyleveltypeid", DbType.Int32, SPUrgencyLevelTypeId)
			}))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return null;
				}
				if (dataReader.Read())
				{
					return this.GetUrgencyLevelTypeFromRecord(dataReader, "");
				}
			}
			return null;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00013AA0 File Offset: 0x00011CA0
		public IList<SPUrgencyLevelType> LoadActiveUrgencyLevelTypes()
		{
			IList<SPUrgencyLevelType> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT spurgencyleveltypeid,urgencytitle,urgencydescription,urgencylevel FROM spurgencyleveltype ORDER BY urgencytitle"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<SPUrgencyLevelType> list = new List<SPUrgencyLevelType>();
					while (dataReader.Read())
					{
						SPUrgencyLevelType urgencyLevelTypeFromRecord = this.GetUrgencyLevelTypeFromRecord(dataReader, "");
						bool flag2 = urgencyLevelTypeFromRecord != null;
						if (flag2)
						{
							list.Add(urgencyLevelTypeFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00003998 File Offset: 0x00001B98
		public void DeleteUrgencyLevelStatusType(int SPUrgencyLevelTypeId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00003998 File Offset: 0x00001B98
		public void UpdateUrgencyLevelStatusType(SPUrgencyLevelType UrgencyLevelType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00003998 File Offset: 0x00001B98
		public int CreateUrgencyLevelStatusType(SPUrgencyLevelType UrgencyLevelType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000E2 RID: 226
		public DatabaseLayer DatabaseManager;
	}
}
