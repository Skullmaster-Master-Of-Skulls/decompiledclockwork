using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Validation;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x0200041D RID: 1053
	internal class BasicViewGenerator : InternalBase
	{
		// Token: 0x060026C3 RID: 9923 RVA: 0x000BB078 File Offset: 0x000B9278
		internal BasicViewGenerator(MemberProjectionIndex projectedSlotMap, List<LeftCellWrapper> usedCells, FragmentQuery activeDomain, ViewgenContext context, MemberDomainMap domainMap, ErrorLog errorLog, ConfigViewGenerator config)
		{
			this.m_projectedSlotMap = projectedSlotMap;
			this.m_usedCells = usedCells;
			this.m_viewgenContext = context;
			this.m_activeDomain = activeDomain;
			this.m_errorLog = errorLog;
			this.m_config = config;
			this.m_domainMap = domainMap;
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060026C4 RID: 9924 RVA: 0x000BB0B5 File Offset: 0x000B92B5
		private FragmentQueryProcessor LeftQP
		{
			get
			{
				return this.m_viewgenContext.LeftFragmentQP;
			}
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x000BB0C4 File Offset: 0x000B92C4
		internal CellTreeNode CreateViewExpression()
		{
			OpCellTreeNode opCellTreeNode = new OpCellTreeNode(this.m_viewgenContext, CellTreeOpType.FOJ);
			foreach (LeftCellWrapper cellWrapper in this.m_usedCells)
			{
				LeafCellTreeNode child = new LeafCellTreeNode(this.m_viewgenContext, cellWrapper);
				opCellTreeNode.Add(child);
			}
			CellTreeNode cellTreeNode = this.GroupByRightExtent(opCellTreeNode);
			cellTreeNode = this.IsolateUnions(cellTreeNode);
			cellTreeNode = this.IsolateByOperator(cellTreeNode, CellTreeOpType.Union);
			cellTreeNode = this.IsolateByOperator(cellTreeNode, CellTreeOpType.IJ);
			cellTreeNode = this.IsolateByOperator(cellTreeNode, CellTreeOpType.LOJ);
			if (this.m_viewgenContext.ViewTarget == ViewTarget.QueryView)
			{
				cellTreeNode = this.ConvertUnionsToNormalizedLOJs(cellTreeNode);
			}
			return cellTreeNode;
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x000BB174 File Offset: 0x000B9374
		internal CellTreeNode GroupByRightExtent(CellTreeNode rootNode)
		{
			KeyToListMap<EntitySetBase, LeafCellTreeNode> keyToListMap = new KeyToListMap<EntitySetBase, LeafCellTreeNode>(EqualityComparer<EntitySetBase>.Default);
			foreach (CellTreeNode cellTreeNode in rootNode.Children)
			{
				LeafCellTreeNode leafCellTreeNode = (LeafCellTreeNode)cellTreeNode;
				EntitySetBase extent = leafCellTreeNode.LeftCellWrapper.RightCellQuery.Extent;
				keyToListMap.Add(extent, leafCellTreeNode);
			}
			OpCellTreeNode opCellTreeNode = new OpCellTreeNode(this.m_viewgenContext, CellTreeOpType.FOJ);
			foreach (EntitySetBase key in keyToListMap.Keys)
			{
				OpCellTreeNode opCellTreeNode2 = new OpCellTreeNode(this.m_viewgenContext, CellTreeOpType.FOJ);
				foreach (LeafCellTreeNode child in keyToListMap.ListForKey(key))
				{
					opCellTreeNode2.Add(child);
				}
				opCellTreeNode.Add(opCellTreeNode2);
			}
			return opCellTreeNode.Flatten();
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x000BB294 File Offset: 0x000B9494
		private CellTreeNode IsolateUnions(CellTreeNode rootNode)
		{
			if (rootNode.Children.Count <= 1)
			{
				return rootNode;
			}
			for (int i = 0; i < rootNode.Children.Count; i++)
			{
				rootNode.Children[i] = this.IsolateUnions(rootNode.Children[i]);
			}
			OpCellTreeNode opCellTreeNode = new OpCellTreeNode(this.m_viewgenContext, CellTreeOpType.Union);
			ModifiableIteratorCollection<CellTreeNode> modifiableIteratorCollection = new ModifiableIteratorCollection<CellTreeNode>(rootNode.Children);
			while (!modifiableIteratorCollection.IsEmpty)
			{
				OpCellTreeNode opCellTreeNode2 = new OpCellTreeNode(this.m_viewgenContext, CellTreeOpType.FOJ);
				CellTreeNode child = modifiableIteratorCollection.RemoveOneElement();
				opCellTreeNode2.Add(child);
				foreach (CellTreeNode cellTreeNode in modifiableIteratorCollection.Elements())
				{
					if (!this.IsDisjoint(opCellTreeNode2, cellTreeNode))
					{
						opCellTreeNode2.Add(cellTreeNode);
						modifiableIteratorCollection.RemoveCurrentOfIterator();
						modifiableIteratorCollection.ResetIterator();
					}
				}
				opCellTreeNode.Add(opCellTreeNode2);
			}
			return opCellTreeNode.Flatten();
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x000BB3A8 File Offset: 0x000B95A8
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private CellTreeNode ConvertUnionsToNormalizedLOJs(CellTreeNode rootNode)
		{
			for (int i = 0; i < rootNode.Children.Count; i++)
			{
				rootNode.Children[i] = this.ConvertUnionsToNormalizedLOJs(rootNode.Children[i]);
			}
			if (rootNode.OpType != CellTreeOpType.LOJ || rootNode.Children.Count < 2)
			{
				return rootNode;
			}
			OpCellTreeNode opCellTreeNode = new OpCellTreeNode(this.m_viewgenContext, rootNode.OpType);
			List<CellTreeNode> list = new List<CellTreeNode>();
			OpCellTreeNode opCellTreeNode2 = null;
			HashSet<CellTreeNode> hashSet = null;
			if (rootNode.Children[0].OpType == CellTreeOpType.IJ)
			{
				opCellTreeNode2 = new OpCellTreeNode(this.m_viewgenContext, rootNode.Children[0].OpType);
				opCellTreeNode.Add(opCellTreeNode2);
				list.AddRange(rootNode.Children[0].Children);
				hashSet = new HashSet<CellTreeNode>(rootNode.Children[0].Children);
			}
			else
			{
				opCellTreeNode.Add(rootNode.Children[0]);
			}
			foreach (CellTreeNode cellTreeNode in rootNode.Children.Skip(1))
			{
				OpCellTreeNode opCellTreeNode3 = cellTreeNode as OpCellTreeNode;
				if (opCellTreeNode3 != null && opCellTreeNode3.OpType == CellTreeOpType.Union)
				{
					list.AddRange(opCellTreeNode3.Children);
				}
				else
				{
					list.Add(cellTreeNode);
				}
			}
			KeyToListMap<EntitySet, LeafCellTreeNode> keyToListMap = new KeyToListMap<EntitySet, LeafCellTreeNode>(EqualityComparer<EntitySet>.Default);
			foreach (CellTreeNode cellTreeNode2 in list)
			{
				LeafCellTreeNode leafCellTreeNode = cellTreeNode2 as LeafCellTreeNode;
				if (leafCellTreeNode != null)
				{
					EntitySetBase leafNodeTable = BasicViewGenerator.GetLeafNodeTable(leafCellTreeNode);
					if (leafNodeTable != null)
					{
						keyToListMap.Add((EntitySet)leafNodeTable, leafCellTreeNode);
					}
				}
				else if (hashSet != null && hashSet.Contains(cellTreeNode2))
				{
					opCellTreeNode2.Add(cellTreeNode2);
				}
				else
				{
					opCellTreeNode.Add(cellTreeNode2);
				}
			}
			KeyValuePair<EntitySet, List<LeafCellTreeNode>>[] array = (from m in keyToListMap.KeyValuePairs
			where m.Value.Count > 1
			select m).ToArray<KeyValuePair<EntitySet, List<LeafCellTreeNode>>>();
			foreach (KeyValuePair<EntitySet, List<LeafCellTreeNode>> keyValuePair in array)
			{
				keyToListMap.RemoveKey(keyValuePair.Key);
				foreach (LeafCellTreeNode leafCellTreeNode2 in keyValuePair.Value)
				{
					if (hashSet != null && hashSet.Contains(leafCellTreeNode2))
					{
						opCellTreeNode2.Add(leafCellTreeNode2);
					}
					else
					{
						opCellTreeNode.Add(leafCellTreeNode2);
					}
				}
			}
			KeyToListMap<EntitySet, EntitySet> keyToListMap2 = new KeyToListMap<EntitySet, EntitySet>(EqualityComparer<EntitySet>.Default);
			Dictionary<EntitySet, OpCellTreeNode> dictionary = new Dictionary<EntitySet, OpCellTreeNode>(EqualityComparer<EntitySet>.Default);
			foreach (KeyValuePair<EntitySet, List<LeafCellTreeNode>> keyValuePair2 in keyToListMap.KeyValuePairs)
			{
				EntitySet key = keyValuePair2.Key;
				foreach (EntitySet entitySet in BasicViewGenerator.GetFKOverPKDependents(key))
				{
					ReadOnlyCollection<LeafCellTreeNode> source;
					if (keyToListMap.TryGetListForKey(entitySet, out source) && (hashSet == null || !hashSet.Contains(source.Single<LeafCellTreeNode>())))
					{
						keyToListMap2.Add(key, entitySet);
					}
				}
				OpCellTreeNode opCellTreeNode4 = new OpCellTreeNode(this.m_viewgenContext, CellTreeOpType.LOJ);
				opCellTreeNode4.Add(keyValuePair2.Value.Single<LeafCellTreeNode>());
				dictionary.Add(key, opCellTreeNode4);
			}
			Dictionary<EntitySet, EntitySet> dictionary2 = new Dictionary<EntitySet, EntitySet>(EqualityComparer<EntitySet>.Default);
			foreach (KeyValuePair<EntitySet, List<EntitySet>> keyValuePair3 in keyToListMap2.KeyValuePairs)
			{
				EntitySet key2 = keyValuePair3.Key;
				foreach (EntitySet entitySet2 in keyValuePair3.Value)
				{
					OpCellTreeNode child;
					if (dictionary.TryGetValue(entitySet2, out child) && !dictionary2.ContainsKey(entitySet2) && !BasicViewGenerator.CheckLOJCycle(entitySet2, key2, dictionary2))
					{
						dictionary[keyValuePair3.Key].Add(child);
						dictionary2.Add(entitySet2, key2);
					}
				}
			}
			foreach (KeyValuePair<EntitySet, OpCellTreeNode> keyValuePair4 in dictionary)
			{
				if (!dictionary2.ContainsKey(keyValuePair4.Key))
				{
					OpCellTreeNode value = keyValuePair4.Value;
					if (hashSet != null && hashSet.Contains(value.Children[0]))
					{
						opCellTreeNode2.Add(value);
					}
					else
					{
						opCellTreeNode.Add(value);
					}
				}
			}
			return opCellTreeNode.Flatten();
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x000BBB9C File Offset: 0x000B9D9C
		private static IEnumerable<EntitySet> GetFKOverPKDependents(EntitySet principal)
		{
			using (IEnumerator<Tuple<AssociationSet, ReferentialConstraint>> enumerator = principal.ForeignKeyPrincipals.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Tuple<AssociationSet, ReferentialConstraint> pkFkInfo = enumerator.Current;
					ReadOnlyMetadataCollection<EdmMember> pkColumns = pkFkInfo.Item2.ToRole.GetEntityType().KeyMembers;
					ReadOnlyMetadataCollection<EdmProperty> fkColumns = pkFkInfo.Item2.ToProperties;
					if (pkColumns.Count == fkColumns.Count)
					{
						int i = 0;
						while (i < pkColumns.Count && pkColumns[i].EdmEquals(fkColumns[i]))
						{
							i++;
						}
						if (i == pkColumns.Count)
						{
							yield return (from ase in pkFkInfo.Item1.AssociationSetEnds
							where ase.Name == pkFkInfo.Item2.ToRole.Name
							select ase).Single<AssociationSetEnd>().EntitySet;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x000BBBB9 File Offset: 0x000B9DB9
		private static EntitySet GetLeafNodeTable(LeafCellTreeNode leaf)
		{
			return leaf.LeftCellWrapper.RightCellQuery.Extent as EntitySet;
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x000BBBD0 File Offset: 0x000B9DD0
		private static bool CheckLOJCycle(EntitySet child, EntitySet parent, Dictionary<EntitySet, EntitySet> nestedExtents)
		{
			while (!EqualityComparer<EntitySet>.Default.Equals(parent, child))
			{
				if (!nestedExtents.TryGetValue(parent, out parent))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x000BBBF0 File Offset: 0x000B9DF0
		internal CellTreeNode IsolateByOperator(CellTreeNode rootNode, CellTreeOpType opTypeToIsolate)
		{
			List<CellTreeNode> children = rootNode.Children;
			if (children.Count <= 1)
			{
				return rootNode;
			}
			for (int i = 0; i < children.Count; i++)
			{
				children[i] = this.IsolateByOperator(children[i], opTypeToIsolate);
			}
			if ((rootNode.OpType != CellTreeOpType.FOJ && rootNode.OpType != CellTreeOpType.LOJ) || rootNode.OpType == opTypeToIsolate)
			{
				return rootNode;
			}
			OpCellTreeNode opCellTreeNode = new OpCellTreeNode(this.m_viewgenContext, rootNode.OpType);
			ModifiableIteratorCollection<CellTreeNode> modifiableIteratorCollection = new ModifiableIteratorCollection<CellTreeNode>(children);
			while (!modifiableIteratorCollection.IsEmpty)
			{
				OpCellTreeNode opCellTreeNode2 = new OpCellTreeNode(this.m_viewgenContext, opTypeToIsolate);
				CellTreeNode child = modifiableIteratorCollection.RemoveOneElement();
				opCellTreeNode2.Add(child);
				foreach (CellTreeNode childNode in modifiableIteratorCollection.Elements())
				{
					if (this.TryAddChildToGroup(opTypeToIsolate, childNode, opCellTreeNode2))
					{
						modifiableIteratorCollection.RemoveCurrentOfIterator();
						if (opTypeToIsolate == CellTreeOpType.LOJ)
						{
							modifiableIteratorCollection.ResetIterator();
						}
					}
				}
				opCellTreeNode.Add(opCellTreeNode2);
			}
			return opCellTreeNode.Flatten();
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x000BBD00 File Offset: 0x000B9F00
		private bool TryAddChildToGroup(CellTreeOpType opTypeToIsolate, CellTreeNode childNode, OpCellTreeNode groupNode)
		{
			switch (opTypeToIsolate)
			{
			case CellTreeOpType.Union:
				if (this.IsDisjoint(childNode, groupNode))
				{
					groupNode.Add(childNode);
					return true;
				}
				break;
			case CellTreeOpType.LOJ:
				if (this.IsContainedIn(childNode, groupNode))
				{
					groupNode.Add(childNode);
					return true;
				}
				if (this.IsContainedIn(groupNode, childNode))
				{
					groupNode.AddFirst(childNode);
					return true;
				}
				break;
			case CellTreeOpType.IJ:
				if (this.IsEquivalentTo(childNode, groupNode))
				{
					groupNode.Add(childNode);
					return true;
				}
				break;
			}
			return false;
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x000BBD78 File Offset: 0x000B9F78
		private bool IsDisjoint(CellTreeNode n1, CellTreeNode n2)
		{
			ViewTarget viewTarget = this.m_viewgenContext.ViewTarget;
			bool flag = this.LeftQP.IsDisjointFrom(n1.LeftFragmentQuery, n2.LeftFragmentQuery);
			if (flag && this.m_viewgenContext.ViewTarget == ViewTarget.QueryView)
			{
				return true;
			}
			CellTreeNode cellTreeNode = new OpCellTreeNode(this.m_viewgenContext, CellTreeOpType.IJ, new CellTreeNode[]
			{
				n1,
				n2
			});
			bool isEmptyRightFragmentQuery = cellTreeNode.IsEmptyRightFragmentQuery;
			if (this.m_viewgenContext.ViewTarget != ViewTarget.UpdateView || !flag || isEmptyRightFragmentQuery)
			{
				return flag || isEmptyRightFragmentQuery;
			}
			if (ErrorPatternMatcher.FindMappingErrors(this.m_viewgenContext, this.m_domainMap, this.m_errorLog))
			{
				return false;
			}
			StringBuilder stringBuilder = new StringBuilder(Strings.Viewgen_RightSideNotDisjoint(this.m_viewgenContext.Extent.ToString()));
			stringBuilder.AppendLine();
			FragmentQuery fragmentQuery = this.LeftQP.Intersect(n1.RightFragmentQuery, n2.RightFragmentQuery);
			if (this.LeftQP.IsSatisfiable(fragmentQuery))
			{
				fragmentQuery.Condition.ExpensiveSimplify();
				RewritingValidator.EntityConfigurationToUserString(fragmentQuery.Condition, stringBuilder);
			}
			this.m_errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.DisjointConstraintViolation, stringBuilder.ToString(), this.m_viewgenContext.AllWrappersForExtent, string.Empty));
			ExceptionHelpers.ThrowMappingException(this.m_errorLog, this.m_config);
			return false;
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x000BBEC4 File Offset: 0x000BA0C4
		private bool IsContainedIn(CellTreeNode n1, CellTreeNode n2)
		{
			FragmentQuery q = this.LeftQP.Intersect(n1.LeftFragmentQuery, this.m_activeDomain);
			FragmentQuery q2 = this.LeftQP.Intersect(n2.LeftFragmentQuery, this.m_activeDomain);
			bool flag = this.LeftQP.IsContainedIn(q, q2);
			if (flag)
			{
				return true;
			}
			CellTreeNode cellTreeNode = new OpCellTreeNode(this.m_viewgenContext, CellTreeOpType.LASJ, new CellTreeNode[]
			{
				n1,
				n2
			});
			return cellTreeNode.IsEmptyRightFragmentQuery;
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x000BBF3F File Offset: 0x000BA13F
		private bool IsEquivalentTo(CellTreeNode n1, CellTreeNode n2)
		{
			return this.IsContainedIn(n1, n2) && this.IsContainedIn(n2, n1);
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x000BBF55 File Offset: 0x000BA155
		internal override void ToCompactString(StringBuilder builder)
		{
			this.m_projectedSlotMap.ToCompactString(builder);
		}

		// Token: 0x04000E93 RID: 3731
		private readonly MemberProjectionIndex m_projectedSlotMap;

		// Token: 0x04000E94 RID: 3732
		private readonly List<LeftCellWrapper> m_usedCells;

		// Token: 0x04000E95 RID: 3733
		private readonly FragmentQuery m_activeDomain;

		// Token: 0x04000E96 RID: 3734
		private readonly ViewgenContext m_viewgenContext;

		// Token: 0x04000E97 RID: 3735
		private readonly ErrorLog m_errorLog;

		// Token: 0x04000E98 RID: 3736
		private readonly ConfigViewGenerator m_config;

		// Token: 0x04000E99 RID: 3737
		private readonly MemberDomainMap m_domainMap;
	}
}
