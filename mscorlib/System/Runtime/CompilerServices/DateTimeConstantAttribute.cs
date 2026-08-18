using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020005E1 RID: 1505
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class DateTimeConstantAttribute : CustomConstantAttribute
	{
		// Token: 0x060037DE RID: 14302 RVA: 0x000BBBDC File Offset: 0x000BABDC
		public DateTimeConstantAttribute(long ticks)
		{
			this.date = new DateTime(ticks);
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x060037DF RID: 14303 RVA: 0x000BBBF0 File Offset: 0x000BABF0
		public override object Value
		{
			get
			{
				return this.date;
			}
		}

		// Token: 0x04001CE7 RID: 7399
		private DateTime date;
	}
}
