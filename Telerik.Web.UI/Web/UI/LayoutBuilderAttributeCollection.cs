using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200183D RID: 6205
	public sealed class LayoutBuilderAttributeCollection
	{
		// Token: 0x0600F108 RID: 61704 RVA: 0x0036D0EC File Offset: 0x0036B2EC
		internal LayoutBuilderAttributeCollection()
		{
		}

		// Token: 0x0600F109 RID: 61705 RVA: 0x0036D0F4 File Offset: 0x0036B2F4
		public LayoutBuilderAttributeCollection(StateBag bag)
		{
			this._bag = bag;
		}

		// Token: 0x0600F10A RID: 61706 RVA: 0x0036D103 File Offset: 0x0036B303
		public void Add(string key, string value)
		{
			this._bag[key] = value;
		}

		// Token: 0x0600F10B RID: 61707 RVA: 0x0036D112 File Offset: 0x0036B312
		public void Clear()
		{
			this._bag.Clear();
		}

		// Token: 0x0600F10C RID: 61708 RVA: 0x0036D120 File Offset: 0x0036B320
		public override bool Equals(object o)
		{
			LayoutBuilderAttributeCollection layoutBuilderAttributeCollection = o as LayoutBuilderAttributeCollection;
			if (layoutBuilderAttributeCollection == null)
			{
				return false;
			}
			if (layoutBuilderAttributeCollection.Count != this._bag.Count)
			{
				return false;
			}
			foreach (object obj in this._bag)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (this[(string)dictionaryEntry.Key] != layoutBuilderAttributeCollection[(string)dictionaryEntry.Key])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600F10D RID: 61709 RVA: 0x0036D1C8 File Offset: 0x0036B3C8
		public override int GetHashCode()
		{
			this._bag.GetHashCode();
			LayoutBuilderAttributeCollection.HashCodeCombiner hashCodeCombiner = new LayoutBuilderAttributeCollection.HashCodeCombiner();
			foreach (object obj in this._bag)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				hashCodeCombiner.AddObject(dictionaryEntry.Key);
				hashCodeCombiner.AddObject(dictionaryEntry.Value);
			}
			return hashCodeCombiner.CombinedHash32;
		}

		// Token: 0x0600F10E RID: 61710 RVA: 0x0036D24C File Offset: 0x0036B44C
		public void Remove(string key)
		{
			this._bag.Remove(key);
		}

		// Token: 0x170048D7 RID: 18647
		// (get) Token: 0x0600F10F RID: 61711 RVA: 0x0036D25A File Offset: 0x0036B45A
		public int Count
		{
			get
			{
				return this._bag.Count;
			}
		}

		// Token: 0x170048D8 RID: 18648
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

		// Token: 0x170048D9 RID: 18649
		// (get) Token: 0x0600F112 RID: 61714 RVA: 0x0036D284 File Offset: 0x0036B484
		public ICollection Keys
		{
			get
			{
				return this._bag.Keys;
			}
		}

		// Token: 0x04004564 RID: 17764
		private StateBag _bag;

		// Token: 0x0200183E RID: 6206
		private class HashCodeCombiner
		{
			// Token: 0x0600F113 RID: 61715 RVA: 0x0036D291 File Offset: 0x0036B491
			internal HashCodeCombiner()
			{
				this._combinedHash = 5381L;
			}

			// Token: 0x0600F114 RID: 61716 RVA: 0x0036D2A5 File Offset: 0x0036B4A5
			internal HashCodeCombiner(long initialCombinedHash)
			{
				this._combinedHash = initialCombinedHash;
			}

			// Token: 0x0600F115 RID: 61717 RVA: 0x0036D2B4 File Offset: 0x0036B4B4
			internal void AddArray(string[] a)
			{
				if (a != null)
				{
					int num = a.Length;
					for (int i = 0; i < num; i++)
					{
						this.AddObject(a[i]);
					}
				}
			}

			// Token: 0x0600F116 RID: 61718 RVA: 0x0036D2DD File Offset: 0x0036B4DD
			internal void AddCaseInsensitiveString(string s)
			{
				if (s != null)
				{
					this.AddInt(StringComparer.OrdinalIgnoreCase.GetHashCode(s));
				}
			}

			// Token: 0x0600F117 RID: 61719 RVA: 0x0036D2F3 File Offset: 0x0036B4F3
			internal void AddDateTime(DateTime dt)
			{
				this.AddInt(dt.GetHashCode());
			}

			// Token: 0x0600F118 RID: 61720 RVA: 0x0036D308 File Offset: 0x0036B508
			private void AddFileSize(long fileSize)
			{
				this.AddInt(fileSize.GetHashCode());
			}

			// Token: 0x0600F119 RID: 61721 RVA: 0x0036D317 File Offset: 0x0036B517
			internal void AddInt(int n)
			{
				this._combinedHash = ((this._combinedHash << 5) + this._combinedHash ^ (long)n);
			}

			// Token: 0x0600F11A RID: 61722 RVA: 0x0036D331 File Offset: 0x0036B531
			internal void AddObject(bool b)
			{
				this.AddInt(b.GetHashCode());
			}

			// Token: 0x0600F11B RID: 61723 RVA: 0x0036D340 File Offset: 0x0036B540
			internal void AddObject(byte b)
			{
				this.AddInt(b.GetHashCode());
			}

			// Token: 0x0600F11C RID: 61724 RVA: 0x0036D34F File Offset: 0x0036B54F
			internal void AddObject(int n)
			{
				this.AddInt(n);
			}

			// Token: 0x0600F11D RID: 61725 RVA: 0x0036D358 File Offset: 0x0036B558
			internal void AddObject(long l)
			{
				this.AddInt(l.GetHashCode());
			}

			// Token: 0x0600F11E RID: 61726 RVA: 0x0036D367 File Offset: 0x0036B567
			internal void AddObject(object o)
			{
				if (o != null)
				{
					this.AddInt(o.GetHashCode());
				}
			}

			// Token: 0x0600F11F RID: 61727 RVA: 0x0036D378 File Offset: 0x0036B578
			internal void AddObject(string s)
			{
				if (s != null)
				{
					this.AddInt(s.GetHashCode());
				}
			}

			// Token: 0x0600F120 RID: 61728 RVA: 0x0036D389 File Offset: 0x0036B589
			internal static int CombineHashCodes(int h1, int h2)
			{
				return (h1 << 5) + h1 ^ h2;
			}

			// Token: 0x0600F121 RID: 61729 RVA: 0x0036D392 File Offset: 0x0036B592
			internal static int CombineHashCodes(int h1, int h2, int h3)
			{
				return LayoutBuilderAttributeCollection.HashCodeCombiner.CombineHashCodes(LayoutBuilderAttributeCollection.HashCodeCombiner.CombineHashCodes(h1, h2), h3);
			}

			// Token: 0x0600F122 RID: 61730 RVA: 0x0036D3A1 File Offset: 0x0036B5A1
			internal static int CombineHashCodes(int h1, int h2, int h3, int h4)
			{
				return LayoutBuilderAttributeCollection.HashCodeCombiner.CombineHashCodes(LayoutBuilderAttributeCollection.HashCodeCombiner.CombineHashCodes(h1, h2), LayoutBuilderAttributeCollection.HashCodeCombiner.CombineHashCodes(h3, h4));
			}

			// Token: 0x0600F123 RID: 61731 RVA: 0x0036D3B6 File Offset: 0x0036B5B6
			internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5)
			{
				return LayoutBuilderAttributeCollection.HashCodeCombiner.CombineHashCodes(LayoutBuilderAttributeCollection.HashCodeCombiner.CombineHashCodes(h1, h2, h3, h4), h5);
			}

			// Token: 0x170048DA RID: 18650
			// (get) Token: 0x0600F124 RID: 61732 RVA: 0x0036D3C8 File Offset: 0x0036B5C8
			internal long CombinedHash
			{
				get
				{
					return this._combinedHash;
				}
			}

			// Token: 0x170048DB RID: 18651
			// (get) Token: 0x0600F125 RID: 61733 RVA: 0x0036D3D0 File Offset: 0x0036B5D0
			internal int CombinedHash32
			{
				get
				{
					return this._combinedHash.GetHashCode();
				}
			}

			// Token: 0x04004565 RID: 17765
			private long _combinedHash;
		}
	}
}
