using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000550 RID: 1360
	internal class ImageGalleryThumbnailsAreaItemTemplate : ITemplate
	{
		// Token: 0x0600302F RID: 12335 RVA: 0x0009DA56 File Offset: 0x0009BC56
		public ImageGalleryThumbnailsAreaItemTemplate(RadImageGallery gallery)
		{
			this.Gallery = gallery;
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x0009DA68 File Offset: 0x0009BC68
		public void InstantiateIn(Control container)
		{
			RadListViewDataItem radListViewDataItem = container as RadListViewDataItem;
			ImageGalleryItemBase imageGalleryItemBase;
			if (this.Gallery.DataSource == this.Gallery.Items)
			{
				int num = radListViewDataItem.DisplayIndex;
				if (this.Gallery.AllowPaging)
				{
					num += this.Gallery.CurrentPageIndex * this.Gallery.PageSize;
				}
				imageGalleryItemBase = this.Gallery.Items[num];
			}
			else if (this.Gallery.DataSource == null && this.Gallery.Items.Count > radListViewDataItem.DisplayIndex)
			{
				imageGalleryItemBase = this.Gallery.Items[radListViewDataItem.DisplayIndex];
			}
			else
			{
				imageGalleryItemBase = new ImageGalleryItem();
				this.Gallery.Items.Add(imageGalleryItemBase);
			}
			if (this.Gallery.ThumbnailsAreaSettings.Mode != ImageGalleryThumbnailsAreaMode.ImageSlider)
			{
				HtmlGenericControl htmlGenericControl = this.CreateWrap(container);
				imageGalleryItemBase.InstantiateIn(htmlGenericControl);
				htmlGenericControl.DataBinding += this.OnDataBinding;
			}
			else
			{
				container.DataBinding += this.OnDataBinding;
			}
			this.Gallery.CallOnItemCreated(new ImageGalleryItemEventArgs(imageGalleryItemBase, radListViewDataItem));
		}

		// Token: 0x06003031 RID: 12337 RVA: 0x0009DB88 File Offset: 0x0009BD88
		private HtmlGenericControl CreateWrap(Control container)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("li");
			container.Controls.Add(htmlGenericControl);
			if (this.Gallery.ThumbnailsAreaSettings.ThumbnailsSpacing.Value > 0.0)
			{
				htmlGenericControl.Style.Add(HtmlTextWriterStyle.Margin, string.Format("0 {0} {0} 0", this.Gallery.ThumbnailsAreaSettings.ThumbnailsSpacing));
			}
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("a");
			htmlGenericControl.Controls.Add(htmlGenericControl2);
			htmlGenericControl2.Attributes.Add("href", "#");
			return htmlGenericControl2;
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x0009DC28 File Offset: 0x0009BE28
		private void OnDataBinding(object sender, EventArgs e)
		{
			Control control = sender as Control;
			RadListViewDataItem radListViewDataItem = control.NamingContainer as RadListViewDataItem;
			if (radListViewDataItem == null)
			{
				radListViewDataItem = (control as RadListViewDataItem);
			}
			object dataItem = radListViewDataItem.DataItem;
			this.Bind(dataItem, radListViewDataItem);
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x0009DC64 File Offset: 0x0009BE64
		private void Bind(object dataItem, RadListViewDataItem listViewItem)
		{
			ImageGalleryItemBase imageGalleryItemBase = dataItem as ImageGalleryItemBase;
			if (imageGalleryItemBase != null)
			{
				this.SetImageAlternateText(imageGalleryItemBase as ImageGalleryItem);
				this.Gallery.CallOnItemDataBound(new ImageGalleryItemEventArgs(imageGalleryItemBase, listViewItem));
				return;
			}
			ImageGalleryItem imageGalleryItem = this.Gallery.Items[this.Gallery.Items.Count - 1] as ImageGalleryItem;
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(dataItem);
			byte[] imageDataValue;
			if (properties.Count == 1 && properties.Find("Length", false) != null)
			{
				if (!(dataItem is string))
				{
					throw new ArgumentException("There are no bindable properties suitable for RadLightBox control");
				}
				imageGalleryItem.ImageUrl = dataItem.ToString();
			}
			else if ((imageDataValue = (dataItem as byte[])) != null)
			{
				imageGalleryItem.ImageDataValue = imageDataValue;
			}
			else
			{
				FileInfo fileInfo;
				if ((fileInfo = (dataItem as FileInfo)) != null)
				{
					string filename = this.Gallery.Page.Server.MapPath(this.Gallery.ImagesFolderPath + "\\" + fileInfo.Name);
					using (Image image = Image.FromFile(filename))
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							image.Save(memoryStream, ImageFormat.Png);
							string imageUrl = this.Gallery.ResolveClientUrl(this.Gallery.ImagesFolderPath + "/" + fileInfo.Name);
							imageGalleryItem.ImageUrl = imageUrl;
							imageGalleryItem.ThumbnailDataValue = memoryStream.ToArray();
						}
						goto IL_16B;
					}
				}
				imageGalleryItemBase = this.BindItem(imageGalleryItem, dataItem, properties);
			}
			IL_16B:
			this.SetImageAlternateText(imageGalleryItem);
			this.Gallery.CallOnItemDataBound(new ImageGalleryItemEventArgs(imageGalleryItem, listViewItem));
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x0009DE14 File Offset: 0x0009C014
		private void SetImageAlternateText(ImageGalleryItem item)
		{
			if (item == null)
			{
				return;
			}
			if (!string.IsNullOrEmpty(item.Title))
			{
				item.ThumbnailBinaryImage.AlternateText = item.Title;
				return;
			}
			if (!string.IsNullOrEmpty(item.ImageUrl))
			{
				item.ThumbnailBinaryImage.AlternateText = this.GetAlternateText(item.ImageUrl);
				return;
			}
			if (!string.IsNullOrEmpty(item.ThumbnailUrl))
			{
				item.ThumbnailBinaryImage.AlternateText = this.GetAlternateText(item.ThumbnailUrl);
				return;
			}
			item.ThumbnailBinaryImage.AlternateText = "Thumbnail Image";
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x0009DEA0 File Offset: 0x0009C0A0
		private string GetAlternateText(string url)
		{
			string result;
			try
			{
				string[] array = new Uri(new Uri(this.Gallery.Page.MapPath("~")), url).Segments.Last<string>().Split(new char[]
				{
					'.'
				});
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length - 1; i++)
				{
					stringBuilder.Append(array[i]);
				}
				result = stringBuilder.ToString();
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x0009DF30 File Offset: 0x0009C130
		private ImageGalleryItemBase BindItem(ImageGalleryItem item, object dataItem, PropertyDescriptorCollection props)
		{
			item.Title = ((this.ResolveItemField(this.Gallery.DataTitleField, dataItem, props) as string) ?? null);
			item.Description = ((this.ResolveItemField(this.Gallery.DataDescriptionField, dataItem, props) as string) ?? null);
			object obj = this.ResolveItemField(this.Gallery.DataThumbnailField, dataItem, props);
			object obj2 = this.ResolveItemField(this.Gallery.DataImageField, dataItem, props);
			byte[] array = obj2 as byte[];
			if (array != null)
			{
				item.ImageDataValue = array;
				item.ThumbnailDataValue = array;
			}
			else if (obj2 != null)
			{
				item.ImageUrl = obj2.ToString();
				item.ThumbnailUrl = obj2.ToString();
			}
			byte[] array2 = obj as byte[];
			if (array2 != null)
			{
				item.ThumbnailDataValue = array2;
			}
			else if (obj != null)
			{
				item.ThumbnailUrl = obj.ToString();
			}
			return item;
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x0009E004 File Offset: 0x0009C204
		private object ResolveItemField(string field, object dataItem, PropertyDescriptorCollection props)
		{
			if (string.IsNullOrEmpty(field))
			{
				return null;
			}
			PropertyDescriptor propertyDescriptor = props.Find(field, false);
			if (propertyDescriptor == null)
			{
				throw new ArgumentException(string.Format("A field with name {0} specified as a data field was not found in the datasource", new object[0]), field);
			}
			return DataBinder.GetPropertyValue(dataItem, propertyDescriptor.Name);
		}

		// Token: 0x04000D0E RID: 3342
		private readonly RadImageGallery Gallery;
	}
}
