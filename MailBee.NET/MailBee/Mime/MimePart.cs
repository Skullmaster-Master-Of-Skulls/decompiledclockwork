using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using a;
using a.i;

namespace MailBee.Mime
{
	// Token: 0x02000563 RID: 1379
	public class MimePart
	{
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06002D92 RID: 11666 RVA: 0x000DB6C1 File Offset: 0x000DA6C1
		// (set) Token: 0x06002D93 RID: 11667 RVA: 0x000DB6F9 File Offset: 0x000DA6F9
		internal bool NeedToRebuild
		{
			get
			{
				if (this.d != null && this.d.NeedToRebuild)
				{
					this.a = true;
				}
				if (this.f.NeedToRebuild)
				{
					this.a = true;
				}
				return this.a;
			}
			set
			{
				if (this.d != null)
				{
					this.d.NeedToRebuild = value;
				}
				this.f.NeedToRebuild = value;
				this.a = value;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06002D94 RID: 11668 RVA: 0x000DB722 File Offset: 0x000DA722
		// (set) Token: 0x06002D95 RID: 11669 RVA: 0x000DB72A File Offset: 0x000DA72A
		internal ao PartValue
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

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06002D96 RID: 11670 RVA: 0x000DB733 File Offset: 0x000DA733
		// (set) Token: 0x06002D97 RID: 11671 RVA: 0x000DB740 File Offset: 0x000DA740
		internal byte[] PartValueAsBytes
		{
			get
			{
				return this.b.c();
			}
			set
			{
				this.b = new ao(value);
				this.a = true;
			}
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x000DB755 File Offset: 0x000DA755
		internal bool n()
		{
			return this.b != null && this.b.d() != null && this.b.e() > 0;
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x000DB77C File Offset: 0x000DA77C
		internal void d()
		{
			this.b = new ao(new byte[0]);
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x000DB78F File Offset: 0x000DA78F
		public byte[] GetRawData()
		{
			return this.RawBody;
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06002D9B RID: 11675 RVA: 0x000DB797 File Offset: 0x000DA797
		// (set) Token: 0x06002D9C RID: 11676 RVA: 0x000DB7AA File Offset: 0x000DA7AA
		internal string PartValueAsString
		{
			get
			{
				return this.b.a(this.a());
			}
			set
			{
				if (value != null)
				{
					this.b = ao.a(value, this.a());
					return;
				}
				this.b = new ao(new byte[0]);
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06002D9D RID: 11677 RVA: 0x000DB7D4 File Offset: 0x000DA7D4
		public string Boundary
		{
			get
			{
				Header header = this.f.a("Content-Type");
				if (header != null && header.HeaderParameters != null)
				{
					global::a.i.n n = header.HeaderParameters.b("boundary");
					if (n != null && n.c() != null)
					{
						if (this.g != null)
						{
							return this.g.f(n.c());
						}
						return n.c();
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06002D9E RID: 11678 RVA: 0x000DB83F File Offset: 0x000DA83F
		public string Charset
		{
			get
			{
				if (this.g != null)
				{
					return this.g.f(this.CharsetInternal);
				}
				return this.CharsetInternal;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06002D9F RID: 11679 RVA: 0x000DB864 File Offset: 0x000DA864
		public string ContentID
		{
			get
			{
				if (this.f["Content-ID"] == null)
				{
					return string.Empty;
				}
				if (this.g != null)
				{
					return this.g.f(this.f.a("Content-ID").Value);
				}
				return this.f.a("Content-ID").Value;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06002DA0 RID: 11680 RVA: 0x000DB8C8 File Offset: 0x000DA8C8
		public string ContentLocation
		{
			get
			{
				if (this.f["Content-Location"] == null)
				{
					return string.Empty;
				}
				if (this.g != null)
				{
					return this.g.f(this.f.a("Content-Location").Value);
				}
				return this.f.a("Content-Location").Value;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06002DA1 RID: 11681 RVA: 0x000DB92C File Offset: 0x000DA92C
		public string Description
		{
			get
			{
				if (this.f["Content-Description"] == null)
				{
					return string.Empty;
				}
				if (this.g != null)
				{
					return this.g.f(this.f.a("Content-Description").Value);
				}
				return this.f.a("Content-Description").Value;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06002DA2 RID: 11682 RVA: 0x000DB990 File Offset: 0x000DA990
		public string Disposition
		{
			get
			{
				if (this.f["Content-Disposition"] == null)
				{
					return string.Empty;
				}
				if (this.g != null)
				{
					return this.g.f(this.f.a("Content-Disposition").Value);
				}
				return this.f.a("Content-Disposition").Value;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06002DA3 RID: 11683 RVA: 0x000DB9F4 File Offset: 0x000DA9F4
		public string Filename
		{
			get
			{
				Header header = this.f.a("content-disposition");
				if (header != null && header.HeaderParameters != null)
				{
					global::a.i.n n = header.HeaderParameters.b("filename");
					if (n != null && n.c() != null)
					{
						if (this.g != null)
						{
							return this.g.f(n.c());
						}
						return n.c();
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06002DA4 RID: 11684 RVA: 0x000DBA5F File Offset: 0x000DAA5F
		public bool IsComplete
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06002DA5 RID: 11685 RVA: 0x000DBA67 File Offset: 0x000DAA67
		public bool IsFile
		{
			get
			{
				return this.Filename != null && this.Filename.Length != 0;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06002DA6 RID: 11686 RVA: 0x000DBA81 File Offset: 0x000DAA81
		public bool IsInline
		{
			get
			{
				return string.Compare(this.Disposition, "attachment", true) != 0;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06002DA7 RID: 11687 RVA: 0x000DBA99 File Offset: 0x000DAA99
		public bool IsMessageInside
		{
			get
			{
				return this.ContentType != string.Empty && this.ContentTypeHeader.Value == "message/rfc822";
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06002DA8 RID: 11688 RVA: 0x000DBAC9 File Offset: 0x000DAAC9
		public bool IsRelated
		{
			get
			{
				return this.ContentID != null && this.ContentID.Length != 0 && this.IsInline;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06002DA9 RID: 11689 RVA: 0x000DBAEB File Offset: 0x000DAAEB
		internal bool IsSigned
		{
			get
			{
				return this.ContentType != null && this.ContentType.ToLower() == "multipart/signed";
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06002DAA RID: 11690 RVA: 0x000DBB0C File Offset: 0x000DAB0C
		public string MailEncodingOriginal
		{
			get
			{
				return global::a.i.h.a(this.MimePartTransferEncoding);
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06002DAB RID: 11691 RVA: 0x000DBB1C File Offset: 0x000DAB1C
		public string Name
		{
			get
			{
				Header header = this.f.a("Content-Type");
				if (header != null && header.HeaderParameters != null)
				{
					global::a.i.n n = header.HeaderParameters.b("name");
					if (n != null && n.c() != null)
					{
						if (this.g != null)
						{
							return this.g.f(n.c());
						}
						return n.c();
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06002DAC RID: 11692 RVA: 0x000DBB88 File Offset: 0x000DAB88
		public MimePartType PartType
		{
			get
			{
				global::a.i.n n = global::a.i.n.a(this.ContentType, '/');
				string text = n.a().ToLower();
				if (!(text == "text"))
				{
					if (text == "message")
					{
						return MimePartType.Rfc822Message;
					}
					if (text == "image")
					{
						return MimePartType.Image;
					}
					if (text == "multipart")
					{
						return MimePartType.Multipart;
					}
				}
				else
				{
					text = n.c().ToLower();
					if (text == "plain")
					{
						return MimePartType.PlainText;
					}
					if (text == "html")
					{
						return MimePartType.Html;
					}
					if (text == "rtf" || text == "richtext")
					{
						return MimePartType.RichText;
					}
					if (text == "xml")
					{
						return MimePartType.Xml;
					}
				}
				return MimePartType.Other;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06002DAD RID: 11693 RVA: 0x000DBC44 File Offset: 0x000DAC44
		public string RawHeader
		{
			get
			{
				if (this.g == null)
				{
					return this.f.RawHeaders.c();
				}
				if (this.g.Parser != null && this.g.Parser.HeadersAsHtml)
				{
					return global::a.i.b.j(this.g.f(this.f.RawHeaders.c()));
				}
				return this.g.f(this.f.RawHeaders.c());
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06002DAE RID: 11694 RVA: 0x000DBCC5 File Offset: 0x000DACC5
		public int Size
		{
			get
			{
				if (this.e == null)
				{
					return 0;
				}
				return this.e.e();
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06002DAF RID: 11695 RVA: 0x000DBCDC File Offset: 0x000DACDC
		public MimePartCollection SubParts
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x000DBCE4 File Offset: 0x000DACE4
		internal MimePartCollection SubPartsInternal
		{
			get
			{
				if (this.d == null)
				{
					this.d = new MimePartCollection();
				}
				return this.d;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06002DB1 RID: 11697 RVA: 0x000DBD00 File Offset: 0x000DAD00
		// (set) Token: 0x06002DB2 RID: 11698 RVA: 0x000DBD38 File Offset: 0x000DAD38
		internal MailTransferEncoding MimePartTransferEncoding
		{
			get
			{
				if (this.f != null)
				{
					Header header = this.f.a("Content-Transfer-Encoding");
					if (header != null)
					{
						return global::a.i.h.b(header.Value);
					}
				}
				return MailTransferEncoding.None;
			}
			set
			{
				if (this.f != null)
				{
					Header header = this.f.a("Content-Transfer-Encoding");
					if (header != null)
					{
						header.Value = global::a.i.h.a(value);
						return;
					}
					this.f.Add("Content-Transfer-Encoding", global::a.i.h.a(value), false);
				}
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06002DB3 RID: 11699 RVA: 0x000DBD86 File Offset: 0x000DAD86
		// (set) Token: 0x06002DB4 RID: 11700 RVA: 0x000DBD93 File Offset: 0x000DAD93
		internal byte[] RawBody
		{
			get
			{
				return this.e.c();
			}
			set
			{
				this.e = new ao(value);
				this.a = true;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06002DB5 RID: 11701 RVA: 0x000DBDA8 File Offset: 0x000DADA8
		// (set) Token: 0x06002DB6 RID: 11702 RVA: 0x000DBDB0 File Offset: 0x000DADB0
		public HeaderCollection Headers
		{
			get
			{
				return this.f;
			}
			set
			{
				this.f = value;
				this.f.MimePart = this;
				this.a = true;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06002DB7 RID: 11703 RVA: 0x000DBDCC File Offset: 0x000DADCC
		// (set) Token: 0x06002DB8 RID: 11704 RVA: 0x000DBE1D File Offset: 0x000DAE1D
		internal string CharsetInternal
		{
			get
			{
				if (this.f != null)
				{
					Header header = this.f.a("Content-Type");
					if (header != null && header.HeaderParameters != null)
					{
						global::a.i.n n = header.HeaderParameters.b("charset");
						if (n != null)
						{
							return n.c();
						}
					}
				}
				return string.Empty;
			}
			set
			{
				this.a(value, true);
			}
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x000DBE28 File Offset: 0x000DAE28
		internal void a(string A_0, bool A_1)
		{
			if (A_1)
			{
				this.b = new ao(Encoding.Convert(this.a(), bb.a(A_0), this.b.c()));
			}
			if (this.f != null)
			{
				Header header = this.f.a("Content-Type");
				if (header != null)
				{
					if (header.HeaderParameters != null)
					{
						if (header.HeaderParameters.b("charset") != null)
						{
							header.HeaderParameters.b("charset").c(A_0);
							return;
						}
						header.HeaderParameters.c(new global::a.i.n("charset", A_0));
						return;
					}
					else
					{
						header.HeaderParameters = new global::a.i.j();
						header.HeaderParameters.c(new global::a.i.n("charset", A_0));
					}
				}
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06002DBA RID: 11706 RVA: 0x000DBEE8 File Offset: 0x000DAEE8
		public string ContentType
		{
			get
			{
				if (this.f["Content-Type"] == null)
				{
					return string.Empty;
				}
				if (this.g != null)
				{
					return this.g.f(this.f.a("Content-Type").Value);
				}
				return this.f.a("Content-Type").Value;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06002DBB RID: 11707 RVA: 0x000DBF4B File Offset: 0x000DAF4B
		internal Header ContentTypeHeader
		{
			get
			{
				return this.f.a("Content-Type");
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06002DBC RID: 11708 RVA: 0x000DBF5D File Offset: 0x000DAF5D
		// (set) Token: 0x06002DBD RID: 11709 RVA: 0x000DBF65 File Offset: 0x000DAF65
		internal MailMessage ParentMessage
		{
			get
			{
				return this.g;
			}
			set
			{
				this.g = value;
			}
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x000DBF70 File Offset: 0x000DAF70
		internal MimePart(MailMessage A_0)
		{
			this.g = A_0;
			this.f = new HeaderCollection(this);
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x000DBFBF File Offset: 0x000DAFBF
		public static MimePart Parse(byte[] dataToParse)
		{
			if (dataToParse == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			ao a_ = new ao(dataToParse);
			global::a.i.k.c(a_);
			return MimePart.a(a_, null);
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x000DBFE0 File Offset: 0x000DAFE0
		internal static MimePart a(ao A_0, MailMessage A_1)
		{
			MimePart mimePart = new MimePart(A_1);
			mimePart.e = A_0;
			int num = global::a.i.k.a(A_0.d(), A_0.b(), A_0.e());
			if (num > 0)
			{
				byte[] array = new byte[num - A_0.b()];
				Buffer.BlockCopy(A_0.d(), A_0.b(), array, 0, array.Length);
				ao ao = new ao(A_0, num, A_0.e() - (num - A_0.b()));
				Encoding encoding = Global.DefaultEncoding;
				if (A_1 != null && A_1.Parser != null && A_1.Parser.EncodingOverride != null)
				{
					encoding = A_1.Parser.EncodingOverride;
				}
				mimePart.f = HeaderCollection.a(encoding.GetString(array, 0, array.Length), mimePart);
				if (mimePart.f["Content-Type"] == null)
				{
					mimePart.b = new ao(A_0, num, A_0.e() - num);
				}
				if (A_1 != null && A_1.Parser.ParseHeaderOnly)
				{
					A_1.IsEntireInternal = false;
					return mimePart;
				}
				string text = string.Empty;
				Header header = mimePart.f.a("Content-Type");
				if (header != null && header.HeaderParameters != null)
				{
					global::a.i.n n = header.HeaderParameters.b("boundary");
					if (n != null)
					{
						text = string.Format("--{0}", n.c());
					}
				}
				if (text != null && text.Length != 0)
				{
					byte[] bytes = Encoding.Default.GetBytes(text);
					int num2 = 0;
					int num3 = global::a.i.k.a(ao.d(), bytes, ao.b(), ao.e(), out num2);
					int num4 = num2;
					if (num3 >= 0)
					{
						byte[] array2 = new byte[num3 - ao.b()];
						Buffer.BlockCopy(ao.d(), ao.b(), array2, 0, array2.Length);
						int a_ = 0;
						byte[] a_2 = global::a.i.h.a(mimePart.MimePartTransferEncoding, new ao(array2), out a_);
						mimePart.b = new ao(a_2, a_);
						int num5 = global::a.i.k.a(ao.d(), bytes, num3 + num2, ao.b() + ao.e() - (num3 + num2), out num2);
						if (num5 != -1)
						{
							while (num5 != -1)
							{
								int num6 = num5;
								int num7 = num2;
								int num8 = num5 - (num3 + num4);
								if (num8 < 0)
								{
									num8 = 0;
								}
								ao a_3 = new ao(ao, num3 + num4, num8);
								mimePart.SubPartsInternal.b(MimePart.a(a_3, A_1));
								byte[] array3 = new byte[num2];
								Buffer.BlockCopy(ao.d(), num5, array3, 0, num2);
								num3 = num5;
								num4 = num2;
								num5 = global::a.i.k.a(ao.d(), bytes, num5 + num2, ao.b() + ao.e() - (num5 + num2), out num2);
								if (num5 == -1 && !global::a.i.k.a(array3, bytes))
								{
									a_3 = new ao(ao, num3 + num4, ao.b() + ao.e() - (num6 + num7));
									mimePart.SubPartsInternal.b(MimePart.a(a_3, A_1));
								}
							}
						}
						else
						{
							mimePart.c = false;
							ao a_4 = new ao(ao, num3 + num4, ao.b() + ao.e() - (num3 + num4));
							mimePart.SubPartsInternal.b(MimePart.a(a_4, A_1));
						}
					}
					else
					{
						int a_5 = 0;
						byte[] a_6 = global::a.i.h.a(mimePart.MimePartTransferEncoding, ao, out a_5);
						mimePart.b = new ao(a_6, a_5);
					}
				}
				else
				{
					int a_7 = 0;
					byte[] a_8 = global::a.i.h.a(mimePart.MimePartTransferEncoding, ao, out a_7);
					mimePart.b = new ao(a_8, a_7);
				}
			}
			else
			{
				byte[] array4 = mimePart.e.c();
				mimePart.f = HeaderCollection.a(Global.DefaultEncoding.GetString(array4, 0, array4.Length), mimePart);
			}
			if (mimePart.d != null)
			{
				mimePart.d.NeedToRebuild = false;
			}
			mimePart.a = false;
			return mimePart;
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x000DC3A4 File Offset: 0x000DB3A4
		public MimePartCollection GetAllParts()
		{
			MimePartCollection mimePartCollection = new MimePartCollection();
			mimePartCollection.b(this);
			if (this.d != null)
			{
				foreach (object obj in this.d)
				{
					MimePart a_ = (MimePart)obj;
					this.a(mimePartCollection, a_);
				}
			}
			return mimePartCollection;
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x000DC418 File Offset: 0x000DB418
		private void a(MimePartCollection A_0, MimePart A_1)
		{
			A_0.b(A_1);
			if (A_1.d != null)
			{
				foreach (object obj in A_1.d)
				{
					MimePart a_ = (MimePart)obj;
					this.a(A_0, a_);
				}
			}
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x000DC484 File Offset: 0x000DB484
		internal static MimePart b(XmlReader A_0, MailMessage A_1)
		{
			MimePart mimePart = new MimePart(A_1);
			bool flag = true;
			if (A_0.Name == "MimePart")
			{
				A_0.Read();
				do
				{
					if (!A_0.IsEmptyElement)
					{
						string name = A_0.Name;
						if (!(name == "Headers"))
						{
							if (!(name == "MimePart"))
							{
								if (name == "PartValue")
								{
									mimePart.PartValueAsBytes = Encoding.GetEncoding(1252).GetBytes(XmlConvert.DecodeName(A_0.ReadElementContentAsString()));
									goto IL_B3;
								}
							}
							else if (A_0.NodeType != XmlNodeType.EndElement)
							{
								MimePart a_ = MimePart.b(A_0, A_1);
								mimePart.SubPartsInternal.b(a_);
								goto IL_B3;
							}
							flag = false;
						}
						else
						{
							mimePart.Headers = HeaderCollection.b(A_0);
						}
					}
					IL_B3:;
				}
				while (flag);
				A_0.Read();
			}
			mimePart.e = mimePart.a(new ao(new byte[0]));
			return mimePart;
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x000DC56C File Offset: 0x000DB56C
		internal void a(XmlWriter A_0)
		{
			A_0.WriteStartElement("MimePart");
			this.f.a(A_0);
			A_0.WriteStartElement("PartValue");
			byte[] array = this.b.c();
			A_0.WriteCData(XmlConvert.EncodeName(Encoding.GetEncoding(1252).GetString(array, 0, array.Length)));
			A_0.WriteEndElement();
			if (this.d != null)
			{
				foreach (object obj in this.d)
				{
					((MimePart)obj).a(A_0);
				}
			}
			A_0.WriteEndElement();
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x000DC624 File Offset: 0x000DB624
		internal ao a(ao A_0)
		{
			if (this.NeedToRebuild)
			{
				if (this.g.Builder.BuildHeaderOnly)
				{
					string s = this.f.a();
					byte[] bytes = Global.DefaultEncoding.GetBytes(s);
					int num = global::a.i.k.a(A_0.d(), A_0.b(), A_0.e());
					int num2 = bytes.Length;
					int num3 = A_0.b() + A_0.e() - num;
					if (num >= A_0.b() + num2)
					{
						Buffer.BlockCopy(bytes, 0, A_0.d(), A_0.b(), num2);
						Buffer.BlockCopy(A_0.d(), num, A_0.d(), A_0.b() + num2, num3);
					}
					else
					{
						if (A_0.b() + num2 + num3 > A_0.d().Length)
						{
							A_0.a(A_0.b() + num2 + num3 - A_0.d().Length);
						}
						Buffer.BlockCopy(A_0.d(), num, A_0.d(), A_0.b() + num2, num3);
						Buffer.BlockCopy(bytes, 0, A_0.d(), A_0.b(), num2);
					}
					A_0.b(num2 + num3);
				}
				else if (this.d != null && this.d.Count > 0)
				{
					string s2 = this.f.a();
					byte[] bytes2 = Global.DefaultEncoding.GetBytes(s2);
					A_0.a(bytes2, 0, bytes2.Length);
					int num4 = 0;
					byte[] array = global::a.i.h.a(this.b.c(), this.MimePartTransferEncoding, out num4);
					if (array != null && array.Length != 0)
					{
						byte[] array2 = new byte[num4 + 2];
						array2[0] = 13;
						array2[1] = 10;
						Array.Copy(array, 0, array2, 2, num4);
						array = array2;
					}
					A_0.a(array, 0, array.Length);
					string s3 = string.Format(CultureInfo.InvariantCulture, "\r\n--{0}\r\n", new object[]
					{
						this.Boundary
					});
					byte[] bytes3 = Global.DefaultEncoding.GetBytes(s3);
					A_0.a(bytes3, 0, bytes3.Length);
					for (int i = 0; i < this.d.Count; i++)
					{
						ao ao = this.d[i].a(new ao(A_0, A_0.b() + A_0.e(), 0));
						A_0 = new ao(A_0, A_0.b(), A_0.e() + ao.e());
						if (i == this.d.Count - 1)
						{
							s3 = string.Format(CultureInfo.InvariantCulture, "\r\n--{0}--\r\n", new object[]
							{
								this.Boundary
							});
							bytes3 = Global.DefaultEncoding.GetBytes(s3);
						}
						A_0.a(bytes3, 0, bytes3.Length);
					}
				}
				else
				{
					string s4 = this.f.a();
					byte[] bytes4 = Global.DefaultEncoding.GetBytes(s4);
					A_0.a(bytes4, 0, bytes4.Length);
					if (!MimePart.c(this))
					{
						global::a.i.h.a(A_0, this.b.c(), this.MimePartTransferEncoding);
					}
					else
					{
						switch (this.MimePartTransferEncoding)
						{
						case MailTransferEncoding.None:
						case MailTransferEncoding.Raw7bit:
						case MailTransferEncoding.Raw8bit:
						{
							byte[] array3 = this.b.c();
							A_0.a(array3, 0, array3.Length);
							break;
						}
						default:
							global::a.i.h.a(A_0, this.b.c(), this.MimePartTransferEncoding);
							break;
						}
					}
				}
			}
			else
			{
				A_0.a(this.e.d(), this.e.b(), this.e.e());
			}
			this.e = A_0;
			return this.e;
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x000DC9A0 File Offset: 0x000DB9A0
		private Encoding a()
		{
			if (this.g != null && this.g.Parser != null)
			{
				if (MimePart.c(this))
				{
					return Global.DefaultEncoding;
				}
				if (this.g.Parser.EncodingOverride != null)
				{
					return this.g.Parser.EncodingOverride;
				}
				if (this.Charset == null || this.Charset.Length == 0)
				{
					return this.g.Parser.EncodingDefault;
				}
			}
			return bb.a(this.Charset);
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x000DCA24 File Offset: 0x000DBA24
		internal static bool c(MimePart A_0)
		{
			if (A_0.SubParts != null && A_0.SubParts.Count > 0)
			{
				return false;
			}
			if (A_0.Headers != null)
			{
				Header header = A_0.Headers.a("Content-Type");
				Header header2 = A_0.Headers.a("Content-Disposition");
				if (header != null && header.HeaderParameters != null)
				{
					global::a.i.n n = header.HeaderParameters.b("name");
					if (n != null && n.c() != null && n.c().Length != 0)
					{
						return true;
					}
				}
				if (header2 != null)
				{
					if (header2.Value != null && header2.Value.ToLower() == "inline")
					{
						return false;
					}
					if (header2.HeaderParameters != null)
					{
						global::a.i.n n2 = header2.HeaderParameters.b("filename");
						if (n2 != null && n2.c() != null && n2.c().Length != 0)
						{
							return true;
						}
					}
				}
				if (header != null && header.Value != null && string.Compare(header.Value, "message/rfc822", true) == 0)
				{
					return true;
				}
				if (header2 != null && header2.Value != null && string.Compare(header2.Value, "attachment", true) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x000DCB44 File Offset: 0x000DBB44
		internal static bool b(MimePart A_0)
		{
			bool flag = false;
			return A_0.ContentTypeHeader == null || string.Compare(global::a.i.n.a(A_0.ContentTypeHeader.Value, '/').a(), "text", true) == 0 || flag;
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x000DCB84 File Offset: 0x000DBB84
		internal static bool a(MimePart A_0)
		{
			bool flag = false;
			return (A_0.ContentTypeHeader != null && string.Compare(global::a.i.n.a(A_0.ContentTypeHeader.Value, '/').a(), "multipart", true) == 0) || flag;
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x000DCBC4 File Offset: 0x000DBBC4
		internal static void a(MimePart A_0, MimePart A_1)
		{
			if (A_0.SubParts != null && A_0.SubParts.Count > 0)
			{
				if (A_0.SubParts.c(A_1))
				{
					A_0.SubParts.a(A_1);
					return;
				}
				for (int i = 0; i < A_0.SubParts.Count; i++)
				{
					MimePart.a(A_0.SubParts[i], A_1);
				}
			}
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x000DCC2C File Offset: 0x000DBC2C
		internal static MimePart a(MimePart A_0, string A_1)
		{
			MimePart mimePart = null;
			if (A_0.SubParts != null && A_0.SubParts.Count > 0)
			{
				if (A_0.SubParts[A_1] != null)
				{
					return A_0.SubParts[A_1];
				}
				for (int i = 0; i < A_0.SubParts.Count; i++)
				{
					mimePart = MimePart.a(A_0.SubParts[i], A_1);
					if (mimePart != null)
					{
						break;
					}
				}
			}
			return mimePart;
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x000DCC9C File Offset: 0x000DBC9C
		internal MimePart g()
		{
			MimePart mimePart = new MimePart(null);
			foreach (object obj in this.Headers)
			{
				Header a_ = (Header)obj;
				mimePart.Headers.b(a_);
			}
			mimePart.PartValueAsBytes = this.PartValueAsBytes;
			return mimePart;
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x000DCD10 File Offset: 0x000DBD10
		internal static Task<MimePart> a(XmlReader A_0, MailMessage A_1)
		{
			MimePart.a a;
			a.d = A_0;
			a.c = A_1;
			a.b = AsyncTaskMethodBuilder<MimePart>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<MimePart> asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<MimePart.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x000DCD60 File Offset: 0x000DBD60
		internal Task b(XmlWriter A_0)
		{
			MimePart.b b;
			b.d = this;
			b.c = A_0;
			b.b = AsyncTaskMethodBuilder.Create();
			b.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<MimePart.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x04001F8A RID: 8074
		private bool a;

		// Token: 0x04001F8B RID: 8075
		private ao b = new ao(new byte[0]);

		// Token: 0x04001F8C RID: 8076
		private bool c = true;

		// Token: 0x04001F8D RID: 8077
		private MimePartCollection d;

		// Token: 0x04001F8E RID: 8078
		private ao e = new ao(new byte[0]);

		// Token: 0x04001F8F RID: 8079
		private HeaderCollection f;

		// Token: 0x04001F90 RID: 8080
		private MailMessage g;
	}
}
