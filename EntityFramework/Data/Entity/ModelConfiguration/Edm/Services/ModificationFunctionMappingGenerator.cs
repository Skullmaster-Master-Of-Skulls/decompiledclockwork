using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Mapping.Update.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Edm.Services
{
	// Token: 0x020002CA RID: 714
	internal class ModificationFunctionMappingGenerator : StructuralTypeMappingGenerator
	{
		// Token: 0x0600193D RID: 6461 RVA: 0x0007D069 File Offset: 0x0007B269
		public ModificationFunctionMappingGenerator(DbProviderManifest providerManifest) : base(providerManifest)
		{
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x0007D0A4 File Offset: 0x0007B2A4
		public void Generate(EntityType entityType, DbDatabaseMapping databaseMapping)
		{
			if (entityType.Abstract)
			{
				return;
			}
			EntitySet entitySet = databaseMapping.Model.GetEntitySet(entityType);
			EntitySetMapping entitySetMapping = databaseMapping.GetEntitySetMapping(entitySet);
			List<ColumnMappingBuilder> columnMappings = ModificationFunctionMappingGenerator.GetColumnMappings(entityType, entitySetMapping).ToList<ColumnMappingBuilder>();
			List<Tuple<ModificationFunctionMemberPath, EdmProperty>> iaFkProperties = ModificationFunctionMappingGenerator.GetIndependentFkColumns(entityType, databaseMapping).ToList<Tuple<ModificationFunctionMemberPath, EdmProperty>>();
			ModificationFunctionMapping insertFunctionMapping = this.GenerateFunctionMapping(ModificationOperator.Insert, entitySetMapping.EntitySet, entityType, databaseMapping, entityType.Properties, iaFkProperties, columnMappings, from p in entityType.Properties
			where p.HasStoreGeneratedPattern()
			select p, null);
			ModificationFunctionMapping updateFunctionMapping = this.GenerateFunctionMapping(ModificationOperator.Update, entitySetMapping.EntitySet, entityType, databaseMapping, entityType.Properties, iaFkProperties, columnMappings, from p in entityType.Properties
			where p.GetStoreGeneratedPattern() == StoreGeneratedPattern.Computed
			select p, null);
			ModificationFunctionMapping deleteFunctionMapping = this.GenerateFunctionMapping(ModificationOperator.Delete, entitySetMapping.EntitySet, entityType, databaseMapping, entityType.Properties, iaFkProperties, columnMappings, null, null);
			EntityTypeModificationFunctionMapping modificationFunctionMapping = new EntityTypeModificationFunctionMapping(entityType, deleteFunctionMapping, insertFunctionMapping, updateFunctionMapping);
			entitySetMapping.AddModificationFunctionMapping(modificationFunctionMapping);
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x0007D258 File Offset: 0x0007B458
		private static IEnumerable<ColumnMappingBuilder> GetColumnMappings(EntityType entityType, EntitySetMapping entitySetMapping)
		{
			return new EntityType[]
			{
				entityType
			}.Concat(ModificationFunctionMappingGenerator.GetParents(entityType)).SelectMany((EntityType et) => (from stm in entitySetMapping.TypeMappings
			where stm.Types.Contains(et)
			select stm).SelectMany((TypeMapping stm) => stm.MappingFragments).SelectMany((MappingFragment mf) => mf.ColumnMappings));
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x0007D29C File Offset: 0x0007B49C
		public void Generate(AssociationSetMapping associationSetMapping, DbDatabaseMapping databaseMapping)
		{
			List<Tuple<ModificationFunctionMemberPath, EdmProperty>> iaFkProperties = ModificationFunctionMappingGenerator.GetIndependentFkColumns(associationSetMapping).ToList<Tuple<ModificationFunctionMemberPath, EdmProperty>>();
			EntityType entityType = associationSetMapping.AssociationSet.ElementType.SourceEnd.GetEntityType();
			EntityType entityType2 = associationSetMapping.AssociationSet.ElementType.TargetEnd.GetEntityType();
			string functionNamePrefix = entityType.Name + entityType2.Name;
			ModificationFunctionMapping insertFunctionMapping = this.GenerateFunctionMapping(ModificationOperator.Insert, associationSetMapping.AssociationSet, associationSetMapping.AssociationSet.ElementType, databaseMapping, Enumerable.Empty<EdmProperty>(), iaFkProperties, new ColumnMappingBuilder[0], null, functionNamePrefix);
			ModificationFunctionMapping deleteFunctionMapping = this.GenerateFunctionMapping(ModificationOperator.Delete, associationSetMapping.AssociationSet, associationSetMapping.AssociationSet.ElementType, databaseMapping, Enumerable.Empty<EdmProperty>(), iaFkProperties, new ColumnMappingBuilder[0], null, functionNamePrefix);
			associationSetMapping.ModificationFunctionMapping = new AssociationSetModificationFunctionMapping(associationSetMapping.AssociationSet, deleteFunctionMapping, insertFunctionMapping);
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0007D63C File Offset: 0x0007B83C
		private static IEnumerable<Tuple<ModificationFunctionMemberPath, EdmProperty>> GetIndependentFkColumns(AssociationSetMapping associationSetMapping)
		{
			foreach (ScalarPropertyMapping propertyMapping in associationSetMapping.SourceEndMapping.PropertyMappings)
			{
				yield return Tuple.Create<ModificationFunctionMemberPath, EdmProperty>(new ModificationFunctionMemberPath(new EdmMember[]
				{
					propertyMapping.Property,
					associationSetMapping.SourceEndMapping.AssociationEnd
				}, associationSetMapping.AssociationSet), propertyMapping.Column);
			}
			foreach (ScalarPropertyMapping propertyMapping2 in associationSetMapping.TargetEndMapping.PropertyMappings)
			{
				yield return Tuple.Create<ModificationFunctionMemberPath, EdmProperty>(new ModificationFunctionMemberPath(new EdmMember[]
				{
					propertyMapping2.Property,
					associationSetMapping.TargetEndMapping.AssociationEnd
				}, associationSetMapping.AssociationSet), propertyMapping2.Column);
			}
			yield break;
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0007D988 File Offset: 0x0007BB88
		private static IEnumerable<Tuple<ModificationFunctionMemberPath, EdmProperty>> GetIndependentFkColumns(EntityType entityType, DbDatabaseMapping databaseMapping)
		{
			foreach (AssociationSetMapping associationSetMapping in databaseMapping.GetAssociationSetMappings())
			{
				AssociationType associationType = associationSetMapping.AssociationSet.ElementType;
				if (!associationType.IsManyToMany())
				{
					AssociationEndMember _;
					AssociationEndMember dependentEnd;
					if (!associationType.TryGuessPrincipalAndDependentEnds(out _, out dependentEnd))
					{
						dependentEnd = associationType.TargetEnd;
					}
					EntityType dependentEntityType = dependentEnd.GetEntityType();
					if (dependentEntityType == entityType || ModificationFunctionMappingGenerator.GetParents(entityType).Contains(dependentEntityType))
					{
						EndPropertyMapping endPropertyMapping = (associationSetMapping.TargetEndMapping.AssociationEnd != dependentEnd) ? associationSetMapping.TargetEndMapping : associationSetMapping.SourceEndMapping;
						foreach (ScalarPropertyMapping propertyMapping in endPropertyMapping.PropertyMappings)
						{
							yield return Tuple.Create<ModificationFunctionMemberPath, EdmProperty>(new ModificationFunctionMemberPath(new EdmMember[]
							{
								propertyMapping.Property,
								dependentEnd
							}, associationSetMapping.AssociationSet), propertyMapping.Column);
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x0007DAA4 File Offset: 0x0007BCA4
		private static IEnumerable<EntityType> GetParents(EntityType entityType)
		{
			while (entityType.BaseType != null)
			{
				yield return (EntityType)entityType.BaseType;
				entityType = (EntityType)entityType.BaseType;
			}
			yield break;
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x0007DB58 File Offset: 0x0007BD58
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		private ModificationFunctionMapping GenerateFunctionMapping(ModificationOperator modificationOperator, EntitySetBase entitySetBase, EntityTypeBase entityTypeBase, DbDatabaseMapping databaseMapping, IEnumerable<EdmProperty> parameterProperties, IEnumerable<Tuple<ModificationFunctionMemberPath, EdmProperty>> iaFkProperties, IList<ColumnMappingBuilder> columnMappings, IEnumerable<EdmProperty> resultProperties = null, string functionNamePrefix = null)
		{
			bool useOriginalValues = modificationOperator == ModificationOperator.Delete;
			FunctionParameterMappingGenerator functionParameterMappingGenerator = new FunctionParameterMappingGenerator(this._providerManifest);
			List<ModificationFunctionParameterBinding> list = functionParameterMappingGenerator.Generate((modificationOperator == ModificationOperator.Insert && ModificationFunctionMappingGenerator.IsTableSplitDependent(entityTypeBase, databaseMapping)) ? ModificationOperator.Update : modificationOperator, parameterProperties, columnMappings, new List<EdmProperty>(), useOriginalValues).Concat(functionParameterMappingGenerator.Generate(iaFkProperties, useOriginalValues)).ToList<ModificationFunctionParameterBinding>();
			List<FunctionParameter> list2 = (from b in list
			select b.Parameter).ToList<FunctionParameter>();
			ModificationFunctionMappingGenerator.UniquifyParameterNames(list2);
			EdmFunctionPayload functionPayload = new EdmFunctionPayload
			{
				ReturnParameters = new FunctionParameter[0],
				Parameters = list2.ToArray(),
				IsComposable = new bool?(false)
			};
			EdmFunction function = databaseMapping.Database.AddFunction((functionNamePrefix ?? entityTypeBase.Name) + "_" + modificationOperator.ToString(), functionPayload);
			return new ModificationFunctionMapping(entitySetBase, entityTypeBase, function, list, null, (resultProperties != null) ? (from p in resultProperties
			select new ModificationFunctionResultBinding(columnMappings.First((ColumnMappingBuilder cm) => cm.PropertyPath.SequenceEqual(new EdmProperty[]
			{
				p
			})).ColumnProperty.Name, p)) : null);
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0007DD50 File Offset: 0x0007BF50
		private static bool IsTableSplitDependent(EntityTypeBase entityTypeBase, DbDatabaseMapping databaseMapping)
		{
			AssociationType associationType = databaseMapping.Model.AssociationTypes.SingleOrDefault((AssociationType at) => at.IsForeignKey && at.IsRequiredToRequired() && !at.IsSelfReferencing() && (at.SourceEnd.GetEntityType().IsAssignableFrom(entityTypeBase) || at.TargetEnd.GetEntityType().IsAssignableFrom(entityTypeBase)) && databaseMapping.Database.AssociationTypes.All((AssociationType fk) => fk.Name != at.Name));
			return associationType != null && associationType.TargetEnd.GetEntityType() == entityTypeBase;
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x0007DDAC File Offset: 0x0007BFAC
		private static void UniquifyParameterNames(IList<FunctionParameter> parameters)
		{
			foreach (FunctionParameter functionParameter in parameters)
			{
				functionParameter.Name = parameters.Except(new FunctionParameter[]
				{
					functionParameter
				}).UniquifyName(functionParameter.Name);
			}
		}
	}
}
