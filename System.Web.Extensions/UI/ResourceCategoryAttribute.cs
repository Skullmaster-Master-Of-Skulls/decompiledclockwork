using System;
using System.ComponentModel;
using System.Web.Resources;

namespace System.Web.UI
{
	// Token: 0x02000068 RID: 104
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class ResourceCategoryAttribute : CategoryAttribute
	{
		// Token: 0x060003B9 RID: 953 RVA: 0x00013BD8 File Offset: 0x00011DD8
		internal ResourceCategoryAttribute(string category) : base(category)
		{
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003BA RID: 954 RVA: 0x00013BE1 File Offset: 0x00011DE1
		public override object TypeId
		{
			get
			{
				return typeof(CategoryAttribute);
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00013BF0 File Offset: 0x00011DF0
		protected override string GetLocalizedString(string value)
		{
			string text = base.GetLocalizedString(value);
			if (text == null)
			{
				text = AtlasWeb.ResourceManager.GetString("Category_" + value, AtlasWeb.Culture);
			}
			return text;
		}
	}
}
