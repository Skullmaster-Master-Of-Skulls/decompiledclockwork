using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.Update.Internal;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Validation;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x0200041F RID: 1055
	internal class CellPartitioner : InternalBase
	{
		// Token: 0x060026DF RID: 9951 RVA: 0x000BCC39 File Offset: 0x000BAE39
		internal CellPartitioner(IEnumerable<Cell> cells, IEnumerable<ForeignConstraint> foreignKeyConstraints)
		{
			this.m_foreignKeyConstraints = foreignKeyConstraints;
			this.m_cells = cells;
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x000BCC68 File Offset: 0x000BAE68
		internal List<Set<Cell>> GroupRelatedCells()
		{
			UndirectedGraph<EntitySetBase> undirectedGraph = new UndirectedGraph<EntitySetBase>(EqualityComparer<EntitySetBase>.Default);
			Dictionary<EntitySetBase, Set<Cell>> extentToCell = new Dictionary<EntitySetBase, Set<Cell>>(EqualityComparer<EntitySetBase>.Default);
			foreach (Cell cell in this.m_cells)
			{
				foreach (EntitySetBase entitySetBase in new EntitySetBase[]
				{
					cell.CQuery.Extent,
					cell.SQuery.Extent
				})
				{
					Set<Cell> set;
					if (!extentToCell.TryGetValue(entitySetBase, out set))
					{
						set = (extentToCell[entitySetBase] = new Set<Cell>());
					}
					set.Add(cell);
					undirectedGraph.AddVertex(entitySetBase);
				}
				undirectedGraph.AddEdge(cell.CQuery.Extent, cell.SQuery.Extent);
				AssociationSet associationSet = cell.CQuery.Extent as AssociationSet;
				if (associationSet != null)
				{
					foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
					{
						undirectedGraph.AddEdge(associationSetEnd.EntitySet, associationSet);
					}
				}
			}
			foreach (ForeignConstraint foreignConstraint in this.m_foreignKeyConstraints)
			{
				undirectedGraph.AddEdge(foreignConstraint.ChildTable, foreignConstraint.ParentTable);
			}
			KeyToListMap<int, EntitySetBase> keyToListMap = undirectedGraph.GenerateConnectedComponents();
			List<Set<Cell>> list = new List<Set<Cell>>();
			foreach (int key in keyToListMap.Keys)
			{
				IEnumerable<Set<Cell>> enumerable = from e in keyToListMap.ListForKey(key)
				select extentToCell[e];
				Set<Cell> set2 = new Set<Cell>();
				foreach (Set<Cell> elements in enumerable)
				{
					set2.AddRange(elements);
				}
				list.Add(set2);
			}
			return list;
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x000BCF24 File Offset: 0x000BB124
		internal override void ToCompactString(StringBuilder builder)
		{
			Cell.CellsToBuilder(builder, this.m_cells);
		}

		// Token: 0x04000E9F RID: 3743
		private readonly IEnumerable<Cell> m_cells;

		// Token: 0x04000EA0 RID: 3744
		private readonly IEnumerable<ForeignConstraint> m_foreignKeyConstraints;
	}
}
