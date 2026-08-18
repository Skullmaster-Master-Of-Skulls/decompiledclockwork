using System;
using System.Xml.Linq;
using WebGrease.Extensions;

namespace WebGrease.Configuration
{
	// Token: 0x020000F6 RID: 246
	internal sealed class JsMinificationConfig : INamedConfig
	{
		// Token: 0x06000FC0 RID: 4032 RVA: 0x00047E3C File Offset: 0x0004603C
		public JsMinificationConfig()
		{
			this.ShouldMinify = true;
			this.GlobalsToIgnore = "jQuery";
			this.MinificationArugments = "";
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00047E64 File Offset: 0x00046064
		internal JsMinificationConfig(XElement element) : this()
		{
			this.Name = (((string)element.Attribute("config")) ?? string.Empty);
			foreach (XElement xelement in element.Descendants())
			{
				string text = xelement.Name.ToString();
				string value = xelement.Value;
				string a;
				if ((a = text) != null)
				{
					if (!(a == "Minify"))
					{
						if (!(a == "GlobalsToIgnore"))
						{
							if (a == "MinifyArguments")
							{
								this.MinificationArugments = ((!value.IsNullOrWhitespace()) ? value : "");
							}
						}
						else
						{
							this.GlobalsToIgnore = ((!value.IsNullOrWhitespace()) ? value : "jQuery");
						}
					}
					else
					{
						this.ShouldMinify = value.TryParseBool();
					}
				}
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x00047F5C File Offset: 0x0004615C
		// (set) Token: 0x06000FC3 RID: 4035 RVA: 0x00047F64 File Offset: 0x00046164
		public string Name { get; set; }

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x00047F6D File Offset: 0x0004616D
		// (set) Token: 0x06000FC5 RID: 4037 RVA: 0x00047F75 File Offset: 0x00046175
		internal bool ShouldMinify { get; set; }

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x00047F7E File Offset: 0x0004617E
		// (set) Token: 0x06000FC7 RID: 4039 RVA: 0x00047F86 File Offset: 0x00046186
		internal string GlobalsToIgnore { get; set; }

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x00047F8F File Offset: 0x0004618F
		// (set) Token: 0x06000FC9 RID: 4041 RVA: 0x00047F97 File Offset: 0x00046197
		internal string MinificationArugments { get; set; }
	}
}
