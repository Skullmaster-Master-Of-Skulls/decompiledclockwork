using System;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000637 RID: 1591
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(byte[]))]
	[KnownType(typeof(DynamicDataDTO))]
	public class DynamicDataDTO : ICloneable<DynamicDataDTO>, ICloneable
	{
		// Token: 0x0600206D RID: 8301 RVA: 0x000036BD File Offset: 0x000018BD
		public DynamicDataDTO()
		{
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x0000EB7C File Offset: 0x0000CD7C
		public DynamicDataDTO(DynamicDataDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.Value = item.Value;
				this.DataId = item.DataId;
				this.Field = ((item.Field == null) ? null : item.Field.Clone());
				this.ValueId = item.ValueId;
				this.SecondaryValue = item.SecondaryValue;
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x0600206F RID: 8303 RVA: 0x0000EBEC File Offset: 0x0000CDEC
		// (set) Token: 0x06002070 RID: 8304 RVA: 0x0000EBF4 File Offset: 0x0000CDF4
		[DataMember]
		public object Value { get; set; }

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06002071 RID: 8305 RVA: 0x0000EBFD File Offset: 0x0000CDFD
		// (set) Token: 0x06002072 RID: 8306 RVA: 0x0000EC05 File Offset: 0x0000CE05
		[DataMember]
		public int DataId { get; set; }

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06002073 RID: 8307 RVA: 0x0000EC0E File Offset: 0x0000CE0E
		// (set) Token: 0x06002074 RID: 8308 RVA: 0x0000EC16 File Offset: 0x0000CE16
		[DataMember]
		public DynamicFieldDTO Field { get; set; }

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06002075 RID: 8309 RVA: 0x0000EC1F File Offset: 0x0000CE1F
		// (set) Token: 0x06002076 RID: 8310 RVA: 0x0000EC27 File Offset: 0x0000CE27
		[DataMember]
		public int ValueId { get; set; }

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06002077 RID: 8311 RVA: 0x0000EC30 File Offset: 0x0000CE30
		// (set) Token: 0x06002078 RID: 8312 RVA: 0x0000EC38 File Offset: 0x0000CE38
		[DataMember]
		public object SecondaryValue { get; set; }

		// Token: 0x06002079 RID: 8313 RVA: 0x0000EC44 File Offset: 0x0000CE44
		public DynamicDataDTO Clone()
		{
			return new DynamicDataDTO(this);
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x0000EC5C File Offset: 0x0000CE5C
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
