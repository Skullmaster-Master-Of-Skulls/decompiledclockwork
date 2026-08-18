using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006F2 RID: 1778
	[ConfigurationCollection(typeof(ComMethodElement))]
	public sealed class ComMethodElementCollection : ServiceModelEnhancedConfigurationElementCollection<ComMethodElement>
	{
		// Token: 0x06004424 RID: 17444 RVA: 0x0010163D File Offset: 0x000FF83D
		public ComMethodElementCollection() : base("add")
		{
		}

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x06004425 RID: 17445 RVA: 0x0010164A File Offset: 0x000FF84A
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004426 RID: 17446 RVA: 0x00101650 File Offset: 0x000FF850
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			ComMethodElement comMethodElement = (ComMethodElement)element;
			return comMethodElement.ExposedMethod;
		}
	}
}
