using System;

namespace System.Web.UI
{
	// Token: 0x02000247 RID: 583
	internal abstract class TemplateControlDependencyParser : DependencyParser
	{
		// Token: 0x06001B07 RID: 6919 RVA: 0x00054EFF File Offset: 0x000530FF
		internal override void ProcessMainDirectiveAttribute(string deviceName, string name, string value)
		{
			if (name == "masterpagefile")
			{
				value = value.Trim();
				if (value.Length > 0)
				{
					base.AddDependency(VirtualPath.Create(value));
					return;
				}
			}
			else
			{
				base.ProcessMainDirectiveAttribute(deviceName, name, value);
			}
		}
	}
}
