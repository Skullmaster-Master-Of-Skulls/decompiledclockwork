using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnoPro.Common.Public.Entities.Templates
{
	// Token: 0x02000173 RID: 371
	public class TemplateGroup : BusinessBase<string>
	{
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00012714 File Offset: 0x00010914
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public virtual string TemplateGroupId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x0001272C File Offset: 0x0001092C
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x00012734 File Offset: 0x00010934
		public string Title { get; set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x0001273D File Offset: 0x0001093D
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x00012745 File Offset: 0x00010945
		public int OrderNum { get; set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00012750 File Offset: 0x00010950
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

		// Token: 0x060008FE RID: 2302 RVA: 0x000127A4 File Offset: 0x000109A4
		public bool IsSameGroupAs(TemplateGroup otherGroup)
		{
			bool flag = otherGroup == null;
			return !flag && (otherGroup.TemplateGroupId ?? "").Equals(this.TemplateGroupId ?? "", StringComparison.OrdinalIgnoreCase);
		}
	}
}
