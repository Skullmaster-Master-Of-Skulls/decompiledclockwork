using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000730 RID: 1840
	internal class ColumnMappingBuilder
	{
		// Token: 0x06005315 RID: 21269 RVA: 0x0016E9B9 File Offset: 0x0016CBB9
		public ColumnMappingBuilder(EdmProperty columnProperty, IList<EdmProperty> propertyPath)
		{
			Check.NotNull<EdmProperty>(columnProperty, "columnProperty");
			Check.NotNull<IList<EdmProperty>>(propertyPath, "propertyPath");
			this._columnProperty = columnProperty;
			this._propertyPath = propertyPath;
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06005316 RID: 21270 RVA: 0x0016E9E7 File Offset: 0x0016CBE7
		public IList<EdmProperty> PropertyPath
		{
			get
			{
				return this._propertyPath;
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06005317 RID: 21271 RVA: 0x0016E9EF File Offset: 0x0016CBEF
		// (set) Token: 0x06005318 RID: 21272 RVA: 0x0016E9F7 File Offset: 0x0016CBF7
		public EdmProperty ColumnProperty
		{
			get
			{
				return this._columnProperty;
			}
			internal set
			{
				this._columnProperty = value;
				if (this._scalarPropertyMapping != null)
				{
					this._scalarPropertyMapping.Column = this._columnProperty;
				}
			}
		}

		// Token: 0x06005319 RID: 21273 RVA: 0x0016EA19 File Offset: 0x0016CC19
		internal void SetTarget(ScalarPropertyMapping scalarPropertyMapping)
		{
			this._scalarPropertyMapping = scalarPropertyMapping;
		}

		// Token: 0x0400224D RID: 8781
		private EdmProperty _columnProperty;

		// Token: 0x0400224E RID: 8782
		private readonly IList<EdmProperty> _propertyPath;

		// Token: 0x0400224F RID: 8783
		private ScalarPropertyMapping _scalarPropertyMapping;
	}
}
