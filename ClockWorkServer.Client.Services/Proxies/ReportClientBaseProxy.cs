using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200011C RID: 284
	internal class ReportClientBaseProxy : ClientBase<IReport>, IReport, IService
	{
		// Token: 0x06000B5A RID: 2906 RVA: 0x0001CD54 File Offset: 0x0001AF54
		public ReportClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0001CD5F File Offset: 0x0001AF5F
		public ReportClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0001CD6C File Offset: 0x0001AF6C
		public CreateReportResp CreateReport(CreateReportReq Request)
		{
			return base.Channel.CreateReport(Request);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0001CD8C File Offset: 0x0001AF8C
		public CreateReportGroupResp CreateReportGroup(CreateReportGroupReq Request)
		{
			return base.Channel.CreateReportGroup(Request);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0001CDAC File Offset: 0x0001AFAC
		public ExecuteReportResp ExecuteReport(ExecuteReportReq Request)
		{
			return base.Channel.ExecuteReport(Request);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0001CDCC File Offset: 0x0001AFCC
		public LoadReportResp LoadReport(LoadReportReq Request)
		{
			return base.Channel.LoadReport(Request);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0001CDEC File Offset: 0x0001AFEC
		public LoadReportForestResp LoadReportForest(LoadReportForestReq Request)
		{
			return base.Channel.LoadReportForest(Request);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0001CE0C File Offset: 0x0001B00C
		public LoadReportsResp LoadReports(LoadReportsReq Request)
		{
			return base.Channel.LoadReports(Request);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0001CE2C File Offset: 0x0001B02C
		public LoadReportForestBySourceResp LoadReportForestBySource(LoadReportForestBySourceReq Request)
		{
			return base.Channel.LoadReportForestBySource(Request);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0001CE4C File Offset: 0x0001B04C
		public LoadReportsInAGroupResp LoadReportsInAGroup(LoadReportsInAGroupReq Request)
		{
			return base.Channel.LoadReportsInAGroup(Request);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0001CE6A File Offset: 0x0001B06A
		public void DeleteReport(DeleteReportReq Request)
		{
			base.Channel.DeleteReport(Request);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0001CE7A File Offset: 0x0001B07A
		public void UpdateReport(UpdateReportReq Request)
		{
			base.Channel.UpdateReport(Request);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0001CE8A File Offset: 0x0001B08A
		public void RecordReportExecution(RecordReportExecutionReq Request)
		{
			base.Channel.RecordReportExecution(Request);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0001CE9A File Offset: 0x0001B09A
		public void DeleteClientReportGroup(DeleteClientReportGroupReq Request)
		{
			base.Channel.DeleteClientReportGroup(Request);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0001CEAC File Offset: 0x0001B0AC
		public LoadReportTechnoProNoteResp LoadReportTechnoProNote(LoadReportTechnoProNoteReq Request)
		{
			return base.Channel.LoadReportTechnoProNote(Request);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0001CECA File Offset: 0x0001B0CA
		public void SaveReportTechnoProNote(SaveReportTechnoProNoteReq Request)
		{
			base.Channel.SaveReportTechnoProNote(Request);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0001CEDC File Offset: 0x0001B0DC
		public CompileCSharpScript2Resp CompileCSharpScript2(CompileCSharpScript2Req Request)
		{
			return base.Channel.CompileCSharpScript2(Request);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0001CEFC File Offset: 0x0001B0FC
		public TryToCompileCSharpResp TryToCompileCSharp(TryToCompileCSharpReq Request)
		{
			return base.Channel.TryToCompileCSharp(Request);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0001CF1C File Offset: 0x0001B11C
		public ExecuteReportFunctionResp ExecuteReportFunction(ExecuteReportFunctionReq Request)
		{
			return base.Channel.ExecuteReportFunction(Request);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0001CF3C File Offset: 0x0001B13C
		public UpdateClientReportBuiltByTproResp UpdateClientReportBuiltByTpro(UpdateClientReportBuiltByTproReq Request)
		{
			return base.Channel.UpdateClientReportBuiltByTpro(Request);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0001CF5C File Offset: 0x0001B15C
		public ValidateClientReportBuiltByTproIsNotTamperedWithResp ValidateClientReportBuiltByTproIsNotTamperedWith(ValidateClientReportBuiltByTproIsNotTamperedWithReq Request)
		{
			return base.Channel.ValidateClientReportBuiltByTproIsNotTamperedWith(Request);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0001CF7C File Offset: 0x0001B17C
		public RevertClientReportBuiltByTproToLastTproChangeResp RevertClientReportBuiltByTproToLastTproChange(RevertClientReportBuiltByTproToLastTproChangeReq Request)
		{
			return base.Channel.RevertClientReportBuiltByTproToLastTproChange(Request);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0001CF9C File Offset: 0x0001B19C
		public CreateClientReportBuiltByTproResp CreateClientReportBuiltByTpro(CreateClientReportBuiltByTproReq Request)
		{
			return base.Channel.CreateClientReportBuiltByTpro(Request);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0001CFBC File Offset: 0x0001B1BC
		public CreateReportCloneResp CreateReportClone(CreateReportCloneReq Request)
		{
			return base.Channel.CreateReportClone(Request);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0001CFDC File Offset: 0x0001B1DC
		public ExportReportToXmlForUserResp ExportReportToXmlForUser(ExportReportToXmlForUserReq Request)
		{
			return base.Channel.ExportReportToXmlForUser(Request);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0001CFFC File Offset: 0x0001B1FC
		public ExportReportToXmlForUpdatingSystemResp ExportReportToXmlForUpdatingSystem(ExportReportToXmlForUpdatingSystemReq Request)
		{
			return base.Channel.ExportReportToXmlForUpdatingSystem(Request);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0001D01C File Offset: 0x0001B21C
		public CloneReportsResp CloneReports(CloneReportsReq Request)
		{
			return base.Channel.CloneReports(Request);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0001D03C File Offset: 0x0001B23C
		public CloneReportResp CloneReport(CloneReportReq Request)
		{
			return base.Channel.CloneReport(Request);
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0001D05C File Offset: 0x0001B25C
		public ImportReportFromXmlForUserResp ImportReportFromXmlForUser(ImportReportFromXmlForUserReq Request)
		{
			return base.Channel.ImportReportFromXmlForUser(Request);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0001D07C File Offset: 0x0001B27C
		public ExportReportToXmlForUserFromReportsResp ExportReportToXmlForUserFromReports(ExportReportToXmlForUserFromReportsReq Request)
		{
			return base.Channel.ExportReportToXmlForUserFromReports(Request);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0001D09C File Offset: 0x0001B29C
		public LoadReportGroupForestResp LoadReportGroupForest(LoadReportGroupForestReq Request)
		{
			return base.Channel.LoadReportGroupForest(Request);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0001D0BC File Offset: 0x0001B2BC
		public ChangeReportOrderInSameReportGroupResp ChangeReportOrderInSameReportGroup(ChangeReportOrderInSameReportGroupReq Request)
		{
			return base.Channel.ChangeReportOrderInSameReportGroup(Request);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0001D0DC File Offset: 0x0001B2DC
		public ChangeReportGroupOrderInSameReportGroupResp ChangeReportGroupOrderInSameReportGroup(ChangeReportGroupOrderInSameReportGroupReq Request)
		{
			return base.Channel.ChangeReportGroupOrderInSameReportGroup(Request);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0001D0FC File Offset: 0x0001B2FC
		public MoveReportResp MoveReport(MoveReportReq Request)
		{
			return base.Channel.MoveReport(Request);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0001D11C File Offset: 0x0001B31C
		public MoveReportGroupResp MoveReportGroup(MoveReportGroupReq Request)
		{
			return base.Channel.MoveReportGroup(Request);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0001D13A File Offset: 0x0001B33A
		public void SortReportGroupMembersAlphabetically(SortReportGroupMembersAlphabeticallyReq Request)
		{
			base.Channel.SortReportGroupMembersAlphabetically(Request);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0001D14C File Offset: 0x0001B34C
		public LoadReportsInAGroupByGroupIdResp LoadReportsInAGroupByGroupId(LoadReportsInAGroupByGroupIdReq Request)
		{
			return base.Channel.LoadReportsInAGroupByGroupId(Request);
		}
	}
}
