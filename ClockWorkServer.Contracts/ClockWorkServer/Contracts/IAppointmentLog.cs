using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentLog;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200000F RID: 15
	[ServiceContract]
	public interface IAppointmentLog : IService
	{
		// Token: 0x0600008A RID: 138
		[OperationContract(IsOneWay = true)]
		void LogAppModifications(LogAppModificationsReq request);

		// Token: 0x0600008B RID: 139
		[OperationContract(IsOneWay = true)]
		void LogAppDeletion(LogAppDeletionReq request);

		// Token: 0x0600008C RID: 140
		[OperationContract(IsOneWay = true)]
		void LogAppCreation(LogAppCreationReq request);
	}
}
