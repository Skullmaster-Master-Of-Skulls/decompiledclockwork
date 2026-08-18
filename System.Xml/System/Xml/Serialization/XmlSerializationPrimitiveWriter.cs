using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200034B RID: 843
	internal class XmlSerializationPrimitiveWriter : XmlSerializationWriter
	{
		// Token: 0x060028ED RID: 10477 RVA: 0x000D23D0 File Offset: 0x000D13D0
		internal void Write_string(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("string", "");
				return;
			}
			base.TopLevelElement();
			base.WriteNullableStringLiteral("string", "", (string)o);
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x000D2408 File Offset: 0x000D1408
		internal void Write_int(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("int", "");
				return;
			}
			base.WriteElementStringRaw("int", "", XmlConvert.ToString((int)o));
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x000D243F File Offset: 0x000D143F
		internal void Write_boolean(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("boolean", "");
				return;
			}
			base.WriteElementStringRaw("boolean", "", XmlConvert.ToString((bool)o));
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x000D2476 File Offset: 0x000D1476
		internal void Write_short(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("short", "");
				return;
			}
			base.WriteElementStringRaw("short", "", XmlConvert.ToString((short)o));
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x000D24AD File Offset: 0x000D14AD
		internal void Write_long(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("long", "");
				return;
			}
			base.WriteElementStringRaw("long", "", XmlConvert.ToString((long)o));
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x000D24E4 File Offset: 0x000D14E4
		internal void Write_float(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("float", "");
				return;
			}
			base.WriteElementStringRaw("float", "", XmlConvert.ToString((float)o));
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x000D251C File Offset: 0x000D151C
		internal void Write_double(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("double", "");
				return;
			}
			base.WriteElementStringRaw("double", "", XmlConvert.ToString((double)o));
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x000D2554 File Offset: 0x000D1554
		internal void Write_decimal(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("decimal", "");
				return;
			}
			base.WriteElementStringRaw("decimal", "", XmlConvert.ToString((decimal)o));
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x000D258B File Offset: 0x000D158B
		internal void Write_dateTime(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("dateTime", "");
				return;
			}
			base.WriteElementStringRaw("dateTime", "", XmlSerializationWriter.FromDateTime((DateTime)o));
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x000D25C2 File Offset: 0x000D15C2
		internal void Write_unsignedByte(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("unsignedByte", "");
				return;
			}
			base.WriteElementStringRaw("unsignedByte", "", XmlConvert.ToString((byte)o));
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x000D25F9 File Offset: 0x000D15F9
		internal void Write_byte(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("byte", "");
				return;
			}
			base.WriteElementStringRaw("byte", "", XmlConvert.ToString((sbyte)o));
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x000D2630 File Offset: 0x000D1630
		internal void Write_unsignedShort(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("unsignedShort", "");
				return;
			}
			base.WriteElementStringRaw("unsignedShort", "", XmlConvert.ToString((ushort)o));
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x000D2667 File Offset: 0x000D1667
		internal void Write_unsignedInt(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("unsignedInt", "");
				return;
			}
			base.WriteElementStringRaw("unsignedInt", "", XmlConvert.ToString((uint)o));
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x000D269E File Offset: 0x000D169E
		internal void Write_unsignedLong(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("unsignedLong", "");
				return;
			}
			base.WriteElementStringRaw("unsignedLong", "", XmlConvert.ToString((ulong)o));
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x000D26D5 File Offset: 0x000D16D5
		internal void Write_base64Binary(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("base64Binary", "");
				return;
			}
			base.TopLevelElement();
			base.WriteNullableStringLiteralRaw("base64Binary", "", XmlSerializationWriter.FromByteArrayBase64((byte[])o));
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x000D2712 File Offset: 0x000D1712
		internal void Write_guid(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("guid", "");
				return;
			}
			base.WriteElementStringRaw("guid", "", XmlConvert.ToString((Guid)o));
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x000D2749 File Offset: 0x000D1749
		internal void Write_char(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("char", "");
				return;
			}
			base.WriteElementString("char", "", XmlSerializationWriter.FromChar((char)o));
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x000D2780 File Offset: 0x000D1780
		internal void Write_QName(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("QName", "");
				return;
			}
			base.TopLevelElement();
			base.WriteNullableQualifiedNameLiteral("QName", "", (XmlQualifiedName)o);
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x000D27B8 File Offset: 0x000D17B8
		protected override void InitCallbacks()
		{
		}
	}
}
