using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace System.Data.Entity.Spatial
{
	// Token: 0x0200071C RID: 1820
	[DataContract]
	public sealed class DbGeometryWellKnownValue
	{
		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06004A04 RID: 18948 RVA: 0x001603E2 File Offset: 0x0015E5E2
		// (set) Token: 0x06004A05 RID: 18949 RVA: 0x001603EA File Offset: 0x0015E5EA
		[DataMember(Order = 1, IsRequired = false, EmitDefaultValue = false)]
		public int CoordinateSystemId { get; set; }

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06004A06 RID: 18950 RVA: 0x001603F3 File Offset: 0x0015E5F3
		// (set) Token: 0x06004A07 RID: 18951 RVA: 0x001603FB File Offset: 0x0015E5FB
		[DataMember(Order = 2, IsRequired = false, EmitDefaultValue = false)]
		public string WellKnownText { get; set; }

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06004A08 RID: 18952 RVA: 0x00160404 File Offset: 0x0015E604
		// (set) Token: 0x06004A09 RID: 18953 RVA: 0x0016040C File Offset: 0x0015E60C
		[DataMember(Order = 3, IsRequired = false, EmitDefaultValue = false)]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Required for this feature")]
		public byte[] WellKnownBinary { get; set; }
	}
}
