using System;
using System.Collections;
using System.Configuration;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.Configuration
{
	// Token: 0x02000764 RID: 1892
	[ConfigurationCollection(typeof(TransformerInfo))]
	public sealed class TransformerInfoCollection : ConfigurationElementCollection
	{
		// Token: 0x17001AB7 RID: 6839
		// (get) Token: 0x06005B30 RID: 23344 RVA: 0x0013CA45 File Offset: 0x0013AC45
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TransformerInfoCollection._properties;
			}
		}

		// Token: 0x17001AB8 RID: 6840
		public TransformerInfo this[int index]
		{
			get
			{
				return (TransformerInfo)base.BaseGet(index);
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

		// Token: 0x06005B33 RID: 23347 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(TransformerInfo transformerInfo)
		{
			this.BaseAdd(transformerInfo);
		}

		// Token: 0x06005B34 RID: 23348 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06005B35 RID: 23349 RVA: 0x0013CA5A File Offset: 0x0013AC5A
		protected override ConfigurationElement CreateNewElement()
		{
			return new TransformerInfo();
		}

		// Token: 0x06005B36 RID: 23350 RVA: 0x00117E19 File Offset: 0x00116019
		public void Remove(string s)
		{
			base.BaseRemove(s);
		}

		// Token: 0x06005B37 RID: 23351 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x06005B38 RID: 23352 RVA: 0x0013CA61 File Offset: 0x0013AC61
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((TransformerInfo)element).Name;
		}

		// Token: 0x06005B39 RID: 23353 RVA: 0x0013CA70 File Offset: 0x0013AC70
		internal Hashtable GetTransformerEntries()
		{
			if (this._transformerEntries == null)
			{
				lock (this)
				{
					if (this._transformerEntries == null)
					{
						this._transformerEntries = new Hashtable(StringComparer.OrdinalIgnoreCase);
						foreach (object obj in this)
						{
							TransformerInfo transformerInfo = (TransformerInfo)obj;
							Type type = ConfigUtil.GetType(transformerInfo.Type, "type", transformerInfo);
							if (!type.IsSubclassOf(typeof(WebPartTransformer)))
							{
								throw new ConfigurationErrorsException(SR.GetString("Type_doesnt_inherit_from_type", new object[]
								{
									transformerInfo.Type,
									typeof(WebPartTransformer).FullName
								}), transformerInfo.ElementInformation.Properties["type"].Source, transformerInfo.ElementInformation.Properties["type"].LineNumber);
							}
							Type consumerType;
							Type providerType;
							try
							{
								consumerType = WebPartTransformerAttribute.GetConsumerType(type);
								providerType = WebPartTransformerAttribute.GetProviderType(type);
							}
							catch (Exception ex)
							{
								throw new ConfigurationErrorsException(SR.GetString("Transformer_attribute_error", new object[]
								{
									ex.Message
								}), ex, transformerInfo.ElementInformation.Properties["type"].Source, transformerInfo.ElementInformation.Properties["type"].LineNumber);
							}
							if (this._transformerEntries.Count != 0)
							{
								foreach (object obj2 in this._transformerEntries)
								{
									DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
									Type transformerType = (Type)dictionaryEntry.Value;
									Type consumerType2 = WebPartTransformerAttribute.GetConsumerType(transformerType);
									Type providerType2 = WebPartTransformerAttribute.GetProviderType(transformerType);
									if (consumerType == consumerType2 && providerType == providerType2)
									{
										throw new ConfigurationErrorsException(SR.GetString("Transformer_types_already_added", new object[]
										{
											(string)dictionaryEntry.Key,
											transformerInfo.Name
										}), transformerInfo.ElementInformation.Properties["type"].Source, transformerInfo.ElementInformation.Properties["type"].LineNumber);
									}
								}
							}
							this._transformerEntries[transformerInfo.Name] = type;
						}
					}
				}
			}
			return this._transformerEntries;
		}

		// Token: 0x04003029 RID: 12329
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x0400302A RID: 12330
		private Hashtable _transformerEntries;
	}
}
