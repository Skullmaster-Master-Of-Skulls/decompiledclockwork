using System;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008CE RID: 2254
	public sealed class UseManagedPresentationBindingElement : BindingElement, IPolicyExportExtension
	{
		// Token: 0x060055FD RID: 22013 RVA: 0x0013AB67 File Offset: 0x00138D67
		public override BindingElement Clone()
		{
			return new UseManagedPresentationBindingElement();
		}

		// Token: 0x060055FE RID: 22014 RVA: 0x0013AB6E File Offset: 0x00138D6E
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			return context.GetInnerProperty<T>();
		}

		// Token: 0x060055FF RID: 22015 RVA: 0x0013AB8C File Offset: 0x00138D8C
		void IPolicyExportExtension.ExportPolicy(MetadataExporter exporter, PolicyConversionContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (context.BindingElements != null)
			{
				UseManagedPresentationBindingElement useManagedPresentationBindingElement = context.BindingElements.Find<UseManagedPresentationBindingElement>();
				if (useManagedPresentationBindingElement != null)
				{
					XmlDocument xmlDocument = new XmlDocument();
					XmlElement item = xmlDocument.CreateElement("ic", "RequireFederatedIdentityProvisioning", "http://schemas.xmlsoap.org/ws/2005/05/identity");
					context.GetBindingAssertions().Add(item);
				}
			}
		}
	}
}
