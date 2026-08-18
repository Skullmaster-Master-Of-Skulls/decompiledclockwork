using System;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000516 RID: 1302
	internal class ImporterCallback : ITypeLibImporterNotifySink
	{
		// Token: 0x060032AD RID: 12973 RVA: 0x000AB454 File Offset: 0x000AA454
		public void ReportEvent(ImporterEventKind EventKind, int EventCode, string EventMsg)
		{
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x000AB458 File Offset: 0x000AA458
		public Assembly ResolveRef(object TypeLib)
		{
			Assembly result;
			try
			{
				ITypeLibConverter typeLibConverter = new TypeLibConverter();
				result = typeLibConverter.ConvertTypeLibToAssembly(TypeLib, Marshal.GetTypeLibName((ITypeLib)TypeLib) + ".dll", TypeLibImporterFlags.None, new ImporterCallback(), null, null, null, null);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}
	}
}
