using System;
using System.Configuration;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006A3 RID: 1699
	public sealed class DataContractSerializerElement : BehaviorExtensionElement
	{
		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x060041D3 RID: 16851 RVA: 0x000F9640 File Offset: 0x000F7840
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("ignoreExtensionDataObject", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxItemsInObjectGraph", typeof(int), int.MaxValue, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x060041D5 RID: 16853 RVA: 0x000F96C5 File Offset: 0x000F78C5
		// (set) Token: 0x060041D6 RID: 16854 RVA: 0x000F96D7 File Offset: 0x000F78D7
		[ConfigurationProperty("ignoreExtensionDataObject", DefaultValue = false)]
		public bool IgnoreExtensionDataObject
		{
			get
			{
				return (bool)base["ignoreExtensionDataObject"];
			}
			set
			{
				base["ignoreExtensionDataObject"] = value;
			}
		}

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x060041D7 RID: 16855 RVA: 0x000F96EA File Offset: 0x000F78EA
		// (set) Token: 0x060041D8 RID: 16856 RVA: 0x000F96FC File Offset: 0x000F78FC
		[ConfigurationProperty("maxItemsInObjectGraph", DefaultValue = 2147483647)]
		[IntegerValidator(MinValue = 0)]
		public int MaxItemsInObjectGraph
		{
			get
			{
				return (int)base["maxItemsInObjectGraph"];
			}
			set
			{
				base["maxItemsInObjectGraph"] = value;
			}
		}

		// Token: 0x060041D9 RID: 16857 RVA: 0x000F9710 File Offset: 0x000F7910
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			DataContractSerializerElement dataContractSerializerElement = (DataContractSerializerElement)from;
			this.IgnoreExtensionDataObject = dataContractSerializerElement.IgnoreExtensionDataObject;
			this.MaxItemsInObjectGraph = dataContractSerializerElement.MaxItemsInObjectGraph;
		}

		// Token: 0x060041DA RID: 16858 RVA: 0x000F9743 File Offset: 0x000F7943
		protected internal override object CreateBehavior()
		{
			return new DataContractSerializerServiceBehavior(this.IgnoreExtensionDataObject, this.MaxItemsInObjectGraph);
		}

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x060041DB RID: 16859 RVA: 0x000F9756 File Offset: 0x000F7956
		public override Type BehaviorType
		{
			get
			{
				return typeof(DataContractSerializerServiceBehavior);
			}
		}

		// Token: 0x04002CF1 RID: 11505
		private ConfigurationPropertyCollection properties;
	}
}
