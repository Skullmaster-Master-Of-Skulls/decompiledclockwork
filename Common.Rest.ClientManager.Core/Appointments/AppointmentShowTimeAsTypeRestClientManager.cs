using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.ClientManager.ICore.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Appointments
{
	// Token: 0x02000070 RID: 112
	public class AppointmentShowTimeAsTypeRestClientManager : BearerTokenRestProxy<IAppointmentShowTimeAsTypeClientManager>, IAppointmentShowTimeAsTypeClientManager, IWebService
	{
		// Token: 0x06000437 RID: 1079 RVA: 0x0000C75B File Offset: 0x0000A95B
		public AppointmentShowTimeAsTypeRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000C765 File Offset: 0x0000A965
		public AppointmentShowTimeAsTypeRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000C770 File Offset: 0x0000A970
		public IList<AppShowTimeAsTypeDTO> LoadAllShowTimeAsTypes()
		{
			return base.GetMany<AppShowTimeAsTypeDTO>("appointmentshowtimeastype", true);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000C77E File Offset: 0x0000A97E
		public AppShowTimeAsTypeDTO LoadShowTimeAsTypeByAppCode(int AppCode)
		{
			return base.Get<AppShowTimeAsTypeDTO>(string.Format("appointmentshowtimeastype/appcode/{0}", AppCode), true);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000C797 File Offset: 0x0000A997
		public AppShowTimeAsTypeDTO LoadShowTimeAsTypeById(int showTimeAsId)
		{
			return base.Get<AppShowTimeAsTypeDTO>(string.Format("appointmentshowtimeastype/appshowtimeasid/{0}", showTimeAsId), true);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000C7B0 File Offset: 0x0000A9B0
		public void DeleteShowTimeAsTypeByAppCode(int AppCode)
		{
			base.Delete(string.Format("appointmentshowtimeastype/appcode/{0}", AppCode));
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000C7C8 File Offset: 0x0000A9C8
		public void DeleteShowTimeAsTypeById(int AppointmentShowTimeAsId)
		{
			base.Delete(string.Format("appointmentshowtimeastype/appshowtimeasid/{0}", AppointmentShowTimeAsId));
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000C7E0 File Offset: 0x0000A9E0
		public void UpdateShowTimeAsType(AppShowTimeAsTypeDTO ShowTimeAsType)
		{
			base.Put<AppShowTimeAsTypeDTO>(ShowTimeAsType, "appointmentshowtimeastype");
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000C7EE File Offset: 0x0000A9EE
		public int CreateShowTimeAsType(AppShowTimeAsTypeDTO ShowTimeAsType)
		{
			return base.Post<AppShowTimeAsTypeDTO, int>(ShowTimeAsType, "appointmentshowtimeastype");
		}
	}
}
