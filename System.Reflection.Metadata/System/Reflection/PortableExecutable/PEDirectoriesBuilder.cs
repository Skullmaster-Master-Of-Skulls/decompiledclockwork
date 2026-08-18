using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000015 RID: 21
	internal sealed class PEDirectoriesBuilder
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00005958 File Offset: 0x00003B58
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00005960 File Offset: 0x00003B60
		public int AddressOfEntryPoint { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00005969 File Offset: 0x00003B69
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00005971 File Offset: 0x00003B71
		public DirectoryEntry ExportTable { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000597A File Offset: 0x00003B7A
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00005982 File Offset: 0x00003B82
		public DirectoryEntry ImportTable { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000598B File Offset: 0x00003B8B
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00005993 File Offset: 0x00003B93
		public DirectoryEntry ResourceTable { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000599C File Offset: 0x00003B9C
		// (set) Token: 0x06000169 RID: 361 RVA: 0x000059A4 File Offset: 0x00003BA4
		public DirectoryEntry ExceptionTable { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000059AD File Offset: 0x00003BAD
		// (set) Token: 0x0600016B RID: 363 RVA: 0x000059B5 File Offset: 0x00003BB5
		public DirectoryEntry CertificateTable { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000059BE File Offset: 0x00003BBE
		// (set) Token: 0x0600016D RID: 365 RVA: 0x000059C6 File Offset: 0x00003BC6
		public DirectoryEntry BaseRelocationTable { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000059CF File Offset: 0x00003BCF
		// (set) Token: 0x0600016F RID: 367 RVA: 0x000059D7 File Offset: 0x00003BD7
		public DirectoryEntry DebugTable { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000059E0 File Offset: 0x00003BE0
		// (set) Token: 0x06000171 RID: 369 RVA: 0x000059E8 File Offset: 0x00003BE8
		public DirectoryEntry CopyrightTable { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000059F1 File Offset: 0x00003BF1
		// (set) Token: 0x06000173 RID: 371 RVA: 0x000059F9 File Offset: 0x00003BF9
		public DirectoryEntry GlobalPointerTable { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00005A02 File Offset: 0x00003C02
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00005A0A File Offset: 0x00003C0A
		public DirectoryEntry ThreadLocalStorageTable { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00005A13 File Offset: 0x00003C13
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00005A1B File Offset: 0x00003C1B
		public DirectoryEntry LoadConfigTable { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00005A24 File Offset: 0x00003C24
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00005A2C File Offset: 0x00003C2C
		public DirectoryEntry BoundImportTable { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00005A35 File Offset: 0x00003C35
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00005A3D File Offset: 0x00003C3D
		public DirectoryEntry ImportAddressTable { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00005A46 File Offset: 0x00003C46
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00005A4E File Offset: 0x00003C4E
		public DirectoryEntry DelayImportTable { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00005A57 File Offset: 0x00003C57
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00005A5F File Offset: 0x00003C5F
		public DirectoryEntry CorHeaderTable { get; set; }
	}
}
