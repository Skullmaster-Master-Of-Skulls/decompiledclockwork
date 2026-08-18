using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007E3 RID: 2019
	internal sealed class BinaryObjectWithMapTyped : IStreamable
	{
		// Token: 0x06004774 RID: 18292 RVA: 0x000F4CC3 File Offset: 0x000F3CC3
		internal BinaryObjectWithMapTyped()
		{
		}

		// Token: 0x06004775 RID: 18293 RVA: 0x000F4CCB File Offset: 0x000F3CCB
		internal BinaryObjectWithMapTyped(BinaryHeaderEnum binaryHeaderEnum)
		{
			this.binaryHeaderEnum = binaryHeaderEnum;
		}

		// Token: 0x06004776 RID: 18294 RVA: 0x000F4CDC File Offset: 0x000F3CDC
		internal void Set(int objectId, string name, int numMembers, string[] memberNames, BinaryTypeEnum[] binaryTypeEnumA, object[] typeInformationA, int[] memberAssemIds, int assemId)
		{
			this.objectId = objectId;
			this.assemId = assemId;
			this.name = name;
			this.numMembers = numMembers;
			this.memberNames = memberNames;
			this.binaryTypeEnumA = binaryTypeEnumA;
			this.typeInformationA = typeInformationA;
			this.memberAssemIds = memberAssemIds;
			this.assemId = assemId;
			if (assemId > 0)
			{
				this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMapTypedAssemId;
				return;
			}
			this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMapTyped;
		}

		// Token: 0x06004777 RID: 18295 RVA: 0x000F4D44 File Offset: 0x000F3D44
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte((byte)this.binaryHeaderEnum);
			sout.WriteInt32(this.objectId);
			sout.WriteString(this.name);
			sout.WriteInt32(this.numMembers);
			for (int i = 0; i < this.numMembers; i++)
			{
				sout.WriteString(this.memberNames[i]);
			}
			for (int j = 0; j < this.numMembers; j++)
			{
				sout.WriteByte((byte)this.binaryTypeEnumA[j]);
			}
			for (int k = 0; k < this.numMembers; k++)
			{
				BinaryConverter.WriteTypeInfo(this.binaryTypeEnumA[k], this.typeInformationA[k], this.memberAssemIds[k], sout);
			}
			if (this.assemId > 0)
			{
				sout.WriteInt32(this.assemId);
			}
		}

		// Token: 0x06004778 RID: 18296 RVA: 0x000F4E08 File Offset: 0x000F3E08
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.name = input.ReadString();
			this.numMembers = input.ReadInt32();
			this.memberNames = new string[this.numMembers];
			this.binaryTypeEnumA = new BinaryTypeEnum[this.numMembers];
			this.typeInformationA = new object[this.numMembers];
			this.memberAssemIds = new int[this.numMembers];
			for (int i = 0; i < this.numMembers; i++)
			{
				this.memberNames[i] = input.ReadString();
			}
			for (int j = 0; j < this.numMembers; j++)
			{
				this.binaryTypeEnumA[j] = (BinaryTypeEnum)input.ReadByte();
			}
			for (int k = 0; k < this.numMembers; k++)
			{
				if (this.binaryTypeEnumA[k] != BinaryTypeEnum.ObjectUrt && this.binaryTypeEnumA[k] != BinaryTypeEnum.ObjectUser)
				{
					this.typeInformationA[k] = BinaryConverter.ReadTypeInfo(this.binaryTypeEnumA[k], input, out this.memberAssemIds[k]);
				}
				else
				{
					BinaryConverter.ReadTypeInfo(this.binaryTypeEnumA[k], input, out this.memberAssemIds[k]);
				}
			}
			if (this.binaryHeaderEnum == BinaryHeaderEnum.ObjectWithMapTypedAssemId)
			{
				this.assemId = input.ReadInt32();
			}
		}

		// Token: 0x04002424 RID: 9252
		internal BinaryHeaderEnum binaryHeaderEnum;

		// Token: 0x04002425 RID: 9253
		internal int objectId;

		// Token: 0x04002426 RID: 9254
		internal string name;

		// Token: 0x04002427 RID: 9255
		internal int numMembers;

		// Token: 0x04002428 RID: 9256
		internal string[] memberNames;

		// Token: 0x04002429 RID: 9257
		internal BinaryTypeEnum[] binaryTypeEnumA;

		// Token: 0x0400242A RID: 9258
		internal object[] typeInformationA;

		// Token: 0x0400242B RID: 9259
		internal int[] memberAssemIds;

		// Token: 0x0400242C RID: 9260
		internal int assemId;
	}
}
