using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x0200012D RID: 301
	internal struct ScalarEncoder
	{
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0001CF07 File Offset: 0x0001B107
		public BlobBuilder Builder { get; }

		// Token: 0x060009E1 RID: 2529 RVA: 0x0001CF0F File Offset: 0x0001B10F
		public ScalarEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0001CF18 File Offset: 0x0001B118
		public void NullArray()
		{
			this.Builder.WriteInt32(-1);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0001CF28 File Offset: 0x0001B128
		public void Constant(object value)
		{
			string text = value as string;
			if (text != null || value == null)
			{
				this.String(text);
				return;
			}
			this.Builder.WriteConstant(value);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0001CF56 File Offset: 0x0001B156
		public void SystemType(string serializedTypeName)
		{
			this.String(serializedTypeName);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0001CF5F File Offset: 0x0001B15F
		private void String(string value)
		{
			this.Builder.WriteSerializedString(value);
		}
	}
}
