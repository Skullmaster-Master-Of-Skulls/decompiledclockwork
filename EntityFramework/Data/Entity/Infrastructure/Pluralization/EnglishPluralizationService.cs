using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Design.PluralizationServices;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Data.Entity.Infrastructure.Pluralization
{
	// Token: 0x0200028D RID: 653
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Pluralization")]
	public sealed class EnglishPluralizationService : IPluralizationService
	{
		// Token: 0x060016CE RID: 5838 RVA: 0x0006F700 File Offset: 0x0006D900
		public EnglishPluralizationService()
		{
			this._userDictionary = new BidirectionalDictionary<string, string>();
			this._irregularPluralsPluralizationService = new StringBidirectionalDictionary(this._irregularPluralsList);
			this._assimilatedClassicalInflectionPluralizationService = new StringBidirectionalDictionary(this._assimilatedClassicalInflectionList);
			this._oSuffixPluralizationService = new StringBidirectionalDictionary(this._oSuffixList);
			this._classicalInflectionPluralizationService = new StringBidirectionalDictionary(this._classicalInflectionList);
			this._wordsEndingWithSePluralizationService = new StringBidirectionalDictionary(this._wordsEndingWithSeList);
			this._wordsEndingWithSisPluralizationService = new StringBidirectionalDictionary(this._wordsEndingWithSisList);
			this._irregularVerbPluralizationService = new StringBidirectionalDictionary(this._irregularVerbList);
			this._knownSingluarWords = new List<string>(this._irregularPluralsList.Keys.Concat(this._assimilatedClassicalInflectionList.Keys).Concat(this._oSuffixList.Keys).Concat(this._classicalInflectionList.Keys).Concat(this._irregularVerbList.Keys).Concat(this._uninflectiveWords).Except(this._knownConflictingPluralList));
			this._knownPluralWords = new List<string>(this._irregularPluralsList.Values.Concat(this._assimilatedClassicalInflectionList.Values).Concat(this._oSuffixList.Values).Concat(this._classicalInflectionList.Values).Concat(this._irregularVerbList.Values).Concat(this._uninflectiveWords));
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x00071684 File Offset: 0x0006F884
		public EnglishPluralizationService(IEnumerable<CustomPluralizationEntry> userDictionaryEntries) : this()
		{
			Check.NotNull<IEnumerable<CustomPluralizationEntry>>(userDictionaryEntries, "userDictionaryEntries");
			userDictionaryEntries.Each(delegate(CustomPluralizationEntry entry)
			{
				this._userDictionary.AddValue(entry.Singular, entry.Plural);
			});
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x000716BC File Offset: 0x0006F8BC
		public string Pluralize(string word)
		{
			return EnglishPluralizationService.Capitalize(word, new Func<string, string>(this.InternalPluralize));
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x00071844 File Offset: 0x0006FA44
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private string InternalPluralize(string word)
		{
			if (this._userDictionary.ExistsInFirst(word))
			{
				return this._userDictionary.GetSecondValue(word);
			}
			if (this.IsNoOpWord(word))
			{
				return word;
			}
			string str;
			string suffixWord = EnglishPluralizationService.GetSuffixWord(word, out str);
			if (this.IsNoOpWord(suffixWord))
			{
				return str + suffixWord;
			}
			if (this.IsUninflective(suffixWord))
			{
				return str + suffixWord;
			}
			if (this._knownPluralWords.Contains(suffixWord.ToLowerInvariant()) || this.IsPlural(suffixWord))
			{
				return str + suffixWord;
			}
			if (this._irregularPluralsPluralizationService.ExistsInFirst(suffixWord))
			{
				return str + this._irregularPluralsPluralizationService.GetSecondValue(suffixWord);
			}
			string str2;
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"man"
			}, (string s) => s.Remove(s.Length - 2, 2) + "en", this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"louse",
				"mouse"
			}, (string s) => s.Remove(s.Length - 4, 4) + "ice", this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"tooth"
			}, (string s) => s.Remove(s.Length - 4, 4) + "eeth", this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"goose"
			}, (string s) => s.Remove(s.Length - 4, 4) + "eese", this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"foot"
			}, (string s) => s.Remove(s.Length - 3, 3) + "eet", this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"zoon"
			}, (string s) => s.Remove(s.Length - 3, 3) + "oa", this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"cis",
				"sis",
				"xis"
			}, (string s) => s.Remove(s.Length - 2, 2) + "es", this._culture, out str2))
			{
				return str + str2;
			}
			if (this._assimilatedClassicalInflectionPluralizationService.ExistsInFirst(suffixWord))
			{
				return str + this._assimilatedClassicalInflectionPluralizationService.GetSecondValue(suffixWord);
			}
			if (this._classicalInflectionPluralizationService.ExistsInFirst(suffixWord))
			{
				return str + this._classicalInflectionPluralizationService.GetSecondValue(suffixWord);
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"trix"
			}, (string s) => s.Remove(s.Length - 1, 1) + "ces", this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"eau",
				"ieu"
			}, (string s) => s + "x", this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"inx",
				"anx",
				"ynx"
			}, (string s) => s.Remove(s.Length - 1, 1) + "ges", this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"ch",
				"sh",
				"ss"
			}, (string s) => s + "es", this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"alf",
				"elf",
				"olf",
				"eaf",
				"arf"
			}, delegate(string s)
			{
				if (!s.EndsWith("deaf", true, this._culture))
				{
					return s.Remove(s.Length - 1, 1) + "ves";
				}
				return s;
			}, this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"nife",
				"life",
				"wife"
			}, (string s) => s.Remove(s.Length - 2, 2) + "ves", this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"ay",
				"ey",
				"iy",
				"oy",
				"uy"
			}, (string s) => s + "s", this._culture, out str2))
			{
				return str + str2;
			}
			if (suffixWord.EndsWith("y", true, this._culture))
			{
				return str + suffixWord.Remove(suffixWord.Length - 1, 1) + "ies";
			}
			if (this._oSuffixPluralizationService.ExistsInFirst(suffixWord))
			{
				return str + this._oSuffixPluralizationService.GetSecondValue(suffixWord);
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"ao",
				"eo",
				"io",
				"oo",
				"uo"
			}, (string s) => s + "s", this._culture, out str2))
			{
				return str + str2;
			}
			if (suffixWord.EndsWith("o", true, this._culture))
			{
				return str + suffixWord + "es";
			}
			if (suffixWord.EndsWith("x", true, this._culture))
			{
				return str + suffixWord + "es";
			}
			return str + suffixWord + "s";
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x00071F11 File Offset: 0x00070111
		public string Singularize(string word)
		{
			return EnglishPluralizationService.Capitalize(word, new Func<string, string>(this.InternalSingularize));
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x000720AC File Offset: 0x000702AC
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		[SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode")]
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		private string InternalSingularize(string word)
		{
			if (this._userDictionary.ExistsInSecond(word))
			{
				return this._userDictionary.GetFirstValue(word);
			}
			if (this.IsNoOpWord(word))
			{
				return word;
			}
			string str;
			string suffixWord = EnglishPluralizationService.GetSuffixWord(word, out str);
			if (this.IsNoOpWord(suffixWord))
			{
				return str + suffixWord;
			}
			if (this.IsUninflective(suffixWord))
			{
				return str + suffixWord;
			}
			if (this._knownSingluarWords.Contains(suffixWord.ToLowerInvariant()))
			{
				return str + suffixWord;
			}
			if (this._irregularVerbPluralizationService.ExistsInSecond(suffixWord))
			{
				return str + this._irregularVerbPluralizationService.GetFirstValue(suffixWord);
			}
			if (this._irregularPluralsPluralizationService.ExistsInSecond(suffixWord))
			{
				return str + this._irregularPluralsPluralizationService.GetFirstValue(suffixWord);
			}
			if (this._wordsEndingWithSisPluralizationService.ExistsInSecond(suffixWord))
			{
				return str + this._wordsEndingWithSisPluralizationService.GetFirstValue(suffixWord);
			}
			if (this._wordsEndingWithSePluralizationService.ExistsInSecond(suffixWord))
			{
				return str + this._wordsEndingWithSePluralizationService.GetFirstValue(suffixWord);
			}
			string str2;
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"men"
			}, (string s) => s.Remove(s.Length - 2, 2) + "an", this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"lice",
				"mice"
			}, (string s) => s.Remove(s.Length - 3, 3) + "ouse", this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"teeth"
			}, (string s) => s.Remove(s.Length - 4, 4) + "ooth", this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"geese"
			}, (string s) => s.Remove(s.Length - 4, 4) + "oose", this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"feet"
			}, (string s) => s.Remove(s.Length - 3, 3) + "oot", this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"zoa"
			}, (string s) => s.Remove(s.Length - 2, 2) + "oon", this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"ches",
				"shes",
				"sses"
			}, (string s) => s.Remove(s.Length - 2, 2), this._culture, out str2))
			{
				return str + str2;
			}
			if (this._assimilatedClassicalInflectionPluralizationService.ExistsInSecond(suffixWord))
			{
				return str + this._assimilatedClassicalInflectionPluralizationService.GetFirstValue(suffixWord);
			}
			if (this._classicalInflectionPluralizationService.ExistsInSecond(suffixWord))
			{
				return str + this._classicalInflectionPluralizationService.GetFirstValue(suffixWord);
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"trices"
			}, (string s) => s.Remove(s.Length - 3, 3) + "x", this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"eaux",
				"ieux"
			}, (string s) => s.Remove(s.Length - 1, 1), this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"inges",
				"anges",
				"ynges"
			}, (string s) => s.Remove(s.Length - 3, 3) + "x", this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"alves",
				"elves",
				"olves",
				"eaves",
				"arves"
			}, (string s) => s.Remove(s.Length - 3, 3) + "f", this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"nives",
				"lives",
				"wives"
			}, (string s) => s.Remove(s.Length - 3, 3) + "fe", this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"ays",
				"eys",
				"iys",
				"oys",
				"uys"
			}, (string s) => s.Remove(s.Length - 1, 1), this._culture, out str2))
			{
				return str + str2;
			}
			if (suffixWord.EndsWith("ies", true, this._culture))
			{
				return str + suffixWord.Remove(suffixWord.Length - 3, 3) + "y";
			}
			if (this._oSuffixPluralizationService.ExistsInSecond(suffixWord))
			{
				return str + this._oSuffixPluralizationService.GetFirstValue(suffixWord);
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"aos",
				"eos",
				"ios",
				"oos",
				"uos"
			}, (string s) => suffixWord.Remove(suffixWord.Length - 1, 1), this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"ces"
			}, (string s) => s.Remove(s.Length - 1, 1), this._culture, out str2))
			{
				return str + str2;
			}
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, new List<string>
			{
				"ces",
				"ses",
				"xes"
			}, (string s) => s.Remove(s.Length - 2, 2), this._culture, out str2))
			{
				return str + str2;
			}
			if (suffixWord.EndsWith("oes", true, this._culture))
			{
				return str + suffixWord.Remove(suffixWord.Length - 2, 2);
			}
			if (suffixWord.EndsWith("ss", true, this._culture))
			{
				return str + suffixWord;
			}
			if (suffixWord.EndsWith("s", true, this._culture))
			{
				return str + suffixWord.Remove(suffixWord.Length - 1, 1);
			}
			return str + suffixWord;
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x00072970 File Offset: 0x00070B70
		private bool IsPlural(string word)
		{
			return this._userDictionary.ExistsInSecond(word) || (!this._userDictionary.ExistsInFirst(word) && (this.IsUninflective(word) || this._knownPluralWords.Contains(word.ToLower(this._culture)) || !this.Singularize(word).Equals(word)));
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x000729D4 File Offset: 0x00070BD4
		private static string Capitalize(string word, Func<string, string> action)
		{
			string text = action(word);
			if (!EnglishPluralizationService.IsCapitalized(word))
			{
				return text;
			}
			if (text.Length == 0)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(text.Length);
			stringBuilder.Append(char.ToUpperInvariant(text[0]));
			stringBuilder.Append(text.Substring(1));
			return stringBuilder.ToString();
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x00072A30 File Offset: 0x00070C30
		private static string GetSuffixWord(string word, out string prefixWord)
		{
			int num = word.LastIndexOf(' ');
			prefixWord = word.Substring(0, num + 1);
			return word.Substring(num + 1);
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x00072A5B File Offset: 0x00070C5B
		private static bool IsCapitalized(string word)
		{
			return !string.IsNullOrEmpty(word) && char.IsUpper(word, 0);
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x00072A6E File Offset: 0x00070C6E
		private static bool IsAlphabets(string word)
		{
			return !string.IsNullOrEmpty(word.Trim()) && word.Equals(word.Trim()) && !Regex.IsMatch(word, "[^a-zA-Z\\s]");
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x00072A9C File Offset: 0x00070C9C
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		private bool IsUninflective(string word)
		{
			return PluralizationServiceUtil.DoesWordContainSuffix(word, this._uninflectiveSuffixes, this._culture) || (!word.ToLower(this._culture).Equals(word) && word.EndsWith("ese", false, this._culture)) || this._uninflectiveWords.Contains(word.ToLowerInvariant());
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x00072AFB File Offset: 0x00070CFB
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		private bool IsNoOpWord(string word)
		{
			return !EnglishPluralizationService.IsAlphabets(word) || word.Length <= 1 || this._pronounList.Contains(word.ToLowerInvariant());
		}

		// Token: 0x0400081D RID: 2077
		private readonly BidirectionalDictionary<string, string> _userDictionary;

		// Token: 0x0400081E RID: 2078
		private readonly StringBidirectionalDictionary _irregularPluralsPluralizationService;

		// Token: 0x0400081F RID: 2079
		private readonly StringBidirectionalDictionary _assimilatedClassicalInflectionPluralizationService;

		// Token: 0x04000820 RID: 2080
		private readonly StringBidirectionalDictionary _oSuffixPluralizationService;

		// Token: 0x04000821 RID: 2081
		private readonly StringBidirectionalDictionary _classicalInflectionPluralizationService;

		// Token: 0x04000822 RID: 2082
		private readonly StringBidirectionalDictionary _irregularVerbPluralizationService;

		// Token: 0x04000823 RID: 2083
		private readonly StringBidirectionalDictionary _wordsEndingWithSePluralizationService;

		// Token: 0x04000824 RID: 2084
		private readonly StringBidirectionalDictionary _wordsEndingWithSisPluralizationService;

		// Token: 0x04000825 RID: 2085
		private readonly List<string> _knownSingluarWords;

		// Token: 0x04000826 RID: 2086
		private readonly List<string> _knownPluralWords;

		// Token: 0x04000827 RID: 2087
		private readonly CultureInfo _culture = new CultureInfo("en-US");

		// Token: 0x04000828 RID: 2088
		private readonly string[] _uninflectiveSuffixes = new string[]
		{
			"fish",
			"ois",
			"sheep",
			"deer",
			"pos",
			"itis",
			"ism"
		};

		// Token: 0x04000829 RID: 2089
		private readonly string[] _uninflectiveWords = new string[]
		{
			"bison",
			"flounder",
			"pliers",
			"bream",
			"gallows",
			"proceedings",
			"breeches",
			"graffiti",
			"rabies",
			"britches",
			"headquarters",
			"salmon",
			"carp",
			"herpes",
			"scissors",
			"chassis",
			"high-jinks",
			"sea-bass",
			"clippers",
			"homework",
			"series",
			"cod",
			"innings",
			"shears",
			"contretemps",
			"jackanapes",
			"species",
			"corps",
			"mackerel",
			"swine",
			"debris",
			"measles",
			"trout",
			"diabetes",
			"mews",
			"tuna",
			"djinn",
			"mumps",
			"whiting",
			"eland",
			"news",
			"wildebeest",
			"elk",
			"pincers",
			"police",
			"hair",
			"ice",
			"chaos",
			"milk",
			"cotton",
			"corn",
			"millet",
			"hay",
			"pneumonoultramicroscopicsilicovolcanoconiosis",
			"information",
			"rice",
			"tobacco",
			"aircraft",
			"rabies",
			"scabies",
			"diabetes",
			"traffic",
			"cotton",
			"corn",
			"millet",
			"rice",
			"hay",
			"hemp",
			"tobacco",
			"cabbage",
			"okra",
			"broccoli",
			"asparagus",
			"lettuce",
			"beef",
			"pork",
			"venison",
			"bison",
			"mutton",
			"cattle",
			"offspring",
			"molasses",
			"shambles",
			"shingles"
		};

		// Token: 0x0400082A RID: 2090
		private readonly Dictionary<string, string> _irregularVerbList = new Dictionary<string, string>
		{
			{
				"am",
				"are"
			},
			{
				"are",
				"are"
			},
			{
				"is",
				"are"
			},
			{
				"was",
				"were"
			},
			{
				"were",
				"were"
			},
			{
				"has",
				"have"
			},
			{
				"have",
				"have"
			}
		};

		// Token: 0x0400082B RID: 2091
		private readonly List<string> _pronounList = new List<string>
		{
			"I",
			"we",
			"you",
			"he",
			"she",
			"they",
			"it",
			"me",
			"us",
			"him",
			"her",
			"them",
			"myself",
			"ourselves",
			"yourself",
			"himself",
			"herself",
			"itself",
			"oneself",
			"oneselves",
			"my",
			"our",
			"your",
			"his",
			"their",
			"its",
			"mine",
			"yours",
			"hers",
			"theirs",
			"this",
			"that",
			"these",
			"those",
			"all",
			"another",
			"any",
			"anybody",
			"anyone",
			"anything",
			"both",
			"each",
			"other",
			"either",
			"everyone",
			"everybody",
			"everything",
			"most",
			"much",
			"nothing",
			"nobody",
			"none",
			"one",
			"others",
			"some",
			"somebody",
			"someone",
			"something",
			"what",
			"whatever",
			"which",
			"whichever",
			"who",
			"whoever",
			"whom",
			"whomever",
			"whose"
		};

		// Token: 0x0400082C RID: 2092
		private readonly Dictionary<string, string> _irregularPluralsList = new Dictionary<string, string>
		{
			{
				"brother",
				"brothers"
			},
			{
				"child",
				"children"
			},
			{
				"cow",
				"cows"
			},
			{
				"ephemeris",
				"ephemerides"
			},
			{
				"genie",
				"genies"
			},
			{
				"money",
				"moneys"
			},
			{
				"mongoose",
				"mongooses"
			},
			{
				"mythos",
				"mythoi"
			},
			{
				"octopus",
				"octopuses"
			},
			{
				"ox",
				"oxen"
			},
			{
				"soliloquy",
				"soliloquies"
			},
			{
				"trilby",
				"trilbys"
			},
			{
				"crisis",
				"crises"
			},
			{
				"synopsis",
				"synopses"
			},
			{
				"rose",
				"roses"
			},
			{
				"gas",
				"gases"
			},
			{
				"bus",
				"buses"
			},
			{
				"axis",
				"axes"
			},
			{
				"memo",
				"memos"
			},
			{
				"casino",
				"casinos"
			},
			{
				"silo",
				"silos"
			},
			{
				"stereo",
				"stereos"
			},
			{
				"studio",
				"studios"
			},
			{
				"lens",
				"lenses"
			},
			{
				"alias",
				"aliases"
			},
			{
				"pie",
				"pies"
			},
			{
				"corpus",
				"corpora"
			},
			{
				"viscus",
				"viscera"
			},
			{
				"hippopotamus",
				"hippopotami"
			},
			{
				"trace",
				"traces"
			},
			{
				"person",
				"people"
			},
			{
				"chilli",
				"chillies"
			},
			{
				"analysis",
				"analyses"
			},
			{
				"basis",
				"bases"
			},
			{
				"neurosis",
				"neuroses"
			},
			{
				"oasis",
				"oases"
			},
			{
				"synthesis",
				"syntheses"
			},
			{
				"thesis",
				"theses"
			},
			{
				"pneumonoultramicroscopicsilicovolcanoconiosis",
				"pneumonoultramicroscopicsilicovolcanoconioses"
			},
			{
				"status",
				"statuses"
			},
			{
				"prospectus",
				"prospectuses"
			},
			{
				"change",
				"changes"
			},
			{
				"lie",
				"lies"
			},
			{
				"calorie",
				"calories"
			},
			{
				"freebie",
				"freebies"
			},
			{
				"case",
				"cases"
			},
			{
				"house",
				"houses"
			},
			{
				"valve",
				"valves"
			},
			{
				"cloth",
				"clothes"
			}
		};

		// Token: 0x0400082D RID: 2093
		private readonly Dictionary<string, string> _assimilatedClassicalInflectionList = new Dictionary<string, string>
		{
			{
				"alumna",
				"alumnae"
			},
			{
				"alga",
				"algae"
			},
			{
				"vertebra",
				"vertebrae"
			},
			{
				"codex",
				"codices"
			},
			{
				"murex",
				"murices"
			},
			{
				"silex",
				"silices"
			},
			{
				"aphelion",
				"aphelia"
			},
			{
				"hyperbaton",
				"hyperbata"
			},
			{
				"perihelion",
				"perihelia"
			},
			{
				"asyndeton",
				"asyndeta"
			},
			{
				"noumenon",
				"noumena"
			},
			{
				"phenomenon",
				"phenomena"
			},
			{
				"criterion",
				"criteria"
			},
			{
				"organon",
				"organa"
			},
			{
				"prolegomenon",
				"prolegomena"
			},
			{
				"agendum",
				"agenda"
			},
			{
				"datum",
				"data"
			},
			{
				"extremum",
				"extrema"
			},
			{
				"bacterium",
				"bacteria"
			},
			{
				"desideratum",
				"desiderata"
			},
			{
				"stratum",
				"strata"
			},
			{
				"candelabrum",
				"candelabra"
			},
			{
				"erratum",
				"errata"
			},
			{
				"ovum",
				"ova"
			},
			{
				"forum",
				"fora"
			},
			{
				"addendum",
				"addenda"
			},
			{
				"stadium",
				"stadia"
			},
			{
				"automaton",
				"automata"
			},
			{
				"polyhedron",
				"polyhedra"
			}
		};

		// Token: 0x0400082E RID: 2094
		private readonly Dictionary<string, string> _oSuffixList = new Dictionary<string, string>
		{
			{
				"albino",
				"albinos"
			},
			{
				"generalissimo",
				"generalissimos"
			},
			{
				"manifesto",
				"manifestos"
			},
			{
				"archipelago",
				"archipelagos"
			},
			{
				"ghetto",
				"ghettos"
			},
			{
				"medico",
				"medicos"
			},
			{
				"armadillo",
				"armadillos"
			},
			{
				"guano",
				"guanos"
			},
			{
				"octavo",
				"octavos"
			},
			{
				"commando",
				"commandos"
			},
			{
				"inferno",
				"infernos"
			},
			{
				"photo",
				"photos"
			},
			{
				"ditto",
				"dittos"
			},
			{
				"jumbo",
				"jumbos"
			},
			{
				"pro",
				"pros"
			},
			{
				"dynamo",
				"dynamos"
			},
			{
				"lingo",
				"lingos"
			},
			{
				"quarto",
				"quartos"
			},
			{
				"embryo",
				"embryos"
			},
			{
				"lumbago",
				"lumbagos"
			},
			{
				"rhino",
				"rhinos"
			},
			{
				"fiasco",
				"fiascos"
			},
			{
				"magneto",
				"magnetos"
			},
			{
				"stylo",
				"stylos"
			}
		};

		// Token: 0x0400082F RID: 2095
		private readonly Dictionary<string, string> _classicalInflectionList = new Dictionary<string, string>
		{
			{
				"stamen",
				"stamina"
			},
			{
				"foramen",
				"foramina"
			},
			{
				"lumen",
				"lumina"
			},
			{
				"anathema",
				"anathemata"
			},
			{
				"enema",
				"enemata"
			},
			{
				"oedema",
				"oedemata"
			},
			{
				"bema",
				"bemata"
			},
			{
				"enigma",
				"enigmata"
			},
			{
				"sarcoma",
				"sarcomata"
			},
			{
				"carcinoma",
				"carcinomata"
			},
			{
				"gumma",
				"gummata"
			},
			{
				"schema",
				"schemata"
			},
			{
				"charisma",
				"charismata"
			},
			{
				"lemma",
				"lemmata"
			},
			{
				"soma",
				"somata"
			},
			{
				"diploma",
				"diplomata"
			},
			{
				"lymphoma",
				"lymphomata"
			},
			{
				"stigma",
				"stigmata"
			},
			{
				"dogma",
				"dogmata"
			},
			{
				"magma",
				"magmata"
			},
			{
				"stoma",
				"stomata"
			},
			{
				"drama",
				"dramata"
			},
			{
				"melisma",
				"melismata"
			},
			{
				"trauma",
				"traumata"
			},
			{
				"edema",
				"edemata"
			},
			{
				"miasma",
				"miasmata"
			},
			{
				"abscissa",
				"abscissae"
			},
			{
				"formula",
				"formulae"
			},
			{
				"medusa",
				"medusae"
			},
			{
				"amoeba",
				"amoebae"
			},
			{
				"hydra",
				"hydrae"
			},
			{
				"nebula",
				"nebulae"
			},
			{
				"antenna",
				"antennae"
			},
			{
				"hyperbola",
				"hyperbolae"
			},
			{
				"nova",
				"novae"
			},
			{
				"aurora",
				"aurorae"
			},
			{
				"lacuna",
				"lacunae"
			},
			{
				"parabola",
				"parabolae"
			},
			{
				"apex",
				"apices"
			},
			{
				"latex",
				"latices"
			},
			{
				"vertex",
				"vertices"
			},
			{
				"cortex",
				"cortices"
			},
			{
				"pontifex",
				"pontifices"
			},
			{
				"vortex",
				"vortices"
			},
			{
				"index",
				"indices"
			},
			{
				"simplex",
				"simplices"
			},
			{
				"iris",
				"irides"
			},
			{
				"clitoris",
				"clitorides"
			},
			{
				"alto",
				"alti"
			},
			{
				"contralto",
				"contralti"
			},
			{
				"soprano",
				"soprani"
			},
			{
				"basso",
				"bassi"
			},
			{
				"crescendo",
				"crescendi"
			},
			{
				"tempo",
				"tempi"
			},
			{
				"canto",
				"canti"
			},
			{
				"solo",
				"soli"
			},
			{
				"aquarium",
				"aquaria"
			},
			{
				"interregnum",
				"interregna"
			},
			{
				"quantum",
				"quanta"
			},
			{
				"compendium",
				"compendia"
			},
			{
				"lustrum",
				"lustra"
			},
			{
				"rostrum",
				"rostra"
			},
			{
				"consortium",
				"consortia"
			},
			{
				"maximum",
				"maxima"
			},
			{
				"spectrum",
				"spectra"
			},
			{
				"cranium",
				"crania"
			},
			{
				"medium",
				"media"
			},
			{
				"speculum",
				"specula"
			},
			{
				"curriculum",
				"curricula"
			},
			{
				"memorandum",
				"memoranda"
			},
			{
				"stadium",
				"stadia"
			},
			{
				"dictum",
				"dicta"
			},
			{
				"millenium",
				"millenia"
			},
			{
				"trapezium",
				"trapezia"
			},
			{
				"emporium",
				"emporia"
			},
			{
				"minimum",
				"minima"
			},
			{
				"ultimatum",
				"ultimata"
			},
			{
				"enconium",
				"enconia"
			},
			{
				"momentum",
				"momenta"
			},
			{
				"vacuum",
				"vacua"
			},
			{
				"gymnasium",
				"gymnasia"
			},
			{
				"optimum",
				"optima"
			},
			{
				"velum",
				"vela"
			},
			{
				"honorarium",
				"honoraria"
			},
			{
				"phylum",
				"phyla"
			},
			{
				"focus",
				"foci"
			},
			{
				"nimbus",
				"nimbi"
			},
			{
				"succubus",
				"succubi"
			},
			{
				"fungus",
				"fungi"
			},
			{
				"nucleolus",
				"nucleoli"
			},
			{
				"torus",
				"tori"
			},
			{
				"genius",
				"genii"
			},
			{
				"radius",
				"radii"
			},
			{
				"umbilicus",
				"umbilici"
			},
			{
				"incubus",
				"incubi"
			},
			{
				"stylus",
				"styli"
			},
			{
				"uterus",
				"uteri"
			},
			{
				"stimulus",
				"stimuli"
			},
			{
				"apparatus",
				"apparatus"
			},
			{
				"impetus",
				"impetus"
			},
			{
				"prospectus",
				"prospectus"
			},
			{
				"cantus",
				"cantus"
			},
			{
				"nexus",
				"nexus"
			},
			{
				"sinus",
				"sinus"
			},
			{
				"coitus",
				"coitus"
			},
			{
				"plexus",
				"plexus"
			},
			{
				"status",
				"status"
			},
			{
				"hiatus",
				"hiatus"
			},
			{
				"afreet",
				"afreeti"
			},
			{
				"afrit",
				"afriti"
			},
			{
				"efreet",
				"efreeti"
			},
			{
				"cherub",
				"cherubim"
			},
			{
				"goy",
				"goyim"
			},
			{
				"seraph",
				"seraphim"
			},
			{
				"alumnus",
				"alumni"
			}
		};

		// Token: 0x04000830 RID: 2096
		private readonly List<string> _knownConflictingPluralList = new List<string>
		{
			"they",
			"them",
			"their",
			"have",
			"were",
			"yourself",
			"are"
		};

		// Token: 0x04000831 RID: 2097
		private readonly Dictionary<string, string> _wordsEndingWithSeList = new Dictionary<string, string>
		{
			{
				"house",
				"houses"
			},
			{
				"case",
				"cases"
			},
			{
				"enterprise",
				"enterprises"
			},
			{
				"purchase",
				"purchases"
			},
			{
				"surprise",
				"surprises"
			},
			{
				"release",
				"releases"
			},
			{
				"disease",
				"diseases"
			},
			{
				"promise",
				"promises"
			},
			{
				"refuse",
				"refuses"
			},
			{
				"whose",
				"whoses"
			},
			{
				"phase",
				"phases"
			},
			{
				"noise",
				"noises"
			},
			{
				"nurse",
				"nurses"
			},
			{
				"rose",
				"roses"
			},
			{
				"franchise",
				"franchises"
			},
			{
				"supervise",
				"supervises"
			},
			{
				"farmhouse",
				"farmhouses"
			},
			{
				"suitcase",
				"suitcases"
			},
			{
				"recourse",
				"recourses"
			},
			{
				"impulse",
				"impulses"
			},
			{
				"license",
				"licenses"
			},
			{
				"diocese",
				"dioceses"
			},
			{
				"excise",
				"excises"
			},
			{
				"demise",
				"demises"
			},
			{
				"blouse",
				"blouses"
			},
			{
				"bruise",
				"bruises"
			},
			{
				"misuse",
				"misuses"
			},
			{
				"curse",
				"curses"
			},
			{
				"prose",
				"proses"
			},
			{
				"purse",
				"purses"
			},
			{
				"goose",
				"gooses"
			},
			{
				"tease",
				"teases"
			},
			{
				"poise",
				"poises"
			},
			{
				"vase",
				"vases"
			},
			{
				"fuse",
				"fuses"
			},
			{
				"muse",
				"muses"
			},
			{
				"slaughterhouse",
				"slaughterhouses"
			},
			{
				"clearinghouse",
				"clearinghouses"
			},
			{
				"endonuclease",
				"endonucleases"
			},
			{
				"steeplechase",
				"steeplechases"
			},
			{
				"metamorphose",
				"metamorphoses"
			},
			{
				"intercourse",
				"intercourses"
			},
			{
				"commonsense",
				"commonsenses"
			},
			{
				"intersperse",
				"intersperses"
			},
			{
				"merchandise",
				"merchandises"
			},
			{
				"phosphatase",
				"phosphatases"
			},
			{
				"summerhouse",
				"summerhouses"
			},
			{
				"watercourse",
				"watercourses"
			},
			{
				"catchphrase",
				"catchphrases"
			},
			{
				"compromise",
				"compromises"
			},
			{
				"greenhouse",
				"greenhouses"
			},
			{
				"lighthouse",
				"lighthouses"
			},
			{
				"paraphrase",
				"paraphrases"
			},
			{
				"mayonnaise",
				"mayonnaises"
			},
			{
				"racecourse",
				"racecourses"
			},
			{
				"apocalypse",
				"apocalypses"
			},
			{
				"courthouse",
				"courthouses"
			},
			{
				"powerhouse",
				"powerhouses"
			},
			{
				"storehouse",
				"storehouses"
			},
			{
				"glasshouse",
				"glasshouses"
			},
			{
				"hypotenuse",
				"hypotenuses"
			},
			{
				"peroxidase",
				"peroxidases"
			},
			{
				"pillowcase",
				"pillowcases"
			},
			{
				"roundhouse",
				"roundhouses"
			},
			{
				"streetwise",
				"streetwises"
			},
			{
				"expertise",
				"expertises"
			},
			{
				"discourse",
				"discourses"
			},
			{
				"warehouse",
				"warehouses"
			},
			{
				"staircase",
				"staircases"
			},
			{
				"workhouse",
				"workhouses"
			},
			{
				"briefcase",
				"briefcases"
			},
			{
				"clubhouse",
				"clubhouses"
			},
			{
				"clockwise",
				"clockwises"
			},
			{
				"concourse",
				"concourses"
			},
			{
				"playhouse",
				"playhouses"
			},
			{
				"turquoise",
				"turquoises"
			},
			{
				"boathouse",
				"boathouses"
			},
			{
				"cellulose",
				"celluloses"
			},
			{
				"epitomise",
				"epitomises"
			},
			{
				"gatehouse",
				"gatehouses"
			},
			{
				"grandiose",
				"grandioses"
			},
			{
				"menopause",
				"menopauses"
			},
			{
				"penthouse",
				"penthouses"
			},
			{
				"racehorse",
				"racehorses"
			},
			{
				"transpose",
				"transposes"
			},
			{
				"almshouse",
				"almshouses"
			},
			{
				"customise",
				"customises"
			},
			{
				"footloose",
				"footlooses"
			},
			{
				"galvanise",
				"galvanises"
			},
			{
				"princesse",
				"princesses"
			},
			{
				"universe",
				"universes"
			},
			{
				"workhorse",
				"workhorses"
			}
		};

		// Token: 0x04000832 RID: 2098
		private readonly Dictionary<string, string> _wordsEndingWithSisList = new Dictionary<string, string>
		{
			{
				"analysis",
				"analyses"
			},
			{
				"crisis",
				"crises"
			},
			{
				"basis",
				"bases"
			},
			{
				"atherosclerosis",
				"atheroscleroses"
			},
			{
				"electrophoresis",
				"electrophoreses"
			},
			{
				"psychoanalysis",
				"psychoanalyses"
			},
			{
				"photosynthesis",
				"photosyntheses"
			},
			{
				"amniocentesis",
				"amniocenteses"
			},
			{
				"metamorphosis",
				"metamorphoses"
			},
			{
				"toxoplasmosis",
				"toxoplasmoses"
			},
			{
				"endometriosis",
				"endometrioses"
			},
			{
				"tuberculosis",
				"tuberculoses"
			},
			{
				"pathogenesis",
				"pathogeneses"
			},
			{
				"osteoporosis",
				"osteoporoses"
			},
			{
				"parenthesis",
				"parentheses"
			},
			{
				"anastomosis",
				"anastomoses"
			},
			{
				"peristalsis",
				"peristalses"
			},
			{
				"hypothesis",
				"hypotheses"
			},
			{
				"antithesis",
				"antitheses"
			},
			{
				"apotheosis",
				"apotheoses"
			},
			{
				"thrombosis",
				"thromboses"
			},
			{
				"diagnosis",
				"diagnoses"
			},
			{
				"synthesis",
				"syntheses"
			},
			{
				"paralysis",
				"paralyses"
			},
			{
				"prognosis",
				"prognoses"
			},
			{
				"cirrhosis",
				"cirrhoses"
			},
			{
				"sclerosis",
				"scleroses"
			},
			{
				"psychosis",
				"psychoses"
			},
			{
				"apoptosis",
				"apoptoses"
			},
			{
				"symbiosis",
				"symbioses"
			}
		};
	}
}
