using System;

namespace System.ServiceModel
{
	// Token: 0x020000E5 RID: 229
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
	[__DynamicallyInvokable]
	public sealed class XmlSerializerFormatAttribute : Attribute
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00016692 File Offset: 0x00014892
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0001669A File Offset: 0x0001489A
		[__DynamicallyInvokable]
		public bool SupportFaults
		{
			[__DynamicallyInvokable]
			get
			{
				return this.supportFaults;
			}
			[__DynamicallyInvokable]
			set
			{
				this.supportFaults = value;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x000166A3 File Offset: 0x000148A3
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x000166AB File Offset: 0x000148AB
		[__DynamicallyInvokable]
		public OperationFormatStyle Style
		{
			[__DynamicallyInvokable]
			get
			{
				return this.style;
			}
			[__DynamicallyInvokable]
			set
			{
				XmlSerializerFormatAttribute.ValidateOperationFormatStyle(value);
				this.style = value;
				this.isStyleSet = true;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x000166C1 File Offset: 0x000148C1
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x000166C9 File Offset: 0x000148C9
		public OperationFormatUse Use
		{
			get
			{
				return this.use;
			}
			set
			{
				XmlSerializerFormatAttribute.ValidateOperationFormatUse(value);
				this.use = value;
				if (!this.isStyleSet && this.IsEncoded)
				{
					this.Style = OperationFormatStyle.Rpc;
				}
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x000166EF File Offset: 0x000148EF
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x000166FA File Offset: 0x000148FA
		internal bool IsEncoded
		{
			get
			{
				return this.use == OperationFormatUse.Encoded;
			}
			set
			{
				this.use = (value ? OperationFormatUse.Encoded : OperationFormatUse.Literal);
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00016709 File Offset: 0x00014909
		internal static void ValidateOperationFormatStyle(OperationFormatStyle value)
		{
			if (!OperationFormatStyleHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00016728 File Offset: 0x00014928
		internal static void ValidateOperationFormatUse(OperationFormatUse value)
		{
			if (!OperationFormatUseHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00016747 File Offset: 0x00014947
		[__DynamicallyInvokable]
		public XmlSerializerFormatAttribute()
		{
		}

		// Token: 0x04000A0C RID: 2572
		private bool supportFaults;

		// Token: 0x04000A0D RID: 2573
		private OperationFormatStyle style;

		// Token: 0x04000A0E RID: 2574
		private bool isStyleSet;

		// Token: 0x04000A0F RID: 2575
		private OperationFormatUse use;
	}
}
