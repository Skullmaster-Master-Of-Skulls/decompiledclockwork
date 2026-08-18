using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Templates;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001BF RID: 447
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(TemplateDTO))]
	public class BaseTemplateDTO
	{
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x00004AC5 File Offset: 0x00002CC5
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x00004ACD File Offset: 0x00002CCD
		[DataMember]
		public int TemplateId { get; set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x00004AD6 File Offset: 0x00002CD6
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x00004ADE File Offset: 0x00002CDE
		[DataMember]
		public string TemplateTitle { get; set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x00004AE7 File Offset: 0x00002CE7
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x00004AEF File Offset: 0x00002CEF
		[DataMember]
		public TemplateGroupDTO Group { get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x00004AF8 File Offset: 0x00002CF8
		// (set) Token: 0x06000A4F RID: 2639 RVA: 0x00004B00 File Offset: 0x00002D00
		[DataMember]
		public eTemplateType TemplateType { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x00004B09 File Offset: 0x00002D09
		// (set) Token: 0x06000A51 RID: 2641 RVA: 0x00004B11 File Offset: 0x00002D11
		[DataMember]
		public int OrderNum { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00004B1A File Offset: 0x00002D1A
		public bool IsTproTemplate
		{
			get
			{
				return Template.IsTemplateIdTproTemplate(this.TemplateId);
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x00004B27 File Offset: 0x00002D27
		public string TemplateGroupId
		{
			get
			{
				TemplateGroupDTO group = this.Group;
				return (group != null) ? group.TemplateGroupId : null;
			}
		}
	}
}
