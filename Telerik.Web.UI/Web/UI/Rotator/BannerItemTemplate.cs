using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Rotator
{
	// Token: 0x02000E5F RID: 3679
	public class BannerItemTemplate : ITemplate, IDisposable
	{
		// Token: 0x06008BA7 RID: 35751 RVA: 0x001FBFB8 File Offset: 0x001FA1B8
		public void InstantiateIn(Control container)
		{
			RadRotatorItem radRotatorItem = container as RadRotatorItem;
			this._image = new Image();
			this._image.ImageUrl = DataBinder.Eval(radRotatorItem.DataItem, "ImageUrl").ToString();
			this._image.AlternateText = DataBinder.Eval(radRotatorItem.DataItem, "AlternateText").ToString();
			this._image.CssClass = "rrBanner";
			container.Controls.Add(this._image);
		}

		// Token: 0x06008BA8 RID: 35752 RVA: 0x001FC038 File Offset: 0x001FA238
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06008BA9 RID: 35753 RVA: 0x001FC041 File Offset: 0x001FA241
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._image != null)
			{
				this._image.Dispose();
			}
		}

		// Token: 0x0400271A RID: 10010
		private Image _image;
	}
}
