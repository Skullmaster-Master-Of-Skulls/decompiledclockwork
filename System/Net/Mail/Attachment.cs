using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x02000679 RID: 1657
	public class Attachment : AttachmentBase
	{
		// Token: 0x06003339 RID: 13113 RVA: 0x000D8749 File Offset: 0x000D7749
		internal Attachment()
		{
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x000D8751 File Offset: 0x000D7751
		public Attachment(string fileName) : base(fileName)
		{
			this.Name = AttachmentBase.ShortNameFromFile(fileName);
		}

		// Token: 0x0600333B RID: 13115 RVA: 0x000D8766 File Offset: 0x000D7766
		public Attachment(string fileName, string mediaType) : base(fileName, mediaType)
		{
			this.Name = AttachmentBase.ShortNameFromFile(fileName);
		}

		// Token: 0x0600333C RID: 13116 RVA: 0x000D877C File Offset: 0x000D777C
		public Attachment(string fileName, ContentType contentType) : base(fileName, contentType)
		{
			if (contentType.Name == null || contentType.Name == string.Empty)
			{
				this.Name = AttachmentBase.ShortNameFromFile(fileName);
				return;
			}
			this.Name = contentType.Name;
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x000D87B9 File Offset: 0x000D77B9
		public Attachment(Stream contentStream, string name) : base(contentStream, null, null)
		{
			this.Name = name;
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x000D87CB File Offset: 0x000D77CB
		public Attachment(Stream contentStream, string name, string mediaType) : base(contentStream, null, mediaType)
		{
			this.Name = name;
		}

		// Token: 0x0600333F RID: 13119 RVA: 0x000D87DD File Offset: 0x000D77DD
		public Attachment(Stream contentStream, ContentType contentType) : base(contentStream, contentType)
		{
			this.Name = contentType.Name;
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x000D87F4 File Offset: 0x000D77F4
		internal void SetContentTypeName()
		{
			if (this.name != null && this.name.Length != 0 && !MimeBasePart.IsAscii(this.name, false))
			{
				Encoding encoding = this.NameEncoding;
				if (encoding == null)
				{
					encoding = Encoding.GetEncoding("utf-8");
				}
				base.MimePart.ContentType.Name = MimeBasePart.EncodeHeaderValue(this.name, encoding, MimeBasePart.ShouldUseBase64Encoding(encoding));
				return;
			}
			base.MimePart.ContentType.Name = this.name;
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06003341 RID: 13121 RVA: 0x000D8872 File Offset: 0x000D7872
		// (set) Token: 0x06003342 RID: 13122 RVA: 0x000D887C File Offset: 0x000D787C
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				Encoding encoding = MimeBasePart.DecodeEncoding(value);
				if (encoding != null)
				{
					this.nameEncoding = encoding;
					this.name = MimeBasePart.DecodeHeaderValue(value);
					base.MimePart.ContentType.Name = value;
					return;
				}
				this.name = value;
				this.SetContentTypeName();
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06003343 RID: 13123 RVA: 0x000D88C5 File Offset: 0x000D78C5
		// (set) Token: 0x06003344 RID: 13124 RVA: 0x000D88CD File Offset: 0x000D78CD
		public Encoding NameEncoding
		{
			get
			{
				return this.nameEncoding;
			}
			set
			{
				this.nameEncoding = value;
				if (this.name != null && this.name != string.Empty)
				{
					this.SetContentTypeName();
				}
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06003345 RID: 13125 RVA: 0x000D88F8 File Offset: 0x000D78F8
		public ContentDisposition ContentDisposition
		{
			get
			{
				ContentDisposition contentDisposition = base.MimePart.ContentDisposition;
				if (contentDisposition == null)
				{
					contentDisposition = new ContentDisposition();
					base.MimePart.ContentDisposition = contentDisposition;
				}
				return contentDisposition;
			}
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x000D8927 File Offset: 0x000D7927
		internal override void PrepareForSending()
		{
			if (this.name != null && this.name != string.Empty)
			{
				this.SetContentTypeName();
			}
			base.PrepareForSending();
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x000D8950 File Offset: 0x000D7950
		public static Attachment CreateAttachmentFromString(string content, string name)
		{
			Attachment attachment = new Attachment();
			attachment.SetContentFromString(content, null, string.Empty);
			attachment.Name = name;
			return attachment;
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x000D8978 File Offset: 0x000D7978
		public static Attachment CreateAttachmentFromString(string content, string name, Encoding contentEncoding, string mediaType)
		{
			Attachment attachment = new Attachment();
			attachment.SetContentFromString(content, contentEncoding, mediaType);
			attachment.Name = name;
			return attachment;
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x000D899C File Offset: 0x000D799C
		public static Attachment CreateAttachmentFromString(string content, ContentType contentType)
		{
			Attachment attachment = new Attachment();
			attachment.SetContentFromString(content, contentType);
			attachment.Name = contentType.Name;
			return attachment;
		}

		// Token: 0x04002F8E RID: 12174
		private string name;

		// Token: 0x04002F8F RID: 12175
		private Encoding nameEncoding;
	}
}
