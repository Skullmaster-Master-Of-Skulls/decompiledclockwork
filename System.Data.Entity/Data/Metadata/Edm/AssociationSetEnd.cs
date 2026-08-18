using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001C2 RID: 450
	public sealed class AssociationSetEnd : MetadataItem
	{
		// Token: 0x06001F32 RID: 7986 RVA: 0x0006E099 File Offset: 0x0006C299
		internal AssociationSetEnd(EntitySet entitySet, AssociationSet parentSet, AssociationEndMember endMember)
		{
			this._entitySet = EntityUtil.GenericCheckArgumentNull<EntitySet>(entitySet, "entitySet");
			this._parentSet = EntityUtil.GenericCheckArgumentNull<AssociationSet>(parentSet, "parentSet");
			this._endMember = EntityUtil.GenericCheckArgumentNull<AssociationEndMember>(endMember, "endMember");
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001F33 RID: 7987 RVA: 0x00017938 File Offset: 0x00015B38
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.AssociationSetEnd;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001F34 RID: 7988 RVA: 0x0006E0D4 File Offset: 0x0006C2D4
		[MetadataProperty(BuiltInTypeKind.AssociationSet, false)]
		public AssociationSet ParentAssociationSet
		{
			get
			{
				return this._parentSet;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001F35 RID: 7989 RVA: 0x0006E0DC File Offset: 0x0006C2DC
		[MetadataProperty(BuiltInTypeKind.AssociationEndMember, false)]
		public AssociationEndMember CorrespondingAssociationEndMember
		{
			get
			{
				return this._endMember;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001F36 RID: 7990 RVA: 0x0006E0E4 File Offset: 0x0006C2E4
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this.CorrespondingAssociationEndMember.Name;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001F37 RID: 7991 RVA: 0x0006E0F1 File Offset: 0x0006C2F1
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		[Obsolete("This property is going away, please use the Name property instead")]
		public string Role
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001F38 RID: 7992 RVA: 0x0006E0F9 File Offset: 0x0006C2F9
		[MetadataProperty(BuiltInTypeKind.EntitySet, false)]
		public EntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x0006E0F1 File Offset: 0x0006C2F1
		internal override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x0006E0F1 File Offset: 0x0006C2F1
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x0006E104 File Offset: 0x0006C304
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

		// Token: 0x04000D16 RID: 3350
		private readonly EntitySet _entitySet;

		// Token: 0x04000D17 RID: 3351
		private readonly AssociationSet _parentSet;

		// Token: 0x04000D18 RID: 3352
		private readonly AssociationEndMember _endMember;
	}
}
