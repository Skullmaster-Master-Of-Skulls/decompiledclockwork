using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.Validation;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002A0 RID: 672
	internal class Cell : InternalBase
	{
		// Token: 0x060027EC RID: 10220 RVA: 0x0009ADD0 File Offset: 0x00098FD0
		private Cell(CellQuery cQuery, CellQuery sQuery, CellLabel label, int cellNumber)
		{
			this.m_cQuery = cQuery;
			this.m_sQuery = sQuery;
			this.m_label = label;
			this.m_cellNumber = cellNumber;
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x0009ADF8 File Offset: 0x00098FF8
		internal Cell(Cell source)
		{
			this.m_cQuery = new CellQuery(source.m_cQuery);
			this.m_sQuery = new CellQuery(source.m_sQuery);
			this.m_label = new CellLabel(source.m_label);
			this.m_cellNumber = source.m_cellNumber;
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x060027EE RID: 10222 RVA: 0x0009AE4A File Offset: 0x0009904A
		internal CellQuery CQuery
		{
			get
			{
				return this.m_cQuery;
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x060027EF RID: 10223 RVA: 0x0009AE52 File Offset: 0x00099052
		internal CellQuery SQuery
		{
			get
			{
				return this.m_sQuery;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x060027F0 RID: 10224 RVA: 0x0009AE5A File Offset: 0x0009905A
		internal CellLabel CellLabel
		{
			get
			{
				return this.m_label;
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x060027F1 RID: 10225 RVA: 0x0009AE62 File Offset: 0x00099062
		internal int CellNumber
		{
			get
			{
				return this.m_cellNumber;
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x060027F2 RID: 10226 RVA: 0x0009AE6A File Offset: 0x0009906A
		internal string CellNumberAsString
		{
			get
			{
				return StringUtil.FormatInvariant("V{0}", new object[]
				{
					this.CellNumber
				});
			}
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x0009AE8A File Offset: 0x0009908A
		internal void GetIdentifiers(CqlIdentifiers identifiers)
		{
			this.m_cQuery.GetIdentifiers(identifiers);
			this.m_sQuery.GetIdentifiers(identifiers);
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x0009AEA4 File Offset: 0x000990A4
		internal Set<EdmProperty> GetCSlotsForTableColumns(IEnumerable<MemberPath> columns)
		{
			List<int> projectedPositions = this.SQuery.GetProjectedPositions(columns);
			if (projectedPositions == null)
			{
				return null;
			}
			Set<EdmProperty> set = new Set<EdmProperty>();
			foreach (int slotNum in projectedPositions)
			{
				ProjectedSlot projectedSlot = this.CQuery.ProjectedSlotAt(slotNum);
				MemberProjectedSlot memberProjectedSlot = projectedSlot as MemberProjectedSlot;
				if (memberProjectedSlot == null)
				{
					return null;
				}
				set.Add((EdmProperty)memberProjectedSlot.MemberPath.LeafEdmMember);
			}
			return set;
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x0009AF40 File Offset: 0x00099140
		internal CellQuery GetLeftQuery(ViewTarget side)
		{
			if (side != ViewTarget.QueryView)
			{
				return this.m_sQuery;
			}
			return this.m_cQuery;
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x0009AF52 File Offset: 0x00099152
		internal CellQuery GetRightQuery(ViewTarget side)
		{
			if (side != ViewTarget.QueryView)
			{
				return this.m_cQuery;
			}
			return this.m_sQuery;
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x0009AF64 File Offset: 0x00099164
		internal ViewCellRelation CreateViewCellRelation(int cellNumber)
		{
			if (this.m_viewCellRelation != null)
			{
				return this.m_viewCellRelation;
			}
			this.GenerateCellRelations(cellNumber);
			return this.m_viewCellRelation;
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x0009AF84 File Offset: 0x00099184
		private void GenerateCellRelations(int cellNumber)
		{
			List<ViewCellSlot> list = new List<ViewCellSlot>();
			for (int i = 0; i < this.CQuery.NumProjectedSlots; i++)
			{
				ProjectedSlot projectedSlot = this.CQuery.ProjectedSlotAt(i);
				ProjectedSlot projectedSlot2 = this.SQuery.ProjectedSlotAt(i);
				MemberProjectedSlot cSlot = (MemberProjectedSlot)projectedSlot;
				MemberProjectedSlot sSlot = (MemberProjectedSlot)projectedSlot2;
				ViewCellSlot item = new ViewCellSlot(i, cSlot, sSlot);
				list.Add(item);
			}
			this.m_viewCellRelation = new ViewCellRelation(this, list, cellNumber);
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x0009AFF9 File Offset: 0x000991F9
		internal override void ToCompactString(StringBuilder builder)
		{
			this.CQuery.ToCompactString(builder);
			builder.Append(" = ");
			this.SQuery.ToCompactString(builder);
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x0009B01F File Offset: 0x0009921F
		internal override void ToFullString(StringBuilder builder)
		{
			this.CQuery.ToFullString(builder);
			builder.Append(" = ");
			this.SQuery.ToFullString(builder);
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x0009B045 File Offset: 0x00099245
		public override string ToString()
		{
			return this.ToFullString();
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x0009B050 File Offset: 0x00099250
		internal static void CellsToBuilder(StringBuilder builder, IEnumerable<Cell> cells)
		{
			builder.AppendLine();
			builder.AppendLine("=========================================================================");
			foreach (Cell cell in cells)
			{
				builder.AppendLine();
				StringUtil.FormatStringBuilder(builder, "Mapping Cell V{0}:", new object[]
				{
					cell.CellNumber
				});
				builder.AppendLine();
				builder.Append("C: ");
				cell.CQuery.ToFullString(builder);
				builder.AppendLine();
				builder.AppendLine();
				builder.Append("S: ");
				cell.SQuery.ToFullString(builder);
				builder.AppendLine();
			}
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x0009B11C File Offset: 0x0009931C
		internal static Cell CreateCS(CellQuery cQuery, CellQuery sQuery, CellLabel label, int cellNumber)
		{
			return new Cell(cQuery, sQuery, label, cellNumber);
		}

		// Token: 0x04001237 RID: 4663
		private CellQuery m_cQuery;

		// Token: 0x04001238 RID: 4664
		private CellQuery m_sQuery;

		// Token: 0x04001239 RID: 4665
		private int m_cellNumber;

		// Token: 0x0400123A RID: 4666
		private CellLabel m_label;

		// Token: 0x0400123B RID: 4667
		private ViewCellRelation m_viewCellRelation;
	}
}
