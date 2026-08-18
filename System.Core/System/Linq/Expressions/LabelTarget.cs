using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000245 RID: 581
	[__DynamicallyInvokable]
	public sealed class LabelTarget
	{
		// Token: 0x0600154F RID: 5455 RVA: 0x0004830B File Offset: 0x0004650B
		internal LabelTarget(Type type, string name)
		{
			this._type = type;
			this._name = name;
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x00048321 File Offset: 0x00046521
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this._name;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x00048329 File Offset: 0x00046529
		[__DynamicallyInvokable]
		public Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return this._type;
			}
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x00048331 File Offset: 0x00046531
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.Name))
			{
				return this.Name;
			}
			return "UnamedLabel";
		}

		// Token: 0x04000A0F RID: 2575
		private readonly Type _type;

		// Token: 0x04000A10 RID: 2576
		private readonly string _name;
	}
}
