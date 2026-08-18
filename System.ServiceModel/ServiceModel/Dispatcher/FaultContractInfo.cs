using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel.Description;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200055D RID: 1373
	[__DynamicallyInvokable]
	public class FaultContractInfo
	{
		// Token: 0x06003585 RID: 13701 RVA: 0x000D05FA File Offset: 0x000CE7FA
		[__DynamicallyInvokable]
		public FaultContractInfo(string action, Type detail) : this(action, detail, null, null, null)
		{
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x000D0608 File Offset: 0x000CE808
		internal FaultContractInfo(string action, Type detail, XmlName elementName, string ns, IList<Type> knownTypes)
		{
			if (action == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("action");
			}
			if (detail == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("detail");
			}
			this.action = action;
			this.detail = detail;
			if (elementName != null)
			{
				this.elementName = elementName.EncodedName;
			}
			this.ns = ns;
			this.knownTypes = knownTypes;
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06003587 RID: 13703 RVA: 0x000D067A File Offset: 0x000CE87A
		[__DynamicallyInvokable]
		public string Action
		{
			[__DynamicallyInvokable]
			get
			{
				return this.action;
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06003588 RID: 13704 RVA: 0x000D0682 File Offset: 0x000CE882
		[__DynamicallyInvokable]
		public Type Detail
		{
			[__DynamicallyInvokable]
			get
			{
				return this.detail;
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06003589 RID: 13705 RVA: 0x000D068A File Offset: 0x000CE88A
		internal string ElementName
		{
			get
			{
				return this.elementName;
			}
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x0600358A RID: 13706 RVA: 0x000D0692 File Offset: 0x000CE892
		internal string ElementNamespace
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x0600358B RID: 13707 RVA: 0x000D069A File Offset: 0x000CE89A
		internal IList<Type> KnownTypes
		{
			get
			{
				return this.knownTypes;
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x0600358C RID: 13708 RVA: 0x000D06A4 File Offset: 0x000CE8A4
		internal DataContractSerializer Serializer
		{
			get
			{
				if (this.serializer == null)
				{
					if (this.elementName == null)
					{
						this.serializer = DataContractSerializerDefaults.CreateSerializer(this.detail, this.knownTypes, int.MaxValue);
					}
					else
					{
						this.serializer = DataContractSerializerDefaults.CreateSerializer(this.detail, this.knownTypes, this.elementName, (this.ns == null) ? string.Empty : this.ns, int.MaxValue);
					}
				}
				return this.serializer;
			}
		}

		// Token: 0x04002883 RID: 10371
		private string action;

		// Token: 0x04002884 RID: 10372
		private Type detail;

		// Token: 0x04002885 RID: 10373
		private string elementName;

		// Token: 0x04002886 RID: 10374
		private string ns;

		// Token: 0x04002887 RID: 10375
		private IList<Type> knownTypes;

		// Token: 0x04002888 RID: 10376
		private DataContractSerializer serializer;
	}
}
