using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020002C3 RID: 707
	internal class TphColumnFixer
	{
		// Token: 0x06001922 RID: 6434 RVA: 0x0007C6C0 File Offset: 0x0007A8C0
		public TphColumnFixer(IEnumerable<ColumnMappingBuilder> columnMappings, EntityType table, EdmModel storeModel)
		{
			this._columnMappings = (from m in columnMappings
			orderby m.ColumnProperty.Name
			select m).ToList<ColumnMappingBuilder>();
			this._table = table;
			this._storeModel = storeModel;
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x0007C8A8 File Offset: 0x0007AAA8
		public void RemoveDuplicateTphColumns()
		{
			int num;
			for (int i = 0; i < this._columnMappings.Count - 1; i = num)
			{
				StructuralType declaringType = this._columnMappings[i].PropertyPath[0].DeclaringType;
				EdmProperty column = this._columnMappings[i].ColumnProperty;
				num = i + 1;
				EdmType edmType;
				while (num < this._columnMappings.Count && column.Name == this._columnMappings[num].ColumnProperty.Name && declaringType != this._columnMappings[num].PropertyPath[0].DeclaringType && TypeSemantics.TryGetCommonBaseType(declaringType, this._columnMappings[num].PropertyPath[0].DeclaringType, out edmType))
				{
					num++;
				}
				PrimitivePropertyConfiguration primitivePropertyConfiguration = column.GetConfiguration() as PrimitivePropertyConfiguration;
				for (int j = i + 1; j < num; j++)
				{
					ColumnMappingBuilder toFixup = this._columnMappings[j];
					PrimitivePropertyConfiguration primitivePropertyConfiguration2 = toFixup.ColumnProperty.GetConfiguration() as PrimitivePropertyConfiguration;
					string p2;
					if (primitivePropertyConfiguration != null && !primitivePropertyConfiguration.IsCompatible(primitivePropertyConfiguration2, false, out p2))
					{
						throw new MappingException(Strings.BadTphMappingToSharedColumn(string.Join(".", from p in this._columnMappings[i].PropertyPath
						select p.Name), declaringType.Name, string.Join(".", from p in toFixup.PropertyPath
						select p.Name), toFixup.PropertyPath[0].DeclaringType.Name, column.Name, column.DeclaringType.Name, p2));
					}
					if (primitivePropertyConfiguration2 != null)
					{
						primitivePropertyConfiguration2.Configure(column, this._table, this._storeModel.ProviderManifest, false, false);
					}
					column.Nullable = true;
					IEnumerable<AssociationType> source = from a in this._storeModel.AssociationTypes
					where a.Constraint != null
					let p = a.Constraint.ToProperties
					where p.Contains(column) || p.Contains(toFixup.ColumnProperty)
					select a;
					foreach (AssociationType associationType in source.ToArray<AssociationType>())
					{
						this._storeModel.RemoveAssociationType(associationType);
					}
					if (toFixup.ColumnProperty.DeclaringType.HasMember(toFixup.ColumnProperty))
					{
						toFixup.ColumnProperty.DeclaringType.RemoveMember(toFixup.ColumnProperty);
					}
					toFixup.ColumnProperty = column;
				}
			}
		}

		// Token: 0x0400089C RID: 2204
		private readonly IList<ColumnMappingBuilder> _columnMappings;

		// Token: 0x0400089D RID: 2205
		private readonly EntityType _table;

		// Token: 0x0400089E RID: 2206
		private readonly EdmModel _storeModel;
	}
}
