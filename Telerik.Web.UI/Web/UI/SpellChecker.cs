using System;
using System.Collections;
using System.IO;
using System.Text;
using Telerik.Web.UI.Dictionaries;
using Telerik.Web.UI.HtmlParsing;
using Telerik.Web.UI.Spell;

namespace Telerik.Web.UI
{
	// Token: 0x020011E3 RID: 4579
	public class SpellChecker : IDisposable
	{
		// Token: 0x17003CFE RID: 15614
		// (get) Token: 0x0600BD0C RID: 48396 RVA: 0x0029E944 File Offset: 0x0029CB44
		// (set) Token: 0x0600BD0D RID: 48397 RVA: 0x0029E952 File Offset: 0x0029CB52
		public string Text
		{
			get
			{
				this.EnsureNotDisposed();
				return this._text;
			}
			set
			{
				this.EnsureNotDisposed();
				this._text = value;
				this.textWords = (TextWord[])this.GetWords(this._text);
				this.InvalidateErrors();
			}
		}

		// Token: 0x17003CFF RID: 15615
		// (get) Token: 0x0600BD0E RID: 48398 RVA: 0x0029E97E File Offset: 0x0029CB7E
		// (set) Token: 0x0600BD0F RID: 48399 RVA: 0x0029E98C File Offset: 0x0029CB8C
		public string DictionaryLanguage
		{
			get
			{
				this.EnsureNotDisposed();
				return this._dictionaryLanguage;
			}
			set
			{
				this.EnsureNotDisposed();
				this._dictionaryLanguage = value;
				this.InvalidateDictionary();
			}
		}

		// Token: 0x17003D00 RID: 15616
		// (get) Token: 0x0600BD10 RID: 48400 RVA: 0x0029E9A1 File Offset: 0x0029CBA1
		// (set) Token: 0x0600BD11 RID: 48401 RVA: 0x0029E9AF File Offset: 0x0029CBAF
		internal string DictionaryPath
		{
			get
			{
				this.EnsureNotDisposed();
				return this._dictPath;
			}
			set
			{
				this.EnsureNotDisposed();
				this._dictPath = value;
				this.InvalidateDictionary();
			}
		}

		// Token: 0x17003D01 RID: 15617
		// (get) Token: 0x0600BD12 RID: 48402 RVA: 0x0029E9C4 File Offset: 0x0029CBC4
		// (set) Token: 0x0600BD13 RID: 48403 RVA: 0x0029E9D2 File Offset: 0x0029CBD2
		public string CustomAppendix
		{
			get
			{
				this.EnsureNotDisposed();
				return this._customAppendix;
			}
			set
			{
				this.EnsureNotDisposed();
				this._customAppendix = value;
				this.CustomDictionarySource.CustomAppendix = this._customAppendix;
				this.InvalidateDictionary();
			}
		}

		// Token: 0x17003D02 RID: 15618
		// (get) Token: 0x0600BD14 RID: 48404 RVA: 0x0029E9F8 File Offset: 0x0029CBF8
		// (set) Token: 0x0600BD15 RID: 48405 RVA: 0x0029EA06 File Offset: 0x0029CC06
		public int EditDistance
		{
			get
			{
				this.EnsureNotDisposed();
				return this._editDistance;
			}
			set
			{
				this.EnsureNotDisposed();
				this._editDistance = value;
				this.InvalidateDictionary();
			}
		}

		// Token: 0x17003D03 RID: 15619
		// (get) Token: 0x0600BD16 RID: 48406 RVA: 0x0029EA1B File Offset: 0x0029CC1B
		private bool CheckAllCaps
		{
			get
			{
				return (this.WordIgnoreOptions & WordIgnoreOptions.UPPERCASE) == WordIgnoreOptions.None;
			}
		}

		// Token: 0x17003D04 RID: 15620
		// (get) Token: 0x0600BD17 RID: 48407 RVA: 0x0029EA28 File Offset: 0x0029CC28
		private bool CheckForRepeatWords
		{
			get
			{
				return (this.WordIgnoreOptions & WordIgnoreOptions.RepeatedWords) == WordIgnoreOptions.None;
			}
		}

		// Token: 0x17003D05 RID: 15621
		// (get) Token: 0x0600BD18 RID: 48408 RVA: 0x0029EA35 File Offset: 0x0029CC35
		private bool CheckCapital
		{
			get
			{
				return (this.WordIgnoreOptions & WordIgnoreOptions.WordsWithCapitalLetters) == WordIgnoreOptions.None;
			}
		}

