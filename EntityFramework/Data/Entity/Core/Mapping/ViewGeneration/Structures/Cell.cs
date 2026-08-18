using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Validation;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000465 RID: 1125
	internal class Cell : InternalBase
	{
		// Token: 0x06002940 RID: 10560 RVA: 0x000C7EB8 File Offset: 0x000C60B8
		private Cell(CellQuery cQuery, CellQuery sQuery, CellLabel label, int cellNumber)
		{
			this.m_cQuery = cQuery;
			this.m_sQuery = sQuery;
			this.m_label = label;
			this.m_cellNumber = cellNumber;
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x000C7EE0 File Offset: 0x000C60E0
		internal Cell(Cell source)
		{
			this.m_cQuery = new CellQuery(source.m_cQuery);
			this.m_sQuery = new CellQuery(source.m_sQuery);
			this.m_label = new CellLabel(source.m_label);
			this.m_cellNumber = source.m_cellNumber;
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06002942 RID: 10562 RVA: 0x000C7F32 File Offset: 0x000C6132
		internal CellQuery CQuery
		{
			get
			{
				return this.m_cQuery;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06002943 RID: 10563 RVA: 0x000C7F3A File Offset: 0x000C613A
		internal CellQuery SQuery
		{
			get
			{
				return this.m_sQuery;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06002944 RID: 10564 RVA: 0x000C7F42 File Offset: 0x000C6142
		internal CellLabel CellLabel
		{
			get
			{
				return this.m_label;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06002945 RID: 10565 RVA: 0x000C7F4A File Offset: 0x000C614A
		internal int CellNumber
		{
			get
			{
				return this.m_cellNumber;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06002946 RID: 10566 RVA: 0x000C7F54 File Offset: 0x000C6154
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

		// Token: 0x06002947 RID: 10567 RVA: 0x000C7F81 File Offset: 0x000C6181
		internal void GetIdentifiers(CqlIdentifiers identifiers)
		{
			this.m_cQuery.GetIdentifiers(identifiers);
			this.m_sQuery.GetIdentifiers(identifiers);
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x000C7F9C File Offset: 0x000C619C
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

		// Token: 0x06002949 RID: 10569 RVA: 0x000C8038 File Offset: 0x000C6238
		internal CellQuery GetLeftQuery(ViewTarget side)
		{
			if (side != ViewTarget.QueryView)
			{
				return this.m_sQuery;
			}
			return this.m_cQuery;
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x000C804A File Offset: 0x000C624A
		internal CellQuery GetRightQuery(ViewTarget side)
		{
			if (side != ViewTarget.QueryView)
			{
				return this.m_cQuery;
			}
			return this.m_sQuery;
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x000C805C File Offset: 0x000C625C
		internal ViewCellRelation CreateViewCellRelation(int cellNumber)
		{
			if (this.m_viewCellRelation != null)
			{
				return this.m_viewCellRelation;
			}
			this.GenerateCellRelations(cellNumber);
			return this.m_viewCellRelation;
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x000C807C File Offset: 0x000C627C
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

		// Token: 0x0600294D RID: 10573 RVA: 0x000C80F1 File Offset: 0x000C62F1
		internal override void ToCompactString(StringBuilder builder)
		{
			this.CQuery.ToCompactString(builder);
			builder.Append(" = ");
			this.SQuery.ToCompactString(builder);
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x000C8117 File Offset: 0x000C6317
		internal override void ToFullString(StringBuilder builder)
		{
			this.CQuery.ToFullString(builder);
			builder.Append(" = ");
			this.SQuery.ToFullString(builder);
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x000C813D File Offset: 0x000C633D
		public override string ToString()
		{
			return this.ToFullString();
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x000C8148 File Offset: 0x000C6348
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

		// Token: 0x06002951 RID: 10577 RVA: 0x000C8214 File Offset: 0x000C6414
		internal static Cell CreateCS(CellQuery cQuery, CellQuery sQuery, CellLabel label, int cellNumber)
		{
			return new Cell(cQuery, sQuery, label, cellNumber);
		}

		// Token: 0x04000F5E RID: 3934
		private readonly CellQuery m_cQuery;

		// Token: 0x04000F5F RID: 3935
		private readonly CellQuery m_sQuery;

		// Token: 0x04000F60 RID: 3936
		private readonly int m_cellNumber;

		// Token: 0x04000F61 RID: 3937
		private readonly CellLabel m_label;

		// Token: 0x04000F62 RID: 3938
		private ViewCellRelation m_viewCellRelation;
	}
}
