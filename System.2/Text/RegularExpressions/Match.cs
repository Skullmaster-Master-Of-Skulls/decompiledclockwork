using System;
using System.Security.Permissions;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200069C RID: 1692
	[__DynamicallyInvokable]
	[Serializable]
	public class Match : Group
	{
		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06003F0F RID: 16143 RVA: 0x00107559 File Offset: 0x00105759
		[__DynamicallyInvokable]
		public static Match Empty
		{
			[__DynamicallyInvokable]
			get
			{
				return Match._empty;
			}
		}

		// Token: 0x06003F10 RID: 16144 RVA: 0x00107560 File Offset: 0x00105760
		internal Match(Regex regex, int capcount, string text, int begpos, int len, int startpos) : base(text, new int[2], 0, "0")
		{
			this._regex = regex;
			this._matchcount = new int[capcount];
			this._matches = new int[capcount][];
			this._matches[0] = this._caps;
			this._textbeg = begpos;
			this._textend = begpos + len;
			this._textstart = startpos;
			this._balancing = false;
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x001075D0 File Offset: 0x001057D0
		internal virtual void Reset(Regex regex, string text, int textbeg, int textend, int textstart)
		{
			this._regex = regex;
			this._text = text;
			this._textbeg = textbeg;
			this._textend = textend;
			this._textstart = textstart;
			for (int i = 0; i < this._matchcount.Length; i++)
			{
				this._matchcount[i] = 0;
			}
			this._balancing = false;
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06003F12 RID: 16146 RVA: 0x00107625 File Offset: 0x00105825
		[__DynamicallyInvokable]
		public virtual GroupCollection Groups
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._groupcoll == null)
				{
					this._groupcoll = new GroupCollection(this, null);
				}
				return this._groupcoll;
			}
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x00107642 File Offset: 0x00105842
		[__DynamicallyInvokable]
		public Match NextMatch()
		{
			if (this._regex == null)
			{
				return this;
			}
			return this._regex.Run(false, this._length, this._text, this._textbeg, this._textend - this._textbeg, this._textpos);
		}

		// Token: 0x06003F14 RID: 16148 RVA: 0x00107680 File Offset: 0x00105880
		[__DynamicallyInvokable]
		public virtual string Result(string replacement)
		{
			if (replacement == null)
			{
				throw new ArgumentNullException("replacement");
			}
			if (this._regex == null)
			{
				throw new NotSupportedException(SR.GetString("NoResultOnFailed"));
			}
			RegexReplacement regexReplacement = (RegexReplacement)this._regex.replref.Get();
			if (regexReplacement == null || !regexReplacement.Pattern.Equals(replacement))
			{
				regexReplacement = RegexParser.ParseReplacement(replacement, this._regex.caps, this._regex.capsize, this._regex.capnames, this._regex.roptions);
				this._regex.replref.Cache(regexReplacement);
			}
			return regexReplacement.Replacement(this);
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x00107728 File Offset: 0x00105928
		internal virtual string GroupToStringImpl(int groupnum)
		{
			int num = this._matchcount[groupnum];
			if (num == 0)
			{
				return string.Empty;
			}
			int[] array = this._matches[groupnum];
			return this._text.Substring(array[(num - 1) * 2], array[num * 2 - 1]);
		}

		// Token: 0x06003F16 RID: 16150 RVA: 0x00107769 File Offset: 0x00105969
		internal string LastGroupToStringImpl()
		{
			return this.GroupToStringImpl(this._matchcount.Length - 1);
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x0010777C File Offset: 0x0010597C
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static Match Synchronized(Match inner)
		{
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			int num = inner._matchcount.Length;
			for (int i = 0; i < num; i++)
			{
				Group inner2 = inner.Groups[i];
				Group.Synchronized(inner2);
			}
			return inner;
		}

		// Token: 0x06003F18 RID: 16152 RVA: 0x001077C4 File Offset: 0x001059C4
		internal virtual void AddMatch(int cap, int start, int len)
		{
			if (this._matches[cap] == null)
			{
				this._matches[cap] = new int[2];
			}
			int num = this._matchcount[cap];
			if (num * 2 + 2 > this._matches[cap].Length)
			{
				int[] array = this._matches[cap];
				int[] array2 = new int[num * 8];
				for (int i = 0; i < num * 2; i++)
				{
					array2[i] = array[i];
				}
				this._matches[cap] = array2;
			}
			this._matches[cap][num * 2] = start;
			this._matches[cap][num * 2 + 1] = len;
			this._matchcount[cap] = num + 1;
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x0010785C File Offset: 0x00105A5C
		internal virtual void BalanceMatch(int cap)
		{
			this._balancing = true;
			int num = this._matchcount[cap];
			int num2 = num * 2 - 2;
			if (this._matches[cap][num2] < 0)
			{
				num2 = -3 - this._matches[cap][num2];
			}
			num2 -= 2;
			if (num2 >= 0 && this._matches[cap][num2] < 0)
			{
				this.AddMatch(cap, this._matches[cap][num2], this._matches[cap][num2 + 1]);
				return;
			}
			this.AddMatch(cap, -3 - num2, -4 - num2);
		}

		// Token: 0x06003F1A RID: 16154 RVA: 0x001078DC File Offset: 0x00105ADC
		internal virtual void RemoveMatch(int cap)
		{
			this._matchcount[cap]--;
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x001078EF File Offset: 0x00105AEF
		internal virtual bool IsMatched(int cap)
		{
			return cap < this._matchcount.Length && this._matchcount[cap] > 0 && this._matches[cap][this._matchcount[cap] * 2 - 1] != -2;
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x00107928 File Offset: 0x00105B28
		internal virtual int MatchIndex(int cap)
		{
			int num = this._matches[cap][this._matchcount[cap] * 2 - 2];
			if (num >= 0)
			{
				return num;
			}
			return this._matches[cap][-3 - num];
		}

		// Token: 0x06003F1D RID: 16157 RVA: 0x00107960 File Offset: 0x00105B60
		internal virtual int MatchLength(int cap)
		{
			int num = this._matches[cap][this._matchcount[cap] * 2 - 1];
			if (num >= 0)
			{
				return num;
			}
			return this._matches[cap][-3 - num];
		}

		// Token: 0x06003F1E RID: 16158 RVA: 0x00107998 File Offset: 0x00105B98
		internal virtual void Tidy(int textpos)
		{
			int[] array = this._matches[0];
			this._index = array[0];
			this._length = array[1];
			this._textpos = textpos;
			this._capcount = this._matchcount[0];
			if (this._balancing)
			{
				for (int i = 0; i < this._matchcount.Length; i++)
				{
					int num = this._matchcount[i] * 2;
					int[] array2 = this._matches[i];
					int j = 0;
					while (j < num && array2[j] >= 0)
					{
						j++;
					}
					int num2 = j;
					while (j < num)
					{
						if (array2[j] < 0)
						{
							num2--;
						}
						else
						{
							if (j != num2)
							{
								array2[num2] = array2[j];
							}
							num2++;
						}
						j++;
					}
					this._matchcount[i] = num2 / 2;
				}
				this._balancing = false;
			}
		}

		// Token: 0x04002DF5 RID: 11765
		internal static Match _empty = new Match(null, 1, string.Empty, 0, 0, 0);

		// Token: 0x04002DF6 RID: 11766
		internal GroupCollection _groupcoll;

		// Token: 0x04002DF7 RID: 11767
		internal Regex _regex;

		// Token: 0x04002DF8 RID: 11768
		internal int _textbeg;

		// Token: 0x04002DF9 RID: 11769
		internal int _textpos;

		// Token: 0x04002DFA RID: 11770
		internal int _textend;

		// Token: 0x04002DFB RID: 11771
		internal int _textstart;

		// Token: 0x04002DFC RID: 11772
		internal int[][] _matches;

		// Token: 0x04002DFD RID: 11773
		internal int[] _matchcount;

		// Token: 0x04002DFE RID: 11774
		internal bool _balancing;
	}
}
