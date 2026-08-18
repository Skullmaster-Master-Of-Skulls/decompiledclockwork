using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using a;
using a.i;

namespace MailBee.Mime
{
	// Token: 0x02000546 RID: 1350
	public class Header
	{
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06002BFF RID: 11263 RVA: 0x000D0278 File Offset: 0x000CF278
		// (set) Token: 0x06002C00 RID: 11264 RVA: 0x000D0280 File Offset: 0x000CF280
		internal EmailAddress Address
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

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06002C01 RID: 11265 RVA: 0x000D0289 File Offset: 0x000CF289
		// (set) Token: 0x06002C02 RID: 11266 RVA: 0x000D0291 File Offset: 0x000CF291
		internal EmailAddressCollection AddressCollection
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06002C03 RID: 11267 RVA: 0x000D029A File Offset: 0x000CF29A
		// (set) Token: 0x06002C04 RID: 11268 RVA: 0x000D02C6 File Offset: 0x000CF2C6
		internal bool NeedToRebuild
		{
			get
			{
				if (!this.c && this.g != null && this.g.a())
				{
					this.c = true;
				}
				return this.c;
			}
			set
			{
				if (this.g != null)
				{
					this.g.a(value);
				}
				this.c = value;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06002C05 RID: 11269 RVA: 0x000D02E3 File Offset: 0x000CF2E3
		// (set) Token: 0x06002C06 RID: 11270 RVA: 0x000D02EB File Offset: 0x000CF2EB
		internal bool NeedToEncode
		{
			get
			{
				return this.d;
			}
			set
			{
				this.d = value;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x000D02F4 File Offset: 0x000CF2F4
		// (set) Token: 0x06002C08 RID: 11272 RVA: 0x000D034A File Offset: 0x000CF34A
		public string Name
		{
			get
			{
				if (this.h != null && this.h.MimePart != null && this.h.MimePart.ParentMessage != null)
				{
					return this.h.MimePart.ParentMessage.f(this.e);
				}
				return this.e;
			}
			set
			{
				this.e = value;
				this.c = true;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x000D035C File Offset: 0x000CF35C
		// (set) Token: 0x06002C0A RID: 11274 RVA: 0x000D040C File Offset: 0x000CF40C
		public string Value
		{
			get
			{
				if (this.h == null || this.h.MimePart == null || this.h.MimePart.ParentMessage == null)
				{
					return this.f;
				}
				if (this.h.MimePart.ParentMessage.Parser != null && this.h.MimePart.ParentMessage.Parser.HeadersAsHtml)
				{
					return global::a.i.b.j(this.h.MimePart.ParentMessage.f(this.f));
				}
				return this.h.MimePart.ParentMessage.f(this.f);
			}
			set
			{
				this.f = value;
				this.c();
				this.c = true;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (set) Token: 0x06002C0B RID: 11275 RVA: 0x000D0422 File Offset: 0x000CF422
		internal string ValueInternal
		{
			set
			{
				this.f = value;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06002C0C RID: 11276 RVA: 0x000D042B File Offset: 0x000CF42B
		// (set) Token: 0x06002C0D RID: 11277 RVA: 0x000D0433 File Offset: 0x000CF433
		internal global::a.i.j HeaderParameters
		{
			get
			{
				return this.g;
			}
			set
			{
				this.g = value;
				this.c = true;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06002C0E RID: 11278 RVA: 0x000D0443 File Offset: 0x000CF443
		// (set) Token: 0x06002C0F RID: 11279 RVA: 0x000D044B File Offset: 0x000CF44B
		internal HeaderCollection ParentCollection
		{
			get
			{
				return this.h;
			}
			set
			{
				this.h = value;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06002C10 RID: 11280 RVA: 0x000D0454 File Offset: 0x000CF454
		internal global::a.i.i RawBody
		{
			get
			{
				return this.i;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06002C11 RID: 11281 RVA: 0x000D045C File Offset: 0x000CF45C
		internal global::a.i.i ValueRawBody
		{
			get
			{
				return this.j;
			}
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x000D0464 File Offset: 0x000CF464
		internal Header()
		{
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x000D0498 File Offset: 0x000CF498
		internal Header(string A_0, string A_1) : this(A_0, A_1, null)
		{
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x000D04A4 File Offset: 0x000CF4A4
		internal Header(string A_0, string A_1, global::a.i.j A_2)
		{
			this.Name = A_0;
			this.Value = A_1;
			this.g = A_2;
			this.c = true;
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x000D04FF File Offset: 0x000CF4FF
		internal static Header a(string A_0)
		{
			return Header.a(A_0, true, null);
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x000D050C File Offset: 0x000CF50C
		internal static Header a(string A_0, bool A_1, Encoding A_2)
		{
			if (A_0 == null || A_0.Length == 0)
			{
				return null;
			}
			int num = A_0.IndexOf(':');
			if (num < 0)
			{
				return null;
			}
			Header header = new Header(A_0.Substring(0, num), string.Empty);
			header.i.a(A_0);
			bool flag = A_0.StartsWith("received:", StringComparison.OrdinalIgnoreCase);
			string text = A_0.Substring(num + 1, A_0.Length - (num + 1)).Trim(global::a.i.k.b());
			StringBuilder stringBuilder = new StringBuilder(text);
			if (flag)
			{
				stringBuilder.Replace("\r\n\t", " ");
			}
			else
			{
				stringBuilder.Replace("\r\n\t", string.Empty);
			}
			stringBuilder.Replace("\r\n ", " ");
			text = stringBuilder.ToString();
			header.j.a(text);
			Header.a(header, text, A_1, A_2);
			header.c = false;
			return header;
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x000D05E0 File Offset: 0x000CF5E0
		private bool a()
		{
			string text = this.e.ToLower();
			return !(text == "x-sender") && !(text == "x-receiver");
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x000D0618 File Offset: 0x000CF618
		internal string a(MailTransferEncoding A_0, string A_1)
		{
			if (!this.NeedToRebuild)
			{
				if (this.h != null && this.h.MimePart != null && this.h.MimePart.ParentMessage != null && this.h.MimePart.ParentMessage.Charset != null && this.h.MimePart.ParentMessage.Charset.Length > 0)
				{
					Encoding encoding = bb.a(this.h.MimePart.ParentMessage.Charset);
					if (global::a.i.h.a(encoding))
					{
						encoding = Encoding.UTF8;
					}
					bool flag = true;
					for (int i = 0; i < this.i.c().Length; i++)
					{
						if (this.i.c()[i] > '\u007f')
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						encoding = Encoding.ASCII;
					}
					byte[] bytes = Global.DefaultEncoding.GetBytes(this.i.c());
					this.i.a(encoding.GetString(bytes));
					this.i.b(encoding);
				}
			}
			else
			{
				bool flag2 = false;
				StringBuilder stringBuilder = new StringBuilder();
				if (this.a != null)
				{
					if (global::a.i.h.d(this.Value) && this.a())
					{
						stringBuilder.Append(string.Format("{0}: {1}", this.e, this.a.a(A_0, A_1)));
						flag2 = true;
					}
				}
				else if (this.b != null && global::a.i.h.d(this.Value) && this.a())
				{
					string a_ = ",";
					if (this.h != null && this.h.MimePart != null && this.h.MimePart.ParentMessage != null && this.h.MimePart.ParentMessage.Builder != null)
					{
						a_ = global::a.i.k.a(this.h.MimePart.ParentMessage.Builder.AddressDelimeter);
					}
					stringBuilder.Append(global::a.i.k.b(string.Format("{0}: {1}", this.e, this.b.a(a_, true, A_0, A_1)), Global.UnwrappedLineLengthLimit));
					flag2 = true;
				}
				if (!flag2)
				{
					if (this.f != null)
					{
						this.f = this.f.Replace("\r", "").Replace("\n", "");
					}
					string text = this.f;
					if (this.a())
					{
						text = global::a.i.h.a(this.e, this.f, A_0, A_1, HeaderEncodingOptions.None, true);
					}
					else if (this.a != null)
					{
						text = this.a.a(A_0, A_1);
					}
					stringBuilder.Append(string.Format(Global.DefaultCulture, "{0}: {1}", new object[]
					{
						this.e,
						text
					}));
					if (this.g != null)
					{
						bool a_2 = true;
						if (string.Compare(this.e, "DomainKey-Signature", true) == 0)
						{
							a_2 = false;
						}
						else if (string.Compare(this.e, "DKIM-Signature", true) == 0)
						{
							a_2 = false;
						}
						foreach (object obj in this.g)
						{
							global::a.i.n n = (global::a.i.n)obj;
							if (n.c() != null && n.c() != null && n.c().Length != 0)
							{
								stringBuilder.Append(string.Format(Global.DefaultCulture, ";\r\n\t{0}", new object[]
								{
									n.a(a_2, A_1)
								}));
							}
						}
					}
				}
				this.i.a(stringBuilder.ToString());
			}
			return this.i.c();
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x000D09EC File Offset: 0x000CF9EC
		internal void a(XmlWriter A_0)
		{
			Header header = new Header(this.Name, this.Value, this.HeaderParameters);
			if (!this.NeedToRebuild)
			{
				header = Header.a(this.RawBody.c(), true, null);
			}
			if (header == null)
			{
				return;
			}
			A_0.WriteStartElement("Header");
			A_0.WriteElementString("Name", header.Name);
			if (this.NeedToRebuild)
			{
				A_0.WriteElementString("Value", header.Value);
			}
			else
			{
				A_0.WriteElementString("Value", header.Value);
			}
			if (header.HeaderParameters != null)
			{
				header.HeaderParameters.a(A_0);
			}
			A_0.WriteEndElement();
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x000D0A94 File Offset: 0x000CFA94
		internal static Header b(XmlReader A_0)
		{
			Header header = new Header();
			bool flag = true;
			A_0.Read();
			do
			{
				if (!A_0.IsEmptyElement)
				{
					string name = A_0.Name;
					if (!(name == "Name"))
					{
						if (!(name == "Value"))
						{
							if (!(name == "HeaderParameters"))
							{
								flag = false;
							}
							else
							{
								header.HeaderParameters = global::a.i.j.b(A_0);
							}
						}
						else
						{
							string text = A_0.ReadElementContentAsString();
							header.Value = text;
							header.ValueRawBody.a(text);
						}
					}
					else
					{
						header.Name = A_0.ReadElementContentAsString();
					}
				}
			}
			while (flag);
			A_0.Read();
			header.a(MailTransferEncoding.None, global::a.i.h.b(Global.DefaultEncoding));
			return header;
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x000D0B40 File Offset: 0x000CFB40
		private static void a(Header A_0, string A_1, bool A_2, Encoding A_3)
		{
			A_1 = A_1.Trim(global::a.i.k.b());
			string a_ = A_0.Name.ToLower();
			uint num = global::b.a(a_);
			if (num <= 1874587459U)
			{
				if (num <= 1111836708U)
				{
					if (num != 479423816U)
					{
						if (num != 1111836708U)
						{
							goto IL_1D3;
						}
						if (!(a_ == "to"))
						{
							goto IL_1D3;
						}
						goto IL_1B4;
					}
					else if (!(a_ == "return-receipt-to"))
					{
						goto IL_1D3;
					}
				}
				else if (num != 1127136262U)
				{
					if (num != 1445564707U)
					{
						if (num != 1874587459U)
						{
							goto IL_1D3;
						}
						if (!(a_ == "bcc"))
						{
							goto IL_1D3;
						}
						goto IL_1B4;
					}
					else
					{
						if (!(a_ == "cc"))
						{
							goto IL_1D3;
						}
						goto IL_1B4;
					}
				}
				else if (!(a_ == "disposition-notification-to"))
				{
					goto IL_1D3;
				}
			}
			else if (num <= 2513272949U)
			{
				if (num != 2221692707U)
				{
					if (num != 2513272949U)
					{
						goto IL_1D3;
					}
					if (!(a_ == "from"))
					{
						goto IL_1D3;
					}
				}
				else if (!(a_ == "x-confirm-reading-to"))
				{
					goto IL_1D3;
				}
			}
			else
			{
				if (num != 2593752131U)
				{
					if (num != 3889184348U)
					{
						if (num != 4244048277U)
						{
							goto IL_1D3;
						}
						if (!(a_ == "content-type"))
						{
							goto IL_1D3;
						}
					}
					else if (!(a_ == "content-disposition"))
					{
						goto IL_1D3;
					}
					Header.a(A_0, A_1);
					A_1 = A_0.Value;
					goto IL_1D3;
				}
				if (!(a_ == "reply-to"))
				{
					goto IL_1D3;
				}
				goto IL_1B4;
			}
			EmailAddressCollection emailAddressCollection = EmailAddressCollection.a(A_1, A_0);
			if (emailAddressCollection.Count > 0)
			{
				A_0.a = emailAddressCollection[0];
			}
			else
			{
				A_0.a = new EmailAddress();
			}
			A_0.f = A_0.a.ToString();
			return;
			IL_1B4:
			A_0.b = EmailAddressCollection.a(A_1, A_0);
			A_0.f = A_0.b.ToString();
			return;
			IL_1D3:
			A_0.Value = (A_2 ? global::a.i.h.a(A_1, A_3) : A_1);
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x000D0D34 File Offset: 0x000CFD34
		private static void a(Header A_0, string A_1)
		{
			int num = A_1.IndexOf(';');
			if (num != -1)
			{
				A_0.Value = A_1.Substring(0, num).Trim();
				A_1 = A_1.Remove(0, num + 1);
				Encoding a_ = null;
				if (A_0 != null && A_0.ParentCollection != null && A_0.ParentCollection.MimePart != null && A_0.ParentCollection.MimePart.ParentMessage != null && A_0.ParentCollection.MimePart.ParentMessage.Parser != null)
				{
					a_ = A_0.ParentCollection.MimePart.ParentMessage.Parser.EncodingOverride;
				}
				A_0.HeaderParameters = global::a.i.j.a(A_1, a_);
				return;
			}
			A_0.Value = A_1;
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x000D0DE3 File Offset: 0x000CFDE3
		internal void c()
		{
			if (this.a != null)
			{
				this.a.b();
			}
			if (this.b != null)
			{
				this.b.c();
			}
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x000D0E0B File Offset: 0x000CFE0B
		internal void d()
		{
			if (this.a != null)
			{
				this.Value = this.a.ToString();
			}
			if (this.b != null)
			{
				this.Value = this.b.ToString();
			}
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x000D0E40 File Offset: 0x000CFE40
		internal Header i()
		{
			global::a.i.j j = new global::a.i.j();
			if (this.g != null)
			{
				foreach (object obj in this.g)
				{
					global::a.i.n n = (global::a.i.n)obj;
					j.c(new global::a.i.n(n.a(), n.c()));
				}
			}
			Header header = new Header(this.Name, this.Value, j);
			if (this.a != null)
			{
				header.a = EmailAddress.a(this.a.ToString(), header);
			}
			if (this.b != null)
			{
				header.b = EmailAddressCollection.a(this.b.ToString(), header);
			}
			return header;
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x000D0F10 File Offset: 0x000CFF10
		internal Task b(XmlWriter A_0)
		{
			Header.a a;
			a.c = this;
			a.d = A_0;
			a.b = AsyncTaskMethodBuilder.Create();
			a.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<Header.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x000D0F60 File Offset: 0x000CFF60
		internal static Task<Header> a(XmlReader A_0)
		{
			Header.b b;
			b.c = A_0;
			b.b = AsyncTaskMethodBuilder<Header>.Create();
			b.a = -1;
			AsyncTaskMethodBuilder<Header> asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<Header.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x04001E95 RID: 7829
		private EmailAddress a;

		// Token: 0x04001E96 RID: 7830
		private EmailAddressCollection b;

		// Token: 0x04001E97 RID: 7831
		private bool c;

		// Token: 0x04001E98 RID: 7832
		private bool d;

		// Token: 0x04001E99 RID: 7833
		private string e = string.Empty;

		// Token: 0x04001E9A RID: 7834
		private string f = string.Empty;

		// Token: 0x04001E9B RID: 7835
		private global::a.i.j g;

		// Token: 0x04001E9C RID: 7836
		private HeaderCollection h;

		// Token: 0x04001E9D RID: 7837
		private global::a.i.i i = new global::a.i.i();

		// Token: 0x04001E9E RID: 7838
		private global::a.i.i j = new global::a.i.i();
	}
}
