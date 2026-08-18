using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000686 RID: 1670
	[__DynamicallyInvokable]
	[Serializable]
	public class Regex : ISerializable
	{
		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06003D9A RID: 15770 RVA: 0x000FCB3B File Offset: 0x000FAD3B
		// (set) Token: 0x06003D9B RID: 15771 RVA: 0x000FCB43 File Offset: 0x000FAD43
		[CLSCompliant(false)]
		protected IDictionary Caps
		{
			get
			{
				return this.caps;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.caps = (value as Hashtable);
				if (this.caps == null)
				{
					this.caps = new Hashtable(value);
				}
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06003D9C RID: 15772 RVA: 0x000FCB73 File Offset: 0x000FAD73
		// (set) Token: 0x06003D9D RID: 15773 RVA: 0x000FCB7B File Offset: 0x000FAD7B
		[CLSCompliant(false)]
		protected IDictionary CapNames
		{
			get
			{
				return this.capnames;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.capnames = (value as Hashtable);
				if (this.capnames == null)
				{
					this.capnames = new Hashtable(value);
				}
			}
		}

		// Token: 0x06003D9E RID: 15774 RVA: 0x000FCBAB File Offset: 0x000FADAB
		[__DynamicallyInvokable]
		protected Regex()
		{
			this.internalMatchTimeout = Regex.DefaultMatchTimeout;
		}

		// Token: 0x06003D9F RID: 15775 RVA: 0x000FCBBE File Offset: 0x000FADBE
		[__DynamicallyInvokable]
		public Regex(string pattern) : this(pattern, RegexOptions.None, Regex.DefaultMatchTimeout, false)
		{
		}

		// Token: 0x06003DA0 RID: 15776 RVA: 0x000FCBCE File Offset: 0x000FADCE
		[__DynamicallyInvokable]
		public Regex(string pattern, RegexOptions options) : this(pattern, options, Regex.DefaultMatchTimeout, false)
		{
		}

		// Token: 0x06003DA1 RID: 15777 RVA: 0x000FCBDE File Offset: 0x000FADDE
		[__DynamicallyInvokable]
		public Regex(string pattern, RegexOptions options, TimeSpan matchTimeout) : this(pattern, options, matchTimeout, false)
		{
		}

		// Token: 0x06003DA2 RID: 15778 RVA: 0x000FCBEC File Offset: 0x000FADEC
		private Regex(string pattern, RegexOptions options, TimeSpan matchTimeout, bool useCache)
		{
			if (pattern == null)
			{
				throw new ArgumentNullException("pattern");
			}
			if (options < RegexOptions.None || options >> 10 != RegexOptions.None)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			if ((options & RegexOptions.ECMAScript) != RegexOptions.None && (options & ~(RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.ECMAScript | RegexOptions.CultureInvariant)) != RegexOptions.None)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			Regex.ValidateMatchTimeout(matchTimeout);
			string text;
			if ((options & RegexOptions.CultureInvariant) != RegexOptions.None)
			{
				text = CultureInfo.InvariantCulture.ToString();
			}
			else
			{
				text = CultureInfo.CurrentCulture.ToString();
			}
			string[] array = new string[5];
			int num = 0;
			int num2 = (int)options;
			array[num] = num2.ToString(NumberFormatInfo.InvariantInfo);
			array[1] = ":";
			array[2] = text;
			array[3] = ":";
			array[4] = pattern;
			string key = string.Concat(array);
			CachedCodeEntry cachedCodeEntry = Regex.LookupCachedAndUpdate(key);
			this.pattern = pattern;
			this.roptions = options;
			this.internalMatchTimeout = matchTimeout;
			if (cachedCodeEntry == null)
			{
				RegexTree regexTree = RegexParser.Parse(pattern, this.roptions);
				this.capnames = regexTree._capnames;
				this.capslist = regexTree._capslist;
				this.code = RegexWriter.Write(regexTree);
				this.caps = this.code._caps;
				this.capsize = this.code._capsize;
				this.InitializeReferences();
				if (useCache)
				{
					cachedCodeEntry = this.CacheCode(key);
				}
			}
			else
			{
				this.caps = cachedCodeEntry._caps;
				this.capnames = cachedCodeEntry._capnames;
				this.capslist = cachedCodeEntry._capslist;
				this.capsize = cachedCodeEntry._capsize;
				this.code = cachedCodeEntry._code;
				this.factory = cachedCodeEntry._factory;
				this.runnerref = cachedCodeEntry._runnerref;
				this.replref = cachedCodeEntry._replref;
				this.refsInitialized = true;
			}
			if (this.UseOptionC() && this.factory == null)
			{
				this.factory = this.Compile(this.code, this.roptions);
				if (useCache && cachedCodeEntry != null)
				{
					cachedCodeEntry.AddCompiled(this.factory);
				}
				this.code = null;
			}
		}

		// Token: 0x06003DA3 RID: 15779 RVA: 0x000FCDD4 File Offset: 0x000FAFD4
		protected Regex(SerializationInfo info, StreamingContext context) : this(info.GetString("pattern"), (RegexOptions)info.GetInt32("options"))
		{
			try
			{
				long @int = info.GetInt64("matchTimeout");
				TimeSpan matchTimeout = new TimeSpan(@int);
				Regex.ValidateMatchTimeout(matchTimeout);
				this.internalMatchTimeout = matchTimeout;
			}
			catch (SerializationException)
			{
			}
		}

		// Token: 0x06003DA4 RID: 15780 RVA: 0x000FCE34 File Offset: 0x000FB034
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			si.AddValue("pattern", this.ToString());
			si.AddValue("options", this.Options);
			si.AddValue("matchTimeout", this.MatchTimeout.Ticks);
		}

		// Token: 0x06003DA5 RID: 15781 RVA: 0x000FCE81 File Offset: 0x000FB081
		protected internal static void ValidateMatchTimeout(TimeSpan matchTimeout)
		{
			if (Regex.InfiniteMatchTimeout == matchTimeout)
			{
				return;
			}
			if (TimeSpan.Zero < matchTimeout && matchTimeout <= Regex.MaximumMatchTimeout)
			{
				return;
			}
			throw new ArgumentOutOfRangeException("matchTimeout");
		}

		// Token: 0x06003DA6 RID: 15782 RVA: 0x000FCEB8 File Offset: 0x000FB0B8
		private static TimeSpan InitDefaultMatchTimeout()
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			object data = currentDomain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT");
			if (data == null)
			{
				return Regex.FallbackDefaultMatchTimeout;
			}
			if (!(data is TimeSpan))
			{
				throw new InvalidCastException(SR.GetString("IllegalDefaultRegexMatchTimeoutInAppDomain", new object[]
				{
					"REGEX_DEFAULT_MATCH_TIMEOUT"
				}));
			}
			TimeSpan timeSpan = (TimeSpan)data;
			try
			{
				Regex.ValidateMatchTimeout(timeSpan);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new ArgumentOutOfRangeException(SR.GetString("IllegalDefaultRegexMatchTimeoutInAppDomain", new object[]
				{
					"REGEX_DEFAULT_MATCH_TIMEOUT"
				}));
			}
			return timeSpan;
		}

		// Token: 0x06003DA7 RID: 15783 RVA: 0x000FCF48 File Offset: 0x000FB148
		[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal RegexRunnerFactory Compile(RegexCode code, RegexOptions roptions)
		{
			return RegexCompiler.Compile(code, roptions);
		}

		// Token: 0x06003DA8 RID: 15784 RVA: 0x000FCF51 File Offset: 0x000FB151
		[__DynamicallyInvokable]
		public static string Escape(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return RegexParser.Escape(str);
		}

		// Token: 0x06003DA9 RID: 15785 RVA: 0x000FCF67 File Offset: 0x000FB167
		[__DynamicallyInvokable]
		public static string Unescape(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return RegexParser.Unescape(str);
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06003DAA RID: 15786 RVA: 0x000FCF7D File Offset: 0x000FB17D
		// (set) Token: 0x06003DAB RID: 15787 RVA: 0x000FCF84 File Offset: 0x000FB184
		[__DynamicallyInvokable]
		public static int CacheSize
		{
			[__DynamicallyInvokable]
			get
			{
				return Regex.cacheSize;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				Regex.cacheSize = value;
				if (Regex.livecode.Count > Regex.cacheSize)
				{
					LinkedList<CachedCodeEntry> obj = Regex.livecode;
					lock (obj)
					{
						while (Regex.livecode.Count > Regex.cacheSize)
						{
							Regex.livecode.RemoveLast();
						}
					}
				}
			}
		}

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06003DAC RID: 15788 RVA: 0x000FD000 File Offset: 0x000FB200
		[__DynamicallyInvokable]
		public RegexOptions Options
		{
			[__DynamicallyInvokable]
			get
			{
				return this.roptions;
			}
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06003DAD RID: 15789 RVA: 0x000FD008 File Offset: 0x000FB208
		[__DynamicallyInvokable]
		public TimeSpan MatchTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return this.internalMatchTimeout;
			}
		}

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06003DAE RID: 15790 RVA: 0x000FD010 File Offset: 0x000FB210
		[__DynamicallyInvokable]
		public bool RightToLeft
		{
			[__DynamicallyInvokable]
			get
			{
				return this.UseOptionR();
			}
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x000FD018 File Offset: 0x000FB218
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.pattern;
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x000FD020 File Offset: 0x000FB220
		[__DynamicallyInvokable]
		public string[] GetGroupNames()
		{
			string[] array;
			if (this.capslist == null)
			{
				int num = this.capsize;
				array = new string[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = Convert.ToString(i, CultureInfo.InvariantCulture);
				}
			}
			else
			{
				array = new string[this.capslist.Length];
				Array.Copy(this.capslist, 0, array, 0, this.capslist.Length);
			}
			return array;
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x000FD084 File Offset: 0x000FB284
		[__DynamicallyInvokable]
		public int[] GetGroupNumbers()
		{
			int[] array;
			if (this.caps == null)
			{
				int num = this.capsize;
				array = new int[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = i;
				}
			}
			else
			{
				array = new int[this.caps.Count];
				IDictionaryEnumerator enumerator = this.caps.GetEnumerator();
				while (enumerator.MoveNext())
				{
					array[(int)enumerator.Value] = (int)enumerator.Key;
				}
			}
			return array;
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x000FD0FC File Offset: 0x000FB2FC
		[__DynamicallyInvokable]
		public string GroupNameFromNumber(int i)
		{
			if (this.capslist == null)
			{
				if (i >= 0 && i < this.capsize)
				{
					return i.ToString(CultureInfo.InvariantCulture);
				}
				return string.Empty;
			}
			else
			{
				if (this.caps != null)
				{
					object obj = this.caps[i];
					if (obj == null)
					{
						return string.Empty;
					}
					i = (int)obj;
				}
				if (i >= 0 && i < this.capslist.Length)
				{
					return this.capslist[i];
				}
				return string.Empty;
			}
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x000FD17C File Offset: 0x000FB37C
		[__DynamicallyInvokable]
		public int GroupNumberFromName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.capnames != null)
			{
				object obj = this.capnames[name];
				if (obj == null)
				{
					return -1;
				}
				return (int)obj;
			}
			else
			{
				int num = 0;
				foreach (char c in name)
				{
					if (c > '9' || c < '0')
					{
						return -1;
					}
					num *= 10;
					num += (int)(c - '0');
				}
				if (num >= 0 && num < this.capsize)
				{
					return num;
				}
				return -1;
			}
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x000FD1FD File Offset: 0x000FB3FD
		[__DynamicallyInvokable]
		public static bool IsMatch(string input, string pattern)
		{
			return Regex.IsMatch(input, pattern, RegexOptions.None, Regex.DefaultMatchTimeout);
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x000FD20C File Offset: 0x000FB40C
		[__DynamicallyInvokable]
		public static bool IsMatch(string input, string pattern, RegexOptions options)
		{
			return Regex.IsMatch(input, pattern, options, Regex.DefaultMatchTimeout);
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x000FD21B File Offset: 0x000FB41B
		[__DynamicallyInvokable]
		public static bool IsMatch(string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).IsMatch(input);
		}

		// Token: 0x06003DB7 RID: 15799 RVA: 0x000FD22C File Offset: 0x000FB42C
		[__DynamicallyInvokable]
		public bool IsMatch(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.IsMatch(input, this.UseOptionR() ? input.Length : 0);
		}

		// Token: 0x06003DB8 RID: 15800 RVA: 0x000FD254 File Offset: 0x000FB454
		[__DynamicallyInvokable]
		public bool IsMatch(string input, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Run(true, -1, input, 0, input.Length, startat) == null;
		}

		// Token: 0x06003DB9 RID: 15801 RVA: 0x000FD278 File Offset: 0x000FB478
		[__DynamicallyInvokable]
		public static Match Match(string input, string pattern)
		{
			return Regex.Match(input, pattern, RegexOptions.None, Regex.DefaultMatchTimeout);
		}

		// Token: 0x06003DBA RID: 15802 RVA: 0x000FD287 File Offset: 0x000FB487
		[__DynamicallyInvokable]
		public static Match Match(string input, string pattern, RegexOptions options)
		{
			return Regex.Match(input, pattern, options, Regex.DefaultMatchTimeout);
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x000FD296 File Offset: 0x000FB496
		[__DynamicallyInvokable]
		public static Match Match(string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Match(input);
		}

		// Token: 0x06003DBC RID: 15804 RVA: 0x000FD2A7 File Offset: 0x000FB4A7
		[__DynamicallyInvokable]
		public Match Match(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Match(input, this.UseOptionR() ? input.Length : 0);
		}

		// Token: 0x06003DBD RID: 15805 RVA: 0x000FD2CF File Offset: 0x000FB4CF
		[__DynamicallyInvokable]
		public Match Match(string input, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Run(false, -1, input, 0, input.Length, startat);
		}

		// Token: 0x06003DBE RID: 15806 RVA: 0x000FD2F0 File Offset: 0x000FB4F0
		[__DynamicallyInvokable]
		public Match Match(string input, int beginning, int length)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Run(false, -1, input, beginning, length, this.UseOptionR() ? (beginning + length) : beginning);
		}

		// Token: 0x06003DBF RID: 15807 RVA: 0x000FD319 File Offset: 0x000FB519
		[__DynamicallyInvokable]
		public static MatchCollection Matches(string input, string pattern)
		{
			return Regex.Matches(input, pattern, RegexOptions.None, Regex.DefaultMatchTimeout);
		}

		// Token: 0x06003DC0 RID: 15808 RVA: 0x000FD328 File Offset: 0x000FB528
		[__DynamicallyInvokable]
		public static MatchCollection Matches(string input, string pattern, RegexOptions options)
		{
			return Regex.Matches(input, pattern, options, Regex.DefaultMatchTimeout);
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x000FD337 File Offset: 0x000FB537
		[__DynamicallyInvokable]
		public static MatchCollection Matches(string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Matches(input);
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x000FD348 File Offset: 0x000FB548
		[__DynamicallyInvokable]
		public MatchCollection Matches(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Matches(input, this.UseOptionR() ? input.Length : 0);
		}

		// Token: 0x06003DC3 RID: 15811 RVA: 0x000FD370 File Offset: 0x000FB570
		[__DynamicallyInvokable]
		public MatchCollection Matches(string input, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return new MatchCollection(this, input, 0, input.Length, startat);
		}

		// Token: 0x06003DC4 RID: 15812 RVA: 0x000FD38F File Offset: 0x000FB58F
		[__DynamicallyInvokable]
		public static string Replace(string input, string pattern, string replacement)
		{
			return Regex.Replace(input, pattern, replacement, RegexOptions.None, Regex.DefaultMatchTimeout);
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x000FD39F File Offset: 0x000FB59F
		[__DynamicallyInvokable]
		public static string Replace(string input, string pattern, string replacement, RegexOptions options)
		{
			return Regex.Replace(input, pattern, replacement, options, Regex.DefaultMatchTimeout);
		}

		// Token: 0x06003DC6 RID: 15814 RVA: 0x000FD3AF File Offset: 0x000FB5AF
		[__DynamicallyInvokable]
		public static string Replace(string input, string pattern, string replacement, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Replace(input, replacement);
		}

		// Token: 0x06003DC7 RID: 15815 RVA: 0x000FD3C2 File Offset: 0x000FB5C2
		[__DynamicallyInvokable]
		public string Replace(string input, string replacement)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Replace(input, replacement, -1, this.UseOptionR() ? input.Length : 0);
		}

		// Token: 0x06003DC8 RID: 15816 RVA: 0x000FD3EC File Offset: 0x000FB5EC
		[__DynamicallyInvokable]
		public string Replace(string input, string replacement, int count)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Replace(input, replacement, count, this.UseOptionR() ? input.Length : 0);
		}

		// Token: 0x06003DC9 RID: 15817 RVA: 0x000FD418 File Offset: 0x000FB618
		[__DynamicallyInvokable]
		public string Replace(string input, string replacement, int count, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (replacement == null)
			{
				throw new ArgumentNullException("replacement");
			}
			RegexReplacement regexReplacement = (RegexReplacement)this.replref.Get();
			if (regexReplacement == null || !regexReplacement.Pattern.Equals(replacement))
			{
				regexReplacement = RegexParser.ParseReplacement(replacement, this.caps, this.capsize, this.capnames, this.roptions);
				this.replref.Cache(regexReplacement);
			}
			return regexReplacement.Replace(this, input, count, startat);
		}

		// Token: 0x06003DCA RID: 15818 RVA: 0x000FD499 File Offset: 0x000FB699
		[__DynamicallyInvokable]
		public static string Replace(string input, string pattern, MatchEvaluator evaluator)
		{
			return Regex.Replace(input, pattern, evaluator, RegexOptions.None, Regex.DefaultMatchTimeout);
		}

		// Token: 0x06003DCB RID: 15819 RVA: 0x000FD4A9 File Offset: 0x000FB6A9
		[__DynamicallyInvokable]
		public static string Replace(string input, string pattern, MatchEvaluator evaluator, RegexOptions options)
		{
			return Regex.Replace(input, pattern, evaluator, options, Regex.DefaultMatchTimeout);
		}

		// Token: 0x06003DCC RID: 15820 RVA: 0x000FD4B9 File Offset: 0x000FB6B9
		[__DynamicallyInvokable]
		public static string Replace(string input, string pattern, MatchEvaluator evaluator, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Replace(input, evaluator);
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x000FD4CC File Offset: 0x000FB6CC
		[__DynamicallyInvokable]
		public string Replace(string input, MatchEvaluator evaluator)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Replace(input, evaluator, -1, this.UseOptionR() ? input.Length : 0);
		}

		// Token: 0x06003DCE RID: 15822 RVA: 0x000FD4F6 File Offset: 0x000FB6F6
		[__DynamicallyInvokable]
		public string Replace(string input, MatchEvaluator evaluator, int count)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Replace(input, evaluator, count, this.UseOptionR() ? input.Length : 0);
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x000FD520 File Offset: 0x000FB720
		[__DynamicallyInvokable]
		public string Replace(string input, MatchEvaluator evaluator, int count, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return RegexReplacement.Replace(evaluator, this, input, count, startat);
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x000FD53B File Offset: 0x000FB73B
		[__DynamicallyInvokable]
		public static string[] Split(string input, string pattern)
		{
			return Regex.Split(input, pattern, RegexOptions.None, Regex.DefaultMatchTimeout);
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x000FD54A File Offset: 0x000FB74A
		[__DynamicallyInvokable]
		public static string[] Split(string input, string pattern, RegexOptions options)
		{
			return Regex.Split(input, pattern, options, Regex.DefaultMatchTimeout);
		}

		// Token: 0x06003DD2 RID: 15826 RVA: 0x000FD559 File Offset: 0x000FB759
		[__DynamicallyInvokable]
		public static string[] Split(string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Split(input);
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x000FD56A File Offset: 0x000FB76A
		[__DynamicallyInvokable]
		public string[] Split(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Split(input, 0, this.UseOptionR() ? input.Length : 0);
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x000FD593 File Offset: 0x000FB793
		[__DynamicallyInvokable]
		public string[] Split(string input, int count)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return RegexReplacement.Split(this, input, count, this.UseOptionR() ? input.Length : 0);
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x000FD5BC File Offset: 0x000FB7BC
		[__DynamicallyInvokable]
		public string[] Split(string input, int count, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return RegexReplacement.Split(this, input, count, startat);
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x000FD5D5 File Offset: 0x000FB7D5
		[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
		public static void CompileToAssembly(RegexCompilationInfo[] regexinfos, AssemblyName assemblyname)
		{
			Regex.CompileToAssemblyInternal(regexinfos, assemblyname, null, null);
		}

		// Token: 0x06003DD7 RID: 15831 RVA: 0x000FD5E0 File Offset: 0x000FB7E0
		[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
		public static void CompileToAssembly(RegexCompilationInfo[] regexinfos, AssemblyName assemblyname, CustomAttributeBuilder[] attributes)
		{
			Regex.CompileToAssemblyInternal(regexinfos, assemblyname, attributes, null);
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x000FD5EB File Offset: 0x000FB7EB
		[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
		public static void CompileToAssembly(RegexCompilationInfo[] regexinfos, AssemblyName assemblyname, CustomAttributeBuilder[] attributes, string resourceFile)
		{
			Regex.CompileToAssemblyInternal(regexinfos, assemblyname, attributes, resourceFile);
		}

		// Token: 0x06003DD9 RID: 15833 RVA: 0x000FD5F6 File Offset: 0x000FB7F6
		private static void CompileToAssemblyInternal(RegexCompilationInfo[] regexinfos, AssemblyName assemblyname, CustomAttributeBuilder[] attributes, string resourceFile)
		{
			if (assemblyname == null)
			{
				throw new ArgumentNullException("assemblyname");
			}
			if (regexinfos == null)
			{
				throw new ArgumentNullException("regexinfos");
			}
			RegexCompiler.CompileToAssembly(regexinfos, assemblyname, attributes, resourceFile);
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x000FD61D File Offset: 0x000FB81D
		protected void InitializeReferences()
		{
			if (this.refsInitialized)
			{
				throw new NotSupportedException(SR.GetString("OnlyAllowedOnce"));
			}
			this.refsInitialized = true;
			this.runnerref = new ExclusiveReference();
			this.replref = new SharedReference();
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x000FD654 File Offset: 0x000FB854
		internal Match Run(bool quick, int prevlen, string input, int beginning, int length, int startat)
		{
			RegexRunner regexRunner = null;
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("start", SR.GetString("BeginIndexNotNegative"));
			}
			if (length < 0 || length > input.Length)
			{
				throw new ArgumentOutOfRangeException("length", SR.GetString("LengthNotNegative"));
			}
			regexRunner = (RegexRunner)this.runnerref.Get();
			if (regexRunner == null)
			{
				if (this.factory != null)
				{
					regexRunner = this.factory.CreateInstance();
				}
				else
				{
					regexRunner = new RegexInterpreter(this.code, this.UseOptionInvariant() ? CultureInfo.InvariantCulture : CultureInfo.CurrentCulture);
				}
			}
			Match result;
			try
			{
				result = regexRunner.Scan(this, input, beginning, beginning + length, startat, prevlen, quick, this.internalMatchTimeout);
			}
			finally
			{
				this.runnerref.Release(regexRunner);
			}
			return result;
		}

		// Token: 0x06003DDC RID: 15836 RVA: 0x000FD730 File Offset: 0x000FB930
		private static CachedCodeEntry LookupCachedAndUpdate(string key)
		{
			LinkedList<CachedCodeEntry> obj = Regex.livecode;
			lock (obj)
			{
				for (LinkedListNode<CachedCodeEntry> linkedListNode = Regex.livecode.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					if (linkedListNode.Value._key == key)
					{
						Regex.livecode.Remove(linkedListNode);
						Regex.livecode.AddFirst(linkedListNode);
						return linkedListNode.Value;
					}
				}
			}
			return null;
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x000FD7B8 File Offset: 0x000FB9B8
		private CachedCodeEntry CacheCode(string key)
		{
			CachedCodeEntry cachedCodeEntry = null;
			LinkedList<CachedCodeEntry> obj = Regex.livecode;
			lock (obj)
			{
				for (LinkedListNode<CachedCodeEntry> linkedListNode = Regex.livecode.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					if (linkedListNode.Value._key == key)
					{
						Regex.livecode.Remove(linkedListNode);
						Regex.livecode.AddFirst(linkedListNode);
						return linkedListNode.Value;
					}
				}
				if (Regex.cacheSize != 0)
				{
					cachedCodeEntry = new CachedCodeEntry(key, this.capnames, this.capslist, this.code, this.caps, this.capsize, this.runnerref, this.replref);
					Regex.livecode.AddFirst(cachedCodeEntry);
					if (Regex.livecode.Count > Regex.cacheSize)
					{
						Regex.livecode.RemoveLast();
					}
				}
			}
			return cachedCodeEntry;
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x000FD8A0 File Offset: 0x000FBAA0
		protected bool UseOptionC()
		{
			return (this.roptions & RegexOptions.Compiled) > RegexOptions.None;
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x000FD8AD File Offset: 0x000FBAAD
		protected bool UseOptionR()
		{
			return (this.roptions & RegexOptions.RightToLeft) > RegexOptions.None;
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x000FD8BB File Offset: 0x000FBABB
		internal bool UseOptionInvariant()
		{
			return (this.roptions & RegexOptions.CultureInvariant) > RegexOptions.None;
		}

		// Token: 0x04002CD6 RID: 11478
		protected internal string pattern;

		// Token: 0x04002CD7 RID: 11479
		protected internal RegexRunnerFactory factory;

		// Token: 0x04002CD8 RID: 11480
		protected internal RegexOptions roptions;

		// Token: 0x04002CD9 RID: 11481
		[NonSerialized]
		private static readonly TimeSpan MaximumMatchTimeout = TimeSpan.FromMilliseconds(2147483646.0);

		// Token: 0x04002CDA RID: 11482
		[__DynamicallyInvokable]
		[NonSerialized]
		public static readonly TimeSpan InfiniteMatchTimeout = Timeout.InfiniteTimeSpan;

		// Token: 0x04002CDB RID: 11483
		[OptionalField(VersionAdded = 2)]
		protected internal TimeSpan internalMatchTimeout;

		// Token: 0x04002CDC RID: 11484
		private const string DefaultMatchTimeout_ConfigKeyName = "REGEX_DEFAULT_MATCH_TIMEOUT";

		// Token: 0x04002CDD RID: 11485
		[NonSerialized]
		internal static readonly TimeSpan FallbackDefaultMatchTimeout = Regex.InfiniteMatchTimeout;

		// Token: 0x04002CDE RID: 11486
		[NonSerialized]
		internal static readonly TimeSpan DefaultMatchTimeout = Regex.InitDefaultMatchTimeout();

		// Token: 0x04002CDF RID: 11487
		protected internal Hashtable caps;

		// Token: 0x04002CE0 RID: 11488
		protected internal Hashtable capnames;

		// Token: 0x04002CE1 RID: 11489
		protected internal string[] capslist;

		// Token: 0x04002CE2 RID: 11490
		protected internal int capsize;

		// Token: 0x04002CE3 RID: 11491
		internal ExclusiveReference runnerref;

		// Token: 0x04002CE4 RID: 11492
		internal SharedReference replref;

		// Token: 0x04002CE5 RID: 11493
		internal RegexCode code;

		// Token: 0x04002CE6 RID: 11494
		internal bool refsInitialized;

		// Token: 0x04002CE7 RID: 11495
		internal static LinkedList<CachedCodeEntry> livecode = new LinkedList<CachedCodeEntry>();

		// Token: 0x04002CE8 RID: 11496
		internal static int cacheSize = 15;

		// Token: 0x04002CE9 RID: 11497
		internal const int MaxOptionShift = 10;
	}
}
