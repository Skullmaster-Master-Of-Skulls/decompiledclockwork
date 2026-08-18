using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Optimization.Resources;

namespace System.Web.Optimization
{
	// Token: 0x0200001B RID: 27
	public sealed class IgnoreList
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x0000443A File Offset: 0x0000263A
		public IgnoreList()
		{
			this.InitializeMatches();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004448 File Offset: 0x00002648
		public void Clear()
		{
			this.InitializeMatches();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004450 File Offset: 0x00002650
		private void InitializeMatches()
		{
			this._exactAlways = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			this._exactWhenOptimized = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			this._exactWhenUnoptimized = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			this._matches = new List<IgnoreList.IgnoreMatch>();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000448D File Offset: 0x0000268D
		public void Ignore(string item)
		{
			this.Ignore(item, OptimizationMode.Always);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004498 File Offset: 0x00002698
		private static Exception ValidateIgnoreMode(OptimizationMode mode, string argName)
		{
			switch (mode)
			{
			case OptimizationMode.Always:
			case OptimizationMode.WhenEnabled:
			case OptimizationMode.WhenDisabled:
				return null;
			default:
				return new ArgumentException(OptimizationResources.InvalidOptimizationMode, argName);
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000044C8 File Offset: 0x000026C8
		public void Ignore(string pattern, OptimizationMode mode)
		{
			if (string.IsNullOrEmpty(pattern))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("pattern");
			}
			PatternType patternType = PatternHelper.GetPatternType(pattern);
			Exception ex = PatternHelper.ValidatePattern(patternType, pattern, "item");
			if (ex != null)
			{
				throw ex;
			}
			ex = IgnoreList.ValidateIgnoreMode(mode, "mode");
			if (ex != null)
			{
				throw ex;
			}
			switch (patternType)
			{
			case PatternType.Exact:
				switch (mode)
				{
				case OptimizationMode.Always:
					this._exactAlways.Add(pattern);
					return;
				case OptimizationMode.WhenEnabled:
					this._exactWhenOptimized.Add(pattern);
					return;
				case OptimizationMode.WhenDisabled:
					this._exactWhenUnoptimized.Add(pattern);
					return;
				default:
					return;
				}
				break;
			case PatternType.All:
				this._matches.Add(new IgnoreList.AllMatch(mode));
				return;
			case PatternType.Suffix:
				this._matches.Add(new IgnoreList.SuffixMatch(pattern.Substring(1), mode));
				return;
			case PatternType.Prefix:
				this._matches.Add(new IgnoreList.PrefixMatch(pattern.Substring(0, pattern.Length - 1), mode));
				return;
			case PatternType.Version:
				this._matches.Add(new IgnoreList.VersionMatch(pattern, mode));
				return;
			default:
				return;
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000045F4 File Offset: 0x000027F4
		public bool ShouldIgnore(BundleContext context, string fileName)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			bool optimizationEnabled = context.EnableOptimizations;
			return string.IsNullOrEmpty(fileName) || this._exactAlways.Contains(fileName) || (optimizationEnabled && this._exactWhenOptimized.Contains(fileName)) || (!optimizationEnabled && this._exactWhenUnoptimized.Contains(fileName)) || this._matches.Any((IgnoreList.IgnoreMatch m) => m.UseMatch(optimizationEnabled) && m.IsMatch(fileName));
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000046C0 File Offset: 0x000028C0
		public IEnumerable<BundleFile> FilterIgnoredFiles(BundleContext context, IEnumerable<BundleFile> files)
		{
			return from f in files
			where !this.ShouldIgnore(context, f.VirtualFile.Name)
			select f;
		}

		// Token: 0x0400004B RID: 75
		private HashSet<string> _exactAlways;

		// Token: 0x0400004C RID: 76
		private HashSet<string> _exactWhenOptimized;

		// Token: 0x0400004D RID: 77
		private HashSet<string> _exactWhenUnoptimized;

		// Token: 0x0400004E RID: 78
		private List<IgnoreList.IgnoreMatch> _matches;

		// Token: 0x0200001C RID: 28
		private abstract class IgnoreMatch
		{
			// Token: 0x060000F8 RID: 248 RVA: 0x000046F3 File Offset: 0x000028F3
			public IgnoreMatch()
			{
			}

			// Token: 0x060000F9 RID: 249 RVA: 0x000046FB File Offset: 0x000028FB
			public IgnoreMatch(string pattern, OptimizationMode mode)
			{
				this.Mode = mode;
				this.Pattern = pattern;
			}

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x060000FA RID: 250 RVA: 0x00004711 File Offset: 0x00002911
			// (set) Token: 0x060000FB RID: 251 RVA: 0x00004719 File Offset: 0x00002919
			public OptimizationMode Mode { get; set; }

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x060000FC RID: 252 RVA: 0x00004722 File Offset: 0x00002922
			// (set) Token: 0x060000FD RID: 253 RVA: 0x0000472A File Offset: 0x0000292A
			public string Pattern { get; set; }

			// Token: 0x060000FE RID: 254 RVA: 0x00004734 File Offset: 0x00002934
			public bool UseMatch(bool optimizationMode)
			{
				switch (this.Mode)
				{
				case OptimizationMode.Always:
					return true;
				case OptimizationMode.WhenEnabled:
					return optimizationMode;
				case OptimizationMode.WhenDisabled:
					return !optimizationMode;
				default:
					return false;
				}
			}

			// Token: 0x060000FF RID: 255
			public abstract bool IsMatch(string input);
		}

		// Token: 0x0200001D RID: 29
		private sealed class AllMatch : IgnoreList.IgnoreMatch
		{
			// Token: 0x06000100 RID: 256 RVA: 0x00004766 File Offset: 0x00002966
			public AllMatch(OptimizationMode mode)
			{
				base.Mode = mode;
			}

			// Token: 0x06000101 RID: 257 RVA: 0x00004775 File Offset: 0x00002975
			public override bool IsMatch(string input)
			{
				return true;
			}
		}

		// Token: 0x0200001E RID: 30
		private sealed class PrefixMatch : IgnoreList.IgnoreMatch
		{
			// Token: 0x06000102 RID: 258 RVA: 0x00004778 File Offset: 0x00002978
			public PrefixMatch(string pattern, OptimizationMode mode) : base(pattern, mode)
			{
			}

			// Token: 0x06000103 RID: 259 RVA: 0x00004782 File Offset: 0x00002982
			public override bool IsMatch(string input)
			{
				return input.StartsWith(base.Pattern, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x0200001F RID: 31
		private sealed class SuffixMatch : IgnoreList.IgnoreMatch
		{
			// Token: 0x06000104 RID: 260 RVA: 0x00004791 File Offset: 0x00002991
			public SuffixMatch(string pattern, OptimizationMode mode) : base(pattern, mode)
			{
			}

			// Token: 0x06000105 RID: 261 RVA: 0x0000479B File Offset: 0x0000299B
			public override bool IsMatch(string input)
			{
				return input.EndsWith(base.Pattern, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x02000020 RID: 32
		private sealed class VersionMatch : IgnoreList.IgnoreMatch
		{
			// Token: 0x06000106 RID: 262 RVA: 0x000047AA File Offset: 0x000029AA
			public VersionMatch(string pattern, OptimizationMode mode) : base(pattern, mode)
			{
				this.RegEx = PatternHelper.BuildRegex(base.Pattern);
			}

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x06000107 RID: 263 RVA: 0x000047C5 File Offset: 0x000029C5
			// (set) Token: 0x06000108 RID: 264 RVA: 0x000047CD File Offset: 0x000029CD
			private Regex RegEx { get; set; }

			// Token: 0x06000109 RID: 265 RVA: 0x000047D6 File Offset: 0x000029D6
			public override bool IsMatch(string input)
			{
				return this.RegEx.IsMatch(input);
			}
		}
	}
}
