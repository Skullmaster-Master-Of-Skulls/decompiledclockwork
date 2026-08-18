using System;
using System.Globalization;

namespace System.Xml.Schema
{
	// Token: 0x020001E8 RID: 488
	internal class TypedObject
	{
		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x000B1FEA File Offset: 0x000B01EA
		public int Dim
		{
			get
			{
				return this.dim;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x0600206D RID: 8301 RVA: 0x000B1FF2 File Offset: 0x000B01F2
		public bool IsList
		{
			get
			{
				return this.isList;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x0600206E RID: 8302 RVA: 0x000B1FFA File Offset: 0x000B01FA
		public bool IsDecimal
		{
			get
			{
				return this.dstruct.IsDecimal;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x0600206F RID: 8303 RVA: 0x000B2007 File Offset: 0x000B0207
		public decimal[] Dvalue
		{
			get
			{
				return this.dstruct.Dvalue;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06002070 RID: 8304 RVA: 0x000B2014 File Offset: 0x000B0214
		// (set) Token: 0x06002071 RID: 8305 RVA: 0x000B201C File Offset: 0x000B021C
		public object Value
		{
			get
			{
				return this.ovalue;
			}
			set
			{
				this.ovalue = value;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06002072 RID: 8306 RVA: 0x000B2025 File Offset: 0x000B0225
		// (set) Token: 0x06002073 RID: 8307 RVA: 0x000B202D File Offset: 0x000B022D
		public XmlSchemaDatatype Type
		{
			get
			{
				return this.xsdtype;
			}
			set
			{
				this.xsdtype = value;
			}
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x000B2038 File Offset: 0x000B0238
		public TypedObject(object obj, string svalue, XmlSchemaDatatype xsdtype)
		{
			this.ovalue = obj;
			this.svalue = svalue;
			this.xsdtype = xsdtype;
			if (xsdtype.Variety == XmlSchemaDatatypeVariety.List || xsdtype is Datatype_base64Binary || xsdtype is Datatype_hexBinary)
			{
				this.isList = true;
				this.dim = ((Array)obj).Length;
			}
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x000B2098 File Offset: 0x000B0298
		public override string ToString()
		{
			return this.svalue;
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x000B20A0 File Offset: 0x000B02A0
		public void SetDecimal()
		{
			if (this.dstruct != null)
			{
				return;
			}
			XmlTypeCode typeCode = this.xsdtype.TypeCode;
			if (typeCode == XmlTypeCode.Decimal || typeCode - XmlTypeCode.Integer <= 12)
			{
				if (this.isList)
				{
					this.dstruct = new TypedObject.DecimalStruct(this.dim);
					for (int i = 0; i < this.dim; i++)
					{
						this.dstruct.Dvalue[i] = Convert.ToDecimal(((Array)this.ovalue).GetValue(i), NumberFormatInfo.InvariantInfo);
					}
				}
				else
				{
					this.dstruct = new TypedObject.DecimalStruct();
					this.dstruct.Dvalue[0] = Convert.ToDecimal(this.ovalue, NumberFormatInfo.InvariantInfo);
				}
				this.dstruct.IsDecimal = true;
				return;
			}
			if (this.isList)
			{
				this.dstruct = new TypedObject.DecimalStruct(this.dim);
				return;
			}
			this.dstruct = new TypedObject.DecimalStruct();
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x000B2188 File Offset: 0x000B0388
		private bool ListDValueEquals(TypedObject other)
		{
			for (int i = 0; i < this.Dim; i++)
			{
				if (this.Dvalue[i] != other.Dvalue[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x000B21C8 File Offset: 0x000B03C8
		public bool Equals(TypedObject other)
		{
			if (this.Dim != other.Dim)
			{
				return false;
			}
			if (this.Type != other.Type)
			{
				if (!this.Type.IsComparable(other.Type))
				{
					return false;
				}
				other.SetDecimal();
				this.SetDecimal();
				if (this.IsDecimal && other.IsDecimal)
				{
					return this.ListDValueEquals(other);
				}
			}
			if (this.IsList)
			{
				if (other.IsList)
				{
					return this.Type.Compare(this.Value, other.Value) == 0;
				}
				Array array = this.Value as Array;
				XmlAtomicValue[] array2 = array as XmlAtomicValue[];
				if (array2 != null)
				{
					return array2.Length == 1 && array2.GetValue(0).Equals(other.Value);
				}
				return array.Length == 1 && array.GetValue(0).Equals(other.Value);
			}
			else
			{
				if (!other.IsList)
				{
					return this.Value.Equals(other.Value);
				}
				Array array3 = other.Value as Array;
				XmlAtomicValue[] array4 = array3 as XmlAtomicValue[];
				if (array4 != null)
				{
					return array4.Length == 1 && array4.GetValue(0).Equals(this.Value);
				}
				return array3.Length == 1 && array3.GetValue(0).Equals(this.Value);
			}
		}

		// Token: 0x04000DA4 RID: 3492
		private TypedObject.DecimalStruct dstruct;

		// Token: 0x04000DA5 RID: 3493
		private object ovalue;

		// Token: 0x04000DA6 RID: 3494
		private string svalue;

		// Token: 0x04000DA7 RID: 3495
		private XmlSchemaDatatype xsdtype;

		// Token: 0x04000DA8 RID: 3496
		private int dim = 1;

		// Token: 0x04000DA9 RID: 3497
		private bool isList;

		// Token: 0x0200048C RID: 1164
		private class DecimalStruct
		{
			// Token: 0x17000A67 RID: 2663
			// (get) Token: 0x0600311C RID: 12572 RVA: 0x0011E055 File Offset: 0x0011C255
			// (set) Token: 0x0600311D RID: 12573 RVA: 0x0011E05D File Offset: 0x0011C25D
			public bool IsDecimal
			{
				get
				{
					return this.isDecimal;
				}
				set
				{
					this.isDecimal = value;
				}
			}

			// Token: 0x17000A68 RID: 2664
			// (get) Token: 0x0600311E RID: 12574 RVA: 0x0011E066 File Offset: 0x0011C266
			public decimal[] Dvalue
			{
				get
				{
					return this.dvalue;
				}
			}

			// Token: 0x0600311F RID: 12575 RVA: 0x0011E06E File Offset: 0x0011C26E
			public DecimalStruct()
			{
				this.dvalue = new decimal[1];
			}

			// Token: 0x06003120 RID: 12576 RVA: 0x0011E082 File Offset: 0x0011C282
			public DecimalStruct(int dim)
			{
				this.dvalue = new decimal[dim];
			}

			// Token: 0x04001E11 RID: 7697
			private bool isDecimal;

			// Token: 0x04001E12 RID: 7698
			private decimal[] dvalue;
		}
	}
}
