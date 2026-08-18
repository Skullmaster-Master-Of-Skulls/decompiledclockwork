using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000679 RID: 1657
	public class StandardBindingCollectionElement<TStandardBinding, TBindingConfiguration> : BindingCollectionElement where TStandardBinding : Binding where TBindingConfiguration : StandardBindingElement, new()
	{
		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x06003FA1 RID: 16289 RVA: 0x000F1554 File Offset: 0x000EF754
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("", typeof(StandardBindingElementCollection<TBindingConfiguration>), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x06003FA2 RID: 16290 RVA: 0x000F159A File Offset: 0x000EF79A
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public StandardBindingElementCollection<TBindingConfiguration> Bindings
		{
			get
			{
				return (StandardBindingElementCollection<TBindingConfiguration>)base[""];
			}
		}

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x06003FA3 RID: 16291 RVA: 0x000F15AC File Offset: 0x000EF7AC
		public override Type BindingType
		{
			get
			{
				return typeof(TStandardBinding);
			}
		}

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x06003FA4 RID: 16292 RVA: 0x000F15B8 File Offset: 0x000EF7B8
		public override ReadOnlyCollection<IBindingConfigurationElement> ConfiguredBindings
		{
			get
			{
				List<IBindingConfigurationElement> list = new List<IBindingConfigurationElement>();
				foreach (object obj in this.Bindings)
				{
					IBindingConfigurationElement item = (IBindingConfigurationElement)obj;
					list.Add(item);
				}
				return new ReadOnlyCollection<IBindingConfigurationElement>(list);
			}
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x000F1620 File Offset: 0x000EF820
		public override bool ContainsKey(string name)
		{
			return this.Bindings.ContainsKey(name);
		}

		// Token: 0x06003FA6 RID: 16294 RVA: 0x000F163B File Offset: 0x000EF83B
		protected internal override Binding GetDefault()
		{
			return Activator.CreateInstance<TStandardBinding>();
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x000F1648 File Offset: 0x000EF848
		protected internal override bool TryAdd(string name, Binding binding, Configuration config)
		{
			bool flag = binding.GetType() == typeof(TStandardBinding) && typeof(StandardBindingElement).IsAssignableFrom(typeof(TBindingConfiguration));
			if (flag)
			{
				TBindingConfiguration tbindingConfiguration = Activator.CreateInstance<TBindingConfiguration>();
				tbindingConfiguration.Name = name;
				tbindingConfiguration.InitializeFrom(binding);
				this.Bindings.Add(tbindingConfiguration);
			}
			return flag;
		}

		// Token: 0x04002CBB RID: 11451
		private ConfigurationPropertyCollection properties;
	}
}
