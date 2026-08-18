using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace System.Data.Entity.Spatial
{
	// Token: 0x0200071A RID: 1818
	[DataContract]
	public sealed class DbGeographyWellKnownValue
	{
		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x060049B2 RID: 18866 RVA: 0x0015FD6E File Offset: 0x0015DF6E
		// (set) Token: 0x060049B3 RID: 18867 RVA: 0x0015FD76 File Offset: 0x0015DF76
		[DataMember(Order = 1, IsRequired = false, EmitDefaultValue = false)]
		public int CoordinateSystemId { get; set; }

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x060049B4 RID: 18868 RVA: 0x0015FD7F File Offset: 0x0015DF7F
		// (set) Token: 0x060049B5 RID: 18869 RVA: 0x0015FD87 File Offset: 0x0015DF87
		[DataMember(Order = 2, IsRequired = false, EmitDefaultValue = false)]
		public string WellKnownText { get; set; }

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x060049B6 RID: 18870 RVA: 0x0015FD90 File Offset: 0x0015DF90
		// (set) Token: 0x060049B7 RID: 18871 RVA: 0x0015FD98 File Offset: 0x0015DF98
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Required for this feature")]
		[DataMember(Order = 3, IsRequired = false, EmitDefaultValue = false)]
		public byte[] WellKnownBinary { get; set; }
	}
}
