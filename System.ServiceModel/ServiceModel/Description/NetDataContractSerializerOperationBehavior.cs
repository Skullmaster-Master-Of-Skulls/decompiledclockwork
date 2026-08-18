using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.Description
{
	// Token: 0x020003DE RID: 990
	internal class NetDataContractSerializerOperationBehavior : DataContractSerializerOperationBehavior
	{
		// Token: 0x0600254D RID: 9549 RVA: 0x00085983 File Offset: 0x00083B83
		internal NetDataContractSerializerOperationBehavior(OperationDescription operation) : base(operation)
		{
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x0008598C File Offset: 0x00083B8C
		public override XmlObjectSerializer CreateSerializer(Type type, string name, string ns, IList<Type> knownTypes)
		{
			return new NetDataContractSerializer(name, ns);
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x00085995 File Offset: 0x00083B95
		public override XmlObjectSerializer CreateSerializer(Type type, XmlDictionaryString name, XmlDictionaryString ns, IList<Type> knownTypes)
		{
			return new NetDataContractSerializer(name, ns);
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x000859A0 File Offset: 0x00083BA0
		internal static NetDataContractSerializerOperationBehavior ApplyTo(OperationDescription operation)
		{
			DataContractSerializerOperationBehavior dataContractSerializerOperationBehavior = operation.Behaviors.Find<DataContractSerializerOperationBehavior>();
			if (dataContractSerializerOperationBehavior != null)
			{
				NetDataContractSerializerOperationBehavior netDataContractSerializerOperationBehavior = new NetDataContractSerializerOperationBehavior(operation);
				operation.Behaviors.Remove(dataContractSerializerOperationBehavior);
				operation.Behaviors.Add(netDataContractSerializerOperationBehavior);
				return netDataContractSerializerOperationBehavior;
			}
			return null;
		}
	}
}
