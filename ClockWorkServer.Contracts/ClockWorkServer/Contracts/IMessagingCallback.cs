using System;
using System.ServiceModel;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000CF RID: 207
	public interface IMessagingCallback
	{
		// Token: 0x060005AD RID: 1453
		[OperationContract(IsOneWay = true)]
		void NotifyLogin(IM_User username);

		// Token: 0x060005AE RID: 1454
		[OperationContract(IsOneWay = true)]
		void MessageDelivery(InstantMessage msg);

		// Token: 0x060005AF RID: 1455
		[OperationContract(IsOneWay = true)]
		void NotifyAttachment(AttachmentInfo attInfo);

		// Token: 0x060005B0 RID: 1456
		[OperationContract(IsOneWay = true)]
		void NotifyLogout(string username);
	}
}
