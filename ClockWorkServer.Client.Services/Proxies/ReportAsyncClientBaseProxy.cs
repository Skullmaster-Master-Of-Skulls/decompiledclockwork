using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200011A RID: 282
	internal class ReportAsyncClientBaseProxy : ClientBase<IReportAsync>, IReportAsync, IReport, IService
	{
		// Token: 0x06000B0E RID: 2830 RVA: 0x0001C13C File Offset: 0x0001A33C
		public ReportAsyncClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0001C147 File Offset: 0x0001A347
		public ReportAsyncClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0001C154 File Offset: 0x0001A354
		public CompileCSharpScript2Resp CompileCSharpScript2(CompileCSharpScript2Req Request)
		{
			return base.Channel.CompileCSharpScript2(Request);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0001C174 File Offset: 0x0001A374
		public CreateReportResp CreateReport(CreateReportReq Request)
		{
			return base.Channel.CreateReport(Request);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0001C194 File Offset: 0x0001A394
		public CreateReportGroupResp CreateReportGroup(CreateReportGroupReq Request)
		{
			return base.Channel.CreateReportGroup(Request);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0001C1B2 File Offset: 0x0001A3B2
		public void DeleteClientReportGroup(DeleteClientReportGroupReq Request)
		{
			base.Channel.DeleteClientReportGroup(Request);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0001C1C2 File Offset: 0x0001A3C2
		public void DeleteReport(DeleteReportReq Request)
		{
			base.Channel.DeleteReport(Request);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0001C1D4 File Offset: 0x0001A3D4
		public ExecuteReportResp ExecuteReport(ExecuteReportReq Request)
		{
			return base.Channel.ExecuteReport(Request);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0001C1F4 File Offset: 0x0001A3F4
		public LoadReportResp LoadReport(LoadReportReq Request)
		{
			return base.Channel.LoadReport(Request);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0001C214 File Offset: 0x0001A414
		public LoadReportForestResp LoadReportForest(LoadReportForestReq Request)
		{
			return base.Channel.LoadReportForest(Request);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0001C234 File Offset: 0x0001A434
		public LoadReportForestBySourceResp LoadReportForestBySource(LoadReportForestBySourceReq Request)
		{
			return base.Channel.LoadReportForestBySource(Request);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0001C254 File Offset: 0x0001A454
		public LoadReportTechnoProNoteResp LoadReportTechnoProNote(LoadReportTechnoProNoteReq Request)
		{
			return base.Channel.LoadReportTechnoProNote(Request);
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0001C274 File Offset: 0x0001A474
		public LoadReportsResp LoadReports(LoadReportsReq Request)
		{
			return base.Channel.LoadReports(Request);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0001C294 File Offset: 0x0001A494
		public LoadReportsInAGroupResp LoadReportsInAGroup(LoadReportsInAGroupReq Request)
		{
			return base.Channel.LoadReportsInAGroup(Request);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0001C2B2 File Offset: 0x0001A4B2
		public void RecordReportExecution(RecordReportExecutionReq Request)
		{
			base.Channel.RecordReportExecution(Request);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0001C2C2 File Offset: 0x0001A4C2
		public void SaveReportTechnoProNote(SaveReportTechnoProNoteReq Request)
		{
			base.Channel.SaveReportTechnoProNote(Request);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0001C2D4 File Offset: 0x0001A4D4
		public TryToCompileCSharpResp TryToCompileCSharp(TryToCompileCSharpReq Request)
		{
			return base.Channel.TryToCompileCSharp(Request);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0001C2F2 File Offset: 0x0001A4F2
		public void UpdateReport(UpdateReportReq Request)
		{
			base.Channel.UpdateReport(Request);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0001C304 File Offset: 0x0001A504
		public IAsyncResult BeginExecuteReport(ExecuteReportReq req, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginExecuteReport(req, callback, asyncState);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0001C324 File Offset: 0x0001A524
		public ExecuteReportResp EndExecuteReport(IAsyncResult result)
		{
			return base.Channel.EndExecuteReport(result);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0001C344 File Offset: 0x0001A544
		public ExecuteReportFunctionResp ExecuteReportFunction(ExecuteReportFunctionReq Request)
		{
			return base.Channel.ExecuteReportFunction(Request);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0001C364 File Offset: 0x0001A564
		public UpdateClientReportBuiltByTproResp UpdateClientReportBuiltByTpro(UpdateClientReportBuiltByTproReq Request)
		{
			return base.Channel.UpdateClientReportBuiltByTpro(Request);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0001C384 File Offset: 0x0001A584
		public ValidateClientReportBuiltByTproIsNotTamperedWithResp ValidateClientReportBuiltByTproIsNotTamperedWith(ValidateClientReportBuiltByTproIsNotTamperedWithReq Request)
		{
			return base.Channel.ValidateClientReportBuiltByTproIsNotTamperedWith(Request);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0001C3A4 File Offset: 0x0001A5A4
		public RevertClientReportBuiltByTproToLastTproChangeResp RevertClientReportBuiltByTproToLastTproChange(RevertClientReportBuiltByTproToLastTproChangeReq Request)
		{
			return base.Channel.RevertClientReportBuiltByTproToLastTproChange(Request);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0001C3C4 File Offset: 0x0001A5C4
		public CreateClientReportBuiltByTproResp CreateClientReportBuiltByTpro(CreateClientReportBuiltByTproReq Request)
		{
			return base.Channel.CreateClientReportBuiltByTpro(Request);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0001C3E4 File Offset: 0x0001A5E4
		public CreateReportCloneResp CreateReportClone(CreateReportCloneReq Request)
		{
			return base.Channel.CreateReportClone(Request);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0001C404 File Offset: 0x0001A604
		public ExportReportToXmlForUserResp ExportReportToXmlForUser(ExportReportToXmlForUserReq Request)
		{
			return base.Channel.ExportReportToXmlForUser(Request);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0001C424 File Offset: 0x0001A624
		public ExportReportToXmlForUpdatingSystemResp ExportReportToXmlForUpdatingSystem(ExportReportToXmlForUpdatingSystemReq Request)
		{
			return base.Channel.ExportReportToXmlForUpdatingSystem(Request);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0001C444 File Offset: 0x0001A644
		public CloneReportsResp CloneReports(CloneReportsReq Request)
		{
			return base.Channel.CloneReports(Request);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0001C464 File Offset: 0x0001A664
		public CloneReportResp CloneReport(CloneReportReq Request)
		{
			return base.Channel.CloneReport(Request);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0001C484 File Offset: 0x0001A684
		public ImportReportFromXmlForUserResp ImportReportFromXmlForUser(ImportReportFromXmlForUserReq Request)
		{
			return base.Channel.ImportReportFromXmlForUser(Request);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0001C4A4 File Offset: 0x0001A6A4
		public ExportReportToXmlForUserFromReportsResp ExportReportToXmlForUserFromReports(ExportReportToXmlForUserFromReportsReq Request)
		{
			return base.Channel.ExportReportToXmlForUserFromReports(Request);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0001C4C4 File Offset: 0x0001A6C4
		public LoadReportGroupForestResp LoadReportGroupForest(LoadReportGroupForestReq Request)
		{
			return base.Channel.LoadReportGroupForest(Request);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0001C4E4 File Offset: 0x0001A6E4
		public ChangeReportOrderInSameReportGroupResp ChangeReportOrderInSameReportGroup(ChangeReportOrderInSameReportGroupReq Request)
		{
			return base.Channel.ChangeReportOrderInSameReportGroup(Request);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0001C504 File Offset: 0x0001A704
		public ChangeReportGroupOrderInSameReportGroupResp ChangeReportGroupOrderInSameReportGroup(ChangeReportGroupOrderInSameReportGroupReq Request)
		{
			return base.Channel.ChangeReportGroupOrderInSameReportGroup(Request);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0001C524 File Offset: 0x0001A724
		public MoveReportResp MoveReport(MoveReportReq Request)
		{
			return base.Channel.MoveReport(Request);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0001C544 File Offset: 0x0001A744
		public MoveReportGroupResp MoveReportGroup(MoveReportGroupReq Request)
		{
			return base.Channel.MoveReportGroup(Request);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0001C562 File Offset: 0x0001A762
		public void SortReportGroupMembersAlphabetically(SortReportGroupMembersAlphabeticallyReq Request)
		{
			base.Channel.SortReportGroupMembersAlphabetically(Request);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0001C574 File Offset: 0x0001A774
		public LoadReportsInAGroupByGroupIdResp LoadReportsInAGroupByGroupId(LoadReportsInAGroupByGroupIdReq Request)
		{
			return base.Channel.LoadReportsInAGroupByGroupId(Request);
		}
	}
}
