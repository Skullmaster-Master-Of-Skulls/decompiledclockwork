using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x02000135 RID: 309
	internal struct CustomAttributeElementTypeEncoder
	{
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0001D0FA File Offset: 0x0001B2FA
		public BlobBuilder Builder { get; }

		// Token: 0x06000A01 RID: 2561 RVA: 0x0001D102 File Offset: 0x0001B302
		public CustomAttributeElementTypeEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0001D10B File Offset: 0x0001B30B
		private void WriteTypeCode(SignatureTypeCode value)
		{
			this.Builder.WriteByte((byte)value);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0001D119 File Offset: 0x0001B319
		public void Boolean()
		{
			this.WriteTypeCode(SignatureTypeCode.Boolean);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0001D122 File Offset: 0x0001B322
		public void Char()
		{
			this.WriteTypeCode(SignatureTypeCode.Char);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0001D12B File Offset: 0x0001B32B
		public void Int8()
		{
			this.WriteTypeCode(SignatureTypeCode.SByte);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0001D134 File Offset: 0x0001B334
		public void UInt8()
		{
			this.WriteTypeCode(SignatureTypeCode.Byte);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0001D13D File Offset: 0x0001B33D
		public void Int16()
		{
			this.WriteTypeCode(SignatureTypeCode.Int16);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0001D146 File Offset: 0x0001B346
		public void UInt16()
		{
			this.WriteTypeCode(SignatureTypeCode.UInt16);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0001D14F File Offset: 0x0001B34F
		public void Int32()
		{
			this.WriteTypeCode(SignatureTypeCode.Int32);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0001D158 File Offset: 0x0001B358
		public void UInt32()
		{
			this.WriteTypeCode(SignatureTypeCode.UInt32);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0001D162 File Offset: 0x0001B362
		public void Int64()
		{
			this.WriteTypeCode(SignatureTypeCode.Int64);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0001D16C File Offset: 0x0001B36C
		public void UInt64()
		{
			this.WriteTypeCode(SignatureTypeCode.UInt64);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0001D176 File Offset: 0x0001B376
		public void Float32()
		{
			this.WriteTypeCode(SignatureTypeCode.Single);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0001D180 File Offset: 0x0001B380
		public void Float64()
		{
			this.WriteTypeCode(SignatureTypeCode.Double);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0001D18A File Offset: 0x0001B38A
		public void String()
		{
			this.WriteTypeCode(SignatureTypeCode.String);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0001D194 File Offset: 0x0001B394
		public void IntPtr()
		{
			this.WriteTypeCode(SignatureTypeCode.IntPtr);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0001D19E File Offset: 0x0001B39E
		public void UIntPtr()
		{
			this.WriteTypeCode(SignatureTypeCode.UIntPtr);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0001D1A8 File Offset: 0x0001B3A8
		public void SystemType()
		{
			this.Builder.WriteByte(80);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0001D1B7 File Offset: 0x0001B3B7
		public void Enum(string enumTypeName)
		{
			this.Builder.WriteByte(85);
			this.Builder.WriteSerializedString(enumTypeName);
		}
	}
}
