using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D1C RID: 3356
	internal class TupleTypeInspector
	{
		// Token: 0x06007D20 RID: 32032 RVA: 0x001CB401 File Offset: 0x001C9601
		public TupleTypeInspector(IList<OlapGroupDescription> groupDescriptions)
		{
			this.groupDescriptions = groupDescriptions;
		}

		// Token: 0x06007D21 RID: 32033 RVA: 0x001CB410 File Offset: 0x001C9610
		public TupleType DetermineTupleType(IList<IOlapMember> tupleMembers)
		{
			this.InitializeLocalState();
			this.ProcessTuple(tupleMembers);
			return this.DetermineType();
		}

		// Token: 0x06007D22 RID: 32034 RVA: 0x001CB434 File Offset: 0x001C9634
		private TupleType DetermineType()
		{
			TupleType result = TupleType.Normal;
			if (this.allMemberAreTotals)
			{
				result = TupleType.GrandTotal;
			}
			else if (this.sequenceOfTotalWasInterrupted)
			{
				result = TupleType.Invalid;
			}
			return result;
		}

		// Token: 0x06007D23 RID: 32035 RVA: 0x001CB45C File Offset: 0x001C965C
		private bool MemberIsTotal(IOlapMember member, int index)
		{
			string allMemberName = this.groupDescriptions[index].FieldInfo.AllMemberName;
			return !string.IsNullOrEmpty(allMemberName) && member.UniqueName == allMemberName;
		}

		// Token: 0x06007D24 RID: 32036 RVA: 0x001CB498 File Offset: 0x001C9698
		private void ProcessTuple(IList<IOlapMember> tupleMembers)
		{
			bool flag = false;
			for (int i = 0; i < this.groupDescriptions.Count; i++)
			{
				IOlapMember member = tupleMembers[i];
				bool flag2 = this.MemberIsTotal(member, i);
				if (flag2)
				{
					flag = true;
				}
				else
				{
					this.allMemberAreTotals = false;
					if (flag)
					{
						this.sequenceOfTotalWasInterrupted = true;
					}
				}
			}
		}

		// Token: 0x06007D25 RID: 32037 RVA: 0x001CB4E6 File Offset: 0x001C96E6
		private void InitializeLocalState()
		{
			this.allMemberAreTotals = true;
			this.sequenceOfTotalWasInterrupted = false;
		}

		// Token: 0x0400224B RID: 8779
		private IList<OlapGroupDescription> groupDescriptions;

		// Token: 0x0400224C RID: 8780
		private bool allMemberAreTotals;

		// Token: 0x0400224D RID: 8781
		private bool sequenceOfTotalWasInterrupted;
	}
}
