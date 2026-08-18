using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Data;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Data;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.Data
{
	// Token: 0x020000F4 RID: 244
	public class DataMaintenanceDAO : IDataMaintenanceDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060006F4 RID: 1780 RVA: 0x00048732 File Offset: 0x00046932
		public DataMaintenanceDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x00048744 File Offset: 0x00046944
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x0004874C File Offset: 0x0004694C
		public OperationContext OpContext { get; set; }

		// Token: 0x060006F7 RID: 1783 RVA: 0x00048758 File Offset: 0x00046958
		private StaffDropListAssignment GetStaffDropListAssignmentFromRecord(IDataRecord record, IBatchDecryptor batchDecryptor)
		{
			bool flag = record == null;
			StaffDropListAssignment result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (record["dataid"] is DBNull) ? 0 : ((int)record["dataid"]);
				bool flag2 = num < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = new StaffDropListAssignment
					{
						DataId = num,
						Student = PeopleDAO.GetBasicPersonFromRecord("", record, batchDecryptor)
					};
				}
			}
			return result;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x000487C8 File Offset: 0x000469C8
		public IList<StaffDropListAssignment> LoadAssignmentsForStaffDropList(int staffDropListCid, int staffPid)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@cid", DbType.Int32, staffDropListCid),
				databaseLayer.GetParameter("@staffpid", DbType.Int32, staffPid)
			};
			IList<StaffDropListAssignment> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT\tm.dataid,m.personid,m.controlvalue AS staffpersonid,\r\n\t\t    p.lastName,p.firstName,p.middleName,p.student_no\r\nFROM\t    maininfops m LEFT JOIN people p ON p.PersonID=m.PersonID\r\nWHERE\t    m.controlid=@cid AND m.controlvalue=@staffpid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					List<StaffDropListAssignment> list = new List<StaffDropListAssignment>();
					while (dataReader.Read())
					{
						StaffDropListAssignment staffDropListAssignmentFromRecord = this.GetStaffDropListAssignmentFromRecord(dataReader, batchDecryptor);
						bool flag2 = staffDropListAssignmentFromRecord == null;
						if (!flag2)
						{
							list.Add(staffDropListAssignmentFromRecord);
						}
					}
					list.Sort(delegate(StaffDropListAssignment g1, StaffDropListAssignment g2)
					{
						BasicPerson student = g1.Student;
						string text = ((student != null) ? student.LastName : null) ?? "";
						BasicPerson student2 = g2.Student;
						return text.CompareTo((student2 != null) ? student2.LastName : null);
					});
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x000488D0 File Offset: 0x00046AD0
		public void ReassignStaffDropList(int staffDropListCid, int staffPidOld, int staffPidNew)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@cid", DbType.Int32, staffDropListCid),
				databaseLayer.GetParameter("@staffpidold", DbType.Int32, staffPidOld),
				databaseLayer.GetParameter("@staffpidnew", DbType.Int32, staffPidNew)
			};
			databaseLayer.ExecuteNonQuery("UPDATE maininfops SET controlvalue=@staffpidnew WHERE controlid=@cid AND controlvalue=@staffpidold", parameters);
		}
	}
}
