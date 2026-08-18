using System;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Caching;

namespace Telerik.Web.UI.Dictionaries
{
	// Token: 0x020011CF RID: 4559
	internal class EditDistanceDictionary : SpellDictionary
	{
		// Token: 0x17003CDC RID: 15580
		// (get) Token: 0x0600BC66 RID: 48230 RVA: 0x0029CB35 File Offset: 0x0029AD35
		// (set) Token: 0x0600BC67 RID: 48231 RVA: 0x0029CB3D File Offset: 0x0029AD3D
		protected WordComparer Comparer
		{
			get
			{
				return this._comparer;
			}
			set
			{
				this._comparer = value;
			}
		}

		// Token: 0x0600BC68 RID: 48232 RVA: 0x0029CB48 File Offset: 0x0029AD48
		internal EditDistanceDictionary(int editDistanceValue)
		{
			this.editDistance = editDistanceValue;
			this.Clear();
		}

		// Token: 0x0600BC69 RID: 48233 RVA: 0x0029CB98 File Offset: 0x0029AD98
		internal override void Load(TextReader baseDictionaryReader, ICustomDictionarySource customSource, string cacheKey)
		{
			this.CacheKey = cacheKey;
			base.LoadBaseDictionary(baseDictionaryReader);
			this.baseWordCount = this.wordList.Count;
			this.LoadCustomDictionary(customSource);
			this.wordList.Sort(this.baseWordCount, this.wordList.Count - this.baseWordCount, this.Comparer);
			this.InitArrays();
			if (this.wordList.Count == 0)
			{
				throw new ArgumentException("No dictionary loaded. Set the DictionaryPath property from the spell checker settings or copy the dictionaries to ~/App_Data/RadSpell/");
			}
		}

		// Token: 0x0600BC6A RID: 48234 RVA: 0x0029CC12 File Offset: 0x0029AE12
		private void Clear()
		{
			this.offset = 0;
			this.runningLength = -1;
			this.offsetsForLength.Clear();
			this.offsetsForLength.Add(0, 0);
			this.wordList.Clear();
		}

		// Token: 0x0600BC6B RID: 48235 RVA: 0x0029CC50 File Offset: 0x0029AE50
		protected override void AddDictionaryWord(string[] wordComponents)
		{
			string text = wordComponents[0];
			int length = text.Length;
			this.wordList.Add(text);
			if (length > this.longestWord)
			{
				this.longestWord = length;
			}
			if (this.runningLength != length)
			{
				this.runningLength = text.Length;
				this.offsetsForLength.Add(length, this.offset);
				this.FillMissingLengthOffsets(length, this.offset);
			}
			this.offset++;
		}

		// Token: 0x0600BC6C RID: 48236 RVA: 0x0029CCD1 File Offset: 0x0029AED1
		internal void FillMissingLengthOffsets(int wordLength, int offset)
		{
			checked
			{
				wordLength--;
				while (!this.offsetsForLength.Contains(wordLength))
				{
					this.offsetsForLength.Add(wordLength, offset);
					wordLength--;
				}
			}
		}

		// Token: 0x0600BC6D RID: 48237 RVA: 0x0029CD0C File Offset: 0x0029AF0C
		internal void InitArrays()
		{
			this.offsetsForLength[0] = this.baseWordCount;
			if (this.offsetsForLength.Count != 1)
			{
				this.offsetsForLength.Add(this.longestWord + 1, this.baseWordCount);
			}
			this.InitEditDistanceMatrix(this.longestWord + this.editDistance + 3);
		}

		// Token: 0x0600BC6E RID: 48238 RVA: 0x0029CD7C File Offset: 0x0029AF7C
		private void InitEditDistanceMatrix(int dimensionLength)
		{
			this.tempDistance = new int[dimensionLength][];
			for (int i = 0; i < dimensionLength; i++)
			{
				this.tempDistance[i] = new int[dimensionLength];
			}
		}

		// Token: 0x0600BC6F RID: 48239 RVA: 0x0029CDB0 File Offset: 0x0029AFB0
		protected override void AddCustomWord(string word)
		{
			this.wordList.Add(word);
			if (word.Length > this.longestWord)
			{
				int num = this.longestWord - 1;
				while (!this.offsetsForLength.Contains(num))
				{
					this.offsetsForLength.Add(num, this.longestWord);
					num--;
				}
				this.longestWord = word.Length;
			}
		}