		// Token: 0x17003D06 RID: 15622
		// (get) Token: 0x0600BD19 RID: 48409 RVA: 0x0029EA42 File Offset: 0x0029CC42
		private bool CheckWordsWNumbers
		{
			get
			{
				return (this.WordIgnoreOptions & WordIgnoreOptions.WordsWithNumbers) == WordIgnoreOptions.None;
			}
		}

		// Token: 0x17003D07 RID: 15623
		// (get) Token: 0x0600BD1A RID: 48410 RVA: 0x0029EA4F File Offset: 0x0029CC4F
		// (set) Token: 0x0600BD1B RID: 48411 RVA: 0x0029EA5D File Offset: 0x0029CC5D
		public WordIgnoreOptions WordIgnoreOptions
		{
			get
			{
				this.EnsureNotDisposed();
				return this._wordIgnoreOptions;
			}
			set
			{
				this.EnsureNotDisposed();
				this._wordIgnoreOptions = value;
			}
		}

		// Token: 0x0600BD1C RID: 48412 RVA: 0x0029EA6C File Offset: 0x0029CC6C
		internal SpellChecker()
		{
		}

		// Token: 0x0600BD1D RID: 48413 RVA: 0x0029EAE4 File Offset: 0x0029CCE4
		public SpellChecker(string dictionaryPath)
		{
			this._dictPath = dictionaryPath;
		}

		// Token: 0x0600BD1E RID: 48414 RVA: 0x0029EB61 File Offset: 0x0029CD61
		public void Close()
		{
			if (this._spellCheckProvider != null)
			{
				this._spellCheckProvider.Close();
			}
		}

		// Token: 0x0600BD1F RID: 48415 RVA: 0x0029EB76 File Offset: 0x0029CD76
		internal ITextWord GetWord(int index)
		{
			return this.textWords[index];
		}

		// Token: 0x17003D08 RID: 15624
		// (get) Token: 0x0600BD20 RID: 48416 RVA: 0x0029EB80 File Offset: 0x0029CD80
		internal int WordCount
		{
			get
			{
				return this.textWords.Length;
			}
		}

		// Token: 0x17003D09 RID: 15625
		// (get) Token: 0x0600BD21 RID: 48417 RVA: 0x0029EB8C File Offset: 0x0029CD8C
		private ISpellCheckProvider SpellProvider
		{
			get
			{
				if (this._spellCheckProvider != null)
				{
					return this._spellCheckProvider;
				}
				if (!string.IsNullOrEmpty(this._spellCheckProviderTypeName))
				{
					this._spellCheckProvider = (ISpellCheckProvider)Activator.CreateInstance(Type.GetType(this._spellCheckProviderTypeName), new object[]
					{
						this
					});
				}
				else if (this._spellCheckProviderType == SpellCheckProvider.MicrosoftWordProvider)
				{
					ISpellCheckProvider spellCheckProvider = WordAdapterLoader.CreateWordProvider();
					spellCheckProvider.WordIgnoreOptions = this.WordIgnoreOptions;
					spellCheckProvider.Text = this.Text;
					if (!string.IsNullOrEmpty(this.DictionaryLanguage))
					{
						spellCheckProvider.Language = this.DictionaryLanguage;
					}
					this._spellCheckProvider = spellCheckProvider;
				}
				else
				{
					this._spellCheckProvider = new TelerikSpellCheckProvider(this);
				}
				return this._spellCheckProvider;
			}
		}

		// Token: 0x17003D0A RID: 15626
		// (get) Token: 0x0600BD22 RID: 48418 RVA: 0x0029EC39 File Offset: 0x0029CE39
		// (set) Token: 0x0600BD23 RID: 48419 RVA: 0x0029EC47 File Offset: 0x0029CE47
		public string SpellCheckProviderTypeName
		{
			get
			{
				this.EnsureNotDisposed();
				return this._spellCheckProviderTypeName;
			}
			set
			{
				this.EnsureNotDisposed();
				this._spellCheckProviderTypeName = value;
				this.InvalidateDictionary();
				this.InvalidateSpellProvider();
			}
		}

