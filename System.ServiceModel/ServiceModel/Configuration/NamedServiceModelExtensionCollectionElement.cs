using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200064E RID: 1614
	public abstract class NamedServiceModelExtensionCollectionElement<TServiceModelExtensionElement> : ServiceModelExtensionCollectionElement<TServiceModelExtensionElement> where TServiceModelExtensionElement : ServiceModelExtensionElement
	{
		// Token: 0x06003E41 RID: 15937 RVA: 0x000ED521 File Offset: 0x000EB721
		internal NamedServiceModelExtensionCollectionElement(string extensionCollectionName, string name) : base(extensionCollectionName)
		{
			if (!string.IsNullOrEmpty(name))
			{
				this.Name = name;
				return;
			}
			this.Name = string.Empty;
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06003E42 RID: 15938 RVA: 0x000ED550 File Offset: 0x000EB750
		// (set) Token: 0x06003E43 RID: 15939 RVA: 0x000ED562 File Offset: 0x000EB762
		[ConfigurationProperty("name", Options = ConfigurationPropertyOptions.IsKey)]
		[StringValidator(MinLength = 0)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["name"] = value;
				base.SetIsModified();
			}
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06003E44 RID: 15940 RVA: 0x000ED588 File Offset: 0x000EB788
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					object obj = this.lockObj;
					lock (obj)
					{
						if (this.properties == null)
						{
							this.properties = base.Properties;
							this.properties.Add(new ConfigurationProperty("name", typeof(string), null, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.IsKey));
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002CA0 RID: 11424
		internal object lockObj = new object();

		// Token: 0x04002CA1 RID: 11425
		private ConfigurationPropertyCollection properties;
	}
}
