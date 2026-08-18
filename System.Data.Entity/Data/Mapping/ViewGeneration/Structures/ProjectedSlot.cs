using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.CqlGeneration;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002B7 RID: 695
	internal abstract class ProjectedSlot : InternalBase, IEquatable<ProjectedSlot>
	{
		// Token: 0x06002971 RID: 10609 RVA: 0x000A1177 File Offset: 0x0009F377
		protected virtual bool IsEqualTo(ProjectedSlot right)
		{
			return base.Equals(right);
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x0009B148 File Offset: 0x00099348
		protected virtual int GetHash()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x000A1180 File Offset: 0x0009F380
		public bool Equals(ProjectedSlot right)
		{
			return ProjectedSlot.EqualityComparer.Equals(this, right);
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x000A1190 File Offset: 0x0009F390
		public override bool Equals(object obj)
		{
			ProjectedSlot right = obj as ProjectedSlot;
			return obj != null && this.Equals(right);
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x000A11B0 File Offset: 0x0009F3B0
		public override int GetHashCode()
		{
			return ProjectedSlot.EqualityComparer.GetHashCode(this);
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x000A11C0 File Offset: 0x0009F3C0
		internal virtual ProjectedSlot DeepQualify(CqlBlock block)
		{
			return new QualifiedSlot(block, this);
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x000A11D6 File Offset: 0x0009F3D6
		internal virtual string GetCqlFieldAlias(MemberPath outputMember)
		{
			return outputMember.CqlFieldAlias;
		}

		// Token: 0x06002978 RID: 10616
		internal abstract StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias, int indentLevel);

		// Token: 0x06002979 RID: 10617
		internal abstract DbExpression AsCqt(DbExpression row, MemberPath outputMember);

		// Token: 0x0600297A RID: 10618 RVA: 0x000A11E0 File Offset: 0x0009F3E0
		internal static bool TryMergeRemapSlots(ProjectedSlot[] slots1, ProjectedSlot[] slots2, out ProjectedSlot[] result)
		{
			ProjectedSlot[] array;
			if (!ProjectedSlot.TryMergeSlots(slots1, slots2, out array))
			{
				result = null;
				return false;
			}
			result = array;
			return true;
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x000A1204 File Offset: 0x0009F404
		private static bool TryMergeSlots(ProjectedSlot[] slots1, ProjectedSlot[] slots2, out ProjectedSlot[] slots)
		{
			slots = new ProjectedSlot[slots1.Length];
			for (int i = 0; i < slots.Length; i++)
			{
				ProjectedSlot projectedSlot = slots1[i];
				ProjectedSlot projectedSlot2 = slots2[i];
				if (projectedSlot == null)
				{
					slots[i] = projectedSlot2;
				}
				else if (projectedSlot2 == null)
				{
					slots[i] = projectedSlot;
				}
				else
				{
					MemberProjectedSlot memberProjectedSlot = projectedSlot as MemberProjectedSlot;
					MemberProjectedSlot memberProjectedSlot2 = projectedSlot2 as MemberProjectedSlot;
					if (memberProjectedSlot != null && memberProjectedSlot2 != null && !ProjectedSlot.EqualityComparer.Equals(memberProjectedSlot, memberProjectedSlot2))
					{
						return false;
					}
					ProjectedSlot projectedSlot3 = (memberProjectedSlot != null) ? projectedSlot : projectedSlot2;
					slots[i] = projectedSlot3;
				}
			}
			return true;
		}

		// Token: 0x04001282 RID: 4738
		internal static readonly IEqualityComparer<ProjectedSlot> EqualityComparer = new ProjectedSlot.Comparer();

		// Token: 0x02000607 RID: 1543
		private sealed class Comparer : IEqualityComparer<ProjectedSlot>
		{
			// Token: 0x06004264 RID: 16996 RVA: 0x000F12E3 File Offset: 0x000EF4E3
			public bool Equals(ProjectedSlot left, ProjectedSlot right)
			{
				return left == right || (left != null && right != null && left.IsEqualTo(right));
			}

			// Token: 0x06004265 RID: 16997 RVA: 0x000F12FA File Offset: 0x000EF4FA
			public int GetHashCode(ProjectedSlot key)
			{
				EntityUtil.CheckArgumentNull<ProjectedSlot>(key, "key");
				return key.GetHash();
			}
		}
	}
}
