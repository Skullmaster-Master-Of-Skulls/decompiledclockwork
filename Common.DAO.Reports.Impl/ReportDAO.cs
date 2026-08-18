using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.Compression;
using TechnoPro.Common.Compression.Entity;
using TechnoPro.Common.DAO.Entity.Reports;
using TechnoPro.Common.DAO.Reports.Impl.Legacy;
using TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity;
using TechnoPro.Common.DAO.Reports.Impl.Properties;
using TechnoPro.Common.DynamicCompiler;
using TechnoPro.Common.DynamicCompiler.CompilerArgs.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.Reports.Serialization;
using TechnoPro.Common.Public.Exceptions.DatabaseOperations;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;

namespace TechnoPro.Common.DAO.Reports.Impl
{
	// Token: 0x02000008 RID: 8
	public class ReportDAO : IReportDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000025D0 File Offset: 0x000007D0
		public ReportDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000025E2 File Offset: 0x000007E2
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000025EA File Offset: 0x000007EA
		public OperationContext OpContext { get; set; }

		// Token: 0x06000019 RID: 25 RVA: 0x000025F4 File Offset: 0x000007F4
		private ReportCollection LoadReportsFromLegacyXml(string reportsXml, ReportContext context)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DataSet dataSet = new DataSet();
			using (StringReader stringReader = new StringReader(reportsXml))
			{
				dataSet.ReadXml(stringReader, XmlReadMode.Auto);
			}
			DataTable dataTable = dataSet.Tables["searchinfo"];
			DataTable dataTable2 = dataSet.Tables["searchfunctions"];
			DataTable dataTable3 = dataSet.Tables["searchgroupinfo"];
			dataTable.Columns.Add("whocreatedpid", typeof(int));
			dataTable.Columns.Add("whocreatedfirstname", typeof(byte[]));
			dataTable.Columns.Add("whocreatedlastname", typeof(byte[]));
			dataTable.Columns.Add("whocreatedstudent_no", typeof(byte[]));
			dataTable.Columns.Add("wholastmodifiedfirstname", typeof(byte[]));
			dataTable.Columns.Add("wholastmodifiedlastname", typeof(byte[]));
			dataTable.Columns.Add("wholastmodifiedstudent_no", typeof(byte[]));
			dataTable.Columns.Add("searchfunctionid", typeof(int));
			dataTable.Columns.Add("functioncode", typeof(int));
			dataTable.Columns.Add("functionparameters");
			dataTable.Columns.Add("functionordernum", typeof(int));
			dataTable.Columns.Add("custom");
			dataTable.Columns.Add("customsqlinjection");
			dataTable.Columns.Add("customsqlinjectionoperator");
			dataTable.Columns.Add("isactive");
			dataTable.Columns.Add("wholastmodifiedpid", typeof(int));
			byte[] value = databaseLayer.Encryption.Encrypt("ClockWork");
			byte[] value2 = databaseLayer.Encryption.Encrypt("TechnoPro");
			byte[] value3 = databaseLayer.Encryption.Encrypt("TPRO");
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				dataRow["whocreatedpid"] = 1000000000;
				dataRow["whocreatedfirstname"] = value;
				dataRow["whocreatedlastname"] = value2;
				dataRow["whocreatedstudent_no"] = value3;
				dataRow["isactive"] = true;
			}
			this.AddTproReportGroupIds(dataTable, null);
			bool flag = !context.ReturnReportDisplayInformationOnly;
			if (flag)
			{
				DataTable dataTable4 = dataTable.Clone();
				foreach (object obj2 in dataTable.Rows)
				{
					DataRow dataRow2 = (DataRow)obj2;
					int item = (int)dataRow2["searchinfoid"];
					bool flag2 = context.ReportIds == null || context.ReportIds.Count < 1 || context.ReportIds.Contains(item);
					if (flag2)
					{
						DataRow[] array = dataTable2.Select("searchinfoid=" + item.ToString());
						bool flag3 = array.Length != 0;
						if (flag3)
						{
							foreach (DataRow dataRow3 in array)
							{
								dataRow2["searchfunctionid"] = dataRow3["searchfunctionid"];
								dataRow2["functioncode"] = dataRow3["functioncode"];
								dataRow2["functionparameters"] = dataRow3["functionparameters"];
								dataRow2["functionordernum"] = dataRow3["ordernum"];
								dataTable4.ImportRow(dataRow2);
							}
						}
						else
						{
							dataTable4.ImportRow(dataRow2);
						}
					}
				}
				dataTable = dataTable4;
			}
			ReportCollection reportsFromReader = this.GetReportsFromReader(dataTable.CreateDataReader());
			foreach (TechnoPro.Common.Public.Entities.Reports.Report report in reportsFromReader.Reports)
			{
				report.IsTechnoProReport = true;
			}
			bool flag4 = context.ReportIds != null && context.ReportIds.Count > 0;
			if (flag4)
			{
				reportsFromReader.Reports = (from g in reportsFromReader.Reports
				where context.ReportIds.Contains(g.ReportId)
				select g).ToList<TechnoPro.Common.Public.Entities.Reports.Report>();
			}
			return reportsFromReader;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002B80 File Offset: 0x00000D80
		private TechnoPro.Common.Public.Entities.Reports.ReportFunction GetFunctionFromReader(IDataReader reader, bool parametersAreEncrypted)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			bool flag = reader["searchfunctionid"] == DBNull.Value;
			TechnoPro.Common.Public.Entities.Reports.ReportFunction result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (reader["functioncode"] == DBNull.Value) ? 0 : ((int)reader["functioncode"]);
				TechnoPro.Common.Public.Entities.Reports.ReportFunction reportFunction = new TechnoPro.Common.Public.Entities.Reports.ReportFunction
				{
					ReportFunctionId = (int)reader["searchfunctionid"],
					Description = reader["custom"].ToString(),
					ExampleUsage = "",
					FunctionCode = (eFunctionType)(Enum.IsDefined(typeof(eFunctionType), num) ? num : -1),
					FunctionParameters = new List<ReportParameter>(),
					OrderNum = ((reader["functionordernum"] == DBNull.Value) ? 0 : ((int)reader["functionordernum"])),
					ExecuteThisFunctionOnClientIfPossible = (ReportDAO.ReaderContainsColumn(reader, "RunOnClient") && reader["RunOnClient"] != DBNull.Value && (bool)reader["RunOnClient"])
				};
				reportFunction.Title = Enum.GetName(typeof(eFunctionType), reportFunction.FunctionCode);
				string text = reader["functionparameters"].ToString();
				if (parametersAreEncrypted)
				{
					byte[] encryptedText = Convert.FromBase64String(text);
					text = databaseLayer.Encryption.Decrypt(encryptedText);
				}
				reportFunction.FunctionParameters.Add(new ReportParameter
				{
					Name = "default",
					Value = text
				});
				result = reportFunction;
			}
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002D48 File Offset: 0x00000F48
		private ReportGroup GetGroupFromReader(IDataReader reader)
		{
			bool flag = reader["searchgroupid"] == DBNull.Value;
			ReportGroup result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ReportGroup reportGroup = new ReportGroup
				{
					Title = (ReportDAO.ReaderContainsColumn(reader, "grouptitle") ? reader["grouptitle"].ToString() : ""),
					GroupId = ((reader["searchgroupid"] == DBNull.Value) ? 0 : ((int)reader["searchgroupid"])),
					Description = (ReportDAO.ReaderContainsColumn(reader, "groupdescription") ? reader["groupdescription"].ToString() : ""),
					ParentGroupId = (ReportDAO.ReaderContainsColumn(reader, "parentsearchgroupid") ? ((reader["parentsearchgroupid"] == DBNull.Value) ? 0 : ((int)reader["parentsearchgroupid"])) : 0)
				};
				reportGroup.IsTechnoProGroup = (reportGroup.GroupId >= 2000000000);
				result = reportGroup;
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002E54 File Offset: 0x00001054
		private ReportParametersLegacy GetLegacyFromReader(IDataReader reader)
		{
			int num = (ReportDAO.ReaderContainsColumn(reader, "overridedynamiccontrolsscreennum") && reader["overridedynamiccontrolsscreennum"] != DBNull.Value) ? ((int)reader["overridedynamiccontrolsscreennum"]) : 0;
			bool flag = num < 1;
			ReportParametersLegacy result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ReportParametersLegacy reportParametersLegacy = new ReportParametersLegacy
				{
					BuiltInDynamicForm = (eReportBuiltInDynamicForm)(Enum.IsDefined(typeof(eReportBuiltInDynamicForm), num) ? num : 0)
				};
				result = reportParametersLegacy;
			}
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002ED4 File Offset: 0x000010D4
		private static bool ReaderContainsColumn(IDataReader reader, string colName)
		{
			for (int i = 0; i < reader.FieldCount; i++)
			{
				bool flag = reader.GetName(i).Equals(colName, StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002F14 File Offset: 0x00001114
		private static FormattedReport GetFormattedReportFromReader(IDataReader reader)
		{
			bool flag = reader == null;
			FormattedReport result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (!ReportDAO.ReaderContainsColumn(reader, "reportfileid") || reader["reportfileid"] is DBNull) ? 0 : ((int)reader["reportfileid"]);
				bool flag2 = num < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					byte[] array = (reader["formattedreportbytes"] is DBNull) ? null : ((byte[])reader["formattedreportbytes"]);
					bool flag3 = array == null || array.Length == 0;
					if (flag3)
					{
						result = new FormattedReport
						{
							Title = reader["formattedreporttitle"].ToString(),
							Description = reader["formattedreportdescription"].ToString(),
							ReportFileId = num,
							FormattedReportTemplate = array,
							FileChecksum = ((reader["filechecksum"] is DBNull) ? null : reader["filechecksum"].ToString()),
							OrderNum = ((reader["formattedreportordernum"] is DBNull) ? 0 : ((int)reader["formattedreportordernum"]))
						};
					}
					else
					{
						try
						{
							CompressionBinaryFile compressedFile = new CompressionBinaryFile
							{
								FileBytes = array,
								FileName = "FormattedReport.zip"
							};
							CompressionBinaryFile compressionBinaryFile = CompressDataAdapter.UncompressFirstLevelFile(compressedFile, "FormattedReport.mrt") ?? CompressDataAdapter.UncompressFirstLevelFile(compressedFile, "FormattedReport.zip");
							bool flag4 = compressionBinaryFile != null;
							if (flag4)
							{
								array = compressionBinaryFile.FileBytes;
							}
						}
						catch (Exception ex)
						{
							CWLogger.Logger.Error("Common.DAO.Reports.Impl.ReportDAO.GetFormattedReportFromReader:Failed to unzip file with FileId={0}", num.ToString());
						}
						result = new FormattedReport
						{
							Title = reader["formattedreporttitle"].ToString(),
							Description = reader["formattedreportdescription"].ToString(),
							ReportFileId = num,
							FormattedReportTemplate = array,
							FileChecksum = ((reader["filechecksum"] is DBNull) ? null : reader["filechecksum"].ToString()),
							OrderNum = ((reader["formattedreportordernum"] is DBNull) ? 0 : ((int)reader["formattedreportordernum"]))
						};
					}
				}
			}
			return result;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003184 File Offset: 0x00001384
		private ReportCollection GetReportsFromReader(IDataReader reader)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<ReportGroup> list = new List<ReportGroup>();
			List<TechnoPro.Common.Public.Entities.Reports.Report> list2 = new List<TechnoPro.Common.Public.Entities.Reports.Report>();
			TechnoPro.Common.Public.Entities.Reports.Report report = null;
			while (reader.Read())
			{
				object obj = reader["searchinfoid"];
				int num = (obj == DBNull.Value) ? 0 : ((int)obj);
				bool flag = report == null || num != report.ReportId;
				if (flag)
				{
					int reportId = (obj is DBNull) ? 0 : ((int)obj);
					report = new TechnoPro.Common.Public.Entities.Reports.Report
					{
						ReportId = reportId,
						ReportParameters = new List<ReportParameter>(),
						FormattedReports = new List<FormattedReport>()
					};
					object obj2 = reader["title"];
					object obj3 = reader["description"];
					object obj4 = reader["datecreated"];
					object obj5 = reader["datelastmodified"];
					object obj6 = reader["whocreatedpid"];
					object obj7 = reader["wholastmodifiedpid"];
					object obj8 = reader["ordernum"];
					object obj9 = reader["searchchartinfoid"];
					report.Title = ((obj2 == DBNull.Value) ? "" : ((string)obj2));
					report.Description = ((obj3 == DBNull.Value) ? "" : ((string)obj3));
					report.FunctionParametersAreEncrypted = (obj9 != DBNull.Value && (int)obj9 == 999);
					report.OrderNum = ((obj8 is DBNull) ? 0 : ((int)obj8));
					report.IsBuiltByTpro = (ReportDAO.ReaderContainsColumn(reader, "IsBuiltByTpro") && !(reader["IsBuiltByTpro"] is DBNull) && Convert.ToBoolean(reader["IsBuiltByTpro"]));
					string text = ReportDAO.ReaderContainsColumn(reader, "reportoptions") ? reader["reportoptions"].ToString().Trim() : "";
					bool flag2 = text.Length > 0;
					if (flag2)
					{
						report.ReportOptions = text.GetReportOptionsFromXml();
					}
					report.CreatedByLocation = (ReportDAO.ReaderContainsColumn(reader, "createdbylocation") ? reader["createdbylocation"].ToString() : "");
					try
					{
						report.ReportUniqueId = new Guid(reader["reportuniqueid"].ToString());
					}
					catch
					{
					}
					bool flag3 = obj4 != DBNull.Value;
					if (flag3)
					{
						report.DateCreated = (DateTime)obj4;
					}
					bool flag4 = obj6 != DBNull.Value;
					if (flag4)
					{
						int num2 = (int)obj6;
						bool flag5 = num2 > 0;
						if (flag5)
						{
							object obj10 = reader["whocreatedfirstname"];
							object obj11 = reader["whocreatedlastname"];
							report.WhoCreated = new PersonBase
							{
								PersonId = num2,
								FirstName = ((obj10 == DBNull.Value) ? "" : databaseLayer.Encryption.Decrypt((byte[])obj10)),
								LastName = ((obj11 == DBNull.Value) ? "" : databaseLayer.Encryption.Decrypt((byte[])obj11))
							};
						}
					}
					bool flag6 = obj5 != DBNull.Value;
					if (flag6)
					{
						report.DateLastModified = (DateTime)obj5;
					}
					bool flag7 = obj7 != DBNull.Value;
					if (flag7)
					{
						int num3 = (int)obj7;
						bool flag8 = num3 > 0;
						if (flag8)
						{
							object obj12 = reader["wholastmodifiedfirstname"];
							object obj13 = reader["wholastmodifiedlastname"];
							report.WhoLastModified = new PersonBase
							{
								PersonId = num3,
								FirstName = ((obj12 == DBNull.Value) ? "" : databaseLayer.Encryption.Decrypt((byte[])obj12)),
								LastName = ((obj13 == DBNull.Value) ? "" : databaseLayer.Encryption.Decrypt((byte[])obj13))
							};
						}
					}
					ReportExecutionContext reportExecutionContextFromRecord = this.GetReportExecutionContextFromRecord(reader);
					bool flag9 = reportExecutionContextFromRecord != null;
					if (flag9)
					{
						report.DateLastExecuted = reportExecutionContextFromRecord.ExecutionTimestamp;
						report.WhoLastExecuted = reportExecutionContextFromRecord.WhoExecuted;
					}
					ReportGroup group = this.GetGroupFromReader(reader);
					bool flag10 = group == null;
					if (flag10)
					{
						report.GroupId = 0;
					}
					else
					{
						bool flag11 = !string.IsNullOrEmpty(group.Title);
						if (flag11)
						{
							ReportGroup reportGroup = list.FirstOrDefault((ReportGroup rr) => rr.GroupId == group.GroupId);
							bool flag12 = reportGroup == null;
							if (flag12)
							{
								list.Add(group);
							}
							report.GroupId = group.GroupId;
						}
						else
						{
							bool isTechnoProGroup = group.IsTechnoProGroup;
							if (isTechnoProGroup)
							{
								report.GroupId = group.GroupId;
							}
							else
							{
								report.GroupId = 0;
							}
						}
					}
					ReportParametersLegacy legacyFromReader = this.GetLegacyFromReader(reader);
					report.LegacyParameters = legacyFromReader;
					list2.Add(report);
				}
				TechnoPro.Common.Public.Entities.Reports.ReportFunction function = this.GetFunctionFromReader(reader, report.FunctionParametersAreEncrypted);
				bool flag13 = function != null && (function.ReportFunctionId < 1 || report.Functions.FirstOrDefault((TechnoPro.Common.Public.Entities.Reports.ReportFunction g) => g.ReportFunctionId == function.ReportFunctionId) == null);
				if (flag13)
				{
					report.Functions.Add(function);
				}
				FormattedReport formattedReport = ReportDAO.GetFormattedReportFromReader(reader);
				bool flag14 = formattedReport != null && (formattedReport.ReportFileId < 1 || report.FormattedReports.FirstOrDefault((FormattedReport g) => g.ReportFileId == formattedReport.ReportFileId) == null);
				if (flag14)
				{
					report.FormattedReports.Add(formattedReport);
				}
			}
			return new ReportCollection
			{
				Reports = list2,
				ReportGroups = list
			};
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000037A4 File Offset: 0x000019A4
		private void UpdateLastExecutionInfoForTproReports(IList<TechnoPro.Common.Public.Entities.Reports.Report> reports)
		{
			int num;
			if (reports.Count >= 1)
			{
				num = reports.Min((TechnoPro.Common.Public.Entities.Reports.Report g) => (g.ReportId >= 500000) ? g.ReportId : int.MaxValue);
			}
			else
			{
				num = 0;
			}
			int num2 = num;
			bool flag = num2 == int.MaxValue || num2 <= 0;
			if (!flag)
			{
				IList<ReportExecutionContext> source = this.LoadReportLastExecutionInfoByMinReportId(num2);
				IEnumerable<TechnoPro.Common.Public.Entities.Reports.Report> enumerable = from g in reports
				where g.IsTechnoProReport
				select g;
				using (IEnumerator<TechnoPro.Common.Public.Entities.Reports.Report> enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TechnoPro.Common.Public.Entities.Reports.Report r = enumerator.Current;
						ReportExecutionContext reportExecutionContext = source.FirstOrDefault((ReportExecutionContext g) => g.ReportId == r.ReportId);
						bool flag2 = reportExecutionContext == null;
						if (!flag2)
						{
							r.DateLastExecuted = reportExecutionContext.ExecutionTimestamp;
							r.WhoLastExecuted = reportExecutionContext.WhoExecuted;
						}
					}
				}
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000038C4 File Offset: 0x00001AC4
		private ReportExecutionContext GetReportExecutionContextFromRecord(IDataReader record)
		{
			bool flag = record == null || !ReportDAO.ReaderContainsColumn(record, "datelastexecuted") || record["datelastexecuted"] is DBNull;
			ReportExecutionContext result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DateTime executionTimestamp = (DateTime)record["datelastexecuted"];
				int num = (record["wholastexecutedpid"] is DBNull) ? 0 : ((int)record["wholastexecutedpid"]);
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				IEncryption encryption = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).Encryption;
				PersonBase whoExecuted = (num < 1) ? null : new PersonBase(num, ReportDAO.GetEncryptedValue(record["wholastexecutedfirstname"], encryption), ReportDAO.GetEncryptedValue(record["wholastexecutedmiddlename"], encryption), ReportDAO.GetEncryptedValue(record["wholastexecutedlastname"], encryption), "", Array.Empty<eCoreGroup>());
				result = new ReportExecutionContext
				{
					ReportId = ((record["searchinfoid"] is DBNull) ? 0 : ((int)record["searchinfoid"])),
					ExecutionTimestamp = executionTimestamp,
					WhoExecutedPersonId = num,
					WhoExecuted = whoExecuted
				};
			}
			return result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003A00 File Offset: 0x00001C00
		private IList<ReportExecutionContext> LoadReportLastExecutionInfoByMinReportId(int minReportId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@minreportid", DbType.Int32, minReportId)
			};
			List<ReportExecutionContext> list = new List<ReportExecutionContext>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    le.reportid AS searchinfoid,le.personid AS wholastexecutedpid,le.firstname AS wholastexecutedfirstname,le.middlename AS wholastexecutedmiddlename,le.lastname AS wholastexecutedlastname,le.DateExecuted AS datelastexecuted\r\nFROM    ReportLastExecutions le \r\nWHERE   le.reportid>=@minreportid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return list;
				}
				while (dataReader.Read())
				{
					ReportExecutionContext reportExecutionContextFromRecord = this.GetReportExecutionContextFromRecord(dataReader);
					bool flag2 = reportExecutionContextFromRecord != null;
					if (flag2)
					{
						list.Add(reportExecutionContextFromRecord);
					}
				}
			}
			return list;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003ABC File Offset: 0x00001CBC
		private static string GetEncryptedValue(object value, IEncryption encryption)
		{
			return (value == null || value == DBNull.Value) ? "" : encryption.Decrypt((byte[])value);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003AEC File Offset: 0x00001CEC
		private bool IsXmlNew(string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			return !flag && xml.IndexOf("<reportpackage>", StringComparison.OrdinalIgnoreCase) >= 0;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003B20 File Offset: 0x00001D20
		public ReportCollection LoadReportsFromXml(string reportsXml, ReportContext context)
		{
			bool flag = string.IsNullOrEmpty(reportsXml);
			ReportCollection result;
			if (flag)
			{
				result = new ReportCollection
				{
					Reports = new List<TechnoPro.Common.Public.Entities.Reports.Report>(),
					ReportGroups = new List<ReportGroup>()
				};
			}
			else
			{
				bool flag2 = !this.IsXmlNew(reportsXml);
				if (flag2)
				{
					result = this.LoadReportsFromLegacyXml(reportsXml, context);
				}
				else
				{
					ReportCollection reportCollection = reportsXml.ParseReportsFromNewXml(!context.ReturnReportDisplayInformationOnly);
					this.UpdateLastExecutionInfoForTproReports(reportCollection.Reports);
					result = reportCollection;
				}
			}
			return result;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003B94 File Offset: 0x00001D94
		private void AddTproReportGroupIds(DataTable searchinfo, DataTable searchGroupInfo)
		{
			bool flag = searchinfo != null;
			if (flag)
			{
				searchinfo.Columns.Add("parentsearchgroupid", typeof(int));
				bool flag2 = !searchinfo.Columns.Contains("searchgroupid");
				if (flag2)
				{
					searchinfo.Columns.Add("searchgroupid", typeof(int));
				}
				searchinfo.Columns.Add("parentgroupdescription");
			}
			bool flag3 = searchGroupInfo != null;
			if (flag3)
			{
				foreach (object obj in searchGroupInfo.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					DataRow dataRow2 = searchinfo.NewRow();
					dataRow2["searchgroupid"] = dataRow["searchgroupinfoid"];
					dataRow2["grouptitle"] = dataRow["grouptitle"];
					searchinfo.Rows.Add(dataRow2);
				}
			}
			int num = 1000000000;
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			bool flag4 = searchinfo == null;
			if (!flag4)
			{
				foreach (object obj2 in searchinfo.Rows)
				{
					DataRow dataRow3 = (DataRow)obj2;
					string text = searchinfo.Columns.Contains("grouptitle") ? dataRow3["grouptitle"].ToString().Trim() : null;
					bool flag5 = string.IsNullOrEmpty(text);
					if (!flag5)
					{
						string key = text.ToLower();
						bool flag6 = dictionary.ContainsKey(key);
						if (flag6)
						{
							dataRow3["searchgroupid"] = dictionary[key];
						}
						else
						{
							int num2 = num++;
							dataRow3["searchgroupid"] = num2;
							dictionary.Add(key, num2);
						}
					}
				}
				foreach (object obj3 in searchinfo.Rows)
				{
					DataRow dataRow4 = (DataRow)obj3;
					string text2 = searchinfo.Columns.Contains("parentgrouptitle") ? dataRow4["parentgrouptitle"].ToString().Trim() : null;
					bool flag7 = string.IsNullOrEmpty(text2);
					if (!flag7)
					{
						DataRow[] array = searchinfo.Select(string.Format("grouptitle='{0}'", text2.Replace("'", "''")));
						bool flag8 = array.Length == 0;
						if (!flag8)
						{
							dataRow4["parentsearchgroupid"] = array[0]["searchgroupid"];
							dataRow4["parentgroupdescription"] = array[0]["groupdescription"];
						}
					}
				}
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003EB0 File Offset: 0x000020B0
		public DataTable RunReportSqlExternal(eExternalQueryDatabaseType dbType, string providerType, string connectionString, string sql, IList<ReportParameter> reportParameters)
		{
			ReportDAO.<>c__DisplayClass19_0 CS$<>8__locals1 = new ReportDAO.<>c__DisplayClass19_0();
			ReportDAO.<>c__DisplayClass19_0 CS$<>8__locals2 = CS$<>8__locals1;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			CS$<>8__locals2.databaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			bool flag = reportParameters != null && reportParameters.Count > 0;
			DbParameter[] array;
			if (flag)
			{
				array = reportParameters.ToList<ReportParameter>().ConvertAll<DbParameter>((ReportParameter g) => g.ConvertToDbParameter(CS$<>8__locals1.databaseManager)).ToArray();
			}
			else
			{
				array = null;
			}
			switch (dbType)
			{
			case eExternalQueryDatabaseType.sqlserver:
			{
				SqlConnection selectConnection = new SqlConnection(connectionString);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("", selectConnection);
				sqlDataAdapter.SelectCommand.CommandText = sql;
				sqlDataAdapter.SelectCommand.Parameters.Clear();
				bool flag2 = reportParameters != null;
				if (flag2)
				{
					foreach (ReportParameter reportParameter in reportParameters)
					{
						string text = reportParameter.Name;
						bool flag3 = !text.StartsWith("@");
						if (flag3)
						{
							text = "@" + text;
						}
						sqlDataAdapter.SelectCommand.Parameters.AddWithValue(text, reportParameter.Value);
					}
				}
				try
				{
					DataTable dataTable = new DataTable("t");
					sqlDataAdapter.Fill(dataTable);
					return dataTable;
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("ReportRun:SqlExternalQuery:sqlserver:sql={0}:error={1}", sql ?? "", ex.ToString());
				}
				break;
			}
			case eExternalQueryDatabaseType.oledb:
			{
				OleDbConnection selectConnection2 = new OleDbConnection(connectionString);
				OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("", selectConnection2);
				oleDbDataAdapter.SelectCommand.CommandText = sql;
				oleDbDataAdapter.SelectCommand.Parameters.Clear();
				bool flag4 = reportParameters != null;
				if (flag4)
				{
					foreach (ReportParameter reportParameter2 in reportParameters)
					{
						string text2 = reportParameter2.Name;
						bool flag5 = !text2.StartsWith("@");
						if (flag5)
						{
							text2 = "@" + text2;
						}
						oleDbDataAdapter.SelectCommand.Parameters.AddWithValue(text2, reportParameter2.Value);
					}
				}
				try
				{
					DataTable dataTable = new DataTable("t");
					oleDbDataAdapter.Fill(dataTable);
					return dataTable;
				}
				catch (Exception ex2)
				{
					CWLogger.Logger.Error("ReportRun:SqlExternalQuery:oledb:sql={0}:error={1}", sql ?? "", ex2.ToString());
				}
				break;
			}
			case eExternalQueryDatabaseType.factory:
			{
				eDatabaseConnectionStringName csName2 = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext2 = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName2, (opContext2 != null) ? opContext2.TenantId : null);
				databaseLayer.ProviderName = providerType;
				databaseLayer.ConnectionString = connectionString;
				bool flag6 = array == null;
				if (flag6)
				{
					return databaseLayer.ExecuteQuery(sql);
				}
				return databaseLayer.ExecuteQuery(sql, array);
			}
			case eExternalQueryDatabaseType.odbc:
			{
				OdbcConnection selectConnection3 = new OdbcConnection(connectionString);
				OdbcDataAdapter odbcDataAdapter = new OdbcDataAdapter("", selectConnection3);
				odbcDataAdapter.SelectCommand.CommandText = sql;
				odbcDataAdapter.SelectCommand.Parameters.Clear();
				bool flag7 = reportParameters != null;
				if (flag7)
				{
					foreach (ReportParameter reportParameter3 in reportParameters)
					{
						string text3 = reportParameter3.Name;
						bool flag8 = !text3.StartsWith("@");
						if (flag8)
						{
							text3 = "@" + text3;
						}
						odbcDataAdapter.SelectCommand.Parameters.AddWithValue(text3, reportParameter3.Value);
					}
				}
				try
				{
					DataTable dataTable = new DataTable("t");
					odbcDataAdapter.Fill(dataTable);
					return dataTable;
				}
				catch (Exception ex3)
				{
					CWLogger.Logger.Error("ReportRun:SqlExternalQuery:odbc:sql={0}:error={1}", sql ?? "", ex3.ToString());
				}
				break;
			}
			case eExternalQueryDatabaseType.odbc2:
			{
				OdbcConnection selectConnection4 = new OdbcConnection(connectionString);
				OdbcDataAdapter odbcDataAdapter2 = new OdbcDataAdapter("", selectConnection4);
				odbcDataAdapter2.SelectCommand.CommandText = sql;
				odbcDataAdapter2.SelectCommand.Parameters.Clear();
				bool flag9 = reportParameters != null;
				if (flag9)
				{
					foreach (ReportParameter reportParameter4 in reportParameters)
					{
						string text4 = reportParameter4.Name;
						bool flag10 = !text4.StartsWith("&");
						if (flag10)
						{
							text4 = "&" + text4;
						}
						odbcDataAdapter2.SelectCommand.Parameters.AddWithValue(text4, reportParameter4.Value);
					}
				}
				try
				{
					DataTable dataTable = new DataTable("t");
					odbcDataAdapter2.Fill(dataTable);
				}
				catch (Exception ex4)
				{
					CWLogger.Logger.Error("ReportRun:SqlExternalQuery:odbc2:sql={0}:error={1}", sql ?? "", ex4.ToString());
				}
				break;
			}
			default:
				throw new NotImplementedException();
			}
			return null;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00004418 File Offset: 0x00002618
		private void CheckForCodeInsertions(ref IList<ReportParameter> reportParameters, ref string sql)
		{
			bool flag = string.IsNullOrEmpty(sql);
			if (!flag)
			{
				Regex regex = new Regex("#<--[A-Za-z0-9_:]*>#");
				MatchCollection matchCollection = regex.Matches(sql);
				foreach (object obj in matchCollection)
				{
					Match match = (Match)obj;
					string text = match.Value.Substring(4, match.Value.Length - 6);
					int num = text.IndexOf(':');
					string text2 = (num > 0) ? text.Substring(0, num) : text;
					string reportCommentStart = (num > 0) ? text.Substring(num + 1).Trim().ToLower() : "";
					int reportNum;
					bool flag2 = reportCommentStart.Length > 0 && text2.Length > 0 && int.TryParse(text2, out reportNum);
					if (flag2)
					{
						TechnoPro.Common.Public.Entities.Reports.Report report = this.LoadClientReportById(reportNum);
						bool flag3 = report == null;
						if (flag3)
						{
							ReportCollection reportCollection = this.LoadTproReports(new ReportContext
							{
								ReportIds = new List<int>
								{
									reportNum
								},
								ReturnReportDisplayInformationOnly = false
							});
							report = ((reportCollection == null || reportCollection.Reports == null) ? null : reportCollection.Reports.FirstOrDefault((TechnoPro.Common.Public.Entities.Reports.Report g) => g.ReportId == reportNum));
						}
						bool flag4 = report == null;
						string newValue;
						if (flag4)
						{
							newValue = "";
							CWLogger.Logger.Warn("ReportDAO:CheckForCodeInsertions:Can'tLoadReportWithReportId={0}", reportNum.ToString());
						}
						else
						{
							List<TechnoPro.Common.Public.Entities.Reports.ReportFunction> list = (from g in report.Functions
							where g.FunctionCode == eFunctionType.Sql_Query || g.FunctionCode == eFunctionType.Sql_Query_Dynamic_Data || g.FunctionCode == eFunctionType.Sql_Query_Dynamic_Data_Keep_Rows_Without_Data_Info || g.FunctionCode == eFunctionType.Sql_Query_Dynamic_Data_2_Per_Student || g.FunctionCode == eFunctionType.Sql_Query_Dynamic_Data_2_Per_Appointment
							select g).ToList<TechnoPro.Common.Public.Entities.Reports.ReportFunction>();
							bool flag5 = list.Count > 0;
							if (flag5)
							{
								TechnoPro.Common.Public.Entities.Reports.ReportFunction reportFunction = list.FirstOrDefault((TechnoPro.Common.Public.Entities.Reports.ReportFunction g) => ReportDAO.GetCodeBit(ReportDAO.GetDefaultFunctionParameter(g), reportCommentStart).Length > 0);
								newValue = ((reportFunction != null) ? ReportDAO.GetCodeBit(ReportDAO.GetDefaultFunctionParameter(reportFunction), reportCommentStart) : "");
							}
							else
							{
								newValue = "";
							}
						}
						sql = sql.Replace("#<--" + text + ">#", newValue);
					}
				}
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00004694 File Offset: 0x00002894
		private static string GetCodeBit(string x, string reportCommentStart)
		{
			int num = x.IndexOf("--" + reportCommentStart);
			bool flag = num < 0;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				int num2 = x.IndexOf(";", num + 1);
				bool flag2 = num2 < 0;
				if (flag2)
				{
					num2 = x.IndexOf("--", num + 1);
				}
				else
				{
					num2++;
				}
				bool flag3 = num2 < 0;
				if (flag3)
				{
					num2 = x.Length - 1;
				}
				int num3 = num2 - num;
				bool flag4 = num3 <= 0;
				if (flag4)
				{
					result = "";
				}
				else
				{
					result = x.Substring(num, num3) + Environment.NewLine;
				}
			}
			return result;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00004738 File Offset: 0x00002938
		private static string GetDefaultFunctionParameter(TechnoPro.Common.Public.Entities.Reports.ReportFunction Function)
		{
			ReportParameter reportParameter = Function.FunctionParameters.FirstOrDefault((ReportParameter f) => f.Name.Equals("default", StringComparison.OrdinalIgnoreCase));
			return (reportParameter == null) ? "" : reportParameter.Value.ToString();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000478A File Offset: 0x0000298A
		private void CheckForDatabaseSpecificStrings(ref IList<ReportParameter> reportParameters, ref string sql)
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00004790 File Offset: 0x00002990
		private void FixEndDates(ref IList<ReportParameter> reportParameters, ref string sql)
		{
			bool flag = reportParameters == null;
			if (!flag)
			{
				ReportParameter reportParameter = reportParameters.FirstOrDefault((ReportParameter g) => g.Name.Equals("@enddate", StringComparison.OrdinalIgnoreCase));
				ReportParameter reportParameter2 = reportParameters.FirstOrDefault((ReportParameter g) => g.Name.Equals("@schoolyear_enddate", StringComparison.OrdinalIgnoreCase));
				this.FixEndDateParameter(ref reportParameter);
				this.FixEndDateParameter(ref reportParameter2);
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000480C File Offset: 0x00002A0C
		private void FixEndDateParameter(ref ReportParameter p)
		{
			bool flag = p == null;
			if (!flag)
			{
				bool flag2 = p.Value == null || p.Value == DBNull.Value || !(p.Value is DateTime);
				if (!flag2)
				{
					DateTime dateTime = (DateTime)p.Value;
					p.Value = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, 59);
				}
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00004893 File Offset: 0x00002A93
		private void SetVariables_ifelseif(ref IList<ReportParameter> reportParameters, ref string sql)
		{
			TechnoPro.Common.DAO.Reports.Impl.Legacy.ReportFunction.SetVariables_ifelseif(ref reportParameters);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000048A0 File Offset: 0x00002AA0
		private void CheckForParametersToEncrypt(ref IList<ReportParameter> reportParameters, ref string sql, IEncryption encryption)
		{
			Regex regex = new Regex("@!([_a-zA-Z]+)");
			MatchCollection matchCollection = regex.Matches(sql);
			bool flag = matchCollection.Count > 0;
			if (flag)
			{
				foreach (object obj in matchCollection)
				{
					Match match = (Match)obj;
					bool flag2 = string.IsNullOrEmpty(match.Value) || match.Value.Length <= 2;
					if (!flag2)
					{
						string pName = string.Format("{0}", match.Value.Substring(2));
						string pName2 = pName + "e";
						ReportParameter reportParameter = reportParameters.FirstOrDefault((ReportParameter g) => g.Name.Equals(pName, StringComparison.OrdinalIgnoreCase));
						bool flag3 = reportParameter == null;
						if (!flag3)
						{
							object value = reportParameter.Value;
							byte[] value2 = (value == null) ? new byte[0] : encryption.Encrypt(value.ToString());
							ReportParameter reportParameter2 = reportParameters.FirstOrDefault((ReportParameter g) => g.Name.Equals(pName2, StringComparison.OrdinalIgnoreCase));
							bool flag4 = reportParameter2 != null;
							if (flag4)
							{
								reportParameter2.Value = value2;
							}
							else
							{
								reportParameters.Add(new ReportParameter
								{
									Name = pName2,
									Value = value2
								});
							}
						}
					}
				}
			}
			bool flag5 = reportParameters == null;
			if (!flag5)
			{
				List<ReportParameter> list = (from g in reportParameters
				where g.Name.EndsWith("encrypted", StringComparison.OrdinalIgnoreCase)
				select g).ToList<ReportParameter>();
				foreach (ReportParameter reportParameter3 in list)
				{
					string plainText = (reportParameter3.Value == null) ? "" : reportParameter3.Value.ToString();
					reportParameter3.Value = encryption.Encrypt(plainText);
				}
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00004AEC File Offset: 0x00002CEC
		public DataTable RunReportSql(IList<ReportParameter> reportParameters1, string sql1)
		{
			return this.RunReportSql(reportParameters1, sql1, 0);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00004B08 File Offset: 0x00002D08
		public DataTable RunReportSql(IList<ReportParameter> reportParameters1, string sql1, int overrideCommandTimeoutInSeconds)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			string query = sql1;
			IList<ReportParameter> list = reportParameters1;
			this.CheckForCodeInsertions(ref list, ref query);
			this.CheckForDatabaseSpecificStrings(ref list, ref query);
			this.FixEndDates(ref list, ref query);
			this.SetVariables_ifelseif(ref list, ref query);
			eDatabaseConnectionStringName csName2 = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.CheckForParametersToEncrypt(ref list, ref query, DatabaseLayerFactory.GetDatabaseLayer(csName2, (opContext2 != null) ? opContext2.TenantId : null).Encryption);
			bool flag = list == null;
			DataTable result;
			if (flag)
			{
				result = databaseLayer.ExecuteQuery(query, (overrideCommandTimeoutInSeconds > 0) ? new CommandOverrideSettings(overrideCommandTimeoutInSeconds) : CommandOverrideSettings.CommandOverrideSettingsTimeout180);
			}
			else
			{
				List<DbParameter> list2 = new List<DbParameter>();
				using (IEnumerator<ReportParameter> enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ReportParameter rp = enumerator.Current;
						DbParameter dbParameter = rp.ConvertToDbParameter(databaseLayer);
						DbParameter dbParameter2 = list2.FirstOrDefault((DbParameter g) => g.ParameterName.Equals(rp.Name, StringComparison.OrdinalIgnoreCase));
						bool flag2 = dbParameter2 == null;
						if (flag2)
						{
							list2.Add(dbParameter);
						}
						else
						{
							bool flag3 = rp == null || rp.Value != null;
							if (flag3)
							{
								try
								{
									dbParameter2.Value = dbParameter.Value;
								}
								catch (Exception ex)
								{
									CWLogger.Logger.Error("Common.DAO.Reports.Impl.ReportDAO.RunReportSql:UnableToSetValue:pname={0}:pval={1}:foundval={2}:pvaltype={3}:foundvaltype={4}:err={5}", new object[]
									{
										(rp == null) ? "NULL1" : (rp.Name ?? "NULL"),
										(rp == null) ? "NULL1" : (rp.Value ?? "NULL"),
										dbParameter2.Value ?? "NULL",
										(rp == null) ? "NULL1" : ((rp.Value == null) ? "NULL" : rp.Value.GetType().ToString()),
										(dbParameter2.Value == null) ? "NULL" : dbParameter2.Value.GetType().ToString(),
										ex.ToString()
									});
								}
							}
						}
					}
				}
				result = databaseLayer.ExecuteQuery(query, (overrideCommandTimeoutInSeconds > 0) ? new CommandOverrideSettings(overrideCommandTimeoutInSeconds) : CommandOverrideSettings.CommandOverrideSettingsTimeout180, list2.ToArray());
			}
			return result;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00004DB4 File Offset: 0x00002FB4
		public DataTable DecryptData(DataTable t, params string[] colsToDecrypt)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			bool flag = t == null || colsToDecrypt == null || colsToDecrypt.Length < 1;
			DataTable result;
			if (flag)
			{
				result = t;
			}
			else
			{
				for (int i = 0; i < colsToDecrypt.Length; i++)
				{
					colsToDecrypt[i] = colsToDecrypt[i].ToLower();
				}
				result = databaseLayer.Encryption.EncryptOrDecryptNameDataTableBatch(false, t, colsToDecrypt);
			}
			return result;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00004E24 File Offset: 0x00003024
		public DataTable EncryptData(DataTable t, params string[] colsToEncrypt)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			bool flag = t == null || colsToEncrypt == null || colsToEncrypt.Length < 1;
			DataTable result;
			if (flag)
			{
				result = t;
			}
			else
			{
				for (int i = 0; i < colsToEncrypt.Length; i++)
				{
					colsToEncrypt[i] = colsToEncrypt[i].ToLower();
				}
				result = databaseLayer.Encryption.EncryptOrDecryptNameDataTableBatch(true, t, colsToEncrypt);
			}
			return result;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00004E94 File Offset: 0x00003094
		public object[] GetReportCodeCompileParameters(RunReportResult CurrentReportResult)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DataTable dataTable = (CurrentReportResult.PrimaryData != null && CurrentReportResult.PrimaryData.Table != null) ? CurrentReportResult.PrimaryData.Table : null;
			DataTable[] array;
			if (CurrentReportResult.AdditionalData != null)
			{
				array = CurrentReportResult.AdditionalData.ToList<RunFunctionData>().ConvertAll<DataTable>((RunFunctionData f) => (f.Table == null) ? null : f.Table).ToArray();
			}
			else
			{
				array = new DataTable[0];
			}
			DataTable[] array2 = array;
			DataTable dataTable2 = dataTable ?? null;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			bool flag = CurrentReportResult.Report.ReportParameters != null;
			if (flag)
			{
				foreach (ReportParameter reportParameter in CurrentReportResult.Report.ReportParameters)
				{
					bool flag2 = !dictionary.ContainsKey(reportParameter.Name);
					if (flag2)
					{
						dictionary.Add(reportParameter.Name, reportParameter.Value);
					}
				}
			}
			return new object[]
			{
				dataTable2,
				"",
				array2,
				"Provider=SQLOLEDB;" + databaseLayer.ConnectionString,
				databaseLayer.Encryption,
				dictionary
			};
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00004FF8 File Offset: 0x000031F8
		public DataTable ImportUserData(DataTable lastReportResultDataView, string parameters)
		{
			return this.ExecuteLegacyFunction(lastReportResultDataView, new TechnoPro.Common.Public.Entities.Reports.ReportFunction
			{
				FunctionCode = eFunctionType.Import_User_Data,
				Title = "ImportUserData Ad Hoc"
			}, parameters);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000502C File Offset: 0x0000322C
		public DataTable ImportStudentCourses(DataTable lastReportResultDataView, string parameters)
		{
			return this.ExecuteLegacyFunction(lastReportResultDataView, new TechnoPro.Common.Public.Entities.Reports.ReportFunction
			{
				FunctionCode = eFunctionType.Data_Sync_Courses_2,
				Title = "DataSyncCourses2 Ad Hoc"
			}, parameters);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00005064 File Offset: 0x00003264
		public int CreateClientReportGroup(ReportGroup Group)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@groupid", DbType.Int32, 0),
				databaseLayer.GetParameter("@grouptitle", DbType.String, Group.Title ?? ""),
				databaseLayer.GetParameter("@groupdescription", DbType.String, Group.Description ?? ""),
				databaseLayer.GetParameter("@iconindex", DbType.Int32, 0),
				databaseLayer.GetParameter("@ordernum", DbType.Int32, 0),
				databaseLayer.GetParameter("@parentsearchgroupinfoid", DbType.Int32, Group.ParentGroupId)
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO searchgroupinfo\r\n    (grouptitle,groupdescription,iconindex,ordernum,parentsearchgroupinfoid)\r\nVALUES\r\n    (@grouptitle,@groupdescription,@iconindex,@ordernum,@parentsearchgroupinfoid);\r\nSET @groupid=(SELECT CAST(SCOPE_IDENTITY() AS int))", array);
			Group.GroupId = ((array[0].Value == null) ? 0 : ((int)array[0].Value));
			return Group.GroupId;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00005160 File Offset: 0x00003360
		public void MarkReportChange(TechnoPro.Common.Public.Entities.Reports.Report ReportAfterChange, int WhoChangedPersonId)
		{
			string text = new ReportForExport
			{
				Report = ReportAfterChange
			}.ConvertReportToNewXml();
			string value = StringCompressor.CompressString(text);
			DatabaseLayer clockWorkArchive = DatabaseLayerFactory.ClockWorkArchive;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWorkArchive.GetParameter("@rid", DbType.Int32, ReportAfterChange.ReportId),
				clockWorkArchive.GetParameter("@who", DbType.Int32, WhoChangedPersonId),
				clockWorkArchive.GetParameter("@xmlafterchange", DbType.String, value),
				clockWorkArchive.GetParameter("@iscompressed", DbType.Boolean, true)
			};
			try
			{
				clockWorkArchive.ExecuteNonQuery("INSERT INTO SearchInfoArchive (reportid,whomodified,xmlafterchange,iscompressed) VALUES (@rid,@who,@xmlafterchange,@iscompressed)", parameters);
			}
			catch (Exception arg)
			{
				CWLogger.Logger.Warn("ReportDAO.MarkReportChange:FailedToMarkReportChange:rid={0}:err={1}", ReportAfterChange.ReportId, arg);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00005230 File Offset: 0x00003430
		public int CreateClientReportFunction(int reportId, bool functionParametersAreEncrypted, TechnoPro.Common.Public.Entities.Reports.ReportFunction function, DbTransaction transaction = null)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IList<ReportParameter> functionParameters = function.FunctionParameters;
			ReportParameter reportParameter;
			if (functionParameters == null)
			{
				reportParameter = null;
			}
			else
			{
				reportParameter = functionParameters.FirstOrDefault((ReportParameter f) => f.Name.Equals("default"));
			}
			ReportParameter reportParameter2 = reportParameter;
			string text = ((reportParameter2 != null) ? reportParameter2.Value.ToString() : null) ?? "";
			if (functionParametersAreEncrypted)
			{
				byte[] array = databaseLayer.Encryption.Encrypt(text);
				text = ((array == null) ? "" : Convert.ToBase64String(array));
			}
			DbParameter[] array2 = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@reportfunctionid", DbType.Int32, 0),
				databaseLayer.GetParameter("@whoami", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@searchinfoid", DbType.Int32, reportId),
				databaseLayer.GetParameter("@functioncode", DbType.Int32, (int)function.FunctionCode),
				databaseLayer.GetParameter("@functionparameters", DbType.String, text),
				databaseLayer.GetParameter("@ordernum", DbType.Int32, function.OrderNum),
				databaseLayer.GetParameter("@custom", DbType.String, function.Description ?? ""),
				databaseLayer.GetParameter("@customsqlinjection", DbType.String, ""),
				databaseLayer.GetParameter("@customsqlinjectionoperator", DbType.String, ""),
				databaseLayer.GetParameter("@RunOnClient", DbType.Boolean, function.ExecuteThisFunctionOnClientIfPossible)
			};
			bool flag = transaction != null;
			if (flag)
			{
				databaseLayer.ExecuteNonQueryTransaction("INSERT INTO searchfunctions \r\n    (searchinfoid,functioncode,functionparameters,ordernum,custom,customsqlinjection,customsqlinjectionoperator,RunOnClient) \r\nVALUES \r\n    (@searchinfoid,@functioncode,@functionparameters,@ordernum,@custom,@customsqlinjection,@customsqlinjectionoperator,@RunOnClient);\r\nSET @reportfunctionid=(SELECT CAST(SCOPE_IDENTITY() AS int))", transaction, array2);
			}
			else
			{
				databaseLayer.ExecuteNonQuery("INSERT INTO searchfunctions \r\n    (searchinfoid,functioncode,functionparameters,ordernum,custom,customsqlinjection,customsqlinjectionoperator,RunOnClient) \r\nVALUES \r\n    (@searchinfoid,@functioncode,@functionparameters,@ordernum,@custom,@customsqlinjection,@customsqlinjectionoperator,@RunOnClient);\r\nSET @reportfunctionid=(SELECT CAST(SCOPE_IDENTITY() AS int))", array2);
			}
			function.ReportFunctionId = ((int?)array2[0].Value).GetValueOrDefault();
			bool flag2 = function.ReportFunctionId < 1;
			if (flag2)
			{
				throw new DatabaseInsertFailedException("CreateReportFunction");
			}
			return function.ReportFunctionId;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00005434 File Offset: 0x00003634
		public DataTable DecodeDynamicData(DataTable t, TechnoPro.Common.Public.Entities.Reports.ReportFunction function, string parameters)
		{
			string[] uniqueColNames = (parameters ?? "").Split(new char[]
			{
				','
			});
			DataView dataView = ReportFunctionsLegacy.DecodeDynamicData(t.DefaultView, this.OpContext, uniqueColNames);
			return (dataView != null) ? dataView.Table : null;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00005480 File Offset: 0x00003680
		public DataTable ExecuteLegacyFunction(DataTable currentTable, TechnoPro.Common.Public.Entities.Reports.ReportFunction function, string functionParameters)
		{
			ArrayList customVariables = new ArrayList();
			DataSet dataSet = new DataSet();
			DataTable dataTable = new DataTable
			{
				TableName = "staffnames"
			};
			DataSet lookupTablesForControls = new DataSet();
			ArrayList variables = new ArrayList();
			ArrayList arrayList = new ArrayList();
			DataTable sessions = new DataTable("t");
			DataTable dynamicScreenNonDataControlsTable = new DataTable
			{
				TableName = "dynamicScreenNonDataControlsTable"
			};
			DataTable searchCustomTable = new DataTable
			{
				TableName = "searchCustomTable"
			};
			bool flag = currentTable == null;
			if (flag)
			{
				currentTable = new DataTable("t");
			}
			TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report report = new TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity.Report(currentTable.DefaultView);
			int functionCode = (int)function.FunctionCode;
			ReportStep reportStep = new ReportStep((eFunctionType)functionCode, functionParameters);
			ReportFunctionsLegacy.RunFunction("", reportStep, ref report, customVariables, ref dataSet, ref dataTable, lookupTablesForControls, variables, sessions, null, dynamicScreenNonDataControlsTable, searchCustomTable, this.OpContext.WhoAmI, -1, ref arrayList, false, true, this.OpContext.AppContext.ExecutingPath, this.OpContext);
			DataView currentDataView = report.GetCurrentDataView();
			return (currentDataView != null) ? currentDataView.Table : null;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000558C File Offset: 0x0000378C
		public ReportCollection LoadClientReports(ReportContext reportContext)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[4];
			int num2 = 0;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@rids";
			DbType pType = DbType.String;
			object value;
			if (reportContext.ReportIds != null)
			{
				value = string.Join(",", reportContext.ReportIds.ToList<int>().ConvertAll<string>((int num) => num.ToString()).ToArray());
			}
			else
			{
				value = "";
			}
			array[num2] = databaseLayer2.GetParameter(pName, pType, value);
			array[1] = databaseLayer.GetParameter("@loadfunctions", DbType.Boolean, !reportContext.ReturnReportDisplayInformationOnly);
			array[2] = databaseLayer.GetParameter("@loadall", DbType.Boolean, reportContext.ReportIds == null || reportContext.ReportIds.Count < 1);
			array[3] = databaseLayer.GetParameter("@loadformattedreportbytes", DbType.Boolean, !reportContext.ReturnReportDisplayInformationOnly);
			DbParameter[] parameters = array;
			ReportCollection result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    s.searchinfoid,s.title,s.description,s.searchgroupid,s.reportoptions,\r\n            s.datecreated,s.datelastmodified,s.whocreated AS whocreatedpid,\r\n            s.wholastmodified AS wholastmodifiedpid,s.ordernum,s.searchchartinfoid,\r\n            p1.firstname AS whocreatedfirstname,p1.lastname AS whocreatedlastname,p1.student_no AS whocreatedstudent_no,\r\n            p2.firstname AS wholastmodifiedfirstname,p2.lastname AS wholastmodifiedlastname,p2.student_no AS wholastmodifiedstudent_no,\r\n            g.grouptitle,g.groupdescription,\r\n            g2.grouptitle AS parentgrouptitle,g2.groupdescription AS parentgroupdescription,g2.searchgroupinfoid AS parentsearchgroupid,\r\n            sf.searchfunctionid,sf.functioncode,sf.functionparameters,sf.ordernum AS functionordernum,sf.custom,\r\n            sf.customsqlinjection,sf.CustomSQLInjectionOperator,sf.isactive,sf.RunOnClient,s.overrideDynamicControlsScreenNum,\r\n            rim.reportfileid,rf.reportfilename AS formattedreportfilename,rf.title AS formattedreporttitle,rf.description AS formattedreportdescription,CASE WHEN @loadformattedreportbytes=1 THEN rf.reportfile ELSE CAST(NULL AS image) END AS formattedreportbytes,rf.filechecksum,rim.ordernum AS formattedreportordernum,\r\n            le.personid AS wholastexecutedpid,le.firstname AS wholastexecutedfirstname,le.middlename AS wholastexecutedmiddlename,le.lastname AS wholastexecutedlastname,\r\n            le.DateExecuted AS datelastexecuted,CASE WHEN s.BuiltByTpro IS NULL THEN CAST(0 AS bit) ELSE CAST(1 AS bit) END AS IsBuiltByTpro,\r\n            s.reportuniqueid,s.createdbylocation\r\nFROM        searchinfo s LEFT JOIN searchgroupinfo g ON g.searchgroupinfoid=s.searchgroupid\r\n            LEFT JOIN people p1 ON p1.personid=s.whocreated\r\n            LEFT JOIN people p2 ON p2.personid=s.wholastmodified\r\n            LEFT JOIN searchgroupinfo g2 ON g2.searchgroupinfoid=g.parentsearchgroupinfoid\r\n            LEFT JOIN searchfunctions sf ON @loadfunctions=1 AND sf.searchinfoid=s.searchinfoid\r\n            LEFT JOIN ReportFileReportIdMatching rim ON rim.reportid=s.searchinfoid AND rim.isactive=1\r\n            LEFT JOIN reportfiles rf ON rf.reportfileid=rim.reportfileid AND rf.isactive=1\r\n            LEFT JOIN ReportLastExecutions le ON le.ReportId=s.searchinfoid\r\nWHERE       @loadall=1 OR s.searchinfoid IN (SELECT orderid AS searchinfoid FROM splitorderids(@rids,','))\r\nORDER BY    g.ordernum,g.grouptitle,s.ordernum,s.title,s.searchinfoid,sf.ordernum,rim.ordernum,rf.title", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					ReportCollection reportsFromReader = this.GetReportsFromReader(dataReader);
					List<ReportGroup> list = new List<ReportGroup>();
					using (IDataReader dataReader2 = databaseLayer.ExecuteQueryReader("SELECT   g.searchgroupinfoid AS searchgroupid,g.grouptitle,g.groupdescription,g.ordernum,\r\n            g.parentsearchgroupinfoid AS parentsearchgroupid,\r\n            g2.grouptitle AS parentgrouptitle,g2.groupdescription AS parentgroupdescription\r\nFROM    searchgroupinfo g LEFT JOIN searchgroupinfo g2 ON g2.searchgroupinfoid=g.parentsearchgroupinfoid\r\nWHERE   NOT g.searchgroupinfoid IN (SELECT searchgroupid AS searchgroupinfoid FROM searchinfo)\r\nORDER BY g.ordernum,g.grouptitle"))
					{
						bool flag2 = dataReader2 == null;
						if (flag2)
						{
							return reportsFromReader;
						}
						while (dataReader2.Read())
						{
							ReportGroup grp = this.GetGroupFromReader(dataReader2);
							bool flag3 = grp != null && list.FirstOrDefault((ReportGroup g) => g.GroupId == grp.GroupId) == null;
							if (flag3)
							{
								list.Add(grp);
							}
						}
					}
					List<ReportGroup> list2 = reportsFromReader.ReportGroups.ToList<ReportGroup>();
					list2.AddRange(list);
					reportsFromReader.ReportGroups = list2;
					result = reportsFromReader;
				}
			}
			return result;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000578C File Offset: 0x0000398C
		public ReportCollection LoadTproReports(ReportContext reportContext)
		{
			string reports2_ = Resources.Reports2_0;
			ReportCollection reportCollection = this.LoadReportsFromXml(reports2_, reportContext);
			bool flag = reportContext.ReportIds != null && reportContext.ReportIds.Count > 0 && reportCollection != null && reportCollection.Reports != null;
			if (flag)
			{
				reportCollection.Reports = (from g in reportCollection.Reports
				where reportContext.ReportIds.Contains(g.ReportId)
				select g).ToList<TechnoPro.Common.Public.Entities.Reports.Report>();
			}
			return reportCollection;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00005818 File Offset: 0x00003A18
		public ReportCollection LoadReportsInAGroup(params string[] GroupTitles)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[3];
			array[0] = databaseLayer.GetParameter("@grouptitles", DbType.String, string.Join(",", GroupTitles.ToList<string>().ConvertAll<string>((string g) => g.Replace(",", "_")).ToArray()));
			array[1] = databaseLayer.GetParameter("@loadformattedreportbytes", DbType.Boolean, false);
			array[2] = databaseLayer.GetParameter("@loadfunctions", DbType.Boolean, false);
			DbParameter[] parameters = array;
			ReportCollection result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    s.searchinfoid,s.title,s.description,s.searchgroupid,s.reportoptions,\r\n            s.datecreated,s.datelastmodified,s.whocreated AS whocreatedpid,\r\n            s.wholastmodified AS wholastmodifiedpid,s.ordernum,s.searchchartinfoid,\r\n            p1.firstname AS whocreatedfirstname,p1.lastname AS whocreatedlastname,p1.student_no AS whocreatedstudent_no,\r\n            p2.firstname AS wholastmodifiedfirstname,p2.lastname AS wholastmodifiedlastname,p2.student_no AS wholastmodifiedstudent_no,\r\n            g.grouptitle,g.groupdescription,\r\n            g2.grouptitle AS parentgrouptitle,g2.groupdescription AS parentgroupdescription,g2.searchgroupinfoid AS parentsearchgroupid,\r\n            sf.searchfunctionid,sf.functioncode,sf.functionparameters,sf.ordernum AS functionordernum,sf.custom,\r\n            sf.customsqlinjection,sf.CustomSQLInjectionOperator,sf.isactive,sf.RunOnClient,s.overrideDynamicControlsScreenNum,\r\n            rim.reportfileid,rf.reportfilename AS formattedreportfilename,rf.title AS formattedreporttitle,rf.description AS formattedreportdescription,CASE WHEN @loadformattedreportbytes=1 THEN rf.reportfile ELSE CAST(NULL AS image) END AS formattedreportbytes,\r\n            rf.filechecksum,rim.ordernum AS formattedreportordernum,\r\n            le.personid AS wholastexecutedpid,le.firstname AS wholastexecutedfirstname,le.middlename AS wholastexecutedmiddlename,le.lastname AS wholastexecutedlastname,\r\n            le.DateExecuted AS datelastexecuted,CASE WHEN s.BuiltByTpro IS NULL THEN CAST(0 AS bit) ELSE CAST(1 AS bit) END AS IsBuiltByTpro,\r\n            s.reportuniqueid,s.createdbylocation\r\nFROM        searchgroupinfo g LEFT JOIN searchinfo s ON s.searchgroupid=g.searchgroupinfoid\r\n            LEFT JOIN people p1 ON p1.personid=s.whocreated\r\n            LEFT JOIN people p2 ON p2.personid=s.wholastmodified\r\n            LEFT JOIN searchgroupinfo g2 ON g2.searchgroupinfoid=g.parentsearchgroupinfoid\r\n            LEFT JOIN searchfunctions sf ON @loadfunctions=1 AND sf.searchinfoid=s.searchinfoid\r\n            LEFT JOIN ReportFileReportIdMatching rim ON rim.reportid=s.searchinfoid AND rim.isactive=1\r\n            LEFT JOIN reportfiles rf ON rf.reportfileid=rim.reportfileid AND rf.isactive=1\r\n            LEFT JOIN ReportLastExecutions le ON le.ReportId=s.searchinfoid\r\nWHERE       g.grouptitle IN (SELECT orderid AS grouptitle FROM splitstrings2( @grouptitles,','))\r\nORDER BY    g.ordernum,g.grouptitle,s.ordernum,s.title,s.searchinfoid,sf.ordernum,rim.ordernum,rf.title", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					ReportCollection reportsFromReader = this.GetReportsFromReader(dataReader);
					List<ReportGroup> list = new List<ReportGroup>();
					using (IDataReader dataReader2 = databaseLayer.ExecuteQueryReader("SELECT   g.searchgroupinfoid AS searchgroupid,g.grouptitle,g.groupdescription,g.ordernum,\r\n            g.parentsearchgroupinfoid AS parentsearchgroupid,\r\n            g2.grouptitle AS parentgrouptitle,g2.groupdescription AS parentgroupdescription\r\nFROM    searchgroupinfo g LEFT JOIN searchgroupinfo g2 ON g2.searchgroupinfoid=g.parentsearchgroupinfoid\r\nWHERE   NOT g.searchgroupinfoid IN (SELECT searchgroupid AS searchgroupinfoid FROM searchinfo)\r\nORDER BY g.ordernum,g.grouptitle"))
					{
						bool flag2 = dataReader2 == null;
						if (flag2)
						{
							return reportsFromReader;
						}
						while (dataReader2.Read())
						{
							ReportGroup grp = this.GetGroupFromReader(dataReader2);
							bool flag3 = grp != null && list.FirstOrDefault((ReportGroup g) => g.GroupId == grp.GroupId) == null;
							if (flag3)
							{
								list.Add(grp);
							}
						}
					}
					List<ReportGroup> list2 = reportsFromReader.ReportGroups.ToList<ReportGroup>();
					list2.AddRange(list);
					reportsFromReader.ReportGroups = list2;
					result = reportsFromReader;
				}
			}
			return result;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000059C8 File Offset: 0x00003BC8
		public ReportCollection LoadReportsInAGroup(params int[] GroupIds)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[3];
			array[0] = databaseLayer.GetParameter("@groupids", DbType.String, string.Join(",", (from g in GroupIds
			select g.ToString()).ToArray<string>()));
			array[1] = databaseLayer.GetParameter("@loadformattedreportbytes", DbType.Boolean, false);
			array[2] = databaseLayer.GetParameter("@loadfunctions", DbType.Boolean, false);
			DbParameter[] parameters = array;
			ReportCollection result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    s.searchinfoid,s.title,s.description,s.searchgroupid,s.reportoptions,\r\n            s.datecreated,s.datelastmodified,s.whocreated AS whocreatedpid,\r\n            s.wholastmodified AS wholastmodifiedpid,s.ordernum,s.searchchartinfoid,\r\n            p1.firstname AS whocreatedfirstname,p1.lastname AS whocreatedlastname,p1.student_no AS whocreatedstudent_no,\r\n            p2.firstname AS wholastmodifiedfirstname,p2.lastname AS wholastmodifiedlastname,p2.student_no AS wholastmodifiedstudent_no,\r\n            g.grouptitle,g.groupdescription,\r\n            g2.grouptitle AS parentgrouptitle,g2.groupdescription AS parentgroupdescription,g2.searchgroupinfoid AS parentsearchgroupid,\r\n            sf.searchfunctionid,sf.functioncode,sf.functionparameters,sf.ordernum AS functionordernum,sf.custom,\r\n            sf.customsqlinjection,sf.CustomSQLInjectionOperator,sf.isactive,sf.RunOnClient,s.overrideDynamicControlsScreenNum,\r\n            rim.reportfileid,rf.reportfilename AS formattedreportfilename,rf.title AS formattedreporttitle,rf.description AS formattedreportdescription,CASE WHEN @loadformattedreportbytes=1 THEN rf.reportfile ELSE CAST(NULL AS image) END AS formattedreportbytes,\r\n            rf.filechecksum,rim.ordernum AS formattedreportordernum,\r\n            le.personid AS wholastexecutedpid,le.firstname AS wholastexecutedfirstname,le.middlename AS wholastexecutedmiddlename,le.lastname AS wholastexecutedlastname,\r\n            le.DateExecuted AS datelastexecuted,CASE WHEN s.BuiltByTpro IS NULL THEN CAST(0 AS bit) ELSE CAST(1 AS bit) END AS IsBuiltByTpro,\r\n            s.reportuniqueid,s.createdbylocation\r\nFROM        searchgroupinfo g LEFT JOIN searchinfo s ON s.searchgroupid=g.searchgroupinfoid\r\n            LEFT JOIN people p1 ON p1.personid=s.whocreated\r\n            LEFT JOIN people p2 ON p2.personid=s.wholastmodified\r\n            LEFT JOIN searchgroupinfo g2 ON g2.searchgroupinfoid=g.parentsearchgroupinfoid\r\n            LEFT JOIN searchfunctions sf ON @loadfunctions=1 AND sf.searchinfoid=s.searchinfoid\r\n            LEFT JOIN ReportFileReportIdMatching rim ON rim.reportid=s.searchinfoid AND rim.isactive=1\r\n            LEFT JOIN reportfiles rf ON rf.reportfileid=rim.reportfileid AND rf.isactive=1\r\n            LEFT JOIN ReportLastExecutions le ON le.ReportId=s.searchinfoid\r\nWHERE       g.searchgroupinfoid IN (SELECT orderid AS searchgroupinfoid FROM splitstrings2( @groupids,','))\r\nORDER BY    g.ordernum,g.grouptitle,s.ordernum,s.title,s.searchinfoid,sf.ordernum,rim.ordernum,rf.title", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					ReportCollection reportsFromReader = this.GetReportsFromReader(dataReader);
					List<ReportGroup> list = new List<ReportGroup>();
					using (IDataReader dataReader2 = databaseLayer.ExecuteQueryReader("SELECT   g.searchgroupinfoid AS searchgroupid,g.grouptitle,g.groupdescription,g.ordernum,\r\n            g.parentsearchgroupinfoid AS parentsearchgroupid,\r\n            g2.grouptitle AS parentgrouptitle,g2.groupdescription AS parentgroupdescription\r\nFROM    searchgroupinfo g LEFT JOIN searchgroupinfo g2 ON g2.searchgroupinfoid=g.parentsearchgroupinfoid\r\nWHERE   NOT g.searchgroupinfoid IN (SELECT searchgroupid AS searchgroupinfoid FROM searchinfo)\r\nORDER BY g.ordernum,g.grouptitle"))
					{
						bool flag2 = dataReader2 == null;
						if (flag2)
						{
							return reportsFromReader;
						}
						while (dataReader2.Read())
						{
							ReportGroup grp = this.GetGroupFromReader(dataReader2);
							bool flag3 = grp != null && list.FirstOrDefault((ReportGroup g) => g.GroupId == grp.GroupId) == null;
							if (flag3)
							{
								list.Add(grp);
							}
						}
					}
					List<ReportGroup> list2 = reportsFromReader.ReportGroups.ToList<ReportGroup>();
					list2.AddRange(list);
					reportsFromReader.ReportGroups = list2;
					result = reportsFromReader;
				}
			}
			return result;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00005B74 File Offset: 0x00003D74
		public ReportGroup LoadClientReportGroupById(int ReportGroupId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@gid", DbType.Int32, ReportGroupId),
				databaseLayer.GetParameter("@loadformattedreportbytes", DbType.Boolean, true)
			};
			ReportGroup result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    g.searchgroupinfoid AS searchgroupid,g.grouptitle,g.groupdescription,g.ordernum,\r\n            g.parentsearchgroupinfoid AS parentsearchgroupid,\r\n            g2.grouptitle AS parentgrouptitle,g2.groupdescription AS parentgroupdescription\r\nFROM    searchgroupinfo g LEFT JOIN searchgroupinfo g2 ON g2.searchgroupinfoid=g.parentsearchgroupinfoid\r\nWHERE       g.searchgroupinfoid=@gid\r\nORDER BY g.ordernum,g.grouptitle", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetGroupFromReader(dataReader);
				}
			}
			return result;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00005C1C File Offset: 0x00003E1C
		public TechnoPro.Common.Public.Entities.Reports.Report LoadClientReportById(int ReportId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@rid", DbType.Int32, ReportId),
				databaseLayer.GetParameter("@loadformattedreportbytes", DbType.Boolean, true)
			};
			List<ReportGroup> list = new List<ReportGroup>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT   g.searchgroupinfoid AS searchgroupid,g.grouptitle,g.groupdescription,g.ordernum,\r\n            g.parentsearchgroupinfoid AS parentsearchgroupid,\r\n            g2.grouptitle AS parentgrouptitle,g2.groupdescription AS parentgroupdescription\r\nFROM    searchgroupinfo g LEFT JOIN searchgroupinfo g2 ON g2.searchgroupinfoid=g.parentsearchgroupinfoid\r\nWHERE   NOT g.searchgroupinfoid IN (SELECT searchgroupid AS searchgroupinfoid FROM searchinfo)\r\nORDER BY g.ordernum,g.grouptitle"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					list = null;
				}
				else
				{
					while (dataReader.Read())
					{
						ReportGroup grp = this.GetGroupFromReader(dataReader);
						bool flag2 = grp != null && list.FirstOrDefault((ReportGroup g) => g.GroupId == grp.GroupId) == null;
						if (flag2)
						{
							list.Add(grp);
						}
					}
				}
			}
			TechnoPro.Common.Public.Entities.Reports.Report result;
			using (IDataReader dataReader2 = databaseLayer.ExecuteQueryReader("SELECT    s.searchinfoid,s.title,s.description,s.searchgroupid,s.reportoptions,\r\n            s.datecreated,s.datelastmodified,s.whocreated AS whocreatedpid,\r\n            s.wholastmodified AS wholastmodifiedpid,s.ordernum,s.searchchartinfoid,\r\n            p1.firstname AS whocreatedfirstname,p1.lastname AS whocreatedlastname,p1.student_no AS whocreatedstudent_no,\r\n            p2.firstname AS wholastmodifiedfirstname,p2.lastname AS wholastmodifiedlastname,p2.student_no AS wholastmodifiedstudent_no,\r\n            g.grouptitle,g.groupdescription,\r\n            g2.grouptitle AS parentgrouptitle,g2.groupdescription AS parentgroupdescription,g2.searchgroupinfoid AS parentsearchgroupid,\r\n            sf.searchfunctionid,sf.functioncode,sf.functionparameters,sf.ordernum AS functionordernum,sf.custom,\r\n            sf.customsqlinjection,sf.CustomSQLInjectionOperator,sf.isactive,sf.RunOnClient,s.overrideDynamicControlsScreenNum,\r\n            rim.reportfileid,rf.reportfilename AS formattedreportfilename,rf.title AS formattedreporttitle,rf.description AS formattedreportdescription,CASE WHEN @loadformattedreportbytes=1 THEN rf.reportfile ELSE CAST(NULL AS image) END AS formattedreportbytes,rf.filechecksum,rim.ordernum AS formattedreportordernum,\r\n            le.personid AS wholastexecutedpid,le.firstname AS wholastexecutedfirstname,le.middlename AS wholastexecutedmiddlename,le.lastname AS wholastexecutedlastname,\r\n            le.DateExecuted AS datelastexecuted,CASE WHEN s.BuiltByTpro IS NULL THEN CAST(0 AS bit) ELSE CAST(1 AS bit) END AS IsBuiltByTpro,\r\n            s.reportuniqueid,s.createdbylocation\r\nFROM        searchinfo s LEFT JOIN searchgroupinfo g ON g.searchgroupinfoid=s.searchgroupid\r\n            LEFT JOIN people p1 ON p1.personid=s.whocreated\r\n            LEFT JOIN people p2 ON p2.personid=s.wholastmodified\r\n            LEFT JOIN searchgroupinfo g2 ON g2.searchgroupinfoid=g.parentsearchgroupinfoid\r\n            LEFT JOIN searchfunctions sf ON sf.searchinfoid=s.searchinfoid\r\n            LEFT JOIN ReportFileReportIdMatching rim ON rim.reportid=s.searchinfoid AND rim.isactive=1\r\n            LEFT JOIN reportfiles rf ON rf.reportfileid=rim.reportfileid AND rf.isactive=1\r\n            LEFT JOIN ReportLastExecutions le ON le.ReportId=s.searchinfoid\r\nWHERE       s.searchinfoid=@rid\r\nORDER BY    g.ordernum,g.grouptitle,s.ordernum,s.title,s.searchinfoid,sf.ordernum,rim.ordernum,rf.title", parameters))
			{
				bool flag3 = dataReader2 == null;
				if (flag3)
				{
					result = null;
				}
				else
				{
					ReportCollection reportsFromReader = this.GetReportsFromReader(dataReader2);
					bool flag4 = list == null;
					if (flag4)
					{
						result = ((((reportsFromReader != null) ? reportsFromReader.Reports : null) == null || reportsFromReader.Reports.Count < 1) ? null : reportsFromReader.Reports[0]);
					}
					else
					{
						List<ReportGroup> list2 = reportsFromReader.ReportGroups.ToList<ReportGroup>();
						list2.AddRange(list);
						reportsFromReader.ReportGroups = list2;
						result = ((reportsFromReader.Reports == null || reportsFromReader.Reports.Count < 1) ? null : reportsFromReader.Reports[0]);
					}
				}
			}
			return result;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00005DE4 File Offset: 0x00003FE4
		public void UpdateBuiltByTpro(int ReportId, byte[] BuiltByTproSignedAndEncrypted)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@rid", DbType.Int32, ReportId),
				databaseLayer.GetParameter("@builtbytpro", DbType.Binary, BuiltByTproSignedAndEncrypted ?? DBNull.Value)
			};
			databaseLayer.ExecuteNonQuery("UPDATE searchinfo SET BuiltByTpro=@builtbytpro WHERE searchinfoid=@rid", parameters);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00005E50 File Offset: 0x00004050
		public TechnoPro.Common.Public.Entities.Reports.Report LoadReportByUniqueId(string ReportUniqueId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@uniqueid", DbType.Guid, new Guid(ReportUniqueId)),
				databaseLayer.GetParameter("@loadformattedreportbytes", DbType.Boolean, true)
			};
			TechnoPro.Common.Public.Entities.Reports.Report result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    s.searchinfoid,s.title,s.description,s.searchgroupid,s.reportoptions,\r\n            s.datecreated,s.datelastmodified,s.whocreated AS whocreatedpid,\r\n            s.wholastmodified AS wholastmodifiedpid,s.ordernum,s.searchchartinfoid,\r\n            p1.firstname AS whocreatedfirstname,p1.lastname AS whocreatedlastname,p1.student_no AS whocreatedstudent_no,\r\n            p2.firstname AS wholastmodifiedfirstname,p2.lastname AS wholastmodifiedlastname,p2.student_no AS wholastmodifiedstudent_no,\r\n            g.grouptitle,g.groupdescription,\r\n            g2.grouptitle AS parentgrouptitle,g2.groupdescription AS parentgroupdescription,g2.searchgroupinfoid AS parentsearchgroupid,\r\n            sf.searchfunctionid,sf.functioncode,sf.functionparameters,sf.ordernum AS functionordernum,sf.custom,\r\n            sf.customsqlinjection,sf.CustomSQLInjectionOperator,sf.isactive,sf.RunOnClient,s.overrideDynamicControlsScreenNum,\r\n            rim.reportfileid,rf.reportfilename AS formattedreportfilename,rf.title AS formattedreporttitle,rf.description AS formattedreportdescription,CASE WHEN @loadformattedreportbytes=1 THEN rf.reportfile ELSE CAST(NULL AS image) END AS formattedreportbytes,rf.filechecksum,rim.ordernum AS formattedreportordernum,\r\n            le.personid AS wholastexecutedpid,le.firstname AS wholastexecutedfirstname,le.middlename AS wholastexecutedmiddlename,le.lastname AS wholastexecutedlastname,\r\n            le.DateExecuted AS datelastexecuted,CASE WHEN s.BuiltByTpro IS NULL THEN CAST(0 AS bit) ELSE CAST(1 AS bit) END AS IsBuiltByTpro,\r\n            s.reportuniqueid,s.createdbylocation\r\nFROM        searchinfo s LEFT JOIN searchgroupinfo g ON g.searchgroupinfoid=s.searchgroupid\r\n            LEFT JOIN people p1 ON p1.personid=s.whocreated\r\n            LEFT JOIN people p2 ON p2.personid=s.wholastmodified\r\n            LEFT JOIN searchgroupinfo g2 ON g2.searchgroupinfoid=g.parentsearchgroupinfoid\r\n            LEFT JOIN searchfunctions sf ON sf.searchinfoid=s.searchinfoid\r\n            LEFT JOIN ReportFileReportIdMatching rim ON rim.reportid=s.searchinfoid AND rim.isactive=1\r\n            LEFT JOIN reportfiles rf ON rf.reportfileid=rim.reportfileid AND rf.isactive=1\r\n            LEFT JOIN ReportLastExecutions le ON le.ReportId=s.searchinfoid\r\nWHERE       s.reportuniqueid=@uniqueid\r\nORDER BY    g.ordernum,g.grouptitle,s.ordernum,s.title,s.searchinfoid,sf.ordernum,rim.ordernum,rf.title", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					ReportCollection reportsFromReader = this.GetReportsFromReader(dataReader);
					List<ReportGroup> list = new List<ReportGroup>();
					using (IDataReader dataReader2 = databaseLayer.ExecuteQueryReader("SELECT   g.searchgroupinfoid AS searchgroupid,g.grouptitle,g.groupdescription,g.ordernum,\r\n            g.parentsearchgroupinfoid AS parentsearchgroupid,\r\n            g2.grouptitle AS parentgrouptitle,g2.groupdescription AS parentgroupdescription\r\nFROM    searchgroupinfo g LEFT JOIN searchgroupinfo g2 ON g2.searchgroupinfoid=g.parentsearchgroupinfoid\r\nWHERE   NOT g.searchgroupinfoid IN (SELECT searchgroupid AS searchgroupinfoid FROM searchinfo)\r\nORDER BY g.ordernum,g.grouptitle"))
					{
						bool flag2 = dataReader2 == null;
						if (flag2)
						{
							return (((reportsFromReader != null) ? reportsFromReader.Reports : null) == null || reportsFromReader.Reports.Count < 1) ? null : reportsFromReader.Reports[0];
						}
						while (dataReader2.Read())
						{
							ReportGroup grp = this.GetGroupFromReader(dataReader2);
							bool flag3 = grp != null && list.FirstOrDefault((ReportGroup g) => g.GroupId == grp.GroupId) == null;
							if (flag3)
							{
								list.Add(grp);
							}
						}
					}
					List<ReportGroup> list2 = reportsFromReader.ReportGroups.ToList<ReportGroup>();
					list2.AddRange(list);
					reportsFromReader.ReportGroups = list2;
					result = ((reportsFromReader.Reports == null || reportsFromReader.Reports.Count < 1) ? null : reportsFromReader.Reports[0]);
				}
			}
			return result;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00006024 File Offset: 0x00004224
		public int CreateClientReport(TechnoPro.Common.Public.Entities.Reports.Report Report)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@reportid", DbType.Int32, 0),
				databaseLayer.GetOutputParameter("@reportuniqueid", DbType.Guid, 0),
				databaseLayer.GetParameter("@whoami", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@title", DbType.String, Report.Title ?? ""),
				databaseLayer.GetParameter("@description", DbType.String, Report.Description ?? ""),
				databaseLayer.GetParameter("@searchgroupid", DbType.Int32, (Report.GroupId > 0) ? Report.GroupId : -1),
				databaseLayer.GetParameter("@datecreated", DbType.DateTime, DateTime.Now),
				databaseLayer.GetParameter("@datelastmodified", DbType.DateTime, DateTime.Now),
				databaseLayer.GetParameter("@ordernum", DbType.Int32, Report.OrderNum),
				databaseLayer.GetParameter("@searchchartinfoid", DbType.Int32, Report.FunctionParametersAreEncrypted ? 999 : 0),
				databaseLayer.GetParameter("@overridedynamiccontrolsscreennum", DbType.Int32, (int)((Report.LegacyParameters == null) ? eReportBuiltInDynamicForm.None : Report.LegacyParameters.BuiltInDynamicForm)),
				databaseLayer.GetParameter("@reportoptions", DbType.String, (Report.ReportOptions == null) ? "" : Report.ReportOptions.ConvertToXml()),
				databaseLayer.GetParameter("@uniqueid", DbType.String, (Report.ReportUniqueId == Guid.Empty) ? DBNull.Value : Report.ReportUniqueId.ToString()),
				databaseLayer.GetParameter("@createdbylocation", DbType.String, string.IsNullOrEmpty(Report.CreatedByLocation) ? DBNull.Value : Report.CreatedByLocation)
			};
			DbTransaction dbTransaction = databaseLayer.BeginDbTransaction();
			try
			{
				databaseLayer.ExecuteNonQueryTransaction("IF @createdbylocation IS NULL \r\n    SET @createdbylocation=(SELECT TOP 1 UniqueName FROM UniqueDatabaseName2())\r\n\r\nIF @uniqueid IS NULL OR EXISTS(SELECT searchinfoid FROM searchinfo WHERE reportuniqueid=@uniqueid)\r\n    INSERT INTO searchinfo \r\n        (title,description,searchgroupid,datecreated,datelastmodified,whocreated,wholastmodified,ordernum,searchchartinfoid,overridedynamiccontrolsscreennum,reportoptions,createdbylocation) \r\n    VALUES\r\n        (@title,@description,@searchgroupid,@datecreated,@datelastmodified,@whoami,@whoami,@ordernum,@searchchartinfoid,@overridedynamiccontrolsscreennum,@reportoptions,@createdbylocation) \r\nELSE\r\n    INSERT INTO searchinfo \r\n        (title,description,searchgroupid,datecreated,datelastmodified,whocreated,wholastmodified,ordernum,searchchartinfoid,overridedynamiccontrolsscreennum,reportoptions,reportuniqueid,createdbylocation) \r\n    VALUES\r\n        (@title,@description,@searchgroupid,@datecreated,@datelastmodified,@whoami,@whoami,@ordernum,@searchchartinfoid,@overridedynamiccontrolsscreennum,@reportoptions,@uniqueid,@createdbylocation) \r\n\r\nSET @reportid=(SELECT CAST(SCOPE_IDENTITY() AS int))\r\nSET @reportuniqueid=(SELECT TOP 1 reportuniqueid FROM searchinfo WHERE searchinfoid=@reportid)", dbTransaction, array);
				Report.ReportId = ((int?)array[0].Value).GetValueOrDefault();
				try
				{
					object value = array[1].Value;
					Report.ReportUniqueId = new Guid(((value != null) ? value.ToString() : null) ?? "");
				}
				catch
				{
				}
				bool flag = Report.ReportId < 1;
				if (flag)
				{
					dbTransaction.Rollback();
					throw new DatabaseInsertFailedException("Failed to create report");
				}
				foreach (TechnoPro.Common.Public.Entities.Reports.ReportFunction function in Report.Functions)
				{
					this.CreateClientReportFunction(Report.ReportId, Report.FunctionParametersAreEncrypted, function, null);
				}
				dbTransaction.Commit();
			}
			catch (DbException innerEx)
			{
				throw new DatabaseInsertFailedException("CreateReport: Failed to finish creating report", innerEx);
			}
			catch (Exception innerEx2)
			{
				dbTransaction.Rollback();
				throw new DatabaseInsertFailedException("CreateReport: Failed to finish creating report", innerEx2);
			}
			return Report.ReportId;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00006370 File Offset: 0x00004570
		public void DeleteClientReportFunction(int ReportFunctionId, DbTransaction transaction = null)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("reportfunctionid", DbType.Int32, ReportFunctionId)
			};
			bool flag = transaction != null;
			if (flag)
			{
				databaseLayer.ExecuteNonQueryTransaction("DELETE FROM searchfunctions WHERE searchfunctionid=@reportfunctionid", transaction, parameters);
			}
			else
			{
				databaseLayer.ExecuteNonQuery("DELETE FROM searchfunctions WHERE searchfunctionid=@reportfunctionid", parameters);
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000063DC File Offset: 0x000045DC
		public void UpdateClientReportFunction(TechnoPro.Common.Public.Entities.Reports.ReportFunction ReportFunction, bool FunctionParametersAreEncrypted, DbTransaction transaction = null)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			bool flag = ReportFunction == null || ReportFunction.ReportFunctionId < 1;
			if (flag)
			{
				throw new NullOrInvalidIdParameterException("UpdateReportFunction");
			}
			IList<ReportParameter> functionParameters = ReportFunction.FunctionParameters;
			ReportParameter reportParameter;
			if (functionParameters == null)
			{
				reportParameter = null;
			}
			else
			{
				reportParameter = functionParameters.FirstOrDefault((ReportParameter f) => f.Name.Equals("default"));
			}
			ReportParameter reportParameter2 = reportParameter;
			string text = ((reportParameter2 != null) ? reportParameter2.Value.ToString() : null) ?? "";
			if (FunctionParametersAreEncrypted)
			{
				byte[] array = databaseLayer.Encryption.Encrypt(text);
				text = ((array == null) ? "" : Convert.ToBase64String(array));
			}
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@reportfunctionid", DbType.Int32, ReportFunction.Id),
				databaseLayer.GetParameter("@functioncode", DbType.Int32, (int)ReportFunction.FunctionCode),
				databaseLayer.GetParameter("@functionparameters", DbType.String, text),
				databaseLayer.GetParameter("@ordernum", DbType.Int32, ReportFunction.OrderNum),
				databaseLayer.GetParameter("@custom", DbType.String, ReportFunction.Description ?? ""),
				databaseLayer.GetParameter("@customsqlinjection", DbType.String, ""),
				databaseLayer.GetParameter("@customsqlinjectionoperator", DbType.String, ""),
				databaseLayer.GetParameter("@RunOnClient", DbType.Boolean, ReportFunction.ExecuteThisFunctionOnClientIfPossible)
			};
			bool flag2 = transaction != null;
			if (flag2)
			{
				databaseLayer.ExecuteNonQueryTransaction("UPDATE searchfunctions SET functioncode=@functioncode,functionparameters=@functionparameters,ordernum=@ordernum,custom=@custom,\r\n        customsqlinjection=@customsqlinjection,customsqlinjectionoperator=@customsqlinjectionoperator,RunOnClient=@RunOnClient\r\nWHERE   searchfunctionid=@reportfunctionid", transaction, parameters);
			}
			else
			{
				databaseLayer.ExecuteNonQuery("UPDATE searchfunctions SET functioncode=@functioncode,functionparameters=@functionparameters,ordernum=@ordernum,custom=@custom,\r\n        customsqlinjection=@customsqlinjection,customsqlinjectionoperator=@customsqlinjectionoperator,RunOnClient=@RunOnClient\r\nWHERE   searchfunctionid=@reportfunctionid", parameters);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00006590 File Offset: 0x00004790
		public IList<FormattedReport> LoadClientFormattedReportsByReportId(int ReportId, bool LoadFileBytes)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@reportid", DbType.Int32, ReportId),
				databaseLayer.GetParameter("@loadformattedreportbytes", DbType.Boolean, LoadFileBytes)
			};
			List<FormattedReport> list = new List<FormattedReport>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    rim.reportfileid,rf.reportfilename AS formattedreportfilename,rf.title AS formattedreporttitle,rf.description AS formattedreportdescription,CASE WHEN @loadformattedreportbytes=1 THEN rf.reportfile ELSE CAST(NULL AS image) END AS formattedreportbytes,rf.filechecksum,rim.ordernum AS formattedreportordernum\r\nFROM        ReportFileReportIdMatching rim LEFT JOIN reportfiles rf ON rf.reportfileid=rim.reportfileid AND rf.isactive=1\r\nWHERE       rim.reportid=@reportid\r\nORDER BY    rim.ordernum,rf.title", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return list;
				}
				while (dataReader.Read())
				{
					FormattedReport item = ReportDAO.GetFormattedReportFromReader(dataReader);
					bool flag2 = item != null && (item.ReportFileId < 1 || list.FirstOrDefault((FormattedReport g) => g.ReportFileId == item.ReportFileId) == null);
					if (flag2)
					{
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000066A0 File Offset: 0x000048A0
		public IList<ReportCompileLineWarningOrError> TryToCompileCSharp(string Code, IList<string> Imports, out bool Successful)
		{
			CustomCSharpCode code = new CustomCSharpCode
			{
				Code = Code,
				BinPath = this.OpContext.AppContext.ExecutingPath,
				Imports = Imports
			};
			CustomCompiler<ReportParameters, ReportReturnValue> customCompiler = new CustomCompiler<ReportParameters, ReportReturnValue>(code, eCustomCompilerType.Reports, "");
			CustomCompileResult customCompileResult = customCompiler.CompileCode(this.OpContext.AppContext.ExecutingPath);
			bool flag = customCompileResult == null;
			IList<ReportCompileLineWarningOrError> result;
			if (flag)
			{
				Successful = false;
				result = null;
			}
			else
			{
				Successful = customCompileResult.Success;
				List<ReportCompileLineWarningOrError> list = new List<ReportCompileLineWarningOrError>();
				bool flag2 = customCompileResult.Errors != null;
				if (flag2)
				{
					list.AddRange(from error in customCompileResult.Errors
					select new ReportCompileLineWarningOrError
					{
						LineType = eReportCompileLineWarningOrErrorType.Error,
						LineNumber = error.LineNumber,
						ColumnNumber = error.ColumnNumber,
						Filename = error.Filename,
						Message = (error.Title ?? "")
					});
				}
				bool flag3 = customCompileResult.Warnings != null;
				if (flag3)
				{
					list.AddRange(from warning in customCompileResult.Warnings
					select new ReportCompileLineWarningOrError
					{
						LineType = eReportCompileLineWarningOrErrorType.Warning,
						LineNumber = warning.LineNumber,
						ColumnNumber = warning.ColumnNumber,
						Filename = warning.Filename,
						Message = (warning.Title ?? "")
					});
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000067B0 File Offset: 0x000049B0
		public void UpdateClientReport(TechnoPro.Common.Public.Entities.Reports.Report Report)
		{
			bool flag = Report == null || Report.ReportId < 1;
			if (flag)
			{
				throw new NullOrInvalidIdParameterException("Report={0}" + ((Report == null) ? "NULL" : Report.Id.ToString()));
			}
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@reportid", DbType.Int32, Report.ReportId)
			};
			List<int> existingReportFunctionIds = new List<int>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT searchfunctionid FROM searchfunctions WHERE searchinfoid=@reportid", parameters))
			{
				bool flag2 = dataReader != null;
				if (flag2)
				{
					while (dataReader.Read())
					{
						int num = (dataReader["searchfunctionid"] is DBNull) ? 0 : ((int)dataReader["searchfunctionid"]);
						bool flag3 = num > 0;
						if (flag3)
						{
							existingReportFunctionIds.Add(num);
						}
					}
				}
			}
			IEnumerable<int> enumerable = from g in existingReportFunctionIds
			where Report.Functions.FirstOrDefault((TechnoPro.Common.Public.Entities.Reports.ReportFunction h) => h.ReportFunctionId == g) == null
			select g;
			IEnumerable<TechnoPro.Common.Public.Entities.Reports.ReportFunction> enumerable2 = from g in Report.Functions
			where !existingReportFunctionIds.Contains(g.ReportFunctionId)
			select g;
			IEnumerable<TechnoPro.Common.Public.Entities.Reports.ReportFunction> enumerable3 = from g in Report.Functions
			where existingReportFunctionIds.Contains(g.ReportFunctionId)
			select g;
			bool flag4 = Report.FormattedReports == null;
			if (flag4)
			{
				Report.FormattedReports = new List<FormattedReport>();
			}
			IList<FormattedReport> existingFormattedReports = this.LoadClientFormattedReportsByReportId(Report.ReportId, false);
			IEnumerable<FormattedReport> enumerable4 = from g in existingFormattedReports
			where Report.FormattedReports.FirstOrDefault((FormattedReport h) => h.ReportFileId == g.ReportFileId) == null
			select g;
			IEnumerable<FormattedReport> enumerable5 = from g in Report.FormattedReports
			where existingFormattedReports.FirstOrDefault((FormattedReport h) => h.ReportFileId == g.ReportFileId) == null
			select g;
			List<FormattedReport> list = new List<FormattedReport>();
			using (IEnumerator<FormattedReport> enumerator = Report.FormattedReports.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					FormattedReport fr = enumerator.Current;
					FormattedReport formattedReport = existingFormattedReports.FirstOrDefault((FormattedReport h) => h.ReportFileId == fr.ReportFileId);
					bool flag5 = formattedReport != null;
					if (flag5)
					{
						list.Add(fr);
					}
				}
			}
			parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@reportid", DbType.Int32, Report.ReportId),
				databaseLayer.GetParameter("@whoami", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@title", DbType.String, Report.Title ?? ""),
				databaseLayer.GetParameter("@description", DbType.String, Report.Description ?? ""),
				databaseLayer.GetParameter("@searchgroupid", DbType.Int32, (Report.GroupId > 0) ? Report.GroupId : -1),
				databaseLayer.GetParameter("@ordernum", DbType.Int32, Report.OrderNum),
				databaseLayer.GetParameter("@searchchartinfoid", DbType.Int32, Report.FunctionParametersAreEncrypted ? 999 : 0),
				databaseLayer.GetParameter("@overridedynamiccontrolsscreennum", DbType.Int32, (int)((Report.LegacyParameters == null) ? eReportBuiltInDynamicForm.None : Report.LegacyParameters.BuiltInDynamicForm)),
				databaseLayer.GetParameter("@reportoptions", DbType.String, (Report.ReportOptions == null) ? "" : Report.ReportOptions.ConvertToXml())
			};
			DbTransaction dbTransaction = databaseLayer.BeginDbTransaction();
			try
			{
				databaseLayer.ExecuteNonQueryTransaction("UPDATE    searchinfo SET title=@title,description=@description,searchgroupid=@searchgroupid,datelastmodified=getdate(),\r\n            wholastmodified=@whoami,ordernum=@ordernum,searchchartinfoid=@searchchartinfoid,\r\n            overridedynamiccontrolsscreennum=@overridedynamiccontrolsscreennum,\r\n            reportoptions=@reportoptions\r\nWHERE       searchinfoid=@reportid", dbTransaction, parameters);
				foreach (int reportFunctionId in enumerable)
				{
					this.DeleteClientReportFunction(reportFunctionId, dbTransaction);
				}
				foreach (TechnoPro.Common.Public.Entities.Reports.ReportFunction function in enumerable2)
				{
					this.CreateClientReportFunction(Report.ReportId, Report.FunctionParametersAreEncrypted, function, dbTransaction);
				}
				foreach (TechnoPro.Common.Public.Entities.Reports.ReportFunction reportFunction in enumerable3)
				{
					this.UpdateClientReportFunction(reportFunction, Report.FunctionParametersAreEncrypted, dbTransaction);
				}
				foreach (FormattedReport formattedReport2 in enumerable4)
				{
					this.DeleteClientFormattedReport(formattedReport2.ReportFileId, dbTransaction);
				}
				foreach (FormattedReport formattedReport3 in enumerable5)
				{
					this.CreateClientFormattedReport(Report.ReportId, formattedReport3, dbTransaction);
				}
				foreach (FormattedReport formattedReport4 in list)
				{
					this.UpdateClientFormattedReport(formattedReport4, dbTransaction);
				}
				dbTransaction.Commit();
			}
			catch (DbException ex)
			{
				throw new DatabaseUpdateFailedException("Failed to update report: " + ex.ToString());
			}
			catch (Exception ex2)
			{
				dbTransaction.Rollback();
				throw new DatabaseUpdateFailedException("Failed to update report: " + ex2.ToString());
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00006E7C File Offset: 0x0000507C
		public void DeleteClientFormattedReport(int FileId, DbTransaction transaction = null)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@fileid", DbType.Int32, FileId)
			};
			bool flag = transaction != null;
			if (flag)
			{
				databaseLayer.ExecuteNonQueryTransaction("DELETE FROM reportfiles WHERE reportfileid=@fileid;\r\nDELETE FROM ReportFileReportIdMatching WHERE reportfileid=@fileid", transaction, parameters);
			}
			else
			{
				databaseLayer.ExecuteNonQuery("DELETE FROM reportfiles WHERE reportfileid=@fileid;\r\nDELETE FROM ReportFileReportIdMatching WHERE reportfileid=@fileid", parameters);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00006EE8 File Offset: 0x000050E8
		public void DeleteClientReport(int ReportId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@reportid", DbType.Int32, ReportId)
			};
			DbTransaction dbTransaction = databaseLayer.BeginDbTransaction();
			try
			{
				databaseLayer.ExecuteNonQueryTransaction("DELETE FROM searchfunctions WHERE searchinfoid=@reportid", dbTransaction, parameters);
				parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@reportid", DbType.Int32, ReportId)
				};
				databaseLayer.ExecuteNonQueryTransaction("DELETE FROM reportfiles WHERE reportfileid IN (SELECT reportfileid FROM reportfilereportidmatching WHERE reportid=@reportid);\r\nDELETE FROM reportfilereportidmatching WHERE reportid=@reportid", dbTransaction, parameters);
				parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@reportid", DbType.Int32, ReportId)
				};
				databaseLayer.ExecuteNonQueryTransaction("DELETE FROM searchinfo WHERE searchinfoid=@reportid", dbTransaction, parameters);
				dbTransaction.Commit();
			}
			catch (Exception ex)
			{
				dbTransaction.Rollback();
				throw new DatabaseDeleteFailedException("DeleteReport:err=" + ex.ToString());
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00006FD4 File Offset: 0x000051D4
		public void UpdateClientFormattedReport(FormattedReport FormattedReport, DbTransaction transaction = null)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			byte[] formattedReportTemplate = FormattedReport.FormattedReportTemplate;
			CompressionBinaryFile compressionBinaryFile = CompressDataAdapter.CompressFile(new CompressionBinaryFile
			{
				FileBytes = formattedReportTemplate,
				FileName = "FormattedReport.mrt"
			});
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@fileid", DbType.Int32, FormattedReport.ReportFileId),
				databaseLayer.GetParameter("@title", DbType.String, FormattedReport.Title ?? ""),
				databaseLayer.GetParameter("@description", DbType.String, FormattedReport.Description ?? ""),
				databaseLayer.GetParameter("@bytes", DbType.Binary, (((compressionBinaryFile != null) ? compressionBinaryFile.FileBytes : null) == null) ? DBNull.Value : compressionBinaryFile.FileBytes),
				databaseLayer.GetParameter("@ordernum", DbType.Int32, FormattedReport.OrderNum),
				databaseLayer.GetParameter("@checksum", DbType.String, (FormattedReport.FormattedReportTemplate == null) ? "" : FormattedReport.FormattedReportTemplate.ComputeFileHash())
			};
			bool flag = transaction != null;
			if (flag)
			{
				databaseLayer.ExecuteNonQueryTransaction("UPDATE reportfiles SET title=@title,description=@description,reportfile=COALESCE(@bytes,reportfile),ordernum=@ordernum,filechecksum=@checksum WHERE reportfileid=@fileid", transaction, parameters);
			}
			else
			{
				databaseLayer.ExecuteNonQuery("UPDATE reportfiles SET title=@title,description=@description,reportfile=COALESCE(@bytes,reportfile),ordernum=@ordernum,filechecksum=@checksum WHERE reportfileid=@fileid", parameters);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00007118 File Offset: 0x00005318
		public int CreateClientFormattedReport(int ReportId, FormattedReport FormattedReport, DbTransaction transaction = null)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			byte[] formattedReportTemplate = FormattedReport.FormattedReportTemplate;
			CompressionBinaryFile compressionBinaryFile = CompressDataAdapter.CompressFile(new CompressionBinaryFile
			{
				FileBytes = formattedReportTemplate,
				FileName = "FormattedReport.mrt"
			});
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@fileid", DbType.Int32, 0),
				databaseLayer.GetParameter("@title", DbType.String, FormattedReport.Title ?? ""),
				databaseLayer.GetParameter("@description", DbType.String, FormattedReport.Description ?? ""),
				databaseLayer.GetParameter("@bytes", DbType.Binary, (compressionBinaryFile == null || compressionBinaryFile.FileBytes == null) ? DBNull.Value : compressionBinaryFile.FileBytes),
				databaseLayer.GetParameter("@filename", DbType.String, "FormattedReport.zip"),
				databaseLayer.GetParameter("@ordernum", DbType.Int32, FormattedReport.OrderNum),
				databaseLayer.GetParameter("@checksum", DbType.String, (FormattedReport.FormattedReportTemplate == null) ? "" : FormattedReport.FormattedReportTemplate.ComputeFileHash())
			};
			bool flag = transaction != null;
			if (flag)
			{
				databaseLayer.ExecuteNonQueryTransaction("INSERT INTO reportfiles\r\n    (reportfile,reportfilename,title,description,isactive,filechecksum,ordernum)\r\nVALUES\r\n    (@bytes,@filename,@title,@description,1,@checksum,@ordernum);\r\nSET @fileid=(SELECT CAST(SCOPE_IDENTITY() AS int))", transaction, array);
			}
			else
			{
				databaseLayer.ExecuteNonQuery("INSERT INTO reportfiles\r\n    (reportfile,reportfilename,title,description,isactive,filechecksum,ordernum)\r\nVALUES\r\n    (@bytes,@filename,@title,@description,1,@checksum,@ordernum);\r\nSET @fileid=(SELECT CAST(SCOPE_IDENTITY() AS int))", array);
			}
			FormattedReport.ReportFileId = ((array[0].Value == null) ? 0 : ((int)array[0].Value));
			bool flag2 = FormattedReport.ReportFileId < 1;
			if (flag2)
			{
				throw new DatabaseInsertFailedException("CreateClientFormattedReport:Fileid=" + FormattedReport.ReportFileId.ToString());
			}
			array = new DbParameter[]
			{
				databaseLayer.GetParameter("@reportid", DbType.Int32, ReportId),
				databaseLayer.GetParameter("@reportfileid", DbType.Int32, FormattedReport.ReportFileId),
				databaseLayer.GetParameter("@ordernum", DbType.Int32, 0)
			};
			bool flag3 = transaction != null;
			if (flag3)
			{
				databaseLayer.ExecuteNonQueryTransaction("INSERT INTO ReportFileReportIdMatching(reportfileid,reportid,ordernum,isactive) VALUES (@reportfileid,@reportid,@ordernum,1)", transaction, array);
			}
			else
			{
				databaseLayer.ExecuteNonQuery("INSERT INTO ReportFileReportIdMatching(reportfileid,reportid,ordernum,isactive) VALUES (@reportfileid,@reportid,@ordernum,1)", array);
			}
			return FormattedReport.ReportFileId;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00007334 File Offset: 0x00005534
		public void RecordReportExecution(ReportExecutionContext Context)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@rid", DbType.Int32, Context.ReportId),
				databaseLayer.GetParameter("@pid", DbType.Int32, (Context.WhoExecutedPersonId > 0) ? Context.WhoExecutedPersonId : DBNull.Value),
				databaseLayer.GetParameter("@location", DbType.Int32, (int)Context.ExecutionLocation)
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO SearchInfoExecuteLog (ReportId,WhoExecutedPersonId,ExecutedFromContext) VALUES (@rid,@pid,@location)", parameters);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000073D0 File Offset: 0x000055D0
		public void DeleteClientReportGroup(int ReportGroupId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@reportgroupid", DbType.Int32, ReportGroupId)
			};
			databaseLayer.ExecuteNonQuery("DELETE FROM searchgroupinfo \r\nWHERE   searchgroupinfoid=@reportgroupid \r\n        AND NOT searchgroupinfoid IN (SELECT parentsearchgroupinfoid AS searchgroupinfoid FROM searchgroupinfo WHERE NOT parentsearchgroupinfoid IS NULL) \r\n        AND NOT searchgroupinfoid IN (SELECT searchgroupid AS searchgroupinfoid FROM searchinfo WHERE NOT searchgroupid IS NULL)", parameters);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00007424 File Offset: 0x00005624
		public string LoadReportTechnoProNote(int ReportId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@rid", DbType.Int32, ReportId)
			};
			string result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT tpronote FROM searchinfo WHERE searchinfoid=@rid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					byte[] array = (dataReader["tpronote"] is DBNull) ? null : ((byte[])dataReader["tpronote"]);
					bool flag2 = array == null || array.Count<byte>() < 1;
					if (flag2)
					{
						result = null;
					}
					else
					{
						result = databaseLayer.Encryption.Decrypt(array);
					}
				}
			}
			return result;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00007500 File Offset: 0x00005700
		public void SaveReportTechnoProNote(int ReportId, string Rtf)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@rid", DbType.Int32, ReportId),
				databaseLayer.GetParameter("@rtf", DbType.Binary, databaseLayer.Encryption.Encrypt(Rtf ?? ""))
			};
			databaseLayer.ExecuteNonQuery("UPDATE searchinfo SET tpronote=@rtf WHERE searchinfoid=@rid", parameters);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00007578 File Offset: 0x00005778
		public byte[] LoadBuiltByTpro(int ReportId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@rid", DbType.Int32, ReportId)
			};
			object obj = databaseLayer.ExecuteScalar("SELECT BuiltByTpro FROM searchinfo WHERE searchinfoid=@rid", parameters);
			bool flag = obj != null && obj != DBNull.Value && obj is byte[];
			byte[] result;
			if (flag)
			{
				result = (byte[])obj;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000075F8 File Offset: 0x000057F8
		public void UpdateReportOrderNum(int ReportId, int NewOrderNum)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@rid", DbType.Int32, ReportId),
				databaseLayer.GetParameter("@ordernum", DbType.Int32, NewOrderNum)
			};
			databaseLayer.ExecuteNonQuery("UPDATE searchinfo SET ordernum=@ordernum WHERE searchinfoid=@rid", parameters);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00007660 File Offset: 0x00005860
		public void UpdateGroupOrderNum(int ReportGroupId, int NewOrderNum)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@gid", DbType.Int32, ReportGroupId),
				databaseLayer.GetParameter("@ordernum", DbType.Int32, NewOrderNum)
			};
			databaseLayer.ExecuteNonQuery("UPDATE searchgroupinfo SET ordernum=@ordernum WHERE searchgroupinfoid=@gid", parameters);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000076C8 File Offset: 0x000058C8
		public void UpdateReportGroup(int ReportId, int NewGroupId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@rid", DbType.Int32, ReportId),
				databaseLayer.GetParameter("@gid", DbType.Int32, NewGroupId)
			};
			databaseLayer.ExecuteNonQuery("UPDATE searchinfo SET searchgroupid=@gid WHERE searchinfoid=@rid", parameters);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00007730 File Offset: 0x00005930
		public void UpdateGroupParent(int ReportGroupId, int NewGroupId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@gid", DbType.Int32, ReportGroupId),
				databaseLayer.GetParameter("@parentgid", DbType.Int32, NewGroupId)
			};
			databaseLayer.ExecuteNonQuery("UPDATE searchgroupinfo SET parentsearchgroupinfoid=@parentgid WHERE searchgroupinfoid=@gid", parameters);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00007798 File Offset: 0x00005998
		public IList<ReportGroup> LoadGroupsInAGroup(int ReportGroupId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@gid", DbType.Int32, (ReportGroupId > 0) ? ReportGroupId : DBNull.Value)
			};
			IList<ReportGroup> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    g.searchgroupinfoid AS searchgroupid,g.grouptitle,g.groupdescription,g.ordernum,\r\n            g.parentsearchgroupinfoid AS parentsearchgroupid,\r\n            g2.grouptitle AS parentgrouptitle,g2.groupdescription AS parentgroupdescription\r\nFROM        searchgroupinfo g LEFT JOIN searchgroupinfo g2 ON g2.searchgroupinfoid=g.parentsearchgroupinfoid\r\nWHERE       ((@gid IS NULL OR @gid < 0) AND g.parentsearchgroupinfoid IS NULL) OR ( NOT g.parentsearchgroupinfoid IS NULL AND NOT @gid IS NULL AND g.parentsearchgroupinfoid=@gid)\r\nORDER BY g.ordernum,g.grouptitle", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<ReportGroup> list = new List<ReportGroup>();
					while (dataReader.Read())
					{
						ReportGroup groupFromReader = this.GetGroupFromReader(dataReader);
						bool flag2 = groupFromReader != null;
						if (flag2)
						{
							list.Add(groupFromReader);
						}
					}
					result = list;
				}
			}
			return result;
		}
	}
}
