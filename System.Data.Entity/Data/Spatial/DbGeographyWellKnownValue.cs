using System;
using System.Runtime.Serialization;

namespace System.Data.Spatial
{
	// Token: 0x020002D7 RID: 727
	[DataContract]
	public sealed class DbGeographyWellKnownValue
	{
		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06002AEA RID: 10986 RVA: 0x000A79A9 File Offset: 0x000A5BA9
		// (set) Token: 0x06002AEB RID: 10987 RVA: 0x000A79B1 File Offset: 0x000A5BB1
		[DataMember(Order = 1, IsRequired = false, EmitDefaultValue = false)]
		public int CoordinateSystemId { get; set; }

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06002AEC RID: 10988 RVA: 0x000A79BA File Offset: 0x000A5BBA
		// (set) Token: 0x06002AED RID: 10989 RVA: 0x000A79C2 File Offset: 0x000A5BC2
		[DataMember(Order = 2, IsRequired = false, EmitDefaultValue = false)]
		public string WellKnownText { get; set; }

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06002AEE RID: 10990 RVA: 0x000A79CB File Offset: 0x000A5BCB
		// (set) Token: 0x06002AEF RID: 10991 RVA: 0x000A79D3 File Offset: 0x000A5BD3
		[DataMember(Order = 3, IsRequired = false, EmitDefaultValue = false)]
		public byte[] WellKnownBinary { get; set; }
	}
}
