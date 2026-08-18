using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.DataSync;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.DataSync
{
	// Token: 0x020000F9 RID: 249
	public class DataSyncInfoDAO : IDataSyncInfoDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0004A3C4 File Offset: 0x000485C4
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x0004A3CC File Offset: 0x000485CC
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x06000726 RID: 1830 RVA: 0x0004A3D5 File Offset: 0x000485D5
		public DataSyncInfoDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x0004A406 File Offset: 0x00048606
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x0004A40E File Offset: 0x0004860E
		public OperationContext OpContext { get; set; }

		// Token: 0x06000729 RID: 1833 RVA: 0x0004A418 File Offset: 0x00048618
		public IList<DynamicData> LoadOnlineIntakeFormData(int ScreenNum, string StudentNumber, out PersonBase StudentInfo)
		{
			string query = "EXEC LoadIntakeData @sn,@snume";
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@sn", DbType.Int32, ScreenNum),
				this.DatabaseManager.GetParameter("@snume", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(StudentNumber ?? ""))
			};
			IList<DynamicData> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader(query, parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					StudentInfo = null;
					result = null;
				}
				else
				{
					DynamicDataDAO dynamicDataDAO = new DynamicDataDAO(this.OpContext);
					result = dynamicDataDAO.GetDataListFromRecordsAndReturnStudentInfo(dataReader, out StudentInfo);
				}
			}
			return result;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0004A4D4 File Offset: 0x000486D4
		public void DataSyncIntakeData(int PersonId, string Student_No, int IntakeScreenNum, bool deleteIntakeEntry = true)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, PersonId),
				databaseLayer.GetParameter("@snume", DbType.Binary, databaseLayer.Encryption.Encrypt(Student_No)),
				databaseLayer.GetParameter("@screennum", DbType.Int32, IntakeScreenNum),
				databaseLayer.GetParameter("@deleteIntakeEntry", DbType.Boolean, deleteIntakeEntry)
			};
			databaseLayer.ExecuteNonQuery("EXEC sp_intake_dataSyncIntakeData @pid,@snume,@screennum,@deleteIntakeEntry", parameters);
		}
	}
}
