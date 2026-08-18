using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001C3 RID: 451
	[DataContract(Namespace = "http://tpro.ca")]
	public class TemplateOrGroupDTO
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x00004CFF File Offset: 0x00002EFF
		// (set) Token: 0x06000A6F RID: 2671 RVA: 0x00004D07 File Offset: 0x00002F07
		[DataMember]
		public TemplateDTO Template { get; set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x00004D10 File Offset: 0x00002F10
		// (set) Token: 0x06000A71 RID: 2673 RVA: 0x00004D18 File Offset: 0x00002F18
		[DataMember]
		public TemplateGroupDTO Group { get; set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x00004D24 File Offset: 0x00002F24
		// (set) Token: 0x06000A73 RID: 2675 RVA: 0x00004D3C File Offset: 0x00002F3C
		public virtual TemplateDTO Item
		{
			get
			{
				return this.Template;
			}
			set
			{
				this.Template = value;
			}
		}
	}
}
