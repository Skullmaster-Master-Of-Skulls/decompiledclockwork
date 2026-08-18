using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000615 RID: 1557
	public sealed class DelegatingHandlerElement : ConfigurationElement
	{
		// Token: 0x06003BEA RID: 15338 RVA: 0x000E5329 File Offset: 0x000E3529
		public DelegatingHandlerElement()
		{
		}

		// Token: 0x06003BEB RID: 15339 RVA: 0x000E533C File Offset: 0x000E353C
		internal DelegatingHandlerElement(Type handlerType)
		{
			this.Type = handlerType.AssemblyQualifiedName;
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06003BEC RID: 15340 RVA: 0x000E535B File Offset: 0x000E355B
		// (set) Token: 0x06003BED RID: 15341 RVA: 0x000E536D File Offset: 0x000E356D
		[ConfigurationProperty("type", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		[StringValidator(MinLength = 1)]
		public string Type
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					value = string.Empty;
				}
				base["type"] = value;
			}
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06003BEE RID: 15342 RVA: 0x000E538A File Offset: 0x000E358A
		internal Guid Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06003BEF RID: 15343 RVA: 0x000E5394 File Offset: 0x000E3594
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("type", typeof(string), null, null, new StringValidator(1, int.MaxValue, null), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C76 RID: 11382
		private Guid id = Guid.NewGuid();

		// Token: 0x04002C77 RID: 11383
		private ConfigurationPropertyCollection properties;
	}
}
