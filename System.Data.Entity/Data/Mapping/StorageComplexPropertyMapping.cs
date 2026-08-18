using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Mapping
{
	// Token: 0x0200023B RID: 571
	internal class StorageComplexPropertyMapping : StoragePropertyMapping
	{
		// Token: 0x06002420 RID: 9248 RVA: 0x00082B18 File Offset: 0x00080D18
		internal StorageComplexPropertyMapping(EdmProperty cdmMember) : base(cdmMember)
		{
			this.m_typeMappings = new List<StorageComplexTypeMapping>();
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06002421 RID: 9249 RVA: 0x00082B2C File Offset: 0x00080D2C
		internal ReadOnlyCollection<StorageComplexTypeMapping> TypeMappings
		{
			get
			{
				return this.m_typeMappings.AsReadOnly();
			}
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x00082B39 File Offset: 0x00080D39
		internal void AddTypeMapping(StorageComplexTypeMapping typeMapping)
		{
			this.m_typeMappings.Add(typeMapping);
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x00082B48 File Offset: 0x00080D48
		internal override void Print(int index)
		{
			StorageEntityContainerMapping.GetPrettyPrintString(ref index);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ComplexPropertyMapping");
			stringBuilder.Append("   ");
			if (this.EdmProperty != null)
			{
				stringBuilder.Append("Name:");
				stringBuilder.Append(this.EdmProperty.Name);
				stringBuilder.Append("   ");
			}
			Console.WriteLine(stringBuilder.ToString());
			foreach (StorageComplexTypeMapping storageComplexTypeMapping in this.TypeMappings)
			{
				storageComplexTypeMapping.Print(index + 5);
			}
		}

		// Token: 0x04001001 RID: 4097
		private List<StorageComplexTypeMapping> m_typeMappings;
	}
}
