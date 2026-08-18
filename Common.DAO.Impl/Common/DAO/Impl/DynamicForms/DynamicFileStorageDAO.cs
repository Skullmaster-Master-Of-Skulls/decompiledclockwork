using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Databases;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.DAO.Helper;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicLists;
using TechnoPro.Common.Public.Entities.Files;

namespace TechnoPro.Common.DAO.Impl.DynamicForms
{
	// Token: 0x020000DE RID: 222
	public class DynamicFileStorageDAO : IDynamicFileStorageDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00041BAF File Offset: 0x0003FDAF
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x00041BB7 File Offset: 0x0003FDB7
		public OperationContext OpContext { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x00041BC0 File Offset: 0x0003FDC0
		private DynamicFieldDAO dynamicFieldDao
		{
			get
			{
				bool flag = this._dynamicFieldDao == null;
				if (flag)
				{
					this._dynamicFieldDao = new DynamicFieldDAO(this.OpContext);
				}
				return this._dynamicFieldDao;
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00041BF8 File Offset: 0x0003FDF8
		public DynamicFileStorageDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00041C28 File Offset: 0x0003FE28
		private static T GetSingleFileDescriptionFromRecord<T>(IDataRecord record) where T : DynamicFileDescription
		{
			bool flag = record == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				bool flag2 = record["dataid"] is DBNull;
				if (flag2)
				{
					result = default(T);
				}
				else
				{
					bool flag3 = !(record["metadata"] is DBNull);
					if (flag3)
					{
						T dynamicFileDescriptionFromMetadata = record["metadata"].ToString().Trim().GetDynamicFileDescriptionFromMetadata((int)record["dataid"], (record["controlid"] is DBNull) ? 0 : ((int)record["controlid"]));
						bool flag4 = dynamicFileDescriptionFromMetadata != null;
						if (flag4)
						{
							return dynamicFileDescriptionFromMetadata;
						}
					}
					byte[] array = (record["controlvalue"] is DBNull) ? null : ((byte[])record["controlvalue"]);
					bool flag5 = ((array != null) ? array.Length : 0) < 1;
					if (flag5)
					{
						result = default(T);
					}
					else
					{
						string text;
						byte[] array2 = array.ParseSingleFileBytes(out text);
						bool flag6 = string.IsNullOrEmpty(text);
						if (flag6)
						{
							result = default(T);
						}
						else
						{
							T t = Activator.CreateInstance<T>();
							t.DataId = (int)record["dataid"];
							t.ControlId = ((record["controlid"] is DBNull) ? 0 : ((int)record["controlid"]));
							t.FileId = 0;
							t.Filename = text;
							result = t;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00041DE4 File Offset: 0x0003FFE4
		private static IList<T> GetFileListFileDescriptionFromRecord<T>(IDataRecord record) where T : DynamicFileDescription
		{
			bool flag = record == null;
			IList<T> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = record["dataid"] is DBNull;
				if (flag2)
				{
					result = null;
				}
				else
				{
					int dataId = (int)record["dataid"];
					int cid = (record["controlid"] is DBNull) ? 0 : ((int)record["controlid"]);
					byte[] array = (record["controlvalue"] is DBNull) ? new byte[0] : ((byte[])record["controlvalue"]);
					bool flag3 = array.Length < 1;
					if (flag3)
					{
						result = null;
					}
					else
					{
						UTF8Encoding utf8Encoding = new UTF8Encoding();
						string @string = utf8Encoding.GetString(array);
						DataTable dataTable = @string.ConvertListViewDataToDataTable(null);
						bool flag4 = dataTable == null || dataTable.Columns.Count < 1;
						if (flag4)
						{
							result = null;
						}
						else
						{
							int filenameCol = dataTable.Columns.Count - 1;
							int dateCol = (dataTable.Columns.Count > 1) ? (dataTable.Columns.Count - 2) : -1;
							var source = (from DataRow dr in dataTable.Rows
							where !(dr[filenameCol] is DBNull) && dr[filenameCol].ToString().Contains(":")
							select new
							{
								Filename = dr[filenameCol].ToString().Trim(),
								DateStr = ((dateCol >= 0) ? dr[dateCol].ToString().Trim() : ""),
								DataRow = dr
							}).ToList();
							bool returnColData = typeof(T) == typeof(DynamicFileDescriptionWithColData);
							result = (from m in source.Select(delegate(g)
							{
								int num = g.Filename.LastIndexOf(":");
								bool flag5 = num < 1;
								T result2;
								if (flag5)
								{
									result2 = default(T);
								}
								else
								{
									string s = g.Filename.Substring(num + 1).Trim();
									int num2;
									bool flag6 = !int.TryParse(s, out num2) || num2 < 1;
									if (flag6)
									{
										result2 = default(T);
									}
									else
									{
										string text = (g.DateStr ?? "").Trim();
										DateTime value;
										DateTime? dateUploaded = (text.Length > 0 && DateTime.TryParse(text, out value)) ? new DateTime?(value) : null;
										T t = Activator.CreateInstance<T>();
										t.DataId = dataId;
										t.ControlId = cid;
										t.FileId = num2;
										t.Filename = g.Filename.Substring(0, num).Trim();
										t.DateUploaded = dateUploaded;
										bool returnColData = returnColData;
										if (returnColData)
										{
											DynamicFileDescriptionWithColData dynamicFileDescriptionWithColData = t as DynamicFileDescriptionWithColData;
											DynamicFileDescriptionWithColData dynamicFileDescriptionWithColData2 = dynamicFileDescriptionWithColData;
											DataRow dataRow = g.DataRow;
											IList<string> columnData;
											if (dataRow == null)
											{
												columnData = null;
											}
											else
											{
												object[] itemArray = dataRow.ItemArray;
												if (itemArray == null)
												{
													columnData = null;
												}
												else
												{
													columnData = (from m in itemArray
													select (m == null) ? "" : m.ToString()).ToList<string>();
												}
											}
											dynamicFileDescriptionWithColData2.ColumnData = columnData;
										}
										result2 = t;
									}
								}
								return result2;
							})
							where m != null
							select m).ToList<T>();
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00041FB8 File Offset: 0x000401B8
		private DynamicFile GetDynamicFileFromReader(IDataReader reader)
		{
			bool flag = reader == null;
			DynamicFile result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int? num = (reader["filetypecode"] is DBNull) ? null : new int?((int)reader["filetypecode"]);
				result = new DynamicFile
				{
					FileId = (int)reader["fileid"],
					DateUploaded = (DateTime)reader["dateuploaded"],
					WhoUploadedPersonId = ((reader["whouploaded"] is DBNull) ? null : new int?((int)reader["whouploaded"])),
					FileTypeCode = (eDynamicFileTypeCode)((num != null && Enum.IsDefined(typeof(eDynamicFileTypeCode), num.Value)) ? num.Value : 0),
					FileContents = new BinaryFile
					{
						FileName = reader["filename"].ToString(),
						ByteArray = ((reader["filebytes"] is DBNull) ? null : ((byte[])reader["filebytes"]))
					}
				};
			}
			return result;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00042110 File Offset: 0x00040310
		public DynamicFile LoadDynamicFileById(int FileId, bool LoadFileContents)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@fileid", DbType.Int32, FileId),
				this.DatabaseManager.GetParameter("@loadfilecontents", DbType.Boolean, LoadFileContents)
			};
			DynamicFile result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    f.fileid,CASE WHEN @loadfilecontents=1 THEN f.filebytes ELSE CAST(NULL AS image) END AS filebytes,f.filename,f.filetypecode,f.isencrypted,f.iscompressed,f.dateuploaded,f.whouploaded\r\nFROM        files f\r\nWHERE       f.fileid=@fileid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetDynamicFileFromReader(dataReader);
				}
			}
			return result;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x000421AC File Offset: 0x000403AC
		public IList<T> LoadPerStudentSingleFileDescriptionsByStudentAndControls<T>(int PersonId, params int[] cids) where T : DynamicFileDescription
		{
			DatabaseLayer databaseManager = this.DatabaseManager;
			string sql = "SELECT dataid,personid,controlid,CAST(metadata AS varchar(max)) AS metadata,CASE WHEN metadata IS NULL THEN controlvalue ELSE CAST(NULL AS varbinary(max)) END AS controlvalue \r\nFROM imageinfops WHERE personid=@pid AND controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))";
			Func<IDataReader, T> getItemFromRecord = (IDataReader g) => DynamicFileStorageDAO.GetSingleFileDescriptionFromRecord<T>(g);
			DbParameter[] array = new DbParameter[2];
			array[0] = this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId);
			array[1] = this.DatabaseManager.GetParameter("@cids", DbType.String, string.Join(",", (from g in cids
			select g.ToString()).ToArray<string>()));
			return databaseManager.ExecuteQueryReturnList(sql, getItemFromRecord, array);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00042258 File Offset: 0x00040458
		[DebuggerStepThrough]
		public Task<IList<T>> LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync<T>(int PersonId, params int[] cids) where T : DynamicFileDescription
		{
			DynamicFileStorageDAO.<LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__14<T> <LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__ = new DynamicFileStorageDAO.<LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__14<T>();
			<LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<T>>.Create();
			<LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__.<>4__this = this;
			<LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__.PersonId = PersonId;
			<LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__.cids = cids;
			<LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__.<>1__state = -1;
			<LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__.<>t__builder.Start<DynamicFileStorageDAO.<LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__14<T>>(ref <LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__);
			return <LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x000422AC File Offset: 0x000404AC
		public IList<DynamicFileDescription> LoadPerStudentFileListFileDescriptionsByStudentAndControls(int PersonId, params int[] cids)
		{
			DatabaseLayer databaseManager = this.DatabaseManager;
			string sql = "SELECT dataid,personid,controlid,controlvalue,CAST(NULL AS varchar(max)) AS metadata \r\nFROM otherinfops WHERE personid=@pid AND controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))";
			Func<IDataReader, IList<DynamicFileDescription>> getItemsFromRecord = new Func<IDataReader, IList<DynamicFileDescription>>(DynamicFileStorageDAO.GetFileListFileDescriptionFromRecord<DynamicFileDescription>);
			DbParameter[] array = new DbParameter[2];
			array[0] = this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId);
			array[1] = this.DatabaseManager.GetParameter("@cids", DbType.String, string.Join(",", (from g in cids
			select g.ToString()).ToArray<string>()));
			return databaseManager.ExecuteQueryReturnList(sql, getItemsFromRecord, array);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00042344 File Offset: 0x00040544
		public IList<DynamicFileDescriptionWithColData> LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControls(int PersonId, params int[] cids)
		{
			DatabaseLayer databaseManager = this.DatabaseManager;
			string sql = "SELECT dataid,personid,controlid,controlvalue,CAST(NULL AS varchar(max)) AS metadata \r\nFROM otherinfops WHERE personid=@pid AND controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))";
			Func<IDataReader, IList<DynamicFileDescriptionWithColData>> getItemsFromRecord = new Func<IDataReader, IList<DynamicFileDescriptionWithColData>>(DynamicFileStorageDAO.GetFileListFileDescriptionFromRecord<DynamicFileDescriptionWithColData>);
			DbParameter[] array = new DbParameter[2];
			array[0] = this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId);
			array[1] = this.DatabaseManager.GetParameter("@cids", DbType.String, string.Join(",", (from g in cids
			select g.ToString()).ToArray<string>()));
			return databaseManager.ExecuteQueryReturnList(sql, getItemsFromRecord, array);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x000423DC File Offset: 0x000405DC
		[DebuggerStepThrough]
		public Task<IList<DynamicFileDescription>> LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync(int PersonId, params int[] cids)
		{
			DynamicFileStorageDAO.<LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__17 <LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__ = new DynamicFileStorageDAO.<LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__17();
			<LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<DynamicFileDescription>>.Create();
			<LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__.<>4__this = this;
			<LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__.PersonId = PersonId;
			<LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__.cids = cids;
			<LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__.<>1__state = -1;
			<LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__.<>t__builder.Start<DynamicFileStorageDAO.<LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__17>(ref <LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__);
			return <LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00042430 File Offset: 0x00040630
		[DebuggerStepThrough]
		public Task<IList<DynamicFileDescriptionWithColData>> LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync(int PersonId, params int[] cids)
		{
			DynamicFileStorageDAO.<LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__18 <LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__ = new DynamicFileStorageDAO.<LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__18();
			<LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<DynamicFileDescriptionWithColData>>.Create();
			<LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__.<>4__this = this;
			<LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__.PersonId = PersonId;
			<LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__.cids = cids;
			<LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__.<>1__state = -1;
			<LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__.<>t__builder.Start<DynamicFileStorageDAO.<LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__18>(ref <LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__);
			return <LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0400030B RID: 779
		private DatabaseLayer DatabaseManager;

		// Token: 0x0400030C RID: 780
		private DynamicFieldDAO _dynamicFieldDao;
	}
}
