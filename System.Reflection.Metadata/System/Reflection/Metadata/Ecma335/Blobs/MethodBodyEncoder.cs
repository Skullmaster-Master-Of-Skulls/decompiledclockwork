using System;
using System.Collections.Immutable;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x0200013E RID: 318
	internal struct MethodBodyEncoder
	{
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x0001D64B File Offset: 0x0001B84B
		public BlobBuilder Builder { get; }

		// Token: 0x06000A48 RID: 2632 RVA: 0x0001D653 File Offset: 0x0001B853
		internal MethodBodyEncoder(BlobBuilder builder, ushort maxStack, int exceptionRegionCount, StandaloneSignatureHandle localVariablesSignature, MethodBodyAttributes attributes)
		{
			this.Builder = builder;
			this._maxStack = maxStack;
			this._localVariablesSignature = localVariablesSignature;
			this._attributes = (byte)attributes;
			this._exceptionRegionCount = exceptionRegionCount;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0001D67C File Offset: 0x0001B87C
		private int WriteHeader(int codeSize)
		{
			int count;
			if (codeSize < 64 && this._maxStack <= 8 && this._localVariablesSignature.IsNil && this._exceptionRegionCount == 0)
			{
				count = this.Builder.Count;
				this.Builder.WriteByte((byte)(codeSize << 2 | 2));
			}
			else
			{
				this.Builder.Align(4);
				count = this.Builder.Count;
				ushort num = 12291;
				if (this._exceptionRegionCount > 0)
				{
					num |= 8;
				}
				if ((this._attributes & 1) != 0)
				{
					num |= 16;
				}
				this.Builder.WriteUInt16((ushort)this._attributes | num);
				this.Builder.WriteUInt16(this._maxStack);
				this.Builder.WriteInt32(codeSize);
				this.Builder.WriteInt32(this._localVariablesSignature.IsNil ? 0 : MetadataTokens.GetToken(this._localVariablesSignature));
			}
			return count;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0001D773 File Offset: 0x0001B973
		private ExceptionRegionEncoder CreateExceptionEncoder()
		{
			return new ExceptionRegionEncoder(this.Builder, this._exceptionRegionCount, (this._attributes & 2) > 0);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0001D791 File Offset: 0x0001B991
		public ExceptionRegionEncoder WriteInstructions(ImmutableArray<byte> buffer, out int offset)
		{
			offset = this.WriteHeader(buffer.Length);
			this.Builder.WriteBytes(buffer);
			return this.CreateExceptionEncoder();
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0001D7B4 File Offset: 0x0001B9B4
		public ExceptionRegionEncoder WriteInstructions(ImmutableArray<byte> buffer, out int offset, out Blob instructionBlob)
		{
			offset = this.WriteHeader(buffer.Length);
			instructionBlob = this.Builder.ReserveBytes(buffer.Length);
			new BlobWriter(instructionBlob).WriteBytes(buffer);
			return this.CreateExceptionEncoder();
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0001D802 File Offset: 0x0001BA02
		public ExceptionRegionEncoder WriteInstructions(BlobBuilder buffer, out int offset)
		{
			offset = this.WriteHeader(buffer.Count);
			buffer.WriteContentTo(this.Builder);
			return this.CreateExceptionEncoder();
		}

		// Token: 0x040008B9 RID: 2233
		private readonly ushort _maxStack;

		// Token: 0x040008BA RID: 2234
		private readonly int _exceptionRegionCount;

		// Token: 0x040008BB RID: 2235
		private readonly StandaloneSignatureHandle _localVariablesSignature;

		// Token: 0x040008BC RID: 2236
		private readonly byte _attributes;
	}
}
