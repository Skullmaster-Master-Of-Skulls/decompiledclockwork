using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Diagnostics;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x020007BE RID: 1982
	[DebuggerDisplay("{Table.Name}")]
	internal class TableMapping
	{
		// Token: 0x060059AF RID: 22959 RVA: 0x00182FB5 File Offset: 0x001811B5
		public TableMapping(EntityType table)
		{
			this._table = table;
			this._entityTypes = new SortedEntityTypeIndex();
			this._columns = new List<ColumnMapping>();
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x060059B0 RID: 22960 RVA: 0x00182FDA File Offset: 0x001811DA
		public EntityType Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x060059B1 RID: 22961 RVA: 0x00182FE2 File Offset: 0x001811E2
		public SortedEntityTypeIndex EntityTypes
		{
			get
			{
				return this._entityTypes;
			}
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x060059B2 RID: 22962 RVA: 0x00182FEA File Offset: 0x001811EA
		public IEnumerable<ColumnMapping> ColumnMappings
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x060059B3 RID: 22963 RVA: 0x00183078 File Offset: 0x00181278
		public void AddEntityTypeMappingFragment(EntitySet entitySet, EntityType entityType, MappingFragment fragment)
		{
			this._entityTypes.Add(entitySet, entityType);
			EdmProperty defaultDiscriminator = fragment.GetDefaultDiscriminator();
			using (IEnumerator<ColumnMappingBuilder> enumerator = fragment.ColumnMappings.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ColumnMappingBuilder cm = enumerator.Current;
					ColumnMapping columnMapping = this.FindOrCreateColumnMapping(cm.ColumnProperty);
					columnMapping.AddMapping(entityType, cm.PropertyPath, from cc in fragment.ColumnConditions
					where cc.Column == cm.ColumnProperty
					select cc, defaultDiscriminator == cm.ColumnProperty);
				}
			}
			foreach (ConditionPropertyMapping conditionPropertyMapping in from cc in fragment.ColumnConditions
			where fragment.ColumnMappings.All((ColumnMappingBuilder pm) => pm.ColumnProperty != cc.Column)
			select cc)
			{
				ColumnMapping columnMapping2 = this.FindOrCreateColumnMapping(conditionPropertyMapping.Column);
				columnMapping2.AddMapping(entityType, null, new ConditionPropertyMapping[]
				{
					conditionPropertyMapping
				}, defaultDiscriminator == conditionPropertyMapping.Column);
			}
		}

		// Token: 0x060059B4 RID: 22964 RVA: 0x001831F8 File Offset: 0x001813F8
		private ColumnMapping FindOrCreateColumnMapping(EdmProperty column)
		{
			ColumnMapping columnMapping = this._columns.SingleOrDefault((ColumnMapping c) => c.Column == column);
			if (columnMapping == null)
			{
				columnMapping = new ColumnMapping(column);
				this._columns.Add(columnMapping);
			}
			return columnMapping;
		}

		// Token: 0x040023D9 RID: 9177
		private readonly EntityType _table;

		// Token: 0x040023DA RID: 9178
		private readonly SortedEntityTypeIndex _entityTypes;

		// Token: 0x040023DB RID: 9179
		private readonly List<ColumnMapping> _columns;
	}
}
