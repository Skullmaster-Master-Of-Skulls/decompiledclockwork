using System;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x020006B3 RID: 1715
	public class PivotFieldInfo : IPivotFieldInfo
	{
		// Token: 0x06003DC2 RID: 15810 RVA: 0x000C6F87 File Offset: 0x000C5187
		public PivotFieldInfo()
		{
			this.AutoGenerateField = true;
		}

		// Token: 0x17001438 RID: 5176
		// (get) Token: 0x06003DC3 RID: 15811 RVA: 0x000C6F96 File Offset: 0x000C5196
		// (set) Token: 0x06003DC4 RID: 15812 RVA: 0x000C6F9E File Offset: 0x000C519E
		public string Name { get; set; }

		// Token: 0x17001439 RID: 5177
		// (get) Token: 0x06003DC5 RID: 15813 RVA: 0x000C6FA7 File Offset: 0x000C51A7
		// (set) Token: 0x06003DC6 RID: 15814 RVA: 0x000C6FAF File Offset: 0x000C51AF
		public string DisplayName { get; set; }

		// Token: 0x1700143A RID: 5178
		// (get) Token: 0x06003DC7 RID: 15815 RVA: 0x000C6FB8 File Offset: 0x000C51B8
		// (set) Token: 0x06003DC8 RID: 15816 RVA: 0x000C6FC0 File Offset: 0x000C51C0
		public Type DataType { get; set; }

		// Token: 0x1700143B RID: 5179
		// (get) Token: 0x06003DC9 RID: 15817 RVA: 0x000C6FC9 File Offset: 0x000C51C9
		// (set) Token: 0x06003DCA RID: 15818 RVA: 0x000C6FD1 File Offset: 0x000C51D1
		public FieldRoles PreferredRole { get; set; }

		// Token: 0x1700143C RID: 5180
		// (get) Token: 0x06003DCB RID: 15819 RVA: 0x000C6FDA File Offset: 0x000C51DA
		// (set) Token: 0x06003DCC RID: 15820 RVA: 0x000C6FE2 File Offset: 0x000C51E2
		public FieldRoles AllowedRoles { get; set; }

		// Token: 0x1700143D RID: 5181
		// (get) Token: 0x06003DCD RID: 15821 RVA: 0x000C6FEB File Offset: 0x000C51EB
		// (set) Token: 0x06003DCE RID: 15822 RVA: 0x000C6FF3 File Offset: 0x000C51F3
		public bool AutoGenerateField { get; set; }

		// Token: 0x06003DCF RID: 15823 RVA: 0x000C6FFC File Offset: 0x000C51FC
		public override bool Equals(object obj)
		{
			PivotFieldInfo pivotFieldInfo = obj as PivotFieldInfo;
			return pivotFieldInfo != null && (pivotFieldInfo.AllowedRoles == this.AllowedRoles && pivotFieldInfo.AutoGenerateField == this.AutoGenerateField && pivotFieldInfo.DataType == this.DataType && pivotFieldInfo.DisplayName == this.DisplayName && pivotFieldInfo.Name == this.Name) && pivotFieldInfo.PreferredRole == this.PreferredRole;
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x000C707A File Offset: 0x000C527A
		public override int GetHashCode()
		{
			if (this.Name != null)
			{
				return this.Name.GetHashCode();
			}
			return base.GetHashCode();
		}
	}
}
