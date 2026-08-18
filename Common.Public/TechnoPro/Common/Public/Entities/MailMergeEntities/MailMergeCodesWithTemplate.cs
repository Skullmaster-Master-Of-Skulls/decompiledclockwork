using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Templates;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities
{
	// Token: 0x020002C3 RID: 707
	public class MailMergeCodesWithTemplate : BusinessBase<int>, ICloneable<MailMergeCodesWithTemplate>, ICloneable
	{
		// Token: 0x06001582 RID: 5506 RVA: 0x0000E1E2 File Offset: 0x0000C3E2
		public MailMergeCodesWithTemplate()
		{
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x0001ADE4 File Offset: 0x00018FE4
		// (set) Token: 0x06001584 RID: 5508 RVA: 0x0001AE0C File Offset: 0x0001900C
		public override int Id
		{
			get
			{
				return (this.Template == null) ? 0 : this.Template.TemplateId;
			}
			set
			{
				bool flag = this.Template != null;
				if (flag)
				{
					this.Template.TemplateId = value;
				}
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x0001AE34 File Offset: 0x00019034
		// (set) Token: 0x06001586 RID: 5510 RVA: 0x0001AE3C File Offset: 0x0001903C
		public List<MailMergeCode> Codes { get; set; }

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x0001AE45 File Offset: 0x00019045
		// (set) Token: 0x06001588 RID: 5512 RVA: 0x0001AE4D File Offset: 0x0001904D
		public Template Template { get; set; }

		// Token: 0x06001589 RID: 5513 RVA: 0x0001AE58 File Offset: 0x00019058
		public MailMergeCodesWithTemplate Clone()
		{
			return new MailMergeCodesWithTemplate(this);
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x0001AE70 File Offset: 0x00019070
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x0001AE88 File Offset: 0x00019088
		public MailMergeCodesWithTemplate(MailMergeCodesWithTemplate item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.Template = ((item.Template == null) ? null : item.Template.Clone());
				List<MailMergeCode> codes;
				if (item.Codes != null)
				{
					codes = item.Codes.ConvertAll<MailMergeCode>((MailMergeCode g) => g.Clone());
				}
				else
				{
					codes = null;
				}
				this.Codes = codes;
			}
		}
	}
}
