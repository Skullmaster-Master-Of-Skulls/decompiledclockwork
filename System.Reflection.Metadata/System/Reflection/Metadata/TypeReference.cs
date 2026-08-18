using System;
using System.Reflection.Metadata.Ecma335;

namespace System.Reflection.Metadata
{
	// Token: 0x020000B1 RID: 177
	public struct TypeReference
	{
		// Token: 0x0600073D RID: 1853 RVA: 0x0001059C File Offset: 0x0000E79C
		internal TypeReference(MetadataReader reader, uint treatmentAndRowId)
		{
			this._reader = reader;
			this._treatmentAndRowId = treatmentAndRowId;
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x000105AC File Offset: 0x0000E7AC
		private int RowId
		{
			get
			{
				return (int)(this._treatmentAndRowId & 16777215U);
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x000105BA File Offset: 0x0000E7BA
		private TypeRefTreatment Treatment
		{
			get
			{
				return (TypeRefTreatment)(this._treatmentAndRowId >> 24);
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x000105C6 File Offset: 0x0000E7C6
		private TypeReferenceHandle Handle
		{
			get
			{
				return TypeReferenceHandle.FromRowId(this.RowId);
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x000105D3 File Offset: 0x0000E7D3
		public EntityHandle ResolutionScope
		{
			get
			{
				if (this.Treatment == TypeRefTreatment.None)
				{
					return this._reader.TypeRefTable.GetResolutionScope(this.Handle);
				}
				return this.GetProjectedResolutionScope();
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x000105FA File Offset: 0x0000E7FA
		public StringHandle Name
		{
			get
			{
				if (this.Treatment == TypeRefTreatment.None)
				{
					return this._reader.TypeRefTable.GetName(this.Handle);
				}
				return this.GetProjectedName();
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00010621 File Offset: 0x0000E821
		public StringHandle Namespace
		{
			get
			{
				if (this.Treatment == TypeRefTreatment.None)
				{
					return this._reader.TypeRefTable.GetNamespace(this.Handle);
				}
				return this.GetProjectedNamespace();
			}
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00010648 File Offset: 0x0000E848
		private EntityHandle GetProjectedResolutionScope()
		{
			switch (this.Treatment)
			{
			case TypeRefTreatment.SystemDelegate:
			case TypeRefTreatment.SystemAttribute:
				return AssemblyReferenceHandle.FromVirtualIndex(AssemblyReferenceHandle.VirtualIndex.System_Runtime);
			case TypeRefTreatment.UseProjectionInfo:
				return MetadataReader.GetProjectedAssemblyRef(this.RowId);
			default:
				return default(AssemblyReferenceHandle);
			}
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001069D File Offset: 0x0000E89D
		private StringHandle GetProjectedName()
		{
			if (this.Treatment == TypeRefTreatment.UseProjectionInfo)
			{
				return MetadataReader.GetProjectedName(this.RowId);
			}
			return this._reader.TypeRefTable.GetName(this.Handle);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x000106CC File Offset: 0x0000E8CC
		private StringHandle GetProjectedNamespace()
		{
			switch (this.Treatment)
			{
			case TypeRefTreatment.SystemDelegate:
			case TypeRefTreatment.SystemAttribute:
				return StringHandle.FromVirtualIndex(StringHandle.VirtualIndex.System);
			case TypeRefTreatment.UseProjectionInfo:
				return MetadataReader.GetProjectedNamespace(this.RowId);
			default:
				return default(StringHandle);
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x00010713 File Offset: 0x0000E913
		internal TypeRefSignatureTreatment SignatureTreatment
		{
			get
			{
				if (this.Treatment == TypeRefTreatment.None)
				{
					return TypeRefSignatureTreatment.None;
				}
				return this.GetProjectedSignatureTreatment();
			}
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00010725 File Offset: 0x0000E925
		private TypeRefSignatureTreatment GetProjectedSignatureTreatment()
		{
			if (this.Treatment == TypeRefTreatment.UseProjectionInfo)
			{
				return MetadataReader.GetProjectedSignatureTreatment(this.RowId);
			}
			return TypeRefSignatureTreatment.None;
		}

		// Token: 0x04000472 RID: 1138
		private readonly MetadataReader _reader;

		// Token: 0x04000473 RID: 1139
		private readonly uint _treatmentAndRowId;
	}
}
