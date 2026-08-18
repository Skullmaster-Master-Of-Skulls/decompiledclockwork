using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Mapping.ViewGeneration
{
	// Token: 0x02000268 RID: 616
	internal sealed class CqlGenerator : InternalBase
	{
		// Token: 0x060025DC RID: 9692 RVA: 0x0008EB13 File Offset: 0x0008CD13
		internal CqlGenerator(CellTreeNode view, Dictionary<MemberPath, CaseStatement> caseStatements, CqlIdentifiers identifiers, MemberProjectionIndex projectedSlotMap, int numCellsInView, BoolExpression topLevelWhereClause, StorageMappingItemCollection mappingItemCollection)
		{
			this.m_view = view;
			this.m_caseStatements = caseStatements;
			this.m_projectedSlotMap = projectedSlotMap;
			this.m_numBools = numCellsInView;
			this.m_topLevelWhereClause = topLevelWhereClause;
			this.m_identifiers = identifiers;
			this.m_mappingItemCollection = mappingItemCollection;
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060025DD RID: 9693 RVA: 0x0008EB50 File Offset: 0x0008CD50
		private int TotalSlots
		{
			get
			{
				return this.m_projectedSlotMap.Count + this.m_numBools;
			}
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x0008EB64 File Offset: 0x0008CD64
		internal string GenerateEsql()
		{
			CqlBlock cqlBlock = this.GenerateCqlBlockTree();
			StringBuilder stringBuilder = new StringBuilder(1024);
			cqlBlock.AsEsql(stringBuilder, true, 1);
			return stringBuilder.ToString();
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x0008EB94 File Offset: 0x0008CD94
		internal DbQueryCommandTree GenerateCqt()
		{
			CqlBlock cqlBlock = this.GenerateCqlBlockTree();
			DbExpression query = cqlBlock.AsCqt(true);
			return DbQueryCommandTree.FromValidExpression(this.m_mappingItemCollection.Workspace, DataSpace.SSpace, query);
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x0008EBC4 File Offset: 0x0008CDC4
		private CqlBlock GenerateCqlBlockTree()
		{
			bool[] requiredSlots = this.GetRequiredSlots();
			List<WithRelationship> withRelationships = new List<WithRelationship>();
			CqlBlock viewBlock = this.m_view.ToCqlBlock(requiredSlots, this.m_identifiers, ref this.m_currentBlockNum, ref withRelationships);
			foreach (CaseStatement caseStatement in this.m_caseStatements.Values)
			{
				caseStatement.Simplify();
			}
			return this.ConstructCaseBlocks(viewBlock, withRelationships);
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x0008EC50 File Offset: 0x0008CE50
		private bool[] GetRequiredSlots()
		{
			bool[] array = new bool[this.TotalSlots];
			foreach (CaseStatement caseStatement in this.m_caseStatements.Values)
			{
				this.GetRequiredSlotsForCaseMember(caseStatement.MemberPath, array);
			}
			for (int i = this.TotalSlots - this.m_numBools; i < this.TotalSlots; i++)
			{
				array[i] = true;
			}
			foreach (CaseStatement caseStatement2 in this.m_caseStatements.Values)
			{
				bool flag = !caseStatement2.MemberPath.IsPartOfKey && !caseStatement2.DependsOnMemberValue;
				if (flag)
				{
					array[this.m_projectedSlotMap.IndexOf(caseStatement2.MemberPath)] = false;
				}
			}
			return array;
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x0008ED54 File Offset: 0x0008CF54
		private CqlBlock ConstructCaseBlocks(CqlBlock viewBlock, IEnumerable<WithRelationship> withRelationships)
		{
			bool[] array = new bool[this.TotalSlots];
			array[0] = true;
			this.m_topLevelWhereClause.GetRequiredSlots(this.m_projectedSlotMap, array);
			return this.ConstructCaseBlocks(viewBlock, 0, array, withRelationships);
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x0008ED90 File Offset: 0x0008CF90
		private CqlBlock ConstructCaseBlocks(CqlBlock viewBlock, int startSlotNum, bool[] parentRequiredSlots, IEnumerable<WithRelationship> withRelationships)
		{
			int count = this.m_projectedSlotMap.Count;
			int num = this.FindNextCaseStatementSlot(startSlotNum, parentRequiredSlots, count);
			if (num == -1)
			{
				return viewBlock;
			}
			MemberPath memberPath = this.m_projectedSlotMap[num];
			bool[] array = new bool[this.TotalSlots];
			this.GetRequiredSlotsForCaseMember(memberPath, array);
			for (int i = 0; i < this.TotalSlots; i++)
			{
				if (parentRequiredSlots[i])
				{
					array[i] = true;
				}
			}
			CaseStatement caseStatement = this.m_caseStatements[memberPath];
			array[num] = caseStatement.DependsOnMemberValue;
			CqlBlock cqlBlock = this.ConstructCaseBlocks(viewBlock, num + 1, array, null);
			SlotInfo[] array2 = this.CreateSlotInfosForCaseStatement(parentRequiredSlots, num, cqlBlock, caseStatement, withRelationships);
			this.m_currentBlockNum++;
			BoolExpression whereClause = (startSlotNum == 0) ? this.m_topLevelWhereClause : BoolExpression.True;
			if (startSlotNum == 0)
			{
				for (int j = 1; j < array2.Length; j++)
				{
					array2[j].ResetIsRequiredByParent();
				}
			}
			return new CaseCqlBlock(array2, num, cqlBlock, whereClause, this.m_identifiers, this.m_currentBlockNum);
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x0008EE8C File Offset: 0x0008D08C
		private SlotInfo[] CreateSlotInfosForCaseStatement(bool[] parentRequiredSlots, int foundSlot, CqlBlock childBlock, CaseStatement thisCaseStatement, IEnumerable<WithRelationship> withRelationships)
		{
			int num = childBlock.Slots.Count - this.TotalSlots;
			SlotInfo[] array = new SlotInfo[this.TotalSlots + num];
			for (int i = 0; i < this.TotalSlots; i++)
			{
				bool flag = childBlock.IsProjected(i);
				bool flag2 = parentRequiredSlots[i];
				ProjectedSlot slotValue = childBlock.SlotValue(i);
				MemberPath outputMemberPath = this.GetOutputMemberPath(i);
				if (i == foundSlot)
				{
					CaseStatement statement = thisCaseStatement.DeepQualify(childBlock);
					slotValue = new CaseStatementProjectedSlot(statement, withRelationships);
					flag = true;
				}
				else if (flag && flag2)
				{
					slotValue = childBlock.QualifySlotWithBlockAlias(i);
				}
				SlotInfo slotInfo = new SlotInfo(flag2 && flag, flag, slotValue, outputMemberPath);
				array[i] = slotInfo;
			}
			for (int j = this.TotalSlots; j < this.TotalSlots + num; j++)
			{
				QualifiedSlot slotValue2 = childBlock.QualifySlotWithBlockAlias(j);
				array[j] = new SlotInfo(true, true, slotValue2, childBlock.MemberPath(j));
			}
			return array;
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x0008EF68 File Offset: 0x0008D168
		private int FindNextCaseStatementSlot(int startSlotNum, bool[] parentRequiredSlots, int numMembers)
		{
			int result = -1;
			for (int i = startSlotNum; i < numMembers; i++)
			{
				MemberPath key = this.m_projectedSlotMap[i];
				if (parentRequiredSlots[i] && this.m_caseStatements.ContainsKey(key))
				{
					result = i;
					break;
				}
			}
			return result;
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x0008EFA8 File Offset: 0x0008D1A8
		private void GetRequiredSlotsForCaseMember(MemberPath caseMemberPath, bool[] requiredSlots)
		{
			CaseStatement caseStatement = this.m_caseStatements[caseMemberPath];
			bool flag = false;
			foreach (CaseStatement.WhenThen whenThen in caseStatement.Clauses)
			{
				whenThen.Condition.GetRequiredSlots(this.m_projectedSlotMap, requiredSlots);
				ProjectedSlot value = whenThen.Value;
				if (!(value is ConstantProjectedSlot))
				{
					flag = true;
				}
			}
			EdmType edmType = caseMemberPath.EdmType;
			if (Helper.IsEntityType(edmType) || Helper.IsComplexType(edmType))
			{
				using (IEnumerator<EdmType> enumerator2 = caseStatement.InstantiatedTypes.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						EdmType edmType2 = enumerator2.Current;
						foreach (object obj in Helper.GetAllStructuralMembers(edmType2))
						{
							EdmMember child = (EdmMember)obj;
							int slotIndex = this.GetSlotIndex(caseMemberPath, child);
							requiredSlots[slotIndex] = true;
						}
					}
					return;
				}
			}
			if (caseMemberPath.IsScalarType())
			{
				if (flag)
				{
					int num = this.m_projectedSlotMap.IndexOf(caseMemberPath);
					requiredSlots[num] = true;
					return;
				}
			}
			else
			{
				if (Helper.IsAssociationType(edmType))
				{
					AssociationSet associationSet = (AssociationSet)caseMemberPath.Extent;
					AssociationType elementType = associationSet.ElementType;
					using (ReadOnlyMetadataCollection<AssociationEndMember>.Enumerator enumerator4 = elementType.AssociationEndMembers.GetEnumerator())
					{
						while (enumerator4.MoveNext())
						{
							AssociationEndMember child2 = enumerator4.Current;
							int slotIndex2 = this.GetSlotIndex(caseMemberPath, child2);
							requiredSlots[slotIndex2] = true;
						}
						return;
					}
				}
				RefType refType = edmType as RefType;
				EntityTypeBase elementType2 = refType.ElementType;
				EntitySet entitySetAtEnd = MetadataHelper.GetEntitySetAtEnd((AssociationSet)caseMemberPath.Extent, (AssociationEndMember)caseMemberPath.LeafEdmMember);
				foreach (EdmMember child3 in elementType2.KeyMembers)
				{
					int slotIndex3 = this.GetSlotIndex(caseMemberPath, child3);
					requiredSlots[slotIndex3] = true;
				}
			}
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x0008F1E8 File Offset: 0x0008D3E8
		private MemberPath GetOutputMemberPath(int slotNum)
		{
			return this.m_projectedSlotMap.GetMemberPath(slotNum, this.TotalSlots - this.m_projectedSlotMap.Count);
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x0008F208 File Offset: 0x0008D408
		private int GetSlotIndex(MemberPath member, EdmMember child)
		{
			MemberPath member2 = new MemberPath(member, child);
			return this.m_projectedSlotMap.IndexOf(member2);
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x0008F22C File Offset: 0x0008D42C
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append("View: ");
			this.m_view.ToCompactString(builder);
			builder.Append("ProjectedSlotMap: ");
			this.m_projectedSlotMap.ToCompactString(builder);
			builder.Append("Case statements: ");
			foreach (MemberPath key in this.m_caseStatements.Keys)
			{
				CaseStatement caseStatement = this.m_caseStatements[key];
				caseStatement.ToCompactString(builder);
				builder.AppendLine();
			}
		}

		// Token: 0x04001175 RID: 4469
		private readonly CellTreeNode m_view;

		// Token: 0x04001176 RID: 4470
		private readonly Dictionary<MemberPath, CaseStatement> m_caseStatements;

		// Token: 0x04001177 RID: 4471
		private MemberProjectionIndex m_projectedSlotMap;

		// Token: 0x04001178 RID: 4472
		private readonly int m_numBools;

		// Token: 0x04001179 RID: 4473
		private int m_currentBlockNum;

		// Token: 0x0400117A RID: 4474
		private readonly BoolExpression m_topLevelWhereClause;

		// Token: 0x0400117B RID: 4475
		private readonly CqlIdentifiers m_identifiers;

		// Token: 0x0400117C RID: 4476
		private readonly StorageMappingItemCollection m_mappingItemCollection;
	}
}
