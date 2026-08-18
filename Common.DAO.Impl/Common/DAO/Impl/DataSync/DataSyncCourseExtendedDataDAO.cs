using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.DataSync;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.LookupCourses.ExtendedDataSyncData;

namespace TechnoPro.Common.DAO.Impl.DataSync
{
	// Token: 0x020000F7 RID: 247
	public class DataSyncCourseExtendedDataDAO : IDataSyncCourseExtendedDataDAO, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000706 RID: 1798 RVA: 0x000491C2 File Offset: 0x000473C2
		public DataSyncCourseExtendedDataDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x000491D4 File Offset: 0x000473D4
		// (set) Token: 0x06000708 RID: 1800 RVA: 0x000491DC File Offset: 0x000473DC
		public OperationContext OpContext { get; set; }

		// Token: 0x06000709 RID: 1801 RVA: 0x000491E8 File Offset: 0x000473E8
		private CourseExtendedDataSyncField GetCourseExtendedDataSyncFieldFromRecord(IDataReader record)
		{
			bool flag = record == null || record["controlid"] is DBNull;
			CourseExtendedDataSyncField result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (record["controlcode"] is DBNull) ? 0 : ((int)record["controlcode"]);
				result = new CourseExtendedDataSyncField
				{
					ControlId = (int)record["controlid"],
					ControlCaption = record["controlcaption"].ToString().Trim(),
					ControlCode = (eControlCode)((num < 1 || !Enum.IsDefined(typeof(eControlCode), num)) ? 0 : num),
					OrderNum = ((record["ordernum"] is DBNull) ? 0 : ((int)record["ordernum"])),
					IsActive = (!(record["isactive"] is DBNull) && Convert.ToBoolean(record["isactive"]))
				};
			}
			return result;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x000492FC File Offset: 0x000474FC
		public IList<CourseExtendedDataSyncField> LoadCourseExtendedDataSyncFields()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IList<CourseExtendedDataSyncField> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT controlid,controlcaption,controlcode,isactive,ordernum FROM LuCourseDataSyncExtendedFields WHERE isactive=1 ORDER BY ordernum"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<CourseExtendedDataSyncField> list = new List<CourseExtendedDataSyncField>();
					while (dataReader.Read())
					{
						CourseExtendedDataSyncField courseExtendedDataSyncFieldFromRecord = this.GetCourseExtendedDataSyncFieldFromRecord(dataReader);
						bool flag2 = courseExtendedDataSyncFieldFromRecord != null;
						if (flag2)
						{
							list.Add(courseExtendedDataSyncFieldFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00049394 File Offset: 0x00047594
		public int AddCourseExtendedDataSyncField(CourseExtendedDataSyncField field)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@cid", DbType.Int32, 0),
				databaseLayer.GetParameter("@caption", DbType.String, field.ControlCaption ?? ""),
				databaseLayer.GetParameter("@code", DbType.Int32, (int)field.ControlCode),
				databaseLayer.GetParameter("@isactive", DbType.Boolean, true),
				databaseLayer.GetParameter("@ordernum", DbType.Int32, field.OrderNum)
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO LuCourseDataSyncExtendedFields (ControlCaption,ControlCode,IsActive,OrderNum) VALUES (@caption,@code,@isactive,@ordernum) \r\nSET @cid=(SELECT TOP 1 CAST(IDENTITY_SCOPE() AS int))", array);
			int num = (array[0].Value == null || !(array[0].Value is int)) ? 0 : ((int)array[0].Value);
			field.ControlId = num;
			return num;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00049484 File Offset: 0x00047684
		public void DeleteCourseExtendedDataSyncField(int ControlId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@cid", DbType.Int32, ControlId)
			};
			databaseLayer.ExecuteNonQuery("UPDATE LuCourseDataSyncExtendedFields SET isactive=0 WHERE controlid=@cid", parameters);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x000494D8 File Offset: 0x000476D8
		public void UpdateCourseExtendedDataSyncField(CourseExtendedDataSyncField field)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@cid", DbType.Int32, field.ControlId),
				databaseLayer.GetParameter("@caption", DbType.String, field.ControlCaption ?? ""),
				databaseLayer.GetParameter("@code", DbType.Int32, (int)field.ControlCode),
				databaseLayer.GetParameter("@isactive", DbType.Boolean, field.IsActive),
				databaseLayer.GetParameter("@ordernum", DbType.Int32, field.OrderNum)
			};
			databaseLayer.ExecuteNonQuery("UPDATE LuCourseDataSyncExtendedFields SET ControlCaption=@caption,ControlCode=@code,IsActive=@isactive,OrderNum=@ordernum\r\nWHERE controlid=@cid", parameters);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x000495A0 File Offset: 0x000477A0
		public void OverwriteCourseExtendedData(int lucid, IList<CourseExtendedDataSyncField> fields, CourseExtendedDataSyncDataItems dataItems)
		{
			throw new NotImplementedException();
		}
	}
}
