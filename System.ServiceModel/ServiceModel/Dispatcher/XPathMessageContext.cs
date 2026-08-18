using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel.XamlIntegration;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000524 RID: 1316
	[TypeConverter(typeof(XPathMessageContextTypeConverter))]
	public class XPathMessageContext : XsltContext
	{
		// Token: 0x06003218 RID: 12824 RVA: 0x000C0C48 File Offset: 0x000BEE48
		static XPathMessageContext()
		{
			XPathMessageContext.defaultNamespaces = new Dictionary<string, string>
			{
				{
					"s11",
					"http://schemas.xmlsoap.org/soap/envelope/"
				},
				{
					"s12",
					"http://www.w3.org/2003/05/soap-envelope"
				},
				{
					"wsa10",
					"http://www.w3.org/2005/08/addressing"
				},
				{
					"wsaAugust2004",
					"http://schemas.xmlsoap.org/ws/2004/08/addressing"
				},
				{
					"tempuri",
					"http://tempuri.org/"
				},
				{
					"ser",
					"http://schemas.microsoft.com/2003/10/Serialization/"
				},
				{
					"sm",
					"http://schemas.microsoft.com/serviceModel/2004/05/xpathfunctions"
				}
			};
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x000C0FC2 File Offset: 0x000BF1C2
		public XPathMessageContext() : this(new NameTable())
		{
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x000C0FD0 File Offset: 0x000BF1D0
		public XPathMessageContext(NameTable table) : base(XPathMessageContext.ArgValidator(table))
		{
			foreach (KeyValuePair<string, string> keyValuePair in XPathMessageContext.defaultNamespaces)
			{
				this.AddNamespace(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x000C103C File Offset: 0x000BF23C
		private static NameTable ArgValidator(NameTable table)
		{
			if (table == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("table");
			}
			return table;
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x0600321C RID: 12828 RVA: 0x000C1052 File Offset: 0x000BF252
		public override bool Whitespace
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x000C1055 File Offset: 0x000BF255
		public override int CompareDocument(string baseUri, string nextBaseUri)
		{
			return 0;
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x000C1058 File Offset: 0x000BF258
		public override bool PreserveWhitespace(XPathNavigator node)
		{
			return false;
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x000C105C File Offset: 0x000BF25C
		public override IXsltContextFunction ResolveFunction(string prefix, string name, XPathResultType[] argTypes)
		{
			if (argTypes == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("argTypes");
			}
			string b = this.LookupNamespace(prefix);
			for (int i = 0; i < XPathMessageContext.functions.Length; i++)
			{
				if (XPathMessageContext.functions[i].name == name && XPathMessageContext.functions[i].ns == b)
				{
					IXsltContextFunction function = XPathMessageContext.functions[i].function;
					if (argTypes.Length <= function.Maxargs && argTypes.Length >= function.Minargs)
					{
						return function;
					}
				}
			}
			return null;
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x000C10F0 File Offset: 0x000BF2F0
		public override IXsltContextVariable ResolveVariable(string prefix, string name)
		{
			return null;
		}

		// Token: 0x040026DC RID: 9948
		internal const string S11NS = "http://schemas.xmlsoap.org/soap/envelope/";

		// Token: 0x040026DD RID: 9949
		internal const string S12NS = "http://www.w3.org/2003/05/soap-envelope";

		// Token: 0x040026DE RID: 9950
		internal const string Wsa200408NS = "http://schemas.xmlsoap.org/ws/2004/08/addressing";

		// Token: 0x040026DF RID: 9951
		internal const string Wsa10NS = "http://www.w3.org/2005/08/addressing";

		// Token: 0x040026E0 RID: 9952
		internal const string WsaNoneNS = "http://schemas.microsoft.com/ws/2005/05/addressing/none";

		// Token: 0x040026E1 RID: 9953
		internal const string TempUriNS = "http://tempuri.org/";

		// Token: 0x040026E2 RID: 9954
		internal const string SerializationNS = "http://schemas.microsoft.com/2003/10/Serialization/";

		// Token: 0x040026E3 RID: 9955
		internal const string IndigoNS = "http://schemas.microsoft.com/serviceModel/2004/05/xpathfunctions";

		// Token: 0x040026E4 RID: 9956
		internal const string S11P = "s11";

		// Token: 0x040026E5 RID: 9957
		internal const string S12P = "s12";

		// Token: 0x040026E6 RID: 9958
		internal const string Wsa200408P = "wsaAugust2004";

		// Token: 0x040026E7 RID: 9959
		internal const string Wsa10P = "wsa10";

		// Token: 0x040026E8 RID: 9960
		internal const string TempUriP = "tempuri";

		// Token: 0x040026E9 RID: 9961
		internal const string SerializationP = "ser";

		// Token: 0x040026EA RID: 9962
		internal const string IndigoP = "sm";

		// Token: 0x040026EB RID: 9963
		internal static Dictionary<string, string> defaultNamespaces;

		// Token: 0x040026EC RID: 9964
		internal const string EnvelopeE = "Envelope";

		// Token: 0x040026ED RID: 9965
		internal const string HeaderE = "Header";

		// Token: 0x040026EE RID: 9966
		internal const string BodyE = "Body";

		// Token: 0x040026EF RID: 9967
		internal const string ActionE = "Action";

		// Token: 0x040026F0 RID: 9968
		internal const string ToE = "To";

		// Token: 0x040026F1 RID: 9969
		internal const string MessageIDE = "MessageID";

		// Token: 0x040026F2 RID: 9970
		internal const string RelatesToE = "RelatesTo";

		// Token: 0x040026F3 RID: 9971
		internal const string ReplyToE = "ReplyTo";

		// Token: 0x040026F4 RID: 9972
		internal const string FromE = "From";

		// Token: 0x040026F5 RID: 9973
		internal const string FaultToE = "FaultTo";

		// Token: 0x040026F6 RID: 9974
		internal static string Actor11A = EnvelopeVersion.Soap11.Actor;

		// Token: 0x040026F7 RID: 9975
		internal static string Actor12A = EnvelopeVersion.Soap12.Actor;

		// Token: 0x040026F8 RID: 9976
		internal const string MandatoryA = "mustUnderstand";

		// Token: 0x040026F9 RID: 9977
		internal static readonly XPathMessageFunction HeaderFun = new XPathMessageFunctionHeader();

		// Token: 0x040026FA RID: 9978
		internal static readonly XPathMessageFunction BodyFun = new XPathMessageFunctionBody();

		// Token: 0x040026FB RID: 9979
		internal static readonly XPathMessageFunction SoapUriFun = new XPathMessageFunctionSoapUri();

		// Token: 0x040026FC RID: 9980
		internal static readonly XPathMessageFunction MessageIDFun = new XPathMessageFunctionMessageID();

		// Token: 0x040026FD RID: 9981
		internal static readonly XPathMessageFunction RelatesToFun = new XPathMessageFunctionRelatesTo();

		// Token: 0x040026FE RID: 9982
		internal static readonly XPathMessageFunction ReplyToFun = new XPathMessageFunctionReplyTo();

		// Token: 0x040026FF RID: 9983
		internal static readonly XPathMessageFunction FromFun = new XPathMessageFunctionFrom();

		// Token: 0x04002700 RID: 9984
		internal static readonly XPathMessageFunction FaultToFun = new XPathMessageFunctionFaultTo();

		// Token: 0x04002701 RID: 9985
		internal static readonly XPathMessageFunction ToFun = new XPathMessageFunctionTo();

		// Token: 0x04002702 RID: 9986
		internal static readonly XPathMessageFunction ActionFun = new XPathMessageFunctionAction();

		// Token: 0x04002703 RID: 9987
		internal static readonly XPathMessageFunction DateNowFun = new XPathMessageFunctionDateNow();

		// Token: 0x04002704 RID: 9988
		internal static readonly XPathMessageFunction HeadersWithActorFun = new XPathMessageFunctionHeadersWithActor();

		// Token: 0x04002705 RID: 9989
		internal static readonly XPathMessageFunction ActorFun = new XPathMessageFunctionActor();

		// Token: 0x04002706 RID: 9990
		internal static readonly XPathMessageFunction IsMandatoryFun = new XPathMessageFunctionIsMandatory();

		// Token: 0x04002707 RID: 9991
		internal static readonly XPathMessageFunction IsActorNextFun = new XPathMessageFunctionIsActorNext();

		// Token: 0x04002708 RID: 9992
		internal static readonly XPathMessageFunction IsActorUltRecFun = new XPathMessageFunctionIsActorUltimateReceiver();

		// Token: 0x04002709 RID: 9993
		internal static readonly XPathMessageFunction DateFun = new XPathMessageFunctionDateStr();

		// Token: 0x0400270A RID: 9994
		internal static readonly XPathMessageFunction SpanFun = new XPathMessageFunctionSpanStr();

		// Token: 0x0400270B RID: 9995
		internal static readonly XPathMessageFunction CorrelationDataFun = new XPathMessageFunctionCorrelationData();

		// Token: 0x0400270C RID: 9996
		private static XPathMessageContext.Function[] functions = new XPathMessageContext.Function[]
		{
			new XPathMessageContext.Function("http://schemas.microsoft.com/serviceModel/2004/05/xpathfunctions", "header", XPathMessageContext.HeaderFun),
			new XPathMessageContext.Function("http://schemas.microsoft.com/serviceModel/2004/05/xpathfunctions", "body", XPathMessageContext.BodyFun),
			new XPathMessageContext.Function("http://schemas.microsoft.com/serviceModel/2004/05/xpathfunctions", "soap-uri", XPathMessageContext.SoapUriFun),
			new XPathMessageContext.Function("http://schemas.microsoft.com/serviceModel/2004/05/xpathfunctions", "headers-with-actor", XPathMessageContext.HeadersWithActorFun),
			new XPathMessageContext.Function("http://schemas.microsoft.com/serviceModel/2004/05/xpathfunctions", "actor", XPathMessageContext.ActorFun),
			new XPathMessageContext.Function("http://schemas.microsoft.com/serviceModel/2004/05/xpathfunctions", "is-mandatory", XPathMessageContext.IsMandatoryFun),
			new XPathMessageContext.Function("http://schemas.microsoft.com/serviceModel/2004/05/xpathfunctions", "is-actor-next", XPathMessageContext.IsActorNextFun),
			new XPathMessageContext.Function("http://schemas.microsoft.com/serviceModel/2004/05/xpathfunctions", "is-actor-ultimate-receiver", XPathMessageContext.IsActorUltRecFun),
			new XPathMessageContext.Function("http://schemas.microsoft.com/serviceModel/2004/05/xpathfunctions", "messageId", XPathMessageContext.MessageIDFun),
			new XPathMessageContext.Function("http://schemas.microsoft.com/serviceModel/2004/05/xpathfunctions", "relatesTo", XPathMessageContext.RelatesToFun),
			new XPathMessageContext.Function("http://schemas.microsoft.com/serviceModel/2004/05/xpathfunctions", "replyTo", XPathMessageContext.ReplyToFun),
			new XPathMessageContext.Function("http://schemas.microsoft.com/serviceModel/2004/05/xpathfunctions", "from", XPathMessageContext.FromFun),
			new XPathMessageContext.Function("http://schemas.microsoft.com/serviceModel/2004/05/xpathfunctions", "faultTo", XPathMessageContext.FaultToFun),
			new XPathMessageContext.Function("http://schemas.microsoft.com/serviceModel/2004/05/xpathfunctions", "to", XPathMessageContext.ToFun),
			new XPathMessageContext.Function("http://schemas.microsoft.com/serviceModel/2004/05/xpathfunctions", "action", XPathMessageContext.ActionFun),
			new XPathMessageContext.Function("http://schemas.microsoft.com/serviceModel/2004/05/xpathfunctions", "date-time", XPathMessageContext.DateFun),
			new XPathMessageContext.Function("http://schemas.microsoft.com/serviceModel/2004/05/xpathfunctions", "duration", XPathMessageContext.SpanFun),
			new XPathMessageContext.Function("http://schemas.microsoft.com/serviceModel/2004/05/xpathfunctions", "utc-now", XPathMessageContext.DateNowFun),
			new XPathMessageContext.Function("http://schemas.microsoft.com/serviceModel/2004/05/xpathfunctions", "correlation-data", XPathMessageContext.CorrelationDataFun)
		};

		// Token: 0x02000C51 RID: 3153
		internal struct Function
		{
			// Token: 0x0600779F RID: 30623 RVA: 0x001BF1E3 File Offset: 0x001BD3E3
			internal Function(string ns, string name, IXsltContextFunction function)
			{
				this.ns = ns;
				this.name = name;
				this.function = function;
			}

			// Token: 0x0400446D RID: 17517
			internal string ns;

			// Token: 0x0400446E RID: 17518
			internal string name;

			// Token: 0x0400446F RID: 17519
			internal IXsltContextFunction function;
		}
	}
}
