using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Net
{
	// Token: 0x0200020C RID: 524
	internal class SpnDictionary : StringDictionary
	{
		// Token: 0x0600138C RID: 5004 RVA: 0x00066D6B File Offset: 0x00064F6B
		internal SpnDictionary()
		{
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x00066D83 File Offset: 0x00064F83
		public override int Count
		{
			get
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				return this.m_SyncTable.Count;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x00066D9A File Offset: 0x00064F9A
		public override bool IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00066DA0 File Offset: 0x00064FA0
		internal SpnToken InternalGet(string canonicalKey)
		{
			int num = 0;
			string text = null;
			object syncRoot = this.m_SyncTable.SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in this.m_SyncTable.Keys)
				{
					string text2 = (string)obj;
					if (text2 != null && text2.Length > num && string.Compare(text2, 0, canonicalKey, 0, text2.Length, StringComparison.OrdinalIgnoreCase) == 0)
					{
						num = text2.Length;
						text = text2;
					}
				}
			}
			if (text == null)
			{
				return null;
			}
			return (SpnToken)this.m_SyncTable[text];
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00066E78 File Offset: 0x00065078
		internal void InternalSet(string canonicalKey, SpnToken spnToken)
		{
			this.m_SyncTable[canonicalKey] = spnToken;
		}

		// Token: 0x17000424 RID: 1060
		public override string this[string key]
		{
			get
			{
				key = SpnDictionary.GetCanonicalKey(key);
				SpnToken spnToken = this.InternalGet(key);
				if (spnToken != null)
				{
					return spnToken.Spn;
				}
				return null;
			}
			set
			{
				key = SpnDictionary.GetCanonicalKey(key);
				this.InternalSet(key, new SpnToken(value));
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x00066EC7 File Offset: 0x000650C7
		public override ICollection Keys
		{
			get
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				return this.m_SyncTable.Keys;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x00066EDE File Offset: 0x000650DE
		public override object SyncRoot
		{
			[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
			get
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				return this.m_SyncTable;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x00066EF0 File Offset: 0x000650F0
		public override ICollection Values
		{
			get
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				if (this.m_ValuesWrapper == null)
				{
					this.m_ValuesWrapper = new SpnDictionary.ValueCollection(this);
				}
				return this.m_ValuesWrapper;
			}
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00066F16 File Offset: 0x00065116
		public override void Add(string key, string value)
		{
			key = SpnDictionary.GetCanonicalKey(key);
			this.m_SyncTable.Add(key, new SpnToken(value));
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00066F32 File Offset: 0x00065132
		public override void Clear()
		{
			ExceptionHelper.WebPermissionUnrestricted.Demand();
			this.m_SyncTable.Clear();
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00066F49 File Offset: 0x00065149
		public override bool ContainsKey(string key)
		{
			key = SpnDictionary.GetCanonicalKey(key);
			return this.m_SyncTable.ContainsKey(key);
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00066F60 File Offset: 0x00065160
		public override bool ContainsValue(string value)
		{
			ExceptionHelper.WebPermissionUnrestricted.Demand();
			foreach (object obj in this.m_SyncTable.Values)
			{
				SpnToken spnToken = (SpnToken)obj;
				if (spnToken.Spn == value)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00066FD8 File Offset: 0x000651D8
		public override void CopyTo(Array array, int index)
		{
			ExceptionHelper.WebPermissionUnrestricted.Demand();
			SpnDictionary.CheckCopyToArguments(array, index, this.Count);
			int num = 0;
			foreach (object value in this)
			{
				array.SetValue(value, num + index);
				num++;
			}
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00067048 File Offset: 0x00065248
		public override IEnumerator GetEnumerator()
		{
			ExceptionHelper.WebPermissionUnrestricted.Demand();
			foreach (object obj in this.m_SyncTable.Keys)
			{
				string key = (string)obj;
				SpnToken spnToken = (SpnToken)this.m_SyncTable[key];
				yield return new DictionaryEntry(key, spnToken.Spn);
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00067057 File Offset: 0x00065257
		public override void Remove(string key)
		{
			key = SpnDictionary.GetCanonicalKey(key);
			this.m_SyncTable.Remove(key);
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00067070 File Offset: 0x00065270
		private static string GetCanonicalKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			try
			{
				Uri uri = new Uri(key);
				key = uri.GetParts(UriComponents.Scheme | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.SafeUnescaped);
				new WebPermission(NetworkAccess.Connect, new Uri(key)).Demand();
			}
			catch (UriFormatException innerException)
			{
				throw new ArgumentException(SR.GetString("net_mustbeuri", new object[]
				{
					"key"
				}), "key", innerException);
			}
			return key;
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x000670E8 File Offset: 0x000652E8
		private static void CheckCopyToArguments(Array array, int index, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(SR.GetString("Arg_RankMultiDimNotSupported"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < count)
			{
				throw new ArgumentException(SR.GetString("Arg_ArrayPlusOffTooSmall"));
			}
		}

		// Token: 0x0400156C RID: 5484
		private Hashtable m_SyncTable = Hashtable.Synchronized(new Hashtable());

		// Token: 0x0400156D RID: 5485
		private SpnDictionary.ValueCollection m_ValuesWrapper;

		// Token: 0x0200075A RID: 1882
		private class ValueCollection : ICollection, IEnumerable
		{
			// Token: 0x06004215 RID: 16917 RVA: 0x001128EF File Offset: 0x00110AEF
			internal ValueCollection(SpnDictionary spnDictionary)
			{
				this.spnDictionary = spnDictionary;
			}

			// Token: 0x06004216 RID: 16918 RVA: 0x00112900 File Offset: 0x00110B00
			public void CopyTo(Array array, int index)
			{
				SpnDictionary.CheckCopyToArguments(array, index, this.Count);
				int num = 0;
				foreach (object value in this)
				{
					array.SetValue(value, num + index);
					num++;
				}
			}

			// Token: 0x17000F1B RID: 3867
			// (get) Token: 0x06004217 RID: 16919 RVA: 0x00112968 File Offset: 0x00110B68
			public int Count
			{
				get
				{
					return this.spnDictionary.m_SyncTable.Values.Count;
				}
			}

			// Token: 0x17000F1C RID: 3868
			// (get) Token: 0x06004218 RID: 16920 RVA: 0x0011297F File Offset: 0x00110B7F
			public bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F1D RID: 3869
			// (get) Token: 0x06004219 RID: 16921 RVA: 0x00112982 File Offset: 0x00110B82
			public object SyncRoot
			{
				get
				{
					return this.spnDictionary.m_SyncTable.SyncRoot;
				}
			}

			// Token: 0x0600421A RID: 16922 RVA: 0x00112994 File Offset: 0x00110B94
			public IEnumerator GetEnumerator()
			{
				foreach (object obj in this.spnDictionary.m_SyncTable.Values)
				{
					SpnToken spnToken = (SpnToken)obj;
					yield return (spnToken != null) ? spnToken.Spn : null;
				}
				IEnumerator enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x0400322E RID: 12846
			private SpnDictionary spnDictionary;
		}
	}
}
