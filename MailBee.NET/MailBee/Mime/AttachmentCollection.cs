using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using a;
using a.i;

namespace MailBee.Mime
{
	// Token: 0x0200052A RID: 1322
	public class AttachmentCollection : CollectionBase
	{
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06002B9B RID: 11163 RVA: 0x000CDE26 File Offset: 0x000CCE26
		// (set) Token: 0x06002B9C RID: 11164 RVA: 0x000CDE2E File Offset: 0x000CCE2E
		internal bool NeedToRebuild
		{
			get
			{
				return this.a;
			}
			set
			{
				this.a = value;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06002B9D RID: 11165 RVA: 0x000CDE37 File Offset: 0x000CCE37
		public int LastResult
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06002B9E RID: 11166 RVA: 0x000CDE40 File Offset: 0x000CCE40
		public int InlineCount
		{
			get
			{
				int num = 0;
				using (IEnumerator enumerator = base.List.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((Attachment)enumerator.Current).IsInline)
						{
							num++;
						}
					}
				}
				return num;
			}
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x000CDEA0 File Offset: 0x000CCEA0
		internal AttachmentCollection(MailMessage A_0)
		{
			this.c = A_0;
		}

		// Token: 0x170004EB RID: 1259
		public Attachment this[int index]
		{
			get
			{
				return (Attachment)base.List[index];
			}
			set
			{
				base.List[index] = value;
				if (this.c != null)
				{
					this.c.MimePart.NeedToRebuild = true;
				}
				this.a = true;
			}
		}

		// Token: 0x170004EC RID: 1260
		public Attachment this[string filename]
		{
			get
			{
				foreach (object obj in base.List)
				{
					Attachment attachment = (Attachment)obj;
					if (string.Compare(attachment.Filename, filename, true) == 0)
					{
						return attachment;
					}
				}
				return null;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06002BA3 RID: 11171 RVA: 0x000CDF60 File Offset: 0x000CCF60
		public bool ThrowExceptions
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (set) Token: 0x06002BA4 RID: 11172 RVA: 0x000CDF68 File Offset: 0x000CCF68
		internal bool ThrowExceptionsInternal
		{
			set
			{
				this.d = value;
			}
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x000CDF74 File Offset: 0x000CCF74
		public void Add(Attachment attach)
		{
			if (attach == null)
			{
				this.b = 21;
				throw new MailBeeInvalidArgumentException(this.b);
			}
			this.b = 0;
			this.a(attach, base.List.Count);
			attach.ThrowExceptionsInternal = this.d;
			base.List.Add(attach);
			attach.AsMimePart.ParentMessage = this.c;
			if (this.c != null && !this.c.MimeParts.c(attach.AsMimePart))
			{
				this.c.MimeParts.b(attach.AsMimePart);
			}
			this.a = true;
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x000CE019 File Offset: 0x000CD019
		public bool Add(string filename)
		{
			return this.Add(filename, null);
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x000CE023 File Offset: 0x000CD023
		public bool Add(string filename, string targetFilename)
		{
			return this.Add(filename, targetFilename, null);
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000CE030 File Offset: 0x000CD030
		public bool Add(string filename, string targetFilename, string contentID)
		{
			return this.b(filename, targetFilename, contentID, null, null, NewAttachmentOptions.None, MailTransferEncoding.Base64, true);
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x000CE04C File Offset: 0x000CD04C
		public void Add(byte[] data, string targetFilename, string contentID, string contentType, HeaderCollection customHeaders, NewAttachmentOptions options, MailTransferEncoding mailEnc)
		{
			this.a(data, targetFilename, contentID, contentType, customHeaders, options, mailEnc, false);
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x000CE06C File Offset: 0x000CD06C
		private void a(byte[] A_0, string A_1, string A_2, string A_3, HeaderCollection A_4, NewAttachmentOptions A_5, MailTransferEncoding A_6, bool A_7)
		{
			if (A_0 == null || A_1 == null)
			{
				this.b = 21;
				throw new MailBeeInvalidArgumentException(this.b);
			}
			A_1 = ap.f(A_1);
			this.b = 0;
			if (A_3 == null)
			{
				A_3 = global::a.i.k.e(Path.GetExtension(A_1));
			}
			if (A_2 == null)
			{
				A_2 = string.Empty;
			}
			MimePart mimePart = new MimePart(this.c);
			Header header = new Header("Content-Type", A_3);
			mimePart.Headers.b(header);
			header.HeaderParameters = new global::a.i.j();
			header.HeaderParameters.c(new global::a.i.n("name", A_1));
			string text = "attachment";
			if (A_2 != null && A_2.Length != 0)
			{
				mimePart.Headers.Add("Content-ID", global::a.i.k.f(A_2), false);
				text = "inline";
			}
			if ((A_5 & NewAttachmentOptions.Inline) == NewAttachmentOptions.Inline)
			{
				text = "inline";
			}
			if (text == "inline" && (A_5 & NewAttachmentOptions.NoContentDispositionForInline) > NewAttachmentOptions.None)
			{
				text = null;
			}
			if (text != null)
			{
				Header header2 = new Header("Content-Disposition", text);
				mimePart.Headers.b(header2);
				header2.HeaderParameters = new global::a.i.j();
				header2.HeaderParameters.c(new global::a.i.n("filename", A_1));
			}
			if ((A_5 & NewAttachmentOptions.NoDefaultHeaders) == NewAttachmentOptions.NoDefaultHeaders)
			{
				mimePart.Headers.Clear();
			}
			if (A_4 != null)
			{
				foreach (object obj in A_4)
				{
					Header a_ = (Header)obj;
					mimePart.Headers.b(a_);
				}
			}
			bool flag = global::a.i.n.a(A_3, '/').a() == "message";
			if (A_6 != MailTransferEncoding.None)
			{
				if (!flag || !A_7)
				{
					mimePart.MimePartTransferEncoding = A_6;
				}
			}
			else if (!flag)
			{
				mimePart.MimePartTransferEncoding = MailTransferEncoding.Base64;
			}
			if ((A_5 & NewAttachmentOptions.ReplaceIfExists) == NewAttachmentOptions.ReplaceIfExists)
			{
				Attachment attachment = this[A_1];
				if (attachment != null)
				{
					this.b(attachment);
				}
			}
			mimePart.PartValueAsBytes = A_0;
			Attachment attach = new Attachment(mimePart);
			this.Add(attach);
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x000CE274 File Offset: 0x000CD274
		public bool Add(string filename, string targetFilename, string contentID, string contentType, HeaderCollection customHeaders, NewAttachmentOptions options, MailTransferEncoding mailEnc)
		{
			return this.b(filename, targetFilename, contentID, contentType, customHeaders, options, mailEnc, false);
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x000CE294 File Offset: 0x000CD294
		private bool b(string A_0, string A_1, string A_2, string A_3, HeaderCollection A_4, NewAttachmentOptions A_5, MailTransferEncoding A_6, bool A_7)
		{
			if (A_0 == null || A_0 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			this.b = 0;
			byte[] a_ = null;
			if ((A_5 & NewAttachmentOptions.PathIsUri) == NewAttachmentOptions.PathIsUri)
			{
				try
				{
					a_ = global::a.i.b.d(A_0);
					goto IL_71;
				}
				catch (MailBeeWebException ex)
				{
					this.b = ex.ErrorCode;
					if (this.d)
					{
						throw;
					}
					return false;
				}
			}
			try
			{
				a_ = ap.e(A_0);
			}
			catch (MailBeeIOException ex2)
			{
				this.b = ex2.ErrorCode;
				if (this.d)
				{
					throw;
				}
				return false;
			}
			IL_71:
			if (A_1 == null)
			{
				A_1 = Path.GetFileName(A_0);
			}
			this.a(a_, A_1, A_2, A_3, A_4, A_5, A_6, A_7);
			return true;
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x000CE350 File Offset: 0x000CD350
		public Task<bool> AddAsync(string filename)
		{
			return this.AddAsync(filename, null);
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x000CE35A File Offset: 0x000CD35A
		public Task<bool> AddAsync(string filename, string targetFilename)
		{
			return this.AddAsync(filename, targetFilename, null);
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x000CE368 File Offset: 0x000CD368
		public Task<bool> AddAsync(string filename, string targetFilename, string contentID)
		{
			return this.a(filename, targetFilename, contentID, null, null, NewAttachmentOptions.Inline, MailTransferEncoding.Base64, true);
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x000CE384 File Offset: 0x000CD384
		public Task<bool> AddAsync(string filename, string targetFilename, string contentID, string contentType, HeaderCollection customHeaders, NewAttachmentOptions options, MailTransferEncoding mailEnc)
		{
			return this.a(filename, targetFilename, contentID, contentType, customHeaders, options, mailEnc, false);
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x000CE3A4 File Offset: 0x000CD3A4
		private Task<bool> a(string A_0, string A_1, string A_2, string A_3, HeaderCollection A_4, NewAttachmentOptions A_5, MailTransferEncoding A_6, bool A_7)
		{
			AttachmentCollection.b b;
			b.d = this;
			b.c = A_0;
			b.f = A_1;
			b.h = A_2;
			b.i = A_3;
			b.j = A_4;
			b.e = A_5;
			b.k = A_6;
			b.l = A_7;
			b.b = AsyncTaskMethodBuilder<bool>.Create();
			b.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<AttachmentCollection.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x000CE430 File Offset: 0x000CD430
		public void Add(MailMessage message, string targetFilename, string contentID, string contentType, HeaderCollection customHeaders, NewAttachmentOptions options, MailTransferEncoding mailEnc)
		{
			if (message == null)
			{
				this.b = 21;
				throw new MailBeeInvalidArgumentException(this.b);
			}
			this.b = 0;
			if (contentType == null)
			{
				contentType = "message/rfc822";
			}
			if (targetFilename == null)
			{
				if (message.Subject.Length > 0)
				{
					targetFilename = string.Format(Global.DefaultCulture, "{0}.eml", new object[]
					{
						message.Subject
					});
				}
				else
				{
					targetFilename = string.Empty;
				}
			}
			byte[] messageRawData = message.GetMessageRawData();
			this.a(messageRawData, targetFilename, contentID, contentType, customHeaders, options, mailEnc, false);
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x000CE4BC File Offset: 0x000CD4BC
		public bool Add(Stream stream, string targetFilename, string contentID, string contentType, HeaderCollection customHeaders, NewAttachmentOptions options, MailTransferEncoding mailEnc)
		{
			if (stream == null || targetFilename == null)
			{
				this.b = 21;
				throw new MailBeeInvalidArgumentException(this.b);
			}
			this.b = 0;
			byte[] a_ = null;
			try
			{
				a_ = ap.f(stream);
			}
			catch (IOException a_2)
			{
				this.b = 30;
				if (this.d)
				{
					throw new MailBeeStreamException(30, a_2);
				}
				return false;
			}
			this.a(a_, targetFilename, contentID, contentType, customHeaders, options, mailEnc, false);
			return true;
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x000CE538 File Offset: 0x000CD538
		public void Add(string data, string contentType, HeaderCollection customHeaders, MailTransferEncoding mailEnc)
		{
			Encoding encoding = (this.c == null) ? Global.DefaultEncoding : bb.a(this.c.Charset);
			this.Add(encoding.GetBytes(data), string.Empty, null, contentType, customHeaders, NewAttachmentOptions.Inline | NewAttachmentOptions.NoContentDispositionForInline, mailEnc);
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x000CE580 File Offset: 0x000CD580
		internal void b(Attachment A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.List.Remove(A_0);
			if (this.c != null && this.c.MimeParts.c(A_0.AsMimePart))
			{
				MimePart.a(this.c.MimePart, A_0.AsMimePart);
			}
			this.a = true;
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x000CE5E4 File Offset: 0x000CD5E4
		public new void RemoveAt(int index)
		{
			Attachment a_ = (Attachment)base.List[index];
			this.b(a_);
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x000CE60C File Offset: 0x000CD60C
		public new void Clear()
		{
			this.b = 0;
			foreach (object obj in base.List)
			{
				Attachment attachment = (Attachment)obj;
				if (this.c != null && this.c.MimeParts.c(attachment.AsMimePart))
				{
					MimePart.a(this.c.MimePart, attachment.AsMimePart);
				}
			}
			base.List.Clear();
			this.a = true;
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x000CE6B0 File Offset: 0x000CD6B0
		public bool Remove(string filename)
		{
			if (filename == null)
			{
				this.b = 21;
				throw new MailBeeInvalidArgumentException(this.b);
			}
			this.b = 0;
			Attachment attachment = this[filename];
			if (attachment != null)
			{
				this.b(attachment);
				return true;
			}
			return false;
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x000CE6F0 File Offset: 0x000CD6F0
		internal bool a(Attachment A_0)
		{
			return base.List.Contains(A_0);
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x000CE6FE File Offset: 0x000CD6FE
		public bool SaveAll(string folderName)
		{
			return this.SaveAll(folderName, false);
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x000CE708 File Offset: 0x000CD708
		public bool SaveAll(string folderName, bool ignoreInlineAttachments)
		{
			if (folderName == null || folderName == string.Empty)
			{
				this.b = 22;
				throw new MailBeeInvalidArgumentException(this.b);
			}
			this.b = 0;
			try
			{
				foreach (object obj in base.List)
				{
					Attachment attachment = (Attachment)obj;
					if (!attachment.IsInline || !ignoreInlineAttachments)
					{
						attachment.SaveToFolder(folderName, false);
					}
				}
			}
			catch (IOException a_)
			{
				this.b = 30;
				if (this.d)
				{
					throw new MailBeeIOException(30, a_);
				}
				return false;
			}
			return true;
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x000CE7CC File Offset: 0x000CD7CC
		public Task<bool> SaveAllAsync(string folderName)
		{
			return this.SaveAllAsync(folderName, false);
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x000CE7D8 File Offset: 0x000CD7D8
		public Task<bool> SaveAllAsync(string folderName, bool ignoreInlineAttachments)
		{
			AttachmentCollection.a a;
			a.d = this;
			a.c = folderName;
			a.e = ignoreInlineAttachments;
			a.b = AsyncTaskMethodBuilder<bool>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<AttachmentCollection.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x000CE830 File Offset: 0x000CD830
		internal void a(Attachment A_0, int A_1)
		{
			string a_ = global::a.i.k.a(A_0, (this.c == null || !A_0.IsMessageInside) ? null : this.c.Subject);
			foreach (object obj in base.List)
			{
				Attachment attachment = (Attachment)obj;
				if (attachment.Filename == a_ || attachment.NameInternal == a_)
				{
					a_ = AttachmentCollection.a(a_, A_1);
					break;
				}
			}
			A_0.FilenameInternal = a_;
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x000CE8D4 File Offset: 0x000CD8D4
		private static string a(string A_0, int A_1)
		{
			return string.Format(Global.DefaultCulture, "{0}[{1}]{2}", new object[]
			{
				Path.GetFileNameWithoutExtension(A_0),
				A_1.ToString(CultureInfo.InvariantCulture),
				Path.GetExtension(A_0)
			});
		}

		// Token: 0x04001E03 RID: 7683
		private bool a;

		// Token: 0x04001E04 RID: 7684
		private int b;

		// Token: 0x04001E05 RID: 7685
		private MailMessage c;

		// Token: 0x04001E06 RID: 7686
		private bool d = true;
	}
}
