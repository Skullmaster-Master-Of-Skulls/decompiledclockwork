using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200033E RID: 830
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
	public class XmlTypeAttribute : Attribute
	{
		// Token: 0x0600289C RID: 10396 RVA: 0x000D1D12 File Offset: 0x000D0D12
		public XmlTypeAttribute()
		{
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x000D1D21 File Offset: 0x000D0D21
		public XmlTypeAttribute(string typeName)
		{
			this.typeName = typeName;
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x0600289E RID: 10398 RVA: 0x000D1D37 File Offset: 0x000D0D37
		// (set) Token: 0x0600289F RID: 10399 RVA: 0x000D1D3F File Offset: 0x000D0D3F
		public bool AnonymousType
		{
			get
			{
				return this.anonymousType;
			}
			set
			{
				this.anonymousType = value;
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060028A0 RID: 10400 RVA: 0x000D1D48 File Offset: 0x000D0D48
		// (set) Token: 0x060028A1 RID: 10401 RVA: 0x000D1D50 File Offset: 0x000D0D50
		public bool IncludeInSchema
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

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060028A2 RID: 10402 RVA: 0x000D1D59 File Offset: 0x000D0D59
		// (set) Token: 0x060028A3 RID: 10403 RVA: 0x000D1D6F File Offset: 0x000D0D6F
		public string TypeName
		{
			get
			{
				if (this.typeName != null)
				{
					return this.typeName;
				}
				return string.Empty;
			}
			set
			{
				this.typeName = value;
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x060028A4 RID: 10404 RVA: 0x000D1D78 File Offset: 0x000D0D78
		// (set) Token: 0x060028A5 RID: 10405 RVA: 0x000D1D80 File Offset: 0x000D0D80
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		// Token: 0x04001689 RID: 5769
		private bool includeInSchema = true;

		// Token: 0x0400168A RID: 5770
		private bool anonymousType;

		// Token: 0x0400168B RID: 5771
		private string ns;

		// Token: 0x0400168C RID: 5772
		private string typeName;
	}
}
