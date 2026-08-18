using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Mapping
{
	// Token: 0x0200023E RID: 574
	internal class StorageEndPropertyMapping : StoragePropertyMapping
	{
		// Token: 0x06002434 RID: 9268 RVA: 0x000831EF File Offset: 0x000813EF
		internal StorageEndPropertyMapping(EdmProperty member) : base(member)
		{
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06002435 RID: 9269 RVA: 0x00083203 File Offset: 0x00081403
		internal ReadOnlyCollection<StoragePropertyMapping> Properties
		{
			get
			{
				return this.m_properties.AsReadOnly();
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06002436 RID: 9270 RVA: 0x00083210 File Offset: 0x00081410
		// (set) Token: 0x06002437 RID: 9271 RVA: 0x00083218 File Offset: 0x00081418
		internal RelationshipEndMember EndMember
		{
			get
			{
				return this.m_endMember;
			}
			set
			{
				this.m_endMember = value;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06002438 RID: 9272 RVA: 0x00083221 File Offset: 0x00081421
		internal IEnumerable<EdmMember> StoreProperties
		{
			get
			{
				return (from propertyMap in this.m_properties.OfType<StorageScalarPropertyMapping>()
				select propertyMap.ColumnProperty).Cast<EdmMember>();
			}
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x00083257 File Offset: 0x00081457
		internal void AddProperty(StoragePropertyMapping prop)
		{
			this.m_properties.Add(prop);
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x00083268 File Offset: 0x00081468
		internal override void Print(int index)
		{
			StorageEntityContainerMapping.GetPrettyPrintString(ref index);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("EndPropertyMapping");
			stringBuilder.Append("   ");
			if (this.EndMember != null)
			{
				stringBuilder.Append("Name:");
				stringBuilder.Append(this.EndMember.Name);
				stringBuilder.Append("   ");
				stringBuilder.Append("TypeName:");
				stringBuilder.Append(this.EndMember.TypeUsage.EdmType.FullName);
			}
			stringBuilder.Append("   ");
			Console.WriteLine(stringBuilder.ToString());
			foreach (StoragePropertyMapping storagePropertyMapping in this.Properties)
			{
				storagePropertyMapping.Print(index + 5);
			}
		}

		// Token: 0x0400100A RID: 4106
		private List<StoragePropertyMapping> m_properties = new List<StoragePropertyMapping>();

		// Token: 0x0400100B RID: 4107
		private RelationshipEndMember m_endMember;
	}
}
