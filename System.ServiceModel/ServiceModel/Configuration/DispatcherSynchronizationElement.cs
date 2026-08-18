using System;
using System.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006BE RID: 1726
	public sealed class DispatcherSynchronizationElement : BehaviorExtensionElement
	{
		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x06004301 RID: 17153 RVA: 0x000FD1FC File Offset: 0x000FB3FC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("asynchronousSendEnabled", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxPendingReceives", typeof(int), 1, null, new IntegerValidator(1, int.MaxValue, false), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x06004303 RID: 17155 RVA: 0x000FD27D File Offset: 0x000FB47D
		// (set) Token: 0x06004304 RID: 17156 RVA: 0x000FD28F File Offset: 0x000FB48F
		[ConfigurationProperty("asynchronousSendEnabled", DefaultValue = false)]
		public bool AsynchronousSendEnabled
		{
			get
			{
				return (bool)base["asynchronousSendEnabled"];
			}
			set
			{
				base["asynchronousSendEnabled"] = value;
			}
		}

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x06004305 RID: 17157 RVA: 0x000FD2A2 File Offset: 0x000FB4A2
		// (set) Token: 0x06004306 RID: 17158 RVA: 0x000FD2B4 File Offset: 0x000FB4B4
		[ConfigurationProperty("maxPendingReceives", DefaultValue = 1)]
		[IntegerValidator(MinValue = 1)]
		public int MaxPendingReceives
		{
			get
			{
				return (int)base["maxPendingReceives"];
			}
			set
			{
				base["maxPendingReceives"] = value;
			}
		}

		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x06004307 RID: 17159 RVA: 0x000FD2C7 File Offset: 0x000FB4C7
		public override Type BehaviorType
		{
			get
			{
				return typeof(DispatcherSynchronizationBehavior);
			}
		}

		// Token: 0x06004308 RID: 17160 RVA: 0x000FD2D4 File Offset: 0x000FB4D4
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			DispatcherSynchronizationElement dispatcherSynchronizationElement = (DispatcherSynchronizationElement)from;
			this.AsynchronousSendEnabled = dispatcherSynchronizationElement.AsynchronousSendEnabled;
			this.MaxPendingReceives = dispatcherSynchronizationElement.MaxPendingReceives;
		}

		// Token: 0x06004309 RID: 17161 RVA: 0x000FD307 File Offset: 0x000FB507
		protected internal override object CreateBehavior()
		{
			return new DispatcherSynchronizationBehavior(this.AsynchronousSendEnabled, this.MaxPendingReceives);
		}

		// Token: 0x04002D0E RID: 11534
		private ConfigurationPropertyCollection properties;
	}
}
