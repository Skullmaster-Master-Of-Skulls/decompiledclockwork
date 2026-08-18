using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200002B RID: 43
	internal class ClassTestDefinitionClientBaseProxy : ClientBase<IClassTestDefinition>, IClassTestDefinition, IService
	{
		// Token: 0x06000257 RID: 599 RVA: 0x00007F68 File Offset: 0x00006168
		public ClassTestDefinitionClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00007F73 File Offset: 0x00006173
		public ClassTestDefinitionClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00007F80 File Offset: 0x00006180
		public CreateClassTestDefinitionBaseResp CreateClassTestDefinitionBase(CreateClassTestDefinitionBaseReq Request)
		{
			return base.Channel.CreateClassTestDefinitionBase(Request);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00007FA0 File Offset: 0x000061A0
		public LoadClassTestBaseByIdResp LoadClassTestBaseById(LoadClassTestBaseByIdReq Request)
		{
			return base.Channel.LoadClassTestBaseById(Request);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00007FC0 File Offset: 0x000061C0
		public LoadClassTestDefinitionsResp LoadClassTestDefinitions(LoadClassTestDefinitionsReq request)
		{
			return base.Channel.LoadClassTestDefinitions(request);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00007FDE File Offset: 0x000061DE
		public void SaveClassTestDefinition(SaveClassTestDefinitionReq request)
		{
			base.Channel.SaveClassTestDefinition(request);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00007FEE File Offset: 0x000061EE
		public void UpdateClassTestDefinitionBase(UpdateClassTestDefinitionBaseReq Request)
		{
			base.Channel.UpdateClassTestDefinitionBase(Request);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00007FFE File Offset: 0x000061FE
		public void UpdateTestDelivered(UpdateTestDeliveredReq Request)
		{
			base.Channel.UpdateTestDelivered(Request);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00008010 File Offset: 0x00006210
		public LoadClassTestByIdResp LoadClassTestById(LoadClassTestByIdReq Request)
		{
			return base.Channel.LoadClassTestById(Request);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00008030 File Offset: 0x00006230
		public LoadClassTestForEditByIdResp LoadClassTestForEditById(LoadClassTestForEditByIdReq Request)
		{
			return base.Channel.LoadClassTestForEditById(Request);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000804E File Offset: 0x0000624E
		public void UpdateClassTestDefinition(UpdateClassTestDefinitionReq Request)
		{
			base.Channel.UpdateClassTestDefinition(Request);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000805E File Offset: 0x0000625E
		public void UpdateInstructorSubmittedTestInfo(UpdateInstructorSubmittedTestInfoReq Request)
		{
			base.Channel.UpdateInstructorSubmittedTestInfo(Request);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000806E File Offset: 0x0000626E
		public void UpdateInstructorContactedInfo(UpdateInstructorContactedInfoReq Request)
		{
			base.Channel.UpdateInstructorContactedInfo(Request);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000807E File Offset: 0x0000627E
		public void UpdateTestPickedUp(UpdateTestPickedUpReq Request)
		{
			base.Channel.UpdateTestPickedUp(Request);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00008090 File Offset: 0x00006290
		public LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContactResp LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContact(LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContactReq Request)
		{
			return base.Channel.LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContact(Request);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000080B0 File Offset: 0x000062B0
		public LoadClassTestForExamRequestByIdResp LoadClassTestForExamRequestById(LoadClassTestForExamRequestByIdReq Request)
		{
			return base.Channel.LoadClassTestForExamRequestById(Request);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000080D0 File Offset: 0x000062D0
		public LoadClassTestsForExamRequestByDateRangeResp LoadClassTestsForExamRequestByDateRange(LoadClassTestsForExamRequestByDateRangeReq Request)
		{
			return base.Channel.LoadClassTestsForExamRequestByDateRange(Request);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000080F0 File Offset: 0x000062F0
		public LoadClassTestsForDisplayResp LoadClassTestsForDisplay(LoadClassTestsForDisplayReq Request)
		{
			return base.Channel.LoadClassTestsForDisplay(Request);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00008110 File Offset: 0x00006310
		public RemoveInstructorHasSubmittedInformationAboutThisTestMarkerResp RemoveInstructorHasSubmittedInformationAboutThisTestMarker(RemoveInstructorHasSubmittedInformationAboutThisTestMarkerReq Request)
		{
			return base.Channel.RemoveInstructorHasSubmittedInformationAboutThisTestMarker(Request);
		}
	}
}
