using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Tutoring;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Tutoring;

namespace TechnoPro.Common.DAO.Impl.Tutoring
{
	// Token: 0x02000032 RID: 50
	public class StudentTuteeDAO : IStudentTuteeDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000096B4 File Offset: 0x000078B4
		// (set) Token: 0x06000138 RID: 312 RVA: 0x000096BC File Offset: 0x000078BC
		public OperationContext OpContext { get; set; }

		// Token: 0x06000139 RID: 313 RVA: 0x000096C5 File Offset: 0x000078C5
		public StudentTuteeDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000096D8 File Offset: 0x000078D8
		public IList<MyTutor> GetStudentMyTutors(int StudentPersonId, DateTime? StartDate, DateTime? EndDate)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[3];
			array[0] = databaseLayer.GetParameter("@pid", DbType.Int32, StudentPersonId);
			int num = 1;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@startdate";
			DbType pType = DbType.DateTime;
			DateTime? dateTime = StartDate;
			array[num] = databaseLayer2.GetParameter(pName, pType, (dateTime != null) ? dateTime.GetValueOrDefault() : DBNull.Value);
			int num2 = 2;
			DatabaseLayer databaseLayer3 = databaseLayer;
			string pName2 = "@enddate";
			DbType pType2 = DbType.DateTime;
			dateTime = EndDate;
			array[num2] = databaseLayer3.GetParameter(pName2, pType2, (dateTime != null) ? dateTime.GetValueOrDefault() : DBNull.Value);
			DbParameter[] parameters = array;
			IList<MyTutor> result;
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_Tutoring_MyTutors", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<MyTutor> list = new List<MyTutor>();
					while (dataReader.Read())
					{
						MyTutor myTutorFromRecord = TutorDAO.GetMyTutorFromRecord(dataReader, this.OpContext);
						bool flag2 = myTutorFromRecord != null;
						if (flag2)
						{
							list.Add(myTutorFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000097F0 File Offset: 0x000079F0
		public bool GetIsStudentAuthorizedToUseTutoring(int studentPersonId, int studentIsAuthorizedCid)
		{
			bool flag = studentIsAuthorizedCid < 1;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] array = new DbParameter[]
				{
					databaseLayer.GetOutputParameter("@isallowed", DbType.Boolean, 0),
					databaseLayer.GetParameter("@pid", DbType.Int32, studentPersonId),
					databaseLayer.GetParameter("@cid", DbType.Int32, studentIsAuthorizedCid)
				};
				databaseLayer.ExecuteNonQuery("IF EXISTS(SELECT controlid FROM dynamicscreencontrols WHERE screennum=4 AND controlid=@cid)\r\nBEGIN --must be acc template\r\n    IF EXISTS(SELECT dataid FROM MainInfoAccommodationPS WHERE personid=@pid AND courseid=0 AND controlid=@cid AND NOT controlvalue=0)\r\n        SET @isallowed = 1\r\n    ELSE\r\n    SET @isallowed = 0\r\nEND\r\nELSE --must be per student\r\nBEGIN\r\n    IF EXISTS(SELECT dataid FROM MainInfoPS WHERE personid=@pid AND controlid=@cid AND NOT controlvalue=0)\r\n        SET @isallowed=1\r\n    ELSE\r\n        SET @isallowed=0\r\nEND", array);
				result = (!(array[0].Value is DBNull) && Convert.ToBoolean(array[0].Value));
			}
			return result;
		}
	}
}
