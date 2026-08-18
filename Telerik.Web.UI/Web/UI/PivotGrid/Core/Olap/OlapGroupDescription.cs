using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x020006F5 RID: 1781
	[DataContract]
	public abstract class OlapGroupDescription : OlapGroupDescriptionBase, IHierarchyGroupDescription
	{
		// Token: 0x170014AB RID: 5291
		// (get) Token: 0x06003F62 RID: 16226 RVA: 0x000C920A File Offset: 0x000C740A
		IEnumerable<IGroupDescription> IHierarchyGroupDescription.Levels
		{
			get
			{
				return this.Levels.OfType<IGroupDescription>();
			}
		}

		// Token: 0x170014AC RID: 5292
		// (get) Token: 0x06003F63 RID: 16227 RVA: 0x000C9217 File Offset: 0x000C7417
		int IHierarchyGroupDescription.LevelsCount
		{
			get
			{
				return this.Levels.Count;
			}
		}

		// Token: 0x170014AD RID: 5293
		// (get) Token: 0x06003F64 RID: 16228 RVA: 0x000C9224 File Offset: 0x000C7424
		bool IHierarchyGroupDescription.IgnoreChildren
		{
			get
			{
				return base.FieldInfo != null && !base.FieldInfo.IsUserHierarchy;
			}
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x000C923E File Offset: 0x000C743E
		internal OlapGroupDescription()
		{
		}

		// Token: 0x170014AE RID: 5294
		// (get) Token: 0x06003F66 RID: 16230 RVA: 0x000C9246 File Offset: 0x000C7446
		[DataMember]
		public Collection<OlapLevelGroupDescription> Levels
		{
			get
			{
				if (this.levels == null)
				{
					this.levels = new Collection<OlapLevelGroupDescription>();
				}
				return this.levels;
			}
		}

		// Token: 0x06003F67 RID: 16231
		internal abstract OlapLevelGroupDescription CreateLevelGroupDescription(OlapHierarchyFieldInfo fieldInfo);

		// Token: 0x06003F68 RID: 16232 RVA: 0x000C9264 File Offset: 0x000C7464
		protected override void CloneCore(Cloneable source)
		{
			OlapGroupDescription olapGroupDescription = source as OlapGroupDescription;
			if (olapGroupDescription != null)
			{
				this.CloneChildDescriptions(olapGroupDescription);
			}
			base.CloneCore(source);
		}

		// Token: 0x06003F69 RID: 16233 RVA: 0x000C928C File Offset: 0x000C748C
		private void CloneChildDescriptions(OlapGroupDescription source)
		{
			foreach (OlapLevelGroupDescription olapLevelGroupDescription in source.Levels)
			{
				this.Levels.Add(olapLevelGroupDescription.Clone() as OlapLevelGroupDescription);
			}
		}

		// Token: 0x06003F6A RID: 16234 RVA: 0x000C92E8 File Offset: 0x000C74E8
		internal override void InitializeFromFieldInfo(OlapHierarchyFieldInfo fieldInfo)
		{
			base.InitializeFromFieldInfo(fieldInfo);
			if (base.FieldInfo == null)
			{
				return;
			}
			this.GenerateChildDescriptions();
		}

		// Token: 0x06003F6B RID: 16235 RVA: 0x000C9300 File Offset: 0x000C7500
		private static OlapLevelGroupDescription FindLevelDescription(IEnumerable<OlapLevelGroupDescription> descriptions, string memberName)
		{
			foreach (OlapLevelGroupDescription olapLevelGroupDescription in descriptions)
			{
				if (olapLevelGroupDescription.MemberName == memberName)
				{
					return olapLevelGroupDescription;
				}
			}
			return null;
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x000C9358 File Offset: 0x000C7558
		private void GenerateChildDescriptions()
		{
			if (base.FieldInfo.Levels.Count == 0)
			{
				return;
			}
			List<OlapLevelGroupDescription> descriptions = this.Levels.ToList<OlapLevelGroupDescription>();
			this.Levels.Clear();
			foreach (OlapHierarchyFieldInfo olapHierarchyFieldInfo in base.FieldInfo.Levels)
			{
				OlapLevelGroupDescription olapLevelGroupDescription = OlapGroupDescription.FindLevelDescription(descriptions, olapHierarchyFieldInfo.Name);
				if (olapLevelGroupDescription == null)
				{
					olapLevelGroupDescription = this.CreateLevelGroupDescription(olapHierarchyFieldInfo);
				}
				olapLevelGroupDescription.FieldInfo = olapHierarchyFieldInfo;
				olapLevelGroupDescription.MemberName = olapHierarchyFieldInfo.Name;
				olapLevelGroupDescription.Provider = base.Provider;
				base.AddSettingsChild(olapLevelGroupDescription);
				this.Levels.Add(olapLevelGroupDescription);
			}
			if (!string.IsNullOrEmpty(base.FieldInfo.AllMemberName))
			{
				this.Levels.RemoveAt(0);
			}
		}

		// Token: 0x06003F6D RID: 16237 RVA: 0x000C9438 File Offset: 0x000C7638
		internal override bool GetSupportsGrandTotal()
		{
			return base.FieldInfo != null && !string.IsNullOrEmpty(base.FieldInfo.AllMemberName);
		}

		// Token: 0x040010C6 RID: 4294
		private Collection<OlapLevelGroupDescription> levels;
	}
}
