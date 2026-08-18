using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x0200104A RID: 4170
	public sealed class AttributeCollection
	{
		// Token: 0x0600A3CD RID: 41933 RVA: 0x00246C08 File Offset: 0x00244E08
		internal AttributeCollection()
		{
		}

		// Token: 0x0600A3CE RID: 41934 RVA: 0x00246C10 File Offset: 0x00244E10
		public AttributeCollection(StateBag bag)
		{
			this._bag = bag;
		}

		// Token: 0x0600A3CF RID: 41935 RVA: 0x00246C1F File Offset: 0x00244E1F
		public void Add(string key, string value)
		{
			this._bag[key] = value;
		}

		// Token: 0x0600A3D0 RID: 41936 RVA: 0x00246C2E File Offset: 0x00244E2E
		public void Clear()
		{
			this._bag.Clear();
		}

		// Token: 0x0600A3D1 RID: 41937 RVA: 0x00246C3C File Offset: 0x00244E3C
		public override bool Equals(object o)
		{
			AttributeCollection attributeCollection = o as AttributeCollection;
			if (attributeCollection == null)
			{
				return false;
			}
			if (attributeCollection.Count != this._bag.Count)
			{
				return false;
			}
			foreach (object obj in this._bag)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (this[(string)dictionaryEntry.Key] != attributeCollection[(string)dictionaryEntry.Key])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600A3D2 RID: 41938 RVA: 0x00246CE4 File Offset: 0x00244EE4
		public override int GetHashCode()
		{
			this._bag.GetHashCode();
			AttributeCollection.HashCodeCombiner hashCodeCombiner = new AttributeCollection.HashCodeCombiner();
			foreach (object obj in this._bag)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				hashCodeCombiner.AddObject(dictionaryEntry.Key);
				hashCodeCombiner.AddObject(dictionaryEntry.Value);
			}
			return hashCodeCombiner.CombinedHash32;
		}

		// Token: 0x0600A3D3 RID: 41939 RVA: 0x00246D68 File Offset: 0x00244F68
		public void Remove(string key)
		{
			this._bag.Remove(key);
		}

		// Token: 0x170033B2 RID: 13234
		// (get) Token: 0x0600A3D4 RID: 41940 RVA: 0x00246D76 File Offset: 0x00244F76
		public int Count
		{
			get
			{
				return this._bag.Count;
			}
		}

		// Token: 0x170033B3 RID: 13235
		public string this[string key]
		{
			get
			{
				return this._bag[key] as string;
			}
			set
			{
				this.Add(key, value);
			}
		}

		// Token: 0x170033B4 RID: 13236
		public string this[StandardDropDownProperties key]
		{
			get
			{
				return this._bag[key.ToString("G")] as string;
			}
			set
			{
				this.Add(key.ToString("G"), value);
			}
		}

		// Token: 0x170033B5 RID: 13237
		// (get) Token: 0x0600A3D9 RID: 41945 RVA: 0x00246DDB File Offset: 0x00244FDB
		public ICollection Keys
		{
			get
			{
				return this._bag.Keys;
			}
		}

		// Token: 0x04002DA8 RID: 11688
		private readonly StateBag _bag;

		// Token: 0x0200104B RID: 4171
		private class HashCodeCombiner
		{
			// Token: 0x0600A3DA RID: 41946 RVA: 0x00246DE8 File Offset: 0x00244FE8
			internal HashCodeCombiner()
			{
				this._combinedHash = 5381L;
			}

			// Token: 0x0600A3DB RID: 41947 RVA: 0x00246DFC File Offset: 0x00244FFC
			internal void AddInt(int n)
			{
				this._combinedHash = ((this._combinedHash << 5) + this._combinedHash ^ (long)n);
			}

			// Token: 0x0600A3DC RID: 41948 RVA: 0x00246E16 File Offset: 0x00245016
			internal void AddObject(object o)
			{
				if (o != null)
				{
					this.AddInt(o.GetHashCode());
				}
			}

			// Token: 0x170033B6 RID: 13238
			// (get) Token: 0x0600A3DD RID: 41949 RVA: 0x00246E27 File Offset: 0x00245027
			internal int CombinedHash32
			{
				get
				{
					return this._combinedHash.GetHashCode();
				}
			}

			// Token: 0x04002DA9 RID: 11689
			private long _combinedHash;
		}
	}
}
