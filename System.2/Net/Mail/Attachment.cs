using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x02000257 RID: 599
	public class Attachment : AttachmentBase
	{
		// Token: 0x060016DE RID: 5854 RVA: 0x00075E7C File Offset: 0x0007407C
		internal Attachment()
		{
			base.MimePart.ContentDisposition = new ContentDisposition();
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x00075E94 File Offset: 0x00074094
		public Attachment(string fileName) : base(fileName)
		{
			this.Name = AttachmentBase.ShortNameFromFile(fileName);
			base.MimePart.ContentDisposition = new ContentDisposition();
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x00075EB9 File Offset: 0x000740B9
		public Attachment(string fileName, string mediaType) : base(fileName, mediaType)
		{
			this.Name = AttachmentBase.ShortNameFromFile(fileName);
			base.MimePart.ContentDisposition = new ContentDisposition();
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x00075EE0 File Offset: 0x000740E0
		public Attachment(string fileName, ContentType contentType) : base(fileName, contentType)
		{
			if (contentType.Name == null || contentType.Name == string.Empty)
			{
				this.Name = AttachmentBase.ShortNameFromFile(fileName);
			}
			else
			{
				this.Name = contentType.Name;
			}
			base.MimePart.ContentDisposition = new ContentDisposition();
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00075F39 File Offset: 0x00074139
		public Attachment(Stream contentStream, string name) : base(contentStream, null, null)
		{
			this.Name = name;
			base.MimePart.ContentDisposition = new ContentDisposition();
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x00075F5B File Offset: 0x0007415B
		public Attachment(Stream contentStream, string name, string mediaType) : base(contentStream, null, mediaType)
		{
			this.Name = name;
			base.MimePart.ContentDisposition = new ContentDisposition();
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00075F7D File Offset: 0x0007417D
		public Attachment(Stream contentStream, ContentType contentType) : base(contentStream, contentType)
		{
			this.Name = contentType.Name;
			base.MimePart.ContentDisposition = new ContentDisposition();
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x00075FA4 File Offset: 0x000741A4
		internal void SetContentTypeName(bool allowUnicode)
		{
			if (!allowUnicode && this.name != null && this.name.Length != 0 && !MimeBasePart.IsAscii(this.name, false))
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

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x00076025 File Offset: 0x00074225
		// (set) Token: 0x060016E7 RID: 5863 RVA: 0x00076030 File Offset: 0x00074230
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
				this.SetContentTypeName(true);
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x0007607A File Offset: 0x0007427A
		// (set) Token: 0x060016E9 RID: 5865 RVA: 0x00076082 File Offset: 0x00074282
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
					this.SetContentTypeName(true);
				}
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x000760AC File Offset: 0x000742AC
		public ContentDisposition ContentDisposition
		{
			get
			{
				return base.MimePart.ContentDisposition;
			}
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x000760B9 File Offset: 0x000742B9
		internal override void PrepareForSending(bool allowUnicode)
		{
			if (this.name != null && this.name != string.Empty)
			{
				this.SetContentTypeName(allowUnicode);
			}
			base.PrepareForSending(allowUnicode);
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x000760E4 File Offset: 0x000742E4
		public static Attachment CreateAttachmentFromString(string content, string name)
		{
			Attachment attachment = new Attachment();
			attachment.SetContentFromString(content, null, string.Empty);
			attachment.Name = name;
			return attachment;
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x0007610C File Offset: 0x0007430C
		public static Attachment CreateAttachmentFromString(string content, string name, Encoding contentEncoding, string mediaType)
		{
			Attachment attachment = new Attachment();
			attachment.SetContentFromString(content, contentEncoding, mediaType);
			attachment.Name = name;
			return attachment;
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x00076130 File Offset: 0x00074330
		public static Attachment CreateAttachmentFromString(string content, ContentType contentType)
		{
			Attachment attachment = new Attachment();
			attachment.SetContentFromString(content, contentType);
			attachment.Name = contentType.Name;
			return attachment;
		}

		// Token: 0x04001774 RID: 6004
		private string name;

		// Token: 0x04001775 RID: 6005
		private Encoding nameEncoding;
	}
}
