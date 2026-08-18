using System;
using System.Xml.Linq;
using WebGrease.Extensions;

namespace WebGrease.Configuration
{
	// Token: 0x020000F0 RID: 240
	public class BundlingConfig : INamedConfig
	{
		// Token: 0x06000F5E RID: 3934 RVA: 0x00046E4A File Offset: 0x0004504A
		public BundlingConfig()
		{
			this.ShouldBundleFiles = true;
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x00046E5C File Offset: 0x0004505C
		internal BundlingConfig(XElement element) : this()
		{
			this.Name = (((string)element.Attribute("config")) ?? string.Empty);
			foreach (XElement xelement in element.Descendants())
			{
				string text = xelement.Name.ToString();
				string value = xelement.Value;
				string a;
				if ((a = text) != null)
				{
					if (!(a == "AssembleFiles"))
					{
						if (a == "MinimalOutput")
						{
							this.MinimalOutput = value.TryParseBool();
						}
					}
					else
					{
						this.ShouldBundleFiles = value.TryParseBool();
					}
				}
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x00046F20 File Offset: 0x00045120
		// (set) Token: 0x06000F61 RID: 3937 RVA: 0x00046F28 File Offset: 0x00045128
		public bool ShouldBundleFiles { get; private set; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x00046F31 File Offset: 0x00045131
		// (set) Token: 0x06000F63 RID: 3939 RVA: 0x00046F39 File Offset: 0x00045139
		public bool MinimalOutput { get; private set; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x00046F42 File Offset: 0x00045142
		// (set) Token: 0x06000F65 RID: 3941 RVA: 0x00046F4A File Offset: 0x0004514A
		public string Name { get; private set; }
	}
}
