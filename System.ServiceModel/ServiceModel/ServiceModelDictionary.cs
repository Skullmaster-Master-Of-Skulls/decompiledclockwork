using System;
using System.Collections.Generic;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x0200004F RID: 79
	internal class ServiceModelDictionary : IXmlDictionary
	{
		// Token: 0x0600021B RID: 539 RVA: 0x0000B174 File Offset: 0x00009374
		public ServiceModelDictionary(ServiceModelStrings strings)
		{
			this.strings = strings;
			this.count = strings.Count;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000B18F File Offset: 0x0000938F
		public static ServiceModelDictionary CurrentVersion
		{
			get
			{
				return ServiceModelDictionary.Version1;
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000B196 File Offset: 0x00009396
		public XmlDictionaryString CreateString(string value, int key)
		{
			return new XmlDictionaryString(this, value, key);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000B1A0 File Offset: 0x000093A0
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

		// Token: 0x0600021F RID: 543 RVA: 0x0000B224 File Offset: 0x00009424
		public bool TryLookup(int key, out XmlDictionaryString value)
		{
			if (key < 0 || key >= this.count)
			{
				value = null;
				return false;
			}
			XmlDictionaryString xmlDictionaryString;
			if (key < 32)
			{
				if (this.dictionaryStrings1 == null)
				{
					this.dictionaryStrings1 = new XmlDictionaryString[32];
				}
				xmlDictionaryString = this.dictionaryStrings1[key];
				if (xmlDictionaryString == null)
				{
					xmlDictionaryString = this.CreateString(this.strings[key], key);
					this.dictionaryStrings1[key] = xmlDictionaryString;
				}
			}
			else
			{
				if (this.dictionaryStrings2 == null)
				{
					this.dictionaryStrings2 = new XmlDictionaryString[this.count - 32];
				}
				xmlDictionaryString = this.dictionaryStrings2[key - 32];
				if (xmlDictionaryString == null)
				{
					xmlDictionaryString = this.CreateString(this.strings[key], key);
					this.dictionaryStrings2[key - 32] = xmlDictionaryString;
				}
			}
			value = xmlDictionaryString;
			return true;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000B2D8 File Offset: 0x000094D8
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
			if (key.Dictionary == ServiceModelDictionary.CurrentVersion)
			{
				if (this.versionedDictionaryStrings == null)
				{
					this.versionedDictionaryStrings = new XmlDictionaryString[ServiceModelDictionary.CurrentVersion.count];
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

		// Token: 0x040002BE RID: 702
		public static readonly ServiceModelDictionary Version1 = new ServiceModelDictionary(new ServiceModelStringsVersion1());

		// Token: 0x040002BF RID: 703
		private ServiceModelStrings strings;

		// Token: 0x040002C0 RID: 704
		private int count;

		// Token: 0x040002C1 RID: 705
		private XmlDictionaryString[] dictionaryStrings1;

		// Token: 0x040002C2 RID: 706
		private XmlDictionaryString[] dictionaryStrings2;

		// Token: 0x040002C3 RID: 707
		private Dictionary<string, int> dictionary;

		// Token: 0x040002C4 RID: 708
		private XmlDictionaryString[] versionedDictionaryStrings;
	}
}
