using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004CB RID: 1227
	[SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance")]
	public class AssociationType : RelationshipType
	{
		// Token: 0x06002D48 RID: 11592 RVA: 0x000DB68D File Offset: 0x000D988D
		internal AssociationType(string name, string namespaceName, bool foreignKey, DataSpace dataSpace) : base(name, namespaceName, dataSpace)
		{
			this._referentialConstraints = new ReadOnlyMetadataCollection<ReferentialConstraint>(new MetadataCollection<ReferentialConstraint>());
			this._isForeignKey = foreignKey;
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06002D49 RID: 11593 RVA: 0x000DB6B9 File Offset: 0x000D98B9
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.AssociationType;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06002D4A RID: 11594 RVA: 0x000DB6BC File Offset: 0x000D98BC
		public ReadOnlyMetadataCollection<AssociationEndMember> AssociationEndMembers
		{
			get
			{
				if (this._associationEndMembers == null)
				{
					Interlocked.CompareExchange<FilteredReadOnlyMetadataCollection<AssociationEndMember, EdmMember>>(ref this._associationEndMembers, new FilteredReadOnlyMetadataCollection<AssociationEndMember, EdmMember>(this.KeyMembers, new Predicate<EdmMember>(Helper.IsAssociationEndMember)), null);
				}
				return this._associationEndMembers;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06002D4B RID: 11595 RVA: 0x000DB6F0 File Offset: 0x000D98F0
		// (set) Token: 0x06002D4C RID: 11596 RVA: 0x000DB700 File Offset: 0x000D9900
		public ReferentialConstraint Constraint
		{
			get
			{
				return this.ReferentialConstraints.SingleOrDefault<ReferentialConstraint>();
			}
			set
			{
				Check.NotNull<ReferentialConstraint>(value, "value");
				Util.ThrowIfReadOnly(this);
				ReferentialConstraint constraint = this.Constraint;
				if (constraint != null)
				{
					this.ReferentialConstraints.Source.Remove(constraint);
				}
				this.AddReferentialConstraint(value);
				this._isForeignKey = true;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06002D4D RID: 11597 RVA: 0x000DB749 File Offset: 0x000D9949
		// (set) Token: 0x06002D4E RID: 11598 RVA: 0x000DB75B File Offset: 0x000D995B
		internal AssociationEndMember SourceEnd
		{
			get
			{
				return this.KeyMembers.FirstOrDefault<EdmMember>() as AssociationEndMember;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				if (this.KeyMembers.Count == 0)
				{
					base.AddKeyMember(value);
					return;
				}
				this.SetKeyMember(0, value);
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06002D4F RID: 11599 RVA: 0x000DB780 File Offset: 0x000D9980
		// (set) Token: 0x06002D50 RID: 11600 RVA: 0x000DB793 File Offset: 0x000D9993
		internal AssociationEndMember TargetEnd
		{
			get
			{
				return this.KeyMembers.ElementAtOrDefault(1) as AssociationEndMember;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				if (this.KeyMembers.Count == 1)
				{
					base.AddKeyMember(value);
					return;
				}
				this.SetKeyMember(1, value);
			}
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x000DB7BC File Offset: 0x000D99BC
		private void SetKeyMember(int index, AssociationEndMember member)
		{
			EdmMember value = this.KeyMembers.Source[index];
			int num = base.Members.IndexOf(value);
			if (num >= 0)
			{
				base.Members.Source[num] = member;
			}
			this.KeyMembers.Source[index] = member;
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06002D52 RID: 11602 RVA: 0x000DB810 File Offset: 0x000D9A10
		[MetadataProperty(BuiltInTypeKind.ReferentialConstraint, true)]
		public ReadOnlyMetadataCollection<ReferentialConstraint> ReferentialConstraints
		{
			get
			{
				return this._referentialConstraints;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06002D53 RID: 11603 RVA: 0x000DB818 File Offset: 0x000D9A18
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		public bool IsForeignKey
		{
			get
			{
				return this._isForeignKey;
			}
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x000DB820 File Offset: 0x000D9A20
		internal override void ValidateMemberForAdd(EdmMember member)
		{
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x000DB822 File Offset: 0x000D9A22
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.ReferentialConstraints.Source.SetReadOnly();
			}
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x000DB843 File Offset: 0x000D9A43
		internal void AddReferentialConstraint(ReferentialConstraint referentialConstraint)
		{
			this.ReferentialConstraints.Source.Add(referentialConstraint);
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x000DB858 File Offset: 0x000D9A58
		public static AssociationType Create(string name, string namespaceName, bool foreignKey, DataSpace dataSpace, AssociationEndMember sourceEnd, AssociationEndMember targetEnd, ReferentialConstraint constraint, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(namespaceName, "namespaceName");
			AssociationType associationType = new AssociationType(name, namespaceName, foreignKey, dataSpace);
			if (sourceEnd != null)
			{
				associationType.SourceEnd = sourceEnd;
			}
			if (targetEnd != null)
			{
				associationType.TargetEnd = targetEnd;
			}
			if (constraint != null)
			{
				associationType.AddReferentialConstraint(constraint);
			}
			if (metadataProperties != null)
			{
				associationType.AddMetadataProperties(metadataProperties.ToList<MetadataProperty>());
			}
			associationType.SetReadOnly();
			return associationType;
		}

		// Token: 0x0400109C RID: 4252
		internal volatile int Index = -1;

		// Token: 0x0400109D RID: 4253
		private readonly ReadOnlyMetadataCollection<ReferentialConstraint> _referentialConstraints;

		// Token: 0x0400109E RID: 4254
		private FilteredReadOnlyMetadataCollection<AssociationEndMember, EdmMember> _associationEndMembers;

		// Token: 0x0400109F RID: 4255
		private bool _isForeignKey;
	}
}
