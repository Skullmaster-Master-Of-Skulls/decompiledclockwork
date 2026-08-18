using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004C6 RID: 1222
	public sealed class AssociationSetEnd : MetadataItem
	{
		// Token: 0x06002D09 RID: 11529 RVA: 0x000DAC94 File Offset: 0x000D8E94
		internal AssociationSetEnd(EntitySet entitySet, AssociationSet parentSet, AssociationEndMember endMember)
		{
			this._entitySet = Check.NotNull<EntitySet>(entitySet, "entitySet");
			this._parentSet = Check.NotNull<AssociationSet>(parentSet, "parentSet");
			this._endMember = Check.NotNull<AssociationEndMember>(endMember, "endMember");
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06002D0A RID: 11530 RVA: 0x000DACCF File Offset: 0x000D8ECF
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.AssociationSetEnd;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06002D0B RID: 11531 RVA: 0x000DACD2 File Offset: 0x000D8ED2
		[MetadataProperty(BuiltInTypeKind.AssociationSet, false)]
		public AssociationSet ParentAssociationSet
		{
			get
			{
				return this._parentSet;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06002D0C RID: 11532 RVA: 0x000DACDA File Offset: 0x000D8EDA
		[MetadataProperty(BuiltInTypeKind.AssociationEndMember, false)]
		public AssociationEndMember CorrespondingAssociationEndMember
		{
			get
			{
				return this._endMember;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06002D0D RID: 11533 RVA: 0x000DACE2 File Offset: 0x000D8EE2
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this.CorrespondingAssociationEndMember.Name;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06002D0E RID: 11534 RVA: 0x000DACEF File Offset: 0x000D8EEF
		[Obsolete("This property is going away, please use the Name property instead")]
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Role
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06002D0F RID: 11535 RVA: 0x000DACF7 File Offset: 0x000D8EF7
		[MetadataProperty(BuiltInTypeKind.EntitySet, false)]
		public EntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06002D10 RID: 11536 RVA: 0x000DACFF File Offset: 0x000D8EFF
		internal override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000DAD07 File Offset: 0x000D8F07
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x000DAD10 File Offset: 0x000D8F10
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				AssociationSet parentAssociationSet = this.ParentAssociationSet;
				if (parentAssociationSet != null)
				{
					parentAssociationSet.SetReadOnly();
				}
				AssociationEndMember correspondingAssociationEndMember = this.CorrespondingAssociationEndMember;
				if (correspondingAssociationEndMember != null)
				{
					correspondingAssociationEndMember.SetReadOnly();
				}
				EntitySet entitySet = this.EntitySet;
				if (entitySet != null)
				{
					entitySet.SetReadOnly();
				}
			}
		}

		// Token: 0x0400108D RID: 4237
		private readonly EntitySet _entitySet;

		// Token: 0x0400108E RID: 4238
		private readonly AssociationSet _parentSet;

		// Token: 0x0400108F RID: 4239
		private readonly AssociationEndMember _endMember;
	}
}
