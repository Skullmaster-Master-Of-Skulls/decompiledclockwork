using System;
using System.Xml.Linq;
using WebGrease.Extensions;

namespace WebGrease.Configuration
{
	// Token: 0x020000F7 RID: 247
	public class JSValidationConfig : INamedConfig
	{
		// Token: 0x06000FCA RID: 4042 RVA: 0x00047FA0 File Offset: 0x000461A0
		public JSValidationConfig()
		{
			this.ShouldAnalyze = true;
			this.AnalyzeArguments = "-analyze -WARN:4";
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00047FBC File Offset: 0x000461BC
		internal JSValidationConfig(XElement element) : this()
		{
			this.Name = (((string)element.Attribute("config")) ?? string.Empty);
			foreach (XElement xelement in element.Descendants())
			{
				string text = xelement.Name.ToString();
				string value = xelement.Value;
				string a;
				if ((a = text) != null)
				{
					if (!(a == "Analyze"))
					{
						if (a == "AnalayzeArguments" || a == "AnalyzeArguments")
						{
							this.AnalyzeArguments = ((!value.IsNullOrWhitespace()) ? value : "-analyze -WARN:4");
						}
					}
					else
					{
						this.ShouldAnalyze = value.TryParseBool();
					}
				}
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x00048098 File Offset: 0x00046298
		// (set) Token: 0x06000FCD RID: 4045 RVA: 0x000480A0 File Offset: 0x000462A0
		public string Name { get; set; }

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x000480A9 File Offset: 0x000462A9
		// (set) Token: 0x06000FCF RID: 4047 RVA: 0x000480B1 File Offset: 0x000462B1
		internal bool ShouldAnalyze { get; set; }

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x000480BA File Offset: 0x000462BA
		// (set) Token: 0x06000FD1 RID: 4049 RVA: 0x000480C2 File Offset: 0x000462C2
		internal string AnalyzeArguments { get; set; }
	}
}
