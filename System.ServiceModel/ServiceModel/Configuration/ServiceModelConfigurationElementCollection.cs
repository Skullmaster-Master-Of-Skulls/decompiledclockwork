using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006CC RID: 1740
	public abstract class ServiceModelConfigurationElementCollection<ConfigurationElementType> : ConfigurationElementCollection where ConfigurationElementType : ConfigurationElement, new()
	{
		// Token: 0x06004344 RID: 17220 RVA: 0x000FE3B4 File Offset: 0x000FC5B4
		internal ServiceModelConfigurationElementCollection() : this(ConfigurationElementCollectionType.AddRemoveClearMap, null)
		{
		}

		// Token: 0x06004345 RID: 17221 RVA: 0x000FE3BE File Offset: 0x000FC5BE
		internal ServiceModelConfigurationElementCollection(ConfigurationElementCollectionType collectionType, string elementName)
		{
			this.collectionType = collectionType;
			this.elementName = elementName;
			if (!string.IsNullOrEmpty(elementName))
			{
				base.AddElementName = elementName;
			}
		}

		// Token: 0x06004346 RID: 17222 RVA: 0x000FE3E3 File Offset: 0x000FC5E3
		internal ServiceModelConfigurationElementCollection(ConfigurationElementCollectionType collectionType, string elementName, IComparer comparer) : base(comparer)
		{
			this.collectionType = collectionType;
			this.elementName = elementName;
		}

		// Token: 0x06004347 RID: 17223 RVA: 0x000FE3FC File Offset: 0x000FC5FC
		protected override void BaseAdd(ConfigurationElement element)
		{
			if (!this.IsReadOnly() && !this.ThrowOnDuplicate)
			{
				object elementKey = this.GetElementKey(element);
				if (this.ContainsKey(elementKey))
				{
					base.BaseRemove(elementKey);
				}
			}
			base.BaseAdd(element);
		}

		// Token: 0x06004348 RID: 17224 RVA: 0x000FE438 File Offset: 0x000FC638
		public void Add(ConfigurationElementType element)
		{
			if (!this.IsReadOnly() && element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			this.BaseAdd(element);
		}

		// Token: 0x06004349 RID: 17225 RVA: 0x000FE466 File Offset: 0x000FC666
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x0600434A RID: 17226 RVA: 0x000FE46E File Offset: 0x000FC66E
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return this.collectionType;
			}
		}

		// Token: 0x0600434B RID: 17227 RVA: 0x000FE478 File Offset: 0x000FC678
		public virtual bool ContainsKey(object key)
		{
			if (key != null)
			{
				return base.BaseGet(key) != null;
			}
			List<string> list = new List<string>();
			ConfigurationElement configurationElement = this.CreateNewElement();
			foreach (object obj in configurationElement.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				if (propertyInformation.IsKey)
				{
					list.Add(propertyInformation.Name);
				}
			}
			if (list.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
			}
			if (1 == list.Count)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigElementKeyNull", new object[]
				{
					list[0]
				})));
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < list.Count - 1; i++)
			{
				stringBuilder = stringBuilder.Append(list[i] + ", ");
			}
			stringBuilder = stringBuilder.Append(list[list.Count - 1]);
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigElementKeysNull", new object[]
			{
				list.ToString()
			})));
		}

		// Token: 0x0600434C RID: 17228 RVA: 0x000FE5C8 File Offset: 0x000FC7C8
		protected override ConfigurationElement CreateNewElement()
		{
			return Activator.CreateInstance<ConfigurationElementType>();
		}

		// Token: 0x0600434D RID: 17229 RVA: 0x000FE5D4 File Offset: 0x000FC7D4
		public void CopyTo(ConfigurationElementType[] array, int start)
		{
			if (array == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("array");
			}
			if (start < 0 || start >= array.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("start", SR.GetString("ConfigInvalidStartValue", new object[]
				{
					array.Length - 1,
					start
				}));
			}
			((ICollection)this).CopyTo(array, start);
		}

		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x0600434E RID: 17230 RVA: 0x000FE63C File Offset: 0x000FC83C
		protected override string ElementName
		{
			get
			{
				string text = this.elementName;
				if (string.IsNullOrEmpty(text))
				{
					text = base.ElementName;
				}
				return text;
			}
		}

		// Token: 0x0600434F RID: 17231 RVA: 0x000FE660 File Offset: 0x000FC860
		public int IndexOf(ConfigurationElementType element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			return base.BaseIndexOf(element);
		}

		// Token: 0x06004350 RID: 17232 RVA: 0x000FE686 File Offset: 0x000FC886
		public void Remove(ConfigurationElementType element)
		{
			if (!this.IsReadOnly() && element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			base.BaseRemove(this.GetElementKey(element));
		}

		// Token: 0x06004351 RID: 17233 RVA: 0x000FE6BA File Offset: 0x000FC8BA
		public void RemoveAt(object key)
		{
			if (!this.IsReadOnly() && key == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
			}
			base.BaseRemove(key);
		}

		// Token: 0x06004352 RID: 17234 RVA: 0x000FE6DE File Offset: 0x000FC8DE
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x17001167 RID: 4455
		public virtual ConfigurationElementType this[object key]
		{
			get
			{
				if (key == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
				}
				ConfigurationElementType configurationElementType = (ConfigurationElementType)((object)base.BaseGet(key));
				if (configurationElementType == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new KeyNotFoundException(SR.GetString("ConfigKeyNotFoundInElementCollection", new object[]
					{
						key.ToString()
					})));
				}
				return configurationElementType;
			}
			set
			{
				if (this.IsReadOnly())
				{
					this.Add(value);
				}
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (key == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
				}
				if (this.GetElementKey(value).ToString().Equals((string)key, StringComparison.Ordinal))
				{
					if (base.BaseGet(key) != null)
					{
						base.BaseRemove(key);
					}
					this.Add(value);
					return;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("ConfigKeysDoNotMatch", new object[]
				{
					this.GetElementKey(value).ToString(),
					key.ToString()
				}));
			}
		}

		// Token: 0x17001168 RID: 4456
		public ConfigurationElementType this[int index]
		{
			get
			{
				return (ConfigurationElementType)((object)base.BaseGet(index));
			}
			set
			{
				if (!this.IsReadOnly() && !this.ThrowOnDuplicate && base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		// Token: 0x04002D11 RID: 11537
		private ConfigurationElementCollectionType collectionType;

		// Token: 0x04002D12 RID: 11538
		private string elementName;
	}
}
