using System;

namespace Telerik.Web
{
	// Token: 0x02000E85 RID: 3717
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class TelerikToolboxCategoryAttribute : Attribute
	{
		// Token: 0x17002C85 RID: 11397
		// (get) Token: 0x06008CF5 RID: 36085 RVA: 0x0020013D File Offset: 0x001FE33D
		// (set) Token: 0x06008CF6 RID: 36086 RVA: 0x00200145 File Offset: 0x001FE345
		public string CategoryTitle { get; set; }

		// Token: 0x06008CF7 RID: 36087 RVA: 0x0020014E File Offset: 0x001FE34E
		public TelerikToolboxCategoryAttribute(string _categoryTitle)
		{
			this.CategoryTitle = _categoryTitle;
		}
	}
}
