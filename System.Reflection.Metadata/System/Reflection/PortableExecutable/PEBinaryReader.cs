using System;
using System.IO;
using System.Text;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x0200001F RID: 31
	internal struct PEBinaryReader
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x00005DEC File Offset: 0x00003FEC
		public PEBinaryReader(Stream stream, int size)
		{
			this._startOffset = stream.Position;
			this._maxOffset = this._startOffset + (long)size;
			this._reader = new BinaryReader(stream, Encoding.UTF8, true);
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00005E1B File Offset: 0x0000401B
		public int CurrentOffset
		{
			get
			{
				return (int)(this._reader.BaseStream.Position - this._startOffset);
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00005E35 File Offset: 0x00004035
		public void Seek(int offset)
		{
			this.CheckBounds(this._startOffset, offset);
			this._reader.BaseStream.Seek((long)offset, SeekOrigin.Begin);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00005E58 File Offset: 0x00004058
		public byte[] ReadBytes(int count)
		{
			this.CheckBounds(this._reader.BaseStream.Position, count);
			return this._reader.ReadBytes(count);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00005E7D File Offset: 0x0000407D
		public byte ReadByte()
		{
			this.CheckBounds(1U);
			return this._reader.ReadByte();
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005E91 File Offset: 0x00004091
		public short ReadInt16()
		{
			this.CheckBounds(2U);
			return this._reader.ReadInt16();
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00005EA5 File Offset: 0x000040A5
		public ushort ReadUInt16()
		{
			this.CheckBounds(2U);
			return this._reader.ReadUInt16();
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00005EB9 File Offset: 0x000040B9
		public int ReadInt32()
		{
			this.CheckBounds(4U);
			return this._reader.ReadInt32();
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00005ECD File Offset: 0x000040CD
		public uint ReadUInt32()
		{
			this.CheckBounds(4U);
			return this._reader.ReadUInt32();
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00005EE1 File Offset: 0x000040E1
		public ulong ReadUInt64()
		{
			this.CheckBounds(8U);
			return this._reader.ReadUInt64();
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00005EF8 File Offset: 0x000040F8
		public string ReadNullPaddedUTF8(int byteCount)
		{
			byte[] array = this.ReadBytes(byteCount);
			int count = 0;
			for (int i = array.Length; i > 0; i--)
			{
				if (array[i - 1] != 0)
				{
					count = i;
					break;
				}
			}
			return Encoding.UTF8.GetString(array, 0, count);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00005F38 File Offset: 0x00004138
		public static int GetAndValidateSize(Stream peStream, int? size)
		{
			long num = peStream.Length - peStream.Position;
			if (size != null)
			{
				if ((long)size.Value > num)
				{
					throw new ArgumentOutOfRangeException("size");
				}
				return size.Value;
			}
			else
			{
				if (num > 2147483647L)
				{
					throw new ArgumentException(SR.StreamTooLarge, "peStream");
				}
				return (int)num;
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00005F95 File Offset: 0x00004195
		private void CheckBounds(uint count)
		{
			if (this._reader.BaseStream.Position + (long)((ulong)count) > this._maxOffset)
			{
				Throw.ImageTooSmall();
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005FB7 File Offset: 0x000041B7
		private void CheckBounds(long startPosition, int count)
		{
			if (startPosition + (long)((ulong)count) > this._maxOffset)
			{
				Throw.ImageTooSmallOrContainsInvalidOffsetOrCount();
			}
		}

		// Token: 0x040000CE RID: 206
		private readonly long _startOffset;

		// Token: 0x040000CF RID: 207
		private readonly long _maxOffset;

		// Token: 0x040000D0 RID: 208
		private readonly BinaryReader _reader;
	}
}
