using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.ClockWorkServer.Contracts.Faults.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Attributes;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000A1 RID: 161
	[ServiceContract(Name = "ReportService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	[XtraTimeService]
	public interface IReport : IService
	{
		// Token: 0x06000497 RID: 1175
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadReportResp LoadReport(LoadReportReq Request);

		// Token: 0x06000498 RID: 1176
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadReportForestResp LoadReportForest(LoadReportForestReq Request);

		// Token: 0x06000499 RID: 1177
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadReportsResp LoadReports(LoadReportsReq Request);

		// Token: 0x0600049A RID: 1178
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ExecuteReportResp ExecuteReport(ExecuteReportReq Request);

		// Token: 0x0600049B RID: 1179
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateReportGroupResp CreateReportGroup(CreateReportGroupReq Request);

		// Token: 0x0600049C RID: 1180
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateReportResp CreateReport(CreateReportReq Request);

		// Token: 0x0600049D RID: 1181
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadReportForestBySourceResp LoadReportForestBySource(LoadReportForestBySourceReq Request);

		// Token: 0x0600049E RID: 1182
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadReportsInAGroupResp LoadReportsInAGroup(LoadReportsInAGroupReq Request);

		// Token: 0x0600049F RID: 1183
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadReportsInAGroupByGroupIdResp LoadReportsInAGroupByGroupId(LoadReportsInAGroupByGroupIdReq Request);

		// Token: 0x060004A0 RID: 1184
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteReport(DeleteReportReq Request);

		// Token: 0x060004A1 RID: 1185
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateReport(UpdateReportReq Request);

		// Token: 0x060004A2 RID: 1186
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void RecordReportExecution(RecordReportExecutionReq Request);

		// Token: 0x060004A3 RID: 1187
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteClientReportGroup(DeleteClientReportGroupReq Request);

		// Token: 0x060004A4 RID: 1188
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void SaveReportTechnoProNote(SaveReportTechnoProNoteReq Request);

		// Token: 0x060004A5 RID: 1189
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadReportTechnoProNoteResp LoadReportTechnoProNote(LoadReportTechnoProNoteReq Request);

		// Token: 0x060004A6 RID: 1190
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CompileCSharpScript2Resp CompileCSharpScript2(CompileCSharpScript2Req Request);

		// Token: 0x060004A7 RID: 1191
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		TryToCompileCSharpResp TryToCompileCSharp(TryToCompileCSharpReq Request);

		// Token: 0x060004A8 RID: 1192
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ExecuteReportFunctionResp ExecuteReportFunction(ExecuteReportFunctionReq Request);

		// Token: 0x060004A9 RID: 1193
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateClientReportBuiltByTproResp UpdateClientReportBuiltByTpro(UpdateClientReportBuiltByTproReq Request);

		// Token: 0x060004AA RID: 1194
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ValidateClientReportBuiltByTproIsNotTamperedWithResp ValidateClientReportBuiltByTproIsNotTamperedWith(ValidateClientReportBuiltByTproIsNotTamperedWithReq Request);

		// Token: 0x060004AB RID: 1195
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		RevertClientReportBuiltByTproToLastTproChangeResp RevertClientReportBuiltByTproToLastTproChange(RevertClientReportBuiltByTproToLastTproChangeReq Request);

		// Token: 0x060004AC RID: 1196
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateClientReportBuiltByTproResp CreateClientReportBuiltByTpro(CreateClientReportBuiltByTproReq Request);

		// Token: 0x060004AD RID: 1197
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateReportCloneResp CreateReportClone(CreateReportCloneReq Request);

		// Token: 0x060004AE RID: 1198
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ExportReportToXmlForUserResp ExportReportToXmlForUser(ExportReportToXmlForUserReq Request);

		// Token: 0x060004AF RID: 1199
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ExportReportToXmlForUpdatingSystemResp ExportReportToXmlForUpdatingSystem(ExportReportToXmlForUpdatingSystemReq Request);

		// Token: 0x060004B0 RID: 1200
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CloneReportsResp CloneReports(CloneReportsReq Request);

		// Token: 0x060004B1 RID: 1201
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CloneReportResp CloneReport(CloneReportReq Request);

		// Token: 0x060004B2 RID: 1202
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ImportReportFromXmlForUserResp ImportReportFromXmlForUser(ImportReportFromXmlForUserReq Request);

		// Token: 0x060004B3 RID: 1203
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ExportReportToXmlForUserFromReportsResp ExportReportToXmlForUserFromReports(ExportReportToXmlForUserFromReportsReq Request);

		// Token: 0x060004B4 RID: 1204
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadReportGroupForestResp LoadReportGroupForest(LoadReportGroupForestReq Request);

		// Token: 0x060004B5 RID: 1205
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ChangeReportOrderInSameReportGroupResp ChangeReportOrderInSameReportGroup(ChangeReportOrderInSameReportGroupReq Request);

		// Token: 0x060004B6 RID: 1206
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ChangeReportGroupOrderInSameReportGroupResp ChangeReportGroupOrderInSameReportGroup(ChangeReportGroupOrderInSameReportGroupReq Request);

		// Token: 0x060004B7 RID: 1207
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MoveReportResp MoveReport(MoveReportReq Request);

		// Token: 0x060004B8 RID: 1208
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MoveReportGroupResp MoveReportGroup(MoveReportGroupReq Request);

		// Token: 0x060004B9 RID: 1209
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void SortReportGroupMembersAlphabetically(SortReportGroupMembersAlphabeticallyReq Request);
	}
}
