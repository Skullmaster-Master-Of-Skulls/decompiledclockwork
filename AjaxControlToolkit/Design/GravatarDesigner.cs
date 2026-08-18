using System;
using System.ComponentModel;
using System.Web.UI.Design;

namespace AjaxControlToolkit.Design
{
	// Token: 0x020000A1 RID: 161
	public class GravatarDesigner : ControlDesigner
	{
		// Token: 0x060004E1 RID: 1249 RVA: 0x0000D645 File Offset: 0x0000B845
		public override void Initialize(IComponent component)
		{
			this._gravatar = (component as Gravatar);
			if (this._gravatar == null)
			{
				throw new ArgumentException("Component must be a gravatar control", "component");
			}
			base.Initialize(component);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000D674 File Offset: 0x0000B874
		public override string GetDesignTimeHtml()
		{
			string str = (this._gravatar.Rating == GravatarRating.Default) ? "G" : this._gravatar.Rating.ToString();
			string webResourceUrl = base.ViewControl.Page.ClientScript.GetWebResourceUrl(base.GetType(), "Gravatar.Images.gravatar-" + str + ".jpg");
			return string.Format("<div style='width:80px; height:80px;'><img src='{0}'/></div>", webResourceUrl);
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0000D6E2 File Offset: 0x0000B8E2
		public override bool AllowResize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0000D6E5 File Offset: 0x0000B8E5
		protected override bool Visible
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040002BD RID: 701
		private Gravatar _gravatar;
	}
}