		// Token: 0x17003D0B RID: 15627
		// (get) Token: 0x0600BD24 RID: 48420 RVA: 0x0029EC62 File Offset: 0x0029CE62
		// (set) Token: 0x0600BD25 RID: 48421 RVA: 0x0029EC70 File Offset: 0x0029CE70
		public SpellCheckProvider SpellCheckProvider
		{
			get
			{
				this.EnsureNotDisposed();
				return this._spellCheckProviderType;
			}
			set
			{
				this.EnsureNotDisposed();
				this._spellCheckProviderType = value;
				this.InvalidateDictionary();
				this.InvalidateSpellProvider();
			}
		}

		// Token: 0x0600BD26 RID: 48422 RVA: 0x0029EC8B File Offset: 0x0029CE8B
		private void InvalidateSpellProvider()
		{
			this._spellCheckProvider = null;
		}

		// Token: 0x0600BD27 RID: 48423 RVA: 0x0029EC94 File Offset: 0x0029CE94
		internal string BadWordsJScript()
		{
			this.CheckText();
			return this._errors.ToJavaScriptArray();
		}

		// Token: 0x0600BD28 RID: 48424 RVA: 0x0029ECA8 File Offset: 0x0029CEA8
		public SpellCheckErrors CheckText()
		{
			this.EnsureNotDisposed();
			ITextWord previous = null;
			this._errors = new SpellCheckErrors(this.CheckAllCaps);
			ISpellCheckProvider spellProvider = this.SpellProvider;
			for (int i = 0; i < this.textWords.Length; i++)
			{
				ITextWord textWord = this.textWords[i];
				if (!spellProvider.CheckWord(textWord, previous))
				{
					string[] suggestions;
					if (this._errors.Contains(textWord.Word))
					{
						suggestions = this._errors.GetSuggestions(textWord.Word);
					}
					else
					{
						suggestions = spellProvider.GetSuggestions(textWord);
					}
					this._errors.Add(i, textWord, suggestions);
				}
				previous = textWord;
			}
			return this.Errors;
		}

