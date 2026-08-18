using System;
using System.ComponentModel;

namespace System.Web
{
	// Token: 0x0200010B RID: 267
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class WebCategoryAttribute : CategoryAttribute
	{
		// Token: 0x0600109A RID: 4250 RVA: 0x0002E1A6 File Offset: 0x0002C3A6
		internal WebCategoryAttribute(string category) : base(category)
		{
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x0002E1AF File Offset: 0x0002C3AF
		public override object TypeId
		{
			get
			{
				return typeof(CategoryAttribute);
			}
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0002E1BC File Offset: 0x0002C3BC
		protected override string GetLocalizedString(string value)
		{
			string text = base.GetLocalizedString(value);
			if (text == null)
			{
				text = SR.GetString("Category_" + value);
			}
			return text;
		}
	}
}
