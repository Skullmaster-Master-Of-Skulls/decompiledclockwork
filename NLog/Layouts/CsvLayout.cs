using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using NLog.Config;

namespace NLog.Layouts
{
	// Token: 0x02000112 RID: 274
	[Layout("CsvLayout")]
	[ThreadAgnostic]
	[AppDomainFixedOutput]
	public class CsvLayout : LayoutWithHeaderAndFooter
	{
		// Token: 0x06000799 RID: 1945 RVA: 0x00010940 File Offset: 0x0000EB40
		public CsvLayout()
		{
			this.Columns = new List<CsvColumn>();
			this.WithHeader = true;
			this.Delimiter = CsvColumnDelimiterMode.Auto;
			this.Quoting = CsvQuotingMode.Auto;
			this.QuoteChar = "\"";
			base.Layout = this;
			base.Header = new CsvLayout.CsvHeaderLayout(this);
			base.Footer = null;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x00010998 File Offset: 0x0000EB98
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x000109A0 File Offset: 0x0000EBA0
		[ArrayParameter(typeof(CsvColumn), "column")]
		public IList<CsvColumn> Columns { get; private set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x000109A9 File Offset: 0x0000EBA9
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x000109B1 File Offset: 0x0000EBB1
		public bool WithHeader { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x000109BA File Offset: 0x0000EBBA
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x000109C2 File Offset: 0x0000EBC2
		[DefaultValue("Auto")]
		public CsvColumnDelimiterMode Delimiter { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x000109CB File Offset: 0x0000EBCB
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x000109D3 File Offset: 0x0000EBD3
		[DefaultValue("Auto")]
		public CsvQuotingMode Quoting { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x000109DC File Offset: 0x0000EBDC
		// (set) Token: 0x060007A3 RID: 1955 RVA: 0x000109E4 File Offset: 0x0000EBE4
		[DefaultValue("\"")]
		public string QuoteChar { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x000109ED File Offset: 0x0000EBED
		// (set) Token: 0x060007A5 RID: 1957 RVA: 0x000109F5 File Offset: 0x0000EBF5
		public string CustomColumnDelimiter { get; set; }

		// Token: 0x060007A6 RID: 1958 RVA: 0x00010A00 File Offset: 0x0000EC00
		protected override void InitializeLayout()
		{
			base.InitializeLayout();
			if (!this.WithHeader)
			{
				base.Header = null;
			}
			switch (this.Delimiter)
			{
			case CsvColumnDelimiterMode.Auto:
				this.actualColumnDelimiter = CultureInfo.CurrentCulture.TextInfo.ListSeparator;
				break;
			case CsvColumnDelimiterMode.Comma:
				this.actualColumnDelimiter = ",";
				break;
			case CsvColumnDelimiterMode.Semicolon:
				this.actualColumnDelimiter = ";";
				break;
			case CsvColumnDelimiterMode.Tab:
				this.actualColumnDelimiter = "\t";
				break;
			case CsvColumnDelimiterMode.Pipe:
				this.actualColumnDelimiter = "|";
				break;
			case CsvColumnDelimiterMode.Space:
				this.actualColumnDelimiter = " ";
				break;
			case CsvColumnDelimiterMode.Custom:
				this.actualColumnDelimiter = this.CustomColumnDelimiter;
				break;
			}
			this.quotableCharacters = (this.QuoteChar + "\r\n" + this.actualColumnDelimiter).ToCharArray();
			this.doubleQuoteChar = this.QuoteChar + this.QuoteChar;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00010AEC File Offset: 0x0000ECEC
		protected override string GetFormattedMessage(LogEventInfo logEvent)
		{
			string result;
			if (logEvent.TryGetCachedLayoutValue(this, out result))
			{
				return result;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while (i < this.Columns.Count)
			{
				CsvColumn csvColumn = this.Columns[i];
				if (i != 0)
				{
					stringBuilder.Append(this.actualColumnDelimiter);
				}
				string text = csvColumn.Layout.Render(logEvent);
				bool flag;
				switch (this.Quoting)
				{
				case CsvQuotingMode.All:
					flag = true;
					break;
				case CsvQuotingMode.Nothing:
					flag = false;
					break;
				case CsvQuotingMode.Auto:
					goto IL_6C;
				default:
					goto IL_6C;
				}
				IL_84:
				if (flag)
				{
					stringBuilder.Append(this.QuoteChar);
				}
				if (flag)
				{
					stringBuilder.Append(text.Replace(this.QuoteChar, this.doubleQuoteChar));
				}
				else
				{
					stringBuilder.Append(text);
				}
				if (flag)
				{
					stringBuilder.Append(this.QuoteChar);
				}
				i++;
				continue;
				IL_6C:
				flag = (text.IndexOfAny(this.quotableCharacters) >= 0);
				goto IL_84;
			}
			return logEvent.AddCachedLayoutValue(this, stringBuilder.ToString());
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00010BEC File Offset: 0x0000EDEC
		private string GetHeader()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while (i < this.Columns.Count)
			{
				CsvColumn csvColumn = this.Columns[i];
				if (i != 0)
				{
					stringBuilder.Append(this.actualColumnDelimiter);
				}
				string name = csvColumn.Name;
				bool flag;
				switch (this.Quoting)
				{
				case CsvQuotingMode.All:
					flag = true;
					break;
				case CsvQuotingMode.Nothing:
					flag = false;
					break;
				case CsvQuotingMode.Auto:
					goto IL_57;
				default:
					goto IL_57;
				}
				IL_6D:
				if (flag)
				{
					stringBuilder.Append(this.QuoteChar);
				}
				if (flag)
				{
					stringBuilder.Append(name.Replace(this.QuoteChar, this.doubleQuoteChar));
				}
				else
				{
					stringBuilder.Append(name);
				}
				if (flag)
				{
					stringBuilder.Append(this.QuoteChar);
				}
				i++;
				continue;
				IL_57:
				flag = (name.IndexOfAny(this.quotableCharacters) >= 0);
				goto IL_6D;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400023D RID: 573
		private string actualColumnDelimiter;

		// Token: 0x0400023E RID: 574
		private string doubleQuoteChar;

		// Token: 0x0400023F RID: 575
		private char[] quotableCharacters;

		// Token: 0x02000113 RID: 275
		[ThreadAgnostic]
		private class CsvHeaderLayout : Layout
		{
			// Token: 0x060007A9 RID: 1961 RVA: 0x00010CC9 File Offset: 0x0000EEC9
			public CsvHeaderLayout(CsvLayout parent)
			{
				this.parent = parent;
			}

			// Token: 0x060007AA RID: 1962 RVA: 0x00010CD8 File Offset: 0x0000EED8
			protected override string GetFormattedMessage(LogEventInfo logEvent)
			{
				string result;
				if (logEvent.TryGetCachedLayoutValue(this, out result))
				{
					return result;
				}
				return logEvent.AddCachedLayoutValue(this, this.parent.GetHeader());
			}

			// Token: 0x04000246 RID: 582
			private CsvLayout parent;
		}
	}
}
