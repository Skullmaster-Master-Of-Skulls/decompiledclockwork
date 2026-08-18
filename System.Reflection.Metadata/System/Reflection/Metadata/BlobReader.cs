using System;
using System.Diagnostics;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000032 RID: 50
	[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
	public struct BlobReader
	{
		// Token: 0x06000286 RID: 646 RVA: 0x000076E0 File Offset: 0x000058E0
		public unsafe BlobReader(byte* buffer, int length)
		{
			this = new BlobReader(MemoryBlock.CreateChecked(buffer, length));
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000076EF File Offset: 0x000058EF
		internal BlobReader(MemoryBlock block)
		{
			this._block = block;
			this._currentPointer = block.Pointer;
			this._endPointer = block.Pointer + block.Length;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00007718 File Offset: 0x00005918
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

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00007793 File Offset: 0x00005993
		public int Length
		{
			get
			{
				return this._block.Length;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600028A RID: 650 RVA: 0x000077A0 File Offset: 0x000059A0
		public int Offset
		{
			get
			{
				return (int)((long)(this._currentPointer - this._block.Pointer));
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600028B RID: 651 RVA: 0x000077B8 File Offset: 0x000059B8
		public int RemainingBytes
		{
			get
			{
				return (int)((long)(this._endPointer - this._currentPointer));
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000077CB File Offset: 0x000059CB
		public void Reset()
		{
			this._currentPointer = this._block.Pointer;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x000077DE File Offset: 0x000059DE
		internal bool SeekOffset(int offset)
		{
			if (offset >= this._block.Length)
			{
				return false;
			}
			this._currentPointer = this._block.Pointer + offset;
			return true;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00007804 File Offset: 0x00005A04
		internal void SkipBytes(int count)
		{
			this.GetCurrentPointerAndAdvance(count);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000780E File Offset: 0x00005A0E
		internal void Align(byte alignment)
		{
			if (!this.TryAlign(alignment))
			{
				Throw.OutOfBounds();
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00007820 File Offset: 0x00005A20
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

		// Token: 0x06000291 RID: 657 RVA: 0x00007859 File Offset: 0x00005A59
		internal MemoryBlock GetMemoryBlockAt(int offset, int length)
		{
			this.CheckBounds(offset, length);
			return new MemoryBlock(this._currentPointer + offset, length);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00007871 File Offset: 0x00005A71
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckBounds(int offset, int byteCount)
		{
			if ((ulong)offset + (ulong)byteCount > (ulong)((long)(this._endPointer - this._currentPointer)))
			{
				Throw.OutOfBounds();
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000788F File Offset: 0x00005A8F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckBounds(int byteCount)
		{
			if ((ulong)byteCount > (ulong)((long)(this._endPointer - this._currentPointer)))
			{
				Throw.OutOfBounds();
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000078AC File Offset: 0x00005AAC
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

		// Token: 0x06000295 RID: 661 RVA: 0x000078E0 File Offset: 0x00005AE0
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

		// Token: 0x06000296 RID: 662 RVA: 0x0000790C File Offset: 0x00005B0C
		public bool ReadBoolean()
		{
			return this.ReadByte() > 0;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00007917 File Offset: 0x00005B17
		public unsafe sbyte ReadSByte()
		{
			return *(sbyte*)this.GetCurrentPointerAndAdvance1();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00007920 File Offset: 0x00005B20
		public unsafe byte ReadByte()
		{
			return *this.GetCurrentPointerAndAdvance1();
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00007929 File Offset: 0x00005B29
		public unsafe char ReadChar()
		{
			return (char)(*(ushort*)this.GetCurrentPointerAndAdvance(2));
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00007933 File Offset: 0x00005B33
		public unsafe short ReadInt16()
		{
			return *(short*)this.GetCurrentPointerAndAdvance(2);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00007929 File Offset: 0x00005B29
		public unsafe ushort ReadUInt16()
		{
			return *(ushort*)this.GetCurrentPointerAndAdvance(2);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000793D File Offset: 0x00005B3D
		public unsafe int ReadInt32()
		{
			return *(int*)this.GetCurrentPointerAndAdvance(4);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00007947 File Offset: 0x00005B47
		public unsafe uint ReadUInt32()
		{
			return *(uint*)this.GetCurrentPointerAndAdvance(4);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00007951 File Offset: 0x00005B51
		public unsafe long ReadInt64()
		{
			return *(long*)this.GetCurrentPointerAndAdvance(8);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00007951 File Offset: 0x00005B51
		public unsafe ulong ReadUInt64()
		{
			return (ulong)(*(long*)this.GetCurrentPointerAndAdvance(8));
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000795B File Offset: 0x00005B5B
		public unsafe float ReadSingle()
		{
			return *(float*)this.GetCurrentPointerAndAdvance(4);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00007965 File Offset: 0x00005B65
		public unsafe double ReadDouble()
		{
			return *(double*)this.GetCurrentPointerAndAdvance(8);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000796F File Offset: 0x00005B6F
		public unsafe Guid ReadGuid()
		{
			return *(Guid*)this.GetCurrentPointerAndAdvance(16);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00007980 File Offset: 0x00005B80
		public unsafe decimal ReadDecimal()
		{
			byte* currentPointerAndAdvance = this.GetCurrentPointerAndAdvance(13);
			byte b = *currentPointerAndAdvance & 127;
			if (b > 28)
			{
				throw new BadImageFormatException(SR.ValueTooLarge);
			}
			return new decimal(*(int*)(currentPointerAndAdvance + 1), *(int*)(currentPointerAndAdvance + 5), *(int*)(currentPointerAndAdvance + 9), (*currentPointerAndAdvance & 128) > 0, b);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x000079CB File Offset: 0x00005BCB
		public DateTime ReadDateTime()
		{
			return new DateTime(this.ReadInt64());
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x000079D8 File Offset: 0x00005BD8
		public SignatureHeader ReadSignatureHeader()
		{
			return new SignatureHeader(this.ReadByte());
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000079E8 File Offset: 0x00005BE8
		public string ReadUTF8(int byteCount)
		{
			string result = this._block.PeekUtf8(this.Offset, byteCount);
			this._currentPointer += byteCount;
			return result;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00007A18 File Offset: 0x00005C18
		public string ReadUTF16(int byteCount)
		{
			string result = this._block.PeekUtf16(this.Offset, byteCount);
			this._currentPointer += byteCount;
			return result;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00007A48 File Offset: 0x00005C48
		public byte[] ReadBytes(int byteCount)
		{
			byte[] result = this._block.PeekBytes(this.Offset, byteCount);
			this._currentPointer += byteCount;
			return result;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00007A78 File Offset: 0x00005C78
		internal string ReadUtf8NullTerminated()
		{
			int num;
			string result = this._block.PeekUtf8NullTerminated(this.Offset, null, MetadataStringDecoder.DefaultUTF8, out num, '\0');
			this._currentPointer += num;
			return result;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00007AB0 File Offset: 0x00005CB0
		private int ReadCompressedIntegerOrInvalid()
		{
			int num;
			int result = this._block.PeekCompressedInteger(this.Offset, out num);
			this._currentPointer += num;
			return result;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00007AE1 File Offset: 0x00005CE1
		public bool TryReadCompressedInteger(out int value)
		{
			value = this.ReadCompressedIntegerOrInvalid();
			return value != int.MaxValue;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00007AF8 File Offset: 0x00005CF8
		public int ReadCompressedInteger()
		{
			int result;
			if (!this.TryReadCompressedInteger(out result))
			{
				Throw.InvalidCompressedInteger();
			}
			return result;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00007B18 File Offset: 0x00005D18
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

		// Token: 0x060002AE RID: 686 RVA: 0x00007B90 File Offset: 0x00005D90
		public int ReadCompressedSignedInteger()
		{
			int result;
			if (!this.TryReadCompressedSignedInteger(out result))
			{
				Throw.InvalidCompressedInteger();
			}
			return result;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00007BB0 File Offset: 0x00005DB0
		public SerializationTypeCode ReadSerializationTypeCode()
		{
			int num = this.ReadCompressedIntegerOrInvalid();
			if (num > 255)
			{
				return SerializationTypeCode.Invalid;
			}
			return (SerializationTypeCode)num;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00007BD0 File Offset: 0x00005DD0
		public SignatureTypeCode ReadSignatureTypeCode()
		{
			int num = this.ReadCompressedIntegerOrInvalid();
			if (num == 17 || num == 18)
			{
				return SignatureTypeCode.TypeHandle;
			}
			if (num > 255)
			{
				return SignatureTypeCode.Invalid;
			}
			return (SignatureTypeCode)num;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00007C00 File Offset: 0x00005E00
		public string ReadSerializedString()
		{
			int byteCount;
			if (this.TryReadCompressedInteger(out byteCount))
			{
				return this.ReadUTF8(byteCount).TrimEnd(BlobReader.s_nullCharArray);
			}
			if (this.ReadByte() != 255)
			{
				Throw.InvalidSerializedString();
			}
			return null;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00007C3C File Offset: 0x00005E3C
		public EntityHandle ReadTypeHandle()
		{
			uint num = (uint)this.ReadCompressedIntegerOrInvalid();
			uint num2 = BlobReader.s_corEncodeTokenArray[(int)(num & 3U)];
			if (num == 2147483647U || num2 == 0U)
			{
				return default(EntityHandle);
			}
			return new EntityHandle(num2 | num >> 2);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00007C79 File Offset: 0x00005E79
		public BlobHandle ReadBlobHandle()
		{
			return BlobHandle.FromOffset(this.ReadCompressedInteger());
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00007C88 File Offset: 0x00005E88
		public object ReadConstant(ConstantTypeCode typeCode)
		{
			switch (typeCode)
			{
			case ConstantTypeCode.Boolean:
				return this.ReadBoolean();
			case ConstantTypeCode.Char:
				return this.ReadChar();
			case ConstantTypeCode.SByte:
				return this.ReadSByte();
			case ConstantTypeCode.Byte:
				return this.ReadByte();
			case ConstantTypeCode.Int16:
				return this.ReadInt16();
			case ConstantTypeCode.UInt16:
				return this.ReadUInt16();
			case ConstantTypeCode.Int32:
				return this.ReadInt32();
			case ConstantTypeCode.UInt32:
				return this.ReadUInt32();
			case ConstantTypeCode.Int64:
				return this.ReadInt64();
			case ConstantTypeCode.UInt64:
				return this.ReadUInt64();
			case ConstantTypeCode.Single:
				return this.ReadSingle();
			case ConstantTypeCode.Double:
				return this.ReadDouble();
			case ConstantTypeCode.String:
				return this.ReadUTF16(this.RemainingBytes);
			case ConstantTypeCode.NullReference:
				if (this.ReadUInt32() != 0U)
				{
					throw new BadImageFormatException(SR.InvalidConstantValue);
				}
				return null;
			}
			throw new ArgumentOutOfRangeException("typeCode");
		}

		// Token: 0x04000269 RID: 617
		private static readonly char[] s_nullCharArray = new char[1];

		// Token: 0x0400026A RID: 618
		internal const int InvalidCompressedInteger = 2147483647;

		// Token: 0x0400026B RID: 619
		private readonly MemoryBlock _block;

		// Token: 0x0400026C RID: 620
		private unsafe readonly byte* _endPointer;

		// Token: 0x0400026D RID: 621
		private unsafe byte* _currentPointer;

		// Token: 0x0400026E RID: 622
		private static readonly uint[] s_corEncodeTokenArray = new uint[]
		{
			33554432U,
			16777216U,
			452984832U,
			0U
		};
	}
}
