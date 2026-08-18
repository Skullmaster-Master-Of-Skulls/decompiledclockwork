using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using a;
using a.i;

namespace MailBee.Mime
{
	// Token: 0x02000549 RID: 1353
	public class HeaderCollection : CollectionBase
	{
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06002C26 RID: 11302 RVA: 0x000D16FE File Offset: 0x000D06FE
		// (set) Token: 0x06002C27 RID: 11303 RVA: 0x000D1706 File Offset: 0x000D0706
		internal MimePart MimePart
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

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06002C28 RID: 11304 RVA: 0x000D1710 File Offset: 0x000D0710
		// (set) Token: 0x06002C29 RID: 11305 RVA: 0x000D1780 File Offset: 0x000D0780
		internal bool NeedToRebuild
		{
			get
			{
				if (!this.b)
				{
					using (IEnumerator enumerator = base.List.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (((Header)enumerator.Current).NeedToRebuild)
							{
								this.b = true;
								break;
							}
						}
					}
				}
				return this.b;
			}
			set
			{
				if (!value)
				{
					foreach (object obj in base.List)
					{
						((Header)obj).NeedToRebuild = value;
					}
				}
				this.b = value;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06002C2A RID: 11306 RVA: 0x000D17E4 File Offset: 0x000D07E4
		internal global::a.i.i RawHeaders
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000D17EC File Offset: 0x000D07EC
		public HeaderCollection()
		{
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x000D17FF File Offset: 0x000D07FF
		internal HeaderCollection(MimePart A_0)
		{
			this.a = A_0;
		}

		// Token: 0x17000505 RID: 1285
		public Header this[int index]
		{
			get
			{
				return (Header)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x17000506 RID: 1286
		public string this[string name]
		{
			get
			{
				foreach (object obj in base.List)
				{
					Header header = (Header)obj;
					if (header.Name != null && string.Compare(header.Name, name, true) == 0)
					{
						return header.Value;
					}
				}
				return null;
			}
			set
			{
				bool flag = false;
				for (int i = 0; i < base.List.Count; i++)
				{
					if (string.Compare(((Header)base.List[i]).Name, name, true) == 0)
					{
						((Header)base.List[i]).Value = value;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.Add(name, value, false);
				}
			}
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x000D1920 File Offset: 0x000D0920
		public HeaderCollection Items(string name)
		{
			HeaderCollection headerCollection = null;
			foreach (object obj in base.List)
			{
				Header header = (Header)obj;
				if (string.Compare(header.Name, name, true) == 0)
				{
					if (headerCollection == null)
					{
						headerCollection = new HeaderCollection();
					}
					headerCollection.b(header);
				}
			}
			return headerCollection;
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x000D1998 File Offset: 0x000D0998
		internal int b(Header A_0)
		{
			this.b = true;
			A_0.ParentCollection = this;
			return base.List.Add(A_0);
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x000D19B4 File Offset: 0x000D09B4
		internal void a(HeaderCollection A_0)
		{
			foreach (object obj in A_0)
			{
				Header a_ = (Header)obj;
				this.b(a_);
				this.b = true;
			}
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x000D1A10 File Offset: 0x000D0A10
		public bool Add(string name, string value, bool overwrite)
		{
			return this.Add(name, value, overwrite, base.List.Count);
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x000D1A28 File Offset: 0x000D0A28
		public bool Add(string name, string value, bool overwrite, int index)
		{
			if (index < 0 || index > base.List.Count)
			{
				throw new MailBeeInvalidArgumentException(23);
			}
			if (overwrite)
			{
				this.Remove(name);
			}
			Header header = Header.a(string.Format("{0}: {1}", name, value));
			if (index < base.List.Count)
			{
				this.a(index, header);
				this.b = true;
			}
			else
			{
				header.NeedToRebuild = true;
				this.b(header);
			}
			return true;
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x000D1A9F File Offset: 0x000D0A9F
		internal int d(Header A_0)
		{
			return base.List.IndexOf(A_0);
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x000D1AAD File Offset: 0x000D0AAD
		internal void a(int A_0, Header A_1)
		{
			this.b = true;
			A_1.ParentCollection = this;
			base.List.Insert(A_0, A_1);
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x000D1ACA File Offset: 0x000D0ACA
		internal void a(Header A_0)
		{
			base.List.Remove(A_0);
			this.b = true;
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000D1ADF File Offset: 0x000D0ADF
		public new void RemoveAt(int index)
		{
			base.List.RemoveAt(index);
			this.b = true;
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x000D1AF4 File Offset: 0x000D0AF4
		public new void Clear()
		{
			base.List.Clear();
			this.b = true;
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x000D1B08 File Offset: 0x000D0B08
		public bool Remove(string name)
		{
			if (name == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			int i = 0;
			bool result = false;
			while (i < base.Count)
			{
				if (string.Compare(this[i].Name, name, true) == 0)
				{
					this.RemoveAt(i);
					result = true;
				}
				else
				{
					i++;
				}
			}
			return result;
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000D1B54 File Offset: 0x000D0B54
		public void RemoveCustomHeaders()
		{
			StringCollection stringCollection = new StringCollection();
			stringCollection.AddRange(global::a.i.f.a);
			HeaderCollection headerCollection = new HeaderCollection();
			foreach (object obj in base.List)
			{
				Header header = (Header)obj;
				if (!stringCollection.Contains(header.Name.ToLower()))
				{
					headerCollection.b(header);
				}
			}
			foreach (object obj2 in headerCollection)
			{
				Header value = (Header)obj2;
				base.List.Remove(value);
				this.b = true;
			}
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000D1C30 File Offset: 0x000D0C30
		public void RemoveRouteHeaders()
		{
			StringCollection stringCollection = new StringCollection();
			string[] value = new string[]
			{
				"return-path",
				"received"
			};
			stringCollection.AddRange(value);
			HeaderCollection headerCollection = new HeaderCollection();
			foreach (object obj in base.List)
			{
				Header header = (Header)obj;
				if (stringCollection.Contains(header.Name.ToLower()))
				{
					headerCollection.b(header);
				}
			}
			foreach (object obj2 in headerCollection)
			{
				Header value2 = (Header)obj2;
				base.List.Remove(value2);
				this.b = true;
			}
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000D1D24 File Offset: 0x000D0D24
		internal bool c(Header A_0)
		{
			return base.List.Contains(A_0);
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000D1D34 File Offset: 0x000D0D34
		public bool Exists(string name)
		{
			if (name == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			using (IEnumerator enumerator = base.List.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (string.Compare(((Header)enumerator.Current).Name, name, true) == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x000D1DA8 File Offset: 0x000D0DA8
		internal Header a(string A_0)
		{
			foreach (object obj in base.List)
			{
				Header header = (Header)obj;
				if (header.Name != null && header.Name != null && string.Compare(header.Name, A_0, true) == 0)
				{
					return header;
				}
			}
			return null;
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x000D1E20 File Offset: 0x000D0E20
		internal int a(string A_0, int A_1)
		{
			for (int i = A_1; i > -1; i--)
			{
				Header header = (Header)base.List[i];
				if (header.Name != null && header.Name != null && string.Compare(header.Name, A_0, true) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x000D1E70 File Offset: 0x000D0E70
		internal static HeaderCollection a(string A_0, MimePart A_1)
		{
			HeaderCollection headerCollection = new HeaderCollection(A_1);
			headerCollection.c.a(A_0);
			if (A_0 == null || A_0.Length == 0)
			{
				return headerCollection;
			}
			StringCollection stringCollection = new StringCollection();
			foreach (string text in A_0.Split(new char[]
			{
				'\n'
			}))
			{
				if (text.Length > 0)
				{
					if (text[text.Length - 1] == '\r')
					{
						stringCollection.Add(text.Substring(0, text.Length - 1));
					}
					else
					{
						stringCollection.Add(text);
					}
				}
			}
			int j = 0;
			while (j < stringCollection.Count)
			{
				if (stringCollection[j].Length > 0 && (stringCollection[j][0] == ' ' || stringCollection[j][0] == '\t') && j > 0)
				{
					StringCollection stringCollection2 = stringCollection;
					int i = j - 1;
					stringCollection2[i] += string.Format(CultureInfo.InvariantCulture, "\r\n{0}", new object[]
					{
						stringCollection[j]
					});
					stringCollection.RemoveAt(j);
				}
				else
				{
					j++;
				}
			}
			Encoding a_ = null;
			if (A_1 != null && A_1.ParentMessage != null && A_1.ParentMessage.Parser != null)
			{
				a_ = A_1.ParentMessage.Parser.EncodingOverride;
			}
			foreach (string a_2 in stringCollection)
			{
				Header header = Header.a(a_2, true, a_);
				if (header != null)
				{
					headerCollection.b(header);
				}
			}
			headerCollection.b = false;
			return headerCollection;
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x000D2030 File Offset: 0x000D1030
		internal string a()
		{
			if (!this.NeedToRebuild)
			{
				if (this.a != null && this.a.ParentMessage != null && this.a.ParentMessage.Charset != null && this.a.ParentMessage.Charset.Length > 0)
				{
					Encoding encoding = bb.a(this.a.ParentMessage.Charset);
					if (global::a.i.h.a(encoding))
					{
						encoding = Encoding.UTF8;
					}
					bool flag = true;
					for (int i = 0; i < this.c.c().Length; i++)
					{
						if (this.c.c()[i] > '\u007f')
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						encoding = Encoding.ASCII;
					}
					global::a.i.i i2 = new global::a.i.i(this.c.c(), encoding);
					this.c.a(Global.DefaultEncoding.GetString(i2.g(), 0, i2.g().Length));
				}
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in base.List)
				{
					Header header = (Header)obj;
					if (header.Value != null && header.Value.Length != 0)
					{
						if (this.a != null && this.a.ParentMessage != null)
						{
							string charset = this.a.ParentMessage.Charset;
							if (charset == null || charset == string.Empty)
							{
								stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "{0}\r\n", new object[]
								{
									header.a(MailTransferEncoding.QuotedPrintable, global::a.i.h.b(Global.DefaultEncoding))
								}));
							}
							else
							{
								header.NeedToEncode = true;
								stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "{0}\r\n", new object[]
								{
									header.a(MailTransferEncoding.QuotedPrintable, charset)
								}));
							}
						}
						else
						{
							stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "{0}\r\n", new object[]
							{
								header.a(MailTransferEncoding.None, global::a.i.h.b(Global.DefaultEncoding))
							}));
						}
					}
				}
				this.c.a(stringBuilder.Append("\r\n").ToString());
			}
			return this.c.c();
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x000D22B4 File Offset: 0x000D12B4
		internal void a(XmlWriter A_0)
		{
			A_0.WriteStartElement("Headers");
			foreach (object obj in base.List)
			{
				((Header)obj).a(A_0);
			}
			A_0.WriteEndElement();
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x000D231C File Offset: 0x000D131C
		internal static HeaderCollection b(XmlReader A_0)
		{
			HeaderCollection headerCollection = new HeaderCollection();
			A_0.Read();
			while (A_0.Name == "Header")
			{
				headerCollection.b(Header.b(A_0));
			}
			A_0.Read();
			headerCollection.a();
			return headerCollection;
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x000D2368 File Offset: 0x000D1368
		internal Task b(XmlWriter A_0)
		{
			HeaderCollection.b b;
			b.d = this;
			b.c = A_0;
			b.b = AsyncTaskMethodBuilder.Create();
			b.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<HeaderCollection.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x000D23B8 File Offset: 0x000D13B8
		internal static Task<HeaderCollection> a(XmlReader A_0)
		{
			HeaderCollection.a a;
			a.c = A_0;
			a.b = AsyncTaskMethodBuilder<HeaderCollection>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<HeaderCollection> asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<HeaderCollection.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x04001EAE RID: 7854
		private MimePart a;

		// Token: 0x04001EAF RID: 7855
		private bool b;

		// Token: 0x04001EB0 RID: 7856
		private global::a.i.i c = new global::a.i.i();
	}
}
