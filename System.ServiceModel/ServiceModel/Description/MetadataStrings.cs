using System;

namespace System.ServiceModel.Description
{
	// Token: 0x0200042F RID: 1071
	internal static class MetadataStrings
	{
		// Token: 0x02000C04 RID: 3076
		public static class MetadataExchangeStrings
		{
			// Token: 0x040042D0 RID: 17104
			public const string Prefix = "wsx";

			// Token: 0x040042D1 RID: 17105
			public const string Name = "WS-MetadataExchange";

			// Token: 0x040042D2 RID: 17106
			public const string Namespace = "http://schemas.xmlsoap.org/ws/2004/09/mex";

			// Token: 0x040042D3 RID: 17107
			public const string HttpBindingName = "MetadataExchangeHttpBinding";

			// Token: 0x040042D4 RID: 17108
			public const string HttpsBindingName = "MetadataExchangeHttpsBinding";

			// Token: 0x040042D5 RID: 17109
			public const string TcpBindingName = "MetadataExchangeTcpBinding";

			// Token: 0x040042D6 RID: 17110
			public const string NamedPipeBindingName = "MetadataExchangeNamedPipeBinding";

			// Token: 0x040042D7 RID: 17111
			public const string BindingNamespace = "http://schemas.microsoft.com/ws/2005/02/mex/bindings";

			// Token: 0x040042D8 RID: 17112
			public const string Metadata = "Metadata";

			// Token: 0x040042D9 RID: 17113
			public const string MetadataSection = "MetadataSection";

			// Token: 0x040042DA RID: 17114
			public const string Dialect = "Dialect";

			// Token: 0x040042DB RID: 17115
			public const string Identifier = "Identifier";

			// Token: 0x040042DC RID: 17116
			public const string MetadataReference = "MetadataReference";

			// Token: 0x040042DD RID: 17117
			public const string Location = "Location";
		}

		// Token: 0x02000C05 RID: 3077
		public static class WSTransfer
		{
			// Token: 0x040042DE RID: 17118
			public const string Prefix = "wxf";

			// Token: 0x040042DF RID: 17119
			public const string Name = "WS-Transfer";

			// Token: 0x040042E0 RID: 17120
			public const string Namespace = "http://schemas.xmlsoap.org/ws/2004/09/transfer";

			// Token: 0x040042E1 RID: 17121
			public const string GetAction = "http://schemas.xmlsoap.org/ws/2004/09/transfer/Get";

			// Token: 0x040042E2 RID: 17122
			public const string GetResponseAction = "http://schemas.xmlsoap.org/ws/2004/09/transfer/GetResponse";
		}

		// Token: 0x02000C06 RID: 3078
		public static class ServiceDescription
		{
			// Token: 0x040042E3 RID: 17123
			public const string Definitions = "definitions";

			// Token: 0x040042E4 RID: 17124
			public const string ArrayType = "arrayType";
		}

		// Token: 0x02000C07 RID: 3079
		public static class XmlSchema
		{
			// Token: 0x040042E5 RID: 17125
			public const string Schema = "schema";
		}

		// Token: 0x02000C08 RID: 3080
		public static class Xml
		{
			// Token: 0x040042E6 RID: 17126
			public const string Prefix = "xml";

			// Token: 0x040042E7 RID: 17127
			public const string NamespaceUri = "http://www.w3.org/XML/1998/namespace";

			// Token: 0x02000F2E RID: 3886
			public static class Attributes
			{
				// Token: 0x04004E18 RID: 19992
				public const string Id = "id";
			}
		}

		// Token: 0x02000C09 RID: 3081
		public static class Addressing200408
		{
			// Token: 0x040042E8 RID: 17128
			public const string Prefix = "wsa";

			// Token: 0x040042E9 RID: 17129
			public const string NamespaceUri = "http://schemas.xmlsoap.org/ws/2004/08/addressing";

			// Token: 0x02000F2F RID: 3887
			public static class Policy
			{
				// Token: 0x04004E19 RID: 19993
				public const string Prefix = "wsap";

				// Token: 0x04004E1A RID: 19994
				public const string NamespaceUri = "http://schemas.xmlsoap.org/ws/2004/08/addressing/policy";

