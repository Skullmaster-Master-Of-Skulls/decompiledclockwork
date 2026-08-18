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
	// Token: 0x02000344 RID: 836
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public sealed class SqlBytes : INullable, IXmlSerializable, ISerializable
	{
		// Token: 0x06002BDE RID: 11230 RVA: 0x002C5B28 File Offset: 0x002C4F28
		public SqlBytes()
		{
			this.SetNull();
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x002C5B48 File Offset: 0x002C4F48
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

		// Token: 0x06002BE0 RID: 11232 RVA: 0x002C5BA8 File Offset: 0x002C4FA8
		public SqlBytes(SqlBinary value) : this(value.IsNull ? null : value.Value)
		{
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x002C5BD8 File Offset: 0x002C4FD8
		public SqlBytes(Stream s)
		{
			this.m_rgbBuf = null;
			this.m_lCurLen = -1L;
			this.m_stream = s;
			this.m_state = ((s == null) ? SqlBytesCharsState.Null : SqlBytesCharsState.Stream);
			this.m_rgbWorkBuf = null;
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x002C5C18 File Offset: 0x002C5018
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

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06002BE3 RID: 11235 RVA: 0x002C5C98 File Offset: 0x002C5098
		public bool IsNull
		{
			get
			{
				return this.m_state == SqlBytesCharsState.Null;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06002BE4 RID: 11236 RVA: 0x002C5CB8 File Offset: 0x002C50B8
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

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06002BE5 RID: 11237 RVA: 0x002C5CE8 File Offset: 0x002C50E8
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

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x002C5D28 File Offset: 0x002C5128
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

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06002BE7 RID: 11239 RVA: 0x002C5D58 File Offset: 0x002C5158
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

		// Token: 0x17000720 RID: 1824
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

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06002BEA RID: 11242 RVA: 0x002C5EB8 File Offset: 0x002C52B8
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

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06002BEB RID: 11243 RVA: 0x002C5EF8 File Offset: 0x002C52F8
		// (set) Token: 0x06002BEC RID: 11244 RVA: 0x002C5F28 File Offset: 0x002C5328
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

		// Token: 0x06002BED RID: 11245 RVA: 0x002C5F58 File Offset: 0x002C5358
		public void SetNull()
		{
			this.m_lCurLen = -1L;
			this.m_stream = null;
			this.m_state = SqlBytesCharsState.Null;
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x002C5F88 File Offset: 0x002C5388
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

		// Token: 0x06002BEF RID: 11247 RVA: 0x002C6008 File Offset: 0x002C5408
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

		// Token: 0x06002BF0 RID: 11248 RVA: 0x002C60E8 File Offset: 0x002C54E8
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

		// Token: 0x06002BF1 RID: 11249 RVA: 0x002C6248 File Offset: 0x002C5648
		public SqlBinary ToSqlBinary()
		{
			if (!this.IsNull)
			{
				return new SqlBinary(this.Value);
			}
			return SqlBinary.Null;
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x002C6278 File Offset: 0x002C5678
		public static explicit operator SqlBinary(SqlBytes value)
		{
			return value.ToSqlBinary();
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x002C6298 File Offset: 0x002C5698
		public static explicit operator SqlBytes(SqlBinary value)
		{
			return new SqlBytes(value);
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x002C62B8 File Offset: 0x002C56B8
		[Conditional("DEBUG")]
		private void AssertValid()
		{
			if (this.IsNull)
			{
			}
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x002C62D8 File Offset: 0x002C56D8
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

		// Token: 0x06002BF6 RID: 11254 RVA: 0x002C6378 File Offset: 0x002C5778
		internal bool FStream()
		{
			return this.m_state == SqlBytesCharsState.Stream;
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x002C6398 File Offset: 0x002C5798
		private void SetBuffer(byte[] buffer)
		{
			this.m_rgbBuf = buffer;
			this.m_lCurLen = ((this.m_rgbBuf == null) ? -1L : ((long)this.m_rgbBuf.Length));
			this.m_stream = null;
			this.m_state = ((this.m_rgbBuf == null) ? SqlBytesCharsState.Null : SqlBytesCharsState.Buffer);
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x002C63E8 File Offset: 0x002C57E8
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x002C63F8 File Offset: 0x002C57F8
		void IXmlSerializable.ReadXml(XmlReader r)
		{
			byte[] buffer = null;
			string attribute = r.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
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

		// Token: 0x06002BFA RID: 11258 RVA: 0x002C6468 File Offset: 0x002C5868
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

		// Token: 0x06002BFB RID: 11259 RVA: 0x002C64B8 File Offset: 0x002C58B8
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("base64Binary", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x002C64D8 File Offset: 0x002C58D8
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

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06002BFD RID: 11261 RVA: 0x002C6538 File Offset: 0x002C5938
		public static SqlBytes Null
		{
			get
			{
				return new SqlBytes(null);
			}
		}

		// Token: 0x04001C75 RID: 7285
		private const long x_lMaxLen = 2147483647L;

		// Token: 0x04001C76 RID: 7286
		private const long x_lNull = -1L;

		// Token: 0x04001C77 RID: 7287
		internal byte[] m_rgbBuf;

		// Token: 0x04001C78 RID: 7288
		private long m_lCurLen;

		// Token: 0x04001C79 RID: 7289
		private IntPtr m_pbData;

		// Token: 0x04001C7A RID: 7290
		internal Stream m_stream;

		// Token: 0x04001C7B RID: 7291
		private SqlBytesCharsState m_state;

		// Token: 0x04001C7C RID: 7292
		private byte[] m_rgbWorkBuf;
	}
}
