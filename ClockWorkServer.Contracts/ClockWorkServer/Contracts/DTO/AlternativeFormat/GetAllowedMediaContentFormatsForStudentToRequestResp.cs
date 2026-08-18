using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C6B RID: 3179
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetAllowedMediaContentFormatsForStudentToRequestResp
	{
		// Token: 0x17001869 RID: 6249
		// (get) Token: 0x0600422F RID: 16943 RVA: 0x000204E3 File Offset: 0x0001E6E3
		// (set) Token: 0x06004230 RID: 16944 RVA: 0x000204EB File Offset: 0x0001E6EB
		[DataMember]
		public MediaContentFormat[] AllowedFormats { get; set; }
	}
}
