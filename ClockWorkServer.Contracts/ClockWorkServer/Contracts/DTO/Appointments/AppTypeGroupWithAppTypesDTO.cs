using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments
{
	// Token: 0x02000932 RID: 2354
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppTypeGroupWithAppTypesDTO
	{
		// Token: 0x040011E9 RID: 4585
		[DataMember]
		public AppTypeGroupDTO Group;

		// Token: 0x040011EA RID: 4586
		[DataMember]
		public IList<AppTypeDTO> SubAppTypes;
	}
}
