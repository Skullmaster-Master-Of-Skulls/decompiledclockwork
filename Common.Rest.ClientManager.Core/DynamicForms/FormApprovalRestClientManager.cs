using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.DynamicForms
{
	// Token: 0x02000057 RID: 87
	public class FormApprovalRestClientManager : BearerTokenRestProxy<IFormApprovalClientManager>, IFormApprovalClientManager, IWebService
	{
		// Token: 0x06000361 RID: 865 RVA: 0x0000A80C File Offset: 0x00008A0C
		public FormApprovalRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000A816 File Offset: 0x00008A16
		public FormApprovalRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000A821 File Offset: 0x00008A21
		public FormApprovalScreenUserOptionsDTO GetFormApprovalScreenUserForLoggedInUserOptions(int screenNum)
		{
			return base.Get<FormApprovalScreenUserOptionsDTO>(string.Format("formapproval/screenuserforloggedinuseroptions/screennum/{0}", screenNum), true);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000A83A File Offset: 0x00008A3A
		public IList<FormApprovalPendingItemDTO> LoadPendingFormApprovalItemsForCurrentUser()
		{
			return base.GetMany<FormApprovalPendingItemDTO>("formapproval/pendingitemsforcurrentuser", true);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000A848 File Offset: 0x00008A48
		public eFormApprovalState LoadFormApprovalStatus(int studentPersonId, int appId, int screenNum)
		{
			return base.Get<eFormApprovalState>("formapproval/status", true);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000A856 File Offset: 0x00008A56
		public FormApprovalPendingItemDTO LoadPendingFormApprovalItemForCurrentUserByFormApprovalId(Guid formApprovalId)
		{
			return base.Get<FormApprovalPendingItemDTO>(string.Format("formapproval/pendingitemforcurrentuser/id/{0}", formApprovalId), true);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000A86F File Offset: 0x00008A6F
		public bool AreAnyFormApprovalScreensEnabledForLoggedInUser()
		{
			return base.Get<bool>("formapproval/formapprovalactive", true);
		}
	}
}
