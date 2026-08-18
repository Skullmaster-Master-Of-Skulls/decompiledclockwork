using System;
using System.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000601 RID: 1537
	public sealed class CallbackDebugElement : BehaviorExtensionElement
	{
		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06003B38 RID: 15160 RVA: 0x000E2E35 File Offset: 0x000E1035
		// (set) Token: 0x06003B39 RID: 15161 RVA: 0x000E2E47 File Offset: 0x000E1047
		[ConfigurationProperty("includeExceptionDetailInFaults", DefaultValue = false)]
		public bool IncludeExceptionDetailInFaults
		{
			get
			{
				return (bool)base["includeExceptionDetailInFaults"];
			}
			set
			{
				base["includeExceptionDetailInFaults"] = value;
			}
		}

		// Token: 0x06003B3A RID: 15162 RVA: 0x000E2E5C File Offset: 0x000E105C
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			CallbackDebugElement callbackDebugElement = (CallbackDebugElement)from;
			this.IncludeExceptionDetailInFaults = callbackDebugElement.IncludeExceptionDetailInFaults;
		}

		// Token: 0x06003B3B RID: 15163 RVA: 0x000E2E83 File Offset: 0x000E1083
		protected internal override object CreateBehavior()
		{
			return new CallbackDebugBehavior(this.IncludeExceptionDetailInFaults);
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06003B3C RID: 15164 RVA: 0x000E2E90 File Offset: 0x000E1090
		public override Type BehaviorType
		{
			get
			{
				return typeof(CallbackDebugBehavior);
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06003B3D RID: 15165 RVA: 0x000E2E9C File Offset: 0x000E109C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("includeExceptionDetailInFaults", typeof(bool), false, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A81 RID: 10881
		private ConfigurationPropertyCollection properties;
	}
}
