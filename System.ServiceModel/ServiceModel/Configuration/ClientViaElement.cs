using System;
using System.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005FE RID: 1534
	public sealed class ClientViaElement : BehaviorExtensionElement
	{
		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06003B28 RID: 15144 RVA: 0x000E2C68 File Offset: 0x000E0E68
		// (set) Token: 0x06003B29 RID: 15145 RVA: 0x000E2C7A File Offset: 0x000E0E7A
		[ConfigurationProperty("viaUri")]
		public Uri ViaUri
		{
			get
			{
				return (Uri)base["viaUri"];
			}
			set
			{
				base["viaUri"] = value;
			}
		}

		// Token: 0x06003B2A RID: 15146 RVA: 0x000E2C88 File Offset: 0x000E0E88
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			ClientViaElement clientViaElement = (ClientViaElement)from;
			this.ViaUri = clientViaElement.ViaUri;
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x000E2CAF File Offset: 0x000E0EAF
		protected internal override object CreateBehavior()
		{
			return new ClientViaBehavior(this.ViaUri);
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06003B2C RID: 15148 RVA: 0x000E2CBC File Offset: 0x000E0EBC
		public override Type BehaviorType
		{
			get
			{
				return typeof(ClientViaBehavior);
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06003B2D RID: 15149 RVA: 0x000E2CC8 File Offset: 0x000E0EC8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("viaUri", typeof(Uri), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A7F RID: 10879
		private ConfigurationPropertyCollection properties;
	}
}
