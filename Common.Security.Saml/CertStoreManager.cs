using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x02000007 RID: 7
	public class CertStoreManager : ICertStoreManager
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002500 File Offset: 0x00000700
		public CertStoreManager(IList<X509Store> stores)
		{
			IList<X509Store> stores2;
			if (stores != null && stores.Count != 0)
			{
				stores2 = stores;
			}
			else
			{
				IList<X509Store> defaultStores = this.GetDefaultStores();
				stores2 = defaultStores;
			}
			this._stores = stores2;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002531 File Offset: 0x00000731
		public CertStoreManager()
		{
			this._stores = this.GetDefaultStores();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002548 File Offset: 0x00000748
		private List<X509Store> GetDefaultStores()
		{
			return new List<X509Store>
			{
				new X509Store(StoreName.My, StoreLocation.LocalMachine),
				new X509Store(StoreName.TrustedPeople, StoreLocation.LocalMachine),
				new X509Store(StoreName.AuthRoot, StoreLocation.LocalMachine),
				new X509Store(StoreName.My, StoreLocation.CurrentUser),
				new X509Store(StoreName.TrustedPeople, StoreLocation.CurrentUser),
				new X509Store(StoreName.AuthRoot, StoreLocation.CurrentUser)
			};
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025B8 File Offset: 0x000007B8
		private List<X509Store> ClonedSupportedList()
		{
			List<X509Store> list = new List<X509Store>();
			list.AddRange(from store in this._stores
			select new X509Store(store.Name, store.Location));
			return list;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002604 File Offset: 0x00000804
		public IList<X509Store> OpenSupportedStores()
		{
			return this.OpenSupportedStores(OpenFlags.ReadOnly);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002620 File Offset: 0x00000820
		public IList<X509Store> OpenSupportedStores(OpenFlags mode)
		{
			List<X509Store> list = this.ClonedSupportedList();
			foreach (X509Store x509Store in list)
			{
				x509Store.Open(mode);
			}
			return list;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002680 File Offset: 0x00000880
		public void CloseSupportedStores(IList<X509Store> certificateStores)
		{
			foreach (X509Store x509Store in certificateStores)
			{
				x509Store.Close();
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026CC File Offset: 0x000008CC
		public X509Certificate2 LookupCertFromSupportedStores(IList<X509Store> supportedStores, X509FindType findType, object findValue)
		{
			X509Certificate2 x509Certificate = null;
			IEnumerator<X509Store> enumerator = supportedStores.GetEnumerator();
			while (x509Certificate == null && enumerator.MoveNext())
			{
				X509Certificate2Collection x509Certificate2Collection = enumerator.Current.Certificates.Find(findType, findValue, false);
				bool flag = x509Certificate2Collection.Count > 0;
				if (flag)
				{
					x509Certificate = x509Certificate2Collection[0];
				}
			}
			return x509Certificate;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000272C File Offset: 0x0000092C
		public X509Certificate2Collection LookupCertsFromSupportedStores(IList<X509Store> supportedStores, X509FindType findType, object findValue)
		{
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			foreach (X509Store x509Store in supportedStores)
			{
				X509Certificate2Collection x509Certificate2Collection2 = x509Store.Certificates.Find(findType, findValue, false);
				bool flag = x509Certificate2Collection2.Count > 0;
				if (flag)
				{
					x509Certificate2Collection.AddRange(x509Certificate2Collection2);
				}
			}
			return x509Certificate2Collection;
		}

		// Token: 0x0400000D RID: 13
		private readonly IList<X509Store> _stores;
	}
}
