using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000154 RID: 340
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public sealed class SqlBytes : INullable, IXmlSerializable, ISerializable
	{
		// Token: 0x06001420 RID: 5152 RVA: 0x0009C358 File Offset: 0x0009B758
		public SqlBytes()
		{
			this.SetNull();
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0009C374 File Offset: 0x0009B774
		public SqlBytes(byte[] buffer)
		{
			this.m_rgbBuf = buffer;
			this.m_stream = null;
			if (this.m_rgbBuf == null)
			{
				this.m_state = SqlBytesCharsState.Null;
				this.m_lCurLen = -1L;
			}
			else
			{
				this.m_state = SqlBytesCharsState.Buffer;
				this.m_lCurLen = (long)this.m_rgbBuf.Length;
			}
			this.m_rgbWorkBuf = null;
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0009C3CC File Offset: 0x0009B7CC
		public SqlBytes(SqlBinary value) : this(value.IsNull ? null : value.Value)
		{
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0009C3F4 File Offset: 0x0009B7F4
		public SqlBytes(Stream s)
		{
			this.m_rgbBuf = null;
			this.m_lCurLen = -1L;
			this.m_stream = s;
			this.m_state = ((s == null) ? SqlBytesCharsState.Null : SqlBytesCharsState.Stream);
			this.m_rgbWorkBuf = null;
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0009C434 File Offset: 0x0009B834
		private SqlBytes(SerializationInfo info, StreamingContext context)
		{
			this.m_stream = null;
			this.m_rgbWorkBuf = null;
			if (info.GetBoolean("IsNull"))
			{
				this.m_state = SqlBytesCharsState.Null;
				this.m_rgbBuf = null;
				return;
			}
			this.m_state = SqlBytesCharsState.Buffer;
			this.m_rgbBuf = (byte[])info.GetValue("data", typeof(byte[]));
			this.m_lCurLen = (long)this.m_rgbBuf.Length;
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x0009C4A8 File Offset: 0x0009B8A8
		public bool IsNull
		{
			get
			{
				return this.m_state == SqlBytesCharsState.Null;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x0009C4C0 File Offset: 0x0009B8C0
		public byte[] Buffer
		{
			get
			{
				if (this.FStream())
				{
					this.CopyStreamToBuffer();
				}
				return this.m_rgbBuf;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x0009C4E4 File Offset: 0x0009B8E4
		public long Length
		{
			get
			{
				SqlBytesCharsState state = this.m_state;
				if (state == SqlBytesCharsState.Null)
				{
					throw new SqlNullValueException();
				}
				if (state != SqlBytesCharsState.Stream)
				{
					return this.m_lCurLen;
				}
				return this.m_stream.Length;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x0009C51C File Offset: 0x0009B91C
		public long MaxLength
		{
			get
			{
				SqlBytesCharsState state = this.m_state;
				if (state == SqlBytesCharsState.Stream)
				{
					return -1L;
				}
				if (this.m_rgbBuf != null)
				{
					return (long)this.m_rgbBuf.Length;
				}
				return -1L;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x0009C54C File Offset: 0x0009B94C
		public byte[] Value
		{
			get
			{
				SqlBytesCharsState state = this.m_state;
				if (state != SqlBytesCharsState.Null)
				{
					byte[] array;
					if (state != SqlBytesCharsState.Stream)
					{
						array = new byte[this.m_lCurLen];
						Array.Copy(this.m_rgbBuf, array, (int)this.m_lCurLen);
					}
					else
					{
						if (this.m_stream.Length > 2147483647L)
						{
							throw new SqlTypeException(Res.GetString("SqlMisc_BufferInsufficientMessage"));
						}
						array = new byte[this.m_stream.Length];
						if (this.m_stream.Position != 0L)
						{
							this.m_stream.Seek(0L, SeekOrigin.Begin);
						}
						this.m_stream.Read(array, 0, checked((int)this.m_stream.Length));
					}
					return array;
				}
				throw new SqlNullValueException();
			}
		}

		// Token: 0x170002F2 RID: 754
		public byte this[long offset]
		{
			get
			{
				if (offset < 0L || offset >= this.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				if (this.m_rgbWorkBuf == null)
				{
					this.m_rgbWorkBuf = new byte[1];
				}
				this.Read(offset, this.m_rgbWorkBuf, 0, 1);
				return this.m_rgbWorkBuf[0];
			}
			set
			{
				if (this.m_rgbWorkBuf == null)
				{
					this.m_rgbWorkBuf = new byte[1];
				}
				this.m_rgbWorkBuf[0] = value;
				this.Write(offset, this.m_rgbWorkBuf, 0, 1);
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x0009C690 File Offset: 0x0009BA90
		public StorageState Storage
		{
			get
			{
				switch (this.m_state)
				{
				case SqlBytesCharsState.Null:
					throw new SqlNullValueException();
				case SqlBytesCharsState.Buffer:
					return StorageState.Buffer;
				case SqlBytesCharsState.Stream:
					return StorageState.Stream;
				}
				return StorageState.UnmanagedBuffer;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x0009C6C8 File Offset: 0x0009BAC8
		// (set) Token: 0x0600142E RID: 5166 RVA: 0x0009C6EC File Offset: 0x0009BAEC
		public Stream Stream
		{
			get
			{
				if (!this.FStream())
				{
					return new StreamOnSqlBytes(this);
				}
				return this.m_stream;
			}
			set
			{
				this.m_lCurLen = -1L;
				this.m_stream = value;
				this.m_state = ((value == null) ? SqlBytesCharsState.Null : SqlBytesCharsState.Stream);
			}
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0009C718 File Offset: 0x0009BB18
		public void SetNull()
		{
			this.m_lCurLen = -1L;
			this.m_stream = null;
			this.m_state = SqlBytesCharsState.Null;
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0009C73C File Offset: 0x0009BB3C
		public void SetLength(long value)
		{
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			if (this.FStream())
			{
				this.m_stream.SetLength(value);
				return;
			}
			if (this.m_rgbBuf == null)
			{
				throw new SqlTypeException(Res.GetString("SqlMisc_NoBufferMessage"));
			}
			if (value > (long)this.m_rgbBuf.Length)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			if (this.IsNull)
			{
				this.m_state = SqlBytesCharsState.Buffer;
			}
			this.m_lCurLen = value;
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0009C7B4 File Offset: 0x0009BBB4
		public long Read(long offset, byte[] buffer, int offsetInBuffer, int count)
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset > this.Length || offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offsetInBuffer > buffer.Length || offsetInBuffer < 0)
			{
				throw new ArgumentOutOfRangeException("offsetInBuffer");
			}
			if (count < 0 || count > buffer.Length - offsetInBuffer)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if ((long)count > this.Length - offset)
			{
				count = (int)(this.Length - offset);
			}
			if (count != 0)
			{
				SqlBytesCharsState state = this.m_state;
				if (state == SqlBytesCharsState.Stream)
				{
					if (this.m_stream.Position != offset)
					{
						this.m_stream.Seek(offset, SeekOrigin.Begin);
					}
					this.m_stream.Read(buffer, offsetInBuffer, count);
				}
				else
				{
					Array.Copy(this.m_rgbBuf, offset, buffer, (long)offsetInBuffer, (long)count);
				}
			}
			return (long)count;
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0009C890 File Offset: 0x0009BC90
		public void Write(long offset, byte[] buffer, int offsetInBuffer, int count)
		{
			if (this.FStream())
			{
				if (this.m_stream.Position != offset)
				{
					this.m_stream.Seek(offset, SeekOrigin.Begin);
				}
				this.m_stream.Write(buffer, offsetInBuffer, count);
				return;
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (this.m_rgbBuf == null)
			{
				throw new SqlTypeException(Res.GetString("SqlMisc_NoBufferMessage"));
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset > (long)this.m_rgbBuf.Length)
			{
				throw new SqlTypeException(Res.GetString("SqlMisc_BufferInsufficientMessage"));
			}
			if (offsetInBuffer < 0 || offsetInBuffer > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offsetInBuffer");
			}
			if (count < 0 || count > buffer.Length - offsetInBuffer)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if ((long)count > (long)this.m_rgbBuf.Length - offset)
			{
				throw new SqlTypeException(Res.GetString("SqlMisc_BufferInsufficientMessage"));
			}
			if (this.IsNull)
			{
				if (offset != 0L)
				{
					throw new SqlTypeException(Res.GetString("SqlMisc_WriteNonZeroOffsetOnNullMessage"));
				}
				this.m_lCurLen = 0L;
				this.m_state = SqlBytesCharsState.Buffer;
			}
			else if (offset > this.m_lCurLen)
			{
				throw new SqlTypeException(Res.GetString("SqlMisc_WriteOffsetLargerThanLenMessage"));
			}
			if (count != 0)
			{
				Array.Copy(buffer, (long)offsetInBuffer, this.m_rgbBuf, offset, (long)count);
				if (this.m_lCurLen < offset + (long)count)
				{
					this.m_lCurLen = offset + (long)count;
				}
			}
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0009C9E4 File Offset: 0x0009BDE4
		public SqlBinary ToSqlBinary()
		{
			if (!this.IsNull)
			{
				return new SqlBinary(this.Value);
			}
			return SqlBinary.Null;
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0009CA0C File Offset: 0x0009BE0C
		public static explicit operator SqlBinary(SqlBytes value)
		{
			return value.ToSqlBinary();
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0009CA20 File Offset: 0x0009BE20
		public static explicit operator SqlBytes(SqlBinary value)
		{
			return new SqlBytes(value);
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x0009CA34 File Offset: 0x0009BE34
		[Conditional("DEBUG")]
		private void AssertValid()
		{
			bool isNull = this.IsNull;
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0009CA48 File Offset: 0x0009BE48
		private void CopyStreamToBuffer()
		{
			long length = this.m_stream.Length;
			if (length >= 2147483647L)
			{
				throw new SqlTypeException(Res.GetString("SqlMisc_WriteOffsetLargerThanLenMessage"));
			}
			if (this.m_rgbBuf == null || (long)this.m_rgbBuf.Length < length)
			{
				this.m_rgbBuf = new byte[length];
			}
			if (this.m_stream.Position != 0L)
			{
				this.m_stream.Seek(0L, SeekOrigin.Begin);
			}
			this.m_stream.Read(this.m_rgbBuf, 0, (int)length);
			this.m_stream = null;
			this.m_lCurLen = length;
			this.m_state = SqlBytesCharsState.Buffer;
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0009CAE4 File Offset: 0x0009BEE4
		internal bool FStream()
		{
			return this.m_state == SqlBytesCharsState.Stream;
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0009CAFC File Offset: 0x0009BEFC
		private void SetBuffer(byte[] buffer)
		{
			this.m_rgbBuf = buffer;
			this.m_lCurLen = ((this.m_rgbBuf == null) ? -1L : ((long)this.m_rgbBuf.Length));
			this.m_stream = null;
			this.m_state = ((this.m_rgbBuf == null) ? SqlBytesCharsState.Null : SqlBytesCharsState.Buffer);
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0009CB44 File Offset: 0x0009BF44
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0009CB54 File Offset: 0x0009BF54
		void IXmlSerializable.ReadXml(XmlReader r)
		{
			byte[] buffer = null;
			string attribute = r.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				r.ReadElementString();
				this.SetNull();
			}
			else
			{
				string text = r.ReadElementString();
				if (text == null)
				{
					buffer = new byte[0];
				}
				else
				{
					text = text.Trim();
					if (text.Length == 0)
					{
						buffer = new byte[0];
					}
					else
					{
						buffer = Convert.FromBase64String(text);
					}
				}
			}
			this.SetBuffer(buffer);
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0009CBC8 File Offset: 0x0009BFC8
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			byte[] buffer = this.Buffer;
			writer.WriteString(Convert.ToBase64String(buffer, 0, (int)this.Length));
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0009CC14 File Offset: 0x0009C014
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("base64Binary", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0009CC30 File Offset: 0x0009C030
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			switch (this.m_state)
			{
			default:
				info.AddValue("IsNull", true);
				return;
			case SqlBytesCharsState.Buffer:
				break;
			case SqlBytesCharsState.Stream:
				this.CopyStreamToBuffer();
				break;
			}
			info.AddValue("IsNull", false);
			info.AddValue("data", this.m_rgbBuf);
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x0009CC8C File Offset: 0x0009C08C
		public static SqlBytes Null
		{
			get
			{
				return new SqlBytes(null);
			}
		}

		// Token: 0x04000D60 RID: 3424
		internal byte[] m_rgbBuf;

		// Token: 0x04000D61 RID: 3425
		private long m_lCurLen;

		// Token: 0x04000D62 RID: 3426
		internal Stream m_stream;

		// Token: 0x04000D63 RID: 3427
		private SqlBytesCharsState m_state;

		// Token: 0x04000D64 RID: 3428
		private byte[] m_rgbWorkBuf;

		// Token: 0x04000D65 RID: 3429
		private const long x_lMaxLen = 2147483647L;

		// Token: 0x04000D66 RID: 3430
		private const long x_lNull = -1L;
	}
}
