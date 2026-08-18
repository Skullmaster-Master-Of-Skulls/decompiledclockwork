using System;
using System.Reflection;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000C6 RID: 198
	[LayoutRenderer("assembly-version")]
	public class AssemblyVersionLayoutRenderer : LayoutRenderer
	{
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0000CFC8 File Offset: 0x0000B1C8
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x0000CFD0 File Offset: 0x0000B1D0
		[DefaultParameter]
		public string Name { get; set; }

		// Token: 0x060005C9 RID: 1481 RVA: 0x0000CFDC File Offset: 0x0000B1DC
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			bool flag = !string.IsNullOrEmpty(this.Name);
			Assembly assembly;
			if (flag)
			{
				assembly = Assembly.Load(new AssemblyName(this.Name));
			}
			else
			{
				assembly = Assembly.GetEntryAssembly();
			}
			string text = string.Format("Could not find {0}", flag ? ("assembly " + this.Name) : "entry assembly");
			string value = (assembly == null) ? text : assembly.GetName().Version.ToString();
			builder.Append(value);
		}
	}
}
