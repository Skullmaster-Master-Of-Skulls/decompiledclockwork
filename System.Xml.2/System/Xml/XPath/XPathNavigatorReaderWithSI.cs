using System;
using System.Xml.Schema;

namespace System.Xml.XPath
{
	// Token: 0x020002ED RID: 749
	internal class XPathNavigatorReaderWithSI : XPathNavigatorReader, IXmlSchemaInfo
	{
		// Token: 0x06002D19 RID: 11545 RVA: 0x000EBF61 File Offset: 0x000EA161
		internal XPathNavigatorReaderWithSI(XPathNavigator navToRead, IXmlLineInfo xli, IXmlSchemaInfo xsi) : base(navToRead, xli, xsi)
		{
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06002D1A RID: 11546 RVA: 0x000EBF6C File Offset: 0x000EA16C
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

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06002D1B RID: 11547 RVA: 0x000EBF83 File Offset: 0x000EA183
		public override bool IsDefault
		{
			get
			{
				return base.IsReading && this.schemaInfo.IsDefault;
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06002D1C RID: 11548 RVA: 0x000EBF9A File Offset: 0x000EA19A
		public virtual bool IsNil
		{
			get
			{
				return base.IsReading && this.schemaInfo.IsNil;
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06002D1D RID: 11549 RVA: 0x000EBFB1 File Offset: 0x000EA1B1
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

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06002D1E RID: 11550 RVA: 0x000EBFC8 File Offset: 0x000EA1C8
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

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06002D1F RID: 11551 RVA: 0x000EBFDF File Offset: 0x000EA1DF
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

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06002D20 RID: 11552 RVA: 0x000EBFF6 File Offset: 0x000EA1F6
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
