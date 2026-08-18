using System;
using System.Collections;
using System.IO;
using log4net.Core;
using log4net.Layout.Pattern;
using log4net.Util;
using log4net.Util.PatternStringConverters;

namespace log4net.Layout
{
	// Token: 0x020000A8 RID: 168
	public class PatternLayout : LayoutSkeleton
	{
		// Token: 0x060004EF RID: 1263 RVA: 0x0000F698 File Offset: 0x0000D898
		static PatternLayout()
		{
			PatternLayout.s_globalRulesRegistry.Add("literal", typeof(LiteralPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("newline", typeof(NewLinePatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("n", typeof(NewLinePatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("aspnet-cache", typeof(AspNetCachePatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("aspnet-context", typeof(AspNetContextPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("aspnet-request", typeof(AspNetRequestPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("aspnet-session", typeof(AspNetSessionPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("c", typeof(LoggerPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("logger", typeof(LoggerPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("C", typeof(TypeNamePatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("class", typeof(TypeNamePatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("type", typeof(TypeNamePatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("d", typeof(log4net.Layout.Pattern.DatePatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("date", typeof(log4net.Layout.Pattern.DatePatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("exception", typeof(ExceptionPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("F", typeof(FileLocationPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("file", typeof(FileLocationPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("l", typeof(FullLocationPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("location", typeof(FullLocationPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("L", typeof(LineLocationPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("line", typeof(LineLocationPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("m", typeof(MessagePatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("message", typeof(MessagePatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("M", typeof(MethodLocationPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("method", typeof(MethodLocationPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("p", typeof(LevelPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("level", typeof(LevelPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("P", typeof(log4net.Layout.Pattern.PropertyPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("property", typeof(log4net.Layout.Pattern.PropertyPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("properties", typeof(log4net.Layout.Pattern.PropertyPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("r", typeof(RelativeTimePatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("timestamp", typeof(RelativeTimePatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("stacktrace", typeof(StackTracePatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("stacktracedetail", typeof(StackTraceDetailPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("t", typeof(ThreadPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("thread", typeof(ThreadPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("x", typeof(NdcPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("ndc", typeof(NdcPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("X", typeof(log4net.Layout.Pattern.PropertyPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("mdc", typeof(log4net.Layout.Pattern.PropertyPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("a", typeof(log4net.Layout.Pattern.AppDomainPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("appdomain", typeof(log4net.Layout.Pattern.AppDomainPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("u", typeof(log4net.Layout.Pattern.IdentityPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("identity", typeof(log4net.Layout.Pattern.IdentityPatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("utcdate", typeof(log4net.Layout.Pattern.UtcDatePatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("utcDate", typeof(log4net.Layout.Pattern.UtcDatePatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("UtcDate", typeof(log4net.Layout.Pattern.UtcDatePatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("w", typeof(log4net.Layout.Pattern.UserNamePatternConverter));
			PatternLayout.s_globalRulesRegistry.Add("username", typeof(log4net.Layout.Pattern.UserNamePatternConverter));
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000FB7A File Offset: 0x0000DD7A
		public PatternLayout() : this("%message%newline")
		{
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000FB87 File Offset: 0x0000DD87
		public PatternLayout(string pattern)
		{
			this.IgnoresException = true;
			this.m_pattern = pattern;
			if (this.m_pattern == null)
			{
				this.m_pattern = "%message%newline";
			}
			this.ActivateOptions();
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0000FBC1 File Offset: 0x0000DDC1
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x0000FBC9 File Offset: 0x0000DDC9
		public string ConversionPattern
		{
			get
			{
				return this.m_pattern;
			}
			set
			{
				this.m_pattern = value;
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000FBD4 File Offset: 0x0000DDD4
		protected virtual PatternParser CreatePatternParser(string pattern)
		{
			PatternParser patternParser = new PatternParser(pattern);
			foreach (object obj in PatternLayout.s_globalRulesRegistry)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				ConverterInfo converterInfo = new ConverterInfo();
				converterInfo.Name = (string)dictionaryEntry.Key;
				converterInfo.Type = (Type)dictionaryEntry.Value;
				patternParser.PatternConverters[dictionaryEntry.Key] = converterInfo;
			}
			foreach (object obj2 in this.m_instanceRulesRegistry)
			{
				DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
				patternParser.PatternConverters[dictionaryEntry2.Key] = dictionaryEntry2.Value;
			}
			return patternParser;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000FCD4 File Offset: 0x0000DED4
		public override void ActivateOptions()
		{
			this.m_head = this.CreatePatternParser(this.m_pattern).Parse();
			for (PatternConverter patternConverter = this.m_head; patternConverter != null; patternConverter = patternConverter.Next)
			{
				PatternLayoutConverter patternLayoutConverter = patternConverter as PatternLayoutConverter;
				if (patternLayoutConverter != null && !patternLayoutConverter.IgnoresException)
				{
					this.IgnoresException = false;
					return;
				}
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000FD28 File Offset: 0x0000DF28
		public override void Format(TextWriter writer, LoggingEvent loggingEvent)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			for (PatternConverter patternConverter = this.m_head; patternConverter != null; patternConverter = patternConverter.Next)
			{
				patternConverter.Format(writer, loggingEvent);
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000FD6C File Offset: 0x0000DF6C
		public void AddConverter(ConverterInfo converterInfo)
		{
			if (converterInfo == null)
			{
				throw new ArgumentNullException("converterInfo");
			}
			if (!typeof(PatternConverter).IsAssignableFrom(converterInfo.Type))
			{
				throw new ArgumentException("The converter type specified [" + converterInfo.Type + "] must be a subclass of log4net.Util.PatternConverter", "converterInfo");
			}
			this.m_instanceRulesRegistry[converterInfo.Name] = converterInfo;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000FDD0 File Offset: 0x0000DFD0
		public void AddConverter(string name, Type type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.AddConverter(new ConverterInfo
			{
				Name = name,
				Type = type
			});
		}

		// Token: 0x0400020A RID: 522
		public const string DefaultConversionPattern = "%message%newline";

		// Token: 0x0400020B RID: 523
		public const string DetailConversionPattern = "%timestamp [%thread] %level %logger %ndc - %message%newline";

		// Token: 0x0400020C RID: 524
		private static Hashtable s_globalRulesRegistry = new Hashtable(45);

		// Token: 0x0400020D RID: 525
		private string m_pattern;

		// Token: 0x0400020E RID: 526
		private PatternConverter m_head;

		// Token: 0x0400020F RID: 527
		private Hashtable m_instanceRulesRegistry = new Hashtable();
	}
}
