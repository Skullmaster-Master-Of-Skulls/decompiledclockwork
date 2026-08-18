using System;
using System.Data;
using System.IO;
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
	// Token: 0x02000068 RID: 104
	public class CustomDataWrite : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x00018463 File Offset: 0x00016663
		public CustomDataWrite()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0001847E File Offset: 0x0001667E
		public CustomDataWrite(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0001849C File Offset: 0x0001669C
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x000184A4 File Offset: 0x000166A4
		public OperationContext OpContext { get; set; }

		// Token: 0x06000444 RID: 1092 RVA: 0x000184B0 File Offset: 0x000166B0
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			string defaultFunctionParameter = function.GetDefaultFunctionParameter();
			CustomDataParametersWithLoadParameters customDataParametersWithLoadParameters = defaultFunctionParameter.CustomDataParametersWithLoadParametersFromXml();
			string text = customDataParametersWithLoadParameters.SourceFileName ?? "";
			bool flag = string.IsNullOrEmpty(text) || !File.Exists(text);
			if (flag)
			{
				throw new InvalidParameterException("CustomDataWrite:Invalid sourcefilename (empty or file doesn't exist):fn=" + (text ?? "NULL"));
			}
			bool flag2 = string.IsNullOrEmpty(customDataParametersWithLoadParameters.ExternalStudentNumberColumnName);
			if (flag2)
			{
				throw new InvalidParameterException("CustomDataWrite:Empty ExternalStudentNumberColumnName");
			}
			bool flag3 = string.IsNullOrEmpty(customDataParametersWithLoadParameters.CustomTableNameWithoutCustomPrefix);
			if (flag3)
			{
				throw new InvalidParameterException("CustomDataWrite:Empty CustomTableNameWithoutCustomPrefix");
			}
			IDataSyncManager dataSyncManager = new DataSyncManager(this.OpContext);
			eCustomDataLoadType loadType = customDataParametersWithLoadParameters.GetLoadType();
			DateTime now = DateTime.Now;
			switch (loadType)
			{
			case eCustomDataLoadType.Csv:
				dataSyncManager.CopyCsvDataToCustomTable(customDataParametersWithLoadParameters.CustomTableNameWithoutCustomPrefix, customDataParametersWithLoadParameters.SourceFileName, customDataParametersWithLoadParameters.ExternalStudentNumberColumnName, !customDataParametersWithLoadParameters.FirstRowDoesntHaveHeaders, Array.Empty<string>());
				goto IL_18F;
			case eCustomDataLoadType.TabDelimited:
				dataSyncManager.CopyTabDelimitedDataToCustomTable(customDataParametersWithLoadParameters.CustomTableNameWithoutCustomPrefix, customDataParametersWithLoadParameters.SourceFileName, customDataParametersWithLoadParameters.ExternalStudentNumberColumnName, !customDataParametersWithLoadParameters.FirstRowDoesntHaveHeaders, Array.Empty<string>());
				goto IL_18F;
			case eCustomDataLoadType.CustomDelimited:
			{
				bool flag4 = string.IsNullOrEmpty(customDataParametersWithLoadParameters.CustomDelimiter);
				if (flag4)
				{
					throw new Exception("CustomDataWrite: Load type is customdelimited but custom delimiter is empty");
				}
				dataSyncManager.CopyCharacterDelimitedDataToCustomTable(customDataParametersWithLoadParameters.CustomDelimiter[0], customDataParametersWithLoadParameters.CustomTableNameWithoutCustomPrefix, customDataParametersWithLoadParameters.SourceFileName, customDataParametersWithLoadParameters.ExternalStudentNumberColumnName, !customDataParametersWithLoadParameters.FirstRowDoesntHaveHeaders, Array.Empty<string>());
				goto IL_18F;
			}
			}
			throw new InvalidParameterException("CustomDataWrite: Invalid load type: " + loadType.ToString());
			IL_18F:
			double totalSeconds = (DateTime.Now - now).TotalSeconds;
			DataTable dataTable = new DataTable("q");
			dataTable.Columns.Add("FileType");
			dataTable.Columns.Add("SecondsToComplete", typeof(int));
			dataTable.Rows.Add(new object[]
			{
				loadType.ToString(),
				totalSeconds.ToString()
			});
			result.Data.Table = dataTable;
		}

		// Token: 0x040000C6 RID: 198
		private ReportDAO dao;
	}
}
