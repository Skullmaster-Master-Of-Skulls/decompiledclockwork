using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x02000431 RID: 1073
	internal sealed class CqlGenerator : InternalBase
	{
		// Token: 0x06002766 RID: 10086 RVA: 0x000BED44 File Offset: 0x000BCF44
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

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06002767 RID: 10087 RVA: 0x000BED81 File Offset: 0x000BCF81
		private int TotalSlots
		{
			get
			{
				return this.m_projectedSlotMap.Count + this.m_numBools;
			}
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x000BED98 File Offset: 0x000BCF98
		internal string GenerateEsql()
		{
			CqlBlock cqlBlock = this.GenerateCqlBlockTree();
			StringBuilder stringBuilder = new StringBuilder(1024);
			cqlBlock.AsEsql(stringBuilder, true, 1);
			return stringBuilder.ToString();
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x000BEDC8 File Offset: 0x000BCFC8
		internal DbQueryCommandTree GenerateCqt()
		{
			CqlBlock cqlBlock = this.GenerateCqlBlockTree();
			DbExpression query = cqlBlock.AsCqt(true);
			return DbQueryCommandTree.FromValidExpression(this.m_mappingItemCollection.Workspace, DataSpace.SSpace, query, true);
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x000BEDF8 File Offset: 0x000BCFF8
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

		// Token: 0x0600276B RID: 10091 RVA: 0x000BEE84 File Offset: 0x000BD084
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

		// Token: 0x0600276C RID: 10092 RVA: 0x000BEF88 File Offset: 0x000BD188
		private CqlBlock ConstructCaseBlocks(CqlBlock viewBlock, IEnumerable<WithRelationship> withRelationships)
		{
			bool[] array = new bool[this.TotalSlots];
			array[0] = true;
			this.m_topLevelWhereClause.GetRequiredSlots(this.m_projectedSlotMap, array);
			return this.ConstructCaseBlocks(viewBlock, 0, array, withRelationships);
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x000BEFC4 File Offset: 0x000BD1C4
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

		// Token: 0x0600276E RID: 10094 RVA: 0x000BF0C0 File Offset: 0x000BD2C0
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

		// Token: 0x0600276F RID: 10095 RVA: 0x000BF1A0 File Offset: 0x000BD3A0
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

		// Token: 0x06002770 RID: 10096 RVA: 0x000BF1E0 File Offset: 0x000BD3E0
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
				MetadataHelper.GetEntitySetAtEnd((AssociationSet)caseMemberPath.Extent, (AssociationEndMember)caseMemberPath.LeafEdmMember);
				foreach (EdmMember child3 in elementType2.KeyMembers)
				{
					int slotIndex3 = this.GetSlotIndex(caseMemberPath, child3);
					requiredSlots[slotIndex3] = true;
				}
			}
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x000BF420 File Offset: 0x000BD620
		private MemberPath GetOutputMemberPath(int slotNum)
		{
			return this.m_projectedSlotMap.GetMemberPath(slotNum, this.TotalSlots - this.m_projectedSlotMap.Count);
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x000BF440 File Offset: 0x000BD640
		private int GetSlotIndex(MemberPath member, EdmMember child)
		{
			MemberPath member2 = new MemberPath(member, child);
			return this.m_projectedSlotMap.IndexOf(member2);
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x000BF464 File Offset: 0x000BD664
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

		// Token: 0x04000ECD RID: 3789
		private readonly CellTreeNode m_view;

		// Token: 0x04000ECE RID: 3790
		private readonly Dictionary<MemberPath, CaseStatement> m_caseStatements;

		// Token: 0x04000ECF RID: 3791
		private readonly MemberProjectionIndex m_projectedSlotMap;

		// Token: 0x04000ED0 RID: 3792
		private readonly int m_numBools;

		// Token: 0x04000ED1 RID: 3793
		private int m_currentBlockNum;

		// Token: 0x04000ED2 RID: 3794
		private readonly BoolExpression m_topLevelWhereClause;

		// Token: 0x04000ED3 RID: 3795
		private readonly CqlIdentifiers m_identifiers;

		// Token: 0x04000ED4 RID: 3796
		private readonly StorageMappingItemCollection m_mappingItemCollection;
	}
}
