using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200005A RID: 90
	public class CssSettings : CommonSettings
	{
		// Token: 0x06000580 RID: 1408 RVA: 0x00019D60 File Offset: 0x00017F60
		public CssSettings()
		{
			this.ColorNames = CssColor.Strict;
			this.CommentMode = CssComment.Important;
			this.MinifyExpressions = true;
			this.CssType = CssType.FullStyleSheet;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00019D84 File Offset: 0x00017F84
		public CssSettings Clone()
		{
			CssSettings cssSettings = new CssSettings
			{
				AllowEmbeddedAspNetBlocks = base.AllowEmbeddedAspNetBlocks,
				ColorNames = this.ColorNames,
				CommentMode = this.CommentMode,
				IgnoreAllErrors = base.IgnoreAllErrors,
				IgnoreErrorList = base.IgnoreErrorList,
				IndentSize = base.IndentSize,
				KillSwitch = base.KillSwitch,
				LineBreakThreshold = base.LineBreakThreshold,
				MinifyExpressions = this.MinifyExpressions,
				OutputMode = base.OutputMode,
				PreprocessorDefineList = base.PreprocessorDefineList,
				TermSemicolons = base.TermSemicolons,
				CssType = this.CssType,
				BlocksStartOnSameLine = base.BlocksStartOnSameLine
			};
			cssSettings.AddResourceStrings(base.ResourceStrings);
			foreach (KeyValuePair<string, string> item in base.ReplacementTokens)
			{
				cssSettings.ReplacementTokens.Add(item);
			}
			foreach (KeyValuePair<string, string> item2 in base.ReplacementFallbacks)
			{
				cssSettings.ReplacementTokens.Add(item2);
			}
			return cssSettings;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x00019EE0 File Offset: 0x000180E0
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x00019EE8 File Offset: 0x000180E8
		public CssColor ColorNames { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x00019EF1 File Offset: 0x000180F1
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x00019EF9 File Offset: 0x000180F9
		public CssComment CommentMode { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00019F02 File Offset: 0x00018102
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x00019F0A File Offset: 0x0001810A
		public bool MinifyExpressions { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x00019F13 File Offset: 0x00018113
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x00019F1B File Offset: 0x0001811B
		public CssType CssType { get; set; }
	}
}
