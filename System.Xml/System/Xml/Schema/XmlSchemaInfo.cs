using System;

namespace System.Xml.Schema
{
	// Token: 0x02000267 RID: 615
	public class XmlSchemaInfo : IXmlSchemaInfo
	{
		// Token: 0x06001C8F RID: 7311 RVA: 0x00083390 File Offset: 0x00082390
		public XmlSchemaInfo()
		{
			this.Clear();
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x0008339E File Offset: 0x0008239E
		internal XmlSchemaInfo(XmlSchemaValidity validity) : this()
		{
			this.validity = validity;
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001C91 RID: 7313 RVA: 0x000833AD File Offset: 0x000823AD
		// (set) Token: 0x06001C92 RID: 7314 RVA: 0x000833B5 File Offset: 0x000823B5
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

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001C93 RID: 7315 RVA: 0x000833BE File Offset: 0x000823BE
		// (set) Token: 0x06001C94 RID: 7316 RVA: 0x000833C6 File Offset: 0x000823C6
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

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001C95 RID: 7317 RVA: 0x000833CF File Offset: 0x000823CF
		// (set) Token: 0x06001C96 RID: 7318 RVA: 0x000833D7 File Offset: 0x000823D7
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

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001C97 RID: 7319 RVA: 0x000833E0 File Offset: 0x000823E0
		// (set) Token: 0x06001C98 RID: 7320 RVA: 0x000833E8 File Offset: 0x000823E8
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

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001C99 RID: 7321 RVA: 0x000833F1 File Offset: 0x000823F1
		// (set) Token: 0x06001C9A RID: 7322 RVA: 0x000833F9 File Offset: 0x000823F9
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

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001C9B RID: 7323 RVA: 0x00083423 File Offset: 0x00082423
		// (set) Token: 0x06001C9C RID: 7324 RVA: 0x0008342B File Offset: 0x0008242B
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

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001C9D RID: 7325 RVA: 0x0008343E File Offset: 0x0008243E
		// (set) Token: 0x06001C9E RID: 7326 RVA: 0x00083446 File Offset: 0x00082446
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

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001C9F RID: 7327 RVA: 0x00083459 File Offset: 0x00082459
		// (set) Token: 0x06001CA0 RID: 7328 RVA: 0x00083461 File Offset: 0x00082461
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

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x0008346A File Offset: 0x0008246A
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

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x00083481 File Offset: 0x00082481
		internal bool HasDefaultValue
		{
			get
			{
				return this.schemaElement != null && this.schemaElement.ElementDecl.DefaultValueTyped != null;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x000834A3 File Offset: 0x000824A3
		internal bool IsUnionType
		{
			get
			{
				return this.schemaType != null && this.schemaType.Datatype != null && this.schemaType.Datatype.Variety == XmlSchemaDatatypeVariety.Union;
			}
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x000834CF File Offset: 0x000824CF
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

		// Token: 0x04001196 RID: 4502
		private bool isDefault;

		// Token: 0x04001197 RID: 4503
		private bool isNil;

		// Token: 0x04001198 RID: 4504
		private XmlSchemaElement schemaElement;

		// Token: 0x04001199 RID: 4505
		private XmlSchemaAttribute schemaAttribute;

		// Token: 0x0400119A RID: 4506
		private XmlSchemaType schemaType;

		// Token: 0x0400119B RID: 4507
		private XmlSchemaSimpleType memberType;

		// Token: 0x0400119C RID: 4508
		private XmlSchemaValidity validity;

		// Token: 0x0400119D RID: 4509
		private XmlSchemaContentType contentType;
	}
}
