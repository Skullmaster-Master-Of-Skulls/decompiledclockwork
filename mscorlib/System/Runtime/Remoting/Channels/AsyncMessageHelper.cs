using System;
using System.Reflection;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006BC RID: 1724
	internal static class AsyncMessageHelper
	{
		// Token: 0x06003E13 RID: 15891 RVA: 0x000D4364 File Offset: 0x000D3364
		internal static void GetOutArgs(ParameterInfo[] syncParams, object[] syncArgs, object[] endArgs)
		{
			int num = 0;
			for (int i = 0; i < syncParams.Length; i++)
			{
				if (syncParams[i].IsOut || syncParams[i].ParameterType.IsByRef)
				{
					endArgs[num++] = syncArgs[i];
				}
			}
		}
	}
}
