using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006CB RID: 1739
	public abstract class ServiceModelEnhancedConfigurationElementCollection<TConfigurationElement> : ServiceModelConfigurationElementCollection<TConfigurationElement> where TConfigurationElement : ConfigurationElement, new()
	{
		// Token: 0x06004341 RID: 17217 RVA: 0x000FE257 File Offset: 0x000FC457
		internal ServiceModelEnhancedConfigurationElementCollection(string elementName) : base(ConfigurationElementCollectionType.AddRemoveClearMap, elementName)
		{
			base.AddElementName = elementName;
		}

		// Token: 0x06004342 RID: 17218 RVA: 0x000FE268 File Offset: 0x000FC468
		protected override void BaseAdd(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			object elementKey = this.GetElementKey(element);
			if (this.ContainsKey(elementKey))
			{
				ConfigurationElement configurationElement = base.BaseGet(elementKey);
				if (configurationElement != null)
				{
					if (configurationElement.ElementInformation.IsPresent)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigDuplicateKeyAtSameScope", new object[]
						{
							this.ElementName,
							elementKey
						})));
					}
					if (DiagnosticUtility.ShouldTraceWarning)
					{
						DictionaryTraceRecord extendedData = new DictionaryTraceRecord(new Dictionary<string, string>(6)
						{
							{
								"ElementName",
								this.ElementName
							},
							{
								"Name",
								elementKey.ToString()
							},
							{
								"OldElementLocation",
								configurationElement.ElementInformation.Source
							},
							{
								"OldElementLineNumber",
								configurationElement.ElementInformation.LineNumber.ToString(NumberFormatInfo.CurrentInfo)
							},
							{
								"NewElementLocation",
								element.ElementInformation.Source
							},
							{
								"NewElementLineNumber",
								element.ElementInformation.LineNumber.ToString(NumberFormatInfo.CurrentInfo)
							}
						});
						TraceUtility.TraceEvent(TraceEventType.Warning, 524329, SR.GetString("TraceCodeOverridingDuplicateConfigurationKey"), extendedData, this, null);
					}
				}
			}
			base.BaseAdd(element);
		}

		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x06004343 RID: 17219 RVA: 0x000FE3B1 File Offset: 0x000FC5B1
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}
	}
}
