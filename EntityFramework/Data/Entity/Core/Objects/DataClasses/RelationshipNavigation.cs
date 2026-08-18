using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Globalization;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000549 RID: 1353
	[Serializable]
	internal class RelationshipNavigation
	{
		// Token: 0x0600346A RID: 13418 RVA: 0x000F8670 File Offset: 0x000F6870
		internal RelationshipNavigation(string relationshipName, string from, string to, NavigationPropertyAccessor fromAccessor, NavigationPropertyAccessor toAccessor)
		{
			Check.NotEmpty(relationshipName, "relationshipName");
			Check.NotEmpty(from, "from");
			Check.NotEmpty(to, "to");
			this._relationshipName = relationshipName;
			this._from = from;
			this._to = to;
			this._fromAccessor = fromAccessor;
			this._toAccessor = toAccessor;
		}

		// Token: 0x0600346B RID: 13419 RVA: 0x000F86CC File Offset: 0x000F68CC
		internal RelationshipNavigation(AssociationType associationType, string from, string to, NavigationPropertyAccessor fromAccessor, NavigationPropertyAccessor toAccessor)
		{
			this._associationType = associationType;
			this._relationshipName = associationType.FullName;
			this._from = from;
			this._to = to;
			this._fromAccessor = fromAccessor;
			this._toAccessor = toAccessor;
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x0600346C RID: 13420 RVA: 0x000F8705 File Offset: 0x000F6905
		internal AssociationType AssociationType
		{
			get
			{
				return this._associationType;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x0600346D RID: 13421 RVA: 0x000F870D File Offset: 0x000F690D
		internal string RelationshipName
		{
			get
			{
				return this._relationshipName;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x0600346E RID: 13422 RVA: 0x000F8715 File Offset: 0x000F6915
		internal string From
		{
			get
			{
				return this._from;
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x0600346F RID: 13423 RVA: 0x000F871D File Offset: 0x000F691D
		internal string To
		{
			get
			{
				return this._to;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06003470 RID: 13424 RVA: 0x000F8725 File Offset: 0x000F6925
		internal NavigationPropertyAccessor ToPropertyAccessor
		{
			get
			{
				return this._toAccessor;
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06003471 RID: 13425 RVA: 0x000F872D File Offset: 0x000F692D
		internal bool IsInitialized
		{
			get
			{
				return this._toAccessor != null && this._fromAccessor != null;
			}
		}

		// Token: 0x06003472 RID: 13426 RVA: 0x000F8745 File Offset: 0x000F6945
		internal void InitializeAccessors(NavigationPropertyAccessor fromAccessor, NavigationPropertyAccessor toAccessor)
		{
			this._fromAccessor = fromAccessor;
			this._toAccessor = toAccessor;
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06003473 RID: 13427 RVA: 0x000F8758 File Offset: 0x000F6958
		internal RelationshipNavigation Reverse
		{
			get
			{
				if (this._reverse == null || !this._reverse.IsInitialized)
				{
					this._reverse = ((this._associationType != null) ? new RelationshipNavigation(this._associationType, this._to, this._from, this._toAccessor, this._fromAccessor) : new RelationshipNavigation(this._relationshipName, this._to, this._from, this._toAccessor, this._fromAccessor));
				}
				return this._reverse;
			}
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x000F87D8 File Offset: 0x000F69D8
		public override bool Equals(object obj)
		{
			RelationshipNavigation relationshipNavigation = obj as RelationshipNavigation;
			return this == relationshipNavigation || (this != null && relationshipNavigation != null && this.RelationshipName == relationshipNavigation.RelationshipName && this.From == relationshipNavigation.From && this.To == relationshipNavigation.To);
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x000F8831 File Offset: 0x000F6A31
		public override int GetHashCode()
		{
			return this.RelationshipName.GetHashCode();
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x000F8840 File Offset: 0x000F6A40
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "RelationshipNavigation: ({0},{1},{2})", new object[]
			{
				this._relationshipName,
				this._from,
				this._to
			});
		}

		// Token: 0x040013A5 RID: 5029
		private readonly string _relationshipName;

		// Token: 0x040013A6 RID: 5030
		private readonly string _from;

		// Token: 0x040013A7 RID: 5031
		private readonly string _to;

		// Token: 0x040013A8 RID: 5032
		[NonSerialized]
		private RelationshipNavigation _reverse;

		// Token: 0x040013A9 RID: 5033
		[NonSerialized]
		private NavigationPropertyAccessor _fromAccessor;

		// Token: 0x040013AA RID: 5034
		[NonSerialized]
		private NavigationPropertyAccessor _toAccessor;

		// Token: 0x040013AB RID: 5035
		[NonSerialized]
		private readonly AssociationType _associationType;
	}
}
