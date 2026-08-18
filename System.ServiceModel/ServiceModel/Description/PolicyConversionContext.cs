using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Description
{
	// Token: 0x02000419 RID: 1049
	public abstract class PolicyConversionContext
	{
		// Token: 0x06002822 RID: 10274 RVA: 0x000970E6 File Offset: 0x000952E6
		protected PolicyConversionContext(ServiceEndpoint endpoint)
		{
			if (endpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpoint");
			}
			this.contract = endpoint.Contract;
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06002823 RID: 10275
		public abstract BindingElementCollection BindingElements { get; }

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06002824 RID: 10276 RVA: 0x0009710D File Offset: 0x0009530D
		internal virtual BindingParameterCollection BindingParameters
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06002825 RID: 10277 RVA: 0x00097110 File Offset: 0x00095310
		public ContractDescription Contract
		{
			get
			{
				return this.contract;
			}
		}

		// Token: 0x06002826 RID: 10278
		public abstract PolicyAssertionCollection GetBindingAssertions();

		// Token: 0x06002827 RID: 10279
		public abstract PolicyAssertionCollection GetOperationBindingAssertions(OperationDescription operation);

		// Token: 0x06002828 RID: 10280
		public abstract PolicyAssertionCollection GetMessageBindingAssertions(MessageDescription message);

		// Token: 0x06002829 RID: 10281
		public abstract PolicyAssertionCollection GetFaultBindingAssertions(FaultDescription fault);

		// Token: 0x0600282A RID: 10282 RVA: 0x00097118 File Offset: 0x00095318
		internal static XmlElement FindAssertion(ICollection<XmlElement> assertions, string localName, string namespaceUri, bool remove)
		{
			XmlElement xmlElement = null;
			foreach (XmlElement xmlElement2 in assertions)
			{
				if (xmlElement2.LocalName == localName && (namespaceUri == null || xmlElement2.NamespaceURI == namespaceUri))
				{
					xmlElement = xmlElement2;
					if (remove)
					{
						assertions.Remove(xmlElement);
						break;
					}
					break;
				}
			}
			return xmlElement;
		}

		// Token: 0x04002215 RID: 8725
		private readonly ContractDescription contract;
	}
}
