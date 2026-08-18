using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.DynamicForms
{
	// Token: 0x02000068 RID: 104
	public class FormApprovalClientManager : IFormApprovalClientManager, IWebService
	{
		// Token: 0x060003D4 RID: 980 RVA: 0x00011584 File Offset: 0x0000F784
		public FormApprovalScreenUserOptionsDTO GetFormApprovalScreenUserForLoggedInUserOptions(int screenNum)
		{
			GetFormApprovalScreenUserForLoggedInUserOptionsReq getFormApprovalScreenUserForLoggedInUserOptionsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetFormApprovalScreenUserForLoggedInUserOptionsReq>();
			getFormApprovalScreenUserForLoggedInUserOptionsReq.ScreenNum = screenNum;
			return ClientServiceFactory.GetClientInstance<IFormApproval>().GetFormApprovalScreenUserForLoggedInUserOptions(getFormApprovalScreenUserForLoggedInUserOptionsReq).Options;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x000115BC File Offset: 0x0000F7BC
		public IList<FormApprovalPendingItemDTO> LoadPendingFormApprovalItemsForCurrentUser()
		{
			LoadPendingFormApprovalItemsForCurrentUserReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPendingFormApprovalItemsForCurrentUserReq>();
			return ClientServiceFactory.GetClientInstance<IFormApproval>().LoadPendingFormApprovalItemsForCurrentUser(request).PendingItems;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x000115EC File Offset: 0x0000F7EC
		public eFormApprovalState LoadFormApprovalStatus(int studentPersonId, int appId, int screenNum)
		{
			LoadFormApprovalStatusReq loadFormApprovalStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadFormApprovalStatusReq>();
			loadFormApprovalStatusReq.StudentPersonId = studentPersonId;
			loadFormApprovalStatusReq.AppointmentId = appId;
			loadFormApprovalStatusReq.ScreenNum = screenNum;
			return ClientServiceFactory.GetClientInstance<IFormApproval>().LoadFormApprovalStatus(loadFormApprovalStatusReq).FormApprovalStatus;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00011634 File Offset: 0x0000F834
		public FormApprovalPendingItemDTO LoadPendingFormApprovalItemForCurrentUserByFormApprovalId(Guid formApprovalId)
		{
			LoadPendingFormApprovalItemForCurrentUserByFormApprovalIdReq loadPendingFormApprovalItemForCurrentUserByFormApprovalIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPendingFormApprovalItemForCurrentUserByFormApprovalIdReq>();
			loadPendingFormApprovalItemForCurrentUserByFormApprovalIdReq.FormApprovalId = formApprovalId;
			return ClientServiceFactory.GetClientInstance<IFormApproval>().LoadPendingFormApprovalItemForCurrentUserByFormApprovalId(loadPendingFormApprovalItemForCurrentUserByFormApprovalIdReq).PendingItem;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001166C File Offset: 0x0000F86C
		public bool AreAnyFormApprovalScreensEnabledForLoggedInUser()
		{
			AreAnyFormApprovalScreensEnabledForLoggedInUserReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AreAnyFormApprovalScreensEnabledForLoggedInUserReq>();
			return ClientServiceFactory.GetClientInstance<IFormApproval>().AreAnyFormApprovalScreensEnabledForLoggedInUser(request).AtLeastOneScreenIsEnabledForThisUser;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0001169C File Offset: 0x0000F89C
		public IDictionary<int, bool> GetActiveFormApprovalScreenNumsWithAdminStatusForCurrentUser()
		{
			GetActiveFormApprovalScreenNumsWithAdminStatusForCurrentUserReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetActiveFormApprovalScreenNumsWithAdminStatusForCurrentUserReq>();
			return ClientServiceFactory.GetClientInstance<IFormApproval>().GetActiveFormApprovalScreenNumsWithAdminStatusForCurrentUser(request).FormApprovalScreenNumsWithAdminStatus;
		}
	}
}
