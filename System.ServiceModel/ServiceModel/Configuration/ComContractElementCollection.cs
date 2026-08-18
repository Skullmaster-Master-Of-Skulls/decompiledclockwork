using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006F1 RID: 1777
	[ConfigurationCollection(typeof(ComContractElement), AddItemName = "comContract")]
	public sealed class ComContractElementCollection : ServiceModelEnhancedConfigurationElementCollection<ComContractElement>
	{
		// Token: 0x06004421 RID: 17441 RVA: 0x00101600 File Offset: 0x000FF800
		public ComContractElementCollection() : base("comContract")
		{
		}

		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x06004422 RID: 17442 RVA: 0x0010160D File Offset: 0x000FF80D
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004423 RID: 17443 RVA: 0x00101610 File Offset: 0x000FF810
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			ComContractElement comContractElement = (ComContractElement)element;
			return comContractElement.Contract;
		}
	}
}
