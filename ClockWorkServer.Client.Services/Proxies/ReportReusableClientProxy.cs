using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200011B RID: 283
	public class ReportReusableClientProxy : WCFTokenBasedReusableClientProxy<IReport>, IReport, IService
	{
		// Token: 0x06000B35 RID: 2869 RVA: 0x0001C592 File Offset: 0x0001A792
		public ReportReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0001C59D File Offset: 0x0001A79D
		public ReportReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0001C5AC File Offset: 0x0001A7AC
		public CreateReportResp CreateReport(CreateReportReq Request)
		{
			return this.WrapServiceMethod<CreateReportResp>(() => this.Proxy.CreateReport(Request));
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0001C5E4 File Offset: 0x0001A7E4
		public CreateReportGroupResp CreateReportGroup(CreateReportGroupReq Request)
		{
			return this.WrapServiceMethod<CreateReportGroupResp>(() => this.Proxy.CreateReportGroup(Request));
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0001C61C File Offset: 0x0001A81C
		public ExecuteReportResp ExecuteReport(ExecuteReportReq Request)
		{
			return this.WrapServiceMethod<ExecuteReportResp>(() => this.Proxy.ExecuteReport(Request));
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0001C654 File Offset: 0x0001A854
		public LoadReportResp LoadReport(LoadReportReq Request)
		{
			return this.WrapServiceMethod<LoadReportResp>(() => this.Proxy.LoadReport(Request));
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0001C68C File Offset: 0x0001A88C
		public LoadReportForestResp LoadReportForest(LoadReportForestReq Request)
		{
			return this.WrapServiceMethod<LoadReportForestResp>(() => this.Proxy.LoadReportForest(Request));
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0001C6C4 File Offset: 0x0001A8C4
		public LoadReportsResp LoadReports(LoadReportsReq Request)
		{
			return this.WrapServiceMethod<LoadReportsResp>(() => this.Proxy.LoadReports(Request));
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0001C6FC File Offset: 0x0001A8FC
		public LoadReportForestBySourceResp LoadReportForestBySource(LoadReportForestBySourceReq Request)
		{
			return this.WrapServiceMethod<LoadReportForestBySourceResp>(() => this.Proxy.LoadReportForestBySource(Request));
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0001C734 File Offset: 0x0001A934
		public LoadReportsInAGroupResp LoadReportsInAGroup(LoadReportsInAGroupReq Request)
		{
			return this.WrapServiceMethod<LoadReportsInAGroupResp>(() => this.Proxy.LoadReportsInAGroup(Request));
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0001C76C File Offset: 0x0001A96C
		public void DeleteReport(DeleteReportReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteReport(Request);
			});
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0001C7A4 File Offset: 0x0001A9A4
		public void UpdateReport(UpdateReportReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateReport(Request);
			});
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0001C7DC File Offset: 0x0001A9DC
		public void RecordReportExecution(RecordReportExecutionReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.RecordReportExecution(Request);
			});
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0001C814 File Offset: 0x0001AA14
		public void DeleteClientReportGroup(DeleteClientReportGroupReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteClientReportGroup(Request);
			});
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0001C84C File Offset: 0x0001AA4C
		public LoadReportTechnoProNoteResp LoadReportTechnoProNote(LoadReportTechnoProNoteReq Request)
		{
			return this.WrapServiceMethod<LoadReportTechnoProNoteResp>(() => this.Proxy.LoadReportTechnoProNote(Request));
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0001C884 File Offset: 0x0001AA84
		public void SaveReportTechnoProNote(SaveReportTechnoProNoteReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SaveReportTechnoProNote(Request);
			});
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0001C8BC File Offset: 0x0001AABC
		public CompileCSharpScript2Resp CompileCSharpScript2(CompileCSharpScript2Req Request)
		{
			return this.WrapServiceMethod<CompileCSharpScript2Resp>(() => this.Proxy.CompileCSharpScript2(Request));
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0001C8F4 File Offset: 0x0001AAF4
		public TryToCompileCSharpResp TryToCompileCSharp(TryToCompileCSharpReq Request)
		{
			return this.WrapServiceMethod<TryToCompileCSharpResp>(() => this.Proxy.TryToCompileCSharp(Request));
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0001C92C File Offset: 0x0001AB2C
		public ExecuteReportFunctionResp ExecuteReportFunction(ExecuteReportFunctionReq Request)
		{
			return this.WrapServiceMethod<ExecuteReportFunctionResp>(() => this.Proxy.ExecuteReportFunction(Request));
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0001C964 File Offset: 0x0001AB64
		public UpdateClientReportBuiltByTproResp UpdateClientReportBuiltByTpro(UpdateClientReportBuiltByTproReq Request)
		{
			return this.WrapServiceMethod<UpdateClientReportBuiltByTproResp>(() => this.Proxy.UpdateClientReportBuiltByTpro(Request));
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0001C99C File Offset: 0x0001AB9C
		public ValidateClientReportBuiltByTproIsNotTamperedWithResp ValidateClientReportBuiltByTproIsNotTamperedWith(ValidateClientReportBuiltByTproIsNotTamperedWithReq Request)
		{
			return this.WrapServiceMethod<ValidateClientReportBuiltByTproIsNotTamperedWithResp>(() => this.Proxy.ValidateClientReportBuiltByTproIsNotTamperedWith(Request));
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0001C9D4 File Offset: 0x0001ABD4
		public RevertClientReportBuiltByTproToLastTproChangeResp RevertClientReportBuiltByTproToLastTproChange(RevertClientReportBuiltByTproToLastTproChangeReq Request)
		{
			return this.WrapServiceMethod<RevertClientReportBuiltByTproToLastTproChangeResp>(() => this.Proxy.RevertClientReportBuiltByTproToLastTproChange(Request));
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0001CA0C File Offset: 0x0001AC0C
		public CreateClientReportBuiltByTproResp CreateClientReportBuiltByTpro(CreateClientReportBuiltByTproReq Request)
		{
			return this.WrapServiceMethod<CreateClientReportBuiltByTproResp>(() => this.Proxy.CreateClientReportBuiltByTpro(Request));
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0001CA44 File Offset: 0x0001AC44
		public CreateReportCloneResp CreateReportClone(CreateReportCloneReq Request)
		{
			return this.WrapServiceMethod<CreateReportCloneResp>(() => this.Proxy.CreateReportClone(Request));
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0001CA7C File Offset: 0x0001AC7C
		public ExportReportToXmlForUserResp ExportReportToXmlForUser(ExportReportToXmlForUserReq Request)
		{
			return this.WrapServiceMethod<ExportReportToXmlForUserResp>(() => this.Proxy.ExportReportToXmlForUser(Request));
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0001CAB4 File Offset: 0x0001ACB4
		public ExportReportToXmlForUpdatingSystemResp ExportReportToXmlForUpdatingSystem(ExportReportToXmlForUpdatingSystemReq Request)
		{
			return this.WrapServiceMethod<ExportReportToXmlForUpdatingSystemResp>(() => this.Proxy.ExportReportToXmlForUpdatingSystem(Request));
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0001CAEC File Offset: 0x0001ACEC
		public CloneReportsResp CloneReports(CloneReportsReq Request)
		{
			return this.WrapServiceMethod<CloneReportsResp>(() => this.Proxy.CloneReports(Request));
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0001CB24 File Offset: 0x0001AD24
		public CloneReportResp CloneReport(CloneReportReq Request)
		{
			return this.WrapServiceMethod<CloneReportResp>(() => this.Proxy.CloneReport(Request));
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0001CB5C File Offset: 0x0001AD5C
		public ImportReportFromXmlForUserResp ImportReportFromXmlForUser(ImportReportFromXmlForUserReq Request)
		{
			return this.WrapServiceMethod<ImportReportFromXmlForUserResp>(() => this.Proxy.ImportReportFromXmlForUser(Request));
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0001CB94 File Offset: 0x0001AD94
		public ExportReportToXmlForUserFromReportsResp ExportReportToXmlForUserFromReports(ExportReportToXmlForUserFromReportsReq Request)
		{
			return this.WrapServiceMethod<ExportReportToXmlForUserFromReportsResp>(() => this.Proxy.ExportReportToXmlForUserFromReports(Request));
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0001CBCC File Offset: 0x0001ADCC
		public LoadReportGroupForestResp LoadReportGroupForest(LoadReportGroupForestReq Request)
		{
			return this.WrapServiceMethod<LoadReportGroupForestResp>(() => this.Proxy.LoadReportGroupForest(Request));
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0001CC04 File Offset: 0x0001AE04
		public ChangeReportOrderInSameReportGroupResp ChangeReportOrderInSameReportGroup(ChangeReportOrderInSameReportGroupReq Request)
		{
			return this.WrapServiceMethod<ChangeReportOrderInSameReportGroupResp>(() => this.Proxy.ChangeReportOrderInSameReportGroup(Request));
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0001CC3C File Offset: 0x0001AE3C
		public ChangeReportGroupOrderInSameReportGroupResp ChangeReportGroupOrderInSameReportGroup(ChangeReportGroupOrderInSameReportGroupReq Request)
		{
			return this.WrapServiceMethod<ChangeReportGroupOrderInSameReportGroupResp>(() => this.Proxy.ChangeReportGroupOrderInSameReportGroup(Request));
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0001CC74 File Offset: 0x0001AE74
		public MoveReportResp MoveReport(MoveReportReq Request)
		{
			return this.WrapServiceMethod<MoveReportResp>(() => this.Proxy.MoveReport(Request));
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0001CCAC File Offset: 0x0001AEAC
		public MoveReportGroupResp MoveReportGroup(MoveReportGroupReq Request)
		{
			return this.WrapServiceMethod<MoveReportGroupResp>(() => this.Proxy.MoveReportGroup(Request));
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0001CCE4 File Offset: 0x0001AEE4
		public void SortReportGroupMembersAlphabetically(SortReportGroupMembersAlphabeticallyReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SortReportGroupMembersAlphabetically(Request);
			});
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0001CD1C File Offset: 0x0001AF1C
		public LoadReportsInAGroupByGroupIdResp LoadReportsInAGroupByGroupId(LoadReportsInAGroupByGroupIdReq Request)
		{
			return this.WrapServiceMethod<LoadReportsInAGroupByGroupIdResp>(() => this.Proxy.LoadReportsInAGroupByGroupId(Request));
		}
	}
}
