using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000071 RID: 113
	[ServiceContract]
	public interface IPeopleCache : IService
	{
		// Token: 0x0600035E RID: 862
		[OperationContract(IsOneWay = true)]
		void LoadAllUserObjectsIntoCache(LoadAllUserObjectsIntoCacheReq request);
	}
}
