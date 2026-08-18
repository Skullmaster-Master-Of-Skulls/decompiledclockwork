using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Mapping
{
	// Token: 0x0200023A RID: 570
	internal class StorageAssociationTypeMapping : StorageTypeMapping
	{
		// Token: 0x0600241B RID: 9243 RVA: 0x00082A30 File Offset: 0x00080C30
		internal StorageAssociationTypeMapping(AssociationType relation, StorageSetMapping setMapping) : base(setMapping)
		{
			this.m_relation = relation;
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x0600241C RID: 9244 RVA: 0x00082A40 File Offset: 0x00080C40
		internal AssociationType AssociationType
		{
			get
			{
				return this.m_relation;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x0600241D RID: 9245 RVA: 0x00082A48 File Offset: 0x00080C48
		internal override ReadOnlyCollection<EdmType> Types
		{
			get
			{
				return new ReadOnlyCollection<EdmType>(new AssociationType[]
				{
					this.m_relation
				});
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600241E RID: 9246 RVA: 0x00082A5E File Offset: 0x00080C5E
		internal override ReadOnlyCollection<EdmType> IsOfTypes
		{
			get
			{
				return new List<EdmType>().AsReadOnly();
			}
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x00082A6C File Offset: 0x00080C6C
		internal override void Print(int index)
		{
			StorageEntityContainerMapping.GetPrettyPrintString(ref index);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AssociationTypeMapping");
			stringBuilder.Append("   ");
			stringBuilder.Append("Type Name:");
			stringBuilder.Append(this.m_relation.Name);
			stringBuilder.Append("   ");
			Console.WriteLine(stringBuilder.ToString());
			foreach (StorageMappingFragment storageMappingFragment in base.MappingFragments)
			{
				storageMappingFragment.Print(index + 5);
			}
		}

		// Token: 0x04001000 RID: 4096
		private AssociationType m_relation;
	}
}
