using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x0200001B RID: 27
	public struct DebugDirectoryEntry
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00005D14 File Offset: 0x00003F14
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00005D1C File Offset: 0x00003F1C
		public uint Stamp { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00005D25 File Offset: 0x00003F25
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00005D2D File Offset: 0x00003F2D
		public ushort MajorVersion { get; private set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00005D36 File Offset: 0x00003F36
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00005D3E File Offset: 0x00003F3E
		public ushort MinorVersion { get; private set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00005D47 File Offset: 0x00003F47
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00005D4F File Offset: 0x00003F4F
		public DebugDirectoryEntryType Type { get; private set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00005D58 File Offset: 0x00003F58
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00005D60 File Offset: 0x00003F60
		public int DataSize { get; private set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00005D69 File Offset: 0x00003F69
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00005D71 File Offset: 0x00003F71
		public int DataRelativeVirtualAddress { get; private set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00005D7A File Offset: 0x00003F7A
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00005D82 File Offset: 0x00003F82
		public int DataPointer { get; private set; }

		// Token: 0x060001BF RID: 447 RVA: 0x00005D8B File Offset: 0x00003F8B
		public DebugDirectoryEntry(uint stamp, ushort majorVersion, ushort minorVersion, DebugDirectoryEntryType type, int dataSize, int dataRelativeVirtualAddress, int dataPointer)
		{
			this.Stamp = stamp;
			this.MajorVersion = majorVersion;
			this.MinorVersion = minorVersion;
			this.Type = type;
			this.DataSize = dataSize;
			this.DataRelativeVirtualAddress = dataRelativeVirtualAddress;
			this.DataPointer = dataPointer;
		}
	}
}
