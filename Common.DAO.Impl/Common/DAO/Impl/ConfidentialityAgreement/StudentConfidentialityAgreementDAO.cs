using System;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.ConfidentialityAgreement;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ConfidentialityAgreement;
using TechnoPro.Common.Public.Entities.OperationContexts;

namespace TechnoPro.Common.DAO.Impl.ConfidentialityAgreement
{
	// Token: 0x0200010A RID: 266
	public class StudentConfidentialityAgreementDAO : IStudentConfidentialityAgreementDAO, IBaseOperationContext<ConfidentialityAgreementOperationContext>
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0004E1B1 File Offset: 0x0004C3B1
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x0004E1B9 File Offset: 0x0004C3B9
		public ConfidentialityAgreementOperationContext OpContext { get; set; }

		// Token: 0x060007A3 RID: 1955 RVA: 0x0004E1C2 File Offset: 0x0004C3C2
		public StudentConfidentialityAgreementDAO(ConfidentialityAgreementOperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0004E1D4 File Offset: 0x0004C3D4
		public void RecordSignedConfidentialityAgreement(int personId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			ConfidentialityAgreementOperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@personid", DbType.Int32, personId),
				databaseLayer.GetParameter("@modulename", DbType.String, this.OpContext.Module.ToString())
			};
			databaseLayer.ExecuteNonQuery("insert into Student_ConfidentialityAgreement \r\n(PersonId, ModuleName)\r\nvalues (@personid, @modulename)", parameters);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0004E250 File Offset: 0x0004C450
		public StudentConfidentialityAgreement LastSignedStudentConfidentialityAgreement(int personId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			ConfidentialityAgreementOperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, personId),
				databaseLayer.GetParameter("@modulename", DbType.String, this.OpContext.Module.ToString())
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select * from Student_ConfidentialityAgreement where personid=@pid and ModuleName=@modulename", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetStudentConfidentialityAgreement(dataReader);
				}
			}
			return null;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0004E310 File Offset: 0x0004C510
		public bool IsConfidentialityAgreementSigningRequired(int pid)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			ConfidentialityAgreementOperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, pid),
				databaseLayer.GetParameter("@modulename", DbType.String, this.OpContext.Module.ToString())
			};
			object obj = databaseLayer.ExecuteScalar("select 1 from Student_ConfidentialityAgreement where personid=@pid and ModuleName=@modulename", parameters);
			return obj == null || Convert.IsDBNull(obj) || (int)obj == 0;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0004E3A8 File Offset: 0x0004C5A8
		private StudentConfidentialityAgreement GetStudentConfidentialityAgreement(IDataRecord record)
		{
			return new StudentConfidentialityAgreement
			{
				StudentConfidentialityAgreementId = (int)record["StudentConfidentialityAgreementId"],
				SignedOn = (DateTime)record["SignedConfidentialityAgreementOn"],
				ModuleName = (Enum.IsDefined(typeof(eClockWorkModules), (string)record["ModuleName"]) ? ((eClockWorkModules)Enum.Parse(typeof(eClockWorkModules), (string)record["SignedConfidentialityAgreementOn"])) : eClockWorkModules.Unknown)
			};
		}
	}
}
