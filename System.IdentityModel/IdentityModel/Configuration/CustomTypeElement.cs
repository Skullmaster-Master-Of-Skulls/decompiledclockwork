using System;
using System.ComponentModel;
using System.Configuration;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001C3 RID: 451
	public sealed class CustomTypeElement : ConfigurationElementInterceptor
	{
		// Token: 0x06000E87 RID: 3719 RVA: 0x00041C1F File Offset: 0x0003FE1F
		public CustomTypeElement()
		{
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00041E73 File Offset: 0x00040073
		internal CustomTypeElement(Type typeName)
		{
			this.Type = typeName;
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00041E82 File Offset: 0x00040082
		public static T Resolve<T>(CustomTypeElement customTypeElement) where T : class
		{
			return TypeResolveHelper.Resolve<T>(customTypeElement, customTypeElement.Type);
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x00041E90 File Offset: 0x00040090
		public bool IsConfigured
		{
			get
			{
				return base.ElementInformation.Properties["type"].ValueOrigin > PropertyValueOrigin.Default;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x00041EAF File Offset: 0x000400AF
		// (set) Token: 0x06000E8C RID: 3724 RVA: 0x00041EC1 File Offset: 0x000400C1
		[ConfigurationProperty("type", IsRequired = true, IsKey = true)]
		[TypeConverter(typeof(TypeNameConverter))]
		public Type Type
		{
			get
			{
				return (Type)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x00041ED0 File Offset: 0x000400D0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("type", typeof(Type), null, new TypeNameConverter(), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04000D67 RID: 3431
		private ConfigurationPropertyCollection properties;
	}
}
