using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000692 RID: 1682
	[DataContract(Namespace = "http://tpro.ca")]
	public class DynamicFileDescriptionDTO
	{
		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x0600221C RID: 8732 RVA: 0x0000F8BB File Offset: 0x0000DABB
		// (set) Token: 0x0600221D RID: 8733 RVA: 0x0000F8C3 File Offset: 0x0000DAC3
		[DataMember]
		public int DataId { get; set; }

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x0000F8CC File Offset: 0x0000DACC
		// (set) Token: 0x0600221F RID: 8735 RVA: 0x0000F8D4 File Offset: 0x0000DAD4
		[DataMember]
		public int ControlId { get; set; }

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06002220 RID: 8736 RVA: 0x0000F8DD File Offset: 0x0000DADD
		// (set) Token: 0x06002221 RID: 8737 RVA: 0x0000F8E5 File Offset: 0x0000DAE5
		[DataMember]
		public string Filename { get; set; }

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06002222 RID: 8738 RVA: 0x0000F8EE File Offset: 0x0000DAEE
		// (set) Token: 0x06002223 RID: 8739 RVA: 0x0000F8F6 File Offset: 0x0000DAF6
		[DataMember]
		public int FileId { get; set; }

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06002224 RID: 8740 RVA: 0x0000F8FF File Offset: 0x0000DAFF
		// (set) Token: 0x06002225 RID: 8741 RVA: 0x0000F907 File Offset: 0x0000DB07
		[DataMember]
		public eDynamicFormType FormType { get; set; }

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06002226 RID: 8742 RVA: 0x0000F910 File Offset: 0x0000DB10
		// (set) Token: 0x06002227 RID: 8743 RVA: 0x0000F918 File Offset: 0x0000DB18
		[DataMember]
		public DateTime? DateUploaded { get; set; }

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06002228 RID: 8744 RVA: 0x0000F921 File Offset: 0x0000DB21
		// (set) Token: 0x06002229 RID: 8745 RVA: 0x0000F929 File Offset: 0x0000DB29
		[DataMember]
		public string Note { get; set; }
	}
}
