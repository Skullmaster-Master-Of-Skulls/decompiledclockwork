using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000563 RID: 1379
	internal class XmlSerializerFaultFormatter : FaultFormatter
	{
		// Token: 0x0600359D RID: 13725 RVA: 0x000D0B5C File Offset: 0x000CED5C
		internal XmlSerializerFaultFormatter(Type[] detailTypes, SynchronizedCollection<XmlSerializerOperationBehavior.Reflector.XmlSerializerFaultContractInfo> xmlSerializerFaultContractInfos) : base(detailTypes)
		{
			this.Initialize(xmlSerializerFaultContractInfos);
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x000D0B6C File Offset: 0x000CED6C
		internal XmlSerializerFaultFormatter(SynchronizedCollection<FaultContractInfo> faultContractInfoCollection, SynchronizedCollection<XmlSerializerOperationBehavior.Reflector.XmlSerializerFaultContractInfo> xmlSerializerFaultContractInfos) : base(faultContractInfoCollection)
		{
			this.Initialize(xmlSerializerFaultContractInfos);
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x000D0B7C File Offset: 0x000CED7C
		private void Initialize(SynchronizedCollection<XmlSerializerOperationBehavior.Reflector.XmlSerializerFaultContractInfo> xmlSerializerFaultContractInfos)
		{
			if (xmlSerializerFaultContractInfos == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("xmlSerializerFaultContractInfos");
			}
			this.xmlSerializerFaultContractInfos = xmlSerializerFaultContractInfos;
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x000D0B98 File Offset: 0x000CED98
		protected override XmlObjectSerializer GetSerializer(Type detailType, string faultExceptionAction, out string action)
		{
			action = faultExceptionAction;
			XmlSerializerOperationBehavior.Reflector.XmlSerializerFaultContractInfo xmlSerializerFaultContractInfo = null;
			for (int i = 0; i < this.xmlSerializerFaultContractInfos.Count; i++)
			{
				if (this.xmlSerializerFaultContractInfos[i].FaultContractInfo.Detail == detailType)
				{
					xmlSerializerFaultContractInfo = this.xmlSerializerFaultContractInfos[i];
					break;
				}
			}
			if (xmlSerializerFaultContractInfo != null)
			{
				if (action == null)
				{
					action = xmlSerializerFaultContractInfo.FaultContractInfo.Action;
				}
				return xmlSerializerFaultContractInfo.Serializer;
			}
			return new XmlSerializerObjectSerializer(detailType);
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x000D0C10 File Offset: 0x000CEE10
		protected override FaultException CreateFaultException(MessageFault messageFault, string action)
		{
			IList<XmlSerializerOperationBehavior.Reflector.XmlSerializerFaultContractInfo> list;
			if (action != null)
			{
				list = new List<XmlSerializerOperationBehavior.Reflector.XmlSerializerFaultContractInfo>();
				for (int i = 0; i < this.xmlSerializerFaultContractInfos.Count; i++)
				{
					if (this.xmlSerializerFaultContractInfos[i].FaultContractInfo.Action == action || this.xmlSerializerFaultContractInfos[i].FaultContractInfo.Action == "*")
					{
						list.Add(this.xmlSerializerFaultContractInfos[i]);
					}
				}
			}
			else
			{
				list = this.xmlSerializerFaultContractInfos;
			}
			for (int j = 0; j < list.Count; j++)
			{
				XmlSerializerOperationBehavior.Reflector.XmlSerializerFaultContractInfo xmlSerializerFaultContractInfo = list[j];
				XmlDictionaryReader readerAtDetailContents = messageFault.GetReaderAtDetailContents();
				XmlObjectSerializer serializer = xmlSerializerFaultContractInfo.Serializer;
				if (serializer.IsStartObject(readerAtDetailContents))
				{
					Type detail = xmlSerializerFaultContractInfo.FaultContractInfo.Detail;
					try
					{
						object detailObj = serializer.ReadObject(readerAtDetailContents);
						FaultException ex = base.CreateFaultException(messageFault, action, detailObj, detail, readerAtDetailContents);
						if (ex != null)
						{
							return ex;
						}
					}
					catch (SerializationException)
					{
					}
				}
			}
			return new FaultException(messageFault, action);
		}

		// Token: 0x0400288A RID: 10378
		private SynchronizedCollection<XmlSerializerOperationBehavior.Reflector.XmlSerializerFaultContractInfo> xmlSerializerFaultContractInfos;
	}
}
