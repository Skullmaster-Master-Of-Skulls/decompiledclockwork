using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel.Description;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000217 RID: 535
	internal static class ComPlusTypeValidator
	{
		// Token: 0x0600104A RID: 4170 RVA: 0x0003B23C File Offset: 0x0003943C
		public static bool IsValidInterface(Guid iid)
		{
			return !(iid == ComPlusTypeValidator.IID_Object) && !(iid == ComPlusTypeValidator.IID_IDisposable) && !(iid == ComPlusTypeValidator.IID_IManagedObject) && !(iid == ComPlusTypeValidator.IID_IProcessInitializer) && !(iid == ComPlusTypeValidator.IID_IRemoteDispatch) && !(iid == ComPlusTypeValidator.IID_IServicedComponentInfo) && !iid.ToString("D").EndsWith("C000-000000000046", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0003B2B4 File Offset: 0x000394B4
		public static bool IsValidParameter(Type type, ICustomAttributeProvider attributeProvider, bool allowReferences)
		{
			object[] customAttributes = ServiceReflector.GetCustomAttributes(attributeProvider, typeof(MarshalAsAttribute), true);
			foreach (MarshalAsAttribute marshalAsAttribute in customAttributes)
			{
				UnmanagedType value = marshalAsAttribute.Value;
				if (value == UnmanagedType.IDispatch || value == UnmanagedType.Interface || value == UnmanagedType.IUnknown)
				{
					return allowReferences;
				}
			}
			XsdDataContractExporter xsdDataContractExporter = new XsdDataContractExporter();
			return xsdDataContractExporter.CanExport(type);
		}

		// Token: 0x0400186B RID: 6251
		private static Guid IID_Object = new Guid("{65074F7F-63C0-304E-AF0A-D51741CB4A8D}");

		// Token: 0x0400186C RID: 6252
		private static Guid IID_IDisposable = new Guid("{805D7A98-D4AF-3F0F-967F-E5CF45312D2C}");

		// Token: 0x0400186D RID: 6253
		private static Guid IID_IManagedObject = new Guid("{C3FCC19E-A970-11D2-8B5A-00A0C9B7C9C4}");

		// Token: 0x0400186E RID: 6254
		private static Guid IID_IProcessInitializer = new Guid("{1113F52D-DC7F-4943-AED6-88D04027E32A}");

		// Token: 0x0400186F RID: 6255
		private static Guid IID_IRemoteDispatch = new Guid("{6619A740-8154-43BE-A186-0319578E02DB}");

		// Token: 0x04001870 RID: 6256
		private static Guid IID_IServicedComponentInfo = new Guid("{8165B19E-8D3A-4D0B-80C8-97DE310DB583}");
	}
}
