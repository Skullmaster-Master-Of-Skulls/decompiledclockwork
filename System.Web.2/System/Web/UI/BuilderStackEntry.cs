using System;

namespace System.Web.UI
{
	// Token: 0x02000315 RID: 789
	internal class BuilderStackEntry : SourceLineInfo
	{
		// Token: 0x060024F7 RID: 9463 RVA: 0x0007A4CA File Offset: 0x000786CA
		internal BuilderStackEntry(ControlBuilder builder, string tagName, string virtualPath, int line, string inputText, int textPos)
		{
			this._builder = builder;
			this._tagName = tagName;
			base.VirtualPath = virtualPath;
			base.Line = line;
			this._inputText = inputText;
			this._textPos = textPos;
		}

		// Token: 0x04001D58 RID: 7512
		internal ControlBuilder _builder;

		// Token: 0x04001D59 RID: 7513
		internal string _tagName;

		// Token: 0x04001D5A RID: 7514
		internal string _inputText;

		// Token: 0x04001D5B RID: 7515
		internal int _textPos;

		// Token: 0x04001D5C RID: 7516
		internal int _repeatCount;
	}
}
