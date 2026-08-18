using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003D4 RID: 980
	public class AssociationTypeMapping : TypeMapping
	{
		// Token: 0x060023B2 RID: 9138 RVA: 0x000A589C File Offset: 0x000A3A9C
		public AssociationTypeMapping(AssociationSetMapping associationSetMapping)
		{
			Check.NotNull<AssociationSetMapping>(associationSetMapping, "associationSetMapping");
			this._associationSetMapping = associationSetMapping;
			this.m_relation = associationSetMapping.AssociationSet.ElementType;
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x000A58C8 File Offset: 0x000A3AC8
		internal AssociationTypeMapping(AssociationType relation, AssociationSetMapping associationSetMapping)
		{
			this._associationSetMapping = associationSetMapping;
			this.m_relation = relation;
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060023B4 RID: 9140 RVA: 0x000A58DE File Offset: 0x000A3ADE
		public AssociationSetMapping AssociationSetMapping
		{
			get
			{
				return this._associationSetMapping;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060023B5 RID: 9141 RVA: 0x000A58E6 File Offset: 0x000A3AE6
		internal override EntitySetBaseMapping SetMapping
		{
			get
			{
				return this.AssociationSetMapping;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060023B6 RID: 9142 RVA: 0x000A58EE File Offset: 0x000A3AEE
		public AssociationType AssociationType
		{
			get
			{
				return this.m_relation;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060023B7 RID: 9143 RVA: 0x000A58F6 File Offset: 0x000A3AF6
		// (set) Token: 0x060023B8 RID: 9144 RVA: 0x000A58FE File Offset: 0x000A3AFE
		public MappingFragment MappingFragment
		{
			get
			{
				return this._mappingFragment;
			}
			internal set
			{
				this._mappingFragment = value;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060023B9 RID: 9145 RVA: 0x000A5908 File Offset: 0x000A3B08
		internal override ReadOnlyCollection<MappingFragment> MappingFragments
		{
			get
			{
				if (this._mappingFragment != null)
				{
					return new ReadOnlyCollection<MappingFragment>(new MappingFragment[]
					{
						this._mappingFragment
					});
				}
				return new ReadOnlyCollection<MappingFragment>(new MappingFragment[0]);
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x000A5940 File Offset: 0x000A3B40
		internal override ReadOnlyCollection<EntityTypeBase> Types
		{
			get
			{
				return new ReadOnlyCollection<EntityTypeBase>(new AssociationType[]
				{
					this.m_relation
				});
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060023BB RID: 9147 RVA: 0x000A5963 File Offset: 0x000A3B63
		internal override ReadOnlyCollection<EntityTypeBase> IsOfTypes
		{
			get
			{
				return new ReadOnlyCollection<EntityTypeBase>(new List<EntityTypeBase>());
			}
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x000A596F File Offset: 0x000A3B6F
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this._mappingFragment);
			base.SetReadOnly();
		}

		// Token: 0x04000C8A RID: 3210
		private readonly AssociationSetMapping _associationSetMapping;

		// Token: 0x04000C8B RID: 3211
		private MappingFragment _mappingFragment;

		// Token: 0x04000C8C RID: 3212
		private readonly AssociationType m_relation;
	}
}
