using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading;

namespace System.Data.Common
{
	// Token: 0x020002E7 RID: 743
	public class DbConnectionStringBuilder : IDictionary, ICollection, IEnumerable, ICustomTypeDescriptor
	{
		// Token: 0x06002EEC RID: 12012 RVA: 0x00129814 File Offset: 0x00128C14
		public DbConnectionStringBuilder()
		{
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x0012984C File Offset: 0x00128C4C
		public DbConnectionStringBuilder(bool useOdbcRules)
		{
			this.UseOdbcRules = useOdbcRules;
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06002EEE RID: 12014 RVA: 0x00129888 File Offset: 0x00128C88
		private ICollection Collection
		{
			get
			{
				return this.CurrentValues;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06002EEF RID: 12015 RVA: 0x0012989C File Offset: 0x00128C9C
		private IDictionary Dictionary
		{
			get
			{
				return this.CurrentValues;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06002EF0 RID: 12016 RVA: 0x001298B0 File Offset: 0x00128CB0
		private Dictionary<string, object> CurrentValues
		{
			get
			{
				Dictionary<string, object> dictionary = this._currentValues;
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
					this._currentValues = dictionary;
				}
				return dictionary;
			}
		}

		// Token: 0x1700079F RID: 1951
		object IDictionary.this[object keyword]
		{
			get
			{
				return this[this.ObjectToString(keyword)];
			}
			set
			{
				this[this.ObjectToString(keyword)] = value;
			}
		}

		// Token: 0x170007A0 RID: 1952
		[Browsable(false)]
		public virtual object this[string keyword]
		{
			get
			{
				Bid.Trace("<comm.DbConnectionStringBuilder.get_Item|API> %d#, keyword='%ls'\n", this.ObjectID, keyword);
				ADP.CheckArgumentNull(keyword, "keyword");
				object result;
				if (this.CurrentValues.TryGetValue(keyword, out result))
				{
					return result;
				}
				throw ADP.KeywordNotSupported(keyword);
			}
			set
			{
				ADP.CheckArgumentNull(keyword, "keyword");
				bool flag;
				if (value != null)
				{
					string value2 = DbConnectionStringBuilderUtil.ConvertToString(value);
					DbConnectionOptions.ValidateKeyValuePair(keyword, value2);
					flag = this.CurrentValues.ContainsKey(keyword);
					this.CurrentValues[keyword] = value2;
				}
				else
				{
					flag = this.Remove(keyword);
				}
				this._connectionString = null;
				if (flag)
				{
					this._propertyDescriptors = null;
				}
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06002EF5 RID: 12021 RVA: 0x001299B8 File Offset: 0x00128DB8
		// (set) Token: 0x06002EF6 RID: 12022 RVA: 0x001299CC File Offset: 0x00128DCC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignOnly(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool BrowsableConnectionString
		{
			get
			{
				return this._browsableConnectionString;
			}
			set
			{
				this._browsableConnectionString = value;
				this._propertyDescriptors = null;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06002EF7 RID: 12023 RVA: 0x001299E8 File Offset: 0x00128DE8
		// (set) Token: 0x06002EF8 RID: 12024 RVA: 0x00129AA8 File Offset: 0x00128EA8
		[ResCategory("DataCategory_Data")]
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("DbConnectionString_ConnectionString")]
		public string ConnectionString
		{
			get
			{
				Bid.Trace("<comm.DbConnectionStringBuilder.get_ConnectionString|API> %d#\n", this.ObjectID);
				string text = this._connectionString;
				if (text == null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (object obj in this.Keys)
					{
						string keyword = (string)obj;
						object value;
						if (this.ShouldSerialize(keyword) && this.TryGetValue(keyword, out value))
						{
							string value2 = this.ConvertValueToString(value);
							DbConnectionStringBuilder.AppendKeyValuePair(stringBuilder, keyword, value2, this.UseOdbcRules);
						}
					}
					text = stringBuilder.ToString();
					this._connectionString = text;
				}
				return text;
			}
			set
			{
				Bid.Trace("<comm.DbConnectionStringBuilder.set_ConnectionString|API> %d#\n", this.ObjectID);
				DbConnectionOptions dbConnectionOptions = new DbConnectionOptions(value, null, this.UseOdbcRules);
				string connectionString = this.ConnectionString;
				this.Clear();
				try
				{
					for (NameValuePair nameValuePair = dbConnectionOptions.KeyChain; nameValuePair != null; nameValuePair = nameValuePair.Next)
					{
						if (nameValuePair.Value != null)
						{
							this[nameValuePair.Name] = nameValuePair.Value;
						}
						else
						{
							this.Remove(nameValuePair.Name);
						}
					}
					this._connectionString = null;
				}
				catch (ArgumentException)
				{
					this.ConnectionString = connectionString;
					this._connectionString = connectionString;
					throw;
				}
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06002EF9 RID: 12025 RVA: 0x00129B54 File Offset: 0x00128F54
		[Browsable(false)]
		public virtual int Count
		{
			get
			{
				return this.CurrentValues.Count;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06002EFA RID: 12026 RVA: 0x00129B6C File Offset: 0x00128F6C
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06002EFB RID: 12027 RVA: 0x00129B7C File Offset: 0x00128F7C
		[Browsable(false)]
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06002EFC RID: 12028 RVA: 0x00129B8C File Offset: 0x00128F8C
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.Collection.IsSynchronized;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06002EFD RID: 12029 RVA: 0x00129BA4 File Offset: 0x00128FA4
		[Browsable(false)]
		public virtual ICollection Keys
		{
			get
			{
				Bid.Trace("<comm.DbConnectionStringBuilder.Keys|API> %d#\n", this.ObjectID);
				return this.Dictionary.Keys;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06002EFE RID: 12030 RVA: 0x00129BCC File Offset: 0x00128FCC
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06002EFF RID: 12031 RVA: 0x00129BE0 File Offset: 0x00128FE0
		object ICollection.SyncRoot
		{
			get
			{
				return this.Collection.SyncRoot;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06002F00 RID: 12032 RVA: 0x00129BF8 File Offset: 0x00128FF8
		[Browsable(false)]
		public virtual ICollection Values
		{
			get
			{
				Bid.Trace("<comm.DbConnectionStringBuilder.Values|API> %d#\n", this.ObjectID);
				ICollection<string> collection = (ICollection<string>)this.Keys;
				IEnumerator<string> enumerator = collection.GetEnumerator();
				object[] array = new object[collection.Count];
				for (int i = 0; i < array.Length; i++)
				{
					enumerator.MoveNext();
					array[i] = this[enumerator.Current];
				}
				return new ReadOnlyCollection<object>(array);
			}
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x00129C60 File Offset: 0x00129060
		internal virtual string ConvertValueToString(object value)
		{
			if (value != null)
			{
				return Convert.ToString(value, CultureInfo.InvariantCulture);
			}
			return null;
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x00129C80 File Offset: 0x00129080
		void IDictionary.Add(object keyword, object value)
		{
			this.Add(this.ObjectToString(keyword), value);
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x00129C9C File Offset: 0x0012909C
		public void Add(string keyword, object value)
		{
			this[keyword] = value;
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x00129CB4 File Offset: 0x001290B4
		public static void AppendKeyValuePair(StringBuilder builder, string keyword, string value)
		{
			DbConnectionOptions.AppendKeyValuePairBuilder(builder, keyword, value, false);
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x00129CCC File Offset: 0x001290CC
		public static void AppendKeyValuePair(StringBuilder builder, string keyword, string value, bool useOdbcRules)
		{
			DbConnectionOptions.AppendKeyValuePairBuilder(builder, keyword, value, useOdbcRules);
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x00129CE4 File Offset: 0x001290E4
		public virtual void Clear()
		{
			Bid.Trace("<comm.DbConnectionStringBuilder.Clear|API>\n");
			this._connectionString = "";
			this._propertyDescriptors = null;
			this.CurrentValues.Clear();
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x00129D18 File Offset: 0x00129118
		protected internal void ClearPropertyDescriptors()
		{
			this._propertyDescriptors = null;
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x00129D2C File Offset: 0x0012912C
		bool IDictionary.Contains(object keyword)
		{
			return this.ContainsKey(this.ObjectToString(keyword));
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x00129D48 File Offset: 0x00129148
		public virtual bool ContainsKey(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			return this.CurrentValues.ContainsKey(keyword);
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x00129D6C File Offset: 0x0012916C
		void ICollection.CopyTo(Array array, int index)
		{
			Bid.Trace("<comm.DbConnectionStringBuilder.ICollection.CopyTo|API> %d#\n", this.ObjectID);
			this.Collection.CopyTo(array, index);
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x00129D98 File Offset: 0x00129198
		public virtual bool EquivalentTo(DbConnectionStringBuilder connectionStringBuilder)
		{
			ADP.CheckArgumentNull(connectionStringBuilder, "connectionStringBuilder");
			Bid.Trace("<comm.DbConnectionStringBuilder.EquivalentTo|API> %d#, connectionStringBuilder=%d#\n", this.ObjectID, connectionStringBuilder.ObjectID);
			if (base.GetType() != connectionStringBuilder.GetType() || this.CurrentValues.Count != connectionStringBuilder.CurrentValues.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, object> keyValuePair in this.CurrentValues)
			{
				object obj;
				if (!connectionStringBuilder.CurrentValues.TryGetValue(keyValuePair.Key, out obj) || !keyValuePair.Value.Equals(obj))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x00129E6C File Offset: 0x0012926C
		IEnumerator IEnumerable.GetEnumerator()
		{
			Bid.Trace("<comm.DbConnectionStringBuilder.IEnumerable.GetEnumerator|API> %d#\n", this.ObjectID);
			return this.Collection.GetEnumerator();
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x00129E94 File Offset: 0x00129294
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			Bid.Trace("<comm.DbConnectionStringBuilder.IDictionary.GetEnumerator|API> %d#\n", this.ObjectID);
			return this.Dictionary.GetEnumerator();
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x00129EBC File Offset: 0x001292BC
		private string ObjectToString(object keyword)
		{
			string result;
			try
			{
				result = (string)keyword;
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException("keyword", "not a string");
			}
			return result;
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x00129F04 File Offset: 0x00129304
		void IDictionary.Remove(object keyword)
		{
			this.Remove(this.ObjectToString(keyword));
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x00129F20 File Offset: 0x00129320
		public virtual bool Remove(string keyword)
		{
			Bid.Trace("<comm.DbConnectionStringBuilder.Remove|API> %d#, keyword='%ls'\n", this.ObjectID, keyword);
			ADP.CheckArgumentNull(keyword, "keyword");
			if (this.CurrentValues.Remove(keyword))
			{
				this._connectionString = null;
				this._propertyDescriptors = null;
				return true;
			}
			return false;
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x00129F68 File Offset: 0x00129368
		public virtual bool ShouldSerialize(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			return this.CurrentValues.ContainsKey(keyword);
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x00129F8C File Offset: 0x0012938C
		public override string ToString()
		{
			return this.ConnectionString;
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x00129FA0 File Offset: 0x001293A0
		public virtual bool TryGetValue(string keyword, out object value)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			return this.CurrentValues.TryGetValue(keyword, out value);
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x00129FC8 File Offset: 0x001293C8
		internal Attribute[] GetAttributesFromCollection(AttributeCollection collection)
		{
			Attribute[] array = new Attribute[collection.Count];
			collection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x00129FEC File Offset: 0x001293EC
		private PropertyDescriptorCollection GetProperties()
		{
			PropertyDescriptorCollection propertyDescriptorCollection = this._propertyDescriptors;
			if (propertyDescriptorCollection == null)
			{
				IntPtr intPtr;
				Bid.ScopeEnter(out intPtr, "<comm.DbConnectionStringBuilder.GetProperties|INFO> %d#", this.ObjectID);
				try
				{
					Hashtable hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
					this.GetProperties(hashtable);
					PropertyDescriptor[] array = new PropertyDescriptor[hashtable.Count];
					hashtable.Values.CopyTo(array, 0);
					propertyDescriptorCollection = new PropertyDescriptorCollection(array);
					this._propertyDescriptors = propertyDescriptorCollection;
				}
				finally
				{
					Bid.ScopeLeave(ref intPtr);
				}
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x0012A078 File Offset: 0x00129478
		protected virtual void GetProperties(Hashtable propertyDescriptors)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<comm.DbConnectionStringBuilder.GetProperties|API> %d#", this.ObjectID);
			try
			{
				foreach (object obj in TypeDescriptor.GetProperties(this, true))
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
					if ("ConnectionString" != propertyDescriptor.Name)
					{
						string displayName = propertyDescriptor.DisplayName;
						if (!propertyDescriptors.ContainsKey(displayName))
						{
							Attribute[] array = this.GetAttributesFromCollection(propertyDescriptor.Attributes);
							PropertyDescriptor value = new DbConnectionStringBuilderDescriptor(propertyDescriptor.Name, propertyDescriptor.ComponentType, propertyDescriptor.PropertyType, propertyDescriptor.IsReadOnly, array);
							propertyDescriptors[displayName] = value;
						}
					}
					else if (this.BrowsableConnectionString)
					{
						propertyDescriptors["ConnectionString"] = propertyDescriptor;
					}
					else
					{
						propertyDescriptors.Remove("ConnectionString");
					}
				}
				if (!this.IsFixedSize)
				{
					Attribute[] array = null;
					foreach (object obj2 in this.Keys)
					{
						string text = (string)obj2;
						if (!propertyDescriptors.ContainsKey(text))
						{
							object obj3 = this[text];
							Type type;
							if (obj3 != null)
							{
								type = obj3.GetType();
								if (typeof(string) == type)
								{
									int num;
									bool flag;
									if (int.TryParse((string)obj3, out num))
									{
										type = typeof(int);
									}
									else if (bool.TryParse((string)obj3, out flag))
									{
										type = typeof(bool);
									}
								}
							}
							else
							{
								type = typeof(string);
							}
							Attribute[] attributes = array;
							if (StringComparer.OrdinalIgnoreCase.Equals("Password", text) || StringComparer.OrdinalIgnoreCase.Equals("pwd", text))
							{
								attributes = new Attribute[]
								{
									BrowsableAttribute.Yes,
									PasswordPropertyTextAttribute.Yes,
									new ResCategoryAttribute("DataCategory_Security"),
									RefreshPropertiesAttribute.All
								};
							}
							else if (array == null)
							{
								array = new Attribute[]
								{
									BrowsableAttribute.Yes,
									RefreshPropertiesAttribute.All
								};
								attributes = array;
							}
							PropertyDescriptor value2 = new DbConnectionStringBuilderDescriptor(text, base.GetType(), type, false, attributes);
							propertyDescriptors[text] = value2;
						}
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x0012A304 File Offset: 0x00129704
		private PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = this.GetProperties();
			if (attributes == null || attributes.Length == 0)
			{
				return properties;
			}
			PropertyDescriptor[] array = new PropertyDescriptor[properties.Count];
			int num = 0;
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				bool flag = true;
				foreach (Attribute attribute in attributes)
				{
					Attribute attribute2 = propertyDescriptor.Attributes[attribute.GetType()];
					if ((attribute2 == null && !attribute.IsDefaultAttribute()) || !attribute2.Match(attribute))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					array[num] = propertyDescriptor;
					num++;
				}
			}
			PropertyDescriptor[] array2 = new PropertyDescriptor[num];
			Array.Copy(array, array2, num);
			return new PropertyDescriptorCollection(array2);
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x0012A3F4 File Offset: 0x001297F4
		string ICustomTypeDescriptor.GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x0012A408 File Offset: 0x00129808
		string ICustomTypeDescriptor.GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x0012A41C File Offset: 0x0012981C
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x0012A430 File Offset: 0x00129830
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x0012A448 File Offset: 0x00129848
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x0012A45C File Offset: 0x0012985C
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x0012A470 File Offset: 0x00129870
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return this.GetProperties();
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x0012A484 File Offset: 0x00129884
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			return this.GetProperties(attributes);
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x0012A498 File Offset: 0x00129898
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x0012A4AC File Offset: 0x001298AC
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x0012A4C0 File Offset: 0x001298C0
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x0012A4D8 File Offset: 0x001298D8
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04001CD9 RID: 7385
		private Dictionary<string, object> _currentValues;

		// Token: 0x04001CDA RID: 7386
		private string _connectionString = "";

		// Token: 0x04001CDB RID: 7387
		private PropertyDescriptorCollection _propertyDescriptors;

		// Token: 0x04001CDC RID: 7388
		private bool _browsableConnectionString = true;

		// Token: 0x04001CDD RID: 7389
		private readonly bool UseOdbcRules;

		// Token: 0x04001CDE RID: 7390
		private static int _objectTypeCount;

		// Token: 0x04001CDF RID: 7391
		internal readonly int _objectID = Interlocked.Increment(ref DbConnectionStringBuilder._objectTypeCount);
	}
}
