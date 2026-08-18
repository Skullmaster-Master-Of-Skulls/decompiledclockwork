using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000005 RID: 5
	[DebuggerDisplay("Count = {Count}")]
	public abstract class ConfigurationElementCollectionBase<T> : ConfigurationElement, ICollection, IEnumerable<T>, IEnumerable where T : ConfigurationElement
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002C64 File Offset: 0x00001C64
		private string AddElementName
		{
			get
			{
				if (this._addElementName == null)
				{
					IAppHostCollectionSchema schema = this.Collection.Schema;
					string addElementNames = schema.AddElementNames;
					int num = addElementNames.IndexOf(',');
					if (num == -1)
					{
						this._addElementName = addElementNames;
					}
					else
					{
						this._addElementName = addElementNames.Substring(0, num);
					}
				}
				return this._addElementName;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002CB6 File Offset: 0x00001CB6
		public bool AllowsAdd
		{
			get
			{
				return !string.IsNullOrEmpty(this.AddElementName);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002CC8 File Offset: 0x00001CC8
		public bool AllowsClear
		{
			get
			{
				return !string.IsNullOrEmpty(this.ClearElementName);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002CDC File Offset: 0x00001CDC
		public bool AllowsRemove
		{
			get
			{
				IAppHostCollectionSchema schema = this.Collection.Schema;
				IAppHostElementSchema removeElementSchema = schema.RemoveElementSchema;
				return removeElementSchema != null && !string.IsNullOrEmpty(removeElementSchema.Name);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002D10 File Offset: 0x00001D10
		private string ClearElementName
		{
			get
			{
				if (this._clearElementName == null)
				{
					IAppHostCollectionSchema schema = this.Collection.Schema;
					if (schema.ClearElementSchema != null)
					{
						this._clearElementName = schema.ClearElementSchema.Name;
					}
				}
				return this._clearElementName;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002D50 File Offset: 0x00001D50
		private IAppHostElementCollection Collection
		{
			get
			{
				if (this._collection == null)
				{
					this._collection = base.AppHostElement.Collection;
				}
				return this._collection;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002D71 File Offset: 0x00001D71
		public int Count
		{
			get
			{
				if (this._elements != null)
				{
					return this.Elements.Count;
				}
				return (int)this.Collection.Count;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002DB4 File Offset: 0x00001DB4
		internal T FindElementWithCollectionKey(string collectionKey, object value)
		{
			IAppHostElement appHostElement = this.Collection.CreateNewElement(this.AddElementName);
			appHostElement.Properties[collectionKey].Value = value;
			IAppHostElement locatedElement = null;
			try
			{
				locatedElement = this.Collection[appHostElement];
			}
			catch (COMException)
			{
				return default(T);
			}
			if (locatedElement == null)
			{
				return default(T);
			}
			Predicate<T> match = (T listElement) => listElement.AppHostElement == locatedElement;
			T t;
			lock (this)
			{
				if (this._elements == null)
				{
					if (this._unpositionedElements == null)
					{
						this._unpositionedElements = new List<T>();
					}
					t = this._unpositionedElements.Find(match);
					if (t == null)
					{
						T t2 = this.CreateNewElement(locatedElement.Name);
						t2.Initialize(base.Configuration, locatedElement);
						this._unpositionedElements.Add(t2);
						t = t2;
					}
				}
				else
				{
					t = this._elements.Find(match);
				}
			}
			return t;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002F0C File Offset: 0x00001F0C
		private List<T> Elements
		{
			get
			{
				if (this._elements == null)
				{
					lock (this)
					{
						if (this._elements == null)
						{
							IAppHostElementCollection collection = this.Collection;
							uint count = collection.Count;
							List<T> list = new List<T>((int)(count + 1U));
							for (uint num = 0U; num < count; num += 1U)
							{
								IAppHostElement appHostElement = collection[num];
								T t = this.CreateNewElement(appHostElement.Name);
								t.Initialize(base.Configuration, appHostElement);
								Predicate<T> match = (T listElement) => listElement.AppHostElement == appHostElement;
								T t2 = (this._unpositionedElements != null) ? this._unpositionedElements.Find(match) : default(T);
								List<T> list2 = list;
								T item;
								if ((item = t2) == null)
								{
									item = t;
								}
								list2.Add(item);
							}
							this._unpositionedElements = null;
							this._elements = list;
						}
					}
				}
				return this._elements;
			}
		}

		// Token: 0x17000034 RID: 52
		public T this[int index]
		{
			get
			{
				return this.Elements[index];
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000052 RID: 82 RVA: 0x0000302C File Offset: 0x0000202C
		public new ConfigurationCollectionSchema Schema
		{
			get
			{
				if (this._schema == null)
				{
					IAppHostCollectionSchema schema = this.Collection.Schema;
					if (schema != null)
					{
						this._schema = new ConfigurationCollectionSchema(schema);
					}
				}
				return this._schema;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003064 File Offset: 0x00002064
		public T Add(T element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.SetDirty();
			this.Collection.AddElement(element.AppHostElement, -1);
			if (this._elements != null)
			{
				this.Elements.Add(element);
			}
			return element;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000030B8 File Offset: 0x000020B8
		public T AddAt(int index, T element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.SetDirty();
			this.Collection.AddElement(element.AppHostElement, index);
			if (this._elements != null)
			{
				this.Elements.Insert(index, element);
			}
			return element;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000310D File Offset: 0x0000210D
		public void Clear()
		{
			base.SetDirty();
			this.Collection.Clear();
			this._elements = null;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003127 File Offset: 0x00002127
		public T CreateElement()
		{
			return this.CreateElement(this.AddElementName);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003138 File Offset: 0x00002138
		public T CreateElement(string elementTagName)
		{
			IAppHostElement appHostElement = this.Collection.CreateNewElement(elementTagName);
			T result = this.CreateNewElement(elementTagName);
			result.Initialize(base.Configuration, appHostElement);
			return result;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000316F File Offset: 0x0000216F
		protected virtual T CreateNewElement(string elementTagName)
		{
			return (T)((object)Activator.CreateInstance(typeof(T), true));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003186 File Offset: 0x00002186
		public IEnumerator<T> GetEnumerator()
		{
			return this.Elements.GetEnumerator();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003198 File Offset: 0x00002198
		public int IndexOf(T element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return this.Elements.IndexOf(element);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000031BC File Offset: 0x000021BC
		public void Remove(T element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (element.Configuration != base.Configuration)
			{
				throw new InvalidOperationException(Resources.InvalidElementConfigurationObject);
			}
			int num = this.Elements.IndexOf(element);
			if (num == -1)
			{
				return;
			}
			this.RemoveAt(num);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003234 File Offset: 0x00002234
		public void RemoveAt(int index)
		{
			if (this._unpositionedElements != null)
			{
				IAppHostElement element = this.Collection[index];
				Predicate<T> match = (T listElement) => listElement.AppHostElement == element;
				this._unpositionedElements.RemoveAll(match);
			}
			this.Collection.DeleteElement(index);
			base.SetDirty();
			if (this._elements != null)
			{
				this.Elements.RemoveAt(index);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000032AB File Offset: 0x000022AB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000032B3 File Offset: 0x000022B3
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000032BB File Offset: 0x000022BB
		void ICollection.CopyTo(Array array, int index)
		{
			this.Elements.CopyTo((T[])array, index);
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000032CF File Offset: 0x000022CF
		bool ICollection.IsSynchronized
		{
			get
			{
				return ((ICollection)this.Elements).IsSynchronized;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000032DC File Offset: 0x000022DC
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this.Elements).SyncRoot;
			}
		}

		// Token: 0x04000011 RID: 17
		private string _addElementName;

		// Token: 0x04000012 RID: 18
		private string _clearElementName;

		// Token: 0x04000013 RID: 19
		private List<T> _elements;

		// Token: 0x04000014 RID: 20
		private List<T> _unpositionedElements;

		// Token: 0x04000015 RID: 21
		private IAppHostElementCollection _collection;

		// Token: 0x04000016 RID: 22
		private ConfigurationCollectionSchema _schema;
	}
}
