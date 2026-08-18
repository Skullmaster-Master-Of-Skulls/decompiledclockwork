using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Core.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers.AppointmentsTestBooking;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200001B RID: 27
	public class TestProctorServiceManager : ITestProctor, IService
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00006F64 File Offset: 0x00005164
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006F78 File Offset: 0x00005178
		public LoadAllProctorsResp LoadAllProctors(LoadAllProctorsReq Request)
		{
			ITestProctorManager testProctorManager = new TestProctorManager(Request.GetOperationContext());
			IList<Proctor> list = testProctorManager.LoadAllProctors();
			LoadAllProctorsResp loadAllProctorsResp = new LoadAllProctorsResp();
			List<ProctorDTO> proctors;
			if (list != null)
			{
				proctors = list.ToList<Proctor>().ConvertAll<ProctorDTO>((Proctor f) => f.ToDTO());
			}
			else
			{
				proctors = null;
			}
			loadAllProctorsResp.Proctors = proctors;
			return loadAllProctorsResp;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006FDC File Offset: 0x000051DC
		public CreateProctorResp CreateProctor(CreateProctorReq Request)
		{
			ITestProctorManager testProctorManager = new TestProctorManager(Request.GetOperationContext());
			return new CreateProctorResp
			{
				PersonId = testProctorManager.CreateProctor(Request.Proctor.ToDomainObject())
			};
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007018 File Offset: 0x00005218
		public void DeleteProctor(DeleteProctorReq Request)
		{
			ITestProctorManager testProctorManager = new TestProctorManager(Request.GetOperationContext());
			testProctorManager.DeleteProctor(Request.PersonId);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007040 File Offset: 0x00005240
		public void UpdateProctor(UpdateProctorReq Request)
		{
			ITestProctorManager testProctorManager = new TestProctorManager(Request.GetOperationContext());
			testProctorManager.UpdateProctor(Request.Proctor.ToDomainObject());
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000706C File Offset: 0x0000526C
		public LoadProctorByIdResp LoadProctorById(LoadProctorByIdReq Request)
		{
			ITestProctorManager testProctorManager = new TestProctorManager(Request.GetOperationContext());
			Proctor proctor = testProctorManager.LoadProctorById(Request.PersonId);
			return new LoadProctorByIdResp
			{
				Proctor = ((proctor == null) ? null : proctor.ToDTO())
			};
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000070B0 File Offset: 0x000052B0
		public LoadAllReadersResp LoadAllReaders(LoadAllReadersReq Request)
		{
			ITestProctorManager testProctorManager = new TestProctorManager(Request.GetOperationContext());
			IList<Proctor> list = testProctorManager.LoadAllReaders();
			LoadAllReadersResp loadAllReadersResp = new LoadAllReadersResp();
			List<ProctorDTO> proctors;
			if (list != null)
			{
				proctors = list.ToList<Proctor>().ConvertAll<ProctorDTO>((Proctor f) => f.ToDTO());
			}
			else
			{
				proctors = null;
			}
			loadAllReadersResp.Proctors = proctors;
			return loadAllReadersResp;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00007114 File Offset: 0x00005314
		public LoadAllScribesResp LoadAllScribes(LoadAllScribesReq Request)
		{
			ITestProctorManager testProctorManager = new TestProctorManager(Request.GetOperationContext());
			IList<Proctor> list = testProctorManager.LoadAllScribes();
			LoadAllScribesResp loadAllScribesResp = new LoadAllScribesResp();
			List<ProctorDTO> proctors;
			if (list != null)
			{
				proctors = list.ToList<Proctor>().ConvertAll<ProctorDTO>((Proctor f) => f.ToDTO());
			}
			else
			{
				proctors = null;
			}
			loadAllScribesResp.Proctors = proctors;
			return loadAllScribesResp;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007178 File Offset: 0x00005378
		public CreateReaderResp CreateReader(CreateReaderReq Request)
		{
			ITestProctorManager testProctorManager = new TestProctorManager(Request.GetOperationContext());
			return new CreateReaderResp
			{
				PersonId = testProctorManager.CreateReader(Request.Proctor.ToDomainObject())
			};
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000071B4 File Offset: 0x000053B4
		public CreateScribeResp CreateScribe(CreateScribeReq Request)
		{
			ITestProctorManager testProctorManager = new TestProctorManager(Request.GetOperationContext());
			return new CreateScribeResp
			{
				PersonId = testProctorManager.CreateScribe(Request.Proctor.ToDomainObject())
			};
		}
	}
}
