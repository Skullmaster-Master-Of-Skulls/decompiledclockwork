using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.UI.Web.Entity.Adapters;

namespace TechnoPro.Common.UI.Web.Entity.People
{
	// Token: 0x02000029 RID: 41
	public class PersonBaseView : WrapperBase<PersonBaseDTO>
	{
		// Token: 0x060000FE RID: 254 RVA: 0x00002CDA File Offset: 0x00000EDA
		public PersonBaseView(PersonBaseDTO item) : base(item)
		{
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00002CE8 File Offset: 0x00000EE8
		public int PersonId
		{
			get
			{
				return (base.Item != null) ? base.Item.PersonId : 0;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00002D10 File Offset: 0x00000F10
		public string FirstName
		{
			get
			{
				return (base.Item != null) ? (base.Item.FirstName ?? string.Empty) : string.Empty;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00002D48 File Offset: 0x00000F48
		public string LastName
		{
			get
			{
				return (base.Item != null) ? (base.Item.LastName ?? string.Empty) : string.Empty;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00002D80 File Offset: 0x00000F80
		public string MiddleName
		{
			get
			{
				return (base.Item != null) ? (base.Item.MiddleName ?? string.Empty) : string.Empty;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00002DB8 File Offset: 0x00000FB8
		public string Student_no
		{
			get
			{
				return (base.Item != null) ? (base.Item.Student_no ?? string.Empty) : string.Empty;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public eCoreGroup? CoreGroup
		{
			get
			{
				return (base.Item != null) ? new eCoreGroup?((eCoreGroup)base.Item.CoreGroup) : null;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00002E28 File Offset: 0x00001028
		public string DisplayName
		{
			get
			{
				return this.GetName();
			}
		}
	}
}