		// Token: 0x0600BD29 RID: 48425 RVA: 0x0029ED44 File Offset: 0x0029CF44
		internal string WordOffsetsJScript()
		{
			StringBuilder stringBuilder = new StringBuilder("[");
			for (int i = 0; i < this.textWords.Length; i++)
			{
				ITextWord textWord = this.textWords[i];
				stringBuilder.Append(string.Format("{0},", textWord.Offset.ToString()));
			}
			SpellCheckError.RemoveLastChar(stringBuilder);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0600BD2A RID: 48426 RVA: 0x0029EDAF File Offset: 0x0029CFAF
		public string[] GetSuggestions(string word)
		{
			return this.dictionary.GetSimilar(word);
		}

		// Token: 0x0600BD2B RID: 48427 RVA: 0x0029EDBD File Offset: 0x0029CFBD
		internal bool CheckWord(string word)
		{
			return this.CheckWord(word, "");
		}

		// Token: 0x0600BD2C RID: 48428 RVA: 0x0029EDCC File Offset: 0x0029CFCC
		internal bool CheckWord(string word, string sLastWord)
		{
			return (!this.CheckForRepeatWords || !(word == sLastWord)) && ((!this.CheckCapital && char.ToUpper(word[0]) == word[0]) || word.Length == 1 || this.dictionary.HasWord(word));
		}

		// Token: 0x0600BD2D RID: 48429 RVA: 0x0029EE24 File Offset: 0x0029D024
		private object GetWords(string text)
		{
			HtmlTokenizer htmlTokenizer = new HtmlTokenizer(text, this.CheckWordsWNumbers, this.ignoreSettings);
			ArrayList arrayList = htmlTokenizer.Tokenize();
			return arrayList.ToArray(typeof(TextWord));
		}

		// Token: 0x0600BD2E RID: 48430 RVA: 0x0029EE5B File Offset: 0x0029D05B
		private static StreamReader ReaderForFile(string customDictionaryFile)
		{
			if (File.Exists(customDictionaryFile))
			{
				return new StreamReader(customDictionaryFile, Encoding.UTF8);
			}
			return new StreamReader(new MemoryStream());
		}

		// Token: 0x0600BD2F RID: 48431 RVA: 0x0029EE7C File Offset: 0x0029D07C
		public void AddToCustom(string word)
		{
			if (this.dictionary.HasWord(word))
			{
				return;
			}
			try
			{
				TextReader dictionaryReader = new StringReader(word);
				ReaderCustomDictionarySource customSource = new ReaderCustomDictionarySource(dictionaryReader);
				this.dictionary.LoadCustomDictionary(customSource);
			}
			catch (IncompatibleLanguageException)
			{
				throw new IncompatibleWordException();
			}
			this._customDictionarySource.AddWord(word);
		}

		// Token: 0x17003D0C RID: 15628
		// (get) Token: 0x0600BD30 RID: 48432 RVA: 0x0029EED8 File Offset: 0x0029D0D8
		// (set) Token: 0x0600BD31 RID: 48433 RVA: 0x0029EEE0 File Offset: 0x0029D0E0
		internal FragmentIgnoreOptions FragmentIgnoreOptions
		{
			get
			{
				return this.ignoreSettings;
			}
			set
			{
				this.ignoreSettings = value;
				this.Text = this.Text;
			}
		}

		// Token: 0x17003D0D RID: 15629
		// (get) Token: 0x0600BD32 RID: 48434 RVA: 0x0029EEF5 File Offset: 0x0029D0F5
		// (set) Token: 0x0600BD33 RID: 48435 RVA: 0x0029EF0B File Offset: 0x0029D10B
		public ICustomDictionarySource CustomDictionarySource
		{
			get
			{
				if (this._customDictionarySource == null)
				{
					this.LoadCustomDictionarySource();
				}
				return this._customDictionarySource;
			}
			set
			{
				this._customDictionarySource = value;
			}
		}

		// Token: 0x0600BD34 RID: 48436 RVA: 0x0029EF14 File Offset: 0x0029D114
		private void InvalidateCustomDictionarySource()
		{
			this._customDictionarySource = null;
		}

		// Token: 0x0600BD35 RID: 48437 RVA: 0x0029EF1D File Offset: 0x0029D11D
		private void LoadCustomDictionarySource()
		{
			if (string.IsNullOrEmpty(this.customDictionarySourceType))
			{
				this._customDictionarySource = new FileCustomDictionarySource();
				return;
			}
			this.LoadCustomSourceFromType();
		}

		// Token: 0x0600BD36 RID: 48438 RVA: 0x0029EF40 File Offset: 0x0029D140
		private void LoadCustomSourceFromType()
		{
			Type type = Type.GetType(this.customDictionarySourceType);
			if (type == null)
			{
				throw new InvalidCustomDictionarySourceException("Can't find the custom dictionary source type.");
			}
			object obj = null;
			try
			{
				obj = Activator.CreateInstance(type);
			}
			catch (Exception ex)
			{
				throw new InvalidCustomDictionarySourceException("Could not create custom dictionary source: " + ex.Message);
			}
			this._customDictionarySource = (obj as ICustomDictionarySource);
			if (this._customDictionarySource == null)
			{
				throw new InvalidCustomDictionarySourceException("The custom dictionary type does not implement the ICustomDictionarySource interface.");
			}
		}

		// Token: 0x17003D0E RID: 15630
		// (get) Token: 0x0600BD37 RID: 48439 RVA: 0x0029EFC0 File Offset: 0x0029D1C0
		// (set) Token: 0x0600BD38 RID: 48440 RVA: 0x0029EFCE File Offset: 0x0029D1CE
		public string CustomDictionarySourceType
		{
			get
			{
				this.EnsureNotDisposed();
				return this.customDictionarySourceType;
			}
			set
			{
				this.EnsureNotDisposed();
				this.customDictionarySourceType = value;
				this.InvalidateCustomDictionarySource();
			}
		}

		// Token: 0x17003D0F RID: 15631
		// (get) Token: 0x0600BD39 RID: 48441 RVA: 0x0029EFE3 File Offset: 0x0029D1E3
		public SpellCheckErrors Errors
		{
			get
			{
				this.EnsureNotDisposed();
				if (this._errors == null)
				{
					this.CheckText();
				}
				return this._errors;
			}
		}

		// Token: 0x0600BD3A RID: 48442 RVA: 0x0029F000 File Offset: 0x0029D200
		private void InvalidateErrors()
		{
			this._errors = null;
		}

		// Token: 0x17003D10 RID: 15632
		// (get) Token: 0x0600BD3B RID: 48443 RVA: 0x0029F009 File Offset: 0x0029D209
		// (set) Token: 0x0600BD3C RID: 48444 RVA: 0x0029F01F File Offset: 0x0029D21F
		internal virtual SpellDictionary dictionary
		{
			get
			{
				if (this._dictionary == null)
				{
					this.LoadDictionary();
				}
				return this._dictionary;
			}
			set
			{
				this._dictionary = value;
			}
		}

		// Token: 0x0600BD3D RID: 48445 RVA: 0x0029F028 File Offset: 0x0029D228
		private void InvalidateDictionary()
		{
			this._dictionary = null;
			this.InvalidateCustomDictionarySource();
			this.InvalidateErrors();
		}

		// Token: 0x0600BD3E RID: 48446 RVA: 0x0029F040 File Offset: 0x0029D240
		internal virtual void LoadDictionary()
		{
			if (this.SpellCheckProvider == SpellCheckProvider.MicrosoftWordProvider)
			{
				return;
			}
			string text = Path.Combine(this.DictionaryPath, this.DictionaryLanguage + ".tdf");
			if (this.SpellCheckProvider == SpellCheckProvider.EditDistanceProvider)
			{
				this._dictionary = new EditDistanceDictionary(this.EditDistance);
			}
			else
			{
				this._dictionary = new MetaphoneDictionary();
			}
			this.CustomDictionarySource.DictionaryPath = this.DictionaryPath;
			this.CustomDictionarySource.Language = this.DictionaryLanguage;
			this.CustomDictionarySource.CustomAppendix = this.CustomAppendix;
			using (StreamReader streamReader = SpellChecker.ReaderForFile(text))
			{
				this._dictionary.Load(streamReader, this.CustomDictionarySource, text);
			}
		}

		// Token: 0x0600BD3F RID: 48447 RVA: 0x0029F104 File Offset: 0x0029D304
		public void Dispose()
		{
			this.Dispose(false);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600BD40 RID: 48448 RVA: 0x0029F113 File Offset: 0x0029D313
		protected virtual void Dispose(bool finalizing)
		{
			if (!finalizing)
			{
				this.Close();
			}
			this.disposed = true;
		}

		// Token: 0x0600BD41 RID: 48449 RVA: 0x0029F128 File Offset: 0x0029D328
		~SpellChecker()
		{
			this.Dispose(true);
		}

		// Token: 0x0600BD42 RID: 48450 RVA: 0x0029F158 File Offset: 0x0029D358
		private void EnsureNotDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("The SpellChecker has been disposed.");
			}
		}

