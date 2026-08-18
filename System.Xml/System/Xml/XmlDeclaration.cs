using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x020000D4 RID: 212
	public class XmlDeclaration : XmlLinkedNode
	{
		// Token: 0x06000C72 RID: 3186 RVA: 0x00038118 File Offset: 0x00037118
		protected internal XmlDeclaration(string version, string encoding, string standalone, XmlDocument doc) : base(doc)
		{
			if (version != "1.0")
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
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x0003819A File Offset: 0x0003719A
		public string Version
		{
			get
			{
				return "1.0";
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x000381A1 File Offset: 0x000371A1
		// (set) Token: 0x06000C75 RID: 3189 RVA: 0x000381A9 File Offset: 0x000371A9
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

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x000381BC File Offset: 0x000371BC
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x000381C4 File Offset: 0x000371C4
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

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x00038225 File Offset: 0x00037225
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x0003822D File Offset: 0x0003722D
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

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00038238 File Offset: 0x00037238
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x000382CC File Offset: 0x000372CC
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
				XmlLoader.ParseXmlDeclarationValue(value, out text, out text2, out text3);
				try
				{
					if (text != null && text != "1.0")
					{
						throw new ArgumentException(Res.GetString("Xdom_Version"));
					}
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
					throw;
				}
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00038354 File Offset: 0x00037354
		public override string Name
		{
			get
			{
				return "xml";
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x0003835B File Offset: 0x0003735B
		public override string LocalName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x00038363 File Offset: 0x00037363
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.XmlDeclaration;
			}
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00038367 File Offset: 0x00037367
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateXmlDeclaration(this.Version, this.Encoding, this.Standalone);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00038386 File Offset: 0x00037386
		public override void WriteTo(XmlWriter w)
		{
			w.WriteProcessingInstruction(this.Name, this.InnerText);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0003839A File Offset: 0x0003739A
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x040008F7 RID: 2295
		private const string YES = "yes";

		// Token: 0x040008F8 RID: 2296
		private const string NO = "no";

		// Token: 0x040008F9 RID: 2297
		private const string VERNUM = "1.0";

		// Token: 0x040008FA RID: 2298
		private string encoding;

		// Token: 0x040008FB RID: 2299
		private string standalone;
	}
}
