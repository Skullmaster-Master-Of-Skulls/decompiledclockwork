using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;
using Telerik.Charting;

namespace Telerik.Web.UI
{
	// Token: 0x02001808 RID: 6152
	public class ChartHttpHandler : IHttpHandler, IRequiresSessionState
	{
		// Token: 0x17004881 RID: 18561
		// (get) Token: 0x0600EFC4 RID: 61380 RVA: 0x003698A7 File Offset: 0x00367AA7
		public static string Path
		{
			get
			{
				return "ChartImage.axd";
			}
		}

		// Token: 0x0600EFC5 RID: 61381 RVA: 0x003698B0 File Offset: 0x00367AB0
		private static ImageFormat GetImageFormat(string imageFormat)
		{
			ImageFormatConverter imageFormatConverter = new ImageFormatConverter();
			ImageFormat result;
			try
			{
				result = (ImageFormat)imageFormatConverter.ConvertFromString(imageFormat);
			}
			catch
			{
				result = ImageFormat.Gif;
			}
			return result;
		}

		// Token: 0x0600EFC6 RID: 61382 RVA: 0x003698EC File Offset: 0x00367AEC
		private static string GetContentType(ImageFormat imageFormat)
		{
			if (imageFormat == ImageFormat.Bmp)
			{
				return "image/bmp";
			}
			if (imageFormat == ImageFormat.Emf)
			{
				return "image/emf";
			}
			if (imageFormat == ImageFormat.Exif)
			{
				return "image/exif";
			}
			if (imageFormat == ImageFormat.Gif)
			{
				return "image/gif";
			}
			if (imageFormat == ImageFormat.Icon)
			{
				return "image/icon";
			}
			if (imageFormat == ImageFormat.Jpeg)
			{
				return "image/jpeg";
			}
			if (imageFormat == ImageFormat.MemoryBmp)
			{
				return "image/bmp";
			}
			if (imageFormat == ImageFormat.Png)
			{
				return "image/png";
			}
			if (imageFormat == ImageFormat.Tiff)
			{
				return "image/png";
			}
			if (imageFormat == ImageFormat.Wmf)
			{
				return "image/wmf";
			}
			return "image/gif";
		}

		// Token: 0x0600EFC7 RID: 61383 RVA: 0x0036998A File Offset: 0x00367B8A
		private static string UngarbleImagePath(string garbledPath)
		{
			return Security.decryptStringFromBytes_AES(Convert.FromBase64String(garbledPath), Security.chartKey, Security.chartIV);
		}

		// Token: 0x17004882 RID: 18562
		// (get) Token: 0x0600EFC8 RID: 61384 RVA: 0x003699A1 File Offset: 0x00367BA1
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600EFC9 RID: 61385 RVA: 0x003699A4 File Offset: 0x00367BA4
		public void ProcessRequest(HttpContext context)
		{
			string value = context.Request.QueryString.Get("useSession");
			bool flag = Convert.ToBoolean(value);
			ImageFormat imageFormat = ChartHttpHandler.GetImageFormat(context.Request.QueryString.Get("imageFormat"));
			if (imageFormat == ImageFormat.Emf)
			{
				flag = false;
			}
			if (flag)
			{
				string text = context.Request.QueryString.Get("ChartID");
				MemoryStream memoryStream = null;
				if (text != null)
				{
					if (context.Cache[text] != null)
					{
						memoryStream = (MemoryStream)context.Cache[text];
					}
					else if (context.Session[text] != null)
					{
						Image image = (Image)context.Session[text];
						context.Session.Remove(text);
						memoryStream = new MemoryStream();
						if (imageFormat != ImageFormat.Emf)
						{
							image.Save(memoryStream, imageFormat);
							context.Cache.Add(text, memoryStream, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5.0), CacheItemPriority.Normal, null);
							image.Save(memoryStream, imageFormat);
						}
						image.Dispose();
					}
					if (memoryStream != null && memoryStream.ToArray().Length > 0)
					{
						context.Response.Clear();
						context.Response.ContentType = ChartHttpHandler.GetContentType(imageFormat);
						context.Response.Expires = 1440;
						byte[] array = memoryStream.ToArray();
						context.Response.OutputStream.Write(array, 0, array.Length);
						context.Response.End();
						return;
					}
				}
			}
			else
			{
				string text2 = context.Request.QueryString.Get("ImageName");
				text2 = ChartHttpHandler.UngarbleImagePath(text2);
				MemoryStream memoryStream2 = null;
				bool flag2 = false;
				if (context.Cache[text2] != null)
				{
					memoryStream2 = (MemoryStream)context.Cache[text2];
				}
				else
				{
					int num = 0;
					while (!File.Exists(context.Server.MapPath(text2)))
					{
						Thread.Sleep(200);
						num++;
						if (num == 5)
						{
							break;
						}
					}
					if (File.Exists(context.Server.MapPath(text2)))
					{
						string applicationPath = context.Request.ApplicationPath;
						if (!this.IsFilePathSecure(text2, applicationPath))
						{
							context.Response.StatusCode = 403;
							context.Response.End();
							return;
						}
						string path = context.Server.MapPath(text2);
						memoryStream2 = new MemoryStream(File.ReadAllBytes(path));
						context.Cache.Add(text2, memoryStream2, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5.0), CacheItemPriority.Normal, null);
						flag2 = true;
					}
				}
				if (memoryStream2 != null)
				{
					context.Response.Clear();
					context.Response.Expires = 1440;
					context.Response.ContentType = ChartHttpHandler.GetContentType(imageFormat);
					byte[] array2 = memoryStream2.ToArray();
					context.Response.OutputStream.Write(array2, 0, array2.Length);
					if (flag2)
					{
						try
						{
							Thread.Sleep(200);
							File.Delete(context.Server.MapPath(text2));
						}
						catch (Exception ex)
						{
							context.Trace.Warn(ex.ToString());
						}
					}
					context.Response.End();
				}
			}
		}

		// Token: 0x0600EFCA RID: 61386 RVA: 0x00369CD8 File Offset: 0x00367ED8
		private bool IsFilePathSecure(string imageName, string virtualPath)
		{
			if (!imageName.StartsWith(virtualPath))
			{
				return false;
			}
			Regex regex = new Regex("chart_(\\{){0,1}[0-9a-fA-F]{8}\\-([0-9a-fA-F]{4}\\-){3}[0-9a-fA-F]{12}(\\}){0,1}");
			if (!regex.Match(imageName).Success)
			{
				return false;
			}
			string[] array = new string[]
			{
				".BMP",
				".EXIF",
				".GIF",
				".ICON",
				".JPEG",
				".PNG",
				".TIFF",
				".WMF"
			};
			bool result = false;
			foreach (string value in array)
			{
				if (imageName.ToUpper().EndsWith(value))
				{
					result = true;
				}
			}
			return result;
		}
	}
}
