using System;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000647 RID: 1607
	public sealed class MsmqTransportElement : MsmqElementBase
	{
		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06003DF3 RID: 15859 RVA: 0x000EC7A8 File Offset: 0x000EA9A8
		// (set) Token: 0x06003DF4 RID: 15860 RVA: 0x000EC7BA File Offset: 0x000EA9BA
		[ConfigurationProperty("maxPoolSize", DefaultValue = 8)]
		[IntegerValidator(MinValue = 0)]
		public int MaxPoolSize
		{
			get
			{
				return (int)base["maxPoolSize"];
			}
			set
			{
				base["maxPoolSize"] = value;
			}
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06003DF5 RID: 15861 RVA: 0x000EC7CD File Offset: 0x000EA9CD
		// (set) Token: 0x06003DF6 RID: 15862 RVA: 0x000EC7DF File Offset: 0x000EA9DF
		[ConfigurationProperty("queueTransferProtocol", DefaultValue = QueueTransferProtocol.Native)]
		[ServiceModelEnumValidator(typeof(QueueTransferProtocolHelper))]
		public QueueTransferProtocol QueueTransferProtocol
		{
			get
			{
				return (QueueTransferProtocol)base["queueTransferProtocol"];
			}
			set
			{
				base["queueTransferProtocol"] = value;
			}
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06003DF7 RID: 15863 RVA: 0x000EC7F2 File Offset: 0x000EA9F2
		// (set) Token: 0x06003DF8 RID: 15864 RVA: 0x000EC804 File Offset: 0x000EAA04
		[ConfigurationProperty("useActiveDirectory", DefaultValue = false)]
		public bool UseActiveDirectory
		{
			get
			{
				return (bool)base["useActiveDirectory"];
			}
			set
			{
				base["useActiveDirectory"] = value;
			}
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06003DF9 RID: 15865 RVA: 0x000EC817 File Offset: 0x000EAA17
		public override Type BindingElementType
		{
			get
			{
				return typeof(MsmqTransportBindingElement);
			}
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x000EC823 File Offset: 0x000EAA23
		protected override TransportBindingElement CreateDefaultBindingElement()
		{
			return new MsmqTransportBindingElement();
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x000EC82C File Offset: 0x000EAA2C
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			MsmqTransportBindingElement msmqTransportBindingElement = bindingElement as MsmqTransportBindingElement;
			msmqTransportBindingElement.MaxPoolSize = this.MaxPoolSize;
			msmqTransportBindingElement.QueueTransferProtocol = this.QueueTransferProtocol;
			msmqTransportBindingElement.UseActiveDirectory = this.UseActiveDirectory;
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x000EC86C File Offset: 0x000EAA6C
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			MsmqTransportElement msmqTransportElement = from as MsmqTransportElement;
			if (msmqTransportElement != null)
			{
				this.MaxPoolSize = msmqTransportElement.MaxPoolSize;
				this.QueueTransferProtocol = msmqTransportElement.QueueTransferProtocol;
				this.UseActiveDirectory = msmqTransportElement.UseActiveDirectory;
			}
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x000EC8B0 File Offset: 0x000EAAB0
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			base.InitializeFrom(bindingElement);
			MsmqTransportBindingElement msmqTransportBindingElement = bindingElement as MsmqTransportBindingElement;
			base.SetPropertyValueIfNotDefaultValue<int>("maxPoolSize", msmqTransportBindingElement.MaxPoolSize);
			base.SetPropertyValueIfNotDefaultValue<QueueTransferProtocol>("queueTransferProtocol", msmqTransportBindingElement.QueueTransferProtocol);
			base.SetPropertyValueIfNotDefaultValue<bool>("useActiveDirectory", msmqTransportBindingElement.UseActiveDirectory);
		}

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06003DFE RID: 15870 RVA: 0x000EC900 File Offset: 0x000EAB00
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					object lockObj = this.lockObj;
					lock (lockObj)
					{
						if (this.properties == null)
						{
							ConfigurationPropertyCollection configurationPropertyCollection = base.Properties;
							configurationPropertyCollection.Add(new ConfigurationProperty("maxPoolSize", typeof(int), 8, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("queueTransferProtocol", typeof(QueueTransferProtocol), QueueTransferProtocol.Native, null, new ServiceModelEnumValidator(typeof(QueueTransferProtocolHelper)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("useActiveDirectory", typeof(bool), false, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002C99 RID: 11417
		private ConfigurationPropertyCollection properties;
	}
}
