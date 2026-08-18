using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003D7 RID: 983
	public class EndPropertyMapping : PropertyMapping
	{
		// Token: 0x060023D4 RID: 9172 RVA: 0x000A5DC0 File Offset: 0x000A3FC0
		public EndPropertyMapping(AssociationEndMember associationEnd)
		{
			Check.NotNull<AssociationEndMember>(associationEnd, "associationEnd");
			this._associationEnd = associationEnd;
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000A5DE6 File Offset: 0x000A3FE6
		internal EndPropertyMapping()
		{
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060023D6 RID: 9174 RVA: 0x000A5DF9 File Offset: 0x000A3FF9
		// (set) Token: 0x060023D7 RID: 9175 RVA: 0x000A5E01 File Offset: 0x000A4001
		public AssociationEndMember AssociationEnd
		{
			get
			{
				return this._associationEnd;
			}
			internal set
			{
				this._associationEnd = value;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060023D8 RID: 9176 RVA: 0x000A5E0A File Offset: 0x000A400A
		public ReadOnlyCollection<ScalarPropertyMapping> PropertyMappings
		{
			get
			{
				return new ReadOnlyCollection<ScalarPropertyMapping>(this._properties);
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060023D9 RID: 9177 RVA: 0x000A5E1F File Offset: 0x000A401F
		internal IEnumerable<EdmMember> StoreProperties
		{
			get
			{
				return from propertyMap in this.PropertyMappings
				select propertyMap.Column;
			}
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000A5E49 File Offset: 0x000A4049
		public void AddPropertyMapping(ScalarPropertyMapping propertyMapping)
		{
			Check.NotNull<ScalarPropertyMapping>(propertyMapping, "propertyMapping");
			base.ThrowIfReadOnly();
			this._properties.Add(propertyMapping);
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000A5E69 File Offset: 0x000A4069
		public void RemovePropertyMapping(ScalarPropertyMapping propertyMapping)
		{
			Check.NotNull<ScalarPropertyMapping>(propertyMapping, "propertyMapping");
			base.ThrowIfReadOnly();
			this._properties.Remove(propertyMapping);
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x000A5E8A File Offset: 0x000A408A
		internal override void SetReadOnly()
		{
			this._properties.TrimExcess();
			MappingItem.SetReadOnly(this._properties);
			base.SetReadOnly();
		}

		// Token: 0x04000C93 RID: 3219
		private AssociationEndMember _associationEnd;

		// Token: 0x04000C94 RID: 3220
		private readonly List<ScalarPropertyMapping> _properties = new List<ScalarPropertyMapping>();
	}
}
