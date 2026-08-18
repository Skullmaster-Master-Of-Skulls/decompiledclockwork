using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace System.ServiceModel.Description
{
	// Token: 0x0200042D RID: 1069
	internal static class WsdlHelper
	{
		// Token: 0x060029A7 RID: 10663 RVA: 0x000A0264 File Offset: 0x0009E464
		public static ServiceDescription GetSingleWsdl(MetadataSet metadataSet)
		{
			if (metadataSet.MetadataSections.Count < 1)
			{
				return null;
			}
			List<ServiceDescription> list = new List<ServiceDescription>();
			List<XmlSchema> list2 = new List<XmlSchema>();
			foreach (MetadataSection metadataSection in metadataSet.MetadataSections)
			{
				if (metadataSection.Metadata is ServiceDescription)
				{
					list.Add((ServiceDescription)metadataSection.Metadata);
				}
				if (metadataSection.Metadata is XmlSchema)
				{
					list2.Add((XmlSchema)metadataSection.Metadata);
				}
			}
			WsdlHelper.VerifyContractNamespace(list);
			ServiceDescription singleWsdl = WsdlHelper.GetSingleWsdl(WsdlHelper.CopyServiceDescriptionCollection(list));
			foreach (XmlSchema originalXsd in list2)
			{
				XmlSchema schema = WsdlHelper.CloneXsd(originalXsd);
				WsdlHelper.RemoveSchemaLocations(schema);
				singleWsdl.Types.Schemas.Add(schema);
			}
			return singleWsdl;
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x000A0378 File Offset: 0x0009E578
		private static void RemoveSchemaLocations(XmlSchema schema)
		{
			foreach (XmlSchemaObject xmlSchemaObject in schema.Includes)
			{
				XmlSchemaExternal xmlSchemaExternal = xmlSchemaObject as XmlSchemaExternal;
				if (xmlSchemaExternal != null)
				{
					xmlSchemaExternal.SchemaLocation = null;
				}
			}
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x000A03D8 File Offset: 0x0009E5D8
		private static ServiceDescription GetSingleWsdl(List<ServiceDescription> wsdls)
		{
			ServiceDescription serviceDescription = wsdls.First((ServiceDescription wsdl) => wsdl.PortTypes.Count > 0);
			if (serviceDescription == null)
			{
				serviceDescription = new ServiceDescription();
			}
			else
			{
				serviceDescription.Types.Schemas.Clear();
				serviceDescription.Imports.Clear();
			}
			Dictionary<XmlQualifiedName, XmlQualifiedName> bindingReferenceChanges = new Dictionary<XmlQualifiedName, XmlQualifiedName>();
			foreach (ServiceDescription serviceDescription2 in wsdls)
			{
				if (serviceDescription2 != serviceDescription)
				{
					WsdlHelper.MergeWsdl(serviceDescription, serviceDescription2, bindingReferenceChanges);
				}
			}
			WsdlHelper.EnsureSingleNamespace(serviceDescription, bindingReferenceChanges);
			return serviceDescription;
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x000A0488 File Offset: 0x0009E688
		private static List<ServiceDescription> CopyServiceDescriptionCollection(List<ServiceDescription> wsdls)
		{
			List<ServiceDescription> list = new List<ServiceDescription>();
			foreach (ServiceDescription originalWsdl in wsdls)
			{
				list.Add(WsdlHelper.CloneWsdl(originalWsdl));
			}
			return list;
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x000A04E4 File Offset: 0x0009E6E4
		private static void MergeWsdl(ServiceDescription singleWsdl, ServiceDescription wsdl, Dictionary<XmlQualifiedName, XmlQualifiedName> bindingReferenceChanges)
		{
			if (wsdl.Services.Count > 0)
			{
				singleWsdl.Name = wsdl.Name;
			}
			foreach (object obj in wsdl.Bindings)
			{
				Binding binding = (Binding)obj;
				string uniqueName = NamingHelper.GetUniqueName(binding.Name, new NamingHelper.DoesNameExist(WsdlHelper.IsBindingNameUsed), singleWsdl.Bindings);
				if (binding.Name != uniqueName)
				{
					bindingReferenceChanges.Add(new XmlQualifiedName(binding.Name, binding.ServiceDescription.TargetNamespace), new XmlQualifiedName(uniqueName, singleWsdl.TargetNamespace));
					WsdlHelper.UpdatePolicyKeys(binding, uniqueName, wsdl);
					binding.Name = uniqueName;
				}
				singleWsdl.Bindings.Add(binding);
			}
			foreach (object extension in wsdl.Extensions)
			{
				singleWsdl.Extensions.Add(extension);
			}
			foreach (object obj2 in wsdl.Messages)
			{
				Message message = (Message)obj2;
				singleWsdl.Messages.Add(message);
			}
			foreach (object obj3 in wsdl.Services)
			{
				Service service = (Service)obj3;
				singleWsdl.Services.Add(service);
			}
			foreach (string value in wsdl.ValidationWarnings)
			{
				singleWsdl.ValidationWarnings.Add(value);
			}
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x000A070C File Offset: 0x0009E90C
		private static void UpdatePolicyKeys(Binding binding, string newBindingName, ServiceDescription wsdl)
		{
			string name = binding.Name;
			IEnumerable<XmlElement> enumerable = WsdlHelper.FindAllElements(wsdl.Extensions, "Policy");
			string format = "{0}_";
			foreach (XmlElement xmlElement in enumerable)
			{
				XmlNode namedItem = xmlElement.Attributes.GetNamedItem("Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
				string value = namedItem.Value;
				string text = string.Format(CultureInfo.InvariantCulture, format, new object[]
				{
					name
				});
				string str = string.Format(CultureInfo.InvariantCulture, format, new object[]
				{
					newBindingName
				});
				if (namedItem != null && value != null && value.StartsWith(text, StringComparison.Ordinal))
				{
					namedItem.Value = str + value.Substring(text.Length);
				}
			}
			WsdlHelper.UpdatePolicyReference(binding.Extensions, name, newBindingName);
			foreach (object obj in binding.Operations)
			{
				OperationBinding operationBinding = (OperationBinding)obj;
				WsdlHelper.UpdatePolicyReference(operationBinding.Extensions, name, newBindingName);
				if (operationBinding.Input != null)
				{
					WsdlHelper.UpdatePolicyReference(operationBinding.Input.Extensions, name, newBindingName);
				}
				if (operationBinding.Output != null)
				{
					WsdlHelper.UpdatePolicyReference(operationBinding.Output.Extensions, name, newBindingName);
				}
				foreach (object obj2 in operationBinding.Faults)
				{
					FaultBinding faultBinding = (FaultBinding)obj2;
					WsdlHelper.UpdatePolicyReference(faultBinding.Extensions, name, newBindingName);
				}
			}
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x000A08EC File Offset: 0x0009EAEC
		private static void UpdatePolicyReference(ServiceDescriptionFormatExtensionCollection extensions, string oldBindingName, string newBindingName)
		{
			IEnumerable<XmlElement> enumerable = WsdlHelper.FindAllElements(extensions, "PolicyReference");
			string format = "#{0}_";
			foreach (XmlElement xmlElement in enumerable)
			{
				XmlNode namedItem = xmlElement.Attributes.GetNamedItem("URI");
				string value = namedItem.Value;
				string text = string.Format(CultureInfo.InvariantCulture, format, new object[]
				{
					oldBindingName
				});
				string str = string.Format(CultureInfo.InvariantCulture, format, new object[]
				{
					newBindingName
				});
				if (namedItem != null && value != null && value.StartsWith(text, StringComparison.Ordinal))
				{
					namedItem.Value = str + namedItem.Value.Substring(text.Length);
				}
			}
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x000A09C4 File Offset: 0x0009EBC4
		private static IEnumerable<XmlElement> FindAllElements(ServiceDescriptionFormatExtensionCollection extensions, string elementName)
		{
			List<XmlElement> list = new List<XmlElement>();
			for (int i = 0; i < extensions.Count; i++)
			{
				XmlElement xmlElement = extensions[i] as XmlElement;
				if (xmlElement != null && xmlElement.LocalName == elementName)
				{
					list.Add(xmlElement);
				}
			}
			return list;
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x000A0A10 File Offset: 0x0009EC10
		private static void VerifyContractNamespace(List<ServiceDescription> wsdls)
		{
			IEnumerable<ServiceDescription> source = from serviceDescription in wsdls
			where serviceDescription.PortTypes.Count > 0
			select serviceDescription;
			if (source.Count<ServiceDescription>() > 1)
			{
				IEnumerable<string> values = from wsdl in source
				select wsdl.TargetNamespace;
				string text = string.Join(", ", values);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SingleWsdlNotGenerated", new object[]
				{
					text
				})));
			}
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x000A0AA4 File Offset: 0x0009ECA4
		private static void EnsureSingleNamespace(ServiceDescription wsdl, Dictionary<XmlQualifiedName, XmlQualifiedName> bindingReferenceChanges)
		{
			string targetNamespace = wsdl.TargetNamespace;
			foreach (object obj in wsdl.Bindings)
			{
				Binding binding = (Binding)obj;
				if (binding.Type.Namespace != targetNamespace)
				{
					binding.Type = new XmlQualifiedName(binding.Type.Name, targetNamespace);
				}
			}
			foreach (object obj2 in wsdl.PortTypes)
			{
				PortType portType = (PortType)obj2;
				foreach (object obj3 in portType.Operations)
				{
					Operation operation = (Operation)obj3;
					OperationInput input = operation.Messages.Input;
					if (input != null && input.Message.Namespace != targetNamespace)
					{
						input.Message = new XmlQualifiedName(input.Message.Name, targetNamespace);
					}
					OperationOutput output = operation.Messages.Output;
					if (output != null && output.Message.Namespace != targetNamespace)
					{
						output.Message = new XmlQualifiedName(output.Message.Name, targetNamespace);
					}
					foreach (object obj4 in operation.Faults)
					{
						OperationFault operationFault = (OperationFault)obj4;
						if (operationFault.Message.Namespace != targetNamespace)
						{
							operationFault.Message = new XmlQualifiedName(operationFault.Message.Name, targetNamespace);
						}
					}
				}
			}
			foreach (object obj5 in wsdl.Services)
			{
				Service service = (Service)obj5;
				foreach (object obj6 in service.Ports)
				{
					Port port = (Port)obj6;
					XmlQualifiedName binding2;
					if (bindingReferenceChanges.TryGetValue(port.Binding, out binding2))
					{
						port.Binding = binding2;
					}
					else if (port.Binding.Namespace != targetNamespace)
					{
						port.Binding = new XmlQualifiedName(port.Binding.Name, targetNamespace);
					}
				}
			}
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x000A0DD8 File Offset: 0x0009EFD8
		private static bool IsBindingNameUsed(string name, object collection)
		{
			BindingCollection bindingCollection = (BindingCollection)collection;
			foreach (object obj in bindingCollection)
			{
				Binding binding = (Binding)obj;
				if (binding.Name == name)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x000A0E44 File Offset: 0x0009F044
		private static ServiceDescription CloneWsdl(ServiceDescription originalWsdl)
		{
			ServiceDescription result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				originalWsdl.Write(memoryStream);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				result = ServiceDescription.Read(memoryStream);
			}
			return result;
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x000A0E8C File Offset: 0x0009F08C
		private static XmlSchema CloneXsd(XmlSchema originalXsd)
		{
			XmlSchema result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				originalXsd.Write(memoryStream);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				result = XmlSchema.Read(new XmlTextReader(memoryStream)
				{
					DtdProcessing = DtdProcessing.Parse
				}, null);
			}
			return result;
		}
	}
}
