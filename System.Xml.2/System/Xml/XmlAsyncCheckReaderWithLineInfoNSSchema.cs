using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000C8 RID: 200
	internal class XmlAsyncCheckReaderWithLineInfoNSSchema : XmlAsyncCheckReaderWithLineInfoNS, IXmlSchemaInfo
	{
		// Token: 0x0600075B RID: 1883 RVA: 0x0001890D File Offset: 0x00016B0D
		public XmlAsyncCheckReaderWithLineInfoNSSchema(XmlReader reader) : base(reader)
		{
			this.readerAsIXmlSchemaInfo = (IXmlSchemaInfo)reader;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x00018922 File Offset: 0x00016B22
		XmlSchemaValidity IXmlSchemaInfo.Validity
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.Validity;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0001892F File Offset: 0x00016B2F
		bool IXmlSchemaInfo.IsDefault
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.IsDefault;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0001893C File Offset: 0x00016B3C
		bool IXmlSchemaInfo.IsNil
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.IsNil;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00018949 File Offset: 0x00016B49
		XmlSchemaSimpleType IXmlSchemaInfo.MemberType
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.MemberType;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x00018956 File Offset: 0x00016B56
		XmlSchemaType IXmlSchemaInfo.SchemaType
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.SchemaType;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00018963 File Offset: 0x00016B63
		XmlSchemaElement IXmlSchemaInfo.SchemaElement
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.SchemaElement;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x00018970 File Offset: 0x00016B70
		XmlSchemaAttribute IXmlSchemaInfo.SchemaAttribute
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.SchemaAttribute;
			}
		}

		// Token: 0x040002DF RID: 735
		private readonly IXmlSchemaInfo readerAsIXmlSchemaInfo;
	}
}
