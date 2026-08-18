using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000442 RID: 1090
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class WinCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06004BB7 RID: 19383 RVA: 0x0013AFD4 File Offset: 0x001391D4
		public WinCategoryAttribute(string category) : base(category)
		{
		}

		// Token: 0x06004BB8 RID: 19384 RVA: 0x0013AFE0 File Offset: 0x001391E0
		protected override string GetLocalizedString(string value)
		{
			string text = base.GetLocalizedString(value);
			if (text == null)
			{
				text = (string)SR.GetObject("WinFormsCategory" + value);
			}
			return text;
		}
	}
}
