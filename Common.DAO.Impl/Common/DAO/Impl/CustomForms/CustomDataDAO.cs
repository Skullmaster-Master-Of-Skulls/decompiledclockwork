using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.Converter.CustomForms;
using TechnoPro.Common.DAO.CustomForms;
using TechnoPro.Common.DAO.Impl.CustomForms.QueryStorage;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CustomForms.Data;
using TechnoPro.Common.Public.Entities.CustomForms.Data.Context;
using TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders;

namespace TechnoPro.Common.DAO.Impl.CustomForms
{
	// Token: 0x020000FE RID: 254
	public class CustomDataDAO : ICustomDataDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000733 RID: 1843 RVA: 0x0004A7C4 File Offset: 0x000489C4
		public CustomDataDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0004A7D6 File Offset: 0x000489D6
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x0004A7DE File Offset: 0x000489DE
		public OperationContext OpContext { get; set; }

		// Token: 0x06000736 RID: 1846 RVA: 0x0004A7E8 File Offset: 0x000489E8
		private CustomDataHolder GetDataItemFromRecord(IDataRecord record, IBatchDecryptor batchDecryptor)
		{
			string g2 = record["DataInstanceId"].ToString();
			int num = (record["DataTypeCode"] is DBNull) ? 0 : ((int)record["DataTypeCode"]);
			eCustomDataPrimitiveType dataPrimitiveType = (eCustomDataPrimitiveType)(Enum.IsDefined(typeof(eCustomDataPrimitiveType), num) ? num : 0);
			string dataValueXml = (record["DataValue"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["DataValue"]);
			Dictionary<string, object> extraValues = (from g in new <>f__AnonymousType8<string, string>[]
			{
				new
				{
					Name = "itemcaption",
					Val = ((record["joinedlistitemcaption"] is DBNull) ? "" : (record["joinedlistitemcaption"] as string))
				},
				new
				{
					Name = "fn",
					Val = ((record["joinedfilename"] is DBNull) ? "" : (record["joinedfilename"] as string))
				}
			}
			where !string.IsNullOrEmpty(g.Val)
			select g).ToDictionary(g => g.Name, g => g.Val);
			string text = (record["DataValueJoinId"] is DBNull) ? "" : (record["DataValueJoinId"] as string);
			CustomDataSerialized serializedData = new CustomDataSerialized
			{
				DataValueXml = dataValueXml,
				DataInstanceId = new Guid(g2),
				DataPrimitiveType = dataPrimitiveType,
				DataValueJoinId = (string.IsNullOrEmpty(text) ? null : new Guid?(new Guid(text))),
				ExtraValues = extraValues
			};
			return serializedData.GetCustomData();
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0004A9E4 File Offset: 0x00048BE4
		[DebuggerStepThrough]
		private Task<CustomDataSet> LoadDataAsync(DbDataReader reader, CustomDataContext context, IEncryption encryption)
		{
			CustomDataDAO.<LoadDataAsync>d__6 <LoadDataAsync>d__ = new CustomDataDAO.<LoadDataAsync>d__6();
			<LoadDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CustomDataSet>.Create();
			<LoadDataAsync>d__.<>4__this = this;
			<LoadDataAsync>d__.reader = reader;
			<LoadDataAsync>d__.context = context;
			<LoadDataAsync>d__.encryption = encryption;
			<LoadDataAsync>d__.<>1__state = -1;
			<LoadDataAsync>d__.<>t__builder.Start<CustomDataDAO.<LoadDataAsync>d__6>(ref <LoadDataAsync>d__);
			return <LoadDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0004AA40 File Offset: 0x00048C40
		[DebuggerStepThrough]
		public Task<CustomDataSet> LoadPerStudentDataAsync(int personId, params Guid[] dataInstanceIds)
		{
			CustomDataDAO.<LoadPerStudentDataAsync>d__7 <LoadPerStudentDataAsync>d__ = new CustomDataDAO.<LoadPerStudentDataAsync>d__7();
			<LoadPerStudentDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CustomDataSet>.Create();
			<LoadPerStudentDataAsync>d__.<>4__this = this;
			<LoadPerStudentDataAsync>d__.personId = personId;
			<LoadPerStudentDataAsync>d__.dataInstanceIds = dataInstanceIds;
			<LoadPerStudentDataAsync>d__.<>1__state = -1;
			<LoadPerStudentDataAsync>d__.<>t__builder.Start<CustomDataDAO.<LoadPerStudentDataAsync>d__7>(ref <LoadPerStudentDataAsync>d__);
			return <LoadPerStudentDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0004AA94 File Offset: 0x00048C94
		[DebuggerStepThrough]
		public Task<CustomDataSet> LoadPerSemesterDataAsync(int personId, int semesterId, params Guid[] dataInstanceIds)
		{
			CustomDataDAO.<LoadPerSemesterDataAsync>d__8 <LoadPerSemesterDataAsync>d__ = new CustomDataDAO.<LoadPerSemesterDataAsync>d__8();
			<LoadPerSemesterDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CustomDataSet>.Create();
			<LoadPerSemesterDataAsync>d__.<>4__this = this;
			<LoadPerSemesterDataAsync>d__.personId = personId;
			<LoadPerSemesterDataAsync>d__.semesterId = semesterId;
			<LoadPerSemesterDataAsync>d__.dataInstanceIds = dataInstanceIds;
			<LoadPerSemesterDataAsync>d__.<>1__state = -1;
			<LoadPerSemesterDataAsync>d__.<>t__builder.Start<CustomDataDAO.<LoadPerSemesterDataAsync>d__8>(ref <LoadPerSemesterDataAsync>d__);
			return <LoadPerSemesterDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0004AAF0 File Offset: 0x00048CF0
		[DebuggerStepThrough]
		public Task<CustomDataSet> LoadPerDateDataAsync(int personId, int customDataPerDateId, params Guid[] dataInstanceIds)
		{
			CustomDataDAO.<LoadPerDateDataAsync>d__9 <LoadPerDateDataAsync>d__ = new CustomDataDAO.<LoadPerDateDataAsync>d__9();
			<LoadPerDateDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CustomDataSet>.Create();
			<LoadPerDateDataAsync>d__.<>4__this = this;
			<LoadPerDateDataAsync>d__.personId = personId;
			<LoadPerDateDataAsync>d__.customDataPerDateId = customDataPerDateId;
			<LoadPerDateDataAsync>d__.dataInstanceIds = dataInstanceIds;
			<LoadPerDateDataAsync>d__.<>1__state = -1;
			<LoadPerDateDataAsync>d__.<>t__builder.Start<CustomDataDAO.<LoadPerDateDataAsync>d__9>(ref <LoadPerDateDataAsync>d__);
			return <LoadPerDateDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0004AB4C File Offset: 0x00048D4C
		private CustomDataSet LoadData(IDataReader reader, CustomDataContext context, IEncryption encryption)
		{
			bool flag = reader == null;
			CustomDataSet result;
			if (flag)
			{
				result = null;
			}
			else
			{
				CustomDataSet customDataSet = new CustomDataSet
				{
					Context = context,
					Data = new List<CustomDataHolderCollection>()
				};
				IBatchDecryptor batchDecryptor = encryption.GetBatchDecryptor();
				while (reader.Read())
				{
					CustomDataHolder dataItem = this.GetDataItemFromRecord(reader, batchDecryptor);
					Func<CustomDataHolder, bool> <>9__1;
					CustomDataHolderCollection customDataHolderCollection = customDataSet.Data.FirstOrDefault(delegate(CustomDataHolderCollection g)
					{
						IEnumerable<CustomDataHolder> datas = g.Datas;
						Func<CustomDataHolder, bool> predicate;
						if ((predicate = <>9__1) == null)
						{
							predicate = (<>9__1 = ((CustomDataHolder h) => h.DataInstanceId == dataItem.DataInstanceId));
						}
						return datas.Any(predicate);
					});
					bool flag2 = customDataHolderCollection == null;
					if (flag2)
					{
						customDataSet.Data.Add(new CustomDataHolderCollection
						{
							Datas = new List<CustomDataHolder>
							{
								dataItem
							}
						});
					}
					else
					{
						customDataHolderCollection.Datas.Add(dataItem);
					}
				}
				result = customDataSet;
			}
			return result;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0004AC20 File Offset: 0x00048E20
		public CustomDataSet LoadPerStudentData(int personId, params Guid[] dataInstanceIds)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, personId),
				databaseLayer.GetParameter("@datainstanceids", DbType.String, string.Join<Guid>(",", dataInstanceIds))
			};
			CustomDataSet result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader(QueryStorageCustomData.QS_LOAD_DATA_PER_STUDENT, parameters))
			{
				result = this.LoadData(dataReader, new CustomDataPerStudentContext
				{
					PersonId = personId
				}, databaseLayer.Encryption);
			}
			return result;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0004ACC8 File Offset: 0x00048EC8
		public CustomDataSet LoadPerSemesterData(int personId, int semesterId, params Guid[] dataInstanceIds)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, personId),
				databaseLayer.GetParameter("@semesterid", DbType.Int32, semesterId),
				databaseLayer.GetParameter("@datainstanceids", DbType.String, string.Join<Guid>(",", dataInstanceIds))
			};
			CustomDataSet result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader(QueryStorageCustomData.QS_LOAD_DATA_PER_SEMESTER, parameters))
			{
				result = this.LoadData(dataReader, new CustomDataPerSemesterContext
				{
					PersonId = personId,
					SemesterId = semesterId
				}, databaseLayer.Encryption);
			}
			return result;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0004AD8C File Offset: 0x00048F8C
		public CustomDataSet LoadPerDateData(int personId, int customDataPerDateId, params Guid[] dataInstanceIds)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, personId),
				databaseLayer.GetParameter("@perdateid", DbType.Int32, customDataPerDateId),
				databaseLayer.GetParameter("@datainstanceids", DbType.String, string.Join<Guid>(",", dataInstanceIds))
			};
			CustomDataSet result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader(QueryStorageCustomData.QS_LOAD_DATA_PER_DATE, parameters))
			{
				result = this.LoadData(dataReader, new CustomDataPerDateContext
				{
					PersonId = personId,
					CustomDataPerDateId = customDataPerDateId
				}, databaseLayer.Encryption);
			}
			return result;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0004AE50 File Offset: 0x00049050
		[DebuggerStepThrough]
		private Task WriteDataAsync(string query, CustomDataSerialized[] serializedDatas, params DbParameter[] contextParameters)
		{
			CustomDataDAO.<WriteDataAsync>d__14 <WriteDataAsync>d__ = new CustomDataDAO.<WriteDataAsync>d__14();
			<WriteDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteDataAsync>d__.<>4__this = this;
			<WriteDataAsync>d__.query = query;
			<WriteDataAsync>d__.serializedDatas = serializedDatas;
			<WriteDataAsync>d__.contextParameters = contextParameters;
			<WriteDataAsync>d__.<>1__state = -1;
			<WriteDataAsync>d__.<>t__builder.Start<CustomDataDAO.<WriteDataAsync>d__14>(ref <WriteDataAsync>d__);
			return <WriteDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0004AEAC File Offset: 0x000490AC
		[DebuggerStepThrough]
		public Task WritePerStudentDataAsync(int personId, params CustomDataSerialized[] serializedDatas)
		{
			CustomDataDAO.<WritePerStudentDataAsync>d__15 <WritePerStudentDataAsync>d__ = new CustomDataDAO.<WritePerStudentDataAsync>d__15();
			<WritePerStudentDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WritePerStudentDataAsync>d__.<>4__this = this;
			<WritePerStudentDataAsync>d__.personId = personId;
			<WritePerStudentDataAsync>d__.serializedDatas = serializedDatas;
			<WritePerStudentDataAsync>d__.<>1__state = -1;
			<WritePerStudentDataAsync>d__.<>t__builder.Start<CustomDataDAO.<WritePerStudentDataAsync>d__15>(ref <WritePerStudentDataAsync>d__);
			return <WritePerStudentDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0004AF00 File Offset: 0x00049100
		[DebuggerStepThrough]
		public Task WritePerSemesterDataAsync(int personId, int semesterId, params CustomDataSerialized[] serializedDatas)
		{
			CustomDataDAO.<WritePerSemesterDataAsync>d__16 <WritePerSemesterDataAsync>d__ = new CustomDataDAO.<WritePerSemesterDataAsync>d__16();
			<WritePerSemesterDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WritePerSemesterDataAsync>d__.<>4__this = this;
			<WritePerSemesterDataAsync>d__.personId = personId;
			<WritePerSemesterDataAsync>d__.semesterId = semesterId;
			<WritePerSemesterDataAsync>d__.serializedDatas = serializedDatas;
			<WritePerSemesterDataAsync>d__.<>1__state = -1;
			<WritePerSemesterDataAsync>d__.<>t__builder.Start<CustomDataDAO.<WritePerSemesterDataAsync>d__16>(ref <WritePerSemesterDataAsync>d__);
			return <WritePerSemesterDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0004AF5C File Offset: 0x0004915C
		[DebuggerStepThrough]
		public Task WritePerDateDataAsync(int personId, int perDateId, params CustomDataSerialized[] serializedDatas)
		{
			CustomDataDAO.<WritePerDateDataAsync>d__17 <WritePerDateDataAsync>d__ = new CustomDataDAO.<WritePerDateDataAsync>d__17();
			<WritePerDateDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WritePerDateDataAsync>d__.<>4__this = this;
			<WritePerDateDataAsync>d__.personId = personId;
			<WritePerDateDataAsync>d__.perDateId = perDateId;
			<WritePerDateDataAsync>d__.serializedDatas = serializedDatas;
			<WritePerDateDataAsync>d__.<>1__state = -1;
			<WritePerDateDataAsync>d__.<>t__builder.Start<CustomDataDAO.<WritePerDateDataAsync>d__17>(ref <WritePerDateDataAsync>d__);
			return <WritePerDateDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0004AFB8 File Offset: 0x000491B8
		[DebuggerStepThrough]
		public Task ClearPerStudentDataAsync(int personId, params Guid[] dataInstanceIds)
		{
			CustomDataDAO.<ClearPerStudentDataAsync>d__18 <ClearPerStudentDataAsync>d__ = new CustomDataDAO.<ClearPerStudentDataAsync>d__18();
			<ClearPerStudentDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ClearPerStudentDataAsync>d__.<>4__this = this;
			<ClearPerStudentDataAsync>d__.personId = personId;
			<ClearPerStudentDataAsync>d__.dataInstanceIds = dataInstanceIds;
			<ClearPerStudentDataAsync>d__.<>1__state = -1;
			<ClearPerStudentDataAsync>d__.<>t__builder.Start<CustomDataDAO.<ClearPerStudentDataAsync>d__18>(ref <ClearPerStudentDataAsync>d__);
			return <ClearPerStudentDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0004B00C File Offset: 0x0004920C
		[DebuggerStepThrough]
		public Task ClearPerSemesterDataAsync(int personId, int semesterId, params Guid[] dataInstanceIds)
		{
			CustomDataDAO.<ClearPerSemesterDataAsync>d__19 <ClearPerSemesterDataAsync>d__ = new CustomDataDAO.<ClearPerSemesterDataAsync>d__19();
			<ClearPerSemesterDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ClearPerSemesterDataAsync>d__.<>4__this = this;
			<ClearPerSemesterDataAsync>d__.personId = personId;
			<ClearPerSemesterDataAsync>d__.semesterId = semesterId;
			<ClearPerSemesterDataAsync>d__.dataInstanceIds = dataInstanceIds;
			<ClearPerSemesterDataAsync>d__.<>1__state = -1;
			<ClearPerSemesterDataAsync>d__.<>t__builder.Start<CustomDataDAO.<ClearPerSemesterDataAsync>d__19>(ref <ClearPerSemesterDataAsync>d__);
			return <ClearPerSemesterDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0004B068 File Offset: 0x00049268
		[DebuggerStepThrough]
		public Task ClearPerDateDataAsync(int personId, int perDateId, params Guid[] dataInstanceIds)
		{
			CustomDataDAO.<ClearPerDateDataAsync>d__20 <ClearPerDateDataAsync>d__ = new CustomDataDAO.<ClearPerDateDataAsync>d__20();
			<ClearPerDateDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ClearPerDateDataAsync>d__.<>4__this = this;
			<ClearPerDateDataAsync>d__.personId = personId;
			<ClearPerDateDataAsync>d__.perDateId = perDateId;
			<ClearPerDateDataAsync>d__.dataInstanceIds = dataInstanceIds;
			<ClearPerDateDataAsync>d__.<>1__state = -1;
			<ClearPerDateDataAsync>d__.<>t__builder.Start<CustomDataDAO.<ClearPerDateDataAsync>d__20>(ref <ClearPerDateDataAsync>d__);
			return <ClearPerDateDataAsync>d__.<>t__builder.Task;
		}
	}
}
