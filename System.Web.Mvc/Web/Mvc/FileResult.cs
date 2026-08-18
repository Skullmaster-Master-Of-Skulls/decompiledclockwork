using System;
using System.Net.Mime;
using System.Text;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x0200019E RID: 414
	public abstract class FileResult : ActionResult
	{
		// Token: 0x06000B9F RID: 2975 RVA: 0x0001E7BC File Offset: 0x0001C9BC
		protected FileResult(string contentType)
		{
			if (string.IsNullOrEmpty(contentType))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "contentType");
			}
			this.ContentType = contentType;
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x0001E7E3 File Offset: 0x0001C9E3
		// (set) Token: 0x06000BA1 RID: 2977 RVA: 0x0001E7EB File Offset: 0x0001C9EB
		public string ContentType { get; private set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0001E7F4 File Offset: 0x0001C9F4
		// (set) Token: 0x06000BA3 RID: 2979 RVA: 0x0001E805 File Offset: 0x0001CA05
		public string FileDownloadName
		{
			get
			{
				return this._fileDownloadName ?? string.Empty;
			}
			set
			{
				this._fileDownloadName = value;
			}
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0001E810 File Offset: 0x0001CA10
		public override void ExecuteResult(ControllerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			HttpResponseBase response = context.HttpContext.Response;
			response.ContentType = this.ContentType;
			if (!string.IsNullOrEmpty(this.FileDownloadName))
			{
				string headerValue = FileResult.ContentDispositionUtil.GetHeaderValue(this.FileDownloadName);
				context.HttpContext.Response.AddHeader("Content-Disposition", headerValue);
			}
			this.WriteFile(response);
		}

		// Token: 0x06000BA5 RID: 2981
		protected abstract void WriteFile(HttpResponseBase response);

		// Token: 0x04000313 RID: 787
		private string _fileDownloadName;

		// Token: 0x0200019F RID: 415
		internal static class ContentDispositionUtil
		{
			// Token: 0x06000BA6 RID: 2982 RVA: 0x0001E87C File Offset: 0x0001CA7C
			private static void AddByteToStringBuilder(byte b, StringBuilder builder)
			{
				builder.Append('%');
				FileResult.ContentDispositionUtil.AddHexDigitToStringBuilder(b >> 4, builder);
				FileResult.ContentDispositionUtil.AddHexDigitToStringBuilder((int)(b % 16), builder);
			}

			// Token: 0x06000BA7 RID: 2983 RVA: 0x0001E8A7 File Offset: 0x0001CAA7
			private static void AddHexDigitToStringBuilder(int digit, StringBuilder builder)
			{
				builder.Append("0123456789ABCDEF"[digit]);
			}

			// Token: 0x06000BA8 RID: 2984 RVA: 0x0001E8BC File Offset: 0x0001CABC
			private static string CreateRfc2231HeaderValue(string filename)
			{
				StringBuilder stringBuilder = new StringBuilder("attachment; filename*=UTF-8''");
				byte[] bytes = Encoding.UTF8.GetBytes(filename);
				foreach (byte b in bytes)
				{
					if (FileResult.ContentDispositionUtil.IsByteValidHeaderValueCharacter(b))
					{
						stringBuilder.Append((char)b);
					}
					else
					{
						FileResult.ContentDispositionUtil.AddByteToStringBuilder(b, stringBuilder);
					}
				}
				return stringBuilder.ToString();
			}

			// Token: 0x06000BA9 RID: 2985 RVA: 0x0001E918 File Offset: 0x0001CB18
			public static string GetHeaderValue(string fileName)
			{
				foreach (char c in fileName)
				{
					if (c > '\u007f')
					{
						return FileResult.ContentDispositionUtil.CreateRfc2231HeaderValue(fileName);
					}
				}
				ContentDisposition contentDisposition = new ContentDisposition
				{
					FileName = fileName
				};
				return contentDisposition.ToString();
			}

			// Token: 0x06000BAA RID: 2986 RVA: 0x0001E970 File Offset: 0x0001CB70
			private static bool IsByteValidHeaderValueCharacter(byte b)
			{
				if (48 <= b && b <= 57)
				{
					return true;
				}
				if (97 <= b && b <= 122)
				{
					return true;
				}
				if (65 <= b && b <= 90)
				{
					return true;
				}
				if (b <= 46)
				{
					if (b != 33)
					{
						switch (b)
						{
						case 36:
						case 38:
							break;
						case 37:
							return false;
						default:
							switch (b)
							{
							case 43:
							case 45:
							case 46:
								break;
							case 44:
								return false;
							default:
								return false;
							}
							break;
						}
					}
				}
				else if (b != 58 && b != 95 && b != 126)
				{
					return false;
				}
				return true;
			}

			// Token: 0x04000315 RID: 789
			private const string HexDigits = "0123456789ABCDEF";
		}
	}
}
