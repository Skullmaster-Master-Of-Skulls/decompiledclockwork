using System;

namespace System.ServiceModel
{
	// Token: 0x020000D9 RID: 217
	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class MessageParameterAttribute : Attribute
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00015950 File Offset: 0x00013B50
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x00015958 File Offset: 0x00013B58
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.name;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (value == string.Empty)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("SFxNameCannotBeEmpty")));
				}
				this.name = value;
				this.isNameSetExplicit = true;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x000159B2 File Offset: 0x00013BB2
		internal bool IsNameSetExplicit
		{
			get
			{
				return this.isNameSetExplicit;
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000159BA File Offset: 0x00013BBA
		[__DynamicallyInvokable]
		public MessageParameterAttribute()
		{
		}

		// Token: 0x040009C1 RID: 2497
		private string name;

		// Token: 0x040009C2 RID: 2498
		private bool isNameSetExplicit;

		// Token: 0x040009C3 RID: 2499
		internal const string NamePropertyName = "Name";
	}
}
