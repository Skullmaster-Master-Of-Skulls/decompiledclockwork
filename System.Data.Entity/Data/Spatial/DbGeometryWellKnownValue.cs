using System;
using System.Runtime.Serialization;

namespace System.Data.Spatial
{
	// Token: 0x020002D9 RID: 729
	[DataContract]
	public sealed class DbGeometryWellKnownValue
	{
		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06002B3A RID: 11066 RVA: 0x000A7FD4 File Offset: 0x000A61D4
		// (set) Token: 0x06002B3B RID: 11067 RVA: 0x000A7FDC File Offset: 0x000A61DC
		[DataMember(Order = 1, IsRequired = false, EmitDefaultValue = false)]
		public int CoordinateSystemId { get; set; }

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06002B3C RID: 11068 RVA: 0x000A7FE5 File Offset: 0x000A61E5
		// (set) Token: 0x06002B3D RID: 11069 RVA: 0x000A7FED File Offset: 0x000A61ED
		[DataMember(Order = 2, IsRequired = false, EmitDefaultValue = false)]
		public string WellKnownText { get; set; }

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x000A7FF6 File Offset: 0x000A61F6
		// (set) Token: 0x06002B3F RID: 11071 RVA: 0x000A7FFE File Offset: 0x000A61FE
		[DataMember(Order = 3, IsRequired = false, EmitDefaultValue = false)]
		public byte[] WellKnownBinary { get; set; }
	}
}
