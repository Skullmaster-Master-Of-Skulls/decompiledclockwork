using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace WebGrease.Activities
{
	// Token: 0x0200003F RID: 63
	internal sealed class RenamedFilesLog
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x0000C404 File Offset: 0x0000A604
		internal RenamedFilesLog(string logFile)
		{
			this.RenamedFiles = new List<RenamedFile>();
			if (string.IsNullOrWhiteSpace(logFile) || !File.Exists(logFile))
			{
				return;
			}
			XDocument xdocument = XDocument.Load(logFile);
			XElement xelement = xdocument.Element("RenamedFiles");
			if (xelement == null)
			{
				return;
			}
			foreach (XElement fileElement in xelement.Elements("File"))
			{
				RenamedFile item = new RenamedFile(fileElement);
				this.RenamedFiles.Add(item);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000C4AC File Offset: 0x0000A6AC
		// (set) Token: 0x060003DB RID: 987 RVA: 0x0000C4B4 File Offset: 0x0000A6B4
		internal List<RenamedFile> RenamedFiles { get; private set; }
	}
}
