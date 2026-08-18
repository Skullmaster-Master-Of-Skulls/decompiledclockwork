using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000E5 RID: 229
	internal sealed class XmlNameEx : XmlName
	{
		// Token: 0x06000DF3 RID: 3571 RVA: 0x0003E068 File Offset: 0x0003D068
		internal XmlNameEx(string prefix, string localName, string ns, int hashCode, XmlDocument ownerDoc, XmlName next, IXmlSchemaInfo schemaInfo) : base(prefix, localName, ns, hashCode, ownerDoc, next)
		{
			this.SetValidity(schemaInfo.Validity);
			this.SetIsDefault(schemaInfo.IsDefault);
			this.SetIsNil(schemaInfo.IsNil);
			this.memberType = schemaInfo.MemberType;
			this.schemaType = schemaInfo.SchemaType;
			this.decl = ((schemaInfo.SchemaElement != null) ? schemaInfo.SchemaElement : schemaInfo.SchemaAttribute);
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x0003E0E4 File Offset: 0x0003D0E4
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

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x0003E0FD File Offset: 0x0003D0FD
		public override bool IsDefault
		{
			get
			{
				return (this.flags & 4) != 0;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x0003E10D File Offset: 0x0003D10D
		public override bool IsNil
		{
			get
			{
				return (this.flags & 8) != 0;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x0003E11D File Offset: 0x0003D11D
		public override XmlSchemaSimpleType MemberType
		{
			get
			{
				return this.memberType;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0003E125 File Offset: 0x0003D125
		public override XmlSchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x0003E12D File Offset: 0x0003D12D
		public override XmlSchemaElement SchemaElement
		{
			get
			{
				return this.decl as XmlSchemaElement;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x0003E13A File Offset: 0x0003D13A
		public override XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				return this.decl as XmlSchemaAttribute;
			}
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0003E147 File Offset: 0x0003D147
		public void SetValidity(XmlSchemaValidity value)
		{
			this.flags = (byte)(((int)this.flags & -4) | (int)((byte)value));
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0003E15C File Offset: 0x0003D15C
		public void SetIsDefault(bool value)
		{
			if (value)
			{
				this.flags |= 4;
				return;
			}
			this.flags = (byte)((int)this.flags & -5);
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0003E181 File Offset: 0x0003D181
		public void SetIsNil(bool value)
		{
			if (value)
			{
				this.flags |= 8;
				return;
			}
			this.flags = (byte)((int)this.flags & -9);
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0003E1A8 File Offset: 0x0003D1A8
		public override bool Equals(IXmlSchemaInfo schemaInfo)
		{
			return schemaInfo != null && schemaInfo.Validity == (XmlSchemaValidity)(this.flags & 3) && schemaInfo.IsDefault == ((this.flags & 4) != 0) && schemaInfo.IsNil == ((this.flags & 8) != 0) && schemaInfo.MemberType == this.memberType && schemaInfo.SchemaType == this.schemaType && schemaInfo.SchemaElement == this.decl as XmlSchemaElement && schemaInfo.SchemaAttribute == this.decl as XmlSchemaAttribute;
		}

		// Token: 0x04000974 RID: 2420
		private const byte ValidityMask = 3;

		// Token: 0x04000975 RID: 2421
		private const byte IsDefaultBit = 4;

		// Token: 0x04000976 RID: 2422
		private const byte IsNilBit = 8;

		// Token: 0x04000977 RID: 2423
		private byte flags;

		// Token: 0x04000978 RID: 2424
		private XmlSchemaSimpleType memberType;

		// Token: 0x04000979 RID: 2425
		private XmlSchemaType schemaType;

		// Token: 0x0400097A RID: 2426
		private object decl;
	}
}
