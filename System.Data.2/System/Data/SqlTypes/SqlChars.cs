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
	// Token: 0x02000156 RID: 342
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public sealed class SqlChars : INullable, IXmlSerializable, ISerializable
	{
		// Token: 0x06001451 RID: 5201 RVA: 0x0009D0D8 File Offset: 0x0009C4D8
		public SqlChars()
		{
			this.SetNull();
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0009D0F4 File Offset: 0x0009C4F4
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

		// Token: 0x06001453 RID: 5203 RVA: 0x0009D14C File Offset: 0x0009C54C
		public SqlChars(SqlString value) : this(value.IsNull ? null : value.Value.ToCharArray())
		{
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0009D178 File Offset: 0x0009C578
		internal SqlChars(SqlStreamChars s)
		{
			this.m_rgchBuf = null;
			this.m_lCurLen = -1L;
			this.m_stream = s;
			this.m_state = ((s == null) ? SqlBytesCharsState.Null : SqlBytesCharsState.Stream);
			this.m_rgchWorkBuf = null;
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0009D1B8 File Offset: 0x0009C5B8
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

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x0009D22C File Offset: 0x0009C62C
		public bool IsNull
		{
			get
			{
				return this.m_state == SqlBytesCharsState.Null;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x0009D244 File Offset: 0x0009C644
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

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x0009D268 File Offset: 0x0009C668
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

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x0009D2A0 File Offset: 0x0009C6A0
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

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x0009D2D0 File Offset: 0x0009C6D0
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

		// Token: 0x17000300 RID: 768
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

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x0009D414 File Offset: 0x0009C814
		// (set) Token: 0x0600145E RID: 5214 RVA: 0x0009D438 File Offset: 0x0009C838
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

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x0009D464 File Offset: 0x0009C864
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

		// Token: 0x06001460 RID: 5216 RVA: 0x0009D49C File Offset: 0x0009C89C
		public void SetNull()
		{
			this.m_lCurLen = -1L;
			this.m_stream = null;
			this.m_state = SqlBytesCharsState.Null;
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0009D4C0 File Offset: 0x0009C8C0
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

		// Token: 0x06001462 RID: 5218 RVA: 0x0009D538 File Offset: 0x0009C938
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

		// Token: 0x06001463 RID: 5219 RVA: 0x0009D614 File Offset: 0x0009CA14
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

		// Token: 0x06001464 RID: 5220 RVA: 0x0009D768 File Offset: 0x0009CB68
		public SqlString ToSqlString()
		{
			if (!this.IsNull)
			{
				return new string(this.Value);
			}
			return SqlString.Null;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0009D794 File Offset: 0x0009CB94
		public static explicit operator SqlString(SqlChars value)
		{
			return value.ToSqlString();
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0009D7A8 File Offset: 0x0009CBA8
		public static explicit operator SqlChars(SqlString value)
		{
			return new SqlChars(value);
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0009D7BC File Offset: 0x0009CBBC
		[Conditional("DEBUG")]
		private void AssertValid()
		{
			bool isNull = this.IsNull;
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0009D7D0 File Offset: 0x0009CBD0
		internal bool FStream()
		{
			return this.m_state == SqlBytesCharsState.Stream;
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0009D7E8 File Offset: 0x0009CBE8
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

		// Token: 0x0600146A RID: 5226 RVA: 0x0009D884 File Offset: 0x0009CC84
		private void SetBuffer(char[] buffer)
		{
			this.m_rgchBuf = buffer;
			this.m_lCurLen = ((this.m_rgchBuf == null) ? -1L : ((long)this.m_rgchBuf.Length));
			this.m_stream = null;
			this.m_state = ((this.m_rgchBuf == null) ? SqlBytesCharsState.Null : SqlBytesCharsState.Buffer);
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x0009D8CC File Offset: 0x0009CCCC
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0009D8DC File Offset: 0x0009CCDC
		void IXmlSerializable.ReadXml(XmlReader r)
		{
			string attribute = r.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				r.ReadElementString();
				this.SetNull();
				return;
			}
			char[] buffer = r.ReadElementString().ToCharArray();
			this.SetBuffer(buffer);
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0009D928 File Offset: 0x0009CD28
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

		// Token: 0x0600146E RID: 5230 RVA: 0x0009D974 File Offset: 0x0009CD74
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0009D990 File Offset: 0x0009CD90
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

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x0009D9EC File Offset: 0x0009CDEC
		public static SqlChars Null
		{
			get
			{
				return new SqlChars(null);
			}
		}

		// Token: 0x04000D69 RID: 3433
		internal char[] m_rgchBuf;

		// Token: 0x04000D6A RID: 3434
		private long m_lCurLen;

		// Token: 0x04000D6B RID: 3435
		internal SqlStreamChars m_stream;

		// Token: 0x04000D6C RID: 3436
		private SqlBytesCharsState m_state;

		// Token: 0x04000D6D RID: 3437
		private char[] m_rgchWorkBuf;

		// Token: 0x04000D6E RID: 3438
		private const long x_lMaxLen = 2147483647L;

		// Token: 0x04000D6F RID: 3439
		private const long x_lNull = -1L;
	}
}
