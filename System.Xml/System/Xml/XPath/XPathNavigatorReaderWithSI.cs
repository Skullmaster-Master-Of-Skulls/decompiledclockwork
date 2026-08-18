using System;
using System.Xml.Schema;

namespace System.Xml.XPath
{
	// Token: 0x0200011C RID: 284
	internal class XPathNavigatorReaderWithSI : XPathNavigatorReader, IXmlSchemaInfo
	{
		// Token: 0x06001101 RID: 4353 RVA: 0x0004D302 File Offset: 0x0004C302
		internal XPathNavigatorReaderWithSI(XPathNavigator navToRead, IXmlLineInfo xli, IXmlSchemaInfo xsi) : base(navToRead, xli, xsi)
		{
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001102 RID: 4354 RVA: 0x0004D30D File Offset: 0x0004C30D
		public virtual XmlSchemaValidity Validity
		{
			get
			{
				if (!base.IsReading)
				{
					return XmlSchemaValidity.NotKnown;
				}
				return this.schemaInfo.Validity;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x0004D324 File Offset: 0x0004C324
		public override bool IsDefault
		{
			get
			{
				return base.IsReading && this.schemaInfo.IsDefault;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x0004D33B File Offset: 0x0004C33B
		public virtual bool IsNil
		{
			get
			{
				return base.IsReading && this.schemaInfo.IsNil;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x0004D352 File Offset: 0x0004C352
		public virtual XmlSchemaSimpleType MemberType
		{
			get
			{
				if (!base.IsReading)
				{
					return null;
				}
				return this.schemaInfo.MemberType;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x0004D369 File Offset: 0x0004C369
		public virtual XmlSchemaType SchemaType
		{
			get
			{
				if (!base.IsReading)
				{
					return null;
				}
				return this.schemaInfo.SchemaType;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x0004D380 File Offset: 0x0004C380
		public virtual XmlSchemaElement SchemaElement
		{
			get
			{
				if (!base.IsReading)
				{
					return null;
				}
				return this.schemaInfo.SchemaElement;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x0004D397 File Offset: 0x0004C397
		public virtual XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				if (!base.IsReading)
				{
					return null;
				}
				return this.schemaInfo.SchemaAttribute;
			}
		}
	}
}
