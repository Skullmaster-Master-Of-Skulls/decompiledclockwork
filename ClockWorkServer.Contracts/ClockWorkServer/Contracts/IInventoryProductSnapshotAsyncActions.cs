using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000050 RID: 80
	[ServiceContract]
	public interface IInventoryProductSnapshotAsyncActions : IService
	{
		// Token: 0x0600026A RID: 618
		[OperationContract(IsOneWay = true)]
		void SaveAsPointOfContact(SaveAsPointOfContactReq request);

		// Token: 0x0600026B RID: 619
		[OperationContract(IsOneWay = true)]
		void SaveListAsPointOfContact(SaveListAsPointOfContactReq request);
	}
}
