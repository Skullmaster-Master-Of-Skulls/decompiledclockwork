using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Telerik.Web.Data
{
	// Token: 0x02001B9C RID: 7068
	public class Group : IGroup
	{
		// Token: 0x17005380 RID: 21376
		// (get) Token: 0x060111AD RID: 70061 RVA: 0x003C5D75 File Offset: 0x003C3F75
		// (set) Token: 0x060111AE RID: 70062 RVA: 0x003C5D7D File Offset: 0x003C3F7D
		public bool HasSubgroups { get; set; }

		// Token: 0x17005381 RID: 21377
		// (get) Token: 0x060111AF RID: 70063 RVA: 0x003C5D86 File Offset: 0x003C3F86
		// (set) Token: 0x060111B0 RID: 70064 RVA: 0x003C5D8E File Offset: 0x003C3F8E
		public int ItemCount { get; set; }

		// Token: 0x17005382 RID: 21378
		// (get) Token: 0x060111B1 RID: 70065 RVA: 0x003C5D97 File Offset: 0x003C3F97
		public ReadOnlyCollection<IGroup> Subgroups
		{
			get
			{
				if (this.subgroups == null)
				{
					this.InitializeSubgroups();
				}
				return this.subgroups;
			}
		}

		// Token: 0x060111B2 RID: 70066 RVA: 0x003C5DB0 File Offset: 0x003C3FB0
		private void InitializeSubgroups()
		{
			List<IGroup> list = new List<IGroup>();
			if (this.HasSubgroups)
			{
				foreach (object obj in this.Items)
				{
					Group group = (Group)obj;
					group.ParentGroup = this;
					list.Add(group);
				}
			}
			this.subgroups = new ReadOnlyCollection<IGroup>(list);
		}

		// Token: 0x17005383 RID: 21379
		// (get) Token: 0x060111B3 RID: 70067 RVA: 0x003C5E2C File Offset: 0x003C402C
		// (set) Token: 0x060111B4 RID: 70068 RVA: 0x003C5E34 File Offset: 0x003C4034
		public IEnumerable Items { get; set; }

		// Token: 0x17005384 RID: 21380
		// (get) Token: 0x060111B5 RID: 70069 RVA: 0x003C5E3D File Offset: 0x003C403D
		// (set) Token: 0x060111B6 RID: 70070 RVA: 0x003C5E45 File Offset: 0x003C4045
		public object Key { get; set; }

		// Token: 0x17005385 RID: 21381
		// (get) Token: 0x060111B7 RID: 70071 RVA: 0x003C5E4E File Offset: 0x003C404E
		// (set) Token: 0x060111B8 RID: 70072 RVA: 0x003C5E56 File Offset: 0x003C4056
		internal Group ParentGroup { get; set; }

		// Token: 0x060111B9 RID: 70073 RVA: 0x003C5E60 File Offset: 0x003C4060
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "[Group: Key={0}; ItemCount={1}; HasSubgroups={2}; ParentGroup={3}];", new object[]
			{
				this.Key,
				this.ItemCount,
				this.HasSubgroups,
				(this.ParentGroup != null) ? this.ParentGroup.ToString() : "null"
			});
		}

		// Token: 0x04004C98 RID: 19608
		private ReadOnlyCollection<IGroup> subgroups;
	}
}
