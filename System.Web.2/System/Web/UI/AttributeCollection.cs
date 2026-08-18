using System;
using System.Collections;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x0200023E RID: 574
	public sealed class AttributeCollection
	{
		// Token: 0x06001ACA RID: 6858 RVA: 0x00054026 File Offset: 0x00052226
		public AttributeCollection(StateBag bag)
		{
			this._bag = bag;
		}

		// Token: 0x17000787 RID: 1927
		public string this[string key]
		{
			get
			{
				if (this._styleColl != null && StringUtil.EqualsIgnoreCase(key, "style"))
				{
					return this._styleColl.Value;
				}
				return this._bag[key] as string;
			}
			set
			{
				this.Add(key, value);
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x00054073 File Offset: 0x00052273
		public ICollection Keys
		{
			get
			{
				return this._bag.Keys;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001ACE RID: 6862 RVA: 0x00054080 File Offset: 0x00052280
		public int Count
		{
			get
			{
				return this._bag.Count;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x0005408D File Offset: 0x0005228D
		public CssStyleCollection CssStyle
		{
			get
			{
				if (this._styleColl == null)
				{
					this._styleColl = new CssStyleCollection(this._bag);
				}
				return this._styleColl;
			}
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x000540AE File Offset: 0x000522AE
		public void Add(string key, string value)
		{
			if (this._styleColl != null && StringUtil.EqualsIgnoreCase(key, "style"))
			{
				this._styleColl.Value = value;
				return;
			}
			this._bag[key] = value;
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x000540E0 File Offset: 0x000522E0
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

		// Token: 0x06001AD2 RID: 6866 RVA: 0x00054188 File Offset: 0x00052388
		public override int GetHashCode()
		{
			HashCodeCombiner hashCodeCombiner = new HashCodeCombiner();
			foreach (object obj in this._bag)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				hashCodeCombiner.AddObject(dictionaryEntry.Key);
				hashCodeCombiner.AddObject(dictionaryEntry.Value);
			}
			return hashCodeCombiner.CombinedHash32;
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00054200 File Offset: 0x00052400
		public void Remove(string key)
		{
			if (this._styleColl != null && StringUtil.EqualsIgnoreCase(key, "style"))
			{
				this._styleColl.Clear();
				return;
			}
			this._bag.Remove(key);
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0005422F File Offset: 0x0005242F
		public void Clear()
		{
			this._bag.Clear();
			if (this._styleColl != null)
			{
				this._styleColl.Clear();
			}
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x00054250 File Offset: 0x00052450
		public void Render(HtmlTextWriter writer)
		{
			if (this._bag.Count > 0)
			{
				IDictionaryEnumerator enumerator = this._bag.GetEnumerator();
				while (enumerator.MoveNext())
				{
					StateItem stateItem = enumerator.Value as StateItem;
					if (stateItem != null)
					{
						string text = stateItem.Value as string;
						string text2 = enumerator.Key as string;
						if (text2 != null && text != null)
						{
							writer.WriteAttribute(text2, text, true);
						}
					}
				}
			}
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x000542B8 File Offset: 0x000524B8
		public void AddAttributes(HtmlTextWriter writer)
		{
			if (this._bag.Count > 0)
			{
				IDictionaryEnumerator enumerator = this._bag.GetEnumerator();
				while (enumerator.MoveNext())
				{
					StateItem stateItem = enumerator.Value as StateItem;
					if (stateItem != null)
					{
						string text = stateItem.Value as string;
						string text2 = enumerator.Key as string;
						if (text2 != null && text != null)
						{
							writer.AddAttribute(text2, text, true);
						}
					}
				}
			}
		}

		// Token: 0x0400185F RID: 6239
		private StateBag _bag;

		// Token: 0x04001860 RID: 6240
		private CssStyleCollection _styleColl;
	}
}
