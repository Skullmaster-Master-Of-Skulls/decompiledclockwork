using System;
using System.Data.Metadata.Edm;
using System.Globalization;

namespace System.Data.Objects.DataClasses
{
	// Token: 0x02000195 RID: 405
	[Serializable]
	internal class RelationshipNavigation
	{
		// Token: 0x06001D05 RID: 7429 RVA: 0x000636C8 File Offset: 0x000618C8
		internal RelationshipNavigation(string relationshipName, string from, string to, NavigationPropertyAccessor fromAccessor, NavigationPropertyAccessor toAccessor)
		{
			EntityUtil.CheckStringArgument(relationshipName, "relationshipName");
			EntityUtil.CheckStringArgument(from, "from");
			EntityUtil.CheckStringArgument(to, "to");
			this._relationshipName = relationshipName;
			this._from = from;
			this._to = to;
			this._fromAccessor = fromAccessor;
			this._toAccessor = toAccessor;
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001D06 RID: 7430 RVA: 0x00063721 File Offset: 0x00061921
		internal string RelationshipName
		{
			get
			{
				return this._relationshipName;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001D07 RID: 7431 RVA: 0x00063729 File Offset: 0x00061929
		internal string From
		{
			get
			{
				return this._from;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x00063731 File Offset: 0x00061931
		internal string To
		{
			get
			{
				return this._to;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x00063739 File Offset: 0x00061939
		internal NavigationPropertyAccessor ToPropertyAccessor
		{
			get
			{
				return this._toAccessor;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x00063741 File Offset: 0x00061941
		internal bool IsInitialized
		{
			get
			{
				return this._toAccessor != null && this._fromAccessor != null;
			}
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x00063756 File Offset: 0x00061956
		internal void InitializeAccessors(NavigationPropertyAccessor fromAccessor, NavigationPropertyAccessor toAccessor)
		{
			this._fromAccessor = fromAccessor;
			this._toAccessor = toAccessor;
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001D0C RID: 7436 RVA: 0x00063768 File Offset: 0x00061968
		internal RelationshipNavigation Reverse
		{
			get
			{
				if (this._reverse == null || !this._reverse.IsInitialized)
				{
					this._reverse = new RelationshipNavigation(this._relationshipName, this._to, this._from, this._toAccessor, this._fromAccessor);
				}
				return this._reverse;
			}
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x000637BC File Offset: 0x000619BC
		public override bool Equals(object obj)
		{
			RelationshipNavigation relationshipNavigation = obj as RelationshipNavigation;
			return this == relationshipNavigation || (this != null && relationshipNavigation != null && this.RelationshipName == relationshipNavigation.RelationshipName && this.From == relationshipNavigation.From && this.To == relationshipNavigation.To);
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x00063815 File Offset: 0x00061A15
		public override int GetHashCode()
		{
			return this.RelationshipName.GetHashCode();
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x00063822 File Offset: 0x00061A22
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "RelationshipNavigation: ({0},{1},{2})", new object[]
			{
				this._relationshipName,
				this._from,
				this._to
			});
		}

		// Token: 0x04000BB9 RID: 3001
		private readonly string _relationshipName;

		// Token: 0x04000BBA RID: 3002
		private readonly string _from;

		// Token: 0x04000BBB RID: 3003
		private readonly string _to;

		// Token: 0x04000BBC RID: 3004
		[NonSerialized]
		private RelationshipNavigation _reverse;

		// Token: 0x04000BBD RID: 3005
		[NonSerialized]
		private NavigationPropertyAccessor _fromAccessor;

		// Token: 0x04000BBE RID: 3006
		[NonSerialized]
		private NavigationPropertyAccessor _toAccessor;
	}
}
