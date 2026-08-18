using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Xml.Serialization;

namespace System.Configuration
{
	// Token: 0x02000718 RID: 1816
	public class SettingsPropertyValue
	{
		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x060037A0 RID: 14240 RVA: 0x000EB931 File Offset: 0x000EA931
		public string Name
		{
			get
			{
				return this._Property.Name;
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x060037A1 RID: 14241 RVA: 0x000EB93E File Offset: 0x000EA93E
		// (set) Token: 0x060037A2 RID: 14242 RVA: 0x000EB946 File Offset: 0x000EA946
		public bool IsDirty
		{
			get
			{
				return this._IsDirty;
			}
			set
			{
				this._IsDirty = value;
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x060037A3 RID: 14243 RVA: 0x000EB94F File Offset: 0x000EA94F
		public SettingsProperty Property
		{
			get
			{
				return this._Property;
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x060037A4 RID: 14244 RVA: 0x000EB957 File Offset: 0x000EA957
		public bool UsingDefaultValue
		{
			get
			{
				return this._UsingDefaultValue;
			}
		}

		// Token: 0x060037A5 RID: 14245 RVA: 0x000EB95F File Offset: 0x000EA95F
		public SettingsPropertyValue(SettingsProperty property)
		{
			this._Property = property;
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x060037A6 RID: 14246 RVA: 0x000EB978 File Offset: 0x000EA978
		// (set) Token: 0x060037A7 RID: 14247 RVA: 0x000EB9EF File Offset: 0x000EA9EF
		public object PropertyValue
		{
			get
			{
				if (!this._Deserialized)
				{
					this._Value = this.Deserialize();
					this._Deserialized = true;
				}
				if (this._Value != null && !this.Property.PropertyType.IsPrimitive && !(this._Value is string) && !(this._Value is DateTime))
				{
					this._UsingDefaultValue = false;
					this._ChangedSinceLastSerialized = true;
					this._IsDirty = true;
				}
				return this._Value;
			}
			set
			{
				this._Value = value;
				this._IsDirty = true;
				this._ChangedSinceLastSerialized = true;
				this._Deserialized = true;
				this._UsingDefaultValue = false;
			}
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x060037A8 RID: 14248 RVA: 0x000EBA14 File Offset: 0x000EAA14
		// (set) Token: 0x060037A9 RID: 14249 RVA: 0x000EBA37 File Offset: 0x000EAA37
		public object SerializedValue
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
			get
			{
				if (this._ChangedSinceLastSerialized)
				{
					this._ChangedSinceLastSerialized = false;
					this._SerializedValue = this.SerializePropertyValue();
				}
				return this._SerializedValue;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
			set
			{
				this._UsingDefaultValue = false;
				this._SerializedValue = value;
			}
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x060037AA RID: 14250 RVA: 0x000EBA47 File Offset: 0x000EAA47
		// (set) Token: 0x060037AB RID: 14251 RVA: 0x000EBA4F File Offset: 0x000EAA4F
		public bool Deserialized
		{
			get
			{
				return this._Deserialized;
			}
			set
			{
				this._Deserialized = value;
			}
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x000EBA58 File Offset: 0x000EAA58
		private bool IsHostedInAspnet()
		{
			return AppDomain.CurrentDomain.GetData(".appDomain") != null;
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x000EBA70 File Offset: 0x000EAA70
		private object Deserialize()
		{
			object obj = null;
			if (this.SerializedValue != null)
			{
				try
				{
					if (this.SerializedValue is string)
					{
						obj = SettingsPropertyValue.GetObjectFromString(this.Property.PropertyType, this.Property.SerializeAs, (string)this.SerializedValue);
					}
					else
					{
						MemoryStream memoryStream = new MemoryStream((byte[])this.SerializedValue);
						try
						{
							obj = new BinaryFormatter().Deserialize(memoryStream);
						}
						finally
						{
							memoryStream.Close();
						}
					}
				}
				catch (Exception ex)
				{
					try
					{
						if (this.IsHostedInAspnet())
						{
							object[] args = new object[]
							{
								this.Property,
								this,
								ex
							};
							Type type = Type.GetType("System.Web.Management.WebBaseEvent, System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);
							type.InvokeMember("RaisePropertyDeserializationWebErrorEvent", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, null, args, CultureInfo.InvariantCulture);
						}
					}
					catch
					{
					}
				}
				if (obj != null && !this.Property.PropertyType.IsAssignableFrom(obj.GetType()))
				{
					obj = null;
				}
			}
			if (obj == null)
			{
				this._UsingDefaultValue = true;
				if (this.Property.DefaultValue == null || this.Property.DefaultValue.ToString() == "[null]")
				{
					if (this.Property.PropertyType.IsValueType)
					{
						return Activator.CreateInstance(this.Property.PropertyType);
					}
					return null;
				}
				else
				{
					if (!(this.Property.DefaultValue is string))
					{
						obj = this.Property.DefaultValue;
					}
					else
					{
						try
						{
							obj = SettingsPropertyValue.GetObjectFromString(this.Property.PropertyType, this.Property.SerializeAs, (string)this.Property.DefaultValue);
						}
						catch (Exception ex2)
						{
							throw new ArgumentException(SR.GetString("Could_not_create_from_default_value", new object[]
							{
								this.Property.Name,
								ex2.Message
							}));
						}
					}
					if (obj != null && !this.Property.PropertyType.IsAssignableFrom(obj.GetType()))
					{
						throw new ArgumentException(SR.GetString("Could_not_create_from_default_value_2", new object[]
						{
							this.Property.Name
						}));
					}
				}
			}
			if (obj == null)
			{
				if (this.Property.PropertyType == typeof(string))
				{
					obj = "";
				}
				else
				{
					try
					{
						obj = Activator.CreateInstance(this.Property.PropertyType);
					}
					catch
					{
					}
				}
			}
			return obj;
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x000EBCF8 File Offset: 0x000EACF8
		private static object GetObjectFromString(Type type, SettingsSerializeAs serializeAs, string attValue)
		{
			if (type == typeof(string) && (attValue == null || attValue.Length < 1 || serializeAs == SettingsSerializeAs.String))
			{
				return attValue;
			}
			if (attValue == null || attValue.Length < 1)
			{
				return null;
			}
			switch (serializeAs)
			{
			case SettingsSerializeAs.String:
			{
				TypeConverter converter = TypeDescriptor.GetConverter(type);
				if (converter != null && converter.CanConvertTo(typeof(string)) && converter.CanConvertFrom(typeof(string)))
				{
					return converter.ConvertFromInvariantString(attValue);
				}
				throw new ArgumentException(SR.GetString("Unable_to_convert_type_from_string", new object[]
				{
					type.ToString()
				}), "type");
			}
			case SettingsSerializeAs.Xml:
				break;
			case SettingsSerializeAs.Binary:
			{
				byte[] buffer = Convert.FromBase64String(attValue);
				MemoryStream memoryStream = null;
				try
				{
					memoryStream = new MemoryStream(buffer);
					return new BinaryFormatter().Deserialize(memoryStream);
				}
				finally
				{
					if (memoryStream != null)
					{
						memoryStream.Close();
					}
				}
				break;
			}
			default:
				return null;
			}
			StringReader textReader = new StringReader(attValue);
			XmlSerializer xmlSerializer = new XmlSerializer(type);
			return xmlSerializer.Deserialize(textReader);
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x000EBE08 File Offset: 0x000EAE08
		private object SerializePropertyValue()
		{
			if (this._Value == null)
			{
				return null;
			}
			if (this.Property.SerializeAs != SettingsSerializeAs.Binary)
			{
				return SettingsPropertyValue.ConvertObjectToString(this._Value, this.Property.PropertyType, this.Property.SerializeAs, this.Property.ThrowOnErrorSerializing);
			}
			MemoryStream memoryStream = new MemoryStream();
			object result;
			try
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, this._Value);
				result = memoryStream.ToArray();
			}
			finally
			{
				memoryStream.Close();
			}
			return result;
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x000EBE94 File Offset: 0x000EAE94
		private static string ConvertObjectToString(object propValue, Type type, SettingsSerializeAs serializeAs, bool throwOnError)
		{
			if (serializeAs == SettingsSerializeAs.ProviderSpecific)
			{
				if (type == typeof(string) || type.IsPrimitive)
				{
					serializeAs = SettingsSerializeAs.String;
				}
				else
				{
					serializeAs = SettingsSerializeAs.Xml;
				}
			}
			try
			{
				switch (serializeAs)
				{
				case SettingsSerializeAs.String:
				{
					TypeConverter converter = TypeDescriptor.GetConverter(type);
					if (converter != null && converter.CanConvertTo(typeof(string)) && converter.CanConvertFrom(typeof(string)))
					{
						return converter.ConvertToInvariantString(propValue);
					}
					throw new ArgumentException(SR.GetString("Unable_to_convert_type_to_string", new object[]
					{
						type.ToString()
					}), "type");
				}
				case SettingsSerializeAs.Xml:
					break;
				case SettingsSerializeAs.Binary:
				{
					MemoryStream memoryStream = new MemoryStream();
					try
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						binaryFormatter.Serialize(memoryStream, propValue);
						byte[] inArray = memoryStream.ToArray();
						return Convert.ToBase64String(inArray);
					}
					finally
					{
						memoryStream.Close();
					}
					break;
				}
				default:
					goto IL_100;
				}
				XmlSerializer xmlSerializer = new XmlSerializer(type);
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				xmlSerializer.Serialize(stringWriter, propValue);
				return stringWriter.ToString();
			}
			catch (Exception)
			{
				if (throwOnError)
				{
					throw;
				}
			}
			IL_100:
			return null;
		}

		// Token: 0x040031E2 RID: 12770
		private object _Value;

		// Token: 0x040031E3 RID: 12771
		private object _SerializedValue;

		// Token: 0x040031E4 RID: 12772
		private bool _Deserialized;

		// Token: 0x040031E5 RID: 12773
		private bool _IsDirty;

		// Token: 0x040031E6 RID: 12774
		private SettingsProperty _Property;

		// Token: 0x040031E7 RID: 12775
		private bool _ChangedSinceLastSerialized;

		// Token: 0x040031E8 RID: 12776
		private bool _UsingDefaultValue = true;
	}
}
