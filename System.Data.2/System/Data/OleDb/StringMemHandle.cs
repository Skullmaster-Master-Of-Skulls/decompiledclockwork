using System;
using System.Data.ProviderBase;

namespace System.Data.OleDb
{
	// Token: 0x02000286 RID: 646
	internal sealed class StringMemHandle : DbBuffer
	{
		// Token: 0x06002706 RID: 9990 RVA: 0x0010866C File Offset: 0x00107A6C
		internal StringMemHandle(string value) : base((value != null) ? checked(2 + 2 * value.Length) : 0)
		{
			if (value != null)
			{
				base.WriteCharArray(0, value.ToCharArray(), 0, value.Length);
			}
		}
	}
}
