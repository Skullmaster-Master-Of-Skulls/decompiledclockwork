using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading;

namespace System.Data.Common
{
	// Token: 0x0200012D RID: 301
	public class DbConnectionStringBuilder : IDictionary, ICollection, IEnumerable, ICustomTypeDescriptor
	{
		// Token: 0x060013AC RID: 5036 RVA: 0x0023C808 File Offset: 0x0023BC08
		public DbConnectionStringBuilder()
		{
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0023C848 File Offset: 0x0023BC48
		public DbConnectionStringBuilder(bool useOdbcRules)
		{
			this.UseOdbcRules = useOdbcRules;
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x0023C888 File Offset: 0x0023BC88
		private ICollection Collection
		{
			get
			{
				return this.CurrentValues;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x0023C8A8 File Offset: 0x0023BCA8
		private IDictionary Dictionary
		{
			get
			{
				return this.CurrentValues;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x0023C8C8 File Offset: 0x0023BCC8
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

		// Token: 0x170002AB RID: 683
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

		// Token: 0x170002AC RID: 684
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

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x0023C9E8 File Offset: 0x0023BDE8
		// (set) Token: 0x060013B6 RID: 5046 RVA: 0x0023CA08 File Offset: 0x0023BE08
		[Browsable(false)]
		[DesignOnly(true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x0023CA28 File Offset: 0x0023BE28
		// (set) Token: 0x060013B8 RID: 5048 RVA: 0x0023CAF8 File Offset: 0x0023BEF8
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Data")]
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
						object obj2;
						if (this.ShouldSerialize(keyword) && this.TryGetValue(keyword, out obj2))
						{
							string value = (obj2 != null) ? Convert.ToString(obj2, CultureInfo.InvariantCulture) : null;
							DbConnectionStringBuilder.AppendKeyValuePair(stringBuilder, keyword, value, this.UseOdbcRules);
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

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x0023CBA8 File Offset: 0x0023BFA8
		[Browsable(false)]
		public virtual int Count
		{
			get
			{
				return this.CurrentValues.Count;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x0023CBC8 File Offset: 0x0023BFC8
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x0023CBD8 File Offset: 0x0023BFD8
		[Browsable(false)]
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x0023CBE8 File Offset: 0x0023BFE8
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.Collection.IsSynchronized;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x0023CC08 File Offset: 0x0023C008
		[Browsable(false)]
		public virtual ICollection Keys
		{
			get
			{
				Bid.Trace("<comm.DbConnectionStringBuilder.Keys|API> %d#\n", this.ObjectID);
				return this.Dictionary.Keys;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x0023CC38 File Offset: 0x0023C038
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x0023CC58 File Offset: 0x0023C058
		object ICollection.SyncRoot
		{
			get
			{
				return this.Collection.SyncRoot;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x0023CC78 File Offset: 0x0023C078
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

		// Token: 0x060013C1 RID: 5057 RVA: 0x0023CCE8 File Offset: 0x0023C0E8
		void IDictionary.Add(object keyword, object value)
		{
			this.Add(this.ObjectToString(keyword), value);
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0023CD08 File Offset: 0x0023C108
		public void Add(string keyword, object value)
		{
			this[keyword] = value;
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0023CD28 File Offset: 0x0023C128
		public static void AppendKeyValuePair(StringBuilder builder, string keyword, string value)
		{
			DbConnectionOptions.AppendKeyValuePairBuilder(builder, keyword, value, false);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0023CD48 File Offset: 0x0023C148
		public static void AppendKeyValuePair(StringBuilder builder, string keyword, string value, bool useOdbcRules)
		{
			DbConnectionOptions.AppendKeyValuePairBuilder(builder, keyword, value, useOdbcRules);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0023CD68 File Offset: 0x0023C168
		public virtual void Clear()
		{
			Bid.Trace("<comm.DbConnectionStringBuilder.Clear|API>\n");
			this._connectionString = "";
			this._propertyDescriptors = null;
			this.CurrentValues.Clear();
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0023CDA8 File Offset: 0x0023C1A8
		protected internal void ClearPropertyDescriptors()
		{
			this._propertyDescriptors = null;
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0023CDC8 File Offset: 0x0023C1C8
		bool IDictionary.Contains(object keyword)
		{
			return this.ContainsKey(this.ObjectToString(keyword));
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0023CDE8 File Offset: 0x0023C1E8
		public virtual bool ContainsKey(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			return this.CurrentValues.ContainsKey(keyword);
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0023CE18 File Offset: 0x0023C218
		void ICollection.CopyTo(Array array, int index)
		{
			Bid.Trace("<comm.DbConnectionStringBuilder.ICollection.CopyTo|API> %d#\n", this.ObjectID);
			this.Collection.CopyTo(array, index);
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0023CE48 File Offset: 0x0023C248
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

		// Token: 0x060013CB RID: 5067 RVA: 0x0023CF18 File Offset: 0x0023C318
		IEnumerator IEnumerable.GetEnumerator()
		{
			Bid.Trace("<comm.DbConnectionStringBuilder.IEnumerable.GetEnumerator|API> %d#\n", this.ObjectID);
			return this.Collection.GetEnumerator();
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0023CF48 File Offset: 0x0023C348
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			Bid.Trace("<comm.DbConnectionStringBuilder.IDictionary.GetEnumerator|API> %d#\n", this.ObjectID);
			return this.Dictionary.GetEnumerator();
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0023CF78 File Offset: 0x0023C378
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

		// Token: 0x060013CE RID: 5070 RVA: 0x0023CFC8 File Offset: 0x0023C3C8
		void IDictionary.Remove(object keyword)
		{
			this.Remove(this.ObjectToString(keyword));
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0023CFE8 File Offset: 0x0023C3E8
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

		// Token: 0x060013D0 RID: 5072 RVA: 0x0023D038 File Offset: 0x0023C438
		public virtual bool ShouldSerialize(string keyword)
		{
			Bid.Trace("<comm.DbConnectionStringBuilder.ShouldSerialize|API> keyword='%ls'\n", keyword);
			ADP.CheckArgumentNull(keyword, "keyword");
			return this.CurrentValues.ContainsKey(keyword);
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0023D068 File Offset: 0x0023C468
		public override string ToString()
		{
			return this.ConnectionString;
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0023D088 File Offset: 0x0023C488
		public virtual bool TryGetValue(string keyword, out object value)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			return this.CurrentValues.TryGetValue(keyword, out value);
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0023D0B8 File Offset: 0x0023C4B8
		internal Attribute[] GetAttributesFromCollection(AttributeCollection collection)
		{
			Attribute[] array = new Attribute[collection.Count];
			collection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0023D0E8 File Offset: 0x0023C4E8
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

		// Token: 0x060013D5 RID: 5077 RVA: 0x0023D178 File Offset: 0x0023C578
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

		// Token: 0x060013D6 RID: 5078 RVA: 0x0023D418 File Offset: 0x0023C818
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

		// Token: 0x060013D7 RID: 5079 RVA: 0x0023D518 File Offset: 0x0023C918
		string ICustomTypeDescriptor.GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0023D538 File Offset: 0x0023C938
		string ICustomTypeDescriptor.GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0023D558 File Offset: 0x0023C958
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0023D578 File Offset: 0x0023C978
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0023D598 File Offset: 0x0023C998
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0023D5B8 File Offset: 0x0023C9B8
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0023D5D8 File Offset: 0x0023C9D8
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return this.GetProperties();
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0023D5F8 File Offset: 0x0023C9F8
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			return this.GetProperties(attributes);
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0023D618 File Offset: 0x0023CA18
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0023D638 File Offset: 0x0023CA38
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0023D658 File Offset: 0x0023CA58
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0023D678 File Offset: 0x0023CA78
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04000C2D RID: 3117
		private Dictionary<string, object> _currentValues;

		// Token: 0x04000C2E RID: 3118
		private string _connectionString = "";

		// Token: 0x04000C2F RID: 3119
		private PropertyDescriptorCollection _propertyDescriptors;

		// Token: 0x04000C30 RID: 3120
		private bool _browsableConnectionString = true;

		// Token: 0x04000C31 RID: 3121
		private readonly bool UseOdbcRules;

		// Token: 0x04000C32 RID: 3122
		private static int _objectTypeCount;

		// Token: 0x04000C33 RID: 3123
		internal readonly int _objectID = Interlocked.Increment(ref DbConnectionStringBuilder._objectTypeCount);
	}
}
