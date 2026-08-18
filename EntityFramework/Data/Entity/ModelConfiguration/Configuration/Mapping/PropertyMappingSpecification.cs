using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x020007BC RID: 1980
	internal class PropertyMappingSpecification
	{
		// Token: 0x060059A3 RID: 22947 RVA: 0x00182E26 File Offset: 0x00181026
		public PropertyMappingSpecification(EntityType entityType, IList<EdmProperty> propertyPath, IList<ConditionPropertyMapping> conditions, bool isDefaultDiscriminatorCondition)
		{
			this._entityType = entityType;
			this._propertyPath = propertyPath;
			this._conditions = conditions;
			this._isDefaultDiscriminatorCondition = isDefaultDiscriminatorCondition;
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x060059A4 RID: 22948 RVA: 0x00182E4B File Offset: 0x0018104B
		public EntityType EntityType
		{
			get
			{
				return this._entityType;
			}
		}

		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x060059A5 RID: 22949 RVA: 0x00182E53 File Offset: 0x00181053
		public IList<EdmProperty> PropertyPath
		{
			get
			{
				return this._propertyPath;
			}
		}

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x060059A6 RID: 22950 RVA: 0x00182E5B File Offset: 0x0018105B
		public IList<ConditionPropertyMapping> Conditions
		{
			get
			{
				return this._conditions;
			}
		}

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x060059A7 RID: 22951 RVA: 0x00182E63 File Offset: 0x00181063
		public bool IsDefaultDiscriminatorCondition
		{
			get
			{
				return this._isDefaultDiscriminatorCondition;
			}
		}

		// Token: 0x040023D3 RID: 9171
		private readonly EntityType _entityType;

		// Token: 0x040023D4 RID: 9172
		private readonly IList<EdmProperty> _propertyPath;

		// Token: 0x040023D5 RID: 9173
		private readonly IList<ConditionPropertyMapping> _conditions;

		// Token: 0x040023D6 RID: 9174
		private readonly bool _isDefaultDiscriminatorCondition;
	}
}
