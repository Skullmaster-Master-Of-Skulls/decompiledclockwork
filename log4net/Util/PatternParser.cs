using System;
using System.Collections;
using System.Globalization;
using log4net.Core;

namespace log4net.Util
{
	// Token: 0x0200010D RID: 269
	public sealed class PatternParser
	{
		// Token: 0x060007C3 RID: 1987 RVA: 0x0001822D File Offset: 0x0001642D
		public PatternParser(string pattern)
		{
			this.m_pattern = pattern;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00018248 File Offset: 0x00016448
		public PatternConverter Parse()
		{
			string[] matches = this.BuildCache();
			this.ParseInternal(this.m_pattern, matches);
			return this.m_head;
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x0001826F File Offset: 0x0001646F
		public Hashtable PatternConverters
		{
			get
			{
				return this.m_patternConverters;
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00018278 File Offset: 0x00016478
		private string[] BuildCache()
		{
			string[] array = new string[this.m_patternConverters.Keys.Count];
			this.m_patternConverters.Keys.CopyTo(array, 0);
			Array.Sort(array, 0, array.Length, PatternParser.StringLengthComparer.Instance);
			return array;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x000182C0 File Offset: 0x000164C0
		private void ParseInternal(string pattern, string[] matches)
		{
			int i = 0;
			while (i < pattern.Length)
			{
				int num = pattern.IndexOf('%', i);
				if (num < 0 || num == pattern.Length - 1)
				{
					this.ProcessLiteral(pattern.Substring(i));
					i = pattern.Length;
				}
				else if (pattern[num + 1] == '%')
				{
					this.ProcessLiteral(pattern.Substring(i, num - i + 1));
					i = num + 2;
				}
				else
				{
					this.ProcessLiteral(pattern.Substring(i, num - i));
					i = num + 1;
					FormattingInfo formattingInfo = new FormattingInfo();
					if (i < pattern.Length && pattern[i] == '-')
					{
						formattingInfo.LeftAlign = true;
						i++;
					}
					while (i < pattern.Length && char.IsDigit(pattern[i]))
					{
						if (formattingInfo.Min < 0)
						{
							formattingInfo.Min = 0;
						}
						formattingInfo.Min = formattingInfo.Min * 10 + int.Parse(pattern[i].ToString(), NumberFormatInfo.InvariantInfo);
						i++;
					}
					if (i < pattern.Length && pattern[i] == '.')
					{
						i++;
					}
					while (i < pattern.Length && char.IsDigit(pattern[i]))
					{
						if (formattingInfo.Max == 2147483647)
						{
							formattingInfo.Max = 0;
						}
						formattingInfo.Max = formattingInfo.Max * 10 + int.Parse(pattern[i].ToString(), NumberFormatInfo.InvariantInfo);
						i++;
					}
					int num2 = pattern.Length - i;
					for (int j = 0; j < matches.Length; j++)
					{
						string text = matches[j];
						if (text.Length <= num2 && string.Compare(pattern, i, text, 0, text.Length) == 0)
						{
							i += matches[j].Length;
							string option = null;
							if (i < pattern.Length && pattern[i] == '{')
							{
								i++;
								int num3 = pattern.IndexOf('}', i);
								if (num3 >= 0)
								{
									option = pattern.Substring(i, num3 - i);
									i = num3 + 1;
								}
							}
							this.ProcessConverter(matches[j], option, formattingInfo);
							break;
						}
					}
				}
			}
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x000184D9 File Offset: 0x000166D9
		private void ProcessLiteral(string text)
		{
			if (text.Length > 0)
			{
				this.ProcessConverter("literal", text, new FormattingInfo());
			}
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x000184F8 File Offset: 0x000166F8
		private void ProcessConverter(string converterName, string option, FormattingInfo formattingInfo)
		{
			LogLog.Debug(PatternParser.declaringType, string.Concat(new object[]
			{
				"Converter [",
				converterName,
				"] Option [",
				option,
				"] Format [min=",
				formattingInfo.Min,
				",max=",
				formattingInfo.Max,
				",leftAlign=",
				formattingInfo.LeftAlign,
				"]"
			}));
			ConverterInfo converterInfo = (ConverterInfo)this.m_patternConverters[converterName];
			if (converterInfo == null)
			{
				LogLog.Error(PatternParser.declaringType, "Unknown converter name [" + converterName + "] in conversion pattern.");
				return;
			}
			PatternConverter patternConverter = null;
			try
			{
				patternConverter = (PatternConverter)Activator.CreateInstance(converterInfo.Type);
			}
			catch (Exception ex)
			{
				LogLog.Error(PatternParser.declaringType, "Failed to create instance of Type [" + converterInfo.Type.FullName + "] using default constructor. Exception: " + ex.ToString());
			}
			patternConverter.FormattingInfo = formattingInfo;
			patternConverter.Option = option;
			patternConverter.Properties = converterInfo.Properties;
			IOptionHandler optionHandler = patternConverter as IOptionHandler;
			if (optionHandler != null)
			{
				optionHandler.ActivateOptions();
			}
			this.AddConverter(patternConverter);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00018644 File Offset: 0x00016844
		private void AddConverter(PatternConverter pc)
		{
			if (this.m_head == null)
			{
				this.m_tail = pc;
				this.m_head = pc;
				return;
			}
			this.m_tail = this.m_tail.SetNext(pc);
		}

		// Token: 0x040002E2 RID: 738
		private const char ESCAPE_CHAR = '%';

		// Token: 0x040002E3 RID: 739
		private PatternConverter m_head;

		// Token: 0x040002E4 RID: 740
		private PatternConverter m_tail;

		// Token: 0x040002E5 RID: 741
		private string m_pattern;

		// Token: 0x040002E6 RID: 742
		private Hashtable m_patternConverters = new Hashtable();

		// Token: 0x040002E7 RID: 743
		private static readonly Type declaringType = typeof(PatternParser);

		// Token: 0x0200010E RID: 270
		private sealed class StringLengthComparer : IComparer
		{
			// Token: 0x060007CC RID: 1996 RVA: 0x0001868D File Offset: 0x0001688D
			private StringLengthComparer()
			{
			}

			// Token: 0x060007CD RID: 1997 RVA: 0x00018698 File Offset: 0x00016898
			public int Compare(object x, object y)
			{
				string text = x as string;
				string text2 = y as string;
				if (text == null && text2 == null)
				{
					return 0;
				}
				if (text == null)
				{
					return 1;
				}
				if (text2 == null)
				{
					return -1;
				}
				return text2.Length.CompareTo(text.Length);
			}

			// Token: 0x040002E8 RID: 744
			public static readonly PatternParser.StringLengthComparer Instance = new PatternParser.StringLengthComparer();
		}
	}
}
