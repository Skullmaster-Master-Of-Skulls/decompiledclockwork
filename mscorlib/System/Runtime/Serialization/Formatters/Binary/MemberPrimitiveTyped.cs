using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007E1 RID: 2017
	internal sealed class MemberPrimitiveTyped : IStreamable
	{
		// Token: 0x06004767 RID: 18279 RVA: 0x000F4AE3 File Offset: 0x000F3AE3
		internal MemberPrimitiveTyped()
		{
		}

		// Token: 0x06004768 RID: 18280 RVA: 0x000F4AEB File Offset: 0x000F3AEB
		internal void Set(InternalPrimitiveTypeE primitiveTypeEnum, object value)
		{
			this.primitiveTypeEnum = primitiveTypeEnum;
			this.value = value;
		}

		// Token: 0x06004769 RID: 18281 RVA: 0x000F4AFB File Offset: 0x000F3AFB
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(8);
			sout.WriteByte((byte)this.primitiveTypeEnum);
			sout.WriteValue(this.primitiveTypeEnum, this.value);
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x000F4B23 File Offset: 0x000F3B23
		public void Read(__BinaryParser input)
		{
			this.primitiveTypeEnum = (InternalPrimitiveTypeE)input.ReadByte();
			this.value = input.ReadValue(this.primitiveTypeEnum);
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x000F4B43 File Offset: 0x000F3B43
		public void Dump()
		{
		}

		// Token: 0x0600476C RID: 18284 RVA: 0x000F4B45 File Offset: 0x000F3B45
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x0400241C RID: 9244
		internal InternalPrimitiveTypeE primitiveTypeEnum;

		// Token: 0x0400241D RID: 9245
		internal object value;
	}
}
