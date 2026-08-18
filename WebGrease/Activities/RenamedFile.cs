using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace WebGrease.Activities
{
	// Token: 0x0200003E RID: 62
	internal sealed class RenamedFile
	{
		// Token: 0x060003D4 RID: 980 RVA: 0x0000C338 File Offset: 0x0000A538
		public RenamedFile(XContainer fileElement)
		{
			if (fileElement == null)
			{
				throw new ArgumentNullException("fileElement", "The fileElement cannot be null.");
			}
			this.InputNames = new List<string>();
			XElement xelement = fileElement.Element("Output");
			if (xelement != null)
			{
				this.OutputName = xelement.Value;
			}
			foreach (XElement xelement2 in fileElement.Elements("Input"))
			{
				this.InputNames.Add(xelement2.Value);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x0000C3E8 File Offset: 0x0000A5E8
		public string OutputName { get; private set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000C3F1 File Offset: 0x0000A5F1
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x0000C3F9 File Offset: 0x0000A5F9
		public List<string> InputNames { get; private set; }
	}
}
