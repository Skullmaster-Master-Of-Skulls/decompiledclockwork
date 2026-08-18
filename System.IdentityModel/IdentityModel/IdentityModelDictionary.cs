using System;
using System.Collections.Generic;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x02000042 RID: 66
	internal class IdentityModelDictionary : IXmlDictionary
	{
		// Token: 0x06000271 RID: 625 RVA: 0x0000A674 File Offset: 0x00008874
		public IdentityModelDictionary(IdentityModelStrings strings)
		{
			this.strings = strings;
			this.count = strings.Count;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000A68F File Offset: 0x0000888F
		public static IdentityModelDictionary CurrentVersion
		{
			get
			{
				return IdentityModelDictionary.Version1;
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000A696 File Offset: 0x00008896
		public XmlDictionaryString CreateString(string value, int key)
		{
			return new XmlDictionaryString(this, value, key);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000A6A0 File Offset: 0x000088A0
		public bool TryLookup(string key, out XmlDictionaryString value)
		{
			if (key == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("key"));
			}
			if (this.dictionary == null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(this.count);
				for (int i = 0; i < this.count; i++)
				{
					dictionary.Add(this.strings[i], i);
				}
				this.dictionary = dictionary;
			}
			int key2;
			if (this.dictionary.TryGetValue(key, out key2))
			{
				return this.TryLookup(key2, out value);
			}
			value = null;
			return false;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000A724 File Offset: 0x00008924
		public bool TryLookup(int key, out XmlDictionaryString value)
		{
			if (key < 0 || key >= this.count)
			{
				value = null;
				return false;
			}
			if (this.dictionaryStrings == null)
			{
				this.dictionaryStrings = new XmlDictionaryString[this.count];
			}
			XmlDictionaryString xmlDictionaryString = this.dictionaryStrings[key];
			if (xmlDictionaryString == null)
			{
				xmlDictionaryString = this.CreateString(this.strings[key], key);
				this.dictionaryStrings[key] = xmlDictionaryString;
			}
			value = xmlDictionaryString;
			return true;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000A78C File Offset: 0x0000898C
		public bool TryLookup(XmlDictionaryString key, out XmlDictionaryString value)
		{
			if (key == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("key"));
			}
			if (key.Dictionary == this)
			{
				value = key;
				return true;
			}
			if (key.Dictionary == IdentityModelDictionary.CurrentVersion)
			{
				if (this.versionedDictionaryStrings == null)
				{
					this.versionedDictionaryStrings = new XmlDictionaryString[IdentityModelDictionary.CurrentVersion.count];
				}
				XmlDictionaryString xmlDictionaryString = this.versionedDictionaryStrings[key.Key];
				if (xmlDictionaryString == null)
				{
					if (!this.TryLookup(key.Value, out xmlDictionaryString))
					{
						value = null;
						return false;
					}
					this.versionedDictionaryStrings[key.Key] = xmlDictionaryString;
				}
				value = xmlDictionaryString;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x04000176 RID: 374
		public static readonly IdentityModelDictionary Version1 = new IdentityModelDictionary(new IdentityModelStringsVersion1());

		// Token: 0x04000177 RID: 375
		private IdentityModelStrings strings;

		// Token: 0x04000178 RID: 376
		private int count;

		// Token: 0x04000179 RID: 377
		private XmlDictionaryString[] dictionaryStrings;

		// Token: 0x0400017A RID: 378
		private Dictionary<string, int> dictionary;

		// Token: 0x0400017B RID: 379
		private XmlDictionaryString[] versionedDictionaryStrings;
	}
}
