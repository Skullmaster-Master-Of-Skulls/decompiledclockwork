using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x0200011E RID: 286
	internal class AlphaSortedEnumConverter : EnumConverter
	{
		// Token: 0x060008EB RID: 2283 RVA: 0x00018355 File Offset: 0x00016555
		public AlphaSortedEnumConverter(Type type) : base(type)
		{
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x0001835E File Offset: 0x0001655E
		protected override IComparer Comparer
		{
			get
			{
				return EnumValAlphaComparer.Default;
			}
		}
	}
}
