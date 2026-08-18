using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002D2 RID: 722
	internal class LiteDropDownRenderer : LiteToolRenderer
	{
		// Token: 0x06001923 RID: 6435 RVA: 0x00052D76 File Offset: 0x00050F76
		public LiteDropDownRenderer(EditorDropDown owner) : base(owner)
		{
			this.Owner = owner;
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06001924 RID: 6436 RVA: 0x00052D86 File Offset: 0x00050F86
		// (set) Token: 0x06001925 RID: 6437 RVA: 0x00052D8E File Offset: 0x00050F8E
		public new EditorDropDown Owner { get; private set; }

		// Token: 0x06001926 RID: 6438 RVA: 0x00052D98 File Offset: 0x00050F98
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.Owner.Width != Unit.Empty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.Owner.Width.ToString());
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x00052DE4 File Offset: 0x00050FE4
		public override void AddTextAttributesToRender(HtmlTextWriter writer)
		{
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06001928 RID: 6440 RVA: 0x00052DE6 File Offset: 0x00050FE6
		public override string CssClassString
		{
			get
			{
				return this.GetCssClassString();
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x00052DEE File Offset: 0x00050FEE
		public override string CssClassFormatString
		{
			get
			{
				return "{0} re{1}";
			}
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x00052DF5 File Offset: 0x00050FF5
		public override string GetCssClassString()
		{
			return string.Format(this.CssClassFormatString, "reDropdown", this.Owner.Name);
		}
	}
}
