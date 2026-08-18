using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020005CA RID: 1482
	internal class MediaPlayerPlaylistLayoutTemplate : ITemplate
	{
		// Token: 0x06003526 RID: 13606 RVA: 0x000AFDE8 File Offset: 0x000ADFE8
		public MediaPlayerPlaylistLayoutTemplate(RadMediaPlayer mediaPlayer, RadListView listView)
		{
			this.MediaPlayer = mediaPlayer;
			this.OwnerListView = listView;
		}

		// Token: 0x06003527 RID: 13607 RVA: 0x000AFE00 File Offset: 0x000AE000
		public void InstantiateIn(Control container)
		{
			Panel panel = new Panel
			{
				ID = "Layout",
				CssClass = "rmpPlaylist rmpPlaylist" + this.MediaPlayer.PlaylistSettings.Position
			};
			if (this.MediaPlayer.PlaylistSettings.Position == MediaPlayerPlaylistPosition.Vertical && this.MediaPlayer.ResolvedRenderMode != RenderMode.Mobile)
			{
				panel.Height = this.MediaPlayer.Height;
			}
			else if (this.MediaPlayer.PlaylistSettings.Position == MediaPlayerPlaylistPosition.VerticalInside)
			{
				panel.Style.Add(HtmlTextWriterStyle.Display, "none");
			}
			container.Controls.Add(panel);
			if (this.MediaPlayer.ResolvedRenderMode != RenderMode.Mobile && this.MediaPlayer.PlaylistSettings.Mode == MediaPlayerPlaylistMode.Buttons)
			{
				Panel panel2 = panel;
				panel2.CssClass += " rmpPlaylistNavButtons";
				this.AddButtonArea(panel, "rmpPlaylistPrevButtonWrap", "PlaylistPrev", "Prev");
			}
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("ul")
			{
				ID = "ItemsList"
			};
			panel.Controls.Add(htmlGenericControl);
			if (this.MediaPlayer.ResolvedRenderMode != RenderMode.Mobile && this.MediaPlayer.PlaylistSettings.Mode == MediaPlayerPlaylistMode.Buttons)
			{
				this.AddButtonArea(panel, "rmpPlaylistNextButtonWrap", "PlaylistNext", "Next");
			}
			Panel panel3 = new Panel();
			panel3.ID = this.OwnerListView.ItemPlaceholderID;
			htmlGenericControl.Controls.Add(panel3);
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x000AFF78 File Offset: 0x000AE178
		private void AddButtonArea(Control wrapper, string wrapClass, string name, string text)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Attributes.Add("class", wrapClass);
			wrapper.Controls.Add(htmlGenericControl);
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("button");
			htmlGenericControl2.Attributes.Add("class", "rmpActionButton rmp" + name + "Button");
			htmlGenericControl.Controls.Add(htmlGenericControl2);
			HtmlGenericControl htmlGenericControl3 = new HtmlGenericControl("span");
			htmlGenericControl3.Attributes.Add("class", "rmpIcon rmp" + name + "Icon");
			htmlGenericControl3.Attributes.Add("title", text);
			htmlGenericControl2.Controls.Add(htmlGenericControl3);
		}

		// Token: 0x04000E66 RID: 3686
		private readonly RadListView OwnerListView;

		// Token: 0x04000E67 RID: 3687
		private readonly RadMediaPlayer MediaPlayer;
	}
}
