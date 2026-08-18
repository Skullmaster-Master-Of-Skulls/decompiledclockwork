using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000017 RID: 23
	public sealed class CoffHeader
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00005A90 File Offset: 0x00003C90
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00005A98 File Offset: 0x00003C98
		public Machine Machine { get; private set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00005AA1 File Offset: 0x00003CA1
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00005AA9 File Offset: 0x00003CA9
		public short NumberOfSections { get; private set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00005AB2 File Offset: 0x00003CB2
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00005ABA File Offset: 0x00003CBA
		public int TimeDateStamp { get; private set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00005AC3 File Offset: 0x00003CC3
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00005ACB File Offset: 0x00003CCB
		public int PointerToSymbolTable { get; private set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00005AD4 File Offset: 0x00003CD4
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00005ADC File Offset: 0x00003CDC
		public int NumberOfSymbols { get; private set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00005AE5 File Offset: 0x00003CE5
		// (set) Token: 0x0600018F RID: 399 RVA: 0x00005AED File Offset: 0x00003CED
		public short SizeOfOptionalHeader { get; private set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00005AF6 File Offset: 0x00003CF6
		// (set) Token: 0x06000191 RID: 401 RVA: 0x00005AFE File Offset: 0x00003CFE
		public Characteristics Characteristics { get; private set; }

		// Token: 0x06000192 RID: 402 RVA: 0x00005B08 File Offset: 0x00003D08
		internal CoffHeader(ref PEBinaryReader reader)
		{
			this.Machine = (Machine)reader.ReadUInt16();
			this.NumberOfSections = reader.ReadInt16();
			this.TimeDateStamp = reader.ReadInt32();
			this.PointerToSymbolTable = reader.ReadInt32();
			this.NumberOfSymbols = reader.ReadInt32();
			this.SizeOfOptionalHeader = reader.ReadInt16();
			this.Characteristics = (Characteristics)reader.ReadUInt16();
		}
	}
}
