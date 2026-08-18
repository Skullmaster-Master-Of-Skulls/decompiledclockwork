using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.ReportFilter;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x020006F0 RID: 1776
	[DataContract]
	public abstract class OlapFilterDescription : OlapFilterDescriptionBase, IHierarchyFilterDescription, IInitializeDescription
	{
		// Token: 0x06003F25 RID: 16165 RVA: 0x000C8AC4 File Offset: 0x000C6CC4
		internal OlapFilterDescription()
		{
		}

		// Token: 0x17001499 RID: 5273
		// (get) Token: 0x06003F26 RID: 16166 RVA: 0x000C8ACC File Offset: 0x000C6CCC
		[DataMember]
		public Collection<OlapLevelFilterDescription> Levels
		{
			get
			{
				if (this.levels == null)
				{
					this.levels = new Collection<OlapLevelFilterDescription>();
				}
				return this.levels;
			}
		}

		// Token: 0x06003F27 RID: 16167 RVA: 0x000C8AE8 File Offset: 0x000C6CE8
		protected override void CloneCore(Cloneable source)
		{
			OlapFilterDescription olapFilterDescription = source as OlapFilterDescription;
			if (olapFilterDescription != null)
			{
				this.CloneChildDescriptions(olapFilterDescription);
			}
			base.CloneCore(source);
		}

		// Token: 0x06003F28 RID: 16168 RVA: 0x000C8B10 File Offset: 0x000C6D10
		private void CloneChildDescriptions(OlapFilterDescription source)
		{
			foreach (OlapLevelFilterDescription olapLevelFilterDescription in source.Levels)
			{
				this.Levels.Add(olapLevelFilterDescription.Clone() as OlapLevelFilterDescription);
			}
		}

		// Token: 0x06003F29 RID: 16169 RVA: 0x000C8B6C File Offset: 0x000C6D6C
		internal static IList<OlapFilterDescriptionBase> GetAllDescriptions(IEnumerable<OlapFilterDescription> descriptions)
		{
			List<OlapFilterDescriptionBase> list = new List<OlapFilterDescriptionBase>();
			if (descriptions == null)
			{
				return list;
			}
			foreach (OlapFilterDescription olapFilterDescription in descriptions)
			{
				IHierarchyFilterDescription hierarchyFilterDescription = olapFilterDescription;
				if (hierarchyFilterDescription == null || hierarchyFilterDescription.IgnoreChildren)
				{
					list.Add(olapFilterDescription);
				}
				else
				{
					foreach (FilterDescription filterDescription in hierarchyFilterDescription.Levels)
					{
						OlapFilterDescriptionBase olapFilterDescriptionBase = filterDescription as OlapFilterDescriptionBase;
						if (olapFilterDescriptionBase != null)
						{
							list.Add(olapFilterDescriptionBase);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x1700149A RID: 5274
		// (get) Token: 0x06003F2A RID: 16170 RVA: 0x000C8C24 File Offset: 0x000C6E24
		IEnumerable<FilterDescription> IHierarchyFilterDescription.Levels
		{
			get
			{
				return this.Levels.OfType<FilterDescription>();
			}
		}

		// Token: 0x1700149B RID: 5275
		// (get) Token: 0x06003F2B RID: 16171 RVA: 0x000C8C31 File Offset: 0x000C6E31
		int IHierarchyFilterDescription.LevelsCount
		{
			get
			{
				return this.Levels.Count;
			}
		}

		// Token: 0x1700149C RID: 5276
		// (get) Token: 0x06003F2C RID: 16172 RVA: 0x000C8C3E File Offset: 0x000C6E3E
		bool IHierarchyFilterDescription.IgnoreChildren
		{
			get
			{
				return base.FieldInfo != null && !base.FieldInfo.IsUserHierarchy;
			}
		}

		// Token: 0x1700149D RID: 5277
		// (get) Token: 0x06003F2D RID: 16173 RVA: 0x000C8C58 File Offset: 0x000C6E58
		bool IInitializeDescription.Initialized
		{
			get
			{
				return base.FieldInfo != null;
			}
		}

		// Token: 0x06003F2E RID: 16174 RVA: 0x000C8C68 File Offset: 0x000C6E68
		void IInitializeDescription.Initialize(IDataProvider provider)
		{
			if (provider == null)
			{
				return;
			}
			base.Provider = (provider as OlapDataProvider);
			OlapHierarchyFieldInfo fieldInfo = provider.FieldInfos.GetFieldDescriptionByMember(base.MemberName) as OlapHierarchyFieldInfo;
			this.InitializeFromFieldInfo(fieldInfo);
		}

		// Token: 0x06003F2F RID: 16175 RVA: 0x000C8CA3 File Offset: 0x000C6EA3
		internal virtual void InitializeFromFieldInfo(OlapHierarchyFieldInfo fieldInfo)
		{
			if (fieldInfo == null)
			{
				return;
			}
			base.FieldInfo = fieldInfo;
			this.GenerateChildDescriptions();
		}

		// Token: 0x06003F30 RID: 16176 RVA: 0x000C8CB8 File Offset: 0x000C6EB8
		private static OlapLevelFilterDescription FindLevelDescription(IEnumerable<OlapLevelFilterDescription> descriptions, string memberName)
		{
			foreach (OlapLevelFilterDescription olapLevelFilterDescription in descriptions)
			{
				if (olapLevelFilterDescription.MemberName == memberName)
				{
					return olapLevelFilterDescription;
				}
			}
			return null;
		}

		// Token: 0x06003F31 RID: 16177 RVA: 0x000C8D10 File Offset: 0x000C6F10
		private void GenerateChildDescriptions()
		{
			if (base.FieldInfo.Levels.Count == 0)
			{
				return;
			}
			List<OlapLevelFilterDescription> descriptions = this.Levels.ToList<OlapLevelFilterDescription>();
			this.Levels.Clear();
			foreach (OlapHierarchyFieldInfo olapHierarchyFieldInfo in base.FieldInfo.Levels)
			{
				OlapLevelFilterDescription olapLevelFilterDescription = OlapFilterDescription.FindLevelDescription(descriptions, olapHierarchyFieldInfo.Name);
				if (olapLevelFilterDescription == null)
				{
					olapLevelFilterDescription = this.CreateFilterDescription(olapHierarchyFieldInfo);
				}
				olapLevelFilterDescription.FieldInfo = olapHierarchyFieldInfo;
				olapLevelFilterDescription.MemberName = olapHierarchyFieldInfo.Name;
				olapLevelFilterDescription.Provider = base.Provider;
				olapLevelFilterDescription.ParentInfo = base.FieldInfo;
				base.AddSettingsChild(olapLevelFilterDescription);
				this.Levels.Add(olapLevelFilterDescription);
			}
			if (!string.IsNullOrEmpty(base.FieldInfo.AllMemberName))
			{
				this.Levels.RemoveAt(0);
			}
		}

		// Token: 0x06003F32 RID: 16178
		internal abstract OlapLevelFilterDescription CreateFilterDescription(OlapHierarchyFieldInfo info);

		// Token: 0x040010BE RID: 4286
		private Collection<OlapLevelFilterDescription> levels;
	}
}
