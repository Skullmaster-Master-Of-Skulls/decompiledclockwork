using System;
using System.Collections;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014C5 RID: 5317
	internal class ExtensionPropertyMapping
	{
		// Token: 0x0600D593 RID: 54675 RVA: 0x002F3BBD File Offset: 0x002F1DBD
		static ExtensionPropertyMapping()
		{
			ExtensionPropertyMapping.mapping.Add("external-destination", ExternalDestinationMaker.Maker("external-destination"));
			ExtensionPropertyMapping.mapping.Add("internal-destination", InternalDestinationMaker.Maker("internal-destination"));
		}

		// Token: 0x0600D594 RID: 54676 RVA: 0x002F3BFB File Offset: 0x002F1DFB
		public static Hashtable getGenericMappings()
		{
			return ExtensionPropertyMapping.mapping;
		}

		// Token: 0x04003A6A RID: 14954
		private static Hashtable mapping = new Hashtable();
	}
}
