using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002C6 RID: 710
	internal abstract class TypeMapping : Mapping
	{
		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060021A9 RID: 8617 RVA: 0x0009F1FC File Offset: 0x0009E1FC
		// (set) Token: 0x060021AA RID: 8618 RVA: 0x0009F204 File Offset: 0x0009E204
		internal bool ReferencedByTopLevelElement
		{
			get
			{
				return this.referencedByTopLevelElement;
			}
			set
			{
				this.referencedByTopLevelElement = value;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060021AB RID: 8619 RVA: 0x0009F20D File Offset: 0x0009E20D
		// (set) Token: 0x060021AC RID: 8620 RVA: 0x0009F21F File Offset: 0x0009E21F
		internal bool ReferencedByElement
		{
			get
			{
				return this.referencedByElement || this.referencedByTopLevelElement;
			}
			set
			{
				this.referencedByElement = value;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060021AD RID: 8621 RVA: 0x0009F228 File Offset: 0x0009E228
		// (set) Token: 0x060021AE RID: 8622 RVA: 0x0009F230 File Offset: 0x0009E230
		internal string Namespace
		{
			get
			{
				return this.typeNs;
			}
			set
			{
				this.typeNs = value;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x0009F239 File Offset: 0x0009E239
		// (set) Token: 0x060021B0 RID: 8624 RVA: 0x0009F241 File Offset: 0x0009E241
		internal string TypeName
		{
			get
			{
				return this.typeName;
			}
			set
			{
				this.typeName = value;
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060021B1 RID: 8625 RVA: 0x0009F24A File Offset: 0x0009E24A
		// (set) Token: 0x060021B2 RID: 8626 RVA: 0x0009F252 File Offset: 0x0009E252
		internal TypeDesc TypeDesc
		{
			get
			{
				return this.typeDesc;
			}
			set
			{
				this.typeDesc = value;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060021B3 RID: 8627 RVA: 0x0009F25B File Offset: 0x0009E25B
		// (set) Token: 0x060021B4 RID: 8628 RVA: 0x0009F263 File Offset: 0x0009E263
		internal bool IncludeInSchema
		{
			get
			{
				return this.includeInSchema;
			}
			set
			{
				this.includeInSchema = value;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x060021B5 RID: 8629 RVA: 0x0009F26C File Offset: 0x0009E26C
		// (set) Token: 0x060021B6 RID: 8630 RVA: 0x0009F26F File Offset: 0x0009E26F
		internal virtual bool IsList
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x0009F271 File Offset: 0x0009E271
		// (set) Token: 0x060021B8 RID: 8632 RVA: 0x0009F279 File Offset: 0x0009E279
		internal bool IsReference
		{
			get
			{
				return this.reference;
			}
			set
			{
				this.reference = value;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x060021B9 RID: 8633 RVA: 0x0009F282 File Offset: 0x0009E282
		internal bool IsAnonymousType
		{
			get
			{
				return this.typeName == null || this.typeName.Length == 0;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x060021BA RID: 8634 RVA: 0x0009F29C File Offset: 0x0009E29C
		internal virtual string DefaultElementName
		{
			get
			{
				if (!this.IsAnonymousType)
				{
					return this.typeName;
				}
				return XmlConvert.EncodeLocalName(this.typeDesc.Name);
			}
		}

		// Token: 0x0400146F RID: 5231
		private TypeDesc typeDesc;

		// Token: 0x04001470 RID: 5232
		private string typeNs;

		// Token: 0x04001471 RID: 5233
		private string typeName;

		// Token: 0x04001472 RID: 5234
		private bool referencedByElement;

		// Token: 0x04001473 RID: 5235
		private bool referencedByTopLevelElement;

		// Token: 0x04001474 RID: 5236
		private bool includeInSchema = true;

		// Token: 0x04001475 RID: 5237
		private bool reference;
	}
}
