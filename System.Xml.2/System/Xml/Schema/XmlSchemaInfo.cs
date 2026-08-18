using System;

namespace System.Xml.Schema
{
	// Token: 0x020002A0 RID: 672
	public class XmlSchemaInfo : IXmlSchemaInfo
	{
		// Token: 0x0600271A RID: 10010 RVA: 0x000CF4CA File Offset: 0x000CD6CA
		public XmlSchemaInfo()
		{
			this.Clear();
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x000CF4D8 File Offset: 0x000CD6D8
		internal XmlSchemaInfo(XmlSchemaValidity validity) : this()
		{
			this.validity = validity;
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x0600271C RID: 10012 RVA: 0x000CF4E7 File Offset: 0x000CD6E7
		// (set) Token: 0x0600271D RID: 10013 RVA: 0x000CF4EF File Offset: 0x000CD6EF
		public XmlSchemaValidity Validity
		{
			get
			{
				return this.validity;
			}
			set
			{
				this.validity = value;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x0600271E RID: 10014 RVA: 0x000CF4F8 File Offset: 0x000CD6F8
		// (set) Token: 0x0600271F RID: 10015 RVA: 0x000CF500 File Offset: 0x000CD700
		public bool IsDefault
		{
			get
			{
				return this.isDefault;
			}
			set
			{
				this.isDefault = value;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06002720 RID: 10016 RVA: 0x000CF509 File Offset: 0x000CD709
		// (set) Token: 0x06002721 RID: 10017 RVA: 0x000CF511 File Offset: 0x000CD711
		public bool IsNil
		{
			get
			{
				return this.isNil;
			}
			set
			{
				this.isNil = value;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06002722 RID: 10018 RVA: 0x000CF51A File Offset: 0x000CD71A
		// (set) Token: 0x06002723 RID: 10019 RVA: 0x000CF522 File Offset: 0x000CD722
		public XmlSchemaSimpleType MemberType
		{
			get
			{
				return this.memberType;
			}
			set
			{
				this.memberType = value;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06002724 RID: 10020 RVA: 0x000CF52B File Offset: 0x000CD72B
		// (set) Token: 0x06002725 RID: 10021 RVA: 0x000CF533 File Offset: 0x000CD733
		public XmlSchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
			set
			{
				this.schemaType = value;
				if (this.schemaType != null)
				{
					this.contentType = this.schemaType.SchemaContentType;
					return;
				}
				this.contentType = XmlSchemaContentType.Empty;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06002726 RID: 10022 RVA: 0x000CF55D File Offset: 0x000CD75D
		// (set) Token: 0x06002727 RID: 10023 RVA: 0x000CF565 File Offset: 0x000CD765
		public XmlSchemaElement SchemaElement
		{
			get
			{
				return this.schemaElement;
			}
			set
			{
				this.schemaElement = value;
				if (value != null)
				{
					this.schemaAttribute = null;
				}
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06002728 RID: 10024 RVA: 0x000CF578 File Offset: 0x000CD778
		// (set) Token: 0x06002729 RID: 10025 RVA: 0x000CF580 File Offset: 0x000CD780
		public XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				return this.schemaAttribute;
			}
			set
			{
				this.schemaAttribute = value;
				if (value != null)
				{
					this.schemaElement = null;
				}
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x0600272A RID: 10026 RVA: 0x000CF593 File Offset: 0x000CD793
		// (set) Token: 0x0600272B RID: 10027 RVA: 0x000CF59B File Offset: 0x000CD79B
		public XmlSchemaContentType ContentType
		{
			get
			{
				return this.contentType;
			}
			set
			{
				this.contentType = value;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x0600272C RID: 10028 RVA: 0x000CF5A4 File Offset: 0x000CD7A4
		internal XmlSchemaType XmlType
		{
			get
			{
				if (this.memberType != null)
				{
					return this.memberType;
				}
				return this.schemaType;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x0600272D RID: 10029 RVA: 0x000CF5BB File Offset: 0x000CD7BB
		internal bool HasDefaultValue
		{
			get
			{
				return this.schemaElement != null && this.schemaElement.ElementDecl.DefaultValueTyped != null;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x0600272E RID: 10030 RVA: 0x000CF5DA File Offset: 0x000CD7DA
		internal bool IsUnionType
		{
			get
			{
				return this.schemaType != null && this.schemaType.Datatype != null && this.schemaType.Datatype.Variety == XmlSchemaDatatypeVariety.Union;
			}
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x000CF606 File Offset: 0x000CD806
		internal void Clear()
		{
			this.isNil = false;
			this.isDefault = false;
			this.schemaType = null;
			this.schemaElement = null;
			this.schemaAttribute = null;
			this.memberType = null;
			this.validity = XmlSchemaValidity.NotKnown;
			this.contentType = XmlSchemaContentType.Empty;
		}

		// Token: 0x04001112 RID: 4370
		private bool isDefault;

		// Token: 0x04001113 RID: 4371
		private bool isNil;

		// Token: 0x04001114 RID: 4372
		private XmlSchemaElement schemaElement;

		// Token: 0x04001115 RID: 4373
		private XmlSchemaAttribute schemaAttribute;

		// Token: 0x04001116 RID: 4374
		private XmlSchemaType schemaType;

		// Token: 0x04001117 RID: 4375
		private XmlSchemaSimpleType memberType;

		// Token: 0x04001118 RID: 4376
		private XmlSchemaValidity validity;

		// Token: 0x04001119 RID: 4377
		private XmlSchemaContentType contentType;
	}
}
