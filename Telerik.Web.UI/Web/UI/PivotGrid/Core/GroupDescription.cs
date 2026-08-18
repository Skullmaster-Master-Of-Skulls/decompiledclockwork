using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x020006F3 RID: 1779
	[DataContract]
	public abstract class GroupDescription : GroupDescriptionBase
	{
		// Token: 0x170014A3 RID: 5283
		// (get) Token: 0x06003F40 RID: 16192 RVA: 0x000C8EE1 File Offset: 0x000C70E1
		// (set) Token: 0x06003F41 RID: 16193 RVA: 0x000C8EE9 File Offset: 0x000C70E9
		[DataMember]
		public bool ShowGroupsWithNoData
		{
			get
			{
				return this.showGroupsWithNoData;
			}
			set
			{
				if (this.showGroupsWithNoData != value)
				{
					this.showGroupsWithNoData = value;
					base.OnPropertyChanged("ShowGroupsWithNoData");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x170014A4 RID: 5284
		// (get) Token: 0x06003F42 RID: 16194 RVA: 0x000C8F11 File Offset: 0x000C7111
		// (set) Token: 0x06003F43 RID: 16195 RVA: 0x000C8F19 File Offset: 0x000C7119
		[DataMember]
		public GroupFilter GroupFilter
		{
			get
			{
				return this.groupFilter;
			}
			set
			{
				if (this.groupFilter != value)
				{
					base.ChangeSettingsProperty<GroupFilter>(ref this.groupFilter, value);
					base.OnPropertyChanged("GroupFilter");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x170014A5 RID: 5285
		// (get) Token: 0x06003F44 RID: 16196 RVA: 0x000C8F47 File Offset: 0x000C7147
		// (set) Token: 0x06003F45 RID: 16197 RVA: 0x000C8F4F File Offset: 0x000C714F
		internal IDataProvider Provider { get; set; }

		// Token: 0x06003F46 RID: 16198 RVA: 0x000C8F58 File Offset: 0x000C7158
		protected internal virtual IEnumerable<object> GetAllNames(IEnumerable<object> uniqueNames, IEnumerable<object> parentGroupNames)
		{
			return uniqueNames;
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x000C8F5C File Offset: 0x000C715C
		protected override void CloneCore(Cloneable source)
		{
			GroupDescription groupDescription = source as GroupDescription;
			if (groupDescription != null)
			{
				this.showGroupsWithNoData = groupDescription.showGroupsWithNoData;
				this.groupFilter = Cloneable.CloneOrDefault<GroupFilter>(groupDescription.groupFilter);
			}
			base.CloneCore(source);
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x000C8F98 File Offset: 0x000C7198
		internal static IList<T> GetAllDescriptions<T>(IEnumerable<T> descriptions) where T : class, IGroupDescription
		{
			List<T> list = new List<T>();
			if (descriptions == null)
			{
				return list;
			}
			foreach (T t in descriptions)
			{
				IHierarchyGroupDescription hierarchyGroupDescription = t as IHierarchyGroupDescription;
				if (hierarchyGroupDescription == null || hierarchyGroupDescription.IgnoreChildren)
				{
					list.Add(t);
				}
				else
				{
					foreach (IGroupDescription groupDescription in hierarchyGroupDescription.Levels)
					{
						list.Add(groupDescription as T);
					}
				}
			}
			return list;
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x000C9058 File Offset: 0x000C7258
		internal override bool TrackDescriptions(IDescriptionIndexMap map)
		{
			base.TrackDescriptions(map);
			this.GroupFilter = DescriptionReferencingUtilities.TrackReferencesOrNull<GroupFilter>(this.GroupFilter, map);
			return true;
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x000C9075 File Offset: 0x000C7275
		internal virtual bool RequiresRefreshForDistinct()
		{
			return true;
		}

		// Token: 0x040010C1 RID: 4289
		private GroupFilter groupFilter;

		// Token: 0x040010C2 RID: 4290
		private bool showGroupsWithNoData;
	}
}
