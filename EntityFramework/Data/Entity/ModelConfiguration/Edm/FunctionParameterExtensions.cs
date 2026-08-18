using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x020002C7 RID: 711
	internal static class FunctionParameterExtensions
	{
		// Token: 0x06001935 RID: 6453 RVA: 0x0007CD0D File Offset: 0x0007AF0D
		public static object GetConfiguration(this FunctionParameter functionParameter)
		{
			return functionParameter.Annotations.GetConfiguration();
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x0007CD1A File Offset: 0x0007AF1A
		public static void SetConfiguration(this FunctionParameter functionParameter, object configuration)
		{
			functionParameter.GetMetadataProperties().SetConfiguration(configuration);
		}
	}
}
