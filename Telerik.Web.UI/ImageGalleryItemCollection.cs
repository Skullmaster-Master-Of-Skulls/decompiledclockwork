using System;
using Telerik.Web;
using Telerik.Web.UI;

// Token: 0x02000556 RID: 1366
public class ImageGalleryItemCollection : StronglyTypedStateManagedCollection<ImageGalleryItemBase>
{
	// Token: 0x0600306C RID: 12396 RVA: 0x0009EBCF File Offset: 0x0009CDCF
	public ImageGalleryItemCollection(RadImageGallery gallery)
	{
		this.Gallery = gallery;
	}

	// Token: 0x17000F9A RID: 3994
	public override ImageGalleryItemBase this[int index]
	{
		get
		{
			if (index < 0 || index >= base.List.Count)
			{
				return null;
			}
			return base.List[index] as ImageGalleryItemBase;
		}
		set
		{
			base[index] = value;
		}
	}

	// Token: 0x0600306F RID: 12399 RVA: 0x0009EC0F File Offset: 0x0009CE0F
	protected override void OnClearComplete()
	{
		base.OnClearComplete();
		if (this.Gallery.DisplayAreaMode == ImageGalleryDisplayAreaMode.LightBox)
		{
			this.Gallery.LightBox.Items.Clear();
		}
	}

	// Token: 0x06003070 RID: 12400 RVA: 0x0009EC3A File Offset: 0x0009CE3A
	protected override void OnRemoveComplete(int index, object value)
	{
		base.OnRemoveComplete(index, value);
		if (this.Gallery.DisplayAreaMode == ImageGalleryDisplayAreaMode.LightBox)
		{
			this.Gallery.LightBox.Items.RemoveAt(index);
		}
	}

	// Token: 0x06003071 RID: 12401 RVA: 0x0009EC68 File Offset: 0x0009CE68
	protected override void OnInsertComplete(int index, object value)
	{
		base.OnInsertComplete(index, value);
		ImageGalleryItemBase imageGalleryItemBase = value as ImageGalleryItemBase;
		imageGalleryItemBase.Gallery = this.Gallery;
		if (this.Gallery.DisplayAreaMode == ImageGalleryDisplayAreaMode.LightBox)
		{
			this.InsertLightBoxItem(index, imageGalleryItemBase);
		}
	}

	// Token: 0x06003072 RID: 12402 RVA: 0x0009ECA8 File Offset: 0x0009CEA8
	private void InsertLightBoxItem(int index, ImageGalleryItemBase baseItem)
	{
		RadLightBoxItem radLightBoxItem = new RadLightBoxItem
		{
			Title = baseItem.Title,
			Description = baseItem.Description,
			Width = baseItem.Width,
			Height = baseItem.Height
		};
		baseItem.LightBoxItem = radLightBoxItem;
		ImageGalleryItem imageGalleryItem = baseItem as ImageGalleryItem;
		if (imageGalleryItem != null)
		{
			radLightBoxItem.ImageUrl = imageGalleryItem.ImageUrl;
		}
		ImageGalleryTemplateItem imageGalleryTemplateItem = baseItem as ImageGalleryTemplateItem;
		if (imageGalleryTemplateItem != null)
		{
			radLightBoxItem.ItemTemplate = imageGalleryTemplateItem.ContentTemplate;
		}
		if (index < 0)
		{
			this.Gallery.LightBox.Items.Add(radLightBoxItem);
			return;
		}
		this.Gallery.LightBox.Items.Insert(index, radLightBoxItem);
	}

	// Token: 0x06003073 RID: 12403 RVA: 0x0009ED54 File Offset: 0x0009CF54
	internal void RefreshLightBoxItems()
	{
		RadLightBoxItemCollection items = this.Gallery.LightBox.Items;
		items.Clear();
		foreach (object obj in this.Gallery.Items)
		{
			ImageGalleryItemBase baseItem = (ImageGalleryItemBase)obj;
			this.InsertLightBoxItem(items.Count, baseItem);
		}
	}

	// Token: 0x06003074 RID: 12404 RVA: 0x0009EDD0 File Offset: 0x0009CFD0
	protected override void SetDirtyObject(object stateManagerObject)
	{
		StateManager stateManager = stateManagerObject as StateManager;
		if (stateManager != null)
		{
			stateManager.SetDirty();
		}
	}

	// Token: 0x06003075 RID: 12405 RVA: 0x0009EDED File Offset: 0x0009CFED
	public override void Add(ImageGalleryItemBase item)
	{
		base.Add(item);
	}

	// Token: 0x04000D1B RID: 3355
	private readonly RadImageGallery Gallery;
}
