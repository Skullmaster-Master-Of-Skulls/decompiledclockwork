using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.Text;

namespace System.Data.Mapping
{
	// Token: 0x02000242 RID: 578
	internal class StorageEntitySetMapping : StorageSetMapping
	{
		// Token: 0x06002466 RID: 9318 RVA: 0x00083A11 File Offset: 0x00081C11
		internal StorageEntitySetMapping(EntitySet extent, StorageEntityContainerMapping entityContainerMapping) : base(extent, entityContainerMapping)
		{
			this.m_modificationFunctionMappings = new List<StorageEntityTypeModificationFunctionMapping>();
			this.m_implicitlyMappedAssociationSetEnds = new List<AssociationSetEnd>();
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06002467 RID: 9319 RVA: 0x00083A31 File Offset: 0x00081C31
		internal IList<StorageEntityTypeModificationFunctionMapping> ModificationFunctionMappings
		{
			get
			{
				return this.m_modificationFunctionMappings.AsReadOnly();
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06002468 RID: 9320 RVA: 0x00083A3E File Offset: 0x00081C3E
		internal IList<AssociationSetEnd> ImplicitlyMappedAssociationSetEnds
		{
			get
			{
				return this.m_implicitlyMappedAssociationSetEnds.AsReadOnly();
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06002469 RID: 9321 RVA: 0x00083A4B File Offset: 0x00081C4B
		internal override bool HasNoContent
		{
			get
			{
				return this.m_modificationFunctionMappings.Count == 0 && base.HasNoContent;
			}
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x00083A64 File Offset: 0x00081C64
		internal override void Print(int index)
		{
			StorageEntityContainerMapping.GetPrettyPrintString(ref index);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("EntitySetMapping");
			stringBuilder.Append("   ");
			stringBuilder.Append("Name:");
			stringBuilder.Append(base.Set.Name);
			if (base.QueryView != null)
			{
				stringBuilder.Append("   ");
				stringBuilder.Append("Query View:");
				stringBuilder.Append(base.QueryView);
			}
			Console.WriteLine(stringBuilder.ToString());
			foreach (StorageTypeMapping storageTypeMapping in base.TypeMappings)
			{
				storageTypeMapping.Print(index + 5);
			}
			foreach (StorageEntityTypeModificationFunctionMapping storageEntityTypeModificationFunctionMapping in this.m_modificationFunctionMappings)
			{
				storageEntityTypeModificationFunctionMapping.Print(index + 10);
			}
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x00083B78 File Offset: 0x00081D78
		internal void AddModificationFunctionMapping(StorageEntityTypeModificationFunctionMapping modificationFunctionMapping)
		{
			this.m_modificationFunctionMappings.Add(modificationFunctionMapping);
			if (modificationFunctionMapping.DeleteFunctionMapping != null)
			{
				this.m_implicitlyMappedAssociationSetEnds.AddRange(modificationFunctionMapping.DeleteFunctionMapping.CollocatedAssociationSetEnds);
			}
			if (modificationFunctionMapping.InsertFunctionMapping != null)
			{
				this.m_implicitlyMappedAssociationSetEnds.AddRange(modificationFunctionMapping.InsertFunctionMapping.CollocatedAssociationSetEnds);
			}
			if (modificationFunctionMapping.UpdateFunctionMapping != null)
			{
				this.m_implicitlyMappedAssociationSetEnds.AddRange(modificationFunctionMapping.UpdateFunctionMapping.CollocatedAssociationSetEnds);
			}
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x00083BEC File Offset: 0x00081DEC
		[Conditional("DEBUG")]
		internal void AssertModificationFunctionMappingInvariants(StorageEntityTypeModificationFunctionMapping modificationFunctionMapping)
		{
			foreach (StorageEntityTypeModificationFunctionMapping storageEntityTypeModificationFunctionMapping in this.m_modificationFunctionMappings)
			{
			}
		}

		// Token: 0x04001020 RID: 4128
		private readonly List<StorageEntityTypeModificationFunctionMapping> m_modificationFunctionMappings;

		// Token: 0x04001021 RID: 4129
		private readonly List<AssociationSetEnd> m_implicitlyMappedAssociationSetEnds;
	}
}
