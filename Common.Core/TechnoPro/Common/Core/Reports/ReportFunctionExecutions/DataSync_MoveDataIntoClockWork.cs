using System;
using System.Data;
using ClockWorkLogger;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.DataSync;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000072 RID: 114
	public class DataSync_MoveDataIntoClockWork : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000482 RID: 1154 RVA: 0x0001A61C File Offset: 0x0001881C
		public DataSync_MoveDataIntoClockWork()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0001A637 File Offset: 0x00018837
		public DataSync_MoveDataIntoClockWork(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0001A655 File Offset: 0x00018855
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x0001A65D File Offset: 0x0001885D
		public OperationContext OpContext { get; set; }

		// Token: 0x06000486 RID: 1158 RVA: 0x0001A668 File Offset: 0x00018868
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			DataTable dataTable = new DataTable("t");
			dataTable.Columns.Add("item");
			dataTable.Columns.Add("success", typeof(bool));
			try
			{
				string defaultFunctionParameter = function.GetDefaultFunctionParameter();
				DataSyncMoveDataIntoClockWorkParameters dataSyncMoveDataIntoClockWorkParameters = defaultFunctionParameter.ConvertXmlToDataSyncMoveDataIntoClockWorkParameters();
				IDataSyncManager dataSyncManager = new DataSyncManager(this.OpContext);
				foreach (DataSyncMoveDataIntoClockWorkItem dataSyncMoveDataIntoClockWorkItem in dataSyncMoveDataIntoClockWorkParameters.Items)
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow["item"] = (dataSyncMoveDataIntoClockWorkItem.FullPathAndFilename ?? "NULL");
					dataTable.Rows.Add(dataRow);
					DataSyncMoveDataIntoClockWorkSourceFileInfo dataSyncMoveDataIntoClockWorkSourceFileInfo = (dataSyncMoveDataIntoClockWorkItem.OverrideSourceFileInfo == null || dataSyncMoveDataIntoClockWorkItem.OverrideSourceFileInfo.SourceFileType == eDataSyncMoveDataIntoClockWorkSourceFileType.Unknown) ? dataSyncMoveDataIntoClockWorkParameters.SourceFileInfo : dataSyncMoveDataIntoClockWorkItem.OverrideSourceFileInfo;
					switch (dataSyncMoveDataIntoClockWorkSourceFileInfo.SourceFileType)
					{
					case eDataSyncMoveDataIntoClockWorkSourceFileType.Csv:
						dataSyncManager.CopyCsvDataToCustomTable(dataSyncMoveDataIntoClockWorkItem.CustomTableNameWithoutCustomPrefix, dataSyncMoveDataIntoClockWorkItem.FullPathAndFilename, dataSyncMoveDataIntoClockWorkItem.StudentNumberExternalColumnName, true, Array.Empty<string>());
						break;
					case eDataSyncMoveDataIntoClockWorkSourceFileType.CharacterDelimited:
					{
						string text = dataSyncMoveDataIntoClockWorkSourceFileInfo.Args[0] ?? "";
						bool flag = text.Length < 1;
						if (flag)
						{
							throw new InvalidParameterException(string.Format("Common.Core.Reports.ReportFunctionExecutions.DataSync_MoveDataIntoClockWork:Invalid char delimited delimiter:info={0}", defaultFunctionParameter ?? "NULL"));
						}
						dataSyncManager.CopyCharacterDelimitedDataToCustomTable(text[0], dataSyncMoveDataIntoClockWorkItem.CustomTableNameWithoutCustomPrefix, dataSyncMoveDataIntoClockWorkItem.FullPathAndFilename, dataSyncMoveDataIntoClockWorkItem.StudentNumberExternalColumnName, true, Array.Empty<string>());
						break;
					}
					case eDataSyncMoveDataIntoClockWorkSourceFileType.TabDelimited:
						dataSyncManager.CopyTabDelimitedDataToCustomTable(dataSyncMoveDataIntoClockWorkItem.CustomTableNameWithoutCustomPrefix, dataSyncMoveDataIntoClockWorkItem.FullPathAndFilename, dataSyncMoveDataIntoClockWorkItem.StudentNumberExternalColumnName, true, Array.Empty<string>());
						break;
					default:
						throw new InvalidParameterException(string.Format("Common.Core.Reports.ReportFunctionExecutions.DataSync_MoveDataIntoClockWork:Invalid source file info:info={0}", defaultFunctionParameter ?? "NULL"));
					}
					dataRow["success"] = true;
				}
				result.Data.Table = dataTable;
			}
			catch (Exception ex)
			{
				string text2 = string.Format("Common.Core.Reports.ReportFunctionExecutions.DataSync_LoadDataFromClockWork:err={0}", ex.ToString());
				result.Result = new RunFunctionResult
				{
					Status = new RunStatus
					{
						ErrorMessage = text2,
						LastStatusStep = eRunStatusStep.Failed
					},
					Function = function
				};
				CWLogger.Logger.Error(text2);
			}
		}

		// Token: 0x040000D6 RID: 214
		private ReportDAO dao;
	}
}
