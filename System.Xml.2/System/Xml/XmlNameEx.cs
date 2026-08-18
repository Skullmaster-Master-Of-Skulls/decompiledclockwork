using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x02000113 RID: 275
	internal sealed class XmlNameEx : XmlName
	{
		// Token: 0x0600132C RID: 4908 RVA: 0x0004FFE4 File Offset: 0x0004E1E4
		internal XmlNameEx(string prefix, string localName, string ns, int hashCode, XmlDocument ownerDoc, XmlName next, IXmlSchemaInfo schemaInfo) : base(prefix, localName, ns, hashCode, ownerDoc, next)
		{
			this.SetValidity(schemaInfo.Validity);
			this.SetIsDefault(schemaInfo.IsDefault);
			this.SetIsNil(schemaInfo.IsNil);
			this.memberType = schemaInfo.MemberType;
			this.schemaType = schemaInfo.SchemaType;
			this.decl = ((schemaInfo.SchemaElement != null) ? schemaInfo.SchemaElement : schemaInfo.SchemaAttribute);
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x00050060 File Offset: 0x0004E260
		public override XmlSchemaValidity Validity
		{
			get
			{
				if (!this.ownerDoc.CanReportValidity)
				{
					return XmlSchemaValidity.NotKnown;
				}
				return (XmlSchemaValidity)(this.flags & 3);
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x00050079 File Offset: 0x0004E279
		public override bool IsDefault
		{
			get
			{
				return (this.flags & 4) > 0;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x00050086 File Offset: 0x0004E286
		public override bool IsNil
		{
			get
			{
				return (this.flags & 8) > 0;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x00050093 File Offset: 0x0004E293
		public override XmlSchemaSimpleType MemberType
		{
			get
			{
				return this.memberType;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x0005009B File Offset: 0x0004E29B
		public override XmlSchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x000500A3 File Offset: 0x0004E2A3
		public override XmlSchemaElement SchemaElement
		{
			get
			{
				return this.decl as XmlSchemaElement;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x000500B0 File Offset: 0x0004E2B0
		public override XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				return this.decl as XmlSchemaAttribute;
			}
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x000500BD File Offset: 0x0004E2BD
		public void SetValidity(XmlSchemaValidity value)
		{
			this.flags = (byte)(((int)this.flags & -4) | (int)((byte)value));
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x000500D2 File Offset: 0x0004E2D2
		public void SetIsDefault(bool value)
		{
			if (value)
			{
				this.flags |= 4;
				return;
			}
			this.flags = (byte)((int)this.flags & -5);
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x000500F7 File Offset: 0x0004E2F7
		public void SetIsNil(bool value)
		{
			if (value)
			{
				this.flags |= 8;
				return;
			}
			this.flags = (byte)((int)this.flags & -9);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0005011C File Offset: 0x0004E31C
		public override bool Equals(IXmlSchemaInfo schemaInfo)
		{
			return schemaInfo != null && schemaInfo.Validity == (XmlSchemaValidity)(this.flags & 3) && schemaInfo.IsDefault == (this.flags & 4) > 0 && schemaInfo.IsNil == (this.flags & 8) > 0 && schemaInfo.MemberType == this.memberType && schemaInfo.SchemaType == this.schemaType && schemaInfo.SchemaElement == this.decl as XmlSchemaElement && schemaInfo.SchemaAttribute == this.decl as XmlSchemaAttribute;
		}

		// Token: 0x04000555 RID: 1365
		private byte flags;

		// Token: 0x04000556 RID: 1366
		private XmlSchemaSimpleType memberType;

		// Token: 0x04000557 RID: 1367
		private XmlSchemaType schemaType;

		// Token: 0x04000558 RID: 1368
		private object decl;

		// Token: 0x04000559 RID: 1369
		private const byte ValidityMask = 3;

		// Token: 0x0400055A RID: 1370
		private const byte IsDefaultBit = 4;

		// Token: 0x0400055B RID: 1371
		private const byte IsNilBit = 8;
	}
}
