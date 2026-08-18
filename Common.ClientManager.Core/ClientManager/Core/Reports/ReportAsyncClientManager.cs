using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Client.Services.Proxies;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Reports;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Exceptions;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Reports
{
	// Token: 0x02000027 RID: 39
	public class ReportAsyncClientManager : IReportAsyncClientManager, IDisposable
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00006591 File Offset: 0x00004791
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00006599 File Offset: 0x00004799
		private IReportAsync reportAsyncProxy { get; set; }

		// Token: 0x06000120 RID: 288 RVA: 0x000065A2 File Offset: 0x000047A2
		public ReportAsyncClientManager()
		{
			this.reportAsyncProxy = ClientServiceFactory.GetAsyncClientInstance<IReportAsync>();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000065BF File Offset: 0x000047BF
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000065CC File Offset: 0x000047CC
		~ReportAsyncClientManager()
		{
			this.Dispose(false);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006600 File Offset: 0x00004800
		protected virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				if (disposing)
				{
				}
				bool flag2 = this.reportAsyncProxy != null;
				if (flag2)
				{
					this.reportAsyncProxy.Close();
				}
				this.reportAsyncProxy = null;
				this.disposed = true;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000664C File Offset: 0x0000484C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006660 File Offset: 0x00004860
		public IAsyncResult BeginExecuteReport(AsyncCallback callback, object asyncState, int ReportId, IList<eFunctionType> FunctionTypesToSkip, IList<ReportParameterDTO> ReportParameters, IList<int> OnlyRunFunctionIds, eReportExecutedFromLocation ExecutedFromLocation)
		{
			bool flag = this.reportAsyncProxy == null;
			if (flag)
			{
				throw new ClockWorkServerNotConnectedException();
			}
			ExecuteReportReq executeReportReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ExecuteReportReq>();
			executeReportReq.ReportId = ReportId;
			executeReportReq.FunctionTypesToSkip = FunctionTypesToSkip;
			executeReportReq.ReportParameters = ReportParameters;
			executeReportReq.OnlyRunFunctionIds = OnlyRunFunctionIds;
			executeReportReq.ExecutedFromLocation = ExecutedFromLocation;
			executeReportReq.BinPath = ((executeReportReq.ApplicationContext != null) ? executeReportReq.ApplicationContext.ExecutingPath : null);
			ReportAsyncTempAsyncCallback asyncState2 = new ReportAsyncTempAsyncCallback
			{
				AsyncState = asyncState,
				Callback = callback,
				ReportAsyncClientManager = this,
				OriginalRequest = executeReportReq
			};
			return this.reportAsyncProxy.BeginExecuteReport(executeReportReq, new AsyncCallback(ReportAsyncClientManager.FakeEndExecuteReport), asyncState2);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006718 File Offset: 0x00004918
		public RunReportResultDTO EndExecuteReport(IAsyncResult result)
		{
			bool flag = this.reportAsyncProxy == null;
			if (flag)
			{
				throw new ClockWorkServerNotConnectedException();
			}
			return this.ReportResult;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006744 File Offset: 0x00004944
		public RunReportResultDTO EndExecuteReportFake(IAsyncResult result)
		{
			bool flag = this.reportAsyncProxy == null;
			if (flag)
			{
				throw new ClockWorkServerNotConnectedException();
			}
			ExecuteReportResp executeReportResp = this.reportAsyncProxy.EndExecuteReport(result);
			executeReportResp.ReportResult.ExecutionPlan = executeReportResp.ExecutionPlan;
			return executeReportResp.ReportResult;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006790 File Offset: 0x00004990
		private static void FakeEndExecuteReport(IAsyncResult result)
		{
			ReportAsyncTempAsyncCallback reportAsyncTempAsyncCallback = (ReportAsyncTempAsyncCallback)result.AsyncState;
			bool flag = reportAsyncTempAsyncCallback == null;
			if (!flag)
			{
				ReportAsyncClientManager reportAsyncClientManager = reportAsyncTempAsyncCallback.ReportAsyncClientManager;
				RunReportResultDTO runReportResultDTO = reportAsyncClientManager.EndExecuteReportFake(result);
				int num;
				if (runReportResultDTO.ExecutionPlan != null && runReportResultDTO.ExecutionPlan.ExecutionSteps != null)
				{
					num = runReportResultDTO.ExecutionPlan.ExecutionSteps.Count((ExecuteReportPlanItemDTO g) => !g.HasCompleted);
				}
				else
				{
					num = 0;
				}
				int num2 = num;
				bool flag2 = num2 < 1;
				if (flag2)
				{
					reportAsyncTempAsyncCallback.Callback(result);
				}
				ExecuteReportReq originalRequest = reportAsyncTempAsyncCallback.OriginalRequest;
				IReportClientManager reportClientManager = new ReportClientManager();
				reportAsyncTempAsyncCallback.ReportAsyncClientManager.ReportResult = reportClientManager.FinishReportExecutionPlan(runReportResultDTO, runReportResultDTO.ExecutionPlan, originalRequest.ExecutedFromLocation, originalRequest.OnlyRunFunctionIds, originalRequest.FunctionTypesToSkip);
				reportAsyncTempAsyncCallback.Callback(result);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00006872 File Offset: 0x00004A72
		// (set) Token: 0x0600012A RID: 298 RVA: 0x0000687A File Offset: 0x00004A7A
		public RunReportResultDTO ReportResult { get; set; }

		// Token: 0x04000006 RID: 6
		private bool disposed = false;
	}
}
