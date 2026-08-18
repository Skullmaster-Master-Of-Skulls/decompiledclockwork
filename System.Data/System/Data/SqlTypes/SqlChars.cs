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
	// Token: 0x02000346 RID: 838
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public sealed class SqlChars : INullable, IXmlSerializable, ISerializable
	{
		// Token: 0x06002C0F RID: 11279 RVA: 0x002C6A08 File Offset: 0x002C5E08
		public SqlChars()
		{
			this.SetNull();
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x002C6A28 File Offset: 0x002C5E28
		public SqlChars(char[] buffer)
		{
			this.m_rgchBuf = buffer;
			this.m_stream = null;
			if (this.m_rgchBuf == null)
			{
				this.m_state = SqlBytesCharsState.Null;
				this.m_lCurLen = -1L;
			}
			else
			{
				this.m_state = SqlBytesCharsState.Buffer;
				this.m_lCurLen = (long)this.m_rgchBuf.Length;
			}
			this.m_rgchWorkBuf = null;
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x002C6A88 File Offset: 0x002C5E88
		public SqlChars(SqlString value) : this(value.IsNull ? null : value.Value.ToCharArray())
		{
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x002C6AB8 File Offset: 0x002C5EB8
		internal SqlChars(SqlStreamChars s)
		{
			this.m_rgchBuf = null;
			this.m_lCurLen = -1L;
			this.m_stream = s;
			this.m_state = ((s == null) ? SqlBytesCharsState.Null : SqlBytesCharsState.Stream);
			this.m_rgchWorkBuf = null;
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x002C6AF8 File Offset: 0x002C5EF8
		private SqlChars(SerializationInfo info, StreamingContext context)
		{
			this.m_stream = null;
			this.m_rgchWorkBuf = null;
			if (info.GetBoolean("IsNull"))
			{
				this.m_state = SqlBytesCharsState.Null;
				this.m_rgchBuf = null;
				return;
			}
			this.m_state = SqlBytesCharsState.Buffer;
			this.m_rgchBuf = (char[])info.GetValue("data", typeof(char[]));
			this.m_lCurLen = (long)this.m_rgchBuf.Length;
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06002C14 RID: 11284 RVA: 0x002C6B78 File Offset: 0x002C5F78
		public bool IsNull
		{
			get
			{
				return this.m_state == SqlBytesCharsState.Null;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06002C15 RID: 11285 RVA: 0x002C6B98 File Offset: 0x002C5F98
		public char[] Buffer
		{
			get
			{
				if (this.FStream())
				{
					this.CopyStreamToBuffer();
				}
				return this.m_rgchBuf;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06002C16 RID: 11286 RVA: 0x002C6BC8 File Offset: 0x002C5FC8
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

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06002C17 RID: 11287 RVA: 0x002C6C08 File Offset: 0x002C6008
		public long MaxLength
		{
			get
			{
				SqlBytesCharsState state = this.m_state;
				if (state == SqlBytesCharsState.Stream)
				{
					return -1L;
				}
				if (this.m_rgchBuf != null)
				{
					return (long)this.m_rgchBuf.Length;
				}
				return -1L;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06002C18 RID: 11288 RVA: 0x002C6C38 File Offset: 0x002C6038
		public char[] Value
		{
			get
			{
				SqlBytesCharsState state = this.m_state;
				if (state != SqlBytesCharsState.Null)
				{
					char[] array;
					if (state != SqlBytesCharsState.Stream)
					{
						array = new char[this.m_lCurLen];
						Array.Copy(this.m_rgchBuf, array, (int)this.m_lCurLen);
					}
					else
					{
						if (this.m_stream.Length > 2147483647L)
						{
							throw new SqlTypeException(Res.GetString("SqlMisc_BufferInsufficientMessage"));
						}
						array = new char[this.m_stream.Length];
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

		// Token: 0x1700072E RID: 1838
		public char this[long offset]
		{
			get
			{
				if (offset < 0L || offset >= this.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				if (this.m_rgchWorkBuf == null)
				{
					this.m_rgchWorkBuf = new char[1];
				}
				this.Read(offset, this.m_rgchWorkBuf, 0, 1);
				return this.m_rgchWorkBuf[0];
			}
			set
			{
				if (this.m_rgchWorkBuf == null)
				{
					this.m_rgchWorkBuf = new char[1];
				}
				this.m_rgchWorkBuf[0] = value;
				this.Write(offset, this.m_rgchWorkBuf, 0, 1);
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06002C1B RID: 11291 RVA: 0x002C6D98 File Offset: 0x002C6198
		// (set) Token: 0x06002C1C RID: 11292 RVA: 0x002C6DC8 File Offset: 0x002C61C8
		internal SqlStreamChars Stream
		{
			get
			{
				if (!this.FStream())
				{
					return new StreamOnSqlChars(this);
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

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06002C1D RID: 11293 RVA: 0x002C6DF8 File Offset: 0x002C61F8
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

		// Token: 0x06002C1E RID: 11294 RVA: 0x002C6E38 File Offset: 0x002C6238
		public void SetNull()
		{
			this.m_lCurLen = -1L;
			this.m_stream = null;
			this.m_state = SqlBytesCharsState.Null;
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x002C6E68 File Offset: 0x002C6268
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
			if (this.m_rgchBuf == null)
			{
				throw new SqlTypeException(Res.GetString("SqlMisc_NoBufferMessage"));
			}
			if (value > (long)this.m_rgchBuf.Length)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			if (this.IsNull)
			{
				this.m_state = SqlBytesCharsState.Buffer;
			}
			this.m_lCurLen = value;
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x002C6EE8 File Offset: 0x002C62E8
		public long Read(long offset, char[] buffer, int offsetInBuffer, int count)
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
					Array.Copy(this.m_rgchBuf, offset, buffer, (long)offsetInBuffer, (long)count);
				}
			}
			return (long)count;
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x002C6FC8 File Offset: 0x002C63C8
		public void Write(long offset, char[] buffer, int offsetInBuffer, int count)
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
			if (this.m_rgchBuf == null)
			{
				throw new SqlTypeException(Res.GetString("SqlMisc_NoBufferMessage"));
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset > (long)this.m_rgchBuf.Length)
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
			if ((long)count > (long)this.m_rgchBuf.Length - offset)
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
				Array.Copy(buffer, (long)offsetInBuffer, this.m_rgchBuf, offset, (long)count);
				if (this.m_lCurLen < offset + (long)count)
				{
					this.m_lCurLen = offset + (long)count;
				}
			}
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x002C7128 File Offset: 0x002C6528
		public SqlString ToSqlString()
		{
			if (!this.IsNull)
			{
				return new string(this.Value);
			}
			return SqlString.Null;
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x002C7158 File Offset: 0x002C6558
		public static explicit operator SqlString(SqlChars value)
		{
			return value.ToSqlString();
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x002C7178 File Offset: 0x002C6578
		public static explicit operator SqlChars(SqlString value)
		{
			return new SqlChars(value);
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x002C7198 File Offset: 0x002C6598
		[Conditional("DEBUG")]
		private void AssertValid()
		{
			if (this.IsNull)
			{
			}
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x002C71B8 File Offset: 0x002C65B8
		internal bool FStream()
		{
			return this.m_state == SqlBytesCharsState.Stream;
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x002C71D8 File Offset: 0x002C65D8
		private void CopyStreamToBuffer()
		{
			long length = this.m_stream.Length;
			if (length >= 2147483647L)
			{
				throw new SqlTypeException(Res.GetString("SqlMisc_BufferInsufficientMessage"));
			}
			if (this.m_rgchBuf == null || (long)this.m_rgchBuf.Length < length)
			{
				this.m_rgchBuf = new char[length];
			}
			if (this.m_stream.Position != 0L)
			{
				this.m_stream.Seek(0L, SeekOrigin.Begin);
			}
			this.m_stream.Read(this.m_rgchBuf, 0, (int)length);
			this.m_stream = null;
			this.m_lCurLen = length;
			this.m_state = SqlBytesCharsState.Buffer;
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x002C7278 File Offset: 0x002C6678
		private void SetBuffer(char[] buffer)
		{
			this.m_rgchBuf = buffer;
			this.m_lCurLen = ((this.m_rgchBuf == null) ? -1L : ((long)this.m_rgchBuf.Length));
			this.m_stream = null;
			this.m_state = ((this.m_rgchBuf == null) ? SqlBytesCharsState.Null : SqlBytesCharsState.Buffer);
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x002C72C8 File Offset: 0x002C66C8
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x002C72D8 File Offset: 0x002C66D8
		void IXmlSerializable.ReadXml(XmlReader r)
		{
			string attribute = r.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				this.SetNull();
				return;
			}
			char[] buffer = r.ReadElementString().ToCharArray();
			this.SetBuffer(buffer);
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x002C7328 File Offset: 0x002C6728
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			char[] buffer = this.Buffer;
			writer.WriteString(new string(buffer, 0, (int)this.Length));
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x002C7378 File Offset: 0x002C6778
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x002C7398 File Offset: 0x002C6798
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
			info.AddValue("data", this.m_rgchBuf);
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06002C2E RID: 11310 RVA: 0x002C73F8 File Offset: 0x002C67F8
		public static SqlChars Null
		{
			get
			{
				return new SqlChars(null);
			}
		}

		// Token: 0x04001C7F RID: 7295
		private const long x_lMaxLen = 2147483647L;

		// Token: 0x04001C80 RID: 7296
		private const long x_lNull = -1L;

		// Token: 0x04001C81 RID: 7297
		internal char[] m_rgchBuf;

		// Token: 0x04001C82 RID: 7298
		private long m_lCurLen;

		// Token: 0x04001C83 RID: 7299
		private IntPtr m_pchData;

		// Token: 0x04001C84 RID: 7300
		internal SqlStreamChars m_stream;

		// Token: 0x04001C85 RID: 7301
		private SqlBytesCharsState m_state;

		// Token: 0x04001C86 RID: 7302
		private char[] m_rgchWorkBuf;
	}
}
