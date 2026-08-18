using System;

namespace System.Xml.Serialization
{
	// Token: 0x020001CB RID: 459
	internal class XmlSerializationPrimitiveReader : XmlSerializationReader
	{
		// Token: 0x06001F35 RID: 7989 RVA: 0x000A96A8 File Offset: 0x000A78A8
		internal object Read_string()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id1_string || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				if (base.ReadNull())
				{
					result = null;
				}
				else
				{
					result = base.Reader.ReadElementString();
				}
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x000A9720 File Offset: 0x000A7920
		internal object Read_int()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id3_int || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToInt32(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x000A9798 File Offset: 0x000A7998
		internal object Read_boolean()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id4_boolean || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToBoolean(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x000A9810 File Offset: 0x000A7A10
		internal object Read_short()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id5_short || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToInt16(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x000A9888 File Offset: 0x000A7A88
		internal object Read_long()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id6_long || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToInt64(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x000A9900 File Offset: 0x000A7B00
		internal object Read_float()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id7_float || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToSingle(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x000A9978 File Offset: 0x000A7B78
		internal object Read_double()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id8_double || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToDouble(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x000A99F0 File Offset: 0x000A7BF0
		internal object Read_decimal()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id9_decimal || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToDecimal(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x000A9A68 File Offset: 0x000A7C68
		internal object Read_dateTime()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id10_dateTime || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlSerializationReader.ToDateTime(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x000A9AE0 File Offset: 0x000A7CE0
		internal object Read_unsignedByte()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id11_unsignedByte || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToByte(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x000A9B58 File Offset: 0x000A7D58
		internal object Read_byte()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id12_byte || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToSByte(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x000A9BD0 File Offset: 0x000A7DD0
		internal object Read_unsignedShort()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id13_unsignedShort || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToUInt16(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x000A9C48 File Offset: 0x000A7E48
		internal object Read_unsignedInt()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id14_unsignedInt || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToUInt32(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x000A9CC0 File Offset: 0x000A7EC0
		internal object Read_unsignedLong()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id15_unsignedLong || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToUInt64(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x000A9D38 File Offset: 0x000A7F38
		internal object Read_base64Binary()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id16_base64Binary || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				if (base.ReadNull())
				{
					result = null;
				}
				else
				{
					result = base.ToByteArrayBase64(false);
				}
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x000A9DAC File Offset: 0x000A7FAC
		internal object Read_guid()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id17_guid || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToGuid(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x000A9E24 File Offset: 0x000A8024
		internal object Read_TimeSpan()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id19_TimeSpan || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				if (base.Reader.IsEmptyElement)
				{
					base.Reader.Skip();
					result = default(TimeSpan);
				}
				else
				{
					result = XmlConvert.ToTimeSpan(base.Reader.ReadElementString());
				}
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x000A9EC4 File Offset: 0x000A80C4
		internal object Read_char()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id18_char || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlSerializationReader.ToChar(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x000A9F3C File Offset: 0x000A813C
		internal object Read_QName()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id1_QName || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				if (base.ReadNull())
				{
					result = null;
				}
				else
				{
					result = base.ReadElementQualifiedName();
				}
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x000A9FAF File Offset: 0x000A81AF
		protected override void InitCallbacks()
		{
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x000A9FB4 File Offset: 0x000A81B4
		protected override void InitIDs()
		{
			this.id4_boolean = base.Reader.NameTable.Add("boolean");
			this.id14_unsignedInt = base.Reader.NameTable.Add("unsignedInt");
			this.id15_unsignedLong = base.Reader.NameTable.Add("unsignedLong");
			this.id7_float = base.Reader.NameTable.Add("float");
			this.id10_dateTime = base.Reader.NameTable.Add("dateTime");
			this.id6_long = base.Reader.NameTable.Add("long");
			this.id9_decimal = base.Reader.NameTable.Add("decimal");
			this.id8_double = base.Reader.NameTable.Add("double");
			this.id17_guid = base.Reader.NameTable.Add("guid");
			if (LocalAppContextSwitches.EnableTimeSpanSerialization)
			{
				this.id19_TimeSpan = base.Reader.NameTable.Add("TimeSpan");
			}
			this.id2_Item = base.Reader.NameTable.Add("");
			this.id13_unsignedShort = base.Reader.NameTable.Add("unsignedShort");
			this.id18_char = base.Reader.NameTable.Add("char");
			this.id3_int = base.Reader.NameTable.Add("int");
			this.id12_byte = base.Reader.NameTable.Add("byte");
			this.id16_base64Binary = base.Reader.NameTable.Add("base64Binary");
			this.id11_unsignedByte = base.Reader.NameTable.Add("unsignedByte");
			this.id5_short = base.Reader.NameTable.Add("short");
			this.id1_string = base.Reader.NameTable.Add("string");
			this.id1_QName = base.Reader.NameTable.Add("QName");
		}

		// Token: 0x04000D05 RID: 3333
		private string id4_boolean;

		// Token: 0x04000D06 RID: 3334
		private string id14_unsignedInt;

		// Token: 0x04000D07 RID: 3335
		private string id15_unsignedLong;

		// Token: 0x04000D08 RID: 3336
		private string id7_float;

		// Token: 0x04000D09 RID: 3337
		private string id10_dateTime;

		// Token: 0x04000D0A RID: 3338
		private string id6_long;

		// Token: 0x04000D0B RID: 3339
		private string id9_decimal;

		// Token: 0x04000D0C RID: 3340
		private string id8_double;

		// Token: 0x04000D0D RID: 3341
		private string id17_guid;

		// Token: 0x04000D0E RID: 3342
		private string id19_TimeSpan;

		// Token: 0x04000D0F RID: 3343
		private string id2_Item;

		// Token: 0x04000D10 RID: 3344
		private string id13_unsignedShort;

		// Token: 0x04000D11 RID: 3345
		private string id18_char;

		// Token: 0x04000D12 RID: 3346
		private string id3_int;

		// Token: 0x04000D13 RID: 3347
		private string id12_byte;

		// Token: 0x04000D14 RID: 3348
		private string id16_base64Binary;

		// Token: 0x04000D15 RID: 3349
		private string id11_unsignedByte;

		// Token: 0x04000D16 RID: 3350
		private string id5_short;

		// Token: 0x04000D17 RID: 3351
		private string id1_string;

		// Token: 0x04000D18 RID: 3352
		private string id1_QName;
	}
}
