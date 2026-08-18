using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000119 RID: 281
	public class ReportAsyncClientProxy : WCFTokenBasedAsyncClientProxy<IReportAsync>, IReportAsync, IReport, IService
	{
		// Token: 0x06000AE7 RID: 2791 RVA: 0x0001B8FB File Offset: 0x00019AFB
		public ReportAsyncClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0001B906 File Offset: 0x00019B06
		public ReportAsyncClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0001B914 File Offset: 0x00019B14
		public CompileCSharpScript2Resp CompileCSharpScript2(CompileCSharpScript2Req Request)
		{
			return this.WrapServiceMethod<CompileCSharpScript2Resp>(() => this.Proxy.CompileCSharpScript2(Request));
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0001B94C File Offset: 0x00019B4C
		public CreateReportResp CreateReport(CreateReportReq Request)
		{
			return this.WrapServiceMethod<CreateReportResp>(() => this.Proxy.CreateReport(Request));
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0001B984 File Offset: 0x00019B84
		public CreateReportGroupResp CreateReportGroup(CreateReportGroupReq Request)
		{
			return this.WrapServiceMethod<CreateReportGroupResp>(() => this.Proxy.CreateReportGroup(Request));
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0001B9BC File Offset: 0x00019BBC
		public void DeleteClientReportGroup(DeleteClientReportGroupReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteClientReportGroup(Request);
			});
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0001B9F4 File Offset: 0x00019BF4
		public void DeleteReport(DeleteReportReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteReport(Request);
			});
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0001BA2C File Offset: 0x00019C2C
		public ExecuteReportResp ExecuteReport(ExecuteReportReq Request)
		{
			return this.WrapServiceMethod<ExecuteReportResp>(() => this.Proxy.ExecuteReport(Request));
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0001BA64 File Offset: 0x00019C64
		public LoadReportResp LoadReport(LoadReportReq Request)
		{
			return this.WrapServiceMethod<LoadReportResp>(() => this.Proxy.LoadReport(Request));
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0001BA9C File Offset: 0x00019C9C
		public LoadReportForestResp LoadReportForest(LoadReportForestReq Request)
		{
			return this.WrapServiceMethod<LoadReportForestResp>(() => this.Proxy.LoadReportForest(Request));
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0001BAD4 File Offset: 0x00019CD4
		public LoadReportForestBySourceResp LoadReportForestBySource(LoadReportForestBySourceReq Request)
		{
			return this.WrapServiceMethod<LoadReportForestBySourceResp>(() => this.Proxy.LoadReportForestBySource(Request));
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0001BB0C File Offset: 0x00019D0C
		public LoadReportTechnoProNoteResp LoadReportTechnoProNote(LoadReportTechnoProNoteReq Request)
		{
			return this.WrapServiceMethod<LoadReportTechnoProNoteResp>(() => this.Proxy.LoadReportTechnoProNote(Request));
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0001BB44 File Offset: 0x00019D44
		public LoadReportsResp LoadReports(LoadReportsReq Request)
		{
			return this.WrapServiceMethod<LoadReportsResp>(() => this.Proxy.LoadReports(Request));
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0001BB7C File Offset: 0x00019D7C
		public LoadReportsInAGroupResp LoadReportsInAGroup(LoadReportsInAGroupReq Request)
		{
			return this.WrapServiceMethod<LoadReportsInAGroupResp>(() => this.Proxy.LoadReportsInAGroup(Request));
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0001BBB4 File Offset: 0x00019DB4
		public void RecordReportExecution(RecordReportExecutionReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.RecordReportExecution(Request);
			});
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0001BBEC File Offset: 0x00019DEC
		public void SaveReportTechnoProNote(SaveReportTechnoProNoteReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SaveReportTechnoProNote(Request);
			});
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0001BC24 File Offset: 0x00019E24
		public TryToCompileCSharpResp TryToCompileCSharp(TryToCompileCSharpReq Request)
		{
			return this.WrapServiceMethod<TryToCompileCSharpResp>(() => this.Proxy.TryToCompileCSharp(Request));
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0001BC5C File Offset: 0x00019E5C
		public void UpdateReport(UpdateReportReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateReport(Request);
			});
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0001BC94 File Offset: 0x00019E94
		public ExecuteReportFunctionResp ExecuteReportFunction(ExecuteReportFunctionReq Request)
		{
			return this.WrapServiceMethod<ExecuteReportFunctionResp>(() => this.Proxy.ExecuteReportFunction(Request));
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0001BCCC File Offset: 0x00019ECC
		public IAsyncResult BeginExecuteReport(ExecuteReportReq req, AsyncCallback callback, object asyncState)
		{
			return this.WrapServiceMethod<IAsyncResult>(() => this.Proxy.BeginExecuteReport(req, callback, asyncState));
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0001BD14 File Offset: 0x00019F14
		public ExecuteReportResp EndExecuteReport(IAsyncResult result)
		{
			return this.WrapServiceMethod<ExecuteReportResp>(() => this.Proxy.EndExecuteReport(result));
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0001BD4C File Offset: 0x00019F4C
		public UpdateClientReportBuiltByTproResp UpdateClientReportBuiltByTpro(UpdateClientReportBuiltByTproReq Request)
		{
			return this.WrapServiceMethod<UpdateClientReportBuiltByTproResp>(() => this.Proxy.UpdateClientReportBuiltByTpro(Request));
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0001BD84 File Offset: 0x00019F84
		public ValidateClientReportBuiltByTproIsNotTamperedWithResp ValidateClientReportBuiltByTproIsNotTamperedWith(ValidateClientReportBuiltByTproIsNotTamperedWithReq Request)
		{
			return this.WrapServiceMethod<ValidateClientReportBuiltByTproIsNotTamperedWithResp>(() => this.Proxy.ValidateClientReportBuiltByTproIsNotTamperedWith(Request));
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0001BDBC File Offset: 0x00019FBC
		public RevertClientReportBuiltByTproToLastTproChangeResp RevertClientReportBuiltByTproToLastTproChange(RevertClientReportBuiltByTproToLastTproChangeReq Request)
		{
			return this.WrapServiceMethod<RevertClientReportBuiltByTproToLastTproChangeResp>(() => this.Proxy.RevertClientReportBuiltByTproToLastTproChange(Request));
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0001BDF4 File Offset: 0x00019FF4
		public CreateClientReportBuiltByTproResp CreateClientReportBuiltByTpro(CreateClientReportBuiltByTproReq Request)
		{
			return this.WrapServiceMethod<CreateClientReportBuiltByTproResp>(() => this.Proxy.CreateClientReportBuiltByTpro(Request));
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0001BE2C File Offset: 0x0001A02C
		public CreateReportCloneResp CreateReportClone(CreateReportCloneReq Request)
		{
			return this.WrapServiceMethod<CreateReportCloneResp>(() => this.Proxy.CreateReportClone(Request));
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0001BE64 File Offset: 0x0001A064
		public ExportReportToXmlForUserResp ExportReportToXmlForUser(ExportReportToXmlForUserReq Request)
		{
			return this.WrapServiceMethod<ExportReportToXmlForUserResp>(() => this.Proxy.ExportReportToXmlForUser(Request));
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0001BE9C File Offset: 0x0001A09C
		public ExportReportToXmlForUpdatingSystemResp ExportReportToXmlForUpdatingSystem(ExportReportToXmlForUpdatingSystemReq Request)
		{
			return this.WrapServiceMethod<ExportReportToXmlForUpdatingSystemResp>(() => this.Proxy.ExportReportToXmlForUpdatingSystem(Request));
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0001BED4 File Offset: 0x0001A0D4
		public CloneReportsResp CloneReports(CloneReportsReq Request)
		{
			return this.WrapServiceMethod<CloneReportsResp>(() => this.Proxy.CloneReports(Request));
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0001BF0C File Offset: 0x0001A10C
		public CloneReportResp CloneReport(CloneReportReq Request)
		{
			return this.WrapServiceMethod<CloneReportResp>(() => this.Proxy.CloneReport(Request));
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0001BF44 File Offset: 0x0001A144
		public ImportReportFromXmlForUserResp ImportReportFromXmlForUser(ImportReportFromXmlForUserReq Request)
		{
			return this.WrapServiceMethod<ImportReportFromXmlForUserResp>(() => this.Proxy.ImportReportFromXmlForUser(Request));
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0001BF7C File Offset: 0x0001A17C
		public ExportReportToXmlForUserFromReportsResp ExportReportToXmlForUserFromReports(ExportReportToXmlForUserFromReportsReq Request)
		{
			return this.WrapServiceMethod<ExportReportToXmlForUserFromReportsResp>(() => this.Proxy.ExportReportToXmlForUserFromReports(Request));
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0001BFB4 File Offset: 0x0001A1B4
		public LoadReportGroupForestResp LoadReportGroupForest(LoadReportGroupForestReq Request)
		{
			return this.WrapServiceMethod<LoadReportGroupForestResp>(() => this.Proxy.LoadReportGroupForest(Request));
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0001BFEC File Offset: 0x0001A1EC
		public ChangeReportOrderInSameReportGroupResp ChangeReportOrderInSameReportGroup(ChangeReportOrderInSameReportGroupReq Request)
		{
			return this.WrapServiceMethod<ChangeReportOrderInSameReportGroupResp>(() => this.Proxy.ChangeReportOrderInSameReportGroup(Request));
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0001C024 File Offset: 0x0001A224
		public ChangeReportGroupOrderInSameReportGroupResp ChangeReportGroupOrderInSameReportGroup(ChangeReportGroupOrderInSameReportGroupReq Request)
		{
			return this.WrapServiceMethod<ChangeReportGroupOrderInSameReportGroupResp>(() => this.Proxy.ChangeReportGroupOrderInSameReportGroup(Request));
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0001C05C File Offset: 0x0001A25C
		public MoveReportResp MoveReport(MoveReportReq Request)
		{
			return this.WrapServiceMethod<MoveReportResp>(() => this.Proxy.MoveReport(Request));
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0001C094 File Offset: 0x0001A294
		public MoveReportGroupResp MoveReportGroup(MoveReportGroupReq Request)
		{
			return this.WrapServiceMethod<MoveReportGroupResp>(() => this.Proxy.MoveReportGroup(Request));
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0001C0CC File Offset: 0x0001A2CC
		public void SortReportGroupMembersAlphabetically(SortReportGroupMembersAlphabeticallyReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SortReportGroupMembersAlphabetically(Request);
			});
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0001C104 File Offset: 0x0001A304
		public LoadReportsInAGroupByGroupIdResp LoadReportsInAGroupByGroupId(LoadReportsInAGroupByGroupIdReq Request)
		{
			return this.WrapServiceMethod<LoadReportsInAGroupByGroupIdResp>(() => this.Proxy.LoadReportsInAGroupByGroupId(Request));
		}
	}
}
