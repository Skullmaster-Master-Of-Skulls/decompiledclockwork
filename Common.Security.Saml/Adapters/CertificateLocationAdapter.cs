using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using ClockWorkLogger;

namespace TechnoPro.Common.Security.Saml.Adapters
{
	// Token: 0x02000021 RID: 33
	public static class CertificateLocationAdapter
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00005A4C File Offset: 0x00003C4C
		public static string CertificateLocationToXml(this CertificateLocation certificateLocation)
		{
			bool flag = certificateLocation == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = new XDocument(new object[]
				{
					new XElement("certificatelocation", new object[]
					{
						new XAttribute("storelocation", certificateLocation.StoreLocation.ToString()),
						new XAttribute("storename", certificateLocation.StoreName.ToString()),
						new XAttribute("findtype", certificateLocation.FindType.ToString()),
						new XAttribute("findvalue", certificateLocation.FindValue ?? "")
					})
				}).ToString();
			}
			return result;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005B30 File Offset: 0x00003D30
		public static CertificateLocation CertificateLocationFromXml(this string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			CertificateLocation result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					XDocument xdocument = XDocument.Parse(xml);
					return (from cl in xdocument.Descendants("certificatelocation")
					let storeNameAttr = cl.Attribute("storename")
					let storeLocationAttr = cl.Attribute("storelocation")
					let findTypeAttr = cl.Attribute("findtype")
					let findValueAttr = cl.Attribute("findvalue")
					select new CertificateLocation
					{
						StoreLocation = CertificateLocationAdapter.ParseEnum<StoreLocation>(storeLocationAttr, StoreLocation.LocalMachine),
						StoreName = CertificateLocationAdapter.ParseEnum<StoreName>(storeNameAttr, StoreName.My),
						FindType = CertificateLocationAdapter.ParseEnum<X509FindType>(findTypeAttr, X509FindType.FindByThumbprint),
						FindValue = findValueAttr.Value
					}).FirstOrDefault<CertificateLocation>();
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("Common.Security.Saml.CertificateLocationAdapter.CertificateLocationFromXml:xml={0}:err={1}", xml ?? "NULL", ex.ToString());
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005C60 File Offset: 0x00003E60
		private static T ParseEnum<T>(XAttribute attr, T defaultValue) where T : struct
		{
			bool flag = attr == null;
			T result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				result = CertificateLocationAdapter.ParseEnum<T>(attr.Value, defaultValue);
			}
			return result;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005C8C File Offset: 0x00003E8C
		private static T ParseEnum<T>(string enumName, T defaultValue) where T : struct
		{
			bool flag = string.IsNullOrEmpty(enumName);
			T result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				T t;
				bool flag2 = !Enum.TryParse<T>(enumName, out t);
				if (flag2)
				{
					result = defaultValue;
				}
				else
				{
					result = t;
				}
			}
			return result;
		}
	}
}
