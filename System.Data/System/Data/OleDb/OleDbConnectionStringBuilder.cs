using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data.Common;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Text;

namespace System.Data.OleDb
{
	// Token: 0x0200021B RID: 539
	[TypeConverter(typeof(OleDbConnectionStringBuilder.OleDbConnectionStringBuilderConverter))]
	[RefreshProperties(RefreshProperties.All)]
	[DefaultProperty("Provider")]
	public sealed class OleDbConnectionStringBuilder : DbConnectionStringBuilder
	{
		// Token: 0x06001EBC RID: 7868 RVA: 0x00275B38 File Offset: 0x00274F38
		static OleDbConnectionStringBuilder()
		{
			string[] array = new string[5];
			array[2] = "Data Source";
			array[0] = "File Name";
			array[4] = "OLE DB Services";
			array[3] = "Persist Security Info";
			array[1] = "Provider";
			OleDbConnectionStringBuilder._validKeywords = array;
			OleDbConnectionStringBuilder._keywords = new Dictionary<string, OleDbConnectionStringBuilder.Keywords>(9, StringComparer.OrdinalIgnoreCase)
			{
				{
					"Data Source",
					OleDbConnectionStringBuilder.Keywords.DataSource
				},
				{
					"File Name",
					OleDbConnectionStringBuilder.Keywords.FileName
				},
				{
					"OLE DB Services",
					OleDbConnectionStringBuilder.Keywords.OleDbServices
				},
				{
					"Persist Security Info",
					OleDbConnectionStringBuilder.Keywords.PersistSecurityInfo
				},
				{
					"Provider",
					OleDbConnectionStringBuilder.Keywords.Provider
				}
			};
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x00275BD8 File Offset: 0x00274FD8
		public OleDbConnectionStringBuilder() : this(null)
		{
			this._knownKeywords = OleDbConnectionStringBuilder._validKeywords;
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x00275BF8 File Offset: 0x00274FF8
		public OleDbConnectionStringBuilder(string connectionString)
		{
			if (!ADP.IsEmpty(connectionString))
			{
				base.ConnectionString = connectionString;
			}
		}

		// Token: 0x17000430 RID: 1072
		public override object this[string keyword]
		{
			get
			{
				Bid.Trace("<comm.OleDbConnectionStringBuilder.get_Item|API> keyword='%ls'\n", keyword);
				ADP.CheckArgumentNull(keyword, "keyword");
				OleDbConnectionStringBuilder.Keywords index;
				object result;
				if (OleDbConnectionStringBuilder._keywords.TryGetValue(keyword, out index))
				{
					result = this.GetAt(index);
				}
				else if (!base.TryGetValue(keyword, out result))
				{
					Dictionary<string, OleDbPropertyInfo> providerInfo = this.GetProviderInfo(this.Provider);
					OleDbPropertyInfo oleDbPropertyInfo = providerInfo[keyword];
					result = oleDbPropertyInfo._defaultValue;
				}
				return result;
			}
			set
			{
				Bid.Trace("<comm.OleDbConnectionStringBuilder.set_Item|API> keyword='%ls'\n", keyword);
				if (value == null)
				{
					this.Remove(keyword);
					return;
				}
				ADP.CheckArgumentNull(keyword, "keyword");
				OleDbConnectionStringBuilder.Keywords keywords;
				if (!OleDbConnectionStringBuilder._keywords.TryGetValue(keyword, out keywords))
				{
					base[keyword] = value;
					this.ClearPropertyDescriptors();
					return;
				}
				switch (keywords)
				{
				case OleDbConnectionStringBuilder.Keywords.FileName:
					this.FileName = OleDbConnectionStringBuilder.ConvertToString(value);
					return;
				case OleDbConnectionStringBuilder.Keywords.Provider:
					this.Provider = OleDbConnectionStringBuilder.ConvertToString(value);
					return;
				case OleDbConnectionStringBuilder.Keywords.DataSource:
					this.DataSource = OleDbConnectionStringBuilder.ConvertToString(value);
					return;
				case OleDbConnectionStringBuilder.Keywords.PersistSecurityInfo:
					this.PersistSecurityInfo = OleDbConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case OleDbConnectionStringBuilder.Keywords.OleDbServices:
					this.OleDbServices = OleDbConnectionStringBuilder.ConvertToInt32(value);
					return;
				default:
					throw ADP.KeywordNotSupported(keyword);
				}
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x00275D78 File Offset: 0x00275178
		// (set) Token: 0x06001EC2 RID: 7874 RVA: 0x00275D98 File Offset: 0x00275198
		[ResDescription("DbConnectionString_DataSource")]
		[ResCategory("DataCategory_Source")]
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Data Source")]
		public string DataSource
		{
			get
			{
				return this._dataSource;
			}
			set
			{
				this.SetValue("Data Source", value);
				this._dataSource = value;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001EC3 RID: 7875 RVA: 0x00275DB8 File Offset: 0x002751B8
		// (set) Token: 0x06001EC4 RID: 7876 RVA: 0x00275DD8 File Offset: 0x002751D8
		[ResCategory("DataCategory_NamedConnectionString")]
		[Editor("System.Windows.Forms.Design.FileNameEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DisplayName("File Name")]
		[ResDescription("DbConnectionString_FileName")]
		[RefreshProperties(RefreshProperties.All)]
		public string FileName
		{
			get
			{
				return this._fileName;
			}
			set
			{
				this.SetValue("File Name", value);
				this._fileName = value;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x00275DF8 File Offset: 0x002751F8
		// (set) Token: 0x06001EC6 RID: 7878 RVA: 0x00275E18 File Offset: 0x00275218
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("DbConnectionString_OleDbServices")]
		[ResCategory("DataCategory_Pooling")]
		[DisplayName("OLE DB Services")]
		[TypeConverter(typeof(OleDbConnectionStringBuilder.OleDbServicesConverter))]
		public int OleDbServices
		{
			get
			{
				return this._oleDbServices;
			}
			set
			{
				this.SetValue("OLE DB Services", value);
				this._oleDbServices = value;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x00275E38 File Offset: 0x00275238
		// (set) Token: 0x06001EC8 RID: 7880 RVA: 0x00275E58 File Offset: 0x00275258
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Persist Security Info")]
		[ResCategory("DataCategory_Security")]
		[ResDescription("DbConnectionString_PersistSecurityInfo")]
		public bool PersistSecurityInfo
		{
			get
			{
				return this._persistSecurityInfo;
			}
			set
			{
				this.SetValue("Persist Security Info", value);
				this._persistSecurityInfo = value;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x00275E78 File Offset: 0x00275278
		// (set) Token: 0x06001ECA RID: 7882 RVA: 0x00275E98 File Offset: 0x00275298
		[DisplayName("Provider")]
		[TypeConverter(typeof(OleDbConnectionStringBuilder.OleDbProviderConverter))]
		[ResCategory("DataCategory_Source")]
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("DbConnectionString_Provider")]
		public string Provider
		{
			get
			{
				return this._provider;
			}
			set
			{
				this.SetValue("Provider", value);
				this._provider = value;
				this.RestartProvider();
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001ECB RID: 7883 RVA: 0x00275EC8 File Offset: 0x002752C8
		public override ICollection Keys
		{
			get
			{
				string[] array = this._knownKeywords;
				if (array == null)
				{
					Dictionary<string, OleDbPropertyInfo> providerInfo = this.GetProviderInfo(this.Provider);
					if (0 < providerInfo.Count)
					{
						array = new string[OleDbConnectionStringBuilder._validKeywords.Length + providerInfo.Count];
						OleDbConnectionStringBuilder._validKeywords.CopyTo(array, 0);
						providerInfo.Keys.CopyTo(array, OleDbConnectionStringBuilder._validKeywords.Length);
					}
					else
					{
						array = OleDbConnectionStringBuilder._validKeywords;
					}
					int num = 0;
					foreach (object obj in base.Keys)
					{
						string y = (string)obj;
						bool flag = true;
						foreach (string x in array)
						{
							if (StringComparer.OrdinalIgnoreCase.Equals(x, y))
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							num++;
						}
					}
					if (0 < num)
					{
						string[] array3 = new string[array.Length + num];
						array.CopyTo(array3, 0);
						int num2 = array.Length;
						foreach (object obj2 in base.Keys)
						{
							string text = (string)obj2;
							bool flag2 = true;
							foreach (string x2 in array)
							{
								if (StringComparer.OrdinalIgnoreCase.Equals(x2, text))
								{
									flag2 = false;
									break;
								}
							}
							if (flag2)
							{
								array3[num2++] = text;
							}
						}
						array = array3;
					}
					this._knownKeywords = array;
				}
				return new ReadOnlyCollection<string>(array);
			}
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x00276098 File Offset: 0x00275498
		public override bool ContainsKey(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			return OleDbConnectionStringBuilder._keywords.ContainsKey(keyword) || base.ContainsKey(keyword);
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x002760C8 File Offset: 0x002754C8
		private static bool ConvertToBoolean(object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToBoolean(value);
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x002760E8 File Offset: 0x002754E8
		private static int ConvertToInt32(object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToInt32(value);
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x00276108 File Offset: 0x00275508
		private static string ConvertToString(object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToString(value);
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x00276128 File Offset: 0x00275528
		public override void Clear()
		{
			base.Clear();
			for (int i = 0; i < OleDbConnectionStringBuilder._validKeywords.Length; i++)
			{
				this.Reset((OleDbConnectionStringBuilder.Keywords)i);
			}
			base.ClearPropertyDescriptors();
			this._knownKeywords = OleDbConnectionStringBuilder._validKeywords;
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x00276168 File Offset: 0x00275568
		private object GetAt(OleDbConnectionStringBuilder.Keywords index)
		{
			switch (index)
			{
			case OleDbConnectionStringBuilder.Keywords.FileName:
				return this.FileName;
			case OleDbConnectionStringBuilder.Keywords.Provider:
				return this.Provider;
			case OleDbConnectionStringBuilder.Keywords.DataSource:
				return this.DataSource;
			case OleDbConnectionStringBuilder.Keywords.PersistSecurityInfo:
				return this.PersistSecurityInfo;
			case OleDbConnectionStringBuilder.Keywords.OleDbServices:
				return this.OleDbServices;
			default:
				throw ADP.KeywordNotSupported(OleDbConnectionStringBuilder._validKeywords[(int)index]);
			}
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x002761D8 File Offset: 0x002755D8
		public override bool Remove(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			bool flag = base.Remove(keyword);
			OleDbConnectionStringBuilder.Keywords index;
			if (OleDbConnectionStringBuilder._keywords.TryGetValue(keyword, out index))
			{
				this.Reset(index);
			}
			else if (flag)
			{
				this.ClearPropertyDescriptors();
			}
			return flag;
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x00276228 File Offset: 0x00275628
		private void Reset(OleDbConnectionStringBuilder.Keywords index)
		{
			switch (index)
			{
			case OleDbConnectionStringBuilder.Keywords.FileName:
				this._fileName = "";
				this.RestartProvider();
				return;
			case OleDbConnectionStringBuilder.Keywords.Provider:
				this._provider = "";
				this.RestartProvider();
				return;
			case OleDbConnectionStringBuilder.Keywords.DataSource:
				this._dataSource = "";
				return;
			case OleDbConnectionStringBuilder.Keywords.PersistSecurityInfo:
				this._persistSecurityInfo = false;
				return;
			case OleDbConnectionStringBuilder.Keywords.OleDbServices:
				this._oleDbServices = -13;
				return;
			default:
				throw ADP.KeywordNotSupported(OleDbConnectionStringBuilder._validKeywords[(int)index]);
			}
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x002762A8 File Offset: 0x002756A8
		private new void ClearPropertyDescriptors()
		{
			base.ClearPropertyDescriptors();
			this._knownKeywords = null;
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x002762C8 File Offset: 0x002756C8
		private void RestartProvider()
		{
			this.ClearPropertyDescriptors();
			this._propertyInfo = null;
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x002762E8 File Offset: 0x002756E8
		private void SetValue(string keyword, bool value)
		{
			base[keyword] = value.ToString(null);
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x00276308 File Offset: 0x00275708
		private void SetValue(string keyword, int value)
		{
			base[keyword] = value.ToString(null);
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x00276328 File Offset: 0x00275728
		private void SetValue(string keyword, string value)
		{
			ADP.CheckArgumentNull(value, keyword);
			base[keyword] = value;
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x00276348 File Offset: 0x00275748
		public override bool TryGetValue(string keyword, out object value)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			OleDbConnectionStringBuilder.Keywords index;
			if (OleDbConnectionStringBuilder._keywords.TryGetValue(keyword, out index))
			{
				value = this.GetAt(index);
				return true;
			}
			if (base.TryGetValue(keyword, out value))
			{
				return true;
			}
			Dictionary<string, OleDbPropertyInfo> providerInfo = this.GetProviderInfo(this.Provider);
			OleDbPropertyInfo oleDbPropertyInfo;
			if (providerInfo.TryGetValue(keyword, out oleDbPropertyInfo))
			{
				value = oleDbPropertyInfo._defaultValue;
				return true;
			}
			return false;
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x002763B8 File Offset: 0x002757B8
		private Dictionary<string, OleDbPropertyInfo> GetProviderInfo(string provider)
		{
			Dictionary<string, OleDbPropertyInfo> dictionary = this._propertyInfo;
			if (dictionary == null)
			{
				dictionary = new Dictionary<string, OleDbPropertyInfo>(StringComparer.OrdinalIgnoreCase);
				if (!ADP.IsEmpty(provider))
				{
					Dictionary<string, OleDbPropertyInfo> dictionary2 = null;
					try
					{
						StringBuilder stringBuilder = new StringBuilder();
						DbConnectionStringBuilder.AppendKeyValuePair(stringBuilder, "Provider", provider);
						OleDbConnectionString oleDbConnectionString = new OleDbConnectionString(stringBuilder.ToString(), true);
						oleDbConnectionString.CreatePermissionSet().Demand();
						using (OleDbConnectionInternal oleDbConnectionInternal = new OleDbConnectionInternal(oleDbConnectionString, null))
						{
							dictionary2 = oleDbConnectionInternal.GetPropertyInfo(new Guid[]
							{
								OleDbPropertySetGuid.DBInitAll
							});
							foreach (KeyValuePair<string, OleDbPropertyInfo> keyValuePair in dictionary2)
							{
								OleDbPropertyInfo value = keyValuePair.Value;
								OleDbConnectionStringBuilder.Keywords keywords;
								if (!OleDbConnectionStringBuilder._keywords.TryGetValue(value._description, out keywords) && (!(OleDbPropertySetGuid.DBInit == value._propertySet) || (200 != value._propertyID && 60 != value._propertyID && 64 != value._propertyID)))
								{
									dictionary[value._description] = value;
								}
							}
							List<Guid> list = new List<Guid>();
							foreach (KeyValuePair<string, OleDbPropertyInfo> keyValuePair2 in dictionary2)
							{
								OleDbPropertyInfo value2 = keyValuePair2.Value;
								if (!list.Contains(value2._propertySet))
								{
									list.Add(value2._propertySet);
								}
							}
							Guid[] array = new Guid[list.Count];
							list.CopyTo(array, 0);
							using (PropertyIDSet propertyIDSet = new PropertyIDSet(array))
							{
								using (IDBPropertiesWrapper idbpropertiesWrapper = oleDbConnectionInternal.IDBProperties())
								{
									OleDbHResult oleDbHResult;
									using (DBPropSet dbpropSet = new DBPropSet(idbpropertiesWrapper.Value, propertyIDSet, ref oleDbHResult))
									{
										if (OleDbHResult.S_OK <= oleDbHResult)
										{
											int propertySetCount = dbpropSet.PropertySetCount;
											for (int i = 0; i < propertySetCount; i++)
											{
												Guid b;
												tagDBPROP[] propertySet = dbpropSet.GetPropertySet(i, out b);
												foreach (tagDBPROP tagDBPROP in propertySet)
												{
													foreach (KeyValuePair<string, OleDbPropertyInfo> keyValuePair3 in dictionary2)
													{
														OleDbPropertyInfo value3 = keyValuePair3.Value;
														if (value3._propertyID == tagDBPROP.dwPropertyID && value3._propertySet == b)
														{
															value3._defaultValue = tagDBPROP.vValue;
															if (value3._defaultValue == null)
															{
																if (typeof(string) == value3._type)
																{
																	value3._defaultValue = "";
																}
																else if (typeof(int) == value3._type)
																{
																	value3._defaultValue = 0;
																}
																else if (typeof(bool) == value3._type)
																{
																	value3._defaultValue = false;
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					catch (InvalidOperationException e)
					{
						ADP.TraceExceptionWithoutRethrow(e);
					}
					catch (OleDbException e2)
					{
						ADP.TraceExceptionWithoutRethrow(e2);
					}
					catch (SecurityException e3)
					{
						ADP.TraceExceptionWithoutRethrow(e3);
					}
				}
				this._propertyInfo = dictionary;
			}
			return dictionary;
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x002767E8 File Offset: 0x00275BE8
		protected override void GetProperties(Hashtable propertyDescriptors)
		{
			Dictionary<string, OleDbPropertyInfo> providerInfo = this.GetProviderInfo(this.Provider);
			if (0 < providerInfo.Count)
			{
				foreach (OleDbPropertyInfo oleDbPropertyInfo in providerInfo.Values)
				{
					OleDbConnectionStringBuilder.Keywords keywords;
					if (!OleDbConnectionStringBuilder._keywords.TryGetValue(oleDbPropertyInfo._description, out keywords))
					{
						bool isReadOnly = false;
						bool refreshOnChange = false;
						Attribute[] attributes;
						if (OleDbPropertySetGuid.DBInit == oleDbPropertyInfo._propertySet)
						{
							int propertyID = oleDbPropertyInfo._propertyID;
							if (propertyID <= 160)
							{
								switch (propertyID)
								{
								case 5:
								case 6:
								case 7:
								case 8:
								case 10:
									attributes = new Attribute[]
									{
										BrowsableAttribute.Yes,
										new ResCategoryAttribute("DataCategory_Security"),
										RefreshPropertiesAttribute.All
									};
									refreshOnChange = (7 == oleDbPropertyInfo._propertyID);
									goto IL_303;
								case 9:
									attributes = new Attribute[]
									{
										BrowsableAttribute.Yes,
										PasswordPropertyTextAttribute.Yes,
										new ResCategoryAttribute("DataCategory_Security"),
										RefreshPropertiesAttribute.All
									};
									isReadOnly = this.ContainsKey("Integrated Security");
									refreshOnChange = true;
									goto IL_303;
								case 11:
									goto IL_265;
								case 12:
									attributes = new Attribute[]
									{
										BrowsableAttribute.Yes,
										new ResCategoryAttribute("DataCategory_Security"),
										RefreshPropertiesAttribute.All
									};
									isReadOnly = this.ContainsKey("Integrated Security");
									refreshOnChange = true;
									goto IL_303;
								default:
									switch (propertyID)
									{
									case 61:
									case 63:
									case 65:
										goto IL_235;
									case 62:
										goto IL_111;
									case 64:
										goto IL_265;
									case 66:
										break;
									default:
										if (propertyID != 160)
										{
											goto IL_265;
										}
										goto IL_235;
									}
									break;
								}
							}
							else if (propertyID <= 233)
							{
								if (propertyID == 186)
								{
									goto IL_235;
								}
								if (propertyID != 233)
								{
									goto IL_265;
								}
								goto IL_111;
							}
							else
							{
								switch (propertyID)
								{
								case 270:
								case 271:
									goto IL_235;
								default:
									if (propertyID != 284)
									{
										goto IL_265;
									}
									break;
								}
							}
							attributes = new Attribute[]
							{
								BrowsableAttribute.Yes,
								new ResCategoryAttribute("DataCategory_Initialization"),
								RefreshPropertiesAttribute.All
							};
							goto IL_303;
							IL_111:
							attributes = new Attribute[]
							{
								BrowsableAttribute.Yes,
								new ResCategoryAttribute("DataCategory_Source"),
								RefreshPropertiesAttribute.All
							};
							goto IL_303;
							IL_235:
							attributes = new Attribute[]
							{
								BrowsableAttribute.Yes,
								new ResCategoryAttribute("DataCategory_Advanced"),
								RefreshPropertiesAttribute.All
							};
							goto IL_303;
							IL_265:
							attributes = new Attribute[]
							{
								BrowsableAttribute.Yes,
								RefreshPropertiesAttribute.All
							};
						}
						else if (oleDbPropertyInfo._description.EndsWith(" Provider", StringComparison.OrdinalIgnoreCase))
						{
							attributes = new Attribute[]
							{
								BrowsableAttribute.Yes,
								RefreshPropertiesAttribute.All,
								new ResCategoryAttribute("DataCategory_Source"),
								new TypeConverterAttribute(typeof(OleDbConnectionStringBuilder.OleDbProviderConverter))
							};
							refreshOnChange = true;
						}
						else
						{
							attributes = new Attribute[]
							{
								BrowsableAttribute.Yes,
								RefreshPropertiesAttribute.All,
								new CategoryAttribute(this.Provider)
							};
						}
						IL_303:
						DbConnectionStringBuilderDescriptor dbConnectionStringBuilderDescriptor = new DbConnectionStringBuilderDescriptor(oleDbPropertyInfo._description, typeof(OleDbConnectionStringBuilder), oleDbPropertyInfo._type, isReadOnly, attributes);
						dbConnectionStringBuilderDescriptor.RefreshOnChange = refreshOnChange;
						propertyDescriptors[oleDbPropertyInfo._description] = dbConnectionStringBuilderDescriptor;
					}
				}
			}
			base.GetProperties(propertyDescriptors);
		}

		// Token: 0x0400129B RID: 4763
		private static readonly string[] _validKeywords;

		// Token: 0x0400129C RID: 4764
		private static readonly Dictionary<string, OleDbConnectionStringBuilder.Keywords> _keywords;

		// Token: 0x0400129D RID: 4765
		private string[] _knownKeywords;

		// Token: 0x0400129E RID: 4766
		private Dictionary<string, OleDbPropertyInfo> _propertyInfo;

		// Token: 0x0400129F RID: 4767
		private string _fileName = "";

		// Token: 0x040012A0 RID: 4768
		private string _dataSource = "";

		// Token: 0x040012A1 RID: 4769
		private string _provider = "";

		// Token: 0x040012A2 RID: 4770
		private int _oleDbServices = -13;

		// Token: 0x040012A3 RID: 4771
		private bool _persistSecurityInfo;

		// Token: 0x0200021C RID: 540
		private enum Keywords
		{
			// Token: 0x040012A5 RID: 4773
			FileName,
			// Token: 0x040012A6 RID: 4774
			Provider,
			// Token: 0x040012A7 RID: 4775
			DataSource,
			// Token: 0x040012A8 RID: 4776
			PersistSecurityInfo,
			// Token: 0x040012A9 RID: 4777
			OleDbServices
		}

		// Token: 0x0200021D RID: 541
		private sealed class OleDbProviderConverter : StringConverter
		{
			// Token: 0x06001EDD RID: 7901 RVA: 0x00276B98 File Offset: 0x00275F98
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			// Token: 0x06001EDE RID: 7902 RVA: 0x00276BA8 File Offset: 0x00275FA8
			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return false;
			}

			// Token: 0x06001EDF RID: 7903 RVA: 0x00276BB8 File Offset: 0x00275FB8
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				TypeConverter.StandardValuesCollection standardValuesCollection = this._standardValues;
				if (this._standardValues == null)
				{
					DataTable elements = new OleDbEnumerator().GetElements();
					DataColumn column = elements.Columns["SOURCES_NAME"];
					DataColumn column2 = elements.Columns["SOURCES_TYPE"];
					List<string> list = new List<string>(elements.Rows.Count);
					foreach (object obj in elements.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						int num = (int)dataRow[column2];
						if (1 == num || 3 == num)
						{
							string text = (string)dataRow[column];
							if (!OleDbConnectionString.IsMSDASQL(text.ToLower(CultureInfo.InvariantCulture)) && 0 > list.IndexOf(text))
							{
								list.Add(text);
							}
						}
					}
					standardValuesCollection = new TypeConverter.StandardValuesCollection(list);
					this._standardValues = standardValuesCollection;
				}
				return standardValuesCollection;
			}

			// Token: 0x040012AA RID: 4778
			private const int DBSOURCETYPE_DATASOURCE_TDP = 1;

			// Token: 0x040012AB RID: 4779
			private const int DBSOURCETYPE_DATASOURCE_MDP = 3;

			// Token: 0x040012AC RID: 4780
			private TypeConverter.StandardValuesCollection _standardValues;
		}

		// Token: 0x0200021E RID: 542
		[Flags]
		internal enum OleDbServiceValues
		{
			// Token: 0x040012AE RID: 4782
			DisableAll = 0,
			// Token: 0x040012AF RID: 4783
			ResourcePooling = 1,
			// Token: 0x040012B0 RID: 4784
			TransactionEnlistment = 2,
			// Token: 0x040012B1 RID: 4785
			ClientCursor = 4,
			// Token: 0x040012B2 RID: 4786
			AggregationAfterSession = 8,
			// Token: 0x040012B3 RID: 4787
			EnableAll = -1,
			// Token: 0x040012B4 RID: 4788
			Default = -13
		}

		// Token: 0x0200021F RID: 543
		internal sealed class OleDbServicesConverter : TypeConverter
		{
			// Token: 0x06001EE1 RID: 7905 RVA: 0x00276CF8 File Offset: 0x002760F8
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return typeof(string) == sourceType || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x06001EE2 RID: 7906 RVA: 0x00276D28 File Offset: 0x00276128
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				string text = value as string;
				if (text == null)
				{
					return base.ConvertFrom(context, culture, value);
				}
				int num;
				if (int.TryParse(text, out num))
				{
					return num;
				}
				if (text.IndexOf(',') != -1)
				{
					int num2 = 0;
					string[] array = text.Split(new char[]
					{
						','
					});
					foreach (string value2 in array)
					{
						num2 |= (int)((OleDbConnectionStringBuilder.OleDbServiceValues)Enum.Parse(typeof(OleDbConnectionStringBuilder.OleDbServiceValues), value2, true));
					}
					return num2;
				}
				return (int)((OleDbConnectionStringBuilder.OleDbServiceValues)Enum.Parse(typeof(OleDbConnectionStringBuilder.OleDbServiceValues), text, true));
			}

			// Token: 0x06001EE3 RID: 7907 RVA: 0x00276DD8 File Offset: 0x002761D8
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(string) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06001EE4 RID: 7908 RVA: 0x00276E08 File Offset: 0x00276208
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (typeof(string) == destinationType && value != null && typeof(int) == value.GetType())
				{
					return Enum.Format(typeof(OleDbConnectionStringBuilder.OleDbServiceValues), (OleDbConnectionStringBuilder.OleDbServiceValues)((int)value), "G");
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

			// Token: 0x06001EE5 RID: 7909 RVA: 0x00276E68 File Offset: 0x00276268
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			// Token: 0x06001EE6 RID: 7910 RVA: 0x00276E78 File Offset: 0x00276278
			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return false;
			}

			// Token: 0x06001EE7 RID: 7911 RVA: 0x00276E88 File Offset: 0x00276288
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				TypeConverter.StandardValuesCollection standardValuesCollection = this._standardValues;
				if (standardValuesCollection == null)
				{
					Array values = Enum.GetValues(typeof(OleDbConnectionStringBuilder.OleDbServiceValues));
					Array.Sort(values, 0, values.Length);
					standardValuesCollection = new TypeConverter.StandardValuesCollection(values);
					this._standardValues = standardValuesCollection;
				}
				return standardValuesCollection;
			}

			// Token: 0x06001EE8 RID: 7912 RVA: 0x00276ED8 File Offset: 0x002762D8
			public override bool IsValid(ITypeDescriptorContext context, object value)
			{
				return true;
			}

			// Token: 0x040012B5 RID: 4789
			private TypeConverter.StandardValuesCollection _standardValues;
		}

		// Token: 0x02000220 RID: 544
		internal sealed class OleDbConnectionStringBuilderConverter : ExpandableObjectConverter
		{
			// Token: 0x06001EEA RID: 7914 RVA: 0x00276F08 File Offset: 0x00276308
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06001EEB RID: 7915 RVA: 0x00276F38 File Offset: 0x00276338
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == null)
				{
					throw ADP.ArgumentNull("destinationType");
				}
				if (typeof(InstanceDescriptor) == destinationType)
				{
					OleDbConnectionStringBuilder oleDbConnectionStringBuilder = value as OleDbConnectionStringBuilder;
					if (oleDbConnectionStringBuilder != null)
					{
						return this.ConvertToInstanceDescriptor(oleDbConnectionStringBuilder);
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

			// Token: 0x06001EEC RID: 7916 RVA: 0x00276F88 File Offset: 0x00276388
			private InstanceDescriptor ConvertToInstanceDescriptor(OleDbConnectionStringBuilder options)
			{
				Type[] types = new Type[]
				{
					typeof(string)
				};
				object[] arguments = new object[]
				{
					options.ConnectionString
				};
				ConstructorInfo constructor = typeof(OleDbConnectionStringBuilder).GetConstructor(types);
				return new InstanceDescriptor(constructor, arguments);
			}
		}
	}
}
