using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006F3 RID: 1779
	[ConfigurationCollection(typeof(ComPersistableTypeElement), AddItemName = "type")]
	public sealed class ComPersistableTypeElementCollection : ServiceModelEnhancedConfigurationElementCollection<ComPersistableTypeElement>
	{
		// Token: 0x06004427 RID: 17447 RVA: 0x0010167D File Offset: 0x000FF87D
		public ComPersistableTypeElementCollection() : base("type")
		{
		}

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x06004428 RID: 17448 RVA: 0x0010168A File Offset: 0x000FF88A
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004429 RID: 17449 RVA: 0x00101690 File Offset: 0x000FF890
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			ComPersistableTypeElement comPersistableTypeElement = (ComPersistableTypeElement)element;
			return comPersistableTypeElement.ID;
		}
	}
}
