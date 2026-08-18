using System;
using System.Xml.Linq;
using WebGrease.Extensions;

namespace WebGrease.Configuration
{
	// Token: 0x020000EF RID: 239
	public class AutoNameConfig : INamedConfig
	{
		// Token: 0x06000F58 RID: 3928 RVA: 0x00046D72 File Offset: 0x00044F72
		public AutoNameConfig()
		{
			this.ShouldAutoName = true;
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00046D84 File Offset: 0x00044F84
		internal AutoNameConfig(XElement element) : this()
		{
			this.Name = (((string)element.Attribute("config")) ?? string.Empty);
			foreach (XElement xelement in element.Descendants())
			{
				string text = xelement.Name.ToString();
				string value = xelement.Value;
				string a;
				if ((a = text) != null && a == "RenameFiles")
				{
					this.ShouldAutoName = value.TryParseBool();
				}
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x00046E28 File Offset: 0x00045028
		// (set) Token: 0x06000F5B RID: 3931 RVA: 0x00046E30 File Offset: 0x00045030
		public bool ShouldAutoName { get; private set; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x00046E39 File Offset: 0x00045039
		// (set) Token: 0x06000F5D RID: 3933 RVA: 0x00046E41 File Offset: 0x00045041
		public string Name { get; private set; }
	}
}
