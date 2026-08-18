using System;

namespace System.Web.ModelBinding
{
	// Token: 0x0200062F RID: 1583
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class BindingBehaviorAttribute : Attribute
	{
		// Token: 0x06004EE0 RID: 20192 RVA: 0x0011271C File Offset: 0x0011091C
		public BindingBehaviorAttribute(BindingBehavior behavior)
		{
			this.Behavior = behavior;
		}

		// Token: 0x170016D1 RID: 5841
		// (get) Token: 0x06004EE1 RID: 20193 RVA: 0x0011272B File Offset: 0x0011092B
		// (set) Token: 0x06004EE2 RID: 20194 RVA: 0x00112733 File Offset: 0x00110933
		public BindingBehavior Behavior { get; private set; }

		// Token: 0x170016D2 RID: 5842
		// (get) Token: 0x06004EE3 RID: 20195 RVA: 0x0011273C File Offset: 0x0011093C
		public override object TypeId
		{
			get
			{
				return BindingBehaviorAttribute._typeId;
			}
		}

		// Token: 0x04002A58 RID: 10840
		private static readonly object _typeId = new object();
	}
}
