using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping.ViewGeneration;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003D9 RID: 985
	internal struct InputForComputingCellGroups : IEquatable<InputForComputingCellGroups>, IEqualityComparer<InputForComputingCellGroups>
	{
		// Token: 0x0600240F RID: 9231 RVA: 0x000A6560 File Offset: 0x000A4760
		internal InputForComputingCellGroups(EntityContainerMapping containerMapping, ConfigViewGenerator config)
		{
			this.ContainerMapping = containerMapping;
			this.Config = config;
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x000A6570 File Offset: 0x000A4770
		public bool Equals(InputForComputingCellGroups other)
		{
			return this.ContainerMapping.Equals(other.ContainerMapping) && this.Config.Equals(other.Config);
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x000A659A File Offset: 0x000A479A
		public bool Equals(InputForComputingCellGroups one, InputForComputingCellGroups two)
		{
			return object.ReferenceEquals(one, two) || (!object.ReferenceEquals(one, null) && !object.ReferenceEquals(two, null) && one.Equals(two));
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x000A65D7 File Offset: 0x000A47D7
		public int GetHashCode(InputForComputingCellGroups value)
		{
			return value.GetHashCode();
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x000A65E6 File Offset: 0x000A47E6
		public override int GetHashCode()
		{
			return this.ContainerMapping.GetHashCode();
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x000A65F3 File Offset: 0x000A47F3
		public override bool Equals(object obj)
		{
			return obj is InputForComputingCellGroups && this.Equals((InputForComputingCellGroups)obj);
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x000A660B File Offset: 0x000A480B
		public static bool operator ==(InputForComputingCellGroups input1, InputForComputingCellGroups input2)
		{
			return object.ReferenceEquals(input1, input2) || input1.Equals(input2);
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x000A662A File Offset: 0x000A482A
		public static bool operator !=(InputForComputingCellGroups input1, InputForComputingCellGroups input2)
		{
			return !(input1 == input2);
		}

		// Token: 0x04000CA6 RID: 3238
		internal readonly EntityContainerMapping ContainerMapping;

		// Token: 0x04000CA7 RID: 3239
		internal readonly ConfigViewGenerator Config;
	}
}
