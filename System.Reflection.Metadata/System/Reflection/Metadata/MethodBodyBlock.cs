using System;
using System.Collections.Immutable;
using System.Reflection.Internal;

namespace System.Reflection.Metadata
{
	// Token: 0x02000080 RID: 128
	public sealed class MethodBodyBlock
	{
		// Token: 0x060005E0 RID: 1504 RVA: 0x0000E3F5 File Offset: 0x0000C5F5
		private MethodBodyBlock(bool localVariablesInitialized, ushort maxStack, StandaloneSignatureHandle localSignatureHandle, MemoryBlock il, ImmutableArray<ExceptionRegion> exceptionRegions, int size)
		{
			this._localVariablesInitialized = localVariablesInitialized;
			this._maxStack = maxStack;
			this._localSignature = localSignatureHandle;
			this._il = il;
			this._exceptionRegions = exceptionRegions;
			this._size = size;
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0000E42A File Offset: 0x0000C62A
		public int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0000E432 File Offset: 0x0000C632
		public int MaxStack
		{
			get
			{
				return (int)this._maxStack;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0000E43A File Offset: 0x0000C63A
		public bool LocalVariablesInitialized
		{
			get
			{
				return this._localVariablesInitialized;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0000E442 File Offset: 0x0000C642
		public StandaloneSignatureHandle LocalSignature
		{
			get
			{
				return this._localSignature;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0000E44A File Offset: 0x0000C64A
		public ImmutableArray<ExceptionRegion> ExceptionRegions
		{
			get
			{
				return this._exceptionRegions;
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0000E454 File Offset: 0x0000C654
		public byte[] GetILBytes()
		{
			return this._il.ToArray();
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0000E470 File Offset: 0x0000C670
		public ImmutableArray<byte> GetILContent()
		{
			byte[] ilbytes = this.GetILBytes();
			return ImmutableByteArrayInterop.DangerousCreateFromUnderlyingArray(ref ilbytes);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0000E48B File Offset: 0x0000C68B
		public BlobReader GetILReader()
		{
			return new BlobReader(this._il);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0000E498 File Offset: 0x0000C698
		public static MethodBodyBlock Create(BlobReader reader)
		{
			int offset = reader.Offset;
			byte b = reader.ReadByte();
			int num;
			if ((b & 3) == 2)
			{
				num = b >> 2;
				return new MethodBodyBlock(false, 8, default(StandaloneSignatureHandle), reader.GetMemoryBlockAt(0, num), ImmutableArray<ExceptionRegion>.Empty, 1 + num);
			}
			if ((b & 3) != 3)
			{
				throw new BadImageFormatException(SR.Format(SR.InvalidMethodHeader1, b));
			}
			byte b2 = reader.ReadByte();
			if (b2 >> 4 != 3)
			{
				throw new BadImageFormatException(SR.Format(SR.InvalidMethodHeader2, b, b2));
			}
			bool localVariablesInitialized = (b & 16) == 16;
			bool flag = (b & 8) == 8;
			ushort maxStack = reader.ReadUInt16();
			num = reader.ReadInt32();
			int num2 = reader.ReadInt32();
			StandaloneSignatureHandle localSignatureHandle;
			if (num2 == 0)
			{
				localSignatureHandle = default(StandaloneSignatureHandle);
			}
			else
			{
				if (((long)num2 & 2130706432L) != 285212672L)
				{
					throw new BadImageFormatException(SR.Format(SR.InvalidLocalSignatureToken, (uint)num2));
				}
				localSignatureHandle = StandaloneSignatureHandle.FromRowId(num2 & 16777215);
			}
			MemoryBlock memoryBlockAt = reader.GetMemoryBlockAt(0, num);
			reader.SkipBytes(num);
			ImmutableArray<ExceptionRegion> exceptionRegions;
			if (flag)
			{
				reader.Align(4);
				byte b3 = reader.ReadByte();
				if ((b3 & 1) != 1)
				{
					throw new BadImageFormatException(SR.Format(SR.InvalidSehHeader, b3));
				}
				bool flag2 = (b3 & 64) == 64;
				int num3 = (int)reader.ReadByte();
				if (flag2)
				{
					num3 += (int)reader.ReadUInt16() << 8;
					exceptionRegions = MethodBodyBlock.ReadFatExceptionHandlers(ref reader, num3 / 24);
				}
				else
				{
					reader.SkipBytes(2);
					exceptionRegions = MethodBodyBlock.ReadSmallExceptionHandlers(ref reader, num3 / 12);
				}
			}
			else
			{
				exceptionRegions = ImmutableArray<ExceptionRegion>.Empty;
			}
			return new MethodBodyBlock(localVariablesInitialized, maxStack, localSignatureHandle, memoryBlockAt, exceptionRegions, reader.Offset - offset);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0000E644 File Offset: 0x0000C844
		private static ImmutableArray<ExceptionRegion> ReadSmallExceptionHandlers(ref BlobReader memReader, int count)
		{
			ExceptionRegion[] array = new ExceptionRegion[count];
			for (int i = 0; i < array.Length; i++)
			{
				ExceptionRegionKind kind = (ExceptionRegionKind)memReader.ReadUInt16();
				ushort tryOffset = memReader.ReadUInt16();
				byte tryLength = memReader.ReadByte();
				ushort handlerOffset = memReader.ReadUInt16();
				byte handlerLength = memReader.ReadByte();
				int classTokenOrFilterOffset = memReader.ReadInt32();
				array[i] = new ExceptionRegion(kind, (int)tryOffset, (int)tryLength, (int)handlerOffset, (int)handlerLength, classTokenOrFilterOffset);
			}
			return ImmutableArray.Create<ExceptionRegion>(array);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0000E6B0 File Offset: 0x0000C8B0
		private static ImmutableArray<ExceptionRegion> ReadFatExceptionHandlers(ref BlobReader memReader, int count)
		{
			ExceptionRegion[] array = new ExceptionRegion[count];
			for (int i = 0; i < array.Length; i++)
			{
				ExceptionRegionKind kind = (ExceptionRegionKind)memReader.ReadUInt32();
				int tryOffset = memReader.ReadInt32();
				int tryLength = memReader.ReadInt32();
				int handlerOffset = memReader.ReadInt32();
				int handlerLength = memReader.ReadInt32();
				int classTokenOrFilterOffset = memReader.ReadInt32();
				array[i] = new ExceptionRegion(kind, tryOffset, tryLength, handlerOffset, handlerLength, classTokenOrFilterOffset);
			}
			return ImmutableArray.Create<ExceptionRegion>(array);
		}

		// Token: 0x040003AA RID: 938
		private readonly MemoryBlock _il;

		// Token: 0x040003AB RID: 939
		private readonly int _size;

		// Token: 0x040003AC RID: 940
		private readonly ushort _maxStack;

		// Token: 0x040003AD RID: 941
		private readonly bool _localVariablesInitialized;

		// Token: 0x040003AE RID: 942
		private readonly StandaloneSignatureHandle _localSignature;

		// Token: 0x040003AF RID: 943
		private readonly ImmutableArray<ExceptionRegion> _exceptionRegions;

		// Token: 0x040003B0 RID: 944
		private const byte ILTinyFormat = 2;

		// Token: 0x040003B1 RID: 945
		private const byte ILFatFormat = 3;

		// Token: 0x040003B2 RID: 946
		private const byte ILFormatMask = 3;

		// Token: 0x040003B3 RID: 947
		private const int ILTinyFormatSizeShift = 2;

		// Token: 0x040003B4 RID: 948
		private const byte ILMoreSects = 8;

		// Token: 0x040003B5 RID: 949
		private const byte ILInitLocals = 16;

		// Token: 0x040003B6 RID: 950
		private const byte ILFatFormatHeaderSize = 3;

		// Token: 0x040003B7 RID: 951
		private const int ILFatFormatHeaderSizeShift = 4;

		// Token: 0x040003B8 RID: 952
		private const byte SectEHTable = 1;

		// Token: 0x040003B9 RID: 953
		private const byte SectOptILTable = 2;

		// Token: 0x040003BA RID: 954
		private const byte SectFatFormat = 64;

		// Token: 0x040003BB RID: 955
		private const byte SectMoreSects = 64;
	}
}
