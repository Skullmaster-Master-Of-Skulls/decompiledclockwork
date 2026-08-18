using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace TechnoPro.Common.Pdf
{
	// Token: 0x02000002 RID: 2
	public static class PdfFormUtility
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static void RegisterAsposeLicense()
		{
			if (PdfFormUtility._registeredAsposeLicense)
			{
				return;
			}
			License license = new License();
			using (MemoryStream memoryStream = new MemoryStream(Encoding.ASCII.GetBytes("<License>\r\n  <Data>\r\n    <LicensedTo>TechnoPro Computer Solutions</LicensedTo>\r\n    <EmailTo>mike@clockworks.ca</EmailTo>\r\n    <LicenseType>Developer OEM</LicenseType>\r\n    <LicenseNote>Limited to 1 developer, unlimited physical locations</LicenseNote>\r\n    <OrderID>190530114858</OrderID>\r\n    <UserID>310030</UserID>\r\n    <OEM>This is a redistributable license</OEM>\r\n    <Products>\r\n      <Product>Aspose.Pdf for .NET</Product>\r\n    </Products>\r\n    <EditionType>Enterprise</EditionType>\r\n    <SerialNumber>b5c58e7d-1c68-4812-b19a-73dbc0c0a028</SerialNumber>\r\n    <SubscriptionExpiry>20200530</SubscriptionExpiry>\r\n    <LicenseVersion>3.0</LicenseVersion>\r\n    <LicenseInstructions>https://purchase.aspose.com/policies/use-license</LicenseInstructions>\r\n  </Data>\r\n  <Signature>uS9s7V9RScPieY/E31ycX3cnbEDN6F7fubbP1Z3a5sOYGG+qFr7Qk6FJl74KSb45yppNs9hpih2hGdLwRorxfKIpgIxaFxXfgvUv7ZvJX/FzZC+SLR5qRDVXaA/BNN+5FUWK/o7BXnSzs/A992GlvnURRkiGjWeDp2vjNAbA8pg=</Signature>\r\n</License>")))
			{
				license.SetLicense(memoryStream);
			}
			PdfFormUtility._registeredAsposeLicense = true;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020AC File Offset: 0x000002AC
		public static Form LoadDocument(byte[] bytes)
		{
			PdfFormUtility.RegisterAsposeLicense();
			if (bytes == null)
			{
				return null;
			}
			MemoryStream srcStream = new MemoryStream(bytes);
			Form form = new Form();
			form.BindPdf(srcStream);
			return form;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020D6 File Offset: 0x000002D6
		public static IList<string> ExtractUniqueCodes(byte[] fileBytes)
		{
			Form form = PdfFormUtility.LoadDocument(fileBytes);
			if (form == null)
			{
				return null;
			}
			return form.FieldNames;
		}

		// Token: 0x04000001 RID: 1
		private static bool _registeredAsposeLicense;
	}
}
