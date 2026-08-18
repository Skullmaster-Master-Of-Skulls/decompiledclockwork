using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;

namespace log4net.Util
{
	// Token: 0x02000110 RID: 272
	[Serializable]
	public class ReadOnlyPropertiesDictionary : ISerializable, IDictionary, ICollection, IEnumerable
	{
		// Token: 0x060007DA RID: 2010 RVA: 0x00018B16 File Offset: 0x00016D16
		public ReadOnlyPropertiesDictionary()
		{
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00018B2C File Offset: 0x00016D2C
		public ReadOnlyPropertiesDictionary(ReadOnlyPropertiesDictionary propertiesDictionary)
		{
			foreach (object obj in ((IEnumerable)propertiesDictionary))
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.InnerHashtable.Add(dictionaryEntry.Key, dictionaryEntry.Value);
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00018BA4 File Offset: 0x00016DA4
		protected ReadOnlyPropertiesDictionary(SerializationInfo info, StreamingContext context)
		{
			foreach (SerializationEntry serializationEntry in info)
			{
				this.InnerHashtable[XmlConvert.DecodeName(serializationEntry.Name)] = serializationEntry.Value;
			}
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00018BF8 File Offset: 0x00016DF8
		public string[] GetKeys()
		{
			string[] array = new string[this.InnerHashtable.Count];
			this.InnerHashtable.Keys.CopyTo(array, 0);
			return array;
		}

		// Token: 0x170001A3 RID: 419
		public virtual object this[string key]
		{
			get
			{
				return this.InnerHashtable[key];
			}
			set
			{
				throw new NotSupportedException("This is a Read Only Dictionary and can not be modified");
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00018C43 File Offset: 0x00016E43
		public bool Contains(string key)
		{
			return this.InnerHashtable.Contains(key);
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x00018C51 File Offset: 0x00016E51
		protected Hashtable InnerHashtable
		{
			get
			{
				return this.m_hashtable;
			}
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00018C5C File Offset: 0x00016E5C
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			foreach (object obj in (this.InnerHashtable.Clone() as IDictionary))
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = dictionaryEntry.Key as string;
				object value = dictionaryEntry.Value;
				bool isSerializable = value.GetType().IsSerializable;
				if (text != null && value != null && isSerializable)
				{
					info.AddValue(XmlConvert.EncodeLocalName(text), value);
				}
			}
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00018CF8 File Offset: 0x00016EF8
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return this.InnerHashtable.GetEnumerator();
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00018D05 File Offset: 0x00016F05
		void IDictionary.Remove(object key)
		{
			throw new NotSupportedException("This is a Read Only Dictionary and can not be modified");
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00018D11 File Offset: 0x00016F11
		bool IDictionary.Contains(object key)
		{
			return this.InnerHashtable.Contains(key);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00018D1F File Offset: 0x00016F1F
		public virtual void Clear()
		{
			throw new NotSupportedException("This is a Read Only Dictionary and can not be modified");
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00018D2B File Offset: 0x00016F2B
		void IDictionary.Add(object key, object value)
		{
			throw new NotSupportedException("This is a Read Only Dictionary and can not be modified");
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00018D37 File Offset: 0x00016F37
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001A6 RID: 422
		object IDictionary.this[object key]
		{
			get
			{
				if (!(key is string))
				{
					throw new ArgumentException("key must be a string");
				}
				return this.InnerHashtable[key];
			}
			set
			{
				throw new NotSupportedException("This is a Read Only Dictionary and can not be modified");
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x00018D67 File Offset: 0x00016F67
		ICollection IDictionary.Values
		{
			get
			{
				return this.InnerHashtable.Values;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x00018D74 File Offset: 0x00016F74
		ICollection IDictionary.Keys
		{
			get
			{
				return this.InnerHashtable.Keys;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x00018D81 File Offset: 0x00016F81
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this.InnerHashtable.IsFixedSize;
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00018D8E File Offset: 0x00016F8E
		void ICollection.CopyTo(Array array, int index)
		{
			this.InnerHashtable.CopyTo(array, index);
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x00018D9D File Offset: 0x00016F9D
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerHashtable.IsSynchronized;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x00018DAA File Offset: 0x00016FAA
		public int Count
		{
			get
			{
				return this.InnerHashtable.Count;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x00018DB7 File Offset: 0x00016FB7
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerHashtable.SyncRoot;
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00018DC4 File Offset: 0x00016FC4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.InnerHashtable).GetEnumerator();
		}

		// Token: 0x040002ED RID: 749
		private readonly Hashtable m_hashtable = new Hashtable();
	}
}
