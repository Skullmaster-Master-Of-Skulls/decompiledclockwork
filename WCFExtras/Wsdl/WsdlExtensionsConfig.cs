using System;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace WCFExtras.Wsdl
{
	// Token: 0x02000018 RID: 24
	internal class WsdlExtensionsConfig : BehaviorExtensionElement
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00004A14 File Offset: 0x00002C14
		public override Type BehaviorType
		{
			get
			{
				return typeof(WsdlExtensions);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004A30 File Offset: 0x00002C30
		protected override object CreateBehavior()
		{
			return new WsdlExtensions(this);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00004A48 File Offset: 0x00002C48
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00004A6A File Offset: 0x00002C6A
		[ConfigurationProperty("location", DefaultValue = null)]
		public Uri Location
		{
			get
			{
				return (Uri)base["location"];
			}
			set
			{
				base["location"] = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004A7C File Offset: 0x00002C7C
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00004A9E File Offset: 0x00002C9E
		[ConfigurationProperty("singleFile", DefaultValue = false)]
		public bool SingleFile
		{
			get
			{
				return (bool)base["singleFile"];
			}
			set
			{
				base["singleFile"] = value;
			}
		}
	}
}
