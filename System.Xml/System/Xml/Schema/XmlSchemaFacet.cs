using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200024F RID: 591
	public abstract class XmlSchemaFacet : XmlSchemaAnnotated
	{
		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001C46 RID: 7238 RVA: 0x00083021 File Offset: 0x00082021
		// (set) Token: 0x06001C47 RID: 7239 RVA: 0x00083029 File Offset: 0x00082029
		[XmlAttribute("value")]
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001C48 RID: 7240 RVA: 0x00083032 File Offset: 0x00082032
		// (set) Token: 0x06001C49 RID: 7241 RVA: 0x0008303A File Offset: 0x0008203A
		[DefaultValue(false)]
		[XmlAttribute("fixed")]
		public virtual bool IsFixed
		{
			get
			{
				return this.isFixed;
			}
			set
			{
				if (!(this is XmlSchemaEnumerationFacet) && !(this is XmlSchemaPatternFacet))
				{
					this.isFixed = value;
				}
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001C4A RID: 7242 RVA: 0x00083053 File Offset: 0x00082053
		// (set) Token: 0x06001C4B RID: 7243 RVA: 0x0008305B File Offset: 0x0008205B
		internal FacetType FacetType
		{
			get
			{
				return this.facetType;
			}
			set
			{
				this.facetType = value;
			}
		}

		// Token: 0x0400117C RID: 4476
		private string value;

		// Token: 0x0400117D RID: 4477
		private bool isFixed;

		// Token: 0x0400117E RID: 4478
		private FacetType facetType;
	}
}
