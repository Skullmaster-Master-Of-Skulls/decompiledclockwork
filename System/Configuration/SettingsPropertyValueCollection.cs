using System;
using System.Collections;

namespace System.Configuration
{
	// Token: 0x02000719 RID: 1817
	public class SettingsPropertyValueCollection : ICloneable, ICollection, IEnumerable
	{
		// Token: 0x060037B1 RID: 14257 RVA: 0x000EBFC4 File Offset: 0x000EAFC4
		public SettingsPropertyValueCollection()
		{
			this._Indices = new Hashtable(10, CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
			this._Values = new ArrayList();
		}

		// Token: 0x060037B2 RID: 14258 RVA: 0x000EBFF0 File Offset: 0x000EAFF0
		public void Add(SettingsPropertyValue property)
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException();
			}
			int num = this._Values.Add(property);
			try
			{
				this._Indices.Add(property.Name, num);
			}
			catch (Exception)
			{
				this._Values.RemoveAt(num);
				throw;
			}
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x000EC050 File Offset: 0x000EB050
		public void Remove(string name)
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException();
			}
			object obj = this._Indices[name];
			if (obj == null || !(obj is int))
			{
				return;
			}
			int num = (int)obj;
			if (num >= this._Values.Count)
			{
				return;
			}
			this._Values.RemoveAt(num);
			this._Indices.Remove(name);
			ArrayList arrayList = new ArrayList();
			foreach (object obj2 in this._Indices)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
				if ((int)dictionaryEntry.Value > num)
				{
					arrayList.Add(dictionaryEntry.Key);
				}
			}
			foreach (object obj3 in arrayList)
			{
				string key = (string)obj3;
				this._Indices[key] = (int)this._Indices[key] - 1;
			}
		}

		// Token: 0x17000CEF RID: 3311
		public SettingsPropertyValue this[string name]
		{
			get
			{
				object obj = this._Indices[name];
				if (obj == null || !(obj is int))
				{
					return null;
				}
				int num = (int)obj;
				if (num >= this._Values.Count)
				{
					return null;
				}
				return (SettingsPropertyValue)this._Values[num];
			}
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x000EC1DB File Offset: 0x000EB1DB
		public IEnumerator GetEnumerator()
		{
			return this._Values.GetEnumerator();
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x000EC1E8 File Offset: 0x000EB1E8
		public object Clone()
		{
			return new SettingsPropertyValueCollection(this._Indices, this._Values);
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x000EC1FB File Offset: 0x000EB1FB
		public void SetReadOnly()
		{
			if (this._ReadOnly)
			{
				return;
			}
			this._ReadOnly = true;
			this._Values = ArrayList.ReadOnly(this._Values);
		}

		// Token: 0x060037B8 RID: 14264 RVA: 0x000EC21E File Offset: 0x000EB21E
		public void Clear()
		{
			this._Values.Clear();
			this._Indices.Clear();
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x060037B9 RID: 14265 RVA: 0x000EC236 File Offset: 0x000EB236
		public int Count
		{
			get
			{
				return this._Values.Count;
			}
		}

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x060037BA RID: 14266 RVA: 0x000EC243 File Offset: 0x000EB243
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x060037BB RID: 14267 RVA: 0x000EC246 File Offset: 0x000EB246
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060037BC RID: 14268 RVA: 0x000EC249 File Offset: 0x000EB249
		public void CopyTo(Array array, int index)
		{
			this._Values.CopyTo(array, index);
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x000EC258 File Offset: 0x000EB258
		private SettingsPropertyValueCollection(Hashtable indices, ArrayList values)
		{
			this._Indices = (Hashtable)indices.Clone();
			this._Values = (ArrayList)values.Clone();
		}

		// Token: 0x040031E9 RID: 12777
		private Hashtable _Indices;

		// Token: 0x040031EA RID: 12778
		private ArrayList _Values;

		// Token: 0x040031EB RID: 12779
		private bool _ReadOnly;
	}
}
