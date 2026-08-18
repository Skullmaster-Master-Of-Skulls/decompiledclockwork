using System;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200064C RID: 1612
	public sealed class NamedPipeTransportElement : ConnectionOrientedTransportElement
	{
		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06003E31 RID: 15921 RVA: 0x000ED2A6 File Offset: 0x000EB4A6
		public override Type BindingElementType
		{
			get
			{
				return typeof(NamedPipeTransportBindingElement);
			}
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x000ED2B4 File Offset: 0x000EB4B4
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			NamedPipeTransportBindingElement namedPipeTransportBindingElement = (NamedPipeTransportBindingElement)bindingElement;
			this.ConnectionPoolSettings.ApplyConfiguration(namedPipeTransportBindingElement.ConnectionPoolSettings);
			this.PipeSettings.ApplyConfiguration(namedPipeTransportBindingElement.PipeSettings);
		}

		// Token: 0x06003E33 RID: 15923 RVA: 0x000ED2F4 File Offset: 0x000EB4F4
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			base.InitializeFrom(bindingElement);
			NamedPipeTransportBindingElement namedPipeTransportBindingElement = (NamedPipeTransportBindingElement)bindingElement;
			this.ConnectionPoolSettings.InitializeFrom(namedPipeTransportBindingElement.ConnectionPoolSettings);
			this.PipeSettings.InitializeFrom(namedPipeTransportBindingElement.PipeSettings);
		}

		// Token: 0x06003E34 RID: 15924 RVA: 0x000ED334 File Offset: 0x000EB534
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			NamedPipeTransportElement namedPipeTransportElement = (NamedPipeTransportElement)from;
			this.ConnectionPoolSettings.CopyFrom(namedPipeTransportElement.ConnectionPoolSettings);
			this.PipeSettings.CopyFrom(namedPipeTransportElement.PipeSettings);
		}

		// Token: 0x06003E35 RID: 15925 RVA: 0x000ED371 File Offset: 0x000EB571
		protected override TransportBindingElement CreateDefaultBindingElement()
		{
			return new NamedPipeTransportBindingElement();
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06003E36 RID: 15926 RVA: 0x000ED378 File Offset: 0x000EB578
		// (set) Token: 0x06003E37 RID: 15927 RVA: 0x000ED38A File Offset: 0x000EB58A
		[ConfigurationProperty("connectionPoolSettings")]
		public NamedPipeConnectionPoolSettingsElement ConnectionPoolSettings
		{
			get
			{
				return (NamedPipeConnectionPoolSettingsElement)base["connectionPoolSettings"];
			}
			set
			{
				base["connectionPoolSettings"] = value;
			}
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06003E38 RID: 15928 RVA: 0x000ED398 File Offset: 0x000EB598
		// (set) Token: 0x06003E39 RID: 15929 RVA: 0x000ED3AA File Offset: 0x000EB5AA
		[ConfigurationProperty("pipeSettings")]
		public NamedPipeSettingsElement PipeSettings
		{
			get
			{
				return (NamedPipeSettingsElement)base["pipeSettings"];
			}
			set
			{
				base["pipeSettings"] = value;
			}
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06003E3A RID: 15930 RVA: 0x000ED3B8 File Offset: 0x000EB5B8
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
							configurationPropertyCollection.Add(new ConfigurationProperty("connectionPoolSettings", typeof(NamedPipeConnectionPoolSettingsElement), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("pipeSettings", typeof(NamedPipeSettingsElement), null, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002C9E RID: 11422
		private ConfigurationPropertyCollection properties;
	}
}
