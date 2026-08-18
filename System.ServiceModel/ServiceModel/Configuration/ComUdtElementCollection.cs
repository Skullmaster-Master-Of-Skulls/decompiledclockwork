using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006F4 RID: 1780
	[ConfigurationCollection(typeof(ComUdtElement), AddItemName = "userDefinedType")]
	public sealed class ComUdtElementCollection : ServiceModelEnhancedConfigurationElementCollection<ComUdtElement>
	{
		// Token: 0x0600442A RID: 17450 RVA: 0x001016BD File Offset: 0x000FF8BD
		public ComUdtElementCollection() : base("userDefinedType")
		{
		}

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x0600442B RID: 17451 RVA: 0x001016CA File Offset: 0x000FF8CA
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600442C RID: 17452 RVA: 0x001016D0 File Offset: 0x000FF8D0
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			ComUdtElement comUdtElement = (ComUdtElement)element;
			return comUdtElement.TypeDefID;
		}
	}
}
