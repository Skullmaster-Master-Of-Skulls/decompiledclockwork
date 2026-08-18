using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel.Description;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x02000015 RID: 21
	[SecurityCritical]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	internal class HttpBindingExtension : IWsdlImportExtension
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00003BF0 File Offset: 0x00001DF0
		private static bool ContainsHttpBindingExtension(Binding wsdlBinding)
		{
			foreach (object obj in wsdlBinding.Extensions)
			{
				if (obj is HttpBinding)
				{
					string verb = ((HttpBinding)obj).Verb;
					if (verb.Equals("GET", StringComparison.OrdinalIgnoreCase) || verb.Equals("POST", StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003C78 File Offset: 0x00001E78
		public bool IsHttpBindingContract(ContractDescription contract)
		{
			return contract != null && this.httpBindingContracts.Contains(contract);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000032F4 File Offset: 0x000014F4
		[SecuritySafeCritical]
		void IWsdlImportExtension.BeforeImport(ServiceDescriptionCollection wsdlDocuments, XmlSchemaSet xmlSchemas, ICollection<XmlElement> policy)
		{
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000032F4 File Offset: 0x000014F4
		[SecuritySafeCritical]
		void IWsdlImportExtension.ImportContract(WsdlImporter importer, WsdlContractConversionContext context)
		{
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003C8B File Offset: 0x00001E8B
		[SecuritySafeCritical]
		void IWsdlImportExtension.ImportEndpoint(WsdlImporter importer, WsdlEndpointConversionContext context)
		{
			if (context != null && context.WsdlBinding != null && HttpBindingExtension.ContainsHttpBindingExtension(context.WsdlBinding))
			{
				this.httpBindingContracts.Add(context.ContractConversionContext.Contract);
			}
		}

		// Token: 0x04000049 RID: 73
		private readonly HashSet<ContractDescription> httpBindingContracts = new HashSet<ContractDescription>();
	}
}
