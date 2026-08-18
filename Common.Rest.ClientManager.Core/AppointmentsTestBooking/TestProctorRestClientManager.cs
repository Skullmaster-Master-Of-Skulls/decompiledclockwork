using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AppointmentsTestBooking
{
	// Token: 0x0200007E RID: 126
	public class TestProctorRestClientManager : BearerTokenRestProxy<ITestProctorClientManager>, ITestProctorClientManager, IWebService
	{
		// Token: 0x060004DE RID: 1246 RVA: 0x0000DBD4 File Offset: 0x0000BDD4
		public TestProctorRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000DBDE File Offset: 0x0000BDDE
		public TestProctorRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000DBE9 File Offset: 0x0000BDE9
		public IList<ProctorDTO> LoadAllProctors(eProctorSubType proctorSubType)
		{
			if (proctorSubType == eProctorSubType.Reader)
			{
				return this.LoadAllReaders();
			}
			if (proctorSubType != eProctorSubType.Scribe)
			{
				return this.LoadAllProctors();
			}
			return this.LoadAllScribes();
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000DC09 File Offset: 0x0000BE09
		public IList<ProctorDTO> LoadAllProctors()
		{
			return base.GetMany<ProctorDTO>("testproctor", true);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000DC17 File Offset: 0x0000BE17
		public IList<ProctorDTO> LoadAllReaders()
		{
			return base.GetMany<ProctorDTO>("testproctor/allreaders", true);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000DC25 File Offset: 0x0000BE25
		public IList<ProctorDTO> LoadAllScribes()
		{
			return base.GetMany<ProctorDTO>("testproctor/allscribes", true);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000DC33 File Offset: 0x0000BE33
		public int CreateProctor(ProctorDTO Proctor, eProctorSubType proctorSubType)
		{
			if (proctorSubType == eProctorSubType.Reader)
			{
				return this.CreateReader(Proctor);
			}
			if (proctorSubType != eProctorSubType.Scribe)
			{
				return this.CreateProctor(Proctor);
			}
			return this.CreateScribe(Proctor);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000DC56 File Offset: 0x0000BE56
		public int CreateProctor(ProctorDTO Proctor)
		{
			return base.Post<ProctorDTO, int>(Proctor, "testproctor");
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000DC64 File Offset: 0x0000BE64
		public int CreateReader(ProctorDTO Proctor)
		{
			return base.Post<ProctorDTO, int>(Proctor, "testproctor/reader");
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000DC72 File Offset: 0x0000BE72
		public int CreateScribe(ProctorDTO Proctor)
		{
			return base.Post<ProctorDTO, int>(Proctor, "testproctor/scribe");
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000DC80 File Offset: 0x0000BE80
		public void UpdateProctor(ProctorDTO Proctor)
		{
			base.Put<ProctorDTO>(Proctor, "testproctor");
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000DC8E File Offset: 0x0000BE8E
		public void DeleteProctor(int PersonId)
		{
			base.Delete(string.Format("testproctor/id/{0}", PersonId));
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000DCA6 File Offset: 0x0000BEA6
		public ProctorDTO LoadProctorById(int PersonId)
		{
			return base.Get<ProctorDTO>(string.Format("testproctor/proctorid/{0}", PersonId), true);
		}
	}
}
