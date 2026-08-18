using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200055E RID: 1374
	internal class FaultFormatter : IClientFaultFormatter, IDispatchFaultFormatter
	{
		// Token: 0x0600358D RID: 13709 RVA: 0x000D071C File Offset: 0x000CE91C
		internal FaultFormatter(Type[] detailTypes)
		{
			List<FaultContractInfo> list = new List<FaultContractInfo>();
			for (int i = 0; i < detailTypes.Length; i++)
			{
				list.Add(new FaultContractInfo("*", detailTypes[i]));
			}
			FaultFormatter.AddInfrastructureFaults(list);
			this.faultContractInfos = FaultFormatter.GetSortedArray(list);
		}

		// Token: 0x0600358E RID: 13710 RVA: 0x000D0768 File Offset: 0x000CE968
		internal FaultFormatter(SynchronizedCollection<FaultContractInfo> faultContractInfoCollection)
		{
			object syncRoot = faultContractInfoCollection.SyncRoot;
			List<FaultContractInfo> faultContractInfoList;
			lock (syncRoot)
			{
				faultContractInfoList = new List<FaultContractInfo>(faultContractInfoCollection);
			}
			FaultFormatter.AddInfrastructureFaults(faultContractInfoList);
			this.faultContractInfos = FaultFormatter.GetSortedArray(faultContractInfoList);
		}

		// Token: 0x0600358F RID: 13711 RVA: 0x000D07C4 File Offset: 0x000CE9C4
		public MessageFault Serialize(FaultException faultException, out string action)
		{
			XmlObjectSerializer serializer = null;
			Type detailType = null;
			string action2;
			action = (action2 = faultException.Action);
			string faultExceptionAction = action2;
			Type type = null;
			Type type2 = faultException.GetType();
			while (type2 != typeof(FaultException))
			{
				if (type2.IsGenericType && type2.GetGenericTypeDefinition() == typeof(FaultException<>))
				{
					type = type2;
					break;
				}
				type2 = type2.BaseType;
			}
			if (type != null)
			{
				detailType = type.GetGenericArguments()[0];
				serializer = this.GetSerializer(detailType, faultExceptionAction, out action);
			}
			return FaultFormatter.CreateMessageFault(serializer, faultException, detailType);
		}

		// Token: 0x06003590 RID: 13712 RVA: 0x000D0855 File Offset: 0x000CEA55
		public FaultException Deserialize(MessageFault messageFault, string action)
		{
			if (!messageFault.HasDetail)
			{
				return new FaultException(messageFault, action);
			}
			return this.CreateFaultException(messageFault, action);
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x000D0870 File Offset: 0x000CEA70
		protected virtual XmlObjectSerializer GetSerializer(Type detailType, string faultExceptionAction, out string action)
		{
			action = faultExceptionAction;
			FaultContractInfo faultContractInfo = null;
			for (int i = 0; i < this.faultContractInfos.Length; i++)
			{
				if (this.faultContractInfos[i].Detail == detailType)
				{
					faultContractInfo = this.faultContractInfos[i];
					break;
				}
			}
			if (faultContractInfo != null)
			{
				if (action == null)
				{
					action = faultContractInfo.Action;
				}
				return faultContractInfo.Serializer;
			}
			return DataContractSerializerDefaults.CreateSerializer(detailType, int.MaxValue);
		}

		// Token: 0x06003592 RID: 13714 RVA: 0x000D08D8 File Offset: 0x000CEAD8
		protected virtual FaultException CreateFaultException(MessageFault messageFault, string action)
		{
			IList<FaultContractInfo> list;
			if (action != null)
			{
				list = new List<FaultContractInfo>();
				for (int i = 0; i < this.faultContractInfos.Length; i++)
				{
					if (this.faultContractInfos[i].Action == action || this.faultContractInfos[i].Action == "*")
					{
						list.Add(this.faultContractInfos[i]);
					}
				}
			}
			else
			{
				list = this.faultContractInfos;
			}
			for (int j = 0; j < list.Count; j++)
			{
				FaultContractInfo faultContractInfo = list[j];
				XmlDictionaryReader readerAtDetailContents = messageFault.GetReaderAtDetailContents();
				XmlObjectSerializer serializer = faultContractInfo.Serializer;
				if (serializer.IsStartObject(readerAtDetailContents))
				{
					Type detail = faultContractInfo.Detail;
					try
					{
						object detailObj = serializer.ReadObject(readerAtDetailContents);
						FaultException ex = this.CreateFaultException(messageFault, action, detailObj, detail, readerAtDetailContents);
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

		// Token: 0x06003593 RID: 13715 RVA: 0x000D09D0 File Offset: 0x000CEBD0
		protected FaultException CreateFaultException(MessageFault messageFault, string action, object detailObj, Type detailType, XmlDictionaryReader detailReader)
		{
			if (!detailReader.EOF)
			{
				detailReader.MoveToContent();
				if (detailReader.NodeType != XmlNodeType.EndElement && !detailReader.EOF)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("ExtraContentIsPresentInFaultDetail")));
				}
			}
			bool flag;
			if (detailObj == null)
			{
				flag = !detailType.IsValueType;
			}
			else
			{
				flag = detailType.IsAssignableFrom(detailObj.GetType());
			}
			if (flag)
			{
				Type type = typeof(FaultException<>).MakeGenericType(new Type[]
				{
					detailType
				});
				return (FaultException)Activator.CreateInstance(type, new object[]
				{
					detailObj,
					messageFault.Reason,
					messageFault.Code,
					action
				});
			}
			return null;
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x000D0A88 File Offset: 0x000CEC88
		private static FaultContractInfo[] GetSortedArray(List<FaultContractInfo> faultContractInfoList)
		{
			FaultContractInfo[] array = faultContractInfoList.ToArray();
			Array.Sort<FaultContractInfo>(array, (FaultContractInfo x, FaultContractInfo y) => string.CompareOrdinal(x.Action, y.Action));
			return array;
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x000D0AC2 File Offset: 0x000CECC2
		private static void AddInfrastructureFaults(List<FaultContractInfo> faultContractInfos)
		{
			faultContractInfos.Add(new FaultContractInfo("http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/dispatcher/fault", typeof(ExceptionDetail)));
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x000D0AE0 File Offset: 0x000CECE0
		private static MessageFault CreateMessageFault(XmlObjectSerializer serializer, FaultException faultException, Type detailType)
		{
			if (!(detailType == null))
			{
				Type type = typeof(FaultFormatter.OperationFault<>).MakeGenericType(new Type[]
				{
					detailType
				});
				return (MessageFault)Activator.CreateInstance(type, new object[]
				{
					serializer,
					faultException
				});
			}
			if (faultException.Fault != null)
			{
				return faultException.Fault;
			}
			return MessageFault.CreateFault(faultException.Code, faultException.Reason);
		}

		// Token: 0x04002889 RID: 10377
		private FaultContractInfo[] faultContractInfos;

		// Token: 0x02000C84 RID: 3204
		internal class OperationFault<T> : XmlObjectSerializerFault
		{
			// Token: 0x06007889 RID: 30857 RVA: 0x001C2437 File Offset: 0x001C0637
			public OperationFault(XmlObjectSerializer serializer, FaultException<T> faultException) : base(faultException.Code, faultException.Reason, faultException.Detail, serializer, string.Empty, string.Empty)
			{
			}
		}
	}
}
