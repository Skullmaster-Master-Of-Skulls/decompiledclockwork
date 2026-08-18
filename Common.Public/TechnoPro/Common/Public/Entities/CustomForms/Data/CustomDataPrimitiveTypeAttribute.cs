using System;

namespace TechnoPro.Common.Public.Entities.CustomForms.Data
{
	// Token: 0x02000426 RID: 1062
	public class CustomDataPrimitiveTypeAttribute : Attribute
	{
		// Token: 0x0600203F RID: 8255 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public CustomDataPrimitiveTypeAttribute()
		{
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x00024855 File Offset: 0x00022A55
		public CustomDataPrimitiveTypeAttribute(string xmlTag)
		{
			this.XmlTag = xmlTag;
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06002041 RID: 8257 RVA: 0x00024867 File Offset: 0x00022A67
		// (set) Token: 0x06002042 RID: 8258 RVA: 0x0002486F File Offset: 0x00022A6F
		public bool IsHidden { get; set; }

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06002043 RID: 8259 RVA: 0x00024878 File Offset: 0x00022A78
		// (set) Token: 0x06002044 RID: 8260 RVA: 0x00024880 File Offset: 0x00022A80
		public string XmlTag { get; set; }
	}
}
