using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnoPro.Common.DynamicCompiler;
using TechnoPro.Common.DynamicCompiler.CompilerArgs;
using TechnoPro.Common.DynamicCompiler.CompilerArgs.Reports;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.DAO.Reports.Impl
{
	// Token: 0x02000005 RID: 5
	public static class CompilerUtility
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000239C File Offset: 0x0000059C
		public static RunReportResult ExecuteReport(int WhoAmI, DataTable previousTable, string code, IList<string> imports, IList<ReportParameter> parameters, string binPath = "")
		{
			CustomCSharpCode code2 = new CustomCSharpCode
			{
				Code = code,
				Imports = imports,
				BinPath = binPath
			};
			CustomCompiler<ReportParameters, ReportReturnValue> customCompiler = new CustomCompiler<ReportParameters, ReportReturnValue>(code2, eCustomCompilerType.Reports, "");
			ReportParameters reportParameters = new ReportParameters();
			reportParameters.Context = new CompileContext
			{
				BinPath = binPath,
				WhoAmI = WhoAmI
			};
			reportParameters.Table = previousTable;
			reportParameters.Variables = parameters.ToList<ReportParameter>().ConvertAll<ReportVariable>((ReportParameter g) => new ReportVariable
			{
				Name = g.Name,
				Value = g.Value
			});
			reportParameters.WhoAmI = WhoAmI;
			ReportParameters codeParameters = reportParameters;
			CustomCompileResult customCompileResult;
			ReportReturnValue reportReturnValue = customCompiler.ExecuteCode(binPath, codeParameters, out customCompileResult);
			RunReportResult runReportResult = new RunReportResult();
			runReportResult.ReportStatus = new RunStatus
			{
				ErrorMessage = customCompileResult.ErrorMessage,
				LastStatusStep = (customCompileResult.Success ? eRunStatusStep.CompletedSuccessfully : eRunStatusStep.Failed)
			};
			runReportResult.PrimaryData = new RunFunctionData
			{
				Table = ((reportReturnValue == null) ? new DataTable("empty") : reportReturnValue.Table)
			};
			IList<ReportParameter> currentReportParameters;
			if (reportReturnValue != null && reportReturnValue.VariablesOut != null)
			{
				currentReportParameters = reportReturnValue.VariablesOut.ToList<ReportVariable>().ConvertAll<ReportParameter>((ReportVariable g) => new ReportParameter
				{
					Name = g.Name,
					Value = g.Value
				});
			}
			else
			{
				currentReportParameters = new List<ReportParameter>();
			}
			runReportResult.CurrentReportParameters = currentReportParameters;
			return runReportResult;
		}
	}
}
