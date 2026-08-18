using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI.Dictionaries
{
	// Token: 0x020011D6 RID: 4566
	internal class MetaphoneDictionary : EditDistanceDictionary
	{
		// Token: 0x0600BC9C RID: 48284 RVA: 0x0029D662 File Offset: 0x0029B862
		internal MetaphoneDictionary() : base(1)
		{
		}

		// Token: 0x0600BC9D RID: 48285 RVA: 0x0029D68C File Offset: 0x0029B88C
		internal override string[] GetSimilar(string word)
		{
			string text = this.encoder.Encode(word, false);
			string text2 = this.encoder.Encode(word, true);
			ArrayList arrayList = new ArrayList();
			this.SimilarForHash(arrayList, text);
			if (text != text2)
			{
				this.SimilarForHash(arrayList, text2);
			}
			arrayList.Sort(base.Comparer);
			return this.SelectBestMatches(word.ToLower(), (string[])arrayList.ToArray(typeof(string)));
		}

		// Token: 0x0600BC9E RID: 48286 RVA: 0x0029D701 File Offset: 0x0029B901
		private void SimilarForHash(ArrayList accumulator, string hash)
		{
			MetaphoneDictionary.AddNewItems(accumulator, MetaphoneDictionary.ListFromTable(this.primaryTable, hash));
			MetaphoneDictionary.AddNewItems(accumulator, MetaphoneDictionary.ListFromTable(this.alternateTable, hash));
		}

		// Token: 0x0600BC9F RID: 48287 RVA: 0x0029D728 File Offset: 0x0029B928
		private static void AddNewItems(ArrayList accumulator, List<string> newItems)
		{
			foreach (string text in newItems)
			{
				if (!accumulator.Contains(text))
				{
					accumulator.Add(text);
				}
			}
		}

		// Token: 0x0600BCA0 RID: 48288 RVA: 0x0029D780 File Offset: 0x0029B980
		private static List<string> ListFromTable(Dictionary<int, List<string>> table, int key)
		{
			if (table.ContainsKey(key))
			{
				return table[key];
			}
			return new List<string>();
		}

		// Token: 0x0600BCA1 RID: 48289 RVA: 0x0029D798 File Offset: 0x0029B998
		private static List<string> ListFromTable(Dictionary<string, List<string>> table, string key)
		{
			if (table.ContainsKey(key))
			{
				return table[key];
			}
			return new List<string>();
		}

		// Token: 0x0600BCA2 RID: 48290 RVA: 0x0029D7B0 File Offset: 0x0029B9B0
		protected override void AddDictionaryWord(string[] wordComponents)
		{
			string word = wordComponents[0];
			string primaryHash = wordComponents[1];
			string alternateHash = wordComponents[2];
			this.AddPhoneticWord(word, primaryHash, alternateHash);
			base.AddDictionaryWord(wordComponents);
		}

		// Token: 0x0600BCA3 RID: 48291 RVA: 0x0029D7DC File Offset: 0x0029B9DC
		private void AddPhoneticWord(string word, string primaryHash, string alternateHash)
		{
			if (string.IsNullOrEmpty(primaryHash) && string.IsNullOrEmpty(alternateHash) && word.Length > 4)
			{
				throw new IncompatibleLanguageException();
			}
			MetaphoneDictionary.AddToTableList(this.primaryTable, word, primaryHash);
			if (primaryHash != alternateHash)
			{
				MetaphoneDictionary.AddToTableList(this.alternateTable, word, alternateHash);
			}
		}

		// Token: 0x0600BCA4 RID: 48292 RVA: 0x0029D82C File Offset: 0x0029BA2C
		private static void AddToTableList(Dictionary<int, List<string>> table, string word, int key)
		{
			List<string> list;
			if (table.ContainsKey(key))
			{
				list = table[key];
			}
			else
			{
				list = new List<string>();
				table[key] = list;
			}
			list.Add(word);
		}

		// Token: 0x0600BCA5 RID: 48293 RVA: 0x0029D864 File Offset: 0x0029BA64
		private static void AddToTableList(Dictionary<string, List<string>> table, string word, string key)
		{
			List<string> list;
			if (table.ContainsKey(key))
			{
				list = table[key];
			}
			else
			{
				list = new List<string>();
				table[key] = list;
			}
			list.Add(word);
		}

		// Token: 0x0600BCA6 RID: 48294 RVA: 0x0029D89C File Offset: 0x0029BA9C
		protected override void AddCustomWord(string word)
		{
			string primaryHash = this.encoder.Encode(word, false);
			string alternateHash = this.encoder.Encode(word, true);
			this.AddPhoneticWord(word, primaryHash, alternateHash);
			base.AddCustomWord(word);
		}

		// Token: 0x0600BCA7 RID: 48295 RVA: 0x0029D8D8 File Offset: 0x0029BAD8
		internal string[] SelectBestMatches(string word, string[] similar)
		{
			int num = 5;
			Dictionary<int, List<string>> table = new Dictionary<int, List<string>>();
			foreach (string text in similar)
			{
				if (Math.Abs(word.Length - text.Length) <= num)
				{
					int num2 = base.CalculateEditDistance(word, text);
					if (num2 < num)
					{
						num = num2;
					}
					MetaphoneDictionary.AddToTableList(table, text, num2);
				}
			}
			List<string> list = MetaphoneDictionary.ListFromTable(table, num);
			list.AddRange(MetaphoneDictionary.ListFromTable(table, num + 1));
			return list.ToArray();
		}

		// Token: 0x0600BCA8 RID: 48296 RVA: 0x0029D958 File Offset: 0x0029BB58
		private Dictionary<string, List<string>> CloneDictionary(Dictionary<string, List<string>> original)
		{
			Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>(original.Count);
			foreach (string key in original.Keys)
			{
				List<string> value = new List<string>(original[key]);
				dictionary.Add(key, value);
			}
			return dictionary;
		}

		// Token: 0x0600BCA9 RID: 48297 RVA: 0x0029D9C8 File Offset: 0x0029BBC8
		protected override bool LoadDictionaryFromCacheSucceeded()
		{
			if (base.Cache == null)
			{
				return false;
			}
			bool flag = base.LoadDictionaryFromCacheSucceeded();
			this.primaryTable = ((this.CachedPrimaryTable == null) ? new Dictionary<string, List<string>>() : this.CloneDictionary(this.CachedPrimaryTable));
			this.alternateTable = ((this.CachedAlternateTable == null) ? new Dictionary<string, List<string>>() : this.CloneDictionary(this.CachedAlternateTable));
			return flag && this.CachedAlternateTable != null && this.CachedPrimaryTable != null;
		}

		// Token: 0x0600BCAA RID: 48298 RVA: 0x0029DA41 File Offset: 0x0029BC41
		protected override void SaveDictionaryToCache()
		{
			if (base.Cache == null)
			{
				return;
			}
			this.CachedPrimaryTable = this.CloneDictionary(this.primaryTable);
			this.CachedAlternateTable = this.CloneDictionary(this.alternateTable);
			base.SaveDictionaryToCache();
		}

		// Token: 0x17003CE9 RID: 15593
		// (get) Token: 0x0600BCAB RID: 48299 RVA: 0x0029DA76 File Offset: 0x0029BC76
		// (set) Token: 0x0600BCAC RID: 48300 RVA: 0x0029DAA2 File Offset: 0x0029BCA2
		private Dictionary<string, List<string>> CachedPrimaryTable
		{
			get
			{
				if (base.Cache == null)
				{
					return null;
				}
				return base.Cache[base.CacheKey + "PrimaryTable"] as Dictionary<string, List<string>>;
			}
			set
			{
				if (base.Cache != null)
				{
					base.Cache[base.CacheKey + "PrimaryTable"] = value;
				}
			}
		}

		// Token: 0x17003CEA RID: 15594
		// (get) Token: 0x0600BCAD RID: 48301 RVA: 0x0029DAC8 File Offset: 0x0029BCC8
		// (set) Token: 0x0600BCAE RID: 48302 RVA: 0x0029DAF4 File Offset: 0x0029BCF4
		private Dictionary<string, List<string>> CachedAlternateTable
		{
			get
			{
				if (base.Cache == null)
				{
					return null;
				}
				return base.Cache[base.CacheKey + "AlternateTable"] as Dictionary<string, List<string>>;
			}
			set
			{
				if (base.Cache != null)
				{
					base.Cache[base.CacheKey + "AlternateTable"] = value;
				}
			}
		}

		// Token: 0x0600BCAF RID: 48303 RVA: 0x0029DB1C File Offset: 0x0029BD1C
		protected override void ResetDictionaryItems()
		{
			if (base.Cache != null)
			{
				base.Cache.Remove(base.CacheKey + "PrimaryTable");
				base.Cache.Remove(base.CacheKey + "AlternateTable");
			}
			this.primaryTable.Clear();
			this.alternateTable.Clear();
			base.ResetDictionaryItems();
		}

		// Token: 0x0400318A RID: 12682
		private const int CLOSEST_MATCH_THRESHOLD = 5;

		// Token: 0x0400318B RID: 12683
		private const int SHORTEST_WORD_WITH_EMPTY_HASH = 4;

		// Token: 0x0400318C RID: 12684
		private Dictionary<string, List<string>> primaryTable = new Dictionary<string, List<string>>();

		// Token: 0x0400318D RID: 12685
		private Dictionary<string, List<string>> alternateTable = new Dictionary<string, List<string>>();

		// Token: 0x0400318E RID: 12686
		private DoubleMetaphone encoder = new DoubleMetaphone();
	}
}
