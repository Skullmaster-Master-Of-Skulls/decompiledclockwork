using System;
using System.Reflection;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls
{
	// Token: 0x02000776 RID: 1910
	[DataContract(Namespace = "http://tpro.ca")]
	[CustomControlBase(eCustomControlType.Unknown)]
	public class CustomControlBaseDTO
	{
		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06002740 RID: 10048 RVA: 0x000125F6 File Offset: 0x000107F6
		// (set) Token: 0x06002741 RID: 10049 RVA: 0x000125FE File Offset: 0x000107FE
		[DataMember]
		public string ControlId { get; set; }

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06002742 RID: 10050 RVA: 0x00012607 File Offset: 0x00010807
		// (set) Token: 0x06002743 RID: 10051 RVA: 0x0001260F File Offset: 0x0001080F
		[DataMember]
		public Guid FormId { get; set; }

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06002744 RID: 10052 RVA: 0x00012618 File Offset: 0x00010818
		// (set) Token: 0x06002745 RID: 10053 RVA: 0x00012620 File Offset: 0x00010820
		[DataMember]
		public string Caption { get; set; }

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06002746 RID: 10054 RVA: 0x00012629 File Offset: 0x00010829
		[DataMember]
		public eCustomControlType CustomControlType
		{
			get
			{
				CustomControlBaseAttribute customAttribute = base.GetType().GetCustomAttribute<CustomControlBaseAttribute>();
				return (customAttribute != null) ? customAttribute.ControlType : eCustomControlType.Unknown;
			}
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06002747 RID: 10055 RVA: 0x00012642 File Offset: 0x00010842
		// (set) Token: 0x06002748 RID: 10056 RVA: 0x0001264A File Offset: 0x0001084A
		[DataMember]
		public bool IsReadOnly { get; set; }
	}
}
