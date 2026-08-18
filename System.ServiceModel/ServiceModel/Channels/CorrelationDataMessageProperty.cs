using System;
using System.Collections.Generic;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008B3 RID: 2227
	public class CorrelationDataMessageProperty : IMessageProperty
	{
		// Token: 0x060054E3 RID: 21731 RVA: 0x00138316 File Offset: 0x00136516
		public CorrelationDataMessageProperty()
		{
		}

		// Token: 0x060054E4 RID: 21732 RVA: 0x0013831E File Offset: 0x0013651E
		private CorrelationDataMessageProperty(IDictionary<string, CorrelationDataMessageProperty.DataProviderEntry> dataProviders)
		{
			if (dataProviders != null && dataProviders.Count > 0)
			{
				this.dataProviders = new Dictionary<string, CorrelationDataMessageProperty.DataProviderEntry>(dataProviders);
			}
		}

		// Token: 0x170014DE RID: 5342
		// (get) Token: 0x060054E5 RID: 21733 RVA: 0x0013833E File Offset: 0x0013653E
		public static string Name
		{
			get
			{
				return "CorrelationDataMessageProperty";
			}
		}

		// Token: 0x060054E6 RID: 21734 RVA: 0x00138348 File Offset: 0x00136548
		public void Add(string name, Func<string> dataProvider)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			if (dataProvider == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dataProvider");
			}
			if (this.dataProviders == null)
			{
				this.dataProviders = new Dictionary<string, CorrelationDataMessageProperty.DataProviderEntry>();
			}
			this.dataProviders.Add(name, new CorrelationDataMessageProperty.DataProviderEntry(dataProvider));
		}

		// Token: 0x060054E7 RID: 21735 RVA: 0x001383A5 File Offset: 0x001365A5
		public bool Remove(string name)
		{
			return this.dataProviders != null && this.dataProviders.Remove(name);
		}

		// Token: 0x060054E8 RID: 21736 RVA: 0x001383C0 File Offset: 0x001365C0
		public bool TryGetValue(string name, out string value)
		{
			CorrelationDataMessageProperty.DataProviderEntry dataProviderEntry;
			if (this.dataProviders != null && this.dataProviders.TryGetValue(name, out dataProviderEntry))
			{
				value = dataProviderEntry.Data;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x060054E9 RID: 21737 RVA: 0x001383F3 File Offset: 0x001365F3
		public static bool TryGet(Message message, out CorrelationDataMessageProperty property)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			return CorrelationDataMessageProperty.TryGet(message.Properties, out property);
		}

		// Token: 0x060054EA RID: 21738 RVA: 0x00138414 File Offset: 0x00136614
		public static bool TryGet(MessageProperties properties, out CorrelationDataMessageProperty property)
		{
			object obj = null;
			if (properties.TryGetValue("CorrelationDataMessageProperty", out obj))
			{
				property = (obj as CorrelationDataMessageProperty);
			}
			else
			{
				property = null;
			}
			return property != null;
		}

		// Token: 0x060054EB RID: 21739 RVA: 0x00138444 File Offset: 0x00136644
		public static void AddData(Message message, string name, Func<string> dataProvider)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			if (dataProvider == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dataProvider");
			}
			CorrelationDataMessageProperty correlationDataMessageProperty = null;
			object obj = null;
			if (message.Properties.TryGetValue("CorrelationDataMessageProperty", out obj))
			{
				correlationDataMessageProperty = (obj as CorrelationDataMessageProperty);
			}
			bool flag = false;
			if (correlationDataMessageProperty == null)
			{
				correlationDataMessageProperty = new CorrelationDataMessageProperty();
				flag = true;
			}
			correlationDataMessageProperty.Add(name, dataProvider);
			if (flag)
			{
				message.Properties["CorrelationDataMessageProperty"] = correlationDataMessageProperty;
			}
		}

		// Token: 0x060054EC RID: 21740 RVA: 0x001384C4 File Offset: 0x001366C4
		public IMessageProperty CreateCopy()
		{
			return new CorrelationDataMessageProperty(this.dataProviders);
		}

		// Token: 0x04003349 RID: 13129
		private const string PropertyName = "CorrelationDataMessageProperty";

		// Token: 0x0400334A RID: 13130
		private Dictionary<string, CorrelationDataMessageProperty.DataProviderEntry> dataProviders;

		// Token: 0x02000D7C RID: 3452
		private class DataProviderEntry
		{
			// Token: 0x06007E6E RID: 32366 RVA: 0x001D79C0 File Offset: 0x001D5BC0
			public DataProviderEntry(Func<string> dataProvider)
			{
				this.dataProvider = dataProvider;
				this.resolvedData = null;
			}

			// Token: 0x17001C29 RID: 7209
			// (get) Token: 0x06007E6F RID: 32367 RVA: 0x001D79D6 File Offset: 0x001D5BD6
			public string Data
			{
				get
				{
					if (this.dataProvider != null)
					{
						this.resolvedData = this.dataProvider();
						this.dataProvider = null;
					}
					return this.resolvedData;
				}
			}

			// Token: 0x0400486E RID: 18542
			private string resolvedData;

			// Token: 0x0400486F RID: 18543
			private Func<string> dataProvider;
		}
	}
}
