using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.Common.UI.Web.Entity.appt
{
	// Token: 0x02000046 RID: 70
	[Serializable]
	public class AttendeeView : WrapperBase<AttendeeDTO>
	{
		// Token: 0x060001CF RID: 463 RVA: 0x00003F62 File Offset: 0x00002162
		public AttendeeView()
		{
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00003F6C File Offset: 0x0000216C
		public AttendeeView(AttendeeDTO dto) : base(dto)
		{
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00003F77 File Offset: 0x00002177
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00003F7F File Offset: 0x0000217F
		public int ColourArgB { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00003F88 File Offset: 0x00002188
		public int PersonId
		{
			get
			{
				return (base.Item == null || base.Item.Person == null) ? 0 : base.Item.Person.PersonId;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00003FC4 File Offset: 0x000021C4
		public string Name
		{
			get
			{
				return (base.Item == null || base.Item.Person == null) ? "" : base.Item.Person.GetName();
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00004004 File Offset: 0x00002204
		public string ID
		{
			get
			{
				return this.PersonId.ToString();
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00004024 File Offset: 0x00002224
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x0000402C File Offset: 0x0000222C
		public IList<int> PersonIds { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00004038 File Offset: 0x00002238
		public bool IsStudentRoomOrResource
		{
			get
			{
				bool flag = base.Item == null || base.Item.Person == null;
				bool result;
				if (flag)
				{
					result = false;
				}
				else
				{
					eCoreGroupDTO coreGroup = base.Item.Person.CoreGroup;
					bool flag2 = coreGroup == eCoreGroupDTO.Students || coreGroup == eCoreGroupDTO.Rooms || coreGroup == eCoreGroupDTO.Resources;
					if (flag2)
					{
						result = true;
					}
					else
					{
						bool flag3 = base.Item.Person.Groups == null;
						if (flag3)
						{
							result = false;
						}
						else
						{
							int[] gids = new int[]
							{
								1,
								3,
								4
							};
							result = (base.Item.Person.Groups.FirstOrDefault((GroupDTO g) => gids.Contains(g.GroupId)) != null);
						}
					}
				}
				return result;
			}
		}
	}
}
