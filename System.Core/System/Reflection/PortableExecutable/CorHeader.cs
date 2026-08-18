using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x0200003E RID: 62
	internal sealed class CorHeader
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00004197 File Offset: 0x00002397
		public ushort MajorRuntimeVersion { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000419F File Offset: 0x0000239F
		public ushort MinorRuntimeVersion { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000041A7 File Offset: 0x000023A7
		public DirectoryEntry MetadataDirectory { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000041AF File Offset: 0x000023AF
		public CorFlags Flags { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000041B7 File Offset: 0x000023B7
		public int EntryPointTokenOrRelativeVirtualAddress { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000041BF File Offset: 0x000023BF
		public DirectoryEntry ResourcesDirectory { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000041C7 File Offset: 0x000023C7
		public DirectoryEntry StrongNameSignatureDirectory { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000041CF File Offset: 0x000023CF
		public DirectoryEntry CodeManagerTableDirectory { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000196 RID: 406 RVA: 0x000041D7 File Offset: 0x000023D7
		public DirectoryEntry VtableFixupsDirectory { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000041DF File Offset: 0x000023DF
		public DirectoryEntry ExportAddressTableJumpsDirectory { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000198 RID: 408 RVA: 0x000041E7 File Offset: 0x000023E7
		public DirectoryEntry ManagedNativeHeaderDirectory { get; }

		// Token: 0x06000199 RID: 409 RVA: 0x000041F0 File Offset: 0x000023F0
		internal CorHeader(ref PEBinaryReader reader)
		{
			reader.ReadInt32();
			this.MajorRuntimeVersion = reader.ReadUInt16();
			this.MinorRuntimeVersion = reader.ReadUInt16();
			this.MetadataDirectory = new DirectoryEntry(ref reader);
			this.Flags = reader.ReadUInt32();
			this.EntryPointTokenOrRelativeVirtualAddress = reader.ReadInt32();
			this.ResourcesDirectory = new DirectoryEntry(ref reader);
			this.StrongNameSignatureDirectory = new DirectoryEntry(ref reader);
			this.CodeManagerTableDirectory = new DirectoryEntry(ref reader);
			this.VtableFixupsDirectory = new DirectoryEntry(ref reader);
			this.ExportAddressTableJumpsDirectory = new DirectoryEntry(ref reader);
			this.ManagedNativeHeaderDirectory = new DirectoryEntry(ref reader);
		}
	}
}
