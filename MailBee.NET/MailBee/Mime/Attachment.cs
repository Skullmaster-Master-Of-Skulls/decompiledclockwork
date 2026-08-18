using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using a;
using a.h;
using a.i;
using MailBee.Tnef;

namespace MailBee.Mime
{
	// Token: 0x02000527 RID: 1319
	public class Attachment
	{
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06002B6C RID: 11116 RVA: 0x000CCD33 File Offset: 0x000CBD33
		public MimePart AsMimePart
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06002B6D RID: 11117 RVA: 0x000CCD3C File Offset: 0x000CBD3C
		public string ContentID
		{
			get
			{
				char[] trimChars = new char[]
				{
					' ',
					'<',
					'>'
				};
				if (this.a != null && this.a.Headers != null)
				{
					Header header = this.a.Headers.a("Content-ID");
					if (header != null)
					{
						if (this.a.ParentMessage != null)
						{
							return this.a.ParentMessage.f(header.Value.Trim(trimChars));
						}
						return header.Value.Trim(trimChars);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06002B6E RID: 11118 RVA: 0x000CCDC8 File Offset: 0x000CBDC8
		public string ContentLocation
		{
			get
			{
				if (this.a != null && this.a.Headers != null)
				{
					Header header = this.a.Headers.a("Content-Location");
					if (header != null)
					{
						if (this.a.ParentMessage != null)
						{
							return this.a.ParentMessage.f(header.Value);
						}
						return header.Value;
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06002B6F RID: 11119 RVA: 0x000CCE34 File Offset: 0x000CBE34
		public string ContentType
		{
			get
			{
				if (this.a != null)
				{
					Header header = this.a.Headers.a("content-type");
					if (header != null)
					{
						if (this.a != null && this.a.ParentMessage != null)
						{
							return this.a.ParentMessage.f(header.Value);
						}
						return header.Value;
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06002B70 RID: 11120 RVA: 0x000CCE9C File Offset: 0x000CBE9C
		public string Description
		{
			get
			{
				if (this.a != null && this.a.Headers != null)
				{
					Header header = this.a.Headers.a("Description");
					if (header != null)
					{
						if (this.a.ParentMessage != null)
						{
							return this.a.ParentMessage.f(header.Value);
						}
						return header.Value;
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06002B71 RID: 11121 RVA: 0x000CCF08 File Offset: 0x000CBF08
		public string FilenameOriginal
		{
			get
			{
				if (this.a != null && this.a.ParentMessage != null && this.a.ParentMessage.Parser != null && this.a.ParentMessage.Parser.HeadersAsHtml)
				{
					return global::a.i.b.j(this.FilenameOriginalInternal);
				}
				return this.FilenameOriginalInternal;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06002B72 RID: 11122 RVA: 0x000CCF68 File Offset: 0x000CBF68
		internal string FilenameOriginalInternal
		{
			get
			{
				if (this.a != null && this.a.Headers != null)
				{
					Header header = this.a.Headers.a("Content-Disposition");
					if (header != null && header.HeaderParameters != null)
					{
						foreach (object obj in header.HeaderParameters)
						{
							global::a.i.n n = (global::a.i.n)obj;
							if (string.Compare(n.a(), "filename", true) == 0)
							{
								return (this.a.ParentMessage != null) ? this.a.ParentMessage.f(n.c()) : n.c();
							}
						}
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x000CD044 File Offset: 0x000CC044
		internal void c(string A_0)
		{
			this.b = A_0;
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06002B74 RID: 11124 RVA: 0x000CD050 File Offset: 0x000CC050
		public string SavedAs
		{
			get
			{
				if (this.a == null || this.a.ParentMessage == null)
				{
					return this.b;
				}
				if (this.a.ParentMessage.Parser != null && this.a.ParentMessage.Parser.HeadersAsHtml)
				{
					return global::a.i.b.j(this.b);
				}
				return this.a.ParentMessage.f(this.b);
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06002B75 RID: 11125 RVA: 0x000CD0C4 File Offset: 0x000CC0C4
		public string Filename
		{
			get
			{
				if (this.a == null || this.a.ParentMessage == null)
				{
					return this.c;
				}
				if (this.a.ParentMessage.Parser != null && this.a.ParentMessage.Parser.HeadersAsHtml)
				{
					return global::a.i.b.j(this.c);
				}
				return this.a.ParentMessage.f(this.c);
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (set) Token: 0x06002B76 RID: 11126 RVA: 0x000CD138 File Offset: 0x000CC138
		internal string FilenameInternal
		{
			set
			{
				this.c = value;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06002B77 RID: 11127 RVA: 0x000CD141 File Offset: 0x000CC141
		public HeaderCollection Headers
		{
			get
			{
				if (this.a != null)
				{
					return this.a.Headers;
				}
				return null;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06002B78 RID: 11128 RVA: 0x000CD158 File Offset: 0x000CC158
		public bool IsFile
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06002B79 RID: 11129 RVA: 0x000CD160 File Offset: 0x000CC160
		public bool IsInline
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06002B7A RID: 11130 RVA: 0x000CD168 File Offset: 0x000CC168
		internal bool IsRelated
		{
			get
			{
				return !this.f && ((this.ContentID != null && this.ContentID.Length > 0) || (this.ContentLocation != null && this.ContentLocation.Length > 0));
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06002B7B RID: 11131 RVA: 0x000CD1A4 File Offset: 0x000CC1A4
		public bool IsMessageInside
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06002B7C RID: 11132 RVA: 0x000CD1AC File Offset: 0x000CC1AC
		public bool IsTnef
		{
			get
			{
				return global::a.h.f.b(this.ContentType);
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06002B7D RID: 11133 RVA: 0x000CD1BC File Offset: 0x000CC1BC
		public bool IsVCard
		{
			get
			{
				if (this.ContentType != null)
				{
					if (this.ContentType.ToLower().StartsWith("text/x-vcard") || this.ContentType.ToLower().StartsWith("text/directory"))
					{
						return true;
					}
					if (this.ContentType.ToLower().StartsWith("application/octet-stream"))
					{
						string text = "BEGIN:VCARD";
						return this.Size >= text.Length && text == Encoding.ASCII.GetString(this.GetData(0, text.Length), 0, text.Length).ToUpper();
					}
				}
				return false;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06002B7E RID: 11134 RVA: 0x000CD25C File Offset: 0x000CC25C
		public bool IsZip
		{
			get
			{
				return this.h;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06002B7F RID: 11135 RVA: 0x000CD264 File Offset: 0x000CC264
		public int LastResult
		{
			get
			{
				return this.i;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06002B80 RID: 11136 RVA: 0x000CD26C File Offset: 0x000CC26C
		public string Name
		{
			get
			{
				if (this.a != null && this.a.ParentMessage != null && this.a.ParentMessage.Parser != null && this.a.ParentMessage.Parser.HeadersAsHtml)
				{
					return global::a.i.b.j(this.NameInternal);
				}
				return this.NameInternal;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06002B81 RID: 11137 RVA: 0x000CD2CC File Offset: 0x000CC2CC
		internal string NameInternal
		{
			get
			{
				if (this.a != null && this.a.Headers != null)
				{
					Header header = this.a.Headers.a("Content-Type");
					if (header != null && header.HeaderParameters != null)
					{
						foreach (object obj in header.HeaderParameters)
						{
							global::a.i.n n = (global::a.i.n)obj;
							if (string.Compare(n.a(), "name", true) == 0)
							{
								return (this.a.ParentMessage != null) ? this.a.ParentMessage.f(n.c()) : n.c();
							}
						}
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06002B82 RID: 11138 RVA: 0x000CD3A8 File Offset: 0x000CC3A8
		public string RawHeader
		{
			get
			{
				if (this.a == null || this.a.ParentMessage == null)
				{
					return string.Empty;
				}
				if (this.a.ParentMessage.Parser != null && this.a.ParentMessage.Parser.HeadersAsHtml)
				{
					return global::a.i.b.j(this.a.ParentMessage.f(this.a.RawHeader));
				}
				return this.a.ParentMessage.f(this.a.RawHeader);
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06002B83 RID: 11139 RVA: 0x000CD435 File Offset: 0x000CC435
		public int Size
		{
			get
			{
				if (this.a != null && this.a.PartValue != null)
				{
					return this.a.PartValue.e();
				}
				return 0;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06002B84 RID: 11140 RVA: 0x000CD45E File Offset: 0x000CC45E
		public bool ThrowExceptions
		{
			get
			{
				return this.j;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (set) Token: 0x06002B85 RID: 11141 RVA: 0x000CD466 File Offset: 0x000CC466
		internal bool ThrowExceptionsInternal
		{
			set
			{
				this.j = value;
			}
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x000CD46F File Offset: 0x000CC46F
		internal Attachment(bool A_0)
		{
			this.j = A_0;
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x000CD4B0 File Offset: 0x000CC4B0
		public Attachment(MimePart src)
		{
			this.a = src;
			this.a(src);
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x000CD504 File Offset: 0x000CC504
		private void a(MimePart A_0)
		{
			if (A_0 == null)
			{
				return;
			}
			if (A_0 != null && A_0.ParentMessage != null && A_0.ParentMessage.MimeParts != null)
			{
				MimePart mimePart = A_0.ParentMessage.MimeParts["multipart/mixed"];
				if (mimePart != null && mimePart.SubParts != null && mimePart.SubParts.d(this.a) >= 0)
				{
					this.f = true;
				}
			}
			this.a(A_0.Headers);
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x000CD578 File Offset: 0x000CC578
		private void a(HeaderCollection A_0)
		{
			foreach (object obj in A_0)
			{
				Header header = (Header)obj;
				string text = header.Name.ToLower();
				if (!(text == "content-type"))
				{
					if (text == "content-disposition")
					{
						if (string.Compare(header.Value, "attachment", true) == 0)
						{
							this.d = true;
							this.e = false;
						}
						if (header.HeaderParameters != null && header.HeaderParameters.b("filename") != null)
						{
							this.d = true;
						}
						try
						{
							if (string.Compare(Path.GetExtension(this.FilenameOriginalInternal), ".zip", true) == 0)
							{
								this.h = true;
							}
						}
						catch (ArgumentException)
						{
						}
					}
				}
				else if (string.Compare(header.Value, "message/rfc822", true) == 0)
				{
					this.g = true;
				}
			}
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x000CD688 File Offset: 0x000CC688
		public AttachmentCollection GetAttachmentsFromTnef()
		{
			return this.GetAttachmentsFromTnef(TnefExtractionOptions.ExtractAttachments);
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x000CD691 File Offset: 0x000CC691
		public AttachmentCollection GetAttachmentsFromTnef(TnefExtractionOptions options)
		{
			return TnefParser.a(this.a.PartValueAsBytes, options, this.j);
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x000CD6AA File Offset: 0x000CC6AA
		public byte[] GetData()
		{
			return this.GetData(0, -1);
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x000CD6B4 File Offset: 0x000CC6B4
		public byte[] GetData(int offset, int size)
		{
			this.i = 0;
			byte[] array = new byte[0];
			if (this.a != null)
			{
				if (offset < 0 || offset > this.a.PartValueAsBytes.Length)
				{
					throw new MailBeeInvalidArgumentException(23);
				}
				if (size < -1 || size > this.a.PartValueAsBytes.Length)
				{
					throw new MailBeeInvalidArgumentException(23);
				}
				if (size == -1)
				{
					size = this.a.PartValueAsBytes.Length - offset;
				}
				array = new byte[size];
				Buffer.BlockCopy(this.a.PartValueAsBytes, offset, array, 0, size);
			}
			return array;
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x000CD740 File Offset: 0x000CC740
		public MailMessage GetEncapsulatedMessage()
		{
			return this.b(false);
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x000CD74C File Offset: 0x000CC74C
		internal MailMessage b(bool A_0)
		{
			this.i = 0;
			MailMessage mailMessage = null;
			if (string.Compare(this.ContentType, "message/rfc822", true) == 0)
			{
				if (this.a != null)
				{
					mailMessage = new MailMessage(new ao(this.a.PartValueAsBytes), A_0);
				}
				else
				{
					mailMessage.LoadMessage(this.GetData());
				}
			}
			return mailMessage;
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x000CD7A4 File Offset: 0x000CC7A4
		public bool Save(string filename, bool overwrite)
		{
			this.i = 0;
			if (filename == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			filename = this.a(filename, overwrite);
			try
			{
				if (this.a != null)
				{
					ap.b(filename, this.a.PartValue.d(), this.a.PartValue.b(), this.a.PartValue.e(), null);
				}
			}
			catch (MailBeeIOException ex)
			{
				this.i = ex.ErrorCode;
				if (this.j)
				{
					throw;
				}
				return false;
			}
			this.b = Path.GetFullPath(filename);
			return true;
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x000CD84C File Offset: 0x000CC84C
		public Task<bool> SaveAsync(string filename, bool overwrite)
		{
			Attachment.b b;
			b.c = this;
			b.d = filename;
			b.e = overwrite;
			b.b = AsyncTaskMethodBuilder<bool>.Create();
			b.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<Attachment.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x000CD8A4 File Offset: 0x000CC8A4
		private string a(string A_0, bool A_1)
		{
			if (File.Exists(A_0) && !A_1)
			{
				int num = 1;
				string text = A_0;
				try
				{
					while (File.Exists(text))
					{
						string directoryName = Path.GetDirectoryName(A_0);
						string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(A_0);
						string extension = Path.GetExtension(A_0);
						text = string.Format(Global.DefaultCulture, "{0}[{1}]{2}", new object[]
						{
							ap.f(fileNameWithoutExtension),
							num,
							extension
						});
						text = ap.a(directoryName, text);
						num++;
					}
				}
				catch (ArgumentException)
				{
				}
				A_0 = text;
			}
			return A_0;
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x000CD934 File Offset: 0x000CC934
		public bool SaveToFolder(string folderName, bool overwrite)
		{
			string text = this.a(folderName);
			return text != null && this.Save(text, overwrite);
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x000CD958 File Offset: 0x000CC958
		public Task<bool> SaveToFolderAsync(string folderName, bool overwrite)
		{
			Attachment.a a;
			a.c = this;
			a.d = folderName;
			a.e = overwrite;
			a.b = AsyncTaskMethodBuilder<bool>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<Attachment.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x000CD9B0 File Offset: 0x000CC9B0
		private string a(string A_0)
		{
			this.i = 0;
			if (A_0 == null)
			{
				A_0 = string.Empty;
			}
			string result = string.Empty;
			try
			{
				if (A_0.Length > 0 && !Directory.Exists(A_0))
				{
					Directory.CreateDirectory(A_0);
				}
				if (this.FilenameOriginalInternal != null && this.FilenameOriginalInternal.Length != 0)
				{
					result = ap.a(A_0, ap.f(this.FilenameOriginalInternal));
				}
				else if (this.NameInternal != null && this.NameInternal.Length != 0)
				{
					result = ap.a(A_0, ap.f(this.NameInternal));
				}
				else if (this.Filename != null && this.Filename.Length == 0)
				{
					result = ap.a(A_0, string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
					{
						global::a.i.k.a(),
						global::a.i.k.d(this.ContentType)
					}));
				}
				else
				{
					string text = ap.f(this.Filename);
					if (text.Length > 127)
					{
						string text2 = text.Substring(text.LastIndexOf('.'));
						text = text.Substring(0, 127 - text2.Length) + text2;
					}
					result = ap.a(A_0, text);
				}
			}
			catch (ArgumentException a_)
			{
				this.i = 32;
				if (this.j)
				{
					throw new MailBeeIOException(20, a_);
				}
				return null;
			}
			catch (UnauthorizedAccessException a_2)
			{
				this.i = 32;
				if (this.j)
				{
					throw new MailBeeIOException(32, a_2);
				}
				return null;
			}
			catch (IOException a_3)
			{
				this.i = 30;
				if (this.j)
				{
					throw new MailBeeIOException(30, a_3);
				}
				return null;
			}
			return result;
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x000CDB60 File Offset: 0x000CCB60
		internal Attachment a()
		{
			return new Attachment(this.a.g());
		}

		// Token: 0x04001DED RID: 7661
		private MimePart a = new MimePart(null);

		// Token: 0x04001DEE RID: 7662
		private string b = string.Empty;

		// Token: 0x04001DEF RID: 7663
		private string c = string.Empty;

		// Token: 0x04001DF0 RID: 7664
		private bool d;

		// Token: 0x04001DF1 RID: 7665
		private bool e = true;

		// Token: 0x04001DF2 RID: 7666
		private bool f;

		// Token: 0x04001DF3 RID: 7667
		private bool g;

		// Token: 0x04001DF4 RID: 7668
		private bool h;

		// Token: 0x04001DF5 RID: 7669
		private int i;

		// Token: 0x04001DF6 RID: 7670
		private bool j = true;
	}
}
