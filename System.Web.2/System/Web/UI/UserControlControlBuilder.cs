using System;

namespace System.Web.UI
{
	// Token: 0x0200031E RID: 798
	public class UserControlControlBuilder : ControlBuilder
	{
		// Token: 0x0600251A RID: 9498 RVA: 0x0007A7C0 File Offset: 0x000789C0
		public override object BuildObject()
		{
			object obj = base.BuildObject();
			if (base.InDesigner)
			{
				IUserControlDesignerAccessor userControlDesignerAccessor = (IUserControlDesignerAccessor)obj;
				userControlDesignerAccessor.TagName = base.TagName;
				if (this._innerText != null)
				{
					userControlDesignerAccessor.InnerText = this._innerText;
				}
			}
			return obj;
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x00075B1E File Offset: 0x00073D1E
		public override bool NeedsTagInnerText()
		{
			return base.InDesigner;
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x0007A804 File Offset: 0x00078A04
		public override void SetTagInnerText(string text)
		{
			this._innerText = text;
		}

		// Token: 0x04001D6F RID: 7535
		private string _innerText;
	}
}
