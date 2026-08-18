using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DataContracts
{
	// Token: 0x020000DD RID: 221
	[DataContract(Namespace = "http://tpro.ca")]
	public class CWDbParameter
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x000026D3 File Offset: 0x000008D3
		// (set) Token: 0x060005D7 RID: 1495 RVA: 0x000026DB File Offset: 0x000008DB
		[DataMember]
		public string ParameterName { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x000026E4 File Offset: 0x000008E4
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x000026EC File Offset: 0x000008EC
		[DataMember]
		public CWDbType DbType { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x000026F5 File Offset: 0x000008F5
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x000026FD File Offset: 0x000008FD
		[DataMember]
		public object Value { get; set; }
	}
}
