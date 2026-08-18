using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000007 RID: 7
	[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
	internal class BlobBuilder
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000315E File Offset: 0x0000135E
		private BlobBuilder FirstChunk
		{
			get
			{
				return this._nextOrPrevious._nextOrPrevious;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000316B File Offset: 0x0000136B
		private bool IsHead
		{
			get
			{
				return (this._length & 2147483648U) == 0U;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000317C File Offset: 0x0000137C
		private int Length
		{
			get
			{
				return (int)(this._length & 2147483647U);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x0000318A File Offset: 0x0000138A
		private uint FrozenLength
		{
			get
			{
				return this._length | 2147483648U;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003198 File Offset: 0x00001398
		public BlobBuilder(int size = 256)
		{
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (!BitConverter.IsLittleEndian)
			{
				throw new PlatformNotSupportedException();
			}
			this._nextOrPrevious = this;
			this._buffer = new byte[Math.Max(16, size)];
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000031D6 File Offset: 0x000013D6
		protected virtual BlobBuilder AllocateChunk(int minimalSize)
		{
			return new BlobBuilder(Math.Max(this._buffer.Length, minimalSize));
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000031EB File Offset: 0x000013EB
		protected virtual void FreeChunk()
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000031F0 File Offset: 0x000013F0
		public void Clear()
		{
			if (!this.IsHead)
			{
				BlobBuilder.ThrowHeadRequired();
			}
			BlobBuilder firstChunk = this.FirstChunk;
			if (firstChunk != this)
			{
				byte[] buffer = firstChunk._buffer;
				firstChunk._length = this.FrozenLength;
				firstChunk._buffer = this._buffer;
				this._buffer = buffer;
			}
			foreach (BlobBuilder blobBuilder in this.GetChunks())
			{
				if (blobBuilder != this)
				{
					blobBuilder.ClearChunk();
					blobBuilder.FreeChunk();
				}
			}
			this.ClearChunk();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003298 File Offset: 0x00001498
		protected void Free()
		{
			this.Clear();
			this.FreeChunk();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000032A6 File Offset: 0x000014A6
		internal void ClearChunk()
		{
			this._length = 0U;
			this._previousLength = 0;
			this._nextOrPrevious = this;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000031EB File Offset: 0x000013EB
		private void CheckInvariants()
		{
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00002C61 File Offset: 0x00000E61
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowHeadRequired()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000032BD File Offset: 0x000014BD
		public int Count
		{
			get
			{
				return this._previousLength + this.Length;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000032CC File Offset: 0x000014CC
		internal int Position
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000032D4 File Offset: 0x000014D4
		private int FreeBytes
		{
			get
			{
				return this._buffer.Length - this.Length;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000032E5 File Offset: 0x000014E5
		internal int BufferSize
		{
			get
			{
				return this._buffer.Length;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000032EF File Offset: 0x000014EF
		internal BlobBuilder.Chunks GetChunks()
		{
			if (!this.IsHead)
			{
				BlobBuilder.ThrowHeadRequired();
			}
			return new BlobBuilder.Chunks(this);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003304 File Offset: 0x00001504
		public BlobBuilder.Blobs GetBlobs()
		{
			if (!this.IsHead)
			{
				BlobBuilder.ThrowHeadRequired();
			}
			return new BlobBuilder.Blobs(this);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000331C File Offset: 0x0000151C
		public bool ContentEquals(BlobBuilder other)
		{
			if (!this.IsHead)
			{
				BlobBuilder.ThrowHeadRequired();
			}
			if (this == other)
			{
				return true;
			}
			if (other == null)
			{
				return false;
			}
			if (!other.IsHead)
			{
				BlobBuilder.ThrowHeadRequired();
			}
			if (this.Count != other.Count)
			{
				return false;
			}
			BlobBuilder.Chunks chunks = this.GetChunks();
			BlobBuilder.Chunks chunks2 = other.GetChunks();
			int num = 0;
			int num2 = 0;
			bool flag = chunks.MoveNext();
			bool flag2 = chunks2.MoveNext();
			while (flag && flag2)
			{
				BlobBuilder blobBuilder = chunks.Current;
				BlobBuilder blobBuilder2 = chunks2.Current;
				int num3 = Math.Min(blobBuilder.Length - num, blobBuilder2.Length - num2);
				if (!ByteSequenceComparer.Equals(blobBuilder._buffer, num, blobBuilder2._buffer, num2, num3))
				{
					return false;
				}
				num += num3;
				num2 += num3;
				if (num == blobBuilder.Length)
				{
					flag = chunks.MoveNext();
					num = 0;
				}
				if (num2 == blobBuilder2.Length)
				{
					flag2 = chunks2.MoveNext();
					num2 = 0;
				}
			}
			return flag == flag2;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000340E File Offset: 0x0000160E
		public byte[] ToArray()
		{
			return this.ToArray(0, this.Count);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003420 File Offset: 0x00001620
		public byte[] ToArray(int start, int byteCount)
		{
			BlobUtilities.ValidateRange(this.Count, start, byteCount);
			byte[] array = new byte[byteCount];
			int num = 0;
			int num2 = 0;
			foreach (BlobBuilder blobBuilder in this.GetChunks())
			{
				int num3 = num + blobBuilder.Length;
				if (num3 > start)
				{
					int num4 = Math.Min(blobBuilder.Length, array.Length - num2);
					if (num4 == 0)
					{
						break;
					}
					Buffer.BlockCopy(blobBuilder._buffer, Math.Max(start - num, 0), array, num2, num4);
					num2 += num4;
				}
				num = num3;
			}
			return array;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000034D4 File Offset: 0x000016D4
		public ImmutableArray<byte> ToImmutableArray()
		{
			return this.ToImmutableArray(0, this.Count);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000034E4 File Offset: 0x000016E4
		public ImmutableArray<byte> ToImmutableArray(int start, int byteCount)
		{
			byte[] array = this.ToArray(start, byteCount);
			return ImmutableByteArrayInterop.DangerousCreateFromUnderlyingArray(ref array);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003504 File Offset: 0x00001704
		public void WriteContentTo(Stream destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			foreach (BlobBuilder blobBuilder in this.GetChunks())
			{
				destination.Write(blobBuilder._buffer, 0, blobBuilder.Length);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003574 File Offset: 0x00001774
		public void WriteContentTo(ref BlobWriter destination)
		{
			if (destination.IsDefault)
			{
				throw new ArgumentNullException("destination");
			}
			foreach (BlobBuilder blobBuilder in this.GetChunks())
			{
				destination.WriteBytes(blobBuilder._buffer, 0, blobBuilder.Length);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000035EC File Offset: 0x000017EC
		public void WriteContentTo(BlobBuilder destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			foreach (BlobBuilder blobBuilder in this.GetChunks())
			{
				destination.WriteBytes(blobBuilder._buffer, 0, blobBuilder.Length);
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000365C File Offset: 0x0000185C
		public void LinkPrefix(BlobBuilder prefix)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			if (!prefix.IsHead || !this.IsHead)
			{
				BlobBuilder.ThrowHeadRequired();
			}
			if (prefix.Count == 0)
			{
				return;
			}
			this._previousLength += prefix.Count;
			prefix._length = prefix.FrozenLength;
			BlobBuilder firstChunk = this.FirstChunk;
			BlobBuilder firstChunk2 = prefix.FirstChunk;
			BlobBuilder nextOrPrevious = this._nextOrPrevious;
			BlobBuilder nextOrPrevious2 = prefix._nextOrPrevious;
			this._nextOrPrevious = ((nextOrPrevious != this) ? nextOrPrevious : prefix);
			prefix._nextOrPrevious = ((firstChunk != this) ? firstChunk : ((firstChunk2 != prefix) ? firstChunk2 : prefix));
			if (nextOrPrevious != this)
			{
				nextOrPrevious._nextOrPrevious = ((firstChunk2 != prefix) ? firstChunk2 : prefix);
			}
			if (nextOrPrevious2 != prefix)
			{
				nextOrPrevious2._nextOrPrevious = prefix;
			}
			prefix.CheckInvariants();
			this.CheckInvariants();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000371C File Offset: 0x0000191C
		public void LinkSuffix(BlobBuilder suffix)
		{
			if (suffix == null)
			{
				throw new ArgumentNullException("suffix");
			}
			if (!this.IsHead || !suffix.IsHead)
			{
				BlobBuilder.ThrowHeadRequired();
			}
			if (suffix.Count == 0)
			{
				return;
			}
			byte[] buffer = suffix._buffer;
			uint length = suffix._length;
			suffix._buffer = this._buffer;
			suffix._length = this.FrozenLength;
			this._buffer = buffer;
			this._length = length;
			int previousLength = suffix._previousLength;
			suffix._previousLength = this._previousLength;
			this._previousLength = this._previousLength + suffix.Length + previousLength;
			BlobBuilder firstChunk = this.FirstChunk;
			BlobBuilder firstChunk2 = suffix.FirstChunk;
			BlobBuilder nextOrPrevious = this._nextOrPrevious;
			BlobBuilder nextOrPrevious2 = suffix._nextOrPrevious;
			this._nextOrPrevious = nextOrPrevious2;
			suffix._nextOrPrevious = ((firstChunk2 != suffix) ? firstChunk2 : ((firstChunk != this) ? firstChunk : suffix));
			if (nextOrPrevious != this)
			{
				nextOrPrevious._nextOrPrevious = suffix;
			}
			if (nextOrPrevious2 != suffix)
			{
				nextOrPrevious2._nextOrPrevious = ((firstChunk != this) ? firstChunk : suffix);
			}
			this.CheckInvariants();
			suffix.CheckInvariants();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000381C File Offset: 0x00001A1C
		private void AddLength(int value)
		{
			this._length += (uint)value;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000382C File Offset: 0x00001A2C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Expand(int newLength)
		{
			if (!this.IsHead)
			{
				BlobBuilder.ThrowHeadRequired();
			}
			BlobBuilder blobBuilder = this.AllocateChunk(Math.Max(newLength, 16));
			if (blobBuilder.BufferSize < newLength)
			{
				throw new InvalidOperationException();
			}
			byte[] buffer = blobBuilder._buffer;
			if (this._length == 0U)
			{
				blobBuilder._buffer = this._buffer;
				this._buffer = buffer;
			}
			else
			{
				BlobBuilder nextOrPrevious = this._nextOrPrevious;
				BlobBuilder firstChunk = this.FirstChunk;
				if (nextOrPrevious == this)
				{
					this._nextOrPrevious = blobBuilder;
				}
				else
				{
					blobBuilder._nextOrPrevious = firstChunk;
					nextOrPrevious._nextOrPrevious = blobBuilder;
					this._nextOrPrevious = blobBuilder;
				}
				blobBuilder._buffer = this._buffer;
				blobBuilder._length = this.FrozenLength;
				blobBuilder._previousLength = this._previousLength;
				this._buffer = buffer;
				this._previousLength += this.Length;
				this._length = 0U;
			}
			this.CheckInvariants();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003904 File Offset: 0x00001B04
		public Blob ReserveBytes(int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount");
			}
			int start = this.ReserveBytesImpl(byteCount);
			return new Blob(this._buffer, start, byteCount);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003938 File Offset: 0x00001B38
		private int ReserveBytesImpl(int byteCount)
		{
			uint num = this._length;
			if ((ulong)num > (ulong)((long)(this._buffer.Length - byteCount)))
			{
				this.Expand(byteCount);
				num = 0U;
			}
			this._length = num + (uint)byteCount;
			return (int)num;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000396E File Offset: 0x00001B6E
		private int ReserveBytesPrimitive(int byteCount)
		{
			return this.ReserveBytesImpl(byteCount);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003978 File Offset: 0x00001B78
		public void WriteBytes(byte value, int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount");
			}
			if (!this.IsHead)
			{
				BlobBuilder.ThrowHeadRequired();
			}
			int num = Math.Min(this.FreeBytes, byteCount);
			this._buffer.WriteBytes(this.Length, value, num);
			this.AddLength(num);
			int num2 = byteCount - num;
			if (num2 > 0)
			{
				this.Expand(num2);
				this._buffer.WriteBytes(0, value, num2);
				this.AddLength(num2);
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000039EC File Offset: 0x00001BEC
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
			if (!this.IsHead)
			{
				BlobBuilder.ThrowHeadRequired();
			}
			this.WriteBytesUnchecked(buffer, byteCount);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003A24 File Offset: 0x00001C24
		private unsafe void WriteBytesUnchecked(byte* buffer, int byteCount)
		{
			int num = Math.Min(this.FreeBytes, byteCount);
			Marshal.Copy((IntPtr)((void*)buffer), this._buffer, this.Length, num);
			this.AddLength(num);
			int num2 = byteCount - num;
			if (num2 > 0)
			{
				this.Expand(num2);
				Marshal.Copy((IntPtr)((void*)(buffer + num)), this._buffer, 0, num2);
				this.AddLength(num2);
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003A88 File Offset: 0x00001C88
		public int TryWriteBytes(Stream source, int byteCount)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount");
			}
			if (byteCount == 0)
			{
				return 0;
			}
			int num = 0;
			int num2 = Math.Min(this.FreeBytes, byteCount);
			if (num2 > 0)
			{
				num = source.TryReadAll(this._buffer, this.Length, num2);
				this.AddLength(num);
				if (num != num2)
				{
					return num;
				}
			}
			int num3 = byteCount - num2;
			if (num3 > 0)
			{
				this.Expand(num3);
				num = source.TryReadAll(this._buffer, 0, num3);
				this.AddLength(num);
				num += num2;
			}
			return num;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003B15 File Offset: 0x00001D15
		public void WriteBytes(ImmutableArray<byte> buffer)
		{
			this.WriteBytes(buffer, 0, buffer.IsDefault ? 0 : buffer.Length);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003B32 File Offset: 0x00001D32
		public void WriteBytes(ImmutableArray<byte> buffer, int start, int byteCount)
		{
			this.WriteBytes(ImmutableByteArrayInterop.DangerousGetUnderlyingArray(buffer), start, byteCount);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003B42 File Offset: 0x00001D42
		public void WriteBytes(byte[] buffer)
		{
			this.WriteBytes(buffer, 0, (buffer != null) ? buffer.Length : 0);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003B58 File Offset: 0x00001D58
		public unsafe void WriteBytes(byte[] buffer, int start, int byteCount)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			BlobUtilities.ValidateRange(buffer.Length, start, byteCount);
			if (!this.IsHead)
			{
				BlobBuilder.ThrowHeadRequired();
			}
			if (buffer.Length == 0)
			{
				return;
			}
			fixed (byte* ptr = buffer)
			{
				this.WriteBytesUnchecked(ptr + start, byteCount);
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003BB4 File Offset: 0x00001DB4
		public void PadTo(int position)
		{
			this.WriteBytes(0, position - this.Count);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003BC8 File Offset: 0x00001DC8
		public void Align(int alignment)
		{
			int count = this.Count;
			this.WriteBytes(0, BitArithmetic.Align(count, alignment) - count);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003BEC File Offset: 0x00001DEC
		public void WriteBoolean(bool value)
		{
			this.WriteByte(value ? 1 : 0);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003BFC File Offset: 0x00001DFC
		public void WriteByte(byte value)
		{
			int start = this.ReserveBytesPrimitive(1);
			this._buffer.WriteByte(start, value);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003C1E File Offset: 0x00001E1E
		public void WriteSByte(sbyte value)
		{
			this.WriteByte((byte)value);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003C28 File Offset: 0x00001E28
		public void WriteDouble(double value)
		{
			int start = this.ReserveBytesPrimitive(8);
			this._buffer.WriteDouble(start, value);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003C4C File Offset: 0x00001E4C
		public void WriteSingle(float value)
		{
			int start = this.ReserveBytesPrimitive(4);
			this._buffer.WriteSingle(start, value);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003C6E File Offset: 0x00001E6E
		public void WriteInt16(short value)
		{
			this.WriteUInt16((ushort)value);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003C78 File Offset: 0x00001E78
		public void WriteUInt16(ushort value)
		{
			int start = this.ReserveBytesPrimitive(2);
			this._buffer.WriteUInt16(start, value);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003C9A File Offset: 0x00001E9A
		public void WriteInt16BE(short value)
		{
			this.WriteUInt16BE((ushort)value);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003CA4 File Offset: 0x00001EA4
		public void WriteUInt16BE(ushort value)
		{
			int start = this.ReserveBytesPrimitive(2);
			this._buffer.WriteUInt16BE(start, value);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003CC6 File Offset: 0x00001EC6
		public void WriteInt32BE(int value)
		{
			this.WriteUInt32BE((uint)value);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003CD0 File Offset: 0x00001ED0
		public void WriteUInt32BE(uint value)
		{
			int start = this.ReserveBytesPrimitive(4);
			this._buffer.WriteUInt32BE(start, value);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003CF2 File Offset: 0x00001EF2
		public void WriteInt32(int value)
		{
			this.WriteUInt32((uint)value);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003CFC File Offset: 0x00001EFC
		public void WriteUInt32(uint value)
		{
			int start = this.ReserveBytesPrimitive(4);
			this._buffer.WriteUInt32(start, value);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003D1E File Offset: 0x00001F1E
		public void WriteInt64(long value)
		{
			this.WriteUInt64((ulong)value);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003D28 File Offset: 0x00001F28
		public void WriteUInt64(ulong value)
		{
			int start = this.ReserveBytesPrimitive(8);
			this._buffer.WriteUInt64(start, value);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003D4C File Offset: 0x00001F4C
		public void WriteDecimal(decimal value)
		{
			int start = this.ReserveBytesPrimitive(13);
			this._buffer.WriteDecimal(start, value);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003D6F File Offset: 0x00001F6F
		public void WriteDateTime(DateTime value)
		{
			this.WriteInt64(value.Ticks);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003D7E File Offset: 0x00001F7E
		internal void WriteReference(uint reference, int size)
		{
			if (size == 2)
			{
				this.WriteUInt16((ushort)reference);
				return;
			}
			this.WriteUInt32(reference);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003D94 File Offset: 0x00001F94
		public unsafe void WriteUTF16(char[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!this.IsHead)
			{
				BlobBuilder.ThrowHeadRequired();
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

		// Token: 0x060000DB RID: 219 RVA: 0x00003DE8 File Offset: 0x00001FE8
		public unsafe void WriteUTF16(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!this.IsHead)
			{
				BlobBuilder.ThrowHeadRequired();
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

		// Token: 0x060000DC RID: 220 RVA: 0x00003E31 File Offset: 0x00002031
		public void WriteSerializedString(string value)
		{
			if (value == null)
			{
				this.WriteByte(byte.MaxValue);
				return;
			}
			this.WriteUTF8(value, 0, value.Length, true, true);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003E52 File Offset: 0x00002052
		public void WriteUTF8(string value, bool allowUnpairedSurrogates)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.WriteUTF8(value, 0, value.Length, allowUnpairedSurrogates, false);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003E74 File Offset: 0x00002074
		private unsafe void WriteUTF8(string str, int start, int length, bool allowUnpairedSurrogates, bool prependSize)
		{
			if (!this.IsHead)
			{
				BlobBuilder.ThrowHeadRequired();
			}
			fixed (string text = str)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr + start;
				int byteLimit = this.FreeBytes - (prependSize ? 4 : 0);
				char* ptr3;
				int utf8ByteCount = BlobUtilities.GetUTF8ByteCount(ptr2, length, byteLimit, out ptr3);
				int num = (int)((long)(ptr3 - ptr2));
				int charCount = str.Length - num;
				int utf8ByteCount2 = BlobUtilities.GetUTF8ByteCount(ptr3, charCount);
				if (prependSize)
				{
					this.WriteCompressedInteger(utf8ByteCount + utf8ByteCount2);
				}
				this._buffer.WriteUTF8(this.Length, ptr2, num, utf8ByteCount, allowUnpairedSurrogates);
				this.AddLength(utf8ByteCount);
				if (utf8ByteCount2 > 0)
				{
					this.Expand(utf8ByteCount2);
					this._buffer.WriteUTF8(0, ptr3, charCount, utf8ByteCount2, allowUnpairedSurrogates);
					this.AddLength(utf8ByteCount2);
				}
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003F3A File Offset: 0x0000213A
		public void WriteCompressedSignedInteger(int value)
		{
			BlobWriterImpl.WriteCompressedSignedInteger(this, value);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003F43 File Offset: 0x00002143
		public void WriteCompressedInteger(int value)
		{
			BlobWriterImpl.WriteCompressedInteger(this, value);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003F4C File Offset: 0x0000214C
		public void WriteConstant(object value)
		{
			BlobWriterImpl.WriteConstant(this, value);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003F58 File Offset: 0x00002158
		internal string GetDebuggerDisplay()
		{
			if (!this.IsHead)
			{
				return string.Format("<{0}>", new object[]
				{
					BlobBuilder.Display(this._buffer, this.Length)
				});
			}
			return string.Join("->", from chunk in this.GetChunks()
			select string.Format("[{0}]", new object[]
			{
				BlobBuilder.Display(chunk._buffer, chunk.Length)
			}));
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003FCB File Offset: 0x000021CB
		private static string Display(byte[] bytes, int length)
		{
			if (length > 64)
			{
				return BitConverter.ToString(bytes, 0, 32) + "-...-" + BitConverter.ToString(bytes, length - 32, 32);
			}
			return BitConverter.ToString(bytes, 0, length);
		}

		// Token: 0x0400000D RID: 13
		internal const int DefaultChunkSize = 256;

		// Token: 0x0400000E RID: 14
		internal const int MinChunkSize = 16;

		// Token: 0x0400000F RID: 15
		private BlobBuilder _nextOrPrevious;

		// Token: 0x04000010 RID: 16
		private int _previousLength;

		// Token: 0x04000011 RID: 17
		private byte[] _buffer;

		// Token: 0x04000012 RID: 18
		private uint _length;

		// Token: 0x04000013 RID: 19
		private const uint IsFrozenMask = 2147483648U;

		// Token: 0x0200016B RID: 363
		internal struct Chunks : IEnumerable<BlobBuilder>, IEnumerable, IEnumerator<BlobBuilder>, IEnumerator, IDisposable
		{
			// Token: 0x06000B50 RID: 2896 RVA: 0x00020960 File Offset: 0x0001EB60
			internal Chunks(BlobBuilder builder)
			{
				this._head = builder;
				this._next = builder.FirstChunk;
				this._currentOpt = null;
			}

			// Token: 0x170002C0 RID: 704
			// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0002097C File Offset: 0x0001EB7C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x170002C1 RID: 705
			// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00020984 File Offset: 0x0001EB84
			public BlobBuilder Current
			{
				get
				{
					return this._currentOpt;
				}
			}

			// Token: 0x06000B53 RID: 2899 RVA: 0x0002098C File Offset: 0x0001EB8C
			public bool MoveNext()
			{
				if (this._currentOpt == this._head)
				{
					return false;
				}
				if (this._currentOpt == this._head._nextOrPrevious)
				{
					this._currentOpt = this._head;
					return true;
				}
				this._currentOpt = this._next;
				this._next = this._next._nextOrPrevious;
				return true;
			}

			// Token: 0x06000B54 RID: 2900 RVA: 0x000209E8 File Offset: 0x0001EBE8
			public void Reset()
			{
				this._currentOpt = null;
				this._next = this._head.FirstChunk;
			}

			// Token: 0x06000B55 RID: 2901 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000B56 RID: 2902 RVA: 0x00020A02 File Offset: 0x0001EC02
			public BlobBuilder.Chunks GetEnumerator()
			{
				return this;
			}

			// Token: 0x06000B57 RID: 2903 RVA: 0x00020A0A File Offset: 0x0001EC0A
			IEnumerator<BlobBuilder> IEnumerable<BlobBuilder>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000B58 RID: 2904 RVA: 0x00020A0A File Offset: 0x0001EC0A
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000942 RID: 2370
			private readonly BlobBuilder _head;

			// Token: 0x04000943 RID: 2371
			private BlobBuilder _next;

			// Token: 0x04000944 RID: 2372
			private BlobBuilder _currentOpt;
		}

		// Token: 0x0200016C RID: 364
		public struct Blobs : IEnumerable<Blob>, IEnumerable, IEnumerator<Blob>, IEnumerator, IDisposable
		{
			// Token: 0x06000B59 RID: 2905 RVA: 0x00020A17 File Offset: 0x0001EC17
			internal Blobs(BlobBuilder builder)
			{
				this._chunks = new BlobBuilder.Chunks(builder);
			}

			// Token: 0x170002C2 RID: 706
			// (get) Token: 0x06000B5A RID: 2906 RVA: 0x00020A25 File Offset: 0x0001EC25
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x170002C3 RID: 707
			// (get) Token: 0x06000B5B RID: 2907 RVA: 0x00020A34 File Offset: 0x0001EC34
			public Blob Current
			{
				get
				{
					BlobBuilder blobBuilder = this._chunks.Current;
					if (blobBuilder != null)
					{
						return new Blob(blobBuilder._buffer, 0, blobBuilder.Length);
					}
					return default(Blob);
				}
			}

			// Token: 0x06000B5C RID: 2908 RVA: 0x00020A6C File Offset: 0x0001EC6C
			public bool MoveNext()
			{
				return this._chunks.MoveNext();
			}

			// Token: 0x06000B5D RID: 2909 RVA: 0x00020A79 File Offset: 0x0001EC79
			public void Reset()
			{
				this._chunks.Reset();
			}

			// Token: 0x06000B5E RID: 2910 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000B5F RID: 2911 RVA: 0x00020A86 File Offset: 0x0001EC86
			public BlobBuilder.Blobs GetEnumerator()
			{
				return this;
			}

			// Token: 0x06000B60 RID: 2912 RVA: 0x00020A8E File Offset: 0x0001EC8E
			IEnumerator<Blob> IEnumerable<Blob>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000B61 RID: 2913 RVA: 0x00020A8E File Offset: 0x0001EC8E
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000945 RID: 2373
			private BlobBuilder.Chunks _chunks;
		}
	}
}
