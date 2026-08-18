using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000558 RID: 1368
	internal class ImageGalleryThumbnailsArea : CompositeControl
	{
		// Token: 0x060030FF RID: 12543 RVA: 0x000A10F3 File Offset: 0x0009F2F3
		public ImageGalleryThumbnailsArea(RadImageGallery gallery)
		{
			this.Gallery = gallery;
			this.Settings = this.Gallery.ThumbnailsAreaSettings;
		}

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06003100 RID: 12544 RVA: 0x000A1113 File Offset: 0x0009F313
		// (set) Token: 0x06003101 RID: 12545 RVA: 0x000A1120 File Offset: 0x0009F320
		public override Unit Width
		{
			get
			{
				return this.Settings.Width;
			}
			set
			{
				this.Settings.Width = value;
			}
		}

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x06003102 RID: 12546 RVA: 0x000A112E File Offset: 0x0009F32E
		// (set) Token: 0x06003103 RID: 12547 RVA: 0x000A113B File Offset: 0x0009F33B
		public override Unit Height
		{
			get
			{
				return this.Settings.Height;
			}
			set
			{
				this.Settings.Height = value;
			}
		}

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x06003104 RID: 12548 RVA: 0x000A1149 File Offset: 0x0009F349
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x000A1150 File Offset: 0x0009F350
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.ScrollPrevButton != null)
			{
				this.ScrollPrevButton.Attributes["title"] = this.Settings.ScrollPrevButtonText;
			}
			if (this.ScrollNextButton != null)
			{
				this.ScrollNextButton.Attributes["title"] = this.Settings.ScrollNextButtonText;
			}
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.Width.ToString());
			writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString());
			if (this.Gallery.DisplayAreaMode == ImageGalleryDisplayAreaMode.Image && (this.Settings.Mode == ImageGalleryThumbnailsAreaMode.ImageSliderPreview || this.Settings.Mode == ImageGalleryThumbnailsAreaMode.ImageSlider))
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("{0} {1}", "rigThumbnailsBox" + this.Settings.ScrollOrientation, "rigPosition" + this.Settings.Position));
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06003106 RID: 12550 RVA: 0x000A126B File Offset: 0x0009F46B
		public void EnsureChildControlsCreated()
		{
			this.EnsureChildControls();
		}

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x06003107 RID: 12551 RVA: 0x000A128C File Offset: 0x0009F48C
		public RadListView ListView
		{
			get
			{
				if (this.listView == null)
				{
					this.listView = new RadListView();
					this.listView.CurrentPageIndex = this.Gallery.CurrentPageIndex;
					this.listView.PageSize = this.Gallery.PageSize;
					this.listView.AllowPaging = this.Gallery.AllowPaging;
					this.listView.DataSource = this.Gallery.DataSource;
					this.listView.DataSourceID = this.Gallery.DataSourceID;
					this.listView.DataKeyNames = this.Gallery.DataKeyNames;
					this.listView.NeedDataSource += this.listView_NeedDataSource;
					this.listView.DataBinding += this.listView_DataBinding;
					this.listView.DataBound += this.listView_DataBound;
					this.listView.LayoutTemplate = new ImageGalleryThumbnailsAreaLayoutTemplate(this.Gallery);
					this.listView.ItemTemplate = new ImageGalleryThumbnailsAreaItemTemplate(this.Gallery);
					this.listView.PreRender += delegate(object sender, EventArgs e)
					{
						((ISkinnableControl)sender).Skin = this.Gallery.RuntimeSkin;
					};
				}
				return this.listView;
			}
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x000A13C8 File Offset: 0x0009F5C8
		private void listView_DataBinding(object sender, EventArgs e)
		{
			this.Gallery.IsDataBinding = true;
			if (this.ListView.DataSource != this.Gallery.Items && !this.Gallery.AppendDataBoundItems)
			{
				this.Gallery.Items.Clear();
			}
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x000A1416 File Offset: 0x0009F616
		private void listView_DataBound(object sender, EventArgs e)
		{
			this.Gallery.IsDataBinding = false;
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x000A1424 File Offset: 0x0009F624
		private void listView_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
		{
			this.Gallery.CallOnNeedDataSource(new ImageGalleryNeedDataSourceEventArgs());
			if (this.Gallery.DataSource == null && string.IsNullOrEmpty(this.Gallery.DataSourceID))
			{
				if (!string.IsNullOrEmpty(this.Gallery.ImagesFolderPath))
				{
					this.PopulateItemsFromFolder(this.Gallery.ImagesFolderPath);
					return;
				}
				this.ListView.DataSource = this.Gallery.Items;
			}
		}

		// Token: 0x0600310B RID: 12555 RVA: 0x000A14BC File Offset: 0x0009F6BC
		private void PopulateItemsFromFolder(string folderPath)
		{
			string path = this.Gallery.Page.Server.MapPath(folderPath);
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			HashSet<string> allowedExtensions = new HashSet<string>(new string[]
			{
				".jpg",
				".jpeg",
				".png",
				".bmp",
				".gif",
				".tiff",
				".emf",
				".exif",
				".icon",
				".wmf"
			});
			IEnumerable<FileInfo> dataSource = from f in directoryInfo.GetFiles()
			where allowedExtensions.Contains(f.Extension.ToLower())
			select f;
			this.ListView.DataSource = dataSource;
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x000A1584 File Offset: 0x0009F784
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			base.CreateChildControls();
			if (this.Settings.ShowScrollButtons)
			{
				this.ScrollPrevButton = RadImageGallery.CreateButton("ScrollPrev", this.Settings.ScrollPrevButtonText);
				this.ScrollPrevButton.Style.Add(HtmlTextWriterStyle.Display, "none");
				this.Controls.Add(this.ScrollPrevButton);
				this.ScrollNextButton = RadImageGallery.CreateButton("ScrollNext", this.Settings.ScrollNextButtonText);
				this.Controls.Add(this.ScrollNextButton);
			}
			this.Controls.Add(this.ListView);
		}

		// Token: 0x04000D53 RID: 3411
		private RadListView listView;

		// Token: 0x04000D54 RID: 3412
		private readonly RadImageGallery Gallery;

		// Token: 0x04000D55 RID: 3413
		private readonly ImageGalleryThumbnailsAreaSettings Settings;

		// Token: 0x04000D56 RID: 3414
		private HtmlGenericControl ScrollPrevButton;

		// Token: 0x04000D57 RID: 3415
		private HtmlGenericControl ScrollNextButton;
	}
}
