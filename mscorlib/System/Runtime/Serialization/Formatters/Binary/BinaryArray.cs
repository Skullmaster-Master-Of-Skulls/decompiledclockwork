using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007E4 RID: 2020
	internal sealed class BinaryArray : IStreamable
	{
		// Token: 0x06004779 RID: 18297 RVA: 0x000F4F36 File Offset: 0x000F3F36
		internal BinaryArray()
		{
		}

		// Token: 0x0600477A RID: 18298 RVA: 0x000F4F3E File Offset: 0x000F3F3E
		internal BinaryArray(BinaryHeaderEnum binaryHeaderEnum)
		{
			this.binaryHeaderEnum = binaryHeaderEnum;
		}

		// Token: 0x0600477B RID: 18299 RVA: 0x000F4F50 File Offset: 0x000F3F50
		internal void Set(int objectId, int rank, int[] lengthA, int[] lowerBoundA, BinaryTypeEnum binaryTypeEnum, object typeInformation, BinaryArrayTypeEnum binaryArrayTypeEnum, int assemId)
		{
			this.objectId = objectId;
			this.binaryArrayTypeEnum = binaryArrayTypeEnum;
			this.rank = rank;
			this.lengthA = lengthA;
			this.lowerBoundA = lowerBoundA;
			this.binaryTypeEnum = binaryTypeEnum;
			this.typeInformation = typeInformation;
			this.assemId = assemId;
			this.binaryHeaderEnum = BinaryHeaderEnum.Array;
			if (binaryArrayTypeEnum == BinaryArrayTypeEnum.Single)
			{
				if (binaryTypeEnum == BinaryTypeEnum.Primitive)
				{
					this.binaryHeaderEnum = BinaryHeaderEnum.ArraySinglePrimitive;
					return;
				}
				if (binaryTypeEnum == BinaryTypeEnum.String)
				{
					this.binaryHeaderEnum = BinaryHeaderEnum.ArraySingleString;
					return;
				}
				if (binaryTypeEnum == BinaryTypeEnum.Object)
				{
					this.binaryHeaderEnum = BinaryHeaderEnum.ArraySingleObject;
				}
			}
		}

		// Token: 0x0600477C RID: 18300 RVA: 0x000F4FD0 File Offset: 0x000F3FD0
		public void Write(__BinaryWriter sout)
		{
			switch (this.binaryHeaderEnum)
			{
			case BinaryHeaderEnum.ArraySinglePrimitive:
				sout.WriteByte((byte)this.binaryHeaderEnum);
				sout.WriteInt32(this.objectId);
				sout.WriteInt32(this.lengthA[0]);
				sout.WriteByte((byte)((InternalPrimitiveTypeE)this.typeInformation));
				return;
			case BinaryHeaderEnum.ArraySingleObject:
				sout.WriteByte((byte)this.binaryHeaderEnum);
				sout.WriteInt32(this.objectId);
				sout.WriteInt32(this.lengthA[0]);
				return;
			case BinaryHeaderEnum.ArraySingleString:
				sout.WriteByte((byte)this.binaryHeaderEnum);
				sout.WriteInt32(this.objectId);
				sout.WriteInt32(this.lengthA[0]);
				return;
			default:
				sout.WriteByte((byte)this.binaryHeaderEnum);
				sout.WriteInt32(this.objectId);
				sout.WriteByte((byte)this.binaryArrayTypeEnum);
				sout.WriteInt32(this.rank);
				for (int i = 0; i < this.rank; i++)
				{
					sout.WriteInt32(this.lengthA[i]);
				}
				if (this.binaryArrayTypeEnum == BinaryArrayTypeEnum.SingleOffset || this.binaryArrayTypeEnum == BinaryArrayTypeEnum.JaggedOffset || this.binaryArrayTypeEnum == BinaryArrayTypeEnum.RectangularOffset)
				{
					for (int j = 0; j < this.rank; j++)
					{
						sout.WriteInt32(this.lowerBoundA[j]);
					}
				}
				sout.WriteByte((byte)this.binaryTypeEnum);
				BinaryConverter.WriteTypeInfo(this.binaryTypeEnum, this.typeInformation, this.assemId, sout);
				return;
			}
		}

		// Token: 0x0600477D RID: 18301 RVA: 0x000F5138 File Offset: 0x000F4138
		public void Read(__BinaryParser input)
		{
			switch (this.binaryHeaderEnum)
			{
			case BinaryHeaderEnum.ArraySinglePrimitive:
				this.objectId = input.ReadInt32();
				this.lengthA = new int[1];
				this.lengthA[0] = input.ReadInt32();
				this.binaryArrayTypeEnum = BinaryArrayTypeEnum.Single;
				this.rank = 1;
				this.lowerBoundA = new int[this.rank];
				this.binaryTypeEnum = BinaryTypeEnum.Primitive;
				this.typeInformation = (InternalPrimitiveTypeE)input.ReadByte();
				return;
			case BinaryHeaderEnum.ArraySingleObject:
				this.objectId = input.ReadInt32();
				this.lengthA = new int[1];
				this.lengthA[0] = input.ReadInt32();
				this.binaryArrayTypeEnum = BinaryArrayTypeEnum.Single;
				this.rank = 1;
				this.lowerBoundA = new int[this.rank];
				this.binaryTypeEnum = BinaryTypeEnum.Object;
				this.typeInformation = null;
				return;
			case BinaryHeaderEnum.ArraySingleString:
				this.objectId = input.ReadInt32();
				this.lengthA = new int[1];
				this.lengthA[0] = input.ReadInt32();
				this.binaryArrayTypeEnum = BinaryArrayTypeEnum.Single;
				this.rank = 1;
				this.lowerBoundA = new int[this.rank];
				this.binaryTypeEnum = BinaryTypeEnum.String;
				this.typeInformation = null;
				return;
			default:
				this.objectId = input.ReadInt32();
				this.binaryArrayTypeEnum = (BinaryArrayTypeEnum)input.ReadByte();
				this.rank = input.ReadInt32();
				this.lengthA = new int[this.rank];
				this.lowerBoundA = new int[this.rank];
				for (int i = 0; i < this.rank; i++)
				{
					this.lengthA[i] = input.ReadInt32();
				}
				if (this.binaryArrayTypeEnum == BinaryArrayTypeEnum.SingleOffset || this.binaryArrayTypeEnum == BinaryArrayTypeEnum.JaggedOffset || this.binaryArrayTypeEnum == BinaryArrayTypeEnum.RectangularOffset)
				{
					for (int j = 0; j < this.rank; j++)
					{
						this.lowerBoundA[j] = input.ReadInt32();
					}
				}
				this.binaryTypeEnum = (BinaryTypeEnum)input.ReadByte();
				this.typeInformation = BinaryConverter.ReadTypeInfo(this.binaryTypeEnum, input, out this.assemId);
				return;
			}
		}

		// Token: 0x0400242D RID: 9261
		internal int objectId;

		// Token: 0x0400242E RID: 9262
		internal int rank;

		// Token: 0x0400242F RID: 9263
		internal int[] lengthA;

		// Token: 0x04002430 RID: 9264
		internal int[] lowerBoundA;

		// Token: 0x04002431 RID: 9265
		internal BinaryTypeEnum binaryTypeEnum;

		// Token: 0x04002432 RID: 9266
		internal object typeInformation;

		// Token: 0x04002433 RID: 9267
		internal int assemId;

		// Token: 0x04002434 RID: 9268
		private BinaryHeaderEnum binaryHeaderEnum;

		// Token: 0x04002435 RID: 9269
		internal BinaryArrayTypeEnum binaryArrayTypeEnum;
	}
}
