using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D0C RID: 3340
	[SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes", Justification = "OlapGroupNames are not supposed to participate in comparison operations.")]
	[DataContract]
	[Serializable]
	public class OlapGroupName : IComparable
	{
		// Token: 0x06007C76 RID: 31862 RVA: 0x001C9A4D File Offset: 0x001C7C4D
		public OlapGroupName(string groupCaption, object groupKey)
		{
			this.GroupCaption = groupCaption;
			this.GroupKey = groupKey;
		}

		// Token: 0x06007C77 RID: 31863 RVA: 0x001C9A63 File Offset: 0x001C7C63
		public OlapGroupName(string groupName)
		{
			this.GroupCaption = groupName;
			this.GroupKey = groupName;
		}

		// Token: 0x06007C78 RID: 31864 RVA: 0x001C9A79 File Offset: 0x001C7C79
		public OlapGroupName()
		{
		}

		// Token: 0x170027B2 RID: 10162
		// (get) Token: 0x06007C79 RID: 31865 RVA: 0x001C9A81 File Offset: 0x001C7C81
		// (set) Token: 0x06007C7A RID: 31866 RVA: 0x001C9A89 File Offset: 0x001C7C89
		[DataMember]
		public object GroupKey { get; set; }

		// Token: 0x170027B3 RID: 10163
		// (get) Token: 0x06007C7B RID: 31867 RVA: 0x001C9A92 File Offset: 0x001C7C92
		// (set) Token: 0x06007C7C RID: 31868 RVA: 0x001C9A9A File Offset: 0x001C7C9A
		[DataMember]
		public string GroupCaption { get; set; }

		// Token: 0x170027B4 RID: 10164
		// (get) Token: 0x06007C7D RID: 31869 RVA: 0x001C9AA3 File Offset: 0x001C7CA3
		[DataMember]
		public Collection<string> SortKeys
		{
			get
			{
				if (this.sortKeys == null)
				{
					this.sortKeys = new Collection<string>();
				}
				return this.sortKeys;
			}
		}

		// Token: 0x06007C7E RID: 31870 RVA: 0x001C9ABE File Offset: 0x001C7CBE
		public override string ToString()
		{
			return this.GroupCaption;
		}

		// Token: 0x06007C7F RID: 31871 RVA: 0x001C9AC6 File Offset: 0x001C7CC6
		public override int GetHashCode()
		{
			return this.GroupKey.GetHashCode();
		}

		// Token: 0x06007C80 RID: 31872 RVA: 0x001C9AD4 File Offset: 0x001C7CD4
		public override bool Equals(object obj)
		{
			OlapGroupName olapGroupName = obj as OlapGroupName;
			return olapGroupName != null && this.GroupKey.Equals(olapGroupName.GroupKey);
		}

		// Token: 0x06007C81 RID: 31873 RVA: 0x001C9B00 File Offset: 0x001C7D00
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			OlapGroupName olapGroupName = obj as OlapGroupName;
			if (olapGroupName == null)
			{
				throw new ArgumentException("Can only compare with other OlapGroupName instances");
			}
			return string.Compare(this.GroupCaption, olapGroupName.GroupCaption, StringComparison.Ordinal);
		}

		// Token: 0x04002218 RID: 8728
		private Collection<string> sortKeys;
	}
}
