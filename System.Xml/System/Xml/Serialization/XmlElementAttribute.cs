using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x0200030B RID: 779
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true)]
	public class XmlElementAttribute : Attribute
	{
		// Token: 0x060024EE RID: 9454 RVA: 0x000ADDE6 File Offset: 0x000ACDE6
		public XmlElementAttribute()
		{
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000ADDF5 File Offset: 0x000ACDF5
		public XmlElementAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000ADE0B File Offset: 0x000ACE0B
		public XmlElementAttribute(Type type)
		{
			this.type = type;
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x000ADE21 File Offset: 0x000ACE21
		public XmlElementAttribute(string elementName, Type type)
		{
			this.elementName = elementName;
			this.type = type;
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x060024F2 RID: 9458 RVA: 0x000ADE3E File Offset: 0x000ACE3E
		// (set) Token: 0x060024F3 RID: 9459 RVA: 0x000ADE46 File Offset: 0x000ACE46
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x000ADE4F File Offset: 0x000ACE4F
		// (set) Token: 0x060024F5 RID: 9461 RVA: 0x000ADE65 File Offset: 0x000ACE65
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

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x060024F6 RID: 9462 RVA: 0x000ADE6E File Offset: 0x000ACE6E
		// (set) Token: 0x060024F7 RID: 9463 RVA: 0x000ADE76 File Offset: 0x000ACE76
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

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x060024F8 RID: 9464 RVA: 0x000ADE7F File Offset: 0x000ACE7F
		// (set) Token: 0x060024F9 RID: 9465 RVA: 0x000ADE95 File Offset: 0x000ACE95
		public string DataType
		{
			get
			{
				if (this.dataType != null)
				{
					return this.dataType;
				}
				return string.Empty;
			}
			set
			{
				this.dataType = value;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x060024FA RID: 9466 RVA: 0x000ADE9E File Offset: 0x000ACE9E
		// (set) Token: 0x060024FB RID: 9467 RVA: 0x000ADEA6 File Offset: 0x000ACEA6
		public bool IsNullable
		{
			get
			{
				return this.nullable;
			}
			set
			{
				this.nullable = value;
				this.nullableSpecified = true;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x060024FC RID: 9468 RVA: 0x000ADEB6 File Offset: 0x000ACEB6
		internal bool IsNullableSpecified
		{
			get
			{
				return this.nullableSpecified;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x060024FD RID: 9469 RVA: 0x000ADEBE File Offset: 0x000ACEBE
		// (set) Token: 0x060024FE RID: 9470 RVA: 0x000ADEC6 File Offset: 0x000ACEC6
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

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x060024FF RID: 9471 RVA: 0x000ADECF File Offset: 0x000ACECF
		// (set) Token: 0x06002500 RID: 9472 RVA: 0x000ADED7 File Offset: 0x000ACED7
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

		// Token: 0x04001577 RID: 5495
		private string elementName;

		// Token: 0x04001578 RID: 5496
		private Type type;

		// Token: 0x04001579 RID: 5497
		private string ns;

		// Token: 0x0400157A RID: 5498
		private string dataType;

		// Token: 0x0400157B RID: 5499
		private bool nullable;

		// Token: 0x0400157C RID: 5500
		private bool nullableSpecified;

		// Token: 0x0400157D RID: 5501
		private XmlSchemaForm form;

		// Token: 0x0400157E RID: 5502
		private int order = -1;
	}
}
