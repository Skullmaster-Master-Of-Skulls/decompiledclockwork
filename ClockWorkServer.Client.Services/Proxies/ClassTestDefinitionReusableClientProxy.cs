using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200002A RID: 42
	public class ClassTestDefinitionReusableClientProxy : WCFTokenBasedReusableClientProxy<IClassTestDefinition>, IClassTestDefinition, IService
	{
		// Token: 0x06000244 RID: 580 RVA: 0x00007B96 File Offset: 0x00005D96
		public ClassTestDefinitionReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00007BA1 File Offset: 0x00005DA1
		public ClassTestDefinitionReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00007BB0 File Offset: 0x00005DB0
		public CreateClassTestDefinitionBaseResp CreateClassTestDefinitionBase(CreateClassTestDefinitionBaseReq Request)
		{
			return this.WrapServiceMethod<CreateClassTestDefinitionBaseResp>(() => this.Proxy.CreateClassTestDefinitionBase(Request));
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00007BE8 File Offset: 0x00005DE8
		public LoadClassTestBaseByIdResp LoadClassTestBaseById(LoadClassTestBaseByIdReq Request)
		{
			return this.WrapServiceMethod<LoadClassTestBaseByIdResp>(() => this.Proxy.LoadClassTestBaseById(Request));
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00007C20 File Offset: 0x00005E20
		public LoadClassTestDefinitionsResp LoadClassTestDefinitions(LoadClassTestDefinitionsReq request)
		{
			return this.WrapServiceMethod<LoadClassTestDefinitionsResp>(() => this.Proxy.LoadClassTestDefinitions(request));
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00007C58 File Offset: 0x00005E58
		public void SaveClassTestDefinition(SaveClassTestDefinitionReq request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SaveClassTestDefinition(request);
			});
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00007C90 File Offset: 0x00005E90
		public void UpdateClassTestDefinitionBase(UpdateClassTestDefinitionBaseReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateClassTestDefinitionBase(Request);
			});
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00007CC8 File Offset: 0x00005EC8
		public void UpdateTestDelivered(UpdateTestDeliveredReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateTestDelivered(Request);
			});
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00007D00 File Offset: 0x00005F00
		public LoadClassTestByIdResp LoadClassTestById(LoadClassTestByIdReq Request)
		{
			return this.WrapServiceMethod<LoadClassTestByIdResp>(() => this.Proxy.LoadClassTestById(Request));
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00007D38 File Offset: 0x00005F38
		public LoadClassTestForEditByIdResp LoadClassTestForEditById(LoadClassTestForEditByIdReq Request)
		{
			return this.WrapServiceMethod<LoadClassTestForEditByIdResp>(() => this.Proxy.LoadClassTestForEditById(Request));
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00007D70 File Offset: 0x00005F70
		public void UpdateClassTestDefinition(UpdateClassTestDefinitionReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateClassTestDefinition(Request);
			});
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00007DA8 File Offset: 0x00005FA8
		public void UpdateInstructorSubmittedTestInfo(UpdateInstructorSubmittedTestInfoReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateInstructorSubmittedTestInfo(Request);
			});
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00007DE0 File Offset: 0x00005FE0
		public void UpdateInstructorContactedInfo(UpdateInstructorContactedInfoReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateInstructorContactedInfo(Request);
			});
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00007E18 File Offset: 0x00006018
		public void UpdateTestPickedUp(UpdateTestPickedUpReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateTestPickedUp(Request);
			});
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00007E50 File Offset: 0x00006050
		public LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContactResp LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContact(LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContactReq Request)
		{
			return this.WrapServiceMethod<LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContactResp>(() => this.Proxy.LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContact(Request));
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00007E88 File Offset: 0x00006088
		public LoadClassTestForExamRequestByIdResp LoadClassTestForExamRequestById(LoadClassTestForExamRequestByIdReq Request)
		{
			return this.WrapServiceMethod<LoadClassTestForExamRequestByIdResp>(() => this.Proxy.LoadClassTestForExamRequestById(Request));
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00007EC0 File Offset: 0x000060C0
		public LoadClassTestsForExamRequestByDateRangeResp LoadClassTestsForExamRequestByDateRange(LoadClassTestsForExamRequestByDateRangeReq Request)
		{
			return this.WrapServiceMethod<LoadClassTestsForExamRequestByDateRangeResp>(() => this.Proxy.LoadClassTestsForExamRequestByDateRange(Request));
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00007EF8 File Offset: 0x000060F8
		public LoadClassTestsForDisplayResp LoadClassTestsForDisplay(LoadClassTestsForDisplayReq Request)
		{
			return this.WrapServiceMethod<LoadClassTestsForDisplayResp>(() => this.Proxy.LoadClassTestsForDisplay(Request));
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00007F30 File Offset: 0x00006130
		public RemoveInstructorHasSubmittedInformationAboutThisTestMarkerResp RemoveInstructorHasSubmittedInformationAboutThisTestMarker(RemoveInstructorHasSubmittedInformationAboutThisTestMarkerReq Request)
		{
			return this.WrapServiceMethod<RemoveInstructorHasSubmittedInformationAboutThisTestMarkerResp>(() => this.Proxy.RemoveInstructorHasSubmittedInformationAboutThisTestMarker(Request));
		}
	}
}
