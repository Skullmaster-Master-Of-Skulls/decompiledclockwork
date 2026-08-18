using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004D4 RID: 1236
	public class ComplexType : StructuralType
	{
		// Token: 0x06002D8D RID: 11661 RVA: 0x000DC456 File Offset: 0x000DA656
		internal ComplexType(string name, string namespaceName, DataSpace dataSpace) : base(name, namespaceName, dataSpace)
		{
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x000DC461 File Offset: 0x000DA661
		internal ComplexType()
		{
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x000DC469 File Offset: 0x000DA669
		internal ComplexType(string name) : this(name, "Transient", DataSpace.CSpace)
		{
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06002D90 RID: 11664 RVA: 0x000DC478 File Offset: 0x000DA678
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.ComplexType;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06002D91 RID: 11665 RVA: 0x000DC47B File Offset: 0x000DA67B
		public virtual ReadOnlyMetadataCollection<EdmProperty> Properties
		{
			get
			{
				return new FilteredReadOnlyMetadataCollection<EdmProperty, EdmMember>(base.Members, new Predicate<EdmMember>(Helper.IsEdmProperty));
			}
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x000DC494 File Offset: 0x000DA694
		internal override void ValidateMemberForAdd(EdmMember member)
		{
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x000DC498 File Offset: 0x000DA698
		public static ComplexType Create(string name, string namespaceName, DataSpace dataSpace, IEnumerable<EdmMember> members, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(namespaceName, "namespaceName");
			Check.NotNull<IEnumerable<EdmMember>>(members, "members");
			ComplexType complexType = new ComplexType(name, namespaceName, dataSpace);
			foreach (EdmMember member in members)
			{
				complexType.AddMember(member);
			}
			if (metadataProperties != null)
			{
				complexType.AddMetadataProperties(metadataProperties.ToList<MetadataProperty>());
			}
			complexType.SetReadOnly();
			return complexType;
		}
	}
}
