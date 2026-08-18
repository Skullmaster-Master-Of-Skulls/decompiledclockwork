using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.Public.Entities.Templates
{
	// Token: 0x02000171 RID: 369
	public class Template : BaseTemplate, ICloneable<Template>, ICloneable
	{
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x000124FE File Offset: 0x000106FE
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x00012506 File Offset: 0x00010706
		public BinaryFile Document { get; set; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x0001250F File Offset: 0x0001070F
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x00012517 File Offset: 0x00010717
		public TPMailMessage EmailBehindDocumentTemplate { get; set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x00012520 File Offset: 0x00010720
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x00012528 File Offset: 0x00010728
		public TPMailMessage EmailTemplate { get; set; }

		// Token: 0x060008E9 RID: 2281 RVA: 0x00012531 File Offset: 0x00010731
		public Template()
		{
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x0001253C File Offset: 0x0001073C
		public bool IsEmpty
		{
			get
			{
				return this.Document == null && this.EmailBehindDocumentTemplate == null && this.EmailTemplate == null;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x0001256C File Offset: 0x0001076C
		public bool IsTproTemplate
		{
			get
			{
				return Template.IsTemplateIdTproTemplate(this.TemplateId);
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0001258C File Offset: 0x0001078C
		public static bool IsTemplateIdTproTemplate(int templateId)
		{
			return templateId >= 10000000;
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x000125A9 File Offset: 0x000107A9
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x000125B1 File Offset: 0x000107B1
		public IDictionary<string, string> FieldMappings { get; set; }

		// Token: 0x060008EF RID: 2287 RVA: 0x000125BC File Offset: 0x000107BC
		public Template Clone()
		{
			return new Template(this);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x000125D4 File Offset: 0x000107D4
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x000125EC File Offset: 0x000107EC
		public Template(Template item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.Document = item.Document;
				this.EmailBehindDocumentTemplate = ((item.EmailBehindDocumentTemplate == null) ? null : item.EmailBehindDocumentTemplate.Clone());
				this.EmailTemplate = ((item.EmailTemplate == null) ? null : item.EmailTemplate.Clone());
				bool flag2 = item.FieldMappings == null;
				if (flag2)
				{
					this.FieldMappings = null;
				}
				else
				{
					this.FieldMappings = new Dictionary<string, string>();
					foreach (KeyValuePair<string, string> keyValuePair in item.FieldMappings)
					{
						this.FieldMappings.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
		}
	}
}
