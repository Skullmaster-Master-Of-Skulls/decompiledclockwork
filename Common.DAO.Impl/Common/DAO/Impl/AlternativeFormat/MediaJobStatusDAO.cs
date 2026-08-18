using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.AlternativeFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.DAO.Impl.AlternativeFormat
{
	// Token: 0x02000169 RID: 361
	public class MediaJobStatusDAO : IMediaJobStatusDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x00072651 File Offset: 0x00070851
		// (set) Token: 0x06000ACD RID: 2765 RVA: 0x00072659 File Offset: 0x00070859
		public OperationContext OpContext { get; set; }

		// Token: 0x06000ACE RID: 2766 RVA: 0x00072662 File Offset: 0x00070862
		public MediaJobStatusDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00072674 File Offset: 0x00070874
		public int CreateMediaJobStatus(MediaJobStatus jobStatus)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@mediajobstatusid", DbType.Int32, 0),
				databaseLayer.GetParameter("@mediajobstatusname", DbType.String, jobStatus.JobStatusName),
				databaseLayer.GetParameter("@mediajobstatusdescription", DbType.String, jobStatus.JobStatusDescription),
				databaseLayer.GetParameter("@mediajobstatusgroupname", DbType.String, jobStatus.JobStatusGroup.ToString())
			};
			databaseLayer.ExecuteNonQuery("declare @ordernum as int\r\nset @ordernum = COALESCE ((select MAX(MediaJobStatusOrderNum)+1 from AlternativeFormat_MediaJobStatus where MediaJobStatusGroupName=@mediajobstatusgroupname), 0)\r\n\r\nINSERT INTO [AlternativeFormat_MediaJobStatus]\r\n                    ([MediaJobStatusName]\r\n                    ,[MediaJobStatusDescription]\r\n                    ,[MediaJobStatusGroupName]\r\n                    ,[MediaJobStatusOrderNum])\r\n                VALUES\r\n                    (@mediajobstatusname\r\n                    ,@mediajobstatusdescription\r\n                    ,@mediajobstatusgroupname\r\n                    ,@ordernum)\r\n            set @mediajobstatusid=scope_identity()", array);
			return jobStatus.Id = Convert.ToInt32(array[0].Value);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00072730 File Offset: 0x00070930
		public MediaJobStatus GetMediaJobStatusByName(string jobStatusName)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@mediajobstatusname", DbType.String, jobStatusName);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select * from AlternativeFormat_MediaJobStatus where MediaJobStatusName=@mediajobstatusname", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetMediaJobStatusFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x000727C0 File Offset: 0x000709C0
		public IList<MediaJobStatus> GetMediaJobStatusByGroup(MediaJobStatusGroup statusGroup)
		{
			List<MediaJobStatus> list = new List<MediaJobStatus>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@mediajobstatusgroupname", DbType.String, statusGroup.ToString());
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT * FROM AlternativeFormat_MediaJobStatus\r\n            where MediaJobStatusGroupName=@mediajobstatusgroupname\r\n            ORDER BY MediaJobStatusOrderNum, MediaJobStatusName", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						MediaJobStatus mediaJobStatusFromReader = this.GetMediaJobStatusFromReader(dataReader);
						bool flag2 = mediaJobStatusFromReader != null;
						if (flag2)
						{
							list.Add(mediaJobStatusFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0007287C File Offset: 0x00070A7C
		public IList<MediaJobStatus> GetAllMediaJobStatus()
		{
			List<MediaJobStatus> list = new List<MediaJobStatus>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select * from AlternativeFormat_MediaJobStatus\r\n            ORDER BY MediaJobStatusOrderNum, MediaJobStatusName"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						MediaJobStatus mediaJobStatusFromReader = this.GetMediaJobStatusFromReader(dataReader);
						bool flag2 = mediaJobStatusFromReader != null;
						if (flag2)
						{
							list.Add(mediaJobStatusFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00072910 File Offset: 0x00070B10
		private MediaJobStatus GetMediaJobStatusFromReader(IDataReader record)
		{
			return new MediaJobStatus
			{
				MediaJobStatusId = (int)record["MediaJobStatusID"],
				JobStatusName = (string)record["MediaJobStatusName"],
				JobStatusDescription = (string)record["MediaJobStatusDescription"],
				JobStatusGroup = (Enum.IsDefined(typeof(MediaJobStatusGroup), Convert.ToString(record["MediaJobStatusGroupName"])) ? ((MediaJobStatusGroup)Enum.Parse(typeof(MediaJobStatusGroup), Convert.ToString(record["MediaJobStatusGroupName"]))) : MediaJobStatusGroup.GeneralActionStatus)
			};
		}
	}
}
