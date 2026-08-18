using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200066E RID: 1646
	internal class ForeignKeyConstraint
	{
		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06004046 RID: 16454 RVA: 0x00126F04 File Offset: 0x00125104
		internal List<string> ParentKeys
		{
			get
			{
				return this.m_parentKeys;
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06004047 RID: 16455 RVA: 0x00126F0C File Offset: 0x0012510C
		internal List<string> ChildKeys
		{
			get
			{
				return this.m_childKeys;
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06004048 RID: 16456 RVA: 0x00126F14 File Offset: 0x00125114
		internal ExtentPair Pair
		{
			get
			{
				return this.m_extentPair;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06004049 RID: 16457 RVA: 0x00126F1C File Offset: 0x0012511C
		internal RelationshipMultiplicity ChildMultiplicity
		{
			get
			{
				return this.m_constraint.ToRole.RelationshipMultiplicity;
			}
		}

		// Token: 0x0600404A RID: 16458 RVA: 0x00126F2E File Offset: 0x0012512E
		internal bool GetParentProperty(string childPropertyName, out string parentPropertyName)
		{
			this.BuildKeyMap();
			return this.m_keyMap.TryGetValue(childPropertyName, out parentPropertyName);
		}

		// Token: 0x0600404B RID: 16459 RVA: 0x00126F44 File Offset: 0x00125144
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		internal ForeignKeyConstraint(RelationshipSet relationshipSet, ReferentialConstraint constraint)
		{
			AssociationSet associationSet = relationshipSet as AssociationSet;
			AssociationEndMember associationEndMember = constraint.FromRole as AssociationEndMember;
			AssociationEndMember associationEndMember2 = constraint.ToRole as AssociationEndMember;
			if (associationSet == null || associationEndMember == null || associationEndMember2 == null)
			{
				throw new NotSupportedException();
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

		// Token: 0x0600404C RID: 16460 RVA: 0x00127084 File Offset: 0x00125284
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x040017F4 RID: 6132
		private readonly ExtentPair m_extentPair;

		// Token: 0x040017F5 RID: 6133
		private readonly List<string> m_parentKeys;

		// Token: 0x040017F6 RID: 6134
		private readonly List<string> m_childKeys;

		// Token: 0x040017F7 RID: 6135
		private readonly ReferentialConstraint m_constraint;

		// Token: 0x040017F8 RID: 6136
		private Dictionary<string, string> m_keyMap;
	}
}
