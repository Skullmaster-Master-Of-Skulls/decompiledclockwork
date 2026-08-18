using System;
using System.Collections.ObjectModel;
using System.Xml;

namespace System.ServiceModel.Description
{
	// Token: 0x020003F6 RID: 1014
	public class ServiceEndpointCollection : Collection<ServiceEndpoint>
	{
		// Token: 0x0600263F RID: 9791 RVA: 0x00089D55 File Offset: 0x00087F55
		internal ServiceEndpointCollection()
		{
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x00089D60 File Offset: 0x00087F60
		public ServiceEndpoint Find(Type contractType)
		{
			if (contractType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contractType");
			}
			foreach (ServiceEndpoint serviceEndpoint in this)
			{
				if (serviceEndpoint != null && serviceEndpoint.Contract.ContractType == contractType)
				{
					return serviceEndpoint;
				}
			}
			return null;
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x00089DD8 File Offset: 0x00087FD8
		public ServiceEndpoint Find(XmlQualifiedName contractName)
		{
			if (contractName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contractName");
			}
			foreach (ServiceEndpoint serviceEndpoint in this)
			{
				if (serviceEndpoint != null && serviceEndpoint.Contract.Name == contractName.Name && serviceEndpoint.Contract.Namespace == contractName.Namespace)
				{
					return serviceEndpoint;
				}
			}
			return null;
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x00089E6C File Offset: 0x0008806C
		public ServiceEndpoint Find(Type contractType, XmlQualifiedName bindingName)
		{
			if (contractType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contractType");
			}
			if (bindingName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("bindingName");
			}
			foreach (ServiceEndpoint serviceEndpoint in this)
			{
				if (serviceEndpoint != null && serviceEndpoint.Contract.ContractType == contractType && serviceEndpoint.Binding.Name == bindingName.Name && serviceEndpoint.Binding.Namespace == bindingName.Namespace)
				{
					return serviceEndpoint;
				}
			}
			return null;
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x00089F2C File Offset: 0x0008812C
		public ServiceEndpoint Find(XmlQualifiedName contractName, XmlQualifiedName bindingName)
		{
			if (contractName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contractName");
			}
			if (bindingName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("bindingName");
			}
			foreach (ServiceEndpoint serviceEndpoint in this)
			{
				if (serviceEndpoint != null && serviceEndpoint.Contract.Name == contractName.Name && serviceEndpoint.Contract.Namespace == contractName.Namespace && serviceEndpoint.Binding.Name == bindingName.Name && serviceEndpoint.Binding.Namespace == bindingName.Namespace)
				{
					return serviceEndpoint;
				}
			}
			return null;
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x0008A00C File Offset: 0x0008820C
		public ServiceEndpoint Find(Uri address)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			foreach (ServiceEndpoint serviceEndpoint in this)
			{
				if (serviceEndpoint != null && serviceEndpoint.Address.Uri == address)
				{
					return serviceEndpoint;
				}
			}
			return null;
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x0008A084 File Offset: 0x00088284
		public Collection<ServiceEndpoint> FindAll(Type contractType)
		{
			if (contractType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contractType");
			}
			Collection<ServiceEndpoint> collection = new Collection<ServiceEndpoint>();
			foreach (ServiceEndpoint serviceEndpoint in this)
			{
				if (serviceEndpoint != null && serviceEndpoint.Contract.ContractType == contractType)
				{
					collection.Add(serviceEndpoint);
				}
			}
			return collection;
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x0008A104 File Offset: 0x00088304
		public Collection<ServiceEndpoint> FindAll(XmlQualifiedName contractName)
		{
			if (contractName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contractName");
			}
			Collection<ServiceEndpoint> collection = new Collection<ServiceEndpoint>();
			foreach (ServiceEndpoint serviceEndpoint in this)
			{
				if (serviceEndpoint != null && serviceEndpoint.Contract.Name == contractName.Name && serviceEndpoint.Contract.Namespace == contractName.Namespace)
				{
					collection.Add(serviceEndpoint);
				}
			}
			return collection;
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x0008A1A0 File Offset: 0x000883A0
		protected override void InsertItem(int index, ServiceEndpoint item)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x0008A1BD File Offset: 0x000883BD
		protected override void SetItem(int index, ServiceEndpoint item)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			base.SetItem(index, item);
		}
	}
}
