using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.Utils;
using System.Data.Mapping.Update.Internal;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Data.Mapping.ViewGeneration.Validation;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Mapping.ViewGeneration
{
	// Token: 0x02000263 RID: 611
	internal class CellPartitioner : InternalBase
	{
		// Token: 0x060025C1 RID: 9665 RVA: 0x0008E67B File Offset: 0x0008C87B
		internal CellPartitioner(IEnumerable<Cell> cells, IEnumerable<ForeignConstraint> foreignKeyConstraints)
		{
			this.m_foreignKeyConstraints = foreignKeyConstraints;
			this.m_cells = cells;
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x0008E694 File Offset: 0x0008C894
		internal List<Set<Cell>> GroupRelatedCells()
		{
			UndirectedGraph<Cell> undirectedGraph = new UndirectedGraph<Cell>(EqualityComparer<Cell>.Default);
			List<Cell> list = new List<Cell>();
			foreach (Cell cell in this.m_cells)
			{
				undirectedGraph.AddVertex(cell);
				EntitySetBase extent = cell.CQuery.Extent;
				EntitySetBase extent2 = cell.SQuery.Extent;
				foreach (Cell cell2 in list)
				{
					EntitySetBase extent3 = cell2.CQuery.Extent;
					EntitySetBase extent4 = cell2.SQuery.Extent;
					bool flag = extent3.Equals(extent) || extent4.Equals(extent2);
					bool flag2 = this.OverlapViaForeignKeys(cell, cell2);
					bool flag3 = CellPartitioner.AreCellsConnectedViaRelationship(cell, cell2);
					if (flag || flag2 || flag3)
					{
						undirectedGraph.AddEdge(cell2, cell);
					}
				}
				list.Add(cell);
			}
			return CellPartitioner.GenerateConnectedComponents(undirectedGraph);
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x0008E7C0 File Offset: 0x0008C9C0
		private static bool AreCellsConnectedViaRelationship(Cell cell1, Cell cell2)
		{
			AssociationSet associationSet = cell1.CQuery.Extent as AssociationSet;
			AssociationSet associationSet2 = cell2.CQuery.Extent as AssociationSet;
			return (associationSet != null && MetadataHelper.IsExtentAtSomeRelationshipEnd(associationSet, cell2.CQuery.Extent)) || (associationSet2 != null && MetadataHelper.IsExtentAtSomeRelationshipEnd(associationSet2, cell1.CQuery.Extent));
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x0008E820 File Offset: 0x0008CA20
		private static List<Set<Cell>> GenerateConnectedComponents(UndirectedGraph<Cell> graph)
		{
			KeyToListMap<int, Cell> keyToListMap = graph.GenerateConnectedComponents();
			List<Set<Cell>> list = new List<Set<Cell>>();
			foreach (int key in keyToListMap.Keys)
			{
				ReadOnlyCollection<Cell> elements = keyToListMap.ListForKey(key);
				Set<Cell> item = new Set<Cell>(elements);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x0008E890 File Offset: 0x0008CA90
		private bool OverlapViaForeignKeys(Cell cell1, Cell cell2)
		{
			EntitySetBase extent = cell1.SQuery.Extent;
			EntitySetBase extent2 = cell2.SQuery.Extent;
			foreach (ForeignConstraint foreignConstraint in this.m_foreignKeyConstraints)
			{
				if ((extent.Equals(foreignConstraint.ParentTable) && extent2.Equals(foreignConstraint.ChildTable)) || (extent2.Equals(foreignConstraint.ParentTable) && extent.Equals(foreignConstraint.ChildTable)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x0008E930 File Offset: 0x0008CB30
		internal override void ToCompactString(StringBuilder builder)
		{
			Cell.CellsToBuilder(builder, this.m_cells);
		}

		// Token: 0x04001153 RID: 4435
		private IEnumerable<Cell> m_cells;

		// Token: 0x04001154 RID: 4436
		private IEnumerable<ForeignConstraint> m_foreignKeyConstraints;
	}
}
