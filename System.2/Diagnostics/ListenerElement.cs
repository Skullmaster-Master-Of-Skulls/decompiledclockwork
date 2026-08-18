using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace System.Diagnostics
{
	// Token: 0x0200049E RID: 1182
	internal class ListenerElement : TypedElement
	{
		// Token: 0x06002BDA RID: 11226 RVA: 0x000C65A0 File Offset: 0x000C47A0
		public ListenerElement(bool allowReferences) : base(typeof(TraceListener))
		{
			this._allowReferences = allowReferences;
			ConfigurationPropertyOptions configurationPropertyOptions = ConfigurationPropertyOptions.None;
			if (!this._allowReferences)
			{
				configurationPropertyOptions |= ConfigurationPropertyOptions.IsRequired;
			}
			this._propListenerTypeName = new ConfigurationProperty("type", typeof(string), null, configurationPropertyOptions);
			this._properties.Remove("type");
			this._properties.Add(this._propListenerTypeName);
			this._properties.Add(ListenerElement._propFilter);
			this._properties.Add(ListenerElement._propName);
			this._properties.Add(ListenerElement._propOutputOpts);
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06002BDB RID: 11227 RVA: 0x000C6640 File Offset: 0x000C4840
		public Hashtable Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new Hashtable(StringComparer.OrdinalIgnoreCase);
				}
				return this._attributes;
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06002BDC RID: 11228 RVA: 0x000C6660 File Offset: 0x000C4860
		[ConfigurationProperty("filter")]
		public FilterElement Filter
		{
			get
			{
				return (FilterElement)base[ListenerElement._propFilter];
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06002BDD RID: 11229 RVA: 0x000C6672 File Offset: 0x000C4872
		// (set) Token: 0x06002BDE RID: 11230 RVA: 0x000C6684 File Offset: 0x000C4884
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)base[ListenerElement._propName];
			}
			set
			{
				base[ListenerElement._propName] = value;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06002BDF RID: 11231 RVA: 0x000C6692 File Offset: 0x000C4892
		// (set) Token: 0x06002BE0 RID: 11232 RVA: 0x000C66A4 File Offset: 0x000C48A4
		[ConfigurationProperty("traceOutputOptions", DefaultValue = TraceOptions.None)]
		public TraceOptions TraceOutputOptions
		{
			get
			{
				return (TraceOptions)base[ListenerElement._propOutputOpts];
			}
			set
			{
				base[ListenerElement._propOutputOpts] = value;
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06002BE1 RID: 11233 RVA: 0x000C66B7 File Offset: 0x000C48B7
		// (set) Token: 0x06002BE2 RID: 11234 RVA: 0x000C66CA File Offset: 0x000C48CA
		[ConfigurationProperty("type")]
		public override string TypeName
		{
			get
			{
				return (string)base[this._propListenerTypeName];
			}
			set
			{
				base[this._propListenerTypeName] = value;
			}
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x000C66DC File Offset: 0x000C48DC
		public override bool Equals(object compareTo)
		{
			if (this.Name.Equals("Default") && this.TypeName.Equals(typeof(DefaultTraceListener).FullName))
			{
				ListenerElement listenerElement = compareTo as ListenerElement;
				return listenerElement != null && listenerElement.Name.Equals("Default") && listenerElement.TypeName.Equals(typeof(DefaultTraceListener).FullName);
			}
			return base.Equals(compareTo);
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x000C6757 File Offset: 0x000C4957
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x000C6760 File Offset: 0x000C4960
		public TraceListener GetRuntimeObject()
		{
			if (this._runtimeObject != null)
			{
				return (TraceListener)this._runtimeObject;
			}
			TraceListener result;
			try
			{
				string typeName = this.TypeName;
				if (string.IsNullOrEmpty(typeName))
				{
					if (this._attributes != null || base.ElementInformation.Properties[ListenerElement._propFilter.Name].ValueOrigin == PropertyValueOrigin.SetHere || this.TraceOutputOptions != TraceOptions.None || !string.IsNullOrEmpty(base.InitData))
					{
						throw new ConfigurationErrorsException(SR.GetString("Reference_listener_cant_have_properties", new object[]
						{
							this.Name
						}));
					}
					if (DiagnosticsConfiguration.SharedListeners == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Reference_to_nonexistent_listener", new object[]
						{
							this.Name
						}));
					}
					ListenerElement listenerElement = DiagnosticsConfiguration.SharedListeners[this.Name];
					if (listenerElement == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Reference_to_nonexistent_listener", new object[]
						{
							this.Name
						}));
					}
					this._runtimeObject = listenerElement.GetRuntimeObject();
					result = (TraceListener)this._runtimeObject;
				}
				else
				{
					TraceListener traceListener = (TraceListener)base.BaseGetRuntimeObject();
					traceListener.initializeData = base.InitData;
					traceListener.Name = this.Name;
					traceListener.SetAttributes(this.Attributes);
					traceListener.TraceOutputOptions = this.TraceOutputOptions;
					if (this.Filter != null && this.Filter.TypeName != null && this.Filter.TypeName.Length != 0)
					{
						traceListener.Filter = this.Filter.GetRuntimeObject();
						XmlWriterTraceListener xmlWriterTraceListener = traceListener as XmlWriterTraceListener;
						if (xmlWriterTraceListener != null)
						{
							xmlWriterTraceListener.shouldRespectFilterOnTraceTransfer = true;
						}
					}
					this._runtimeObject = traceListener;
					result = traceListener;
				}
			}
			catch (ArgumentException inner)
			{
				throw new ConfigurationErrorsException(SR.GetString("Could_not_create_listener", new object[]
				{
					this.Name
				}), inner);
			}
			return result;
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x000C693C File Offset: 0x000C4B3C
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			this.Attributes.Add(name, value);
			return true;
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x000C694C File Offset: 0x000C4B4C
		protected override void PreSerialize(XmlWriter writer)
		{
			if (this._attributes != null)
			{
				IDictionaryEnumerator enumerator = this._attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Value;
					string localName = (string)enumerator.Key;
					if (text != null && writer != null)
					{
						writer.WriteAttributeString(localName, text);
					}
				}
			}
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x000C69A0 File Offset: 0x000C4BA0
		protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			return base.SerializeElement(writer, serializeCollectionKey) || (this._attributes != null && this._attributes.Count > 0);
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x000C69D8 File Offset: 0x000C4BD8
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			ListenerElement listenerElement = sourceElement as ListenerElement;
			if (listenerElement != null && listenerElement._attributes != null)
			{
				this._attributes = listenerElement._attributes;
			}
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x000C6A0C File Offset: 0x000C4C0C
		internal void ResetProperties()
		{
			if (this._attributes != null)
			{
				this._attributes.Clear();
				this._properties.Clear();
				this._properties.Add(this._propListenerTypeName);
				this._properties.Add(ListenerElement._propFilter);
				this._properties.Add(ListenerElement._propName);
				this._properties.Add(ListenerElement._propOutputOpts);
			}
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x000C6A78 File Offset: 0x000C4C78
		internal TraceListener RefreshRuntimeObject(TraceListener listener)
		{
			this._runtimeObject = null;
			TraceListener result;
			try
			{
				string typeName = this.TypeName;
				if (string.IsNullOrEmpty(typeName))
				{
					if (this._attributes != null || base.ElementInformation.Properties[ListenerElement._propFilter.Name].ValueOrigin == PropertyValueOrigin.SetHere || this.TraceOutputOptions != TraceOptions.None || !string.IsNullOrEmpty(base.InitData))
					{
						throw new ConfigurationErrorsException(SR.GetString("Reference_listener_cant_have_properties", new object[]
						{
							this.Name
						}));
					}
					if (DiagnosticsConfiguration.SharedListeners == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Reference_to_nonexistent_listener", new object[]
						{
							this.Name
						}));
					}
					ListenerElement listenerElement = DiagnosticsConfiguration.SharedListeners[this.Name];
					if (listenerElement == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Reference_to_nonexistent_listener", new object[]
						{
							this.Name
						}));
					}
					this._runtimeObject = listenerElement.RefreshRuntimeObject(listener);
					result = (TraceListener)this._runtimeObject;
				}
				else if (Type.GetType(typeName) != listener.GetType() || base.InitData != listener.initializeData)
				{
					result = this.GetRuntimeObject();
				}
				else
				{
					listener.SetAttributes(this.Attributes);
					listener.TraceOutputOptions = this.TraceOutputOptions;
					if (listener.Filter != null)
					{
						if (base.ElementInformation.Properties[ListenerElement._propFilter.Name].ValueOrigin == PropertyValueOrigin.SetHere || base.ElementInformation.Properties[ListenerElement._propFilter.Name].ValueOrigin == PropertyValueOrigin.Inherited)
						{
							listener.Filter = this.Filter.RefreshRuntimeObject(listener.Filter);
						}
						else
						{
							listener.Filter = null;
						}
					}
					this._runtimeObject = listener;
					result = listener;
				}
			}
			catch (ArgumentException inner)
			{
				throw new ConfigurationErrorsException(SR.GetString("Could_not_create_listener", new object[]
				{
					this.Name
				}), inner);
			}
			return result;
		}

		// Token: 0x04002699 RID: 9881
		private static readonly ConfigurationProperty _propFilter = new ConfigurationProperty("filter", typeof(FilterElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x0400269A RID: 9882
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x0400269B RID: 9883
		private static readonly ConfigurationProperty _propOutputOpts = new ConfigurationProperty("traceOutputOptions", typeof(TraceOptions), TraceOptions.None, ConfigurationPropertyOptions.None);

		// Token: 0x0400269C RID: 9884
		private ConfigurationProperty _propListenerTypeName;

		// Token: 0x0400269D RID: 9885
		private bool _allowReferences;

		// Token: 0x0400269E RID: 9886
		private Hashtable _attributes;

		// Token: 0x0400269F RID: 9887
		internal bool _isAddedByDefault;
	}
}
