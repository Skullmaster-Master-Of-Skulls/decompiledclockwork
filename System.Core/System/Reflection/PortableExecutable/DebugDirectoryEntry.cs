using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000040 RID: 64
	internal struct DebugDirectoryEntry
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600019E RID: 414 RVA: 0x000042BD File Offset: 0x000024BD
		public uint Stamp { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600019F RID: 415 RVA: 0x000042C5 File Offset: 0x000024C5
		public ushort MajorVersion { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000042CD File Offset: 0x000024CD
		public ushort MinorVersion { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000042D5 File Offset: 0x000024D5
		public DebugDirectoryEntryType Type { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000042DD File Offset: 0x000024DD
		public int DataSize { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x000042E5 File Offset: 0x000024E5
		public int DataRelativeVirtualAddress { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000042ED File Offset: 0x000024ED
		public int DataPointer { get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x000042F5 File Offset: 0x000024F5
		public bool IsPortableCodeView
		{
			get
			{
				return this.MinorVersion == 20557;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00004304 File Offset: 0x00002504
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

		// Token: 0x04000216 RID: 534
		internal const int Size = 28;
	}
}
