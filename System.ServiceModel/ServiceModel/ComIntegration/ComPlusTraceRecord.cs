using System;
using System.Runtime.Serialization;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000215 RID: 533
	internal static class ComPlusTraceRecord
	{
		// Token: 0x0600103D RID: 4157 RVA: 0x0003A878 File Offset: 0x00038A78
		public static void SerializeRecord(XmlWriter xmlWriter, object o)
		{
			DataContractSerializer dataContractSerializer = DataContractSerializerDefaults.CreateSerializer((o == null) ? typeof(object) : o.GetType(), int.MaxValue);
			dataContractSerializer.WriteObject(xmlWriter, o);
		}
	}
}
