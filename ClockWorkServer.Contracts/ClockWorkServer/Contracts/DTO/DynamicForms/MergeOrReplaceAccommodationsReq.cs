using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000625 RID: 1573
	[DataContract(Namespace = "http://tpro.ca")]
	public class MergeOrReplaceAccommodationsReq : BaseMessageReq
	{
		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06001FF9 RID: 8185 RVA: 0x0000E83B File Offset: 0x0000CA3B
		// (set) Token: 0x06001FFA RID: 8186 RVA: 0x0000E843 File Offset: 0x0000CA43
		[DataMember]
		public bool ReplaceExistingAccommodations { get; set; }

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x0000E84C File Offset: 0x0000CA4C
		// (set) Token: 0x06001FFC RID: 8188 RVA: 0x0000E854 File Offset: 0x0000CA54
		[DataMember]
		public int SourcePersonId { get; set; }

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x0000E85D File Offset: 0x0000CA5D
		// (set) Token: 0x06001FFE RID: 8190 RVA: 0x0000E865 File Offset: 0x0000CA65
		[DataMember]
		public int SourceLuCourseId { get; set; }

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06001FFF RID: 8191 RVA: 0x0000E86E File Offset: 0x0000CA6E
		// (set) Token: 0x06002000 RID: 8192 RVA: 0x0000E876 File Offset: 0x0000CA76
		[DataMember]
		public int DestPersonId { get; set; }

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x0000E87F File Offset: 0x0000CA7F
		// (set) Token: 0x06002002 RID: 8194 RVA: 0x0000E887 File Offset: 0x0000CA87
		[DataMember]
		public int DestLuCourseId { get; set; }
	}
}
