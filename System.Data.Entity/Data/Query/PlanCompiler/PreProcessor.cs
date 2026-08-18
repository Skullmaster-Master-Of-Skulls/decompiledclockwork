using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Mapping;
using System.Data.Mapping.ViewGeneration;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200005F RID: 95
	internal class PreProcessor : SubqueryTrackingVisitor
	{
		// Token: 0x060007ED RID: 2029 RVA: 0x00029174 File Offset: 0x00027374
		private PreProcessor(PlanCompiler planCompilerState) : base(planCompilerState)
		{
			this.m_relPropertyHelper = new RelPropertyHelper(base.m_command.MetadataWorkspace, base.m_command.ReferencedRelProperties);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00029204 File Offset: 0x00027404
		internal static void Process(PlanCompiler planCompilerState, out StructuredTypeInfo typeInfo, out Dictionary<EdmFunction, EdmProperty[]> tvfResultKeys)
		{
			PreProcessor preProcessor = new PreProcessor(planCompilerState);
			preProcessor.Process(out tvfResultKeys);
			StructuredTypeInfo.Process(planCompilerState.Command, preProcessor.m_referencedTypes, preProcessor.m_referencedEntitySets, preProcessor.m_freeFloatingEntityConstructorTypes, preProcessor.m_suppressDiscriminatorMaps ? null : preProcessor.m_discriminatorMaps, preProcessor.m_relPropertyHelper, preProcessor.m_typesNeedingNullSentinel, out typeInfo);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0002925C File Offset: 0x0002745C
		internal void Process(out Dictionary<EdmFunction, EdmProperty[]> tvfResultKeys)
		{
			base.m_command.Root = base.VisitNode(base.m_command.Root);
			foreach (Var var in base.m_command.Vars)
			{
				this.AddTypeReference(var.Type);
			}
			if (this.m_referencedTypes.Count > 0)
			{
				this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.NTE);
				PhysicalProjectOp physicalProjectOp = (PhysicalProjectOp)base.m_command.Root.Op;
				physicalProjectOp.ColumnMap.Accept<HashSet<string>>(StructuredTypeNullabilityAnalyzer.Instance, this.m_typesNeedingNullSentinel);
			}
			tvfResultKeys = this.m_tvfResultKeys;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00029320 File Offset: 0x00027520
		private void AddEntitySetReference(EntitySet entitySet)
		{
			this.m_referencedEntitySets.Add(entitySet);
			if (!this.m_referencedEntityContainers.Contains(entitySet.EntityContainer))
			{
				this.m_referencedEntityContainers.Add(entitySet.EntityContainer);
			}
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00029354 File Offset: 0x00027554
		private void AddTypeReference(TypeUsage type)
		{
			if (TypeUtils.IsStructuredType(type) || TypeUtils.IsCollectionType(type) || TypeUtils.IsEnumerationType(type))
			{
				this.m_referencedTypes.Add(type);
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0002937C File Offset: 0x0002757C
		private List<RelationshipSet> GetRelationshipSets(RelationshipType relType)
		{
			List<RelationshipSet> list = new List<RelationshipSet>();
			foreach (EntityContainer entityContainer in this.m_referencedEntityContainers)
			{
				foreach (EntitySetBase entitySetBase in entityContainer.BaseEntitySets)
				{
					RelationshipSet relationshipSet = entitySetBase as RelationshipSet;
					if (relationshipSet != null && relationshipSet.ElementType.Equals(relType))
					{
						list.Add(relationshipSet);
					}
				}
			}
			return list;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00029430 File Offset: 0x00027630
		private List<EntitySet> GetEntitySets(TypeUsage entityType)
		{
			List<EntitySet> list = new List<EntitySet>();
			foreach (EntityContainer entityContainer in this.m_referencedEntityContainers)
			{
				foreach (EntitySetBase entitySetBase in entityContainer.BaseEntitySets)
				{
					EntitySet entitySet = entitySetBase as EntitySet;
					if (entitySet != null && (entitySet.ElementType.Equals(entityType.EdmType) || TypeSemantics.IsSubTypeOf(entityType.EdmType, entitySet.ElementType) || TypeSemantics.IsSubTypeOf(entitySet.ElementType, entityType.EdmType)))
					{
						list.Add(entitySet);
					}
				}
			}
			return list;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00029518 File Offset: 0x00027718
		private Node ExpandView(Node node, ScanTableOp scanTableOp, ref IsOfOp typeFilter)
		{
			EntitySetBase extent = scanTableOp.Table.TableMetadata.Extent;
			PlanCompiler.Assert(extent != null, "The target of a ScanTableOp must reference an EntitySet to be used with ExpandView");
			PlanCompiler.Assert(extent.EntityContainer.DataSpace == DataSpace.CSpace, "Store entity sets cannot have Query Mapping Views and should not be used with ExpandView");
			if (typeFilter != null && !typeFilter.IsOfOnly && TypeSemantics.IsSubTypeOf(extent.ElementType, typeFilter.IsOfType.EdmType))
			{
				typeFilter = null;
			}
			GeneratedView generatedView = null;
			EntityTypeBase entityTypeBase = scanTableOp.Table.TableMetadata.Extent.ElementType;
			bool includeSubtypes = true;
			if (typeFilter != null)
			{
				entityTypeBase = (EntityTypeBase)typeFilter.IsOfType.EdmType;
				includeSubtypes = !typeFilter.IsOfOnly;
				if (base.m_command.MetadataWorkspace.TryGetGeneratedViewOfType(extent, entityTypeBase, includeSubtypes, out generatedView))
				{
					typeFilter = null;
				}
			}
			if (generatedView == null)
			{
				generatedView = base.m_command.MetadataWorkspace.GetGeneratedView(extent);
			}
			PlanCompiler.Assert(generatedView != null, Strings.ADP_NoQueryMappingView(extent.EntityContainer.Name, extent.Name));
			Node internalTree = generatedView.GetInternalTree(base.m_command);
			this.DetermineDiscriminatorMapUsage(internalTree, extent, entityTypeBase, includeSubtypes);
			ScanViewOp op = base.m_command.CreateScanViewOp(scanTableOp.Table);
			return base.m_command.CreateNode(op, internalTree);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00029650 File Offset: 0x00027850
		private void DetermineDiscriminatorMapUsage(Node viewNode, EntitySetBase entitySet, EntityTypeBase rootEntityType, bool includeSubtypes)
		{
			ExplicitDiscriminatorMap discriminatorMap = null;
			if (viewNode.Op.OpType == OpType.Project)
			{
				DiscriminatedNewEntityOp discriminatedNewEntityOp = viewNode.Child1.Child0.Child0.Op as DiscriminatedNewEntityOp;
				if (discriminatedNewEntityOp != null)
				{
					discriminatorMap = discriminatedNewEntityOp.DiscriminatorMap;
				}
			}
			DiscriminatorMapInfo discriminatorMapInfo;
			if (!this.m_discriminatorMaps.TryGetValue(entitySet, out discriminatorMapInfo))
			{
				if (rootEntityType == null)
				{
					rootEntityType = entitySet.ElementType;
					includeSubtypes = true;
				}
				discriminatorMapInfo = new DiscriminatorMapInfo(rootEntityType, includeSubtypes, discriminatorMap);
				this.m_discriminatorMaps.Add(entitySet, discriminatorMapInfo);
				return;
			}
			discriminatorMapInfo.Merge(rootEntityType, includeSubtypes, discriminatorMap);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x000296D4 File Offset: 0x000278D4
		private Node RewriteNavigateOp(Node navigateOpNode, NavigateOp navigateOp, out Var outputVar)
		{
			outputVar = null;
			if (!Helper.IsAssociationType(navigateOp.Relationship))
			{
				throw EntityUtil.NotSupported(Strings.Cqt_RelNav_NoCompositions);
			}
			if (navigateOpNode.Child0.Op.OpType == OpType.GetEntityRef && (navigateOp.ToEnd.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne || navigateOp.ToEnd.RelationshipMultiplicity == RelationshipMultiplicity.One))
			{
				bool condition = base.m_command.IsRelPropertyReferenced(navigateOp.RelProperty);
				string str = "Unreferenced rel property? ";
				RelProperty relProperty = navigateOp.RelProperty;
				PlanCompiler.Assert(condition, str + ((relProperty != null) ? relProperty.ToString() : null));
				Op op = base.m_command.CreateRelPropertyOp(navigateOp.RelProperty);
				return base.m_command.CreateNode(op, navigateOpNode.Child0.Child0);
			}
			List<RelationshipSet> relationshipSets = this.GetRelationshipSets(navigateOp.Relationship);
			if (relationshipSets.Count != 0)
			{
				List<Node> list = new List<Node>();
				List<Var> list2 = new List<Var>();
				foreach (RelationshipSet extent in relationshipSets)
				{
					TableMD tableMetadata = Command.CreateTableDefinition(extent);
					ScanTableOp scanTableOp = base.m_command.CreateScanTableOp(tableMetadata);
					Node item = base.m_command.CreateNode(scanTableOp);
					Var item2 = scanTableOp.Table.Columns[0];
					list2.Add(item2);
					list.Add(item);
				}
				Node arg = null;
				Var v;
				base.m_command.BuildUnionAllLadder(list, list2, out arg, out v);
				Node computedExpression = base.m_command.CreateNode(base.m_command.CreatePropertyOp(navigateOp.ToEnd), base.m_command.CreateNode(base.m_command.CreateVarRefOp(v)));
				Node arg2 = base.m_command.CreateNode(base.m_command.CreatePropertyOp(navigateOp.FromEnd), base.m_command.CreateNode(base.m_command.CreateVarRefOp(v)));
				Node arg3 = base.m_command.BuildComparison(OpType.EQ, navigateOpNode.Child0, arg2);
				Node input = base.m_command.CreateNode(base.m_command.CreateFilterOp(), arg, arg3);
				Var var;
				Node node = base.m_command.BuildProject(input, computedExpression, out var);
				Node result;
				if (navigateOp.ToEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many)
				{
					result = base.m_command.BuildCollect(node, var);
				}
				else
				{
					result = node;
					outputVar = var;
				}
				return result;
			}
			if (navigateOp.ToEnd.RelationshipMultiplicity != RelationshipMultiplicity.Many)
			{
				return base.m_command.CreateNode(base.m_command.CreateNullOp(navigateOp.Type));
			}
			return base.m_command.CreateNode(base.m_command.CreateNewMultisetOp(navigateOp.Type));
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0002996C File Offset: 0x00027B6C
		private Node BuildOfTypeTable(EntitySetBase entitySet, TypeUsage ofType, out Var resultVar)
		{
			TableMD tableMetadata = Command.CreateTableDefinition(entitySet);
			ScanTableOp scanTableOp = base.m_command.CreateScanTableOp(tableMetadata);
			Node node = base.m_command.CreateNode(scanTableOp);
			Var var = scanTableOp.Table.Columns[0];
			Node result;
			if (ofType != null && !entitySet.ElementType.EdmEquals(ofType.EdmType))
			{
				base.m_command.BuildOfTypeTree(node, var, ofType, true, out result, out resultVar);
			}
			else
			{
				result = node;
				resultVar = var;
			}
			return result;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x000299E0 File Offset: 0x00027BE0
		private Node RewriteDerefOp(Node derefOpNode, DerefOp derefOp, out Var outputVar)
		{
			TypeUsage type = derefOp.Type;
			List<EntitySet> entitySets = this.GetEntitySets(type);
			if (entitySets.Count == 0)
			{
				outputVar = null;
				return base.m_command.CreateNode(base.m_command.CreateNullOp(type));
			}
			List<Node> list = new List<Node>();
			List<Var> list2 = new List<Var>();
			foreach (EntitySet entitySet in entitySets)
			{
				Var item2;
				Node item = this.BuildOfTypeTable(entitySet, type, out item2);
				list.Add(item);
				list2.Add(item2);
			}
			Node arg;
			Var var;
			base.m_command.BuildUnionAllLadder(list, list2, out arg, out var);
			Node arg2 = base.m_command.CreateNode(base.m_command.CreateGetEntityRefOp(derefOpNode.Child0.Op.Type), base.m_command.CreateNode(base.m_command.CreateVarRefOp(var)));
			Node arg3 = base.m_command.BuildComparison(OpType.EQ, derefOpNode.Child0, arg2);
			Node result = base.m_command.CreateNode(base.m_command.CreateFilterOp(), arg, arg3);
			outputVar = var;
			return result;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00029B10 File Offset: 0x00027D10
		private static EntitySetBase FindTargetEntitySet(RelationshipSet relationshipSet, RelationshipEndMember targetEnd)
		{
			EntitySetBase entitySetBase = null;
			AssociationSet associationSet = (AssociationSet)relationshipSet;
			entitySetBase = null;
			foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
			{
				if (associationSetEnd.CorrespondingAssociationEndMember.EdmEquals(targetEnd))
				{
					entitySetBase = associationSetEnd.EntitySet;
					break;
				}
			}
			PlanCompiler.Assert(entitySetBase != null, "Could not find entityset for relationshipset " + ((relationshipSet != null) ? relationshipSet.ToString() : null) + ";association end " + ((targetEnd != null) ? targetEnd.ToString() : null));
			return entitySetBase;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00029BB4 File Offset: 0x00027DB4
		private Node BuildJoinForNavProperty(RelationshipSet relSet, RelationshipEndMember end, out Var rsVar, out Var esVar)
		{
			EntitySetBase entitySet = PreProcessor.FindTargetEntitySet(relSet, end);
			Node arg = this.BuildOfTypeTable(relSet, null, out rsVar);
			Node arg2 = this.BuildOfTypeTable(entitySet, TypeHelpers.GetElementTypeUsage(end.TypeUsage), out esVar);
			Node arg3 = base.m_command.BuildComparison(OpType.EQ, base.m_command.CreateNode(base.m_command.CreateGetEntityRefOp(end.TypeUsage), base.m_command.CreateNode(base.m_command.CreateVarRefOp(esVar))), base.m_command.CreateNode(base.m_command.CreatePropertyOp(end), base.m_command.CreateNode(base.m_command.CreateVarRefOp(rsVar))));
			return base.m_command.CreateNode(base.m_command.CreateInnerJoinOp(), arg, arg2, arg3);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00029C78 File Offset: 0x00027E78
		private Node RewriteManyToOneNavigationProperty(RelProperty relProperty, Node sourceEntityNode, TypeUsage resultType)
		{
			RelPropertyOp op = base.m_command.CreateRelPropertyOp(relProperty);
			Node arg = base.m_command.CreateNode(op, sourceEntityNode);
			DerefOp op2 = base.m_command.CreateDerefOp(resultType);
			return base.m_command.CreateNode(op2, arg);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00029CBC File Offset: 0x00027EBC
		private Node RewriteOneToManyNavigationProperty(RelProperty relProperty, List<RelationshipSet> relationshipSets, Node sourceRefNode)
		{
			Var relOpVar;
			Node relOpNode = this.RewriteFromOneNavigationProperty(relProperty, relationshipSets, sourceRefNode, out relOpVar);
			return base.m_command.BuildCollect(relOpNode, relOpVar);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00029CE4 File Offset: 0x00027EE4
		private Node RewriteOneToOneNavigationProperty(RelProperty relProperty, List<RelationshipSet> relationshipSets, Node sourceRefNode)
		{
			Var outputVar;
			Node node = this.RewriteFromOneNavigationProperty(relProperty, relationshipSets, sourceRefNode, out outputVar);
			node = base.VisitNode(node);
			return base.AddSubqueryToParentRelOp(outputVar, node);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00029D10 File Offset: 0x00027F10
		private Node RewriteFromOneNavigationProperty(RelProperty relProperty, List<RelationshipSet> relationshipSets, Node sourceRefNode, out Var outputVar)
		{
			PlanCompiler.Assert(relationshipSets.Count > 0, "expected at least one relationshipset here");
			PlanCompiler.Assert(relProperty.FromEnd.RelationshipMultiplicity != RelationshipMultiplicity.Many, "Expected source end multiplicity to be one. Found 'Many' instead " + ((relProperty != null) ? relProperty.ToString() : null));
			TypeUsage elementTypeUsage = TypeHelpers.GetElementTypeUsage(relProperty.ToEnd.TypeUsage);
			List<Node> list = new List<Node>(relationshipSets.Count);
			List<Var> list2 = new List<Var>(relationshipSets.Count);
			foreach (RelationshipSet relationshipSet in relationshipSets)
			{
				EntitySetBase entitySet = PreProcessor.FindTargetEntitySet(relationshipSet, relProperty.ToEnd);
				Var item2;
				Node item = this.BuildOfTypeTable(entitySet, elementTypeUsage, out item2);
				list.Add(item);
				list2.Add(item2);
			}
			Node arg;
			base.m_command.BuildUnionAllLadder(list, list2, out arg, out outputVar);
			RelProperty relProperty2 = new RelProperty(relProperty.Relationship, relProperty.ToEnd, relProperty.FromEnd);
			bool condition = base.m_command.IsRelPropertyReferenced(relProperty2);
			string str = "Unreferenced rel property? ";
			RelProperty relProperty3 = relProperty2;
			PlanCompiler.Assert(condition, str + ((relProperty3 != null) ? relProperty3.ToString() : null));
			Node arg2 = base.m_command.CreateNode(base.m_command.CreateRelPropertyOp(relProperty2), base.m_command.CreateNode(base.m_command.CreateVarRefOp(outputVar)));
			Node arg3 = base.m_command.BuildComparison(OpType.EQ, sourceRefNode, arg2);
			return base.m_command.CreateNode(base.m_command.CreateFilterOp(), arg, arg3);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00029EA4 File Offset: 0x000280A4
		private Node RewriteManyToManyNavigationProperty(RelProperty relProperty, List<RelationshipSet> relationshipSets, Node sourceRefNode)
		{
			PlanCompiler.Assert(relationshipSets.Count > 0, "expected at least one relationshipset here");
			PlanCompiler.Assert(relProperty.ToEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many && relProperty.FromEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many, "Expected target end multiplicity to be 'many'. Found " + ((relProperty != null) ? relProperty.ToString() : null) + "; multiplicity = " + relProperty.ToEnd.RelationshipMultiplicity.ToString());
			List<Node> list = new List<Node>(relationshipSets.Count);
			List<Var> list2 = new List<Var>(relationshipSets.Count * 2);
			foreach (RelationshipSet relSet in relationshipSets)
			{
				Var item2;
				Var item3;
				Node item = this.BuildJoinForNavProperty(relSet, relProperty.ToEnd, out item2, out item3);
				list.Add(item);
				list2.Add(item2);
				list2.Add(item3);
			}
			Node arg;
			IList<Var> list3;
			base.m_command.BuildUnionAllLadder(list, list2, out arg, out list3);
			Node arg2 = base.m_command.CreateNode(base.m_command.CreatePropertyOp(relProperty.FromEnd), base.m_command.CreateNode(base.m_command.CreateVarRefOp(list3[0])));
			Node arg3 = base.m_command.BuildComparison(OpType.EQ, sourceRefNode, arg2);
			Node inputNode = base.m_command.CreateNode(base.m_command.CreateFilterOp(), arg, arg3);
			Node relOpNode = base.m_command.BuildProject(inputNode, new Var[]
			{
				list3[1]
			}, new Node[0]);
			return base.m_command.BuildCollect(relOpNode, list3[1]);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0002A05C File Offset: 0x0002825C
		private Node RewriteNavigationProperty(NavigationProperty navProperty, Node sourceEntityNode, TypeUsage resultType)
		{
			RelProperty relProperty = new RelProperty(navProperty.RelationshipType, navProperty.FromEndMember, navProperty.ToEndMember);
			bool condition = base.m_command.IsRelPropertyReferenced(relProperty) || relProperty.ToEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many;
			string str = "Unreferenced rel property? ";
			RelProperty relProperty2 = relProperty;
			PlanCompiler.Assert(condition, str + ((relProperty2 != null) ? relProperty2.ToString() : null));
			if (relProperty.FromEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many && relProperty.ToEnd.RelationshipMultiplicity != RelationshipMultiplicity.Many)
			{
				return this.RewriteManyToOneNavigationProperty(relProperty, sourceEntityNode, resultType);
			}
			List<RelationshipSet> relationshipSets = this.GetRelationshipSets(relProperty.Relationship);
			if (relationshipSets.Count == 0)
			{
				if (relProperty.ToEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many)
				{
					return base.m_command.CreateNode(base.m_command.CreateNewMultisetOp(resultType));
				}
				return base.m_command.CreateNode(base.m_command.CreateNullOp(resultType));
			}
			else
			{
				Node sourceRefNode = base.m_command.CreateNode(base.m_command.CreateGetEntityRefOp(relProperty.FromEnd.TypeUsage), sourceEntityNode);
				if (relProperty.ToEnd.RelationshipMultiplicity != RelationshipMultiplicity.Many)
				{
					return this.RewriteOneToOneNavigationProperty(relProperty, relationshipSets, sourceRefNode);
				}
				if (relProperty.FromEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many)
				{
					return this.RewriteManyToManyNavigationProperty(relProperty, relationshipSets, sourceRefNode);
				}
				return this.RewriteOneToManyNavigationProperty(relProperty, relationshipSets, sourceRefNode);
			}
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0002A191 File Offset: 0x00028391
		protected override Node VisitScalarOpDefault(ScalarOp op, Node n)
		{
			this.VisitChildren(n);
			this.AddTypeReference(op.Type);
			return n;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0002A1A8 File Offset: 0x000283A8
		public override Node Visit(DerefOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
			Var var;
			Node node = this.RewriteDerefOp(n, op, out var);
			node = base.VisitNode(node);
			if (var != null)
			{
				node = base.AddSubqueryToParentRelOp(var, node);
			}
			return node;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0002A1E0 File Offset: 0x000283E0
		public override Node Visit(ElementOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
			Node child = n.Child0;
			ProjectOp projectOp = (ProjectOp)child.Op;
			PlanCompiler.Assert(projectOp.Outputs.Count == 1, "input to ElementOp has more than one output var?");
			Var first = projectOp.Outputs.First;
			return base.AddSubqueryToParentRelOp(first, child);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0002A237 File Offset: 0x00028437
		public override Node Visit(ExistsOp op, Node n)
		{
			this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.Normalization);
			return base.Visit(op, n);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0002A250 File Offset: 0x00028450
		public override Node Visit(FunctionOp op, Node n)
		{
			if (!op.Function.IsFunctionImport)
			{
				PlanCompiler.Assert(op.Function.EntitySet == null, "Entity type scope is not supported on functions that aren't mapped.");
				if (TypeSemantics.IsCollectionType(op.Type) || PlanCompilerUtil.IsCollectionAggregateFunction(op, n))
				{
					this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.NestPullup);
					this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.Normalization);
				}
				return base.Visit(op, n);
			}
			PlanCompiler.Assert(op.Function.IsComposableAttribute, "Cannot process a non-composable function inside query tree composition.");
			FunctionImportMapping functionImportMapping = null;
			if (!base.m_command.MetadataWorkspace.TryGetFunctionImportMapping(op.Function, out functionImportMapping))
			{
				throw EntityUtil.Metadata(Strings.EntityClient_UnmappedFunctionImport(op.Function.FullName));
			}
			PlanCompiler.Assert(functionImportMapping is FunctionImportMappingComposable, "Composable function import must have corresponding mapping.");
			FunctionImportMappingComposable functionImportMappingComposable = (FunctionImportMappingComposable)functionImportMapping;
			this.VisitChildren(n);
			Node node = functionImportMappingComposable.GetInternalTree(base.m_command, n.Children);
			if (op.Function.EntitySet != null)
			{
				this.m_entityTypeScopes.Push(op.Function.EntitySet);
				this.AddEntitySetReference(op.Function.EntitySet);
				PlanCompiler.Assert(functionImportMappingComposable.TvfKeys != null && functionImportMappingComposable.TvfKeys.Length != 0, "Function imports returning entities must have inferred keys.");
				if (!this.m_tvfResultKeys.ContainsKey(functionImportMappingComposable.TargetFunction))
				{
					this.m_tvfResultKeys.Add(functionImportMappingComposable.TargetFunction, functionImportMappingComposable.TvfKeys);
				}
			}
			node = base.VisitNode(node);
			if (op.Function.EntitySet != null)
			{
				EntitySet entitySet = this.m_entityTypeScopes.Pop();
				PlanCompiler.Assert(entitySet == op.Function.EntitySet, "m_entityTypeScopes stack is broken");
			}
			return node;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0002A3F0 File Offset: 0x000285F0
		public override Node Visit(CaseOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
			bool flag;
			if (PlanCompilerUtil.IsRowTypeCaseOpWithNullability(op, n, out flag))
			{
				this.m_typesNeedingNullSentinel.Add(op.Type.EdmType.Identity);
			}
			return n;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0002A42E File Offset: 0x0002862E
		public override Node Visit(ConditionalOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
			this.ProcessConditionalOp(op, n);
			return n;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0002A444 File Offset: 0x00028644
		private void ProcessConditionalOp(ConditionalOp op, Node n)
		{
			if ((op.OpType == OpType.IsNull && TypeSemantics.IsRowType(n.Child0.Op.Type)) || TypeSemantics.IsComplexType(n.Child0.Op.Type))
			{
				StructuredTypeNullabilityAnalyzer.MarkAsNeedingNullSentinel(this.m_typesNeedingNullSentinel, n.Child0.Op.Type);
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0002A4A4 File Offset: 0x000286A4
		private void ValidateNavPropertyOp(PropertyOp op, Node n)
		{
			NavigationProperty navigationProperty = (NavigationProperty)op.PropertyInfo;
			TypeUsage typeUsage = navigationProperty.ToEndMember.TypeUsage;
			if (TypeSemantics.IsReferenceType(typeUsage))
			{
				typeUsage = TypeHelpers.GetElementTypeUsage(typeUsage);
			}
			if (navigationProperty.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
			{
				typeUsage = TypeUsage.Create(typeUsage.EdmType.GetCollectionType());
			}
			if (!TypeSemantics.IsStructurallyEqualOrPromotableTo(typeUsage, op.Type))
			{
				throw EntityUtil.Metadata(Strings.EntityClient_IncompatibleNavigationPropertyResult(navigationProperty.DeclaringType.FullName, navigationProperty.Name));
			}
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0002A524 File Offset: 0x00028724
		private Node VisitNavPropertyOp(PropertyOp op, Node n)
		{
			this.ValidateNavPropertyOp(op, n);
			if (!PreProcessor.IsNavigationPropertyOverVarRef(n.Child0))
			{
				this.VisitScalarOpDefault(op, n);
			}
			NavigationProperty navProperty = (NavigationProperty)op.PropertyInfo;
			Node n2 = this.RewriteNavigationProperty(navProperty, n.Child0, op.Type);
			return base.VisitNode(n2);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0002A57C File Offset: 0x0002877C
		private static bool IsNavigationPropertyOverVarRef(Node n)
		{
			if (n.Op.OpType != OpType.Property || !Helper.IsNavigationProperty(((PropertyOp)n.Op).PropertyInfo))
			{
				return false;
			}
			Node child = n.Child0;
			if (child.Op.OpType == OpType.SoftCast)
			{
				child = child.Child0;
			}
			return child.Op.OpType == OpType.VarRef;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0002A5DC File Offset: 0x000287DC
		public override Node Visit(PropertyOp op, Node n)
		{
			Node result;
			if (Helper.IsNavigationProperty(op.PropertyInfo))
			{
				result = this.VisitNavPropertyOp(op, n);
			}
			else
			{
				result = this.VisitScalarOpDefault(op, n);
			}
			return result;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0002A60B File Offset: 0x0002880B
		public override Node Visit(RefOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
			this.AddEntitySetReference(op.EntitySet);
			return n;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0002A623 File Offset: 0x00028823
		public override Node Visit(TreatOp op, Node n)
		{
			n = base.Visit(op, n);
			if (this.CanRewriteTypeTest(op.Type.EdmType, n.Child0.Op.Type.EdmType))
			{
				return n.Child0;
			}
			return n;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0002A660 File Offset: 0x00028860
		public override Node Visit(IsOfOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
			this.AddTypeReference(op.IsOfType);
			if (this.CanRewriteTypeTest(op.IsOfType.EdmType, n.Child0.Op.Type.EdmType))
			{
				n = this.RewriteIsOfAsIsNull(op, n);
			}
			if (op.IsOfOnly && op.IsOfType.EdmType.Abstract)
			{
				this.m_suppressDiscriminatorMaps = true;
			}
			return n;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0002A6D8 File Offset: 0x000288D8
		private bool CanRewriteTypeTest(EdmType testType, EdmType argumentType)
		{
			if (!testType.EdmEquals(argumentType))
			{
				return false;
			}
			if (testType.BaseType != null)
			{
				return false;
			}
			int num = 0;
			foreach (EdmType edmType in MetadataHelper.GetTypeAndSubtypesOf(testType, base.m_command.MetadataWorkspace, true))
			{
				num++;
				if (2 == num)
				{
					break;
				}
			}
			return 1 == num;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0002A750 File Offset: 0x00028950
		private Node RewriteIsOfAsIsNull(IsOfOp op, Node n)
		{
			ConditionalOp op2 = base.m_command.CreateConditionalOp(OpType.IsNull);
			Node node = base.m_command.CreateNode(op2, n.Child0);
			this.ProcessConditionalOp(op2, node);
			ConditionalOp op3 = base.m_command.CreateConditionalOp(OpType.Not);
			Node arg = base.m_command.CreateNode(op3, node);
			ConstantBaseOp op4 = base.m_command.CreateConstantOp(op.Type, true);
			Node arg2 = base.m_command.CreateNode(op4);
			NullOp op5 = base.m_command.CreateNullOp(op.Type);
			Node arg3 = base.m_command.CreateNode(op5);
			CaseOp op6 = base.m_command.CreateCaseOp(op.Type);
			Node arg4 = base.m_command.CreateNode(op6, arg, arg2, arg3);
			ComparisonOp op7 = base.m_command.CreateComparisonOp(OpType.EQ);
			return base.m_command.CreateNode(op7, arg4, arg2);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0002A838 File Offset: 0x00028A38
		public override Node Visit(NavigateOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
			Var var;
			Node node = this.RewriteNavigateOp(n, op, out var);
			node = base.VisitNode(node);
			if (var != null)
			{
				node = base.AddSubqueryToParentRelOp(var, node);
			}
			return node;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0002A86E File Offset: 0x00028A6E
		private EntitySet GetCurrentEntityTypeScope()
		{
			if (this.m_entityTypeScopes.Count == 0)
			{
				return null;
			}
			return this.m_entityTypeScopes.Peek();
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0002A88C File Offset: 0x00028A8C
		private RelationshipSet FindRelationshipSet(EntitySetBase entitySet, RelProperty relProperty)
		{
			foreach (EntitySetBase entitySetBase in entitySet.EntityContainer.BaseEntitySets)
			{
				AssociationSet associationSet = entitySetBase as AssociationSet;
				if (associationSet != null && associationSet.ElementType.EdmEquals(relProperty.Relationship) && associationSet.AssociationSetEnds[relProperty.FromEnd.Identity].EntitySet.EdmEquals(entitySet))
				{
					return associationSet;
				}
			}
			return null;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0002A924 File Offset: 0x00028B24
		private int FindPosition(EdmType type, EdmMember member)
		{
			int num = 0;
			foreach (object obj in TypeHelpers.GetAllStructuralMembers(type))
			{
				EdmMember edmMember = (EdmMember)obj;
				if (edmMember.EdmEquals(member))
				{
					return num;
				}
				num++;
			}
			PlanCompiler.Assert(false, "Could not find property " + ((member != null) ? member.ToString() : null) + " in type " + type.Name);
			return -1;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0002A9B8 File Offset: 0x00028BB8
		private Node BuildKeyExpressionForNewEntityOp(Op op, Node n)
		{
			PlanCompiler.Assert(op.OpType == OpType.NewEntity || op.OpType == OpType.DiscriminatedNewEntity, "BuildKeyExpression: Unexpected OpType:" + op.OpType.ToString());
			int num = (op.OpType == OpType.DiscriminatedNewEntity) ? 1 : 0;
			EntityTypeBase entityTypeBase = (EntityTypeBase)op.Type.EdmType;
			List<Node> list = new List<Node>();
			List<KeyValuePair<string, TypeUsage>> list2 = new List<KeyValuePair<string, TypeUsage>>();
			foreach (EdmMember edmMember in entityTypeBase.KeyMembers)
			{
				int num2 = this.FindPosition(entityTypeBase, edmMember) + num;
				PlanCompiler.Assert(n.Children.Count > num2, "invalid position " + num2.ToString() + "; total count = " + n.Children.Count.ToString());
				list.Add(n.Children[num2]);
				list2.Add(new KeyValuePair<string, TypeUsage>(edmMember.Name, edmMember.TypeUsage));
			}
			TypeUsage type = TypeHelpers.CreateRowTypeUsage(list2, true);
			NewRecordOp op2 = base.m_command.CreateNewRecordOp(type);
			return base.m_command.CreateNode(op2, list);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0002AB14 File Offset: 0x00028D14
		private Node BuildRelPropertyExpression(EntitySetBase entitySet, RelProperty relProperty, Node keyExpr)
		{
			keyExpr = OpCopier.Copy(base.m_command, keyExpr);
			RelationshipSet relationshipSet = this.FindRelationshipSet(entitySet, relProperty);
			if (relationshipSet == null)
			{
				return base.m_command.CreateNode(base.m_command.CreateNullOp(relProperty.ToEnd.TypeUsage));
			}
			ScanTableOp scanTableOp = base.m_command.CreateScanTableOp(Command.CreateTableDefinition(relationshipSet));
			bool condition = scanTableOp.Table.Columns.Count == 1;
			string str = "Unexpected column count for table:";
			EntitySetBase extent = scanTableOp.Table.TableMetadata.Extent;
			PlanCompiler.Assert(condition, str + ((extent != null) ? extent.ToString() : null) + "=" + scanTableOp.Table.Columns.Count.ToString());
			Var var = scanTableOp.Table.Columns[0];
			Node arg = base.m_command.CreateNode(scanTableOp);
			Node arg2 = base.m_command.CreateNode(base.m_command.CreatePropertyOp(relProperty.FromEnd), base.m_command.CreateNode(base.m_command.CreateVarRefOp(var)));
			Node arg3 = base.m_command.BuildComparison(OpType.EQ, keyExpr, base.m_command.CreateNode(base.m_command.CreateGetRefKeyOp(keyExpr.Op.Type), arg2));
			Node n = base.m_command.CreateNode(base.m_command.CreateFilterOp(), arg, arg3);
			Node node = base.VisitNode(n);
			node = base.AddSubqueryToParentRelOp(var, node);
			return base.m_command.CreateNode(base.m_command.CreatePropertyOp(relProperty.ToEnd), node);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0002ACA4 File Offset: 0x00028EA4
		private IEnumerable<Node> BuildAllRelPropertyExpressions(EntitySetBase entitySet, List<RelProperty> relPropertyList, Dictionary<RelProperty, Node> prebuiltExpressions, Node keyExpr)
		{
			foreach (RelProperty relProperty in relPropertyList)
			{
				Node node;
				if (!prebuiltExpressions.TryGetValue(relProperty, out node))
				{
					node = this.BuildRelPropertyExpression(entitySet, relProperty, keyExpr);
				}
				yield return node;
			}
			List<RelProperty>.Enumerator enumerator = default(List<RelProperty>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0002ACD4 File Offset: 0x00028ED4
		public override Node Visit(NewEntityOp op, Node n)
		{
			if (op.Scoped || op.Type.EdmType.BuiltInTypeKind != BuiltInTypeKind.EntityType)
			{
				return base.Visit(op, n);
			}
			EntityType entityType = (EntityType)op.Type.EdmType;
			EntitySet currentEntityTypeScope = this.GetCurrentEntityTypeScope();
			List<RelProperty> list;
			List<Node> list2;
			if (currentEntityTypeScope == null)
			{
				this.m_freeFloatingEntityConstructorTypes.Add(entityType);
				PlanCompiler.Assert(op.RelationshipProperties == null || op.RelationshipProperties.Count == 0, "Related Entities cannot be specified for Entity constructors that are not part of the Query Mapping View for an Entity Set.");
				this.VisitScalarOpDefault(op, n);
				list = op.RelationshipProperties;
				list2 = n.Children;
			}
			else
			{
				list = new List<RelProperty>(this.m_relPropertyHelper.GetRelProperties(entityType));
				int num = op.RelationshipProperties.Count - 1;
				List<RelProperty> list3 = new List<RelProperty>(op.RelationshipProperties);
				int i = n.Children.Count - 1;
				while (i >= entityType.Properties.Count)
				{
					if (!list.Contains(op.RelationshipProperties[num]))
					{
						n.Children.RemoveAt(i);
						list3.RemoveAt(num);
					}
					i--;
					num--;
				}
				this.VisitScalarOpDefault(op, n);
				Node keyExpr = this.BuildKeyExpressionForNewEntityOp(op, n);
				Dictionary<RelProperty, Node> dictionary = new Dictionary<RelProperty, Node>();
				num = 0;
				int j = entityType.Properties.Count;
				while (j < n.Children.Count)
				{
					dictionary[list3[num]] = n.Children[j];
					j++;
					num++;
				}
				list2 = new List<Node>();
				for (int k = 0; k < entityType.Properties.Count; k++)
				{
					list2.Add(n.Children[k]);
				}
				foreach (Node item in this.BuildAllRelPropertyExpressions(currentEntityTypeScope, list, dictionary, keyExpr))
				{
					list2.Add(item);
				}
			}
			Op op2 = base.m_command.CreateScopedNewEntityOp(op.Type, list, currentEntityTypeScope);
			return base.m_command.CreateNode(op2, list2);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0002AF00 File Offset: 0x00029100
		public override Node Visit(DiscriminatedNewEntityOp op, Node n)
		{
			HashSet<RelProperty> hashSet = new HashSet<RelProperty>();
			List<RelProperty> list = new List<RelProperty>();
			foreach (KeyValuePair<object, EntityType> keyValuePair in op.DiscriminatorMap.TypeMap)
			{
				EntityTypeBase value = keyValuePair.Value;
				this.AddTypeReference(TypeUsage.Create(value));
				foreach (RelProperty item in this.m_relPropertyHelper.GetRelProperties(value))
				{
					hashSet.Add(item);
				}
			}
			list = new List<RelProperty>(hashSet);
			this.VisitScalarOpDefault(op, n);
			Node keyExpr = this.BuildKeyExpressionForNewEntityOp(op, n);
			List<Node> list2 = new List<Node>();
			int num = n.Children.Count - op.RelationshipProperties.Count;
			for (int i = 0; i < num; i++)
			{
				list2.Add(n.Children[i]);
			}
			Dictionary<RelProperty, Node> dictionary = new Dictionary<RelProperty, Node>();
			int j = num;
			int num2 = 0;
			while (j < n.Children.Count)
			{
				dictionary[op.RelationshipProperties[num2]] = n.Children[j];
				j++;
				num2++;
			}
			foreach (Node item2 in this.BuildAllRelPropertyExpressions(op.EntitySet, list, dictionary, keyExpr))
			{
				list2.Add(item2);
			}
			Op op2 = base.m_command.CreateDiscriminatedNewEntityOp(op.Type, op.DiscriminatorMap, op.EntitySet, list);
			return base.m_command.CreateNode(op2, list2);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0002B0E8 File Offset: 0x000292E8
		public override Node Visit(NewMultisetOp op, Node n)
		{
			Node node = null;
			Var var = null;
			CollectionType edmType = TypeHelpers.GetEdmType<CollectionType>(op.Type);
			if (!n.HasChild0)
			{
				Node arg = base.m_command.CreateNode(base.m_command.CreateSingleRowTableOp());
				Node input = base.m_command.CreateNode(base.m_command.CreateFilterOp(), arg, base.m_command.CreateNode(base.m_command.CreateFalseOp()));
				Node computedExpression = base.m_command.CreateNode(base.m_command.CreateNullOp(edmType.TypeUsage));
				Var var2;
				Node node2 = base.m_command.BuildProject(input, computedExpression, out var2);
				node = node2;
				var = var2;
			}
			else if (n.Children.Count == 1 || this.AreAllConstantsOrNulls(n.Children))
			{
				List<Node> list = new List<Node>();
				List<Var> list2 = new List<Var>();
				foreach (Node computedExpression2 in n.Children)
				{
					Node input2 = base.m_command.CreateNode(base.m_command.CreateSingleRowTableOp());
					Var item2;
					Node item = base.m_command.BuildProject(input2, computedExpression2, out item2);
					list.Add(item);
					list2.Add(item2);
				}
				base.m_command.BuildUnionAllLadder(list, list2, out node, out var);
			}
			else
			{
				List<Node> list3 = new List<Node>();
				List<Var> list4 = new List<Var>();
				for (int i = 0; i < n.Children.Count; i++)
				{
					Node input3 = base.m_command.CreateNode(base.m_command.CreateSingleRowTableOp());
					Node computedExpression3 = base.m_command.CreateNode(base.m_command.CreateInternalConstantOp(base.m_command.IntegerType, i));
					Var item4;
					Node item3 = base.m_command.BuildProject(input3, computedExpression3, out item4);
					list3.Add(item3);
					list4.Add(item4);
				}
				base.m_command.BuildUnionAllLadder(list3, list4, out node, out var);
				List<Node> list5 = new List<Node>(n.Children.Count * 2 + 1);
				for (int j = 0; j < n.Children.Count; j++)
				{
					if (j != n.Children.Count - 1)
					{
						ComparisonOp op2 = base.m_command.CreateComparisonOp(OpType.EQ);
						Node item5 = base.m_command.CreateNode(op2, base.m_command.CreateNode(base.m_command.CreateVarRefOp(var)), base.m_command.CreateNode(base.m_command.CreateConstantOp(base.m_command.IntegerType, j)));
						list5.Add(item5);
					}
					list5.Add(n.Children[j]);
				}
				Node computedExpression4 = base.m_command.CreateNode(base.m_command.CreateCaseOp(edmType.TypeUsage), list5);
				node = base.m_command.BuildProject(node, computedExpression4, out var);
			}
			PhysicalProjectOp op3 = base.m_command.CreatePhysicalProjectOp(var);
			Node arg2 = base.m_command.CreateNode(op3, node);
			CollectOp op4 = base.m_command.CreateCollectOp(op.Type);
			Node n2 = base.m_command.CreateNode(op4, arg2);
			return base.VisitNode(n2);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0002B434 File Offset: 0x00029634
		private bool AreAllConstantsOrNulls(List<Node> nodes)
		{
			foreach (Node node in nodes)
			{
				if (node.Op.OpType != OpType.Constant && node.Op.OpType != OpType.Null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0002B4A0 File Offset: 0x000296A0
		public override Node Visit(CollectOp op, Node n)
		{
			this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.NestPullup);
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0002B4B8 File Offset: 0x000296B8
		private void HandleTableOpMetadata(ScanTableBaseOp op)
		{
			EntitySet entitySet = op.Table.TableMetadata.Extent as EntitySet;
			if (entitySet != null)
			{
				this.AddEntitySetReference(entitySet);
			}
			TypeUsage type = TypeUsage.Create(op.Table.TableMetadata.Extent.ElementType);
			this.AddTypeReference(type);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0002B508 File Offset: 0x00029708
		private Node ProcessScanTable(Node scanTableNode, ScanTableOp scanTableOp, ref IsOfOp typeFilter)
		{
			this.HandleTableOpMetadata(scanTableOp);
			PlanCompiler.Assert(scanTableOp.Table.TableMetadata.Extent != null, "ScanTableOp must reference a table with an extent");
			if (scanTableOp.Table.TableMetadata.Extent.EntityContainer.DataSpace == DataSpace.SSpace)
			{
				return scanTableNode;
			}
			Node n = this.ExpandView(scanTableNode, scanTableOp, ref typeFilter);
			return base.VisitNode(n);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0002B570 File Offset: 0x00029770
		public override Node Visit(ScanTableOp op, Node n)
		{
			IsOfOp isOfOp = null;
			return this.ProcessScanTable(n, op, ref isOfOp);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0002B58C File Offset: 0x0002978C
		public override Node Visit(ScanViewOp op, Node n)
		{
			bool flag = false;
			if (op.Table.TableMetadata.Extent.BuiltInTypeKind == BuiltInTypeKind.EntitySet)
			{
				this.m_entityTypeScopes.Push((EntitySet)op.Table.TableMetadata.Extent);
				flag = true;
			}
			this.HandleTableOpMetadata(op);
			this.VisitRelOpDefault(op, n);
			if (flag)
			{
				EntitySet entitySet = this.m_entityTypeScopes.Pop();
				PlanCompiler.Assert(entitySet == op.Table.TableMetadata.Extent, "m_entityTypeScopes stack is broken");
			}
			return n;
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0002B613 File Offset: 0x00029813
		protected override Node VisitJoinOp(JoinBaseOp op, Node n)
		{
			if (op.OpType == OpType.InnerJoin || op.OpType == OpType.LeftOuterJoin)
			{
				this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.JoinElimination);
			}
			if (base.ProcessJoinOp(op, n))
			{
				this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.Normalization);
			}
			return n;
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0002B64C File Offset: 0x0002984C
		protected override Node VisitApplyOp(ApplyBaseOp op, Node n)
		{
			this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.JoinElimination);
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0002B664 File Offset: 0x00029864
		private bool IsSortUnnecessary()
		{
			Node node = this.m_ancestors.Peek();
			PlanCompiler.Assert(node != null, "unexpected SortOp as root node?");
			return node.Op.OpType != OpType.PhysicalProject;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0002B69D File Offset: 0x0002989D
		public override Node Visit(SortOp op, Node n)
		{
			if (this.IsSortUnnecessary())
			{
				return base.VisitNode(n.Child0);
			}
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0002B6BC File Offset: 0x000298BC
		private bool IsOfTypeOverScanTable(Node n, out IsOfOp typeFilter)
		{
			typeFilter = null;
			IsOfOp isOfOp = n.Child1.Op as IsOfOp;
			if (isOfOp == null)
			{
				return false;
			}
			ScanTableOp scanTableOp = n.Child0.Op as ScanTableOp;
			if (scanTableOp == null || scanTableOp.Table.Columns.Count != 1)
			{
				return false;
			}
			VarRefOp varRefOp = n.Child1.Child0.Op as VarRefOp;
			if (varRefOp == null || varRefOp.Var != scanTableOp.Table.Columns[0])
			{
				return false;
			}
			typeFilter = isOfOp;
			return true;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0002B744 File Offset: 0x00029944
		public override Node Visit(FilterOp op, Node n)
		{
			IsOfOp isOfOp;
			if (this.IsOfTypeOverScanTable(n, out isOfOp))
			{
				Node node = this.ProcessScanTable(n.Child0, (ScanTableOp)n.Child0.Op, ref isOfOp);
				if (isOfOp != null)
				{
					n.Child1 = base.VisitNode(n.Child1);
					n.Child0 = node;
					node = n;
				}
				return node;
			}
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0002B7A4 File Offset: 0x000299A4
		public override Node Visit(ProjectOp op, Node n)
		{
			PlanCompiler.Assert(n.HasChild0, "projectOp without input?");
			if (OpType.Sort == n.Child0.Op.OpType || OpType.ConstrainedSort == n.Child0.Op.OpType)
			{
				SortBaseOp sortBaseOp = (SortBaseOp)n.Child0.Op;
				if (sortBaseOp.Keys.Count > 0)
				{
					IList<Node> list = new List<Node>();
					list.Add(n);
					for (int i = 1; i < n.Child0.Children.Count; i++)
					{
						list.Add(n.Child0.Children[i]);
					}
					n.Child0 = n.Child0.Child0;
					foreach (SortKey sortKey in sortBaseOp.Keys)
					{
						op.Outputs.Set(sortKey.Var);
					}
					return base.VisitNode(base.m_command.CreateNode(sortBaseOp, list));
				}
			}
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0002B8D0 File Offset: 0x00029AD0
		public override Node Visit(GroupByIntoOp op, Node n)
		{
			this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.AggregatePushdown);
			return base.Visit(op, n);
		}

		// Token: 0x040007E7 RID: 2023
		private readonly Stack<EntitySet> m_entityTypeScopes = new Stack<EntitySet>();

		// Token: 0x040007E8 RID: 2024
		private readonly HashSet<EntityContainer> m_referencedEntityContainers = new HashSet<EntityContainer>();

		// Token: 0x040007E9 RID: 2025
		private readonly HashSet<EntitySet> m_referencedEntitySets = new HashSet<EntitySet>();

		// Token: 0x040007EA RID: 2026
		private readonly HashSet<TypeUsage> m_referencedTypes = new HashSet<TypeUsage>();

		// Token: 0x040007EB RID: 2027
		private readonly HashSet<EntityType> m_freeFloatingEntityConstructorTypes = new HashSet<EntityType>();

		// Token: 0x040007EC RID: 2028
		private readonly HashSet<string> m_typesNeedingNullSentinel = new HashSet<string>();

		// Token: 0x040007ED RID: 2029
		private readonly Dictionary<EdmFunction, EdmProperty[]> m_tvfResultKeys = new Dictionary<EdmFunction, EdmProperty[]>();

		// Token: 0x040007EE RID: 2030
		private RelPropertyHelper m_relPropertyHelper;

		// Token: 0x040007EF RID: 2031
		private bool m_suppressDiscriminatorMaps;

		// Token: 0x040007F0 RID: 2032
		private readonly Dictionary<EntitySetBase, DiscriminatorMapInfo> m_discriminatorMaps = new Dictionary<EntitySetBase, DiscriminatorMapInfo>();
	}
}
