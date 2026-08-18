using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000019 RID: 25
	public sealed class CorHeader
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00005B6F File Offset: 0x00003D6F
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00005B77 File Offset: 0x00003D77
		public ushort MajorRuntimeVersion { get; private set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00005B80 File Offset: 0x00003D80
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00005B88 File Offset: 0x00003D88
		public ushort MinorRuntimeVersion { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00005B91 File Offset: 0x00003D91
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00005B99 File Offset: 0x00003D99
		public DirectoryEntry MetadataDirectory { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00005BA2 File Offset: 0x00003DA2
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00005BAA File Offset: 0x00003DAA
		public CorFlags Flags { get; private set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00005BB3 File Offset: 0x00003DB3
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00005BBB File Offset: 0x00003DBB
		public int EntryPointTokenOrRelativeVirtualAddress { get; private set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00005BC4 File Offset: 0x00003DC4
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00005BCC File Offset: 0x00003DCC
		public DirectoryEntry ResourcesDirectory { get; private set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00005BD5 File Offset: 0x00003DD5
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00005BDD File Offset: 0x00003DDD
		public DirectoryEntry StrongNameSignatureDirectory { get; private set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00005BE6 File Offset: 0x00003DE6
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00005BEE File Offset: 0x00003DEE
		public DirectoryEntry CodeManagerTableDirectory { get; private set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00005BF7 File Offset: 0x00003DF7
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00005BFF File Offset: 0x00003DFF
		public DirectoryEntry VtableFixupsDirectory { get; private set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00005C08 File Offset: 0x00003E08
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00005C10 File Offset: 0x00003E10
		public DirectoryEntry ExportAddressTableJumpsDirectory { get; private set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00005C19 File Offset: 0x00003E19
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00005C21 File Offset: 0x00003E21
		public DirectoryEntry ManagedNativeHeaderDirectory { get; private set; }

		// Token: 0x060001A9 RID: 425 RVA: 0x00005C2C File Offset: 0x00003E2C
		internal CorHeader(ref PEBinaryReader reader)
		{
			reader.ReadInt32();
			this.MajorRuntimeVersion = reader.ReadUInt16();
			this.MinorRuntimeVersion = reader.ReadUInt16();
			this.MetadataDirectory = new DirectoryEntry(ref reader);
			this.Flags = (CorFlags)reader.ReadUInt32();
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
