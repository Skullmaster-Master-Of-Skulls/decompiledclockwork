using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200004B RID: 75
	internal class ForeignKeyConstraint
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x00019DE6 File Offset: 0x00017FE6
		internal List<string> ParentKeys
		{
			get
			{
				return this.m_parentKeys;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00019DEE File Offset: 0x00017FEE
		internal List<string> ChildKeys
		{
			get
			{
				return this.m_childKeys;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x00019DF6 File Offset: 0x00017FF6
		internal ExtentPair Pair
		{
			get
			{
				return this.m_extentPair;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x00019DFE File Offset: 0x00017FFE
		internal RelationshipMultiplicity ChildMultiplicity
		{
			get
			{
				return this.m_constraint.ToRole.RelationshipMultiplicity;
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00019E10 File Offset: 0x00018010
		internal bool GetParentProperty(string childPropertyName, out string parentPropertyName)
		{
			this.BuildKeyMap();
			return this.m_keyMap.TryGetValue(childPropertyName, out parentPropertyName);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00019E28 File Offset: 0x00018028
		internal ForeignKeyConstraint(RelationshipType relType, RelationshipSet relationshipSet, ReferentialConstraint constraint)
		{
			AssociationSet associationSet = relationshipSet as AssociationSet;
			AssociationEndMember associationEndMember = constraint.FromRole as AssociationEndMember;
			AssociationEndMember associationEndMember2 = constraint.ToRole as AssociationEndMember;
			if (associationSet == null || associationEndMember == null || associationEndMember2 == null)
			{
				throw EntityUtil.NotSupported();
			}
			this.m_constraint = constraint;
			EntitySet entitySetAtEnd = MetadataHelper.GetEntitySetAtEnd(associationSet, associationEndMember);
			EntitySet entitySetAtEnd2 = MetadataHelper.GetEntitySetAtEnd(associationSet, associationEndMember2);
			this.m_extentPair = new ExtentPair(entitySetAtEnd, entitySetAtEnd2);
			this.m_childKeys = new List<string>();
			foreach (EdmProperty edmProperty in constraint.ToProperties)
			{
				this.m_childKeys.Add(edmProperty.Name);
			}
			this.m_parentKeys = new List<string>();
			foreach (EdmProperty edmProperty2 in constraint.FromProperties)
			{
				this.m_parentKeys.Add(edmProperty2.Name);
			}
			PlanCompiler.Assert(associationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne || RelationshipMultiplicity.One == associationEndMember.RelationshipMultiplicity, "from-end of relationship constraint cannot have multiplicity greater than 1");
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00019F68 File Offset: 0x00018168
		private void BuildKeyMap()
		{
			if (this.m_keyMap != null)
			{
				return;
			}
			this.m_keyMap = new Dictionary<string, string>();
			IEnumerator<EdmProperty> enumerator = this.m_constraint.FromProperties.GetEnumerator();
			IEnumerator<EdmProperty> enumerator2 = this.m_constraint.ToProperties.GetEnumerator();
			for (;;)
			{
				bool flag = !enumerator.MoveNext();
				bool flag2 = !enumerator2.MoveNext();
				PlanCompiler.Assert(flag == flag2, "key count mismatch");
				if (flag)
				{
					break;
				}
				this.m_keyMap[enumerator2.Current.Name] = enumerator.Current.Name;
			}
		}

		// Token: 0x0400076B RID: 1899
		private ExtentPair m_extentPair;

		// Token: 0x0400076C RID: 1900
		private List<string> m_parentKeys;

		// Token: 0x0400076D RID: 1901
		private List<string> m_childKeys;

		// Token: 0x0400076E RID: 1902
		private ReferentialConstraint m_constraint;

		// Token: 0x0400076F RID: 1903
		private Dictionary<string, string> m_keyMap;
	}
}
