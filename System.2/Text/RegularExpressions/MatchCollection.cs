using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200069E RID: 1694
	[__DynamicallyInvokable]
	[Serializable]
	public class MatchCollection : ICollection, IEnumerable
	{
		// Token: 0x06003F22 RID: 16162 RVA: 0x00107ABC File Offset: 0x00105CBC
		internal MatchCollection(Regex regex, string input, int beginning, int length, int startat)
		{
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat", SR.GetString("BeginIndexNotNegative"));
			}
			this._regex = regex;
			this._input = input;
			this._beginning = beginning;
			this._length = length;
			this._startat = startat;
			this._prevlen = -1;
			this._matches = new ArrayList();
			this._done = false;
		}

		// Token: 0x06003F23 RID: 16163 RVA: 0x00107B34 File Offset: 0x00105D34
		internal Match GetMatch(int i)
		{
			if (i < 0)
			{
				return null;
			}
			if (this._matches.Count > i)
			{
				return (Match)this._matches[i];
			}
			if (this._done)
			{
				return null;
			}
			for (;;)
			{
				Match match = this._regex.Run(false, this._prevlen, this._input, this._beginning, this._length, this._startat);
				if (!match.Success)
				{
					break;
				}
				this._matches.Add(match);
				this._prevlen = match._length;
				this._startat = match._textpos;
				if (this._matches.Count > i)
				{
					return match;
				}
			}
			this._done = true;
			return null;
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06003F24 RID: 16164 RVA: 0x00107BE1 File Offset: 0x00105DE1
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._done)
				{
					return this._matches.Count;
				}
				this.GetMatch(MatchCollection.infinite);
				return this._matches.Count;
			}
		}

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06003F25 RID: 16165 RVA: 0x00107C0E File Offset: 0x00105E0E
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06003F26 RID: 16166 RVA: 0x00107C11 File Offset: 0x00105E11
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06003F27 RID: 16167 RVA: 0x00107C14 File Offset: 0x00105E14
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000ED6 RID: 3798
		[__DynamicallyInvokable]
		public virtual Match this[int i]
		{
			[__DynamicallyInvokable]
			get
			{
				Match match = this.GetMatch(i);
				if (match == null)
				{
					throw new ArgumentOutOfRangeException("i");
				}
				return match;
			}
		}

		// Token: 0x06003F29 RID: 16169 RVA: 0x00107C3C File Offset: 0x00105E3C
		public void CopyTo(Array array, int arrayIndex)
		{
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException(SR.GetString("Arg_RankMultiDimNotSupported"));
			}
			int count = this.Count;
			try
			{
				this._matches.CopyTo(array, arrayIndex);
			}
			catch (ArrayTypeMismatchException innerException)
			{
				throw new ArgumentException(SR.GetString("Arg_InvalidArrayType"), innerException);
			}
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x00107CA0 File Offset: 0x00105EA0
		[__DynamicallyInvokable]
		public IEnumerator GetEnumerator()
		{
			return new MatchEnumerator(this);
		}

		// Token: 0x04002E00 RID: 11776
		internal Regex _regex;

		// Token: 0x04002E01 RID: 11777
		internal ArrayList _matches;

		// Token: 0x04002E02 RID: 11778
		internal bool _done;

		// Token: 0x04002E03 RID: 11779
		internal string _input;

		// Token: 0x04002E04 RID: 11780
		internal int _beginning;

		// Token: 0x04002E05 RID: 11781
		internal int _length;

		// Token: 0x04002E06 RID: 11782
		internal int _startat;

		// Token: 0x04002E07 RID: 11783
		internal int _prevlen;

		// Token: 0x04002E08 RID: 11784
		private static int infinite = int.MaxValue;
	}
}
