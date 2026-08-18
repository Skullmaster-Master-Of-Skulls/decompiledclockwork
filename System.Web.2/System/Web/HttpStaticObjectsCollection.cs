using System;
using System.Collections;
using System.IO;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000BC RID: 188
	public sealed class HttpStaticObjectsCollection : ICollection, IEnumerable
	{
		// Token: 0x06000D21 RID: 3361 RVA: 0x00024D05 File Offset: 0x00022F05
		internal void Add(string name, Type t, bool lateBound)
		{
			this._objects.Add(name, new HttpStaticObjectsEntry(name, t, lateBound));
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x00024D1B File Offset: 0x00022F1B
		internal IDictionary Objects
		{
			get
			{
				return this._objects;
			}
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00024D24 File Offset: 0x00022F24
		internal HttpStaticObjectsCollection Clone()
		{
			HttpStaticObjectsCollection httpStaticObjectsCollection = new HttpStaticObjectsCollection();
			IDictionaryEnumerator enumerator = this._objects.GetEnumerator();
			while (enumerator.MoveNext())
			{
				HttpStaticObjectsEntry httpStaticObjectsEntry = (HttpStaticObjectsEntry)enumerator.Value;
				httpStaticObjectsCollection.Add(httpStaticObjectsEntry.Name, httpStaticObjectsEntry.ObjectType, httpStaticObjectsEntry.LateBound);
			}
			return httpStaticObjectsCollection;
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00024D74 File Offset: 0x00022F74
		internal int GetInstanceCount()
		{
			int num = 0;
			IDictionaryEnumerator enumerator = this._objects.GetEnumerator();
			while (enumerator.MoveNext())
			{
				HttpStaticObjectsEntry httpStaticObjectsEntry = (HttpStaticObjectsEntry)enumerator.Value;
				if (httpStaticObjectsEntry.HasInstance)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x00024DB2 File Offset: 0x00022FB2
		public bool NeverAccessed
		{
			get
			{
				return this.GetInstanceCount() == 0;
			}
		}

		// Token: 0x170004BE RID: 1214
		public object this[string name]
		{
			get
			{
				HttpStaticObjectsEntry httpStaticObjectsEntry = (HttpStaticObjectsEntry)this._objects[name];
				if (httpStaticObjectsEntry == null)
				{
					return null;
				}
				return httpStaticObjectsEntry.Instance;
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00024DEA File Offset: 0x00022FEA
		public object GetObject(string name)
		{
			return this[name];
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00024DF3 File Offset: 0x00022FF3
		public int Count
		{
			get
			{
				return this._objects.Count;
			}
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00024E00 File Offset: 0x00023000
		public IEnumerator GetEnumerator()
		{
			return new HttpStaticObjectsEnumerator(this._objects.GetEnumerator());
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00024E14 File Offset: 0x00023014
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x000097B7 File Offset: 0x000079B7
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00024E44 File Offset: 0x00023044
		public void Serialize(BinaryWriter writer)
		{
			writer.Write(this.Count);
			IDictionaryEnumerator enumerator = this._objects.GetEnumerator();
			while (enumerator.MoveNext())
			{
				HttpStaticObjectsEntry httpStaticObjectsEntry = (HttpStaticObjectsEntry)enumerator.Value;
				writer.Write(httpStaticObjectsEntry.Name);
				bool hasInstance = httpStaticObjectsEntry.HasInstance;
				writer.Write(hasInstance);
				if (hasInstance)
				{
					AltSerialization.WriteValueToStream(httpStaticObjectsEntry.Instance, writer);
				}
				else
				{
					writer.Write(httpStaticObjectsEntry.ObjectType.FullName);
					writer.Write(httpStaticObjectsEntry.LateBound);
				}
			}
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00024EC8 File Offset: 0x000230C8
		public static HttpStaticObjectsCollection Deserialize(BinaryReader reader)
		{
			HttpStaticObjectsCollection httpStaticObjectsCollection = new HttpStaticObjectsCollection();
			int num = reader.ReadInt32();
			while (num-- > 0)
			{
				string text = reader.ReadString();
				bool flag = reader.ReadBoolean();
				HttpStaticObjectsEntry value;
				if (flag)
				{
					object instance = AltSerialization.ReadValueFromStream(reader);
					value = new HttpStaticObjectsEntry(text, instance, 0);
				}
				else
				{
					string typeName = reader.ReadString();
					bool lateBound = reader.ReadBoolean();
					value = new HttpStaticObjectsEntry(text, Type.GetType(typeName), lateBound);
				}
				httpStaticObjectsCollection._objects.Add(text, value);
			}
			return httpStaticObjectsCollection;
		}

		// Token: 0x040004E3 RID: 1251
		private IDictionary _objects = new Hashtable(StringComparer.OrdinalIgnoreCase);
	}
}
