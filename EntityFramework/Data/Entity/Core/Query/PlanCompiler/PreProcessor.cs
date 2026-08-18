using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Mapping.ViewGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200068F RID: 1679
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal class PreProcessor : SubqueryTrackingVisitor
	{
		// Token: 0x0600422D RID: 16941 RVA: 0x001382D4 File Offset: 0x001364D4
		private PreProcessor(PlanCompiler planCompilerState) : base(planCompilerState)
		{
			this.m_relPropertyHelper = new RelPropertyHelper(base.m_command.MetadataWorkspace, base.m_command.ReferencedRelProperties);
		}

		// Token: 0x0600422E RID: 16942 RVA: 0x0013836C File Offset: 0x0013656C
		internal static void Process(PlanCompiler planCompilerState, out StructuredTypeInfo typeInfo, out Dictionary<EdmFunction, EdmProperty[]> tvfResultKeys)
		{
			PreProcessor preProcessor = new PreProcessor(planCompilerState);
			preProcessor.Process(out tvfResultKeys);
			StructuredTypeInfo.Process(planCompilerState.Command, preProcessor.m_referencedTypes, preProcessor.m_referencedEntitySets, preProcessor.m_freeFloatingEntityConstructorTypes, preProcessor.m_suppressDiscriminatorMaps ? null : preProcessor.m_discriminatorMaps, preProcessor.m_relPropertyHelper, preProcessor.m_typesNeedingNullSentinel, out typeInfo);
		}

		// Token: 0x0600422F RID: 16943 RVA: 0x001383C4 File Offset: 0x001365C4
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

		// Token: 0x06004230 RID: 16944 RVA: 0x00138488 File Offset: 0x00136688
		private void AddEntitySetReference(EntitySet entitySet)
		{
			this.m_referencedEntitySets.Add(entitySet);
			if (!this.m_referencedEntityContainers.Contains(entitySet.EntityContainer))
			{
				this.m_referencedEntityContainers.Add(entitySet.EntityContainer);
			}
		}

		// Token: 0x06004231 RID: 16945 RVA: 0x001384BC File Offset: 0x001366BC
		private void AddTypeReference(TypeUsage type)
		{
			if (TypeUtils.IsStructuredType(type) || TypeUtils.IsCollectionType(type) || TypeUtils.IsEnumerationType(type))
			{
				this.m_referencedTypes.Add(type);
			}
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x001384E4 File Offset: 0x001366E4
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

		// Token: 0x06004233 RID: 16947 RVA: 0x00138594 File Offset: 0x00136794
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

		// Token: 0x06004234 RID: 16948 RVA: 0x00138674 File Offset: 0x00136874
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "EntitySet")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ExpandView")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Common.Utils.TreeNode.#ctor(System.String,System.Data.Entity.Core.Common.Utils.TreeNode[])")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ScanTableOp")]
		private Node ExpandView(ScanTableOp scanTableOp, ref IsOfOp typeFilter)
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

		// Token: 0x06004235 RID: 16949 RVA: 0x001387B0 File Offset: 0x001369B0
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

		// Token: 0x06004236 RID: 16950 RVA: 0x00138834 File Offset: 0x00136A34
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "rel")]
		private Node RewriteNavigateOp(Node navigateOpNode, NavigateOp navigateOp, out Var outputVar)
		{
			outputVar = null;
			if (!Helper.IsAssociationType(navigateOp.Relationship))
			{
				throw new NotSupportedException(Strings.Cqt_RelNav_NoCompositions);
			}
			if (navigateOpNode.Child0.Op.OpType == OpType.GetEntityRef && (navigateOp.ToEnd.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne || navigateOp.ToEnd.RelationshipMultiplicity == RelationshipMultiplicity.One))
			{
				PlanCompiler.Assert(base.m_command.IsRelPropertyReferenced(navigateOp.RelProperty), "Unreferenced rel property? " + navigateOp.RelProperty);
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
				Node arg3 = base.m_command.BuildComparison(OpType.EQ, navigateOpNode.Child0, arg2, true);
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

		// Token: 0x06004237 RID: 16951 RVA: 0x00138AC4 File Offset: 0x00136CC4
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

		// Token: 0x06004238 RID: 16952 RVA: 0x00138B38 File Offset: 0x00136D38
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
			Node arg3 = base.m_command.BuildComparison(OpType.EQ, derefOpNode.Child0, arg2, true);
			Node result = base.m_command.CreateNode(base.m_command.CreateFilterOp(), arg, arg3);
			outputVar = var;
			return result;
		}

		// Token: 0x06004239 RID: 16953 RVA: 0x00138C68 File Offset: 0x00136E68
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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
			PlanCompiler.Assert(entitySetBase != null, string.Concat(new object[]
			{
				"Could not find entity set for relationship set ",
				relationshipSet,
				";association end ",
				targetEnd
			}));
			return entitySetBase;
		}

		// Token: 0x0600423A RID: 16954 RVA: 0x00138D10 File Offset: 0x00136F10
		private Node BuildJoinForNavProperty(RelationshipSet relSet, RelationshipEndMember end, out Var rsVar, out Var esVar)
		{
			EntitySetBase entitySet = PreProcessor.FindTargetEntitySet(relSet, end);
			Node arg = this.BuildOfTypeTable(relSet, null, out rsVar);
			Node arg2 = this.BuildOfTypeTable(entitySet, TypeHelpers.GetElementTypeUsage(end.TypeUsage), out esVar);
			Node arg3 = base.m_command.BuildComparison(OpType.EQ, base.m_command.CreateNode(base.m_command.CreateGetEntityRefOp(end.TypeUsage), base.m_command.CreateNode(base.m_command.CreateVarRefOp(esVar))), base.m_command.CreateNode(base.m_command.CreatePropertyOp(end), base.m_command.CreateNode(base.m_command.CreateVarRefOp(rsVar))), true);
			return base.m_command.CreateNode(base.m_command.CreateInnerJoinOp(), arg, arg2, arg3);
		}

		// Token: 0x0600423B RID: 16955 RVA: 0x00138DD4 File Offset: 0x00136FD4
		private Node RewriteManyToOneNavigationProperty(RelProperty relProperty, Node sourceEntityNode, TypeUsage resultType)
		{
			RelPropertyOp op = base.m_command.CreateRelPropertyOp(relProperty);
			Node arg = base.m_command.CreateNode(op, sourceEntityNode);
			DerefOp op2 = base.m_command.CreateDerefOp(resultType);
			return base.m_command.CreateNode(op2, arg);
		}

		// Token: 0x0600423C RID: 16956 RVA: 0x00138E18 File Offset: 0x00137018
		private Node RewriteOneToManyNavigationProperty(RelProperty relProperty, List<RelationshipSet> relationshipSets, Node sourceRefNode)
		{
			Var relOpVar;
			Node relOpNode = this.RewriteFromOneNavigationProperty(relProperty, relationshipSets, sourceRefNode, out relOpVar);
			return base.m_command.BuildCollect(relOpNode, relOpVar);
		}

		// Token: 0x0600423D RID: 16957 RVA: 0x00138E40 File Offset: 0x00137040
		private Node RewriteOneToOneNavigationProperty(RelProperty relProperty, List<RelationshipSet> relationshipSets, Node sourceRefNode)
		{
			Var outputVar;
			Node node = this.RewriteFromOneNavigationProperty(relProperty, relationshipSets, sourceRefNode, out outputVar);
			node = base.VisitNode(node);
			return base.AddSubqueryToParentRelOp(outputVar, node);
		}

		// Token: 0x0600423E RID: 16958 RVA: 0x00138E6C File Offset: 0x0013706C
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "rel")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private Node RewriteFromOneNavigationProperty(RelProperty relProperty, List<RelationshipSet> relationshipSets, Node sourceRefNode, out Var outputVar)
		{
			PlanCompiler.Assert(relationshipSets.Count > 0, "expected at least one relationship set here");
			PlanCompiler.Assert(relProperty.FromEnd.RelationshipMultiplicity != RelationshipMultiplicity.Many, "Expected source end multiplicity to be one. Found 'Many' instead " + relProperty);
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
			PlanCompiler.Assert(base.m_command.IsRelPropertyReferenced(relProperty2), "Unreferenced rel property? " + relProperty2);
			Node arg2 = base.m_command.CreateNode(base.m_command.CreateRelPropertyOp(relProperty2), base.m_command.CreateNode(base.m_command.CreateVarRefOp(outputVar)));
			Node arg3 = base.m_command.BuildComparison(OpType.EQ, sourceRefNode, arg2, true);
			return base.m_command.CreateNode(base.m_command.CreateFilterOp(), arg, arg3);
		}

		// Token: 0x0600423F RID: 16959 RVA: 0x00138FE8 File Offset: 0x001371E8
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private Node RewriteManyToManyNavigationProperty(RelProperty relProperty, List<RelationshipSet> relationshipSets, Node sourceRefNode)
		{
			PlanCompiler.Assert(relationshipSets.Count > 0, "expected at least one relationship set here");
			PlanCompiler.Assert(relProperty.ToEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many && relProperty.FromEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many, string.Concat(new object[]
			{
				"Expected target end multiplicity to be 'many'. Found ",
				relProperty,
				"; multiplicity = ",
				relProperty.ToEnd.RelationshipMultiplicity
			}));
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
			Node arg3 = base.m_command.BuildComparison(OpType.EQ, sourceRefNode, arg2, true);
			Node inputNode = base.m_command.CreateNode(base.m_command.CreateFilterOp(), arg, arg3);
			Node relOpNode = base.m_command.BuildProject(inputNode, new Var[]
			{
				list3[1]
			}, new Node[0]);
			return base.m_command.BuildCollect(relOpNode, list3[1]);
		}

		// Token: 0x06004240 RID: 16960 RVA: 0x001391A8 File Offset: 0x001373A8
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "rel")]
		private Node RewriteNavigationProperty(NavigationProperty navProperty, Node sourceEntityNode, TypeUsage resultType)
		{
			RelProperty relProperty = new RelProperty(navProperty.RelationshipType, navProperty.FromEndMember, navProperty.ToEndMember);
			PlanCompiler.Assert(base.m_command.IsRelPropertyReferenced(relProperty) || relProperty.ToEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many, "Unreferenced rel property? " + relProperty);
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

		// Token: 0x06004241 RID: 16961 RVA: 0x001392D1 File Offset: 0x001374D1
		protected override Node VisitScalarOpDefault(ScalarOp op, Node n)
		{
			this.VisitChildren(n);
			this.AddTypeReference(op.Type);
			return n;
		}

		// Token: 0x06004242 RID: 16962 RVA: 0x001392E8 File Offset: 0x001374E8
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

		// Token: 0x06004243 RID: 16963 RVA: 0x00139320 File Offset: 0x00137520
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ElementOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		public override Node Visit(ElementOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
			Node child = n.Child0;
			ProjectOp projectOp = (ProjectOp)child.Op;
			PlanCompiler.Assert(projectOp.Outputs.Count == 1, "input to ElementOp has more than one output var?");
			Var first = projectOp.Outputs.First;
			return base.AddSubqueryToParentRelOp(first, child);
		}

		// Token: 0x06004244 RID: 16964 RVA: 0x00139377 File Offset: 0x00137577
		public override Node Visit(ExistsOp op, Node n)
		{
			this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.Normalization);
			return base.Visit(op, n);
		}

		// Token: 0x06004245 RID: 16965 RVA: 0x00139390 File Offset: 0x00137590
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "mentityTypeScopes")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
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
				throw new MetadataException(Strings.EntityClient_UnmappedFunctionImport(op.Function.FullName));
			}
			PlanCompiler.Assert(functionImportMapping is FunctionImportMappingComposable, "Composable function import must have corresponding mapping.");
			FunctionImportMappingComposable functionImportMappingComposable = (FunctionImportMappingComposable)functionImportMapping;
			this.VisitChildren(n);
			Node node = functionImportMappingComposable.GetInternalTree(base.m_command, n.Children);
			if (op.Function.EntitySet != null)
			{
				this.m_entityTypeScopes.Push(op.Function.EntitySet);
				this.AddEntitySetReference(op.Function.EntitySet);
				PlanCompiler.Assert(functionImportMappingComposable.TvfKeys != null && functionImportMappingComposable.TvfKeys.Length > 0, "Function imports returning entities must have inferred keys.");
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

		// Token: 0x06004246 RID: 16966 RVA: 0x00139530 File Offset: 0x00137730
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

		// Token: 0x06004247 RID: 16967 RVA: 0x0013956E File Offset: 0x0013776E
		public override Node Visit(ConditionalOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
			this.ProcessConditionalOp(op, n);
			return n;
		}

		// Token: 0x06004248 RID: 16968 RVA: 0x00139584 File Offset: 0x00137784
		private void ProcessConditionalOp(ConditionalOp op, Node n)
		{
			if ((op.OpType == OpType.IsNull && TypeSemantics.IsRowType(n.Child0.Op.Type)) || TypeSemantics.IsComplexType(n.Child0.Op.Type))
			{
				StructuredTypeNullabilityAnalyzer.MarkAsNeedingNullSentinel(this.m_typesNeedingNullSentinel, n.Child0.Op.Type);
			}
		}

		// Token: 0x06004249 RID: 16969 RVA: 0x001395E4 File Offset: 0x001377E4
		private static void ValidateNavPropertyOp(PropertyOp op)
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
				throw new MetadataException(Strings.EntityClient_IncompatibleNavigationPropertyResult(navigationProperty.DeclaringType.FullName, navigationProperty.Name));
			}
		}

		// Token: 0x0600424A RID: 16970 RVA: 0x00139664 File Offset: 0x00137864
		private Node VisitNavPropertyOp(PropertyOp op, Node n)
		{
			PreProcessor.ValidateNavPropertyOp(op);
			if (!PreProcessor.IsNavigationPropertyOverVarRef(n.Child0))
			{
				this.VisitScalarOpDefault(op, n);
			}
			PreProcessor.NavigationPropertyOpInfo navigationPropertyOpInfo = new PreProcessor.NavigationPropertyOpInfo(n, base.FindRelOpAncestor(), base.m_command);
			Node node;
			if (this._navigationPropertyOpRewrites.TryGetValue(navigationPropertyOpInfo, out node))
			{
				return OpCopier.Copy(base.m_command, node);
			}
			navigationPropertyOpInfo.Seal();
			node = this.RewriteNavigationProperty((NavigationProperty)op.PropertyInfo, n.Child0, op.Type);
			node = base.VisitNode(node);
			this._navigationPropertyOpRewrites.Add(navigationPropertyOpInfo, node);
			return node;
		}

		// Token: 0x0600424B RID: 16971 RVA: 0x001396FC File Offset: 0x001378FC
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

		// Token: 0x0600424C RID: 16972 RVA: 0x0013975C File Offset: 0x0013795C
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

		// Token: 0x0600424D RID: 16973 RVA: 0x0013978B File Offset: 0x0013798B
		public override Node Visit(RefOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
			this.AddEntitySetReference(op.EntitySet);
			return n;
		}

		// Token: 0x0600424E RID: 16974 RVA: 0x001397A3 File Offset: 0x001379A3
		public override Node Visit(TreatOp op, Node n)
		{
			n = base.Visit(op, n);
			if (this.CanRewriteTypeTest(op.Type.EdmType, n.Child0.Op.Type.EdmType))
			{
				return n.Child0;
			}
			return n;
		}

		// Token: 0x0600424F RID: 16975 RVA: 0x001397E0 File Offset: 0x001379E0
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

		// Token: 0x06004250 RID: 16976 RVA: 0x00139858 File Offset: 0x00137A58
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

		// Token: 0x06004251 RID: 16977 RVA: 0x001398D0 File Offset: 0x00137AD0
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
			ComparisonOp op7 = base.m_command.CreateComparisonOp(OpType.EQ, false);
			return base.m_command.CreateNode(op7, arg4, arg2);
		}

		// Token: 0x06004252 RID: 16978 RVA: 0x001399B8 File Offset: 0x00137BB8
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

		// Token: 0x06004253 RID: 16979 RVA: 0x001399EE File Offset: 0x00137BEE
		private EntitySet GetCurrentEntityTypeScope()
		{
			if (this.m_entityTypeScopes.Count == 0)
			{
				return null;
			}
			return this.m_entityTypeScopes.Peek();
		}

		// Token: 0x06004254 RID: 16980 RVA: 0x00139A0C File Offset: 0x00137C0C
		private static RelationshipSet FindRelationshipSet(EntitySetBase entitySet, RelProperty relProperty)
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

		// Token: 0x06004255 RID: 16981 RVA: 0x00139AA4 File Offset: 0x00137CA4
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private static int FindPosition(EdmType type, EdmMember member)
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
			PlanCompiler.Assert(false, string.Concat(new object[]
			{
				"Could not find property ",
				member,
				" in type ",
				type.Name
			}));
			return -1;
		}

		// Token: 0x06004256 RID: 16982 RVA: 0x00139B48 File Offset: 0x00137D48
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "OpType")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "BuildKeyExpression")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private Node BuildKeyExpressionForNewEntityOp(Op op, Node n)
		{
			PlanCompiler.Assert(op.OpType == OpType.NewEntity || op.OpType == OpType.DiscriminatedNewEntity, "BuildKeyExpression: Unexpected OpType:" + op.OpType);
			int num = (op.OpType == OpType.DiscriminatedNewEntity) ? 1 : 0;
			EntityTypeBase entityTypeBase = (EntityTypeBase)op.Type.EdmType;
			List<Node> list = new List<Node>();
			List<KeyValuePair<string, TypeUsage>> list2 = new List<KeyValuePair<string, TypeUsage>>();
			foreach (EdmMember edmMember in entityTypeBase.KeyMembers)
			{
				int num2 = PreProcessor.FindPosition(entityTypeBase, edmMember) + num;
				PlanCompiler.Assert(n.Children.Count > num2, string.Concat(new object[]
				{
					"invalid position ",
					num2,
					"; total count = ",
					n.Children.Count
				}));
				list.Add(n.Children[num2]);
				list2.Add(new KeyValuePair<string, TypeUsage>(edmMember.Name, edmMember.TypeUsage));
			}
			TypeUsage type = TypeHelpers.CreateRowTypeUsage(list2);
			NewRecordOp op2 = base.m_command.CreateNewRecordOp(type);
			return base.m_command.CreateNode(op2, list);
		}

		// Token: 0x06004257 RID: 16983 RVA: 0x00139CB0 File Offset: 0x00137EB0
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private Node BuildRelPropertyExpression(EntitySetBase entitySet, RelProperty relProperty, Node keyExpr)
		{
			keyExpr = OpCopier.Copy(base.m_command, keyExpr);
			RelationshipSet relationshipSet = PreProcessor.FindRelationshipSet(entitySet, relProperty);
			if (relationshipSet == null)
			{
				return base.m_command.CreateNode(base.m_command.CreateNullOp(relProperty.ToEnd.TypeUsage));
			}
			ScanTableOp scanTableOp = base.m_command.CreateScanTableOp(Command.CreateTableDefinition(relationshipSet));
			PlanCompiler.Assert(scanTableOp.Table.Columns.Count == 1, string.Concat(new object[]
			{
				"Unexpected column count for table:",
				scanTableOp.Table.TableMetadata.Extent,
				"=",
				scanTableOp.Table.Columns.Count
			}));
			Var var = scanTableOp.Table.Columns[0];
			Node arg = base.m_command.CreateNode(scanTableOp);
			Node arg2 = base.m_command.CreateNode(base.m_command.CreatePropertyOp(relProperty.FromEnd), base.m_command.CreateNode(base.m_command.CreateVarRefOp(var)));
			Node arg3 = base.m_command.BuildComparison(OpType.EQ, keyExpr, base.m_command.CreateNode(base.m_command.CreateGetRefKeyOp(keyExpr.Op.Type), arg2), true);
			Node n = base.m_command.CreateNode(base.m_command.CreateFilterOp(), arg, arg3);
			Node node = base.VisitNode(n);
			node = base.AddSubqueryToParentRelOp(var, node);
			return base.m_command.CreateNode(base.m_command.CreatePropertyOp(relProperty.ToEnd), node);
		}

		// Token: 0x06004258 RID: 16984 RVA: 0x0013A03C File Offset: 0x0013823C
		private IEnumerable<Node> BuildAllRelPropertyExpressions(EntitySetBase entitySet, List<RelProperty> relPropertyList, Dictionary<RelProperty, Node> prebuiltExpressions, Node keyExpr)
		{
			foreach (RelProperty r in relPropertyList)
			{
				Node relPropNode;
				if (!prebuiltExpressions.TryGetValue(r, out relPropNode))
				{
					relPropNode = this.BuildRelPropertyExpression(entitySet, r, keyExpr);
				}
				yield return relPropNode;
			}
			yield break;
		}

		// Token: 0x06004259 RID: 16985 RVA: 0x0013A078 File Offset: 0x00138278
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x0600425A RID: 16986 RVA: 0x0013A2A4 File Offset: 0x001384A4
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

		// Token: 0x0600425B RID: 16987 RVA: 0x0013A48C File Offset: 0x0013868C
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
			else if (n.Children.Count == 1 || PreProcessor.AreAllConstantsOrNulls(n.Children))
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
						ComparisonOp op2 = base.m_command.CreateComparisonOp(OpType.EQ, false);
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

		// Token: 0x0600425C RID: 16988 RVA: 0x0013A7D8 File Offset: 0x001389D8
		private static bool AreAllConstantsOrNulls(List<Node> nodes)
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

		// Token: 0x0600425D RID: 16989 RVA: 0x0013A844 File Offset: 0x00138A44
		public override Node Visit(CollectOp op, Node n)
		{
			this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.NestPullup);
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x0600425E RID: 16990 RVA: 0x0013A85C File Offset: 0x00138A5C
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

		// Token: 0x0600425F RID: 16991 RVA: 0x0013A8AC File Offset: 0x00138AAC
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ScanTableOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private Node ProcessScanTable(Node scanTableNode, ScanTableOp scanTableOp, ref IsOfOp typeFilter)
		{
			this.HandleTableOpMetadata(scanTableOp);
			PlanCompiler.Assert(scanTableOp.Table.TableMetadata.Extent != null, "ScanTableOp must reference a table with an extent");
			if (scanTableOp.Table.TableMetadata.Extent.EntityContainer.DataSpace == DataSpace.SSpace)
			{
				return scanTableNode;
			}
			Node n = this.ExpandView(scanTableOp, ref typeFilter);
			return base.VisitNode(n);
		}

		// Token: 0x06004260 RID: 16992 RVA: 0x0013A914 File Offset: 0x00138B14
		public override Node Visit(ScanTableOp op, Node n)
		{
			IsOfOp isOfOp = null;
			return this.ProcessScanTable(n, op, ref isOfOp);
		}

		// Token: 0x06004261 RID: 16993 RVA: 0x0013A930 File Offset: 0x00138B30
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "mentityTypeScopes")]
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

		// Token: 0x06004262 RID: 16994 RVA: 0x0013A9B7 File Offset: 0x00138BB7
		protected override Node VisitJoinOp(JoinBaseOp op, Node n)
		{
			if (op.OpType == OpType.InnerJoin || op.OpType == OpType.LeftOuterJoin)
			{
				this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.JoinElimination);
			}
			if (base.ProcessJoinOp(n))
			{
				this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.Normalization);
			}
			return n;
		}

		// Token: 0x06004263 RID: 16995 RVA: 0x0013A9EF File Offset: 0x00138BEF
		protected override Node VisitApplyOp(ApplyBaseOp op, Node n)
		{
			this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.JoinElimination);
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06004264 RID: 16996 RVA: 0x0013AA08 File Offset: 0x00138C08
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "SortOp")]
		private bool IsSortUnnecessary()
		{
			Node node = this.m_ancestors.Peek();
			PlanCompiler.Assert(node != null, "unexpected SortOp as root node?");
			return node.Op.OpType != OpType.PhysicalProject;
		}

		// Token: 0x06004265 RID: 16997 RVA: 0x0013AA44 File Offset: 0x00138C44
		public override Node Visit(SortOp op, Node n)
		{
			if (this.IsSortUnnecessary())
			{
				return base.VisitNode(n.Child0);
			}
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06004266 RID: 16998 RVA: 0x0013AA64 File Offset: 0x00138C64
		private static bool IsOfTypeOverScanTable(Node n, out IsOfOp typeFilter)
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

		// Token: 0x06004267 RID: 16999 RVA: 0x0013AAEC File Offset: 0x00138CEC
		public override Node Visit(FilterOp op, Node n)
		{
			IsOfOp isOfOp;
			if (PreProcessor.IsOfTypeOverScanTable(n, out isOfOp))
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

		// Token: 0x06004268 RID: 17000 RVA: 0x0013AB4C File Offset: 0x00138D4C
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "projectOp")]
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

		// Token: 0x06004269 RID: 17001 RVA: 0x0013AC78 File Offset: 0x00138E78
		public override Node Visit(GroupByIntoOp op, Node n)
		{
			this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.AggregatePushdown);
			return base.Visit(op, n);
		}

		// Token: 0x0600426A RID: 17002 RVA: 0x0013AC8E File Offset: 0x00138E8E
		public override Node Visit(ComparisonOp op, Node n)
		{
			if (op.OpType == OpType.EQ || op.OpType == OpType.NE)
			{
				this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.NullSemantics);
			}
			return base.Visit(op, n);
		}

		// Token: 0x0400188F RID: 6287
		private readonly Stack<EntitySet> m_entityTypeScopes = new Stack<EntitySet>();

		// Token: 0x04001890 RID: 6288
		private readonly HashSet<EntityContainer> m_referencedEntityContainers = new HashSet<EntityContainer>();

		// Token: 0x04001891 RID: 6289
		private readonly HashSet<EntitySet> m_referencedEntitySets = new HashSet<EntitySet>();

		// Token: 0x04001892 RID: 6290
		private readonly HashSet<TypeUsage> m_referencedTypes = new HashSet<TypeUsage>();

		// Token: 0x04001893 RID: 6291
		private readonly HashSet<EntityType> m_freeFloatingEntityConstructorTypes = new HashSet<EntityType>();

		// Token: 0x04001894 RID: 6292
		private readonly HashSet<string> m_typesNeedingNullSentinel = new HashSet<string>();

		// Token: 0x04001895 RID: 6293
		private readonly Dictionary<EdmFunction, EdmProperty[]> m_tvfResultKeys = new Dictionary<EdmFunction, EdmProperty[]>();

		// Token: 0x04001896 RID: 6294
		private readonly RelPropertyHelper m_relPropertyHelper;

		// Token: 0x04001897 RID: 6295
		private bool m_suppressDiscriminatorMaps;

		// Token: 0x04001898 RID: 6296
		private readonly Dictionary<EntitySetBase, DiscriminatorMapInfo> m_discriminatorMaps = new Dictionary<EntitySetBase, DiscriminatorMapInfo>();

		// Token: 0x04001899 RID: 6297
		private readonly Dictionary<PreProcessor.NavigationPropertyOpInfo, Node> _navigationPropertyOpRewrites = new Dictionary<PreProcessor.NavigationPropertyOpInfo, Node>();

		// Token: 0x02000690 RID: 1680
		private class NavigationPropertyOpInfo
		{
			// Token: 0x0600426B RID: 17003 RVA: 0x0013ACB8 File Offset: 0x00138EB8
			public NavigationPropertyOpInfo(Node node, Node root, Command command)
			{
				this._node = node;
				this._root = root;
				this._command = command;
				this._hashCode = ((((this._root != null) ? RuntimeHelpers.GetHashCode(this._root) : 0) * 397 ^ RuntimeHelpers.GetHashCode(PreProcessor.NavigationPropertyOpInfo.GetProperty(this._node))) * 397 ^ this._node.GetNodeInfo(this._command).HashValue);
			}

			// Token: 0x0600426C RID: 17004 RVA: 0x0013AD30 File Offset: 0x00138F30
			public override int GetHashCode()
			{
				return this._hashCode;
			}

			// Token: 0x0600426D RID: 17005 RVA: 0x0013AD38 File Offset: 0x00138F38
			public override bool Equals(object obj)
			{
				PreProcessor.NavigationPropertyOpInfo navigationPropertyOpInfo = obj as PreProcessor.NavigationPropertyOpInfo;
				return navigationPropertyOpInfo != null && this._root != null && object.ReferenceEquals(this._root, navigationPropertyOpInfo._root) && object.ReferenceEquals(PreProcessor.NavigationPropertyOpInfo.GetProperty(this._node), PreProcessor.NavigationPropertyOpInfo.GetProperty(navigationPropertyOpInfo._node)) && this._node.IsEquivalent(navigationPropertyOpInfo._node);
			}

			// Token: 0x0600426E RID: 17006 RVA: 0x0013AD9A File Offset: 0x00138F9A
			public void Seal()
			{
				this._node = OpCopier.Copy(this._command, this._node);
			}

			// Token: 0x0600426F RID: 17007 RVA: 0x0013ADB3 File Offset: 0x00138FB3
			private static EdmMember GetProperty(Node node)
			{
				return ((PropertyOp)node.Op).PropertyInfo;
			}

			// Token: 0x0400189A RID: 6298
			private Node _node;

			// Token: 0x0400189B RID: 6299
			private readonly Node _root;

			// Token: 0x0400189C RID: 6300
			private readonly Command _command;

			// Token: 0x0400189D RID: 6301
			private readonly int _hashCode;
		}
	}
}
