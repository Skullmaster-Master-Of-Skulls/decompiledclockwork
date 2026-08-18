using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000056 RID: 86
	internal class ActivityIdFlowDictionary
	{
		// Token: 0x06000255 RID: 597 RVA: 0x0000CD2E File Offset: 0x0000AF2E
		public ActivityIdFlowDictionary(ServiceModelDictionary dictionary)
		{
			this.ActivityId = dictionary.CreateString("ActivityId", 425);
			this.ActivityIdNamespace = dictionary.CreateString("http://schemas.microsoft.com/2004/09/ServiceModel/Diagnostics", 426);
		}

		// Token: 0x040004D1 RID: 1233
		public XmlDictionaryString ActivityId;

		// Token: 0x040004D2 RID: 1234
		public XmlDictionaryString ActivityIdNamespace;
	}
}
