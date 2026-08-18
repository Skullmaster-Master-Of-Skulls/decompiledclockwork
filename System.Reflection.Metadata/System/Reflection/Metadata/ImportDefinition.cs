using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200009B RID: 155
	public struct ImportDefinition
	{
		// Token: 0x060006C2 RID: 1730 RVA: 0x0000F7FE File Offset: 0x0000D9FE
		internal ImportDefinition(ImportDefinitionKind kind, BlobHandle alias = default(BlobHandle), AssemblyReferenceHandle assembly = default(AssemblyReferenceHandle), Handle typeOrNamespace = default(Handle))
		{
			this._kind = kind;
			this._alias = alias;
			this._assembly = assembly;
			this._typeOrNamespace = typeOrNamespace;
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0000F81D File Offset: 0x0000DA1D
		public ImportDefinitionKind Kind
		{
			get
			{
				return this._kind;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0000F825 File Offset: 0x0000DA25
		public BlobHandle Alias
		{
			get
			{
				return this._alias;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0000F82D File Offset: 0x0000DA2D
		public AssemblyReferenceHandle TargetAssembly
		{
			get
			{
				return this._assembly;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0000F835 File Offset: 0x0000DA35
		public BlobHandle TargetNamespace
		{
			get
			{
				return (BlobHandle)this._typeOrNamespace;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0000F842 File Offset: 0x0000DA42
		public EntityHandle TargetType
		{
			get
			{
				return (EntityHandle)this._typeOrNamespace;
			}
		}

		// Token: 0x040003FC RID: 1020
		private readonly ImportDefinitionKind _kind;

		// Token: 0x040003FD RID: 1021
		private readonly BlobHandle _alias;

		// Token: 0x040003FE RID: 1022
		private readonly AssemblyReferenceHandle _assembly;

		// Token: 0x040003FF RID: 1023
		private readonly Handle _typeOrNamespace;
	}
}
