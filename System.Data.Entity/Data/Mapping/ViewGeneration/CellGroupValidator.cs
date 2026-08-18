using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Data.Mapping.ViewGeneration.Utils;
using System.Data.Mapping.ViewGeneration.Validation;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.Linq;

namespace System.Data.Mapping.ViewGeneration
{
	// Token: 0x0200026B RID: 619
	internal class CellGroupValidator
	{
		// Token: 0x060025FF RID: 9727 RVA: 0x000900FD File Offset: 0x0008E2FD
		internal CellGroupValidator(IEnumerable<Cell> cells, ConfigViewGenerator config)
		{
			this.m_cells = cells;
			this.m_config = config;
			this.m_errorLog = new ErrorLog();
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x00090120 File Offset: 0x0008E320
		internal ErrorLog Validate()
		{
			if (this.m_config.IsValidationEnabled)
			{
				if (!this.PerformSingleCellChecks())
				{
					return this.m_errorLog;
				}
			}
			else if (!this.CheckCellsWithDistinctFlag())
			{
				return this.m_errorLog;
			}
			SchemaConstraints<BasicKeyConstraint> schemaConstraints = new SchemaConstraints<BasicKeyConstraint>();
			SchemaConstraints<BasicKeyConstraint> schemaConstraints2 = new SchemaConstraints<BasicKeyConstraint>();
			this.ConstructCellRelationsWithConstraints(schemaConstraints, schemaConstraints2);
			if (this.m_config.IsVerboseTracing)
			{
				Trace.WriteLine(string.Empty);
				Trace.WriteLine("C-Level Basic Constraints");
				Trace.WriteLine(schemaConstraints);
				Trace.WriteLine("S-Level Basic Constraints");
				Trace.WriteLine(schemaConstraints2);
			}
			this.m_cViewConstraints = CellGroupValidator.PropagateConstraints(schemaConstraints);
			this.m_sViewConstraints = CellGroupValidator.PropagateConstraints(schemaConstraints2);
			if (this.m_config.IsVerboseTracing)
			{
				Trace.WriteLine(string.Empty);
				Trace.WriteLine("C-Level View Constraints");
				Trace.WriteLine(this.m_cViewConstraints);
				Trace.WriteLine("S-Level View Constraints");
				Trace.WriteLine(this.m_sViewConstraints);
			}
			if (this.m_config.IsValidationEnabled)
			{
				this.CheckImplication(this.m_cViewConstraints, this.m_sViewConstraints);
			}
			return this.m_errorLog;
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x00090224 File Offset: 0x0008E424
		private void ConstructCellRelationsWithConstraints(SchemaConstraints<BasicKeyConstraint> cConstraints, SchemaConstraints<BasicKeyConstraint> sConstraints)
		{
			int num = 0;
			foreach (Cell cell in this.m_cells)
			{
				cell.CreateViewCellRelation(num);
				BasicCellRelation basicCellRelation = cell.CQuery.BasicCellRelation;
				BasicCellRelation basicCellRelation2 = cell.SQuery.BasicCellRelation;
				CellGroupValidator.PopulateBaseConstraints(basicCellRelation, cConstraints);
				CellGroupValidator.PopulateBaseConstraints(basicCellRelation2, sConstraints);
				num++;
			}
			foreach (Cell cell2 in this.m_cells)
			{
				foreach (Cell cell3 in this.m_cells)
				{
				}
			}
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x0009031C File Offset: 0x0008E51C
		private static void PopulateBaseConstraints(BasicCellRelation baseRelation, SchemaConstraints<BasicKeyConstraint> constraints)
		{
			baseRelation.PopulateKeyConstraints(constraints);
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x00090328 File Offset: 0x0008E528
		private static SchemaConstraints<ViewKeyConstraint> PropagateConstraints(SchemaConstraints<BasicKeyConstraint> baseConstraints)
		{
			SchemaConstraints<ViewKeyConstraint> schemaConstraints = new SchemaConstraints<ViewKeyConstraint>();
			foreach (BasicKeyConstraint basicKeyConstraint in baseConstraints.KeyConstraints)
			{
				ViewKeyConstraint viewKeyConstraint = basicKeyConstraint.Propagate();
				if (viewKeyConstraint != null)
				{
					schemaConstraints.Add(viewKeyConstraint);
				}
			}
			return schemaConstraints;
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x00090388 File Offset: 0x0008E588
		private void CheckImplication(SchemaConstraints<ViewKeyConstraint> cViewConstraints, SchemaConstraints<ViewKeyConstraint> sViewConstraints)
		{
			this.CheckImplicationKeyConstraints(cViewConstraints, sViewConstraints);
			KeyToListMap<CellGroupValidator.ExtentPair, ViewKeyConstraint> keyToListMap = new KeyToListMap<CellGroupValidator.ExtentPair, ViewKeyConstraint>(EqualityComparer<CellGroupValidator.ExtentPair>.Default);
			foreach (ViewKeyConstraint viewKeyConstraint in cViewConstraints.KeyConstraints)
			{
				CellGroupValidator.ExtentPair key = new CellGroupValidator.ExtentPair(viewKeyConstraint.Cell.CQuery.Extent, viewKeyConstraint.Cell.SQuery.Extent);
				keyToListMap.Add(key, viewKeyConstraint);
			}
			foreach (CellGroupValidator.ExtentPair key2 in keyToListMap.Keys)
			{
				ReadOnlyCollection<ViewKeyConstraint> readOnlyCollection = keyToListMap.ListForKey(key2);
				bool flag = false;
				foreach (ViewKeyConstraint second in readOnlyCollection)
				{
					foreach (ViewKeyConstraint viewKeyConstraint2 in sViewConstraints.KeyConstraints)
					{
						if (viewKeyConstraint2.Implies(second))
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					this.m_errorLog.AddEntry(ViewKeyConstraint.GetErrorRecord(readOnlyCollection));
				}
			}
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x000904F8 File Offset: 0x0008E6F8
		private void CheckImplicationKeyConstraints(SchemaConstraints<ViewKeyConstraint> leftViewConstraints, SchemaConstraints<ViewKeyConstraint> rightViewConstraints)
		{
			foreach (ViewKeyConstraint viewKeyConstraint in rightViewConstraints.KeyConstraints)
			{
				bool flag = false;
				foreach (ViewKeyConstraint viewKeyConstraint2 in leftViewConstraints.KeyConstraints)
				{
					if (viewKeyConstraint2.Implies(viewKeyConstraint))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.m_errorLog.AddEntry(ViewKeyConstraint.GetErrorRecord(viewKeyConstraint));
				}
			}
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x00090598 File Offset: 0x0008E798
		private bool CheckCellsWithDistinctFlag()
		{
			int count = this.m_errorLog.Count;
			using (IEnumerator<Cell> enumerator = this.m_cells.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Cell cell = enumerator.Current;
					if (cell.SQuery.SelectDistinctFlag == CellQuery.SelectDistinct.Yes)
					{
						EntitySetBase cExtent = cell.CQuery.Extent;
						EntitySetBase sExtent = cell.SQuery.Extent;
						IEnumerable<Cell> enumerable = from otherCell in this.m_cells
						where otherCell != cell
						where otherCell.CQuery.Extent == cExtent && otherCell.SQuery.Extent == sExtent
						select otherCell;
						if (enumerable.Any<Cell>())
						{
							IEnumerable<Cell> sourceCells = Enumerable.Repeat<Cell>(cell, 1).Union(enumerable);
							ErrorLog.Record record = new ErrorLog.Record(true, ViewGenErrorCode.MultipleFragmentsBetweenCandSExtentWithDistinct, Strings.Viewgen_MultipleFragmentsBetweenCandSExtentWithDistinct(cExtent.Name, sExtent.Name), sourceCells, string.Empty);
							this.m_errorLog.AddEntry(record);
						}
					}
				}
			}
			return this.m_errorLog.Count == count;
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x000906D4 File Offset: 0x0008E8D4
		private bool PerformSingleCellChecks()
		{
			int count = this.m_errorLog.Count;
			foreach (Cell cell in this.m_cells)
			{
				ErrorLog.Record record = cell.SQuery.CheckForDuplicateFields(cell.CQuery, cell);
				if (record != null)
				{
					this.m_errorLog.AddEntry(record);
				}
				record = cell.CQuery.VerifyKeysPresent(cell, new Func<object, object, string>(Strings.ViewGen_EntitySetKey_Missing), new Func<object, object, object, string>(Strings.ViewGen_AssociationSetKey_Missing), ViewGenErrorCode.KeyNotMappedForCSideExtent);
				if (record != null)
				{
					this.m_errorLog.AddEntry(record);
				}
				record = cell.SQuery.VerifyKeysPresent(cell, new Func<object, object, string>(Strings.ViewGen_TableKey_Missing), null, ViewGenErrorCode.KeyNotMappedForTable);
				if (record != null)
				{
					this.m_errorLog.AddEntry(record);
				}
				record = cell.CQuery.CheckForProjectedNotNullSlots(cell, from c in this.m_cells
				where c.SQuery.Extent is AssociationSet
				select c);
				if (record != null)
				{
					this.m_errorLog.AddEntry(record);
				}
				record = cell.SQuery.CheckForProjectedNotNullSlots(cell, from c in this.m_cells
				where c.CQuery.Extent is AssociationSet
				select c);
				if (record != null)
				{
					this.m_errorLog.AddEntry(record);
				}
			}
			return this.m_errorLog.Count == count;
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x000089D0 File Offset: 0x00006BD0
		[Conditional("DEBUG")]
		private static void CheckConstraintSanity(SchemaConstraints<BasicKeyConstraint> cConstraints, SchemaConstraints<BasicKeyConstraint> sConstraints, SchemaConstraints<ViewKeyConstraint> cViewConstraints, SchemaConstraints<ViewKeyConstraint> sViewConstraints)
		{
		}

		// Token: 0x04001186 RID: 4486
		private IEnumerable<Cell> m_cells;

		// Token: 0x04001187 RID: 4487
		private ConfigViewGenerator m_config;

		// Token: 0x04001188 RID: 4488
		private ErrorLog m_errorLog;

		// Token: 0x04001189 RID: 4489
		private SchemaConstraints<ViewKeyConstraint> m_cViewConstraints;

		// Token: 0x0400118A RID: 4490
		private SchemaConstraints<ViewKeyConstraint> m_sViewConstraints;

		// Token: 0x02000598 RID: 1432
		private class ExtentPair
		{
			// Token: 0x0600402F RID: 16431 RVA: 0x000EC791 File Offset: 0x000EA991
			internal ExtentPair(EntitySetBase acExtent, EntitySetBase asExtent)
			{
				this.cExtent = acExtent;
				this.sExtent = asExtent;
			}

			// Token: 0x06004030 RID: 16432 RVA: 0x000EC7A8 File Offset: 0x000EA9A8
			public override bool Equals(object obj)
			{
				if (this == obj)
				{
					return true;
				}
				CellGroupValidator.ExtentPair extentPair = obj as CellGroupValidator.ExtentPair;
				return extentPair != null && extentPair.cExtent.Equals(this.cExtent) && extentPair.sExtent.Equals(this.sExtent);
			}

			// Token: 0x06004031 RID: 16433 RVA: 0x000EC7ED File Offset: 0x000EA9ED
			public override int GetHashCode()
			{
				return this.cExtent.GetHashCode() ^ this.sExtent.GetHashCode();
			}

			// Token: 0x04001CC4 RID: 7364
			internal EntitySetBase cExtent;

			// Token: 0x04001CC5 RID: 7365
			internal EntitySetBase sExtent;
		}
	}
}
