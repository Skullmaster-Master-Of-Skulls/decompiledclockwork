using System;
using System.Collections.Immutable;
using System.IO;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000005 RID: 5
	internal struct BlobWriter
	{
		// Token: 0x0600005D RID: 93 RVA: 0x000025F0 File Offset: 0x000007F0
		public BlobWriter(int size)
		{
			this = new BlobWriter(new byte[size]);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000025FE File Offset: 0x000007FE
		public BlobWriter(byte[] buffer)
		{
			this = new BlobWriter(buffer, 0, buffer.Length);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000260B File Offset: 0x0000080B
		public BlobWriter(Blob blob)
		{
			this = new BlobWriter(blob.Buffer, blob.Start, blob.Length);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002626 File Offset: 0x00000826
		public BlobWriter(byte[] buffer, int start, int count)
		{
			this._buffer = buffer;
			this._start = start;
			this._position = start;
			this._end = start + count;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002646 File Offset: 0x00000846
		internal bool IsDefault
		{
			get
			{
				return this._buffer == null;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002651 File Offset: 0x00000851
		public bool ContentEquals(BlobWriter other)
		{
			return this.Length == other.Length && ByteSequenceComparer.Equals(this._buffer, this._start, other._buffer, other._start, this.Length);
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002687 File Offset: 0x00000887
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002696 File Offset: 0x00000896
		public int Offset
		{
			get
			{
				return this._position - this._start;
			}
			set
			{
				if (value < 0 || this._start > this._end - value)
				{
					BlobWriter.ValueArgumentOutOfRange();
				}
				this._position = this._start + value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000026BF File Offset: 0x000008BF
		public int Length
		{
			get
			{
				return this._end - this._start;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000026CE File Offset: 0x000008CE
		public int RemainingBytes
		{
			get
			{
				return this._end - this._position;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000026DD File Offset: 0x000008DD
		public Blob Blob
		{
			get
			{
				return new Blob(this._buffer, this._start, this.Length);
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000026F6 File Offset: 0x000008F6
		public byte[] ToArray()
		{
			return this.ToArray(0, this.Offset);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002708 File Offset: 0x00000908
		public byte[] ToArray(int start, int byteCount)
		{
			BlobUtilities.ValidateRange(this.Length, start, byteCount);
			byte[] array = new byte[byteCount];
			Buffer.BlockCopy(this._buffer, this._start + start, array, 0, byteCount);
			return array;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002740 File Offset: 0x00000940
		public ImmutableArray<byte> ToImmutableArray()
		{
			return this.ToImmutableArray(0, this.Offset);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002750 File Offset: 0x00000950
		public ImmutableArray<byte> ToImmutableArray(int start, int byteCount)
		{
			byte[] array = this.ToArray(start, byteCount);
			return ImmutableByteArrayInterop.DangerousCreateFromUnderlyingArray(ref array);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002770 File Offset: 0x00000970
		private int Advance(int value)
		{
			int position = this._position;
			if (position > this._end - value)
			{
				BlobWriter.ThrowOutOfBounds();
			}
			this._position = position + value;
			return position;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000027A0 File Offset: 0x000009A0
		public unsafe void WriteBytes(byte value, int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount");
			}
			int num = this.Advance(byteCount);
			fixed (byte* buffer = this._buffer)
			{
				byte* ptr = buffer + num;
				for (int i = 0; i < byteCount; i++)
				{
					ptr[i] = value;
				}
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000027FE File Offset: 0x000009FE
		public unsafe void WriteBytes(byte* buffer, int byteCount)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount");
			}
			this.WriteBytesUnchecked(buffer, byteCount);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002828 File Offset: 0x00000A28
		private unsafe void WriteBytesUnchecked(byte* buffer, int byteCount)
		{
			int startIndex = this.Advance(byteCount);
			Marshal.Copy((IntPtr)((void*)buffer), this._buffer, startIndex, byteCount);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002850 File Offset: 0x00000A50
		public void WriteBytes(BlobBuilder source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			source.WriteContentTo(ref this);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002868 File Offset: 0x00000A68
		public int WriteBytes(Stream source, int byteCount)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount");
			}
			int num = this.Advance(byteCount);
			int num2 = source.TryReadAll(this._buffer, num, byteCount);
			this._position = num + num2;
			return num2;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000028B3 File Offset: 0x00000AB3
		public void WriteBytes(ImmutableArray<byte> buffer)
		{
			this.WriteBytes(buffer, 0, buffer.IsDefault ? 0 : buffer.Length);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000028D0 File Offset: 0x00000AD0
		public void WriteBytes(ImmutableArray<byte> buffer, int start, int byteCount)
		{
			this.WriteBytes(ImmutableByteArrayInterop.DangerousGetUnderlyingArray(buffer), start, byteCount);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000028E0 File Offset: 0x00000AE0
		public void WriteBytes(byte[] buffer)
		{
			this.WriteBytes(buffer, 0, (buffer != null) ? buffer.Length : 0);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000028F4 File Offset: 0x00000AF4
		public unsafe void WriteBytes(byte[] buffer, int start, int byteCount)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			BlobUtilities.ValidateRange(buffer.Length, start, byteCount);
			if (buffer.Length == 0)
			{
				return;
			}
			fixed (byte* ptr = buffer)
			{
				this.WriteBytes(ptr + start, byteCount);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002943 File Offset: 0x00000B43
		public void PadTo(int offset)
		{
			this.WriteBytes(0, offset - this.Offset);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002954 File Offset: 0x00000B54
		public void Align(int alignment)
		{
			int offset = this.Offset;
			this.WriteBytes(0, BitArithmetic.Align(offset, alignment) - offset);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002978 File Offset: 0x00000B78
		public void WriteBoolean(bool value)
		{
			this.WriteByte(value ? 1 : 0);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002988 File Offset: 0x00000B88
		public void WriteByte(byte value)
		{
			int num = this.Advance(1);
			this._buffer[num] = value;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000029A6 File Offset: 0x00000BA6
		public void WriteSByte(sbyte value)
		{
			this.WriteByte((byte)value);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000029B0 File Offset: 0x00000BB0
		public void WriteDouble(double value)
		{
			int start = this.Advance(8);
			this._buffer.WriteDouble(start, value);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000029D4 File Offset: 0x00000BD4
		public void WriteSingle(float value)
		{
			int start = this.Advance(4);
			this._buffer.WriteSingle(start, value);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000029F6 File Offset: 0x00000BF6
		public void WriteInt16(short value)
		{
			this.WriteUInt16((ushort)value);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002A00 File Offset: 0x00000C00
		public void WriteUInt16(ushort value)
		{
			int start = this.Advance(2);
			this._buffer.WriteUInt16(start, value);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002A22 File Offset: 0x00000C22
		public void WriteInt16BE(short value)
		{
			this.WriteUInt16BE((ushort)value);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002A2C File Offset: 0x00000C2C
		public void WriteUInt16BE(ushort value)
		{
			int start = this.Advance(2);
			this._buffer.WriteUInt16BE(start, value);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002A4E File Offset: 0x00000C4E
		public void WriteInt32BE(int value)
		{
			this.WriteUInt32BE((uint)value);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002A58 File Offset: 0x00000C58
		public void WriteUInt32BE(uint value)
		{
			int start = this.Advance(4);
			this._buffer.WriteUInt32BE(start, value);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002A7A File Offset: 0x00000C7A
		public void WriteInt32(int value)
		{
			this.WriteUInt32((uint)value);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002A84 File Offset: 0x00000C84
		public void WriteUInt32(uint value)
		{
			int start = this.Advance(4);
			this._buffer.WriteUInt32(start, value);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002AA6 File Offset: 0x00000CA6
		public void WriteInt64(long value)
		{
			this.WriteUInt64((ulong)value);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public void WriteUInt64(ulong value)
		{
			int start = this.Advance(8);
			this._buffer.WriteUInt64(start, value);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public void WriteDecimal(decimal value)
		{
			int start = this.Advance(13);
			this._buffer.WriteDecimal(start, value);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002AF7 File Offset: 0x00000CF7
		public void WriteDateTime(DateTime value)
		{
			this.WriteInt64(value.Ticks);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002B06 File Offset: 0x00000D06
		public void WriteReference(uint reference, int size)
		{
			if (size == 2)
			{
				this.WriteUInt16((ushort)reference);
				return;
			}
			this.WriteUInt32(reference);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002B1C File Offset: 0x00000D1C
		public unsafe void WriteUTF16(char[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length == 0)
			{
				return;
			}
			fixed (char* ptr = value)
			{
				this.WriteBytesUnchecked((byte*)ptr, value.Length * 2);
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002B64 File Offset: 0x00000D64
		public unsafe void WriteUTF16(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				this.WriteBytesUnchecked((byte*)ptr, value.Length * 2);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002BA0 File Offset: 0x00000DA0
		public void WriteSerializedString(string str)
		{
			if (str == null)
			{
				this.WriteByte(byte.MaxValue);
				return;
			}
			this.WriteUTF8(str, 0, str.Length, true, true);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002BC1 File Offset: 0x00000DC1
		public void WriteUTF8(string value, bool allowUnpairedSurrogates)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.WriteUTF8(value, 0, value.Length, allowUnpairedSurrogates, false);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002BE4 File Offset: 0x00000DE4
		private unsafe void WriteUTF8(string str, int start, int length, bool allowUnpairedSurrogates, bool prependSize)
		{
			fixed (string text = str)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr + start;
				int utf8ByteCount = BlobUtilities.GetUTF8ByteCount(ptr2, length);
				if (prependSize)
				{
					this.WriteCompressedInteger(utf8ByteCount);
				}
				int start2 = this.Advance(utf8ByteCount);
				this._buffer.WriteUTF8(start2, ptr2, length, utf8ByteCount, allowUnpairedSurrogates);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002C38 File Offset: 0x00000E38
		public void WriteCompressedSignedInteger(int value)
		{
			BlobWriterImpl.WriteCompressedSignedInteger(ref this, value);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002C41 File Offset: 0x00000E41
		public void WriteCompressedInteger(int value)
		{
			BlobWriterImpl.WriteCompressedInteger(ref this, value);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002C4A File Offset: 0x00000E4A
		public void WriteConstant(object value)
		{
			BlobWriterImpl.WriteConstant(ref this, value);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002C53 File Offset: 0x00000E53
		public void Clear()
		{
			this._position = this._start;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002C61 File Offset: 0x00000E61
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void ThrowOutOfBounds()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002C68 File Offset: 0x00000E68
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void ValueArgumentOutOfRange()
		{
			throw new ArgumentOutOfRangeException("value");
		}

		// Token: 0x04000006 RID: 6
		private readonly byte[] _buffer;

		// Token: 0x04000007 RID: 7
		private readonly int _start;

		// Token: 0x04000008 RID: 8
		private readonly int _end;

		// Token: 0x04000009 RID: 9
		private int _position;
	}
}
