using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AD5 RID: 2773
	[DataContract(Namespace = "http://tpro.ca")]
	public class FreeTimeSearchReq : BaseMessageReq
	{
		// Token: 0x0400165C RID: 5724
		[DataMember]
		public List<int> PersonIds;

		// Token: 0x0400165D RID: 5725
		[DataMember]
		public DateTime StartDateTime;

		// Token: 0x0400165E RID: 5726
		[DataMember]
		public DateTime EndDateTime;
	}
}
