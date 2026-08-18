using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel.Description;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x0200000C RID: 12
	[SecurityCritical]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	internal class AsmxEndpointPickerExtension : IWsdlImportExtension
	{
		// Token: 0x0600006E RID: 110 RVA: 0x000032F4 File Offset: 0x000014F4
		[SecuritySafeCritical]
		void IWsdlImportExtension.ImportContract(WsdlImporter importer, WsdlContractConversionContext context)
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000032F4 File Offset: 0x000014F4
		[SecuritySafeCritical]
		void IWsdlImportExtension.ImportEndpoint(WsdlImporter importer, WsdlEndpointConversionContext context)
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000032F8 File Offset: 0x000014F8
		[SecuritySafeCritical]
		void IWsdlImportExtension.BeforeImport(ServiceDescriptionCollection wsdlDocuments, XmlSchemaSet xmlSchemas, ICollection<XmlElement> policy)
		{
			if (wsdlDocuments == null)
			{
				throw new ArgumentNullException("wsdlDocuments");
			}
			foreach (object obj in wsdlDocuments)
			{
				System.Web.Services.Description.ServiceDescription serviceDescription = (System.Web.Services.Description.ServiceDescription)obj;
				foreach (object obj2 in serviceDescription.Services)
				{
					Service service = (Service)obj2;
					if (service.Ports.Count == 2)
					{
						Port port = null;
						if (this.IsSoapAsmxPort(typeof(SoapAddressBinding), service.Ports[0]) && this.IsSoapAsmxPort(typeof(Soap12AddressBinding), service.Ports[1]))
						{
							port = service.Ports[1];
						}
						else if (this.IsSoapAsmxPort(typeof(SoapAddressBinding), service.Ports[1]) && this.IsSoapAsmxPort(typeof(Soap12AddressBinding), service.Ports[0]))
						{
							port = service.Ports[0];
						}
						if (port != null)
						{
							service.Ports.Remove(port);
							if (port.Binding != null)
							{
								List<Binding> list = new List<Binding>();
								foreach (object obj3 in serviceDescription.Bindings)
								{
									Binding binding = (Binding)obj3;
									if (string.Equals(binding.Name, port.Binding.Name, StringComparison.Ordinal))
									{
										list.Add(binding);
									}
								}
								foreach (Binding binding2 in list)
								{
									serviceDescription.Bindings.Remove(binding2);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003560 File Offset: 0x00001760
		private bool IsSoapAsmxPort(Type addressType, Port port)
		{
			SoapAddressBinding soapAddressBinding = port.Extensions.Find(addressType) as SoapAddressBinding;
			return soapAddressBinding != null && soapAddressBinding.GetType() == addressType && this.IsAsmxUri(soapAddressBinding.Location);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000035A4 File Offset: 0x000017A4
		private bool IsAsmxUri(string location)
		{
			Uri uri = null;
			if (!Uri.TryCreate(location, UriKind.Absolute, out uri))
			{
				return false;
			}
			string[] segments = uri.Segments;
			if (segments.Length != 0)
			{
				try
				{
					string path = segments[segments.Length - 1];
					if (string.Equals(Path.GetExtension(path), ".asmx", StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
				catch (ArgumentException)
				{
				}
				return false;
			}
			return false;
		}
	}
}
