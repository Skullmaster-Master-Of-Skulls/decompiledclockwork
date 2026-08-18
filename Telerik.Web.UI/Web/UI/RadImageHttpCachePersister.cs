using System;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x020016BC RID: 5820
	public class RadImageHttpCachePersister : IRadImagePersister, IRadImageFileNameContainer
	{
		// Token: 0x0600E0AF RID: 57519 RVA: 0x0031F17A File Offset: 0x0031D37A
		public RadImageHttpCachePersister() : this(new CachePersistentMedia())
		{
		}

		// Token: 0x0600E0B0 RID: 57520 RVA: 0x0031F187 File Offset: 0x0031D387
		internal RadImageHttpCachePersister(IPersistentMedia persistenMedia)
		{
			this._persistenMedia = persistenMedia;
		}

		// Token: 0x0600E0B1 RID: 57521 RVA: 0x0031F198 File Offset: 0x0031D398
		public virtual string GenerateBinaryImageUrl(string imageHandlerUrl)
		{
			return string.Format("{0}?{1}", imageHandlerUrl, string.Format("{0}={1}", this.CurrentContext.Server.UrlEncode(this.UrlKey), this.CurrentContext.Server.UrlEncode(this.ImageKey)));
		}

		// Token: 0x170044DD RID: 17629
		// (get) Token: 0x0600E0B2 RID: 57522 RVA: 0x0031F1E8 File Offset: 0x0031D3E8
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

		// Token: 0x170044DE RID: 17630
		// (get) Token: 0x0600E0B3 RID: 57523 RVA: 0x0031F230 File Offset: 0x0031D430
		protected virtual string UrlKey
		{
			get
			{
				return "imgid";
			}
		}

		// Token: 0x0600E0B4 RID: 57524 RVA: 0x0031F238 File Offset: 0x0031D438
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

		// Token: 0x0600E0B5 RID: 57525 RVA: 0x0031F278 File Offset: 0x0031D478
		public virtual BinaryImageDataContainer LoadImage()
		{
			if (this.CurrentContext != null && !string.IsNullOrEmpty(this.CurrentContext.Request[this.UrlKey]))
			{
				return this._persistenMedia.Get<BinaryImageDataContainer>(this.CurrentContext.Request[this.UrlKey]);
			}
			return null;
		}

		// Token: 0x170044DF RID: 17631
		// (get) Token: 0x0600E0B6 RID: 57526 RVA: 0x0031F2CD File Offset: 0x0031D4CD
		protected HttpContext CurrentContext
		{
			get
			{
				return HttpContext.Current;
			}
		}

		// Token: 0x170044E0 RID: 17632
		// (get) Token: 0x0600E0B7 RID: 57527 RVA: 0x0031F2D4 File Offset: 0x0031D4D4
		// (set) Token: 0x0600E0B8 RID: 57528 RVA: 0x0031F2DC File Offset: 0x0031D4DC
		public virtual string ImageFileName { get; set; }

		// Token: 0x040040FC RID: 16636
		private string _imageKey;

		// Token: 0x040040FD RID: 16637
		private IPersistentMedia _persistenMedia;
	}
}
