using System;
using System.Collections;
using System.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x020002C1 RID: 705
	internal abstract class XmlBaseConverter : XmlValueConverter
	{
		// Token: 0x06002911 RID: 10513 RVA: 0x000D659C File Offset: 0x000D479C
		protected XmlBaseConverter(XmlSchemaType schemaType)
		{
			XmlSchemaDatatype datatype = schemaType.Datatype;
			while (schemaType != null && !(schemaType is XmlSchemaSimpleType))
			{
				schemaType = schemaType.BaseXmlSchemaType;
			}
			if (schemaType == null)
			{
				schemaType = XmlSchemaType.GetBuiltInSimpleType(datatype.TypeCode);
			}
			this.schemaType = schemaType;
			this.typeCode = schemaType.TypeCode;
			this.clrTypeDefault = schemaType.Datatype.ValueType;
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x000D6600 File Offset: 0x000D4800
		protected XmlBaseConverter(XmlTypeCode typeCode)
		{
			if (typeCode != XmlTypeCode.Item)
			{
				if (typeCode != XmlTypeCode.Node)
				{
					if (typeCode == XmlTypeCode.AnyAtomicType)
					{
						this.clrTypeDefault = XmlBaseConverter.XmlAtomicValueType;
					}
				}
				else
				{
					this.clrTypeDefault = XmlBaseConverter.XPathNavigatorType;
				}
			}
			else
			{
				this.clrTypeDefault = XmlBaseConverter.XPathItemType;
			}
			this.typeCode = typeCode;
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x000D664E File Offset: 0x000D484E
		protected XmlBaseConverter(XmlBaseConverter converterAtomic)
		{
			this.schemaType = converterAtomic.schemaType;
			this.typeCode = converterAtomic.typeCode;
			this.clrTypeDefault = Array.CreateInstance(converterAtomic.DefaultClrType, 0).GetType();
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x000D6685 File Offset: 0x000D4885
		protected XmlBaseConverter(XmlBaseConverter converterAtomic, Type clrTypeDefault)
		{
			this.schemaType = converterAtomic.schemaType;
			this.typeCode = converterAtomic.typeCode;
			this.clrTypeDefault = clrTypeDefault;
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x000D66AC File Offset: 0x000D48AC
		public override bool ToBoolean(bool value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x000D66C5 File Offset: 0x000D48C5
		public override bool ToBoolean(DateTime value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x000D66DE File Offset: 0x000D48DE
		public override bool ToBoolean(DateTimeOffset value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x000D66F7 File Offset: 0x000D48F7
		public override bool ToBoolean(decimal value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x000D6710 File Offset: 0x000D4910
		public override bool ToBoolean(double value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x000D6729 File Offset: 0x000D4929
		public override bool ToBoolean(int value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x000D6742 File Offset: 0x000D4942
		public override bool ToBoolean(long value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x000D675B File Offset: 0x000D495B
		public override bool ToBoolean(float value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x000D6774 File Offset: 0x000D4974
		public override bool ToBoolean(string value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x000D6788 File Offset: 0x000D4988
		public override bool ToBoolean(object value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x000D679C File Offset: 0x000D499C
		public override DateTime ToDateTime(bool value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x000D67B5 File Offset: 0x000D49B5
		public override DateTime ToDateTime(DateTime value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x000D67CE File Offset: 0x000D49CE
		public override DateTime ToDateTime(DateTimeOffset value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x000D67E7 File Offset: 0x000D49E7
		public override DateTime ToDateTime(decimal value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x000D6800 File Offset: 0x000D4A00
		public override DateTime ToDateTime(double value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x000D6819 File Offset: 0x000D4A19
		public override DateTime ToDateTime(int value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x000D6832 File Offset: 0x000D4A32
		public override DateTime ToDateTime(long value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002926 RID: 10534 RVA: 0x000D684B File Offset: 0x000D4A4B
		public override DateTime ToDateTime(float value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x000D6864 File Offset: 0x000D4A64
		public override DateTime ToDateTime(string value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x000D6878 File Offset: 0x000D4A78
		public override DateTime ToDateTime(object value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x000D688C File Offset: 0x000D4A8C
		public override DateTimeOffset ToDateTimeOffset(bool value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x000D68A5 File Offset: 0x000D4AA5
		public override DateTimeOffset ToDateTimeOffset(DateTime value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x000D68BE File Offset: 0x000D4ABE
		public override DateTimeOffset ToDateTimeOffset(DateTimeOffset value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x000D68D7 File Offset: 0x000D4AD7
		public override DateTimeOffset ToDateTimeOffset(decimal value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x000D68F0 File Offset: 0x000D4AF0
		public override DateTimeOffset ToDateTimeOffset(double value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x000D6909 File Offset: 0x000D4B09
		public override DateTimeOffset ToDateTimeOffset(int value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x000D6922 File Offset: 0x000D4B22
		public override DateTimeOffset ToDateTimeOffset(long value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x000D693B File Offset: 0x000D4B3B
		public override DateTimeOffset ToDateTimeOffset(float value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x000D6954 File Offset: 0x000D4B54
		public override DateTimeOffset ToDateTimeOffset(string value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x000D6968 File Offset: 0x000D4B68
		public override DateTimeOffset ToDateTimeOffset(object value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x000D697C File Offset: 0x000D4B7C
		public override decimal ToDecimal(bool value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x000D6995 File Offset: 0x000D4B95
		public override decimal ToDecimal(DateTime value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x000D69AE File Offset: 0x000D4BAE
		public override decimal ToDecimal(DateTimeOffset value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x000D69C7 File Offset: 0x000D4BC7
		public override decimal ToDecimal(decimal value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x000D69E0 File Offset: 0x000D4BE0
		public override decimal ToDecimal(double value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x000D69F9 File Offset: 0x000D4BF9
		public override decimal ToDecimal(int value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x000D6A12 File Offset: 0x000D4C12
		public override decimal ToDecimal(long value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x000D6A2B File Offset: 0x000D4C2B
		public override decimal ToDecimal(float value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x000D6A44 File Offset: 0x000D4C44
		public override decimal ToDecimal(string value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x000D6A58 File Offset: 0x000D4C58
		public override decimal ToDecimal(object value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x000D6A6C File Offset: 0x000D4C6C
		public override double ToDouble(bool value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x000D6A85 File Offset: 0x000D4C85
		public override double ToDouble(DateTime value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x000D6A9E File Offset: 0x000D4C9E
		public override double ToDouble(DateTimeOffset value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x000D6AB7 File Offset: 0x000D4CB7
		public override double ToDouble(decimal value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x000D6AD0 File Offset: 0x000D4CD0
		public override double ToDouble(double value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x000D6AE9 File Offset: 0x000D4CE9
		public override double ToDouble(int value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x000D6B02 File Offset: 0x000D4D02
		public override double ToDouble(long value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x000D6B1B File Offset: 0x000D4D1B
		public override double ToDouble(float value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x000D6B34 File Offset: 0x000D4D34
		public override double ToDouble(string value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x000D6B48 File Offset: 0x000D4D48
		public override double ToDouble(object value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x000D6B5C File Offset: 0x000D4D5C
		public override int ToInt32(bool value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x000D6B75 File Offset: 0x000D4D75
		public override int ToInt32(DateTime value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x000D6B8E File Offset: 0x000D4D8E
		public override int ToInt32(DateTimeOffset value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x000D6BA7 File Offset: 0x000D4DA7
		public override int ToInt32(decimal value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x000D6BC0 File Offset: 0x000D4DC0
		public override int ToInt32(double value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x000D6BD9 File Offset: 0x000D4DD9
		public override int ToInt32(int value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x000D6BF2 File Offset: 0x000D4DF2
		public override int ToInt32(long value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x000D6C0B File Offset: 0x000D4E0B
		public override int ToInt32(float value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x000D6C24 File Offset: 0x000D4E24
		public override int ToInt32(string value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x000D6C38 File Offset: 0x000D4E38
		public override int ToInt32(object value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x000D6C4C File Offset: 0x000D4E4C
		public override long ToInt64(bool value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x000D6C65 File Offset: 0x000D4E65
		public override long ToInt64(DateTime value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x000D6C7E File Offset: 0x000D4E7E
		public override long ToInt64(DateTimeOffset value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x000D6C97 File Offset: 0x000D4E97
		public override long ToInt64(decimal value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x000D6CB0 File Offset: 0x000D4EB0
		public override long ToInt64(double value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x000D6CC9 File Offset: 0x000D4EC9
		public override long ToInt64(int value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x000D6CE2 File Offset: 0x000D4EE2
		public override long ToInt64(long value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x000D6CFB File Offset: 0x000D4EFB
		public override long ToInt64(float value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x000D6D14 File Offset: 0x000D4F14
		public override long ToInt64(string value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x000D6D28 File Offset: 0x000D4F28
		public override long ToInt64(object value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x000D6D3C File Offset: 0x000D4F3C
		public override float ToSingle(bool value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x000D6D55 File Offset: 0x000D4F55
		public override float ToSingle(DateTime value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x000D6D6E File Offset: 0x000D4F6E
		public override float ToSingle(DateTimeOffset value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x000D6D87 File Offset: 0x000D4F87
		public override float ToSingle(decimal value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x000D6DA0 File Offset: 0x000D4FA0
		public override float ToSingle(double value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x000D6DB9 File Offset: 0x000D4FB9
		public override float ToSingle(int value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x000D6DD2 File Offset: 0x000D4FD2
		public override float ToSingle(long value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x000D6DEB File Offset: 0x000D4FEB
		public override float ToSingle(float value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x000D6E04 File Offset: 0x000D5004
		public override float ToSingle(string value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x000D6E18 File Offset: 0x000D5018
		public override float ToSingle(object value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x000D6E2C File Offset: 0x000D502C
		public override string ToString(bool value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x000D6E45 File Offset: 0x000D5045
		public override string ToString(DateTime value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x000D6E5E File Offset: 0x000D505E
		public override string ToString(DateTimeOffset value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x000D6E77 File Offset: 0x000D5077
		public override string ToString(decimal value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x000D6E90 File Offset: 0x000D5090
		public override string ToString(double value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x000D6EA9 File Offset: 0x000D50A9
		public override string ToString(int value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x000D6EC2 File Offset: 0x000D50C2
		public override string ToString(long value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x000D6EDB File Offset: 0x000D50DB
		public override string ToString(float value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x000D6EF4 File Offset: 0x000D50F4
		public override string ToString(string value, IXmlNamespaceResolver nsResolver)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, nsResolver);
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x000D6F08 File Offset: 0x000D5108
		public override string ToString(object value, IXmlNamespaceResolver nsResolver)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, nsResolver);
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x000D6F1C File Offset: 0x000D511C
		public override string ToString(string value)
		{
			return this.ToString(value, null);
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x000D6F26 File Offset: 0x000D5126
		public override string ToString(object value)
		{
			return this.ToString(value, null);
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000D6F30 File Offset: 0x000D5130
		public override object ChangeType(bool value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x000D6F40 File Offset: 0x000D5140
		public override object ChangeType(DateTime value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x000D6F50 File Offset: 0x000D5150
		public override object ChangeType(DateTimeOffset value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x000D6F60 File Offset: 0x000D5160
		public override object ChangeType(decimal value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x000D6F70 File Offset: 0x000D5170
		public override object ChangeType(double value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x000D6F80 File Offset: 0x000D5180
		public override object ChangeType(int value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x000D6F90 File Offset: 0x000D5190
		public override object ChangeType(long value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x000D6FA0 File Offset: 0x000D51A0
		public override object ChangeType(float value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000D6FB0 File Offset: 0x000D51B0
		public override object ChangeType(string value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			return this.ChangeType(value, destinationType, nsResolver);
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x000D6FBB File Offset: 0x000D51BB
		public override object ChangeType(string value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x000D6FC6 File Offset: 0x000D51C6
		public override object ChangeType(object value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x0600297C RID: 10620 RVA: 0x000D6FD1 File Offset: 0x000D51D1
		protected XmlSchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x0600297D RID: 10621 RVA: 0x000D6FD9 File Offset: 0x000D51D9
		protected XmlTypeCode TypeCode
		{
			get
			{
				return this.typeCode;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x0600297E RID: 10622 RVA: 0x000D6FE4 File Offset: 0x000D51E4
		protected string XmlTypeName
		{
			get
			{
				XmlSchemaType baseXmlSchemaType = this.schemaType;
				if (baseXmlSchemaType != null)
				{
					while (baseXmlSchemaType.QualifiedName.IsEmpty)
					{
						baseXmlSchemaType = baseXmlSchemaType.BaseXmlSchemaType;
					}
					return XmlBaseConverter.QNameToString(baseXmlSchemaType.QualifiedName);
				}
				if (this.typeCode == XmlTypeCode.Node)
				{
					return "node";
				}
				if (this.typeCode == XmlTypeCode.AnyAtomicType)
				{
					return "xdt:anyAtomicType";
				}
				return "item";
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x0600297F RID: 10623 RVA: 0x000D7041 File Offset: 0x000D5241
		protected Type DefaultClrType
		{
			get
			{
				return this.clrTypeDefault;
			}
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x000D7049 File Offset: 0x000D5249
		protected static bool IsDerivedFrom(Type derivedType, Type baseType)
		{
			while (derivedType != null)
			{
				if (derivedType == baseType)
				{
					return true;
				}
				derivedType = derivedType.BaseType;
			}
			return false;
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x000D706C File Offset: 0x000D526C
		protected Exception CreateInvalidClrMappingException(Type sourceType, Type destinationType)
		{
			if (sourceType == destinationType)
			{
				return new InvalidCastException(Res.GetString("XmlConvert_TypeBadMapping", new object[]
				{
					this.XmlTypeName,
					sourceType.Name
				}));
			}
			return new InvalidCastException(Res.GetString("XmlConvert_TypeBadMapping2", new object[]
			{
				this.XmlTypeName,
				sourceType.Name,
				destinationType.Name
			}));
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x000D70DC File Offset: 0x000D52DC
		protected static string QNameToString(XmlQualifiedName name)
		{
			if (name.Namespace.Length == 0)
			{
				return name.Name;
			}
			if (name.Namespace == "http://www.w3.org/2001/XMLSchema")
			{
				return "xs:" + name.Name;
			}
			if (name.Namespace == "http://www.w3.org/2003/11/xpath-datatypes")
			{
				return "xdt:" + name.Name;
			}
			return "{" + name.Namespace + "}" + name.Name;
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x000D715E File Offset: 0x000D535E
		protected virtual object ChangeListType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			throw this.CreateInvalidClrMappingException(value.GetType(), destinationType);
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x000D716D File Offset: 0x000D536D
		protected static byte[] StringToBase64Binary(string value)
		{
			return Convert.FromBase64String(XmlConvert.TrimString(value));
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x000D717A File Offset: 0x000D537A
		protected static DateTime StringToDate(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Date);
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x000D7188 File Offset: 0x000D5388
		protected static DateTime StringToDateTime(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.DateTime);
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x000D7198 File Offset: 0x000D5398
		protected static TimeSpan StringToDayTimeDuration(string value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.DayTimeDuration).ToTimeSpan(XsdDuration.DurationType.DayTimeDuration);
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x000D71B8 File Offset: 0x000D53B8
		protected static TimeSpan StringToDuration(string value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.Duration).ToTimeSpan(XsdDuration.DurationType.Duration);
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x000D71D5 File Offset: 0x000D53D5
		protected static DateTime StringToGDay(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GDay);
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x000D71E4 File Offset: 0x000D53E4
		protected static DateTime StringToGMonth(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonth);
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x000D71F6 File Offset: 0x000D53F6
		protected static DateTime StringToGMonthDay(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonthDay);
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x000D7205 File Offset: 0x000D5405
		protected static DateTime StringToGYear(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYear);
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x000D7214 File Offset: 0x000D5414
		protected static DateTime StringToGYearMonth(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYearMonth);
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x000D7222 File Offset: 0x000D5422
		protected static DateTimeOffset StringToDateOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Date);
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x000D7230 File Offset: 0x000D5430
		protected static DateTimeOffset StringToDateTimeOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.DateTime);
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x000D723E File Offset: 0x000D543E
		protected static DateTimeOffset StringToGDayOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GDay);
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x000D724D File Offset: 0x000D544D
		protected static DateTimeOffset StringToGMonthOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonth);
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x000D725F File Offset: 0x000D545F
		protected static DateTimeOffset StringToGMonthDayOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonthDay);
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x000D726E File Offset: 0x000D546E
		protected static DateTimeOffset StringToGYearOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYear);
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x000D727D File Offset: 0x000D547D
		protected static DateTimeOffset StringToGYearMonthOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYearMonth);
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x000D728C File Offset: 0x000D548C
		protected static byte[] StringToHexBinary(string value)
		{
			byte[] result;
			try
			{
				result = XmlConvert.FromBinHexString(XmlConvert.TrimString(value), false);
			}
			catch (XmlException ex)
			{
				throw new FormatException(ex.Message);
			}
			return result;
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x000D72C8 File Offset: 0x000D54C8
		protected static XmlQualifiedName StringToQName(string value, IXmlNamespaceResolver nsResolver)
		{
			value = value.Trim();
			string text;
			string name;
			try
			{
				ValidateNames.ParseQNameThrow(value, out text, out name);
			}
			catch (XmlException ex)
			{
				throw new FormatException(ex.Message);
			}
			if (nsResolver == null)
			{
				throw new InvalidCastException(Res.GetString("XmlConvert_TypeNoNamespace", new object[]
				{
					value,
					text
				}));
			}
			string text2 = nsResolver.LookupNamespace(text);
			if (text2 == null)
			{
				throw new InvalidCastException(Res.GetString("XmlConvert_TypeNoNamespace", new object[]
				{
					value,
					text
				}));
			}
			return new XmlQualifiedName(name, text2);
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x000D7358 File Offset: 0x000D5558
		protected static DateTime StringToTime(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Time);
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x000D7366 File Offset: 0x000D5566
		protected static DateTimeOffset StringToTimeOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Time);
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x000D7374 File Offset: 0x000D5574
		protected static TimeSpan StringToYearMonthDuration(string value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.YearMonthDuration).ToTimeSpan(XsdDuration.DurationType.YearMonthDuration);
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x000D7391 File Offset: 0x000D5591
		protected static string AnyUriToString(Uri value)
		{
			return value.OriginalString;
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x000D7399 File Offset: 0x000D5599
		protected static string Base64BinaryToString(byte[] value)
		{
			return Convert.ToBase64String(value);
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x000D73A4 File Offset: 0x000D55A4
		protected static string DateToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Date).ToString();
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x000D73C8 File Offset: 0x000D55C8
		protected static string DateTimeToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.DateTime).ToString();
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x000D73EC File Offset: 0x000D55EC
		protected static string DayTimeDurationToString(TimeSpan value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.DayTimeDuration).ToString(XsdDuration.DurationType.DayTimeDuration);
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x000D740C File Offset: 0x000D560C
		protected static string DurationToString(TimeSpan value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.Duration).ToString(XsdDuration.DurationType.Duration);
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x000D742C File Offset: 0x000D562C
		protected static string GDayToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GDay).ToString();
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x000D7450 File Offset: 0x000D5650
		protected static string GMonthToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonth).ToString();
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x000D7478 File Offset: 0x000D5678
		protected static string GMonthDayToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonthDay).ToString();
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x000D749C File Offset: 0x000D569C
		protected static string GYearToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYear).ToString();
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x000D74C0 File Offset: 0x000D56C0
		protected static string GYearMonthToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYearMonth).ToString();
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x000D74E4 File Offset: 0x000D56E4
		protected static string DateOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Date).ToString();
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x000D7508 File Offset: 0x000D5708
		protected static string DateTimeOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.DateTime).ToString();
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x000D752C File Offset: 0x000D572C
		protected static string GDayOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GDay).ToString();
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x000D7550 File Offset: 0x000D5750
		protected static string GMonthOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonth).ToString();
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x000D7578 File Offset: 0x000D5778
		protected static string GMonthDayOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonthDay).ToString();
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x000D759C File Offset: 0x000D579C
		protected static string GYearOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYear).ToString();
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x000D75C0 File Offset: 0x000D57C0
		protected static string GYearMonthOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYearMonth).ToString();
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x000D75E4 File Offset: 0x000D57E4
		protected static string QNameToString(XmlQualifiedName qname, IXmlNamespaceResolver nsResolver)
		{
			if (nsResolver == null)
			{
				return "{" + qname.Namespace + "}" + qname.Name;
			}
			string text = nsResolver.LookupPrefix(qname.Namespace);
			if (text == null)
			{
				throw new InvalidCastException(Res.GetString("XmlConvert_TypeNoPrefix", new object[]
				{
					qname.ToString(),
					qname.Namespace
				}));
			}
			if (text.Length == 0)
			{
				return qname.Name;
			}
			return text + ":" + qname.Name;
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x000D7668 File Offset: 0x000D5868
		protected static string TimeToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Time).ToString();
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x000D768C File Offset: 0x000D588C
		protected static string TimeOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Time).ToString();
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x000D76B0 File Offset: 0x000D58B0
		protected static string YearMonthDurationToString(TimeSpan value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.YearMonthDuration).ToString(XsdDuration.DurationType.YearMonthDuration);
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x000D76CD File Offset: 0x000D58CD
		internal static DateTime DateTimeOffsetToDateTime(DateTimeOffset value)
		{
			return value.LocalDateTime;
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x000D76D8 File Offset: 0x000D58D8
		internal static int DecimalToInt32(decimal value)
		{
			if (value < -2147483648m || value > 2147483647m)
			{
				string name = "XmlConvert_Overflow";
				object[] args = new string[]
				{
					XmlConvert.ToString(value),
					"Int32"
				};
				throw new OverflowException(Res.GetString(name, args));
			}
			return (int)value;
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x000D7738 File Offset: 0x000D5938
		protected static long DecimalToInt64(decimal value)
		{
			if (value < -9223372036854775808m || value > 9223372036854775807m)
			{
				string name = "XmlConvert_Overflow";
				object[] args = new string[]
				{
					XmlConvert.ToString(value),
					"Int64"
				};
				throw new OverflowException(Res.GetString(name, args));
			}
			return (long)value;
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x000D77A0 File Offset: 0x000D59A0
		protected static ulong DecimalToUInt64(decimal value)
		{
			if (value < 0m || value > 18446744073709551615m)
			{
				string name = "XmlConvert_Overflow";
				object[] args = new string[]
				{
					XmlConvert.ToString(value),
					"UInt64"
				};
				throw new OverflowException(Res.GetString(name, args));
			}
			return (ulong)value;
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x000D77F8 File Offset: 0x000D59F8
		protected static byte Int32ToByte(int value)
		{
			if (value < 0 || value > 255)
			{
				string name = "XmlConvert_Overflow";
				object[] args = new string[]
				{
					XmlConvert.ToString(value),
					"Byte"
				};
				throw new OverflowException(Res.GetString(name, args));
			}
			return (byte)value;
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x000D783C File Offset: 0x000D5A3C
		protected static short Int32ToInt16(int value)
		{
			if (value < -32768 || value > 32767)
			{
				string name = "XmlConvert_Overflow";
				object[] args = new string[]
				{
					XmlConvert.ToString(value),
					"Int16"
				};
				throw new OverflowException(Res.GetString(name, args));
			}
			return (short)value;
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x000D7884 File Offset: 0x000D5A84
		protected static sbyte Int32ToSByte(int value)
		{
			if (value < -128 || value > 127)
			{
				string name = "XmlConvert_Overflow";
				object[] args = new string[]
				{
					XmlConvert.ToString(value),
					"SByte"
				};
				throw new OverflowException(Res.GetString(name, args));
			}
			return (sbyte)value;
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x000D78C8 File Offset: 0x000D5AC8
		protected static ushort Int32ToUInt16(int value)
		{
			if (value < 0 || value > 65535)
			{
				string name = "XmlConvert_Overflow";
				object[] args = new string[]
				{
					XmlConvert.ToString(value),
					"UInt16"
				};
				throw new OverflowException(Res.GetString(name, args));
			}
			return (ushort)value;
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x000D790C File Offset: 0x000D5B0C
		protected static int Int64ToInt32(long value)
		{
			if (value < -2147483648L || value > 2147483647L)
			{
				string name = "XmlConvert_Overflow";
				object[] args = new string[]
				{
					XmlConvert.ToString(value),
					"Int32"
				};
				throw new OverflowException(Res.GetString(name, args));
			}
			return (int)value;
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x000D7958 File Offset: 0x000D5B58
		protected static uint Int64ToUInt32(long value)
		{
			if (value < 0L || value > (long)((ulong)-1))
			{
				string name = "XmlConvert_Overflow";
				object[] args = new string[]
				{
					XmlConvert.ToString(value),
					"UInt32"
				};
				throw new OverflowException(Res.GetString(name, args));
			}
			return (uint)value;
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x000D799A File Offset: 0x000D5B9A
		protected static DateTime UntypedAtomicToDateTime(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.AllXsd);
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x000D79AC File Offset: 0x000D5BAC
		protected static DateTimeOffset UntypedAtomicToDateTimeOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.AllXsd);
		}

		// Token: 0x040011F7 RID: 4599
		private XmlSchemaType schemaType;

		// Token: 0x040011F8 RID: 4600
		private XmlTypeCode typeCode;

		// Token: 0x040011F9 RID: 4601
		private Type clrTypeDefault;

		// Token: 0x040011FA RID: 4602
		protected static readonly Type ICollectionType = typeof(ICollection);

		// Token: 0x040011FB RID: 4603
		protected static readonly Type IEnumerableType = typeof(IEnumerable);

		// Token: 0x040011FC RID: 4604
		protected static readonly Type IListType = typeof(IList);

		// Token: 0x040011FD RID: 4605
		protected static readonly Type ObjectArrayType = typeof(object[]);

		// Token: 0x040011FE RID: 4606
		protected static readonly Type StringArrayType = typeof(string[]);

		// Token: 0x040011FF RID: 4607
		protected static readonly Type XmlAtomicValueArrayType = typeof(XmlAtomicValue[]);

		// Token: 0x04001200 RID: 4608
		protected static readonly Type DecimalType = typeof(decimal);

		// Token: 0x04001201 RID: 4609
		protected static readonly Type Int32Type = typeof(int);

		// Token: 0x04001202 RID: 4610
		protected static readonly Type Int64Type = typeof(long);

		// Token: 0x04001203 RID: 4611
		protected static readonly Type StringType = typeof(string);

		// Token: 0x04001204 RID: 4612
		protected static readonly Type XmlAtomicValueType = typeof(XmlAtomicValue);

		// Token: 0x04001205 RID: 4613
		protected static readonly Type ObjectType = typeof(object);

		// Token: 0x04001206 RID: 4614
		protected static readonly Type ByteType = typeof(byte);

		// Token: 0x04001207 RID: 4615
		protected static readonly Type Int16Type = typeof(short);

		// Token: 0x04001208 RID: 4616
		protected static readonly Type SByteType = typeof(sbyte);

		// Token: 0x04001209 RID: 4617
		protected static readonly Type UInt16Type = typeof(ushort);

		// Token: 0x0400120A RID: 4618
		protected static readonly Type UInt32Type = typeof(uint);

		// Token: 0x0400120B RID: 4619
		protected static readonly Type UInt64Type = typeof(ulong);

		// Token: 0x0400120C RID: 4620
		protected static readonly Type XPathItemType = typeof(XPathItem);

		// Token: 0x0400120D RID: 4621
		protected static readonly Type DoubleType = typeof(double);

		// Token: 0x0400120E RID: 4622
		protected static readonly Type SingleType = typeof(float);

		// Token: 0x0400120F RID: 4623
		protected static readonly Type DateTimeType = typeof(DateTime);

		// Token: 0x04001210 RID: 4624
		protected static readonly Type DateTimeOffsetType = typeof(DateTimeOffset);

		// Token: 0x04001211 RID: 4625
		protected static readonly Type BooleanType = typeof(bool);

		// Token: 0x04001212 RID: 4626
		protected static readonly Type ByteArrayType = typeof(byte[]);

		// Token: 0x04001213 RID: 4627
		protected static readonly Type XmlQualifiedNameType = typeof(XmlQualifiedName);

		// Token: 0x04001214 RID: 4628
		protected static readonly Type UriType = typeof(Uri);

		// Token: 0x04001215 RID: 4629
		protected static readonly Type TimeSpanType = typeof(TimeSpan);

		// Token: 0x04001216 RID: 4630
		protected static readonly Type XPathNavigatorType = typeof(XPathNavigator);
	}
}
