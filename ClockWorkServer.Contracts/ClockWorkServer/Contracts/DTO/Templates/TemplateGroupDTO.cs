using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Templates;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001C2 RID: 450
	[DataContract(Namespace = "http://tpro.ca")]
	public class TemplateGroupDTO
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00004BFB File Offset: 0x00002DFB
		// (set) Token: 0x06000A65 RID: 2661 RVA: 0x00004C03 File Offset: 0x00002E03
		[DataMember]
		public string TemplateGroupId { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00004C0C File Offset: 0x00002E0C
		// (set) Token: 0x06000A67 RID: 2663 RVA: 0x00004C14 File Offset: 0x00002E14
		[DataMember]
		public string Title { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x00004C1D File Offset: 0x00002E1D
		// (set) Token: 0x06000A69 RID: 2665 RVA: 0x00004C25 File Offset: 0x00002E25
		[DataMember]
		public int OrderNum { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00004C30 File Offset: 0x00002E30
		public eTemplateGroupMeaning Meaning
		{
			get
			{
				bool flag = string.IsNullOrEmpty(this.Title);
				eTemplateGroupMeaning result;
				if (flag)
				{
					result = eTemplateGroupMeaning.Unknown;
				}
				else
				{
					List<eTemplateGroupMeaning> source = ((eTemplateGroupMeaning[])Enum.GetValues(typeof(eTemplateGroupMeaning))).ToList<eTemplateGroupMeaning>();
					result = source.FirstOrDefault(delegate(eTemplateGroupMeaning g)
					{
						TemplateGroupMeaningAttribute attribute = TemplateGroupMeaningAttribute.GetAttribute(g);
						bool flag2 = attribute == null;
						return !flag2 && attribute.GroupTitle.Equals(this.Title, StringComparison.OrdinalIgnoreCase);
					});
				}
				return result;
			}
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00004C84 File Offset: 0x00002E84
		public bool IsSameGroupAs(TemplateGroupDTO otherGroup)
		{
			bool flag = otherGroup == null;
			return !flag && (otherGroup.TemplateGroupId ?? "").Equals(this.TemplateGroupId ?? "", StringComparison.OrdinalIgnoreCase);
		}
	}
}
