using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200126F RID: 4719
	[Serializable]
	public class TreeListHierarchyIndex : IEquatable<TreeListHierarchyIndex>
	{
		// Token: 0x17003F46 RID: 16198
		// (get) Token: 0x0600C456 RID: 50262 RVA: 0x002BF231 File Offset: 0x002BD431
		// (set) Token: 0x0600C457 RID: 50263 RVA: 0x002BF239 File Offset: 0x002BD439
		public int NestedLevel { get; set; }

		// Token: 0x17003F47 RID: 16199
		// (get) Token: 0x0600C458 RID: 50264 RVA: 0x002BF242 File Offset: 0x002BD442
		// (set) Token: 0x0600C459 RID: 50265 RVA: 0x002BF24A File Offset: 0x002BD44A
		public int LevelIndex { get; set; }

		// Token: 0x0600C45A RID: 50266 RVA: 0x002BF254 File Offset: 0x002BD454
		public bool Equals(TreeListHierarchyIndex other)
		{
			return !object.ReferenceEquals(null, other) && (object.ReferenceEquals(this, other) || (object.Equals(other.NestedLevel, this.NestedLevel) && object.Equals(other.LevelIndex, this.LevelIndex)));
		}

		// Token: 0x0600C45B RID: 50267 RVA: 0x002BF2B1 File Offset: 0x002BD4B1
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (!(obj.GetType() != typeof(TreeListHierarchyIndex)) && this.Equals((TreeListHierarchyIndex)obj)));
		}

		// Token: 0x0600C45C RID: 50268 RVA: 0x002BF2F0 File Offset: 0x002BD4F0
		public override int GetHashCode()
		{
			return this.NestedLevel.GetHashCode() * 397 ^ this.LevelIndex.GetHashCode();
		}

		// Token: 0x0600C45D RID: 50269 RVA: 0x002BF320 File Offset: 0x002BD520
		public static bool operator ==(TreeListHierarchyIndex left, TreeListHierarchyIndex right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x0600C45E RID: 50270 RVA: 0x002BF329 File Offset: 0x002BD529
		public static bool operator !=(TreeListHierarchyIndex left, TreeListHierarchyIndex right)
		{
			return !object.Equals(left, right);
		}
	}
}
