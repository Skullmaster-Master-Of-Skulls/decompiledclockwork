using System;
using System.Collections;
using System.Globalization;
using System.IO;
using log4net.Core;
using log4net.Util.PatternStringConverters;

namespace log4net.Util
{
	// Token: 0x0200010F RID: 271
	public class PatternString : IOptionHandler
	{
		// Token: 0x060007CF RID: 1999 RVA: 0x000186E8 File Offset: 0x000168E8
		static PatternString()
		{
			PatternString.s_globalRulesRegistry.Add("appdomain", typeof(AppDomainPatternConverter));
			PatternString.s_globalRulesRegistry.Add("date", typeof(DatePatternConverter));
			PatternString.s_globalRulesRegistry.Add("env", typeof(EnvironmentPatternConverter));
			PatternString.s_globalRulesRegistry.Add("envFolderPath", typeof(EnvironmentFolderPathPatternConverter));
			PatternString.s_globalRulesRegistry.Add("identity", typeof(IdentityPatternConverter));
			PatternString.s_globalRulesRegistry.Add("literal", typeof(LiteralPatternConverter));
			PatternString.s_globalRulesRegistry.Add("newline", typeof(NewLinePatternConverter));
			PatternString.s_globalRulesRegistry.Add("processid", typeof(ProcessIdPatternConverter));
			PatternString.s_globalRulesRegistry.Add("property", typeof(PropertyPatternConverter));
			PatternString.s_globalRulesRegistry.Add("random", typeof(RandomStringPatternConverter));
			PatternString.s_globalRulesRegistry.Add("username", typeof(UserNamePatternConverter));
			PatternString.s_globalRulesRegistry.Add("utcdate", typeof(UtcDatePatternConverter));
			PatternString.s_globalRulesRegistry.Add("utcDate", typeof(UtcDatePatternConverter));
			PatternString.s_globalRulesRegistry.Add("UtcDate", typeof(UtcDatePatternConverter));
			PatternString.s_globalRulesRegistry.Add("appsetting", typeof(AppSettingPatternConverter));
			PatternString.s_globalRulesRegistry.Add("appSetting", typeof(AppSettingPatternConverter));
			PatternString.s_globalRulesRegistry.Add("AppSetting", typeof(AppSettingPatternConverter));
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x000188AA File Offset: 0x00016AAA
		public PatternString()
		{
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x000188BD File Offset: 0x00016ABD
		public PatternString(string pattern)
		{
			this.m_pattern = pattern;
			this.ActivateOptions();
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x000188DD File Offset: 0x00016ADD
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x000188E5 File Offset: 0x00016AE5
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

		// Token: 0x060007D4 RID: 2004 RVA: 0x000188EE File Offset: 0x00016AEE
		public virtual void ActivateOptions()
		{
			this.m_head = this.CreatePatternParser(this.m_pattern).Parse();
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00018908 File Offset: 0x00016B08
		private PatternParser CreatePatternParser(string pattern)
		{
			PatternParser patternParser = new PatternParser(pattern);
			foreach (object obj in PatternString.s_globalRulesRegistry)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				ConverterInfo converterInfo = new ConverterInfo();
				converterInfo.Name = (string)dictionaryEntry.Key;
				converterInfo.Type = (Type)dictionaryEntry.Value;
				patternParser.PatternConverters.Add(dictionaryEntry.Key, converterInfo);
			}
			foreach (object obj2 in this.m_instanceRulesRegistry)
			{
				DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
				patternParser.PatternConverters[dictionaryEntry2.Key] = dictionaryEntry2.Value;
			}
			return patternParser;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00018A08 File Offset: 0x00016C08
		public void Format(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			for (PatternConverter patternConverter = this.m_head; patternConverter != null; patternConverter = patternConverter.Next)
			{
				patternConverter.Format(writer, null);
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00018A40 File Offset: 0x00016C40
		public string Format()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.Format(stringWriter);
			return stringWriter.ToString();
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00018A68 File Offset: 0x00016C68
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

		// Token: 0x060007D9 RID: 2009 RVA: 0x00018ACC File Offset: 0x00016CCC
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

		// Token: 0x040002E9 RID: 745
		private static Hashtable s_globalRulesRegistry = new Hashtable(18);

		// Token: 0x040002EA RID: 746
		private string m_pattern;

		// Token: 0x040002EB RID: 747
		private PatternConverter m_head;

		// Token: 0x040002EC RID: 748
		private Hashtable m_instanceRulesRegistry = new Hashtable();
	}
}
