using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Core.DynamicForms.FormApproval;
using TechnoPro.Common.Core.Mappers.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.ICore.DynamicForms.FormApproval;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000040 RID: 64
	public class FormApprovalServiceManager : IFormApproval, IService
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0000CAD0 File Offset: 0x0000ACD0
		public GetFormApprovalScreenUserForLoggedInUserOptionsResp GetFormApprovalScreenUserForLoggedInUserOptions(GetFormApprovalScreenUserForLoggedInUserOptionsReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			IFormApprovalManager formApprovalManager = new FormApprovalManager(operationContext);
			FormApprovalScreenUserOptions formApprovalScreenUserForLoggedInUserOptions = formApprovalManager.GetFormApprovalScreenUserForLoggedInUserOptions(Request.ScreenNum);
			return new GetFormApprovalScreenUserForLoggedInUserOptionsResp
			{
				Options = ((formApprovalScreenUserForLoggedInUserOptions != null) ? formApprovalScreenUserForLoggedInUserOptions.ToDTO() : null)
			};
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000CB18 File Offset: 0x0000AD18
		public LoadPendingFormApprovalItemsForCurrentUserResp LoadPendingFormApprovalItemsForCurrentUser(LoadPendingFormApprovalItemsForCurrentUserReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			IFormApprovalManager formApprovalManager = new FormApprovalManager(operationContext);
			IList<FormApprovalPendingItem> list = formApprovalManager.LoadPendingFormApprovalItemsForCurrentUser();
			LoadPendingFormApprovalItemsForCurrentUserResp loadPendingFormApprovalItemsForCurrentUserResp = new LoadPendingFormApprovalItemsForCurrentUserResp();
			IList<FormApprovalPendingItemDTO> pendingItems;
			if (list == null)
			{
				pendingItems = null;
			}
			else
			{
				pendingItems = (from g in list
				select g.ToDTO()).ToList<FormApprovalPendingItemDTO>();
			}
			loadPendingFormApprovalItemsForCurrentUserResp.PendingItems = pendingItems;
			return loadPendingFormApprovalItemsForCurrentUserResp;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000CB7C File Offset: 0x0000AD7C
		public LoadFormApprovalStatusResp LoadFormApprovalStatus(LoadFormApprovalStatusReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			IFormApprovalManager formApprovalManager = new FormApprovalManager(operationContext);
			eFormApprovalState formApprovalStatus = formApprovalManager.LoadFormApprovalStatus(Request.StudentPersonId, Request.AppointmentId, Request.ScreenNum);
			return new LoadFormApprovalStatusResp
			{
				FormApprovalStatus = formApprovalStatus
			};
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000CBC4 File Offset: 0x0000ADC4
		public LoadPendingFormApprovalItemForCurrentUserByFormApprovalIdResp LoadPendingFormApprovalItemForCurrentUserByFormApprovalId(LoadPendingFormApprovalItemForCurrentUserByFormApprovalIdReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			IFormApprovalManager formApprovalManager = new FormApprovalManager(operationContext);
			FormApprovalPendingItem formApprovalPendingItem = formApprovalManager.LoadPendingFormApprovalItemForCurrentUserByFormApprovalId(Request.FormApprovalId);
			return new LoadPendingFormApprovalItemForCurrentUserByFormApprovalIdResp
			{
				PendingItem = ((formApprovalPendingItem != null) ? formApprovalPendingItem.ToDTO() : null)
			};
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000CC0C File Offset: 0x0000AE0C
		public AreAnyFormApprovalScreensEnabledForLoggedInUserResp AreAnyFormApprovalScreensEnabledForLoggedInUser(AreAnyFormApprovalScreensEnabledForLoggedInUserReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			IFormApprovalManager formApprovalManager = new FormApprovalManager(operationContext);
			return new AreAnyFormApprovalScreensEnabledForLoggedInUserResp
			{
				AtLeastOneScreenIsEnabledForThisUser = formApprovalManager.AreAnyFormApprovalScreensEnabledForLoggedInUser()
			};
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000CC40 File Offset: 0x0000AE40
		public GetActiveFormApprovalScreenNumsWithAdminStatusForCurrentUserResp GetActiveFormApprovalScreenNumsWithAdminStatusForCurrentUser(GetActiveFormApprovalScreenNumsWithAdminStatusForCurrentUserReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			IFormApprovalManager formApprovalManager = new FormApprovalManager(operationContext);
			return new GetActiveFormApprovalScreenNumsWithAdminStatusForCurrentUserResp
			{
				FormApprovalScreenNumsWithAdminStatus = formApprovalManager.GetActiveFormApprovalScreenNumsWithAdminStatus(operationContext.WhoAmI)
			};
		}
	}
}
