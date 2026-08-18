using System;
using System.Collections;

namespace System.Web.UI
{
	// Token: 0x02000303 RID: 771
	internal sealed class StringPropertyBuilder : ControlBuilder
	{
		// Token: 0x060023B8 RID: 9144 RVA: 0x00057398 File Offset: 0x00055598
		internal StringPropertyBuilder()
		{
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x000745E4 File Offset: 0x000727E4
		internal StringPropertyBuilder(string text)
		{
			this._text = text;
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x000745F3 File Offset: 0x000727F3
		public string Text
		{
			get
			{
				if (this._text != null)
				{
					return this._text;
				}
				return string.Empty;
			}
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x00074609 File Offset: 0x00072809
		public override void AppendLiteralString(string s)
		{
			if (base.ParentBuilder != null && base.ParentBuilder.HtmlDecodeLiterals())
			{
				s = HttpUtility.HtmlDecode(s);
			}
			this._text = s;
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x0007462F File Offset: 0x0007282F
		public override void AppendSubBuilder(ControlBuilder subBuilder)
		{
			throw new HttpException(SR.GetString("StringPropertyBuilder_CannotHaveChildObjects", new object[]
			{
				base.TagName,
				(base.ParentBuilder != null) ? base.ParentBuilder.TagName : string.Empty
			}));
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x0007466C File Offset: 0x0007286C
		public override object BuildObject()
		{
			return this.Text;
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x00074674 File Offset: 0x00072874
		public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string ID, IDictionary attribs)
		{
			base.Init(parser, parentBuilder, type, tagName, ID, attribs);
			base.SetControlType(typeof(string));
		}

		// Token: 0x04001CC9 RID: 7369
		private string _text;
	}
}
