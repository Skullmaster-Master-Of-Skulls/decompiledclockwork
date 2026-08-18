using System;
using System.Collections.Generic;
using System.Data.Mapping.ViewGeneration;

namespace System.Data.Mapping
{
	// Token: 0x02000240 RID: 576
	internal struct InputForComputingCellGroups : IEquatable<InputForComputingCellGroups>, IEqualityComparer<InputForComputingCellGroups>
	{
		// Token: 0x0600245E RID: 9310 RVA: 0x00083953 File Offset: 0x00081B53
		internal InputForComputingCellGroups(StorageEntityContainerMapping containerMapping, ConfigViewGenerator config)
		{
			this.ContainerMapping = containerMapping;
			this.Config = config;
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x00083963 File Offset: 0x00081B63
		public bool Equals(InputForComputingCellGroups other)
		{
			return this.ContainerMapping.Equals(other.ContainerMapping) && this.Config.Equals(other.Config);
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x0008398B File Offset: 0x00081B8B
		public bool Equals(InputForComputingCellGroups one, InputForComputingCellGroups two)
		{
			return one == two || (one != null && two != null && one.Equals(two));
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000839B7 File Offset: 0x00081BB7
		public int GetHashCode(InputForComputingCellGroups value)
		{
			return value.GetHashCode();
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x000839C6 File Offset: 0x00081BC6
		public override int GetHashCode()
		{
			return this.ContainerMapping.GetHashCode();
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x000839D3 File Offset: 0x00081BD3
		public override bool Equals(object obj)
		{
			return obj is InputForComputingCellGroups && this.Equals((InputForComputingCellGroups)obj);
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x000839EB File Offset: 0x00081BEB
		public static bool operator ==(InputForComputingCellGroups input1, InputForComputingCellGroups input2)
		{
			return input1 == input2 || input1.Equals(input2);
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x00083A05 File Offset: 0x00081C05
		public static bool operator !=(InputForComputingCellGroups input1, InputForComputingCellGroups input2)
		{
			return !(input1 == input2);
		}

		// Token: 0x04001019 RID: 4121
		internal readonly StorageEntityContainerMapping ContainerMapping;

		// Token: 0x0400101A RID: 4122
		internal readonly ConfigViewGenerator Config;
	}
}
