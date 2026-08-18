using System;
using System.Globalization;

namespace System.Xml.Schema
{
	// Token: 0x0200018F RID: 399
	internal class TypedObject
	{
		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x0005E2CE File Offset: 0x0005D2CE
		public int Dim
		{
			get
			{
				return this.dim;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x0005E2D6 File Offset: 0x0005D2D6
		public bool IsList
		{
			get
			{
				return this.isList;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x0005E2DE File Offset: 0x0005D2DE
		public bool IsDecimal
		{
			get
			{
				return this.dstruct.IsDecimal;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x0005E2EB File Offset: 0x0005D2EB
		public decimal[] Dvalue
		{
			get
			{
				return this.dstruct.Dvalue;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x0005E2F8 File Offset: 0x0005D2F8
		// (set) Token: 0x06001521 RID: 5409 RVA: 0x0005E300 File Offset: 0x0005D300
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

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x0005E309 File Offset: 0x0005D309
		// (set) Token: 0x06001523 RID: 5411 RVA: 0x0005E311 File Offset: 0x0005D311
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

		// Token: 0x06001524 RID: 5412 RVA: 0x0005E31C File Offset: 0x0005D31C
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

		// Token: 0x06001525 RID: 5413 RVA: 0x0005E37C File Offset: 0x0005D37C
		public override string ToString()
		{
			return this.svalue;
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0005E384 File Offset: 0x0005D384
		public void SetDecimal()
		{
			if (this.dstruct != null)
			{
				return;
			}
			XmlTypeCode typeCode = this.xsdtype.TypeCode;
			if (typeCode != XmlTypeCode.Decimal)
			{
				switch (typeCode)
				{
				case XmlTypeCode.Integer:
				case XmlTypeCode.NonPositiveInteger:
				case XmlTypeCode.NegativeInteger:
				case XmlTypeCode.Long:
				case XmlTypeCode.Int:
				case XmlTypeCode.Short:
				case XmlTypeCode.Byte:
				case XmlTypeCode.NonNegativeInteger:
				case XmlTypeCode.UnsignedLong:
				case XmlTypeCode.UnsignedInt:
				case XmlTypeCode.UnsignedShort:
				case XmlTypeCode.UnsignedByte:
				case XmlTypeCode.PositiveInteger:
					break;
				default:
					if (this.isList)
					{
						this.dstruct = new TypedObject.DecimalStruct(this.dim);
						return;
					}
					this.dstruct = new TypedObject.DecimalStruct();
					return;
				}
			}
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
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0005E4B0 File Offset: 0x0005D4B0
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

		// Token: 0x06001528 RID: 5416 RVA: 0x0005E4FC File Offset: 0x0005D4FC
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

		// Token: 0x04000CAD RID: 3245
		private TypedObject.DecimalStruct dstruct;

		// Token: 0x04000CAE RID: 3246
		private object ovalue;

		// Token: 0x04000CAF RID: 3247
		private string svalue;

		// Token: 0x04000CB0 RID: 3248
		private XmlSchemaDatatype xsdtype;

		// Token: 0x04000CB1 RID: 3249
		private int dim = 1;

		// Token: 0x04000CB2 RID: 3250
		private bool isList;

		// Token: 0x02000190 RID: 400
		private class DecimalStruct
		{
			// Token: 0x1700050F RID: 1295
			// (get) Token: 0x06001529 RID: 5417 RVA: 0x0005E642 File Offset: 0x0005D642
			// (set) Token: 0x0600152A RID: 5418 RVA: 0x0005E64A File Offset: 0x0005D64A
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

			// Token: 0x17000510 RID: 1296
			// (get) Token: 0x0600152B RID: 5419 RVA: 0x0005E653 File Offset: 0x0005D653
			public decimal[] Dvalue
			{
				get
				{
					return this.dvalue;
				}
			}

			// Token: 0x0600152C RID: 5420 RVA: 0x0005E65B File Offset: 0x0005D65B
			public DecimalStruct()
			{
				this.dvalue = new decimal[1];
			}

			// Token: 0x0600152D RID: 5421 RVA: 0x0005E66F File Offset: 0x0005D66F
			public DecimalStruct(int dim)
			{
				this.dvalue = new decimal[dim];
			}

			// Token: 0x04000CB3 RID: 3251
			private bool isDecimal;

			// Token: 0x04000CB4 RID: 3252
			private decimal[] dvalue;
		}
	}
}
