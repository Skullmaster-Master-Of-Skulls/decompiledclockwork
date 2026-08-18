using System;
using System.Net;
using Microsoft.Web.Management.Utility;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000014 RID: 20
	public sealed class BindingCollection : ConfigurationElementCollectionBase<Binding>
	{
		// Token: 0x06000109 RID: 265 RVA: 0x00004B01 File Offset: 0x00003B01
		internal BindingCollection(ServerManager serverManager)
		{
			this._serverManager = serverManager;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004B10 File Offset: 0x00003B10
		public new Binding Add(Binding binding)
		{
			if (binding == null)
			{
				throw new ArgumentNullException("binding");
			}
			if (string.Equals(binding.Protocol, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase) || string.Equals(binding.Protocol, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase))
			{
				if (BindingUtility.EndPointFromBindingInformation(binding.BindingInformation) == null)
				{
					throw new ArgumentException(Resources.BindingInvalidHttpsBinding);
				}
				if (binding.CertificateHash != null)
				{
					this._serverManager.BindingManager.RemoveTransactionsFor(binding);
					this._serverManager.BindingManager.AddNewBindingTransaction(binding, binding.CertificateHash, binding.CertificateStoreName);
					if (binding.UseDsMapperInternalSet)
					{
						this._serverManager.BindingManager.AddModifyDSMapperPropertyTransaction(binding, binding.UseDsMapperInternal);
					}
				}
			}
			return base.Add(binding);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004BC8 File Offset: 0x00003BC8
		public Binding Add(string bindingInformation, string bindingProtocol)
		{
			Binding binding = base.CreateElement();
			binding.Protocol = bindingProtocol;
			binding.BindingInformation = bindingInformation;
			return this.Add(binding);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004BF4 File Offset: 0x00003BF4
		public Binding Add(string bindingInformation, byte[] certificateHash, string certificateStoreName)
		{
			Binding binding = base.CreateElement();
			binding.BindingInformation = bindingInformation;
			binding.CertificateHash = certificateHash;
			binding.CertificateStoreName = certificateStoreName;
			binding.Protocol = "https";
			return this.Add(binding);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004C2F File Offset: 0x00003C2F
		protected override Binding CreateNewElement(string elementTagName)
		{
			return new Binding(this._serverManager);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004C3C File Offset: 0x00003C3C
		public new void Remove(Binding element)
		{
			base.Remove(element);
			this.RemoveIPPortBinding(element);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004C4C File Offset: 0x00003C4C
		public new void RemoveAt(int index)
		{
			Binding element = base[index];
			base.RemoveAt(index);
			this.RemoveIPPortBinding(element);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004C70 File Offset: 0x00003C70
		private void RemoveIPPortBinding(Binding element)
		{
			if (element != null && string.Equals(element.Protocol, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
			{
				IPEndPoint endPoint = element.EndPoint;
				if (endPoint != null)
				{
					this._serverManager.BindingManager.AddRemoveBindingTransaction(element);
				}
			}
		}

		// Token: 0x04000037 RID: 55
		private ServerManager _serverManager;
	}
}
