using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000300 RID: 768
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false)]
	public class XmlArrayAttribute : Attribute
	{
		// Token: 0x060023E9 RID: 9193 RVA: 0x000AA3F9 File Offset: 0x000A93F9
		public XmlArrayAttribute()
		{
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x000AA408 File Offset: 0x000A9408
		public XmlArrayAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x060023EB RID: 9195 RVA: 0x000AA41E File Offset: 0x000A941E
		// (set) Token: 0x060023EC RID: 9196 RVA: 0x000AA434 File Offset: 0x000A9434
		public string ElementName
		{
			get
			{
				if (this.elementName != null)
				{
					return this.elementName;
				}
				return string.Empty;
			}
			set
			{
				this.elementName = value;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x000AA43D File Offset: 0x000A943D
		// (set) Token: 0x060023EE RID: 9198 RVA: 0x000AA445 File Offset: 0x000A9445
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

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x060023EF RID: 9199 RVA: 0x000AA44E File Offset: 0x000A944E
		// (set) Token: 0x060023F0 RID: 9200 RVA: 0x000AA456 File Offset: 0x000A9456
		public bool IsNullable
		{
			get
			{
				return this.nullable;
			}
			set
			{
				this.nullable = value;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x060023F1 RID: 9201 RVA: 0x000AA45F File Offset: 0x000A945F
		// (set) Token: 0x060023F2 RID: 9202 RVA: 0x000AA467 File Offset: 0x000A9467
		public XmlSchemaForm Form
		{
			get
			{
				return this.form;
			}
			set
			{
				this.form = value;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x060023F3 RID: 9203 RVA: 0x000AA470 File Offset: 0x000A9470
		// (set) Token: 0x060023F4 RID: 9204 RVA: 0x000AA478 File Offset: 0x000A9478
		public int Order
		{
			get
			{
				return this.order;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(Res.GetString("XmlDisallowNegativeValues"), "Order");
				}
				this.order = value;
			}
		}

		// Token: 0x04001541 RID: 5441
		private string elementName;

		// Token: 0x04001542 RID: 5442
		private string ns;

		// Token: 0x04001543 RID: 5443
		private bool nullable;

		// Token: 0x04001544 RID: 5444
		private XmlSchemaForm form;

		// Token: 0x04001545 RID: 5445
		private int order = -1;
	}
}
