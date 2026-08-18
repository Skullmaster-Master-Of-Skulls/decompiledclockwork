using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000003 RID: 3
	public class ConfigurationElement
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002320 File Offset: 0x00001320
		protected internal ConfigurationElement()
		{
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002328 File Offset: 0x00001328
		internal IAppHostElement AppHostElement
		{
			get
			{
				return this._appHostElement;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002330 File Offset: 0x00001330
		public ConfigurationAttributeCollection Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new ConfigurationAttributeCollection(this.AppHostElement.Properties, this);
				}
				return this._attributes;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002358 File Offset: 0x00001358
		public ConfigurationChildElementCollection ChildElements
		{
			get
			{
				if (this._childElements == null)
				{
					IAppHostChildElementCollection childElements = this.AppHostElement.ChildElements;
					if (childElements != null)
					{
						this._childElements = new ConfigurationChildElementCollection(this.Configuration, childElements);
					}
					else
					{
						this._childElements = ConfigurationChildElementCollection.Empty;
					}
				}
				return this._childElements;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000023A1 File Offset: 0x000013A1
		internal Configuration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000023A9 File Offset: 0x000013A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string ElementTagName
		{
			get
			{
				return this.AppHostElement.Name;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000023B8 File Offset: 0x000013B8
		private ICollection<IAppHostPropertySchema> InternalSchema
		{
			get
			{
				if (this._schema == null)
				{
					this._schema = new List<IAppHostPropertySchema>();
					IAppHostPropertyCollection properties = this.AppHostElement.Properties;
					uint count = properties.Count;
					for (uint num = 0U; num < count; num += 1U)
					{
						this._schema.Add(properties[num].Schema);
					}
				}
				return this._schema;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000241C File Offset: 0x0000141C
		public bool IsLocallyStored
		{
			get
			{
				if (this is ConfigurationSection)
				{
					return string.Equals((string)this.AppHostElement.GetMetadata("deepestPathSet"), this._configuration.ConfigurationPathToEdit, StringComparison.OrdinalIgnoreCase);
				}
				bool result;
				try
				{
					result = string.Equals((string)this.AppHostElement.GetMetadata("collectionItemFileConfigPath"), this._configuration.ConfigurationPathToEdit, StringComparison.OrdinalIgnoreCase);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147024846)
					{
						throw;
					}
					result = (bool)this.AppHostElement.GetMetadata("isPresent");
				}
				return result;
			}
		}

		// Token: 0x17000021 RID: 33
		public object this[string attributeName]
		{
			get
			{
				return this.GetAttributeValue(attributeName);
			}
			set
			{
				this.SetAttributeValue(attributeName, value);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000024D0 File Offset: 0x000014D0
		public ConfigurationMethodCollection Methods
		{
			get
			{
				if (this._methods == null)
				{
					IAppHostMethodCollection methods = this.AppHostElement.Methods;
					if (methods == null)
					{
						return null;
					}
					this._methods = new ConfigurationMethodCollection(this.Configuration, methods);
				}
				return this._methods;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002510 File Offset: 0x00001510
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public IDictionary<string, string> RawAttributes
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				ICollection<IAppHostPropertySchema> internalSchema = this.InternalSchema;
				foreach (IAppHostPropertySchema appHostPropertySchema in internalSchema)
				{
					string name = appHostPropertySchema.Name;
					dictionary.Add(name, this[name].ToString());
				}
				return dictionary;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002580 File Offset: 0x00001580
		public ConfigurationElementSchema Schema
		{
			get
			{
				if (this._elementSchema == null)
				{
					IAppHostElementSchema schema = this.AppHostElement.Schema;
					if (schema != null)
					{
						this._elementSchema = new ConfigurationElementSchema(schema);
					}
				}
				return this._elementSchema;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025B8 File Offset: 0x000015B8
		[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.MemberAccess)]
		internal static ConfigurationElement CreateStronglyTypedWrapper(Type elementType)
		{
			if (!typeof(ConfigurationElement).IsAssignableFrom(elementType))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, Resources.InvalidType, new object[]
				{
					elementType.FullName
				}));
			}
			ConstructorInfo constructor = elementType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null);
			if (constructor == null)
			{
				throw new InvalidOperationException(Resources.ConstructorNotFound);
			}
			return (ConfigurationElement)constructor.Invoke(null);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002628 File Offset: 0x00001628
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Delete()
		{
			this.SetDirty();
			this.AppHostElement.Clear();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000263C File Offset: 0x0000163C
		internal void ExecuteMethod(string methodName)
		{
			ConfigurationMethod configurationMethod = this.Methods[methodName];
			if (configurationMethod == null)
			{
				throw new InvalidOperationException();
			}
			ConfigurationMethodInstance configurationMethodInstance = configurationMethod.CreateInstance();
			configurationMethodInstance.Execute();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000266C File Offset: 0x0000166C
		public ConfigurationAttribute GetAttribute(string attributeName)
		{
			if (string.IsNullOrEmpty(attributeName))
			{
				throw new ArgumentNullException("attributeName");
			}
			IAppHostProperty propertyByName = this.AppHostElement.GetPropertyByName(attributeName);
			return new ConfigurationAttribute(propertyByName, this);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026A0 File Offset: 0x000016A0
		public object GetAttributeValue(string attributeName)
		{
			if (string.IsNullOrEmpty(attributeName))
			{
				throw new ArgumentNullException("attributeName");
			}
			IAppHostProperty propertyByName = this.AppHostElement.GetPropertyByName(attributeName);
			return ConfigurationElement.GetPropertyValue(propertyByName);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026D4 File Offset: 0x000016D4
		public ConfigurationElement GetChildElement(string elementName)
		{
			if (string.IsNullOrEmpty(elementName))
			{
				throw new ArgumentNullException("elementName");
			}
			IAppHostElement elementByName = this.AppHostElement.GetElementByName(elementName);
			if (elementByName == null)
			{
				return null;
			}
			ConfigurationElement configurationElement = new ConfigurationElement();
			configurationElement.Initialize(this.Configuration, elementByName);
			return configurationElement;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000271C File Offset: 0x0000171C
		public ConfigurationElement GetChildElement(string elementName, Type elementType)
		{
			if (string.IsNullOrEmpty(elementName))
			{
				throw new ArgumentNullException("elementName");
			}
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			IAppHostElement elementByName = this.AppHostElement.GetElementByName(elementName);
			if (elementByName == null)
			{
				return null;
			}
			ConfigurationElement configurationElement = ConfigurationElement.CreateStronglyTypedWrapper(elementType);
			configurationElement.Initialize(this.Configuration, elementByName);
			return configurationElement;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002774 File Offset: 0x00001774
		public ConfigurationElementCollection GetCollection(string collectionName)
		{
			if (string.IsNullOrEmpty(collectionName))
			{
				throw new ArgumentNullException("collectionName");
			}
			IAppHostElement elementByName = this.AppHostElement.GetElementByName(collectionName);
			if (elementByName == null || elementByName.Collection == null)
			{
				return null;
			}
			ConfigurationElementCollection configurationElementCollection = new ConfigurationElementCollection();
			configurationElementCollection.Initialize(this.Configuration, elementByName);
			return configurationElementCollection;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000027C4 File Offset: 0x000017C4
		public ConfigurationElement GetCollection(string collectionName, Type collectionType)
		{
			if (string.IsNullOrEmpty(collectionName))
			{
				throw new ArgumentNullException("collectionName");
			}
			if (collectionType == null)
			{
				throw new ArgumentNullException("collectionType");
			}
			IAppHostElement elementByName = this.AppHostElement.GetElementByName(collectionName);
			if (elementByName == null || elementByName.Collection == null)
			{
				return null;
			}
			ConfigurationElement configurationElement = ConfigurationElement.CreateStronglyTypedWrapper(collectionType);
			configurationElement.Initialize(this.Configuration, elementByName);
			return configurationElement;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002824 File Offset: 0x00001824
		public ConfigurationElementCollection GetCollection()
		{
			if (this.AppHostElement.Collection == null)
			{
				return null;
			}
			ConfigurationElementCollection configurationElementCollection = new ConfigurationElementCollection();
			configurationElementCollection.Initialize(this.Configuration, this.AppHostElement);
			return configurationElementCollection;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000285C File Offset: 0x0000185C
		public ConfigurationElement GetCollection(Type collectionType)
		{
			if (collectionType == null)
			{
				throw new ArgumentNullException("collectionType");
			}
			if (this.AppHostElement.Collection == null)
			{
				return null;
			}
			ConfigurationElement configurationElement = ConfigurationElement.CreateStronglyTypedWrapper(collectionType);
			configurationElement.Initialize(this.Configuration, this.AppHostElement);
			return configurationElement;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000028A0 File Offset: 0x000018A0
		public object GetMetadata(string metadataType)
		{
			return this.AppHostElement.GetMetadata(metadataType);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000028B0 File Offset: 0x000018B0
		internal static object GetPropertyValue(IAppHostProperty property)
		{
			object value = property.Value;
			if (value is uint)
			{
				return (long)((ulong)((uint)value));
			}
			if (value is ulong)
			{
				IAppHostPropertySchema schema = property.Schema;
				if (string.Equals(schema.Type, "timeSpan", StringComparison.OrdinalIgnoreCase))
				{
					return new TimeSpan((long)((ulong)value));
				}
			}
			return value;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000290D File Offset: 0x0000190D
		internal void Initialize(Configuration configuration, IAppHostElement appHostElement)
		{
			this._configuration = configuration;
			this._appHostElement = appHostElement;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000291D File Offset: 0x0000191D
		internal void InitializeMethodElement(IAppHostElement appHostElement)
		{
			this._configuration = null;
			this._isMethodElement = true;
			this._appHostElement = appHostElement;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002934 File Offset: 0x00001934
		public void SetAttributeValue(string attributeName, object value)
		{
			if (string.IsNullOrEmpty(attributeName))
			{
				throw new ArgumentNullException("attributeName");
			}
			IAppHostProperty propertyByName = this.AppHostElement.GetPropertyByName(attributeName);
			IAppHostPropertySchema schema = propertyByName.Schema;
			string type = schema.Type;
			if (string.Equals(type, "timeSpan", StringComparison.OrdinalIgnoreCase))
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				value = ((TimeSpan)value).Ticks;
			}
			else if (string.Equals(type, "uint", StringComparison.OrdinalIgnoreCase) && !(value is uint))
			{
				if (value is long)
				{
					long num = (long)value;
					if (num > (long)((ulong)-1) || num < 0L)
					{
						throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Resources.UIntArgumentOutOfRange, new object[]
						{
							attributeName,
							0U,
							uint.MaxValue
						}));
					}
					value = (uint)num;
				}
				else
				{
					int num2 = (int)value;
					if ((long)num2 < 0L)
					{
						throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Resources.UIntArgumentOutOfRange, new object[]
						{
							attributeName,
							0U,
							uint.MaxValue
						}));
					}
					value = (uint)((int)value);
				}
			}
			this.SetDirty();
			propertyByName.Value = value;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A89 File Offset: 0x00001A89
		internal void SetDirty()
		{
			if (this._isMethodElement)
			{
				return;
			}
			if (this._configuration == null)
			{
				throw new InvalidOperationException(Resources.ConfigurationReadOnly);
			}
			this._configuration.SetDirty();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002AB2 File Offset: 0x00001AB2
		public void SetMetadata(string metadataType, object value)
		{
			this.SetDirty();
			this.AppHostElement.SetMetadata(metadataType, value);
		}

		// Token: 0x04000003 RID: 3
		private const BindingFlags DefaultBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04000004 RID: 4
		private IAppHostElement _appHostElement;

		// Token: 0x04000005 RID: 5
		private Configuration _configuration;

		// Token: 0x04000006 RID: 6
		private ICollection<IAppHostPropertySchema> _schema;

		// Token: 0x04000007 RID: 7
		private ConfigurationElementSchema _elementSchema;

		// Token: 0x04000008 RID: 8
		private ConfigurationAttributeCollection _attributes;

		// Token: 0x04000009 RID: 9
		private ConfigurationChildElementCollection _childElements;

		// Token: 0x0400000A RID: 10
		private ConfigurationMethodCollection _methods;

		// Token: 0x0400000B RID: 11
		private bool _isMethodElement;
	}
}
