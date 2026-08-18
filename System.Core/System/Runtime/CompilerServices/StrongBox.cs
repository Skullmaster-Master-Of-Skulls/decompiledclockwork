using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000146 RID: 326
	[__DynamicallyInvokable]
	public class StrongBox<T> : IStrongBox
	{
		// Token: 0x06000A9A RID: 2714 RVA: 0x0002642E File Offset: 0x0002462E
		[__DynamicallyInvokable]
		public StrongBox()
		{
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00026436 File Offset: 0x00024636
		[__DynamicallyInvokable]
		public StrongBox(T value)
		{
			this.Value = value;
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x00026445 File Offset: 0x00024645
		// (set) Token: 0x06000A9D RID: 2717 RVA: 0x00026452 File Offset: 0x00024652
		[__DynamicallyInvokable]
		object IStrongBox.Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Value;
			}
			[__DynamicallyInvokable]
			set
			{
				this.Value = (T)((object)value);
			}
		}

		// Token: 0x0400077A RID: 1914
		[__DynamicallyInvokable]
		public T Value;
	}
}
