using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000059 RID: 89
	public class CommonSettings
	{
		// Token: 0x06000554 RID: 1364 RVA: 0x00019720 File Offset: 0x00017920
		protected CommonSettings()
		{
			this.IndentSize = 4;
			this.OutputMode = OutputMode.SingleLine;
			this.TermSemicolons = false;
			this.KillSwitch = 0L;
			this.LineBreakThreshold = 2147482647;
			this.AllowEmbeddedAspNetBlocks = false;
			this.IgnoreErrorCollection = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			this.PreprocessorValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			this.ResourceStrings = new List<ResourceStrings>();
			this.ReplacementTokens = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			this.ReplacementFallbacks = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x000197AD File Offset: 0x000179AD
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x000197B5 File Offset: 0x000179B5
		public bool AllowEmbeddedAspNetBlocks { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x000197BE File Offset: 0x000179BE
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x000197C6 File Offset: 0x000179C6
		public BlockStart BlocksStartOnSameLine { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x000197CF File Offset: 0x000179CF
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x000197D7 File Offset: 0x000179D7
		public bool IgnoreAllErrors { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x000197E0 File Offset: 0x000179E0
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x000197E8 File Offset: 0x000179E8
		public int IndentSize { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x000197F1 File Offset: 0x000179F1
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x000197F9 File Offset: 0x000179F9
		public int LineBreakThreshold { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00019802 File Offset: 0x00017A02
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x0001980A File Offset: 0x00017A0A
		public OutputMode OutputMode { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00019813 File Offset: 0x00017A13
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x0001981B File Offset: 0x00017A1B
		public bool TermSemicolons { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00019824 File Offset: 0x00017A24
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x0001982C File Offset: 0x00017A2C
		public long KillSwitch { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00019835 File Offset: 0x00017A35
		public string LineTerminator
		{
			get
			{
				if (this.OutputMode != OutputMode.MultipleLines)
				{
					return "\n";
				}
				return "\r\n";
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0001984B File Offset: 0x00017A4B
		internal void Indent()
		{
			this.m_indentLevel++;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001985B File Offset: 0x00017A5B
		internal void Unindent()
		{
			if (this.m_indentLevel > 0)
			{
				this.m_indentLevel--;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00019874 File Offset: 0x00017A74
		internal string TabSpaces
		{
			get
			{
				return new string(' ', this.m_indentLevel * this.IndentSize);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0001988A File Offset: 0x00017A8A
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x00019892 File Offset: 0x00017A92
		public ICollection<string> IgnoreErrorCollection { get; private set; }

		// Token: 0x0600056B RID: 1387 RVA: 0x0001989C File Offset: 0x00017A9C
		public int SetIgnoreErrors(IEnumerable<string> ignoreErrors)
		{
			this.IgnoreErrorCollection.Clear();
			if (ignoreErrors != null)
			{
				foreach (string text in ignoreErrors)
				{
					this.IgnoreErrorCollection.Add(text.Trim());
				}
			}
			return this.IgnoreErrorCollection.Count;
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00019908 File Offset: 0x00017B08
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x00019974 File Offset: 0x00017B74
		public string IgnoreErrorList
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value in this.IgnoreErrorCollection)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(',');
					}
					stringBuilder.Append(value);
				}
				return stringBuilder.ToString();
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					foreach (string item in value.Split(new char[]
					{
						','
					}))
					{
						this.IgnoreErrorCollection.Add(item);
					}
					return;
				}
				this.IgnoreErrorCollection.Clear();
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x000199C7 File Offset: 0x00017BC7
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x000199CF File Offset: 0x00017BCF
		public IDictionary<string, string> PreprocessorValues { get; private set; }

		// Token: 0x06000570 RID: 1392 RVA: 0x000199D8 File Offset: 0x00017BD8
		public int SetPreprocessorDefines(params string[] definedNames)
		{
			this.PreprocessorValues.Clear();
			if (definedNames != null && definedNames.Length > 0)
			{
				foreach (string text in definedNames)
				{
					int num = text.IndexOf('=');
					string text2;
					if (num < 0)
					{
						text2 = text.Trim();
					}
					else
					{
						text2 = text.Substring(0, num).Trim();
					}
					if (JSScanner.IsValidIdentifier(text2))
					{
						this.PreprocessorValues.Add(text2, (num < 0) ? string.Empty : text.Substring(num + 1));
					}
				}
			}
			return this.PreprocessorValues.Count;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00019A6C File Offset: 0x00017C6C
		public int SetPreprocessorValues(IDictionary<string, string> defines)
		{
			this.PreprocessorValues.Clear();
			if (defines != null && defines.Count > 0)
			{
				foreach (KeyValuePair<string, string> keyValuePair in defines)
				{
					if (JSScanner.IsValidIdentifier(keyValuePair.Key))
					{
						this.PreprocessorValues.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			return this.PreprocessorValues.Count;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00019AF8 File Offset: 0x00017CF8
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x00019B90 File Offset: 0x00017D90
		public string PreprocessorDefineList
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (KeyValuePair<string, string> keyValuePair in this.PreprocessorValues)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(',');
					}
					stringBuilder.Append(keyValuePair.Key);
					if (!string.IsNullOrEmpty(keyValuePair.Value))
					{
						stringBuilder.Append('=');
						stringBuilder.Append(keyValuePair.Value);
					}
				}
				return stringBuilder.ToString();
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.SetPreprocessorDefines(value.Split(new char[]
					{
						','
					}));
					return;
				}
				this.PreprocessorValues.Clear();
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00019BCB File Offset: 0x00017DCB
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x00019BD3 File Offset: 0x00017DD3
		public IList<ResourceStrings> ResourceStrings { get; private set; }

		// Token: 0x06000576 RID: 1398 RVA: 0x00019BDC File Offset: 0x00017DDC
		public void AddResourceStrings(ResourceStrings resourceStrings)
		{
			this.ResourceStrings.Add(resourceStrings);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00019BEC File Offset: 0x00017DEC
		public void AddResourceStrings(IEnumerable<ResourceStrings> collection)
		{
			if (collection != null)
			{
				foreach (ResourceStrings item in collection)
				{
					this.ResourceStrings.Add(item);
				}
			}
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00019C3C File Offset: 0x00017E3C
		public void ClearResourceStrings()
		{
			this.ResourceStrings.Clear();
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00019C49 File Offset: 0x00017E49
		public void RemoveResourceStrings(ResourceStrings resourceStrings)
		{
			this.ResourceStrings.Remove(resourceStrings);
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00019C58 File Offset: 0x00017E58
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x00019C60 File Offset: 0x00017E60
		public IDictionary<string, string> ReplacementTokens { get; private set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00019C69 File Offset: 0x00017E69
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x00019C71 File Offset: 0x00017E71
		public IDictionary<string, string> ReplacementFallbacks { get; private set; }

		// Token: 0x0600057E RID: 1406 RVA: 0x00019C7C File Offset: 0x00017E7C
		public void ReplacementTokensApplyDefaults(IDictionary<string, string> otherSet)
		{
			if (otherSet != null)
			{
				foreach (KeyValuePair<string, string> item in otherSet)
				{
					if (!this.ReplacementTokens.ContainsKey(item.Key))
					{
						this.ReplacementTokens.Add(item);
					}
				}
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00019CE0 File Offset: 0x00017EE0
		public void ReplacementTokensApplyOverrides(IDictionary<string, string> otherSet)
		{
			if (otherSet != null)
			{
				foreach (KeyValuePair<string, string> item in otherSet)
				{
					if (!this.ReplacementTokens.ContainsKey(item.Key))
					{
						this.ReplacementTokens.Add(item);
					}
					else
					{
						this.ReplacementTokens[item.Key] = item.Value;
					}
				}
			}
		}

		// Token: 0x040001D8 RID: 472
		private int m_indentLevel;
	}
}
