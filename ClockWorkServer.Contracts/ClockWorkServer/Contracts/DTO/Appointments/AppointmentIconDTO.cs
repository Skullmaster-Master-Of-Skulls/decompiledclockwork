using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments
{
	// Token: 0x0200092A RID: 2346
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppointmentIconDTO : ICloneable<AppointmentIconDTO>, ICloneable
	{
		// Token: 0x06002F96 RID: 12182 RVA: 0x000036BD File Offset: 0x000018BD
		public AppointmentIconDTO()
		{
		}

		// Token: 0x06002F97 RID: 12183 RVA: 0x00016BA4 File Offset: 0x00014DA4
		public AppointmentIconDTO(AppointmentIconDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.AppointmentIconId = item.AppointmentIconId;
				this.Icon = ((item.Icon == null) ? null : item.Icon.Clone());
				this.Screen = ((item.Screen == null) ? null : item.Screen.Clone());
			}
		}

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x06002F98 RID: 12184 RVA: 0x00016C0A File Offset: 0x00014E0A
		// (set) Token: 0x06002F99 RID: 12185 RVA: 0x00016C12 File Offset: 0x00014E12
		[DataMember]
		public int AppointmentIconId { get; set; }

		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x06002F9A RID: 12186 RVA: 0x00016C1B File Offset: 0x00014E1B
		// (set) Token: 0x06002F9B RID: 12187 RVA: 0x00016C23 File Offset: 0x00014E23
		[DataMember]
		public IconInfoDTO Icon { get; set; }

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x06002F9C RID: 12188 RVA: 0x00016C2C File Offset: 0x00014E2C
		// (set) Token: 0x06002F9D RID: 12189 RVA: 0x00016C34 File Offset: 0x00014E34
		[DataMember]
		public DynamicFormBaseDTO Screen { get; set; }

		// Token: 0x06002F9E RID: 12190 RVA: 0x00016C40 File Offset: 0x00014E40
		public AppointmentIconDTO Clone()
		{
			return new AppointmentIconDTO(this);
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x00016C58 File Offset: 0x00014E58
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
