using System;
using System.Diagnostics;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Metadata
{
	// Token: 0x02000053 RID: 83
	[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
	internal struct BlobReader
	{
		// Token: 0x0600023C RID: 572 RVA: 0x00005EDF File Offset: 0x000040DF
		[SecurityCritical]
		public unsafe BlobReader(byte* buffer, int length)
		{
			this = new BlobReader(MemoryBlock.CreateChecked(buffer, length));
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00005EEE File Offset: 0x000040EE
		[SecuritySafeCritical]
		internal BlobReader(MemoryBlock block)
		{
			this._block = block;
			this._currentPointer = block.Pointer;
			this._endPointer = block.Pointer + block.Length;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00005F18 File Offset: 0x00004118
		[SecuritySafeCritical]
		internal string GetDebuggerDisplay()
		{
			if (this._block.Pointer == null)
			{
				return "<null>";
			}
			int num;
			string text = this._block.GetDebuggerDisplay(out num);
			if (this.Offset < num)
			{
				text = text.Insert(this.Offset * 3, "*");
			}
			else if (num == this._block.Length)
			{
				text += "*";
			}
			else
			{
				text += "*...";
			}
			return text;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00005F93 File Offset: 0x00004193
		public unsafe byte* StartPointer
		{
			[SecurityCritical]
			get
			{
				return this._block.Pointer;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00005FA0 File Offset: 0x000041A0
		public unsafe byte* CurrentPointer
		{
			[SecurityCritical]
			get
			{
				return this._currentPointer;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00005FA8 File Offset: 0x000041A8
		public int Length
		{
			get
			{
				return this._block.Length;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00005FB5 File Offset: 0x000041B5
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00005FCD File Offset: 0x000041CD
		public int Offset
		{
			[SecuritySafeCritical]
			get
			{
				return (int)((long)(this._currentPointer - this._block.Pointer));
			}
			[SecuritySafeCritical]
			set
			{
				if (value > this._block.Length)
				{
					Throw.OutOfBounds();
				}
				this._currentPointer = this._block.Pointer + value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00005FF5 File Offset: 0x000041F5
		public int RemainingBytes
		{
			[SecuritySafeCritical]
			get
			{
				return (int)((long)(this._endPointer - this._currentPointer));
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00006008 File Offset: 0x00004208
		[SecuritySafeCritical]
		public void Reset()
		{
			this._currentPointer = this._block.Pointer;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000601B File Offset: 0x0000421B
		public void Align(byte alignment)
		{
			if (!this.TryAlign(alignment))
			{
				Throw.OutOfBounds();
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000602C File Offset: 0x0000422C
		[SecuritySafeCritical]
		internal bool TryAlign(byte alignment)
		{
			int num = this.Offset & (int)(alignment - 1);
			if (num != 0)
			{
				int num2 = (int)alignment - num;
				if (num2 > this.RemainingBytes)
				{
					return false;
				}
				this._currentPointer += num2;
			}
			return true;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00006065 File Offset: 0x00004265
		[SecuritySafeCritical]
		internal MemoryBlock GetMemoryBlockAt(int offset, int length)
		{
			this.CheckBounds(offset, length);
			return new MemoryBlock(this._currentPointer + offset, length);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000607D File Offset: 0x0000427D
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckBounds(int offset, int byteCount)
		{
			if ((ulong)offset + (ulong)byteCount > (ulong)((long)(this._endPointer - this._currentPointer)))
			{
				Throw.OutOfBounds();
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000609B File Offset: 0x0000429B
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckBounds(int byteCount)
		{
			if ((ulong)byteCount > (ulong)((long)(this._endPointer - this._currentPointer)))
			{
				Throw.OutOfBounds();
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000060B8 File Offset: 0x000042B8
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe byte* GetCurrentPointerAndAdvance(int length)
		{
			byte* currentPointer = this._currentPointer;
			if (length > (int)((uint)((long)(this._endPointer - currentPointer))))
			{
				Throw.OutOfBounds();
			}
			this._currentPointer = currentPointer + length;
			return currentPointer;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000060EC File Offset: 0x000042EC
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe byte* GetCurrentPointerAndAdvance1()
		{
			byte* currentPointer = this._currentPointer;
			if (currentPointer == this._endPointer)
			{
				Throw.OutOfBounds();
			}
			this._currentPointer = currentPointer + 1;
			return currentPointer;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00006118 File Offset: 0x00004318
		public bool ReadBoolean()
		{
			return this.ReadByte() > 0;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00006123 File Offset: 0x00004323
		[SecuritySafeCritical]
		public unsafe sbyte ReadSByte()
		{
			return *(sbyte*)this.GetCurrentPointerAndAdvance1();
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000612C File Offset: 0x0000432C
		[SecuritySafeCritical]
		public unsafe byte ReadByte()
		{
			return *this.GetCurrentPointerAndAdvance1();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00006138 File Offset: 0x00004338
		[SecuritySafeCritical]
		public unsafe char ReadChar()
		{
			byte* currentPointerAndAdvance = this.GetCurrentPointerAndAdvance(2);
			return (char)((int)(*currentPointerAndAdvance) + ((int)currentPointerAndAdvance[1] << 8));
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00006158 File Offset: 0x00004358
		[SecuritySafeCritical]
		public unsafe short ReadInt16()
		{
			byte* currentPointerAndAdvance = this.GetCurrentPointerAndAdvance(2);
			return (short)((int)(*currentPointerAndAdvance) + ((int)currentPointerAndAdvance[1] << 8));
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00006178 File Offset: 0x00004378
		[SecuritySafeCritical]
		public unsafe ushort ReadUInt16()
		{
			byte* currentPointerAndAdvance = this.GetCurrentPointerAndAdvance(2);
			return (ushort)((int)(*currentPointerAndAdvance) + ((int)currentPointerAndAdvance[1] << 8));
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00006198 File Offset: 0x00004398
		[SecuritySafeCritical]
		public unsafe int ReadInt32()
		{
			byte* currentPointerAndAdvance = this.GetCurrentPointerAndAdvance(4);
			return (int)(*currentPointerAndAdvance) + ((int)currentPointerAndAdvance[1] << 8) + ((int)currentPointerAndAdvance[2] << 16) + ((int)currentPointerAndAdvance[3] << 24);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000061C8 File Offset: 0x000043C8
		[SecuritySafeCritical]
		public unsafe uint ReadUInt32()
		{
			byte* currentPointerAndAdvance = this.GetCurrentPointerAndAdvance(4);
			return (uint)((int)(*currentPointerAndAdvance) + ((int)currentPointerAndAdvance[1] << 8) + ((int)currentPointerAndAdvance[2] << 16) + ((int)currentPointerAndAdvance[3] << 24));
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000061F8 File Offset: 0x000043F8
		[SecuritySafeCritical]
		public unsafe long ReadInt64()
		{
			byte* currentPointerAndAdvance = this.GetCurrentPointerAndAdvance(8);
			uint num = (uint)((int)(*currentPointerAndAdvance) + ((int)currentPointerAndAdvance[1] << 8) + ((int)currentPointerAndAdvance[2] << 16) + ((int)currentPointerAndAdvance[3] << 24));
			uint num2 = (uint)((int)currentPointerAndAdvance[4] + ((int)currentPointerAndAdvance[5] << 8) + ((int)currentPointerAndAdvance[6] << 16) + ((int)currentPointerAndAdvance[7] << 24));
			return (long)((ulong)num + ((ulong)num2 << 32));
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000624B File Offset: 0x0000444B
		public ulong ReadUInt64()
		{
			return (ulong)this.ReadInt64();
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00006254 File Offset: 0x00004454
		[SecuritySafeCritical]
		public unsafe float ReadSingle()
		{
			int num = this.ReadInt32();
			return *(float*)(&num);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000626C File Offset: 0x0000446C
		[SecuritySafeCritical]
		public unsafe double ReadDouble()
		{
			long num = this.ReadInt64();
			return *(double*)(&num);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00006284 File Offset: 0x00004484
		[SecuritySafeCritical]
		public unsafe Guid ReadGuid()
		{
			byte* currentPointerAndAdvance = this.GetCurrentPointerAndAdvance(16);
			if (BitConverter.IsLittleEndian)
			{
				return *(Guid*)currentPointerAndAdvance;
			}
			return new Guid((int)(*currentPointerAndAdvance) | (int)currentPointerAndAdvance[1] << 8 | (int)currentPointerAndAdvance[2] << 16 | (int)currentPointerAndAdvance[3] << 24, (short)((int)currentPointerAndAdvance[4] | (int)currentPointerAndAdvance[5] << 8), (short)((int)currentPointerAndAdvance[6] | (int)currentPointerAndAdvance[7] << 8), currentPointerAndAdvance[8], currentPointerAndAdvance[9], currentPointerAndAdvance[10], currentPointerAndAdvance[11], currentPointerAndAdvance[12], currentPointerAndAdvance[13], currentPointerAndAdvance[14], currentPointerAndAdvance[15]);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00006308 File Offset: 0x00004508
		[SecuritySafeCritical]
		public unsafe decimal ReadDecimal()
		{
			byte* currentPointerAndAdvance = this.GetCurrentPointerAndAdvance(13);
			byte b = *currentPointerAndAdvance & 127;
			if (b > 28)
			{
				throw new BadImageFormatException("ValueTooLarge");
			}
			return new decimal((int)currentPointerAndAdvance[1] | (int)currentPointerAndAdvance[2] << 8 | (int)currentPointerAndAdvance[3] << 16 | (int)currentPointerAndAdvance[4] << 24, (int)currentPointerAndAdvance[5] | (int)currentPointerAndAdvance[6] << 8 | (int)currentPointerAndAdvance[7] << 16 | (int)currentPointerAndAdvance[8] << 24, (int)currentPointerAndAdvance[9] | (int)currentPointerAndAdvance[10] << 8 | (int)currentPointerAndAdvance[11] << 16 | (int)currentPointerAndAdvance[12] << 24, (*currentPointerAndAdvance & 128) > 0, b);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000639B File Offset: 0x0000459B
		public DateTime ReadDateTime()
		{
			return new DateTime(this.ReadInt64());
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000063A8 File Offset: 0x000045A8
		public int IndexOf(byte value)
		{
			int offset = this.Offset;
			int num = this._block.IndexOfUnchecked(value, offset);
			if (num < 0)
			{
				return -1;
			}
			return num - offset;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000063D8 File Offset: 0x000045D8
		[SecuritySafeCritical]
		public string ReadUTF8(int byteCount)
		{
			string result = this._block.PeekUtf8(this.Offset, byteCount);
			this._currentPointer += byteCount;
			return result;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000640C File Offset: 0x0000460C
		[SecuritySafeCritical]
		public string ReadUTF16(int byteCount)
		{
			string result = this._block.PeekUtf16(this.Offset, byteCount);
			this._currentPointer += byteCount;
			return result;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00006440 File Offset: 0x00004640
		[SecuritySafeCritical]
		public byte[] ReadBytes(int byteCount)
		{
			byte[] result = this._block.PeekBytes(this.Offset, byteCount);
			this._currentPointer += byteCount;
			return result;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00006472 File Offset: 0x00004672
		[SecuritySafeCritical]
		public unsafe void ReadBytes(int byteCount, byte[] buffer, int bufferOffset)
		{
			Marshal.Copy((IntPtr)((void*)this.GetCurrentPointerAndAdvance(byteCount)), buffer, bufferOffset, byteCount);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00006488 File Offset: 0x00004688
		[SecuritySafeCritical]
		internal string ReadUtf8NullTerminated()
		{
			int num;
			string result = this._block.PeekUtf8NullTerminated(this.Offset, out num, '\0');
			this._currentPointer += num;
			return result;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000064BC File Offset: 0x000046BC
		[SecuritySafeCritical]
		private int ReadCompressedIntegerOrInvalid()
		{
			int num;
			int result = this._block.PeekCompressedInteger(this.Offset, out num);
			this._currentPointer += num;
			return result;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000064EF File Offset: 0x000046EF
		public bool TryReadCompressedInteger(out int value)
		{
			value = this.ReadCompressedIntegerOrInvalid();
			return value != int.MaxValue;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00006508 File Offset: 0x00004708
		public int ReadCompressedInteger()
		{
			int result;
			if (!this.TryReadCompressedInteger(out result))
			{
				Throw.InvalidCompressedInteger();
			}
			return result;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00006528 File Offset: 0x00004728
		[SecuritySafeCritical]
		public bool TryReadCompressedSignedInteger(out int value)
		{
			int num;
			value = this._block.PeekCompressedInteger(this.Offset, out num);
			if (value == 2147483647)
			{
				return false;
			}
			bool flag = (value & 1) != 0;
			value >>= 1;
			if (flag)
			{
				if (num != 1)
				{
					if (num != 2)
					{
						value |= -268435456;
					}
					else
					{
						value |= -8192;
					}
				}
				else
				{
					value |= -64;
				}
			}
			this._currentPointer += num;
			return true;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000065A4 File Offset: 0x000047A4
		public int ReadCompressedSignedInteger()
		{
			int result;
			if (!this.TryReadCompressedSignedInteger(out result))
			{
				Throw.InvalidCompressedInteger();
			}
			return result;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000065C1 File Offset: 0x000047C1
		public BlobHandle ReadBlobHandle()
		{
			return BlobHandle.FromOffset(this.ReadCompressedInteger());
		}

		// Token: 0x040002FC RID: 764
		private static readonly char[] s_nullCharArray = new char[1];

		// Token: 0x040002FD RID: 765
		internal const int InvalidCompressedInteger = 2147483647;

		// Token: 0x040002FE RID: 766
		private readonly MemoryBlock _block;

		// Token: 0x040002FF RID: 767
		[SecurityCritical]
		private unsafe readonly byte* _endPointer;

		// Token: 0x04000300 RID: 768
		[SecurityCritical]
		private unsafe byte* _currentPointer;
	}
}
