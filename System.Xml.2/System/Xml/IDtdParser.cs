using System;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x020000AA RID: 170
	internal interface IDtdParser
	{
		// Token: 0x060005E5 RID: 1509
		IDtdInfo ParseInternalDtd(IDtdParserAdapter adapter, bool saveInternalSubset);

		// Token: 0x060005E6 RID: 1510
		IDtdInfo ParseFreeFloatingDtd(string baseUri, string docTypeName, string publicId, string systemId, string internalSubset, IDtdParserAdapter adapter);

		// Token: 0x060005E7 RID: 1511
		Task<IDtdInfo> ParseInternalDtdAsync(IDtdParserAdapter adapter, bool saveInternalSubset);

		// Token: 0x060005E8 RID: 1512
		Task<IDtdInfo> ParseFreeFloatingDtdAsync(string baseUri, string docTypeName, string publicId, string systemId, string internalSubset, IDtdParserAdapter adapter);
	}
}
