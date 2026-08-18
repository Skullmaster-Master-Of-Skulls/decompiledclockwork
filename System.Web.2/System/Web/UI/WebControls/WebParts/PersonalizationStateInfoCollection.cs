using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Util;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000560 RID: 1376
	[Serializable]
	public sealed class PersonalizationStateInfoCollection : ICollection, IEnumerable
	{
		// Token: 0x060045DC RID: 17884 RVA: 0x000E63DB File Offset: 0x000E45DB
		public PersonalizationStateInfoCollection()
		{
			this._indices = new Dictionary<PersonalizationStateInfoCollection.Key, int>(PersonalizationStateInfoCollection.KeyComparer.Default);
			this._values = new ArrayList();
		}

		// Token: 0x17001495 RID: 5269
		// (get) Token: 0x060045DD RID: 17885 RVA: 0x000E63FE File Offset: 0x000E45FE
		public int Count
		{
			get
			{
				return this._values.Count;
			}
		}

		// Token: 0x17001496 RID: 5270
		public PersonalizationStateInfo this[string path, string username]
		{
			get
			{
				if (path == null)
				{
					throw new ArgumentNullException("path");
				}
				PersonalizationStateInfoCollection.Key key = new PersonalizationStateInfoCollection.Key(path, username);
				int index;
				if (!this._indices.TryGetValue(key, out index))
				{
					return null;
				}
				return (PersonalizationStateInfo)this._values[index];
			}
		}

		// Token: 0x17001497 RID: 5271
		public PersonalizationStateInfo this[int index]
		{
			get
			{
				return (PersonalizationStateInfo)this._values[index];
			}
		}

		// Token: 0x060045E0 RID: 17888 RVA: 0x000E6468 File Offset: 0x000E4668
		public void Add(PersonalizationStateInfo data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			UserPersonalizationStateInfo userPersonalizationStateInfo = data as UserPersonalizationStateInfo;
			PersonalizationStateInfoCollection.Key key;
			if (userPersonalizationStateInfo != null)
			{
				key = new PersonalizationStateInfoCollection.Key(userPersonalizationStateInfo.Path, userPersonalizationStateInfo.Username);
			}
			else
			{
				key = new PersonalizationStateInfoCollection.Key(data.Path, null);
			}
			if (!this._indices.ContainsKey(key))
			{
				int num = this._values.Add(data);
				try
				{
					this._indices.Add(key, num);
				}
				catch
				{
					this._values.RemoveAt(num);
					throw;
				}
				return;
			}
			if (userPersonalizationStateInfo != null)
			{
				throw new ArgumentException(SR.GetString("PersonalizationStateInfoCollection_CouldNotAddUserStateInfo", new object[]
				{
					key.Path,
					key.Username
				}));
			}
			throw new ArgumentException(SR.GetString("PersonalizationStateInfoCollection_CouldNotAddSharedStateInfo", new object[]
			{
				key.Path
			}));
		}

		// Token: 0x060045E1 RID: 17889 RVA: 0x000E6544 File Offset: 0x000E4744
		public void Clear()
		{
			this._values.Clear();
			this._indices.Clear();
		}

		// Token: 0x060045E2 RID: 17890 RVA: 0x000E655C File Offset: 0x000E475C
		public void CopyTo(PersonalizationStateInfo[] array, int index)
		{
			this._values.CopyTo(array, index);
		}

		// Token: 0x060045E3 RID: 17891 RVA: 0x000E656B File Offset: 0x000E476B
		public IEnumerator GetEnumerator()
		{
			return this._values.GetEnumerator();
		}

		// Token: 0x17001498 RID: 5272
		// (get) Token: 0x060045E4 RID: 17892 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060045E5 RID: 17893 RVA: 0x000E6578 File Offset: 0x000E4778
		public void Remove(string path, string username)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			PersonalizationStateInfoCollection.Key key = new PersonalizationStateInfoCollection.Key(path, username);
			int num;
			if (!this._indices.TryGetValue(key, out num))
			{
				return;
			}
			this._indices.Remove(key);
			try
			{
				this._values.RemoveAt(num);
			}
			catch
			{
				this._indices.Add(key, num);
				throw;
			}
			ArrayList arrayList = new ArrayList();
			foreach (KeyValuePair<PersonalizationStateInfoCollection.Key, int> keyValuePair in this._indices)
			{
				if (keyValuePair.Value > num)
				{
					arrayList.Add(keyValuePair.Key);
				}
			}
			foreach (object obj in arrayList)
			{
				PersonalizationStateInfoCollection.Key key2 = (PersonalizationStateInfoCollection.Key)obj;
				this._indices[key2] = this._indices[key2] - 1;
			}
		}

		// Token: 0x060045E6 RID: 17894 RVA: 0x000E66A4 File Offset: 0x000E48A4
		public void SetReadOnly()
		{
			if (this._readOnly)
			{
				return;
			}
			this._readOnly = true;
			this._values = ArrayList.ReadOnly(this._values);
		}

		// Token: 0x17001499 RID: 5273
		// (get) Token: 0x060045E7 RID: 17895 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060045E8 RID: 17896 RVA: 0x000E655C File Offset: 0x000E475C
		void ICollection.CopyTo(Array array, int index)
		{
			this._values.CopyTo(array, index);
		}

		// Token: 0x04002683 RID: 9859
		private Dictionary<PersonalizationStateInfoCollection.Key, int> _indices;

		// Token: 0x04002684 RID: 9860
		private bool _readOnly;

		// Token: 0x04002685 RID: 9861
		private ArrayList _values;

		// Token: 0x020009EE RID: 2542
		[Serializable]
		private sealed class Key
		{
			// Token: 0x06006D1B RID: 27931 RVA: 0x001868EC File Offset: 0x00184AEC
			internal Key(string path, string username)
			{
				this.Path = path;
				this.Username = username;
			}

			// Token: 0x04003A21 RID: 14881
			public string Path;

			// Token: 0x04003A22 RID: 14882
			public string Username;
		}

		// Token: 0x020009EF RID: 2543
		[Serializable]
		private sealed class KeyComparer : IEqualityComparer<PersonalizationStateInfoCollection.Key>
		{
			// Token: 0x06006D1C RID: 27932 RVA: 0x00186902 File Offset: 0x00184B02
			bool IEqualityComparer<PersonalizationStateInfoCollection.Key>.Equals(PersonalizationStateInfoCollection.Key x, PersonalizationStateInfoCollection.Key y)
			{
				return this.Compare(x, y) == 0;
			}

			// Token: 0x06006D1D RID: 27933 RVA: 0x00186910 File Offset: 0x00184B10
			int IEqualityComparer<PersonalizationStateInfoCollection.Key>.GetHashCode(PersonalizationStateInfoCollection.Key key)
			{
				if (key == null)
				{
					return 0;
				}
				int hashCode = key.Path.ToLowerInvariant().GetHashCode();
				int h = 0;
				if (key.Username != null)
				{
					h = key.Username.ToLowerInvariant().GetHashCode();
				}
				return HashCodeCombiner.CombineHashCodes(hashCode, h);
			}

			// Token: 0x06006D1E RID: 27934 RVA: 0x00186958 File Offset: 0x00184B58
			private int Compare(PersonalizationStateInfoCollection.Key x, PersonalizationStateInfoCollection.Key y)
			{
				if (x == null && y == null)
				{
					return 0;
				}
				if (x == null)
				{
					return -1;
				}
				if (y == null)
				{
					return 1;
				}
				int num = string.Compare(x.Path, y.Path, StringComparison.OrdinalIgnoreCase);
				if (num != 0)
				{
					return num;
				}
				return string.Compare(x.Username, y.Username, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x04003A23 RID: 14883
			internal static readonly IEqualityComparer<PersonalizationStateInfoCollection.Key> Default = new PersonalizationStateInfoCollection.KeyComparer();
		}
	}
}
