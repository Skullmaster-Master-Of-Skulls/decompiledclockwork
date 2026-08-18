using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000101 RID: 257
	public class XmlDeclaration : XmlLinkedNode
	{
		// Token: 0x060011A1 RID: 4513 RVA: 0x00049F48 File Offset: 0x00048148
		protected internal XmlDeclaration(string version, string encoding, string standalone, XmlDocument doc) : base(doc)
		{
			if (!this.IsValidXmlVersion(version))
			{
				throw new ArgumentException(Res.GetString("Xdom_Version"));
			}
			if (standalone != null && standalone.Length > 0 && standalone != "yes" && standalone != "no")
			{
				throw new ArgumentException(Res.GetString("Xdom_standalone", new object[]
				{
					standalone
				}));
			}
			this.Encoding = encoding;
			this.Standalone = standalone;
			this.Version = version;
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x00049FCB File Offset: 0x000481CB
		// (set) Token: 0x060011A3 RID: 4515 RVA: 0x00049FD3 File Offset: 0x000481D3
		public string Version
		{
			get
			{
				return this.version;
			}
			internal set
			{
				this.version = value;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x00049FDC File Offset: 0x000481DC
		// (set) Token: 0x060011A5 RID: 4517 RVA: 0x00049FE4 File Offset: 0x000481E4
		public string Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.encoding = ((value == null) ? string.Empty : value);
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x00049FF7 File Offset: 0x000481F7
		// (set) Token: 0x060011A7 RID: 4519 RVA: 0x0004A000 File Offset: 0x00048200
		public string Standalone
		{
			get
			{
				return this.standalone;
			}
			set
			{
				if (value == null)
				{
					this.standalone = string.Empty;
					return;
				}
				if (value.Length == 0 || value == "yes" || value == "no")
				{
					this.standalone = value;
					return;
				}
				throw new ArgumentException(Res.GetString("Xdom_standalone", new object[]
				{
					value
				}));
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x0004A05F File Offset: 0x0004825F
		// (set) Token: 0x060011A9 RID: 4521 RVA: 0x0004A067 File Offset: 0x00048267
		public override string Value
		{
			get
			{
				return this.InnerText;
			}
			set
			{
				this.InnerText = value;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x0004A070 File Offset: 0x00048270
		// (set) Token: 0x060011AB RID: 4523 RVA: 0x0004A104 File Offset: 0x00048304
		public override string InnerText
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder("version=\"" + this.Version + "\"");
				if (this.Encoding.Length > 0)
				{
					stringBuilder.Append(" encoding=\"");
					stringBuilder.Append(this.Encoding);
					stringBuilder.Append("\"");
				}
				if (this.Standalone.Length > 0)
				{
					stringBuilder.Append(" standalone=\"");
					stringBuilder.Append(this.Standalone);
					stringBuilder.Append("\"");
				}
				return stringBuilder.ToString();
			}
			set
			{
				string text = null;
				string text2 = null;
				string text3 = null;
				string text4 = this.Encoding;
				string text5 = this.Standalone;
				string text6 = this.Version;
				XmlLoader.ParseXmlDeclarationValue(value, out text, out text2, out text3);
				try
				{
					if (text != null && !this.IsValidXmlVersion(text))
					{
						throw new ArgumentException(Res.GetString("Xdom_Version"));
					}
					this.Version = text;
					if (text2 != null)
					{
						this.Encoding = text2;
					}
					if (text3 != null)
					{
						this.Standalone = text3;
					}
				}
				catch
				{
					this.Encoding = text4;
					this.Standalone = text5;
					this.Version = text6;
					throw;
				}
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x0004A1A0 File Offset: 0x000483A0
		public override string Name
		{
			get
			{
				return "xml";
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x0004A1A7 File Offset: 0x000483A7
		public override string LocalName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x0004A1AF File Offset: 0x000483AF
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.XmlDeclaration;
			}
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0004A1B3 File Offset: 0x000483B3
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateXmlDeclaration(this.Version, this.Encoding, this.Standalone);
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0004A1D2 File Offset: 0x000483D2
		public override void WriteTo(XmlWriter w)
		{
			w.WriteProcessingInstruction(this.Name, this.InnerText);
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0004A1E6 File Offset: 0x000483E6
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x0004A1E8 File Offset: 0x000483E8
		private bool IsValidXmlVersion(string ver)
		{
			return ver.Length >= 3 && ver[0] == '1' && ver[1] == '.' && XmlCharType.IsOnlyDigits(ver, 2, ver.Length - 2);
		}

		// Token: 0x040004D6 RID: 1238
		private const string YES = "yes";

		// Token: 0x040004D7 RID: 1239
		private const string NO = "no";

		// Token: 0x040004D8 RID: 1240
		private string version;

		// Token: 0x040004D9 RID: 1241
		private string encoding;

		// Token: 0x040004DA RID: 1242
		private string standalone;
	}
}
