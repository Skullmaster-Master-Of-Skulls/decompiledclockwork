using System;
using System.IdentityModel;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Security
{
	// Token: 0x02000350 RID: 848
	internal class ServiceModelDictionaryManager
	{
		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x00071C68 File Offset: 0x0006FE68
		public static DictionaryManager Instance
		{
			get
			{
				if (ServiceModelDictionaryManager.dictionaryManager == null)
				{
					ServiceModelDictionaryManager.dictionaryManager = new DictionaryManager(BinaryMessageEncoderFactory.XmlDictionary);
				}
				return ServiceModelDictionaryManager.dictionaryManager;
			}
		}

		// Token: 0x04001EB3 RID: 7859
		private static DictionaryManager dictionaryManager;
	}
}
