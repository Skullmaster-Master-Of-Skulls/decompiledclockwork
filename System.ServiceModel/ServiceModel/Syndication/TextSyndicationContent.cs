using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System.ServiceModel.Syndication
{
	// Token: 0x0200019A RID: 410
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class TextSyndicationContent : SyndicationContent
	{
		// Token: 0x06000D3E RID: 3390 RVA: 0x00030613 File Offset: 0x0002E813
		public TextSyndicationContent(string text) : this(text, TextSyndicationContentKind.Plaintext)
		{
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0003061D File Offset: 0x0002E81D
		public TextSyndicationContent(string text, TextSyndicationContentKind textKind)
		{
			if (!TextSyndicationContentKindHelper.IsDefined(textKind))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("textKind"));
			}
			this.text = text;
			this.textKind = textKind;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00030650 File Offset: 0x0002E850
		protected TextSyndicationContent(TextSyndicationContent source) : base(source)
		{
			if (source == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("source");
			}
			this.text = source.text;
			this.textKind = source.textKind;
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x00030684 File Offset: 0x0002E884
		public string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x0003068C File Offset: 0x0002E88C
		public override string Type
		{
			get
			{
				TextSyndicationContentKind textSyndicationContentKind = this.textKind;
				if (textSyndicationContentKind == TextSyndicationContentKind.Html)
				{
					return "html";
				}
				if (textSyndicationContentKind != TextSyndicationContentKind.XHtml)
				{
					return "text";
				}
				return "xhtml";
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x000306BB File Offset: 0x0002E8BB
		public override SyndicationContent Clone()
		{
			return new TextSyndicationContent(this);
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x000306C4 File Offset: 0x0002E8C4
		protected override void WriteContentsTo(XmlWriter writer)
		{
			string data = this.text ?? string.Empty;
			if (this.textKind == TextSyndicationContentKind.XHtml)
			{
				writer.WriteRaw(data);
				return;
			}
			writer.WriteString(data);
		}

		// Token: 0x040016F9 RID: 5881
		private string text;

		// Token: 0x040016FA RID: 5882
		private TextSyndicationContentKind textKind;
	}
}