		// Token: 0x0600BC70 RID: 48240 RVA: 0x0029CE24 File Offset: 0x0029B024
		internal override string[] GetSimilar(string word)
		{
			if (word.Length - this.editDistance > this.longestWord)
			{
				return new string[0];
			}
			word = word.ToLower();
			ArrayList arrayList = new ArrayList();
			int num = (word.Length - this.editDistance <= 0) ? 1 : (word.Length - this.editDistance);
			int num2 = (word.Length + this.editDistance + 1 > this.longestWord) ? this.longestWord : (word.Length + this.editDistance + 1);
			if (!this.offsetsForLength.Contains(num) || !this.offsetsForLength.Contains(num2))
			{
				return new string[0];
			}
			int startOffset = (int)this.offsetsForLength[num];
			int stopOffset = (int)this.offsetsForLength[num2];
			this.SuggestionsFromRange(word, arrayList, startOffset, stopOffset);
			this.SuggestionsFromRange(word, arrayList, this.baseWordCount, this.wordList.Count - 1);
			if (arrayList.Count > 0)
			{
				return (string[])arrayList.ToArray(typeof(string));
			}
			return new string[0];
		}

		// Token: 0x0600BC71 RID: 48241 RVA: 0x0029CF54 File Offset: 0x0029B154
		private void SuggestionsFromRange(string word, ArrayList suggestions, int startOffset, int stopOffset)
		{
			for (int i = startOffset; i <= stopOffset; i++)
			{
				if (this.CalculateEditDistance(word, ((string)this.wordList[i]).ToLower()) <= this.editDistance)
				{
					suggestions.Add(this.wordList[i]);
				}
			}
		}

