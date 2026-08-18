using System;
using System.Collections;
using System.Text;

namespace MailBee.Mime
{
	// Token: 0x0200056A RID: 1386
	public class TextBodyPartCollection : CollectionBase
	{
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06002E02 RID: 11778 RVA: 0x000DDB8C File Offset: 0x000DCB8C
		// (set) Token: 0x06002E03 RID: 11779 RVA: 0x000DDB94 File Offset: 0x000DCB94
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

		// Token: 0x06002E04 RID: 11780 RVA: 0x000DDBA0 File Offset: 0x000DCBA0
		internal TextBodyPartCollection()
		{
			this.b.IsCollectionMember = false;
			this.b.RootCollection = this;
			this.c.IsCollectionMember = false;
			this.c.RootCollection = this;
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x000DDC03 File Offset: 0x000DCC03
		internal TextBodyPartCollection(MailMessage A_0) : this()
		{
			this.d = A_0;
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06002E06 RID: 11782 RVA: 0x000DDC12 File Offset: 0x000DCC12
		public TextBodyPart Html
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06002E07 RID: 11783 RVA: 0x000DDC1A File Offset: 0x000DCC1A
		public TextBodyPart Plain
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x1700059D RID: 1437
		public TextBodyPart this[int index]
		{
			get
			{
				return (TextBodyPart)base.List[index];
			}
			set
			{
				base.List[index] = value;
				if (this.d != null)
				{
					this.d.MimeParts.NeedToRebuild = true;
				}
				this.a = true;
			}
		}

		// Token: 0x1700059E RID: 1438
		public TextBodyPart this[string contentType]
		{
			get
			{
				if (contentType == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				foreach (object obj in base.List)
				{
					TextBodyPart textBodyPart = (TextBodyPart)obj;
					if (textBodyPart.Headers.a("Content-Type") != null && string.Compare(textBodyPart.Headers.a("Content-Type").Value, contentType, true) == 0)
					{
						return textBodyPart;
					}
				}
				return null;
			}
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x000DDCF8 File Offset: 0x000DCCF8
		public TextBodyPart Add(TextBodyPart part)
		{
			return this.a(part, false);
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x000DDD04 File Offset: 0x000DCD04
		private TextBodyPart a(TextBodyPart A_0, bool A_1)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if ((string.Compare(A_0.AsMimePart.ContentType, "text/plain", true) == 0 || A_0.AsMimePart.ContentType == string.Empty) && !this.b.IsCollectionMember)
			{
				if (this.d != null && this.d.Parser != null && this.d.Parser.FixCrLf)
				{
					StringBuilder stringBuilder = new StringBuilder(A_0.Text);
					stringBuilder.Replace("\r", "");
					stringBuilder.Replace("\n", "\r\n");
					A_0.Text = stringBuilder.ToString();
				}
				this.b = A_0;
			}
			if (string.Compare(A_0.AsMimePart.ContentType, "text/html", true) == 0 && !this.c.IsCollectionMember)
			{
				this.c = A_0;
			}
			if (A_1)
			{
				base.List.Insert(0, A_0);
			}
			else
			{
				base.List.Add(A_0);
			}
			A_0.RootCollection = this;
			A_0.AsMimePart.ParentMessage = this.d;
			A_0.IsCollectionMember = true;
			if (this.d != null)
			{
				if (!this.d.MimeParts.c(A_0.AsMimePart))
				{
					this.d.MimeParts.b(A_0.AsMimePart);
					this.a = true;
				}
				if (this.d.Charset != null && this.d.Charset.Length != 0 && (A_0.Charset == null || A_0.Charset == string.Empty))
				{
					A_0.CharsetInternal = this.d.Charset;
				}
			}
			return A_0;
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x000DDEB6 File Offset: 0x000DCEB6
		internal TextBodyPart c(TextBodyPart A_0)
		{
			return this.a(A_0, true);
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x000DDEC0 File Offset: 0x000DCEC0
		public TextBodyPart Add(string contentType)
		{
			return this.Add(contentType, null, false);
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x000DDECC File Offset: 0x000DCECC
		public TextBodyPart Add(string contentType, HeaderCollection customHeaders, bool noDefaultHeaders)
		{
			if (contentType == null && !noDefaultHeaders)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			TextBodyPart part = new TextBodyPart(contentType, customHeaders, noDefaultHeaders);
			return this.Add(part);
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x000DDEF8 File Offset: 0x000DCEF8
		public new void Clear()
		{
			foreach (object obj in base.List)
			{
				TextBodyPart textBodyPart = (TextBodyPart)obj;
				if (this.d != null && this.d.MimeParts.c(textBodyPart.AsMimePart))
				{
					MimePart.a(this.d.MimePart, textBodyPart.AsMimePart);
					this.d.MimeParts.a(textBodyPart.AsMimePart);
				}
			}
			base.List.Clear();
			this.b = new TextBodyPart("text/plain");
			this.c = new TextBodyPart("text/html");
			this.b.IsCollectionMember = false;
			this.b.RootCollection = this;
			this.c.IsCollectionMember = false;
			this.c.RootCollection = this;
			this.a = true;
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x000DDFF8 File Offset: 0x000DCFF8
		public bool Remove(string contentType)
		{
			if (contentType == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			TextBodyPart a_ = this[contentType];
			if (this.b(a_))
			{
				this.a(a_);
				return true;
			}
			return false;
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x000DE02C File Offset: 0x000DD02C
		internal void a(TextBodyPart A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.List.Remove(A_0);
			if (this.d != null && this.d.MimeParts.c(A_0.AsMimePart))
			{
				this.d.MimeParts.a(A_0.AsMimePart);
				MimePart.a(this.d.MimePart, A_0.AsMimePart);
			}
			this.a = true;
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x000DE0A4 File Offset: 0x000DD0A4
		public new void RemoveAt(int index)
		{
			TextBodyPart a_ = (TextBodyPart)base.List[index];
			this.a(a_);
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x000DE0CA File Offset: 0x000DD0CA
		internal bool b(TextBodyPart A_0)
		{
			return base.List.Contains(A_0);
		}

		// Token: 0x04001FAF RID: 8111
		private bool a;

		// Token: 0x04001FB0 RID: 8112
		private TextBodyPart b = new TextBodyPart("text/plain");

		// Token: 0x04001FB1 RID: 8113
		private TextBodyPart c = new TextBodyPart("text/html");

		// Token: 0x04001FB2 RID: 8114
		private MailMessage d;
	}
}
