using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.Diagnostics;
using System.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006D1 RID: 1745
	public abstract class ServiceModelExtensionCollectionElement<TServiceModelExtensionElement> : ConfigurationElement, ICollection<!0>, IEnumerable<!0>, IEnumerable, IConfigurationContextProviderInternal where TServiceModelExtensionElement : ServiceModelExtensionElement
	{
		// Token: 0x0600437A RID: 17274 RVA: 0x000FEE13 File Offset: 0x000FD013
		internal ServiceModelExtensionCollectionElement(string extensionCollectionName)
		{
			this.extensionCollectionName = extensionCollectionName;
		}

		// Token: 0x17001171 RID: 4465
		public TServiceModelExtensionElement this[int index]
		{
			get
			{
				return this.Items[index];
			}
		}

		// Token: 0x17001172 RID: 4466
		public TServiceModelExtensionElement this[Type extensionType]
		{
			get
			{
				if (extensionType == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("extensionType");
				}
				if (!this.CollectionElementBaseType.IsAssignableFrom(extensionType))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("extensionType", SR.GetString("ConfigInvalidExtensionType", new object[]
					{
						extensionType.ToString(),
						this.CollectionElementBaseType.FullName,
						this.extensionCollectionName
					}));
				}
				TServiceModelExtensionElement result = default(TServiceModelExtensionElement);
				foreach (TServiceModelExtensionElement tserviceModelExtensionElement in this)
				{
					if (tserviceModelExtensionElement != null && tserviceModelExtensionElement.GetType() == extensionType)
					{
						result = tserviceModelExtensionElement;
					}
				}
				return result;
			}
		}

		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x0600437D RID: 17277 RVA: 0x000FEF00 File Offset: 0x000FD100
		public int Count
		{
			get
			{
				return this.Items.Count;
			}
		}

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x0600437E RID: 17278 RVA: 0x000FEF0D File Offset: 0x000FD10D
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return this.IsReadOnly();
			}
		}

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x0600437F RID: 17279 RVA: 0x000FEF15 File Offset: 0x000FD115
		internal List<TServiceModelExtensionElement> Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new List<TServiceModelExtensionElement>();
				}
				return this.items;
			}
		}

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x06004380 RID: 17280 RVA: 0x000FEF30 File Offset: 0x000FD130
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection();
				}
				return this.properties;
			}
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x000FEF4C File Offset: 0x000FD14C
		public virtual void Add(TServiceModelExtensionElement element)
		{
			if (this.IsReadOnly())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigReadOnly")));
			}
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			element.ExtensionCollectionName = this.extensionCollectionName;
			if (this.Contains(element))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("element", SR.GetString("ConfigDuplicateKey", new object[]
				{
					element.ConfigurationElementName
				}));
			}
			if (!this.CanAdd(element))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("element", SR.GetString("ConfigElementTypeNotAllowed", new object[]
				{
					element.ConfigurationElementName,
					this.extensionCollectionName
				}));
			}
			element.ContainingEvaluationContext = ConfigurationHelpers.GetEvaluationContext(this);
			ConfigurationProperty configurationProperty = new ConfigurationProperty(element.ConfigurationElementName, element.GetType(), null);
			this.Properties.Add(configurationProperty);
			base[configurationProperty] = element;
			this.Items.Add(element);
			this.modified = true;
		}

		// Token: 0x06004382 RID: 17282 RVA: 0x000FF078 File Offset: 0x000FD278
		internal void AddItem(TServiceModelExtensionElement element)
		{
			if (this.IsReadOnly())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigReadOnly")));
			}
			element.ExtensionCollectionName = this.extensionCollectionName;
			element.ContainingEvaluationContext = ConfigurationHelpers.GetEvaluationContext(this);
			this.Items.Add(element);
			this.modified = true;
		}

		// Token: 0x06004383 RID: 17283 RVA: 0x000FF0DC File Offset: 0x000FD2DC
		public virtual bool CanAdd(TServiceModelExtensionElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			bool result = false;
			Type type = element.GetType();
			if (!this.IsReadOnly())
			{
				if (!this.ContainsKey(type))
				{
					result = element.CanAdd(this.extensionCollectionName, ConfigurationHelpers.GetEvaluationContext(this));
				}
				else if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 524317, SR.GetString("TraceCodeExtensionElementAlreadyExistsInCollection"), this.CreateCanAddRecord(this[type]), this, null);
				}
			}
			else if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 524310, SR.GetString("TraceCodeConfigurationIsReadOnly"), null, this, null);
			}
			return result;
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x000FF187 File Offset: 0x000FD387
		private DictionaryTraceRecord CreateCanAddRecord(TServiceModelExtensionElement element)
		{
			return this.CreateCanAddRecord(element, new Dictionary<string, string>(3));
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x000FF198 File Offset: 0x000FD398
		private DictionaryTraceRecord CreateCanAddRecord(TServiceModelExtensionElement element, Dictionary<string, string> values)
		{
			values["ElementType"] = DiagnosticTraceBase.XmlEncode(typeof(TServiceModelExtensionElement).AssemblyQualifiedName);
			values["ConfiguredSectionName"] = element.ConfigurationElementName;
			values["CollectionName"] = ConfigurationStrings.ExtensionsSectionPath + "/" + this.extensionCollectionName;
			return new DictionaryTraceRecord(values);
		}

		// Token: 0x06004386 RID: 17286 RVA: 0x000FF200 File Offset: 0x000FD400
		public void Clear()
		{
			if (this.IsReadOnly())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigReadOnly")));
			}
			if (this.Properties.Count > 0)
			{
				this.modified = true;
			}
			List<string> list = new List<string>(this.Items.Count);
			foreach (TServiceModelExtensionElement tserviceModelExtensionElement in this.Items)
			{
				list.Add(tserviceModelExtensionElement.ConfigurationElementName);
			}
			this.Items.Clear();
			foreach (string name in list)
			{
				this.Properties.Remove(name);
			}
		}

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x06004387 RID: 17287 RVA: 0x000FF2F8 File Offset: 0x000FD4F8
		internal Type CollectionElementBaseType
		{
			get
			{
				return typeof(TServiceModelExtensionElement);
			}
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x000FF304 File Offset: 0x000FD504
		public bool Contains(TServiceModelExtensionElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			return this.ContainsKey(element.GetType());
		}

		// Token: 0x06004389 RID: 17289 RVA: 0x000FF32F File Offset: 0x000FD52F
		public bool ContainsKey(Type elementType)
		{
			if (elementType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("elementType");
			}
			return this[elementType] != null;
		}

		// Token: 0x0600438A RID: 17290 RVA: 0x000FF35C File Offset: 0x000FD55C
		public bool ContainsKey(string elementName)
		{
			if (string.IsNullOrEmpty(elementName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("elementName");
			}
			bool result = false;
			foreach (TServiceModelExtensionElement tserviceModelExtensionElement in this)
			{
				if (tserviceModelExtensionElement != null)
				{
					string configurationElementName = tserviceModelExtensionElement.ConfigurationElementName;
					if (configurationElementName.Equals(elementName, StringComparison.Ordinal))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x0600438B RID: 17291 RVA: 0x000FF3DC File Offset: 0x000FD5DC
		public void CopyTo(TServiceModelExtensionElement[] elements, int start)
		{
			if (elements == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("elements");
			}
			if (start < 0 || start >= elements.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("start", SR.GetString("ConfigInvalidStartValue", new object[]
				{
					elements.Length - 1,
					start
				}));
			}
			foreach (TServiceModelExtensionElement tserviceModelExtensionElement in this)
			{
				if (tserviceModelExtensionElement != null)
				{
					string configurationElementName = tserviceModelExtensionElement.ConfigurationElementName;
					TServiceModelExtensionElement tserviceModelExtensionElement2 = this.CreateNewSection(configurationElementName);
					if (tserviceModelExtensionElement2 != null && start < elements.Length)
					{
						tserviceModelExtensionElement2.CopyFrom(tserviceModelExtensionElement);
						elements[start] = tserviceModelExtensionElement2;
						start++;
					}
				}
			}
		}

		// Token: 0x0600438C RID: 17292 RVA: 0x000FF4B8 File Offset: 0x000FD6B8
		private TServiceModelExtensionElement CreateNewSection(string name)
		{
			if (this.ContainsKey(name) && !(name == "clear") && !(name == "remove"))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigDuplicateItem", new object[]
				{
					name,
					base.GetType().Name
				}), base.ElementInformation.Source, base.ElementInformation.LineNumber));
			}
			TServiceModelExtensionElement tserviceModelExtensionElement = default(TServiceModelExtensionElement);
			ContextInformation evaluationContext = ConfigurationHelpers.GetEvaluationContext(this);
			Type extensionType;
			try
			{
				extensionType = this.GetExtensionType(evaluationContext, name);
			}
			catch (ConfigurationErrorsException exception)
			{
				if (AppContainerInfo.IsRunningInAppContainer && evaluationContext.IsMachineLevel)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					TServiceModelExtensionElement result = default(TServiceModelExtensionElement);
					return result;
				}
				throw;
			}
			if (!(null != extensionType))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidExtensionElementName", new object[]
				{
					name,
					this.extensionCollectionName
				}), base.ElementInformation.Source, base.ElementInformation.LineNumber));
			}
			if (this.CollectionElementBaseType.IsAssignableFrom(extensionType))
			{
				tserviceModelExtensionElement = (TServiceModelExtensionElement)((object)Activator.CreateInstance(extensionType));
				tserviceModelExtensionElement.ExtensionCollectionName = this.extensionCollectionName;
				tserviceModelExtensionElement.ConfigurationElementName = name;
				tserviceModelExtensionElement.InternalInitializeDefault();
				return tserviceModelExtensionElement;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidExtensionElement", new object[]
			{
				name,
				this.CollectionElementBaseType.FullName
			}), base.ElementInformation.Source, base.ElementInformation.LineNumber));
		}

		// Token: 0x0600438D RID: 17293 RVA: 0x000FF668 File Offset: 0x000FD868
		[SecuritySafeCritical]
		private Type GetExtensionType(ContextInformation evaluationContext, string name)
		{
			ExtensionElementCollection extensionElementCollection = ExtensionsSection.UnsafeLookupCollection(this.extensionCollectionName, evaluationContext);
			if (!extensionElementCollection.ContainsKey(name))
			{
				return null;
			}
			ExtensionElement extensionElement = extensionElementCollection[name];
			Type type = Type.GetType(extensionElement.Type, false);
			if (null == type)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidType", new object[]
				{
					extensionElement.Type,
					extensionElement.Name
				}), base.ElementInformation.Source, base.ElementInformation.LineNumber));
			}
			return type;
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x000FF6F4 File Offset: 0x000FD8F4
		internal void MergeWith(List<TServiceModelExtensionElement> parentExtensionElements)
		{
			ServiceModelExtensionCollectionElement<TServiceModelExtensionElement>.Merge(parentExtensionElements, this);
			this.Clear();
			foreach (TServiceModelExtensionElement element in parentExtensionElements)
			{
				this.Add(element);
			}
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x000FF750 File Offset: 0x000FD950
		private static void Merge(List<TServiceModelExtensionElement> parentExtensionElements, IEnumerable<TServiceModelExtensionElement> childExtensionElements)
		{
			foreach (TServiceModelExtensionElement tserviceModelExtensionElement in childExtensionElements)
			{
				if (tserviceModelExtensionElement is ClearBehaviorElement)
				{
					parentExtensionElements.Clear();
				}
				else if (tserviceModelExtensionElement is RemoveBehaviorElement)
				{
					string childExtensionElementName = (tserviceModelExtensionElement as RemoveBehaviorElement).Name;
					if (!string.IsNullOrEmpty(childExtensionElementName))
					{
						parentExtensionElements.RemoveAll((TServiceModelExtensionElement element) => element != null && element.ConfigurationElementName == childExtensionElementName);
					}
				}
				else
				{
					Type childExtensionElementType = tserviceModelExtensionElement.GetType();
					parentExtensionElements.RemoveAll((TServiceModelExtensionElement element) => element != null && element.GetType() == childExtensionElementType);
					parentExtensionElements.Add(tserviceModelExtensionElement);
				}
			}
		}

		// Token: 0x06004390 RID: 17296 RVA: 0x000FF828 File Offset: 0x000FDA28
		[SecuritySafeCritical]
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			this.SetIsPresent();
			this.DeserializeElementCore(reader);
		}

		// Token: 0x06004391 RID: 17297 RVA: 0x000FF838 File Offset: 0x000FDA38
		private void DeserializeElementCore(XmlReader reader)
		{
			if (reader.HasAttributes && 0 < reader.AttributeCount)
			{
				while (reader.MoveToNextAttribute())
				{
					if (this.Properties.Contains(reader.Name))
					{
						base[reader.Name] = this.Properties[reader.Name].Converter.ConvertFromString(reader.Value);
					}
					else
					{
						this.OnDeserializeUnrecognizedAttribute(reader.Name, reader.Value);
					}
				}
			}
			if (XmlNodeType.Element != reader.NodeType)
			{
				reader.MoveToElement();
			}
			XmlReader xmlReader = reader.ReadSubtree();
			if (xmlReader.Read())
			{
				while (xmlReader.Read())
				{
					if (XmlNodeType.Element == xmlReader.NodeType)
					{
						TServiceModelExtensionElement tserviceModelExtensionElement = this.CreateNewSection(xmlReader.Name);
						if (tserviceModelExtensionElement != null)
						{
							this.Add(tserviceModelExtensionElement);
							tserviceModelExtensionElement.DeserializeInternal(xmlReader, false);
						}
					}
				}
			}
		}

		// Token: 0x06004392 RID: 17298 RVA: 0x000FF910 File Offset: 0x000FDB10
		[SecurityCritical]
		private void SetIsPresent()
		{
			ConfigurationHelpers.SetIsPresent(this);
		}

		// Token: 0x06004393 RID: 17299 RVA: 0x000FF918 File Offset: 0x000FDB18
		public IEnumerator<TServiceModelExtensionElement> GetEnumerator()
		{
			int num;
			for (int index = 0; index < this.Items.Count; index = num)
			{
				TServiceModelExtensionElement tserviceModelExtensionElement = this.items[index];
				yield return tserviceModelExtensionElement;
				num = index + 1;
			}
			yield break;
		}

		// Token: 0x06004394 RID: 17300 RVA: 0x000FF928 File Offset: 0x000FDB28
		protected override bool IsModified()
		{
			bool flag = this.modified;
			if (!flag)
			{
				for (int i = 0; i < this.Items.Count; i++)
				{
					TServiceModelExtensionElement tserviceModelExtensionElement = this.Items[i];
					if (tserviceModelExtensionElement.IsModifiedInternal())
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06004395 RID: 17301 RVA: 0x000FF974 File Offset: 0x000FDB74
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			this.DeserializeElement(reader, false);
			return true;
		}

		// Token: 0x06004396 RID: 17302 RVA: 0x000FF980 File Offset: 0x000FDB80
		public bool Remove(TServiceModelExtensionElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			bool result = false;
			if (this.Contains(element))
			{
				string configurationElementName = element.ConfigurationElementName;
				TServiceModelExtensionElement item = this[element.GetType()];
				this.Items.Remove(item);
				this.Properties.Remove(configurationElementName);
				this.modified = true;
				result = true;
			}
			return result;
		}

		// Token: 0x06004397 RID: 17303 RVA: 0x000FF9F4 File Offset: 0x000FDBF4
		[SecurityCritical]
		protected override void Reset(ConfigurationElement parentElement)
		{
			ServiceModelExtensionCollectionElement<TServiceModelExtensionElement> serviceModelExtensionCollectionElement = (ServiceModelExtensionCollectionElement<TServiceModelExtensionElement>)parentElement;
			foreach (TServiceModelExtensionElement item in serviceModelExtensionCollectionElement.Items)
			{
				this.Items.Add(item);
			}
			this.UpdateProperties(serviceModelExtensionCollectionElement);
			this.contextHelper.OnReset(parentElement);
			base.Reset(parentElement);
		}

		// Token: 0x06004398 RID: 17304 RVA: 0x000FFA70 File Offset: 0x000FDC70
		protected override void ResetModified()
		{
			for (int i = 0; i < this.Items.Count; i++)
			{
				TServiceModelExtensionElement tserviceModelExtensionElement = this.Items[i];
				tserviceModelExtensionElement.ResetModifiedInternal();
			}
			this.modified = false;
		}

		// Token: 0x06004399 RID: 17305 RVA: 0x000FFAB2 File Offset: 0x000FDCB2
		protected void SetIsModified()
		{
			this.modified = true;
		}

		// Token: 0x0600439A RID: 17306 RVA: 0x000FFABC File Offset: 0x000FDCBC
		protected override void SetReadOnly()
		{
			base.SetReadOnly();
			for (int i = 0; i < this.Items.Count; i++)
			{
				TServiceModelExtensionElement tserviceModelExtensionElement = this.Items[i];
				tserviceModelExtensionElement.SetReadOnlyInternal();
			}
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x000FFAFD File Offset: 0x000FDCFD
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600439C RID: 17308 RVA: 0x000FFB08 File Offset: 0x000FDD08
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			if (sourceElement == null)
			{
				return;
			}
			ServiceModelExtensionCollectionElement<TServiceModelExtensionElement> sourceElement2 = (ServiceModelExtensionCollectionElement<TServiceModelExtensionElement>)sourceElement;
			this.UpdateProperties(sourceElement2);
			base.Unmerge(sourceElement, parentElement, saveMode);
		}

		// Token: 0x0600439D RID: 17309 RVA: 0x000FFB30 File Offset: 0x000FDD30
		private void UpdateProperties(ServiceModelExtensionCollectionElement<TServiceModelExtensionElement> sourceElement)
		{
			foreach (object obj in sourceElement.Properties)
			{
				ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
				if (!this.Properties.Contains(configurationProperty.Name))
				{
					this.Properties.Add(configurationProperty);
				}
			}
			foreach (TServiceModelExtensionElement tserviceModelExtensionElement in this.Items)
			{
				if (!(tserviceModelExtensionElement is ClearBehaviorElement) && !(tserviceModelExtensionElement is RemoveBehaviorElement))
				{
					string configurationElementName = tserviceModelExtensionElement.ConfigurationElementName;
					if (!this.Properties.Contains(configurationElementName))
					{
						ConfigurationProperty property = new ConfigurationProperty(configurationElementName, tserviceModelExtensionElement.GetType(), null);
						this.Properties.Add(property);
					}
				}
			}
		}

		// Token: 0x0600439E RID: 17310 RVA: 0x000FFC3C File Offset: 0x000FDE3C
		ContextInformation IConfigurationContextProviderInternal.GetEvaluationContext()
		{
			return base.EvaluationContext;
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x000FFC44 File Offset: 0x000FDE44
		[SecurityCritical]
		ContextInformation IConfigurationContextProviderInternal.GetOriginalEvaluationContext()
		{
			return this.contextHelper.GetOriginalContext(this);
		}

		// Token: 0x04002D1D RID: 11549
		[SecurityCritical]
		private EvaluationContextHelper contextHelper;

		// Token: 0x04002D1E RID: 11550
		private string extensionCollectionName;

		// Token: 0x04002D1F RID: 11551
		private bool modified;

		// Token: 0x04002D20 RID: 11552
		private List<TServiceModelExtensionElement> items;

		// Token: 0x04002D21 RID: 11553
		private ConfigurationPropertyCollection properties;
	}
}
