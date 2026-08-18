using System;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x020016B9 RID: 5817
	public class RadBinaryImageHandler : IHttpHandler
	{
		// Token: 0x0600E099 RID: 57497 RVA: 0x0031EF99 File Offset: 0x0031D199
		public RadBinaryImageHandler() : this(new ImageHttpResponseWrapper())
		{
		}

		// Token: 0x0600E09A RID: 57498 RVA: 0x0031EFA6 File Offset: 0x0031D1A6
		internal RadBinaryImageHandler(ImageHttpResponseWrapper httpResponseWrapper)
		{
			this._httpResponse = httpResponseWrapper;
		}

		// Token: 0x170044D6 RID: 17622
		// (get) Token: 0x0600E09B RID: 57499 RVA: 0x0031EFB5 File Offset: 0x0031D1B5
		protected virtual IRadImagePersister ImagePersister
		{
			get
			{
				if (this._persister == null)
				{
					this._persister = new RadImageHttpCachePersister();
				}
				return this._persister;
			}
		}

		// Token: 0x0600E09C RID: 57500 RVA: 0x0031EFD0 File Offset: 0x0031D1D0
		public void ProcessRequest(HttpContext context)
		{
			this.ProcessRequestInternal();
		}

		// Token: 0x0600E09D RID: 57501 RVA: 0x0031EFD8 File Offset: 0x0031D1D8
		protected virtual void ProcessRequestInternal()
		{
			BinaryImageDataContainer binaryImageDataContainer = this.ImagePersister.LoadImage();
			if (binaryImageDataContainer == null)
			{
				binaryImageDataContainer = new RadImageSessionPersister().LoadImage();
				if (binaryImageDataContainer == null)
				{
					return;
				}
			}
			byte[] data = binaryImageDataContainer.Data;
			if (data == null)
			{
				return;
			}
			this._httpResponse.Clear();
			if (!string.IsNullOrEmpty(binaryImageDataContainer.ImageName))
			{
				this._httpResponse.FileName(binaryImageDataContainer.ImageName);
			}
			this._httpResponse.BinaryWrite(data);
			this._httpResponse.ContentType = BinaryImageFormatHelper.GetImageMimeType(data);
			this._httpResponse.SetCacheExpires(DateTime.Now.AddHours(2.0));
			this._httpResponse.End();
		}

		// Token: 0x170044D7 RID: 17623
		// (get) Token: 0x0600E09E RID: 57502 RVA: 0x0031F080 File Offset: 0x0031D280
		bool IHttpHandler.IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040040F8 RID: 16632
		private ImageHttpResponseWrapper _httpResponse;

		// Token: 0x040040F9 RID: 16633
		private IRadImagePersister _persister;
	}
}
