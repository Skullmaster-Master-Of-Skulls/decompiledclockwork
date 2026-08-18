using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000085 RID: 133
	public struct ModuleDefinition
	{
		// Token: 0x06000610 RID: 1552 RVA: 0x0000EB24 File Offset: 0x0000CD24
		internal ModuleDefinition(MetadataReader reader)
		{
			this._reader = reader;
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0000EB2D File Offset: 0x0000CD2D
		public int Generation
		{
			get
			{
				return (int)this._reader.ModuleTable.GetGeneration();
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x0000EB3F File Offset: 0x0000CD3F
		public StringHandle Name
		{
			get
			{
				return this._reader.ModuleTable.GetName();
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0000EB51 File Offset: 0x0000CD51
		public GuidHandle Mvid
		{
			get
			{
				return this._reader.ModuleTable.GetMvid();
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0000EB63 File Offset: 0x0000CD63
		public GuidHandle GenerationId
		{
			get
			{
				return this._reader.ModuleTable.GetEncId();
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0000EB75 File Offset: 0x0000CD75
		public GuidHandle BaseGenerationId
		{
			get
			{
				return this._reader.ModuleTable.GetEncBaseId();
			}
		}

		// Token: 0x040003C5 RID: 965
		private readonly MetadataReader _reader;
	}
}
