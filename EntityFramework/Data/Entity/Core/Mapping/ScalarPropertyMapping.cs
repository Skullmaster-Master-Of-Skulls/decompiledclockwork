using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003E8 RID: 1000
	public class ScalarPropertyMapping : PropertyMapping
	{
		// Token: 0x060024FF RID: 9471 RVA: 0x000AEA14 File Offset: 0x000ACC14
		public ScalarPropertyMapping(EdmProperty property, EdmProperty column) : base(property)
		{
			Check.NotNull<EdmProperty>(property, "property");
			Check.NotNull<EdmProperty>(column, "column");
			if (!Helper.IsScalarType(property.TypeUsage.EdmType) || !Helper.IsPrimitiveType(column.TypeUsage.EdmType))
			{
				throw new ArgumentException(Strings.StorageScalarPropertyMapping_OnlyScalarPropertiesAllowed);
			}
			this._column = column;
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06002500 RID: 9472 RVA: 0x000AEA76 File Offset: 0x000ACC76
		// (set) Token: 0x06002501 RID: 9473 RVA: 0x000AEA7E File Offset: 0x000ACC7E
		public EdmProperty Column
		{
			get
			{
				return this._column;
			}
			internal set
			{
				this._column = value;
			}
		}

		// Token: 0x04000DB8 RID: 3512
		private EdmProperty _column;
	}
}
