using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003D2 RID: 978
	public class AssociationSetMapping : EntitySetBaseMapping
	{
		// Token: 0x06002397 RID: 9111 RVA: 0x000A5594 File Offset: 0x000A3794
		public AssociationSetMapping(AssociationSet associationSet, EntitySet storeEntitySet, EntityContainerMapping containerMapping) : base(containerMapping)
		{
			Check.NotNull<AssociationSet>(associationSet, "associationSet");
			Check.NotNull<EntitySet>(storeEntitySet, "storeEntitySet");
			this._associationSet = associationSet;
			this._associationTypeMapping = new AssociationTypeMapping(associationSet.ElementType, this);
			this._associationTypeMapping.MappingFragment = new MappingFragment(storeEntitySet, this._associationTypeMapping, false);
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000A55F1 File Offset: 0x000A37F1
		internal AssociationSetMapping(AssociationSet associationSet, EntitySet storeEntitySet) : this(associationSet, storeEntitySet, null)
		{
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x000A55FC File Offset: 0x000A37FC
		internal AssociationSetMapping(AssociationSet associationSet, EntityContainerMapping containerMapping) : base(containerMapping)
		{
			this._associationSet = associationSet;
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x0600239A RID: 9114 RVA: 0x000A560C File Offset: 0x000A380C
		public AssociationSet AssociationSet
		{
			get
			{
				return this._associationSet;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x0600239B RID: 9115 RVA: 0x000A5614 File Offset: 0x000A3814
		internal override EntitySetBase Set
		{
			get
			{
				return this.AssociationSet;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x0600239C RID: 9116 RVA: 0x000A561C File Offset: 0x000A381C
		// (set) Token: 0x0600239D RID: 9117 RVA: 0x000A5624 File Offset: 0x000A3824
		public AssociationTypeMapping AssociationTypeMapping
		{
			get
			{
				return this._associationTypeMapping;
			}
			internal set
			{
				this._associationTypeMapping = value;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600239E RID: 9118 RVA: 0x000A56FC File Offset: 0x000A38FC
		internal override IEnumerable<TypeMapping> TypeMappings
		{
			get
			{
				yield return this._associationTypeMapping;
				yield break;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x0600239F RID: 9119 RVA: 0x000A5719 File Offset: 0x000A3919
		// (set) Token: 0x060023A0 RID: 9120 RVA: 0x000A5721 File Offset: 0x000A3921
		public AssociationSetModificationFunctionMapping ModificationFunctionMapping
		{
			get
			{
				return this._modificationFunctionMapping;
			}
			set
			{
				base.ThrowIfReadOnly();
				this._modificationFunctionMapping = value;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060023A1 RID: 9121 RVA: 0x000A5730 File Offset: 0x000A3930
		// (set) Token: 0x060023A2 RID: 9122 RVA: 0x000A5747 File Offset: 0x000A3947
		public EntitySet StoreEntitySet
		{
			get
			{
				if (this.SingleFragment == null)
				{
					return null;
				}
				return this.SingleFragment.StoreEntitySet;
			}
			internal set
			{
				this.SingleFragment.StoreEntitySet = value;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060023A3 RID: 9123 RVA: 0x000A5755 File Offset: 0x000A3955
		internal EntityType Table
		{
			get
			{
				if (this.StoreEntitySet == null)
				{
					return null;
				}
				return this.StoreEntitySet.ElementType;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060023A4 RID: 9124 RVA: 0x000A576C File Offset: 0x000A396C
		// (set) Token: 0x060023A5 RID: 9125 RVA: 0x000A578D File Offset: 0x000A398D
		public EndPropertyMapping SourceEndMapping
		{
			get
			{
				if (this.SingleFragment == null)
				{
					return null;
				}
				return this.SingleFragment.PropertyMappings.OfType<EndPropertyMapping>().FirstOrDefault<EndPropertyMapping>();
			}
			set
			{
				Check.NotNull<EndPropertyMapping>(value, "value");
				base.ThrowIfReadOnly();
				this.SingleFragment.AddPropertyMapping(value);
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x000A57AD File Offset: 0x000A39AD
		// (set) Token: 0x060023A7 RID: 9127 RVA: 0x000A57CF File Offset: 0x000A39CF
		public EndPropertyMapping TargetEndMapping
		{
			get
			{
				if (this.SingleFragment == null)
				{
					return null;
				}
				return this.SingleFragment.PropertyMappings.OfType<EndPropertyMapping>().ElementAtOrDefault(1);
			}
			set
			{
				Check.NotNull<EndPropertyMapping>(value, "value");
				base.ThrowIfReadOnly();
				this.SingleFragment.AddPropertyMapping(value);
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060023A8 RID: 9128 RVA: 0x000A57EF File Offset: 0x000A39EF
		public ReadOnlyCollection<ConditionPropertyMapping> Conditions
		{
			get
			{
				if (this.SingleFragment == null)
				{
					return new ReadOnlyCollection<ConditionPropertyMapping>(new List<ConditionPropertyMapping>());
				}
				return this.SingleFragment.Conditions;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060023A9 RID: 9129 RVA: 0x000A580F File Offset: 0x000A3A0F
		private MappingFragment SingleFragment
		{
			get
			{
				if (this._associationTypeMapping == null)
				{
					return null;
				}
				return this._associationTypeMapping.MappingFragment;
			}
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x000A5826 File Offset: 0x000A3A26
		public void AddCondition(ConditionPropertyMapping condition)
		{
			Check.NotNull<ConditionPropertyMapping>(condition, "condition");
			base.ThrowIfReadOnly();
			if (this.SingleFragment != null)
			{
				this.SingleFragment.AddCondition(condition);
			}
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x000A584E File Offset: 0x000A3A4E
		public void RemoveCondition(ConditionPropertyMapping condition)
		{
			Check.NotNull<ConditionPropertyMapping>(condition, "condition");
			base.ThrowIfReadOnly();
			if (this.SingleFragment != null)
			{
				this.SingleFragment.RemoveCondition(condition);
			}
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x000A5876 File Offset: 0x000A3A76
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this._associationTypeMapping);
			MappingItem.SetReadOnly(this._modificationFunctionMapping);
			base.SetReadOnly();
		}

		// Token: 0x04000C87 RID: 3207
		private readonly AssociationSet _associationSet;

		// Token: 0x04000C88 RID: 3208
		private AssociationTypeMapping _associationTypeMapping;

		// Token: 0x04000C89 RID: 3209
		private AssociationSetModificationFunctionMapping _modificationFunctionMapping;
	}
}
