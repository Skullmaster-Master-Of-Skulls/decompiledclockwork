using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Configuration;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005B2 RID: 1458
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class TypeConverter
	{
		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06003642 RID: 13890 RVA: 0x000ECB8C File Offset: 0x000EAD8C
		private static bool UseCompatibleTypeConversion
		{
			get
			{
				if (TypeConverter.firstLoadAppSetting)
				{
					object obj = TypeConverter.loadAppSettingLock;
					lock (obj)
					{
						if (TypeConverter.firstLoadAppSetting)
						{
							string text = ConfigurationManager.AppSettings["UseCompatibleTypeConverterBehavior"];
							try
							{
								if (!string.IsNullOrEmpty(text))
								{
									TypeConverter.useCompatibleTypeConversion = bool.Parse(text.Trim());
								}
							}
							catch
							{
								TypeConverter.useCompatibleTypeConversion = false;
							}
							TypeConverter.firstLoadAppSetting = false;
						}
					}
				}
				return TypeConverter.useCompatibleTypeConversion;
			}
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x000ECC2C File Offset: 0x000EAE2C
		public bool CanConvertFrom(Type sourceType)
		{
			return this.CanConvertFrom(null, sourceType);
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x000ECC36 File Offset: 0x000EAE36
		public virtual bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(InstanceDescriptor);
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x000ECC4D File Offset: 0x000EAE4D
		public bool CanConvertTo(Type destinationType)
		{
			return this.CanConvertTo(null, destinationType);
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x000ECC57 File Offset: 0x000EAE57
		public virtual bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string);
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x000ECC69 File Offset: 0x000EAE69
		public object ConvertFrom(object value)
		{
			return this.ConvertFrom(null, CultureInfo.CurrentCulture, value);
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x000ECC78 File Offset: 0x000EAE78
		public virtual object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			InstanceDescriptor instanceDescriptor = value as InstanceDescriptor;
			if (instanceDescriptor != null)
			{
				return instanceDescriptor.Invoke();
			}
			throw this.GetConvertFromException(value);
		}

		// Token: 0x06003649 RID: 13897 RVA: 0x000ECC9D File Offset: 0x000EAE9D
		public object ConvertFromInvariantString(string text)
		{
			return this.ConvertFromString(null, CultureInfo.InvariantCulture, text);
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x000ECCAC File Offset: 0x000EAEAC
		public object ConvertFromInvariantString(ITypeDescriptorContext context, string text)
		{
			return this.ConvertFromString(context, CultureInfo.InvariantCulture, text);
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x000ECCBB File Offset: 0x000EAEBB
		public object ConvertFromString(string text)
		{
			return this.ConvertFrom(null, null, text);
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x000ECCC6 File Offset: 0x000EAEC6
		public object ConvertFromString(ITypeDescriptorContext context, string text)
		{
			return this.ConvertFrom(context, CultureInfo.CurrentCulture, text);
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x000ECCD5 File Offset: 0x000EAED5
		public object ConvertFromString(ITypeDescriptorContext context, CultureInfo culture, string text)
		{
			return this.ConvertFrom(context, culture, text);
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x000ECCE0 File Offset: 0x000EAEE0
		public object ConvertTo(object value, Type destinationType)
		{
			return this.ConvertTo(null, null, value, destinationType);
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x000ECCEC File Offset: 0x000EAEEC
		public virtual object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(string)))
			{
				throw this.GetConvertToException(value, destinationType);
			}
			if (value == null)
			{
				return string.Empty;
			}
			if (culture != null && culture != CultureInfo.CurrentCulture)
			{
				IFormattable formattable = value as IFormattable;
				if (formattable != null)
				{
					return formattable.ToString(null, culture);
				}
			}
			return value.ToString();
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x000ECD58 File Offset: 0x000EAF58
		public string ConvertToInvariantString(object value)
		{
			return this.ConvertToString(null, CultureInfo.InvariantCulture, value);
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x000ECD67 File Offset: 0x000EAF67
		public string ConvertToInvariantString(ITypeDescriptorContext context, object value)
		{
			return this.ConvertToString(context, CultureInfo.InvariantCulture, value);
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x000ECD76 File Offset: 0x000EAF76
		public string ConvertToString(object value)
		{
			return (string)this.ConvertTo(null, CultureInfo.CurrentCulture, value, typeof(string));
		}

		// Token: 0x06003653 RID: 13907 RVA: 0x000ECD94 File Offset: 0x000EAF94
		public string ConvertToString(ITypeDescriptorContext context, object value)
		{
			return (string)this.ConvertTo(context, CultureInfo.CurrentCulture, value, typeof(string));
		}

		// Token: 0x06003654 RID: 13908 RVA: 0x000ECDB2 File Offset: 0x000EAFB2
		public string ConvertToString(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			return (string)this.ConvertTo(context, culture, value, typeof(string));
		}

		// Token: 0x06003655 RID: 13909 RVA: 0x000ECDCC File Offset: 0x000EAFCC
		public object CreateInstance(IDictionary propertyValues)
		{
			return this.CreateInstance(null, propertyValues);
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x000ECDD6 File Offset: 0x000EAFD6
		public virtual object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return null;
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x000ECDDC File Offset: 0x000EAFDC
		protected Exception GetConvertFromException(object value)
		{
			string text;
			if (value == null)
			{
				text = SR.GetString("ToStringNull");
			}
			else
			{
				text = value.GetType().FullName;
			}
			throw new NotSupportedException(SR.GetString("ConvertFromException", new object[]
			{
				base.GetType().Name,
				text
			}));
		}

		// Token: 0x06003658 RID: 13912 RVA: 0x000ECE2C File Offset: 0x000EB02C
		protected Exception GetConvertToException(object value, Type destinationType)
		{
			string text;
			if (value == null)
			{
				text = SR.GetString("ToStringNull");
			}
			else
			{
				text = value.GetType().FullName;
			}
			throw new NotSupportedException(SR.GetString("ConvertToException", new object[]
			{
				base.GetType().Name,
				text,
				destinationType.FullName
			}));
		}

		// Token: 0x06003659 RID: 13913 RVA: 0x000ECE85 File Offset: 0x000EB085
		public bool GetCreateInstanceSupported()
		{
			return this.GetCreateInstanceSupported(null);
		}

		// Token: 0x0600365A RID: 13914 RVA: 0x000ECE8E File Offset: 0x000EB08E
		public virtual bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x0600365B RID: 13915 RVA: 0x000ECE91 File Offset: 0x000EB091
		public PropertyDescriptorCollection GetProperties(object value)
		{
			return this.GetProperties(null, value);
		}

		// Token: 0x0600365C RID: 13916 RVA: 0x000ECE9B File Offset: 0x000EB09B
		public PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value)
		{
			return this.GetProperties(context, value, new Attribute[]
			{
				BrowsableAttribute.Yes
			});
		}

		// Token: 0x0600365D RID: 13917 RVA: 0x000ECEB3 File Offset: 0x000EB0B3
		public virtual PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return null;
		}

		// Token: 0x0600365E RID: 13918 RVA: 0x000ECEB6 File Offset: 0x000EB0B6
		public bool GetPropertiesSupported()
		{
			return this.GetPropertiesSupported(null);
		}

		// Token: 0x0600365F RID: 13919 RVA: 0x000ECEBF File Offset: 0x000EB0BF
		public virtual bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06003660 RID: 13920 RVA: 0x000ECEC2 File Offset: 0x000EB0C2
		public ICollection GetStandardValues()
		{
			return this.GetStandardValues(null);
		}

		// Token: 0x06003661 RID: 13921 RVA: 0x000ECECB File Offset: 0x000EB0CB
		public virtual TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return null;
		}

		// Token: 0x06003662 RID: 13922 RVA: 0x000ECECE File Offset: 0x000EB0CE
		public bool GetStandardValuesExclusive()
		{
			return this.GetStandardValuesExclusive(null);
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x000ECED7 File Offset: 0x000EB0D7
		public virtual bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x000ECEDA File Offset: 0x000EB0DA
		public bool GetStandardValuesSupported()
		{
			return this.GetStandardValuesSupported(null);
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x000ECEE3 File Offset: 0x000EB0E3
		public virtual bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x000ECEE6 File Offset: 0x000EB0E6
		public bool IsValid(object value)
		{
			return this.IsValid(null, value);
		}

		// Token: 0x06003667 RID: 13927 RVA: 0x000ECEF0 File Offset: 0x000EB0F0
		public virtual bool IsValid(ITypeDescriptorContext context, object value)
		{
			if (TypeConverter.UseCompatibleTypeConversion)
			{
				return true;
			}
			bool result = true;
			try
			{
				if (value == null || this.CanConvertFrom(context, value.GetType()))
				{
					this.ConvertFrom(context, CultureInfo.InvariantCulture, value);
				}
				else
				{
					result = false;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06003668 RID: 13928 RVA: 0x000ECF44 File Offset: 0x000EB144
		protected PropertyDescriptorCollection SortProperties(PropertyDescriptorCollection props, string[] names)
		{
			props.Sort(names);
			return props;
		}

		// Token: 0x04002AA6 RID: 10918
		private const string s_UseCompatibleTypeConverterBehavior = "UseCompatibleTypeConverterBehavior";

		// Token: 0x04002AA7 RID: 10919
		private static volatile bool useCompatibleTypeConversion = false;

		// Token: 0x04002AA8 RID: 10920
		private static volatile bool firstLoadAppSetting = true;

		// Token: 0x04002AA9 RID: 10921
		private static object loadAppSettingLock = new object();

		// Token: 0x0200089E RID: 2206
		protected abstract class SimplePropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x060045C9 RID: 17865 RVA: 0x00124192 File Offset: 0x00122392
			protected SimplePropertyDescriptor(Type componentType, string name, Type propertyType) : this(componentType, name, propertyType, new Attribute[0])
			{
			}

			// Token: 0x060045CA RID: 17866 RVA: 0x001241A3 File Offset: 0x001223A3
			protected SimplePropertyDescriptor(Type componentType, string name, Type propertyType, Attribute[] attributes) : base(name, attributes)
			{
				this.componentType = componentType;
				this.propertyType = propertyType;
			}

			// Token: 0x17000FCA RID: 4042
			// (get) Token: 0x060045CB RID: 17867 RVA: 0x001241BC File Offset: 0x001223BC
			public override Type ComponentType
			{
				get
				{
					return this.componentType;
				}
			}

			// Token: 0x17000FCB RID: 4043
			// (get) Token: 0x060045CC RID: 17868 RVA: 0x001241C4 File Offset: 0x001223C4
			public override bool IsReadOnly
			{
				get
				{
					return this.Attributes.Contains(ReadOnlyAttribute.Yes);
				}
			}

			// Token: 0x17000FCC RID: 4044
			// (get) Token: 0x060045CD RID: 17869 RVA: 0x001241D6 File Offset: 0x001223D6
			public override Type PropertyType
			{
				get
				{
					return this.propertyType;
				}
			}

			// Token: 0x060045CE RID: 17870 RVA: 0x001241E0 File Offset: 0x001223E0
			public override bool CanResetValue(object component)
			{
				DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)this.Attributes[typeof(DefaultValueAttribute)];
				return defaultValueAttribute != null && defaultValueAttribute.Value.Equals(this.GetValue(component));
			}

			// Token: 0x060045CF RID: 17871 RVA: 0x00124220 File Offset: 0x00122420
			public override void ResetValue(object component)
			{
				DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)this.Attributes[typeof(DefaultValueAttribute)];
				if (defaultValueAttribute != null)
				{
					this.SetValue(component, defaultValueAttribute.Value);
				}
			}

			// Token: 0x060045D0 RID: 17872 RVA: 0x00124258 File Offset: 0x00122458
			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}

			// Token: 0x040037F5 RID: 14325
			private Type componentType;

			// Token: 0x040037F6 RID: 14326
			private Type propertyType;
		}

		// Token: 0x0200089F RID: 2207
		public class StandardValuesCollection : ICollection, IEnumerable
		{
			// Token: 0x060045D1 RID: 17873 RVA: 0x0012425C File Offset: 0x0012245C
			public StandardValuesCollection(ICollection values)
			{
				if (values == null)
				{
					values = new object[0];
				}
				Array array = values as Array;
				if (array != null)
				{
					this.valueArray = array;
				}
				this.values = values;
			}

			// Token: 0x17000FCD RID: 4045
			// (get) Token: 0x060045D2 RID: 17874 RVA: 0x00124292 File Offset: 0x00122492
			public int Count
			{
				get
				{
					if (this.valueArray != null)
					{
						return this.valueArray.Length;
					}
					return this.values.Count;
				}
			}

			// Token: 0x17000FCE RID: 4046
			public object this[int index]
			{
				get
				{
					if (this.valueArray != null)
					{
						return this.valueArray.GetValue(index);
					}
					IList list = this.values as IList;
					if (list != null)
					{
						return list[index];
					}
					this.valueArray = new object[this.values.Count];
					this.values.CopyTo(this.valueArray, 0);
					return this.valueArray.GetValue(index);
				}
			}

			// Token: 0x060045D4 RID: 17876 RVA: 0x00124321 File Offset: 0x00122521
			public void CopyTo(Array array, int index)
			{
				this.values.CopyTo(array, index);
			}

			// Token: 0x060045D5 RID: 17877 RVA: 0x00124330 File Offset: 0x00122530
			public IEnumerator GetEnumerator()
			{
				return this.values.GetEnumerator();
			}

			// Token: 0x17000FCF RID: 4047
			// (get) Token: 0x060045D6 RID: 17878 RVA: 0x0012433D File Offset: 0x0012253D
			int ICollection.Count
			{
				get
				{
					return this.Count;
				}
			}

			// Token: 0x17000FD0 RID: 4048
			// (get) Token: 0x060045D7 RID: 17879 RVA: 0x00124345 File Offset: 0x00122545
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000FD1 RID: 4049
			// (get) Token: 0x060045D8 RID: 17880 RVA: 0x00124348 File Offset: 0x00122548
			object ICollection.SyncRoot
			{
				get
				{
					return null;
				}
			}

			// Token: 0x060045D9 RID: 17881 RVA: 0x0012434B File Offset: 0x0012254B
			void ICollection.CopyTo(Array array, int index)
			{
				this.CopyTo(array, index);
			}

			// Token: 0x060045DA RID: 17882 RVA: 0x00124355 File Offset: 0x00122555
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040037F7 RID: 14327
			private ICollection values;

			// Token: 0x040037F8 RID: 14328
			private Array valueArray;
		}
	}
}
