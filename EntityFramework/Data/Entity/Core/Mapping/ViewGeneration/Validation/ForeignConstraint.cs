using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000498 RID: 1176
	internal class ForeignConstraint : InternalBase
	{
		// Token: 0x06002B5B RID: 11099 RVA: 0x000D21B4 File Offset: 0x000D03B4
		internal ForeignConstraint(AssociationSet i_fkeySet, EntitySet i_parentTable, EntitySet i_childTable, ReadOnlyMetadataCollection<EdmProperty> i_parentColumns, ReadOnlyMetadataCollection<EdmProperty> i_childColumns)
		{
			this.m_fKeySet = i_fkeySet;
			this.m_parentTable = i_parentTable;
			this.m_childTable = i_childTable;
			this.m_childColumns = new List<MemberPath>();
			foreach (EdmProperty member in i_childColumns)
			{
				MemberPath item = new MemberPath(this.m_childTable, member);
				this.m_childColumns.Add(item);
			}
			this.m_parentColumns = new List<MemberPath>();
			foreach (EdmProperty member2 in i_parentColumns)
			{
				MemberPath item2 = new MemberPath(this.m_parentTable, member2);
				this.m_parentColumns.Add(item2);
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06002B5C RID: 11100 RVA: 0x000D2298 File Offset: 0x000D0498
		internal EntitySet ParentTable
		{
			get
			{
				return this.m_parentTable;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06002B5D RID: 11101 RVA: 0x000D22A0 File Offset: 0x000D04A0
		internal EntitySet ChildTable
		{
			get
			{
				return this.m_childTable;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06002B5E RID: 11102 RVA: 0x000D22A8 File Offset: 0x000D04A8
		internal IEnumerable<MemberPath> ChildColumns
		{
			get
			{
				return this.m_childColumns;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06002B5F RID: 11103 RVA: 0x000D22B0 File Offset: 0x000D04B0
		internal IEnumerable<MemberPath> ParentColumns
		{
			get
			{
				return this.m_parentColumns;
			}
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x000D22B8 File Offset: 0x000D04B8
		internal static List<ForeignConstraint> GetForeignConstraints(EntityContainer container)
		{
			List<ForeignConstraint> list = new List<ForeignConstraint>();
			foreach (EntitySetBase entitySetBase in container.BaseEntitySets)
			{
				AssociationSet associationSet = entitySetBase as AssociationSet;
				if (associationSet != null)
				{
					Dictionary<string, EntitySet> dictionary = new Dictionary<string, EntitySet>();
					foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
					{
						dictionary.Add(associationSetEnd.Name, associationSetEnd.EntitySet);
					}
					AssociationType elementType = associationSet.ElementType;
					foreach (ReferentialConstraint referentialConstraint in elementType.ReferentialConstraints)
					{
						EntitySet i_parentTable = dictionary[referentialConstraint.FromRole.Name];
						EntitySet i_childTable = dictionary[referentialConstraint.ToRole.Name];
						ForeignConstraint item = new ForeignConstraint(associationSet, i_parentTable, i_childTable, referentialConstraint.FromProperties, referentialConstraint.ToProperties);
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x000D2408 File Offset: 0x000D0608
		internal void CheckConstraint(Set<Cell> cells, QueryRewriter childRewriter, QueryRewriter parentRewriter, ErrorLog errorLog, ConfigViewGenerator config)
		{
			if (!this.IsConstraintRelevantForCells(cells))
			{
				return;
			}
			if (config.IsNormalTracing)
			{
				Trace.WriteLine(string.Empty);
				Trace.WriteLine(string.Empty);
				Trace.Write("Checking: ");
				Trace.WriteLine(this);
			}
			if (childRewriter == null && parentRewriter == null)
			{
				return;
			}
			if (childRewriter == null)
			{
				string message = Strings.ViewGen_Foreign_Key_Missing_Table_Mapping(this.ToUserString(), this.ChildTable.Name);
				ErrorLog.Record record = new ErrorLog.Record(ViewGenErrorCode.ForeignKeyMissingTableMapping, message, parentRewriter.UsedCells, string.Empty);
				errorLog.AddEntry(record);
				return;
			}
			if (parentRewriter == null)
			{
				string message2 = Strings.ViewGen_Foreign_Key_Missing_Table_Mapping(this.ToUserString(), this.ParentTable.Name);
				ErrorLog.Record record2 = new ErrorLog.Record(ViewGenErrorCode.ForeignKeyMissingTableMapping, message2, childRewriter.UsedCells, string.Empty);
				errorLog.AddEntry(record2);
				return;
			}
			if (this.CheckIfConstraintMappedToForeignKeyAssociation(childRewriter, parentRewriter, cells))
			{
				return;
			}
			int count = errorLog.Count;
			if (this.IsForeignKeySuperSetOfPrimaryKeyInChildTable())
			{
				this.GuaranteeForeignKeyConstraintInCSpace(childRewriter, parentRewriter, errorLog);
			}
			else
			{
				this.GuaranteeMappedRelationshipForForeignKey(childRewriter, parentRewriter, cells, errorLog, config);
			}
			if (count == errorLog.Count)
			{
				this.CheckForeignKeyColumnOrder(cells, errorLog);
			}
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x000D2514 File Offset: 0x000D0714
		private void GuaranteeForeignKeyConstraintInCSpace(QueryRewriter childRewriter, QueryRewriter parentRewriter, ErrorLog errorLog)
		{
			ViewgenContext viewgenContext = childRewriter.ViewgenContext;
			ViewgenContext viewgenContext2 = parentRewriter.ViewgenContext;
			CellTreeNode basicView = childRewriter.BasicView;
			CellTreeNode basicView2 = parentRewriter.BasicView;
			FragmentQueryProcessor fragmentQueryProcessor = FragmentQueryProcessor.Merge(viewgenContext.RightFragmentQP, viewgenContext2.RightFragmentQP);
			if (!fragmentQueryProcessor.IsContainedIn(basicView.RightFragmentQuery, basicView2.RightFragmentQuery))
			{
				LeftCellWrapper.GetExtentListAsUserString(basicView.GetLeaves());
				LeftCellWrapper.GetExtentListAsUserString(basicView2.GetLeaves());
				string message = Strings.ViewGen_Foreign_Key_Not_Guaranteed_InCSpace(this.ToUserString());
				Set<LeftCellWrapper> set = new Set<LeftCellWrapper>(basicView2.GetLeaves());
				set.AddRange(basicView.GetLeaves());
				ErrorLog.Record record = new ErrorLog.Record(ViewGenErrorCode.ForeignKeyNotGuaranteedInCSpace, message, set, string.Empty);
				errorLog.AddEntry(record);
			}
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x000D25C8 File Offset: 0x000D07C8
		private void GuaranteeMappedRelationshipForForeignKey(QueryRewriter childRewriter, QueryRewriter parentRewriter, IEnumerable<Cell> cells, ErrorLog errorLog, ConfigViewGenerator config)
		{
			ViewgenContext viewgenContext = childRewriter.ViewgenContext;
			ViewgenContext viewgenContext2 = parentRewriter.ViewgenContext;
			MemberPath prefix = new MemberPath(this.ChildTable);
			ExtentKey primaryKeyForEntityType = ExtentKey.GetPrimaryKeyForEntityType(prefix, this.ChildTable.ElementType);
			IEnumerable<MemberPath> keyFields = primaryKeyForEntityType.KeyFields;
			bool flag = false;
			bool flag2 = false;
			List<ErrorLog.Record> list = null;
			foreach (Cell cell in cells)
			{
				if (cell.SQuery.Extent.Equals(this.ChildTable))
				{
					AssociationEndMember relationEndForColumns = ForeignConstraint.GetRelationEndForColumns(cell, this.ChildColumns);
					if (relationEndForColumns == null || this.CheckParentColumnsForForeignKey(cell, cells, relationEndForColumns, ref list))
					{
						flag2 = true;
						AssociationEndMember relationEndForColumns2 = ForeignConstraint.GetRelationEndForColumns(cell, keyFields);
						if (relationEndForColumns2 != null && relationEndForColumns != null && ForeignConstraint.FindEntitySetForColumnsMappedToEntityKeys(cells, keyFields).Count > 0)
						{
							flag = true;
							this.CheckConstraintWhenParentChildMapped(cell, errorLog, relationEndForColumns, config);
							break;
						}
						if (relationEndForColumns != null)
						{
							AssociationSet associationSet = (AssociationSet)cell.CQuery.Extent;
							MetadataHelper.GetEntitySetAtEnd(associationSet, relationEndForColumns);
							flag = ForeignConstraint.CheckConstraintWhenOnlyParentMapped(associationSet, relationEndForColumns, childRewriter, parentRewriter);
							if (flag)
							{
								break;
							}
						}
					}
				}
			}
			if (!flag2)
			{
				foreach (ErrorLog.Record record in list)
				{
					errorLog.AddEntry(record);
				}
				return;
			}
			if (!flag)
			{
				string message = Strings.ViewGen_Foreign_Key_Missing_Relationship_Mapping(this.ToUserString());
				IEnumerable<LeftCellWrapper> wrappersFromContext = ForeignConstraint.GetWrappersFromContext(viewgenContext2, this.ParentTable);
				IEnumerable<LeftCellWrapper> wrappersFromContext2 = ForeignConstraint.GetWrappersFromContext(viewgenContext, this.ChildTable);
				Set<LeftCellWrapper> set = new Set<LeftCellWrapper>(wrappersFromContext);
				set.AddRange(wrappersFromContext2);
				ErrorLog.Record record2 = new ErrorLog.Record(ViewGenErrorCode.ForeignKeyMissingRelationshipMapping, message, set, string.Empty);
				errorLog.AddEntry(record2);
			}
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x000D27F0 File Offset: 0x000D09F0
		private bool CheckIfConstraintMappedToForeignKeyAssociation(QueryRewriter childRewriter, QueryRewriter parentRewriter, Set<Cell> cells)
		{
			ViewgenContext viewgenContext = childRewriter.ViewgenContext;
			ViewgenContext viewgenContext2 = parentRewriter.ViewgenContext;
			List<Set<EdmProperty>> list = new List<Set<EdmProperty>>();
			List<Set<EdmProperty>> list2 = new List<Set<EdmProperty>>();
			foreach (Cell cell in cells)
			{
				if (cell.CQuery.Extent.BuiltInTypeKind != BuiltInTypeKind.AssociationSet)
				{
					Set<EdmProperty> cslotsForTableColumns = cell.GetCSlotsForTableColumns(this.ChildColumns);
					if (cslotsForTableColumns != null && cslotsForTableColumns.Count != 0)
					{
						list.Add(cslotsForTableColumns);
					}
					Set<EdmProperty> cslotsForTableColumns2 = cell.GetCSlotsForTableColumns(this.ParentColumns);
					if (cslotsForTableColumns2 != null && cslotsForTableColumns2.Count != 0)
					{
						list2.Add(cslotsForTableColumns2);
					}
				}
			}
			if (list.Count != 0 && list2.Count != 0)
			{
				IEnumerable<AssociationType> enumerable = from it in viewgenContext.EntityContainerMapping.EdmEntityContainer.BaseEntitySets.OfType<AssociationSet>()
				where it.ElementType.IsForeignKey
				select it.ElementType;
				foreach (AssociationType associationType in enumerable)
				{
					ReferentialConstraint refConstraint = associationType.ReferentialConstraints.FirstOrDefault<ReferentialConstraint>();
					IEnumerable<Set<EdmProperty>> enumerable2 = from it in list
					where it.SetEquals(new Set<EdmProperty>(refConstraint.ToProperties))
					select it;
					IEnumerable<Set<EdmProperty>> enumerable3 = from it in list2
					where it.SetEquals(new Set<EdmProperty>(refConstraint.FromProperties))
					select it;
					if (enumerable2.Count<Set<EdmProperty>>() != 0 && enumerable3.Count<Set<EdmProperty>>() != 0)
					{
						foreach (Set<EdmProperty> properties in enumerable3)
						{
							Set<int> propertyIndexes = ForeignConstraint.GetPropertyIndexes(properties, refConstraint.FromProperties);
							foreach (Set<EdmProperty> properties2 in enumerable2)
							{
								Set<int> propertyIndexes2 = ForeignConstraint.GetPropertyIndexes(properties2, refConstraint.ToProperties);
								if (propertyIndexes2.SequenceEqual(propertyIndexes))
								{
									return true;
								}
							}
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x000D2A94 File Offset: 0x000D0C94
		private static Set<int> GetPropertyIndexes(IEnumerable<EdmProperty> properties1, ReadOnlyMetadataCollection<EdmProperty> properties2)
		{
			Set<int> set = new Set<int>();
			foreach (EdmProperty value in properties1)
			{
				set.Add(properties2.IndexOf(value));
			}
			return set;
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x000D2AEC File Offset: 0x000D0CEC
		private static bool CheckConstraintWhenOnlyParentMapped(AssociationSet assocSet, AssociationEndMember endMember, QueryRewriter childRewriter, QueryRewriter parentRewriter)
		{
			ViewgenContext viewgenContext = childRewriter.ViewgenContext;
			ViewgenContext viewgenContext2 = parentRewriter.ViewgenContext;
			CellTreeNode basicView = parentRewriter.BasicView;
			RoleBoolean literal = new RoleBoolean(assocSet.AssociationSetEnds[endMember.Name]);
			BoolExpression whereClause = basicView.RightFragmentQuery.Condition.Create(literal);
			FragmentQuery q = FragmentQuery.Create(basicView.RightFragmentQuery.Attributes, whereClause);
			FragmentQueryProcessor fragmentQueryProcessor = FragmentQueryProcessor.Merge(viewgenContext.RightFragmentQP, viewgenContext2.RightFragmentQP);
			return fragmentQueryProcessor.IsContainedIn(q, basicView.RightFragmentQuery);
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x000D2B74 File Offset: 0x000D0D74
		private bool CheckConstraintWhenParentChildMapped(Cell cell, ErrorLog errorLog, AssociationEndMember parentEnd, ConfigViewGenerator config)
		{
			bool flag = true;
			if (parentEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many)
			{
				string message = Strings.ViewGen_Foreign_Key_UpperBound_MustBeOne(this.ToUserString(), cell.CQuery.Extent.Name, parentEnd.Name);
				ErrorLog.Record record = new ErrorLog.Record(ViewGenErrorCode.ForeignKeyUpperBoundMustBeOne, message, cell, string.Empty);
				errorLog.AddEntry(record);
				flag = false;
			}
			if (!MemberPath.AreAllMembersNullable(this.ChildColumns) && parentEnd.RelationshipMultiplicity != RelationshipMultiplicity.One)
			{
				string message2 = Strings.ViewGen_Foreign_Key_LowerBound_MustBeOne(this.ToUserString(), cell.CQuery.Extent.Name, parentEnd.Name);
				ErrorLog.Record record2 = new ErrorLog.Record(ViewGenErrorCode.ForeignKeyLowerBoundMustBeOne, message2, cell, string.Empty);
				errorLog.AddEntry(record2);
				flag = false;
			}
			if (config.IsNormalTracing && flag)
			{
				Trace.WriteLine("Foreign key mapped to relationship " + cell.CQuery.Extent.Name);
			}
			return flag;
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x000D2C4C File Offset: 0x000D0E4C
		private bool CheckParentColumnsForForeignKey(Cell cell, IEnumerable<Cell> cells, AssociationEndMember parentEnd, ref List<ErrorLog.Record> errorList)
		{
			AssociationSet associationSet = (AssociationSet)cell.CQuery.Extent;
			EntitySet entitySetAtEnd = MetadataHelper.GetEntitySetAtEnd(associationSet, parentEnd);
			IList<EntitySet> list = ForeignConstraint.FindEntitySetForColumnsMappedToEntityKeys(cells, this.ParentColumns);
			if (!list.Contains(entitySetAtEnd))
			{
				if (errorList == null)
				{
					errorList = new List<ErrorLog.Record>();
				}
				string message = Strings.ViewGen_Foreign_Key_ParentTable_NotMappedToEnd(this.ToUserString(), this.ChildTable.Name, cell.CQuery.Extent.Name, parentEnd.Name, this.ParentTable.Name, entitySetAtEnd.Name);
				ErrorLog.Record item = new ErrorLog.Record(ViewGenErrorCode.ForeignKeyParentTableNotMappedToEnd, message, cell, string.Empty);
				errorList.Add(item);
				return false;
			}
			return true;
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x000D2CF4 File Offset: 0x000D0EF4
		private static IList<EntitySet> FindEntitySetForColumnsMappedToEntityKeys(IEnumerable<Cell> cells, IEnumerable<MemberPath> tableColumns)
		{
			List<EntitySet> list = new List<EntitySet>();
			foreach (Cell cell in cells)
			{
				CellQuery cquery = cell.CQuery;
				if (!(cquery.Extent is AssociationSet))
				{
					Set<EdmProperty> cslotsForTableColumns = cell.GetCSlotsForTableColumns(tableColumns);
					if (cslotsForTableColumns != null)
					{
						EntitySet entitySet = (EntitySet)cquery.Extent;
						List<EdmProperty> list2 = new List<EdmProperty>();
						foreach (EdmMember edmMember in entitySet.ElementType.KeyMembers)
						{
							EdmProperty item = (EdmProperty)edmMember;
							list2.Add(item);
						}
						Set<EdmProperty> set = new Set<EdmProperty>(list2).MakeReadOnly();
						if (set.SetEquals(cslotsForTableColumns))
						{
							list.Add(entitySet);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x000D2DF0 File Offset: 0x000D0FF0
		private static AssociationEndMember GetRelationEndForColumns(Cell cell, IEnumerable<MemberPath> columns)
		{
			if (cell.CQuery.Extent is EntitySet)
			{
				return null;
			}
			AssociationSet associationSet = (AssociationSet)cell.CQuery.Extent;
			foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
			{
				AssociationEndMember correspondingAssociationEndMember = associationSetEnd.CorrespondingAssociationEndMember;
				MemberPath prefix = new MemberPath(associationSet, correspondingAssociationEndMember);
				ExtentKey primaryKeyForEntityType = ExtentKey.GetPrimaryKeyForEntityType(prefix, associationSetEnd.EntitySet.ElementType);
				List<int> projectedPositions = cell.CQuery.GetProjectedPositions(primaryKeyForEntityType.KeyFields);
				if (projectedPositions != null)
				{
					List<int> projectedPositions2 = cell.SQuery.GetProjectedPositions(columns, projectedPositions);
					if (projectedPositions2 != null && Helpers.IsSetEqual<int>(projectedPositions2, projectedPositions, EqualityComparer<int>.Default))
					{
						return correspondingAssociationEndMember;
					}
				}
			}
			return null;
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x000D2ECC File Offset: 0x000D10CC
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "extent")]
		private static List<LeftCellWrapper> GetWrappersFromContext(ViewgenContext context, EntitySetBase extent)
		{
			List<LeftCellWrapper> result;
			if (context == null)
			{
				result = new List<LeftCellWrapper>();
			}
			else
			{
				result = context.AllWrappersForExtent;
			}
			return result;
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x000D2EEC File Offset: 0x000D10EC
		private bool CheckForeignKeyColumnOrder(Set<Cell> cells, ErrorLog errorLog)
		{
			List<Cell> list = new List<Cell>();
			List<Cell> list2 = new List<Cell>();
			foreach (Cell cell in cells)
			{
				if (cell.SQuery.Extent.Equals(this.ChildTable))
				{
					list2.Add(cell);
				}
				if (cell.SQuery.Extent.Equals(this.ParentTable))
				{
					list.Add(cell);
				}
			}
			foreach (Cell cell2 in list2)
			{
				List<List<int>> slotNumsForColumns = ForeignConstraint.GetSlotNumsForColumns(cell2, this.ChildColumns);
				if (slotNumsForColumns.Count != 0)
				{
					List<MemberPath> list3 = null;
					List<MemberPath> list4 = null;
					Cell cell3 = null;
					foreach (List<int> list5 in slotNumsForColumns)
					{
						list3 = new List<MemberPath>(list5.Count);
						foreach (int slotNum in list5)
						{
							MemberProjectedSlot memberProjectedSlot = (MemberProjectedSlot)cell2.CQuery.ProjectedSlotAt(slotNum);
							list3.Add(memberProjectedSlot.MemberPath);
						}
						foreach (Cell cell4 in list)
						{
							List<List<int>> slotNumsForColumns2 = ForeignConstraint.GetSlotNumsForColumns(cell4, this.ParentColumns);
							if (slotNumsForColumns2.Count != 0)
							{
								foreach (List<int> list6 in slotNumsForColumns2)
								{
									list4 = new List<MemberPath>(list6.Count);
									foreach (int slotNum2 in list6)
									{
										MemberProjectedSlot memberProjectedSlot2 = (MemberProjectedSlot)cell4.CQuery.ProjectedSlotAt(slotNum2);
										list4.Add(memberProjectedSlot2.MemberPath);
									}
									if (list3.Count == list4.Count)
									{
										bool flag = false;
										int num = 0;
										while (num < list3.Count && !flag)
										{
											MemberPath memberPath = list4[num];
											MemberPath memberPath2 = list3[num];
											if (!memberPath.LeafEdmMember.Equals(memberPath2.LeafEdmMember))
											{
												if (memberPath.IsEquivalentViaRefConstraint(memberPath2))
												{
													return true;
												}
												flag = true;
											}
											num++;
										}
										if (!flag)
										{
											return true;
										}
										cell3 = cell4;
									}
								}
							}
						}
					}
					string message = Strings.ViewGen_Foreign_Key_ColumnOrder_Incorrect(this.ToUserString(), MemberPath.PropertiesToUserString(this.ChildColumns, false), this.ChildTable.Name, MemberPath.PropertiesToUserString(list3, false), cell2.CQuery.Extent.Name, MemberPath.PropertiesToUserString(this.ParentColumns, false), this.ParentTable.Name, MemberPath.PropertiesToUserString(list4, false), cell3.CQuery.Extent.Name);
					ErrorLog.Record record = new ErrorLog.Record(ViewGenErrorCode.ForeignKeyColumnOrderIncorrect, message, new Cell[]
					{
						cell3,
						cell2
					}, string.Empty);
					errorLog.AddEntry(record);
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x000D3300 File Offset: 0x000D1500
		private static List<List<int>> GetSlotNumsForColumns(Cell cell, IEnumerable<MemberPath> columns)
		{
			List<List<int>> list = new List<List<int>>();
			AssociationSet associationSet = cell.CQuery.Extent as AssociationSet;
			if (associationSet != null)
			{
				using (ReadOnlyMetadataCollection<AssociationSetEnd>.Enumerator enumerator = associationSet.AssociationSetEnds.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AssociationSetEnd associationSetEnd = enumerator.Current;
						List<int> associationEndSlots = cell.CQuery.GetAssociationEndSlots(associationSetEnd.CorrespondingAssociationEndMember);
						List<int> projectedPositions = cell.SQuery.GetProjectedPositions(columns, associationEndSlots);
						if (projectedPositions != null)
						{
							list.Add(projectedPositions);
						}
					}
					return list;
				}
			}
			List<int> projectedPositions2 = cell.SQuery.GetProjectedPositions(columns);
			if (projectedPositions2 != null)
			{
				list.Add(projectedPositions2);
			}
			return list;
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x000D33B0 File Offset: 0x000D15B0
		private bool IsForeignKeySuperSetOfPrimaryKeyInChildTable()
		{
			bool result = true;
			foreach (EdmMember edmMember in this.m_childTable.ElementType.KeyMembers)
			{
				EdmProperty obj = (EdmProperty)edmMember;
				bool flag = false;
				foreach (MemberPath memberPath in this.m_childColumns)
				{
					if (memberPath.LeafEdmMember.Equals(obj))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x000D3468 File Offset: 0x000D1668
		private bool IsConstraintRelevantForCells(IEnumerable<Cell> cells)
		{
			bool result = false;
			foreach (Cell cell in cells)
			{
				EntitySetBase extent = cell.SQuery.Extent;
				if (extent.Equals(this.m_parentTable) || extent.Equals(this.m_childTable))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x000D34D8 File Offset: 0x000D16D8
		internal string ToUserString()
		{
			string p = MemberPath.PropertiesToUserString(this.m_childColumns, false);
			string p2 = MemberPath.PropertiesToUserString(this.m_parentColumns, false);
			return Strings.ViewGen_Foreign_Key(this.m_fKeySet.Name, this.m_childTable.Name, p, this.m_parentTable.Name, p2);
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x000D3529 File Offset: 0x000D1729
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append(this.m_fKeySet.Name + ": ");
			builder.Append(this.ToUserString());
		}

		// Token: 0x04001004 RID: 4100
		private readonly AssociationSet m_fKeySet;

		// Token: 0x04001005 RID: 4101
		private readonly EntitySet m_parentTable;

		// Token: 0x04001006 RID: 4102
		private readonly EntitySet m_childTable;

		// Token: 0x04001007 RID: 4103
		private readonly List<MemberPath> m_parentColumns;

		// Token: 0x04001008 RID: 4104
		private readonly List<MemberPath> m_childColumns;
	}
}
