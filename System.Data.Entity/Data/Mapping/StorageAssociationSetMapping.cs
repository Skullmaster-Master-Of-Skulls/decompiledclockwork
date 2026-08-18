using System;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Mapping
{
	// Token: 0x02000239 RID: 569
	internal class StorageAssociationSetMapping : StorageSetMapping
	{
		// Token: 0x06002416 RID: 9238 RVA: 0x000828E1 File Offset: 0x00080AE1
		internal StorageAssociationSetMapping(AssociationSet extent, StorageEntityContainerMapping entityContainerMapping) : base(extent, entityContainerMapping)
		{
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002417 RID: 9239 RVA: 0x000828EB File Offset: 0x00080AEB
		// (set) Token: 0x06002418 RID: 9240 RVA: 0x000828F3 File Offset: 0x00080AF3
		internal StorageAssociationSetModificationFunctionMapping ModificationFunctionMapping
		{
			get
			{
				return this.m_modificationFunctionMapping;
			}
			set
			{
				this.m_modificationFunctionMapping = value;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06002419 RID: 9241 RVA: 0x000828FC File Offset: 0x00080AFC
		internal EntitySetBase StoreEntitySet
		{
			get
			{
				if (base.TypeMappings.Count != 0 && base.TypeMappings.First<StorageTypeMapping>().MappingFragments.Count != 0)
				{
					return base.TypeMappings.First<StorageTypeMapping>().MappingFragments.First<StorageMappingFragment>().TableSet;
				}
				return null;
			}
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x0008294C File Offset: 0x00080B4C
		internal override void Print(int index)
		{
			StorageEntityContainerMapping.GetPrettyPrintString(ref index);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AssociationSetMapping");
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
			if (this.m_modificationFunctionMapping != null)
			{
				this.m_modificationFunctionMapping.Print(index + 5);
			}
		}

		// Token: 0x04000FFF RID: 4095
		private StorageAssociationSetModificationFunctionMapping m_modificationFunctionMapping;
	}
}
