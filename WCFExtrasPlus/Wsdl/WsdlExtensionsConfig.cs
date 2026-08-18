using System;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace WCFExtrasPlus.Wsdl
{
	// Token: 0x02000022 RID: 34
	internal class WsdlExtensionsConfig : BehaviorExtensionElement
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000057C8 File Offset: 0x000039C8
		public override Type BehaviorType
		{
			get
			{
				return typeof(WsdlExtensions);
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000057D4 File Offset: 0x000039D4
		protected override object CreateBehavior()
		{
			return new WsdlExtensions(this);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000057DC File Offset: 0x000039DC
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x000057EE File Offset: 0x000039EE
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

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000057FC File Offset: 0x000039FC
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x0000580E File Offset: 0x00003A0E
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
