using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007E7 RID: 2023
	internal sealed class ObjectNull : IStreamable
	{
		// Token: 0x0600478B RID: 18315 RVA: 0x000F53D8 File Offset: 0x000F43D8
		internal ObjectNull()
		{
		}

		// Token: 0x0600478C RID: 18316 RVA: 0x000F53E0 File Offset: 0x000F43E0
		internal void SetNullCount(int nullCount)
		{
			this.nullCount = nullCount;
		}

		// Token: 0x0600478D RID: 18317 RVA: 0x000F53EC File Offset: 0x000F43EC
		public void Write(__BinaryWriter sout)
		{
			if (this.nullCount == 1)
			{
				sout.WriteByte(10);
				return;
			}
			if (this.nullCount < 256)
			{
				sout.WriteByte(13);
				sout.WriteByte((byte)this.nullCount);
				return;
			}
			sout.WriteByte(14);
			sout.WriteInt32(this.nullCount);
		}

		// Token: 0x0600478E RID: 18318 RVA: 0x000F5442 File Offset: 0x000F4442
		public void Read(__BinaryParser input)
		{
			this.Read(input, BinaryHeaderEnum.ObjectNull);
		}

		// Token: 0x0600478F RID: 18319 RVA: 0x000F5450 File Offset: 0x000F4450
		public void Read(__BinaryParser input, BinaryHeaderEnum binaryHeaderEnum)
		{
			switch (binaryHeaderEnum)
			{
			case BinaryHeaderEnum.ObjectNull:
				this.nullCount = 1;
				return;
			case BinaryHeaderEnum.MessageEnd:
			case BinaryHeaderEnum.Assembly:
				break;
			case BinaryHeaderEnum.ObjectNullMultiple256:
				this.nullCount = (int)input.ReadByte();
				return;
			case BinaryHeaderEnum.ObjectNullMultiple:
				this.nullCount = input.ReadInt32();
				break;
			default:
				return;
			}
		}

		// Token: 0x06004790 RID: 18320 RVA: 0x000F549E File Offset: 0x000F449E
		public void Dump()
		{
		}

		// Token: 0x06004791 RID: 18321 RVA: 0x000F54A0 File Offset: 0x000F44A0
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			if (BCLDebug.CheckEnabled("BINARY"))
			{
				if (this.nullCount == 1)
				{
					return;
				}
				if (this.nullCount < 256)
				{
				}
			}
		}

		// Token: 0x04002439 RID: 9273
		internal int nullCount;
	}
}
