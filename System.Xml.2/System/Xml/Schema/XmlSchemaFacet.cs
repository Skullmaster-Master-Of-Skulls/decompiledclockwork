using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000287 RID: 647
	public abstract class XmlSchemaFacet : XmlSchemaAnnotated
	{
		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x060026CD RID: 9933 RVA: 0x000CF149 File Offset: 0x000CD349
		// (set) Token: 0x060026CE RID: 9934 RVA: 0x000CF151 File Offset: 0x000CD351
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

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x060026CF RID: 9935 RVA: 0x000CF15A File Offset: 0x000CD35A
		// (set) Token: 0x060026D0 RID: 9936 RVA: 0x000CF162 File Offset: 0x000CD362
		[XmlAttribute("fixed")]
		[DefaultValue(false)]
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

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x060026D1 RID: 9937 RVA: 0x000CF17B File Offset: 0x000CD37B
		// (set) Token: 0x060026D2 RID: 9938 RVA: 0x000CF183 File Offset: 0x000CD383
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

		// Token: 0x040010F8 RID: 4344
		private string value;

		// Token: 0x040010F9 RID: 4345
		private bool isFixed;

		// Token: 0x040010FA RID: 4346
		private FacetType facetType;
	}
}
