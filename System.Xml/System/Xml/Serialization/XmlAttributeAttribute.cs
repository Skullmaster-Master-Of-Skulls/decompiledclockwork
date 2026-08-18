using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000303 RID: 771
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public class XmlAttributeAttribute : Attribute
	{
		// Token: 0x06002411 RID: 9233 RVA: 0x000AA5F8 File Offset: 0x000A95F8
		public XmlAttributeAttribute()
		{
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x000AA600 File Offset: 0x000A9600
		public XmlAttributeAttribute(string attributeName)
		{
			this.attributeName = attributeName;
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x000AA60F File Offset: 0x000A960F
		public XmlAttributeAttribute(Type type)
		{
			this.type = type;
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x000AA61E File Offset: 0x000A961E
		public XmlAttributeAttribute(string attributeName, Type type)
		{
			this.attributeName = attributeName;
			this.type = type;
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06002415 RID: 9237 RVA: 0x000AA634 File Offset: 0x000A9634
		// (set) Token: 0x06002416 RID: 9238 RVA: 0x000AA63C File Offset: 0x000A963C
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

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06002417 RID: 9239 RVA: 0x000AA645 File Offset: 0x000A9645
		// (set) Token: 0x06002418 RID: 9240 RVA: 0x000AA65B File Offset: 0x000A965B
		public string AttributeName
		{
			get
			{
				if (this.attributeName != null)
				{
					return this.attributeName;
				}
				return string.Empty;
			}
			set
			{
				this.attributeName = value;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06002419 RID: 9241 RVA: 0x000AA664 File Offset: 0x000A9664
		// (set) Token: 0x0600241A RID: 9242 RVA: 0x000AA66C File Offset: 0x000A966C
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

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x0600241B RID: 9243 RVA: 0x000AA675 File Offset: 0x000A9675
		// (set) Token: 0x0600241C RID: 9244 RVA: 0x000AA68B File Offset: 0x000A968B
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

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x0600241D RID: 9245 RVA: 0x000AA694 File Offset: 0x000A9694
		// (set) Token: 0x0600241E RID: 9246 RVA: 0x000AA69C File Offset: 0x000A969C
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

		// Token: 0x0400154E RID: 5454
		private string attributeName;

		// Token: 0x0400154F RID: 5455
		private Type type;

		// Token: 0x04001550 RID: 5456
		private string ns;

		// Token: 0x04001551 RID: 5457
		private string dataType;

		// Token: 0x04001552 RID: 5458
		private XmlSchemaForm form;
	}
}
