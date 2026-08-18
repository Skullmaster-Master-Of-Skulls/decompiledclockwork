using System;
using System.Collections;
using System.Configuration;
using System.Security.Permissions;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.Configuration
{
	// Token: 0x02000259 RID: 601
	[ConfigurationCollection(typeof(TransformerInfo))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class TransformerInfoCollection : ConfigurationElementCollection
	{
		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001FC1 RID: 8129 RVA: 0x0008BD1D File Offset: 0x0008AD1D
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TransformerInfoCollection._properties;
			}
		}

		// Token: 0x170006CC RID: 1740
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

		// Token: 0x06001FC4 RID: 8132 RVA: 0x0008BD4C File Offset: 0x0008AD4C
		public void Add(TransformerInfo transformerInfo)
		{
			this.BaseAdd(transformerInfo);
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x0008BD55 File Offset: 0x0008AD55
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x0008BD5D File Offset: 0x0008AD5D
		protected override ConfigurationElement CreateNewElement()
		{
			return new TransformerInfo();
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x0008BD64 File Offset: 0x0008AD64
		public void Remove(string s)
		{
			base.BaseRemove(s);
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x0008BD6D File Offset: 0x0008AD6D
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x0008BD76 File Offset: 0x0008AD76
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((TransformerInfo)element).Name;
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x0008BD84 File Offset: 0x0008AD84
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

		// Token: 0x04001A6D RID: 6765
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04001A6E RID: 6766
		private Hashtable _transformerEntries;
	}
}
