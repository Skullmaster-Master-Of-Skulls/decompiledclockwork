using System;
using System.Configuration;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000040 RID: 64
	[ConfigurationCollection(typeof(ContainerElement), AddItemName = "container")]
	public class ContainerElementCollection : DeserializableConfigurationElementCollection<ContainerElement>
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00007465 File Offset: 0x00005665
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000746D File Offset: 0x0000566D
		internal UnityConfigurationSection ContainingSection { get; set; }

		// Token: 0x17000077 RID: 119
		public ContainerElement this[string name]
		{
			get
			{
				return this.GetElement(name);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000747F File Offset: 0x0000567F
		public ContainerElement Default
		{
			get
			{
				return this.GetElement(string.Empty);
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000748C File Offset: 0x0000568C
		protected override ContainerElement GetElement(int index)
		{
			return this.PrepareElement(base.GetElement(index));
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000749B File Offset: 0x0000569B
		protected override ContainerElement GetElement(object key)
		{
			return this.PrepareElement(base.GetElement(key));
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000074AA File Offset: 0x000056AA
		private ContainerElement PrepareElement(ContainerElement element)
		{
			if (element != null)
			{
				element.ContainingSection = this.ContainingSection;
			}
			return element;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000074BC File Offset: 0x000056BC
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ContainerElement)element).Name;
		}
	}
}
