using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006EB RID: 1771
	[ConfigurationCollection(typeof(XPathMessageFilterElement))]
	public sealed class XPathMessageFilterElementCollection : ServiceModelConfigurationElementCollection<XPathMessageFilterElement>
	{
		// Token: 0x0600440F RID: 17423 RVA: 0x00100F93 File Offset: 0x000FF193
		public XPathMessageFilterElementCollection() : base(ConfigurationElementCollectionType.AddRemoveClearMap, null, new XPathMessageFilterElementComparer())
		{
		}

		// Token: 0x06004410 RID: 17424 RVA: 0x00100FA4 File Offset: 0x000FF1A4
		public override bool ContainsKey(object key)
		{
			if (key == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
			}
			string key2 = string.Empty;
			if (key.GetType().IsAssignableFrom(typeof(XPathMessageFilter)))
			{
				key2 = XPathMessageFilterElementComparer.ParseXPathString((XPathMessageFilter)key);
			}
			else
			{
				if (!key.GetType().IsAssignableFrom(typeof(string)))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ConfigInvalidKeyType", new object[]
					{
						"XPathMessageFilterElement",
						typeof(XPathMessageFilter).AssemblyQualifiedName,
						key.GetType().AssemblyQualifiedName
					})));
				}
				key2 = (string)key;
			}
			return base.ContainsKey(key2);
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x00101060 File Offset: 0x000FF260
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			XPathMessageFilterElement xpathMessageFilterElement = (XPathMessageFilterElement)element;
			if (xpathMessageFilterElement.Filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("element", SR.GetString("ConfigXPathFilterIsNull"));
			}
			return XPathMessageFilterElementComparer.ParseXPathString(xpathMessageFilterElement.Filter);
		}

		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x06004412 RID: 17426 RVA: 0x001010B4 File Offset: 0x000FF2B4
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return new ConfigurationPropertyCollection();
			}
		}

		// Token: 0x17001198 RID: 4504
		public override XPathMessageFilterElement this[object key]
		{
			get
			{
				if (key == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
				}
				if (!key.GetType().IsAssignableFrom(typeof(XPathMessageFilter)))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ConfigInvalidKeyType", new object[]
					{
						"XPathMessageFilterElement",
						typeof(XPathMessageFilter).AssemblyQualifiedName,
						key.GetType().AssemblyQualifiedName
					})));
				}
				XPathMessageFilterElement xpathMessageFilterElement = (XPathMessageFilterElement)base.BaseGet(key);
				if (xpathMessageFilterElement == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new KeyNotFoundException(SR.GetString("ConfigKeyNotFoundInElementCollection", new object[]
					{
						key.ToString()
					})));
				}
				return xpathMessageFilterElement;
			}
			set
			{
				if (this.IsReadOnly())
				{
					base.Add(value);
				}
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (key == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
				}
				if (!key.GetType().IsAssignableFrom(typeof(XPathMessageFilter)))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ConfigInvalidKeyType", new object[]
					{
						"XPathMessageFilterElement",
						typeof(XPathMessageFilter).AssemblyQualifiedName,
						key.GetType().AssemblyQualifiedName
					})));
				}
				string a = XPathMessageFilterElementComparer.ParseXPathString((XPathMessageFilter)key);
				string b = (string)this.GetElementKey(value);
				if (string.Equals(a, b, StringComparison.Ordinal))
				{
					if (base.BaseGet(key) != null)
					{
						base.BaseRemove(key);
					}
					base.Add(value);
					return;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("ConfigKeysDoNotMatch", new object[]
				{
					this.GetElementKey(value).ToString(),
					key.ToString()
				}));
			}
		}
	}
}
