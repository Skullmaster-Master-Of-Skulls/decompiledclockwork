using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006C4 RID: 1732
	[ConfigurationCollection(typeof(ServiceActivationElement))]
	public sealed class ServiceActivationElementCollection : ServiceModelConfigurationElementCollection<ServiceActivationElement>
	{
		// Token: 0x06004323 RID: 17187 RVA: 0x000FDC37 File Offset: 0x000FBE37
		public ServiceActivationElementCollection() : base(ConfigurationElementCollectionType.AddRemoveClearMap, "add")
		{
		}

		// Token: 0x06004324 RID: 17188 RVA: 0x000FDC45 File Offset: 0x000FBE45
		protected override ConfigurationElement CreateNewElement()
		{
			return new ServiceActivationElement();
		}

		// Token: 0x06004325 RID: 17189 RVA: 0x000FDC4C File Offset: 0x000FBE4C
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			ServiceActivationElement serviceActivationElement = (ServiceActivationElement)element;
			return serviceActivationElement.RelativeAddress;
		}

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x06004326 RID: 17190 RVA: 0x000FDC79 File Offset: 0x000FBE79
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return true;
			}
		}
	}
}
