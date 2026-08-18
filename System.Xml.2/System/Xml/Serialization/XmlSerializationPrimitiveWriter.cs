using System;

namespace System.Xml.Serialization
{
	// Token: 0x020001CA RID: 458
	internal class XmlSerializationPrimitiveWriter : XmlSerializationWriter
	{
		// Token: 0x06001F20 RID: 7968 RVA: 0x000A926E File Offset: 0x000A746E
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

		// Token: 0x06001F21 RID: 7969 RVA: 0x000A92A6 File Offset: 0x000A74A6
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

		// Token: 0x06001F22 RID: 7970 RVA: 0x000A92DD File Offset: 0x000A74DD
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

		// Token: 0x06001F23 RID: 7971 RVA: 0x000A9314 File Offset: 0x000A7514
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

		// Token: 0x06001F24 RID: 7972 RVA: 0x000A934B File Offset: 0x000A754B
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

		// Token: 0x06001F25 RID: 7973 RVA: 0x000A9382 File Offset: 0x000A7582
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

		// Token: 0x06001F26 RID: 7974 RVA: 0x000A93B9 File Offset: 0x000A75B9
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

		// Token: 0x06001F27 RID: 7975 RVA: 0x000A93F0 File Offset: 0x000A75F0
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

		// Token: 0x06001F28 RID: 7976 RVA: 0x000A9427 File Offset: 0x000A7627
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

		// Token: 0x06001F29 RID: 7977 RVA: 0x000A945E File Offset: 0x000A765E
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

		// Token: 0x06001F2A RID: 7978 RVA: 0x000A9495 File Offset: 0x000A7695
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

		// Token: 0x06001F2B RID: 7979 RVA: 0x000A94CC File Offset: 0x000A76CC
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

		// Token: 0x06001F2C RID: 7980 RVA: 0x000A9503 File Offset: 0x000A7703
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

		// Token: 0x06001F2D RID: 7981 RVA: 0x000A953A File Offset: 0x000A773A
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

		// Token: 0x06001F2E RID: 7982 RVA: 0x000A9571 File Offset: 0x000A7771
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

		// Token: 0x06001F2F RID: 7983 RVA: 0x000A95AE File Offset: 0x000A77AE
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

		// Token: 0x06001F30 RID: 7984 RVA: 0x000A95E8 File Offset: 0x000A77E8
		internal void Write_TimeSpan(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("TimeSpan", "");
				return;
			}
			TimeSpan value = (TimeSpan)o;
			base.WriteElementStringRaw("TimeSpan", "", XmlConvert.ToString(value));
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x000A962C File Offset: 0x000A782C
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

		// Token: 0x06001F32 RID: 7986 RVA: 0x000A9663 File Offset: 0x000A7863
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

		// Token: 0x06001F33 RID: 7987 RVA: 0x000A969B File Offset: 0x000A789B
		protected override void InitCallbacks()
		{
		}
	}
}
