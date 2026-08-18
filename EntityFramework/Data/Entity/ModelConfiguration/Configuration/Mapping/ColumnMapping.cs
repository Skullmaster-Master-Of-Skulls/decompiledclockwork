using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x020007BA RID: 1978
	[DebuggerDisplay("{Column.Name}")]
	internal class ColumnMapping
	{
		// Token: 0x0600597A RID: 22906 RVA: 0x00181257 File Offset: 0x0017F457
		public ColumnMapping(EdmProperty column)
		{
			this._column = column;
			this._propertyMappings = new List<PropertyMappingSpecification>();
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x0600597B RID: 22907 RVA: 0x00181271 File Offset: 0x0017F471
		public EdmProperty Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x0600597C RID: 22908 RVA: 0x00181279 File Offset: 0x0017F479
		public IList<PropertyMappingSpecification> PropertyMappings
		{
			get
			{
				return this._propertyMappings;
			}
		}

		// Token: 0x0600597D RID: 22909 RVA: 0x00181281 File Offset: 0x0017F481
		public void AddMapping(EntityType entityType, IList<EdmProperty> propertyPath, IEnumerable<ConditionPropertyMapping> conditions, bool isDefaultDiscriminatorCondition)
		{
			this._propertyMappings.Add(new PropertyMappingSpecification(entityType, propertyPath, conditions.ToList<ConditionPropertyMapping>(), isDefaultDiscriminatorCondition));
		}

		// Token: 0x040023BA RID: 9146
		private readonly EdmProperty _column;

		// Token: 0x040023BB RID: 9147
		private readonly List<PropertyMappingSpecification> _propertyMappings;
	}
}
