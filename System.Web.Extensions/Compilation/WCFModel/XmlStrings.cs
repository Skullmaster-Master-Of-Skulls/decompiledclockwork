using System;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x02000028 RID: 40
	internal class XmlStrings
	{
		// Token: 0x02000139 RID: 313
		internal class DISCO
		{
			// Token: 0x0400047E RID: 1150
			internal const string Prefix = "disco";

			// Token: 0x0400047F RID: 1151
			internal const string NamespaceUri = "http://schemas.xmlsoap.org/disco/";

			// Token: 0x02000181 RID: 385
			internal class Elements
			{
				// Token: 0x0400052F RID: 1327
				internal const string Root = "discovery";
			}
		}

		// Token: 0x0200013A RID: 314
		internal class WSDL
		{
			// Token: 0x04000480 RID: 1152
			internal const string Prefix = "wsdl";

			// Token: 0x04000481 RID: 1153
			internal const string NamespaceUri = "http://schemas.xmlsoap.org/wsdl/";

			// Token: 0x02000182 RID: 386
			internal class Elements
			{
				// Token: 0x04000530 RID: 1328
				internal const string Root = "definitions";
			}
		}

		// Token: 0x0200013B RID: 315
		internal class XmlSchema
		{
			// Token: 0x04000482 RID: 1154
			internal const string Prefix = "xsd";

			// Token: 0x04000483 RID: 1155
			internal const string NamespaceUri = "http://www.w3.org/2001/XMLSchema";

			// Token: 0x02000183 RID: 387
			internal class Elements
			{
				// Token: 0x04000531 RID: 1329
				internal const string Root = "schema";
			}
		}

		// Token: 0x0200013C RID: 316
		internal class DataSet
		{
			// Token: 0x04000484 RID: 1156
			internal const string NamespaceUri = "urn:schemas-microsoft-com:xml-msdata";

			// Token: 0x02000184 RID: 388
			internal class Attributes
			{
				// Token: 0x04000532 RID: 1330
				internal const string IsDataSet = "IsDataSet";
			}
		}

		// Token: 0x0200013D RID: 317
		internal class MetadataExchange
		{
			// Token: 0x04000485 RID: 1157
			internal const string Prefix = "wsx";

			// Token: 0x04000486 RID: 1158
			internal const string Name = "WS-MetadataExchange";

			// Token: 0x04000487 RID: 1159
			internal const string NamespaceUri = "http://schemas.xmlsoap.org/ws/2004/09/mex";

			// Token: 0x02000185 RID: 389
			internal class Elements
			{
				// Token: 0x04000533 RID: 1331
				internal const string Metadata = "Metadata";
			}
		}

		// Token: 0x0200013E RID: 318
		internal class WsdlContractInheritance
		{
			// Token: 0x04000488 RID: 1160
			internal const string Prefix = "wsdl-ex";

			// Token: 0x04000489 RID: 1161
			internal const string NamespaceUri = "http://schemas.microsoft.com/ws/2005/01/WSDL/Extensions/ContractInheritance";
		}

		// Token: 0x0200013F RID: 319
		internal class Xml
		{
			// Token: 0x0400048A RID: 1162
			internal const string Prefix = "xml";

			// Token: 0x0400048B RID: 1163
			internal const string NamespaceUri = "http://www.w3.org/XML/1998/namespace";

			// Token: 0x02000186 RID: 390
			internal class Attributes
			{
				// Token: 0x04000534 RID: 1332
				internal const string Base = "base";

				// Token: 0x04000535 RID: 1333
				internal const string Id = "id";
			}
		}

		// Token: 0x02000140 RID: 320
		internal class WSAddressing
		{
			// Token: 0x0400048C RID: 1164
			internal const string Prefix = "wsa";

			// Token: 0x0400048D RID: 1165
			internal const string NamespaceUri = "http://schemas.xmlsoap.org/ws/2004/08/addressing";

			// Token: 0x02000187 RID: 391
			internal class Elements
			{
				// Token: 0x04000536 RID: 1334
				internal const string EndpointReference = "EndpointReference";
			}
		}

		// Token: 0x02000141 RID: 321
		internal class Wsu
		{
			// Token: 0x0400048E RID: 1166
			internal const string Prefix = "wsu";

			// Token: 0x0400048F RID: 1167
			internal const string NamespaceUri = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";

			// Token: 0x02000188 RID: 392
			internal class Attributes
			{
				// Token: 0x04000537 RID: 1335
				internal const string Id = "Id";
			}
		}

		// Token: 0x02000142 RID: 322
		internal class WSPolicy
		{
			// Token: 0x04000490 RID: 1168
			internal const string Prefix = "wsp";

			// Token: 0x04000491 RID: 1169
			internal const string NamespaceUri = "http://schemas.xmlsoap.org/ws/2004/09/policy";

			// Token: 0x04000492 RID: 1170
			internal const string NamespaceUri15 = "http://www.w3.org/ns/ws-policy";

			// Token: 0x02000189 RID: 393
			internal class Attributes
			{
				// Token: 0x04000538 RID: 1336
				internal const string PolicyURIs = "PolicyURIs";
			}

			// Token: 0x0200018A RID: 394
			internal class Elements
			{
				// Token: 0x04000539 RID: 1337
				internal const string PolicyReference = "PolicyReference";

				// Token: 0x0400053A RID: 1338
				internal const string All = "All";

				// Token: 0x0400053B RID: 1339
				internal const string ExactlyOne = "ExactlyOne";

				// Token: 0x0400053C RID: 1340
				internal const string Policy = "Policy";
			}
		}

		// Token: 0x02000143 RID: 323
		internal class DataServices
		{
			// Token: 0x04000493 RID: 1171
			internal const string NamespaceUri = "http://schemas.microsoft.com/ado/2007/06/edmx";

			// Token: 0x0200018B RID: 395
			internal class Elements
			{
				// Token: 0x0400053D RID: 1341
				internal const string Root = "Edmx";
			}
		}
	}
}
