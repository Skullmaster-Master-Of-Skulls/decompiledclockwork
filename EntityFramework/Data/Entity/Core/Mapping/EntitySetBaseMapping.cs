using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003D1 RID: 977
	public abstract class EntitySetBaseMapping : MappingItem
	{
		// Token: 0x06002385 RID: 9093 RVA: 0x000A5409 File Offset: 0x000A3609
		internal EntitySetBaseMapping(EntityContainerMapping containerMapping)
		{
			this._containerMapping = containerMapping;
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06002386 RID: 9094 RVA: 0x000A5428 File Offset: 0x000A3628
		public EntityContainerMapping ContainerMapping
		{
			get
			{
				return this._containerMapping;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x000A5430 File Offset: 0x000A3630
		internal EntityContainerMapping EntityContainerMapping
		{
			get
			{
				return this.ContainerMapping;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06002388 RID: 9096 RVA: 0x000A5438 File Offset: 0x000A3638
		// (set) Token: 0x06002389 RID: 9097 RVA: 0x000A5440 File Offset: 0x000A3640
		public string QueryView
		{
			get
			{
				return this._queryView;
			}
			set
			{
				base.ThrowIfReadOnly();
				this._queryView = value;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x0600238A RID: 9098
		internal abstract EntitySetBase Set { get; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x0600238B RID: 9099
		internal abstract IEnumerable<TypeMapping> TypeMappings { get; }

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600238C RID: 9100 RVA: 0x000A5450 File Offset: 0x000A3650
		internal virtual bool HasNoContent
		{
			get
			{
				if (this.QueryView != null)
				{
					return false;
				}
				foreach (TypeMapping typeMapping in this.TypeMappings)
				{
					foreach (MappingFragment mappingFragment in typeMapping.MappingFragments)
					{
						using (IEnumerator<PropertyMapping> enumerator3 = mappingFragment.AllProperties.GetEnumerator())
						{
							if (enumerator3.MoveNext())
							{
								PropertyMapping propertyMapping = enumerator3.Current;
								return false;
							}
						}
					}
				}
				return true;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600238D RID: 9101 RVA: 0x000A551C File Offset: 0x000A371C
		// (set) Token: 0x0600238E RID: 9102 RVA: 0x000A5524 File Offset: 0x000A3724
		internal int StartLineNumber { get; set; }

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600238F RID: 9103 RVA: 0x000A552D File Offset: 0x000A372D
		// (set) Token: 0x06002390 RID: 9104 RVA: 0x000A5535 File Offset: 0x000A3735
		internal int StartLinePosition { get; set; }

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06002391 RID: 9105 RVA: 0x000A553E File Offset: 0x000A373E
		// (set) Token: 0x06002392 RID: 9106 RVA: 0x000A5546 File Offset: 0x000A3746
		internal bool HasModificationFunctionMapping { get; set; }

		// Token: 0x06002393 RID: 9107 RVA: 0x000A554F File Offset: 0x000A374F
		internal bool ContainsTypeSpecificQueryView(Pair<EntitySetBase, Pair<EntityTypeBase, bool>> key)
		{
			return this._typeSpecificQueryViews.ContainsKey(key);
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x000A555D File Offset: 0x000A375D
		internal void AddTypeSpecificQueryView(Pair<EntitySetBase, Pair<EntityTypeBase, bool>> key, string viewString)
		{
			this._typeSpecificQueryViews.Add(key, viewString);
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x000A556C File Offset: 0x000A376C
		internal ReadOnlyCollection<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>> GetTypeSpecificQVKeys()
		{
			return new ReadOnlyCollection<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>>(this._typeSpecificQueryViews.Keys.ToList<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>>());
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x000A5583 File Offset: 0x000A3783
		internal string GetTypeSpecificQueryView(Pair<EntitySetBase, Pair<EntityTypeBase, bool>> key)
		{
			return this._typeSpecificQueryViews[key];
		}

		// Token: 0x04000C81 RID: 3201
		private readonly EntityContainerMapping _containerMapping;

		// Token: 0x04000C82 RID: 3202
		private string _queryView;

		// Token: 0x04000C83 RID: 3203
		private readonly Dictionary<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, string> _typeSpecificQueryViews = new Dictionary<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, string>(Pair<EntitySetBase, Pair<EntityTypeBase, bool>>.PairComparer.Instance);
	}
}
