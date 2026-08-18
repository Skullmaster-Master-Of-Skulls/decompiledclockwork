using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Spire.CompoundFile.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Core;
using Spire.Doc.Core.DataStreamParser.Escher;
using Spire.Doc.Documents;
using Spire.Doc.Documents.Rendering;
using Spire.Doc.Documents.XML;
using Spire.Doc.Fields;
using Spire.Doc.Fields.Shape;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;
using Spire.Doc.Reporting;
using Spire.Layouting;
using Spire.License;
using Spire.Pdf;

namespace Spire.Doc
{
	// Token: 0x020000F1 RID: 241
	[LicenseProvider(typeof(Spire.License.LicenseProvider))]
	public class Document : DocumentContainer, IDocument, IXmlSerializable, spr\u17C8
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000435 RID: 1077 RVA: 0x0002E140 File Offset: 0x0002D140
		// (remove) Token: 0x06000436 RID: 1078 RVA: 0x0002E1D8 File Offset: 0x0002D1D8
		public event PageLayoutHandler PageLayout
		{
			add
			{
				for (;;)
				{
					PageLayoutHandler pageLayoutHandler = this.ᝯ;
					if (true)
					{
					}
					int num = 1;
					for (;;)
					{
						PageLayoutHandler pageLayoutHandler2;
						switch (num)
						{
						case 0:
							if (pageLayoutHandler == pageLayoutHandler2)
							{
								goto IL_7C;
							}
							goto IL_53;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_7C;
							default:
								if (false)
								{
								}
								goto IL_53;
							}
							break;
						case 2:
							return;
						}
						break;
						IL_53:
						pageLayoutHandler2 = pageLayoutHandler;
						PageLayoutHandler value2 = (PageLayoutHandler)Delegate.Combine(pageLayoutHandler2, value);
						pageLayoutHandler = Interlocked.CompareExchange<PageLayoutHandler>(ref this.ᝯ, value2, pageLayoutHandler2);
						num = 0;
						continue;
						IL_7C:
						num = 2;
					}
				}
			}
			remove
			{
				for (;;)
				{
					PageLayoutHandler pageLayoutHandler = this.ᝯ;
					if (true)
					{
					}
					int num = 1;
					for (;;)
					{
						PageLayoutHandler pageLayoutHandler2;
						switch (num)
						{
						case 0:
							if (pageLayoutHandler == pageLayoutHandler2)
							{
								goto IL_7C;
							}
							goto IL_53;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_7C;
							default:
								if (false)
								{
								}
								goto IL_53;
							}
							break;
						case 2:
							return;
						}
						break;
						IL_53:
						pageLayoutHandler2 = pageLayoutHandler;
						PageLayoutHandler value2 = (PageLayoutHandler)Delegate.Remove(pageLayoutHandler2, value);
						pageLayoutHandler = Interlocked.CompareExchange<PageLayoutHandler>(ref this.ᝯ, value2, pageLayoutHandler2);
						num = 0;
						continue;
						IL_7C:
						num = 2;
					}
				}
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0002E270 File Offset: 0x0002D270
		internal new void ᜀ(PageLayoutEventArgs A_0)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					this.ᝯ(this, A_0);
					goto IL_57;
				case 2:
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_57:
					if (true)
					{
					}
					num = 2;
					break;
				default:
					if (false)
					{
					}
					if (this.ᝯ == null)
					{
						return;
					}
					num = 1;
					break;
				}
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000438 RID: 1080 RVA: 0x0002E2F0 File Offset: 0x0002D2F0
		// (remove) Token: 0x06000439 RID: 1081 RVA: 0x0002E388 File Offset: 0x0002D388
		internal event spr\u24DA PageImagePainted
		{
			add
			{
				for (;;)
				{
					spr\u24DA spr_u24DA = this.ᝰ;
					int num = 0;
					for (;;)
					{
						spr\u24DA spr_u24DA2;
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_74;
							default:
								if (false)
								{
								}
								goto IL_4B;
							}
							break;
						case 1:
							if (spr_u24DA == spr_u24DA2)
							{
								goto IL_74;
							}
							goto IL_4B;
						case 2:
							return;
						}
						break;
						IL_4B:
						spr_u24DA2 = spr_u24DA;
						spr\u24DA value2 = (spr\u24DA)Delegate.Combine(spr_u24DA2, value);
						spr_u24DA = Interlocked.CompareExchange<spr\u24DA>(ref this.ᝰ, value2, spr_u24DA2);
						num = 1;
						continue;
						IL_74:
						if (true)
						{
						}
						num = 2;
					}
				}
			}
			remove
			{
				for (;;)
				{
					spr\u24DA spr_u24DA = this.ᝰ;
					int num = 2;
					for (;;)
					{
						spr\u24DA spr_u24DA2;
						switch (num)
						{
						case 0:
							return;
						case 1:
							if (spr_u24DA == spr_u24DA2)
							{
								goto IL_7C;
							}
							goto IL_4B;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_7C;
							default:
								if (false)
								{
								}
								goto IL_4B;
							}
							break;
						}
						break;
						IL_4B:
						spr_u24DA2 = spr_u24DA;
						spr\u24DA value2 = (spr\u24DA)Delegate.Remove(spr_u24DA2, value);
						spr_u24DA = Interlocked.CompareExchange<spr\u24DA>(ref this.ᝰ, value2, spr_u24DA2);
						if (true)
						{
						}
						num = 1;
						continue;
						IL_7C:
						num = 0;
					}
				}
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0002E420 File Offset: 0x0002D420
		internal new void ᜀ(spr\u249F A_0)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᝰ(this, A_0);
					goto IL_5F;
				case 1:
					return;
				}
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_5F:
					num = 1;
					break;
				default:
					if (false)
					{
					}
					if (this.ᝰ == null)
					{
						return;
					}
					num = 0;
					break;
				}
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0002E4A0 File Offset: 0x0002D4A0
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x0002E4E4 File Offset: 0x0002D4E4
		internal InternalLicense InternalLicense
		{
			[CompilerGenerated]
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.\u1771;
			}
			[CompilerGenerated]
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.\u1771 = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0002E528 File Offset: 0x0002D528
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x0002E56C File Offset: 0x0002D56C
		internal ushort WordVersion
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.\u1755;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.\u1755 = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0002E5B0 File Offset: 0x0002D5B0
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x0002E634 File Offset: 0x0002D634
		internal List<Font> UsedFontNames
		{
			get
			{
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.ᝍ = new List<Font>();
						goto IL_5D;
					case 2:
						goto IL_65;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_5D:
						num = 2;
						break;
					default:
						if (false)
						{
						}
						if (this.ᝍ != null)
						{
							goto IL_71;
						}
						num = 1;
						break;
					}
				}
				IL_65:
				IL_71:
				return this.ᝍ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᝍ = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0002E678 File Offset: 0x0002D678
		internal bool HasTOC
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᝌ != null;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0002E6C0 File Offset: 0x0002D6C0
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x0002E710 File Offset: 0x0002D710
		internal TableOfContent TOC
		{
			get
			{
				while (this.HasTOC)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						return this.ᝌ;
					}
				}
				return null;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᝌ = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0002E754 File Offset: 0x0002D754
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x0002E798 File Offset: 0x0002D798
		internal string HtmlBaseUrl
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᝪ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᝪ = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0002E7DC File Offset: 0x0002D7DC
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x0002E81C File Offset: 0x0002D81C
		internal static bool IsCloneParagraphCheckFormat
		{
			[CompilerGenerated]
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return Document.\u1772;
			}
			[CompilerGenerated]
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				Document.\u1772 = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0002E860 File Offset: 0x0002D860
		internal LicenseType LicenseType
		{
			get
			{
				int num = 1;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_75;
					case 2:
						if (this.\u176D.Type == LicenseType.None)
						{
							num = 0;
							continue;
						}
						goto IL_77;
					case 3:
						num = 2;
						continue;
					}
					IL_28:
					if (this.\u176D == null)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					goto IL_28;
				}
				return LicenseType.Demo;
				IL_75:
				return LicenseType.Demo;
				IL_77:
				return this.\u176D.Type;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0002E8FC File Offset: 0x0002D8FC
		internal List<Stream> FootnoteNodes2010
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_65;
					case 2:
						this.\u1738 = new List<Stream>();
						goto IL_5D;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_5D:
						num = 1;
						break;
					default:
						if (false)
						{
						}
						if (this.\u1738 != null)
						{
							goto IL_71;
						}
						if (true)
						{
						}
						num = 2;
						break;
					}
				}
				IL_65:
				IL_71:
				return this.\u1738;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0002E980 File Offset: 0x0002D980
		internal List<Stream> EndnoteNodes2010
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6F;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 2:
						this.\u173A = new List<Stream>();
						if (true)
						{
						}
						num = 0;
						continue;
					}
					if (this.\u173A != null)
					{
						break;
					}
					num = 2;
				}
				IL_6F:
				return this.\u173A;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0002EA04 File Offset: 0x0002DA04
		public List<XmlNode> Footnotes
		{
			get
			{
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6F;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 2:
						this.\u1739 = new List<XmlNode>();
						num = 0;
						continue;
					}
					if (this.\u1739 != null)
					{
						break;
					}
					num = 2;
				}
				IL_6F:
				return this.\u1739;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0002EA88 File Offset: 0x0002DA88
		public List<XmlNode> Endnotes
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						this.\u173B = new List<XmlNode>();
						num = 2;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6F;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 2:
						goto IL_6F;
					}
					if (this.\u173B != null)
					{
						break;
					}
					num = 0;
				}
				IL_6F:
				return this.\u173B;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0002EB0C File Offset: 0x0002DB0C
		public override DocumentObjectType DocumentObjectType
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return DocumentObjectType.Document;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0002EB48 File Offset: 0x0002DB48
		public BuiltinDocumentProperties BuiltinDocumentProperties
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜊ;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0002EB8C File Offset: 0x0002DB8C
		public CustomDocumentProperties CustomDocumentProperties
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜋ;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0002EBD0 File Offset: 0x0002DBD0
		public SectionCollection Sections
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.m_sections;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0002EC14 File Offset: 0x0002DC14
		public StyleCollection Styles
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.m_styles;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0002EC58 File Offset: 0x0002DC58
		public ListStyleCollection ListStyles
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜄ();
						num = 2;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6F;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							break;
						}
						break;
					case 2:
						goto IL_6F;
					}
					if (this.m_listStyles.Count != 0)
					{
						break;
					}
					num = 0;
				}
				IL_6F:
				return this.m_listStyles;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0002ECDC File Offset: 0x0002DCDC
		public BookmarkCollection Bookmarks
		{
			get
			{
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_70;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 1:
						this.\u170D = new BookmarkCollection(this);
						num = 2;
						continue;
					case 2:
						goto IL_70;
					}
					if (this.\u170D != null)
					{
						break;
					}
					num = 1;
				}
				IL_70:
				return this.\u170D;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0002ED64 File Offset: 0x0002DD64
		internal spr\u2062 Fields
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_70;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_70;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 2:
						if (true)
						{
						}
						this.ᜎ = new spr\u2062(this);
						num = 0;
						continue;
					}
					if (this.ᜎ != null)
					{
						break;
					}
					num = 2;
				}
				IL_70:
				return this.ᜎ;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0002EDEC File Offset: 0x0002DDEC
		public CommentsCollection Comments
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_70;
					case 1:
						this.ᜐ = new CommentsCollection(this);
						num = 0;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_70;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							break;
						}
						break;
					}
					if (this.ᜐ != null)
					{
						break;
					}
					num = 1;
				}
				IL_70:
				return this.ᜐ;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0002EE74 File Offset: 0x0002DE74
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x0002EEB8 File Offset: 0x0002DEB8
		public TextBoxCollection TextBoxes
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜏ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜏ = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0002EEFC File Offset: 0x0002DEFC
		public Section LastSection
		{
			get
			{
				int count = this.Sections.Count;
				if (count <= 0)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						return null;
					}
				}
				if (true)
				{
				}
				return this.Sections[count - 1];
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0002EF5C File Offset: 0x0002DF5C
		public Paragraph LastParagraph
		{
			get
			{
				Section lastSection;
				int count;
				for (;;)
				{
					lastSection = this.LastSection;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (count <= 0)
							{
								goto IL_B3;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num = 3;
								continue;
							}
							break;
						case 1:
							lastSection.Body.Paragraphs.ᜁ();
							count = lastSection.Body.Paragraphs.Count;
							num = 0;
							continue;
						case 2:
							if (lastSection != null)
							{
								num = 1;
								continue;
							}
							goto IL_B3;
						case 3:
							goto IL_B1;
						}
						break;
					}
				}
				IL_B1:
				if (true)
				{
				}
				return lastSection.Body.Paragraphs[count - 1];
				IL_B3:
				return null;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0002F020 File Offset: 0x0002E020
		public EndnoteOptions EndnoteOptions
		{
			get
			{
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.\u1716 = new EndnoteOptions(this.\u1715);
						num = 2;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_75;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 2:
						goto IL_75;
					}
					if (this.\u1716 != null)
					{
						break;
					}
					num = 0;
				}
				IL_75:
				return this.\u1716;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0002F0AC File Offset: 0x0002E0AC
		public FootEndnoteOptions FooternoteOptions
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.\u1717 = new FootEndnoteOptions(this.\u1715);
						num = 2;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_75;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							break;
						}
						break;
					case 2:
						goto IL_75;
					}
					if (this.\u1717 != null)
					{
						break;
					}
					num = 0;
				}
				IL_75:
				return this.\u1717;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0002F138 File Offset: 0x0002E138
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x0002F17C File Offset: 0x0002E17C
		public WatermarkBase Watermark
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.\u1713;
			}
			set
			{
				for (;;)
				{
					if (true)
					{
					}
					this.ᜁ();
					this.\u1713 = value;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_44;
							default:
								if (false)
								{
								}
								(this.\u1713 as PictureWatermark).WordPicture.ᜀ(this);
								num = 4;
								continue;
							}
							break;
						case 1:
							if (this.\u1713 != null)
							{
								goto IL_44;
							}
							return;
						case 2:
							this.\u1713.ᜀ(this);
							num = 3;
							continue;
						case 3:
							if (this.\u1713 is PictureWatermark)
							{
								num = 0;
								continue;
							}
							return;
						case 4:
							return;
						}
						break;
						IL_44:
						num = 2;
					}
				}
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0002F24C File Offset: 0x0002E24C
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x0002F290 File Offset: 0x0002E290
		internal spr\u1937 BackgroundShape
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᝠ;
			}
			set
			{
				int a_ = 17;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_8B;
					case 1:
						if (value.Document != this)
						{
							num = 3;
							continue;
						}
						num = 5;
						continue;
					case 3:
						goto IL_D4;
					case 4:
						if (value.\u1774() == Spire.Doc.Fields.Shape.ShapeType.Rectangle)
						{
							goto IL_112;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_53;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 5:
						if (true)
						{
						}
						if (value.\u1771() != null)
						{
							num = 6;
							continue;
						}
						goto IL_53;
					case 6:
						goto IL_B3;
					case 7:
						num = 1;
						continue;
					}
					if (value != null)
					{
						num = 7;
						continue;
					}
					goto IL_112;
					IL_53:
					num = 4;
				}
				IL_8B:
				throw new ArgumentException(ClipboardData.b("㡶᝸᝺Ѽ彾ꎂﾊﾒ랖ﲜ쒠莢욤욦잨讪쾬쪮醰삲킴쎶馸\udaba캼龾ꃀꇄ꣆꫈뻊ꃌ꫎뿐꟒뗖룘룚뛜룞鏠賢郤触跨엪", a_));
				IL_B3:
				throw new ArgumentException(ClipboardData.b("⍶ᅸṺ嵼౾ꦈﺌ꾎뎒ﾖ列뾞캠얢薤욦잨쒪\ud9ac잮풰솲閴\ud9b6횸\udfba\ud8bc醾", a_));
				IL_D4:
				throw new ArgumentException(ClipboardData.b("⍶ᅸṺ嵼౾ꦈﲊﲎ놐ﮞ膠얢힤좦쒨讪첬辮햰\udab2펴톶\udcb8즺\ud8bc톾뗀ꇄ꣆꫈뻊ꃌ꫎뿐꟒ﯔ", a_));
				IL_112:
				this.ᝠ = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0002F3B8 File Offset: 0x0002E3B8
		internal Hashtable CanvasCache
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᝡ = new Hashtable();
						num = 2;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6F;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 2:
						goto IL_6F;
					}
					if (this.ᝡ != null)
					{
						break;
					}
					if (true)
					{
					}
					num = 0;
				}
				IL_6F:
				return this.ᝡ;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0002F43C File Offset: 0x0002E43C
		internal string FileName
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.\u1759;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0002F480 File Offset: 0x0002E480
		public Background Background
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.\u1714;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0002F4C4 File Offset: 0x0002E4C4
		public MailMerge MailMerge
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜑ;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0002F508 File Offset: 0x0002E508
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x0002F550 File Offset: 0x0002E550
		public ProtectionType ProtectionType
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.\u1715.ᜎ();
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.\u1715.ᜀ(value);
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0002F598 File Offset: 0x0002E598
		public ViewSetup ViewSetup
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.\u1712;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0002F5DC File Offset: 0x0002E5DC
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x0002F620 File Offset: 0x0002E620
		public bool QuiteMode
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return !this.ᜥ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜥ = !value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0002F668 File Offset: 0x0002E668
		public DocumentObjectCollection ChildObjects
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.m_sections;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0002F6AC File Offset: 0x0002E6AC
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x0002F6F0 File Offset: 0x0002E6F0
		public XHTMLValidationType XHTMLValidateOption
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜨ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜨ = value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0002F734 File Offset: 0x0002E734
		public VariableCollection Variables
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 1:
						this.ᜰ = new VariableCollection();
						num = 0;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6F;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					}
					if (true)
					{
					}
					if (this.ᜰ != null)
					{
						break;
					}
					num = 1;
				}
				IL_6F:
				return this.ᜰ;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0002F7B8 File Offset: 0x0002E7B8
		public DocumentProperties Properties
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜱ = new DocumentProperties(this.\u1715);
						num = 2;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_75;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							break;
						}
						break;
					case 2:
						goto IL_75;
					}
					if (this.ᜱ != null)
					{
						break;
					}
					num = 0;
				}
				IL_75:
				return this.ᜱ;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0002F844 File Offset: 0x0002E844
		public bool HasChanges
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜂ();
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0002F888 File Offset: 0x0002E888
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x0002F8D0 File Offset: 0x0002E8D0
		public bool TrackChanges
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.\u1715.ᜉ();
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.\u1715.ᜅ(value);
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0002F918 File Offset: 0x0002E918
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x0002F95C File Offset: 0x0002E95C
		public bool ReplaceFirst
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.\u1732;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.\u1732 = value;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0002F9A0 File Offset: 0x0002E9A0
		public HtmlExportOptions HtmlExportOptions
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 1:
						this.\u1735 = new HtmlExportOptions();
						if (true)
						{
						}
						num = 0;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6F;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					}
					if (this.\u1735 != null)
					{
						break;
					}
					num = 1;
				}
				IL_6F:
				return this.\u1735;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0002FA24 File Offset: 0x0002EA24
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x0002FA68 File Offset: 0x0002EA68
		public bool IsUpdateFields
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᝁ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᝁ = value;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0002FAAC File Offset: 0x0002EAAC
		internal List<Stream> DocxProps2010
		{
			get
			{
				int num = 1;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6F;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 2:
						this.\u1736 = new List<Stream>();
						num = 0;
						continue;
					}
					if (this.\u1736 != null)
					{
						break;
					}
					num = 2;
				}
				IL_6F:
				return this.\u1736;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0002FB30 File Offset: 0x0002EB30
		internal List<XmlNode> DocxProps
		{
			get
			{
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 1:
						this.\u1737 = new List<XmlNode>();
						num = 0;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6F;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					}
					if (this.\u1737 != null)
					{
						break;
					}
					num = 1;
				}
				IL_6F:
				return this.\u1737;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0002FBB4 File Offset: 0x0002EBB4
		internal bool HasDocxProps
		{
			get
			{
				if (this.\u1737 == null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0002FBFC File Offset: 0x0002EBFC
		internal bool IsClosing
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᝅ;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0002FC40 File Offset: 0x0002EC40
		internal Dictionary<string, string> StyleNameIds
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6F;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							break;
						}
						break;
					case 1:
						goto IL_6F;
					case 2:
						this.ᝄ = new Dictionary<string, string>();
						num = 1;
						continue;
					}
					if (this.ᝄ != null)
					{
						break;
					}
					num = 2;
				}
				IL_6F:
				return this.ᝄ;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0002FCC4 File Offset: 0x0002ECC4
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x0002FD08 File Offset: 0x0002ED08
		public FileFormat DetectedFormatType
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜆ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜆ = value;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0002FD4C File Offset: 0x0002ED4C
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0002FD90 File Offset: 0x0002ED90
		public int JPEGQuality
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᝮ;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᝮ = value;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x0002FDD4 File Offset: 0x0002EDD4
		public PrintDialog PrintDialog
		{
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᝣ = value;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0002FE18 File Offset: 0x0002EE18
		[Browsable(false)]
		public PrintDocument PrintDocument
		{
			get
			{
				for (;;)
				{
					if (true)
					{
					}
					this.ᝢ.Clear();
					this.PageImagePainted += this.ᜀ;
					this.ᜀ(ImageType.Metafile, false);
					this.PageImagePainted -= this.ᜀ;
					this.ᝥ = this.ᝣ.PrinterSettings.FromPage;
					this.ᝦ = this.ᝣ.PrinterSettings.ToPage;
					int num = 6;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_B9;
						case 1:
							goto IL_B9;
						case 2:
							this.ᝧ = 0;
							num = 0;
							continue;
						case 3:
							if (this.ᝢ.Count > 0)
							{
								num = 5;
								continue;
							}
							goto IL_173;
						case 4:
							goto IL_171;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_B9;
							default:
								if (false)
								{
								}
								this.ᝤ = new PrintDocument();
								this.ᝤ.PrinterSettings = this.ᝣ.PrinterSettings;
								this.ᝤ.PrintPage += this.OnPrintPage;
								num = 4;
								continue;
							}
							break;
						case 6:
							if (this.ᝥ == 0)
							{
								num = 2;
								continue;
							}
							this.ᝧ = this.ᝥ - 1;
							num = 1;
							continue;
						}
						break;
						IL_B9:
						num = 3;
					}
				}
				IL_171:
				IL_173:
				return this.ᝤ;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0002FFA0 File Offset: 0x0002EFA0
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x00030024 File Offset: 0x0002F024
		internal Dictionary<string, string> FontSubstitutionTable
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4B;
					case 2:
						goto IL_36;
					}
					if (true)
					{
					}
					if (this.ᝊ == null)
					{
						num = 2;
						continue;
					}
					goto IL_4B;
					IL_36:
					this.ᝊ = new Dictionary<string, string>();
					num = 0;
					continue;
					IL_4B:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_36;
					default:
						goto IL_6B;
					}
				}
				IL_6B:
				if (false)
				{
				}
				return this.ᝊ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᝊ = value;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00030068 File Offset: 0x0002F068
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x000300EC File Offset: 0x0002F0EC
		internal Dictionary<string, string> ColorScheme
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_36;
					case 2:
						goto IL_4B;
					}
					if (true)
					{
					}
					if (this.ᝋ == null)
					{
						num = 0;
						continue;
					}
					goto IL_4B;
					IL_36:
					this.ᝋ = new Dictionary<string, string>();
					num = 2;
					continue;
					IL_4B:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_36;
					default:
						goto IL_6B;
					}
				}
				IL_6B:
				if (false)
				{
				}
				return this.ᝋ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᝋ = value;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00030130 File Offset: 0x0002F130
		public bool IsContainMacro
		{
			get
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.VbaData.Count <= 0)
						{
							num = 1;
							continue;
						}
						return true;
					case 1:
						goto IL_92;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return false;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					if (this.VbaProject == null)
					{
						return false;
					}
					num = 2;
				}
				return true;
				IL_92:
				if (true)
				{
				}
				return this.DocEvents.Count > 0;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x000301D4 File Offset: 0x0002F1D4
		internal spr\u18F7 Images
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4C;
					case 1:
						if (true)
						{
						}
						break;
					case 2:
						goto IL_36;
					}
					if (this.\u1758 == null)
					{
						num = 2;
						continue;
					}
					goto IL_4C;
					IL_36:
					this.\u1758 = new spr\u18F7(this);
					num = 0;
					continue;
					IL_4C:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_36;
					default:
						goto IL_6C;
					}
				}
				IL_6C:
				if (false)
				{
				}
				return this.\u1758;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0003025C File Offset: 0x0002F25C
		internal Stack<Field> ClonedFields
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4B;
					case 1:
						goto IL_36;
					}
					if (true)
					{
					}
					if (this.\u1756 == null)
					{
						num = 1;
						continue;
					}
					goto IL_4B;
					IL_36:
					this.\u1756 = new Stack<Field>();
					num = 0;
					continue;
					IL_4B:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_36;
					default:
						goto IL_6B;
					}
				}
				IL_6B:
				if (false)
				{
				}
				return this.\u1756;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x000302E0 File Offset: 0x0002F2E0
		internal spr\u1B79 ListOverrides
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜌ;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00030324 File Offset: 0x0002F324
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x00030368 File Offset: 0x0002F368
		internal sprᥚ GrammarSpellingData
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.\u1718;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.\u1718 = value;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x000303AC File Offset: 0x0002F3AC
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x000303F0 File Offset: 0x0002F3F0
		internal spr\u202E DOP
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.\u1715;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.\u1715 = value;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00030434 File Offset: 0x0002F434
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x00030478 File Offset: 0x0002F478
		internal spr\u24E3 Escher
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.\u1719;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.\u1719 = value;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x000304BC File Offset: 0x0002F4BC
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x00030500 File Offset: 0x0002F500
		internal Section CurClonedSection
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜦ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜦ = value;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00030544 File Offset: 0x0002F544
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x00030588 File Offset: 0x0002F588
		internal byte[] ObjectPool
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.\u171B;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.\u171B = value;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x000305CC File Offset: 0x0002F5CC
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x00030610 File Offset: 0x0002F610
		internal FileFormat SaveFormatType
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.\u1754;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.\u1754 = value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x00030654 File Offset: 0x0002F654
		internal bool IsMacroEnabled
		{
			get
			{
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.SaveFormatType != FileFormat.Dotm)
						{
							num = 2;
							continue;
						}
						return true;
					case 1:
						if (this.SaveFormatType != FileFormat.Docm2010)
						{
							num = 3;
							continue;
						}
						return true;
					case 2:
						goto IL_88;
					case 3:
						num = 0;
						continue;
					case 4:
						if (true)
						{
						}
						num = 1;
						continue;
					}
					if (this.SaveFormatType == FileFormat.Docm)
					{
						return true;
					}
					num = 4;
				}
				IL_88:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return true;
				default:
					if (false)
					{
					}
					return this.SaveFormatType == FileFormat.Dotm2010;
				}
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00030718 File Offset: 0x0002F718
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x0003075C File Offset: 0x0002F75C
		internal Stream VbaProject
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᝏ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᝏ = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x000307A0 File Offset: 0x0002F7A0
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x000307E4 File Offset: 0x0002F7E4
		internal sprᭇ CustomUIPartContainer
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᝐ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᝐ = value;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00030828 File Offset: 0x0002F828
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x0003086C File Offset: 0x0002F86C
		internal sprᭇ CustomXMLContainer
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᝑ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᝑ = value;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x000308B0 File Offset: 0x0002F8B0
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x00030934 File Offset: 0x0002F934
		internal List<sprᴚ> VbaData
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4B;
					case 1:
						goto IL_36;
					case 2:
						if (true)
						{
						}
						break;
					}
					if (this.\u1752 == null)
					{
						num = 1;
						continue;
					}
					goto IL_4B;
					IL_36:
					this.\u1752 = new List<sprᴚ>();
					num = 0;
					continue;
					IL_4B:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_36;
					default:
						goto IL_6B;
					}
				}
				IL_6B:
				if (false)
				{
				}
				return this.\u1752;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.\u1752 = value;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00030978 File Offset: 0x0002F978
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x000309FC File Offset: 0x0002F9FC
		internal List<string> DocEvents
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						break;
					case 1:
						goto IL_36;
					case 2:
						goto IL_4B;
					}
					if (this.\u1753 == null)
					{
						num = 1;
						continue;
					}
					goto IL_4B;
					IL_36:
					this.\u1753 = new List<string>();
					num = 2;
					continue;
					IL_4B:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_36;
					default:
						goto IL_6B;
					}
				}
				IL_6B:
				if (false)
				{
				}
				return this.\u1753;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.\u1753 = value;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00030A40 File Offset: 0x0002FA40
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x00030A84 File Offset: 0x0002FA84
		internal byte[] MacrosData
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.\u171C;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.\u171C = value;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00030AC8 File Offset: 0x0002FAC8
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x00030B0C File Offset: 0x0002FB0C
		internal DigitalSignatures DigitalSignatures
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᝬ;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᝬ = value;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00030B50 File Offset: 0x0002FB50
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x00030B94 File Offset: 0x0002FB94
		internal byte[] MacroCommands
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.\u171F;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.\u171F = value;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00030BD8 File Offset: 0x0002FBD8
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x00030C1C File Offset: 0x0002FC1C
		internal string StandardAsciiFont
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜡ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜡ = value;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00030C60 File Offset: 0x0002FC60
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x00030CA4 File Offset: 0x0002FCA4
		internal string StandardFarEastFont
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜢ;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜢ = value;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00030CE8 File Offset: 0x0002FCE8
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x00030D2C File Offset: 0x0002FD2C
		internal string StandardNonFarEastFont
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜣ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜣ = value;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00030D70 File Offset: 0x0002FD70
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x00030DB4 File Offset: 0x0002FDB4
		internal string StandardBidiFont
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜤ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜤ = value;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x00030DF8 File Offset: 0x0002FDF8
		internal string Password
		{
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.\u171A = value;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00030E3C File Offset: 0x0002FE3C
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x00030E80 File Offset: 0x0002FE80
		internal MemoryStream LatentStyles2010
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜪ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜪ = value;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00030EC4 File Offset: 0x0002FEC4
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x00030F08 File Offset: 0x0002FF08
		internal XmlNode LatentStyles
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜩ;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜩ = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00030F4C File Offset: 0x0002FF4C
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x00030F90 File Offset: 0x0002FF90
		internal spr᪆ DocxPackage
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜭ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜭ = value;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00030FD4 File Offset: 0x0002FFD4
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x00031018 File Offset: 0x00030018
		internal bool ImportStyles
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜯ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜯ = value;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0003105C File Offset: 0x0003005C
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x000310A0 File Offset: 0x000300A0
		internal ImportOptions ImportOption
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜮ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜮ = value;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x000310E4 File Offset: 0x000300E4
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x00031128 File Offset: 0x00030128
		internal CharacterFormat DefCharFormat
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜫ;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜫ = value;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0003116C File Offset: 0x0003016C
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x000311B0 File Offset: 0x000301B0
		internal ParagraphFormat DefParaFormat
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜬ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜬ = value;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x000311F4 File Offset: 0x000301F4
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x00031238 File Offset: 0x00030238
		internal byte[] AssociatedStrings
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.\u173D;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.\u173D = value;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0003127C File Offset: 0x0003027C
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x000312C0 File Offset: 0x000302C0
		internal bool IsEncrypted
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᝀ;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᝀ = value;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00031304 File Offset: 0x00030304
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x00031348 File Offset: 0x00030348
		internal bool HasPicture
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᝉ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᝉ = value;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0003138C File Offset: 0x0003038C
		internal bool WriteWarning
		{
			get
			{
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_47;
					case 1:
						return false;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_47;
						default:
							if (false)
							{
							}
							if (this.\u173E)
							{
								num = 0;
								continue;
							}
							return false;
						}
						break;
					case 3:
						if (this.\u173F)
						{
							num = 1;
							continue;
						}
						return true;
					case 4:
						goto IL_3D;
					}
					if (spr\u2347.ᜀ(this.InternalLicense))
					{
						num = 4;
						continue;
					}
					num = 2;
					continue;
					IL_47:
					num = 3;
				}
				IL_3D:
				if (true)
				{
				}
				this.\u176D = this.InternalLicense.License;
				this.\u173E = false;
				return false;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0003145C File Offset: 0x0003045C
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x000314A0 File Offset: 0x000304A0
		internal bool WriteProtected
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᝂ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᝂ = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x000314E4 File Offset: 0x000304E4
		internal List<string> ObjPoolContainers
		{
			get
			{
				int num = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 1:
						this.ᝃ = new List<string>();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2C;
						}
						if (false)
						{
						}
						num = 0;
						continue;
					}
					goto IL_24;
					IL_2C:
					num = 1;
					continue;
					IL_24:
					if (this.ᝃ == null)
					{
						goto IL_2C;
					}
					break;
				}
				IL_6F:
				return this.ᝃ;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00031568 File Offset: 0x00030568
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x000315AC File Offset: 0x000305AC
		internal DocumentOperationType OperationType
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᝫ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᝫ = value;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x000315F0 File Offset: 0x000305F0
		private HybridDictionary ListNames
		{
			get
			{
				int num = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 1:
						this.\u175B = new HybridDictionary();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2C;
						}
						if (false)
						{
						}
						num = 2;
						continue;
					case 2:
						goto IL_6F;
					}
					goto IL_24;
					IL_2C:
					num = 1;
					continue;
					IL_24:
					if (this.\u175B == null)
					{
						goto IL_2C;
					}
					break;
				}
				IL_6F:
				return this.\u175B;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00031674 File Offset: 0x00030674
		private Dictionary<string, Dictionary<int, int>> Lists
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.\u175A = new Dictionary<string, Dictionary<int, int>>();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_24;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_67;
					}
					goto IL_1C;
					IL_24:
					num = 0;
					continue;
					IL_1C:
					if (this.\u175A == null)
					{
						goto IL_24;
					}
					goto IL_71;
				}
				IL_67:
				if (true)
				{
				}
				IL_71:
				return this.\u175A;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x000316F8 File Offset: 0x000306F8
		private Dictionary<string, int> PreviousListLevel
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 1:
						this.\u175C = new Dictionary<string, int>();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_24;
						}
						if (false)
						{
						}
						num = 0;
						continue;
					}
					goto IL_1C;
					IL_24:
					if (true)
					{
					}
					num = 1;
					continue;
					IL_1C:
					if (this.\u175C == null)
					{
						goto IL_24;
					}
					break;
				}
				IL_6F:
				return this.\u175C;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0003177C File Offset: 0x0003077C
		private Dictionary<string, int> LfoListLevel
		{
			get
			{
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.\u175D = new Dictionary<string, int>();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2C;
						}
						if (false)
						{
						}
						num = 1;
						continue;
					case 1:
						goto IL_6F;
					}
					goto IL_24;
					IL_2C:
					num = 0;
					continue;
					IL_24:
					if (this.\u175D == null)
					{
						goto IL_2C;
					}
					break;
				}
				IL_6F:
				return this.\u175D;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00031800 File Offset: 0x00030800
		internal List<DocumentObject> DocObject
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.\u175F = new List<DocumentObject>();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_24;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 1:
						goto IL_6F;
					}
					goto IL_1C;
					IL_24:
					num = 0;
					continue;
					IL_1C:
					if (this.\u175F == null)
					{
						goto IL_24;
					}
					break;
				}
				IL_6F:
				return this.\u175F;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00031884 File Offset: 0x00030884
		internal bool UseHangingIndentAsListTab
		{
			get
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_84:
					if (this.DetectedFormatType != FileFormat.Docx2010)
					{
						return true;
					}
					num = 4;
					break;
				default:
					if (false)
					{
					}
					num = 3;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						num = 6;
						continue;
					case 1:
						if (this.DetectedFormatType != FileFormat.Docx)
						{
							num = 5;
							continue;
						}
						goto IL_6B;
					case 2:
						goto IL_84;
					case 4:
						goto IL_95;
					case 5:
						num = 2;
						continue;
					case 6:
						if (this.DetectedFormatType != FileFormat.Docx)
						{
							num = 7;
							continue;
						}
						goto IL_6B;
					case 7:
						num = 1;
						continue;
					}
					if (this.DetectedFormatType == FileFormat.Doc)
					{
						return false;
					}
					num = 0;
				}
				IL_6B:
				return !this.CompatibilitySettings.ᜀ(CompatibilityOptions.DontUseIndentAsListTabStop);
				IL_95:
				goto IL_6B;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00031974 File Offset: 0x00030974
		internal spr\u2100 CompatibilitySettings
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 1:
						if (true)
						{
						}
						break;
					case 2:
						this.ᝎ = new spr\u2100();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2C;
						}
						if (false)
						{
						}
						num = 0;
						continue;
					}
					goto IL_24;
					IL_2C:
					num = 2;
					continue;
					IL_24:
					if (this.ᝎ == null)
					{
						goto IL_2C;
					}
					break;
				}
				IL_6F:
				return this.ᝎ;
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000319F8 File Offset: 0x000309F8
		public Document(string fileName) : this()
		{
			this.ᜀ(fileName, "");
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00031A18 File Offset: 0x00030A18
		public Document(string fileName, string password) : this()
		{
			this.ᜀ(fileName, password);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00031A34 File Offset: 0x00030A34
		public Document(string fileName, FileFormat type) : this()
		{
			this.LoadFromFile(fileName, type, "");
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00031A54 File Offset: 0x00030A54
		public Document(string fileName, FileFormat type, XHTMLValidationType validationType) : this()
		{
			if (type == FileFormat.Auto)
			{
				type = this.\u170D(fileName);
			}
			this.LoadFromFile(fileName, type, validationType);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00031A84 File Offset: 0x00030A84
		public Document(string fileName, FileFormat type, string password) : this()
		{
			if (type == FileFormat.Auto)
			{
				type = this.\u170D(fileName);
			}
			this.LoadFromFile(fileName, type, password);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00031AB4 File Offset: 0x00030AB4
		private new void ᜀ(string A_0, string A_1)
		{
			int a_ = 14;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					string extension = Path.GetExtension(A_0);
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_CC;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				}
				case 2:
				{
					string extension;
					if (!string.IsNullOrEmpty(extension))
					{
						goto IL_CC;
					}
					goto IL_D9;
				}
				case 3:
					goto IL_D7;
				}
				if (File.Exists(A_0))
				{
					num = 0;
					continue;
				}
				goto IL_70;
				IL_CC:
				num = 3;
			}
			IL_5E:
			FileFormat fileFormat = this.\u170D(A_0);
			this.LoadFromFile(A_0, fileFormat, A_1);
			return;
			IL_70:
			throw new Exception(ClipboardData.b("㝳͵੷ࡹ᥻ၽꊁ겋ﺍﲓ뚕벛쾟횡蒣쎥킧쎩\ud8ab\uddad麯", a_));
			IL_D7:
			goto IL_5E;
			try
			{
				IL_D9:
				this.LoadFromFile(A_0, FileFormat.Doc, A_1);
			}
			catch (Exception)
			{
				goto IL_3B;
			}
			return;
			try
			{
				IL_3B:
				this.LoadFromFile(A_0, FileFormat.Docx, A_1);
				return;
			}
			catch
			{
				throw new Exception(ClipboardData.b("㉳ήᑷό屻᡽ﲇꪉ쾋ﺏﲑﮓ뢗몙ﺛﮝ肟킡솣얥잧충슫잭쪯ힱ킳颵颷", a_));
			}
			goto IL_5E;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00031BC8 File Offset: 0x00030BC8
		public Document(Stream stream, FileFormat type, XHTMLValidationType validationType) : this()
		{
			this.LoadFromStream(stream, type, validationType);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00031BE4 File Offset: 0x00030BE4
		public Document() : base(null, null)
		{
			this.\u173E = true;
			if (this.ᜥ())
			{
				this.ᜀ();
			}
			this.m_doc = this;
			this.ᜅ();
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00031CA0 File Offset: 0x00030CA0
		public Document(Stream stream) : this()
		{
			FileFormat fileFormat = FileFormat.Auto;
			this.LoadFromStream(stream, fileFormat, "");
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00031CC8 File Offset: 0x00030CC8
		public Document(Stream stream, FileFormat type) : this()
		{
			this.LoadFromStream(stream, type, null);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00031CE4 File Offset: 0x00030CE4
		public Document(Stream stream, string password) : this()
		{
			FileFormat fileFormat = FileFormat.Auto;
			this.LoadFromStream(stream, fileFormat, password);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00031D08 File Offset: 0x00030D08
		public Document(Stream stream, FileFormat type, string password) : this()
		{
			this.LoadFromStream(stream, type, password);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00031D24 File Offset: 0x00030D24
		protected Document(Document doc) : this()
		{
			this.ᜡ = doc.StandardAsciiFont;
			this.ᜢ = doc.StandardFarEastFont;
			this.ᜣ = doc.StandardNonFarEastFont;
			this.ᜤ = doc.ᜤ;
			this.\u1712 = doc.ViewSetup.ᜀ(this);
			if (doc.BuiltinDocumentProperties != null)
			{
				this.ᜊ = doc.BuiltinDocumentProperties.Clone();
			}
			if (doc.CustomDocumentProperties != null)
			{
				this.ᜋ = doc.CustomDocumentProperties.Clone();
			}
			if (doc.Watermark.Type != WatermarkType.NoWatermark)
			{
				this.Watermark = (WatermarkBase)doc.Watermark.Clone();
			}
			if (doc.Background.Type != BackgroundType.NoBackground)
			{
				this.\u1714 = doc.Background.ᜇ();
				this.\u1714.ᜀ(this);
				this.\u1714.ᜀ(this);
			}
			if (doc.DOP != null)
			{
				this.\u1715 = doc.DOP.ᜊ();
			}
			if (doc.DefCharFormat != null)
			{
				this.ᜫ = new CharacterFormat(this);
				this.ᜫ.ImportContainer(doc.DefCharFormat);
			}
			if (doc.DefParaFormat != null)
			{
				goto IL_158;
			}
			IL_8D:
			using (Dictionary<string, string>.Enumerator enumerator = doc.FontSubstitutionTable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, string> keyValuePair = enumerator.Current;
					if (!this.FontSubstitutionTable.ContainsKey(keyValuePair.Key))
					{
						this.FontSubstitutionTable.Add(keyValuePair.Key, keyValuePair.Value);
					}
					else
					{
						this.FontSubstitutionTable[keyValuePair.Key] = keyValuePair.Value;
					}
				}
				goto IL_1F8;
			}
			goto IL_158;
			IL_1F8:
			this.ImportContent(doc);
			return;
			IL_158:
			this.ᜬ = new ParagraphFormat(this);
			this.ᜬ.ImportContainer(doc.DefParaFormat);
			goto IL_8D;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00031F40 File Offset: 0x00030F40
		private FileFormat \u170D(string A_0)
		{
			int a_ = 16;
			switch (0)
			{
			default:
			{
				FileFormat result;
				for (;;)
				{
					string text = Path.GetExtension(A_0).ToLower();
					int num = 18;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_2C8;
						case 1:
							goto IL_12A;
						case 2:
							goto IL_2F4;
						case 3:
							return result;
						case 4:
							goto IL_2E4;
						case 5:
							goto IL_C7;
						case 6:
							num = 12;
							continue;
						case 7:
							spr᧓.៧ = new Dictionary<string, int>(11)
							{
								{
									ClipboardData.b("塵ᱷᕹύ", a_),
									0
								},
								{
									ClipboardData.b("塵ᱷᕹࡻ", a_),
									1
								},
								{
									ClipboardData.b("塵ᱷᕹύٽ", a_),
									2
								},
								{
									ClipboardData.b("塵ᱷᕹࡻٽ", a_),
									3
								},
								{
									ClipboardData.b("塵ᱷᕹύ፽", a_),
									4
								},
								{
									ClipboardData.b("塵ᱷᕹࡻ፽", a_),
									5
								},
								{
									ClipboardData.b("塵౷ɹࡻ", a_),
									6
								},
								{
									ClipboardData.b("塵w᝹ၻ", a_),
									7
								},
								{
									ClipboardData.b("塵ၷ๹ᅻች", a_),
									8
								},
								{
									ClipboardData.b("塵੷๹᩻", a_),
									9
								},
								{
									ClipboardData.b("塵ᵷ੹ॻᱽ", a_),
									10
								}
							};
							num = 8;
							continue;
						case 8:
							goto IL_26F;
						case 9:
						{
							int num2;
							switch (num2)
							{
							case 0:
							case 1:
								result = FileFormat.Doc;
								goto IL_2E8;
							case 2:
								result = FileFormat.Docx;
								num = 16;
								continue;
							case 3:
								result = FileFormat.Dotx;
								num = 19;
								continue;
							case 4:
								result = FileFormat.Docm;
								num = 5;
								continue;
							case 5:
								result = FileFormat.Dotm;
								num = 3;
								continue;
							case 6:
								result = FileFormat.Txt;
								num = 10;
								continue;
							case 7:
								result = FileFormat.Xml;
								num = 11;
								continue;
							case 8:
								result = FileFormat.Html;
								if (true)
								{
								}
								num = 4;
								continue;
							case 9:
								result = FileFormat.Rtf;
								num = 1;
								continue;
							case 10:
								result = FileFormat.EPub;
								num = 0;
								continue;
							default:
								num = 6;
								continue;
							}
							break;
						}
						case 10:
							goto IL_243;
						case 11:
							goto IL_B4;
						case 12:
							goto IL_32B;
						case 13:
							num = 17;
							continue;
						case 14:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2E8;
							default:
								if (false)
								{
								}
								num = 9;
								continue;
							}
							break;
						case 15:
						{
							int num2;
							string key;
							if (spr᧓.៧.TryGetValue(key, out num2))
							{
								num = 14;
								continue;
							}
							goto IL_248;
						}
						case 16:
							return result;
						case 17:
							if (spr᧓.៧ == null)
							{
								num = 7;
								continue;
							}
							goto IL_26F;
						case 18:
						{
							string key;
							if ((key = text) != null)
							{
								num = 13;
								continue;
							}
							goto IL_248;
						}
						case 19:
							goto IL_26A;
						}
						break;
						IL_26F:
						num = 15;
						continue;
						IL_2E8:
						num = 2;
					}
				}
				IL_B4:
				IL_C7:
				IL_12A:
				IL_243:
				return result;
				IL_248:
				throw new Exception(ClipboardData.b("㕵᥷ᑹቻᅽꊁﲇ揄낏ﾙ肟쒡춣쪥춧誩\ud8ab힭삯ힱ", a_));
				IL_26A:
				IL_2C8:
				IL_2E4:
				IL_2F4:
				return result;
				IL_32B:
				goto IL_248;
			}
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000322A0 File Offset: 0x000312A0
		public Paragraph CreateParagraph()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return new Paragraph(this);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000322E4 File Offset: 0x000312E4
		public void CreateMinialDocument()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 2:
					this.AddSection().Body.AddParagraph();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_29;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				goto IL_1C;
				IL_29:
				num = 2;
				continue;
				IL_1C:
				if (this.Sections.Count == 0)
				{
					goto IL_29;
				}
				break;
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00032370 File Offset: 0x00031370
		public Section AddSection()
		{
			Section section;
			for (;;)
			{
				IL_1C:
				section = new Section(base.Document);
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_64:
					num = 1;
					break;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_C7;
					case 1:
					{
						PageSetup pageSetup = this.m_sections[this.m_sections.Count - 1].PageSetup;
						PageSetup pageSetup2 = section.PageSetup;
						pageSetup2.Margins = pageSetup.Margins.Clone();
						pageSetup2.PageSize = pageSetup.ᜂ();
						pageSetup2.Orientation = pageSetup.Orientation;
						num = 0;
						continue;
					}
					case 2:
						goto IL_56;
					}
					goto IL_1C;
				}
				IL_56:
				if (this.m_sections.Count > 0)
				{
					goto IL_64;
				}
				break;
			}
			IL_C7:
			this.m_sections.Add(section);
			return section;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00032454 File Offset: 0x00031454
		public ParagraphStyle AddParagraphStyle(string styleName)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ᜀ(StyleType.ParagraphStyle, styleName) as ParagraphStyle;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0003249C File Offset: 0x0003149C
		public ListStyle AddListStyle(ListType listType, string styleName)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			ListStyle listStyle = new ListStyle(this, listType);
			this.ListStyles.Add(listStyle);
			listStyle.Name = styleName;
			return listStyle;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x000324F4 File Offset: 0x000314F4
		public string GetText()
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			spr\u2194 spr_u = new spr\u2194();
			return spr_u.ᜀ(this);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0003253C File Offset: 0x0003153C
		public new Document Clone()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return (Document)this.CloneImpl();
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00032584 File Offset: 0x00031584
		public void ImportSection(ISection section)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			ISection section2 = section.Clone();
			this.Sections.Add(section2);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000325D4 File Offset: 0x000315D4
		public void ImportContent(IDocument doc)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ImportContent(doc, true);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00032618 File Offset: 0x00031618
		internal new void ᜀ(IDocument A_0, ImportOptions A_1)
		{
			for (;;)
			{
				(A_0 as Document).ᜉ = true;
				this.ᜮ = A_1;
				int num = 14;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜀ(A_0);
						num = 7;
						continue;
					case 1:
						goto IL_81;
					case 2:
						if (this.ᜭ == null)
						{
							num = 6;
							continue;
						}
						goto IL_162;
					case 3:
						goto IL_162;
					case 4:
						this.ᜯ = false;
						num = 15;
						continue;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_74;
						default:
							if (false)
							{
							}
							this.ᜁ(A_0);
							num = 10;
							continue;
						}
						break;
					case 6:
						num = 13;
						continue;
					case 7:
						goto IL_1D3;
					case 8:
						if (this.ᜮ == ImportOptions.KeepTextOnly)
						{
							num = 0;
							continue;
						}
						A_0.Sections.ᜀ(this.m_sections);
						this.ᜀ((A_0 as Document).MacrosData, ref this.\u171C);
						this.ᜀ((A_0 as Document).MacroCommands, ref this.\u171F);
						num = 16;
						continue;
					case 9:
						goto IL_18F;
					case 10:
						goto IL_81;
					case 11:
						num = 17;
						continue;
					case 12:
						this.ᜭ = (A_0 as Document).DocxPackage.ᜀ();
						num = 3;
						continue;
					case 13:
						if ((A_0 as Document).DocxPackage != null)
						{
							num = 12;
							continue;
						}
						goto IL_162;
					case 14:
						if (this.ᜮ == ImportOptions.UseDestinationStyles)
						{
							goto IL_74;
						}
						goto IL_DF;
					case 15:
						goto IL_DF;
					case 16:
						if ((A_0 as Document).ObjectPool != null)
						{
							num = 11;
							continue;
						}
						goto IL_1D8;
					case 17:
						if (this.ObjectPool != null)
						{
							num = 5;
							continue;
						}
						goto IL_1D8;
					}
					break;
					IL_74:
					num = 4;
					continue;
					IL_81:
					if (true)
					{
					}
					num = 2;
					continue;
					IL_DF:
					num = 8;
					continue;
					IL_162:
					this.\u1737 = (A_0 as Document).\u1737;
					this.\u1736 = (A_0 as Document).\u1736;
					num = 9;
					continue;
					IL_1D8:
					this.ᜀ((A_0 as Document).ObjectPool, ref this.\u171B);
					num = 1;
				}
			}
			IL_18F:
			IL_1D3:
			(A_0 as Document).ᜉ = false;
			this.ᜮ = ImportOptions.UseDestinationStyles;
			this.ᜯ = true;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000328A8 File Offset: 0x000318A8
		private new void ᜀ(IDocument A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					string text = A_0.Sections.ᜀ();
					this.ᜅ = null;
					ISection section = this.AddSection();
					string[] array = text.Split(new char[]
					{
						'\r'
					});
					int num = 0;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_6F;
						case 1:
							if (num >= array.Length)
							{
								num2 = 3;
								continue;
							}
							section.AddParagraph().AppendText(array[num]);
							num++;
							goto IL_C6;
						case 2:
							goto IL_6F;
						case 3:
							return;
						}
						break;
						IL_6F:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_C6:
							num2 = 0;
							break;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num2 = 1;
							break;
						}
					}
				}
				return;
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0003298C File Offset: 0x0003198C
		public void ImportContent(IDocument doc, bool importStyles)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					(doc as Document).ᜉ = true;
					this.ᜯ = importStyles;
					doc.Sections.ᜀ(this.m_sections);
					Style style = null;
					Dictionary<string, string>.Enumerator enumerator = (doc as Document).StyleNameIds.GetEnumerator();
					int num = 30;
					for (;;)
					{
						int num2;
						int num3;
						int num4;
						int count3;
						switch (num)
						{
						case 0:
							goto IL_60B;
						case 1:
							num = 44;
							continue;
						case 2:
						{
							try
							{
								num = 4;
								for (;;)
								{
									KeyValuePair<string, string> keyValuePair;
									switch (num)
									{
									case 0:
										num = 5;
										continue;
									case 1:
										goto IL_322;
									case 2:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_322;
										default:
											if (false)
											{
											}
											this.FontSubstitutionTable.Add(keyValuePair.Key, keyValuePair.Value);
											num = 3;
											continue;
										}
										break;
									case 5:
										goto IL_3D7;
									case 7:
									{
										Dictionary<string, string>.Enumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num = 0;
											continue;
										}
										keyValuePair = enumerator2.Current;
										num = 1;
										continue;
									}
									}
									goto IL_30E;
									IL_322:
									if (!this.FontSubstitutionTable.ContainsKey(keyValuePair.Key))
									{
										num = 2;
										continue;
									}
									this.FontSubstitutionTable[keyValuePair.Key] = keyValuePair.Value;
									num = 6;
									continue;
									IL_341:
									num = 7;
									continue;
									IL_30E:
									goto IL_341;
								}
								IL_3D7:
								goto IL_543;
							}
							finally
							{
								Dictionary<string, string>.Enumerator enumerator2;
								((IDisposable)enumerator2).Dispose();
							}
							goto Block_7;
							IL_543:
							spr\u177D spr_u177D = null;
							num2 = 0;
							int count = (doc as Document).ListOverrides.Count;
							num = 36;
							continue;
						}
						case 3:
							if (this.ᜫ == null)
							{
								num = 45;
								continue;
							}
							goto IL_60B;
						case 4:
						{
							ListStyle listStyle = null;
							num3 = 0;
							int count2 = doc.ListStyles.Count;
							num = 25;
							continue;
						}
						case 5:
						{
							ListStyle listStyle;
							this.ListStyles.Add((ListStyle)listStyle.Clone());
							num = 32;
							continue;
						}
						case 6:
							this.ᜬ = new ParagraphFormat(this.m_doc);
							num = 27;
							continue;
						case 7:
							goto IL_594;
						case 8:
							goto IL_197;
						case 9:
							if (this.ᜬ == null)
							{
								num = 6;
								continue;
							}
							goto IL_4FA;
						case 10:
							goto IL_22F;
						case 11:
						{
							int count2;
							if (num3 >= count2)
							{
								num = 40;
								continue;
							}
							ListStyle listStyle = doc.ListStyles[num3];
							num = 16;
							continue;
						}
						case 12:
							num = 9;
							continue;
						case 13:
							if (this.ObjectPool != null)
							{
								num = 20;
								continue;
							}
							goto IL_1DF;
						case 14:
							this.ᜀ((doc as Document).MacrosData, ref this.\u171C);
							this.ᜀ((doc as Document).MacroCommands, ref this.\u171F);
							num = 18;
							continue;
						case 15:
						{
							spr\u177D spr_u177D;
							if (this.ListOverrides.ᜀ(spr_u177D.Name) == null)
							{
								num = 26;
								continue;
							}
							goto IL_22F;
						}
						case 16:
						{
							ListStyle listStyle;
							if (this.ListStyles.FindByName(listStyle.Name) == null)
							{
								num = 5;
								continue;
							}
							goto IL_4E3;
						}
						case 17:
							this.ᜭ = (doc as Document).DocxPackage.ᜀ();
							num = 39;
							continue;
						case 18:
							if ((doc as Document).ObjectPool != null)
							{
								num = 35;
								continue;
							}
							goto IL_1DF;
						case 19:
							goto IL_145;
						case 20:
							this.ᜁ(doc);
							num = 22;
							continue;
						case 21:
							this.Styles.Add(style.Clone());
							num = 23;
							continue;
						case 22:
							goto IL_11B;
						case 23:
							goto IL_4D1;
						case 24:
							if (this.ᜬ == null)
							{
								num = 12;
								continue;
							}
							goto IL_207;
						case 25:
							goto IL_594;
						case 26:
						{
							spr\u177D spr_u177D;
							this.ListOverrides.ᜀ((spr\u177D)spr_u177D.Clone());
							num = 10;
							continue;
						}
						case 27:
							goto IL_4FA;
						case 28:
						{
							Style style2;
							if (style2 == null)
							{
								num = 21;
								continue;
							}
							goto IL_4D1;
						}
						case 29:
						{
							if (num4 >= count3)
							{
								num = 4;
								continue;
							}
							style = (doc.Styles[num4] as Style);
							Style style2 = this.Styles.FindByName(style.Name, style.StyleType) as Style;
							num = 28;
							continue;
						}
						case 30:
							goto IL_3EA;
						case 31:
							if (this.ᜭ == null)
							{
								num = 1;
								continue;
							}
							goto IL_7F8;
						case 32:
							goto IL_4E3;
						case 33:
							if ((doc as Document).DefCharFormat != null)
							{
								num = 34;
								continue;
							}
							goto IL_197;
						case 34:
							num = 3;
							continue;
						case 35:
							num = 13;
							continue;
						case 36:
							goto IL_145;
						case 37:
							goto IL_207;
						case 38:
						{
							int count;
							if (num2 >= count)
							{
								if (true)
								{
								}
								num = 14;
								continue;
							}
							spr\u177D spr_u177D = (doc as Document).ListOverrides.ᜀ(num2);
							num = 15;
							continue;
						}
						case 39:
							goto IL_2CD;
						case 40:
						{
							Dictionary<string, string>.Enumerator enumerator2 = (doc as Document).FontSubstitutionTable.GetEnumerator();
							num = 2;
							continue;
						}
						case 41:
							goto IL_4AD;
						case 42:
							goto IL_4AD;
						case 43:
							goto IL_11B;
						case 44:
							if ((doc as Document).DocxPackage != null)
							{
								num = 17;
								continue;
							}
							goto IL_7F8;
						case 45:
							this.ᜫ = new CharacterFormat(this.m_doc);
							num = 0;
							continue;
						}
						break;
						IL_11B:
						num = 33;
						continue;
						IL_145:
						num = 38;
						continue;
						IL_197:
						num = 24;
						continue;
						IL_1DF:
						this.ᜀ((doc as Document).ObjectPool, ref this.\u171B);
						num = 43;
						continue;
						IL_207:
						num = 31;
						continue;
						IL_22F:
						num2++;
						num = 19;
						continue;
						IL_4AD:
						num = 29;
						continue;
						Block_7:
						try
						{
							IL_3EA:
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_49A;
								case 2:
								{
									KeyValuePair<string, string> keyValuePair2;
									this.StyleNameIds.Add(keyValuePair2.Key, keyValuePair2.Value);
									num = 3;
									continue;
								}
								case 4:
								{
									if (!enumerator.MoveNext())
									{
										num = 6;
										continue;
									}
									KeyValuePair<string, string> keyValuePair2 = enumerator.Current;
									num = 5;
									continue;
								}
								case 5:
								{
									KeyValuePair<string, string> keyValuePair2;
									if (!this.StyleNameIds.ContainsKey(keyValuePair2.Key))
									{
										num = 2;
										continue;
									}
									break;
								}
								case 6:
									num = 0;
									continue;
								}
								IL_41A:
								num = 4;
								continue;
								goto IL_41A;
							}
							IL_49A:
							goto IL_7D9;
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
						goto IL_4AD;
						IL_7D9:
						num4 = 0;
						count3 = doc.Styles.Count;
						num = 42;
						continue;
						IL_4D1:
						num4++;
						num = 41;
						continue;
						IL_4E3:
						num3++;
						num = 7;
						continue;
						IL_4FA:
						this.ᜬ.ImportContainer((doc as Document).DefParaFormat);
						num = 37;
						continue;
						IL_594:
						num = 11;
						continue;
						IL_60B:
						this.ᜫ.ImportContainer((doc as Document).DefCharFormat);
						num = 8;
					}
				}
				IL_2CD:
				IL_7F8:
				this.\u1737 = (doc as Document).\u1737;
				this.\u1736 = (doc as Document).\u1736;
				(doc as Document).ᜉ = false;
				return;
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000331F4 File Offset: 0x000321F4
		internal void ᜁ(IDocument A_0)
		{
			int a_ = 10;
			switch (0)
			{
			default:
			{
				MemoryStream memoryStream = new MemoryStream((A_0 as Document).ObjectPool);
				int num = -1;
				try
				{
					for (;;)
					{
						memoryStream.Position = 0L;
						sprᤘ sprᤘ = new sprᤘ(memoryStream);
						string text = sprᤘ.\u1717()[0].Replace(ClipboardData.b("⽯", a_), string.Empty);
						int num2 = 0;
						for (;;)
						{
							sprᤘ sprᤘ2;
							int num3;
							switch (num2)
							{
							case 0:
								if (text == ClipboardData.b("㽯ၱṳ፵᭷๹ⱻᅽ", a_))
								{
									num2 = 6;
									continue;
								}
								goto IL_1F2;
							case 1:
								goto IL_12C;
							case 2:
								goto IL_138;
							case 3:
								goto IL_1F2;
							case 4:
								if (num != -1)
								{
									num2 = 9;
									continue;
								}
								goto IL_172;
							case 5:
								goto IL_20C;
							case 6:
								sprᤘ2 = sprᤘ.ᜆ(text);
								num3 = 0;
								num2 = 8;
								continue;
							case 7:
								goto IL_172;
							case 8:
								goto IL_12C;
							case 9:
							{
								byte[] a_2 = this.m_doc.ObjectPool;
								spr\u1C2D.ᜀ(new MemoryStream((A_0 as Document).ObjectPool), num, new MemoryStream(this.m_doc.ObjectPool), out a_2);
								this.m_doc.ObjectPool = a_2;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_138;
								default:
									if (false)
									{
									}
									num2 = 7;
									continue;
								}
								break;
							}
							}
							break;
							IL_138:
							if (num3 >= sprᤘ2.\u1717().Length)
							{
								num2 = 3;
								continue;
							}
							if (true)
							{
							}
							text = sprᤘ2.\u1717()[num3].Replace(ClipboardData.b("⽯", a_), string.Empty);
							this.m_doc.ObjPoolContainers.Add(text);
							num = int.Parse(text);
							num2 = 4;
							continue;
							IL_12C:
							num2 = 2;
							continue;
							IL_172:
							num3++;
							num2 = 1;
							continue;
							IL_1F2:
							sprᤘ.Close();
							sprᤘ.Dispose();
							num2 = 5;
						}
					}
					IL_20C:;
				}
				catch
				{
				}
				memoryStream.Close();
				memoryStream.Dispose();
				return;
			}
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0003343C File Offset: 0x0003243C
		public Style AddStyle(BuiltinStyle builtinStyle)
		{
			int a_ = 11;
			Style style;
			for (;;)
			{
				this.ᜋ();
				string name = Style.ᜁ(builtinStyle);
				style = base.Document.Styles.FindByName(name);
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						if (builtinStyle != BuiltinStyle.CommentSubject)
						{
							num = 5;
							continue;
						}
						goto IL_11E;
					case 2:
						if (builtinStyle != BuiltinStyle.MacroText)
						{
							num = 0;
							continue;
						}
						goto IL_11E;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5A;
						default:
							if (false)
							{
							}
							style = Style.CreateBuiltinStyle(builtinStyle, base.Document);
							base.Document.Styles.Add(style);
							num = 2;
							continue;
						}
						break;
					case 4:
						if (style == null)
						{
							goto IL_5A;
						}
						goto IL_11E;
					case 5:
					{
						IStyle style2 = base.Document.Styles.FindByName(name);
						(style2 as Style).ApplyBaseStyle(ClipboardData.b("㽰ᱲݴ᩶ᡸ᝺", a_));
						num = 6;
						continue;
					}
					case 6:
						goto IL_9A;
					}
					break;
					IL_5A:
					num = 3;
				}
			}
			IL_9A:
			IL_11E:
			if (true)
			{
			}
			return style;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00033570 File Offset: 0x00032570
		public void AcceptChanges()
		{
			IEnumerator enumerator = this.Sections.GetEnumerator();
			try
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 1:
					{
						if (!enumerator.MoveNext())
						{
							num = 2;
							continue;
						}
						Section section = (Section)enumerator.Current;
						section.ᜁ(true);
						num = 0;
						continue;
					}
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8F;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 3:
						goto IL_8F;
					}
					IL_51:
					num = 1;
					continue;
					goto IL_51;
				}
				IL_8F:;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (disposable != null)
							{
								num = 2;
								continue;
							}
							goto IL_D1;
						case 1:
							goto IL_CF;
						case 2:
							disposable.Dispose();
							num = 1;
							continue;
						}
						break;
					}
				}
				IL_CF:
				IL_D1:;
			}
			if (true)
			{
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00033674 File Offset: 0x00032674
		public void RejectChanges()
		{
			IEnumerator enumerator = this.Sections.GetEnumerator();
			try
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_8F;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8F;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 3:
					{
						if (!enumerator.MoveNext())
						{
							num = 2;
							continue;
						}
						Section section = (Section)enumerator.Current;
						section.ᜁ(false);
						num = 1;
						continue;
					}
					}
					IL_51:
					num = 3;
					continue;
					goto IL_51;
				}
				IL_8F:;
			}
			finally
			{
				for (;;)
				{
					if (true)
					{
					}
					IDisposable disposable = enumerator as IDisposable;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (disposable != null)
							{
								num = 1;
								continue;
							}
							goto IL_D9;
						case 1:
							disposable.Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_D7;
						}
						break;
					}
				}
				IL_D7:
				IL_D9:;
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00033778 File Offset: 0x00032778
		public void Protect(ProtectionType type)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.Protect(type, null);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000337BC File Offset: 0x000327BC
		public void Protect(ProtectionType type, string password)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.\u1715.ᜀ(type, password);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00033804 File Offset: 0x00032804
		public void Encrypt(string password)
		{
			int a_ = 16;
			if (!string.IsNullOrEmpty(password))
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					this.\u171A = password;
					this.ᝀ = true;
					return;
				}
			}
			throw new Exception(ClipboardData.b("♵᥷ॹཻॽꚅ黎겋ﺑ뚕벛ﮝ춟튡킣\udfa5覧", a_));
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00033874 File Offset: 0x00032874
		public void RemoveEncryption()
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.\u171A = null;
			this.ᝀ = false;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x000338C0 File Offset: 0x000328C0
		internal new IStyle ᜀ(StyleType A_0, string A_1)
		{
			int num = 13;
			for (;;)
			{
				IStyle style;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_110;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (A_1.Length <= 0)
						{
							num = 15;
							continue;
						}
						goto IL_F6;
					}
					break;
				case 1:
					num = 0;
					continue;
				case 2:
					return style;
				case 3:
					goto IL_F6;
				case 4:
					if (style != null)
					{
						num = 9;
						continue;
					}
					return style;
				case 5:
					switch (A_0)
					{
					case StyleType.ParagraphStyle:
						style = new ParagraphStyle(base.Document);
						num = 16;
						continue;
					case StyleType.TableStyle:
						goto IL_A7;
					case StyleType.CharacterStyle:
						style = new sprᯉ(base.Document);
						num = 8;
						continue;
					default:
						num = 7;
						continue;
					}
					break;
				case 6:
					goto IL_A7;
				case 7:
					num = 6;
					continue;
				case 8:
					goto IL_A7;
				case 9:
					num = 10;
					continue;
				case 10:
					if (A_1 != null)
					{
						num = 1;
						continue;
					}
					goto IL_130;
				case 11:
					goto IL_60;
				case 12:
					if (this.ᜇ)
					{
						num = 3;
						continue;
					}
					goto IL_130;
				case 14:
					goto IL_130;
				case 15:
					goto IL_110;
				case 16:
					goto IL_A7;
				}
				if (A_0 == StyleType.OtherStyle)
				{
					num = 11;
					continue;
				}
				style = null;
				num = 5;
				continue;
				IL_A7:
				num = 4;
				continue;
				IL_F6:
				style.Name = A_1;
				num = 14;
				continue;
				IL_110:
				num = 12;
				continue;
				IL_130:
				this.m_styles.Add(style);
				num = 2;
			}
			IL_60:
			throw new NotSupportedException();
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00033A88 File Offset: 0x00032A88
		private void ᜋ()
		{
			int a_ = 0;
			for (;;)
			{
				ParagraphStyle paragraphStyle = base.Document.Styles.FindByName(ClipboardData.b("⡥ݧᡩū཭ᱯ", a_), StyleType.ParagraphStyle) as ParagraphStyle;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_83;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_83;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							if (paragraphStyle == null)
							{
								num = 0;
								continue;
							}
							return;
						}
						break;
					case 2:
						return;
					}
					break;
					IL_83:
					paragraphStyle = (ParagraphStyle)Style.CreateBuiltinStyle(BuiltinStyle.Normal, base.Document);
					base.Document.Styles.Add(paragraphStyle);
					num = 2;
				}
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00033B4C File Offset: 0x00032B4C
		private new void ᜀ(object A_0, spr\u249F A_1)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			Document.ᜀ value = default(Document.ᜀ);
			value.ᜁ = sprᦪ.ᜅ((double)A_1.ᜃ()[A_1.ᜄ()].Height).ᜅ();
			value.ᜀ = sprᦪ.ᜅ((double)A_1.ᜃ()[A_1.ᜄ()].Width).ᜅ();
			value.ᜂ = A_1.ᜁ();
			this.ᝢ.Add(A_1.ᜄ(), value);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00033C04 File Offset: 0x00032C04
		public virtual void OnPrintPage(object sender, PrintPageEventArgs e)
		{
			switch (0)
			{
			default:
			{
				int num = 4;
				for (;;)
				{
					Graphics graphics;
					switch (num)
					{
					case 0:
						try
						{
							GraphicsState gstate = graphics.Save();
							graphics.TranslateTransform(0f, 0f);
							Image image;
							graphics.DrawImage(image, new RectangleF(0f, 0f, (float)image.Width, (float)image.Height));
							graphics.Restore(gstate);
						}
						finally
						{
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_1AB;
								case 2:
									((IDisposable)graphics).Dispose();
									num = 0;
									continue;
								}
								if (graphics == null)
								{
									break;
								}
								num = 2;
							}
							IL_1AB:;
						}
						this.ᝧ++;
						e.HasMorePages = (this.ᝧ < this.ᝦ);
						num = 7;
						continue;
					case 1:
						goto IL_D7;
					case 2:
						if (this.ᝧ < this.ᝦ)
						{
							num = 14;
							continue;
						}
						return;
					case 3:
						goto IL_88;
					case 5:
						goto IL_72;
					case 6:
					{
						Image image = this.ᜀ(image, (int)this.ᝨ, (int)this.ᝩ, 90);
						float num3;
						float num4;
						float num2 = Math.Min(this.ᝨ / num3, this.ᝩ / num4);
						num = 3;
						continue;
					}
					case 7:
						return;
					case 8:
					{
						float num2;
						if ((double)num2 < 1.0)
						{
							num = 10;
							continue;
						}
						goto IL_72;
					}
					case 9:
					{
						float num3;
						float num4;
						if (num4 > num3)
						{
							num = 13;
							continue;
						}
						goto IL_88;
					}
					case 10:
					{
						Image image = this.ᜀ(image, (int)this.ᝨ, (int)this.ᝩ, 0);
						num = 5;
						continue;
					}
					case 11:
						if (this.ᝨ < this.ᝩ)
						{
							num = 6;
							continue;
						}
						goto IL_88;
					case 12:
						this.ᝦ = this.ᝢ.Count;
						num = 1;
						continue;
					case 13:
						num = 11;
						continue;
					case 14:
					{
						Document.ᜀ ᜀ = this.ᝢ[this.ᝧ];
						Image image = (Image)ᜀ.ᜂ.Clone();
						float num4 = (float)ᜀ.ᜀ;
						float num3 = (float)ᜀ.ᜁ;
						this.ᝨ = (float)e.PageSettings.Bounds.Width;
						this.ᝩ = (float)e.PageSettings.Bounds.Height;
						float num2 = Math.Min(this.ᝨ / num4, this.ᝩ / num3);
						num = 9;
						continue;
					}
					}
					if (this.ᝦ == 0)
					{
						num = 12;
						continue;
					}
					goto IL_D7;
					IL_72:
					graphics = e.Graphics;
					num = 0;
					continue;
					IL_88:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 8;
						continue;
					}
					IL_D7:
					num = 2;
				}
				return;
			}
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00033F48 File Offset: 0x00032F48
		private new Image ᜀ(Image A_0, int A_1, int A_2, int A_3)
		{
			switch (0)
			{
			default:
			{
				int num = 7;
				Image image;
				Graphics graphics4;
				for (;;)
				{
					Image image2;
					Graphics graphics3;
					int num2;
					int width;
					int num3;
					int height;
					switch (num)
					{
					case 0:
						goto IL_3B9;
					case 1:
						image = null;
						num = 10;
						continue;
					case 2:
					{
						MemoryStream stream = new MemoryStream();
						Bitmap bitmap = new Bitmap(A_1, A_2, PixelFormat.Format32bppPArgb);
						num = 4;
						continue;
					}
					case 3:
					{
						MemoryStream stream2 = new MemoryStream();
						Bitmap bitmap2 = new Bitmap(A_1, A_2, PixelFormat.Format32bppPArgb);
						num = 8;
						continue;
					}
					case 4:
						try
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
							{
								if (false)
								{
								}
								Bitmap bitmap;
								bitmap.SetResolution(A_0.HorizontalResolution, A_0.VerticalResolution);
								Graphics graphics = Graphics.FromImage(bitmap);
								try
								{
									IntPtr hdc = graphics.GetHdc();
									RectangleF frameRect = new RectangleF(0f, 0f, (float)A_1, (float)A_2);
									MemoryStream stream;
									image2 = new Metafile(stream, hdc, frameRect, MetafileFrameUnit.Pixel, EmfType.EmfPlusDual);
									graphics.Dispose();
								}
								finally
								{
									num = 0;
									for (;;)
									{
										switch (num)
										{
										case 1:
											((IDisposable)graphics).Dispose();
											num = 2;
											continue;
										case 2:
											goto IL_112;
										}
										if (graphics == null)
										{
											break;
										}
										num = 1;
									}
									IL_112:;
								}
								break;
							}
							}
							goto IL_22C;
						}
						finally
						{
							num = 2;
							for (;;)
							{
								Bitmap bitmap;
								switch (num)
								{
								case 0:
									goto IL_157;
								case 1:
									((IDisposable)bitmap).Dispose();
									num = 0;
									continue;
								}
								if (bitmap == null)
								{
									break;
								}
								num = 1;
							}
							IL_157:;
						}
						goto IL_15A;
					case 5:
						goto IL_22C;
					case 6:
						goto IL_3A5;
					case 8:
						try
						{
							Bitmap bitmap2;
							bitmap2.SetResolution(A_0.HorizontalResolution, A_0.VerticalResolution);
							Graphics graphics2 = Graphics.FromImage(bitmap2);
							try
							{
								IntPtr hdc2 = graphics2.GetHdc();
								RectangleF frameRect2 = new RectangleF(0f, 0f, (float)A_1, (float)A_2);
								MemoryStream stream2;
								image = new Metafile(stream2, hdc2, frameRect2, MetafileFrameUnit.Pixel, EmfType.EmfPlusDual);
								graphics2.Dispose();
							}
							finally
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										((IDisposable)graphics2).Dispose();
										num = 2;
										continue;
									case 2:
										goto IL_46D;
									}
									if (graphics2 == null)
									{
										break;
									}
									num = 0;
								}
								IL_46D:;
							}
							goto IL_3A5;
						}
						finally
						{
							num = 2;
							for (;;)
							{
								Bitmap bitmap2;
								switch (num)
								{
								case 0:
									((IDisposable)bitmap2).Dispose();
									num = 1;
									continue;
								case 1:
									goto IL_4B0;
								}
								if (bitmap2 == null)
								{
									break;
								}
								num = 0;
							}
							IL_4B0:;
						}
						goto Block_7;
					case 9:
						try
						{
							graphics3.InterpolationMode = InterpolationMode.High;
							graphics3.SmoothingMode = SmoothingMode.HighQuality;
							Point point = new Point((num2 - width) / 2, (num3 - height) / 2);
							Rectangle rect = new Rectangle(point.X, point.Y, width, height);
							Point point2 = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
							graphics3.TranslateTransform((float)point2.X, (float)point2.Y);
							graphics3.RotateTransform((float)A_3);
							graphics3.TranslateTransform((float)(-(float)point2.X), (float)(-(float)point2.Y));
							graphics3.DrawImage(A_0, rect);
							graphics3.ResetTransform();
							graphics3.Save();
							graphics3.Dispose();
							return image2;
						}
						finally
						{
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 1:
									goto IL_372;
								case 2:
									((IDisposable)graphics3).Dispose();
									num = 1;
									continue;
								}
								if (graphics3 == null)
								{
									break;
								}
								num = 2;
							}
							IL_372:;
						}
						goto IL_375;
					case 10:
						if (A_0 is Metafile)
						{
							num = 3;
							continue;
						}
						goto IL_375;
					case 11:
						if (A_0 is Metafile)
						{
							num = 2;
							continue;
						}
						image2 = new Bitmap(A_1, A_2);
						num = 5;
						continue;
					}
					if (A_3 == 0)
					{
						num = 1;
						continue;
					}
					IL_15A:
					A_3 %= 360;
					double num4 = (double)A_3 * 3.141592653589793 / 180.0;
					double num5 = Math.Cos(num4);
					double num6 = Math.Sin(num4);
					width = A_0.Width;
					height = A_0.Height;
					num2 = (int)Math.Max(Math.Abs((double)width * num5 - (double)height * num6), Math.Abs((double)width * num5 + (double)height * num6));
					num3 = (int)Math.Max(Math.Abs((double)width * num6 - (double)height * num5), Math.Abs((double)width * num6 + (double)height * num5));
					image2 = null;
					num = 11;
					continue;
					IL_22C:
					if (true)
					{
					}
					graphics3 = Graphics.FromImage(image2);
					num = 9;
					continue;
					IL_375:
					image = new Bitmap(A_1, A_2);
					num = 6;
					continue;
					IL_3A5:
					graphics4 = Graphics.FromImage(image);
					num = 0;
				}
				IL_3B9:
				Block_7:
				try
				{
					graphics4.InterpolationMode = InterpolationMode.High;
					graphics4.DrawImage(A_0, new Rectangle(0, 0, A_1, A_2), new Rectangle(0, 0, A_0.Width, A_0.Height), GraphicsUnit.Pixel);
				}
				finally
				{
					num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							((IDisposable)graphics4).Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_51F;
						}
						if (graphics4 == null)
						{
							break;
						}
						num = 1;
					}
					IL_51F:;
				}
				return image;
			}
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000344F0 File Offset: 0x000334F0
		private void ᜌ(string A_0)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			sprᣑ sprᣑ = new sprᣑ();
			this.ᜇ = true;
			sprᣑ.ᜀ(A_0, this);
			this.ᜇ = false;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00034548 File Offset: 0x00033548
		private void ᜈ(Stream A_0)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			sprᣑ sprᣑ = new sprᣑ();
			this.ᜇ = true;
			sprᣑ.ᜁ(A_0, this);
			this.ᜇ = false;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x000345A0 File Offset: 0x000335A0
		private void ᜋ(string A_0)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			sprᣑ sprᣑ = new sprᣑ();
			this.ᜇ = true;
			sprᣑ.ᜀ(A_0, this);
			this.ᜇ = false;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000345F8 File Offset: 0x000335F8
		private void ᜇ(Stream A_0)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			sprᣑ sprᣑ = new sprᣑ();
			this.ᜇ = true;
			sprᣑ.ᜁ(A_0, this);
			this.ᜇ = false;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00034650 File Offset: 0x00033650
		internal void \u1714(string A_0)
		{
			if (true)
			{
			}
			FileStream fileStream = new FileStream(A_0, FileMode.Open, FileAccess.Read);
			try
			{
				this.ᜎ(fileStream);
			}
			finally
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_69;
					case 1:
						goto IL_79;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_69;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					}
					if (fileStream != null)
					{
						num = 0;
						continue;
					}
					break;
					IL_69:
					((IDisposable)fileStream).Dispose();
					num = 1;
				}
				IL_79:;
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000346EC File Offset: 0x000336EC
		internal void ᜎ(Stream A_0)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			xmlReaderSettings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
			xmlReaderSettings.CheckCharacters = false;
			XmlReader a_ = XmlReader.Create(A_0, xmlReaderSettings);
			this.ᜀ(a_);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00034758 File Offset: 0x00033758
		internal void ᜏ(Stream A_0)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			sprᤍ sprᤍ = new sprᤍ();
			sprᤍ.ᜀ(A_0, this);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000347A4 File Offset: 0x000337A4
		private new static void ᜀ(object A_0, ValidationEventArgs A_1)
		{
			int a_ = 2;
			while (A_1.Severity != XmlSeverityType.Warning)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					if (true)
					{
					}
					string a_2 = ClipboardData.b("慧㱩൫ɭ᥯ᙱᕳɵᅷᕹቻ幽慎낉겋", a_) + A_1.Message;
					throw new spr\u218C(a_2);
				}
				}
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00034814 File Offset: 0x00033814
		private void ᜊ(string A_0)
		{
			while (this.LicenseType != LicenseType.None)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					this.\u173A();
					XmlTextWriter xmlTextWriter = new XmlTextWriter(A_0, Encoding.Unicode);
					xmlTextWriter.Formatting = Formatting.Indented;
					this.ᜀ(xmlTextWriter);
					xmlTextWriter.Close();
					return;
				}
				}
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00034880 File Offset: 0x00033880
		private void ᜆ(Stream A_0)
		{
			int num = 0;
			for (;;)
			{
				XmlTextWriter xmlTextWriter;
				switch (num)
				{
				case 1:
					return;
				case 2:
					try
					{
						xmlTextWriter.Formatting = Formatting.Indented;
						this.ᜀ(xmlTextWriter);
						return;
					}
					finally
					{
						xmlTextWriter.Flush();
					}
					goto IL_45;
				}
				if (this.LicenseType == LicenseType.None)
				{
					num = 1;
					continue;
				}
				IL_45:
				this.\u173A();
				xmlTextWriter = new XmlTextWriter(A_0, Encoding.Unicode);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 2;
					break;
				}
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00034930 File Offset: 0x00033930
		private void ᜉ(string A_0)
		{
			while (this.LicenseType != LicenseType.None)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					this.\u173A();
					this.SaveToTxt(A_0, Encoding.UTF8);
					return;
				}
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00034988 File Offset: 0x00033988
		public void SaveToTxt(string fileName, Encoding encoding)
		{
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				StreamWriter streamWriter;
				switch (num)
				{
				case 0:
					return;
				case 1:
					try
					{
						spr\u2194 spr_u = new spr\u2194();
						spr_u.ᜀ(streamWriter, this);
						return;
					}
					finally
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_9F;
							case 1:
								goto IL_8F;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_8F;
								default:
									if (false)
									{
									}
									break;
								}
								break;
							}
							if (streamWriter != null)
							{
								num = 1;
								continue;
							}
							break;
							IL_8F:
							((IDisposable)streamWriter).Dispose();
							num = 0;
						}
						IL_9F:;
					}
					goto IL_A2;
				}
				if (this.LicenseType == LicenseType.None)
				{
					num = 0;
					continue;
				}
				IL_A2:
				this.\u173A();
				streamWriter = new StreamWriter(fileName, false, encoding);
				num = 1;
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00034A74 File Offset: 0x00033A74
		private void ᜅ(Stream A_0)
		{
			int num = 1;
			for (;;)
			{
				StreamWriter streamWriter;
				switch (num)
				{
				case 0:
					try
					{
						spr\u2194 spr_u = new spr\u2194();
						spr_u.ᜀ(streamWriter, this);
						return;
					}
					finally
					{
						if (true)
						{
						}
						streamWriter.Flush();
					}
					goto IL_57;
				case 2:
					return;
				}
				if (this.LicenseType == LicenseType.None)
				{
					num = 2;
					continue;
				}
				IL_57:
				this.\u173A();
				streamWriter = new StreamWriter(A_0);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					num = 0;
					break;
				}
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00034B1C File Offset: 0x00033B1C
		private void ᜈ(string A_0)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.\u173A();
			spr\u17BE spr_u17BE = new spr\u17BE();
			spr_u17BE.ᜀ(A_0, this);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00034B6C File Offset: 0x00033B6C
		private void ᜄ(Stream A_0)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.\u173A();
			spr\u17BE spr_u17BE = new spr\u17BE();
			spr_u17BE.ᜀ(A_0, this);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00034BBC File Offset: 0x00033BBC
		private new void ᜀ(string A_0, DocPicture A_1)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			sprᰋ sprᰋ = new sprᰋ();
			sprᰋ.ᜂ(Path.GetFileName(A_0).Replace(Path.GetExtension(A_0), string.Empty));
			sprᰋ.ᜀ(A_1);
			sprᰋ.ᜀ(A_0, this);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00034C28 File Offset: 0x00033C28
		private new void ᜀ(Stream A_0, DocPicture A_1)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			sprᰋ sprᰋ = new sprᰋ();
			sprᰋ.ᜀ(A_1);
			sprᰋ.ᜀ(A_0, this);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00034C78 File Offset: 0x00033C78
		internal void ᜋ(Stream A_0)
		{
			StreamReader streamReader = new StreamReader(A_0);
			try
			{
				this.ᜀ(streamReader);
			}
			finally
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_55;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_55;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 2:
						goto IL_6D;
					}
					if (streamReader != null)
					{
						num = 0;
						continue;
					}
					break;
					IL_55:
					if (true)
					{
					}
					((IDisposable)streamReader).Dispose();
					num = 2;
				}
				IL_6D:;
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00034D10 File Offset: 0x00033D10
		internal new void ᜀ(Stream A_0, Encoding A_1)
		{
			StreamReader streamReader = new StreamReader(A_0, A_1);
			try
			{
				this.ᜀ(streamReader);
			}
			finally
			{
				int num = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5E;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 1:
						goto IL_6E;
					case 2:
						goto IL_5E;
					}
					if (streamReader != null)
					{
						num = 2;
						continue;
					}
					break;
					IL_5E:
					((IDisposable)streamReader).Dispose();
					num = 1;
				}
				IL_6E:;
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00034DA8 File Offset: 0x00033DA8
		internal new void ᜀ(TextReader A_0)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			spr\u2194 spr_u = new spr\u2194();
			spr_u.ᜀ(A_0, this);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00034DF4 File Offset: 0x00033DF4
		internal void \u170D(Stream A_0)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜁ(A_0, null);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00034E38 File Offset: 0x00033E38
		internal void ᜁ(Stream A_0, Encoding A_1)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			sprᭊ sprᭊ = new sprᭊ(this, A_0, A_1);
			sprᭊ.ᜢ();
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00034E84 File Offset: 0x00033E84
		internal void ᜁ(TextReader A_0)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			sprᭊ sprᭊ = new sprᭊ(this, A_0);
			sprᭊ.ᜢ();
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00034ED0 File Offset: 0x00033ED0
		public void LoadHTML(TextReader reader, string baseURL, XHTMLValidationType validationType)
		{
			int a_ = 6;
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					baseURL = null;
					num = 5;
					continue;
				case 1:
					goto IL_A5;
				case 3:
					goto IL_C9;
				case 4:
					if (!Uri.IsWellFormedUriString(baseURL, UriKind.Absolute))
					{
						goto IL_9A;
					}
					this.HtmlBaseUrl = baseURL;
					num = 3;
					continue;
				case 5:
					goto IL_B5;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_9A:
					num = 1;
					break;
				default:
					if (false)
					{
					}
					if (string.IsNullOrEmpty(baseURL))
					{
						num = 0;
					}
					else
					{
						num = 4;
					}
					break;
				}
			}
			IL_A5:
			throw new ArgumentException(ClipboardData.b("㡫٭ᕯ剱ᙳ᝵୷ό屻୽ꒃ曆늑뢗ﮙ벛ﾝ슟톡쮣쪥\udda7\udea9즫躭톯\udcb1킳隵쾷\udfb9킻튽꓁ꯃ듅ꗇ꿉꣋ꗏꃑ뷓ꯗ껙껛럝軟藡", a_), ClipboardData.b("๫཭ͯ᝱ⅳ⑵㑷", a_));
			IL_B5:
			IL_C9:
			this.LoadHTML(reader, validationType);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00034FBC File Offset: 0x00033FBC
		internal new void ᜀ(Stream A_0, XHTMLValidationType A_1)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			StreamReader reader = new StreamReader(A_0, Encoding.GetEncoding(1252));
			this.LoadHTML(reader, A_1);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00035010 File Offset: 0x00034010
		public void LoadHTML(TextReader reader, XHTMLValidationType validationType)
		{
			string html;
			for (;;)
			{
				html = reader.ReadToEnd().Trim();
				reader.Close();
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6F;
						default:
							if (false)
							{
							}
							this.CreateMinialDocument();
							num = 0;
							continue;
						}
						break;
					case 2:
						if (this.Sections.Count == 0)
						{
							num = 1;
							continue;
						}
						goto IL_7B;
					}
					break;
				}
			}
			IL_6F:
			IL_7B:
			if (true)
			{
			}
			this.ViewSetup.DocumentViewType = DocumentViewType.WebLayout;
			this.XHTMLValidateOption = validationType;
			this.LastSection.PageSetup.Margins.All = 72f;
			this.LastSection.Body.InsertXHTML(html, 0);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000350E0 File Offset: 0x000340E0
		public void LoadText(string fileName)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.LoadFromFile(fileName, FileFormat.Txt);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00035124 File Offset: 0x00034124
		public void LoadText(Stream stream)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.LoadFromStream(stream, FileFormat.Txt);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00035168 File Offset: 0x00034168
		public void LoadText(string fileName, Encoding encoding)
		{
			fileName = this.ᜀ(fileName, FileFormat.Txt);
			Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			try
			{
				this.LoadText(stream, encoding);
			}
			finally
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_63;
					case 1:
						goto IL_7B;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_63;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					}
					if (stream != null)
					{
						num = 0;
						continue;
					}
					break;
					IL_63:
					if (true)
					{
					}
					((IDisposable)stream).Dispose();
					num = 1;
				}
				IL_7B:;
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00035210 File Offset: 0x00034210
		public void LoadText(Stream stream, Encoding encoding)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜀ(stream, encoding);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00035254 File Offset: 0x00034254
		public void LoadText(TextReader reader)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜀ(reader);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00035298 File Offset: 0x00034298
		private void ᜇ(string A_0)
		{
			sprỀ sprỀ = new sprỀ();
			sprᬛ sprᬛ = new sprᬛ(A_0);
			try
			{
				sprỀ.ᜀ(sprᬛ, this);
			}
			finally
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_66;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 1:
						goto IL_76;
					case 2:
						goto IL_66;
					}
					if (sprᬛ != null)
					{
						num = 2;
						continue;
					}
					break;
					IL_66:
					((IDisposable)sprᬛ).Dispose();
					num = 1;
				}
				IL_76:;
			}
			if (true)
			{
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00035338 File Offset: 0x00034338
		public void LoadFromFile(string fileName)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜀ(fileName, "");
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00035380 File Offset: 0x00034380
		public void LoadFromFile(string fileName, FileFormat fileFormat)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.LoadFromFile(fileName, fileFormat, null);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x000353C4 File Offset: 0x000343C4
		internal new void ᜀ(string A_0, FileFormat A_1, XHTMLValidationType A_2, string A_3)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.HtmlBaseUrl = A_3;
			this.LoadFromFile(A_0, A_1, A_2);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00035410 File Offset: 0x00034410
		public void LoadFromFile(string fileName, FileFormat fileFormat, XHTMLValidationType validationType)
		{
			int num = 2;
			Stream stream;
			for (;;)
			{
				switch (num)
				{
				case 0:
					fileFormat = this.\u170D(fileName);
					if (true)
					{
					}
					num = 3;
					continue;
				case 1:
					if (FileFormat.Html == fileFormat)
					{
						num = 5;
						continue;
					}
					this.LoadFromFile(fileName, fileFormat);
					num = 6;
					continue;
				case 3:
					goto IL_7E;
				case 4:
					goto IL_4C;
				case 5:
					stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
					num = 4;
					continue;
				case 6:
					return;
				}
				if (fileFormat == FileFormat.Auto)
				{
					num = 0;
					continue;
				}
				IL_7E:
				fileName = this.ᜀ(fileName, fileFormat);
				this.ᜀ(fileName, ref fileFormat);
				this.DetectedFormatType = fileFormat;
				num = 1;
			}
			IL_4C:
			try
			{
				this.ᜅ();
				this.HtmlBaseUrl = Path.GetDirectoryName(fileName).TrimEnd(new char[]
				{
					'\\'
				});
				this.ᜀ(stream, validationType);
			}
			finally
			{
				num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_126;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 1:
						goto IL_126;
					case 2:
						goto IL_136;
					}
					if (stream != null)
					{
						num = 1;
						continue;
					}
					break;
					IL_126:
					((IDisposable)stream).Dispose();
					num = 2;
				}
				IL_136:;
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00035570 File Offset: 0x00034570
		public void LoadFromFile(string fileName, FileFormat fileFormat, string password)
		{
			int a_ = 11;
			switch (0)
			{
			default:
			{
				Stream stream4;
				for (;;)
				{
					this.ᜅ();
					this.ᜀ(fileName);
					this.Password = password;
					int num = 3;
					for (;;)
					{
						FileFormat fileFormat2;
						switch (num)
						{
						case 0:
							goto IL_1F6;
						case 1:
							goto IL_1AC;
						case 2:
							fileFormat = this.\u170D(fileName);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_F8;
							default:
								if (false)
								{
								}
								num = 0;
								continue;
							}
							break;
						case 3:
							if (fileFormat == FileFormat.Auto)
							{
								num = 2;
								continue;
							}
							goto IL_1F6;
						case 4:
							goto IL_104;
						case 5:
							goto IL_112;
						case 6:
							switch (fileFormat2)
							{
							case FileFormat.Doc:
							case FileFormat.Dot:
								goto IL_DF;
							case FileFormat.Docx:
							case FileFormat.Dotx:
							case FileFormat.Docm:
							case FileFormat.Dotm:
								goto IL_131;
							case FileFormat.Docx2010:
							case FileFormat.Dotx2010:
							case FileFormat.Docm2010:
							case FileFormat.Dotm2010:
								goto IL_1EE;
							case FileFormat.Rtf:
							{
								Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
								num = 8;
								continue;
							}
							case FileFormat.Xml:
								goto IL_2D0;
							case FileFormat.Txt:
							{
								Stream stream2 = new FileStream(fileName, FileMode.Open, FileAccess.Read);
								num = 1;
								continue;
							}
							case FileFormat.Html:
								goto IL_C5;
							case FileFormat.PDF:
							case FileFormat.EPub:
							case FileFormat.XPS:
								goto IL_183;
							case FileFormat.WordML:
							{
								if (true)
								{
								}
								Stream stream3 = new FileStream(fileName, FileMode.Open, FileAccess.Read);
								goto IL_F8;
							}
							default:
								num = 7;
								continue;
							}
							break;
						case 7:
							num = 5;
							continue;
						case 8:
							try
							{
								Stream stream;
								this.\u170D(stream);
								return;
							}
							finally
							{
								num = 0;
								for (;;)
								{
									Stream stream;
									switch (num)
									{
									case 1:
										goto IL_C2;
									case 2:
										((IDisposable)stream).Dispose();
										num = 1;
										continue;
									}
									if (stream == null)
									{
										break;
									}
									num = 2;
								}
								IL_C2:;
							}
							goto IL_C5;
						case 9:
							goto IL_DA;
						}
						break;
						IL_C5:
						stream4 = new FileStream(fileName, FileMode.Open, FileAccess.Read);
						num = 9;
						continue;
						IL_F8:
						num = 4;
						continue;
						IL_1F6:
						fileName = this.ᜀ(fileName, fileFormat);
						this.\u1759 = fileName;
						this.ᜀ(fileName, ref fileFormat);
						this.DetectedFormatType = fileFormat;
						fileFormat2 = fileFormat;
						num = 6;
					}
				}
				IL_DA:
				try
				{
					this.HtmlBaseUrl = Path.GetDirectoryName(fileName);
					this.LoadFromStream(stream4, fileFormat, XHTMLValidationType.Strict);
				}
				finally
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							((IDisposable)stream4).Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_32A;
						}
						if (stream4 == null)
						{
							break;
						}
						num = 0;
					}
					IL_32A:;
				}
				return;
				IL_DF:
				this.ᜇ(fileName);
				return;
				IL_104:
				try
				{
					Stream stream3;
					this.ᜏ(stream3);
					return;
				}
				finally
				{
					int num = 2;
					for (;;)
					{
						Stream stream3;
						switch (num)
						{
						case 0:
							goto IL_180;
						case 1:
							((IDisposable)stream3).Dispose();
							num = 0;
							continue;
						}
						if (stream3 == null)
						{
							break;
						}
						num = 1;
					}
					IL_180:;
				}
				IL_112:
				goto IL_183;
				IL_131:
				this.ᜌ(fileName);
				return;
				IL_183:
				throw new NotSupportedException(ClipboardData.b("╰᭲ၴ坶ὸቺᅼ᩾ꆀ歷꾎ﮔ練붜ﶞ쒠莢횤튦\ud9a8\udbaa슬\uddae얰횲톴馶", a_));
				IL_1AC:
				try
				{
					Stream stream2;
					this.ᜋ(stream2);
					return;
				}
				finally
				{
					int num = 0;
					for (;;)
					{
						Stream stream2;
						switch (num)
						{
						case 1:
							((IDisposable)stream2).Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_2CD;
						}
						if (stream2 == null)
						{
							break;
						}
						num = 1;
					}
					IL_2CD:;
				}
				goto IL_2D0;
				IL_1EE:
				this.ᜋ(fileName);
				return;
				IL_2D0:
				this.\u1714(fileName);
				return;
			}
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000358E0 File Offset: 0x000348E0
		private new void ᜀ(string A_0, ref FileFormat A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					FileStream fileStream = new FileStream(A_0, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
					fileStream.Position = 0L;
					int num = 17;
					for (;;)
					{
						byte[] array;
						switch (num)
						{
						case 0:
							if (array[1] == 92)
							{
								num = 16;
								continue;
							}
							goto IL_10B;
						case 1:
							if (array[1] == 72)
							{
								num = 32;
								continue;
							}
							goto IL_1E1;
						case 2:
							if (array[0] == 123)
							{
								num = 26;
								continue;
							}
							goto IL_10B;
						case 3:
							if (array[0] == 80)
							{
								num = 34;
								continue;
							}
							goto IL_2C5;
						case 4:
							num = 7;
							continue;
						case 5:
							goto IL_158;
						case 6:
							num = 3;
							continue;
						case 7:
							if (array[2] == 116)
							{
								num = 9;
								continue;
							}
							goto IL_175;
						case 8:
							if (array[4] == 102)
							{
								num = 30;
								continue;
							}
							goto IL_10B;
						case 9:
							num = 18;
							continue;
						case 10:
							goto IL_1FB;
						case 11:
							if (fileStream.Read(array, 0, 5) == 5)
							{
								num = 6;
								continue;
							}
							goto IL_2C5;
						case 12:
							num = 28;
							continue;
						case 13:
							if (array[3] == 116)
							{
								num = 33;
								continue;
							}
							goto IL_10B;
						case 14:
						{
							spr\u21F4 spr_u21F = this.ᜊ(fileStream);
							num = 21;
							continue;
						}
						case 15:
							goto IL_1FB;
						case 16:
							num = 22;
							continue;
						case 17:
							if (this.ᜉ(fileStream))
							{
								num = 14;
								continue;
							}
							goto IL_28B;
						case 18:
							if (array[3] != 109)
							{
								num = 19;
								continue;
							}
							goto IL_158;
						case 19:
							goto IL_175;
						case 20:
							if (array[0] == 60)
							{
								num = 12;
								continue;
							}
							goto IL_1E1;
						case 21:
							try
							{
								for (;;)
								{
									spr\u21F4 spr_u21F;
									spr\u2547 a_ = spr_u21F.ᜀ();
									spr\u1AED spr_u1AED = new spr\u1AED();
									spr\u1AED.EncrytionType encrytionType = spr_u1AED.ᜀ(a_);
									fileStream.Position = 0L;
									num = 1;
									for (;;)
									{
										switch (num)
										{
										case 0:
											A_1 = FileFormat.Docx;
											fileStream.Close();
											num = 3;
											continue;
										case 1:
											if (encrytionType != spr\u1AED.EncrytionType.None)
											{
												num = 0;
												continue;
											}
											num = 2;
											continue;
										case 2:
											goto IL_42C;
										case 3:
											goto IL_41E;
										}
										break;
									}
								}
								IL_41E:
								return;
								IL_42C:
								goto IL_28B;
							}
							finally
							{
								num = 0;
								for (;;)
								{
									spr\u21F4 spr_u21F;
									switch (num)
									{
									case 1:
										goto IL_46C;
									case 2:
										spr_u21F.Dispose();
										num = 1;
										continue;
									}
									if (spr_u21F == null)
									{
										break;
									}
									num = 2;
								}
								IL_46C:;
							}
							goto IL_46F;
						case 22:
							if (array[2] == 114)
							{
								num = 36;
								continue;
							}
							goto IL_10B;
						case 23:
							if (true)
							{
							}
							goto IL_1FB;
						case 24:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_10B;
							default:
								if (false)
								{
								}
								fileStream.Position = 0L;
								A_1 = FileFormat.Docx;
								num = 15;
								continue;
							}
							break;
						case 25:
							if (array[3] == 77)
							{
								num = 5;
								continue;
							}
							goto IL_1E1;
						case 26:
							num = 0;
							continue;
						case 27:
							if (array[2] == 84)
							{
								num = 29;
								continue;
							}
							goto IL_1E1;
						case 28:
							if (array[1] == 104)
							{
								num = 4;
								continue;
							}
							goto IL_175;
						case 29:
							num = 25;
							continue;
						case 30:
							fileStream.Position = 0L;
							A_1 = FileFormat.Rtf;
							num = 23;
							continue;
						case 31:
							return;
						case 32:
							num = 27;
							continue;
						case 33:
							goto IL_46F;
						case 34:
							num = 35;
							continue;
						case 35:
							if (array[1] == 75)
							{
								num = 24;
								continue;
							}
							goto IL_2C5;
						case 36:
							num = 13;
							continue;
						}
						break;
						IL_10B:
						num = 20;
						continue;
						IL_158:
						fileStream.Position = 0L;
						A_1 = FileFormat.Html;
						num = 10;
						continue;
						IL_175:
						num = 1;
						continue;
						IL_1FB:
						fileStream.Close();
						num = 31;
						continue;
						try
						{
							IL_1E1:
							sprᬛ sprᬛ = new sprᬛ(A_0);
							A_1 = FileFormat.Doc;
							sprᬛ.\u171D();
						}
						catch (Exception)
						{
						}
						goto IL_1FB;
						IL_28B:
						fileStream.Position = 0L;
						array = new byte[5];
						num = 11;
						continue;
						IL_2C5:
						num = 2;
						continue;
						IL_46F:
						num = 8;
					}
				}
				return;
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00035DC8 File Offset: 0x00034DC8
		public void LoadFromFileInReadMode(string strFileName, FileFormat fileFormat)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			FileStream fileStream = new FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			try
			{
				this.\u173C = true;
				this.LoadFromStream(fileStream, fileFormat);
			}
			finally
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						((IDisposable)fileStream).Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_82;
					}
					if (fileStream == null)
					{
						break;
					}
					num = 0;
				}
				IL_82:;
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00035E6C File Offset: 0x00034E6C
		public void LoadRtf(string fileName)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜇ = true;
			this.LoadFromFile(fileName, FileFormat.Rtf);
			this.ᜇ = false;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00035EC0 File Offset: 0x00034EC0
		public void LoadRtf(Stream stream)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.LoadFromStream(stream, FileFormat.Rtf);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00035F04 File Offset: 0x00034F04
		public void LoadRtf(string fileName, Encoding encoding)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			fileName = this.ᜀ(fileName, FileFormat.Rtf);
			Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			try
			{
				this.LoadRtf(stream, encoding);
			}
			finally
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_85;
					case 1:
						((IDisposable)stream).Dispose();
						num = 0;
						continue;
					}
					if (stream == null)
					{
						break;
					}
					num = 1;
				}
				IL_85:;
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00035FAC File Offset: 0x00034FAC
		public void LoadRtf(Stream stream, Encoding encoding)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜁ(stream, encoding);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00035FF0 File Offset: 0x00034FF0
		public void LoadRtf(TextReader reader)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᜁ(reader);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00036034 File Offset: 0x00035034
		public void SaveToFile(string fileName)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.SaveToFile(fileName, FileFormat.Auto);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00036078 File Offset: 0x00035078
		private void ᜆ(string A_0)
		{
			int num = 0;
			sprᴠ sprᴠ;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_CF;
				default:
				{
					if (false)
					{
					}
					FileStream fileStream;
					switch (num)
					{
					case 1:
						try
						{
							sprច sprច = new sprច(fileStream);
							sprᴠ.ᜀ(sprច, this);
							sprច.\u1719();
							goto IL_CF;
						}
						finally
						{
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 1:
									goto IL_AA;
								case 2:
									((IDisposable)fileStream).Dispose();
									num = 1;
									continue;
								}
								if (fileStream == null)
								{
									break;
								}
								num = 2;
							}
							IL_AA:;
						}
						goto IL_AD;
					case 2:
						return;
					}
					if (true)
					{
					}
					if (this.LicenseType == LicenseType.None)
					{
						num = 2;
						break;
					}
					IL_AD:
					this.\u173A();
					sprᴠ = new sprᴠ();
					fileStream = new FileStream(A_0, FileMode.Create);
					num = 1;
					break;
				}
				}
			}
			return;
			IL_CF:
			sprᴠ.\u171E();
			sprᴠ = null;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0003616C File Offset: 0x0003516C
		public void SaveToFile(string fileName, ToPdfParameterList paramList)
		{
			int a_ = 11;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3A;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			if (paramList != null)
			{
				this.ᜀ(fileName, paramList);
				return;
			}
			IL_3A:
			throw new ArgumentNullException(ClipboardData.b("印ͲᑴնᡸᙺㅼᙾꞄꞆ麗ﾌﲐ붜ﲞ삠춢芤펦覨춪슬\uddae醰횲\ud8b4잶춸슺鎼", a_));
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000361D4 File Offset: 0x000351D4
		[Obsolete("Use SaveToFile(string fileName, ToPdfParameterList paramList)")]
		public void SaveToFile(string fileName, List<string> embeddedFontNameList)
		{
			int a_ = 7;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3A;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			if (embeddedFontNameList != null)
			{
				this.ᜀ(fileName, embeddedFontNameList);
				return;
			}
			IL_3A:
			throw new ArgumentNullException(ClipboardData.b("佬੮ᱰᅲၴ፶ᵸṺ᥼㥾즆쎎떖릘ﲜ삠캢삤펦첨\ud9aa\udeac辮튰튲\udbb4邶춸鮺\udbbc킾돀ꃄ꫆마뿊듌", a_));
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0003623C File Offset: 0x0003523C
		internal void ᜁ(string A_0, DocPicture A_1)
		{
			int num = 1;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					this.\u173B();
					num = 2;
					continue;
				case 2:
					goto IL_60;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_60;
				default:
					if (false)
					{
					}
					if (!this.IsUpdateFields)
					{
						goto IL_62;
					}
					num = 0;
					break;
				}
			}
			IL_60:
			IL_62:
			this.ᜀ(A_0, A_1);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x000362C0 File Offset: 0x000352C0
		public void SaveToFile(string fileName, FileFormat fileFormat)
		{
			int a_ = 18;
			int num = 6;
			for (;;)
			{
				FileFormat fileFormat2;
				switch (num)
				{
				case 0:
					this.\u173B();
					num = 7;
					continue;
				case 1:
					switch (fileFormat2)
					{
					case FileFormat.Doc:
						goto IL_1C6;
					case FileFormat.Dot:
						goto IL_102;
					case FileFormat.Docx:
					case FileFormat.Docx2010:
					case FileFormat.Dotx:
					case FileFormat.Dotx2010:
					case FileFormat.Docm:
					case FileFormat.Docm2010:
					case FileFormat.Dotm:
					case FileFormat.Dotm2010:
						goto IL_1E5;
					case FileFormat.Rtf:
						goto IL_128;
					case FileFormat.Xml:
						goto IL_5E;
					case FileFormat.Txt:
						goto IL_56;
					case FileFormat.Html:
						goto IL_1CE;
					case FileFormat.PDF:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_14B;
						default:
							goto IL_9F;
						}
						break;
					case FileFormat.EPub:
						goto IL_F9;
					case FileFormat.XPS:
						goto IL_130;
					case FileFormat.WordML:
						goto IL_1DD;
					case FileFormat.DocPre97:
						goto IL_D7;
					default:
						num = 3;
						continue;
					}
					break;
				case 2:
					goto IL_150;
				case 3:
					return;
				case 4:
					if (this.IsUpdateFields)
					{
						num = 0;
						continue;
					}
					goto IL_66;
				case 5:
					if (true)
					{
					}
					fileFormat = this.\u170D(fileName);
					num = 2;
					continue;
				case 7:
					goto IL_14B;
				case 8:
					return;
				case 9:
					if (fileFormat == FileFormat.Auto)
					{
						num = 5;
						continue;
					}
					goto IL_150;
				}
				if (this.LicenseType == LicenseType.None)
				{
					num = 8;
					continue;
				}
				num = 4;
				continue;
				IL_66:
				this.\u173A();
				num = 9;
				continue;
				IL_14B:
				goto IL_66;
				IL_150:
				this.SaveFormatType = fileFormat;
				fileFormat2 = fileFormat;
				num = 1;
			}
			return;
			IL_56:
			this.ᜉ(fileName);
			return;
			IL_5E:
			this.ᜊ(fileName);
			return;
			IL_9F:
			if (false)
			{
			}
			this.ᜂ(fileName);
			return;
			IL_D7:
			throw new ArgumentException(ClipboardData.b("⽷ࡹᕻ੽ꚅ캇횏﶑ﮕ聯벛\uda9d쾟송풥춧鎩鮫躭\ud9af솱钳\ud8b5ힷ캹鲻춽떿닁듃꧅뫇뻉꧋꫍ﻏ", a_), ClipboardData.b("ṷ፹ၻ᭽왿ﺉ", a_));
			IL_F9:
			this.ᜀ(fileName, null);
			return;
			IL_102:
			this.ᜅ(fileName);
			return;
			IL_128:
			this.ᜄ(fileName);
			return;
			IL_130:
			this.ᜁ(fileName);
			return;
			IL_1C6:
			this.ᜆ(fileName);
			return;
			IL_1CE:
			sprᴫ sprᴫ = new sprᴫ();
			sprᴫ.ᜂ(this, fileName);
			return;
			IL_1DD:
			this.ᜃ(fileName);
			return;
			IL_1E5:
			this.ᜈ(fileName);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000364BC File Offset: 0x000354BC
		public void SaveToFile(string fileName, FileFormat fileFormat, HttpResponse response, HttpContentType contentType)
		{
			int a_ = 6;
			int num = 11;
			string value;
			for (;;)
			{
				FileFormat fileFormat2;
				switch (num)
				{
				case 0:
					goto IL_BD;
				case 1:
					if (fileFormat == FileFormat.Auto)
					{
						num = 18;
						continue;
					}
					goto IL_1FD;
				case 2:
					goto IL_1FD;
				case 3:
					goto IL_EE;
				case 4:
					goto IL_EE;
				case 5:
					goto IL_EE;
				case 6:
					if (contentType != HttpContentType.InBrowser)
					{
						num = 16;
						continue;
					}
					num = 10;
					continue;
				case 7:
					if (this.IsUpdateFields)
					{
						num = 15;
						continue;
					}
					goto IL_BD;
				case 8:
					switch (fileFormat2)
					{
					case FileFormat.Doc:
					case FileFormat.Dot:
						value = ClipboardData.b("൫ṭoṱᵳᕵ᥷๹ᕻᅽ궁ﾇﺋ", a_);
						num = 12;
						continue;
					case FileFormat.Docx:
					case FileFormat.Dotx:
					case FileFormat.Docm:
					case FileFormat.Dotm:
						value = ClipboardData.b("൫ṭoṱᵳᕵ᥷๹ᕻᅽ궁ꒉﶍ붏ﮓﲗ뒙쎟힡즣쎥욧\udea9芫龭芯", a_);
						num = 5;
						continue;
					case FileFormat.Docx2010:
					case FileFormat.Dotx2010:
					case FileFormat.Docm2010:
					case FileFormat.Dotm2010:
						value = ClipboardData.b("൫ṭoṱᵳᕵ᥷๹ᕻᅽ궁ꒉﶍ붏ﮓﲗ뒙쎟힡즣쎥욧\udea9芫龭蒯", a_);
						num = 14;
						continue;
					case FileFormat.Rtf:
					case FileFormat.Txt:
					case FileFormat.Html:
						goto IL_EE;
					case FileFormat.Xml:
						value = ClipboardData.b("൫ṭoṱᵳᕵ᥷๹ᕻᅽ궁ﲃ", a_);
						num = 9;
						continue;
					case FileFormat.PDF:
						value = ClipboardData.b("൫ṭoṱᵳᕵ᥷๹ᕻᅽ궁", a_);
						if (true)
						{
						}
						num = 4;
						continue;
					case FileFormat.EPub:
						value = ClipboardData.b("൫ṭoṱᵳᕵ᥷๹ᕻᅽ궁ﶇꞋ憐", a_);
						num = 3;
						continue;
					default:
						num = 19;
						continue;
					}
					break;
				case 9:
					goto IL_EE;
				case 10:
					goto IL_170;
				case 12:
					goto IL_EE;
				case 13:
					goto IL_EE;
				case 14:
					goto IL_EE;
				case 15:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_CB;
					default:
						if (false)
						{
						}
						this.\u173B();
						num = 0;
						continue;
					}
					break;
				case 16:
					num = 20;
					continue;
				case 17:
					return;
				case 18:
					fileFormat = this.\u170D(fileName);
					num = 2;
					continue;
				case 19:
					num = 13;
					continue;
				case 20:
					goto IL_1C3;
				}
				if (this.LicenseType == LicenseType.None)
				{
					num = 17;
					continue;
				}
				num = 7;
				continue;
				IL_CB:
				num = 1;
				continue;
				IL_BD:
				this.\u173A();
				fileName = Path.GetFileName(fileName);
				goto IL_CB;
				IL_EE:
				num = 6;
				continue;
				IL_1FD:
				this.SaveFormatType = fileFormat;
				response.Clear();
				value = string.Empty;
				fileFormat2 = fileFormat;
				num = 8;
			}
			return;
			IL_170:
			string text = ClipboardData.b("իmᱯ᭱ᩳ፵", a_);
			goto IL_2C3;
			IL_1C3:
			text = ClipboardData.b("൫ᩭѯ፱ᝳṵᕷόቻ੽", a_);
			IL_2C3:
			string arg = text;
			response.AddHeader(ClipboardData.b("⽫ŭṯٱᅳᡵ౷坹⡻ݽ", a_), value);
			response.AddHeader(ClipboardData.b("⽫ŭṯٱᅳᡵ౷坹㡻᝽ﺉﺏ", a_), string.Format(ClipboardData.b("ᝫ幭൯䥱ታήᑷόቻώ릃ﶅ릇랋", a_), arg, fileName));
			this.SaveToFile(response.OutputStream, fileFormat);
			response.End();
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000367E0 File Offset: 0x000357E0
		private void ᜅ(string A_0)
		{
			int num = 2;
			sprᴠ sprᴠ;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_C6;
				default:
				{
					if (false)
					{
					}
					FileStream fileStream;
					switch (num)
					{
					case 0:
						try
						{
							sprច sprច = new sprច(fileStream);
							sprច.ᜂ(true);
							sprᴠ.ᜀ(sprច, this);
							goto IL_C6;
						}
						finally
						{
							num = 1;
							for (;;)
							{
								if (true)
								{
								}
								switch (num)
								{
								case 0:
									((IDisposable)fileStream).Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_A1;
								}
								if (fileStream == null)
								{
									break;
								}
								num = 0;
							}
							IL_A1:;
						}
						goto IL_A4;
					case 1:
						return;
					}
					if (this.LicenseType == LicenseType.None)
					{
						num = 1;
						break;
					}
					IL_A4:
					this.\u173A();
					sprᴠ = new sprᴠ();
					fileStream = new FileStream(A_0, FileMode.Create);
					num = 0;
					break;
				}
				}
			}
			return;
			IL_C6:
			sprᴠ = null;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000368D0 File Offset: 0x000358D0
		private void ᜄ(string A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (false)
			{
			}
			if (this.LicenseType != LicenseType.None)
			{
				if (true)
				{
				}
				this.\u173A();
				spr\u21C0 spr_u21C = new spr\u21C0();
				spr_u21C.ᜀ(A_0, this);
				return;
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0003692C File Offset: 0x0003592C
		private void ᜃ(Stream A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			if (this.LicenseType != LicenseType.None)
			{
				this.\u173A();
				StreamWriter a_ = new StreamWriter(A_0, Encoding.ASCII);
				spr\u21C0 spr_u21C = new spr\u21C0();
				spr_u21C.ᜀ(a_, this);
				return;
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00036994 File Offset: 0x00035994
		private void ᜂ(Stream A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			if (this.LicenseType != LicenseType.None)
			{
				this.\u173A();
				spr\u22DE spr_u22DE = new spr\u22DE();
				spr_u22DE.ᜀ(this, A_0);
				return;
			}
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000369F0 File Offset: 0x000359F0
		private void ᜃ(string A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			if (this.LicenseType != LicenseType.None)
			{
				this.\u173A();
				spr\u22DE spr_u22DE = new spr\u22DE();
				spr_u22DE.ᜀ(this, A_0);
				return;
			}
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00036A4C File Offset: 0x00035A4C
		private void ᜂ(string A_0)
		{
			if (true)
			{
			}
			int num = 0;
			for (;;)
			{
				spr\u21E1 spr_u21E;
				switch (num)
				{
				case 1:
					return;
				case 2:
					try
					{
						spr_u21E.ᜀ(this.JPEGQuality);
						PdfNewDocument pdfNewDocument = spr_u21E.ᜀ(this);
						try
						{
							this.ᜀ(this.\u176D, pdfNewDocument);
							pdfNewDocument.Save(A_0);
							pdfNewDocument.Dispose();
						}
						finally
						{
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 1:
									goto IL_AF;
								case 2:
									((IDisposable)pdfNewDocument).Dispose();
									num = 1;
									continue;
								}
								if (pdfNewDocument == null)
								{
									break;
								}
								num = 2;
							}
							IL_AF:;
						}
						spr_u21E.Dispose();
						return;
					}
					finally
					{
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									continue;
								default:
									if (false)
									{
									}
									((IDisposable)spr_u21E).Dispose();
									num = 2;
									continue;
								}
								break;
							case 2:
								goto IL_10D;
							}
							if (spr_u21E == null)
							{
								break;
							}
							num = 1;
						}
						IL_10D:;
					}
					goto IL_110;
				}
				if (this.LicenseType == LicenseType.None)
				{
					num = 1;
					continue;
				}
				IL_110:
				this.\u173A();
				spr_u21E = new spr\u21E1();
				num = 2;
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00036BA4 File Offset: 0x00035BA4
		private void ᜁ(string A_0)
		{
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				FileStream fileStream;
				switch (num)
				{
				case 0:
					try
					{
						spr\u2079 spr_u = new spr\u2079();
						try
						{
							spr_u.ᜀ(this.JPEGQuality);
							spr_u.ᜀ(this, fileStream);
							spr_u.Dispose();
						}
						finally
						{
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_A1;
								case 2:
									((IDisposable)spr_u).Dispose();
									num = 0;
									continue;
								}
								if (spr_u == null)
								{
									break;
								}
								num = 2;
							}
							IL_A1:;
						}
						return;
					}
					finally
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_F9;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									continue;
								default:
									if (false)
									{
									}
									((IDisposable)fileStream).Dispose();
									num = 0;
									continue;
								}
								break;
							}
							if (fileStream == null)
							{
								break;
							}
							num = 2;
						}
						IL_F9:;
					}
					goto IL_FC;
				case 2:
					return;
				}
				if (this.LicenseType == LicenseType.None)
				{
					num = 2;
					continue;
				}
				IL_FC:
				this.\u173A();
				fileStream = new FileStream(A_0, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
				num = 0;
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00036CEC File Offset: 0x00035CEC
		private new void ᜀ(string A_0, ToPdfParameterList A_1)
		{
			int num = 0;
			for (;;)
			{
				spr\u21E1 spr_u21E;
				switch (num)
				{
				case 1:
					try
					{
						spr_u21E.ᜀ(this.JPEGQuality);
						PdfNewDocument pdfNewDocument = spr_u21E.ᜀ(this, A_1);
						try
						{
							this.ᜀ(this.\u176D, pdfNewDocument);
							pdfNewDocument.Save(A_0);
						}
						finally
						{
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 1:
									goto IL_A2;
								case 2:
									((IDisposable)pdfNewDocument).Dispose();
									num = 1;
									continue;
								}
								if (pdfNewDocument == null)
								{
									break;
								}
								num = 2;
							}
							IL_A2:;
						}
						return;
					}
					finally
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									continue;
								default:
									if (false)
									{
									}
									((IDisposable)spr_u21E).Dispose();
									num = 2;
									continue;
								}
								break;
							case 2:
								goto IL_FA;
							}
							if (spr_u21E == null)
							{
								break;
							}
							num = 0;
						}
						IL_FA:;
					}
					goto IL_FD;
				case 2:
					goto IL_39;
				}
				if (this.LicenseType == LicenseType.None)
				{
					num = 2;
					continue;
				}
				IL_FD:
				this.\u173A();
				spr_u21E = new spr\u21E1();
				num = 1;
			}
			IL_39:
			if (true)
			{
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00036E38 File Offset: 0x00035E38
		private new void ᜀ(string A_0, List<string> A_1)
		{
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				spr\u21E1 spr_u21E;
				switch (num)
				{
				case 0:
					try
					{
						spr_u21E.ᜀ(this.JPEGQuality);
						PdfNewDocument pdfNewDocument = spr_u21E.ᜀ(this, A_1);
						try
						{
							this.ᜀ(this.\u176D, pdfNewDocument);
							pdfNewDocument.Save(A_0);
						}
						finally
						{
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 1:
									goto IL_AA;
								case 2:
									((IDisposable)pdfNewDocument).Dispose();
									num = 1;
									continue;
								}
								if (pdfNewDocument == null)
								{
									break;
								}
								num = 2;
							}
							IL_AA:;
						}
						return;
					}
					finally
					{
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									continue;
								default:
									if (false)
									{
									}
									((IDisposable)spr_u21E).Dispose();
									num = 2;
									continue;
								}
								break;
							case 2:
								goto IL_102;
							}
							if (spr_u21E == null)
							{
								break;
							}
							num = 1;
						}
						IL_102:;
					}
					goto IL_105;
				case 2:
					return;
				}
				if (this.LicenseType == LicenseType.None)
				{
					num = 2;
					continue;
				}
				IL_105:
				this.\u173A();
				spr_u21E = new spr\u21E1();
				num = 0;
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00036F84 File Offset: 0x00035F84
		private void ᜁ(Stream A_0)
		{
			int num = 2;
			for (;;)
			{
				spr\u2079 spr_u;
				switch (num)
				{
				case 0:
					return;
				case 1:
					try
					{
						spr_u.ᜀ(this.JPEGQuality);
						spr_u.ᜀ(this, A_0);
						spr_u.Dispose();
						return;
					}
					finally
					{
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									continue;
								default:
									if (false)
									{
									}
									((IDisposable)spr_u).Dispose();
									num = 2;
									continue;
								}
								break;
							case 2:
								goto IL_B5;
							}
							if (spr_u == null)
							{
								break;
							}
							num = 1;
						}
						IL_B5:;
					}
					goto IL_B8;
				}
				if (true)
				{
				}
				if (this.LicenseType == LicenseType.None)
				{
					num = 0;
					continue;
				}
				IL_B8:
				this.\u173A();
				spr_u = new spr\u2079();
				num = 1;
			}
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00037078 File Offset: 0x00036078
		private new void ᜀ(Stream A_0)
		{
			int num = 2;
			for (;;)
			{
				spr\u21E1 spr_u21E;
				switch (num)
				{
				case 0:
					return;
				case 1:
					try
					{
						spr_u21E.ᜀ(this.JPEGQuality);
						PdfNewDocument pdfNewDocument = spr_u21E.ᜀ(this);
						try
						{
							this.ᜀ(this.\u176D, pdfNewDocument);
							pdfNewDocument.Save(A_0);
							pdfNewDocument.Dispose();
						}
						finally
						{
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_AF;
								case 2:
									((IDisposable)pdfNewDocument).Dispose();
									num = 0;
									continue;
								}
								if (pdfNewDocument == null)
								{
									break;
								}
								num = 2;
							}
							IL_AF:;
						}
						spr_u21E.Dispose();
						return;
					}
					finally
					{
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									continue;
								default:
									if (false)
									{
									}
									((IDisposable)spr_u21E).Dispose();
									num = 2;
									continue;
								}
								break;
							case 2:
								goto IL_10D;
							}
							if (spr_u21E == null)
							{
								break;
							}
							num = 1;
						}
						IL_10D:;
					}
					goto IL_110;
				}
				if (true)
				{
				}
				if (this.LicenseType == LicenseType.None)
				{
					num = 0;
					continue;
				}
				IL_110:
				this.\u173A();
				spr_u21E = new spr\u21E1();
				num = 1;
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000371D0 File Offset: 0x000361D0
		private new void ᜀ(Stream A_0, ToPdfParameterList A_1)
		{
			int num = 0;
			for (;;)
			{
				spr\u21E1 spr_u21E;
				switch (num)
				{
				case 1:
					return;
				case 2:
					try
					{
						spr_u21E.ᜀ(this.JPEGQuality);
						PdfNewDocument pdfNewDocument = spr_u21E.ᜀ(this, A_1);
						try
						{
							this.ᜀ(this.\u176D, pdfNewDocument);
							pdfNewDocument.Save(A_0);
							pdfNewDocument.Dispose();
						}
						finally
						{
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_A8;
								case 2:
									((IDisposable)pdfNewDocument).Dispose();
									num = 0;
									continue;
								}
								if (pdfNewDocument == null)
								{
									break;
								}
								num = 2;
							}
							IL_A8:;
						}
						spr_u21E.Dispose();
						goto IL_126;
					}
					finally
					{
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									continue;
								default:
									if (false)
									{
									}
									((IDisposable)spr_u21E).Dispose();
									num = 2;
									continue;
								}
								break;
							case 2:
								goto IL_106;
							}
							if (spr_u21E == null)
							{
								break;
							}
							num = 1;
						}
						IL_106:;
					}
					goto IL_109;
				}
				if (this.LicenseType == LicenseType.None)
				{
					num = 1;
					continue;
				}
				IL_109:
				this.\u173A();
				spr_u21E = new spr\u21E1();
				num = 2;
			}
			return;
			IL_126:
			if (true)
			{
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00037328 File Offset: 0x00036328
		public void SaveToStream(Stream stream, ToPdfParameterList paramList)
		{
			while (this.LicenseType != LicenseType.None)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					this.\u173A();
					this.ᜀ(stream, paramList);
					return;
				}
			}
			if (true)
			{
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0003737C File Offset: 0x0003637C
		public void LoadFromStream(Stream stream, FileFormat fileFormat, XHTMLValidationType validationType)
		{
			for (;;)
			{
				this.ᜀ(stream, ref fileFormat);
				this.DetectedFormatType = fileFormat;
				if (FileFormat.Html == fileFormat)
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_4E;
				}
			}
			if (true)
			{
			}
			this.ᜅ();
			this.ᜀ(stream, validationType);
			return;
			IL_4E:
			if (false)
			{
			}
			this.LoadFromStream(stream, fileFormat);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000373E8 File Offset: 0x000363E8
		public void LoadFromStream(Stream stream, FileFormat fileFormat)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.LoadFromStream(stream, fileFormat, null);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0003742C File Offset: 0x0003642C
		public void LoadFromStream(Stream stream, FileFormat fileFormat, string password)
		{
			int a_ = 2;
			for (;;)
			{
				this.ᜅ();
				this.Password = password;
				sprỀ sprỀ = new sprỀ();
				int num = 10;
				for (;;)
				{
					FileFormat fileFormat2;
					switch (num)
					{
					case 0:
						goto IL_B0;
					case 1:
						goto IL_89;
					case 2:
						goto IL_CC;
					case 3:
						switch (fileFormat2)
						{
						case FileFormat.Doc:
						case FileFormat.Dot:
						{
							sprỀ = new sprỀ();
							sprᬛ sprᬛ = new sprᬛ(stream);
							num = 11;
							continue;
						}
						case FileFormat.Docx:
						case FileFormat.Docx2010:
						case FileFormat.Dotx2010:
						case FileFormat.Docm2010:
						case FileFormat.Dotm2010:
							this.ᜇ(stream);
							num = 4;
							continue;
						case FileFormat.Dotx:
						case FileFormat.Docm:
						case FileFormat.Dotm:
							this.ᜈ(stream);
							num = 7;
							continue;
						case FileFormat.Rtf:
							this.\u170D(stream);
							num = 1;
							continue;
						case FileFormat.Xml:
							this.ᜎ(stream);
							num = 9;
							continue;
						case FileFormat.Txt:
							this.ᜋ(stream);
							num = 12;
							continue;
						case FileFormat.Html:
							this.LoadFromStream(stream, fileFormat, XHTMLValidationType.Transitional);
							num = 13;
							continue;
						case FileFormat.PDF:
						case FileFormat.EPub:
						case FileFormat.XPS:
							goto IL_161;
						case FileFormat.WordML:
							this.ᜏ(stream);
							num = 14;
							continue;
						default:
							num = 6;
							continue;
						}
						break;
					case 4:
						return;
					case 5:
						goto IL_237;
					case 6:
						num = 0;
						continue;
					case 7:
						goto IL_C7;
					case 8:
						return;
					case 9:
						return;
					case 10:
						if (fileFormat == FileFormat.Auto)
						{
							num = 5;
							continue;
						}
						goto IL_CC;
					case 11:
						try
						{
							if (true)
							{
							}
							sprᬛ sprᬛ;
							sprỀ.ᜀ(sprᬛ, this);
							goto IL_175;
						}
						finally
						{
							num = 2;
							for (;;)
							{
								sprᬛ sprᬛ;
								switch (num)
								{
								case 0:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										continue;
									default:
										if (false)
										{
										}
										((IDisposable)sprᬛ).Dispose();
										num = 1;
										continue;
									}
									break;
								case 1:
									goto IL_234;
								}
								if (sprᬛ == null)
								{
									break;
								}
								num = 0;
							}
							IL_234:;
						}
						goto IL_237;
						IL_175:
						sprỀ = null;
						num = 8;
						continue;
					case 12:
						goto IL_A0;
					case 13:
						goto IL_15C;
					case 14:
						return;
					}
					break;
					IL_CC:
					this.DetectedFormatType = fileFormat;
					fileFormat2 = fileFormat;
					num = 3;
					continue;
					IL_237:
					this.ᜀ(stream, ref fileFormat);
					num = 2;
				}
			}
			IL_89:
			IL_A0:
			return;
			IL_B0:
			goto IL_161;
			IL_C7:
			IL_15C:
			return;
			IL_161:
			throw new NotSupportedException(ClipboardData.b("㱧ɩ५乭ᙯ᭱ᡳ፵塷ᱹ፻౽ꚅﾏ뒓ﶗ몙킟튡쮣풥\udca7쾩좫肭", a_));
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000376B0 File Offset: 0x000366B0
		private new void ᜀ(Stream A_0, ref FileFormat A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					byte[] array;
					switch (num)
					{
					case 0:
						if (array[0] == 123)
						{
							num = 11;
							continue;
						}
						goto IL_1C2;
					case 1:
						goto IL_1DC;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A2;
						default:
							if (false)
							{
							}
							if (A_0 is FileStream)
							{
								num = 26;
								continue;
							}
							A_1 = FileFormat.Doc;
							num = 17;
							continue;
						}
						break;
					case 3:
					{
						spr\u21F4 spr_u21F = this.ᜊ(A_0);
						num = 10;
						continue;
					}
					case 4:
						if (A_0.Read(array, 0, 5) == 5)
						{
							num = 25;
							continue;
						}
						goto IL_3C0;
					case 6:
						if (array[1] == 92)
						{
							num = 18;
							continue;
						}
						goto IL_1C2;
					case 7:
						num = 8;
						continue;
					case 8:
						if (array[1] == 75)
						{
							num = 23;
							continue;
						}
						goto IL_3C0;
					case 9:
						if (array[4] == 102)
						{
							num = 13;
							continue;
						}
						goto IL_1C2;
					case 10:
						try
						{
							for (;;)
							{
								spr\u21F4 spr_u21F;
								spr\u2547 a_ = spr_u21F.ᜀ();
								spr\u1AED spr_u1AED = new spr\u1AED();
								spr\u1AED.EncrytionType encrytionType = spr_u1AED.ᜀ(a_);
								A_0.Position = 0L;
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 0:
										if (encrytionType != spr\u1AED.EncrytionType.None)
										{
											num = 1;
											continue;
										}
										num = 2;
										continue;
									case 1:
										A_1 = FileFormat.Docx;
										A_0.Close();
										num = 3;
										continue;
									case 2:
										goto IL_2E4;
									case 3:
										goto IL_2D6;
									}
									break;
								}
							}
							IL_2D6:
							return;
							IL_2E4:
							goto IL_1F5;
						}
						finally
						{
							num = 0;
							for (;;)
							{
								spr\u21F4 spr_u21F;
								switch (num)
								{
								case 1:
									spr_u21F.Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_324;
								}
								if (spr_u21F == null)
								{
									break;
								}
								num = 1;
							}
							IL_324:;
						}
						goto IL_327;
					case 11:
						num = 6;
						continue;
					case 12:
						goto IL_EB;
					case 13:
						A_0.Position = 0L;
						A_1 = FileFormat.Rtf;
						num = 1;
						continue;
					case 14:
						num = 19;
						continue;
					case 15:
						num = 9;
						continue;
					case 16:
						goto IL_A2;
					case 17:
						goto IL_EB;
					case 18:
						num = 22;
						continue;
					case 19:
						if (array[3] == 116)
						{
							num = 15;
							continue;
						}
						goto IL_1C2;
					case 20:
						if (this.ᜉ(A_0))
						{
							num = 3;
							continue;
						}
						goto IL_1F5;
					case 21:
						if (array[0] == 80)
						{
							num = 7;
							continue;
						}
						goto IL_3C0;
					case 22:
						if (array[2] == 114)
						{
							num = 14;
							continue;
						}
						goto IL_1C2;
					case 23:
						A_0.Position = 0L;
						A_1 = FileFormat.Docx;
						num = 27;
						continue;
					case 24:
						return;
					case 25:
						num = 21;
						continue;
					case 26:
						goto IL_327;
					case 27:
						goto IL_1DC;
					}
					if (A_1 == FileFormat.Auto)
					{
						num = 16;
						continue;
					}
					goto IL_EB;
					IL_A2:
					num = 2;
					continue;
					IL_EB:
					A_0.Position = 0L;
					if (true)
					{
					}
					num = 20;
					continue;
					IL_1DC:
					A_0.Position = 0L;
					num = 24;
					continue;
					try
					{
						IL_1C2:
						sprᬛ sprᬛ = new sprᬛ(A_0);
						A_1 = FileFormat.Doc;
						sprᬛ.\u171D();
					}
					catch (Exception)
					{
					}
					goto IL_1DC;
					IL_1F5:
					A_0.Position = 0L;
					array = new byte[5];
					num = 4;
					continue;
					IL_327:
					A_1 = this.\u170D((A_0 as FileStream).Name);
					num = 12;
					continue;
					IL_3C0:
					num = 0;
				}
				return;
			}
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00037AC0 File Offset: 0x00036AC0
		public void SaveToStream(Stream stream, FileFormat fileFormat)
		{
			if (this.LicenseType == LicenseType.None)
			{
				for (;;)
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_28;
					}
				}
				IL_28:
				if (false)
				{
				}
				return;
			}
			this.\u173A();
			this.SaveToFile(stream, fileFormat);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00037B14 File Offset: 0x00036B14
		public void SaveToFile(Stream stream, FileFormat fileFormat)
		{
			int a_ = 15;
			switch (0)
			{
			default:
			{
				int num = 3;
				sprᴠ sprᴠ;
				for (;;)
				{
					sprច sprច;
					switch (num)
					{
					case 0:
						goto IL_A9;
					case 1:
						goto IL_2B8;
					case 2:
						goto IL_EC;
					case 4:
						goto IL_210;
					case 5:
						goto IL_2CD;
					case 6:
						sprច.ᜂ(true);
						num = 16;
						continue;
					case 7:
						goto IL_1FF;
					case 8:
						goto IL_1E7;
					case 9:
						goto IL_2F8;
					case 10:
						goto IL_1C5;
					case 11:
						goto IL_228;
					case 12:
						this.\u173B();
						num = 2;
						continue;
					case 13:
						if (fileFormat == FileFormat.Dot)
						{
							num = 6;
							continue;
						}
						goto IL_2A4;
					case 14:
						return;
					case 15:
						if (true)
						{
						}
						num = 4;
						continue;
					case 16:
						goto IL_2A4;
					case 17:
						goto IL_2E2;
					case 18:
						goto IL_196;
					case 19:
						if (this.IsUpdateFields)
						{
							num = 12;
							continue;
						}
						goto IL_EC;
					case 20:
						switch (fileFormat)
						{
						case FileFormat.Doc:
						case FileFormat.Dot:
							sprច = new sprច(stream);
							num = 13;
							continue;
						case FileFormat.Docx:
						case FileFormat.Docx2010:
						case FileFormat.Dotx:
						case FileFormat.Dotx2010:
						case FileFormat.Docm:
						case FileFormat.Docm2010:
						case FileFormat.Dotm:
						case FileFormat.Dotm2010:
							this.ᜄ(stream);
							num = 8;
							continue;
						case FileFormat.Rtf:
							this.ᜃ(stream);
							num = 10;
							continue;
						case FileFormat.Xml:
							this.ᜆ(stream);
							num = 5;
							continue;
						case FileFormat.Txt:
							this.ᜅ(stream);
							num = 17;
							continue;
						case FileFormat.Html:
						{
							sprᴫ sprᴫ = new sprᴫ();
							sprᴫ.ᜀ(this, stream);
							num = 18;
							continue;
						}
						case FileFormat.PDF:
							this.ᜀ(stream);
							num = 11;
							continue;
						case FileFormat.EPub:
							this.ᜀ(stream, null);
							num = 9;
							continue;
						case FileFormat.XPS:
							this.ᜁ(stream);
							num = 7;
							continue;
						case FileFormat.WordML:
							this.ᜂ(stream);
							num = 0;
							continue;
						case FileFormat.DocPre97:
							goto IL_AE;
						case FileFormat.Auto:
							goto IL_22D;
						default:
							num = 15;
							continue;
						}
						break;
					}
					if (this.LicenseType == LicenseType.None)
					{
						num = 14;
						continue;
					}
					num = 19;
					continue;
					IL_EC:
					this.\u173A();
					sprᴠ = new sprᴠ();
					this.SaveFormatType = fileFormat;
					num = 20;
					continue;
					IL_2A4:
					sprᴠ.ᜀ(sprច, this);
					num = 1;
				}
				return;
				IL_A9:
				goto IL_2FA;
				IL_AE:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_2CD:
					break;
				default:
					if (false)
					{
					}
					throw new ArgumentException(ClipboardData.b("≴նၸེᑼᅾꎂ쎄쮌ﺒ릘\udf9aﲞ톢삤麦麨讪쒬\udcae醰\uddb2\udab4쎶馸좺좼쾾뇀곂럄돆곈꿊", a_), ClipboardData.b("፴ṶᕸṺ㭼ၾ", a_));
				}
				IL_196:
				IL_1C5:
				IL_1E7:
				IL_1FF:
				IL_210:
				IL_228:
				goto IL_2FA;
				IL_22D:
				throw new Exception(ClipboardData.b("㙴ᙶ᝸孺፼ၾꎂꮊ搜ﲒ떔ﺞ햠莢톤\udea6\ud9a8캪趬\udbae\udeb0鎲운횶쾸\udeba鶼\ud9beꣀ꿂ꃄ", a_));
				IL_2B8:
				IL_2E2:
				IL_2F8:
				IL_2FA:
				sprᴠ = null;
				return;
			}
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00037E20 File Offset: 0x00036E20
		public void Close()
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᝅ = true;
			this.ᜊ();
			GC.WaitForPendingFinalizers();
			this.m_doc = this;
			this.ᜅ();
			this.ᝅ = false;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00037E84 File Offset: 0x00036E84
		private void ᜊ()
		{
			for (;;)
			{
				this.ᜊ = null;
				this.ᜋ = null;
				this.ᝡ = null;
				this.ᜉ();
				this.ᜈ();
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.\u1758 != null)
						{
							num = 12;
							continue;
						}
						goto IL_177;
					case 1:
						goto IL_19D;
					case 2:
						goto IL_295;
					case 3:
						if (this.ᜏ != null)
						{
							num = 14;
							continue;
						}
						goto IL_94;
					case 4:
						if (this.ᜬ != null)
						{
							num = 11;
							continue;
						}
						goto IL_2BB;
					case 5:
						if (this.ᜫ != null)
						{
							num = 13;
							continue;
						}
						goto IL_19D;
					case 6:
						goto IL_177;
					case 7:
						goto IL_21B;
					case 8:
						if (this.\u1719 != null)
						{
							num = 16;
							continue;
						}
						goto IL_295;
					case 9:
						goto IL_151;
					case 10:
						if (this.\u170D != null)
						{
							num = 17;
							continue;
						}
						goto IL_151;
					case 11:
						this.ᜬ.Close();
						this.ᜬ = null;
						num = 7;
						continue;
					case 12:
						this.\u1758.ᜀ();
						this.\u1758 = null;
						num = 6;
						continue;
					case 13:
						this.ᜫ.Close();
						this.ᜫ = null;
						num = 1;
						continue;
					case 14:
						if (true)
						{
						}
						this.ᜏ.Clear();
						this.ᜏ = null;
						num = 15;
						continue;
					case 15:
						goto IL_94;
					case 16:
						this.\u1719.ᜌ();
						this.\u1719 = null;
						num = 2;
						continue;
					case 17:
						this.\u170D.Clear();
						this.\u170D = null;
						goto IL_232;
					}
					break;
					IL_94:
					this.ᜑ = null;
					this.\u1712 = null;
					this.\u1713 = null;
					this.\u1714 = null;
					this.\u1715 = null;
					this.\u1718 = null;
					this.\u171A = null;
					this.\u171B = null;
					this.\u171C = null;
					this.\u171D = null;
					this.\u171E = null;
					this.\u171F = null;
					this.ᜠ = 1;
					this.ᜡ = null;
					this.ᜢ = null;
					this.ᜣ = null;
					this.ᜥ = false;
					this.ᜦ = null;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_232:
						num = 9;
						continue;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					IL_151:
					num = 3;
					continue;
					IL_177:
					num = 8;
					continue;
					IL_19D:
					num = 4;
					continue;
					IL_295:
					num = 10;
				}
			}
			IL_21B:
			IL_2BB:
			this.ᝊ = null;
			this.ᝋ = null;
			this.ᜨ = XHTMLValidationType.Transitional;
			this.ᜩ = null;
			this.ᜭ = null;
			this.\u1737 = null;
			this.\u1739 = null;
			this.\u173B = null;
			this.ᜯ = true;
			this.ᜰ = null;
			this.ᜦ = null;
			this.\u1733 = null;
			this.\u1734 = null;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000381A8 File Offset: 0x000371A8
		private void ᜉ()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2 = 0;
					num = 5;
					continue;
				}
				case 2:
					if (this.m_sections.Count > 0)
					{
						num = 0;
						continue;
					}
					goto IL_F3;
				case 3:
					goto IL_88;
				case 4:
					goto IL_AE;
				case 5:
					goto IL_AE;
				case 6:
					goto IL_F1;
				case 7:
				{
					int num2;
					if (num2 >= this.m_sections.Count)
					{
						num = 6;
						continue;
					}
					if (true)
					{
					}
					Section section = this.m_sections[num2];
					section.ᜁ();
					num2++;
					num = 4;
					continue;
				}
				}
				if (this.m_sections != null)
				{
					num = 3;
					continue;
				}
				break;
				IL_88:
				num = 2;
				continue;
				IL_AE:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_88;
				default:
					if (false)
					{
					}
					num = 7;
					break;
				}
			}
			IL_F1:
			IL_F3:
			this.m_sections.Clear();
			this.m_sections = null;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000382BC File Offset: 0x000372BC
		private void ᜈ()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int count = this.m_styles.Count;
					int num = 0;
					int num2 = 8;
					for (;;)
					{
						int num3;
						switch (num2)
						{
						case 0:
							goto IL_24E;
						case 1:
							goto IL_132;
						case 2:
							goto IL_178;
						case 3:
						{
							if (num >= count)
							{
								num2 = 6;
								continue;
							}
							Style style = this.m_styles[num] as Style;
							style.Close();
							num++;
							num2 = 17;
							continue;
						}
						case 4:
							return;
						case 5:
							count = this.ᜌ.Count;
							num3 = 0;
							num2 = 7;
							continue;
						case 6:
							this.m_styles.InnerList.Clear();
							this.m_styles = null;
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_13E;
							}
							if (false)
							{
							}
							num2 = 11;
							continue;
						case 7:
							goto IL_132;
						case 8:
							goto IL_154;
						case 9:
						{
							count = this.m_listStyles.Count;
							int num4 = 0;
							num2 = 13;
							continue;
						}
						case 10:
							if (this.ᜌ != null)
							{
								num2 = 5;
								continue;
							}
							return;
						case 11:
							if (this.m_listStyles != null)
							{
								num2 = 9;
								continue;
							}
							goto IL_178;
						case 12:
							goto IL_13E;
						case 13:
							goto IL_24E;
						case 14:
							this.m_listStyles.InnerList.Clear();
							this.m_listStyles = null;
							num2 = 2;
							continue;
						case 15:
						{
							int num4;
							if (num4 >= count)
							{
								num2 = 14;
								continue;
							}
							ListStyle listStyle = this.m_listStyles[num4];
							listStyle.ᜅ();
							num4++;
							num2 = 0;
							continue;
						}
						case 16:
							this.ᜌ.InnerList.Clear();
							this.ᜌ = null;
							num2 = 4;
							continue;
						case 17:
							goto IL_154;
						}
						break;
						IL_132:
						num2 = 12;
						continue;
						IL_13E:
						if (num3 >= count)
						{
							num2 = 16;
							continue;
						}
						spr\u177D spr_u177D = this.ᜌ.ᜀ(num3);
						spr_u177D.ᜅ();
						num3++;
						num2 = 1;
						continue;
						IL_154:
						num2 = 3;
						continue;
						IL_178:
						num2 = 10;
						continue;
						IL_24E:
						num2 = 15;
					}
				}
				return;
			}
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0003855C File Offset: 0x0003755C
		public Image[] SaveToImages(ImageType type)
		{
			if (this.LicenseType == LicenseType.None)
			{
				for (;;)
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_28;
					}
				}
				IL_28:
				if (false)
				{
				}
				return null;
			}
			this.\u173A();
			spr\u24D6 spr_u24D = new spr\u24D6();
			return spr_u24D.ᜀ(this, type);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x000385B8 File Offset: 0x000375B8
		public Stream SaveToImages(int pageIndex, System.Drawing.Imaging.ImageFormat imageFormat)
		{
			if (this.LicenseType == LicenseType.None)
			{
				for (;;)
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_28;
					}
				}
				IL_28:
				if (false)
				{
				}
				return null;
			}
			this.\u173A();
			spr\u24D6 spr_u24D = new spr\u24D6();
			return spr_u24D.ᜀ(pageIndex, this, imageFormat);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00038614 File Offset: 0x00037614
		public Image SaveToImages(int pageIndex, ImageType type)
		{
			int num = 3;
			Image[] array;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_38;
				case 1:
					if (array == null)
					{
						goto IL_B5;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_42;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 2:
					goto IL_50;
				case 4:
					goto IL_42;
				case 5:
					num = 4;
					continue;
				}
				if (this.LicenseType == LicenseType.None)
				{
					num = 0;
					continue;
				}
				this.\u173A();
				spr\u24D6 spr_u24D = new spr\u24D6();
				array = spr_u24D.ᜀ(pageIndex, 1, this, type);
				num = 1;
				continue;
				IL_42:
				if (array.Length <= pageIndex)
				{
					goto IL_B5;
				}
				num = 2;
			}
			IL_38:
			return null;
			IL_50:
			return array[pageIndex];
			IL_B5:
			return null;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x000386D8 File Offset: 0x000376D8
		public Image[] SaveToImages(int pageIndex, int pageCount, ImageType type)
		{
			switch (0)
			{
			default:
			{
				int num = 6;
				List<Image> list;
				for (;;)
				{
					Image[] array;
					int num2;
					switch (num)
					{
					case 0:
						goto IL_C6;
					case 1:
						if (array[num2] != null)
						{
							num = 3;
							continue;
						}
						goto IL_62;
					case 2:
						goto IL_C6;
					case 3:
						list.Add(array[num2]);
						num = 9;
						continue;
					case 4:
						goto IL_116;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_116;
						default:
							goto IL_10C;
						}
						break;
					case 7:
						if (list.Count < pageCount)
						{
							num = 4;
							continue;
						}
						goto IL_14D;
					case 8:
						goto IL_5D;
					case 9:
						goto IL_62;
					case 10:
						if (true)
						{
						}
						if (num2 >= array.Length)
						{
							num = 5;
							continue;
						}
						num = 1;
						continue;
					}
					if (this.LicenseType == LicenseType.None)
					{
						num = 8;
						continue;
					}
					this.\u173A();
					spr\u24D6 spr_u24D = new spr\u24D6();
					array = spr_u24D.ᜀ(pageIndex, pageCount, this, type);
					list = new List<Image>();
					num2 = 0;
					num = 0;
					continue;
					IL_62:
					num = 7;
					continue;
					IL_C6:
					num = 10;
					continue;
					IL_116:
					num2++;
					num = 2;
				}
				IL_5D:
				return null;
				IL_10C:
				if (false)
				{
				}
				IL_14D:
				return list.ToArray();
			}
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00038838 File Offset: 0x00037838
		internal new void ᜀ(ImageType A_0, bool A_1)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			spr\u24D6 spr_u24D = new spr\u24D6();
			spr_u24D.ᜀ(this, A_0, A_1);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00038884 File Offset: 0x00037884
		public TextSelection FindPattern(Regex pattern)
		{
			switch (0)
			{
			default:
			{
				IEnumerator enumerator = this.Sections.GetEnumerator();
				TextSelection result;
				try
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							if (!enumerator.MoveNext())
							{
								num = 3;
								continue;
							}
							Section section = (Section)enumerator.Current;
							IEnumerator enumerator2 = section.ChildObjects.GetEnumerator();
							num = 4;
							continue;
						}
						case 2:
							goto IL_1AC;
						case 3:
							goto IL_1A0;
						case 4:
							try
							{
								num = 3;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										TextSelection textSelection;
										result = textSelection;
										num = 5;
										continue;
									}
									case 1:
									{
										IEnumerator enumerator2;
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											break;
										default:
											if (false)
											{
											}
											if (!enumerator2.MoveNext())
											{
												num = 6;
												continue;
											}
											break;
										}
										Body body = (Body)enumerator2.Current;
										TextSelection textSelection = body.ᜀ(pattern);
										num = 4;
										continue;
									}
									case 2:
										goto IL_152;
									case 4:
									{
										TextSelection textSelection;
										if (textSelection != null)
										{
											num = 0;
											continue;
										}
										break;
									}
									case 5:
										goto IL_D9;
									case 6:
										num = 2;
										continue;
									}
									IL_10A:
									num = 1;
									continue;
									goto IL_10A;
								}
								IL_D9:
								return result;
								IL_152:
								break;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator2;
									IDisposable disposable = enumerator2 as IDisposable;
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_19D;
										case 1:
											disposable.Dispose();
											num = 0;
											continue;
										case 2:
											if (disposable != null)
											{
												num = 1;
												continue;
											}
											goto IL_19F;
										}
										break;
									}
								}
								IL_19D:
								IL_19F:;
							}
							goto IL_1A0;
						}
						IL_59:
						num = 0;
						continue;
						goto IL_59;
						IL_1A0:
						num = 2;
					}
					IL_1AC:
					goto IL_1D;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable2 = enumerator as IDisposable;
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (disposable2 != null)
								{
									num = 2;
									continue;
								}
								goto IL_1F9;
							case 1:
								goto IL_1F7;
							case 2:
								disposable2.Dispose();
								num = 1;
								continue;
							}
							break;
						}
					}
					IL_1F7:
					IL_1F9:;
				}
				return result;
				IL_1D:
				if (true)
				{
				}
				return null;
			}
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00038AC0 File Offset: 0x00037AC0
		public TextSelection[] FindPatternInLine(Regex pattern)
		{
			switch (0)
			{
			default:
			{
				TextSelection[] array = null;
				IEnumerator enumerator = this.Sections.GetEnumerator();
				try
				{
					int num = 4;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							goto IL_A3;
						case 1:
							goto IL_AF;
						case 3:
						{
							if (!enumerator.MoveNext())
							{
								num = 0;
								continue;
							}
							Section section = (Section)enumerator.Current;
							array = spr\u25C5.ᜀ().ᜀ(section.Body, pattern);
							num = 5;
							continue;
						}
						case 5:
							if (array == null)
							{
								num = 2;
								continue;
							}
							goto IL_A3;
						}
						IL_87:
						num = 3;
						continue;
						goto IL_87;
						IL_A3:
						num = 1;
					}
					IL_AF:;
				}
				finally
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						break;
					}
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (disposable != null)
								{
									num = 2;
									continue;
								}
								goto IL_111;
							case 1:
								goto IL_10F;
							case 2:
								disposable.Dispose();
								num = 1;
								continue;
							}
							break;
						}
					}
					IL_10F:
					IL_111:;
				}
				return array;
			}
			}
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00038BFC File Offset: 0x00037BFC
		public TextSelection FindString(string stringValue, bool caseSensitive, bool wholeWord)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			Regex pattern = spr\u1AB5.ᜀ(stringValue, caseSensitive, wholeWord);
			return this.FindPattern(pattern);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00038C48 File Offset: 0x00037C48
		public TextSelection[] FindStringInLine(string given, bool caseSensitive, bool wholeWord)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			Regex pattern = spr\u1AB5.ᜀ(given, caseSensitive, wholeWord);
			return this.FindPatternInLine(pattern);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00038C94 File Offset: 0x00037C94
		public TextSelection[] FindAllPattern(Regex pattern)
		{
			switch (0)
			{
			default:
			{
				spr\u226E spr_u226E;
				for (;;)
				{
					spr_u226E = null;
					IEnumerator enumerator = this.Sections.GetEnumerator();
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							try
							{
								num = 3;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_22B;
									case 1:
										goto IL_237;
									case 2:
									{
										if (!enumerator.MoveNext())
										{
											num = 0;
											continue;
										}
										Section section = (Section)enumerator.Current;
										IEnumerator enumerator2 = section.ChildObjects.GetEnumerator();
										num = 4;
										continue;
									}
									case 4:
										try
										{
											num = 4;
											for (;;)
											{
												switch (num)
												{
												case 2:
												{
													if (spr_u226E == null)
													{
														num = 8;
														continue;
													}
													spr\u226E spr_u226E2;
													spr_u226E.AddRange(spr_u226E2);
													num = 0;
													continue;
												}
												case 3:
												{
													spr\u226E spr_u226E2;
													if (spr_u226E2.Count > 0)
													{
														num = 5;
														continue;
													}
													break;
												}
												case 5:
													num = 2;
													continue;
												case 6:
												{
													IEnumerator enumerator2;
													if (!enumerator2.MoveNext())
													{
														num = 9;
														continue;
													}
													Body body = (Body)enumerator2.Current;
													spr\u226E spr_u226E2 = body.ᜁ(pattern);
													num = 11;
													continue;
												}
												case 7:
													num = 3;
													continue;
												case 8:
												{
													spr\u226E spr_u226E2;
													spr_u226E = spr_u226E2;
													num = 1;
													continue;
												}
												case 9:
													num = 10;
													continue;
												case 10:
													goto IL_1DD;
												case 11:
												{
													spr\u226E spr_u226E2;
													if (spr_u226E2 != null)
													{
														num = 7;
														continue;
													}
													break;
												}
												}
												IL_13A:
												num = 6;
												continue;
												goto IL_13A;
											}
											IL_1DD:
											break;
										}
										finally
										{
											for (;;)
											{
												IEnumerator enumerator2;
												IDisposable disposable = enumerator2 as IDisposable;
												num = 0;
												for (;;)
												{
													switch (num)
													{
													case 0:
														if (disposable != null)
														{
															num = 2;
															continue;
														}
														goto IL_22A;
													case 1:
														goto IL_228;
													case 2:
														disposable.Dispose();
														num = 1;
														continue;
													}
													break;
												}
											}
											IL_228:
											IL_22A:;
										}
										goto IL_22B;
									}
									IL_B9:
									num = 2;
									continue;
									goto IL_B9;
									IL_22B:
									num = 1;
								}
								IL_237:
								goto IL_47;
							}
							finally
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									break;
								}
								for (;;)
								{
									IDisposable disposable2 = enumerator as IDisposable;
									num = 1;
									for (;;)
									{
										switch (num)
										{
										case 0:
											disposable2.Dispose();
											num = 2;
											continue;
										case 1:
											if (disposable2 != null)
											{
												num = 0;
												continue;
											}
											goto IL_2A0;
										case 2:
											goto IL_29E;
										}
										break;
									}
								}
								IL_29E:
								IL_2A0:;
							}
							goto IL_2A1;
							IL_47:
							num = 1;
							continue;
						case 1:
							if (true)
							{
							}
							if (spr_u226E == null)
							{
								num = 2;
								continue;
							}
							goto IL_2A3;
						case 2:
							goto IL_67;
						}
						break;
					}
				}
				IL_67:
				IL_2A1:
				return null;
				IL_2A3:
				return spr_u226E.ToArray();
			}
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00038F80 File Offset: 0x00037F80
		public TextSelection[] FindAllString(string matchString, bool caseSensitive, bool wholeWord)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			Regex pattern = spr\u1AB5.ᜀ(matchString, caseSensitive, wholeWord);
			return this.FindAllPattern(pattern);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00038FCC File Offset: 0x00037FCC
		public int Replace(Regex pattern, string replace)
		{
			switch (0)
			{
			default:
			{
				int num = 0;
				IEnumerator enumerator = this.Sections.GetEnumerator();
				int result;
				try
				{
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_1A5;
						case 2:
						{
							if (!enumerator.MoveNext())
							{
								num2 = 0;
								continue;
							}
							Section section = (Section)enumerator.Current;
							IEnumerator enumerator2 = section.ChildObjects.GetEnumerator();
							num2 = 3;
							continue;
						}
						case 3:
							try
							{
								num2 = 0;
								for (;;)
								{
									switch (num2)
									{
									case 1:
										if (num > 0)
										{
											num2 = 3;
											continue;
										}
										break;
									case 2:
									{
										IEnumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num2 = 8;
											continue;
										}
										Body body = (Body)enumerator2.Current;
										num += body.ᜀ(pattern, replace);
										num2 = 4;
										continue;
									}
									case 3:
										result = num;
										num2 = 7;
										continue;
									case 4:
										if (this.ReplaceFirst)
										{
											num2 = 6;
											continue;
										}
										break;
									case 5:
										goto IL_157;
									case 6:
										num2 = 1;
										continue;
									case 7:
										goto IL_D1;
									case 8:
										num2 = 5;
										continue;
									}
									IL_10A:
									num2 = 2;
									continue;
									goto IL_10A;
								}
								IL_D1:
								return result;
								IL_157:
								break;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator2;
									IDisposable disposable = enumerator2 as IDisposable;
									num2 = 1;
									for (;;)
									{
										switch (num2)
										{
										case 0:
											disposable.Dispose();
											num2 = 2;
											continue;
										case 1:
											if (disposable != null)
											{
												num2 = 0;
												continue;
											}
											goto IL_1A4;
										case 2:
											goto IL_1A2;
										}
										break;
									}
								}
								IL_1A2:
								IL_1A4:;
							}
							goto IL_1A5;
						case 4:
							goto IL_1B9;
						}
						IL_49:
						num2 = 2;
						continue;
						goto IL_49;
						IL_1A5:
						if (true)
						{
						}
						num2 = 4;
					}
					IL_1B9:
					return num;
				}
				finally
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						break;
					}
					for (;;)
					{
						IDisposable disposable2 = enumerator as IDisposable;
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								if (disposable2 != null)
								{
									num2 = 2;
									continue;
								}
								goto IL_222;
							case 1:
								goto IL_220;
							case 2:
								disposable2.Dispose();
								num2 = 1;
								continue;
							}
							break;
						}
					}
					IL_220:
					IL_222:;
				}
				return result;
			}
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0003923C File Offset: 0x0003823C
		public int Replace(string matchString, string newValue, bool caseSensitive, bool wholeWord)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			Regex pattern = spr\u1AB5.ᜀ(matchString, caseSensitive, wholeWord);
			return this.Replace(pattern, newValue);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0003928C File Offset: 0x0003828C
		public int Replace(string matchString, TextSelection textSelection, bool caseSensitive, bool wholeWord)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ᜀ(matchString, textSelection, caseSensitive, wholeWord, false);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x000392D4 File Offset: 0x000382D4
		internal new int ᜀ(string A_0, TextSelection A_1, bool A_2, bool A_3, bool A_4)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			Regex a_ = spr\u1AB5.ᜀ(A_0, A_2, A_3);
			return this.ᜀ(a_, A_1, A_4);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00039324 File Offset: 0x00038324
		public int Replace(Regex pattern, TextSelection textSelection)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ᜀ(pattern, textSelection, false);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00039368 File Offset: 0x00038368
		internal new int ᜀ(Regex A_0, TextSelection A_1, bool A_2)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				A_1.ᜂ();
				int num = 0;
				IEnumerator enumerator = this.Sections.GetEnumerator();
				int result;
				try
				{
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_1B4;
						case 1:
							try
							{
								num2 = 5;
								for (;;)
								{
									switch (num2)
									{
									case 0:
									{
										IEnumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num2 = 4;
											continue;
										}
										Body body = (Body)enumerator2.Current;
										num += body.ᜀ(A_0, A_1, A_2);
										num2 = 8;
										continue;
									}
									case 1:
										goto IL_166;
									case 2:
										if (num > 0)
										{
											num2 = 6;
											continue;
										}
										break;
									case 3:
										goto IL_DF;
									case 4:
										num2 = 1;
										continue;
									case 6:
										result = num;
										num2 = 3;
										continue;
									case 7:
										num2 = 2;
										continue;
									case 8:
										if (this.ReplaceFirst)
										{
											num2 = 7;
											continue;
										}
										break;
									}
									IL_119:
									num2 = 0;
									continue;
									goto IL_119;
								}
								IL_DF:
								return result;
								IL_166:
								break;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator2;
									IDisposable disposable = enumerator2 as IDisposable;
									num2 = 0;
									for (;;)
									{
										switch (num2)
										{
										case 0:
											if (disposable != null)
											{
												num2 = 1;
												continue;
											}
											goto IL_1B3;
										case 1:
											disposable.Dispose();
											num2 = 2;
											continue;
										case 2:
											goto IL_1B1;
										}
										break;
									}
								}
								IL_1B1:
								IL_1B3:;
							}
							goto IL_1B4;
						case 3:
						{
							if (!enumerator.MoveNext())
							{
								num2 = 0;
								continue;
							}
							Section section = (Section)enumerator.Current;
							IEnumerator enumerator2 = section.ChildObjects.GetEnumerator();
							num2 = 1;
							continue;
						}
						case 4:
							goto IL_1C0;
						}
						IL_57:
						num2 = 3;
						continue;
						goto IL_57;
						IL_1B4:
						num2 = 4;
					}
					IL_1C0:
					return num;
				}
				finally
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						break;
					}
					for (;;)
					{
						IDisposable disposable2 = enumerator as IDisposable;
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								if (disposable2 != null)
								{
									num2 = 1;
									continue;
								}
								goto IL_229;
							case 1:
								disposable2.Dispose();
								num2 = 2;
								continue;
							case 2:
								goto IL_227;
							}
							break;
						}
					}
					IL_227:
					IL_229:;
				}
				return result;
			}
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x000395E0 File Offset: 0x000385E0
		internal new int ᜀ(string A_0, TextBodyPart A_1, bool A_2, bool A_3)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ᜀ(A_0, A_1, A_2, A_3, false);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00039628 File Offset: 0x00038628
		internal new int ᜀ(string A_0, TextBodyPart A_1, bool A_2, bool A_3, bool A_4)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			Regex a_ = spr\u1AB5.ᜀ(A_0, A_2, A_3);
			return this.ᜀ(a_, A_1, A_4);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00039678 File Offset: 0x00038678
		internal new int ᜀ(Regex A_0, TextBodyPart A_1)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return this.ᜀ(A_0, A_1, false);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000396BC File Offset: 0x000386BC
		internal new int ᜀ(Regex A_0, TextBodyPart A_1, bool A_2)
		{
			switch (0)
			{
			default:
			{
				int num = 0;
				IEnumerator enumerator = this.Sections.GetEnumerator();
				int result;
				try
				{
					int num2 = 3;
					for (;;)
					{
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
							goto IL_1BA;
						case 1:
							try
							{
								num2 = 7;
								for (;;)
								{
									switch (num2)
									{
									case 0:
									{
										IEnumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num2 = 1;
											continue;
										}
										Body body = (Body)enumerator2.Current;
										num += body.ᜀ(A_0, A_1, A_2);
										num2 = 8;
										continue;
									}
									case 1:
										num2 = 3;
										continue;
									case 2:
										if (num > 0)
										{
											num2 = 4;
											continue;
										}
										break;
									case 3:
										goto IL_160;
									case 4:
										result = num;
										num2 = 6;
										continue;
									case 5:
										num2 = 2;
										continue;
									case 6:
										goto IL_D9;
									case 8:
										if (this.ReplaceFirst)
										{
											num2 = 5;
											continue;
										}
										break;
									}
									IL_113:
									num2 = 0;
									continue;
									goto IL_113;
								}
								IL_D9:
								return result;
								IL_160:
								break;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator2;
									IDisposable disposable = enumerator2 as IDisposable;
									num2 = 1;
									for (;;)
									{
										switch (num2)
										{
										case 0:
											disposable.Dispose();
											num2 = 2;
											continue;
										case 1:
											if (disposable != null)
											{
												num2 = 0;
												continue;
											}
											goto IL_1AD;
										case 2:
											goto IL_1AB;
										}
										break;
									}
								}
								IL_1AB:
								IL_1AD:;
							}
							goto IL_1AE;
						case 2:
							goto IL_1AE;
						case 4:
						{
							if (!enumerator.MoveNext())
							{
								num2 = 2;
								continue;
							}
							Section section = (Section)enumerator.Current;
							IEnumerator enumerator2 = section.ChildObjects.GetEnumerator();
							num2 = 1;
							continue;
						}
						}
						IL_51:
						num2 = 4;
						continue;
						goto IL_51;
						IL_1AE:
						num2 = 0;
					}
					IL_1BA:
					return num;
				}
				finally
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						break;
					}
					for (;;)
					{
						IDisposable disposable2 = enumerator as IDisposable;
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_221;
							case 1:
								disposable2.Dispose();
								num2 = 0;
								continue;
							case 2:
								if (disposable2 != null)
								{
									num2 = 1;
									continue;
								}
								goto IL_223;
							}
							break;
						}
					}
					IL_221:
					IL_223:;
				}
				return result;
			}
			}
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0003992C File Offset: 0x0003892C
		public int Replace(string matchString, IDocument matchDoc, bool caseSensitive, bool wholeWord)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ᜀ(matchString, matchDoc, caseSensitive, wholeWord, false);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00039974 File Offset: 0x00038974
		internal new int ᜀ(string A_0, IDocument A_1, bool A_2, bool A_3, bool A_4)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			Regex a_ = spr\u1AB5.ᜀ(A_0, A_2, A_3);
			return this.ᜀ(a_, A_1, A_4);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x000399C4 File Offset: 0x000389C4
		internal new int ᜀ(Regex A_0, IDocument A_1, bool A_2)
		{
			switch (0)
			{
			default:
			{
				int num;
				for (;;)
				{
					num = 0;
					IEnumerator enumerator = this.Sections.GetEnumerator();
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_2AC;
						case 1:
							if (true)
							{
							}
							this.ᜁ(A_1);
							num2 = 0;
							continue;
						case 2:
							try
							{
								num2 = 2;
								for (;;)
								{
									IEnumerator enumerator2;
									switch (num2)
									{
									case 0:
										num2 = 1;
										continue;
									case 1:
										goto IL_249;
									case 3:
										try
										{
											num2 = 6;
											int result;
											for (;;)
											{
												switch (num2)
												{
												case 0:
													goto IL_1A8;
												case 1:
													if (this.ReplaceFirst)
													{
														num2 = 4;
														continue;
													}
													break;
												case 2:
												{
													if (!enumerator2.MoveNext())
													{
														num2 = 5;
														continue;
													}
													Body body = (Body)enumerator2.Current;
													num += body.ᜀ(A_0, A_1, A_2);
													num2 = 1;
													continue;
												}
												case 3:
													result = num;
													num2 = 8;
													continue;
												case 4:
													num2 = 7;
													continue;
												case 5:
													num2 = 0;
													continue;
												case 7:
													if (num > 0)
													{
														num2 = 3;
														continue;
													}
													break;
												case 8:
													goto IL_197;
												}
												IL_119:
												num2 = 2;
												continue;
												goto IL_119;
											}
											IL_197:
											return result;
											IL_1A8:
											break;
										}
										finally
										{
											for (;;)
											{
												for (;;)
												{
													IDisposable disposable = enumerator2 as IDisposable;
													num2 = 0;
													for (;;)
													{
														switch (num2)
														{
														case 0:
															if (disposable != null)
															{
																num2 = 1;
																continue;
															}
															goto IL_211;
														case 1:
															switch ((1 == 1) ? 1 : 0)
															{
															case 0:
															case 2:
																break;
															default:
																if (false)
																{
																}
																disposable.Dispose();
																num2 = 2;
																continue;
															}
															break;
														case 2:
															goto IL_20F;
														}
														break;
													}
												}
											}
											IL_20F:
											IL_211:;
										}
										goto IL_212;
									case 4:
										if (!enumerator.MoveNext())
										{
											num2 = 0;
											continue;
										}
										goto IL_212;
									}
									IL_BE:
									num2 = 4;
									continue;
									goto IL_BE;
									IL_212:
									Section section = (Section)enumerator.Current;
									enumerator2 = section.ChildObjects.GetEnumerator();
									num2 = 3;
								}
								IL_249:
								goto IL_6A;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable2 = enumerator as IDisposable;
									num2 = 1;
									for (;;)
									{
										switch (num2)
										{
										case 0:
											goto IL_294;
										case 1:
											if (disposable2 != null)
											{
												num2 = 2;
												continue;
											}
											goto IL_296;
										case 2:
											disposable2.Dispose();
											num2 = 0;
											continue;
										}
										break;
									}
								}
								IL_294:
								IL_296:;
							}
							return num;
							IL_6A:
							num2 = 4;
							continue;
						case 3:
							return num;
						case 4:
							if (this.m_doc.ObjectPool != null)
							{
								num2 = 1;
								continue;
							}
							this.ᜀ((A_1 as Document).ObjectPool, ref this.\u171B);
							num2 = 3;
							continue;
						}
						break;
					}
				}
				return num;
				IL_2AC:
				return num;
			}
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00039CC0 File Offset: 0x00038CC0
		public void UpdateWordCount()
		{
			switch (0)
			{
			default:
			{
				this.ᝆ = (this.ᝇ = (this.ᝈ = 0));
				IEnumerator enumerator = this.Sections.GetEnumerator();
				try
				{
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (enumerator.MoveNext())
							{
								Section section = (Section)enumerator.Current;
								this.ᜀ(section.Body.Items);
								num = 2;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_C5;
							default:
								if (false)
								{
								}
								num = 1;
								continue;
							}
							break;
						case 1:
							goto IL_C5;
						case 3:
							goto IL_D3;
						}
						IL_8F:
						num = 0;
						continue;
						goto IL_8F;
						IL_C5:
						num = 3;
					}
					IL_D3:;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								disposable.Dispose();
								num = 1;
								continue;
							case 1:
								goto IL_11A;
							case 2:
								if (disposable != null)
								{
									num = 0;
									continue;
								}
								goto IL_11C;
							}
							break;
						}
					}
					IL_11A:
					IL_11C:;
				}
				if (true)
				{
				}
				this.BuiltinDocumentProperties.ParagraphCount = this.ᝆ;
				this.BuiltinDocumentProperties.WordCount = this.ᝇ;
				this.BuiltinDocumentProperties.CharCount = this.ᝈ;
				return;
			}
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00039E38 File Offset: 0x00038E38
		internal void \u173B()
		{
			for (;;)
			{
				spr\u1A69 spr_u1A = new spr\u1A69();
				spr_u1A.ᜁ(this);
				spr_u1A.ᜠ();
				this.\u175E = spr_u1A.ᜤ().Count;
				int num = 0;
				if (true)
				{
				}
				int num2 = 6;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						this.m_doc.Fields.ᜀ(num).ᜎ();
						num2 = 1;
						continue;
					case 1:
						goto IL_5C;
					case 2:
						if (num >= this.m_doc.Fields.Count)
						{
							num2 = 4;
							continue;
						}
						num2 = 5;
						continue;
					case 3:
						goto IL_CD;
					case 4:
						goto IL_F6;
					case 5:
						if (!this.\u1757.Contains(this.m_doc.Fields.ᜀ(num)))
						{
							num2 = 0;
							continue;
						}
						goto IL_5C;
					case 6:
						goto IL_CD;
					}
					break;
					IL_5C:
					num++;
					num2 = 3;
					continue;
					IL_CD:
					num2 = 2;
				}
			}
			for (;;)
			{
				IL_F6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_10E;
				}
			}
			IL_10E:
			if (false)
			{
			}
			this.\u1757.Clear();
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00039F64 File Offset: 0x00038F64
		private new void ᜀ(BodyRegionCollection A_0)
		{
			IEnumerator enumerator = A_0.GetEnumerator();
			try
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
					{
						BodyRegion bodyRegion;
						if (bodyRegion is Paragraph)
						{
							num = 5;
							continue;
						}
						if (true)
						{
						}
						this.ᜀ(bodyRegion as Table);
						num = 3;
						continue;
					}
					case 2:
						goto IL_D9;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 5:
					{
						BodyRegion bodyRegion;
						this.ᜀ(bodyRegion as Paragraph);
						num = 4;
						continue;
					}
					case 6:
						num = 2;
						continue;
					case 7:
					{
						if (!enumerator.MoveNext())
						{
							num = 6;
							continue;
						}
						BodyRegion bodyRegion = (BodyRegion)enumerator.Current;
						num = 1;
						continue;
					}
					}
					IL_61:
					num = 7;
					continue;
					goto IL_61;
				}
				IL_D9:;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (disposable != null)
							{
								num = 1;
								continue;
							}
							goto IL_11B;
						case 1:
							disposable.Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_119;
						}
						break;
					}
				}
				IL_119:
				IL_11B:;
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0003A0A8 File Offset: 0x000390A8
		private new void ᜀ(Table A_0)
		{
			switch (0)
			{
			default:
			{
				IEnumerator enumerator = A_0.Rows.GetEnumerator();
				try
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							try
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 2:
										num = 3;
										continue;
									case 3:
										goto IL_108;
									case 4:
									{
										IEnumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num = 2;
											continue;
										}
										TableCell tableCell = (TableCell)enumerator2.Current;
										this.ᜀ(tableCell.Items);
										num = 0;
										continue;
									}
									}
									IL_C0:
									num = 4;
									continue;
									goto IL_C0;
								}
								IL_108:
								break;
							}
							finally
							{
								for (;;)
								{
									for (;;)
									{
										IEnumerator enumerator2;
										IDisposable disposable = enumerator2 as IDisposable;
										num = 0;
										for (;;)
										{
											switch (num)
											{
											case 0:
												if (disposable != null)
												{
													num = 1;
													continue;
												}
												goto IL_170;
											case 1:
												switch ((1 == 1) ? 1 : 0)
												{
												case 0:
												case 2:
													break;
												default:
													if (false)
													{
													}
													disposable.Dispose();
													num = 2;
													continue;
												}
												break;
											case 2:
												goto IL_16E;
											}
											break;
										}
									}
								}
								IL_16E:
								IL_170:;
							}
							goto IL_171;
						case 2:
						{
							if (!enumerator.MoveNext())
							{
								if (true)
								{
								}
								num = 4;
								continue;
							}
							TableRow tableRow = (TableRow)enumerator.Current;
							IEnumerator enumerator2 = tableRow.Cells.GetEnumerator();
							num = 0;
							continue;
						}
						case 3:
							goto IL_17D;
						case 4:
							goto IL_171;
						}
						IL_71:
						num = 2;
						continue;
						goto IL_71;
						IL_171:
						num = 3;
					}
					IL_17D:;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable2 = enumerator as IDisposable;
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_1C4;
							case 1:
								if (disposable2 != null)
								{
									num = 2;
									continue;
								}
								goto IL_1C6;
							case 2:
								disposable2.Dispose();
								num = 0;
								continue;
							}
							break;
						}
					}
					IL_1C4:
					IL_1C6:;
				}
				return;
			}
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0003A2B0 File Offset: 0x000392B0
		private new void ᜀ(Paragraph A_0)
		{
			int a_ = 6;
			switch (0)
			{
			default:
				for (;;)
				{
					IL_48:
					string text = A_0.Text;
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_E7:
						goto IL_194;
					default:
						if (false)
						{
						}
						num = 8;
						break;
					}
					int num2;
					for (;;)
					{
						IL_19:
						switch (num)
						{
						case 0:
							return;
						case 1:
						{
							this.ᝆ++;
							string[] array = text.Split(ClipboardData.b("䱫", a_).ToCharArray());
							string[] array2 = array;
							num2 = 0;
							num = 4;
							continue;
						}
						case 2:
							goto IL_9F;
						case 3:
						{
							string[] array2;
							if (num2 >= array2.Length)
							{
								num = 9;
								continue;
							}
							string a = array2[num2];
							num = 7;
							continue;
						}
						case 4:
							goto IL_9F;
						case 5:
							goto IL_E7;
						case 6:
							this.ᝇ++;
							num = 5;
							continue;
						case 7:
						{
							string a;
							if (a != string.Empty)
							{
								num = 6;
								continue;
							}
							goto IL_194;
						}
						case 8:
							if (true)
							{
							}
							if (A_0.Text != string.Empty)
							{
								num = 1;
								continue;
							}
							return;
						case 9:
							text = text.Replace(ClipboardData.b("䱫", a_), string.Empty);
							this.ᝈ += text.Length;
							num = 0;
							continue;
						}
						goto IL_48;
						IL_9F:
						num = 3;
					}
					IL_194:
					num2++;
					num = 2;
					goto IL_19;
				}
				return;
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0003A468 File Offset: 0x00039468
		public void UpdateTableOfContents()
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_52:
				this.TOC.\u1716();
				this.ᜱ();
				num = 0;
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num = 1;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 2:
					goto IL_50;
				}
				if (!this.HasTOC)
				{
					return;
				}
				num = 2;
			}
			IL_50:
			goto IL_52;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0003A4EC File Offset: 0x000394EC
		internal new string ᜀ(Paragraph A_0, ListFormat A_1, ListLevel A_2)
		{
			int a_ = 1;
			switch (0)
			{
			default:
			{
				string text2;
				for (;;)
				{
					string text = A_1.CustomStyleName;
					int num = 40;
					for (;;)
					{
						string key;
						spr\u177D spr_u177D;
						int a_2;
						int num2;
						switch (num)
						{
						case 0:
							if (A_1.CurrentListLevel.PatternType == ListPatternType.LowLetter)
							{
								num = 84;
								continue;
							}
							goto IL_751;
						case 1:
							if (A_1.CurrentListLevel.NumberSufix != null)
							{
								num = 65;
								continue;
							}
							goto IL_365;
						case 2:
							goto IL_83A;
						case 3:
							goto IL_365;
						case 4:
							if (A_1.CurrentListLevel.NumberPrefix.StartsWith(ClipboardData.b("杦䝨", a_)))
							{
								num = 24;
								continue;
							}
							goto IL_924;
						case 5:
							num = 1;
							continue;
						case 6:
							if (!this.LfoListLevel.ContainsKey(key))
							{
								num = 13;
								continue;
							}
							goto IL_731;
						case 7:
							goto IL_5A6;
						case 8:
							num = 29;
							continue;
						case 9:
							num = 56;
							continue;
						case 10:
							num = 32;
							continue;
						case 11:
							goto IL_34B;
						case 12:
							if (text2.StartsWith(A_1.CurrentListLevel.NumberPrefix))
							{
								num = 3;
								continue;
							}
							goto IL_83A;
						case 13:
							this.LfoListLevel.Add(key, A_2.LevelNumber);
							num = 36;
							continue;
						case 14:
							goto IL_532;
						case 15:
							goto IL_8A3;
						case 16:
							if (A_2.LevelNumber > this.PreviousListLevel[text])
							{
								num = 66;
								continue;
							}
							goto IL_5A6;
						case 17:
							if (spr_u177D == null)
							{
								num = 57;
								continue;
							}
							goto IL_696;
						case 18:
							goto IL_696;
						case 19:
							goto IL_532;
						case 20:
							if (this.LfoListLevel.ContainsKey(key))
							{
								num = 58;
								continue;
							}
							goto IL_34B;
						case 21:
							num = 6;
							continue;
						case 22:
							num = 16;
							continue;
						case 23:
							num = 43;
							continue;
						case 24:
							text2 = this.ᜀ(A_1, text, A_2, a_2, num2);
							num = 31;
							continue;
						case 25:
							if (A_1.CurrentListLevel.NumberPrefix != null)
							{
								num = 8;
								continue;
							}
							goto IL_2A8;
						case 26:
							num = 54;
							continue;
						case 27:
							num = 20;
							continue;
						case 28:
							if (A_1.CurrentListLevel.NumberPrefix != null)
							{
								num = 59;
								continue;
							}
							goto IL_38F;
						case 29:
							if (A_1.CurrentListLevel.NumberSufix != null)
							{
								num = 62;
								continue;
							}
							goto IL_2A8;
						case 30:
							num = 83;
							continue;
						case 31:
							goto IL_38F;
						case 32:
							if (A_1.CurrentListLevel.PatternType == ListPatternType.LowRoman)
							{
								num = 9;
								continue;
							}
							goto IL_38F;
						case 33:
							this.LfoListLevel.Clear();
							num = 18;
							continue;
						case 34:
							goto IL_80D;
						case 35:
							if (A_2.PatternType != ListPatternType.Bullet)
							{
								num = 72;
								continue;
							}
							goto IL_696;
						case 36:
							goto IL_696;
						case 37:
							if (spr_u177D != null)
							{
								num = 21;
								continue;
							}
							goto IL_731;
						case 38:
							num = 0;
							continue;
						case 39:
							if (spr_u177D != null)
							{
								num = 44;
								continue;
							}
							goto IL_5D4;
						case 40:
							if (A_0.IsInCell)
							{
								num = 76;
								continue;
							}
							goto IL_80D;
						case 41:
							if (A_1.CurrentListLevel.NumberPrefix != null)
							{
								num = 48;
								continue;
							}
							goto IL_924;
						case 42:
							if (A_0.ListFormat.IsRestartNumbering)
							{
								num = 11;
								continue;
							}
							num = 77;
							continue;
						case 43:
							if (text2.StartsWith(A_1.CurrentListLevel.NumberPrefix))
							{
								num = 85;
								continue;
							}
							goto IL_4EC;
						case 44:
							num = 47;
							continue;
						case 45:
							if (!text2.StartsWith(A_1.CurrentListLevel.NumberPrefix))
							{
								num = 64;
								continue;
							}
							goto IL_751;
						case 46:
							goto IL_38F;
						case 47:
							if (spr_u177D.ᜃ().ᜁ(A_2.LevelNumber))
							{
								num = 52;
								continue;
							}
							goto IL_5D4;
						case 48:
							if (true)
							{
							}
							num = 4;
							continue;
						case 49:
							if ((A_0.OwnerTextBody as TableCell).OwnerRow.OwnerTable.IsTextBox)
							{
								num = 55;
								continue;
							}
							goto IL_80D;
						case 50:
							if (A_1.LFOStyleName != null)
							{
								num = 26;
								continue;
							}
							goto IL_8A3;
						case 51:
							if (A_1.CurrentListLevel.NumberPrefix != null)
							{
								num = 30;
								continue;
							}
							goto IL_751;
						case 52:
							num = 69;
							continue;
						case 53:
							return text2;
						case 54:
							if (A_1.LFOStyleName.Length > 0)
							{
								num = 60;
								continue;
							}
							goto IL_8A3;
						case 55:
							text += ClipboardData.b("㡦ᵨ๪ᕬ᭮፰ᱲ൴", a_);
							num = 34;
							continue;
						case 56:
							if (!text2.StartsWith(A_1.CurrentListLevel.NumberPrefix))
							{
								num = 2;
								continue;
							}
							goto IL_38F;
						case 57:
							num = 35;
							continue;
						case 58:
							goto IL_5D4;
						case 59:
							num = 70;
							continue;
						case 60:
							spr_u177D = this.ListOverrides.ᜀ(A_1.LFOStyleName);
							num = 61;
							continue;
						case 61:
							if (spr_u177D != null)
							{
								num = 78;
								continue;
							}
							goto IL_8A3;
						case 62:
							num = 75;
							continue;
						case 63:
							goto IL_5A6;
						case 64:
							goto IL_4EC;
						case 65:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1C6;
							default:
								if (false)
								{
								}
								num = 68;
								continue;
							}
							break;
						case 66:
							this.ᜀ(A_1, text, false);
							num = 7;
							continue;
						case 67:
							goto IL_38F;
						case 68:
							if (A_1.CurrentListLevel.PatternType == ListPatternType.UpRoman)
							{
								num = 81;
								continue;
							}
							goto IL_365;
						case 69:
							if (spr_u177D.ᜃ().ᜀ(A_2.LevelNumber).OverrideStartAtValue)
							{
								num = 27;
								continue;
							}
							goto IL_5D4;
						case 70:
							if (A_1.CurrentListLevel.NumberSufix != null)
							{
								num = 10;
								continue;
							}
							goto IL_38F;
						case 71:
							if (A_2.PatternType == ListPatternType.Bullet)
							{
								num = 82;
								continue;
							}
							return text2;
						case 72:
							num = 73;
							continue;
						case 73:
							if (A_0.ListFormat.ListType == ListType.Numbered)
							{
								num = 33;
								continue;
							}
							goto IL_696;
						case 74:
							this.PreviousListLevel[text] = A_2.LevelNumber;
							num = 14;
							continue;
						case 75:
							if (A_1.CurrentListLevel.PatternType == ListPatternType.UpLetter)
							{
								num = 23;
								continue;
							}
							goto IL_2A8;
						case 76:
							num = 49;
							continue;
						case 77:
							if (this.PreviousListLevel.ContainsKey(text))
							{
								num = 22;
								continue;
							}
							goto IL_5A6;
						case 78:
							key = A_1.LFOStyleName + ClipboardData.b("㡦", a_) + A_2.LevelNumber.ToString();
							num = 15;
							continue;
						case 79:
							if (A_1.CurrentListLevel.NumberPrefix != null)
							{
								num = 5;
								continue;
							}
							goto IL_365;
						case 80:
							if (this.PreviousListLevel.ContainsKey(text))
							{
								num = 74;
								continue;
							}
							this.PreviousListLevel.Add(text, A_2.LevelNumber);
							num = 19;
							continue;
						case 81:
							num = 12;
							continue;
						case 82:
							text2 = A_2.BulletCharacter;
							num = 53;
							continue;
						case 83:
							if (A_1.CurrentListLevel.NumberSufix != null)
							{
								goto IL_1C6;
							}
							goto IL_751;
						case 84:
							num = 45;
							continue;
						case 85:
							goto IL_2A8;
						}
						break;
						IL_1C6:
						num = 38;
						continue;
						IL_2A8:
						num = 51;
						continue;
						IL_34B:
						this.ᜀ(A_1, text, true);
						num = 63;
						continue;
						IL_365:
						num = 28;
						continue;
						IL_38F:
						num = 71;
						continue;
						IL_4EC:
						text2 = A_1.CurrentListLevel.NumberPrefix + text2.Replace(ClipboardData.b("䥦", a_), "") + A_1.CurrentListLevel.NumberSufix;
						num = 46;
						continue;
						IL_532:
						num = 37;
						continue;
						IL_5A6:
						num = 80;
						continue;
						IL_5D4:
						num = 42;
						continue;
						IL_696:
						text2 = string.Empty;
						a_2 = 0;
						num2 = this.ᜁ(A_1, text);
						text2 = A_2.GetListItemText(num2, A_1.ListType);
						a_2 = this.ᜀ(A_1, text);
						num = 41;
						continue;
						IL_731:
						num = 17;
						continue;
						IL_751:
						num = 79;
						continue;
						IL_80D:
						spr_u177D = null;
						key = string.Empty;
						num = 50;
						continue;
						IL_83A:
						text2 = A_1.CurrentListLevel.NumberPrefix + text2.Replace(ClipboardData.b("䥦", a_), "") + A_1.CurrentListLevel.NumberSufix;
						num = 67;
						continue;
						IL_8A3:
						num = 39;
						continue;
						IL_924:
						num = 25;
					}
				}
				return text2;
			}
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0003AF58 File Offset: 0x00039F58
		internal void ᜱ()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.PreviousListLevel.Clear();
			this.Lists.Clear();
			this.ListNames.Clear();
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0003AFB4 File Offset: 0x00039FB4
		private new void ᜀ(ListFormat A_0, string A_1, bool A_2)
		{
			for (;;)
			{
				IL_00:
				switch (0)
				{
				default:
				{
					int num = 1;
					for (;;)
					{
						int[] array;
						int num3;
						int num2;
						spr\u177D spr_u177D;
						HybridDictionary hybridDictionary;
						switch (num)
						{
						case 0:
							num2 = A_0.CurrentListStyle.Levels[array[num3]].StartAt;
							num = 16;
							continue;
						case 2:
							num2 = 0;
							num = 12;
							continue;
						case 3:
							num = 13;
							continue;
						case 4:
							goto IL_393;
						case 5:
							goto IL_393;
						case 6:
							if (A_0.LFOStyleName.Length > 0)
							{
								num = 23;
								continue;
							}
							goto IL_20F;
						case 7:
							num2 = spr_u177D.ᜃ().ᜀ(A_0.ListLevelNumber).StartAt;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								num = 26;
								continue;
							}
							break;
						case 8:
						{
							if (hybridDictionary == null)
							{
								num = 11;
								continue;
							}
							ICollection keys = hybridDictionary.Keys;
							IEnumerator enumerator = keys.GetEnumerator();
							int count = keys.Count;
							array = new int[count];
							int num4 = 0;
							num = 4;
							continue;
						}
						case 9:
							num = 17;
							continue;
						case 10:
							goto IL_269;
						case 11:
							return;
						case 12:
							if (spr_u177D.ᜃ().ᜀ(A_0.ListLevelNumber).OverrideStartAtValue)
							{
								num = 7;
								continue;
							}
							goto IL_241;
						case 13:
							if (spr_u177D.ᜃ().ᜁ(A_0.ListLevelNumber))
							{
								num = 2;
								continue;
							}
							goto IL_241;
						case 14:
							goto IL_20F;
						case 15:
							return;
						case 16:
							if (spr_u177D != null)
							{
								num = 3;
								continue;
							}
							goto IL_241;
						case 17:
							if (!A_0.CurrentListStyle.Levels[array[num3]].NoRestartByHigher)
							{
								num = 0;
								continue;
							}
							goto IL_1DF;
						case 18:
							if (A_0.LFOStyleName != null)
							{
								num = 21;
								continue;
							}
							goto IL_20F;
						case 19:
						{
							IEnumerator enumerator;
							if (!enumerator.MoveNext())
							{
								num = 25;
								continue;
							}
							int num4;
							array[num4] = (int)enumerator.Current;
							num4++;
							num = 5;
							continue;
						}
						case 20:
							if (array[num3] >= A_0.ListLevelNumber)
							{
								num = 9;
								continue;
							}
							goto IL_1DF;
						case 21:
							num = 6;
							continue;
						case 22:
							return;
						case 23:
							spr_u177D = this.ListOverrides.ᜀ(A_0.LFOStyleName);
							num = 14;
							continue;
						case 24:
						{
							int count;
							if (num3 >= count)
							{
								num = 15;
								continue;
							}
							num = 20;
							continue;
						}
						case 25:
							num3 = 0;
							num = 10;
							continue;
						case 26:
							goto IL_241;
						case 27:
							goto IL_269;
						case 28:
							goto IL_1DF;
						}
						if (this.\u175B == null)
						{
							num = 22;
							continue;
						}
						spr_u177D = null;
						num = 18;
						continue;
						IL_1DF:
						if (true)
						{
						}
						num3++;
						num = 27;
						continue;
						IL_20F:
						hybridDictionary = (this.ListNames[A_1] as HybridDictionary);
						num = 8;
						continue;
						IL_241:
						hybridDictionary[array[num3]] = num2;
						num = 28;
						continue;
						IL_269:
						num = 24;
						continue;
						IL_393:
						num = 19;
					}
					break;
				}
				}
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0003B37C File Offset: 0x0003A37C
		private int ᜁ(ListFormat A_0, string A_1)
		{
			HybridDictionary hybridDictionary;
			HybridDictionary hybridDictionary2;
			int num2;
			for (;;)
			{
				IL_00:
				switch (0)
				{
				default:
					for (;;)
					{
						spr\u177D spr_u177D = null;
						int num = 19;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (!spr_u177D.ᜃ().ᜁ(A_0.ListLevelNumber))
								{
									goto IL_D2;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_00;
								default:
									if (false)
									{
									}
									num = 12;
									continue;
								}
								break;
							case 1:
								if (hybridDictionary == null)
								{
									num = 3;
									continue;
								}
								num = 20;
								continue;
							case 2:
								if (spr_u177D.ᜃ().ᜀ(A_0.ListLevelNumber).OverrideStartAtValue)
								{
									num = 4;
									continue;
								}
								goto IL_37F;
							case 3:
								hybridDictionary2 = new HybridDictionary();
								this.ListNames.Add(A_1, hybridDictionary2);
								num2 = A_0.CurrentListStyle.Levels[A_0.ListLevelNumber].StartAt;
								num = 9;
								continue;
							case 4:
								num2 = spr_u177D.ᜃ().ᜀ(A_0.ListLevelNumber).StartAt;
								num = 5;
								continue;
							case 5:
								goto IL_1DE;
							case 6:
								goto IL_206;
							case 7:
								num2 = spr_u177D.ᜃ().ᜀ(A_0.ListLevelNumber).StartAt;
								num = 6;
								continue;
							case 8:
								num = 2;
								continue;
							case 9:
								if (spr_u177D != null)
								{
									num = 13;
									continue;
								}
								goto IL_D2;
							case 10:
								if (spr_u177D != null)
								{
									num = 21;
									continue;
								}
								goto IL_37F;
							case 11:
								if (spr_u177D.ᜃ().ᜀ(A_0.ListLevelNumber).OverrideStartAtValue)
								{
									num = 7;
									continue;
								}
								goto IL_D2;
							case 12:
								num = 11;
								continue;
							case 13:
								num = 0;
								continue;
							case 14:
								goto IL_EF;
							case 15:
								if (A_0.LFOStyleName.Length > 0)
								{
									num = 18;
									continue;
								}
								goto IL_EF;
							case 16:
								num = 15;
								continue;
							case 17:
								if (spr_u177D.ᜃ().ᜁ(A_0.ListLevelNumber))
								{
									num = 8;
									continue;
								}
								goto IL_37F;
							case 18:
								spr_u177D = this.ListOverrides.ᜀ(A_0.LFOStyleName);
								num = 14;
								continue;
							case 19:
								if (A_0.LFOStyleName != null)
								{
									num = 16;
									continue;
								}
								goto IL_EF;
							case 20:
								if (hybridDictionary[A_0.ListLevelNumber] != null)
								{
									num = 22;
									continue;
								}
								num2 = A_0.CurrentListStyle.Levels[A_0.ListLevelNumber].StartAt;
								num = 10;
								continue;
							case 21:
								num = 17;
								continue;
							case 22:
								goto IL_2C8;
							}
							break;
							IL_EF:
							hybridDictionary = (this.ListNames[A_1] as HybridDictionary);
							num2 = 0;
							num = 1;
						}
					}
					break;
				}
			}
			IL_D2:
			hybridDictionary2.Add(A_0.ListLevelNumber, num2 + 1);
			return num2 - 1;
			IL_1DE:
			goto IL_37F;
			IL_206:
			if (true)
			{
			}
			goto IL_D2;
			IL_2C8:
			num2 = (int)hybridDictionary[A_0.ListLevelNumber];
			hybridDictionary[A_0.ListLevelNumber] = num2 + 1;
			return num2 - 1;
			IL_37F:
			hybridDictionary.Add(A_0.ListLevelNumber, num2 + 1);
			return num2 - 1;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0003B724 File Offset: 0x0003A724
		private new int ᜀ(ListFormat A_0, string A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 1;
				ListLevel listLevel;
				Dictionary<int, int> dictionary2;
				for (;;)
				{
					IL_23:
					switch (num)
					{
					case 0:
					{
						int i;
						while (i > listLevel.LevelNumber)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num = 14;
								goto IL_23;
							}
						}
						Dictionary<int, int> dictionary;
						dictionary.Add(i, A_0.CurrentListStyle.Levels[i].StartAt + 1);
						i++;
						num = 2;
						continue;
					}
					case 2:
						goto IL_14D;
					case 3:
						if (dictionary2.ContainsKey(A_0.ListLevelNumber))
						{
							num = 6;
							continue;
						}
						goto IL_28A;
					case 4:
					{
						Dictionary<int, int> dictionary = new Dictionary<int, int>();
						this.Lists.Add(A_1, dictionary);
						num = 12;
						continue;
					}
					case 5:
						return 1;
					case 6:
					{
						int num2 = dictionary2[A_0.ListLevelNumber];
						dictionary2[A_0.ListLevelNumber] = num2 + 1;
						int num3 = A_0.ListLevelNumber;
						num = 10;
						continue;
					}
					case 7:
					{
						int num3;
						if (!dictionary2.ContainsKey(num3 + 1))
						{
							num = 17;
							continue;
						}
						dictionary2[num3 + 1] = 1;
						num3++;
						num = 13;
						continue;
					}
					case 8:
						if (A_0.CurrentListLevel.PatternType == ListPatternType.Bullet)
						{
							num = 5;
							continue;
						}
						goto IL_C4;
					case 9:
					{
						listLevel = A_0.CurrentListStyle.Levels[A_0.ListLevelNumber];
						int i = 0;
						num = 16;
						continue;
					}
					case 10:
						goto IL_1CF;
					case 11:
						if (!this.Lists.ContainsKey(A_1))
						{
							num = 4;
							continue;
						}
						dictionary2 = this.Lists[A_1];
						num = 3;
						continue;
					case 12:
						if (A_0.CurrentListStyle != null)
						{
							num = 9;
							continue;
						}
						return 1;
					case 13:
						goto IL_1CF;
					case 14:
						goto IL_18D;
					case 15:
						num = 8;
						continue;
					case 16:
						goto IL_14D;
					case 17:
					{
						int num2;
						return num2;
					}
					}
					if (A_0.CurrentListLevel != null)
					{
						num = 15;
						continue;
					}
					IL_C4:
					num = 11;
					continue;
					IL_14D:
					num = 0;
					continue;
					IL_1CF:
					num = 7;
				}
				IL_18D:
				if (true)
				{
				}
				return listLevel.StartAt;
				IL_28A:
				ListLevel listLevel2 = A_0.CurrentListStyle.Levels[A_0.ListLevelNumber];
				dictionary2.Add(A_0.ListLevelNumber, listLevel2.StartAt + 1);
				return listLevel2.StartAt;
			}
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0003B9F0 File Offset: 0x0003A9F0
		private new string ᜀ(ListFormat A_0, string A_1, ListLevel A_2, int A_3, int A_4)
		{
			int a_ = 3;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_26B:
				goto IL_29C;
			default:
				if (false)
				{
				}
				switch (0)
				{
				default:
					goto IL_94;
				}
				break;
			}
			int num;
			string str;
			int num3;
			string result;
			for (;;)
			{
				IL_35:
				int num2;
				bool flag;
				int num4;
				int num5;
				switch (num)
				{
				case 0:
				{
					Dictionary<int, int> dictionary;
					str = str + Convert.ToString(Convert.ToInt32(dictionary[num2]) - 1) + ClipboardData.b("䝨", a_);
					num = 12;
					continue;
				}
				case 1:
					num = 11;
					continue;
				case 2:
					flag = (num2 <= num3);
					goto IL_23A;
				case 3:
				{
					int[] array;
					if (array.Length <= 0)
					{
						num = 15;
						continue;
					}
					num = 5;
					continue;
				}
				case 4:
					str += ClipboardData.b("奨", a_);
					num = 14;
					continue;
				case 5:
				{
					int[] array;
					num4 = array[0];
					goto IL_19A;
				}
				case 6:
					if (A_2.PatternType == ListPatternType.LeadingZero)
					{
						if (true)
						{
						}
						num = 19;
						continue;
					}
					goto IL_29C;
				case 7:
					if (this.Lists.ContainsKey(A_1))
					{
						num = 16;
						continue;
					}
					return result;
				case 8:
					goto IL_2C5;
				case 9:
					goto IL_2C5;
				case 10:
				{
					Dictionary<int, int> dictionary;
					if (dictionary.ContainsKey(num2))
					{
						num = 0;
						continue;
					}
					goto IL_D6;
				}
				case 11:
					flag = (num2 < num3);
					goto IL_23A;
				case 12:
					goto IL_D6;
				case 13:
					if (A_3 < 10)
					{
						num = 4;
						continue;
					}
					goto IL_29C;
				case 14:
					goto IL_26B;
				case 15:
					num = 20;
					continue;
				case 16:
				{
					str = string.Empty;
					Dictionary<int, int> dictionary = this.Lists[A_1];
					int[] array = new int[dictionary.Count];
					dictionary.Keys.CopyTo(array, 0);
					array = this.ᜀ(array);
					num = 3;
					continue;
				}
				case 17:
					num = 6;
					continue;
				case 18:
					return result;
				case 19:
					num = 13;
					continue;
				case 20:
					num4 = 0;
					goto IL_19A;
				case 21:
					if (num5 != num3)
					{
						num = 1;
						continue;
					}
					num = 2;
					continue;
				}
				goto IL_94;
				IL_D6:
				num2++;
				num = 9;
				continue;
				IL_19A:
				num5 = num4;
				num2 = num5;
				num = 8;
				continue;
				IL_23A:
				if (!flag)
				{
					num = 17;
					continue;
				}
				num = 10;
				continue;
				IL_2C5:
				num = 21;
			}
			return result;
			IL_94:
			result = string.Empty;
			num3 = A_2.LevelNumber;
			num = 7;
			goto IL_35;
			IL_29C:
			str += A_3.ToString();
			result = str + A_2.NumberSufix;
			num = 18;
			goto IL_35;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0003BCE8 File Offset: 0x0003ACE8
		private new int[] ᜀ(int[] A_0)
		{
			for (;;)
			{
				if (true)
				{
				}
				int num = 0;
				int num2 = 4;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
						if (num3 >= A_0.Length)
						{
							num2 = 3;
							continue;
						}
						num2 = 10;
						continue;
					case 1:
					{
						int num4 = A_0[num];
						A_0[num] = A_0[num3];
						A_0[num3] = num4;
						num2 = 9;
						continue;
					}
					case 2:
						goto IL_B1;
					case 3:
						num++;
						num2 = 2;
						continue;
					case 4:
						goto IL_B1;
					case 5:
						goto IL_52;
					case 6:
						if (num >= A_0.Length - 1)
						{
							num2 = 8;
							continue;
						}
						num3 = num + 1;
						num2 = 7;
						continue;
					case 7:
						goto IL_52;
					case 8:
						return A_0;
					case 9:
						goto IL_10F;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_10F;
						default:
							if (false)
							{
							}
							if (A_0[num] > A_0[num3])
							{
								num2 = 1;
								continue;
							}
							goto IL_6A;
						}
						break;
					}
					break;
					IL_52:
					num2 = 0;
					continue;
					IL_6A:
					num3++;
					num2 = 5;
					continue;
					IL_10F:
					goto IL_6A;
					IL_B1:
					num2 = 6;
				}
			}
			return A_0;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0003BE0C File Offset: 0x0003AE0C
		public int ReplaceInLine(string matchString, string newValue, bool caseSensitive, bool wholeWord)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			Regex pattern = spr\u1AB5.ᜀ(matchString, caseSensitive, wholeWord);
			return this.ReplaceInLine(pattern, newValue);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0003BE5C File Offset: 0x0003AE5C
		public int ReplaceInLine(Regex pattern, string newValue)
		{
			int num;
			for (;;)
			{
				BodyRegion a_ = this.Sections[0].Body.Items[0];
				num = this.ᜀ(pattern, newValue, a_);
				if (true)
				{
				}
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_61;
						default:
							goto IL_81;
						}
						break;
					case 1:
						num2 = 2;
						continue;
					case 2:
						if (num > 0)
						{
							num2 = 0;
							continue;
						}
						goto IL_A5;
					case 3:
						if (this.ReplaceFirst)
						{
							goto IL_61;
						}
						goto IL_A5;
					}
					break;
					IL_61:
					num2 = 1;
				}
			}
			IL_81:
			if (false)
			{
			}
			return num;
			IL_A5:
			return num + this.ᜀ(pattern, newValue);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0003BF1C File Offset: 0x0003AF1C
		public int ReplaceInLine(string matchString, TextSelection matchSelection, bool caseSensitive, bool wholeWord)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			Regex pattern = spr\u1AB5.ᜀ(matchString, caseSensitive, wholeWord);
			return this.ReplaceInLine(pattern, matchSelection);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0003BF6C File Offset: 0x0003AF6C
		public int ReplaceInLine(Regex pattern, TextSelection matchSelection)
		{
			int num;
			for (;;)
			{
				num = 0;
				BodyRegion start = this.Sections[0].Body.Items[0];
				TextSelection[] array = this.FindPatternInLine(start, pattern);
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_50;
						default:
							if (false)
							{
							}
							if (array == null)
							{
								num2 = 1;
								continue;
							}
							spr\u21D6.ᜀ().ᜀ(array, matchSelection);
							num++;
							num2 = 5;
							continue;
						}
						break;
					case 1:
						return num;
					case 2:
						goto IL_50;
					case 3:
						array = this.FindPatternInLine(start, pattern);
						if (true)
						{
						}
						num2 = 4;
						continue;
					case 4:
						goto IL_52;
					case 5:
						if (!this.ReplaceFirst)
						{
							num2 = 3;
							continue;
						}
						return num;
					}
					break;
					IL_52:
					num2 = 0;
					continue;
					IL_50:
					goto IL_52;
				}
			}
			return num;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0003C058 File Offset: 0x0003B058
		internal int ᜁ(string A_0, TextBodyPart A_1, bool A_2, bool A_3)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			Regex a_ = spr\u1AB5.ᜀ(A_0, A_2, A_3);
			return this.ᜁ(a_, A_1);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0003C0A8 File Offset: 0x0003B0A8
		internal int ᜁ(Regex A_0, TextBodyPart A_1)
		{
			int num;
			for (;;)
			{
				num = 0;
				BodyRegion start = this.Sections[0].Body.Items[0];
				TextSelection[] array = this.FindPatternInLine(start, A_0);
				int num2 = 4;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return num;
					case 1:
						if (true)
						{
						}
						goto IL_52;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_50;
						default:
							if (false)
							{
							}
							if (array == null)
							{
								num2 = 0;
								continue;
							}
							spr\u21D6.ᜀ().ᜀ(array, A_1);
							num++;
							num2 = 5;
							continue;
						}
						break;
					case 3:
						array = this.FindPatternInLine(start, A_0);
						num2 = 1;
						continue;
					case 4:
						goto IL_50;
					case 5:
						if (!this.ReplaceFirst)
						{
							num2 = 3;
							continue;
						}
						return num;
					}
					break;
					IL_52:
					num2 = 2;
					continue;
					IL_50:
					goto IL_52;
				}
			}
			return num;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0003C194 File Offset: 0x0003B194
		private new int ᜀ(Regex A_0, string A_1)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				this.ᜇ();
				int num = 0;
				IEnumerator enumerator = this.Sections.GetEnumerator();
				try
				{
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							if (!enumerator.MoveNext())
							{
								num2 = 3;
								continue;
							}
							Section section = (Section)enumerator.Current;
							IEnumerator enumerator2 = section.HeadersFooters.GetEnumerator();
							num2 = 4;
							continue;
						}
						case 2:
							goto IL_1A8;
						case 3:
							goto IL_19C;
						case 4:
							try
							{
								num2 = 2;
								for (;;)
								{
									switch (num2)
									{
									case 0:
									{
										IEnumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num2 = 5;
											continue;
										}
										HeaderFooter headerFooter = (HeaderFooter)enumerator2.Current;
										num2 = 1;
										continue;
									}
									case 1:
									{
										HeaderFooter headerFooter;
										if (headerFooter.Items.Count > 0)
										{
											num2 = 3;
											continue;
										}
										break;
									}
									case 3:
									{
										HeaderFooter headerFooter;
										num += this.ᜀ(A_0, A_1, headerFooter.Items[0]);
										num2 = 4;
										continue;
									}
									case 5:
										num2 = 6;
										continue;
									case 6:
										goto IL_14E;
									}
									IL_D1:
									num2 = 0;
									continue;
									goto IL_D1;
								}
								IL_14E:
								break;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator2;
									IDisposable disposable = enumerator2 as IDisposable;
									num2 = 1;
									for (;;)
									{
										switch (num2)
										{
										case 0:
											goto IL_199;
										case 1:
											if (disposable != null)
											{
												num2 = 2;
												continue;
											}
											goto IL_19B;
										case 2:
											disposable.Dispose();
											num2 = 0;
											continue;
										}
										break;
									}
								}
								IL_199:
								IL_19B:;
							}
							goto IL_19C;
						}
						IL_82:
						num2 = 0;
						continue;
						goto IL_82;
						IL_19C:
						num2 = 2;
					}
					IL_1A8:;
				}
				finally
				{
					for (;;)
					{
						IL_1BF:
						IDisposable disposable2 = enumerator as IDisposable;
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								if (disposable2 != null)
								{
									num2 = 2;
									continue;
								}
								goto IL_1F1;
							case 1:
								goto IL_1EF;
							case 2:
								disposable2.Dispose();
								num2 = 1;
								continue;
							}
							goto IL_1BF;
						}
						IL_1F1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							goto IL_207;
						}
						IL_1EF:
						goto IL_1F1;
					}
					IL_207:
					if (false)
					{
					}
				}
				this.ᜇ();
				return num;
			}
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0003C3EC File Offset: 0x0003B3EC
		private void ᜇ()
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ResetFindState();
			spr\u25C5.ᜀ().ᜁ().Clear();
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0003C43C File Offset: 0x0003B43C
		private new int ᜀ(Regex A_0, string A_1, BodyRegion A_2)
		{
			int num;
			for (;;)
			{
				num = 0;
				TextSelection[] array = this.FindPatternInLine(A_2, A_0);
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_33;
					case 1:
						goto IL_64;
					case 2:
						if (!this.ReplaceFirst)
						{
							num2 = 3;
							continue;
						}
						goto IL_A9;
					case 3:
						array = this.FindPatternInLine(A_2, A_0);
						num2 = 4;
						continue;
					case 4:
						goto IL_35;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_33;
						default:
							if (false)
							{
							}
							if (array == null)
							{
								num2 = 1;
								continue;
							}
							spr\u21D6.ᜀ().ᜀ(array, A_1);
							num++;
							num2 = 2;
							continue;
						}
						break;
					}
					break;
					IL_35:
					num2 = 5;
					continue;
					IL_33:
					goto IL_35;
				}
			}
			IL_64:
			IL_A9:
			if (true)
			{
			}
			return num;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0003C508 File Offset: 0x0003B508
		public TextSelection FindString(BodyRegion start, string matchString, bool caseSensitive, bool wholeWord)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			Regex pattern = spr\u1AB5.ᜀ(matchString, caseSensitive, wholeWord);
			return this.FindPattern(start, pattern);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0003C558 File Offset: 0x0003B558
		public TextSelection FindPattern(BodyRegion start, Regex pattern)
		{
			int a_ = 1;
			int num = 4;
			TextSelection textSelection;
			for (;;)
			{
				BodyRegion bodyRegion;
				switch (num)
				{
				case 0:
					goto IL_152;
				case 1:
					goto IL_12A;
				case 2:
					if (this.\u1733 != null)
					{
						num = 10;
						continue;
					}
					goto IL_1D4;
				case 3:
					if (textSelection != null)
					{
						num = 5;
						continue;
					}
					start = this.\u1733.OwnerParagraph.NextTextBodyItem;
					num = 11;
					continue;
				case 5:
					goto IL_276;
				case 6:
					goto IL_22D;
				case 7:
					if (this.ᜀ(textSelection))
					{
						num = 16;
						continue;
					}
					bodyRegion = bodyRegion.NextTextBodyItem;
					num = 15;
					continue;
				case 8:
					goto IL_22D;
				case 9:
					this.\u1733 = null;
					this.\u1734 = start;
					num = 8;
					continue;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_276;
					default:
						if (false)
						{
						}
						num = 17;
						continue;
					}
					break;
				case 11:
					if (start == null)
					{
						num = 1;
						continue;
					}
					goto IL_1D4;
				case 12:
					this.\u1734 = start;
					num = 6;
					continue;
				case 13:
					textSelection = this.ᜁ(pattern);
					num = 3;
					continue;
				case 14:
					goto IL_AD;
				case 15:
					if (bodyRegion == null)
					{
						num = 0;
						continue;
					}
					goto IL_AD;
				case 16:
					goto IL_D4;
				case 17:
					if (this.\u1733.OwnerParagraph != null)
					{
						num = 13;
						continue;
					}
					goto IL_1D4;
				case 18:
					goto IL_78;
				case 19:
					if (this.\u1734 == null)
					{
						num = 12;
						continue;
					}
					num = 20;
					continue;
				case 20:
					if (true)
					{
					}
					if (this.\u1734 != start)
					{
						num = 9;
						continue;
					}
					goto IL_22D;
				}
				if (start == null)
				{
					num = 18;
					continue;
				}
				num = 19;
				continue;
				IL_AD:
				textSelection = bodyRegion.Find(pattern);
				num = 7;
				continue;
				IL_1D4:
				bodyRegion = start;
				num = 14;
				continue;
				IL_22D:
				textSelection = null;
				num = 2;
			}
			IL_78:
			throw new ArgumentException(ClipboardData.b("㑦ᵨ੪Ὤ᭮兰ᅲᩴ፶x孺ᑼ୾ꖄꪌﮎ놐랖", a_), ClipboardData.b("ᑦᵨ੪Ὤ᭮", a_));
			IL_D4:
			textSelection.GetAsOneRange();
			this.ᜁ(textSelection);
			return textSelection;
			IL_12A:
			this.\u1733 = null;
			return null;
			IL_152:
			return null;
			IL_276:
			textSelection.GetAsOneRange();
			this.ᜁ(textSelection);
			return textSelection;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0003C7E4 File Offset: 0x0003B7E4
		private TextSelection ᜁ(Regex A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					Paragraph ownerParagraph = this.\u1733.OwnerParagraph;
					spr\u226E spr_u226E = ownerParagraph.FindAll(A_0);
					int num = 0;
					for (;;)
					{
						int num2;
						int num3;
						List<TextSelection>.Enumerator enumerator;
						switch (num)
						{
						case 0:
							if (spr_u226E.Count > 0)
							{
								num = 1;
								continue;
							}
							goto IL_197;
						case 1:
							goto IL_168;
						case 2:
							try
							{
								num = 7;
								TextSelection result;
								for (;;)
								{
									TextSelection textSelection;
									switch (num)
									{
									case 0:
										num = 4;
										continue;
									case 1:
										if (num2 <= num3)
										{
											num = 6;
											continue;
										}
										break;
									case 2:
										goto IL_10F;
									case 3:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_E8;
										default:
											if (false)
											{
											}
											if (this.ᜀ(textSelection))
											{
												num = 8;
												continue;
											}
											break;
										}
										break;
									case 4:
										goto IL_158;
									case 5:
										goto IL_E8;
									case 6:
										result = textSelection;
										num = 2;
										continue;
									case 8:
										textSelection.StartTextRange.ឯ();
										num3 = textSelection.EndTextRange.ឯ();
										num = 1;
										continue;
									}
									goto IL_9A;
									IL_E8:
									if (!enumerator.MoveNext())
									{
										num = 0;
										continue;
									}
									textSelection = enumerator.Current;
									num = 3;
									continue;
									IL_DF:
									num = 5;
									continue;
									IL_9A:
									goto IL_DF;
								}
								IL_10F:
								return result;
								IL_158:
								goto IL_197;
							}
							finally
							{
								((IDisposable)enumerator).Dispose();
							}
							goto IL_168;
						}
						break;
						IL_168:
						num2 = this.\u1733.ឯ();
						num3 = 0;
						enumerator = spr_u226E.GetEnumerator();
						if (true)
						{
						}
						num = 2;
					}
				}
				IL_197:
				return null;
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0003C99C File Offset: 0x0003B99C
		private void ᜁ(TextSelection A_0)
		{
			TextRange textRange;
			for (;;)
			{
				this.\u1733 = null;
				TextRange[] ranges = A_0.GetRanges();
				int num = 0;
				for (;;)
				{
					BodyRegion bodyRegion;
					switch (num)
					{
					case 0:
						if (ranges != null)
						{
							num = 8;
							continue;
						}
						goto IL_7E;
					case 1:
						if (bodyRegion.NextTextBodyItem == null)
						{
							num = 5;
							continue;
						}
						bodyRegion = bodyRegion.NextTextBodyItem;
						this.\u1733 = this.ᜀ(bodyRegion);
						num = 6;
						continue;
					case 2:
						return;
					case 3:
						if (textRange.NextSibling != null)
						{
							num = 7;
							continue;
						}
						goto IL_7E;
					case 4:
						goto IL_BA;
					case 5:
						goto IL_D8;
					case 6:
						if (this.\u1733 != null)
						{
							num = 2;
							continue;
						}
						goto IL_BA;
					case 7:
						goto IL_B8;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							textRange = ranges[ranges.Length - 1];
							num = 3;
							continue;
						}
						break;
					}
					break;
					IL_7E:
					bodyRegion = A_0.OwnerParagraph;
					num = 4;
					continue;
					IL_BA:
					num = 1;
				}
			}
			IL_B8:
			this.\u1733 = (textRange.NextSibling as ParagraphBase);
			return;
			IL_D8:;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0003CACC File Offset: 0x0003BACC
		private new ParagraphBase ᜀ(BodyRegion A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 6;
				for (;;)
				{
					ParagraphBase paragraphBase;
					IEnumerator enumerator2;
					switch (num)
					{
					case 0:
						goto IL_D3;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_69;
						}
						break;
					case 2:
						if (A_0.NextSibling != null)
						{
							num = 8;
							continue;
						}
						goto IL_123;
					case 3:
						goto IL_2D9;
					case 4:
						if (A_0 is Table)
						{
							num = 3;
							continue;
						}
						num = 5;
						continue;
					case 5:
						if ((A_0 as Paragraph).Items.Count > 0)
						{
							num = 0;
							continue;
						}
						num = 2;
						continue;
					case 7:
						try
						{
							num = 4;
							for (;;)
							{
								IEnumerator enumerator;
								switch (num)
								{
								case 0:
									try
									{
										num = 4;
										ParagraphBase result;
										for (;;)
										{
											switch (num)
											{
											case 0:
												result = paragraphBase;
												num = 6;
												continue;
											case 1:
												num = 3;
												continue;
											case 2:
												if (paragraphBase != null)
												{
													num = 0;
													continue;
												}
												break;
											case 3:
												goto IL_206;
											case 5:
											{
												if (!enumerator.MoveNext())
												{
													num = 1;
													continue;
												}
												TableCell a_ = (TableCell)enumerator.Current;
												paragraphBase = this.ᜀ(a_);
												num = 2;
												continue;
											}
											case 6:
												goto IL_1D8;
											}
											IL_1DD:
											num = 5;
											continue;
											goto IL_1DD;
										}
										IL_1D8:
										return result;
										IL_206:
										break;
									}
									finally
									{
										for (;;)
										{
											IDisposable disposable = enumerator as IDisposable;
											num = 1;
											for (;;)
											{
												switch (num)
												{
												case 0:
													disposable.Dispose();
													num = 2;
													continue;
												case 1:
													if (disposable != null)
													{
														num = 0;
														continue;
													}
													goto IL_253;
												case 2:
													goto IL_251;
												}
												break;
											}
										}
										IL_251:
										IL_253:;
									}
									goto IL_254;
								case 1:
									goto IL_28B;
								case 2:
									num = 1;
									continue;
								case 3:
									if (!enumerator2.MoveNext())
									{
										num = 2;
										continue;
									}
									goto IL_254;
								}
								IL_14D:
								num = 3;
								continue;
								goto IL_14D;
								IL_254:
								TableRow tableRow = (TableRow)enumerator2.Current;
								enumerator = tableRow.Cells.GetEnumerator();
								num = 0;
							}
							IL_28B:
							goto IL_123;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable2 = enumerator2 as IDisposable;
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_2D6;
									case 1:
										if (disposable2 != null)
										{
											num = 2;
											continue;
										}
										goto IL_2D8;
									case 2:
										disposable2.Dispose();
										num = 0;
										continue;
									}
									break;
								}
							}
							IL_2D6:
							IL_2D8:;
						}
						goto IL_2D9;
					case 8:
						goto IL_A6;
					}
					if (A_0 == null)
					{
						num = 1;
						continue;
					}
					num = 4;
					continue;
					IL_2D9:
					paragraphBase = null;
					Table table = A_0 as Table;
					enumerator2 = table.Rows.GetEnumerator();
					num = 7;
				}
				IL_69:
				if (true)
				{
				}
				if (false)
				{
				}
				return null;
				IL_A6:
				return this.ᜀ(A_0.NextSibling as BodyRegion);
				IL_D3:
				return (A_0 as Paragraph).Items[0];
				IL_123:
				return null;
			}
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0003CE10 File Offset: 0x0003BE10
		private new ParagraphBase ᜀ(Body A_0)
		{
			for (;;)
			{
				switch (0)
				{
				default:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_24;
					}
					break;
				}
			}
			IL_24:
			if (false)
			{
			}
			ParagraphBase paragraphBase = null;
			IEnumerator enumerator = A_0.Items.GetEnumerator();
			ParagraphBase result;
			try
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (!enumerator.MoveNext())
						{
							num = 4;
							continue;
						}
						BodyRegion bodyRegion = (BodyRegion)enumerator.Current;
						paragraphBase = this.ᜀ(bodyRegion);
						num = 1;
						continue;
					}
					case 1:
					{
						if (true)
						{
						}
						BodyRegion bodyRegion;
						if (bodyRegion != null)
						{
							num = 2;
							continue;
						}
						break;
					}
					case 2:
						result = paragraphBase;
						num = 6;
						continue;
					case 4:
						num = 5;
						continue;
					case 5:
						goto IL_DE;
					case 6:
						goto IL_D0;
					}
					IL_76:
					num = 0;
					continue;
					goto IL_76;
				}
				IL_D0:
				return result;
				IL_DE:
				goto IL_44;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							disposable.Dispose();
							num = 1;
							continue;
						case 1:
							goto IL_128;
						case 2:
							if (disposable != null)
							{
								num = 0;
								continue;
							}
							goto IL_12A;
						}
						break;
					}
				}
				IL_128:
				IL_12A:;
			}
			return result;
			IL_44:
			return null;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0003CF5C File Offset: 0x0003BF5C
		private new bool ᜀ(TextSelection A_0)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5B;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 2:
					goto IL_5B;
				case 3:
					return true;
				}
				if (true)
				{
				}
				if (A_0 != null)
				{
					num = 1;
					continue;
				}
				return false;
				IL_5B:
				if (A_0.Count <= 0)
				{
					return false;
				}
				num = 3;
			}
			return true;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0003CFE4 File Offset: 0x0003BFE4
		public TextSelection[] FindStringInLine(BodyRegion start, string matchString, bool caseSensitive, bool wholeWord)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			Regex pattern = spr\u1AB5.ᜀ(matchString, caseSensitive, wholeWord);
			return this.FindPatternInLine(start, pattern);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0003D034 File Offset: 0x0003C034
		public TextSelection[] FindPatternInLine(BodyRegion start, Regex pattern)
		{
			int a_ = 10;
			int num = 0;
			TextSelection[] array;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (this.\u1734 == null)
					{
						num = 3;
						continue;
					}
					num = 5;
					continue;
				case 2:
					goto IL_126;
				case 3:
					this.\u1734 = start;
					num = 2;
					continue;
				case 4:
					goto IL_58;
				case 5:
					if (this.\u1734 != start)
					{
						num = 8;
						continue;
					}
					goto IL_126;
				case 6:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						this.\u1733 = this.ᜀ(start);
						num = 11;
						continue;
					}
					break;
				case 7:
					goto IL_126;
				case 8:
					this.\u1733 = null;
					this.\u1734 = start;
					num = 7;
					continue;
				case 9:
					goto IL_102;
				case 10:
					if (array != null)
					{
						num = 9;
						continue;
					}
					goto IL_1A3;
				case 11:
					goto IL_DE;
				case 12:
					if (this.\u1733 == null)
					{
						num = 6;
						continue;
					}
					goto IL_DE;
				}
				IL_4D:
				if (start == null)
				{
					num = 4;
					continue;
				}
				num = 1;
				continue;
				goto IL_4D;
				IL_DE:
				array = this.ᜀ(pattern);
				num = 10;
				continue;
				IL_126:
				array = null;
				num = 12;
			}
			IL_58:
			throw new ArgumentException(ClipboardData.b("⍯ٱᕳѵ౷婹ṻᅽﮁꒃﲇ꺍望놕몙ﺛﮝ肟첡톣쪥쒧", a_), ClipboardData.b("ͯٱᕳѵ౷", a_));
			IL_102:
			TextSelection textSelection = array[array.Length - 1];
			textSelection.GetAsOneRange();
			this.ᜁ(textSelection);
			return array;
			IL_1A3:
			this.\u1733 = null;
			return null;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0003D1EC File Offset: 0x0003C1EC
		private new TextSelection[] ᜀ(Regex A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 18;
				for (;;)
				{
					BodyRegion bodyRegion;
					TextSelection[] array;
					int num2;
					Paragraph ownerParagraph;
					switch (num)
					{
					case 0:
						bodyRegion = bodyRegion.NextTextBodyItem;
						num = 6;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1B7;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							goto IL_99;
						}
						break;
					case 2:
						if (array == null)
						{
							num = 21;
							continue;
						}
						return array;
					case 3:
					{
						Body ownerTextBody;
						array = spr\u25C5.ᜀ().ᜀ(ownerTextBody, A_0, num2 + 1, ownerTextBody.Items.Count - 1);
						num = 19;
						continue;
					}
					case 4:
					{
						Body ownerTextBody;
						if (ownerTextBody != null)
						{
							num = 3;
							continue;
						}
						goto IL_1D0;
					}
					case 5:
						if (num2 == 0)
						{
							num = 9;
							continue;
						}
						goto IL_1F3;
					case 6:
						goto IL_115;
					case 7:
						if (bodyRegion.ឯ() == 0)
						{
							num = 14;
							continue;
						}
						goto IL_99;
					case 8:
						goto IL_94;
					case 9:
						spr\u25C5.ᜀ().ᜁ().Clear();
						num = 23;
						continue;
					case 10:
						if (this.\u1733 == null)
						{
							num = 0;
							continue;
						}
						goto IL_23A;
					case 11:
						if (bodyRegion != null)
						{
							num = 15;
							continue;
						}
						return array;
					case 12:
						goto IL_23A;
					case 13:
						return array;
					case 14:
						spr\u25C5.ᜀ().ᜁ().Clear();
						num = 1;
						continue;
					case 15:
						goto IL_1B7;
					case 16:
					{
						Body ownerTextBody = ownerParagraph.OwnerTextBody;
						num = 4;
						continue;
					}
					case 17:
						if (bodyRegion == null)
						{
							num = 12;
							continue;
						}
						num = 7;
						continue;
					case 19:
						goto IL_1D0;
					case 20:
						if (array == null)
						{
							num = 16;
							continue;
						}
						return array;
					case 21:
					{
						Body ownerTextBody;
						BodyRegion bodyRegion2 = ownerTextBody.Items[ownerTextBody.Items.Count - 1];
						bodyRegion = bodyRegion2.NextTextBodyItem;
						num = 22;
						continue;
					}
					case 22:
						goto IL_115;
					case 23:
						goto IL_1F3;
					}
					if (this.\u1733 == null)
					{
						num = 8;
						continue;
					}
					ownerParagraph = this.\u1733.OwnerParagraph;
					num2 = ownerParagraph.ឯ();
					num = 5;
					continue;
					IL_99:
					this.\u1733 = this.ᜀ(bodyRegion);
					num = 10;
					continue;
					IL_115:
					num = 17;
					continue;
					IL_1B7:
					array = this.ᜀ(A_0);
					num = 13;
					continue;
					IL_1D0:
					num = 2;
					continue;
					IL_1F3:
					int a_ = this.\u1733.ឯ();
					array = spr\u25C5.ᜀ().ᜀ(ownerParagraph, A_0, a_, ownerParagraph.Items.Count - 1);
					num = 20;
					continue;
					IL_23A:
					num = 11;
				}
				IL_94:
				return null;
			}
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0003D504 File Offset: 0x0003C504
		public void ResetFindState()
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.\u1733 = null;
			this.\u1734 = null;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0003D550 File Offset: 0x0003C550
		public ParagraphBase CreateParagraphItem(ParagraphItemType itemType)
		{
			int a_ = 15;
			for (;;)
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_94;
					case 1:
						switch (itemType)
						{
						case ParagraphItemType.TextRange:
							goto IL_E3;
						case ParagraphItemType.Picture:
							goto IL_12B;
						case ParagraphItemType.Field:
							goto IL_132;
						case ParagraphItemType.FieldMark:
							goto IL_C6;
						case ParagraphItemType.MergeField:
							goto IL_139;
						case ParagraphItemType.FormField:
						case ParagraphItemType.SeqField:
						case ParagraphItemType.ControlField:
							goto IL_163;
						case ParagraphItemType.CheckBox:
							goto IL_15C;
						case ParagraphItemType.TextFormField:
							goto IL_147;
						case ParagraphItemType.DropDownFormField:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_94;
							default:
								goto IL_B9;
							}
							break;
						case ParagraphItemType.EmbedField:
							goto IL_14E;
						case ParagraphItemType.BookmarkStart:
							goto IL_D5;
						case ParagraphItemType.BookmarkEnd:
							goto IL_FF;
						case ParagraphItemType.ShapeObject:
							goto IL_DC;
						case ParagraphItemType.InlineShapeObject:
							goto IL_106;
						case ParagraphItemType.Comment:
							goto IL_EA;
						case ParagraphItemType.Footnote:
							goto IL_140;
						case ParagraphItemType.TextBox:
							goto IL_114;
						case ParagraphItemType.Break:
							goto IL_155;
						case ParagraphItemType.Symbol:
							goto IL_F8;
						case ParagraphItemType.TOC:
							goto IL_F1;
						case ParagraphItemType.OleObject:
							goto IL_10D;
						default:
							num = 0;
							continue;
						}
						break;
					case 2:
						goto IL_126;
					}
					break;
					IL_94:
					num = 2;
				}
			}
			IL_B9:
			if (false)
			{
			}
			return new DropDownFormField(this);
			IL_C6:
			if (true)
			{
			}
			return new FieldMark(this);
			IL_D5:
			return new BookmarkStart(this);
			IL_DC:
			return new spr\u248F(this);
			IL_E3:
			return new TextRange(this);
			IL_EA:
			return new Comment(this);
			IL_F1:
			return new TableOfContent(this);
			IL_F8:
			return new Symbol(this);
			IL_FF:
			return new BookmarkEnd(this);
			IL_106:
			return new sprẛ(this);
			IL_10D:
			return new DocOleObject(this);
			IL_114:
			return new Spire.Doc.Fields.TextBox(this);
			IL_126:
			goto IL_163;
			IL_12B:
			return new DocPicture(this);
			IL_132:
			return new Field(this);
			IL_139:
			return new MergeField(this);
			IL_140:
			return new Footnote(this);
			IL_147:
			return new TextFormField(this);
			IL_14E:
			return new sprᶖ(this);
			IL_155:
			return new Break(this);
			IL_15C:
			return new CheckBoxFormField(this);
			IL_163:
			throw new ArgumentException(ClipboardData.b("㱴Ŷᡸ᝺ᑼ᭾ꆀﲄꮊ놐ﲚﺞ토쮢薤캦\udda8캪사", a_));
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0003D6D4 File Offset: 0x0003C6D4
		protected override object CloneImpl()
		{
			if (true)
			{
			}
			object result;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				lock (this.ᜧ)
				{
					result = new Document(this);
				}
				break;
			}
			return result;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0003D740 File Offset: 0x0003C740
		protected internal CharacterFormat CreateCharacterFormatImpl()
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return new CharacterFormat(this);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0003D784 File Offset: 0x0003C784
		protected internal ListStyle CreateListStyleImpl()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return new ListStyle(this);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0003D7C8 File Offset: 0x0003C7C8
		protected internal ListLevel CreateListLevelImpl(ListStyle style)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return new ListLevel(style);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0003D80C File Offset: 0x0003C80C
		protected internal ParagraphFormat CreateParagraphFormatImpl()
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return new ParagraphFormat(this);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0003D850 File Offset: 0x0003C850
		protected internal RowFormat CreateTableFormatImpl()
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return new RowFormat();
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0003D890 File Offset: 0x0003C890
		protected internal CellFormat CreateCellFormatImpl()
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return new CellFormat();
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0003D8D0 File Offset: 0x0003C8D0
		protected internal TextBoxFormat CreateTextboxFormatImpl()
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return new TextBoxFormat();
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0003D910 File Offset: 0x0003C910
		protected internal TextBoxItemCollection CreateTextBoxCollectionImpl()
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return new TextBoxItemCollection(this);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0003D954 File Offset: 0x0003C954
		protected internal ListFormat CreateListFormatImpl(IParagraph owner)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return new ListFormat(owner);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0003D998 File Offset: 0x0003C998
		internal spr\u21F4 ᝌ()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return new spr\u20BF();
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0003D9DC File Offset: 0x0003C9DC
		internal spr\u21F4 ᜊ(Stream A_0)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return new spr\u20BF(A_0);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0003DA20 File Offset: 0x0003CA20
		internal bool ᜉ(Stream A_0)
		{
			int a_ = 16;
			int num = 0;
			bool result;
			for (;;)
			{
				switch (num)
				{
				case 1:
					result = true;
					num = 2;
					continue;
				case 2:
					goto IL_80;
				case 3:
					if (spr\u20BF.ᜁ(A_0))
					{
						num = 1;
						continue;
					}
					goto IL_80;
				case 4:
					goto IL_38;
				}
				goto IL_2D;
				IL_30:
				num = 4;
				continue;
				IL_2D:
				if (A_0 == null)
				{
					goto IL_30;
				}
				result = false;
				num = 3;
				continue;
				IL_80:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_30;
				default:
					goto IL_96;
				}
			}
			IL_38:
			throw new ArgumentNullException(ClipboardData.b("յ౷ࡹ᥻ώ", a_));
			IL_96:
			if (true)
			{
			}
			if (false)
			{
			}
			return result;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0003DAD4 File Offset: 0x0003CAD4
		internal new void ᜀ(IParagraph A_0)
		{
			int a_ = 15;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_EC;
					default:
						if (false)
						{
						}
						if (this.Styles.FindByName(ClipboardData.b("㭴ᡶ୸ᙺᱼ፾", a_)) == null)
						{
							num = 4;
							continue;
						}
						goto IL_46;
					}
					break;
				case 2:
					num = 1;
					continue;
				case 3:
					goto IL_46;
				case 4:
					goto IL_EC;
				}
				if (A_0.StyleName == null)
				{
					num = 2;
					continue;
				}
				break;
				IL_46:
				if (true)
				{
				}
				A_0.ApplyStyle(ClipboardData.b("㭴ᡶ୸ᙺᱼ፾", a_));
				num = 0;
				continue;
				IL_EC:
				this.ᜀ(StyleType.ParagraphStyle, ClipboardData.b("㭴ᡶ୸ᙺᱼ፾", a_));
				num = 3;
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0003DBD0 File Offset: 0x0003CBD0
		internal new void ᜀ(Document A_0, IParagraphBase A_1)
		{
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1 is spr\u248F)
					{
						goto IL_7F;
					}
					goto IL_12E;
				case 1:
					if (A_1 is IPicture)
					{
						num = 11;
						continue;
					}
					num = 7;
					continue;
				case 2:
					if (A_0.\u1719 == null)
					{
						num = 5;
						continue;
					}
					goto IL_188;
				case 3:
					num = 2;
					continue;
				case 4:
					goto IL_188;
				case 5:
					A_0.\u1719 = new spr\u24E3(A_0);
					num = 4;
					continue;
				case 6:
					this.ᜀ(A_0, A_1 as Spire.Doc.Fields.TextBox);
					num = 10;
					continue;
				case 7:
					if (A_1 is ITextBox)
					{
						num = 6;
						continue;
					}
					num = 0;
					continue;
				case 8:
					if (true)
					{
					}
					goto IL_12E;
				case 10:
					goto IL_12E;
				case 11:
					this.ᜀ(A_0, A_1 as DocPicture);
					num = 12;
					continue;
				case 12:
					goto IL_12E;
				case 13:
					if (A_1 != null)
					{
						num = 16;
						continue;
					}
					return;
				case 14:
					this.ᜀ(A_0, A_1 as spr\u248F);
					num = 8;
					continue;
				case 15:
					return;
				case 16:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7F;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				if (this.Escher != null)
				{
					num = 3;
					continue;
				}
				break;
				IL_7F:
				num = 14;
				continue;
				IL_12E:
				this.ᜠ++;
				num = 15;
				continue;
				IL_188:
				num = 13;
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0003DDA0 File Offset: 0x0003CDA0
		internal string \u1734()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.\u171A;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0003DDE4 File Offset: 0x0003CDE4
		internal new void ᜀ(WatermarkType A_0)
		{
			for (;;)
			{
				this.ᜁ();
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						if (A_0 != WatermarkType.NoWatermark)
						{
							num = 7;
							continue;
						}
						goto IL_72;
					case 1:
						if (A_0 == WatermarkType.TextWatermark)
						{
							num = 6;
							continue;
						}
						goto IL_FA;
					case 2:
						this.\u1719 = new spr\u24E3(this);
						num = 8;
						continue;
					case 3:
						if (A_0 == WatermarkType.PictureWatermark)
						{
							num = 4;
							continue;
						}
						num = 1;
						continue;
					case 4:
						goto IL_89;
					case 5:
						if (this.\u1719 == null)
						{
							num = 2;
							continue;
						}
						goto IL_72;
					case 6:
						goto IL_70;
					case 7:
						goto IL_4D;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4D;
						default:
							if (false)
							{
							}
							goto IL_72;
						}
						break;
					}
					break;
					IL_4D:
					num = 5;
					continue;
					IL_72:
					num = 3;
				}
			}
			IL_70:
			this.\u1713 = new TextWatermark(this);
			return;
			IL_89:
			this.\u1713 = new PictureWatermark(this);
			return;
			IL_FA:
			this.\u1713 = new WatermarkBase(this, A_0);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0003DEF8 File Offset: 0x0003CEF8
		internal void \u171A()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_58;
					}
					break;
				case 2:
					this.\u1714 = new Background(this);
					num = 0;
					continue;
				}
				if (this.\u1719 == null)
				{
					return;
				}
				num = 2;
			}
			IL_58:
			if (false)
			{
			}
			if (true)
			{
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0003DF78 File Offset: 0x0003CF78
		internal new void ᜀ(MemoryStream A_0)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜆ(A_0);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0003DFBC File Offset: 0x0003CFBC
		internal bool \u173C()
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				if (this.m_listStyles.Count <= 0)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0003E00C File Offset: 0x0003D00C
		private void ᜆ()
		{
			int a_ = 8;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
				{
					if (false)
					{
					}
					this.ᜬ = new ParagraphFormat(this);
					ParagraphStyle paragraphStyle = this.Styles.FindByName(ClipboardData.b("⁭Ὧqᥳ᝵ᑷ", a_), StyleType.ParagraphStyle) as ParagraphStyle;
					if (true)
					{
					}
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (paragraphStyle != null)
							{
								num = 2;
								continue;
							}
							return;
						case 1:
							return;
						case 2:
							this.ᜬ.ImportContainer(paragraphStyle.ParagraphFormat);
							this.ᜬ.ᜃ(paragraphStyle.ParagraphFormat);
							num = 1;
							continue;
						}
						break;
					}
					break;
				}
				}
			}
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0003E0D4 File Offset: 0x0003D0D4
		private void ᜅ()
		{
			for (;;)
			{
				this.ᜄ = false;
				this.ᜑ = new MailMerge(this);
				this.\u1712 = new ViewSetup(this);
				this.\u1715 = new spr\u202E();
				this.m_sections = new SectionCollection(this);
				this.m_styles = new StyleCollection(this);
				this.m_listStyles = new ListStyleCollection(this);
				this.ᜌ = new spr\u1B79(this);
				this.ᜏ = new TextBoxCollection(this);
				this.\u1713 = new WatermarkBase(this, WatermarkType.NoWatermark);
				this.\u1714 = new Background(BackgroundType.NoBackground);
				this.ᜊ = new BuiltinDocumentProperties();
				this.ᜋ = new CustomDocumentProperties();
				int num = 25;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜐ != null)
						{
							num = 49;
							continue;
						}
						goto IL_254;
					case 1:
						if (this.\u173B != null)
						{
							num = 62;
							continue;
						}
						goto IL_51C;
					case 2:
						if (this.\u1719 != null)
						{
							num = 5;
							continue;
						}
						goto IL_746;
					case 3:
						this.ᝊ.Clear();
						num = 37;
						continue;
					case 4:
						this.\u1739.Clear();
						num = 54;
						continue;
					case 5:
						this.\u1719.ᜌ();
						this.\u1719 = null;
						num = 61;
						continue;
					case 6:
						if (this.\u1737 != null)
						{
							num = 19;
							continue;
						}
						goto IL_2A0;
					case 7:
						this.ᝎ.ᜀ().Clear();
						num = 42;
						continue;
					case 8:
						goto IL_5FD;
					case 9:
						goto IL_3BD;
					case 10:
						if (this.ClonedFields != null)
						{
							num = 27;
							continue;
						}
						goto IL_7F0;
					case 11:
						if (this.\u170D != null)
						{
							num = 44;
							continue;
						}
						goto IL_405;
					case 12:
						if (this.\u1736 != null)
						{
							num = 22;
							continue;
						}
						goto IL_53F;
					case 13:
						goto IL_664;
					case 14:
						goto IL_2C6;
					case 15:
						this.\u1758.ᜀ();
						this.\u1758 = null;
						num = 43;
						continue;
					case 16:
						this.\u173B.Clear();
						if (true)
						{
						}
						num = 32;
						continue;
					case 17:
						goto IL_51C;
					case 18:
						if (this.ᝎ != null)
						{
							num = 7;
							continue;
						}
						goto IL_703;
					case 19:
						this.\u1737.Clear();
						num = 53;
						continue;
					case 20:
						this.ᝍ.Clear();
						num = 8;
						continue;
					case 21:
						this.ᜫ.Close();
						this.ᜫ = null;
						num = 51;
						continue;
					case 22:
						goto IL_623;
					case 23:
						if (this.ᝃ != null)
						{
							num = 36;
							continue;
						}
						goto IL_3BD;
					case 24:
						if (this.ᜫ != null)
						{
							num = 21;
							continue;
						}
						goto IL_27A;
					case 25:
						if (this.ᝄ != null)
						{
							num = 60;
							continue;
						}
						goto IL_63E;
					case 26:
						goto IL_63E;
					case 27:
						this.ClonedFields.Clear();
						num = 41;
						continue;
					case 28:
						this.\u1738.Clear();
						num = 13;
						continue;
					case 29:
						this.ᜬ.Close();
						this.ᜬ = null;
						num = 58;
						continue;
					case 30:
						goto IL_4F6;
					case 31:
						if (this.ᝊ != null)
						{
							num = 3;
							continue;
						}
						goto IL_363;
					case 32:
						goto IL_565;
					case 33:
						if (this.\u1739 != null)
						{
							num = 4;
							continue;
						}
						goto IL_68A;
					case 34:
						goto IL_7CD;
					case 35:
						if (this.ᜎ != null)
						{
							num = 45;
							continue;
						}
						goto IL_7CD;
					case 36:
						this.ᝃ.Clear();
						num = 9;
						continue;
					case 37:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_623;
						default:
							if (false)
							{
							}
							goto IL_363;
						}
						break;
					case 38:
						if (this.ᝍ != null)
						{
							num = 20;
							continue;
						}
						goto IL_5FD;
					case 39:
						this.\u173A.Clear();
						num = 30;
						continue;
					case 40:
						goto IL_254;
					case 41:
						goto IL_328;
					case 42:
						goto IL_703;
					case 43:
						goto IL_213;
					case 44:
						this.\u170D.Clear();
						num = 59;
						continue;
					case 45:
						this.ᜎ.ᜀ();
						num = 34;
						continue;
					case 46:
						if (this.\u1738 != null)
						{
							num = 28;
							continue;
						}
						goto IL_664;
					case 47:
						if (this.\u1758 != null)
						{
							num = 15;
							continue;
						}
						goto IL_213;
					case 48:
						if (this.ᝋ != null)
						{
							num = 57;
							continue;
						}
						goto IL_2C6;
					case 49:
						this.ᜐ.Clear();
						num = 40;
						continue;
					case 50:
						if (this.\u173B != null)
						{
							num = 16;
							continue;
						}
						goto IL_565;
					case 51:
						goto IL_27A;
					case 52:
						if (this.ᜬ != null)
						{
							num = 29;
							continue;
						}
						goto IL_2EC;
					case 53:
						goto IL_2A0;
					case 54:
						goto IL_68A;
					case 55:
						goto IL_53F;
					case 56:
						if (this.\u173A != null)
						{
							num = 39;
							continue;
						}
						goto IL_4F6;
					case 57:
						this.ᝋ.Clear();
						num = 14;
						continue;
					case 58:
						goto IL_2EC;
					case 59:
						goto IL_405;
					case 60:
						this.ᝄ.Clear();
						num = 26;
						continue;
					case 61:
						goto IL_746;
					case 62:
						this.\u173B.Clear();
						num = 17;
						continue;
					}
					break;
					IL_213:
					num = 52;
					continue;
					IL_254:
					num = 35;
					continue;
					IL_27A:
					num = 2;
					continue;
					IL_2A0:
					num = 12;
					continue;
					IL_2C6:
					num = 23;
					continue;
					IL_2EC:
					num = 24;
					continue;
					IL_363:
					num = 48;
					continue;
					IL_3BD:
					num = 50;
					continue;
					IL_405:
					this.ᜨ = XHTMLValidationType.Transitional;
					this.ᜥ = false;
					this.ᝉ = false;
					this.ᝂ = false;
					this.ᝁ = false;
					this.ᝀ = false;
					this.\u1732 = false;
					this.\u173C = false;
					this.ᜯ = true;
					this.ᜠ = 1;
					this.ᝌ = null;
					this.ᜪ = null;
					this.ᜩ = null;
					this.ᜤ = null;
					this.ᜣ = null;
					this.ᜢ = null;
					this.ᜡ = null;
					this.\u171F = null;
					this.\u171C = null;
					this.\u171B = null;
					this.\u171A = null;
					this.\u173D = null;
					this.\u1735 = null;
					this.\u1734 = null;
					this.\u1733 = null;
					this.ᜱ = null;
					this.ᜰ = null;
					this.ᜭ = null;
					this.\u1718 = null;
					num = 47;
					continue;
					IL_4F6:
					num = 33;
					continue;
					IL_51C:
					num = 56;
					continue;
					IL_53F:
					num = 0;
					continue;
					IL_565:
					num = 1;
					continue;
					IL_5FD:
					num = 31;
					continue;
					IL_623:
					this.\u1736.Clear();
					num = 55;
					continue;
					IL_63E:
					num = 18;
					continue;
					IL_664:
					num = 6;
					continue;
					IL_68A:
					num = 46;
					continue;
					IL_703:
					num = 38;
					continue;
					IL_746:
					this.ClearMacros();
					num = 10;
					continue;
					IL_7CD:
					num = 11;
				}
			}
			IL_328:
			IL_7F0:
			this.\u1757.Clear();
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0003E8DC File Offset: 0x0003D8DC
		private void ᜄ()
		{
			int a_ = 10;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			ListStyle listStyle = new ListStyle(this, ListType.Numbered);
			listStyle.Name = ClipboardData.b("㹯ݱᥳᑵᵷࡹ᥻᩽", a_);
			listStyle.ListType = ListType.Numbered;
			this.m_listStyles.Add(listStyle);
			ListStyle listStyle2 = new ListStyle(this, ListType.Bulleted);
			listStyle2.Name = ClipboardData.b("㉯ݱᡳ᩵ᵷ๹᥻᩽", a_);
			listStyle2.ListType = ListType.Bulleted;
			this.m_listStyles.Add(listStyle2);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0003E980 File Offset: 0x0003D980
		private new void ᜀ(byte[] A_0, ref byte[] A_1)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					A_1 = new byte[A_0.Length];
					A_0.CopyTo(A_1, 0);
					num = 1;
					continue;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_5A;
					}
					break;
				}
				if (A_0 == null)
				{
					return;
				}
				num = 0;
			}
			IL_5A:
			if (false)
			{
			}
			if (true)
			{
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0003EA04 File Offset: 0x0003DA04
		private new void ᜀ(Document A_0, DocPicture A_1)
		{
			for (;;)
			{
				int num = A_1.ShapeId;
				int num2 = 8;
				for (;;)
				{
					WordSubdocument wordSubdocument;
					switch (num2)
					{
					case 0:
						A_1.ShapeId = this.ᜠ;
						num2 = 1;
						continue;
					case 1:
						return;
					case 2:
						wordSubdocument = WordSubdocument.HeaderFooter;
						goto IL_94;
					case 3:
						num2 = 7;
						continue;
					case 4:
						goto IL_51;
					case 5:
						if (!A_1.IsHeaderPicture)
						{
							if (true)
							{
							}
							num2 = 3;
							continue;
						}
						num2 = 2;
						continue;
					case 6:
						if (this.ᜠ != -1)
						{
							num2 = 0;
							continue;
						}
						return;
					case 7:
						wordSubdocument = WordSubdocument.Main;
						goto IL_94;
					case 8:
						if (!this.ᜀ(EscherShapeType.msosptPictureFrame, num))
						{
							num2 = 4;
							continue;
						}
						num2 = 5;
						continue;
					}
					break;
					IL_94:
					WordSubdocument a_ = wordSubdocument;
					this.ᜠ = this.Escher.ᜀ(A_0, a_, num, this.ᜠ);
					num2 = 6;
				}
			}
			for (;;)
			{
				IL_51:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_8D;
				}
			}
			IL_8D:
			if (false)
			{
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0003EB28 File Offset: 0x0003DB28
		private new void ᜀ(Document A_0, Spire.Doc.Fields.TextBox A_1)
		{
			for (;;)
			{
				int num = A_1.Format.TextBoxShapeID;
				int num2 = 4;
				for (;;)
				{
					IL_02:
					WordSubdocument wordSubdocument;
					switch (num2)
					{
					case 0:
						wordSubdocument = WordSubdocument.HeaderFooter;
						goto IL_85;
					case 1:
						wordSubdocument = WordSubdocument.Main;
						goto IL_85;
					case 2:
						if (this.ᜠ != -1)
						{
							num2 = 5;
							continue;
						}
						return;
					case 3:
						return;
					case 4:
						while (this.ᜀ(EscherShapeType.msosptTextBox, num))
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								if (true)
								{
								}
								num2 = 8;
								goto IL_02;
							}
						}
						num2 = 6;
						continue;
					case 5:
						A_1.Format.TextBoxShapeID = this.ᜠ;
						num2 = 3;
						continue;
					case 6:
						return;
					case 7:
						num2 = 1;
						continue;
					case 8:
						if (!A_1.Format.IsHeaderTextBox)
						{
							num2 = 7;
							continue;
						}
						num2 = 0;
						continue;
					}
					break;
					IL_85:
					WordSubdocument a_ = wordSubdocument;
					this.ᜠ = this.Escher.ᜀ(A_0, a_, num, this.ᜠ);
					num2 = 2;
				}
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0003EC5C File Offset: 0x0003DC5C
		private new void ᜀ(Document A_0, spr\u248F A_1)
		{
			for (;;)
			{
				int a_ = A_1.ᜏ().ᜡ();
				int num = 6;
				for (;;)
				{
					WordSubdocument wordSubdocument;
					switch (num)
					{
					case 0:
						return;
					case 1:
						wordSubdocument = WordSubdocument.HeaderFooter;
						goto IL_7B;
					case 2:
						num = 4;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_38;
						default:
							if (false)
							{
							}
							A_1.ᜏ().ᜄ(this.ᜠ);
							if (true)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 4:
						wordSubdocument = WordSubdocument.Main;
						goto IL_7B;
					case 5:
						if (this.ᜠ != -1)
						{
							num = 3;
							continue;
						}
						return;
					case 6:
						goto IL_38;
					}
					break;
					IL_38:
					if (!A_1.\u1713())
					{
						num = 2;
						continue;
					}
					num = 1;
					continue;
					IL_7B:
					WordSubdocument a_2 = wordSubdocument;
					this.ᜠ = this.Escher.ᜀ(A_0, a_2, a_, this.ᜠ);
					num = 5;
				}
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0003ED58 File Offset: 0x0003DD58
		private new bool ᜀ(EscherShapeType A_0, int A_1)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return true;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 3:
					if ((this.Escher.ᜈ()[A_1] as spr\u2459).ᜅ().ᜊ() != A_0)
					{
						num = 5;
						continue;
					}
					return true;
				case 4:
					if (this.Escher.ᜈ()[A_1] is spr\u2459)
					{
						num = 0;
						continue;
					}
					return true;
				case 5:
					goto IL_AA;
				}
				if (!this.Escher.ᜈ().ContainsKey(A_1))
				{
					break;
				}
				num = 1;
			}
			IL_6B:
			this.ᜠ = -1;
			return false;
			IL_AA:
			if (true)
			{
			}
			goto IL_6B;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0003EE4C File Offset: 0x0003DE4C
		private Image ᜃ()
		{
			if (this.\u1714.Type == BackgroundType.Picture)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					return this.\u1714.Picture;
				}
			}
			return null;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0003EEA4 File Offset: 0x0003DEA4
		private new void ᜀ(Image A_0)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.\u1714.Picture = A_0;
			this.\u1714.Type = BackgroundType.Picture;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0003EEF8 File Offset: 0x0003DEF8
		private bool ᜂ()
		{
			switch (0)
			{
			default:
			{
				int num = 0;
				IEnumerator enumerator;
				for (;;)
				{
					switch (num)
					{
					case 1:
						if (this.m_sections.Count == 0)
						{
							num = 4;
							continue;
						}
						enumerator = this.m_sections.GetEnumerator();
						num = 2;
						continue;
					case 2:
						goto IL_5C;
					case 3:
						num = 1;
						continue;
					case 4:
						goto IL_87;
					}
					if (this.m_sections == null)
					{
						goto IL_8B;
					}
					num = 3;
				}
				IL_5C:
				bool result;
				try
				{
					num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_EC;
						case 1:
						{
							if (!enumerator.MoveNext())
							{
								num = 2;
								continue;
							}
							Section section = (Section)enumerator.Current;
							num = 3;
							continue;
						}
						case 2:
							num = 6;
							continue;
						case 3:
						{
							Section section;
							if (section.\u170D())
							{
								num = 5;
								continue;
							}
							break;
						}
						case 5:
							result = true;
							num = 0;
							continue;
						case 6:
							goto IL_125;
						}
						IL_C5:
						num = 1;
						continue;
						goto IL_C5;
					}
					IL_EC:
					return result;
					IL_125:
					return false;
				}
				finally
				{
					for (;;)
					{
						IL_13F:
						IDisposable disposable = enumerator as IDisposable;
						for (;;)
						{
							IL_146:
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_146;
									default:
										if (false)
										{
										}
										disposable.Dispose();
										num = 1;
										continue;
									}
									break;
								case 1:
									goto IL_188;
								case 2:
									if (disposable != null)
									{
										num = 0;
										continue;
									}
									goto IL_18A;
								}
								goto IL_13F;
							}
						}
					}
					IL_188:
					IL_18A:;
				}
				return result;
				IL_87:
				IL_8B:
				if (true)
				{
				}
				return false;
			}
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0003F0A4 File Offset: 0x0003E0A4
		internal bool ᜥ()
		{
			bool result;
			for (;;)
			{
				SecurityPermission securityPermission = new SecurityPermission(PermissionState.Unrestricted);
				result = false;
				try
				{
					securityPermission.Demand();
					result = true;
				}
				catch (SecurityException)
				{
				}
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_40;
				}
			}
			IL_40:
			if (false)
			{
			}
			return result;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0003F108 File Offset: 0x0003E108
		private new string ᜀ(string A_0, FileFormat A_1)
		{
			int a_ = 10;
			for (;;)
			{
				FileInfo fileInfo = new FileInfo(A_0);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						string extension;
						if (extension != string.Empty)
						{
							num = 6;
							continue;
						}
						goto IL_E0;
					}
					case 1:
						if (!fileInfo.Exists)
						{
							num = 5;
							continue;
						}
						return A_0;
					case 2:
					{
						if (true)
						{
						}
						string extension = fileInfo.Extension;
						num = 7;
						continue;
					}
					case 3:
						goto IL_E0;
					case 4:
						if (A_1 == FileFormat.Html)
						{
							return A_0;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_146;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 5:
						num = 4;
						continue;
					case 6:
					{
						string extension;
						int startIndex = A_0.LastIndexOf(extension);
						A_0 = A_0.Remove(startIndex);
						num = 3;
						continue;
					}
					case 7:
					{
						string extension;
						if (extension != A_1.ToString())
						{
							num = 8;
							continue;
						}
						return A_0;
					}
					case 8:
						goto IL_146;
					case 9:
						return A_0;
					}
					break;
					IL_E0:
					A_0 = A_0 + ClipboardData.b("幯", a_) + A_1.ToString();
					num = 9;
					continue;
					IL_146:
					num = 0;
				}
			}
			return A_0;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0003F264 File Offset: 0x0003E264
		private new void ᜀ(string A_0)
		{
			int a_ = 3;
			for (;;)
			{
				bool flag = false;
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					IL_0B:
					switch (num)
					{
					case 0:
						while (A_0.Length < 260)
						{
							string directoryName = Path.GetDirectoryName(A_0);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num = 6;
								goto IL_0B;
							}
						}
						num = 3;
						continue;
					case 1:
						flag = true;
						num = 4;
						continue;
					case 2:
						goto IL_70;
					case 3:
						flag = true;
						num = 7;
						continue;
					case 4:
						goto IL_5A;
					case 5:
						if (flag)
						{
							num = 2;
							continue;
						}
						return;
					case 6:
					{
						string directoryName;
						if (directoryName.Length >= 248)
						{
							num = 1;
							continue;
						}
						goto IL_5A;
					}
					case 7:
						goto IL_5A;
					}
					break;
					IL_5A:
					num = 5;
				}
			}
			IL_70:
			throw new PathTooLongException(ClipboardData.b("㵨ͪ࡬佮ᝰᩲᥴቶ奸ᕺᱼቾꎂꦈﾊ놐ﾒ杖練ﺘ떚붜쮞즠욢薤솦\udca8잪솬횮醰슲살횶햸튺\udbbc횾꓀Ꟃꇆꃈꟊ꣌뿐닒룔닖律뛚꣜곞闠쏢蟤苦짨蟪裬鳮苰폲致鿶飸闺\uddfc췾㜀㌂┄搆愈樊缌渎爐朒瀔攖樘㬚簜焞䔠̢儤伦䰨ପ䤬䘮䌰嘲嘴䌶嘸䤺䐼Ἶ⽀≂⡄≆楈♊㡌㱎═獒㝔㉖祘㝚㡜ⱞበ䍢ᅤསࡨժ䵬嵮䕰䭲啴ᑶᅸོ᩺Ṿ愈", a_));
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0003F368 File Offset: 0x0003E368
		internal void \u173A()
		{
			int a_ = 5;
			if (!this.WriteWarning)
			{
				goto IL_78;
			}
			IL_11:
			try
			{
				Paragraph paragraph = new Paragraph(this);
				ITextRange textRange = paragraph.AppendText(ClipboardData.b("⹪᭬๮ᵰٲᑴͶၸᑺ፼彾횀꾎ꮐ뎒솔ﾖﲘ뮚列슠횢좤슦잨\udfaa趬\ud8ae킰삲閴풶쮸\udeba\udcbc쮾꓀Ꟃ냆ꃈ뿊ꗌ苐ꏒ볔ꗖ볘駜냞苠쏢菤裦鯨쯪쏬ꇮ듰ꟲ\udbf4", a_));
				textRange.CharacterFormat.TextColor = Color.Red;
				textRange.CharacterFormat.FontSize = 12f;
				this.m_sections[0].Body.ChildObjects.Insert(0, paragraph);
				this.\u173F = true;
			}
			catch
			{
			}
			IL_78:
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_11;
			}
			if (false)
			{
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0003F42C File Offset: 0x0003E42C
		private void ᜁ()
		{
			IEnumerator enumerator = this.Sections.GetEnumerator();
			try
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 2:
						goto IL_A2;
					case 3:
					{
						if (!enumerator.MoveNext())
						{
							num = 0;
							continue;
						}
						Section section = (Section)enumerator.Current;
						section.HeadersFooters.EvenHeader.WriteWatermark = false;
						section.HeadersFooters.OddHeader.WriteWatermark = false;
						section.HeadersFooters.FirstPageHeader.WriteWatermark = false;
						num = 1;
						continue;
					}
					}
					IL_7D:
					num = 3;
					continue;
					goto IL_7D;
				}
				IL_A2:;
			}
			finally
			{
				for (;;)
				{
					IL_B8:
					IDisposable disposable = enumerator as IDisposable;
					for (;;)
					{
						IL_BF:
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_FE;
							case 1:
								if (disposable != null)
								{
									num = 2;
									continue;
								}
								goto IL_100;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_BF;
								default:
									if (false)
									{
									}
									disposable.Dispose();
									num = 0;
									continue;
								}
								break;
							}
							goto IL_B8;
						}
					}
				}
				IL_FE:
				IL_100:;
			}
			if (true)
			{
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0003F55C File Offset: 0x0003E55C
		private new void ᜀ(ProtectionType A_0)
		{
			int num = 1;
			for (;;)
			{
				IEnumerator enumerator;
				switch (num)
				{
				case 0:
					try
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_95;
							case 1:
								num = 0;
								continue;
							case 4:
							{
								if (!enumerator.MoveNext())
								{
									num = 1;
									continue;
								}
								Section section = (Section)enumerator.Current;
								section.ProtectForm = true;
								num = 3;
								continue;
							}
							}
							IL_56:
							num = 4;
							continue;
							goto IL_56;
						}
						IL_95:
						return;
					}
					finally
					{
						for (;;)
						{
							IL_AE:
							IDisposable disposable = enumerator as IDisposable;
							for (;;)
							{
								IL_B5:
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_B5;
										default:
											if (false)
											{
											}
											disposable.Dispose();
											num = 2;
											continue;
										}
										break;
									case 1:
										if (disposable != null)
										{
											num = 0;
											continue;
										}
										goto IL_F6;
									case 2:
										goto IL_F4;
									}
									goto IL_AE;
								}
							}
						}
						IL_F4:
						IL_F6:;
					}
					goto IL_F7;
				case 2:
					goto IL_F7;
				}
				if (A_0 == ProtectionType.AllowOnlyFormFields)
				{
					num = 2;
					continue;
				}
				break;
				IL_F7:
				enumerator = this.Sections.GetEnumerator();
				if (true)
				{
				}
				num = 0;
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0003F6A0 File Offset: 0x0003E6A0
		private new void ᜀ(XmlWriter A_0)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			sprṑ sprṑ = new sprṑ(A_0);
			sprṑ.ᜀ(this);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0003F6EC File Offset: 0x0003E6EC
		private new void ᜀ(XmlReader A_0)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			XDLSReader xdlsreader = new XDLSReader(A_0);
			xdlsreader.Deserialize(this);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0003F738 File Offset: 0x0003E738
		XmlSchema IXmlSerializable.GetSchema()
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return this.GetSchema();
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0003F77C File Offset: 0x0003E77C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜀ(reader);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0003F7C0 File Offset: 0x0003E7C0
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜀ(writer);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0003F804 File Offset: 0x0003E804
		protected XmlSchema GetSchema()
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return spr\u2533.ᜀ();
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0003F844 File Offset: 0x0003E844
		protected override void InitXDLSHolder()
		{
			int a_ = 19;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			base.XDLSHolder.AddElement(ClipboardData.b("੸ེѼ፾", a_), this.Styles);
			base.XDLSHolder.AddElement(ClipboardData.b("ᕸቺ๼୾ﲄ", a_), this.m_listStyles);
			base.XDLSHolder.AddElement(ClipboardData.b("੸ṺṼ୾", a_), this.Sections);
			base.XDLSHolder.AddElement(ClipboardData.b("ླྀቺ᡼ࡾ검ﲈﮊ", a_), this.ViewSetup);
			base.XDLSHolder.AddElement(ClipboardData.b("᭸๺ᑼ፾ꪆ麗力ﾎﺖﲘ", a_), this.BuiltinDocumentProperties);
			base.XDLSHolder.AddElement(ClipboardData.b("᩸๺๼୾ꢄﮈﶌﲔ", a_), this.CustomDocumentProperties);
			base.XDLSHolder.AddElement(ClipboardData.b("ᕸቺ๼୾검ﮈ力", a_), this.ListOverrides);
			base.XDLSHolder.AddElement(ClipboardData.b("᭸᩺Ṽᑾ", a_), this.Background);
			base.XDLSHolder.AddElement(ClipboardData.b("๸᩺ॼ᩾", a_), this.Watermark);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0003F9A0 File Offset: 0x0003E9A0
		protected override void WriteXmlContent(IXDLSContentWriter writer)
		{
			int a_ = 2;
			switch (0)
			{
			default:
				for (;;)
				{
					base.WriteXmlContent(writer);
					int num = 17;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.GrammarSpellingData.ᜁ() != null)
							{
								num = 8;
								continue;
							}
							goto IL_37F;
						case 1:
							goto IL_2AF;
						case 2:
							if (this.\u1719 != null)
							{
								num = 18;
								continue;
							}
							goto IL_301;
						case 3:
							goto IL_28A;
						case 4:
							writer.WriteChildBinaryElement(ClipboardData.b("ཧᡩ൫ͭᵯ፱ٳ孵ᱷ᭹ࡻώ", a_), this.GrammarSpellingData.ᜁ());
							writer.WriteChildBinaryElement(ClipboardData.b("᭧ᩩ५ɭᱯ᭱ᩳᅵ啷ṹᵻ੽", a_), this.GrammarSpellingData.ᜀ());
							num = 10;
							continue;
						case 5:
							goto IL_A4;
						case 6:
							num = 0;
							continue;
						case 7:
							if (this.\u1718 != null)
							{
								num = 6;
								continue;
							}
							goto IL_37F;
						case 8:
							num = 15;
							continue;
						case 9:
							writer.WriteChildBinaryElement(ClipboardData.b("է୩ཫᱭὯű", a_), this.MacrosData);
							num = 13;
							continue;
						case 10:
							goto IL_37F;
						case 11:
							if (this.ObjectPool != null)
							{
								num = 21;
								continue;
							}
							goto IL_28A;
						case 12:
						{
							MemoryStream memoryStream = new MemoryStream();
							this.\u1715.ᜀ(memoryStream);
							byte[] value = memoryStream.ToArray();
							writer.WriteChildBinaryElement(ClipboardData.b("౧թᱫ䍭᥯ᱱs፵੷ᑹᵻች", a_), value);
							if (true)
							{
							}
							num = 5;
							continue;
						}
						case 13:
							goto IL_1C1;
						case 14:
							writer.WriteChildBinaryElement(ClipboardData.b("է୩ཫᱭὯű女ᕵ᝷᝹ᅻώ", a_), this.MacroCommands);
							num = 1;
							continue;
						case 15:
							if (this.GrammarSpellingData.ᜀ() != null)
							{
								num = 4;
								continue;
							}
							goto IL_37F;
						case 16:
							if (this.MacroCommands != null)
							{
								num = 14;
								continue;
							}
							goto IL_2AF;
						case 17:
							if (this.MacrosData != null)
							{
								num = 9;
								continue;
							}
							goto IL_1C1;
						case 18:
						{
							MemoryStream memoryStream2 = new MemoryStream();
							this.\u1719.ᜃ(memoryStream2);
							this.\u171D = memoryStream2.ToArray();
							writer.WriteChildBinaryElement(ClipboardData.b("൧ᥩཫ٭ᕯq女ት᥷๹ᵻ", a_), this.\u171D);
							memoryStream2.Close();
							MemoryStream memoryStream3 = new MemoryStream();
							this.\u1719.ᜄ(memoryStream3);
							this.\u171E = memoryStream3.ToArray();
							writer.WriteChildBinaryElement(ClipboardData.b("൧ᥩཫ٭ᕯq女ᕵ᝷ᑹࡻώﮇ", a_), this.\u171E);
							memoryStream3.Close();
							this.\u171D = null;
							this.\u171E = null;
							num = 20;
							continue;
						}
						case 19:
							if (this.\u1715 != null)
							{
								num = 12;
								continue;
							}
							goto IL_A4;
						case 20:
							goto IL_301;
						case 21:
							IL_2CF:
							writer.WriteChildBinaryElement(ClipboardData.b("ݧࡩ٫୭፯ٱ女ٵ᝷ᕹၻ", a_), this.ObjectPool);
							num = 3;
							continue;
						}
						break;
						IL_A4:
						num = 7;
						continue;
						IL_1C1:
						num = 16;
						continue;
						IL_28A:
						num = 2;
						continue;
						IL_2AF:
						num = 11;
						continue;
						IL_37F:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2CF;
						default:
							goto IL_395;
						}
						IL_301:
						num = 19;
					}
				}
				IL_395:
				if (false)
				{
				}
				return;
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0003FD48 File Offset: 0x0003ED48
		protected override bool ReadXmlContent(IXDLSContentReader reader)
		{
			int a_ = 17;
			switch (0)
			{
			default:
			{
				int num = 12;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.\u171E != null)
						{
							num = 24;
							continue;
						}
						goto IL_3E6;
					case 1:
						goto IL_314;
					case 2:
						if (reader.TagName == ClipboardData.b("ၶ୸᩺ၼቾꢄﾊ", a_))
						{
							num = 4;
							continue;
						}
						goto IL_1FE;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2E9;
						default:
							if (false)
							{
							}
							if (reader.TagName == ClipboardData.b("᩶ᡸ᡺ོၾ꺂", a_))
							{
								num = 14;
								continue;
							}
							goto IL_128;
						}
						break;
					case 4:
						this.\u1718.ᜀ(reader.ReadChildBinaryElement());
						num = 20;
						continue;
					case 5:
						if (reader.TagName == ClipboardData.b("ቶ੸᡺ᕼ᩾꺂ﶈ", a_))
						{
							goto IL_2E9;
						}
						goto IL_236;
					case 6:
						this.\u1718.ᜁ(reader.ReadChildBinaryElement());
						num = 25;
						continue;
					case 7:
						this.ObjectPool = reader.ReadChildBinaryElement();
						num = 1;
						continue;
					case 8:
						this.MacrosData = reader.ReadChildBinaryElement();
						num = 13;
						continue;
					case 9:
						goto IL_128;
					case 10:
						this.\u171D = reader.ReadChildBinaryElement();
						num = 18;
						continue;
					case 11:
						if (reader.TagName == ClipboardData.b("ᡶ᭸ᅺ᡼᱾꺂", a_))
						{
							num = 7;
							continue;
						}
						goto IL_314;
					case 13:
						goto IL_34C;
					case 14:
						this.MacroCommands = reader.ReadChildBinaryElement();
						num = 9;
						continue;
					case 15:
						if (reader.TagName == ClipboardData.b("፶ᙸ୺偼ᙾ", a_))
						{
							num = 29;
							continue;
						}
						goto IL_DB;
					case 16:
						if (this.\u171D != null)
						{
							num = 31;
							continue;
						}
						goto IL_3E6;
					case 17:
						if (reader.TagName == ClipboardData.b("ቶ੸᡺ᕼ᩾꺂ﾊﾐ", a_))
						{
							num = 28;
							continue;
						}
						goto IL_2BF;
					case 18:
						goto IL_236;
					case 19:
						goto IL_3E6;
					case 20:
						goto IL_1FE;
					case 21:
						goto IL_2BF;
					case 22:
						if (true)
						{
						}
						goto IL_DB;
					case 23:
						if (reader.TagName == ClipboardData.b("ѶॸṺᅼ፾ꪆ歷", a_))
						{
							num = 6;
							continue;
						}
						goto IL_4AD;
					case 24:
					{
						MemoryStream memoryStream = new MemoryStream(this.\u171D, 0, this.\u171D.Length);
						MemoryStream memoryStream2 = new MemoryStream(this.\u171E, 0, this.\u171E.Length);
						this.\u1719 = new spr\u24E3(memoryStream2, memoryStream, 0, (int)memoryStream2.Length, this);
						memoryStream.Close();
						memoryStream2.Close();
						this.\u171D = null;
						this.\u171E = null;
						num = 19;
						continue;
					}
					case 25:
						goto IL_2BA;
					case 26:
						goto IL_163;
					case 27:
						this.\u1718 = new sprᥚ();
						num = 26;
						continue;
					case 28:
						this.\u171E = reader.ReadChildBinaryElement();
						num = 21;
						continue;
					case 29:
					{
						byte[] buffer = reader.ReadChildBinaryElement();
						MemoryStream memoryStream3 = new MemoryStream(buffer);
						this.\u1715 = new spr\u202E(memoryStream3, 0, (int)memoryStream3.Length, false);
						memoryStream3.Close();
						num = 22;
						continue;
					}
					case 30:
						if (this.\u1718 == null)
						{
							num = 27;
							continue;
						}
						goto IL_163;
					case 31:
						num = 0;
						continue;
					}
					if (reader.TagName == ClipboardData.b("᩶ᡸ᡺ོၾ", a_))
					{
						num = 8;
						continue;
					}
					goto IL_34C;
					IL_DB:
					num = 30;
					continue;
					IL_128:
					num = 11;
					continue;
					IL_163:
					num = 2;
					continue;
					IL_1FE:
					num = 23;
					continue;
					IL_236:
					num = 16;
					continue;
					IL_2BF:
					num = 5;
					continue;
					IL_2E9:
					num = 10;
					continue;
					IL_314:
					num = 17;
					continue;
					IL_34C:
					num = 3;
					continue;
					IL_3E6:
					num = 15;
				}
				IL_2BA:
				IL_4AD:
				return base.ReadXmlContent(reader);
			}
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0004020C File Offset: 0x0003F20C
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 8;
			for (;;)
			{
				base.WriteXmlAttributes(writer);
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						writer.WriteValue(ClipboardData.b("㵭ѯ፱ᩳት᥷ࡹ᡻㽽", a_), this.ᜡ);
						num = 3;
						continue;
					case 1:
						if (this.ᜣ != null)
						{
							goto IL_AA;
						}
						goto IL_135;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AA;
						default:
							if (false)
							{
							}
							if (this.\u1713.Type != WatermarkType.NoWatermark)
							{
								num = 5;
								continue;
							}
							return;
						}
						break;
					case 3:
						goto IL_B7;
					case 4:
						if (this.ᜡ != null)
						{
							num = 0;
							continue;
						}
						goto IL_B7;
					case 5:
						writer.WriteValue(ClipboardData.b("㥭ᅯٱᅳѵᕷ᭹๻ᕽ푿ﮁ", a_), this.\u1713.Type);
						num = 11;
						continue;
					case 6:
						goto IL_8C;
					case 7:
						if (this.ᜢ != null)
						{
							num = 10;
							continue;
						}
						goto IL_8C;
					case 8:
						writer.WriteValue(ClipboardData.b("㵭ѯ፱ᩳት᥷ࡹ᡻ぽ슃慎쾉ﶍ", a_), this.ᜣ);
						num = 9;
						continue;
					case 9:
						goto IL_135;
					case 10:
						writer.WriteValue(ClipboardData.b("㵭ѯ፱ᩳት᥷ࡹ᡻㡽솃ﮇﺉ", a_), this.ᜢ);
						num = 6;
						continue;
					case 11:
						return;
					}
					break;
					IL_8C:
					if (true)
					{
					}
					num = 1;
					continue;
					IL_AA:
					num = 8;
					continue;
					IL_B7:
					num = 7;
					continue;
					IL_135:
					num = 2;
				}
			}
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x000403C8 File Offset: 0x0003F3C8
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 1;
			for (;;)
			{
				base.ReadXmlAttributes(reader);
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_A5;
					case 1:
						goto IL_D6;
					case 2:
						if (reader.HasAttribute(ClipboardData.b("㑦ᵨ੪ͬ୮ၰŲᅴㅶᡸॺ㡼Ṿ", a_)))
						{
							num = 5;
							continue;
						}
						goto IL_A5;
					case 3:
						goto IL_16C;
					case 4:
						if (true)
						{
						}
						if (reader.HasAttribute(ClipboardData.b("㑦ᵨ੪ͬ୮ၰŲᅴ㙶੸᡺ᑼᙾ", a_)))
						{
							num = 11;
							continue;
						}
						goto IL_D6;
					case 5:
						this.ᜢ = reader.ReadString(ClipboardData.b("㑦ᵨ੪ͬ୮ၰŲᅴㅶᡸॺ㡼Ṿ", a_));
						num = 0;
						continue;
					case 6:
						this.ᜣ = reader.ReadString(ClipboardData.b("㑦ᵨ੪ͬ୮ၰŲᅴ㥶ᙸᕺ㭼Ṿ욂ﶈ", a_));
						num = 3;
						continue;
					case 7:
						return;
					case 8:
					{
						WatermarkType a_2 = (WatermarkType)reader.ReadEnum(ClipboardData.b("てࡨὪ࡬ᵮᱰቲݴᱶ⵸ɺർ᩾", a_), typeof(WatermarkType));
						this.ᜀ(a_2);
						num = 7;
						continue;
					}
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C9;
						default:
							if (false)
							{
							}
							if (reader.HasAttribute(ClipboardData.b("てࡨὪ࡬ᵮᱰቲݴᱶ⵸ɺർ᩾", a_)))
							{
								num = 8;
								continue;
							}
							return;
						}
						break;
					case 10:
						if (reader.HasAttribute(ClipboardData.b("㑦ᵨ੪ͬ୮ၰŲᅴ㥶ᙸᕺ㭼Ṿ욂ﶈ", a_)))
						{
							goto IL_C9;
						}
						goto IL_16C;
					case 11:
						this.ᜡ = reader.ReadString(ClipboardData.b("㑦ᵨ੪ͬ୮ၰŲᅴ㙶੸᡺ᑼᙾ", a_));
						num = 1;
						continue;
					}
					break;
					IL_A5:
					num = 10;
					continue;
					IL_C9:
					num = 6;
					continue;
					IL_D6:
					num = 2;
					continue;
					IL_16C:
					num = 9;
				}
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x000405C4 File Offset: 0x0003F5C4
		protected override void CreateLayoutInfo()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜀ = new spr\u22A8(ChildrenLayoutDirection.Vertical);
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0004060C File Offset: 0x0003F60C
		protected override IDocumentObjectCollection WidgetCollection
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.Sections;
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00040650 File Offset: 0x0003F650
		private new void ᜀ(LicenseInfo A_0, PdfDocumentBase A_1)
		{
			int a_ = 18;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					return;
				case 2:
					A_1.InternalLicense = new InternalLicense
					{
						License = A_0,
						LicenseType = A_0.Type,
						ProductName = ClipboardData.b("⭷੹ᕻ౽겁삃", a_),
						AssemblyList = new string[]
						{
							ClipboardData.b("⭷੹ᕻ౽겁삃", a_)
						}
					};
					num = 1;
					continue;
				case 3:
					if (this.\u176D != null)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					return;
				case 4:
					num = 3;
					continue;
				}
				if (this.\u173E)
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					num = 4;
					break;
				}
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00040750 File Offset: 0x0003F750
		private new void ᜀ()
		{
			for (;;)
			{
				this.\u173E = true;
				this.\u176D = null;
				License license = null;
				LicenseManager.IsValid(typeof(Document), this, out license);
				LicenseType licenseType = spr\u2543.ᜀ(license);
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						this.\u173E = false;
						this.\u176D = (LicenseInfo)license;
						this.\u176D.Type = licenseType;
						num = 2;
						continue;
					case 1:
						return;
					case 2:
						return;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							if ((licenseType & LicenseType.Runtime) == LicenseType.Runtime)
							{
								num = 0;
								continue;
							}
							this.\u173E = true;
							num = 1;
							continue;
						}
						break;
					}
					break;
				}
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00040828 File Offset: 0x0003F828
		internal void ᜁ(spr\u1937 A_0)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᝠ = A_0;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0004086C File Offset: 0x0003F86C
		public void ClearMacros()
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4A;
					default:
						goto IL_68;
					}
					break;
				case 1:
					this.VbaProject.Close();
					this.VbaProject = null;
					goto IL_4A;
				}
				if (this.VbaProject != null)
				{
					num = 1;
					continue;
				}
				goto IL_78;
				IL_4A:
				num = 0;
			}
			IL_68:
			if (false)
			{
			}
			if (true)
			{
			}
			IL_78:
			this.VbaData.Clear();
			this.DocEvents.Clear();
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00040908 File Offset: 0x0003F908
		internal new void ᜀ(int A_0, int A_1)
		{
			int a_ = 6;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_68;
				case 2:
					goto IL_CC;
				case 3:
					num = 7;
					continue;
				case 4:
					if (A_0 + A_1 > this.\u175E)
					{
						num = 1;
						continue;
					}
					return;
				case 5:
					num = 4;
					continue;
				case 6:
					if (A_1 >= 1)
					{
						num = 5;
						continue;
					}
					goto IL_CE;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (A_0 > this.\u175E - 1)
						{
							num = 2;
							continue;
						}
						break;
					}
					if (true)
					{
					}
					num = 6;
					continue;
				}
				if (A_0 < 0)
				{
					goto IL_E2;
				}
				num = 3;
			}
			IL_68:
			goto IL_CE;
			IL_CC:
			goto IL_E2;
			IL_CE:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ᱫ཭ᝯ᝱㝳᥵൷ᑹࡻ", a_));
			IL_E2:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ᱫ཭ᝯ᝱㵳ᡵᱷόѻ", a_));
		}

		// Token: 0x04000D15 RID: 3349
		private new const string ᜀ = "Normal";

		// Token: 0x04000D16 RID: 3350
		internal const string ᜁ = "Bulleted";

		// Token: 0x04000D17 RID: 3351
		internal const string ᜂ = "Numbered";

		// Token: 0x04000D18 RID: 3352
		internal bool ᜃ = true;

		// Token: 0x04000D19 RID: 3353
		internal new bool ᜄ;

		// Token: 0x04000D1A RID: 3354
		internal BodyRegion ᜅ;

		// Token: 0x04000D1B RID: 3355
		private FileFormat ᜆ;

		// Token: 0x04000D1C RID: 3356
		internal bool ᜇ;

		// Token: 0x04000D1D RID: 3357
		internal bool ᜈ;

		// Token: 0x04000D1E RID: 3358
		internal bool ᜉ;

		// Token: 0x04000D1F RID: 3359
		internal BuiltinDocumentProperties ᜊ = new BuiltinDocumentProperties();

		// Token: 0x04000D20 RID: 3360
		internal CustomDocumentProperties ᜋ = new CustomDocumentProperties();

		// Token: 0x04000D21 RID: 3361
		protected SectionCollection m_sections;

		// Token: 0x04000D22 RID: 3362
		protected StyleCollection m_styles;

		// Token: 0x04000D23 RID: 3363
		protected ListStyleCollection m_listStyles;

		// Token: 0x04000D24 RID: 3364
		private spr\u1B79 ᜌ;

		// Token: 0x04000D25 RID: 3365
		private BookmarkCollection \u170D;

		// Token: 0x04000D26 RID: 3366
		private spr\u2062 ᜎ;

		// Token: 0x04000D27 RID: 3367
		private TextBoxCollection ᜏ;

		// Token: 0x04000D28 RID: 3368
		private CommentsCollection ᜐ;

		// Token: 0x04000D29 RID: 3369
		private MailMerge ᜑ;

		// Token: 0x04000D2A RID: 3370
		private ViewSetup \u1712;

		// Token: 0x04000D2B RID: 3371
		private WatermarkBase \u1713;

		// Token: 0x04000D2C RID: 3372
		private Background \u1714;

		// Token: 0x04000D2D RID: 3373
		private spr\u202E \u1715;

		// Token: 0x04000D2E RID: 3374
		private EndnoteOptions \u1716;

		// Token: 0x04000D2F RID: 3375
		private FootEndnoteOptions \u1717;

		// Token: 0x04000D30 RID: 3376
		private sprᥚ \u1718;

		// Token: 0x04000D31 RID: 3377
		private spr\u24E3 \u1719;

		// Token: 0x04000D32 RID: 3378
		internal string \u171A;

		// Token: 0x04000D33 RID: 3379
		private byte[] \u171B;

		// Token: 0x04000D34 RID: 3380
		private byte[] \u171C;

		// Token: 0x04000D35 RID: 3381
		private byte[] \u171D;

		// Token: 0x04000D36 RID: 3382
		private byte[] \u171E;

		// Token: 0x04000D37 RID: 3383
		private byte[] \u171F;

		// Token: 0x04000D38 RID: 3384
		private int ᜠ = 1;

		// Token: 0x04000D39 RID: 3385
		private string ᜡ;

		// Token: 0x04000D3A RID: 3386
		private string ᜢ;

		// Token: 0x04000D3B RID: 3387
		private string ᜣ;

		// Token: 0x04000D3C RID: 3388
		private string ᜤ;

		// Token: 0x04000D3D RID: 3389
		private bool ᜥ;

		// Token: 0x04000D3E RID: 3390
		private Section ᜦ;

		// Token: 0x04000D3F RID: 3391
		private readonly object ᜧ = new object();

		// Token: 0x04000D40 RID: 3392
		private XHTMLValidationType ᜨ = XHTMLValidationType.Transitional;

		// Token: 0x04000D41 RID: 3393
		private XmlNode ᜩ;

		// Token: 0x04000D42 RID: 3394
		private MemoryStream ᜪ;

		// Token: 0x04000D43 RID: 3395
		private CharacterFormat ᜫ;

		// Token: 0x04000D44 RID: 3396
		internal ParagraphFormat ᜬ;

		// Token: 0x04000D45 RID: 3397
		private spr᪆ ᜭ;

		// Token: 0x04000D46 RID: 3398
		private ImportOptions ᜮ = ImportOptions.UseDestinationStyles;

		// Token: 0x04000D47 RID: 3399
		private bool ᜯ = true;

		// Token: 0x04000D48 RID: 3400
		private VariableCollection ᜰ;

		// Token: 0x04000D49 RID: 3401
		private DocumentProperties ᜱ;

		// Token: 0x04000D4A RID: 3402
		private bool \u1732;

		// Token: 0x04000D4B RID: 3403
		private ParagraphBase \u1733;

		// Token: 0x04000D4C RID: 3404
		private BodyRegion \u1734;

		// Token: 0x04000D4D RID: 3405
		private HtmlExportOptions \u1735;

		// Token: 0x04000D4E RID: 3406
		private List<Stream> \u1736;

		// Token: 0x04000D4F RID: 3407
		private List<XmlNode> \u1737;

		// Token: 0x04000D50 RID: 3408
		private List<Stream> \u1738;

		// Token: 0x04000D51 RID: 3409
		private List<XmlNode> \u1739;

		// Token: 0x04000D52 RID: 3410
		private List<Stream> \u173A;

		// Token: 0x04000D53 RID: 3411
		private List<XmlNode> \u173B;

		// Token: 0x04000D54 RID: 3412
		internal bool \u173C;

		// Token: 0x04000D55 RID: 3413
		private byte[] \u173D;

		// Token: 0x04000D56 RID: 3414
		private bool \u173E = true;

		// Token: 0x04000D57 RID: 3415
		private bool \u173F;

		// Token: 0x04000D58 RID: 3416
		private bool ᝀ;

		// Token: 0x04000D59 RID: 3417
		private bool ᝁ;

		// Token: 0x04000D5A RID: 3418
		private bool ᝂ;

		// Token: 0x04000D5B RID: 3419
		private List<string> ᝃ;

		// Token: 0x04000D5C RID: 3420
		private Dictionary<string, string> ᝄ;

		// Token: 0x04000D5D RID: 3421
		private bool ᝅ;

		// Token: 0x04000D5E RID: 3422
		private int ᝆ;

		// Token: 0x04000D5F RID: 3423
		private int ᝇ;

		// Token: 0x04000D60 RID: 3424
		private int ᝈ;

		// Token: 0x04000D61 RID: 3425
		private bool ᝉ;

		// Token: 0x04000D62 RID: 3426
		private Dictionary<string, string> ᝊ;

		// Token: 0x04000D63 RID: 3427
		private Dictionary<string, string> ᝋ;

		// Token: 0x04000D64 RID: 3428
		private TableOfContent ᝌ;

		// Token: 0x04000D65 RID: 3429
		private List<Font> ᝍ;

		// Token: 0x04000D66 RID: 3430
		private spr\u2100 ᝎ;

		// Token: 0x04000D67 RID: 3431
		private Stream ᝏ;

		// Token: 0x04000D68 RID: 3432
		private sprᭇ ᝐ;

		// Token: 0x04000D69 RID: 3433
		private sprᭇ ᝑ;

		// Token: 0x04000D6A RID: 3434
		private List<sprᴚ> \u1752;

		// Token: 0x04000D6B RID: 3435
		private List<string> \u1753;

		// Token: 0x04000D6C RID: 3436
		private FileFormat \u1754;

		// Token: 0x04000D6D RID: 3437
		private ushort \u1755;

		// Token: 0x04000D6E RID: 3438
		private Stack<Field> \u1756;

		// Token: 0x04000D6F RID: 3439
		internal List<Field> \u1757 = new List<Field>();

		// Token: 0x04000D70 RID: 3440
		private spr\u18F7 \u1758;

		// Token: 0x04000D71 RID: 3441
		private string \u1759;

		// Token: 0x04000D72 RID: 3442
		private Dictionary<string, Dictionary<int, int>> \u175A;

		// Token: 0x04000D73 RID: 3443
		private HybridDictionary \u175B;

		// Token: 0x04000D74 RID: 3444
		private Dictionary<string, int> \u175C;

		// Token: 0x04000D75 RID: 3445
		private Dictionary<string, int> \u175D;

		// Token: 0x04000D76 RID: 3446
		internal int \u175E;

		// Token: 0x04000D77 RID: 3447
		private List<DocumentObject> \u175F;

		// Token: 0x04000D78 RID: 3448
		private spr\u1937 ᝠ;

		// Token: 0x04000D79 RID: 3449
		private Hashtable ᝡ;

		// Token: 0x04000D7A RID: 3450
		private Dictionary<int, Document.ᜀ> ᝢ = new Dictionary<int, Document.ᜀ>();

		// Token: 0x04000D7B RID: 3451
		private PrintDialog ᝣ;

		// Token: 0x04000D7C RID: 3452
		private PrintDocument ᝤ;

		// Token: 0x04000D7D RID: 3453
		private int ᝥ;

		// Token: 0x04000D7E RID: 3454
		private int ᝦ;

		// Token: 0x04000D7F RID: 3455
		private int ᝧ;

		// Token: 0x04000D80 RID: 3456
		private float ᝨ;

		// Token: 0x04000D81 RID: 3457
		private float ᝩ;

		// Token: 0x04000D82 RID: 3458
		private string ᝪ = string.Empty;

		// Token: 0x04000D83 RID: 3459
		private DocumentOperationType ᝫ;

		// Token: 0x04000D84 RID: 3460
		private DigitalSignatures ᝬ = new DigitalSignatures();

		// Token: 0x04000D85 RID: 3461
		private LicenseInfo \u176D;

		// Token: 0x04000D86 RID: 3462
		private int ᝮ = 80;

		// Token: 0x04000D87 RID: 3463
		private PageLayoutHandler ᝯ;

		// Token: 0x04000D88 RID: 3464
		private spr\u24DA ᝰ;

		// Token: 0x04000D89 RID: 3465
		[CompilerGenerated]
		private InternalLicense \u1771;

		// Token: 0x04000D8A RID: 3466
		[CompilerGenerated]
		private static bool \u1772;

		// Token: 0x020000F3 RID: 243
		private new struct ᜀ
		{
			// Token: 0x04000D8B RID: 3467
			public double ᜀ;

			// Token: 0x04000D8C RID: 3468
			public double ᜁ;

			// Token: 0x04000D8D RID: 3469
			public Image ᜂ;
		}
	}
}
