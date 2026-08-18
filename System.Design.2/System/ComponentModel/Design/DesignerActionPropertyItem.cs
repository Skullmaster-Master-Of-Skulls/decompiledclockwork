using System;

namespace System.ComponentModel.Design
{
	// Token: 0x020001A4 RID: 420
	public sealed class DesignerActionPropertyItem : DesignerActionItem
	{
		// Token: 0x06000F8F RID: 3983 RVA: 0x00059156 File Offset: 0x00057356
		public DesignerActionPropertyItem(string memberName, string displayName, string category, string description) : base(displayName, category, description)
		{
			this.memberName = memberName;
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x00059169 File Offset: 0x00057369
		public DesignerActionPropertyItem(string memberName, string displayName) : this(memberName, displayName, null, null)
		{
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00059175 File Offset: 0x00057375
		public DesignerActionPropertyItem(string memberName, string displayName, string category) : this(memberName, displayName, category, null)
		{
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x00059181 File Offset: 0x00057381
		public string MemberName
		{
			get
			{
				return this.memberName;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x00059189 File Offset: 0x00057389
		// (set) Token: 0x06000F94 RID: 3988 RVA: 0x00059191 File Offset: 0x00057391
		public IComponent RelatedComponent
		{
			get
			{
				return this.relatedComponent;
			}
			set
			{
				this.relatedComponent = value;
			}
		}

		// Token: 0x0400091E RID: 2334
		private string memberName;

		// Token: 0x0400091F RID: 2335
		private IComponent relatedComponent;
	}
}
