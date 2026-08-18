using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200002F RID: 47
	public struct AssemblyDefinition
	{
		// Token: 0x06000264 RID: 612 RVA: 0x0000732C File Offset: 0x0000552C
		internal AssemblyDefinition(MetadataReader reader)
		{
			this._reader = reader;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00007335 File Offset: 0x00005535
		public AssemblyHashAlgorithm HashAlgorithm
		{
			get
			{
				return this._reader.AssemblyTable.GetHashAlgorithm();
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00007347 File Offset: 0x00005547
		public Version Version
		{
			get
			{
				return this._reader.AssemblyTable.GetVersion();
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00007359 File Offset: 0x00005559
		public AssemblyFlags Flags
		{
			get
			{
				return this._reader.AssemblyTable.GetFlags();
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000736B File Offset: 0x0000556B
		public StringHandle Name
		{
			get
			{
				return this._reader.AssemblyTable.GetName();
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000737D File Offset: 0x0000557D
		public StringHandle Culture
		{
			get
			{
				return this._reader.AssemblyTable.GetCulture();
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000738F File Offset: 0x0000558F
		public BlobHandle PublicKey
		{
			get
			{
				return this._reader.AssemblyTable.GetPublicKey();
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000073A1 File Offset: 0x000055A1
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, EntityHandle.AssemblyDefinition);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000073B8 File Offset: 0x000055B8
		public DeclarativeSecurityAttributeHandleCollection GetDeclarativeSecurityAttributes()
		{
			return new DeclarativeSecurityAttributeHandleCollection(this._reader, EntityHandle.AssemblyDefinition);
		}

		// Token: 0x04000263 RID: 611
		private readonly MetadataReader _reader;
	}
}
