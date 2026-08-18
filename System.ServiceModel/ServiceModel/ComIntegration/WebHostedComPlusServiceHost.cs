using System;
using System.ServiceModel.Configuration;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000277 RID: 631
	internal class WebHostedComPlusServiceHost : ComPlusServiceHost
	{
		// Token: 0x060011FF RID: 4607 RVA: 0x0004201C File Offset: 0x0004021C
		public WebHostedComPlusServiceHost(string webhostParams, Uri[] baseAddresses)
		{
			foreach (Uri item in baseAddresses)
			{
				base.InternalBaseAddresses.Add(item);
			}
			string[] array = webhostParams.Split(new char[]
			{
				','
			});
			if (array.Length != 2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(SR.GetString("ServiceStringFormatError", new object[]
				{
					webhostParams
				})));
			}
			Guid guid;
			if (!DiagnosticUtility.Utility.TryCreateGuid(array[0], out guid))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(SR.GetString("ServiceStringFormatError", new object[]
				{
					webhostParams
				})));
			}
			Guid guid2;
			if (!DiagnosticUtility.Utility.TryCreateGuid(array[1], out guid2))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(SR.GetString("ServiceStringFormatError", new object[]
				{
					webhostParams
				})));
			}
			string text = guid.ToString("B").ToUpperInvariant();
			ComCatalogObject comCatalogObject = CatalogUtil.FindApplication(guid2);
			if (comCatalogObject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(SR.GetString("ApplicationNotFound", new object[]
				{
					guid2.ToString("B").ToUpperInvariant()
				})));
			}
			ComCatalogCollection collection = comCatalogObject.GetCollection("Components");
			ComCatalogObject comCatalogObject2 = null;
			foreach (ComCatalogObject comCatalogObject3 in collection)
			{
				string value = (string)comCatalogObject3.GetValue("CLSID");
				if (text.Equals(value, StringComparison.OrdinalIgnoreCase))
				{
					comCatalogObject2 = comCatalogObject3;
					break;
				}
			}
			if (comCatalogObject2 == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(SR.GetString("ClsidNotInApplication", new object[]
				{
					text,
					guid2.ToString("B").ToUpperInvariant()
				})));
			}
			ServicesSection section = ServicesSection.GetSection();
			ServiceElement serviceElement = null;
			foreach (object obj in section.Services)
			{
				ServiceElement serviceElement2 = (ServiceElement)obj;
				Guid empty = Guid.Empty;
				Guid empty2 = Guid.Empty;
				string[] array2 = serviceElement2.Name.Split(new char[]
				{
					','
				});
				if (array2.Length == 2 && DiagnosticUtility.Utility.TryCreateGuid(array2[0], out empty2) && DiagnosticUtility.Utility.TryCreateGuid(array2[1], out empty) && empty == guid && empty2 == guid2)
				{
					serviceElement = serviceElement2;
					break;
				}
			}
			if (serviceElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(SR.GetString("ClsidNotInConfiguration", new object[]
				{
					text
				})));
			}
			HostingMode hostingMode;
			if ((int)comCatalogObject.GetValue("Activation") == 0)
			{
				hostingMode = HostingMode.WebHostInProcess;
			}
			else
			{
				hostingMode = HostingMode.WebHostOutOfProcess;
			}
			base.Initialize(guid, serviceElement, comCatalogObject, comCatalogObject2, hostingMode);
		}
	}
}
