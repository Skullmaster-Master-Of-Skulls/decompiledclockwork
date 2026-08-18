using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003E6 RID: 998
	public sealed class ModificationFunctionMapping : MappingItem
	{
		// Token: 0x060024F3 RID: 9459 RVA: 0x000AE720 File Offset: 0x000AC920
		public ModificationFunctionMapping(EntitySetBase entitySet, EntityTypeBase entityType, EdmFunction function, IEnumerable<ModificationFunctionParameterBinding> parameterBindings, FunctionParameter rowsAffectedParameter, IEnumerable<ModificationFunctionResultBinding> resultBindings)
		{
			Check.NotNull<EntitySetBase>(entitySet, "entitySet");
			Check.NotNull<EdmFunction>(function, "function");
			Check.NotNull<IEnumerable<ModificationFunctionParameterBinding>>(parameterBindings, "parameterBindings");
			this._function = function;
			this._rowsAffectedParameter = rowsAffectedParameter;
			this._parameterBindings = new ReadOnlyCollection<ModificationFunctionParameterBinding>(parameterBindings.ToList<ModificationFunctionParameterBinding>());
			if (resultBindings != null)
			{
				List<ModificationFunctionResultBinding> list = resultBindings.ToList<ModificationFunctionResultBinding>();
				if (0 < list.Count)
				{
					this._resultBindings = new ReadOnlyCollection<ModificationFunctionResultBinding>(list);
				}
			}
			this._collocatedAssociationSetEnds = new ReadOnlyCollection<AssociationSetEnd>(ModificationFunctionMapping.GetReferencedAssociationSetEnds(entitySet as EntitySet, entityType as EntityType, parameterBindings).ToList<AssociationSetEnd>());
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x000AE7BD File Offset: 0x000AC9BD
		// (set) Token: 0x060024F5 RID: 9461 RVA: 0x000AE7C5 File Offset: 0x000AC9C5
		public FunctionParameter RowsAffectedParameter
		{
			get
			{
				return this._rowsAffectedParameter;
			}
			internal set
			{
				this._rowsAffectedParameter = value;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060024F6 RID: 9462 RVA: 0x000AE7CE File Offset: 0x000AC9CE
		internal string RowsAffectedParameterName
		{
			get
			{
				if (this.RowsAffectedParameter == null)
				{
					return null;
				}
				return this.RowsAffectedParameter.Name;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060024F7 RID: 9463 RVA: 0x000AE7E5 File Offset: 0x000AC9E5
		public EdmFunction Function
		{
			get
			{
				return this._function;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060024F8 RID: 9464 RVA: 0x000AE7ED File Offset: 0x000AC9ED
		public ReadOnlyCollection<ModificationFunctionParameterBinding> ParameterBindings
		{
			get
			{
				return this._parameterBindings;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060024F9 RID: 9465 RVA: 0x000AE7F5 File Offset: 0x000AC9F5
		internal ReadOnlyCollection<AssociationSetEnd> CollocatedAssociationSetEnds
		{
			get
			{
				return this._collocatedAssociationSetEnds;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060024FA RID: 9466 RVA: 0x000AE7FD File Offset: 0x000AC9FD
		public ReadOnlyCollection<ModificationFunctionResultBinding> ResultBindings
		{
			get
			{
				return this._resultBindings;
			}
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x000AE808 File Offset: 0x000ACA08
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Func{{{0}}}: Prm={{{1}}}, Result={{{2}}}", new object[]
			{
				this.Function,
				StringUtil.ToCommaSeparatedStringSorted(this.ParameterBindings),
				StringUtil.ToCommaSeparatedStringSorted(this.ResultBindings)
			});
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x000AE851 File Offset: 0x000ACA51
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this._parameterBindings);
			MappingItem.SetReadOnly(this._resultBindings);
			base.SetReadOnly();
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x000AE870 File Offset: 0x000ACA70
		private static IEnumerable<AssociationSetEnd> GetReferencedAssociationSetEnds(EntitySet entitySet, EntityType entityType, IEnumerable<ModificationFunctionParameterBinding> parameterBindings)
		{
			HashSet<AssociationSetEnd> hashSet = new HashSet<AssociationSetEnd>();
			if (entitySet != null && entityType != null)
			{
				foreach (ModificationFunctionParameterBinding modificationFunctionParameterBinding in parameterBindings)
				{
					AssociationSetEnd associationSetEnd = modificationFunctionParameterBinding.MemberPath.AssociationSetEnd;
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

		// Token: 0x04000D5E RID: 3422
		private FunctionParameter _rowsAffectedParameter;

		// Token: 0x04000D5F RID: 3423
		private readonly EdmFunction _function;

		// Token: 0x04000D60 RID: 3424
		private readonly ReadOnlyCollection<ModificationFunctionParameterBinding> _parameterBindings;

		// Token: 0x04000D61 RID: 3425
		private readonly ReadOnlyCollection<AssociationSetEnd> _collocatedAssociationSetEnds;

		// Token: 0x04000D62 RID: 3426
		private readonly ReadOnlyCollection<ModificationFunctionResultBinding> _resultBindings;
	}
}