		// Token: 0x17003D11 RID: 15633
		// (get) Token: 0x0600BD43 RID: 48451 RVA: 0x0029F16D File Offset: 0x0029D36D
		public TextWord[] TextWords
		{
			get
			{
				return this.textWords;
			}
		}

		// Token: 0x040031B8 RID: 12728
		private WordIgnoreOptions _wordIgnoreOptions;

		// Token: 0x040031B9 RID: 12729
		private bool disposed;

		// Token: 0x040031BA RID: 12730
		private string _text = "";

		// Token: 0x040031BB RID: 12731
		private string _dictionaryLanguage = "en-US";

		// Token: 0x040031BC RID: 12732
		private string _dictPath = "";

		// Token: 0x040031BD RID: 12733
		private string _customAppendix = "-Custom";

		// Token: 0x040031BE RID: 12734
		private int _editDistance = 1;

		// Token: 0x040031BF RID: 12735
		private ICustomDictionarySource _customDictionarySource;

		// Token: 0x040031C0 RID: 12736
		private string customDictionarySourceType = string.Empty;

		// Token: 0x040031C1 RID: 12737
		private TextWord[] textWords = new TextWord[0];

		// Token: 0x040031C2 RID: 12738
		private ISpellCheckProvider _spellCheckProvider;

		// Token: 0x040031C3 RID: 12739
		private string _spellCheckProviderTypeName = string.Empty;

		// Token: 0x040031C4 RID: 12740
		private SpellCheckProvider _spellCheckProviderType = SpellCheckProvider.PhoneticProvider;

		// Token: 0x040031C5 RID: 12741
		private FragmentIgnoreOptions ignoreSettings = FragmentIgnoreOptions.All;

		// Token: 0x040031C6 RID: 12742
		private SpellDictionary _dictionary;

		// Token: 0x040031C7 RID: 12743
		private SpellCheckErrors _errors;
	}
}
