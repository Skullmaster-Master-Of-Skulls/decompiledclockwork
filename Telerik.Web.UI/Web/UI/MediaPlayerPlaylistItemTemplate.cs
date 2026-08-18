using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020005C9 RID: 1481
	internal class MediaPlayerPlaylistItemTemplate : ITemplate
	{
		// Token: 0x06003520 RID: 13600 RVA: 0x000AFB7F File Offset: 0x000ADD7F
		public MediaPlayerPlaylistItemTemplate(RadMediaPlayer mediaPlayer)
		{
			this.MediaPlayer = mediaPlayer;
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x000AFB90 File Offset: 0x000ADD90
		public void InstantiateIn(Control container)
		{
			RadListViewDataItem radListViewDataItem = container as RadListViewDataItem;
			if (radListViewDataItem != null)
			{
				HtmlGenericControl htmlGenericControl = new HtmlGenericControl("li");
				container.ID = "item" + radListViewDataItem.DataItemIndex.ToString();
				container.Controls.Add(htmlGenericControl);
				htmlGenericControl.DataBinding += this.itemContainer_DataBinding;
				HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("a");
				htmlGenericControl2.Attributes.Add("href", "#");
				htmlGenericControl.Controls.Add(htmlGenericControl2);
				htmlGenericControl2.DataBinding += this.linkWrapper_DataBinding;
				HtmlGenericControl htmlGenericControl3 = new HtmlGenericControl("span");
				htmlGenericControl3.Attributes.Add("class", "rmpThumbWrap");
				htmlGenericControl2.Controls.Add(htmlGenericControl3);
				Image image = new Image();
				image.CssClass = "rmpListItemThumb";
				htmlGenericControl3.Controls.Add(image);
				image.DataBinding += this.image_DataBinding;
				HtmlGenericControl htmlGenericControl4 = new HtmlGenericControl("span");
				htmlGenericControl4.Attributes.Add("class", "rmpTitle");
				htmlGenericControl2.Controls.Add(htmlGenericControl4);
				htmlGenericControl4.DataBinding += this.label_DataBinding;
				if (radListViewDataItem.DisplayIndex == this.MediaPlayer.PlaylistSettings.SelectedIndex)
				{
					htmlGenericControl.Attributes.Add("class", "rmpActive");
				}
			}
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000AFD00 File Offset: 0x000ADF00
		private void itemContainer_DataBinding(object sender, EventArgs e)
		{
			HtmlGenericControl htmlGenericControl = sender as HtmlGenericControl;
			RadListViewDataItem radListViewDataItem = htmlGenericControl.NamingContainer as RadListViewDataItem;
			object dataItem = radListViewDataItem.DataItem;
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x000AFD28 File Offset: 0x000ADF28
		private void linkWrapper_DataBinding(object sender, EventArgs e)
		{
			HtmlGenericControl htmlGenericControl = sender as HtmlGenericControl;
			RadListViewDataItem radListViewDataItem = htmlGenericControl.NamingContainer as RadListViewDataItem;
			MediaPlayerFile mediaPlayerFile = radListViewDataItem.DataItem as MediaPlayerFile;
			htmlGenericControl.Attributes.Add("title", mediaPlayerFile.Title);
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x000AFD6C File Offset: 0x000ADF6C
		private void image_DataBinding(object sender, EventArgs e)
		{
			Image image = sender as Image;
			RadListViewDataItem radListViewDataItem = image.NamingContainer as RadListViewDataItem;
			MediaPlayerFile mediaPlayerFile = radListViewDataItem.DataItem as MediaPlayerFile;
			image.AlternateText = mediaPlayerFile.Poster;
			image.ImageUrl = mediaPlayerFile.Poster;
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x000AFDB0 File Offset: 0x000ADFB0
		private void label_DataBinding(object sender, EventArgs e)
		{
			HtmlGenericControl htmlGenericControl = sender as HtmlGenericControl;
			RadListViewDataItem radListViewDataItem = htmlGenericControl.NamingContainer as RadListViewDataItem;
			MediaPlayerFile mediaPlayerFile = radListViewDataItem.DataItem as MediaPlayerFile;
			htmlGenericControl.InnerText = mediaPlayerFile.Title;
		}

		// Token: 0x04000E65 RID: 3685
		private readonly RadMediaPlayer MediaPlayer;
	}
}
