using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking
{
	// Token: 0x0200008E RID: 142
	public interface ITestProctorClientManager : IWebService
	{
		// Token: 0x0600043B RID: 1083
		IList<ProctorDTO> LoadAllProctors(eProctorSubType proctorSubType);

		// Token: 0x0600043C RID: 1084
		IList<ProctorDTO> LoadAllProctors();

		// Token: 0x0600043D RID: 1085
		IList<ProctorDTO> LoadAllReaders();

		// Token: 0x0600043E RID: 1086
		IList<ProctorDTO> LoadAllScribes();

		// Token: 0x0600043F RID: 1087
		int CreateProctor(ProctorDTO Proctor, eProctorSubType proctorSubType);

		// Token: 0x06000440 RID: 1088
		int CreateProctor(ProctorDTO Proctor);

		// Token: 0x06000441 RID: 1089
		int CreateReader(ProctorDTO Proctor);

		// Token: 0x06000442 RID: 1090
		int CreateScribe(ProctorDTO Proctor);

		// Token: 0x06000443 RID: 1091
		void UpdateProctor(ProctorDTO Proctor);

		// Token: 0x06000444 RID: 1092
		void DeleteProctor(int PersonId);

		// Token: 0x06000445 RID: 1093
		ProctorDTO LoadProctorById(int PersonId);
	}
}
