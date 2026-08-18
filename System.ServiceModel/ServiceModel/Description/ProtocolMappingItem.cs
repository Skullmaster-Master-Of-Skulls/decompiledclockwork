using System;

namespace System.ServiceModel.Description
{
	// Token: 0x020003FD RID: 1021
	internal class ProtocolMappingItem
	{
		// Token: 0x060026F4 RID: 9972 RVA: 0x0008ED44 File Offset: 0x0008CF44
		public ProtocolMappingItem(string binding, string bindingConfiguration)
		{
			this.Binding = binding;
			this.BindingConfiguration = bindingConfiguration;
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x060026F5 RID: 9973 RVA: 0x0008ED5A File Offset: 0x0008CF5A
		// (set) Token: 0x060026F6 RID: 9974 RVA: 0x0008ED62 File Offset: 0x0008CF62
		public string Binding { get; set; }

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x060026F7 RID: 9975 RVA: 0x0008ED6B File Offset: 0x0008CF6B
		// (set) Token: 0x060026F8 RID: 9976 RVA: 0x0008ED73 File Offset: 0x0008CF73
		public string BindingConfiguration { get; set; }
	}
}
