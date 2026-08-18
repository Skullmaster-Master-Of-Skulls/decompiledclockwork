using System;
using System.Threading;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001C8 RID: 456
	public class ComplexType : StructuralType
	{
		// Token: 0x06001F52 RID: 8018 RVA: 0x0006E33D File Offset: 0x0006C53D
		internal ComplexType(string name, string namespaceName, DataSpace dataSpace) : base(name, namespaceName, dataSpace)
		{
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x0006E348 File Offset: 0x0006C548
		internal ComplexType()
		{
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001F54 RID: 8020 RVA: 0x0006E350 File Offset: 0x0006C550
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.ComplexType;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001F55 RID: 8021 RVA: 0x0006E353 File Offset: 0x0006C553
		public ReadOnlyMetadataCollection<EdmProperty> Properties
		{
			get
			{
				if (this._properties == null)
				{
					Interlocked.CompareExchange<ReadOnlyMetadataCollection<EdmProperty>>(ref this._properties, new FilteredReadOnlyMetadataCollection<EdmProperty, EdmMember>(base.Members, new Predicate<EdmMember>(Helper.IsEdmProperty)), null);
				}
				return this._properties;
			}
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal override void ValidateMemberForAdd(EdmMember member)
		{
		}

		// Token: 0x04000D4A RID: 3402
		private ReadOnlyMetadataCollection<EdmProperty> _properties;
	}
}
