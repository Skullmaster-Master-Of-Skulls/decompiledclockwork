using System;
using System.Data.ProviderBase;

namespace System.Data.OleDb
{
	// Token: 0x02000261 RID: 609
	internal sealed class StringMemHandle : DbBuffer
	{
		// Token: 0x060020C7 RID: 8391 RVA: 0x00282528 File Offset: 0x00281928
		internal StringMemHandle(string value) : base((value != null) ? checked(2 + 2 * value.Length) : 0)
		{
			if (value != null)
			{
				base.WriteCharArray(0, value.ToCharArray(), 0, value.Length);
			}
		}
	}
}
