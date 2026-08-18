using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x02000063 RID: 99
	[CLSCompliant(true)]
	public class LBERDecoder : Asn1Decoder, ISerializable
	{
		// Token: 0x06000390 RID: 912 RVA: 0x0001179C File Offset: 0x0001079C
		public LBERDecoder()
		{
			this.InitBlock();
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000117B8 File Offset: 0x000107B8
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000117C8 File Offset: 0x000107C8
		private void InitBlock()
		{
			this.asn1ID = new Asn1Identifier();
			this.asn1Len = new Asn1Length();
		}

		// Token: 0x06000393 RID: 915 RVA: 0x000117EC File Offset: 0x000107EC
		[CLSCompliant(false)]
		public virtual Asn1Object decode(sbyte[] value_Renamed)
		{
			Asn1Object result = null;
			MemoryStream in_Renamed = new MemoryStream(SupportClass.ToByteArray(value_Renamed));
			try
			{
				result = this.decode(in_Renamed);
			}
			catch (IOException ex)
			{
			}
			return result;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00011838 File Offset: 0x00010838
		public virtual Asn1Object decode(Stream in_Renamed)
		{
			int[] len = new int[1];
			return this.decode(in_Renamed, len);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00011858 File Offset: 0x00010858
		public virtual Asn1Object decode(Stream in_Renamed, int[] len)
		{
			this.asn1ID.reset(in_Renamed);
			this.asn1Len.reset(in_Renamed);
			int length = this.asn1Len.Length;
			len[0] = this.asn1ID.EncodedLength + this.asn1Len.EncodedLength + length;
			if (this.asn1ID.Universal)
			{
				int tag = this.asn1ID.Tag;
				switch (tag)
				{
				case 1:
					return new Asn1Boolean(this, in_Renamed, length);
				case 2:
					return new Asn1Integer(this, in_Renamed, length);
				case 3:
					break;
				case 4:
					return new Asn1OctetString(this, in_Renamed, length);
				case 5:
					return new Asn1Null();
				default:
					if (tag == 10)
					{
						return new Asn1Enumerated(this, in_Renamed, length);
					}
					switch (tag)
					{
					case 16:
						return new Asn1Sequence(this, in_Renamed, length);
					case 17:
						return new Asn1Set(this, in_Renamed, length);
					}
					break;
				}
				throw new EndOfStreamException("Unknown tag");
			}
			return new Asn1Tagged(this, in_Renamed, length, (Asn1Identifier)this.asn1ID.Clone());
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00011968 File Offset: 0x00010968
		public object decodeBoolean(Stream in_Renamed, int len)
		{
			sbyte[] array = new sbyte[len];
			int num = SupportClass.ReadInput(in_Renamed, ref array, 0, array.Length);
			if (num != len)
			{
				throw new EndOfStreamException("LBER: BOOLEAN: decode error: EOF");
			}
			return array[0] != 0;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000119AC File Offset: 0x000109AC
		public object decodeNumeric(Stream in_Renamed, int len)
		{
			long num = 0L;
			int num2 = in_Renamed.ReadByte();
			if (num2 < 0)
			{
				throw new EndOfStreamException("LBER: NUMERIC: decode error: EOF");
			}
			if ((num2 & 128) != 0)
			{
				num = -1L;
			}
			num = (num << 8 | (long)num2);
			for (int i = 1; i < len; i++)
			{
				num2 = in_Renamed.ReadByte();
				if (num2 < 0)
				{
					throw new EndOfStreamException("LBER: NUMERIC: decode error: EOF");
				}
				num = (num << 8 | (long)num2);
			}
			return num;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00011A18 File Offset: 0x00010A18
		public object decodeOctetString(Stream in_Renamed, int len)
		{
			sbyte[] result = new sbyte[len];
			int num;
			for (int i = 0; i < len; i += num)
			{
				num = SupportClass.ReadInput(in_Renamed, ref result, i, len - i);
			}
			return result;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00011A4C File Offset: 0x00010A4C
		public object decodeCharacterString(Stream in_Renamed, int len)
		{
			sbyte[] array = new sbyte[len];
			for (int i = 0; i < len; i++)
			{
				int num = in_Renamed.ReadByte();
				if (num == -1)
				{
					throw new EndOfStreamException("LBER: CHARACTER STRING: decode error: EOF");
				}
				array[i] = (sbyte)num;
			}
			Encoding encoding = Encoding.GetEncoding("utf-8");
			char[] chars = encoding.GetChars(SupportClass.ToByteArray(array));
			return new string(chars);
		}

		// Token: 0x040001A4 RID: 420
		private Asn1Identifier asn1ID;

		// Token: 0x040001A5 RID: 421
		private Asn1Length asn1Len;
	}
}