		// Token: 0x0600BC72 RID: 48242 RVA: 0x0029CFA8 File Offset: 0x0029B1A8
		internal override bool HasWord(string word)
		{
			if (!this.offsetsForLength.Contains(word.Length) || !this.offsetsForLength.Contains(word.Length + 1))
			{
				return false;
			}
			if (this.FindWord(word))
			{
				return true;
			}
			if (word.Length >= 2 && word.EndsWith("'s"))
			{
				word = word.Substring(0, word.Length - 2);
				if (this.FindWord(word))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600BC73 RID: 48243 RVA: 0x0029D028 File Offset: 0x0029B228
		private bool FindWord(string word)
		{
			int count = this.wordList.Count - this.baseWordCount;
			string value = word.ToLower()[0] + word.Substring(1, word.Length - 1);
			int index = (int)this.offsetsForLength[word.Length];
			int count2 = (int)this.offsetsForLength[word.Length + 1] - (int)this.offsetsForLength[word.Length];
			return this.wordList.BinarySearch(index, count2, word, this.Comparer) >= 0 || this.wordList.BinarySearch(index, count2, word.ToLower(), this.Comparer) >= 0 || this.wordList.BinarySearch(index, count2, value, this.Comparer) >= 0 || this.wordList.BinarySearch(this.baseWordCount, count, word, this.Comparer) >= 0 || this.wordList.BinarySearch(this.baseWordCount, count, word.ToLower(), this.Comparer) >= 0 || this.wordList.BinarySearch(this.baseWordCount, count, value, this.Comparer) >= 0;
		}

		// Token: 0x0600BC74 RID: 48244 RVA: 0x0029D170 File Offset: 0x0029B370
		protected internal int CalculateEditDistance(string first, string second)
		{
			int length = first.Length;
			int length2 = second.Length;
			int num = length2;
			if (num < length)
			{
				num = length;
			}
			if (num >= this.tempDistance.Length)
			{
				this.InitEditDistanceMatrix(num + this.editDistance + 3);
			}
			for (int i = 0; i <= length; i++)
			{
				this.tempDistance[i][0] = i;
			}
			for (int j = 0; j <= length2; j++)
			{
				this.tempDistance[0][j] = j;
			}
			for (int k = 1; k <= length; k++)
			{
				for (int l = 1; l <= length2; l++)
				{
					int num2 = this.tempDistance[k - 1][l] + 1;
					int num3 = this.tempDistance[k][l - 1] + 1;
					int num4 = this.tempDistance[k - 1][l - 1] + ((first[k - 1] == second[l - 1]) ? 0 : 1);
					this.tempDistance[k][l] = ((num2 < num3) ? ((num2 < num4) ? num2 : num4) : ((num3 < num4) ? num3 : num4));
				}
			}
			return this.tempDistance[length][length2];
		}

		// Token: 0x17003CDD RID: 15581
		// (get) Token: 0x0600BC75 RID: 48245 RVA: 0x0029D294 File Offset: 0x0029B494
		// (set) Token: 0x0600BC76 RID: 48246 RVA: 0x0029D29C File Offset: 0x0029B49C
		protected string CacheKey
		{
			get
			{
				return this._cacheKey;
			}
			set
			{
				this._cacheKey = value;
			}
		}

		// Token: 0x17003CDE RID: 15582
		// (get) Token: 0x0600BC77 RID: 48247 RVA: 0x0029D2A8 File Offset: 0x0029B4A8
		protected Cache Cache
		{
			get
			{
				if (this._cache == null)
				{
					HttpContext httpContext = HttpContext.Current;
					this._cache = ((httpContext == null) ? null : httpContext.Cache);
				}
				return this._cache;
			}
		}

		// Token: 0x0600BC78 RID: 48248 RVA: 0x0029D2DC File Offset: 0x0029B4DC
		protected override bool LoadDictionaryFromCacheSucceeded()
		{
			if (this.Cache == null)
			{
				return false;
			}
			this.wordList = ((this.CachedWordList == null) ? new ArrayList() : ((ArrayList)this.CachedWordList.Clone()));
			if (this.CachedOffsetsForLength != null)
			{
				this.offsetsForLength = (Hashtable)this.CachedOffsetsForLength.Clone();
			}
			bool flag = this.wordList.Count > 0 && this.CachedWordList != null && this.CachedOffsetsForLength != null;
			if (flag)
			{
				this.longestWord = ((string)this.wordList[this.wordList.Count - 1]).Length;
				this.runningLength = this.longestWord;
			}
			return flag;
		}

		// Token: 0x0600BC79 RID: 48249 RVA: 0x0029D394 File Offset: 0x0029B594
		protected override void SaveDictionaryToCache()
		{
			if (this.Cache == null)
			{
				return;
			}
			this.CachedWordList = (ArrayList)this.wordList.Clone();
			this.CachedOffsetsForLength = (Hashtable)this.offsetsForLength.Clone();
		}

		// Token: 0x17003CDF RID: 15583
		// (get) Token: 0x0600BC7A RID: 48250 RVA: 0x0029D3CB File Offset: 0x0029B5CB
		// (set) Token: 0x0600BC7B RID: 48251 RVA: 0x0029D3F7 File Offset: 0x0029B5F7
		private ArrayList CachedWordList
		{
			get
			{
				if (this.Cache == null)
				{
					return null;
				}
				return this.Cache[this.CacheKey + "WordList"] as ArrayList;
			}
			set
			{
				if (this.Cache != null)
				{
					this.Cache[this.CacheKey + "WordList"] = value;
				}
			}
		}

		// Token: 0x17003CE0 RID: 15584
		// (get) Token: 0x0600BC7C RID: 48252 RVA: 0x0029D41D File Offset: 0x0029B61D
		// (set) Token: 0x0600BC7D RID: 48253 RVA: 0x0029D449 File Offset: 0x0029B649
		private Hashtable CachedOffsetsForLength
		{
			get
			{
				if (this.Cache == null)
				{
					return null;
				}
				return this.Cache[this.CacheKey + "OffsetsForLength"] as Hashtable;
			}
			set
			{
				if (this.Cache != null)
				{
					this.Cache[this.CacheKey + "OffsetsForLength"] = value;
				}
			}
		}

		// Token: 0x0600BC7E RID: 48254 RVA: 0x0029D470 File Offset: 0x0029B670
		protected override void ResetDictionaryItems()
		{
			if (this.Cache != null)
			{
				this.Cache.Remove(this.CacheKey + "WordList");
				this.Cache.Remove(this.CacheKey + "OffsetsForLength");
			}
			this.Clear();
		}

		// Token: 0x04003179 RID: 12665
		private const int NO_REALLOCATION_LENGTH_PADDING = 3;

		// Token: 0x0400317A RID: 12666
		private int baseWordCount;

		// Token: 0x0400317B RID: 12667
		private Hashtable offsetsForLength = new Hashtable();

		// Token: 0x0400317C RID: 12668
		private int longestWord = 1;

		// Token: 0x0400317D RID: 12669
		private int[][] tempDistance;

		// Token: 0x0400317E RID: 12670
		private int offset;

		// Token: 0x0400317F RID: 12671
		private int runningLength = -1;

		// Token: 0x04003180 RID: 12672
		private ArrayList wordList = new ArrayList();

		// Token: 0x04003181 RID: 12673
		private readonly int editDistance;

		// Token: 0x04003182 RID: 12674
		private WordComparer _comparer = new WordComparer();

		// Token: 0x04003183 RID: 12675
		private string _cacheKey;

		// Token: 0x04003184 RID: 12676
		private Cache _cache;
	}
}
