using System;
using log4net.Core;

namespace log4net.Filter
{
	// Token: 0x02000087 RID: 135
	public class PropertyFilter : StringMatchFilter
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000E88E File Offset: 0x0000CA8E
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x0000E896 File Offset: 0x0000CA96
		public string Key
		{
			get
			{
				return this.m_key;
			}
			set
			{
				this.m_key = value;
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000E8A0 File Offset: 0x0000CAA0
		public override FilterDecision Decide(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			if (this.m_key == null)
			{
				return FilterDecision.Neutral;
			}
			object obj = loggingEvent.LookupProperty(this.m_key);
			string text = loggingEvent.Repository.RendererMap.FindAndRender(obj);
			if (text == null || (this.m_stringToMatch == null && this.m_regexToMatch == null))
			{
				return FilterDecision.Neutral;
			}
			if (this.m_regexToMatch != null)
			{
				if (!this.m_regexToMatch.Match(text).Success)
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
				if (text.IndexOf(this.m_stringToMatch) == -1)
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

		// Token: 0x040001F2 RID: 498
		private string m_key;
	}
}
