using System;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000122 RID: 290
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class HttpBindingBehaviorAttribute : Attribute
	{
		// Token: 0x06000708 RID: 1800 RVA: 0x0001742B File Offset: 0x0001562B
		public HttpBindingBehaviorAttribute(HttpBindingBehavior behavior)
		{
			this.Behavior = behavior;
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0001743A File Offset: 0x0001563A
		// (set) Token: 0x0600070A RID: 1802 RVA: 0x00017442 File Offset: 0x00015642
		public HttpBindingBehavior Behavior { get; private set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0001744B File Offset: 0x0001564B
		public override object TypeId
		{
			get
			{
				return HttpBindingBehaviorAttribute._typeId;
			}
		}

		// Token: 0x04000201 RID: 513
		private static readonly object _typeId = new object();
	}
}
