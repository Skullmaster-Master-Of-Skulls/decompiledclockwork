using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using Telerik.Web.UI.Export;
using Telerik.Windows.Documents.Media;

namespace Telerik.Web.UI.ExportInfrastructure
{
	// Token: 0x02000A4E RID: 2638
	public class Image : IDisposable
	{
		// Token: 0x170021B1 RID: 8625
		// (get) Token: 0x0600662F RID: 26159 RVA: 0x0017E1B0 File Offset: 0x0017C3B0
		// (set) Token: 0x06006630 RID: 26160 RVA: 0x0017E1B8 File Offset: 0x0017C3B8
		internal int Width
		{
			get
			{
				return this._width;
			}
			set
			{
				this._width = value;
			}
		}

		// Token: 0x170021B2 RID: 8626
		// (get) Token: 0x06006631 RID: 26161 RVA: 0x0017E1C1 File Offset: 0x0017C3C1
		// (set) Token: 0x06006632 RID: 26162 RVA: 0x0017E1C9 File Offset: 0x0017C3C9
		internal int Height
		{
			get
			{
				return this._height;
			}
			set
			{
				this._height = value;
			}
		}

		// Token: 0x170021B3 RID: 8627
		// (get) Token: 0x06006633 RID: 26163 RVA: 0x0017E1D2 File Offset: 0x0017C3D2
		// (set) Token: 0x06006634 RID: 26164 RVA: 0x0017E1DA File Offset: 0x0017C3DA
		public byte[] ImageData
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
			}
		}

		// Token: 0x170021B4 RID: 8628
		// (get) Token: 0x06006635 RID: 26165 RVA: 0x0017E1E3 File Offset: 0x0017C3E3
		// (set) Token: 0x06006636 RID: 26166 RVA: 0x0017E1EB File Offset: 0x0017C3EB
		public string ImageUrl
		{
			get
			{
				return this._url;
			}
			set
			{
				this._url = value;
			}
		}

		// Token: 0x170021B5 RID: 8629
		// (get) Token: 0x06006637 RID: 26167 RVA: 0x0017E1F4 File Offset: 0x0017C3F4
		// (set) Token: 0x06006638 RID: 26168 RVA: 0x0017E1FC File Offset: 0x0017C3FC
		public Range ImageRange
		{
			get
			{
				return this._range;
			}
			set
			{
				this._range = value;
			}
		}

		// Token: 0x170021B6 RID: 8630
		// (get) Token: 0x06006639 RID: 26169 RVA: 0x0017E205 File Offset: 0x0017C405
		// (set) Token: 0x0600663A RID: 26170 RVA: 0x0017E20D File Offset: 0x0017C40D
		[DefaultValue(false)]
		public bool AutoSize
		{
			get
			{
				return this._autoSize;
			}
			set
			{
				this._autoSize = value;
			}
		}

		// Token: 0x0600663B RID: 26171 RVA: 0x0017E216 File Offset: 0x0017C416
		internal Image GetImage()
		{
			if (this.ImageData != null)
			{
				return this.GetImageFromByteArray();
			}
			if (this.ImageUrl != null)
			{
				return this.GetImageFromUrl();
			}
			return null;
		}

		// Token: 0x0600663C RID: 26172 RVA: 0x0017E238 File Offset: 0x0017C438
		internal ImageSource GetImageSource()
		{
			ImageSource imageSource = null;
			if (!string.IsNullOrEmpty(this.ImageUrl))
			{
				byte[] buffer;
				if (Regex.IsMatch(this.ImageUrl, "^http.?://") || this.ImageUrl.Contains(".ashx") || this.ImageUrl.Contains(".axd"))
				{
					buffer = this.DownloadImage();
					this._ms = new MemoryStream(buffer);
					imageSource = new ImageSource(this._ms, Utils.GetFileExtensionFromUrl(this.ImageUrl));
					goto IL_101;
				}
				string text = HttpContext.Current.Server.MapPath(this.ImageUrl);
				FileInfo fileInfo = new FileInfo(text);
				if (!fileInfo.Exists || fileInfo.Length >= 2147483647L)
				{
					goto IL_101;
				}
				buffer = new byte[fileInfo.Length];
				using (FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.Read))
				{
					imageSource = new ImageSource(fileStream, fileInfo.Extension);
					goto IL_101;
				}
			}
			if (this.ImageData.Length > 0)
			{
				imageSource = new ImageSource(this.ImageData, Utils.GetFileExtensionFromByteArray(this.ImageData));
			}
			IL_101:
			this.DetermineImageDimensions(imageSource);
			return imageSource;
		}

		// Token: 0x0600663D RID: 26173 RVA: 0x0017E360 File Offset: 0x0017C560
		private void DetermineImageDimensions(ImageSource imgSource)
		{
			if (imgSource.Data.Length == 0)
			{
				return;
			}
			Image imageFromByteArray = this.GetImageFromByteArray(imgSource.Data);
			this.Width = imageFromByteArray.Width;
			this.Height = imageFromByteArray.Height;
		}

		// Token: 0x0600663E RID: 26174 RVA: 0x0017E39D File Offset: 0x0017C59D
		private Image GetImageFromByteArray()
		{
			return this.GetImageFromByteArray(this.ImageData);
		}

		// Token: 0x0600663F RID: 26175 RVA: 0x0017E3AC File Offset: 0x0017C5AC
		private Image GetImageFromByteArray(byte[] data)
		{
			this._ms = new MemoryStream(data);
			return Image.FromStream(this._ms);
		}

		// Token: 0x06006640 RID: 26176 RVA: 0x0017E3D4 File Offset: 0x0017C5D4
		private Image GetImageFromUrl()
		{
			Image result = null;
			if (Regex.IsMatch(this.ImageUrl, "^http.?://") || this.ImageUrl.Contains(".ashx") || this.ImageUrl.Contains(".axd"))
			{
				byte[] buffer = this.DownloadImage();
				this._ms = new MemoryStream(buffer);
				result = Image.FromStream(this._ms);
			}
			else
			{
				string text = HttpContext.Current.Server.MapPath(this.ImageUrl);
				FileInfo fileInfo = new FileInfo(text);
				if (fileInfo.Exists && fileInfo.Length < 2147483647L)
				{
					byte[] buffer = new byte[fileInfo.Length];
					using (FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.Read))
					{
						fileStream.Read(buffer, 0, (int)fileInfo.Length);
					}
					result = Image.FromFile(text);
				}
			}
			return result;
		}

		// Token: 0x06006641 RID: 26177 RVA: 0x0017E4C0 File Offset: 0x0017C6C0
		internal byte[] DownloadImage()
		{
			WebClient webClient = new WebClient();
			if ((this.ImageUrl.Contains(".ashx") || this.ImageUrl.Contains(".axd")) && !Regex.IsMatch(this.ImageUrl, "^https?://"))
			{
				string str = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/");
				this.ImageUrl = this.ImageUrl.Replace("~/", "");
				this.ImageUrl = (this.ImageUrl.StartsWith("/") ? this.ImageUrl.Substring(1) : this.ImageUrl);
				this.ImageUrl = str + this.ImageUrl;
			}
			return webClient.DownloadData(this.ImageUrl);
		}

		// Token: 0x06006642 RID: 26178 RVA: 0x0017E596 File Offset: 0x0017C796
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06006643 RID: 26179 RVA: 0x0017E59F File Offset: 0x0017C79F
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._ms.Dispose();
				this._ms = null;
			}
		}

		// Token: 0x040018BC RID: 6332
		private byte[] _data;

		// Token: 0x040018BD RID: 6333
		private Range _range;

		// Token: 0x040018BE RID: 6334
		private string _url;

		// Token: 0x040018BF RID: 6335
		private MemoryStream _ms;

		// Token: 0x040018C0 RID: 6336
		private bool _autoSize;

		// Token: 0x040018C1 RID: 6337
		private int _width;

		// Token: 0x040018C2 RID: 6338
		private int _height;
	}
}
