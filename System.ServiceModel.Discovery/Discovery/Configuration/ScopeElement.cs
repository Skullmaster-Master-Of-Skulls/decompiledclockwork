using System;
using System.Configuration;

namespace System.ServiceModel.Discovery.Configuration
{
	// Token: 0x020000BA RID: 186
	public sealed class ScopeElement : ConfigurationElement
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x000137C0 File Offset: 0x000119C0
		// (set) Token: 0x0600077D RID: 1917 RVA: 0x000137D2 File Offset: 0x000119D2
		[ConfigurationProperty("scope", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		[CallbackValidator(CallbackMethodName = "ScopeValidatorCallback", Type = typeof(ScopeElement))]
		public Uri Scope
		{
			get
			{
				return (Uri)base["scope"];
			}
			set
			{
				base["scope"] = value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x000137E0 File Offset: 0x000119E0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("scope", typeof(Uri), null, null, new CallbackValidator(typeof(Uri), new ValidatorCallback(ScopeElement.ScopeValidatorCallback)), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00013840 File Offset: 0x00011A40
		internal static void ScopeValidatorCallback(object scope)
		{
			if (scope != null && !((Uri)scope).IsAbsoluteUri)
			{
				throw FxTrace.Exception.AsError(new ArgumentException(SR.DiscoveryConfigInvalidScopeUri(scope)));
			}
		}

		// Token: 0x040001CE RID: 462
		private ConfigurationPropertyCollection properties;
	}
}
