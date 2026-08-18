using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Common;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Web.ApplicationServices;
using System.Web.Resources;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace System.Web.ClientServices.Providers
{
	// Token: 0x02000111 RID: 273
	[SecurityCritical]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class ClientSettingsProvider : SettingsProvider, IApplicationSettingsProvider
	{
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x00032138 File Offset: 0x00030338
		// (set) Token: 0x06000E52 RID: 3666 RVA: 0x000032F4 File Offset: 0x000014F4
		public override string ApplicationName
		{
			[SecuritySafeCritical]
			get
			{
				return "";
			}
			[SecuritySafeCritical]
			set
			{
			}
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x00032B84 File Offset: 0x00030D84
		public static SettingsPropertyCollection GetPropertyMetadata(string serviceUri)
		{
			CookieContainer cookieContainer = null;
			IIdentity identity = Thread.CurrentPrincipal.Identity;
			SettingsPropertyCollection settingsPropertyCollection = new SettingsPropertyCollection();
			if (identity is ClientFormsIdentity)
			{
				cookieContainer = ((ClientFormsIdentity)identity).AuthenticationCookies;
			}
			if (serviceUri.EndsWith(".svc", StringComparison.OrdinalIgnoreCase))
			{
				throw new NotImplementedException();
			}
			object obj = ProxyHelper.CreateWebRequestAndGetResponse(serviceUri + "/GetPropertiesMetadata", ref cookieContainer, identity.Name, null, null, null, null, typeof(Collection<ProfilePropertyMetadata>));
			Collection<ProfilePropertyMetadata> collection = (Collection<ProfilePropertyMetadata>)obj;
			if (collection != null)
			{
				foreach (ProfilePropertyMetadata p in collection)
				{
					ClientSettingsProvider.AddToColl(p, settingsPropertyCollection, identity.IsAuthenticated);
				}
			}
			return settingsPropertyCollection;
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00032C4C File Offset: 0x00030E4C
		private static void AddToColl(ProfilePropertyMetadata p, SettingsPropertyCollection retColl, bool isAuthenticated)
		{
			string propertyName = p.PropertyName;
			Type type = Type.GetType(p.TypeName, false, true);
			bool allowAnonymousAccess = p.AllowAnonymousAccess;
			bool isReadOnly = p.IsReadOnly;
			if (!allowAnonymousAccess && !isAuthenticated)
			{
				return;
			}
			SettingsSerializeAs serializeAs = (SettingsSerializeAs)p.SerializeAs;
			SettingsAttributeDictionary settingsAttributeDictionary = new SettingsAttributeDictionary();
			settingsAttributeDictionary.Add("AllowAnonymous", allowAnonymousAccess);
			retColl.Add(new SettingsProperty(propertyName, type, null, isReadOnly, p.DefaultValue, serializeAs, settingsAttributeDictionary, true, true));
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00032CC0 File Offset: 0x00030EC0
		[SecuritySafeCritical]
		public override void Initialize(string name, NameValueCollection config)
		{
			ClientSettingsProvider._UsingIsolatedStore = false;
			string text = ConfigurationManager.AppSettings["ClientSettingsProvider.ServiceUri"];
			if (!string.IsNullOrEmpty(text))
			{
				ClientSettingsProvider.ServiceUri = text;
			}
			text = ConfigurationManager.AppSettings["ClientSettingsProvider.ConnectionStringName"];
			if (!string.IsNullOrEmpty(text))
			{
				if (ConfigurationManager.ConnectionStrings[text] != null)
				{
					this._ConnectionStringProvider = ConfigurationManager.ConnectionStrings[text].ProviderName;
					this._ConnectionString = ConfigurationManager.ConnectionStrings[text].ConnectionString;
				}
				else
				{
					this._ConnectionString = text;
				}
			}
			else
			{
				this._ConnectionString = SqlHelper.GetDefaultConnectionString();
			}
			text = ConfigurationManager.AppSettings["ClientSettingsProvider.HonorCookieExpiry"];
			if (!string.IsNullOrEmpty(text))
			{
				this._HonorCookieExpiry = (string.Compare(text, "true", StringComparison.OrdinalIgnoreCase) == 0);
			}
			if (name == null)
			{
				name = base.GetType().ToString();
			}
			base.Initialize(name, config);
			if (config != null)
			{
				text = config["serviceUri"];
				if (!string.IsNullOrEmpty(text))
				{
					ClientSettingsProvider.ServiceUri = text;
				}
				text = config["connectionStringName"];
				if (!string.IsNullOrEmpty(text))
				{
					if (ConfigurationManager.ConnectionStrings[text] != null)
					{
						this._ConnectionStringProvider = ConfigurationManager.ConnectionStrings[text].ProviderName;
						this._ConnectionString = ConfigurationManager.ConnectionStrings[text].ConnectionString;
					}
					else
					{
						this._ConnectionString = text;
					}
				}
				config.Remove("name");
				config.Remove("description");
				config.Remove("connectionStringName");
				config.Remove("serviceUri");
				foreach (object obj in config.Keys)
				{
					string text2 = (string)obj;
					if (!string.IsNullOrEmpty(text2))
					{
						throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.AttributeNotRecognized, new object[]
						{
							text2
						}));
					}
				}
			}
			int num = SqlHelper.IsSpecialConnectionString(this._ConnectionString);
			if (num == 1)
			{
				ClientSettingsProvider._UsingFileSystemStore = true;
				return;
			}
			if (num != 2)
			{
				return;
			}
			ClientSettingsProvider._UsingIsolatedStore = true;
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00032ED0 File Offset: 0x000310D0
		[SecuritySafeCritical]
		public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection propertyCollection)
		{
			if (propertyCollection == null || propertyCollection.Count < 1)
			{
				return new SettingsPropertyValueCollection();
			}
			object @lock = ClientSettingsProvider._lock;
			SettingsPropertyValueCollection propertyValues;
			lock (@lock)
			{
				if (ClientSettingsProvider._SettingsBaseClass == null && context != null)
				{
					Type type = context["SettingsClassType"] as Type;
					if (type != null)
					{
						ClientSettingsProvider._SettingsBaseClass = (type.InvokeMember("Default", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, null, null, null, CultureInfo.InvariantCulture) as ApplicationSettingsBase);
					}
				}
				this._PropertyValues = new SettingsPropertyValueCollection();
				this._Properties = propertyCollection;
				ClientSettingsProvider.StoreKnownTypes(propertyCollection);
				this.GetPropertyValuesCore();
				propertyValues = this._PropertyValues;
			}
			return propertyValues;
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00032F88 File Offset: 0x00031188
		private void GetPropertyValuesCore()
		{
			this._UserName = Thread.CurrentPrincipal.Identity.Name;
			if (this._firstTime)
			{
				this._firstTime = false;
				this._NeedToDoReset = this.GetNeedToReset();
				this.RegisterForValidateUserEvent();
			}
			if (this._NeedToDoReset)
			{
				this._NeedToDoReset = false;
				this.SetNeedToReset(false);
				this._PropertyValues = new SettingsPropertyValueCollection();
				this.SetRemainingValuesToDefault();
				this.SetPropertyValuesCore(this._PropertyValues, false);
			}
			bool isCacheMoreFresh = this.GetIsCacheMoreFresh();
			this.GetPropertyValuesFromSQL();
			if (!ConnectivityStatus.IsOffline)
			{
				if (isCacheMoreFresh)
				{
					this.SetPropertyValuesWeb(this._PropertyValues, isCacheMoreFresh);
				}
				else
				{
					this.GetPropertyValuesFromWeb();
					this.SetPropertyValuesSQL(this._PropertyValues, false);
				}
			}
			if (this._PropertyValues.Count < this._Properties.Count)
			{
				this.SetRemainingValuesToDefault();
			}
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00033058 File Offset: 0x00031258
		[SecuritySafeCritical]
		public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection propertyValueCollection)
		{
			if (propertyValueCollection == null || propertyValueCollection.Count < 1)
			{
				return;
			}
			object @lock = ClientSettingsProvider._lock;
			lock (@lock)
			{
				ClientSettingsProvider.StoreKnownTypes(propertyValueCollection);
				this.SetPropertyValuesCore(propertyValueCollection, true);
			}
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x000330AC File Offset: 0x000312AC
		private void SetPropertyValuesCore(SettingsPropertyValueCollection values, bool raiseEvent)
		{
			object @lock = ClientSettingsProvider._lock;
			lock (@lock)
			{
				bool isCacheMoreFresh = this.GetIsCacheMoreFresh();
				this.SetPropertyValuesSQL(values, true);
				Collection<string> collection = null;
				if (!ConnectivityStatus.IsOffline)
				{
					collection = this.SetPropertyValuesWeb(values, isCacheMoreFresh);
				}
				if (raiseEvent && this.SettingsSaved != null)
				{
					if (collection == null)
					{
						collection = new Collection<string>();
					}
					this.SettingsSaved(this, new SettingsSavedEventArgs(collection));
				}
			}
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0003312C File Offset: 0x0003132C
		[SecuritySafeCritical]
		public void Reset(SettingsContext context)
		{
			object @lock = ClientSettingsProvider._lock;
			lock (@lock)
			{
				if (this._Properties == null)
				{
					this.SetNeedToReset(true);
				}
				else
				{
					this._PropertyValues = new SettingsPropertyValueCollection();
					this.SetRemainingValuesToDefault();
					this.SetPropertyValues(context, this._PropertyValues);
					this._NeedToDoReset = false;
					this.SetNeedToReset(false);
				}
			}
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x000032F4 File Offset: 0x000014F4
		[SecuritySafeCritical]
		public void Upgrade(SettingsContext context, SettingsPropertyCollection properties)
		{
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x000331A4 File Offset: 0x000313A4
		[SecuritySafeCritical]
		public SettingsPropertyValue GetPreviousVersion(SettingsContext context, SettingsProperty property)
		{
			if (this._Properties == null)
			{
				this._Properties = new SettingsPropertyCollection();
			}
			if (this._Properties[property.Name] == null)
			{
				this._Properties.Add(property);
			}
			this.GetPropertyValuesCore();
			return this._PropertyValues[property.Name];
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x000331FA File Offset: 0x000313FA
		private string GetServiceUri()
		{
			if (string.IsNullOrEmpty(ClientSettingsProvider._ServiceUri))
			{
				throw new ArgumentException(AtlasWeb.ServiceUriNotFound);
			}
			return ClientSettingsProvider._ServiceUri;
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x00033218 File Offset: 0x00031418
		// (set) Token: 0x06000E5F RID: 3679 RVA: 0x0003321F File Offset: 0x0003141F
		public static string ServiceUri
		{
			get
			{
				return ClientSettingsProvider._ServiceUri;
			}
			set
			{
				ClientSettingsProvider._ServiceUri = value;
				if (string.IsNullOrEmpty(ClientSettingsProvider._ServiceUri))
				{
					ClientSettingsProvider._UsingWFCService = false;
					return;
				}
				ClientSettingsProvider._UsingWFCService = ClientSettingsProvider._ServiceUri.EndsWith(".svc", StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06000E60 RID: 3680 RVA: 0x00033250 File Offset: 0x00031450
		// (remove) Token: 0x06000E61 RID: 3681 RVA: 0x00033288 File Offset: 0x00031488
		public event EventHandler<SettingsSavedEventArgs> SettingsSaved;

		// Token: 0x06000E62 RID: 3682 RVA: 0x000332BD File Offset: 0x000314BD
		internal static Type[] GetKnownTypes(ICustomAttributeProvider knownTypeAttributeTarget)
		{
			if (ClientSettingsProvider._KnownTypesArray == null)
			{
				ClientSettingsProvider.InitKnownTypes();
			}
			return ClientSettingsProvider._KnownTypesArray;
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x000332D0 File Offset: 0x000314D0
		private static void InitKnownTypes()
		{
			ClientSettingsProvider._KnownTypesHashtable = new Hashtable();
			ClientSettingsProvider._KnownTypesArray = new Type[]
			{
				typeof(bool),
				typeof(string),
				typeof(ArrayList),
				typeof(ProfilePropertyMetadata),
				typeof(IDictionary<string, object>),
				typeof(Collection<string>)
			};
			for (int i = 0; i < ClientSettingsProvider._KnownTypesArray.Length; i++)
			{
				ClientSettingsProvider._KnownTypesHashtable.Add(ClientSettingsProvider._KnownTypesArray[i], string.Empty);
			}
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00033368 File Offset: 0x00031568
		private static void StoreKnownTypes(SettingsPropertyValueCollection propertyValueCollection)
		{
			if (ClientSettingsProvider._KnownTypesHashtable == null)
			{
				ClientSettingsProvider.InitKnownTypes();
			}
			ArrayList arrayList = null;
			foreach (object obj in propertyValueCollection)
			{
				SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj;
				if (!ClientSettingsProvider._KnownTypesHashtable.Contains(settingsPropertyValue.Property.PropertyType))
				{
					ClientSettingsProvider._KnownTypesHashtable.Add(settingsPropertyValue.Property.PropertyType, string.Empty);
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					arrayList.Add(settingsPropertyValue.Property.PropertyType);
				}
			}
			if (arrayList != null)
			{
				Type[] array = new Type[ClientSettingsProvider._KnownTypesArray.Length + arrayList.Count];
				ClientSettingsProvider._KnownTypesArray.CopyTo(array, 0);
				arrayList.CopyTo(array, ClientSettingsProvider._KnownTypesArray.Length);
				ClientSettingsProvider._KnownTypesArray = array;
			}
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0003344C File Offset: 0x0003164C
		private static void StoreKnownTypes(SettingsPropertyCollection propertyCollection)
		{
			if (ClientSettingsProvider._KnownTypesHashtable == null)
			{
				ClientSettingsProvider.InitKnownTypes();
			}
			ArrayList arrayList = null;
			foreach (object obj in propertyCollection)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				if (!ClientSettingsProvider._KnownTypesHashtable.Contains(settingsProperty.PropertyType))
				{
					ClientSettingsProvider._KnownTypesHashtable.Add(settingsProperty.PropertyType, string.Empty);
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					arrayList.Add(settingsProperty.PropertyType);
				}
			}
			if (arrayList != null)
			{
				Type[] array = new Type[ClientSettingsProvider._KnownTypesArray.Length + arrayList.Count];
				ClientSettingsProvider._KnownTypesArray.CopyTo(array, 0);
				arrayList.CopyTo(array, ClientSettingsProvider._KnownTypesArray.Length);
				ClientSettingsProvider._KnownTypesArray = array;
			}
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00033520 File Offset: 0x00031720
		private void GetPropertyValuesFromWeb()
		{
			this.GetPropertyValuesFromWebCore(this._HonorCookieExpiry);
			bool flag = this._PropertyValues.Count < this._Properties.Count;
			if (!this._HonorCookieExpiry && flag)
			{
				ClientFormsIdentity clientFormsIdentity = Thread.CurrentPrincipal.Identity as ClientFormsIdentity;
				if (clientFormsIdentity != null)
				{
					clientFormsIdentity.RevalidateUser();
					this.GetPropertyValuesFromWebCore(true);
				}
			}
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00033580 File Offset: 0x00031780
		private void GetPropertyValuesFromWebCore(bool bubbleExceptionFromSvc)
		{
			string[] array = new string[this._Properties.Count];
			int num = 0;
			CookieContainer cookieContainer = null;
			IIdentity identity = Thread.CurrentPrincipal.Identity;
			foreach (object obj in this._Properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				array[num++] = settingsProperty.Name;
			}
			if (identity is ClientFormsIdentity)
			{
				cookieContainer = ((ClientFormsIdentity)identity).AuthenticationCookies;
			}
			if (ClientSettingsProvider._UsingWFCService)
			{
				throw new NotImplementedException();
			}
			string[] paramNames = new string[]
			{
				"properties",
				"authenticatedUserOnly"
			};
			object[] paramValues = new object[]
			{
				array,
				identity.IsAuthenticated && identity is ClientFormsIdentity
			};
			object obj2 = null;
			try
			{
				obj2 = ProxyHelper.CreateWebRequestAndGetResponse(this.GetServiceUri() + "/GetPropertiesForCurrentUser", ref cookieContainer, identity.Name, this._ConnectionString, this._ConnectionStringProvider, paramNames, paramValues, typeof(Dictionary<string, object>));
			}
			catch
			{
				if (bubbleExceptionFromSvc)
				{
					throw;
				}
			}
			if (obj2 != null)
			{
				Dictionary<string, object> dictionary = (Dictionary<string, object>)obj2;
				foreach (KeyValuePair<string, object> keyValuePair in dictionary)
				{
					SettingsProperty settingsProperty2 = this._Properties[keyValuePair.Key];
					if (settingsProperty2 != null)
					{
						bool flag = false;
						SettingsPropertyValue settingsPropertyValue = this._PropertyValues[settingsProperty2.Name];
						if (settingsPropertyValue == null)
						{
							settingsPropertyValue = new SettingsPropertyValue(settingsProperty2);
							flag = true;
						}
						if (keyValuePair.Value != null && !settingsProperty2.PropertyType.IsAssignableFrom(keyValuePair.Value.GetType()))
						{
							object propertyValue = null;
							if (!ObjectConverter.TryConvertObjectToType(keyValuePair.Value, settingsProperty2.PropertyType, new JavaScriptSerializer(), out propertyValue))
							{
								continue;
							}
							settingsPropertyValue.PropertyValue = propertyValue;
						}
						else
						{
							settingsPropertyValue.PropertyValue = keyValuePair.Value;
						}
						settingsPropertyValue.Deserialized = true;
						settingsPropertyValue.IsDirty = false;
						if (flag)
						{
							this._PropertyValues.Add(settingsPropertyValue);
						}
					}
				}
			}
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x000337C8 File Offset: 0x000319C8
		private Collection<string> SetPropertyValuesWeb(SettingsPropertyValueCollection values, bool cacheIsMoreFresh)
		{
			bool flag = false;
			Collection<string> collection = null;
			ClientFormsIdentity clientFormsIdentity = Thread.CurrentPrincipal.Identity as ClientFormsIdentity;
			try
			{
				collection = this.SetPropertyValuesWebCore(values, cacheIsMoreFresh);
				flag = (collection != null && collection.Count > 0);
			}
			catch (WebException)
			{
				if (clientFormsIdentity == null || this._HonorCookieExpiry)
				{
					throw;
				}
				flag = true;
			}
			if (!this._HonorCookieExpiry && flag && clientFormsIdentity != null)
			{
				clientFormsIdentity.RevalidateUser();
				collection = this.SetPropertyValuesWebCore(values, cacheIsMoreFresh);
			}
			return collection;
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00033848 File Offset: 0x00031A48
		private Collection<string> SetPropertyValuesWebCore(SettingsPropertyValueCollection values, bool cacheIsMoreFresh)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (object obj in values)
			{
				SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj;
				if (cacheIsMoreFresh || settingsPropertyValue.IsDirty)
				{
					dictionary.Add(settingsPropertyValue.Property.Name, settingsPropertyValue.PropertyValue);
				}
			}
			CookieContainer cookieContainer = null;
			IIdentity identity = Thread.CurrentPrincipal.Identity;
			if (identity is ClientFormsIdentity)
			{
				cookieContainer = ((ClientFormsIdentity)identity).AuthenticationCookies;
			}
			if (ClientSettingsProvider._UsingWFCService)
			{
				throw new NotImplementedException();
			}
			string[] paramNames = new string[]
			{
				"values",
				"authenticatedUserOnly"
			};
			object[] paramValues = new object[]
			{
				dictionary,
				identity.IsAuthenticated && identity is ClientFormsIdentity
			};
			object obj2 = ProxyHelper.CreateWebRequestAndGetResponse(this.GetServiceUri() + "/SetPropertiesForCurrentUser", ref cookieContainer, identity.Name, this._ConnectionString, this._ConnectionStringProvider, paramNames, paramValues, typeof(Collection<string>));
			Collection<string> result = (Collection<string>)obj2;
			this.SetIsCacheMoreFresh(false);
			return result;
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x00033984 File Offset: 0x00031B84
		private void GetPropertyValuesFromSQL()
		{
			if (!ClientSettingsProvider._UsingFileSystemStore && !ClientSettingsProvider._UsingIsolatedStore)
			{
				using (DbConnection connection = SqlHelper.GetConnection(Thread.CurrentPrincipal.Identity.Name, this.GetConnectionString(), this._ConnectionStringProvider))
				{
					DbTransaction dbTransaction = null;
					try
					{
						dbTransaction = connection.BeginTransaction();
						DbCommand dbCommand = connection.CreateCommand();
						dbCommand.CommandText = "SELECT PropertyName, PropertyStoredAs, PropertyValue FROM Settings";
						dbCommand.Transaction = dbTransaction;
						using (DbDataReader dbDataReader = dbCommand.ExecuteReader())
						{
							while (dbDataReader.Read())
							{
								string @string = dbDataReader.GetString(0);
								string string2 = dbDataReader.GetString(1);
								string propVal = dbDataReader.IsDBNull(2) ? null : dbDataReader.GetString(2);
								this.AddProperty(@string, string2, propVal);
							}
						}
					}
					catch
					{
						if (dbTransaction != null)
						{
							dbTransaction.Rollback();
							dbTransaction = null;
						}
						throw;
					}
					finally
					{
						if (dbTransaction != null)
						{
							dbTransaction.Commit();
						}
					}
				}
				return;
			}
			ClientData userClientData = ClientDataManager.GetUserClientData(Thread.CurrentPrincipal.Identity.Name, ClientSettingsProvider._UsingIsolatedStore);
			if (userClientData.SettingsNames == null || userClientData.SettingsValues == null)
			{
				return;
			}
			int num = userClientData.SettingsNames.Length;
			if (userClientData.SettingsNames.Length != userClientData.SettingsStoredAs.Length || userClientData.SettingsValues.Length != userClientData.SettingsStoredAs.Length)
			{
				return;
			}
			for (int i = 0; i < num; i++)
			{
				this.AddProperty(userClientData.SettingsNames[i], userClientData.SettingsStoredAs[i], userClientData.SettingsValues[i]);
			}
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x00033B28 File Offset: 0x00031D28
		private void AddProperty(string name, string storedAs, string propVal)
		{
			if (storedAs != "S" && storedAs != "B" && storedAs != "N")
			{
				return;
			}
			SettingsProperty settingsProperty = this._Properties[name];
			if (settingsProperty == null)
			{
				return;
			}
			SettingsPropertyValue settingsPropertyValue = this._PropertyValues[name];
			bool flag = false;
			if (settingsPropertyValue == null)
			{
				settingsPropertyValue = new SettingsPropertyValue(settingsProperty);
				flag = true;
			}
			if (!(storedAs == "S"))
			{
				if (!(storedAs == "B"))
				{
					if (storedAs == "N")
					{
						settingsPropertyValue.SerializedValue = null;
					}
				}
				else
				{
					settingsPropertyValue.SerializedValue = Convert.FromBase64String(propVal);
				}
			}
			else
			{
				settingsPropertyValue.SerializedValue = propVal;
			}
			settingsPropertyValue.Deserialized = false;
			settingsPropertyValue.IsDirty = false;
			if (flag)
			{
				this._PropertyValues.Add(settingsPropertyValue);
			}
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00033BF0 File Offset: 0x00031DF0
		private void SetPropertyValuesSQL(SettingsPropertyValueCollection values, bool updateSaveTime)
		{
			string name = Thread.CurrentPrincipal.Identity.Name;
			if (ClientSettingsProvider._UsingFileSystemStore || ClientSettingsProvider._UsingIsolatedStore)
			{
				ClientData userClientData = ClientDataManager.GetUserClientData(name, ClientSettingsProvider._UsingIsolatedStore);
				userClientData.SettingsNames = new string[values.Count];
				userClientData.SettingsStoredAs = new string[values.Count];
				userClientData.SettingsValues = new string[values.Count];
				int num = 0;
				foreach (object obj in values)
				{
					SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj;
					userClientData.SettingsNames[num] = settingsPropertyValue.Property.Name;
					object serializedValue = settingsPropertyValue.SerializedValue;
					if (serializedValue == null)
					{
						userClientData.SettingsStoredAs[num] = "N";
					}
					else if (serializedValue is string)
					{
						userClientData.SettingsStoredAs[num] = "S";
						userClientData.SettingsValues[num] = (string)serializedValue;
					}
					else
					{
						userClientData.SettingsStoredAs[num] = "B";
						userClientData.SettingsValues[num] = Convert.ToBase64String((byte[])serializedValue);
					}
					num++;
				}
				if (updateSaveTime)
				{
					userClientData.SettingsCacheIsMoreFresh = true;
				}
				userClientData.Save();
				return;
			}
			using (DbConnection connection = SqlHelper.GetConnection(name, this.GetConnectionString(), this._ConnectionStringProvider))
			{
				DbTransaction dbTransaction = null;
				try
				{
					dbTransaction = connection.BeginTransaction();
					foreach (object obj2 in values)
					{
						SettingsPropertyValue settingsPropertyValue2 = (SettingsPropertyValue)obj2;
						DbCommand dbCommand = connection.CreateCommand();
						dbCommand.Transaction = dbTransaction;
						dbCommand.CommandText = "DELETE FROM Settings WHERE PropertyName = @PropName";
						SqlHelper.AddParameter(connection, dbCommand, "@PropName", settingsPropertyValue2.Property.Name);
						dbCommand.ExecuteNonQuery();
						dbCommand = connection.CreateCommand();
						dbCommand.Transaction = dbTransaction;
						object serializedValue2 = settingsPropertyValue2.SerializedValue;
						if (serializedValue2 == null)
						{
							dbCommand.CommandText = "INSERT INTO Settings (PropertyName, PropertyStoredAs, PropertyValue) VALUES (@PropName, 'N', '')";
							SqlHelper.AddParameter(connection, dbCommand, "@PropName", settingsPropertyValue2.Property.Name);
						}
						else if (serializedValue2 is string)
						{
							dbCommand.CommandText = "INSERT INTO Settings (PropertyName, PropertyStoredAs, PropertyValue) VALUES (@PropName, 'S', @PropVal)";
							SqlHelper.AddParameter(connection, dbCommand, "@PropName", settingsPropertyValue2.Property.Name);
							SqlHelper.AddParameter(connection, dbCommand, "@PropVal", (string)serializedValue2);
						}
						else
						{
							dbCommand.CommandText = "INSERT INTO Settings (PropertyName, PropertyStoredAs, PropertyValue) VALUES (@PropName, 'B', @PropVal)";
							SqlHelper.AddParameter(connection, dbCommand, "@PropName", settingsPropertyValue2.Property.Name);
							SqlHelper.AddParameter(connection, dbCommand, "@PropVal", Convert.ToBase64String((byte[])serializedValue2));
						}
						dbCommand.ExecuteNonQuery();
					}
				}
				catch
				{
					if (dbTransaction != null)
					{
						dbTransaction.Rollback();
						dbTransaction = null;
					}
					throw;
				}
				finally
				{
					if (dbTransaction != null)
					{
						dbTransaction.Commit();
					}
				}
			}
			if (updateSaveTime)
			{
				this.SetIsCacheMoreFresh(true);
			}
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00033F50 File Offset: 0x00032150
		private bool GetNeedToReset()
		{
			if (ClientSettingsProvider._UsingFileSystemStore || ClientSettingsProvider._UsingIsolatedStore)
			{
				ClientData userClientData = ClientDataManager.GetUserClientData(Thread.CurrentPrincipal.Identity.Name, ClientSettingsProvider._UsingIsolatedStore);
				return userClientData.SettingsNeedReset;
			}
			string tagValue = this.GetTagValue("NeeedToDoReset");
			return tagValue != null && tagValue == "1";
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00033FA8 File Offset: 0x000321A8
		private void SetNeedToReset(bool fSet)
		{
			if (ClientSettingsProvider._UsingFileSystemStore || ClientSettingsProvider._UsingIsolatedStore)
			{
				ClientData userClientData = ClientDataManager.GetUserClientData(Thread.CurrentPrincipal.Identity.Name, ClientSettingsProvider._UsingIsolatedStore);
				userClientData.SettingsNeedReset = fSet;
				userClientData.Save();
				return;
			}
			this.SetTagValue("NeeedToDoReset", fSet ? "1" : "0");
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00034008 File Offset: 0x00032208
		private bool GetIsCacheMoreFresh()
		{
			if (ClientSettingsProvider._UsingFileSystemStore || ClientSettingsProvider._UsingIsolatedStore)
			{
				ClientData userClientData = ClientDataManager.GetUserClientData(Thread.CurrentPrincipal.Identity.Name, ClientSettingsProvider._UsingIsolatedStore);
				return userClientData.SettingsCacheIsMoreFresh;
			}
			string tagValue = this.GetTagValue("IsCacheMoreFresh");
			return tagValue != null && tagValue == "1";
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00034060 File Offset: 0x00032260
		private void SetIsCacheMoreFresh(bool fSet)
		{
			if (ClientSettingsProvider._UsingFileSystemStore || ClientSettingsProvider._UsingIsolatedStore)
			{
				ClientData userClientData = ClientDataManager.GetUserClientData(Thread.CurrentPrincipal.Identity.Name, ClientSettingsProvider._UsingIsolatedStore);
				userClientData.SettingsCacheIsMoreFresh = fSet;
				userClientData.Save();
				return;
			}
			this.SetTagValue("IsCacheMoreFresh", fSet ? "1" : "0");
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x000340C0 File Offset: 0x000322C0
		private string GetTagValue(string tagName)
		{
			string name = Thread.CurrentPrincipal.Identity.Name;
			string result;
			using (DbConnection connection = SqlHelper.GetConnection(name, this.GetConnectionString(), this._ConnectionStringProvider))
			{
				DbCommand dbCommand = connection.CreateCommand();
				dbCommand.CommandText = "SELECT PropertyValue FROM Settings WHERE PropertyName = @PropName AND PropertyStoredAs='I'";
				SqlHelper.AddParameter(connection, dbCommand, "@PropName", tagName);
				result = (dbCommand.ExecuteScalar() as string);
			}
			return result;
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00034138 File Offset: 0x00032338
		private void SetTagValue(string tagName, string tagValue)
		{
			string name = Thread.CurrentPrincipal.Identity.Name;
			using (DbConnection connection = SqlHelper.GetConnection(name, this.GetConnectionString(), this._ConnectionStringProvider))
			{
				DbCommand dbCommand = connection.CreateCommand();
				dbCommand.CommandText = "DELETE FROM Settings WHERE PropertyName = @PropName AND PropertyStoredAs='I'";
				SqlHelper.AddParameter(connection, dbCommand, "@PropName", tagName);
				dbCommand.ExecuteNonQuery();
				if (tagValue != null)
				{
					dbCommand = connection.CreateCommand();
					dbCommand.CommandText = "INSERT INTO Settings (PropertyName, PropertyStoredAs, PropertyValue) VALUES  (@PropName, 'I', @PropValue)";
					SqlHelper.AddParameter(connection, dbCommand, "@PropName", tagName);
					SqlHelper.AddParameter(connection, dbCommand, "@PropValue", tagValue);
					dbCommand.ExecuteNonQuery();
				}
			}
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x000341E0 File Offset: 0x000323E0
		private void RegisterForValidateUserEvent()
		{
			foreach (object obj in Membership.Providers)
			{
				MembershipProvider membershipProvider = (MembershipProvider)obj;
				EventInfo @event = membershipProvider.GetType().GetEvent("UserValidated");
				if (!(@event == null))
				{
					MethodInfo addMethod = @event.GetAddMethod();
					if (!(addMethod == null))
					{
						ParameterInfo[] parameters = addMethod.GetParameters();
						Delegate @delegate = Delegate.CreateDelegate(parameters[0].ParameterType, this, "OnUserValidated");
						addMethod.Invoke(membershipProvider, new object[]
						{
							@delegate
						});
					}
				}
			}
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00034290 File Offset: 0x00032490
		private void OnUserValidated(object src, UserValidatedEventArgs e)
		{
			this._NeedToDoReset = this.GetNeedToReset();
			if (this._Properties != null && this._Properties.Count > 0 && string.Compare(e.UserName, this._UserName, StringComparison.OrdinalIgnoreCase) != 0)
			{
				try
				{
					if (ClientSettingsProvider._SettingsBaseClass != null)
					{
						ClientSettingsProvider._SettingsBaseClass.Reload();
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x000342FC File Offset: 0x000324FC
		private void SetRemainingValuesToDefault()
		{
			foreach (object obj in this._Properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				if (this._PropertyValues[settingsProperty.Name] == null)
				{
					SettingsPropertyValue settingsPropertyValue = new SettingsPropertyValue(settingsProperty);
					settingsPropertyValue.SerializedValue = settingsProperty.DefaultValue;
					settingsPropertyValue.Deserialized = false;
					object propertyValue = settingsPropertyValue.PropertyValue;
					settingsPropertyValue.PropertyValue = propertyValue;
					this._PropertyValues.Add(settingsPropertyValue);
				}
			}
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0003439C File Offset: 0x0003259C
		private string GetConnectionString()
		{
			if (this._ConnectionString == null)
			{
				this._ConnectionString = SqlHelper.GetDefaultConnectionString();
			}
			return this._ConnectionString;
		}

		// Token: 0x0400040E RID: 1038
		private string _ConnectionString;

		// Token: 0x0400040F RID: 1039
		private string _ConnectionStringProvider = "";

		// Token: 0x04000410 RID: 1040
		private bool _NeedToDoReset;

		// Token: 0x04000411 RID: 1041
		private bool _HonorCookieExpiry;

		// Token: 0x04000412 RID: 1042
		private bool _firstTime = true;

		// Token: 0x04000413 RID: 1043
		private string _UserName = "";

		// Token: 0x04000414 RID: 1044
		private SettingsPropertyValueCollection _PropertyValues = new SettingsPropertyValueCollection();

		// Token: 0x04000415 RID: 1045
		private SettingsPropertyCollection _Properties;

		// Token: 0x04000416 RID: 1046
		private static Hashtable _KnownTypesHashtable = null;

		// Token: 0x04000417 RID: 1047
		private static Type[] _KnownTypesArray = null;

		// Token: 0x04000418 RID: 1048
		private static string _ServiceUri = "";

		// Token: 0x04000419 RID: 1049
		private static object _lock = new object();

		// Token: 0x0400041A RID: 1050
		private static bool _UsingFileSystemStore = false;

		// Token: 0x0400041B RID: 1051
		private static bool _UsingIsolatedStore = true;

		// Token: 0x0400041C RID: 1052
		private static bool _UsingWFCService = false;

		// Token: 0x0400041D RID: 1053
		private static ApplicationSettingsBase _SettingsBaseClass = null;
	}
}
