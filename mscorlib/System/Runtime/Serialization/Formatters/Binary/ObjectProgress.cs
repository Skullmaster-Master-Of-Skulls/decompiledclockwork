using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007EA RID: 2026
	internal sealed class ObjectProgress
	{
		// Token: 0x0600479D RID: 18333 RVA: 0x000F575C File Offset: 0x000F475C
		internal ObjectProgress()
		{
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x000F5778 File Offset: 0x000F4778
		[Conditional("SER_LOGGING")]
		private void Counter()
		{
			lock (this)
			{
				this.opRecordId = ObjectProgress.opRecordIdCount++;
				if (ObjectProgress.opRecordIdCount > 1000)
				{
					ObjectProgress.opRecordIdCount = 1;
				}
			}
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x000F57CC File Offset: 0x000F47CC
		internal void Init()
		{
			this.isInitial = false;
			this.count = 0;
			this.expectedType = BinaryTypeEnum.ObjectUrt;
			this.expectedTypeInformation = null;
			this.name = null;
			this.objectTypeEnum = InternalObjectTypeE.Empty;
			this.memberTypeEnum = InternalMemberTypeE.Empty;
			this.memberValueEnum = InternalMemberValueE.Empty;
			this.dtType = null;
			this.numItems = 0;
			this.nullCount = 0;
			this.typeInformation = null;
			this.memberLength = 0;
			this.binaryTypeEnumA = null;
			this.typeInformationA = null;
			this.memberNames = null;
			this.memberTypes = null;
			this.pr.Init();
		}

		// Token: 0x060047A0 RID: 18336 RVA: 0x000F585B File Offset: 0x000F485B
		internal void ArrayCountIncrement(int value)
		{
			this.count += value;
		}

		// Token: 0x060047A1 RID: 18337 RVA: 0x000F586C File Offset: 0x000F486C
		internal bool GetNext(out BinaryTypeEnum outBinaryTypeEnum, out object outTypeInformation)
		{
			outBinaryTypeEnum = BinaryTypeEnum.Primitive;
			outTypeInformation = null;
			if (this.objectTypeEnum == InternalObjectTypeE.Array)
			{
				if (this.count == this.numItems)
				{
					return false;
				}
				outBinaryTypeEnum = this.binaryTypeEnum;
				outTypeInformation = this.typeInformation;
				if (this.count == 0)
				{
					this.isInitial = false;
				}
				this.count++;
				return true;
			}
			else
			{
				if (this.count == this.memberLength && !this.isInitial)
				{
					return false;
				}
				outBinaryTypeEnum = this.binaryTypeEnumA[this.count];
				outTypeInformation = this.typeInformationA[this.count];
				if (this.count == 0)
				{
					this.isInitial = false;
				}
				this.name = this.memberNames[this.count];
				Type[] array = this.memberTypes;
				this.dtType = this.memberTypes[this.count];
				this.count++;
				return true;
			}
		}

		// Token: 0x04002445 RID: 9285
		internal static int opRecordIdCount = 1;

		// Token: 0x04002446 RID: 9286
		internal int opRecordId;

		// Token: 0x04002447 RID: 9287
		internal bool isInitial;

		// Token: 0x04002448 RID: 9288
		internal int count;

		// Token: 0x04002449 RID: 9289
		internal BinaryTypeEnum expectedType = BinaryTypeEnum.ObjectUrt;

		// Token: 0x0400244A RID: 9290
		internal object expectedTypeInformation;

		// Token: 0x0400244B RID: 9291
		internal string name;

		// Token: 0x0400244C RID: 9292
		internal InternalObjectTypeE objectTypeEnum;

		// Token: 0x0400244D RID: 9293
		internal InternalMemberTypeE memberTypeEnum;

		// Token: 0x0400244E RID: 9294
		internal InternalMemberValueE memberValueEnum;

		// Token: 0x0400244F RID: 9295
		internal Type dtType;

		// Token: 0x04002450 RID: 9296
		internal int numItems;

		// Token: 0x04002451 RID: 9297
		internal BinaryTypeEnum binaryTypeEnum;

		// Token: 0x04002452 RID: 9298
		internal object typeInformation;

		// Token: 0x04002453 RID: 9299
		internal int nullCount;

		// Token: 0x04002454 RID: 9300
		internal int memberLength;

		// Token: 0x04002455 RID: 9301
		internal BinaryTypeEnum[] binaryTypeEnumA;

		// Token: 0x04002456 RID: 9302
		internal object[] typeInformationA;

		// Token: 0x04002457 RID: 9303
		internal string[] memberNames;

		// Token: 0x04002458 RID: 9304
		internal Type[] memberTypes;

		// Token: 0x04002459 RID: 9305
		internal ParseRecord pr = new ParseRecord();
	}
}
