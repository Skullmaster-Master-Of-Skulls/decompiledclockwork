using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004C5 RID: 1221
	public sealed class AssociationSet : RelationshipSet
	{
		// Token: 0x06002CFB RID: 11515 RVA: 0x000DA9BC File Offset: 0x000D8BBC
		internal AssociationSet(string name, AssociationType associationType) : base(name, null, null, null, associationType)
		{
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06002CFC RID: 11516 RVA: 0x000DA9D9 File Offset: 0x000D8BD9
		public new AssociationType ElementType
		{
			get
			{
				return (AssociationType)base.ElementType;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06002CFD RID: 11517 RVA: 0x000DA9E6 File Offset: 0x000D8BE6
		[MetadataProperty(BuiltInTypeKind.AssociationSetEnd, true)]
		public ReadOnlyMetadataCollection<AssociationSetEnd> AssociationSetEnds
		{
			get
			{
				return this._associationSetEnds;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06002CFE RID: 11518 RVA: 0x000DA9F0 File Offset: 0x000D8BF0
		// (set) Token: 0x06002CFF RID: 11519 RVA: 0x000DAA14 File Offset: 0x000D8C14
		internal EntitySet SourceSet
		{
			get
			{
				AssociationSetEnd associationSetEnd = this.AssociationSetEnds.FirstOrDefault<AssociationSetEnd>();
				if (associationSetEnd == null)
				{
					return null;
				}
				return associationSetEnd.EntitySet;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				AssociationSetEnd associationSetEnd = new AssociationSetEnd(value, this, this.ElementType.SourceEnd);
				if (this.AssociationSetEnds.Count == 0)
				{
					this.AddAssociationSetEnd(associationSetEnd);
					return;
				}
				this.AssociationSetEnds.Source[0] = associationSetEnd;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06002D00 RID: 11520 RVA: 0x000DAA64 File Offset: 0x000D8C64
		// (set) Token: 0x06002D01 RID: 11521 RVA: 0x000DAA8C File Offset: 0x000D8C8C
		internal EntitySet TargetSet
		{
			get
			{
				AssociationSetEnd associationSetEnd = this.AssociationSetEnds.ElementAtOrDefault(1);
				if (associationSetEnd == null)
				{
					return null;
				}
				return associationSetEnd.EntitySet;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				AssociationSetEnd associationSetEnd = new AssociationSetEnd(value, this, this.ElementType.TargetEnd);
				if (this.AssociationSetEnds.Count == 1)
				{
					this.AddAssociationSetEnd(associationSetEnd);
					return;
				}
				this.AssociationSetEnds.Source[1] = associationSetEnd;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06002D02 RID: 11522 RVA: 0x000DAAFC File Offset: 0x000D8CFC
		internal AssociationEndMember SourceEnd
		{
			get
			{
				AssociationSetEnd associationSetEnd = this.AssociationSetEnds.FirstOrDefault<AssociationSetEnd>();
				if (associationSetEnd == null)
				{
					return null;
				}
				return this.ElementType.KeyMembers.OfType<AssociationEndMember>().SingleOrDefault((AssociationEndMember e) => e.Name == associationSetEnd.Name);
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06002D03 RID: 11523 RVA: 0x000DAB6C File Offset: 0x000D8D6C
		internal AssociationEndMember TargetEnd
		{
			get
			{
				AssociationSetEnd associationSetEnd = this.AssociationSetEnds.ElementAtOrDefault(1);
				if (associationSetEnd == null)
				{
					return null;
				}
				return this.ElementType.KeyMembers.OfType<AssociationEndMember>().SingleOrDefault((AssociationEndMember e) => e.Name == associationSetEnd.Name);
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06002D04 RID: 11524 RVA: 0x000DABBC File Offset: 0x000D8DBC
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.AssociationSet;
			}
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x000DABBF File Offset: 0x000D8DBF
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.AssociationSetEnds.Source.SetReadOnly();
			}
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x000DABE0 File Offset: 0x000D8DE0
		internal void AddAssociationSetEnd(AssociationSetEnd associationSetEnd)
		{
			this.AssociationSetEnds.Source.Add(associationSetEnd);
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x000DABF4 File Offset: 0x000D8DF4
		public static AssociationSet Create(string name, AssociationType type, EntitySet sourceSet, EntitySet targetSet, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<AssociationType>(type, "type");
			if (!AssociationSet.CheckEntitySetAgainstEndMember(sourceSet, type.SourceEnd) || !AssociationSet.CheckEntitySetAgainstEndMember(targetSet, type.TargetEnd))
			{
				throw new ArgumentException(Strings.AssociationSet_EndEntityTypeMismatch);
			}
			AssociationSet associationSet = new AssociationSet(name, type);
			if (sourceSet != null)
			{
				associationSet.SourceSet = sourceSet;
			}
			if (targetSet != null)
			{
				associationSet.TargetSet = targetSet;
			}
			if (metadataProperties != null)
			{
				associationSet.AddMetadataProperties(metadataProperties.ToList<MetadataProperty>());
			}
			associationSet.SetReadOnly();
			return associationSet;
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x000DAC74 File Offset: 0x000D8E74
		private static bool CheckEntitySetAgainstEndMember(EntitySet entitySet, AssociationEndMember endMember)
		{
			return (entitySet == null && endMember == null) || (entitySet != null && endMember != null && entitySet.ElementType == endMember.GetEntityType());
		}

		// Token: 0x0400108C RID: 4236
		private readonly ReadOnlyMetadataCollection<AssociationSetEnd> _associationSetEnds = new ReadOnlyMetadataCollection<AssociationSetEnd>(new MetadataCollection<AssociationSetEnd>());
	}
}
