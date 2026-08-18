using System;
using System.ComponentModel;
using System.Web.UI.HtmlControls;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000EB6 RID: 3766
	[ToolboxItem(false)]
	public class Flip : ImageEditorDialog
	{
		// Token: 0x06008F64 RID: 36708 RVA: 0x00205666 File Offset: 0x00203866
		public Flip(string skin, RadImageEditor parentImageEditor) : base(skin, parentImageEditor)
		{
		}

		// Token: 0x17002D5C RID: 11612
		// (get) Token: 0x06008F65 RID: 36709 RVA: 0x00205670 File Offset: 0x00203870
		public override string DialogName
		{
			get
			{
				return "Flip";
			}
		}

		// Token: 0x17002D5D RID: 11613
		// (get) Token: 0x06008F66 RID: 36710 RVA: 0x00205677 File Offset: 0x00203877
		public override string ScriptUrl
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17002D5E RID: 11614
		// (get) Token: 0x06008F67 RID: 36711 RVA: 0x0020567E File Offset: 0x0020387E
		public override string Title
		{
			get
			{
				return base.ParentImageEditor.Localization.Dialogs.Flip_Title;
			}
		}

		// Token: 0x06008F68 RID: 36712 RVA: 0x00205698 File Offset: 0x00203898
		protected override void SetChildrensProperties()
		{
			base.SetChildrensProperties();
			DialogsStrings dialogs = base.ParentImageEditor.Localization.Dialogs;
			MainStrings main = base.ParentImageEditor.Localization.Main;
			this._flipNone = (HtmlAnchor)base.FindControlRecursive("FlipNone");
			if (this._flipNone != null)
			{
				this._flipNone.Title = dialogs.Flip_None;
			}
			this._imgFlipNone = (HtmlImage)base.FindControlRecursive("ImgFlipNone");
			if (this._imgFlipNone != null)
			{
				this._imgFlipNone.Alt = dialogs.Flip_None;
				this._imgFlipNone.Src = SkinRegistrar.GetWebResourceUrl(this.Page, typeof(RadImageEditor), "Telerik.Web.UI.Skins.Common.ImageEditor.flipNone.png");
			}
			this._flipHorizontal = (HtmlAnchor)base.FindControlRecursive("FlipHorizontal");
			if (this._flipHorizontal != null)
			{
				this._flipHorizontal.Title = main.FlipHorizontal;
			}
			this._imgFlipHorizontal = (HtmlImage)base.FindControlRecursive("ImgFlipHorizontal");
			if (this._imgFlipHorizontal != null)
			{
				this._imgFlipHorizontal.Alt = main.FlipHorizontal;
				this._imgFlipHorizontal.Src = SkinRegistrar.GetWebResourceUrl(this.Page, typeof(RadImageEditor), "Telerik.Web.UI.Skins.Common.ImageEditor.flipHorizontal.png");
			}
			this._flipVertical = (HtmlAnchor)base.FindControlRecursive("FlipVertical");
			if (this._flipVertical != null)
			{
				this._flipVertical.Title = main.FlipVertical;
			}
			this._imgFlipVertical = (HtmlImage)base.FindControlRecursive("ImgFlipVertical");
			if (this._imgFlipVertical != null)
			{
				this._imgFlipVertical.Alt = main.FlipVertical;
				this._imgFlipVertical.Src = SkinRegistrar.GetWebResourceUrl(this.Page, typeof(RadImageEditor), "Telerik.Web.UI.Skins.Common.ImageEditor.flipVertical.png");
			}
			this._flipBoth = (HtmlAnchor)base.FindControlRecursive("FlipBoth");
			if (this._flipBoth != null)
			{
				this._flipBoth.Title = main.FlipBoth;
			}
			this._imgFlipBoth = (HtmlImage)base.FindControlRecursive("ImgFlipBoth");
			if (this._imgFlipBoth != null)
			{
				this._imgFlipBoth.Alt = main.FlipBoth;
				this._imgFlipBoth.Src = SkinRegistrar.GetWebResourceUrl(this.Page, typeof(RadImageEditor), "Telerik.Web.UI.Skins.Common.ImageEditor.flipBoth.png");
			}
		}

		// Token: 0x040027FC RID: 10236
		private HtmlAnchor _flipNone;

		// Token: 0x040027FD RID: 10237
		private HtmlImage _imgFlipNone;

		// Token: 0x040027FE RID: 10238
		private HtmlAnchor _flipHorizontal;

		// Token: 0x040027FF RID: 10239
		private HtmlImage _imgFlipHorizontal;

		// Token: 0x04002800 RID: 10240
		private HtmlAnchor _flipVertical;

		// Token: 0x04002801 RID: 10241
		private HtmlImage _imgFlipVertical;

		// Token: 0x04002802 RID: 10242
		private HtmlAnchor _flipBoth;

		// Token: 0x04002803 RID: 10243
		private HtmlImage _imgFlipBoth;
	}
}
