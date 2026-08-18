using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x0200070A RID: 1802
	[DataContract]
	[SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes", Justification = "Design choice.")]
	[Serializable]
	public sealed class MemberDistinctValue : IComparable
	{
		// Token: 0x06003FE0 RID: 16352 RVA: 0x000C9ED7 File Offset: 0x000C80D7
		public MemberDistinctValue()
		{
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x000C9EDF File Offset: 0x000C80DF
		internal MemberDistinctValue(string uniqueName)
		{
			this.UniqueName = uniqueName;
			this.Caption = uniqueName;
		}

		// Token: 0x170014D0 RID: 5328
		// (get) Token: 0x06003FE2 RID: 16354 RVA: 0x000C9EF5 File Offset: 0x000C80F5
		// (set) Token: 0x06003FE3 RID: 16355 RVA: 0x000C9EFD File Offset: 0x000C80FD
		[DataMember]
		public string Caption { get; set; }

		// Token: 0x170014D1 RID: 5329
		// (get) Token: 0x06003FE4 RID: 16356 RVA: 0x000C9F06 File Offset: 0x000C8106
		// (set) Token: 0x06003FE5 RID: 16357 RVA: 0x000C9F0E File Offset: 0x000C810E
		[DataMember]
		public string UniqueName { get; set; }

		// Token: 0x06003FE6 RID: 16358 RVA: 0x000C9F18 File Offset: 0x000C8118
		public override bool Equals(object obj)
		{
			MemberDistinctValue memberDistinctValue = obj as MemberDistinctValue;
			return memberDistinctValue != null && this.UniqueName.Equals(memberDistinctValue.UniqueName);
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x000C9F42 File Offset: 0x000C8142
		public override int GetHashCode()
		{
			return this.UniqueName.GetHashCode();
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x000C9F50 File Offset: 0x000C8150
		public int CompareTo(object obj)
		{
			MemberDistinctValue memberDistinctValue = obj as MemberDistinctValue;
			if (memberDistinctValue == null)
			{
				return 0;
			}
			return string.Compare(this.Caption, memberDistinctValue.Caption, StringComparison.Ordinal);
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x000C9F7B File Offset: 0x000C817B
		public override string ToString()
		{
			return this.Caption;
		}
	}
}
