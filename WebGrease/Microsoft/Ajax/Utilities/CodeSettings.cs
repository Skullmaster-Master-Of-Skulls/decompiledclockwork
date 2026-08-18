using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000079 RID: 121
	public class CodeSettings : CommonSettings
	{
		// Token: 0x0600073F RID: 1855 RVA: 0x00022828 File Offset: 0x00020A28
		public CodeSettings()
		{
			this.MinifyCode = true;
			this.EvalTreatment = EvalTreatment.Ignore;
			this.InlineSafeStrings = true;
			this.MacSafariQuirks = true;
			this.PreserveImportantComments = true;
			this.QuoteObjectLiteralProperties = false;
			this.StrictMode = false;
			this.StripDebugStatements = true;
			this.ManualRenamesProperties = true;
			base.OutputMode = OutputMode.SingleLine;
			this.m_knownGlobals = new HashSet<string>();
			this.m_debugLookups = new HashSet<string>();
			this.m_noRenameSet = new HashSet<string>(new string[]
			{
				"$super"
			});
			this.m_identifierReplacementMap = new Dictionary<string, string>();
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x000228C0 File Offset: 0x00020AC0
		public CodeSettings Clone()
		{
			CodeSettings codeSettings = new CodeSettings
			{
				m_minify = this.m_minify,
				AllowEmbeddedAspNetBlocks = base.AllowEmbeddedAspNetBlocks,
				AlwaysEscapeNonAscii = this.AlwaysEscapeNonAscii,
				CollapseToLiteral = this.CollapseToLiteral,
				ConstStatementsMozilla = this.ConstStatementsMozilla,
				DebugLookupList = this.DebugLookupList,
				EvalLiteralExpressions = this.EvalLiteralExpressions,
				EvalTreatment = this.EvalTreatment,
				Format = this.Format,
				IgnoreConditionalCompilation = this.IgnoreConditionalCompilation,
				IgnoreAllErrors = base.IgnoreAllErrors,
				IgnoreErrorList = base.IgnoreErrorList,
				IgnorePreprocessorDefines = this.IgnorePreprocessorDefines,
				IndentSize = base.IndentSize,
				InlineSafeStrings = this.InlineSafeStrings,
				KillSwitch = base.KillSwitch,
				KnownGlobalNamesList = this.KnownGlobalNamesList,
				LineBreakThreshold = base.LineBreakThreshold,
				LocalRenaming = this.LocalRenaming,
				MacSafariQuirks = this.MacSafariQuirks,
				ManualRenamesProperties = this.ManualRenamesProperties,
				NoAutoRenameList = this.NoAutoRenameList,
				OutputMode = base.OutputMode,
				PreprocessOnly = this.PreprocessOnly,
				PreprocessorDefineList = base.PreprocessorDefineList,
				PreserveFunctionNames = this.PreserveFunctionNames,
				PreserveImportantComments = this.PreserveImportantComments,
				QuoteObjectLiteralProperties = this.QuoteObjectLiteralProperties,
				RemoveFunctionExpressionNames = this.RemoveFunctionExpressionNames,
				RemoveUnneededCode = this.RemoveUnneededCode,
				RenamePairs = this.RenamePairs,
				ReorderScopeDeclarations = this.ReorderScopeDeclarations,
				SourceMode = this.SourceMode,
				StrictMode = this.StrictMode,
				StripDebugStatements = this.StripDebugStatements,
				TermSemicolons = base.TermSemicolons,
				BlocksStartOnSameLine = base.BlocksStartOnSameLine,
				ErrorIfNotInlineSafe = this.ErrorIfNotInlineSafe,
				SymbolsMap = this.SymbolsMap
			};
			codeSettings.AddResourceStrings(base.ResourceStrings);
			foreach (KeyValuePair<string, string> item in base.ReplacementTokens)
			{
				codeSettings.ReplacementTokens.Add(item);
			}
			foreach (KeyValuePair<string, string> item2 in base.ReplacementFallbacks)
			{
				codeSettings.ReplacementTokens.Add(item2);
			}
			return codeSettings;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00022B48 File Offset: 0x00020D48
		public bool AddRenamePair(string sourceName, string newName)
		{
			bool result = false;
			if (JSScanner.IsValidIdentifier(sourceName) && JSScanner.IsValidIdentifier(newName))
			{
				if (this.m_identifierReplacementMap.ContainsKey(sourceName))
				{
					this.m_identifierReplacementMap[sourceName] = newName;
				}
				else
				{
					this.m_identifierReplacementMap.Add(sourceName, newName);
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00022B94 File Offset: 0x00020D94
		public void ClearRenamePairs()
		{
			this.m_identifierReplacementMap.Clear();
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00022BA1 File Offset: 0x00020DA1
		public bool HasRenamePairs
		{
			get
			{
				return this.m_identifierReplacementMap.Count > 0;
			}
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00022BB4 File Offset: 0x00020DB4
		public string GetNewName(string sourceName)
		{
			string result;
			if (!this.m_identifierReplacementMap.TryGetValue(sourceName, out result))
			{
				result = null;
			}
			return result;
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x00022BD4 File Offset: 0x00020DD4
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x00022C64 File Offset: 0x00020E64
		public string RenamePairs
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (KeyValuePair<string, string> keyValuePair in this.m_identifierReplacementMap)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(',');
					}
					stringBuilder.Append(keyValuePair.Key);
					stringBuilder.Append('=');
					stringBuilder.Append(keyValuePair.Value);
				}
				return stringBuilder.ToString();
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					foreach (string text in value.Split(new char[]
					{
						',',
						';'
					}))
					{
						string[] array2 = text.Split(new char[]
						{
							'='
						});
						if (array2.Length == 2)
						{
							this.AddRenamePair(array2[0].Trim(), array2[1].Trim());
						}
					}
					return;
				}
				this.m_identifierReplacementMap.Clear();
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x00022CE9 File Offset: 0x00020EE9
		public IEnumerable<string> NoAutoRenameCollection
		{
			get
			{
				return this.m_noRenameSet;
			}
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00022CF4 File Offset: 0x00020EF4
		public int SetNoAutoRenames(IEnumerable<string> noRenameNames)
		{
			this.m_noRenameSet.Clear();
			if (noRenameNames != null)
			{
				foreach (string noRename in noRenameNames)
				{
					this.AddNoAutoRename(noRename);
				}
			}
			return this.m_noRenameSet.Count;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00022D58 File Offset: 0x00020F58
		public bool AddNoAutoRename(string noRename)
		{
			if (!JSScanner.IsValidIdentifier(noRename))
			{
				return false;
			}
			this.m_noRenameSet.Add(noRename);
			return true;
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x00022D74 File Offset: 0x00020F74
		// (set) Token: 0x0600074B RID: 1867 RVA: 0x00022DE8 File Offset: 0x00020FE8
		public string NoAutoRenameList
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value in this.m_noRenameSet)
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
					foreach (string noRename in value.Split(new char[]
					{
						',',
						';'
					}))
					{
						this.AddNoAutoRename(noRename);
					}
					return;
				}
				this.m_noRenameSet.Clear();
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x00022E3C File Offset: 0x0002103C
		public IEnumerable<string> KnownGlobalCollection
		{
			get
			{
				return this.m_knownGlobals;
			}
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00022E44 File Offset: 0x00021044
		public int SetKnownGlobalIdentifiers(IEnumerable<string> globalArray)
		{
			this.m_knownGlobals.Clear();
			if (globalArray != null)
			{
				foreach (string identifier in globalArray)
				{
					this.AddKnownGlobal(identifier);
				}
			}
			return this.m_knownGlobals.Count;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00022EA8 File Offset: 0x000210A8
		public bool AddKnownGlobal(string identifier)
		{
			if (JSScanner.IsValidIdentifier(identifier))
			{
				this.m_knownGlobals.Add(identifier);
				return true;
			}
			return false;
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x00022EC4 File Offset: 0x000210C4
		// (set) Token: 0x06000750 RID: 1872 RVA: 0x00022F38 File Offset: 0x00021138
		public string KnownGlobalNamesList
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value in this.m_knownGlobals)
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
					foreach (string identifier in value.Split(new char[]
					{
						',',
						';'
					}))
					{
						this.AddKnownGlobal(identifier);
					}
					return;
				}
				this.m_knownGlobals.Clear();
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x00022F8C File Offset: 0x0002118C
		public IEnumerable<string> DebugLookupCollection
		{
			get
			{
				return this.m_debugLookups;
			}
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00022F94 File Offset: 0x00021194
		public int SetDebugNamespaces(IEnumerable<string> debugLookups)
		{
			this.m_debugLookups.Clear();
			if (debugLookups != null)
			{
				foreach (string debugNamespace in debugLookups)
				{
					this.AddDebugLookup(debugNamespace);
				}
			}
			return this.m_debugLookups.Count;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00022FF8 File Offset: 0x000211F8
		public bool AddDebugLookup(string debugNamespace)
		{
			if (!string.IsNullOrEmpty(debugNamespace))
			{
				if (debugNamespace.IndexOf('.') > 0)
				{
					string[] array = debugNamespace.Split(new char[]
					{
						'.'
					});
					foreach (string name in array)
					{
						if (!JSScanner.IsValidIdentifier(name))
						{
							return false;
						}
					}
				}
				else if (!JSScanner.IsValidIdentifier(debugNamespace))
				{
					return false;
				}
				this.m_debugLookups.Add(debugNamespace);
				return true;
			}
			return false;
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x00023074 File Offset: 0x00021274
		// (set) Token: 0x06000755 RID: 1877 RVA: 0x000230E8 File Offset: 0x000212E8
		public string DebugLookupList
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value in this.m_debugLookups)
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
				this.m_debugLookups.Clear();
				if (!string.IsNullOrEmpty(value))
				{
					foreach (string debugNamespace in value.Split(new char[]
					{
						',',
						';'
					}))
					{
						this.AddDebugLookup(debugNamespace);
					}
				}
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x0002313B File Offset: 0x0002133B
		// (set) Token: 0x06000757 RID: 1879 RVA: 0x00023143 File Offset: 0x00021343
		public bool AlwaysEscapeNonAscii { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0002314C File Offset: 0x0002134C
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x00023154 File Offset: 0x00021354
		public bool CollapseToLiteral { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0002315D File Offset: 0x0002135D
		// (set) Token: 0x0600075B RID: 1883 RVA: 0x00023165 File Offset: 0x00021365
		public bool ConstStatementsMozilla { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x0002316E File Offset: 0x0002136E
		// (set) Token: 0x0600075D RID: 1885 RVA: 0x00023176 File Offset: 0x00021376
		public bool ErrorIfNotInlineSafe { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0002317F File Offset: 0x0002137F
		// (set) Token: 0x0600075F RID: 1887 RVA: 0x00023187 File Offset: 0x00021387
		public bool EvalLiteralExpressions { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x00023190 File Offset: 0x00021390
		// (set) Token: 0x06000761 RID: 1889 RVA: 0x00023198 File Offset: 0x00021398
		public EvalTreatment EvalTreatment { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x000231A1 File Offset: 0x000213A1
		// (set) Token: 0x06000763 RID: 1891 RVA: 0x000231A9 File Offset: 0x000213A9
		public JavaScriptFormat Format { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x000231B2 File Offset: 0x000213B2
		// (set) Token: 0x06000765 RID: 1893 RVA: 0x000231BA File Offset: 0x000213BA
		public bool IgnoreConditionalCompilation { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x000231C3 File Offset: 0x000213C3
		// (set) Token: 0x06000767 RID: 1895 RVA: 0x000231CB File Offset: 0x000213CB
		public bool IgnorePreprocessorDefines { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x000231D4 File Offset: 0x000213D4
		// (set) Token: 0x06000769 RID: 1897 RVA: 0x000231DC File Offset: 0x000213DC
		public bool InlineSafeStrings { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x000231E5 File Offset: 0x000213E5
		// (set) Token: 0x0600076B RID: 1899 RVA: 0x000231ED File Offset: 0x000213ED
		public LocalRenaming LocalRenaming { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x000231F6 File Offset: 0x000213F6
		// (set) Token: 0x0600076D RID: 1901 RVA: 0x000231FE File Offset: 0x000213FE
		public bool MacSafariQuirks { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x00023207 File Offset: 0x00021407
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x00023210 File Offset: 0x00021410
		public bool MinifyCode
		{
			get
			{
				return this.m_minify;
			}
			set
			{
				this.m_minify = value;
				this.CollapseToLiteral = this.m_minify;
				this.EvalLiteralExpressions = this.m_minify;
				this.RemoveFunctionExpressionNames = this.m_minify;
				this.RemoveUnneededCode = this.m_minify;
				this.ReorderScopeDeclarations = this.m_minify;
				this.PreserveFunctionNames = !this.m_minify;
				this.PreserveImportantComments = !this.m_minify;
				this.LocalRenaming = (this.m_minify ? LocalRenaming.CrunchAll : LocalRenaming.KeepAll);
				base.KillSwitch = (this.m_minify ? 0L : -2L);
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x000232A5 File Offset: 0x000214A5
		// (set) Token: 0x06000771 RID: 1905 RVA: 0x000232AD File Offset: 0x000214AD
		public bool ManualRenamesProperties { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x000232B6 File Offset: 0x000214B6
		// (set) Token: 0x06000773 RID: 1907 RVA: 0x000232BE File Offset: 0x000214BE
		public bool PreprocessOnly { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x000232C7 File Offset: 0x000214C7
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x000232CF File Offset: 0x000214CF
		public bool PreserveFunctionNames { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x000232D8 File Offset: 0x000214D8
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x000232E0 File Offset: 0x000214E0
		public bool PreserveImportantComments { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x000232E9 File Offset: 0x000214E9
		// (set) Token: 0x06000779 RID: 1913 RVA: 0x000232F1 File Offset: 0x000214F1
		public bool QuoteObjectLiteralProperties { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x000232FA File Offset: 0x000214FA
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x00023302 File Offset: 0x00021502
		public bool ReorderScopeDeclarations { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x0002330B File Offset: 0x0002150B
		// (set) Token: 0x0600077D RID: 1917 RVA: 0x00023313 File Offset: 0x00021513
		public bool RemoveFunctionExpressionNames { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x0002331C File Offset: 0x0002151C
		// (set) Token: 0x0600077F RID: 1919 RVA: 0x00023324 File Offset: 0x00021524
		public bool RemoveUnneededCode { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x0002332D File Offset: 0x0002152D
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x00023335 File Offset: 0x00021535
		public ScriptVersion ScriptVersion { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x0002333E File Offset: 0x0002153E
		// (set) Token: 0x06000783 RID: 1923 RVA: 0x00023346 File Offset: 0x00021546
		public JavaScriptSourceMode SourceMode { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x0002334F File Offset: 0x0002154F
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x00023357 File Offset: 0x00021557
		public bool StrictMode { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x00023360 File Offset: 0x00021560
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x00023368 File Offset: 0x00021568
		public bool StripDebugStatements { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x00023371 File Offset: 0x00021571
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x00023379 File Offset: 0x00021579
		public ISourceMap SymbolsMap { get; set; }

		// Token: 0x0600078A RID: 1930 RVA: 0x00023382 File Offset: 0x00021582
		public bool IsModificationAllowed(TreeModifications modification)
		{
			return (base.KillSwitch & (long)modification) == 0L;
		}

		// Token: 0x0400028E RID: 654
		private bool m_minify;

		// Token: 0x0400028F RID: 655
		private Dictionary<string, string> m_identifierReplacementMap;

		// Token: 0x04000290 RID: 656
		private HashSet<string> m_noRenameSet;

		// Token: 0x04000291 RID: 657
		private HashSet<string> m_knownGlobals;

		// Token: 0x04000292 RID: 658
		private HashSet<string> m_debugLookups;
	}
}
