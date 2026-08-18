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
	// Token: 0x02000249 RID: 585
	[DefaultProperty("Provider")]
	[TypeConverter(typeof(OleDbConnectionStringBuilder.OleDbConnectionStringBuilderConverter))]
	[RefreshProperties(RefreshProperties.All)]
	public sealed class OleDbConnectionStringBuilder : DbConnectionStringBuilder
	{
		// Token: 0x060024E6 RID: 9446 RVA: 0x000FB9BC File Offset: 0x000FADBC
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

		// Token: 0x060024E7 RID: 9447 RVA: 0x000FBA50 File Offset: 0x000FAE50
		public OleDbConnectionStringBuilder() : this(null)
		{
			this._knownKeywords = OleDbConnectionStringBuilder._validKeywords;
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x000FBA70 File Offset: 0x000FAE70
		public OleDbConnectionStringBuilder(string connectionString)
		{
			if (!ADP.IsEmpty(connectionString))
			{
				base.ConnectionString = connectionString;
			}
		}

		// Token: 0x170005F9 RID: 1529
		public override object this[string keyword]
		{
			get
			{
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

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060024EB RID: 9451 RVA: 0x000FBBC0 File Offset: 0x000FAFC0
		// (set) Token: 0x060024EC RID: 9452 RVA: 0x000FBBD4 File Offset: 0x000FAFD4
		[DisplayName("Data Source")]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Source")]
		[ResDescription("DbConnectionString_DataSource")]
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

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060024ED RID: 9453 RVA: 0x000FBBF4 File Offset: 0x000FAFF4
		// (set) Token: 0x060024EE RID: 9454 RVA: 0x000FBC08 File Offset: 0x000FB008
		[DisplayName("File Name")]
		[Editor("System.Windows.Forms.Design.FileNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ResCategory("DataCategory_NamedConnectionString")]
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

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060024EF RID: 9455 RVA: 0x000FBC28 File Offset: 0x000FB028
		// (set) Token: 0x060024F0 RID: 9456 RVA: 0x000FBC3C File Offset: 0x000FB03C
		[ResCategory("DataCategory_Pooling")]
		[DisplayName("OLE DB Services")]
		[TypeConverter(typeof(OleDbConnectionStringBuilder.OleDbServicesConverter))]
		[ResDescription("DbConnectionString_OleDbServices")]
		[RefreshProperties(RefreshProperties.All)]
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

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060024F1 RID: 9457 RVA: 0x000FBC5C File Offset: 0x000FB05C
		// (set) Token: 0x060024F2 RID: 9458 RVA: 0x000FBC70 File Offset: 0x000FB070
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

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060024F3 RID: 9459 RVA: 0x000FBC90 File Offset: 0x000FB090
		// (set) Token: 0x060024F4 RID: 9460 RVA: 0x000FBCA4 File Offset: 0x000FB0A4
		[TypeConverter(typeof(OleDbConnectionStringBuilder.OleDbProviderConverter))]
		[ResDescription("DbConnectionString_Provider")]
		[DisplayName("Provider")]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Source")]
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

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x060024F5 RID: 9461 RVA: 0x000FBCCC File Offset: 0x000FB0CC
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

		// Token: 0x060024F6 RID: 9462 RVA: 0x000FBE94 File Offset: 0x000FB294
		public override bool ContainsKey(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			return OleDbConnectionStringBuilder._keywords.ContainsKey(keyword) || base.ContainsKey(keyword);
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x000FBEC4 File Offset: 0x000FB2C4
		private static bool ConvertToBoolean(object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToBoolean(value);
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x000FBED8 File Offset: 0x000FB2D8
		private static int ConvertToInt32(object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToInt32(value);
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x000FBEEC File Offset: 0x000FB2EC
		private static string ConvertToString(object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToString(value);
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x000FBF00 File Offset: 0x000FB300
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

		// Token: 0x060024FB RID: 9467 RVA: 0x000FBF40 File Offset: 0x000FB340
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

		// Token: 0x060024FC RID: 9468 RVA: 0x000FBFA4 File Offset: 0x000FB3A4
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

		// Token: 0x060024FD RID: 9469 RVA: 0x000FBFE8 File Offset: 0x000FB3E8
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

		// Token: 0x060024FE RID: 9470 RVA: 0x000FC060 File Offset: 0x000FB460
		private new void ClearPropertyDescriptors()
		{
			base.ClearPropertyDescriptors();
			this._knownKeywords = null;
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x000FC07C File Offset: 0x000FB47C
		private void RestartProvider()
		{
			this.ClearPropertyDescriptors();
			this._propertyInfo = null;
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x000FC098 File Offset: 0x000FB498
		private void SetValue(string keyword, bool value)
		{
			base[keyword] = value.ToString(null);
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x000FC0B4 File Offset: 0x000FB4B4
		private void SetValue(string keyword, int value)
		{
			base[keyword] = value.ToString(null);
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x000FC0D0 File Offset: 0x000FB4D0
		private void SetValue(string keyword, string value)
		{
			ADP.CheckArgumentNull(value, keyword);
			base[keyword] = value;
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x000FC0EC File Offset: 0x000FB4EC
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

		// Token: 0x06002504 RID: 9476 RVA: 0x000FC150 File Offset: 0x000FB550
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

		// Token: 0x06002505 RID: 9477 RVA: 0x000FC584 File Offset: 0x000FB984
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
									goto IL_2BA;
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
									goto IL_2BA;
								case 11:
									goto IL_22F;
								case 12:
									attributes = new Attribute[]
									{
										BrowsableAttribute.Yes,
										new ResCategoryAttribute("DataCategory_Security"),
										RefreshPropertiesAttribute.All
									};
									isReadOnly = this.ContainsKey("Integrated Security");
									refreshOnChange = true;
									goto IL_2BA;
								default:
									switch (propertyID)
									{
									case 61:
									case 63:
									case 65:
										goto IL_206;
									case 62:
										goto IL_109;
									case 64:
										goto IL_22F;
									case 66:
										break;
									default:
										if (propertyID != 160)
										{
											goto IL_22F;
										}
										goto IL_206;
									}
									break;
								}
							}
							else if (propertyID <= 233)
							{
								if (propertyID == 186)
								{
									goto IL_206;
								}
								if (propertyID != 233)
								{
									goto IL_22F;
								}
								goto IL_109;
							}
							else
							{
								if (propertyID - 270 <= 1)
								{
									goto IL_206;
								}
								if (propertyID != 284)
								{
									goto IL_22F;
								}
							}
							attributes = new Attribute[]
							{
								BrowsableAttribute.Yes,
								new ResCategoryAttribute("DataCategory_Initialization"),
								RefreshPropertiesAttribute.All
							};
							goto IL_2BA;
							IL_109:
							attributes = new Attribute[]
							{
								BrowsableAttribute.Yes,
								new ResCategoryAttribute("DataCategory_Source"),
								RefreshPropertiesAttribute.All
							};
							goto IL_2BA;
							IL_206:
							attributes = new Attribute[]
							{
								BrowsableAttribute.Yes,
								new ResCategoryAttribute("DataCategory_Advanced"),
								RefreshPropertiesAttribute.All
							};
							goto IL_2BA;
							IL_22F:
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
						IL_2BA:
						DbConnectionStringBuilderDescriptor dbConnectionStringBuilderDescriptor = new DbConnectionStringBuilderDescriptor(oleDbPropertyInfo._description, typeof(OleDbConnectionStringBuilder), oleDbPropertyInfo._type, isReadOnly, attributes);
						dbConnectionStringBuilderDescriptor.RefreshOnChange = refreshOnChange;
						propertyDescriptors[oleDbPropertyInfo._description] = dbConnectionStringBuilderDescriptor;
					}
				}
			}
			base.GetProperties(propertyDescriptors);
		}

		// Token: 0x040015C2 RID: 5570
		private static readonly string[] _validKeywords;

		// Token: 0x040015C3 RID: 5571
		private static readonly Dictionary<string, OleDbConnectionStringBuilder.Keywords> _keywords;

		// Token: 0x040015C4 RID: 5572
		private string[] _knownKeywords;

		// Token: 0x040015C5 RID: 5573
		private Dictionary<string, OleDbPropertyInfo> _propertyInfo;

		// Token: 0x040015C6 RID: 5574
		private string _fileName = "";

		// Token: 0x040015C7 RID: 5575
		private string _dataSource = "";

		// Token: 0x040015C8 RID: 5576
		private string _provider = "";

		// Token: 0x040015C9 RID: 5577
		private int _oleDbServices = -13;

		// Token: 0x040015CA RID: 5578
		private bool _persistSecurityInfo;

		// Token: 0x02000400 RID: 1024
		private enum Keywords
		{
			// Token: 0x040021B4 RID: 8628
			FileName,
			// Token: 0x040021B5 RID: 8629
			Provider,
			// Token: 0x040021B6 RID: 8630
			DataSource,
			// Token: 0x040021B7 RID: 8631
			PersistSecurityInfo,
			// Token: 0x040021B8 RID: 8632
			OleDbServices
		}

		// Token: 0x02000401 RID: 1025
		private sealed class OleDbProviderConverter : StringConverter
		{
			// Token: 0x060035CA RID: 13770 RVA: 0x00146F18 File Offset: 0x00146318
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			// Token: 0x060035CB RID: 13771 RVA: 0x00146F28 File Offset: 0x00146328
			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return false;
			}

			// Token: 0x060035CC RID: 13772 RVA: 0x00146F38 File Offset: 0x00146338
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

			// Token: 0x040021B9 RID: 8633
			private const int DBSOURCETYPE_DATASOURCE_TDP = 1;

			// Token: 0x040021BA RID: 8634
			private const int DBSOURCETYPE_DATASOURCE_MDP = 3;

			// Token: 0x040021BB RID: 8635
			private TypeConverter.StandardValuesCollection _standardValues;
		}

		// Token: 0x02000402 RID: 1026
		[Flags]
		internal enum OleDbServiceValues
		{
			// Token: 0x040021BD RID: 8637
			DisableAll = 0,
			// Token: 0x040021BE RID: 8638
			ResourcePooling = 1,
			// Token: 0x040021BF RID: 8639
			TransactionEnlistment = 2,
			// Token: 0x040021C0 RID: 8640
			ClientCursor = 4,
			// Token: 0x040021C1 RID: 8641
			AggregationAfterSession = 8,
			// Token: 0x040021C2 RID: 8642
			EnableAll = -1,
			// Token: 0x040021C3 RID: 8643
			Default = -13
		}

		// Token: 0x02000403 RID: 1027
		internal sealed class OleDbServicesConverter : TypeConverter
		{
			// Token: 0x060035CE RID: 13774 RVA: 0x00147060 File Offset: 0x00146460
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return typeof(string) == sourceType || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x060035CF RID: 13775 RVA: 0x0014708C File Offset: 0x0014648C
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

			// Token: 0x060035D0 RID: 13776 RVA: 0x00147134 File Offset: 0x00146534
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(string) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x060035D1 RID: 13777 RVA: 0x00147160 File Offset: 0x00146560
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (typeof(string) == destinationType && value != null && typeof(int) == value.GetType())
				{
					return Enum.Format(typeof(OleDbConnectionStringBuilder.OleDbServiceValues), (OleDbConnectionStringBuilder.OleDbServiceValues)((int)value), "G");
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

			// Token: 0x060035D2 RID: 13778 RVA: 0x001471C8 File Offset: 0x001465C8
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			// Token: 0x060035D3 RID: 13779 RVA: 0x001471D8 File Offset: 0x001465D8
			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return false;
			}

			// Token: 0x060035D4 RID: 13780 RVA: 0x001471E8 File Offset: 0x001465E8
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

			// Token: 0x060035D5 RID: 13781 RVA: 0x0014722C File Offset: 0x0014662C
			public override bool IsValid(ITypeDescriptorContext context, object value)
			{
				return true;
			}

			// Token: 0x040021C4 RID: 8644
			private TypeConverter.StandardValuesCollection _standardValues;
		}

		// Token: 0x02000404 RID: 1028
		internal sealed class OleDbConnectionStringBuilderConverter : ExpandableObjectConverter
		{
			// Token: 0x060035D7 RID: 13783 RVA: 0x00147250 File Offset: 0x00146650
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x060035D8 RID: 13784 RVA: 0x0014727C File Offset: 0x0014667C
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

			// Token: 0x060035D9 RID: 13785 RVA: 0x001472D0 File Offset: 0x001466D0
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
