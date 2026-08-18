using System;

namespace System.Collections.Specialized
{
	// Token: 0x020003B8 RID: 952
	[Serializable]
	internal class StringDictionaryWithComparer : StringDictionary
	{
		// Token: 0x060023E8 RID: 9192 RVA: 0x000A8EE9 File Offset: 0x000A70E9
		public StringDictionaryWithComparer() : this(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x000A8EF6 File Offset: 0x000A70F6
		public StringDictionaryWithComparer(IEqualityComparer comparer)
		{
			base.ReplaceHashtable(new Hashtable(comparer));
		}

		// Token: 0x1700091C RID: 2332
		public override string this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				return (string)this.contents[key];
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this.contents[key] = value;
			}
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x000A8F48 File Offset: 0x000A7148
		public override void Add(string key, string value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Add(key, value);
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x000A8F65 File Offset: 0x000A7165
		public override bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.contents.ContainsKey(key);
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x000A8F81 File Offset: 0x000A7181
		public override void Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Remove(key);
		}
	}
}
