using System;
using System.Configuration;
using System.Xml;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Practices.Unity.Configuration.Properties;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000032 RID: 50
	public class ParameterValueElementCollection : DeserializableConfigurationElementCollectionBase<ParameterValueElement>
	{
		// Token: 0x0600017D RID: 381 RVA: 0x00005D50 File Offset: 0x00003F50
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			ParameterValueElementCollection.DeserializedElementHolder deserializedElementHolder = new ParameterValueElementCollection.DeserializedElementHolder();
			ValueElementHelper valueElementHelper = new ValueElementHelper(deserializedElementHolder);
			if (valueElementHelper.DeserializeUnknownElement(elementName, reader))
			{
				base.Add(deserializedElementHolder.Value);
				return true;
			}
			return base.OnDeserializeUnrecognizedElement(elementName, reader);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005D8A File Offset: 0x00003F8A
		protected override ConfigurationElement CreateNewElement()
		{
			throw new InvalidOperationException(Resources.CannotCreateParameterValueElement);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005D96 File Offset: 0x00003F96
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ParameterValueElement)element).Key;
		}

		// Token: 0x02000033 RID: 51
		private class DeserializedElementHolder : IValueProvidingElement
		{
			// Token: 0x17000042 RID: 66
			// (get) Token: 0x06000181 RID: 385 RVA: 0x00005DAB File Offset: 0x00003FAB
			// (set) Token: 0x06000182 RID: 386 RVA: 0x00005DB3 File Offset: 0x00003FB3
			private ParameterValueElement ValueElement { get; set; }

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x06000183 RID: 387 RVA: 0x00005DBC File Offset: 0x00003FBC
			// (set) Token: 0x06000184 RID: 388 RVA: 0x00005DC4 File Offset: 0x00003FC4
			public ParameterValueElement Value
			{
				get
				{
					return this.ValueElement;
				}
				set
				{
					this.ValueElement = value;
				}
			}

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x06000185 RID: 389 RVA: 0x00005DCD File Offset: 0x00003FCD
			public string DestinationName
			{
				get
				{
					throw new NotImplementedException();
				}
			}
		}
	}
}
