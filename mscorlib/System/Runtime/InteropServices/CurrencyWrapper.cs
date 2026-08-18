using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200051E RID: 1310
	[ComVisible(true)]
	[Serializable]
	public sealed class CurrencyWrapper
	{
		// Token: 0x060032DA RID: 13018 RVA: 0x000ABB57 File Offset: 0x000AAB57
		public CurrencyWrapper(decimal obj)
		{
			this.m_WrappedObject = obj;
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x000ABB66 File Offset: 0x000AAB66
		public CurrencyWrapper(object obj)
		{
			if (!(obj is decimal))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDecimal"), "obj");
			}
			this.m_WrappedObject = (decimal)obj;
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x060032DC RID: 13020 RVA: 0x000ABB97 File Offset: 0x000AAB97
		public decimal WrappedObject
		{
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x040019FA RID: 6650
		private decimal m_WrappedObject;
	}
}
