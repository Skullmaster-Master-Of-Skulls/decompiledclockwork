using System;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x020000A8 RID: 168
	public class RadImageSessionPersister : IRadImagePersister, IRadImageFileNameContainer
	{
		// Token: 0x0600067F RID: 1663 RVA: 0x0001A86E File Offset: 0x00018A6E
		public RadImageSessionPersister() : this(new SessionPersistentMedia())
		{
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001A87B File Offset: 0x00018A7B
		internal RadImageSessionPersister(IPersistentMedia persistenMedia)
		{
			this._persistenMedia = persistenMedia;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001A88C File Offset: 0x00018A8C
		public virtual string GenerateBinaryImageUrl(string imageHandlerUrl)
		{
			return string.Format("{0}?{1}", imageHandlerUrl, string.Format("{0}={1}", this.CurrentContext.Server.UrlEncode(this.UrlKey), this.CurrentContext.Server.UrlEncode(this.ImageKey)));
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0001A8DC File Offset: 0x00018ADC
		protected virtual string ImageKey
		{
			get
			{
				if (string.IsNullOrEmpty(this._imageKey))
				{
					this._imageKey = Guid.NewGuid().ToString().Replace("-", "");
				}
				return this._imageKey;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x0001A924 File Offset: 0x00018B24
		protected virtual string UrlKey
		{
			get
			{
				return "imgid";
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001A92C File Offset: 0x00018B2C
		public virtual void SaveImage(byte[] image)
		{
			if (image != null)
			{
				BinaryImageDataContainer item = new BinaryImageDataContainer
				{
					Data = image,
					ImageName = this.ImageFileName
				};
				this._persistenMedia.Add<BinaryImageDataContainer>(this.ImageKey, item);
			}
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001A96C File Offset: 0x00018B6C
		public virtual BinaryImageDataContainer LoadImage()
		{
			if (this.CurrentContext != null && !string.IsNullOrEmpty(this.CurrentContext.Request[this.UrlKey]))
			{
				return this._persistenMedia.Get<BinaryImageDataContainer>(this.CurrentContext.Request[this.UrlKey]);
			}
			return null;
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0001A9C1 File Offset: 0x00018BC1
		protected HttpContext CurrentContext
		{
			get
			{
				return HttpContext.Current;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0001A9C8 File Offset: 0x00018BC8
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x0001A9D0 File Offset: 0x00018BD0
		public virtual string ImageFileName { get; set; }

		// Token: 0x04000161 RID: 353
		private string _imageKey;

		// Token: 0x04000162 RID: 354
		private IPersistentMedia _persistenMedia;
	}
}
