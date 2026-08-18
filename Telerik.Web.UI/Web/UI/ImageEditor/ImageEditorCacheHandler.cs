using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Web;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000E8A RID: 3722
	public class ImageEditorCacheHandler : IHttpHandler, IImageEditorCacheHandler
	{
		// Token: 0x06008D05 RID: 36101 RVA: 0x002002C4 File Offset: 0x001FE4C4
		public ImageEditorCacheHandler()
		{
			this._graphicsCore = new GDIGraphicsCore();
		}

		// Token: 0x06008D06 RID: 36102 RVA: 0x00200300 File Offset: 0x001FE500
		public void ProcessRequest(HttpContext context)
		{
			string text = context.Request["dkey"];
			string text2 = context.Request.Form["encryptedDownloadKey"];
			if (this.IsCustomDownloadOperation(text) && !this.IsValidDownloadKey(text2))
			{
				this.CompleteAsBadRequest(context.ApplicationInstance);
				return;
			}
			string fileName = context.Request["fileName"];
			if (this.IsDownloadedFromImageProvider(text))
			{
				ICacheImageProvider imageProvider = this.GetImageProvider(context);
				string key = context.Request["key"];
				if (text == "1" && !this.IsValidDownloadKey(text2))
				{
					this.CompleteAsBadRequest(context.ApplicationInstance);
					return;
				}
				using (EditableImage editableImage = imageProvider.Retrieve(key))
				{
					this.SendImage(editableImage, context, text, fileName);
					goto IL_10C;
				}
			}
			if (this.IsDownloadedFromCanvas(text))
			{
				this.SendImageCanvas(context.Request.Form["base64"], context, text, fileName, context.Request["mime"]);
			}
			else
			{
				string downloadKey = this.Decrypt(text2);
				this.GetImageFromFileSystem(context, downloadKey, fileName);
			}
			IL_10C:
			context.ApplicationInstance.CompleteRequest();
		}

		// Token: 0x06008D07 RID: 36103 RVA: 0x00200434 File Offset: 0x001FE634
		public virtual bool IsCustomDownloadOperation(string downloadKey)
		{
			return DownloadOperation.IsCustom(downloadKey);
		}

		// Token: 0x06008D08 RID: 36104 RVA: 0x0020043C File Offset: 0x001FE63C
		public virtual bool IsDownloadedFromImageProvider(string downloadKey)
		{
			return DownloadOperation.IsFromImageProvider(downloadKey);
		}

		// Token: 0x06008D09 RID: 36105 RVA: 0x00200444 File Offset: 0x001FE644
		public virtual bool IsDownloadedFromCanvas(string downloadKey)
		{
			return DownloadOperation.IsFromCanvas(downloadKey);
		}

		// Token: 0x06008D0A RID: 36106 RVA: 0x0020044C File Offset: 0x001FE64C
		public virtual bool IsValidDownloadKey(string downloadKey)
		{
			bool result;
			try
			{
				result = (!string.IsNullOrEmpty(downloadKey) && this.Decrypt(downloadKey).Length == this.GUID_LENGTH);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06008D0B RID: 36107 RVA: 0x00200494 File Offset: 0x001FE694
		private void CompleteAsBadRequest(HttpApplication app)
		{
			this.CompleteRequest(app, HttpStatusCode.BadRequest);
		}

		// Token: 0x06008D0C RID: 36108 RVA: 0x002004A2 File Offset: 0x001FE6A2
		private void CompleteRequest(HttpApplication app, HttpStatusCode statusCode)
		{
			app.Response.StatusCode = (int)statusCode;
			app.CompleteRequest();
		}

		// Token: 0x06008D0D RID: 36109 RVA: 0x002004B6 File Offset: 0x001FE6B6
		public virtual string Encrypt(string input)
		{
			return HmacEnabledCryptoService.GetService("").Encrypt(input);
		}

		// Token: 0x06008D0E RID: 36110 RVA: 0x002004C8 File Offset: 0x001FE6C8
		public virtual string Decrypt(string input)
		{
			return HmacEnabledCryptoService.GetService("").Decrypt(input);
		}

		// Token: 0x06008D0F RID: 36111 RVA: 0x002004DA File Offset: 0x001FE6DA
		public virtual void SendImage(EditableImage editableImage, HttpContext context, string downloadKey, string fileName)
		{
			if (downloadKey == "1")
			{
				this.OutputImageForDownload(editableImage, context, fileName);
				return;
			}
			this.OutputImage(editableImage, context);
		}

		// Token: 0x06008D10 RID: 36112 RVA: 0x002004FC File Offset: 0x001FE6FC
		public virtual void SendImageCanvas(string base64String, HttpContext context, string downloadKey, string fileName, string mimeType)
		{
			string text = (mimeType == "jpg") ? "jpg" : "png";
			string text2 = this.ReadCustomImageExtension(context);
			bool flag = !string.IsNullOrEmpty(text2);
			if (flag)
			{
				text = text2;
			}
			byte[] array = Convert.FromBase64String(base64String);
			if (flag)
			{
				this.OutputImageWithExtension(new EditableImage(new MemoryStream(array)), context, fileName, text2);
				return;
			}
			context.Response.ContentType = "image/" + this.EnsureCorrectJpegMimeType(text2 ?? text);
			context.Response.OutputStream.Write(array, 0, array.Length);
			if (downloadKey == "2")
			{
				this.SetHttpHeaderFileName(context, this.CombineFileName(fileName, text));
			}
		}

		// Token: 0x17002C8A RID: 11402
		// (get) Token: 0x06008D11 RID: 36113 RVA: 0x002005AE File Offset: 0x001FE7AE
		// (set) Token: 0x06008D12 RID: 36114 RVA: 0x002005B6 File Offset: 0x001FE7B6
		public virtual IGraphicsCore GraphicsCore
		{
			get
			{
				return this._graphicsCore;
			}
			set
			{
				this._graphicsCore = value;
			}
		}

		// Token: 0x06008D13 RID: 36115 RVA: 0x002005C0 File Offset: 0x001FE7C0
		public void OutputImageWithExtension(EditableImage editableImage, HttpContext context, string fileName, string customExt)
		{
			try
			{
				editableImage.ConvertTo(this.ReadFormatFromExtension(customExt));
				this.OutputImageForDownload(editableImage, context, fileName);
			}
			finally
			{
				if (editableImage != null)
				{
					((IDisposable)editableImage).Dispose();
				}
			}
		}

		// Token: 0x06008D14 RID: 36116 RVA: 0x00200604 File Offset: 0x001FE804
		public virtual void OutputImage(EditableImage editableImage, HttpContext context)
		{
			ImageFormat rawFormat = editableImage.RawFormat;
			string format = editableImage.Format.ToLower();
			Image image = editableImage.Image;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				image.Save(memoryStream, rawFormat);
				context.Response.ContentType = string.Format("image/{0}", this.EnsureCorrectJpegMimeType(format));
				memoryStream.WriteTo(context.Response.OutputStream);
			}
		}

		// Token: 0x06008D15 RID: 36117 RVA: 0x00200684 File Offset: 0x001FE884
		public virtual void OutputImageForDownload(EditableImage editableImage, HttpContext context, string fileName)
		{
			fileName = this.CombineImageFileName(editableImage, fileName);
			this.OutputImage(editableImage, context);
			this.SetHttpHeaderFileName(context, fileName);
		}

		// Token: 0x06008D16 RID: 36118 RVA: 0x002006A0 File Offset: 0x001FE8A0
		public virtual void SetHttpHeaderFileName(HttpContext context, string fileName)
		{
			context.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
		}

		// Token: 0x06008D17 RID: 36119 RVA: 0x002006BD File Offset: 0x001FE8BD
		public string CombineImageFileName(EditableImage editableImage, string fileName)
		{
			return this.CombineFileName(fileName, editableImage.Format);
		}

		// Token: 0x06008D18 RID: 36120 RVA: 0x002006CC File Offset: 0x001FE8CC
		public string CombineFileName(string fileName, string extension)
		{
			string str = string.IsNullOrEmpty(fileName) ? "Telerik_RadImageEditor_Image" : fileName.Replace("\n", " ").Replace("\r", " ");
			return str + string.Format(".{0}", extension);
		}

		// Token: 0x06008D19 RID: 36121 RVA: 0x0020071C File Offset: 0x001FE91C
		private string ReadCustomImageExtension(HttpContext context)
		{
			string text = context.Request["ext"];
			if (this.IsEditableImageFormat(text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x06008D1A RID: 36122 RVA: 0x00200746 File Offset: 0x001FE946
		private EditableFormat ReadFormatFromExtension(string extension)
		{
			return (EditableFormat)Enum.Parse(typeof(EditableFormat), extension, true);
		}

		// Token: 0x06008D1B RID: 36123 RVA: 0x00200760 File Offset: 0x001FE960
		private bool IsEditableImageFormat(string extension)
		{
			bool result;
			try
			{
				this.ReadFormatFromExtension(extension);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06008D1C RID: 36124 RVA: 0x00200790 File Offset: 0x001FE990
		private string EnsureCorrectJpegMimeType(string format)
		{
			return format.ToLowerInvariant().Replace("jpg", "jpeg");
		}

		// Token: 0x06008D1D RID: 36125 RVA: 0x002007A8 File Offset: 0x001FE9A8
		private void GetImageFromFileSystem(HttpContext context, string downloadKey, string fileName)
		{
			string text = this.ResolveFilePathFromKey(context, downloadKey);
			using (StreamReader streamReader = new StreamReader(text))
			{
				using (BinaryReader binaryReader = new BinaryReader(streamReader.BaseStream))
				{
					byte[] array = new byte[streamReader.BaseStream.Length];
					binaryReader.Read(array, 0, array.Length);
					if (array.Length == 0)
					{
						throw new FileNotFoundException("The specified image could not be found.", text);
					}
					string text2 = this.ReadCustomImageExtension(context);
					if (string.IsNullOrEmpty(text2))
					{
						text2 = Path.GetExtension(text).Substring(1).ToLowerInvariant();
						fileName = (string.IsNullOrEmpty(fileName) ? Path.GetFileName(text) : (fileName + "." + text2));
						this.WriteFile(array, fileName, "image/" + text2, context.Response);
					}
					else
					{
						this.OutputImageWithExtension(new EditableImage(new MemoryStream(array)), context, fileName, text2);
					}
				}
			}
		}

		// Token: 0x06008D1E RID: 36126 RVA: 0x002008AC File Offset: 0x001FEAAC
		private string ResolveFilePathFromKey(HttpContext context, string downloadKey)
		{
			object obj = context.Cache.Get(downloadKey);
			if (obj == null && context.Session != null)
			{
				obj = context.Session[downloadKey];
			}
			if (obj == null)
			{
				throw new ArgumentException("The download key does not correspond to a valid virtual image path.", downloadKey);
			}
			string text = obj.ToString();
			text = context.Request.MapPath(text, context.Request.ApplicationPath, false);
			if (text == null)
			{
				throw new ArgumentNullException("filePath", "The physical path to the image is missing. Please make sure the Query String is not modified.");
			}
			if (!text.StartsWith(context.Request.PhysicalApplicationPath))
			{
				throw new NotSupportedException("Using images outside of the web application path is not supported.");
			}
			return text;
		}

		// Token: 0x06008D1F RID: 36127 RVA: 0x00200940 File Offset: 0x001FEB40
		private void WriteFile(byte[] content, string fileName, string contentType, HttpResponse response)
		{
			fileName = fileName.Replace("\n", " ").Replace("\r", " ");
			contentType = contentType.Replace("\n", " ").Replace("\r", " ");
			response.Buffer = true;
			response.Clear();
			response.ContentType = this.EnsureCorrectJpegMimeType(contentType);
			response.AddHeader("content-disposition", "attachment; filename=" + fileName);
			response.BinaryWrite(content);
			response.Flush();
			response.End();
		}

		// Token: 0x06008D20 RID: 36128 RVA: 0x002009DC File Offset: 0x001FEBDC
		private ICacheImageProvider GetImageProvider(HttpContext context)
		{
			ICacheImageProvider cacheImageProvider = string.IsNullOrEmpty(context.Request["prtype"]) ? new CacheImageProvider() : RadImageEditor.InitCacheImageProvider(RadImageEditor.GetICacheImageProviderType(context.Request["prtype"]));
			string a;
			if ((a = context.Request["pr"]) != null)
			{
				if (a == "c")
				{
					cacheImageProvider.Storage = ImageStorage.Cache;
					return cacheImageProvider;
				}
				if (a == "s")
				{
					cacheImageProvider.Storage = ImageStorage.Session;
					return cacheImageProvider;
				}
				if (a == "f")
				{
					throw new NotImplementedException();
				}
			}
			throw new NotSupportedException("This image provider is not supported");
		}

		// Token: 0x17002C8B RID: 11403
		// (get) Token: 0x06008D21 RID: 36129 RVA: 0x00200A81 File Offset: 0x001FEC81
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0400279C RID: 10140
		private readonly int GUID_LENGTH = Guid.NewGuid().ToString().Length;

		// Token: 0x0400279D RID: 10141
		private IGraphicsCore _graphicsCore;
	}
}
