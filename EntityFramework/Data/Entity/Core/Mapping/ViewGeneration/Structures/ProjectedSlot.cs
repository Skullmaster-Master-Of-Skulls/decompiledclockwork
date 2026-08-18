using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000422 RID: 1058
	internal abstract class ProjectedSlot : InternalBase, IEquatable<ProjectedSlot>
	{
		// Token: 0x06002703 RID: 9987 RVA: 0x000BDACF File Offset: 0x000BBCCF
		protected virtual bool IsEqualTo(ProjectedSlot right)
		{
			return base.Equals(right);
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x000BDAD8 File Offset: 0x000BBCD8
		protected virtual int GetHash()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x000BDAE0 File Offset: 0x000BBCE0
		public bool Equals(ProjectedSlot right)
		{
			return ProjectedSlot.EqualityComparer.Equals(this, right);
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x000BDAF0 File Offset: 0x000BBCF0
		public override bool Equals(object obj)
		{
			ProjectedSlot right = obj as ProjectedSlot;
			return obj != null && this.Equals(right);
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x000BDB10 File Offset: 0x000BBD10
		public override int GetHashCode()
		{
			return ProjectedSlot.EqualityComparer.GetHashCode(this);
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x000BDB20 File Offset: 0x000BBD20
		internal virtual ProjectedSlot DeepQualify(CqlBlock block)
		{
			return new QualifiedSlot(block, this);
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x000BDB36 File Offset: 0x000BBD36
		internal virtual string GetCqlFieldAlias(MemberPath outputMember)
		{
			return outputMember.CqlFieldAlias;
		}

		// Token: 0x0600270A RID: 9994
		internal abstract StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias, int indentLevel);

		// Token: 0x0600270B RID: 9995
		internal abstract DbExpression AsCqt(DbExpression row, MemberPath outputMember);

		// Token: 0x0600270C RID: 9996 RVA: 0x000BDB40 File Offset: 0x000BBD40
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

		// Token: 0x0600270D RID: 9997 RVA: 0x000BDB64 File Offset: 0x000BBD64
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

		// Token: 0x04000EAB RID: 3755
		internal static readonly IEqualityComparer<ProjectedSlot> EqualityComparer = new ProjectedSlot.Comparer();

		// Token: 0x02000423 RID: 1059
		private sealed class Comparer : IEqualityComparer<ProjectedSlot>
		{
			// Token: 0x06002710 RID: 10000 RVA: 0x000BDBF1 File Offset: 0x000BBDF1
			public bool Equals(ProjectedSlot left, ProjectedSlot right)
			{
				return object.ReferenceEquals(left, right) || (left != null && right != null && left.IsEqualTo(right));
			}

			// Token: 0x06002711 RID: 10001 RVA: 0x000BDC0D File Offset: 0x000BBE0D
			public int GetHashCode(ProjectedSlot key)
			{
				return key.GetHash();
			}
		}
	}
}
