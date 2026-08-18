using System;
using System.Collections;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x0200081C RID: 2076
	internal class BuildResultNoCompilePage : BuildResultNoCompileTemplateControl
	{
		// Token: 0x06006364 RID: 25444 RVA: 0x0015C214 File Offset: 0x0015A414
		internal BuildResultNoCompilePage(Type baseType, TemplateParser parser) : base(baseType, parser)
		{
			PageParser pageParser = (PageParser)parser;
			this._traceEnabled = pageParser.TraceEnabled;
			this._traceMode = pageParser.TraceMode;
			if (pageParser.OutputCacheParameters != null)
			{
				this._outputCacheData = pageParser.OutputCacheParameters;
				if (this._outputCacheData.Duration == 0 || this._outputCacheData.Location == OutputCacheLocation.None)
				{
					this._outputCacheData = null;
				}
				else
				{
					this._fileDependencies = new string[pageParser.SourceDependencies.Count];
					int num = 0;
					foreach (object obj in ((IEnumerable)pageParser.SourceDependencies))
					{
						string text = (string)obj;
						this._fileDependencies[num++] = text;
					}
				}
			}
			this._validateRequest = pageParser.ValidateRequest;
			this._stylesheetTheme = pageParser.StyleSheetTheme;
		}

		// Token: 0x06006365 RID: 25445 RVA: 0x0015C308 File Offset: 0x0015A508
		internal override void FrameworkInitialize(TemplateControl templateControl)
		{
			Page page = (Page)templateControl;
			page.StyleSheetTheme = this._stylesheetTheme;
			page.InitializeStyleSheet();
			base.FrameworkInitialize(templateControl);
			if (this._traceEnabled != TraceEnable.Default)
			{
				page.TraceEnabled = (this._traceEnabled == TraceEnable.Enable);
			}
			if (this._traceMode != TraceMode.Default)
			{
				page.TraceModeValue = this._traceMode;
			}
			if (this._outputCacheData != null)
			{
				page.AddWrappedFileDependencies(this._fileDependencies);
				page.InitOutputCache(this._outputCacheData);
			}
			if (this._validateRequest)
			{
				page.Request.ValidateInput();
				return;
			}
			if (MultiTargetingUtil.TargetFrameworkVersion >= VersionUtil.Framework45)
			{
				page.ValidateRequestMode = ValidateRequestMode.Disabled;
			}
		}

		// Token: 0x0400337D RID: 13181
		private TraceEnable _traceEnabled;

		// Token: 0x0400337E RID: 13182
		private TraceMode _traceMode;

		// Token: 0x0400337F RID: 13183
		private OutputCacheParameters _outputCacheData;

		// Token: 0x04003380 RID: 13184
		private string[] _fileDependencies;

		// Token: 0x04003381 RID: 13185
		private bool _validateRequest;

		// Token: 0x04003382 RID: 13186
		private string _stylesheetTheme;
	}
}
