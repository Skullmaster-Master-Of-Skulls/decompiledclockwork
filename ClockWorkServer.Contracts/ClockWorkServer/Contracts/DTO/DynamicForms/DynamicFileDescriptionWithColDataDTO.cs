using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000693 RID: 1683
	[DataContract(Namespace = "http://tpro.ca")]
	public class DynamicFileDescriptionWithColDataDTO : DynamicFileDescriptionDTO
	{
		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x0600222B RID: 8747 RVA: 0x0000F932 File Offset: 0x0000DB32
		// (set) Token: 0x0600222C RID: 8748 RVA: 0x0000F93A File Offset: 0x0000DB3A
		[DataMember]
		public IList<string> ColumnData { get; set; }
	}
}
