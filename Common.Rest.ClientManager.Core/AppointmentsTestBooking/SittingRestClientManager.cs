using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AppointmentsTestBooking
{
	// Token: 0x02000078 RID: 120
	public class SittingRestClientManager : BearerTokenRestProxy<ISittingClientManager>, ISittingClientManager, IWebService
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x0000D2BB File Offset: 0x0000B4BB
		public SittingRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000D2C5 File Offset: 0x0000B4C5
		public SittingRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000D2D0 File Offset: 0x0000B4D0
		public int CreateSitting(SittingDTO Sitting)
		{
			return base.Post<SittingDTO, int>(Sitting, "sitting");
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000D2DE File Offset: 0x0000B4DE
		public void UpdateSitting(SittingDTO Sitting)
		{
			base.Put<SittingDTO>(Sitting, "sitting");
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000D2EC File Offset: 0x0000B4EC
		public SittingDTO LoadSittingById(int SittingId)
		{
			return base.Get<SittingDTO>(string.Format("sitting/id/{0}", SittingId), true);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000D305 File Offset: 0x0000B505
		public IList<SittingDTO> LoadSittingsByDateRange(DateTime StartDate, DateTime EndDate)
		{
			return base.GetMany<SittingDTO>(string.Format("sitting/range/{0}/{1}", StartDate, EndDate), true);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000D324 File Offset: 0x0000B524
		public void ClearSittingsOnAppointments(params int[] AppointmentIds)
		{
			base.Post(string.Format("sitting/clearsittingonappointment/appids/{0}", AppointmentIds.CommaSeparatedValuesWithoutSpace<int>()));
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000D33C File Offset: 0x0000B53C
		public void SetSittingOnAppointment(int AppointmentId, int SittingId)
		{
			this.SetSittingsOnAppointments(new Dictionary<int, int>
			{
				{
					AppointmentId,
					SittingId
				}
			});
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000D354 File Offset: 0x0000B554
		public void SetSittingsOnAppointments(IDictionary<int, int> AppointmentIdWithSittingIds)
		{
			SetSittingOnAppointmentReq setSittingOnAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetSittingOnAppointmentReq>();
			setSittingOnAppointmentReq.AppointmentIdWithSittingIds = AppointmentIdWithSittingIds;
			base.Post<SetSittingOnAppointmentReq>(setSittingOnAppointmentReq, "sitting/setsittingonappointment");
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00002BEE File Offset: 0x00000DEE
		public IList<int> LoadBookingAppointmentIdsBySitting(int SittingId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00002BEE File Offset: 0x00000DEE
		public void DeleteSitting(int SittingId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00002BEE File Offset: 0x00000DEE
		public IList<BasicTestDTO> LoadBasicBookingsBySitting(int SittingId)
		{
			throw new NotImplementedException();
		}
	}
}