				// Token: 0x04004E1B RID: 19995
				public const string UsingAddressing = "UsingAddressing";
			}
		}

		// Token: 0x02000C0A RID: 3082
		public static class Addressing10
		{
			// Token: 0x040042EA RID: 17130
			public const string Prefix = "wsa10";

			// Token: 0x040042EB RID: 17131
			public const string NamespaceUri = "http://www.w3.org/2005/08/addressing";

			// Token: 0x02000F30 RID: 3888
			public static class WsdlBindingPolicy
			{
				// Token: 0x04004E1C RID: 19996
				public const string Prefix = "wsaw";

				// Token: 0x04004E1D RID: 19997
				public const string NamespaceUri = "http://www.w3.org/2006/05/addressing/wsdl";

				// Token: 0x04004E1E RID: 19998
				public const string UsingAddressing = "UsingAddressing";
			}

			// Token: 0x02000F31 RID: 3889
			public static class MetadataPolicy
			{
				// Token: 0x04004E1F RID: 19999
				public const string Prefix = "wsam";

				// Token: 0x04004E20 RID: 20000
				public const string NamespaceUri = "http://www.w3.org/2007/05/addressing/metadata";

				// Token: 0x04004E21 RID: 20001
				public const string Addressing = "Addressing";

				// Token: 0x04004E22 RID: 20002
				public const string AnonymousResponses = "AnonymousResponses";

				// Token: 0x04004E23 RID: 20003
				public const string NonAnonymousResponses = "NonAnonymousResponses";
			}
		}

		// Token: 0x02000C0B RID: 3083
		public static class AddressingWsdl
		{
			// Token: 0x040042EC RID: 17132
			public const string Prefix = "wsaw";

			// Token: 0x040042ED RID: 17133
			public const string NamespaceUri = "http://www.w3.org/2006/05/addressing/wsdl";

			// Token: 0x040042EE RID: 17134
			public const string Action = "Action";
		}

		// Token: 0x02000C0C RID: 3084
		public static class AddressingMetadata
		{
			// Token: 0x040042EF RID: 17135
			public const string Prefix = "wsam";

			// Token: 0x040042F0 RID: 17136
			public const string NamespaceUri = "http://www.w3.org/2007/05/addressing/metadata";

			// Token: 0x040042F1 RID: 17137
			public const string Action = "Action";
		}

		// Token: 0x02000C0D RID: 3085
		public static class Wsu
		{
			// Token: 0x040042F2 RID: 17138
			public const string Prefix = "wsu";

			// Token: 0x040042F3 RID: 17139
			public const string NamespaceUri = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";

			// Token: 0x02000F32 RID: 3890
			public static class Attributes
			{
				// Token: 0x04004E24 RID: 20004
				public const string Id = "Id";
			}
		}

		// Token: 0x02000C0E RID: 3086
		public static class WSPolicy
		{
			// Token: 0x040042F4 RID: 17140
			public const string Prefix = "wsp";

			// Token: 0x040042F5 RID: 17141
			public const string NamespaceUri = "http://schemas.xmlsoap.org/ws/2004/09/policy";

			// Token: 0x040042F6 RID: 17142
			public const string NamespaceUri15 = "http://www.w3.org/ns/ws-policy";

			// Token: 0x02000F33 RID: 3891
			public static class Attributes
			{
				// Token: 0x04004E25 RID: 20005
				public const string Optional = "Optional";

				// Token: 0x04004E26 RID: 20006
				public const string PolicyURIs = "PolicyURIs";

				// Token: 0x04004E27 RID: 20007
				public const string URI = "URI";

				// Token: 0x04004E28 RID: 20008
				public const string TargetNamespace = "TargetNamespace";
			}

			// Token: 0x02000F34 RID: 3892
			public static class Elements
			{
				// Token: 0x04004E29 RID: 20009
				public const string PolicyReference = "PolicyReference";

				// Token: 0x04004E2A RID: 20010
				public const string All = "All";

				// Token: 0x04004E2B RID: 20011
				public const string ExactlyOne = "ExactlyOne";

				// Token: 0x04004E2C RID: 20012
				public const string Policy = "Policy";
			}
		}
	}
}
