using System;

namespace Telerik.Web.UI
{
	// Token: 0x020019CF RID: 6607
	public class RadRatingItemCollection : StronglyTypedStateManagedCollection<RadRatingItem>
	{
		// Token: 0x0600FF67 RID: 65383 RVA: 0x00395271 File Offset: 0x00393471
		public RadRatingItemCollection(RadRating parent)
		{
			this.Parent = parent;
		}

		// Token: 0x17004D14 RID: 19732
		// (get) Token: 0x0600FF68 RID: 65384 RVA: 0x00395280 File Offset: 0x00393480
		// (set) Token: 0x0600FF69 RID: 65385 RVA: 0x00395288 File Offset: 0x00393488
		internal RadRating Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x0600FF6A RID: 65386 RVA: 0x00395294 File Offset: 0x00393494
		public virtual void Add(string imageUrl)
		{
			RadRatingItem item = new RadRatingItem(imageUrl);
			this.Add(item);
		}

		// Token: 0x0600FF6B RID: 65387 RVA: 0x003952B0 File Offset: 0x003934B0
		public virtual void Add(string imageUrl, string selectedImageUrl)
		{
			RadRatingItem item = new RadRatingItem(imageUrl, selectedImageUrl);
			this.Add(item);
		}

		// Token: 0x0600FF6C RID: 65388 RVA: 0x003952CC File Offset: 0x003934CC
		public virtual void Add(string imageUrl, string selectedImageUrl, string hoveredImageUrl)
		{
			RadRatingItem item = new RadRatingItem(imageUrl, selectedImageUrl, hoveredImageUrl);
			this.Add(item);
		}

		// Token: 0x0600FF6D RID: 65389 RVA: 0x003952EC File Offset: 0x003934EC
		public virtual void Add(string imageUrl, string selectedImageUrl, string hoveredImageUrl, string hoveredSelectedImageUrl)
		{
			RadRatingItem item = new RadRatingItem(imageUrl, selectedImageUrl, hoveredImageUrl, hoveredSelectedImageUrl);
			this.Add(item);
		}

		// Token: 0x0600FF6E RID: 65390 RVA: 0x0039530C File Offset: 0x0039350C
		protected override void OnInsertComplete(int index, object value)
		{
			RadRatingItem radRatingItem = value as RadRatingItem;
			radRatingItem.Owner = this.parent;
			if (this.parent != null)
			{
				this.parent.InitializeItem(radRatingItem);
			}
		}

		// Token: 0x0600FF6F RID: 65391 RVA: 0x00395340 File Offset: 0x00393540
		protected override void OnRemoveComplete(int index, object value)
		{
			((RadRatingItem)value).Owner = null;
		}

		// Token: 0x0600FF70 RID: 65392 RVA: 0x00395350 File Offset: 0x00393550
		protected override void OnClear()
		{
			foreach (object obj in this)
			{
				RadRatingItem radRatingItem = (RadRatingItem)obj;
				radRatingItem.Owner = null;
			}
			base.OnClear();
		}

		// Token: 0x0600FF71 RID: 65393 RVA: 0x003953AC File Offset: 0x003935AC
		protected override void SetDirtyObject(object o)
		{
			((StateManager)o).SetDirty();
		}

		// Token: 0x0400485F RID: 18527
		private RadRating parent;
	}
}
