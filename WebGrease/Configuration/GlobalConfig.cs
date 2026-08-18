using System;
using System.Xml.Linq;

namespace WebGrease.Configuration
{
	// Token: 0x02000028 RID: 40
	public class GlobalConfig : INamedConfig
	{
		// Token: 0x060002FB RID: 763 RVA: 0x00007518 File Offset: 0x00005718
		public GlobalConfig(XElement settingElement)
		{
			this.Name = (((string)settingElement.Attribute("config")) ?? string.Empty);
			bool? flag = (bool?)settingElement.Attribute("treatWarningsAsErrors");
			this.TreatWarningsAsErrors = ((flag != null) ? new bool?(flag.GetValueOrDefault()) : ((bool?)settingElement.Element("TreatWarningsAsErrors")));
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00007597 File Offset: 0x00005797
		public GlobalConfig()
		{
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000759F File Offset: 0x0000579F
		// (set) Token: 0x060002FE RID: 766 RVA: 0x000075A7 File Offset: 0x000057A7
		public bool? TreatWarningsAsErrors { get; private set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002FF RID: 767 RVA: 0x000075B0 File Offset: 0x000057B0
		// (set) Token: 0x06000300 RID: 768 RVA: 0x000075B8 File Offset: 0x000057B8
		public string Name { get; private set; }
	}
}
