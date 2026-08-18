using System;

namespace EncryptionClassLibrary
{
	// Token: 0x0200000D RID: 13
	internal class CustomPair<S, T>
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00002FE3 File Offset: 0x000011E3
		public CustomPair()
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003ADC File Offset: 0x00001CDC
		public CustomPair(S item1, T item2)
		{
			this.Item1 = item1;
			this.Item2 = item2;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003AF6 File Offset: 0x00001CF6
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00003AFE File Offset: 0x00001CFE
		public S Item1 { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003B07 File Offset: 0x00001D07
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00003B0F File Offset: 0x00001D0F
		public T Item2 { get; set; }
	}
}
