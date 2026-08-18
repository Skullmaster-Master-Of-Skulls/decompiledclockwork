using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200047D RID: 1149
	public class CallingDataMethodsEventArgs : EventArgs
	{
		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x06003912 RID: 14610 RVA: 0x000B9DFE File Offset: 0x000B7FFE
		// (set) Token: 0x06003913 RID: 14611 RVA: 0x000B9E06 File Offset: 0x000B8006
		public Type DataMethodsType { get; set; }

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x06003914 RID: 14612 RVA: 0x000B9E0F File Offset: 0x000B800F
		// (set) Token: 0x06003915 RID: 14613 RVA: 0x000B9E17 File Offset: 0x000B8017
		public object DataMethodsObject { get; set; }
	}
}
