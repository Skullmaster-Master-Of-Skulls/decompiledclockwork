using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace System.ServiceModel.Description
{
	// Token: 0x020003DD RID: 989
	[__DynamicallyInvokable]
	public class DataContractSerializerOperationBehavior : IOperationBehavior, IWsdlExportExtension
	{
		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06002533 RID: 9523 RVA: 0x0008568B File Offset: 0x0008388B
		[__DynamicallyInvokable]
		public DataContractFormatAttribute DataContractFormatAttribute
		{
			[__DynamicallyInvokable]
			get
			{
				return this.dataContractFormatAttribute;
			}
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x00085693 File Offset: 0x00083893
		[__DynamicallyInvokable]
		public DataContractSerializerOperationBehavior(OperationDescription operation) : this(operation, null)
		{
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x0008569D File Offset: 0x0008389D
		[__DynamicallyInvokable]
		public DataContractSerializerOperationBehavior(OperationDescription operation, DataContractFormatAttribute dataContractFormatAttribute)
		{
			this.dataContractFormatAttribute = (dataContractFormatAttribute ?? new DataContractFormatAttribute());
			this.operation = operation;
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000856C7 File Offset: 0x000838C7
		internal DataContractSerializerOperationBehavior(OperationDescription operation, DataContractFormatAttribute dataContractFormatAttribute, bool builtInOperationBehavior) : this(operation, dataContractFormatAttribute)
		{
			this.builtInOperationBehavior = builtInOperationBehavior;
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06002537 RID: 9527 RVA: 0x000856D8 File Offset: 0x000838D8
		internal bool IsBuiltInOperationBehavior
		{
			get
			{
				return this.builtInOperationBehavior;
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06002538 RID: 9528 RVA: 0x000856E0 File Offset: 0x000838E0
		// (set) Token: 0x06002539 RID: 9529 RVA: 0x000856E8 File Offset: 0x000838E8
		[__DynamicallyInvokable]
		public int MaxItemsInObjectGraph
		{
			[__DynamicallyInvokable]
			get
			{
				return this.maxItemsInObjectGraph;
			}
			[__DynamicallyInvokable]
			set
			{
				this.maxItemsInObjectGraph = value;
				this.maxItemsInObjectGraphSetExplicit = true;
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x0600253A RID: 9530 RVA: 0x000856F8 File Offset: 0x000838F8
		// (set) Token: 0x0600253B RID: 9531 RVA: 0x00085700 File Offset: 0x00083900
		internal bool MaxItemsInObjectGraphSetExplicit
		{
			get
			{
				return this.maxItemsInObjectGraphSetExplicit;
			}
			set
			{
				this.maxItemsInObjectGraphSetExplicit = value;
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x0600253C RID: 9532 RVA: 0x00085709 File Offset: 0x00083909
		// (set) Token: 0x0600253D RID: 9533 RVA: 0x00085711 File Offset: 0x00083911
		public bool IgnoreExtensionDataObject
		{
			get
			{
				return this.ignoreExtensionDataObject;
			}
			set
			{
				this.ignoreExtensionDataObject = value;
				this.ignoreExtensionDataObjectSetExplicit = true;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x0600253E RID: 9534 RVA: 0x00085721 File Offset: 0x00083921
		// (set) Token: 0x0600253F RID: 9535 RVA: 0x00085729 File Offset: 0x00083929
		internal bool IgnoreExtensionDataObjectSetExplicit
		{
			get
			{
				return this.ignoreExtensionDataObjectSetExplicit;
			}
			set
			{
				this.ignoreExtensionDataObjectSetExplicit = value;
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06002540 RID: 9536 RVA: 0x00085732 File Offset: 0x00083932
		// (set) Token: 0x06002541 RID: 9537 RVA: 0x0008573A File Offset: 0x0008393A
		public IDataContractSurrogate DataContractSurrogate
		{
			get
			{
				return this.dataContractSurrogate;
			}
			set
			{
				this.dataContractSurrogate = value;
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06002542 RID: 9538 RVA: 0x00085743 File Offset: 0x00083943
		// (set) Token: 0x06002543 RID: 9539 RVA: 0x0008574B File Offset: 0x0008394B
		[__DynamicallyInvokable]
		public DataContractResolver DataContractResolver
		{
			[__DynamicallyInvokable]
			get
			{
				return this.dataContractResolver;
			}
			[__DynamicallyInvokable]
			set
			{
				this.dataContractResolver = value;
			}
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x00085754 File Offset: 0x00083954
		[__DynamicallyInvokable]
		public virtual XmlObjectSerializer CreateSerializer(Type type, string name, string ns, IList<Type> knownTypes)
		{
			return new DataContractSerializer(type, name, ns, knownTypes, this.MaxItemsInObjectGraph, this.IgnoreExtensionDataObject, false, this.DataContractSurrogate, this.DataContractResolver);
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x00085784 File Offset: 0x00083984
		[__DynamicallyInvokable]
		public virtual XmlObjectSerializer CreateSerializer(Type type, XmlDictionaryString name, XmlDictionaryString ns, IList<Type> knownTypes)
		{
			return new DataContractSerializer(type, name, ns, knownTypes, this.MaxItemsInObjectGraph, this.IgnoreExtensionDataObject, false, this.DataContractSurrogate, this.DataContractResolver);
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x000857B4 File Offset: 0x000839B4
		internal object GetFormatter(OperationDescription operation, out bool formatRequest, out bool formatReply, bool isProxy)
		{
			MessageDescription messageDescription = operation.Messages[0];
			MessageDescription messageDescription2 = null;
			if (operation.Messages.Count == 2)
			{
				messageDescription2 = operation.Messages[1];
			}
			formatRequest = (messageDescription != null && !messageDescription.IsUntypedMessage);
			formatReply = (messageDescription2 != null && !messageDescription2.IsUntypedMessage);
			if (!(formatRequest | formatReply))
			{
				return null;
			}
			if (PrimitiveOperationFormatter.IsContractSupported(operation))
			{
				return new PrimitiveOperationFormatter(operation, this.dataContractFormatAttribute.Style == OperationFormatStyle.Rpc);
			}
			return new DataContractSerializerOperationFormatter(operation, this.dataContractFormatAttribute, this);
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x00085840 File Offset: 0x00083A40
		[__DynamicallyInvokable]
		void IOperationBehavior.Validate(OperationDescription description)
		{
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x00085842 File Offset: 0x00083A42
		[__DynamicallyInvokable]
		void IOperationBehavior.AddBindingParameters(OperationDescription description, BindingParameterCollection parameters)
		{
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x00085844 File Offset: 0x00083A44
		[__DynamicallyInvokable]
		void IOperationBehavior.ApplyDispatchBehavior(OperationDescription description, DispatchOperation dispatch)
		{
			if (description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("description");
			}
			if (dispatch == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dispatch");
			}
			if (dispatch.Formatter != null)
			{
				return;
			}
			bool deserializeRequest;
			bool serializeReply;
			dispatch.Formatter = (IDispatchMessageFormatter)this.GetFormatter(description, out deserializeRequest, out serializeReply, false);
			dispatch.DeserializeRequest = deserializeRequest;
			dispatch.SerializeReply = serializeReply;
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x000858A8 File Offset: 0x00083AA8
		[__DynamicallyInvokable]
		void IOperationBehavior.ApplyClientBehavior(OperationDescription description, ClientOperation proxy)
		{
			if (description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("description");
			}
			if (proxy == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("proxy");
			}
			if (proxy.Formatter != null)
			{
				return;
			}
			bool serializeRequest;
			bool deserializeReply;
			proxy.Formatter = (IClientMessageFormatter)this.GetFormatter(description, out serializeRequest, out deserializeReply, true);
			proxy.SerializeRequest = serializeRequest;
			proxy.DeserializeReply = deserializeReply;
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x00085909 File Offset: 0x00083B09
		void IWsdlExportExtension.ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext endpointContext)
		{
			if (exporter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("exporter");
			}
			if (endpointContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointContext");
			}
			MessageContractExporter.ExportMessageBinding(exporter, endpointContext, typeof(DataContractSerializerMessageContractExporter), this.operation);
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x00085948 File Offset: 0x00083B48
		void IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext contractContext)
		{
			if (exporter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("exporter");
			}
			if (contractContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contractContext");
			}
			new DataContractSerializerMessageContractExporter(exporter, contractContext, this.operation, this).ExportMessageContract();
		}

		// Token: 0x040020BB RID: 8379
		private readonly bool builtInOperationBehavior;

		// Token: 0x040020BC RID: 8380
		private OperationDescription operation;

		// Token: 0x040020BD RID: 8381
		private DataContractFormatAttribute dataContractFormatAttribute;

		// Token: 0x040020BE RID: 8382
		internal bool ignoreExtensionDataObject;

		// Token: 0x040020BF RID: 8383
		private bool ignoreExtensionDataObjectSetExplicit;

		// Token: 0x040020C0 RID: 8384
		internal int maxItemsInObjectGraph = int.MaxValue;

		// Token: 0x040020C1 RID: 8385
		private bool maxItemsInObjectGraphSetExplicit;

		// Token: 0x040020C2 RID: 8386
		private IDataContractSurrogate dataContractSurrogate;

		// Token: 0x040020C3 RID: 8387
		private DataContractResolver dataContractResolver;
	}
}
