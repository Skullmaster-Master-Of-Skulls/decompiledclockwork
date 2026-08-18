using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AppointmentsTestBooking
{
	// Token: 0x02000094 RID: 148
	public class TestProctorClientManager : ITestProctorClientManager, IWebService
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x00017A84 File Offset: 0x00015C84
		public IList<ProctorDTO> LoadAllProctors()
		{
			LoadAllProctorsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllProctorsReq>();
			return ClientServiceFactory.GetClientInstance<ITestProctor>().LoadAllProctors(request).Proctors;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00017AB4 File Offset: 0x00015CB4
		public int CreateProctor(ProctorDTO Proctor)
		{
			CreateProctorReq createProctorReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateProctorReq>();
			createProctorReq.Proctor = Proctor;
			return ClientServiceFactory.GetClientInstance<ITestProctor>().CreateProctor(createProctorReq).PersonId;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00017AEC File Offset: 0x00015CEC
		public void UpdateProctor(ProctorDTO Proctor)
		{
			UpdateProctorReq updateProctorReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateProctorReq>();
			updateProctorReq.Proctor = Proctor;
			ClientServiceFactory.GetClientInstance<ITestProctor>().UpdateProctor(updateProctorReq);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00017B1C File Offset: 0x00015D1C
		public void DeleteProctor(int PersonId)
		{
			DeleteProctorReq deleteProctorReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteProctorReq>();
			deleteProctorReq.PersonId = PersonId;
			ClientServiceFactory.GetClientInstance<ITestProctor>().DeleteProctor(deleteProctorReq);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00017B4C File Offset: 0x00015D4C
		public ProctorDTO LoadProctorById(int PersonId)
		{
			LoadProctorByIdReq loadProctorByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadProctorByIdReq>();
			loadProctorByIdReq.PersonId = PersonId;
			return ClientServiceFactory.GetClientInstance<ITestProctor>().LoadProctorById(loadProctorByIdReq).Proctor;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00017B84 File Offset: 0x00015D84
		public IList<ProctorDTO> LoadAllReaders()
		{
			LoadAllReadersReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllReadersReq>();
			return ClientServiceFactory.GetClientInstance<ITestProctor>().LoadAllReaders(request).Proctors;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00017BB4 File Offset: 0x00015DB4
		public IList<ProctorDTO> LoadAllScribes()
		{
			LoadAllScribesReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllScribesReq>();
			return ClientServiceFactory.GetClientInstance<ITestProctor>().LoadAllScribes(request).Proctors;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00017BE4 File Offset: 0x00015DE4
		public int CreateReader(ProctorDTO Proctor)
		{
			CreateReaderReq createReaderReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateReaderReq>();
			createReaderReq.Proctor = Proctor;
			return ClientServiceFactory.GetClientInstance<ITestProctor>().CreateReader(createReaderReq).PersonId;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00017C1C File Offset: 0x00015E1C
		public int CreateScribe(ProctorDTO Proctor)
		{
			CreateScribeReq createScribeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateScribeReq>();
			createScribeReq.Proctor = Proctor;
			return ClientServiceFactory.GetClientInstance<ITestProctor>().CreateScribe(createScribeReq).PersonId;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00017C54 File Offset: 0x00015E54
		public IList<ProctorDTO> LoadAllProctors(eProctorSubType proctorSubType)
		{
			IList<ProctorDTO> result;
			if (proctorSubType != eProctorSubType.Reader)
			{
				if (proctorSubType != eProctorSubType.Scribe)
				{
					result = this.LoadAllProctors();
				}
				else
				{
					result = this.LoadAllScribes();
				}
			}
			else
			{
				result = this.LoadAllReaders();
			}
			return result;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00017C90 File Offset: 0x00015E90
		public int CreateProctor(ProctorDTO Proctor, eProctorSubType proctorSubType)
		{
			int result;
			if (proctorSubType != eProctorSubType.Reader)
			{
				if (proctorSubType != eProctorSubType.Scribe)
				{
					result = this.CreateProctor(Proctor);
				}
				else
				{
					result = this.CreateScribe(Proctor);
				}
			}
			else
			{
				result = this.CreateReader(Proctor);
			}
			return result;
		}
	}
}
