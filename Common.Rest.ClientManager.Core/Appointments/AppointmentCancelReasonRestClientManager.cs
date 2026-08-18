using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.ClientManager.ICore.Appointments;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Appointments
{
	// Token: 0x0200006E RID: 110
	public class AppointmentCancelReasonRestClientManager : BearerTokenRestProxy<IAppointmentCancelReasonClientManager>, IAppointmentCancelReasonClientManager, IWebService
	{
		// Token: 0x06000425 RID: 1061 RVA: 0x0000C5BA File Offset: 0x0000A7BA
		public AppointmentCancelReasonRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000C5C4 File Offset: 0x0000A7C4
		public AppointmentCancelReasonRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000C5CF File Offset: 0x0000A7CF
		public Forest<AppCancelReasonOrGroupDTO> LoadCancelReasons()
		{
			LoadCancelReasonsResp loadCancelReasonsResp = base.Get<LoadCancelReasonsResp>("appointmentcancelreason/group", true);
			if (loadCancelReasonsResp == null)
			{
				return null;
			}
			return loadCancelReasonsResp.Forest;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000C5E8 File Offset: 0x0000A7E8
		public IList<AppCancelReasonDTO> LoadAllCancelReasons()
		{
			return base.GetMany<AppCancelReasonDTO>("appointmentcancelreason", true);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000C5F6 File Offset: 0x0000A7F6
		public AppCancelReasonDTO LoadCancelReasonById(int CancelReasonId)
		{
			return base.Get<AppCancelReasonDTO>(string.Format("appointmentcancelreason/id/{0}", CancelReasonId), true);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000C60F File Offset: 0x0000A80F
		public void DeleteCancelReason(int CancelReasonId)
		{
			base.Delete(string.Format("appointmentcancelreason/id/{0}", CancelReasonId));
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000C627 File Offset: 0x0000A827
		public void UpdateCancelReason(AppCancelReasonDTO CancelReason)
		{
			base.Put<AppCancelReasonDTO>(CancelReason, "appointmentcancelreason");
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000C635 File Offset: 0x0000A835
		public int CreateCancelReason(AppCancelReasonDTO CancelReason)
		{
			return base.Post<AppCancelReasonDTO, int>(CancelReason, "appointmentcancelreason");
		}
	}
}
