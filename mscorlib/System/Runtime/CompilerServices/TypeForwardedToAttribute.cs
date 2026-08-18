using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000609 RID: 1545
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	public sealed class TypeForwardedToAttribute : Attribute
	{
		// Token: 0x06003807 RID: 14343 RVA: 0x000BBDE1 File Offset: 0x000BADE1
		public TypeForwardedToAttribute(Type destination)
		{
			this._destination = destination;
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06003808 RID: 14344 RVA: 0x000BBDF0 File Offset: 0x000BADF0
		public Type Destination
		{
			get
			{
				return this._destination;
			}
		}

		// Token: 0x04001D08 RID: 7432
		private Type _destination;
	}
}
