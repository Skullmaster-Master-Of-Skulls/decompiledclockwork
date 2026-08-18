using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Validation;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x0200049D RID: 1181
	internal class CellGroupValidator
	{
		// Token: 0x06002B8E RID: 11150 RVA: 0x000D3C80 File Offset: 0x000D1E80
		internal CellGroupValidator(IEnumerable<Cell> cells, ConfigViewGenerator config)
		{
			this.m_cells = cells;
			this.m_config = config;
			this.m_errorLog = new ErrorLog();
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x000D3CA4 File Offset: 0x000D1EA4
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

		// Token: 0x06002B90 RID: 11152 RVA: 0x000D3DA8 File Offset: 0x000D1FA8
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
			foreach (Cell objA in this.m_cells)
			{
				foreach (Cell objB in this.m_cells)
				{
					object.ReferenceEquals(objA, objB);
				}
			}
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x000D3EA8 File Offset: 0x000D20A8
		private static void PopulateBaseConstraints(BasicCellRelation baseRelation, SchemaConstraints<BasicKeyConstraint> constraints)
		{
			baseRelation.PopulateKeyConstraints(constraints);
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x000D3EB4 File Offset: 0x000D20B4
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

		// Token: 0x06002B93 RID: 11155 RVA: 0x000D3F14 File Offset: 0x000D2114
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

		// Token: 0x06002B94 RID: 11156 RVA: 0x000D4084 File Offset: 0x000D2284
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

		// Token: 0x06002B95 RID: 11157 RVA: 0x000D4170 File Offset: 0x000D2370
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
							ErrorLog.Record record = new ErrorLog.Record(ViewGenErrorCode.MultipleFragmentsBetweenCandSExtentWithDistinct, Strings.Viewgen_MultipleFragmentsBetweenCandSExtentWithDistinct(cExtent.Name, sExtent.Name), sourceCells, string.Empty);
							this.m_errorLog.AddEntry(record);
						}
					}
				}
			}
			return this.m_errorLog.Count == count;
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x000D4304 File Offset: 0x000D2504
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

		// Token: 0x06002B97 RID: 11159 RVA: 0x000D4488 File Offset: 0x000D2688
		[Conditional("DEBUG")]
		private static void CheckConstraintSanity(SchemaConstraints<BasicKeyConstraint> cConstraints, SchemaConstraints<BasicKeyConstraint> sConstraints, SchemaConstraints<ViewKeyConstraint> cViewConstraints, SchemaConstraints<ViewKeyConstraint> sViewConstraints)
		{
		}

		// Token: 0x04001011 RID: 4113
		private readonly IEnumerable<Cell> m_cells;

		// Token: 0x04001012 RID: 4114
		private readonly ConfigViewGenerator m_config;

		// Token: 0x04001013 RID: 4115
		private readonly ErrorLog m_errorLog;

		// Token: 0x04001014 RID: 4116
		private SchemaConstraints<ViewKeyConstraint> m_cViewConstraints;

		// Token: 0x04001015 RID: 4117
		private SchemaConstraints<ViewKeyConstraint> m_sViewConstraints;

		// Token: 0x0200049E RID: 1182
		private class ExtentPair
		{
			// Token: 0x06002B9A RID: 11162 RVA: 0x000D448A File Offset: 0x000D268A
			internal ExtentPair(EntitySetBase acExtent, EntitySetBase asExtent)
			{
				this.cExtent = acExtent;
				this.sExtent = asExtent;
			}

			// Token: 0x06002B9B RID: 11163 RVA: 0x000D44A0 File Offset: 0x000D26A0
			public override bool Equals(object obj)
			{
				if (object.ReferenceEquals(this, obj))
				{
					return true;
				}
				CellGroupValidator.ExtentPair extentPair = obj as CellGroupValidator.ExtentPair;
				return extentPair != null && extentPair.cExtent.Equals(this.cExtent) && extentPair.sExtent.Equals(this.sExtent);
			}

			// Token: 0x06002B9C RID: 11164 RVA: 0x000D44EA File Offset: 0x000D26EA
			public override int GetHashCode()
			{
				return this.cExtent.GetHashCode() ^ this.sExtent.GetHashCode();
			}

			// Token: 0x04001018 RID: 4120
			internal readonly EntitySetBase cExtent;

			// Token: 0x04001019 RID: 4121
			internal readonly EntitySetBase sExtent;
		}
	}
}
