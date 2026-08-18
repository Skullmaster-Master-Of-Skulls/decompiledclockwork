using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x02000256 RID: 598
	public abstract class AttachmentBase : IDisposable
	{
		// Token: 0x060016C4 RID: 5828 RVA: 0x000758A5 File Offset: 0x00073AA5
		internal AttachmentBase()
		{
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x000758B8 File Offset: 0x00073AB8
		protected AttachmentBase(string fileName)
		{
			this.SetContentFromFile(fileName, string.Empty);
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x000758D7 File Offset: 0x00073AD7
		protected AttachmentBase(string fileName, string mediaType)
		{
			this.SetContentFromFile(fileName, mediaType);
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x000758F2 File Offset: 0x00073AF2
		protected AttachmentBase(string fileName, ContentType contentType)
		{
			this.SetContentFromFile(fileName, contentType);
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x0007590D File Offset: 0x00073B0D
		protected AttachmentBase(Stream contentStream)
		{
			this.part.SetContent(contentStream);
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x0007592C File Offset: 0x00073B2C
		protected AttachmentBase(Stream contentStream, string mediaType)
		{
			this.part.SetContent(contentStream, null, mediaType);
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x0007594D File Offset: 0x00073B4D
		internal AttachmentBase(Stream contentStream, string name, string mediaType)
		{
			this.part.SetContent(contentStream, name, mediaType);
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x0007596E File Offset: 0x00073B6E
		protected AttachmentBase(Stream contentStream, ContentType contentType)
		{
			this.part.SetContent(contentStream, contentType);
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x0007598E File Offset: 0x00073B8E
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00075997 File Offset: 0x00073B97
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				this.part.Dispose();
			}
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x000759B8 File Offset: 0x00073BB8
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

		// Token: 0x060016CF RID: 5839 RVA: 0x00075A08 File Offset: 0x00073C08
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

		// Token: 0x060016D0 RID: 5840 RVA: 0x00075A6C File Offset: 0x00073C6C
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

		// Token: 0x060016D1 RID: 5841 RVA: 0x00075AD0 File Offset: 0x00073CD0
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

		// Token: 0x060016D2 RID: 5842 RVA: 0x00075B78 File Offset: 0x00073D78
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

		// Token: 0x060016D3 RID: 5843 RVA: 0x00075CD0 File Offset: 0x00073ED0
		internal virtual void PrepareForSending(bool allowUnicode)
		{
			this.part.ResetStream();
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060016D4 RID: 5844 RVA: 0x00075CDD File Offset: 0x00073EDD
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

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x00075D04 File Offset: 0x00073F04
		// (set) Token: 0x060016D6 RID: 5846 RVA: 0x00075D7C File Offset: 0x00073F7C
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

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x00075DE7 File Offset: 0x00073FE7
		// (set) Token: 0x060016D8 RID: 5848 RVA: 0x00075DF4 File Offset: 0x00073FF4
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

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x00075E02 File Offset: 0x00074002
		// (set) Token: 0x060016DA RID: 5850 RVA: 0x00075E0F File Offset: 0x0007400F
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

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x00075E20 File Offset: 0x00074020
		// (set) Token: 0x060016DC RID: 5852 RVA: 0x00075E45 File Offset: 0x00074045
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

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x00075E74 File Offset: 0x00074074
		internal MimePart MimePart
		{
			get
			{
				return this.part;
			}
		}

		// Token: 0x04001772 RID: 6002
		internal bool disposed;

		// Token: 0x04001773 RID: 6003
		private MimePart part = new MimePart();
	}
}
