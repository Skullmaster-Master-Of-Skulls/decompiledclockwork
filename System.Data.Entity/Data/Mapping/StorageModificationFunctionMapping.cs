using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Linq;

namespace System.Data.Mapping
{
	// Token: 0x02000246 RID: 582
	internal sealed class StorageModificationFunctionMapping
	{
		// Token: 0x0600247A RID: 9338 RVA: 0x0008406C File Offset: 0x0008226C
		internal StorageModificationFunctionMapping(EntitySetBase entitySet, EntityTypeBase entityType, EdmFunction function, IEnumerable<StorageModificationFunctionParameterBinding> parameterBindings, FunctionParameter rowsAffectedParameter, IEnumerable<StorageModificationFunctionResultBinding> resultBindings)
		{
			EntityUtil.CheckArgumentNull<EntitySetBase>(entitySet, "entitySet");
			this.Function = EntityUtil.CheckArgumentNull<EdmFunction>(function, "function");
			this.RowsAffectedParameter = rowsAffectedParameter;
			this.ParameterBindings = EntityUtil.CheckArgumentNull<IEnumerable<StorageModificationFunctionParameterBinding>>(parameterBindings, "parameterBindings").ToList<StorageModificationFunctionParameterBinding>().AsReadOnly();
			if (resultBindings != null)
			{
				List<StorageModificationFunctionResultBinding> list = resultBindings.ToList<StorageModificationFunctionResultBinding>();
				if (0 < list.Count)
				{
					this.ResultBindings = list.AsReadOnly();
				}
			}
			this.CollocatedAssociationSetEnds = StorageModificationFunctionMapping.GetReferencedAssociationSetEnds(entitySet as EntitySet, entityType as EntityType, parameterBindings).ToList<AssociationSetEnd>().AsReadOnly();
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x00084104 File Offset: 0x00082304
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Func{{{0}}}: Prm={{{1}}}, Result={{{2}}}", new object[]
			{
				this.Function,
				StringUtil.ToCommaSeparatedStringSorted(this.ParameterBindings),
				StringUtil.ToCommaSeparatedStringSorted(this.ResultBindings)
			});
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x00084140 File Offset: 0x00082340
		private static IEnumerable<AssociationSetEnd> GetReferencedAssociationSetEnds(EntitySet entitySet, EntityType entityType, IEnumerable<StorageModificationFunctionParameterBinding> parameterBindings)
		{
			HashSet<AssociationSetEnd> hashSet = new HashSet<AssociationSetEnd>();
			if (entitySet != null && entityType != null)
			{
				foreach (StorageModificationFunctionParameterBinding storageModificationFunctionParameterBinding in parameterBindings)
				{
					AssociationSetEnd associationSetEnd = storageModificationFunctionParameterBinding.MemberPath.AssociationSetEnd;
					if (associationSetEnd != null)
					{
						hashSet.Add(associationSetEnd);
					}
				}
				foreach (AssociationSet associationSet in MetadataHelper.GetAssociationsForEntitySet(entitySet))
				{
					ReadOnlyMetadataCollection<ReferentialConstraint> referentialConstraints = associationSet.ElementType.ReferentialConstraints;
					if (referentialConstraints != null)
					{
						foreach (ReferentialConstraint referentialConstraint in referentialConstraints)
						{
							if (associationSet.AssociationSetEnds[referentialConstraint.ToRole.Name].EntitySet == entitySet && referentialConstraint.ToRole.GetEntityType().IsAssignableFrom(entityType))
							{
								hashSet.Add(associationSet.AssociationSetEnds[referentialConstraint.FromRole.Name]);
							}
						}
					}
				}
			}
			return hashSet;
		}

		// Token: 0x0400102B RID: 4139
		internal readonly FunctionParameter RowsAffectedParameter;

		// Token: 0x0400102C RID: 4140
		internal readonly EdmFunction Function;

		// Token: 0x0400102D RID: 4141
		internal readonly ReadOnlyCollection<StorageModificationFunctionParameterBinding> ParameterBindings;

		// Token: 0x0400102E RID: 4142
		internal readonly ReadOnlyCollection<AssociationSetEnd> CollocatedAssociationSetEnds;

		// Token: 0x0400102F RID: 4143
		internal readonly ReadOnlyCollection<StorageModificationFunctionResultBinding> ResultBindings;
	}
}
