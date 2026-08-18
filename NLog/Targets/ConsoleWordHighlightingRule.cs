using System;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using NLog.Config;

namespace NLog.Targets
{
	// Token: 0x02000151 RID: 337
	[NLogConfigurationItem]
	public class ConsoleWordHighlightingRule
	{
		// Token: 0x06000C06 RID: 3078 RVA: 0x0001BF88 File Offset: 0x0001A188
		public ConsoleWordHighlightingRule()
		{
			this.BackgroundColor = ConsoleOutputColor.NoChange;
			this.ForegroundColor = ConsoleOutputColor.NoChange;
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0001BFA0 File Offset: 0x0001A1A0
		public ConsoleWordHighlightingRule(string text, ConsoleOutputColor foregroundColor, ConsoleOutputColor backgroundColor)
		{
			this.Text = text;
			this.ForegroundColor = foregroundColor;
			this.BackgroundColor = backgroundColor;
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0001BFBD File Offset: 0x0001A1BD
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x0001BFC5 File Offset: 0x0001A1C5
		public string Regex { get; set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0001BFCE File Offset: 0x0001A1CE
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x0001BFD6 File Offset: 0x0001A1D6
		[DefaultValue(false)]
		public bool CompileRegex { get; set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0001BFDF File Offset: 0x0001A1DF
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x0001BFE7 File Offset: 0x0001A1E7
		public string Text { get; set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0001BFF0 File Offset: 0x0001A1F0
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x0001BFF8 File Offset: 0x0001A1F8
		[DefaultValue(false)]
		public bool WholeWords { get; set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0001C001 File Offset: 0x0001A201
		// (set) Token: 0x06000C11 RID: 3089 RVA: 0x0001C009 File Offset: 0x0001A209
		[DefaultValue(false)]
		public bool IgnoreCase { get; set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x0001C012 File Offset: 0x0001A212
		// (set) Token: 0x06000C13 RID: 3091 RVA: 0x0001C01A File Offset: 0x0001A21A
		[DefaultValue("NoChange")]
		public ConsoleOutputColor ForegroundColor { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0001C023 File Offset: 0x0001A223
		// (set) Token: 0x06000C15 RID: 3093 RVA: 0x0001C02B File Offset: 0x0001A22B
		[DefaultValue("NoChange")]
		public ConsoleOutputColor BackgroundColor { get; set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0001C034 File Offset: 0x0001A234
		public Regex CompiledRegex
		{
			get
			{
				if (this.compiledRegex == null)
				{
					string regexExpression = this.GetRegexExpression();
					if (regexExpression == null)
					{
						return null;
					}
					RegexOptions regexOptions = this.GetRegexOptions(RegexOptions.Compiled);
					this.compiledRegex = new Regex(regexExpression, regexOptions);
				}
				return this.compiledRegex;
			}
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0001C070 File Offset: 0x0001A270
		private RegexOptions GetRegexOptions(RegexOptions regexOptions)
		{
			if (this.IgnoreCase)
			{
				regexOptions |= RegexOptions.IgnoreCase;
			}
			return regexOptions;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0001C080 File Offset: 0x0001A280
		private string GetRegexExpression()
		{
			string text = this.Regex;
			if (text == null && this.Text != null)
			{
				text = System.Text.RegularExpressions.Regex.Escape(this.Text);
				if (this.WholeWords)
				{
					text = "\\b" + text + "\\b";
				}
			}
			return text;
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0001C0C8 File Offset: 0x0001A2C8
		private string MatchEvaluator(Match m)
		{
			StringBuilder stringBuilder = new StringBuilder(m.Value.Length + 5);
			stringBuilder.Append('\a');
			stringBuilder.Append((char)(this.ForegroundColor + 65));
			stringBuilder.Append((char)(this.BackgroundColor + 65));
			stringBuilder.Append(m.Value);
			stringBuilder.Append('\a');
			stringBuilder.Append('X');
			return stringBuilder.ToString();
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0001C138 File Offset: 0x0001A338
		internal string ReplaceWithEscapeSequences(string message)
		{
			if (this.CompileRegex)
			{
				Regex regex = this.CompiledRegex;
				if (regex == null)
				{
					return message;
				}
				return regex.Replace(message, new MatchEvaluator(this.MatchEvaluator));
			}
			else
			{
				string regexExpression = this.GetRegexExpression();
				if (regexExpression != null)
				{
					RegexOptions regexOptions = this.GetRegexOptions(RegexOptions.None);
					return System.Text.RegularExpressions.Regex.Replace(message, regexExpression, new MatchEvaluator(this.MatchEvaluator), regexOptions);
				}
				return message;
			}
		}

		// Token: 0x04000300 RID: 768
		private Regex compiledRegex;
	}
}
