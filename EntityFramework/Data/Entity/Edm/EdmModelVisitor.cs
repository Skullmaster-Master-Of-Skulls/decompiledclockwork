using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.Edm
{
	// Token: 0x02000024 RID: 36
	internal abstract class EdmModelVisitor
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00007AB8 File Offset: 0x00005CB8
		protected static void VisitCollection<T>(IEnumerable<T> collection, Action<T> visitMethod)
		{
			if (collection != null)
			{
				foreach (T obj in collection)
				{
					visitMethod(obj);
				}
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00007B04 File Offset: 0x00005D04
		protected internal virtual void VisitEdmModel(EdmModel item)
		{
			if (item != null)
			{
				this.VisitComplexTypes(item.ComplexTypes);
				this.VisitEntityTypes(item.EntityTypes);
				this.VisitEnumTypes(item.EnumTypes);
				this.VisitAssociationTypes(item.AssociationTypes);
				this.VisitFunctions(item.Functions);
				this.VisitEntityContainers(item.Containers);
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00007B5C File Offset: 0x00005D5C
		protected virtual void VisitAnnotations(MetadataItem item, IEnumerable<MetadataProperty> annotations)
		{
			EdmModelVisitor.VisitCollection<MetadataProperty>(annotations, new Action<MetadataProperty>(this.VisitAnnotation));
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00007B71 File Offset: 0x00005D71
		protected virtual void VisitAnnotation(MetadataProperty item)
		{
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00007B73 File Offset: 0x00005D73
		protected internal virtual void VisitMetadataItem(MetadataItem item)
		{
			if (item != null && item.Annotations.Any<MetadataProperty>())
			{
				this.VisitAnnotations(item, item.Annotations);
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00007B92 File Offset: 0x00005D92
		protected virtual void VisitEntityContainers(IEnumerable<EntityContainer> entityContainers)
		{
			EdmModelVisitor.VisitCollection<EntityContainer>(entityContainers, new Action<EntityContainer>(this.VisitEdmEntityContainer));
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007BA8 File Offset: 0x00005DA8
		protected virtual void VisitEdmEntityContainer(EntityContainer item)
		{
			this.VisitMetadataItem(item);
			if (item != null)
			{
				if (item.EntitySets.Count > 0)
				{
					this.VisitEntitySets(item, item.EntitySets);
				}
				if (item.AssociationSets.Count > 0)
				{
					this.VisitAssociationSets(item, item.AssociationSets);
				}
				if (item.FunctionImports.Count > 0)
				{
					this.VisitFunctionImports(item, item.FunctionImports);
				}
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007C10 File Offset: 0x00005E10
		protected internal virtual void VisitEdmFunction(EdmFunction function)
		{
			this.VisitMetadataItem(function);
			if (function != null)
			{
				if (function.Parameters != null)
				{
					this.VisitFunctionParameters(function.Parameters);
				}
				if (function.ReturnParameters != null)
				{
					this.VisitFunctionReturnParameters(function.ReturnParameters);
				}
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007C44 File Offset: 0x00005E44
		protected virtual void VisitEntitySets(EntityContainer container, IEnumerable<EntitySet> entitySets)
		{
			EdmModelVisitor.VisitCollection<EntitySet>(entitySets, new Action<EntitySet>(this.VisitEdmEntitySet));
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007C59 File Offset: 0x00005E59
		protected internal virtual void VisitEdmEntitySet(EntitySet item)
		{
			this.VisitMetadataItem(item);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007C62 File Offset: 0x00005E62
		protected virtual void VisitAssociationSets(EntityContainer container, IEnumerable<AssociationSet> associationSets)
		{
			EdmModelVisitor.VisitCollection<AssociationSet>(associationSets, new Action<AssociationSet>(this.VisitEdmAssociationSet));
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007C77 File Offset: 0x00005E77
		protected virtual void VisitEdmAssociationSet(AssociationSet item)
		{
			this.VisitMetadataItem(item);
			if (item.SourceSet != null)
			{
				this.VisitEdmAssociationSetEnd(item.SourceSet);
			}
			if (item.TargetSet != null)
			{
				this.VisitEdmAssociationSetEnd(item.TargetSet);
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00007CA8 File Offset: 0x00005EA8
		protected virtual void VisitEdmAssociationSetEnd(EntitySet item)
		{
			this.VisitMetadataItem(item);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00007CB1 File Offset: 0x00005EB1
		protected internal virtual void VisitFunctionImports(EntityContainer container, IEnumerable<EdmFunction> functionImports)
		{
			EdmModelVisitor.VisitCollection<EdmFunction>(functionImports, new Action<EdmFunction>(this.VisitFunctionImport));
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007CC6 File Offset: 0x00005EC6
		protected internal virtual void VisitFunctionImport(EdmFunction functionImport)
		{
			this.VisitMetadataItem(functionImport);
			if (functionImport.Parameters != null)
			{
				this.VisitFunctionImportParameters(functionImport.Parameters);
			}
			if (functionImport.ReturnParameters != null)
			{
				this.VisitFunctionImportReturnParameters(functionImport.ReturnParameters);
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00007CF7 File Offset: 0x00005EF7
		protected internal virtual void VisitFunctionImportParameters(IEnumerable<FunctionParameter> parameters)
		{
			EdmModelVisitor.VisitCollection<FunctionParameter>(parameters, new Action<FunctionParameter>(this.VisitFunctionImportParameter));
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00007D0C File Offset: 0x00005F0C
		protected internal virtual void VisitFunctionImportParameter(FunctionParameter parameter)
		{
			this.VisitMetadataItem(parameter);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00007D15 File Offset: 0x00005F15
		protected internal virtual void VisitFunctionImportReturnParameters(IEnumerable<FunctionParameter> parameters)
		{
			EdmModelVisitor.VisitCollection<FunctionParameter>(parameters, new Action<FunctionParameter>(this.VisitFunctionImportReturnParameter));
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00007D2A File Offset: 0x00005F2A
		protected internal virtual void VisitFunctionImportReturnParameter(FunctionParameter parameter)
		{
			this.VisitMetadataItem(parameter);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00007D33 File Offset: 0x00005F33
		protected virtual void VisitComplexTypes(IEnumerable<ComplexType> complexTypes)
		{
			EdmModelVisitor.VisitCollection<ComplexType>(complexTypes, new Action<ComplexType>(this.VisitComplexType));
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00007D48 File Offset: 0x00005F48
		protected virtual void VisitComplexType(ComplexType item)
		{
			this.VisitMetadataItem(item);
			if (item.Properties.Count > 0)
			{
				EdmModelVisitor.VisitCollection<EdmProperty>(item.Properties, new Action<EdmProperty>(this.VisitEdmProperty));
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00007D77 File Offset: 0x00005F77
		protected virtual void VisitDeclaredProperties(ComplexType complexType, IEnumerable<EdmProperty> properties)
		{
			EdmModelVisitor.VisitCollection<EdmProperty>(properties, new Action<EdmProperty>(this.VisitEdmProperty));
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007D8C File Offset: 0x00005F8C
		protected virtual void VisitEntityTypes(IEnumerable<EntityType> entityTypes)
		{
			EdmModelVisitor.VisitCollection<EntityType>(entityTypes, new Action<EntityType>(this.VisitEdmEntityType));
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00007DA1 File Offset: 0x00005FA1
		protected virtual void VisitEnumTypes(IEnumerable<EnumType> enumTypes)
		{
			EdmModelVisitor.VisitCollection<EnumType>(enumTypes, new Action<EnumType>(this.VisitEdmEnumType));
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00007DB6 File Offset: 0x00005FB6
		protected internal virtual void VisitFunctions(IEnumerable<EdmFunction> functions)
		{
			EdmModelVisitor.VisitCollection<EdmFunction>(functions, new Action<EdmFunction>(this.VisitEdmFunction));
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00007DCB File Offset: 0x00005FCB
		protected virtual void VisitFunctionParameters(IEnumerable<FunctionParameter> parameters)
		{
			EdmModelVisitor.VisitCollection<FunctionParameter>(parameters, new Action<FunctionParameter>(this.VisitFunctionParameter));
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00007DE0 File Offset: 0x00005FE0
		protected internal virtual void VisitFunctionParameter(FunctionParameter functionParameter)
		{
			this.VisitMetadataItem(functionParameter);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00007DE9 File Offset: 0x00005FE9
		protected internal virtual void VisitFunctionReturnParameters(IEnumerable<FunctionParameter> returnParameters)
		{
			EdmModelVisitor.VisitCollection<FunctionParameter>(returnParameters, new Action<FunctionParameter>(this.VisitFunctionReturnParameter));
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00007DFE File Offset: 0x00005FFE
		protected internal virtual void VisitFunctionReturnParameter(FunctionParameter returnParameter)
		{
			this.VisitMetadataItem(returnParameter);
			this.VisitEdmType(returnParameter.TypeUsage.EdmType);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00007E18 File Offset: 0x00006018
		protected internal virtual void VisitEdmType(EdmType edmType)
		{
			BuiltInTypeKind builtInTypeKind = edmType.BuiltInTypeKind;
			if (builtInTypeKind == BuiltInTypeKind.CollectionType)
			{
				this.VisitCollectionType((CollectionType)edmType);
				return;
			}
			if (builtInTypeKind == BuiltInTypeKind.PrimitiveType)
			{
				this.VisitPrimitiveType((PrimitiveType)edmType);
				return;
			}
			if (builtInTypeKind != BuiltInTypeKind.RowType)
			{
				return;
			}
			this.VisitRowType((RowType)edmType);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00007E61 File Offset: 0x00006061
		protected internal virtual void VisitCollectionType(CollectionType collectionType)
		{
			this.VisitMetadataItem(collectionType);
			this.VisitEdmType(collectionType.TypeUsage.EdmType);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00007E7B File Offset: 0x0000607B
		protected internal virtual void VisitRowType(RowType rowType)
		{
			this.VisitMetadataItem(rowType);
			if (rowType.DeclaredProperties.Count > 0)
			{
				EdmModelVisitor.VisitCollection<EdmProperty>(rowType.DeclaredProperties, new Action<EdmProperty>(this.VisitEdmProperty));
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00007EAA File Offset: 0x000060AA
		protected internal virtual void VisitPrimitiveType(PrimitiveType primitiveType)
		{
			this.VisitMetadataItem(primitiveType);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00007EB3 File Offset: 0x000060B3
		protected virtual void VisitEdmEnumType(EnumType item)
		{
			this.VisitMetadataItem(item);
			if (item != null && item.Members.Count > 0)
			{
				this.VisitEnumMembers(item, item.Members);
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00007EDA File Offset: 0x000060DA
		protected virtual void VisitEnumMembers(EnumType enumType, IEnumerable<EnumMember> members)
		{
			EdmModelVisitor.VisitCollection<EnumMember>(members, new Action<EnumMember>(this.VisitEdmEnumTypeMember));
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00007EF0 File Offset: 0x000060F0
		protected internal virtual void VisitEdmEntityType(EntityType item)
		{
			this.VisitMetadataItem(item);
			if (item != null)
			{
				if (item.BaseType == null && item.KeyProperties.Count > 0)
				{
					this.VisitKeyProperties(item, item.KeyProperties);
				}
				if (item.DeclaredProperties.Count > 0)
				{
					this.VisitDeclaredProperties(item, item.DeclaredProperties);
				}
				if (item.DeclaredNavigationProperties.Count > 0)
				{
					this.VisitDeclaredNavigationProperties(item, item.DeclaredNavigationProperties);
				}
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00007F60 File Offset: 0x00006160
		protected virtual void VisitKeyProperties(EntityType entityType, IList<EdmProperty> properties)
		{
			EdmModelVisitor.VisitCollection<EdmProperty>(properties, new Action<EdmProperty>(this.VisitEdmProperty));
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00007F75 File Offset: 0x00006175
		protected virtual void VisitDeclaredProperties(EntityType entityType, IList<EdmProperty> properties)
		{
			EdmModelVisitor.VisitCollection<EdmProperty>(properties, new Action<EdmProperty>(this.VisitEdmProperty));
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00007F8A File Offset: 0x0000618A
		protected virtual void VisitDeclaredNavigationProperties(EntityType entityType, IEnumerable<NavigationProperty> navigationProperties)
		{
			EdmModelVisitor.VisitCollection<NavigationProperty>(navigationProperties, new Action<NavigationProperty>(this.VisitEdmNavigationProperty));
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00007F9F File Offset: 0x0000619F
		protected virtual void VisitAssociationTypes(IEnumerable<AssociationType> associationTypes)
		{
			EdmModelVisitor.VisitCollection<AssociationType>(associationTypes, new Action<AssociationType>(this.VisitEdmAssociationType));
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00007FB4 File Offset: 0x000061B4
		protected internal virtual void VisitEdmAssociationType(AssociationType item)
		{
			this.VisitMetadataItem(item);
			if (item != null)
			{
				if (item.SourceEnd != null)
				{
					this.VisitEdmAssociationEnd(item.SourceEnd);
				}
				if (item.TargetEnd != null)
				{
					this.VisitEdmAssociationEnd(item.TargetEnd);
				}
			}
			if (item.Constraint != null)
			{
				this.VisitEdmAssociationConstraint(item.Constraint);
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00008007 File Offset: 0x00006207
		protected internal virtual void VisitEdmProperty(EdmProperty item)
		{
			this.VisitMetadataItem(item);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00008010 File Offset: 0x00006210
		protected virtual void VisitEdmEnumTypeMember(EnumMember item)
		{
			this.VisitMetadataItem(item);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00008019 File Offset: 0x00006219
		protected virtual void VisitEdmAssociationEnd(RelationshipEndMember item)
		{
			this.VisitMetadataItem(item);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00008022 File Offset: 0x00006222
		protected virtual void VisitEdmAssociationConstraint(ReferentialConstraint item)
		{
			if (item != null)
			{
				this.VisitMetadataItem(item);
				if (item.ToRole != null)
				{
					this.VisitEdmAssociationEnd(item.ToRole);
				}
				EdmModelVisitor.VisitCollection<EdmProperty>(item.ToProperties, new Action<EdmProperty>(this.VisitEdmProperty));
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000805A File Offset: 0x0000625A
		protected virtual void VisitEdmNavigationProperty(NavigationProperty item)
		{
			this.VisitMetadataItem(item);
		}
	}
}
