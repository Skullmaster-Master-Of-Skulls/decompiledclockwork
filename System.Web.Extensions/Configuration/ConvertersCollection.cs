using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Security;
using System.Web.Compilation;
using System.Web.Resources;
using System.Web.Script.Serialization;

namespace System.Web.Configuration
{
	// Token: 0x020000E9 RID: 233
	[ConfigurationCollection(typeof(Converter))]
	public class ConvertersCollection : ConfigurationElementCollection
	{
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0002B090 File Offset: 0x00029290
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ConvertersCollection._properties;
			}
		}

		// Token: 0x170004F5 RID: 1269
		public Converter this[int index]
		{
			get
			{
				return (Converter)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0002B0BF File Offset: 0x000292BF
		public void Add(Converter converter)
		{
			this.BaseAdd(converter);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0002B0C8 File Offset: 0x000292C8
		public void Remove(Converter converter)
		{
			base.BaseRemove(this.GetElementKey(converter));
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0002B0D7 File Offset: 0x000292D7
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0002B0DF File Offset: 0x000292DF
		protected override ConfigurationElement CreateNewElement()
		{
			return new Converter();
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0002B0E6 File Offset: 0x000292E6
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((Converter)element).Name;
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0002B0F4 File Offset: 0x000292F4
		[SecuritySafeCritical]
		internal JavaScriptConverter[] CreateConverters()
		{
			List<JavaScriptConverter> list = new List<JavaScriptConverter>();
			foreach (object obj in this)
			{
				Converter converter = (Converter)obj;
				Type type = BuildManager.GetType(converter.Type, false);
				if (type == null)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.ConvertersCollection_UnknownType, new object[]
					{
						converter.Type
					}));
				}
				if (!typeof(JavaScriptConverter).IsAssignableFrom(type))
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.ConvertersCollection_NotJavaScriptConverter, new object[]
					{
						type.Name
					}));
				}
				list.Add((JavaScriptConverter)Activator.CreateInstance(type));
			}
			return list.ToArray();
		}

		// Token: 0x0400038A RID: 906
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
