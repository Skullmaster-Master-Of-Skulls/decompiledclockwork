using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Resources;
using System.Web.Services.Description;
using System.Xml;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x02000027 RID: 39
	internal class WsdlInspector
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x000078AA File Offset: 0x00005AAA
		private WsdlInspector(IList<ProxyGenerationError> importErrors)
		{
			this.importErrors = importErrors;
			this.portTypes = new Dictionary<XmlQualifiedName, PortType>();
			this.messages = new Dictionary<XmlQualifiedName, Message>();
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000078D0 File Offset: 0x00005AD0
		internal static void CheckDuplicatedWsdlItems(ICollection<ServiceDescription> wsdlFiles, IList<ProxyGenerationError> importErrors)
		{
			WsdlInspector wsdlInspector = new WsdlInspector(importErrors);
			wsdlInspector.CheckServiceDescriptions(wsdlFiles);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000078EC File Offset: 0x00005AEC
		private void CheckServiceDescriptions(ICollection<ServiceDescription> wsdlFiles)
		{
			foreach (ServiceDescription serviceDescription in wsdlFiles)
			{
				string text = serviceDescription.TargetNamespace;
				if (string.IsNullOrEmpty(text))
				{
					text = string.Empty;
				}
				foreach (object obj in serviceDescription.PortTypes)
				{
					PortType portType = (PortType)obj;
					XmlQualifiedName key = new XmlQualifiedName(portType.Name, text);
					PortType x;
					if (this.portTypes.TryGetValue(key, out x))
					{
						this.MatchPortTypes(x, portType);
					}
					else
					{
						this.portTypes.Add(key, portType);
					}
				}
				foreach (object obj2 in serviceDescription.Messages)
				{
					Message message = (Message)obj2;
					XmlQualifiedName key2 = new XmlQualifiedName(message.Name, text);
					Message x2;
					if (this.messages.TryGetValue(key2, out x2))
					{
						this.MatchMessages(x2, message);
					}
					else
					{
						this.messages.Add(key2, message);
					}
				}
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00007A70 File Offset: 0x00005C70
		private void MatchPortTypes(PortType x, PortType y)
		{
			Operation[] array = new Operation[x.Operations.Count];
			x.Operations.CopyTo(array, 0);
			Array.Sort<Operation>(array, new WsdlInspector.OperationComparer());
			Operation[] array2 = new Operation[y.Operations.Count];
			y.Operations.CopyTo(array2, 0);
			Array.Sort<Operation>(array2, new WsdlInspector.OperationComparer());
			this.MatchCollections<Operation>(array, array2, delegate(Operation operationX, Operation operationY)
			{
				if (operationX != null && operationY != null)
				{
					int num = string.Compare(operationX.Name, operationY.Name, StringComparison.Ordinal);
					if (num < 0)
					{
						this.ReportUniqueOperation(operationX, x, y);
						return false;
					}
					if (num > 0)
					{
						this.ReportUniqueOperation(operationY, y, x);
						return false;
					}
					return this.MatchOperations(operationX, operationY);
				}
				else
				{
					if (operationX != null)
					{
						this.ReportUniqueOperation(operationX, x, y);
						return false;
					}
					if (operationY != null)
					{
						this.ReportUniqueOperation(operationY, y, x);
						return false;
					}
					return true;
				}
			});
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00007B14 File Offset: 0x00005D14
		private bool MatchOperations(Operation x, Operation y)
		{
			if (!this.MatchOperationMessages(x.Messages.Input, y.Messages.Input))
			{
				this.ReportOperationDefinedDifferently(x, y);
				return false;
			}
			if (!this.MatchOperationMessages(x.Messages.Output, y.Messages.Output))
			{
				this.ReportOperationDefinedDifferently(x, y);
				return false;
			}
			OperationFault[] array = new OperationFault[x.Faults.Count];
			x.Faults.CopyTo(array, 0);
			Array.Sort<OperationFault>(array, new WsdlInspector.OperationFaultComparer());
			OperationFault[] array2 = new OperationFault[y.Faults.Count];
			y.Faults.CopyTo(array2, 0);
			Array.Sort<OperationFault>(array2, new WsdlInspector.OperationFaultComparer());
			if (!this.MatchCollections<OperationFault>(array, array2, delegate(OperationFault faultX, OperationFault faultY)
			{
				if (faultX != null && faultY != null)
				{
					return this.MatchXmlQualifiedNames(faultX.Message, faultY.Message);
				}
				return faultX == null && faultY == null;
			}))
			{
				this.ReportOperationDefinedDifferently(x, y);
				return false;
			}
			return true;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00007BE4 File Offset: 0x00005DE4
		private bool MatchOperationMessages(OperationMessage x, OperationMessage y)
		{
			return (x == null && y == null) || (x != null && y != null && this.MatchXmlQualifiedNames(x.Message, y.Message));
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00007C08 File Offset: 0x00005E08
		private void MatchMessages(Message x, Message y)
		{
			MessagePart[] array = new MessagePart[x.Parts.Count];
			x.Parts.CopyTo(array, 0);
			Array.Sort<MessagePart>(array, new WsdlInspector.MessagePartComparer());
			MessagePart[] array2 = new MessagePart[y.Parts.Count];
			y.Parts.CopyTo(array2, 0);
			Array.Sort<MessagePart>(array2, new WsdlInspector.MessagePartComparer());
			this.MatchCollections<MessagePart>(array, array2, delegate(MessagePart partX, MessagePart partY)
			{
				if (partX != null && partY != null)
				{
					int num = string.Compare(partX.Name, partY.Name, StringComparison.Ordinal);
					if (num < 0)
					{
						this.ReportUniqueMessagePart(partX, x, y);
						return false;
					}
					if (num > 0)
					{
						this.ReportUniqueMessagePart(partY, y, x);
						return false;
					}
					return this.MatchMessageParts(partX, partY);
				}
				else
				{
					if (partX != null)
					{
						this.ReportUniqueMessagePart(partX, x, y);
						return false;
					}
					if (partY != null)
					{
						this.ReportUniqueMessagePart(partY, y, x);
						return false;
					}
					return true;
				}
			});
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007CAB File Offset: 0x00005EAB
		private bool MatchMessageParts(MessagePart partX, MessagePart partY)
		{
			if (!this.MatchXmlQualifiedNames(partX.Type, partY.Type) || !this.MatchXmlQualifiedNames(partX.Element, partY.Element))
			{
				this.ReportMessageDefinedDifferently(partX, partX.Message, partY.Message);
				return false;
			}
			return true;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007CEB File Offset: 0x00005EEB
		private bool MatchXmlQualifiedNames(XmlQualifiedName x, XmlQualifiedName y)
		{
			if (x != null && y != null)
			{
				return x == y;
			}
			return x == null && y == null;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007D1C File Offset: 0x00005F1C
		private void ReportUniqueOperation(Operation operation, PortType portType1, PortType portType2)
		{
			this.importErrors.Add(new ProxyGenerationError(ProxyGenerationError.GeneratorState.MergeMetadata, string.Empty, new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, WCFModelStrings.ReferenceGroup_OperationDefinedInOneOfDuplicatedServiceContract, new object[]
			{
				portType1.Name,
				portType1.ServiceDescription.RetrievalUrl,
				portType2.ServiceDescription.RetrievalUrl,
				operation.Name
			}))));
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00007D88 File Offset: 0x00005F88
		private void ReportOperationDefinedDifferently(Operation x, Operation y)
		{
			this.importErrors.Add(new ProxyGenerationError(ProxyGenerationError.GeneratorState.MergeMetadata, string.Empty, new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, WCFModelStrings.ReferenceGroup_OperationDefinedDifferently, new object[]
			{
				x.Name,
				x.PortType.Name,
				x.PortType.ServiceDescription.RetrievalUrl,
				y.PortType.ServiceDescription.RetrievalUrl
			}))));
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007E04 File Offset: 0x00006004
		private void ReportUniqueMessagePart(MessagePart part, Message message1, Message message2)
		{
			this.importErrors.Add(new ProxyGenerationError(ProxyGenerationError.GeneratorState.MergeMetadata, string.Empty, new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, WCFModelStrings.ReferenceGroup_FieldDefinedInOneOfDuplicatedMessage, new object[]
			{
				message1.Name,
				message1.ServiceDescription.RetrievalUrl,
				message2.ServiceDescription.RetrievalUrl,
				part.Name
			}))));
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007E70 File Offset: 0x00006070
		private void ReportMessageDefinedDifferently(MessagePart part, Message x, Message y)
		{
			this.importErrors.Add(new ProxyGenerationError(ProxyGenerationError.GeneratorState.MergeMetadata, string.Empty, new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, WCFModelStrings.ReferenceGroup_FieldDefinedDifferentlyInDuplicatedMessage, new object[]
			{
				part.Name,
				x.Name,
				x.ServiceDescription.RetrievalUrl,
				y.ServiceDescription.RetrievalUrl
			}))));
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007EDC File Offset: 0x000060DC
		private bool MatchCollections<T>(T[] x, T[] y, WsdlInspector.MatchCollectionItemDelegate<T> compareItems) where T : class
		{
			IEnumerator enumerator = x.GetEnumerator();
			IEnumerator enumerator2 = y.GetEnumerator();
			T t;
			T t2;
			for (;;)
			{
				t = (enumerator.MoveNext() ? ((T)((object)enumerator.Current)) : default(T));
				t2 = (enumerator2.MoveNext() ? ((T)((object)enumerator2.Current)) : default(T));
				if (t != null && t2 != null && !compareItems(t, t2))
				{
					break;
				}
				if (t == null || t2 == null)
				{
					goto IL_7A;
				}
			}
			return false;
			IL_7A:
			return compareItems(t, t2);
		}

		// Token: 0x04000082 RID: 130
		private IList<ProxyGenerationError> importErrors;

		// Token: 0x04000083 RID: 131
		private Dictionary<XmlQualifiedName, PortType> portTypes;

		// Token: 0x04000084 RID: 132
		private Dictionary<XmlQualifiedName, Message> messages;

		// Token: 0x02000133 RID: 307
		private class OperationComparer : IComparer<Operation>
		{
			// Token: 0x06000F5F RID: 3935 RVA: 0x0003719B File Offset: 0x0003539B
			public int Compare(Operation x, Operation y)
			{
				return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
			}
		}

		// Token: 0x02000134 RID: 308
		private class OperationFaultComparer : IComparer<OperationFault>
		{
			// Token: 0x06000F61 RID: 3937 RVA: 0x000371B0 File Offset: 0x000353B0
			public int Compare(OperationFault x, OperationFault y)
			{
				int num = string.Compare(x.Message.Namespace, y.Message.Namespace, StringComparison.Ordinal);
				if (num != 0)
				{
					return num;
				}
				return string.Compare(x.Message.Name, y.Message.Name, StringComparison.Ordinal);
			}
		}

		// Token: 0x02000135 RID: 309
		private class MessagePartComparer : IComparer<MessagePart>
		{
			// Token: 0x06000F63 RID: 3939 RVA: 0x0003719B File Offset: 0x0003539B
			public int Compare(MessagePart x, MessagePart y)
			{
				return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
			}
		}

		// Token: 0x02000136 RID: 310
		// (Invoke) Token: 0x06000F66 RID: 3942
		private delegate bool MatchCollectionItemDelegate<T>(T x, T y);
	}
}
