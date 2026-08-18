using System;
using System.IO;

namespace Telerik.Web.UI.Dictionaries
{
	// Token: 0x020011CE RID: 4558
	internal abstract class SpellDictionary
	{
		// Token: 0x0600BC5B RID: 48219
		internal abstract bool HasWord(string word);

		// Token: 0x0600BC5C RID: 48220
		internal abstract string[] GetSimilar(string word);

		// Token: 0x0600BC5D RID: 48221
		internal abstract void Load(TextReader baseDictionaryReader, ICustomDictionarySource customSource, string cacheKey);

		// Token: 0x0600BC5E RID: 48222 RVA: 0x0029CAC4 File Offset: 0x0029ACC4
		protected void LoadBaseDictionary(TextReader baseDictionaryReader)
		{
			if (!this.LoadDictionaryFromCacheSucceeded())
			{
				this.ResetDictionaryItems();
				string line;
				while ((line = baseDictionaryReader.ReadLine()) != null)
				{
					string[] wordComponents = DictionaryImporter.ParseLine(line);
					this.AddDictionaryWord(wordComponents);
				}
				this.SaveDictionaryToCache();
			}
		}

		// Token: 0x0600BC5F RID: 48223 RVA: 0x0029CB00 File Offset: 0x0029AD00
		protected internal virtual void LoadCustomDictionary(ICustomDictionarySource customSource)
		{
			string text;
			while ((text = customSource.ReadWord()) != null)
			{
				if (!string.IsNullOrEmpty(text.Trim()))
				{
					this.AddCustomWord(text);
				}
			}
		}

		// Token: 0x0600BC60 RID: 48224
		protected abstract void AddDictionaryWord(string[] wordComponents);

		// Token: 0x0600BC61 RID: 48225
		protected abstract void AddCustomWord(string word);

		// Token: 0x0600BC62 RID: 48226
		protected abstract bool LoadDictionaryFromCacheSucceeded();

		// Token: 0x0600BC63 RID: 48227
		protected abstract void SaveDictionaryToCache();

		// Token: 0x0600BC64 RID: 48228
		protected abstract void ResetDictionaryItems();
	}
}
