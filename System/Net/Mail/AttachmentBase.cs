using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x02000676 RID: 1654
	public abstract class AttachmentBase : IDisposable
	{
		// Token: 0x0600330B RID: 13067 RVA: 0x000D7F4F File Offset: 0x000D6F4F
		internal AttachmentBase()
		{
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x000D7F62 File Offset: 0x000D6F62
		protected AttachmentBase(string fileName)
		{
			this.SetContentFromFile(fileName, string.Empty);
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x000D7F81 File Offset: 0x000D6F81
		protected AttachmentBase(string fileName, string mediaType)
		{
			this.SetContentFromFile(fileName, mediaType);
		}

		// Token: 0x0600330E RID: 13070 RVA: 0x000D7F9C File Offset: 0x000D6F9C
		protected AttachmentBase(string fileName, ContentType contentType)
		{
			this.SetContentFromFile(fileName, contentType);
		}

		// Token: 0x0600330F RID: 13071 RVA: 0x000D7FB7 File Offset: 0x000D6FB7
		protected AttachmentBase(Stream contentStream)
		{
			this.part.SetContent(contentStream);
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x000D7FD6 File Offset: 0x000D6FD6
		protected AttachmentBase(Stream contentStream, string mediaType)
		{
			this.part.SetContent(contentStream, null, mediaType);
		}

		// Token: 0x06003311 RID: 13073 RVA: 0x000D7FF7 File Offset: 0x000D6FF7
		internal AttachmentBase(Stream contentStream, string name, string mediaType)
		{
			this.part.SetContent(contentStream, name, mediaType);
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x000D8018 File Offset: 0x000D7018
		protected AttachmentBase(Stream contentStream, ContentType contentType)
		{
			this.part.SetContent(contentStream, contentType);
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x000D8038 File Offset: 0x000D7038
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06003314 RID: 13076 RVA: 0x000D8041 File Offset: 0x000D7041
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				this.part.Dispose();
			}
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x000D8060 File Offset: 0x000D7060
		internal static string ShortNameFromFile(string fileName)
		{
			int num = fileName.LastIndexOfAny(new char[]
			{
				'\\',
				':'
			}, fileName.Length - 1, fileName.Length);
			string result;
			if (num > 0)
			{
				result = fileName.Substring(num + 1, fileName.Length - num - 1);
			}
			else
			{
				result = fileName;
			}
			return result;
		}

		// Token: 0x06003316 RID: 13078 RVA: 0x000D80B4 File Offset: 0x000D70B4
		internal void SetContentFromFile(string fileName, ContentType contentType)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (fileName == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[]
				{
					"fileName"
				}), "fileName");
			}
			Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			this.part.SetContent(stream, contentType);
		}

		// Token: 0x06003317 RID: 13079 RVA: 0x000D8118 File Offset: 0x000D7118
		internal void SetContentFromFile(string fileName, string mediaType)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (fileName == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[]
				{
					"fileName"
				}), "fileName");
			}
			Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			this.part.SetContent(stream, null, mediaType);
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x000D8180 File Offset: 0x000D7180
		internal void SetContentFromString(string contentString, ContentType contentType)
		{
			if (contentString == null)
			{
				throw new ArgumentNullException("content");
			}
			if (this.part.Stream != null)
			{
				this.part.Stream.Close();
			}
			Encoding encoding;
			if (contentType != null && contentType.CharSet != null)
			{
				encoding = Encoding.GetEncoding(contentType.CharSet);
			}
			else if (MimeBasePart.IsAscii(contentString, false))
			{
				encoding = Encoding.ASCII;
			}
			else
			{
				encoding = Encoding.GetEncoding("utf-8");
			}
			byte[] bytes = encoding.GetBytes(contentString);
			this.part.SetContent(new MemoryStream(bytes), contentType);
			if (MimeBasePart.ShouldUseBase64Encoding(encoding))
			{
				this.part.TransferEncoding = TransferEncoding.Base64;
				return;
			}
			this.part.TransferEncoding = TransferEncoding.QuotedPrintable;
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x000D8228 File Offset: 0x000D7228
		internal void SetContentFromString(string contentString, Encoding encoding, string mediaType)
		{
			if (contentString == null)
			{
				throw new ArgumentNullException("content");
			}
			if (this.part.Stream != null)
			{
				this.part.Stream.Close();
			}
			if (mediaType == null || mediaType == string.Empty)
			{
				mediaType = "text/plain";
			}
			int num = 0;
			try
			{
				string text = MailBnfHelper.ReadToken(mediaType, ref num, null);
				if (text.Length == 0 || num >= mediaType.Length || mediaType[num++] != '/')
				{
					throw new ArgumentException(SR.GetString("MediaTypeInvalid"), "mediaType");
				}
				text = MailBnfHelper.ReadToken(mediaType, ref num, null);
				if (text.Length == 0 || num < mediaType.Length)
				{
					throw new ArgumentException(SR.GetString("MediaTypeInvalid"), "mediaType");
				}
			}
			catch (FormatException)
			{
				throw new ArgumentException(SR.GetString("MediaTypeInvalid"), "mediaType");
			}
			ContentType contentType = new ContentType(mediaType);
			if (encoding == null)
			{
				if (MimeBasePart.IsAscii(contentString, false))
				{
					encoding = Encoding.ASCII;
				}
				else
				{
					encoding = Encoding.GetEncoding("utf-8");
				}
			}
			contentType.CharSet = encoding.BodyName;
			byte[] bytes = encoding.GetBytes(contentString);
			this.part.SetContent(new MemoryStream(bytes), contentType);
			if (MimeBasePart.ShouldUseBase64Encoding(encoding))
			{
				this.part.TransferEncoding = TransferEncoding.Base64;
				return;
			}
			this.part.TransferEncoding = TransferEncoding.QuotedPrintable;
		}

		// Token: 0x0600331A RID: 13082 RVA: 0x000D8380 File Offset: 0x000D7380
		internal virtual void PrepareForSending()
		{
			this.part.ResetStream();
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x0600331B RID: 13083 RVA: 0x000D838D File Offset: 0x000D738D
		public Stream ContentStream
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				return this.part.Stream;
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x0600331C RID: 13084 RVA: 0x000D83B4 File Offset: 0x000D73B4
		// (set) Token: 0x0600331D RID: 13085 RVA: 0x000D842C File Offset: 0x000D742C
		public string ContentId
		{
			get
			{
				string text = this.part.ContentID;
				if (string.IsNullOrEmpty(text))
				{
					text = Guid.NewGuid().ToString();
					this.ContentId = text;
					return text;
				}
				if (text.Length >= 2 && text[0] == '<' && text[text.Length - 1] == '>')
				{
					return text.Substring(1, text.Length - 2);
				}
				return text;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.part.ContentID = null;
					return;
				}
				if (value.IndexOfAny(new char[]
				{
					'<',
					'>'
				}) != -1)
				{
					throw new ArgumentException(SR.GetString("MailHeaderInvalidCID"), "value");
				}
				this.part.ContentID = "<" + value + ">";
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x0600331E RID: 13086 RVA: 0x000D8499 File Offset: 0x000D7499
		// (set) Token: 0x0600331F RID: 13087 RVA: 0x000D84A6 File Offset: 0x000D74A6
		public ContentType ContentType
		{
			get
			{
				return this.part.ContentType;
			}
			set
			{
				this.part.ContentType = value;
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06003320 RID: 13088 RVA: 0x000D84B4 File Offset: 0x000D74B4
		// (set) Token: 0x06003321 RID: 13089 RVA: 0x000D84C1 File Offset: 0x000D74C1
		public TransferEncoding TransferEncoding
		{
			get
			{
				return this.part.TransferEncoding;
			}
			set
			{
				this.part.TransferEncoding = value;
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06003322 RID: 13090 RVA: 0x000D84D0 File Offset: 0x000D74D0
		// (set) Token: 0x06003323 RID: 13091 RVA: 0x000D84F5 File Offset: 0x000D74F5
		internal Uri ContentLocation
		{
			get
			{
				Uri result;
				if (!Uri.TryCreate(this.part.ContentLocation, UriKind.RelativeOrAbsolute, out result))
				{
					return null;
				}
				return result;
			}
			set
			{
				this.part.ContentLocation = ((value == null) ? null : (value.IsAbsoluteUri ? value.AbsoluteUri : value.OriginalString));
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06003324 RID: 13092 RVA: 0x000D8524 File Offset: 0x000D7524
		internal MimePart MimePart
		{
			get
			{
				return this.part;
			}
		}

		// Token: 0x04002F8A RID: 12170
		internal bool disposed;

		// Token: 0x04002F8B RID: 12171
		private MimePart part = new MimePart();
	}
}
