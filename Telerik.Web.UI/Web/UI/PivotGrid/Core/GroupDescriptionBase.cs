using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x020006F2 RID: 1778
	[DataContract]
	public abstract class GroupDescriptionBase : DescriptionBase, IGroupDescription, IDescriptionBase, ISortableGroupDescription, IEditable, INamed, IDescriptionsReferencing, IGrandTotalSupport
	{
		// Token: 0x06003F35 RID: 16181 RVA: 0x000C8DFC File Offset: 0x000C6FFC
		internal GroupDescriptionBase()
		{
			this.Initialize();
		}

		// Token: 0x170014A0 RID: 5280
		// (get) Token: 0x06003F36 RID: 16182 RVA: 0x000C8E0A File Offset: 0x000C700A
		bool IGrandTotalSupport.SupportsGrandTotal
		{
			get
			{
				return this.GetSupportsGrandTotal();
			}
		}

		// Token: 0x170014A1 RID: 5281
		// (get) Token: 0x06003F37 RID: 16183 RVA: 0x000C8E12 File Offset: 0x000C7012
		// (set) Token: 0x06003F38 RID: 16184 RVA: 0x000C8E1A File Offset: 0x000C701A
		[DataMember]
		public GroupComparer GroupComparer
		{
			get
			{
				return this.groupComparer;
			}
			set
			{
				if (this.groupComparer != value)
				{
					base.ChangeSettingsProperty<GroupComparer>(ref this.groupComparer, value);
					base.OnPropertyChanged("GroupComparer");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x170014A2 RID: 5282
		// (get) Token: 0x06003F39 RID: 16185 RVA: 0x000C8E48 File Offset: 0x000C7048
		// (set) Token: 0x06003F3A RID: 16186 RVA: 0x000C8E50 File Offset: 0x000C7050
		[DataMember]
		public SortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
			set
			{
				if (this.sortOrder != value)
				{
					this.sortOrder = value;
					base.OnPropertyChanged("SortOrder");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x000C8E78 File Offset: 0x000C7078
		protected override void CloneCore(Cloneable source)
		{
			base.CloneCore(source);
			GroupDescriptionBase groupDescriptionBase = source as GroupDescriptionBase;
			if (groupDescriptionBase != null)
			{
				this.SortOrder = groupDescriptionBase.SortOrder;
				this.groupComparer = Cloneable.CloneOrDefault<GroupComparer>(groupDescriptionBase.GroupComparer);
			}
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x000C8EB3 File Offset: 0x000C70B3
		internal virtual bool TrackDescriptions(IDescriptionIndexMap map)
		{
			this.GroupComparer = DescriptionReferencingUtilities.TrackReferencesOrNull<GroupComparer>(this.GroupComparer, map);
			return true;
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x000C8EC8 File Offset: 0x000C70C8
		bool IDescriptionsReferencing.TrackDescriptions(IDescriptionIndexMap map)
		{
			return this.TrackDescriptions(map);
		}

		// Token: 0x06003F3E RID: 16190 RVA: 0x000C8ED1 File Offset: 0x000C70D1
		internal virtual bool GetSupportsGrandTotal()
		{
			return true;
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x000C8ED4 File Offset: 0x000C70D4
		private void Initialize()
		{
			this.groupComparer = new GroupNameComparer();
		}

		// Token: 0x040010BF RID: 4287
		private SortOrder sortOrder;

		// Token: 0x040010C0 RID: 4288
		private GroupComparer groupComparer;
	}
}
