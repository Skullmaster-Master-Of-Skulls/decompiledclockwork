using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000535 RID: 1333
	public class ImageGalleryPageIndexChangedEventArgs : ImageGalleryCommandEventArgs
	{
		// Token: 0x06002F37 RID: 12087 RVA: 0x0009A7BB File Offset: 0x000989BB
		public ImageGalleryPageIndexChangedEventArgs(object commandArgument) : base("Page", commandArgument)
		{
		}

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06002F38 RID: 12088 RVA: 0x0009A7C9 File Offset: 0x000989C9
		// (set) Token: 0x06002F39 RID: 12089 RVA: 0x0009A7D1 File Offset: 0x000989D1
		public int NewPageIndex { get; internal set; }

		// Token: 0x06002F3A RID: 12090 RVA: 0x0009A7DC File Offset: 0x000989DC
		public override void ExecuteCommand(RadImageGallery gallery)
		{
			this.SetNewPageIndex(gallery);
			gallery.CallOnCommand(this);
			if (this.Canceled)
			{
				return;
			}
			gallery.CallOnPageIndexChanged(this);
			if (this.Canceled)
			{
				return;
			}
			gallery.CurrentItemIndex = 0;
			gallery.CurrentPageIndex = this.NewPageIndex;
			gallery.Rebind();
			gallery.CallOnPageIndexChanged(this);
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x0009A830 File Offset: 0x00098A30
		private void SetNewPageIndex(RadImageGallery gallery)
		{
			string text = base.CommandArgument.ToString();
			if ("First" == text)
			{
				this.NewPageIndex = 0;
				return;
			}
			if ("Last" == text)
			{
				this.NewPageIndex = gallery.PageCount - 1;
				return;
			}
			if ("Next" == text)
			{
				this.NewPageIndex = gallery.CurrentPageIndex + 1;
				return;
			}
			if ("Prev" == text)
			{
				this.NewPageIndex = gallery.CurrentPageIndex - 1;
				return;
			}
			int newPageIndex;
			int.TryParse(text, out newPageIndex);
			this.NewPageIndex = newPageIndex;
		}
	}
}
