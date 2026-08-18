using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.Diagnostics;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006D0 RID: 1744
	[ConfigurationPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public abstract class ServiceModelExtensionElement : ServiceModelConfigurationElement, IConfigurationContextProviderInternal
	{
		// Token: 0x06004361 RID: 17249 RVA: 0x000FE968 File Offset: 0x000FCB68
		[SecuritySafeCritical]
		internal bool CanAdd(string extensionCollectionName, ContextInformation evaluationContext)
		{
			bool flag = false;
			ExtensionElementCollection extensionElementCollection = ExtensionsSection.UnsafeLookupCollection(extensionCollectionName, evaluationContext);
			if (extensionElementCollection != null && extensionElementCollection.Count != 0)
			{
				string assemblyQualifiedName = this.ThisType.AssemblyQualifiedName;
				string typeName = ExtensionElement.GetTypeName(assemblyQualifiedName);
				foreach (object obj in extensionElementCollection)
				{
					ExtensionElement extensionElement = (ExtensionElement)obj;
					string type = extensionElement.Type;
					if (type.Equals(assemblyQualifiedName, StringComparison.Ordinal))
					{
						flag = true;
						break;
					}
					if (extensionElement.TypeName.Equals(typeName, StringComparison.Ordinal))
					{
						Type type2 = Type.GetType(type, false);
						if (type2 != null && type2.Equals(this.ThisType))
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag && DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 524311, SR.GetString("TraceCodeConfiguredExtensionTypeNotFound"), this.CreateCanAddRecord(extensionCollectionName), this, null);
				}
			}
			else if (DiagnosticUtility.ShouldTraceWarning)
			{
				int traceCode;
				string @string;
				if (extensionElementCollection != null && extensionElementCollection.Count == 0)
				{
					traceCode = 524316;
					@string = SR.GetString("TraceCodeExtensionCollectionIsEmpty");
				}
				else
				{
					traceCode = 524314;
					@string = SR.GetString("TraceCodeExtensionCollectionDoesNotExist");
				}
				TraceUtility.TraceEvent(TraceEventType.Warning, traceCode, @string, this.CreateCanAddRecord(extensionCollectionName), this, null);
			}
			return flag;
		}

		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x06004362 RID: 17250 RVA: 0x000FEAB4 File Offset: 0x000FCCB4
		// (set) Token: 0x06004363 RID: 17251 RVA: 0x000FEAD5 File Offset: 0x000FCCD5
		public string ConfigurationElementName
		{
			get
			{
				if (string.IsNullOrEmpty(this.configurationElementName))
				{
					this.configurationElementName = this.GetConfigurationElementName();
				}
				return this.configurationElementName;
			}
			internal set
			{
				if (!string.IsNullOrEmpty(this.configurationElementName))
				{
					return;
				}
				this.configurationElementName = value;
			}
		}

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x06004364 RID: 17252 RVA: 0x000FEAEC File Offset: 0x000FCCEC
		// (set) Token: 0x06004365 RID: 17253 RVA: 0x000FEAF4 File Offset: 0x000FCCF4
		internal ContextInformation ContainingEvaluationContext
		{
			get
			{
				return this.containingEvaluationContext;
			}
			set
			{
				this.containingEvaluationContext = value;
			}
		}

		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x06004366 RID: 17254 RVA: 0x000FEAFD File Offset: 0x000FCCFD
		private Type ThisType
		{
			get
			{
				if (this.thisType == null)
				{
					this.thisType = base.GetType();
				}
				return this.thisType;
			}
		}

		// Token: 0x06004367 RID: 17255 RVA: 0x000FEB1F File Offset: 0x000FCD1F
		public virtual void CopyFrom(ServiceModelExtensionElement from)
		{
			if (this.IsReadOnly())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigReadOnly")));
			}
			if (from == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("from");
			}
		}

		// Token: 0x06004368 RID: 17256 RVA: 0x000FEB58 File Offset: 0x000FCD58
		private DictionaryTraceRecord CreateCanAddRecord(string extensionCollectionName)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(2);
			dictionary["ElementType"] = DiagnosticTraceBase.XmlEncode(this.ThisType.AssemblyQualifiedName);
			dictionary["CollectionName"] = ConfigurationStrings.ExtensionsSectionPath + "/" + extensionCollectionName;
			return new DictionaryTraceRecord(dictionary);
		}

		// Token: 0x06004369 RID: 17257 RVA: 0x000FEBA8 File Offset: 0x000FCDA8
		internal void DeserializeInternal(XmlReader reader, bool serializeCollectionKey)
		{
			this.DeserializeElement(reader, serializeCollectionKey);
		}

		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x0600436B RID: 17259 RVA: 0x000FEBBB File Offset: 0x000FCDBB
		// (set) Token: 0x0600436A RID: 17258 RVA: 0x000FEBB2 File Offset: 0x000FCDB2
		internal string ExtensionCollectionName
		{
			get
			{
				return this.extensionCollectionName;
			}
			set
			{
				this.extensionCollectionName = value;
			}
		}

		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x0600436C RID: 17260 RVA: 0x000FEBC3 File Offset: 0x000FCDC3
		internal ContextInformation EvalContext
		{
			get
			{
				return base.EvaluationContext;
			}
		}

		// Token: 0x0600436D RID: 17261 RVA: 0x000FEBCB File Offset: 0x000FCDCB
		internal object FromProperty(ConfigurationProperty property)
		{
			return base[property];
		}

		// Token: 0x0600436E RID: 17262 RVA: 0x000FEBD4 File Offset: 0x000FCDD4
		[SecuritySafeCritical]
		private string GetConfigurationElementName()
		{
			string text = string.Empty;
			Type type = this.ThisType;
			ContextInformation evaluationContext = this.ContainingEvaluationContext;
			if (evaluationContext == null)
			{
				evaluationContext = ConfigurationHelpers.GetEvaluationContext(this);
			}
			ExtensionElementCollection extensionElementCollection;
			if (string.IsNullOrEmpty(this.extensionCollectionName))
			{
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 524315, SR.GetString("TraceCodeExtensionCollectionNameNotFound"), this, null);
				}
				extensionElementCollection = ExtensionsSection.UnsafeLookupAssociatedCollection(this.ThisType, evaluationContext, out this.extensionCollectionName);
			}
			else
			{
				extensionElementCollection = ExtensionsSection.UnsafeLookupCollection(this.extensionCollectionName, evaluationContext);
			}
			if (extensionElementCollection == null)
			{
				if (string.IsNullOrEmpty(this.extensionCollectionName))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigNoExtensionCollectionAssociatedWithType", new object[]
					{
						type.AssemblyQualifiedName
					}), base.ElementInformation.Source, base.ElementInformation.LineNumber));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigExtensionCollectionNotFound", new object[]
				{
					this.extensionCollectionName
				}), base.ElementInformation.Source, base.ElementInformation.LineNumber));
			}
			else
			{
				for (int i = 0; i < extensionElementCollection.Count; i++)
				{
					ExtensionElement extensionElement = extensionElementCollection[i];
					if (extensionElement.Type.Equals(type.AssemblyQualifiedName, StringComparison.Ordinal))
					{
						text = extensionElement.Name;
						break;
					}
					Type type2 = Type.GetType(extensionElement.Type, false);
					if (null != type2 && type.Equals(type2))
					{
						text = extensionElement.Name;
						break;
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigExtensionTypeNotRegisteredInCollection", new object[]
					{
						type.AssemblyQualifiedName,
						this.extensionCollectionName
					}), base.ElementInformation.Source, base.ElementInformation.LineNumber));
				}
				return text;
			}
		}

		// Token: 0x0600436F RID: 17263 RVA: 0x000FED9B File Offset: 0x000FCF9B
		internal void InternalInitializeDefault()
		{
			this.InitializeDefault();
		}

		// Token: 0x06004370 RID: 17264 RVA: 0x000FEDA3 File Offset: 0x000FCFA3
		protected override bool IsModified()
		{
			return this.modified | base.IsModified();
		}

		// Token: 0x06004371 RID: 17265 RVA: 0x000FEDB2 File Offset: 0x000FCFB2
		internal bool IsModifiedInternal()
		{
			return this.IsModified();
		}

		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x06004372 RID: 17266 RVA: 0x000FEDBA File Offset: 0x000FCFBA
		internal ConfigurationPropertyCollection PropertiesInternal
		{
			get
			{
				return this.Properties;
			}
		}

		// Token: 0x06004373 RID: 17267 RVA: 0x000FEDC2 File Offset: 0x000FCFC2
		internal void ResetModifiedInternal()
		{
			this.ResetModified();
		}

		// Token: 0x06004374 RID: 17268 RVA: 0x000FEDCA File Offset: 0x000FCFCA
		protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			base.SerializeElement(writer, serializeCollectionKey);
			return true;
		}

		// Token: 0x06004375 RID: 17269 RVA: 0x000FEDD6 File Offset: 0x000FCFD6
		internal bool SerializeInternal(XmlWriter writer, bool serializeCollectionKey)
		{
			return this.SerializeElement(writer, serializeCollectionKey);
		}

		// Token: 0x06004376 RID: 17270 RVA: 0x000FEDE0 File Offset: 0x000FCFE0
		internal void SetReadOnlyInternal()
		{
			this.SetReadOnly();
		}

		// Token: 0x06004377 RID: 17271 RVA: 0x000FEDE8 File Offset: 0x000FCFE8
		[SecurityCritical]
		protected override void Reset(ConfigurationElement parentElement)
		{
			this.contextHelper.OnReset(parentElement);
			base.Reset(parentElement);
		}

		// Token: 0x06004378 RID: 17272 RVA: 0x000FEDFD File Offset: 0x000FCFFD
		ContextInformation IConfigurationContextProviderInternal.GetEvaluationContext()
		{
			return base.EvaluationContext;
		}

		// Token: 0x06004379 RID: 17273 RVA: 0x000FEE05 File Offset: 0x000FD005
		[SecurityCritical]
		ContextInformation IConfigurationContextProviderInternal.GetOriginalEvaluationContext()
		{
			return this.contextHelper.GetOriginalContext(this);
		}

		// Token: 0x04002D17 RID: 11543
		[SecurityCritical]
		private EvaluationContextHelper contextHelper;

		// Token: 0x04002D18 RID: 11544
		private ContextInformation containingEvaluationContext;

		// Token: 0x04002D19 RID: 11545
		private string configurationElementName = string.Empty;

		// Token: 0x04002D1A RID: 11546
		private string extensionCollectionName = string.Empty;

		// Token: 0x04002D1B RID: 11547
		private bool modified;

		// Token: 0x04002D1C RID: 11548
		private Type thisType;
	}
}
