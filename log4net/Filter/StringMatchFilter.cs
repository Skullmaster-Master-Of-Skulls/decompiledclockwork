using System;
using System.Text.RegularExpressions;
using log4net.Core;

namespace log4net.Filter
{
	// Token: 0x02000086 RID: 134
	public class StringMatchFilter : FilterSkeleton
	{
		// Token: 0x0600047C RID: 1148 RVA: 0x0000E7AF File Offset: 0x0000C9AF
		public override void ActivateOptions()
		{
			if (this.m_stringRegexToMatch != null)
			{
				this.m_regexToMatch = new Regex(this.m_stringRegexToMatch, RegexOptions.Compiled);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000E7CB File Offset: 0x0000C9CB
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public bool AcceptOnMatch
		{
			get
			{
				return this.m_acceptOnMatch;
			}
			set
			{
				this.m_acceptOnMatch = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000E7DC File Offset: 0x0000C9DC
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0000E7E4 File Offset: 0x0000C9E4
		public string StringToMatch
		{
			get
			{
				return this.m_stringToMatch;
			}
			set
			{
				this.m_stringToMatch = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000E7ED File Offset: 0x0000C9ED
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0000E7F5 File Offset: 0x0000C9F5
		public string RegexToMatch
		{
			get
			{
				return this.m_stringRegexToMatch;
			}
			set
			{
				this.m_stringRegexToMatch = value;
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000E800 File Offset: 0x0000CA00
		public override FilterDecision Decide(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			string renderedMessage = loggingEvent.RenderedMessage;
			if (renderedMessage == null || (this.m_stringToMatch == null && this.m_regexToMatch == null))
			{
				return FilterDecision.Neutral;
			}
			if (this.m_regexToMatch != null)
			{
				if (!this.m_regexToMatch.Match(renderedMessage).Success)
				{
					return FilterDecision.Neutral;
				}
				if (this.m_acceptOnMatch)
				{
					return FilterDecision.Accept;
				}
				return FilterDecision.Deny;
			}
			else
			{
				if (this.m_stringToMatch == null)
				{
					return FilterDecision.Neutral;
				}
				if (renderedMessage.IndexOf(this.m_stringToMatch) == -1)
				{
					return FilterDecision.Neutral;
				}
				if (this.m_acceptOnMatch)
				{
					return FilterDecision.Accept;
				}
				return FilterDecision.Deny;
			}
		}

		// Token: 0x040001EE RID: 494
		protected bool m_acceptOnMatch = true;

		// Token: 0x040001EF RID: 495
		protected string m_stringToMatch;

		// Token: 0x040001F0 RID: 496
		protected string m_stringRegexToMatch;

		// Token: 0x040001F1 RID: 497
		protected Regex m_regexToMatch;
	}
}
