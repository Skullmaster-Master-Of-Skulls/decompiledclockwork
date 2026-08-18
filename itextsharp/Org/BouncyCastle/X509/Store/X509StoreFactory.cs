using System;
using System.Collections;
using System.Globalization;

namespace Org.BouncyCastle.X509.Store
{
	// Token: 0x02000005 RID: 5
	public sealed class X509StoreFactory
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000024E0 File Offset: 0x000014E0
		private X509StoreFactory()
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024E8 File Offset: 0x000014E8
		public static IX509Store Create(string type, IX509StoreParameters parameters)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			string[] array = type.ToUpper(CultureInfo.InvariantCulture).Split(new char[]
			{
				'/'
			});
			if (array.Length < 2)
			{
				throw new ArgumentException("type");
			}
			if (array[1] != "COLLECTION")
			{
				throw new NoSuchStoreException("X.509 store type '" + type + "' not available.");
			}
			X509CollectionStoreParameters x509CollectionStoreParameters = (X509CollectionStoreParameters)parameters;
			ICollection collection = x509CollectionStoreParameters.GetCollection();
			string a;
			if ((a = array[0]) != null)
			{
				if (!(a == "ATTRIBUTECERTIFICATE"))
				{
					if (!(a == "CERTIFICATE"))
					{
						if (!(a == "CERTIFICATEPAIR"))
						{
							if (!(a == "CRL"))
							{
								goto IL_FD;
							}
							X509StoreFactory.checkCorrectType(collection, typeof(X509Crl));
						}
						else
						{
							X509StoreFactory.checkCorrectType(collection, typeof(X509CertificatePair));
						}
					}
					else
					{
						X509StoreFactory.checkCorrectType(collection, typeof(X509Certificate));
					}
				}
				else
				{
					X509StoreFactory.checkCorrectType(collection, typeof(IX509AttributeCertificate));
				}
				return new X509CollectionStore(collection);
			}
			IL_FD:
			throw new NoSuchStoreException("X.509 store type '" + type + "' not available.");
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002610 File Offset: 0x00001610
		private static void checkCorrectType(ICollection coll, Type t)
		{
			foreach (object o in coll)
			{
				if (!t.IsInstanceOfType(o))
				{
					throw new InvalidCastException("Can't cast object to type: " + t.FullName);
				}
			}
		}
	}
}
