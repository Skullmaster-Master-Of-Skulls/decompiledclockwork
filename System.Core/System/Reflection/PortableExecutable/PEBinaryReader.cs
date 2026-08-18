using System;
using System.IO;
using System.Text;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000044 RID: 68
	internal struct PEBinaryReader
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x00004365 File Offset: 0x00002565
		public PEBinaryReader(Stream stream, int size)
		{
			this._startOffset = stream.Position;
			this._maxOffset = this._startOffset + (long)size;
			this._reader = new BinaryReader(stream, Encoding.UTF8, true);
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00004394 File Offset: 0x00002594
		public int CurrentOffset
		{
			get
			{
				return (int)(this._reader.BaseStream.Position - this._startOffset);
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000043AE File Offset: 0x000025AE
		public void Seek(int offset)
		{
			this.CheckBounds(this._startOffset, offset);
			this._reader.BaseStream.Seek((long)offset, SeekOrigin.Begin);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000043D1 File Offset: 0x000025D1
		public byte[] ReadBytes(int count)
		{
			this.CheckBounds(this._reader.BaseStream.Position, count);
			return this._reader.ReadBytes(count);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000043F6 File Offset: 0x000025F6
		public byte ReadByte()
		{
			this.CheckBounds(1U);
			return this._reader.ReadByte();
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000440A File Offset: 0x0000260A
		public short ReadInt16()
		{
			this.CheckBounds(2U);
			return this._reader.ReadInt16();
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000441E File Offset: 0x0000261E
		public ushort ReadUInt16()
		{
			this.CheckBounds(2U);
			return this._reader.ReadUInt16();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00004432 File Offset: 0x00002632
		public int ReadInt32()
		{
			this.CheckBounds(4U);
			return this._reader.ReadInt32();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00004446 File Offset: 0x00002646
		public uint ReadUInt32()
		{
			this.CheckBounds(4U);
			return this._reader.ReadUInt32();
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000445A File Offset: 0x0000265A
		public ulong ReadUInt64()
		{
			this.CheckBounds(8U);
			return this._reader.ReadUInt64();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00004470 File Offset: 0x00002670
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

		// Token: 0x060001B4 RID: 436 RVA: 0x000044AD File Offset: 0x000026AD
		private void CheckBounds(uint count)
		{
			if (this._reader.BaseStream.Position + (long)((ulong)count) > this._maxOffset)
			{
				Throw.ImageTooSmall();
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000044CF File Offset: 0x000026CF
		private void CheckBounds(long startPosition, int count)
		{
			if (startPosition + (long)((ulong)count) > this._maxOffset)
			{
				Throw.ImageTooSmallOrContainsInvalidOffsetOrCount();
			}
		}

		// Token: 0x0400023F RID: 575
		private readonly long _startOffset;

		// Token: 0x04000240 RID: 576
		private readonly long _maxOffset;

		// Token: 0x04000241 RID: 577
		private readonly BinaryReader _reader;
	}
}
