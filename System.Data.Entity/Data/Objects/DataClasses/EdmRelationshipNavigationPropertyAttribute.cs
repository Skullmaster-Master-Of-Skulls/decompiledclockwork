using System;

namespace System.Data.Objects.DataClasses
{
	// Token: 0x02000185 RID: 389
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class EdmRelationshipNavigationPropertyAttribute : EdmPropertyAttribute
	{
		// Token: 0x06001C18 RID: 7192 RVA: 0x0005FA9C File Offset: 0x0005DC9C
		public EdmRelationshipNavigationPropertyAttribute(string relationshipNamespaceName, string relationshipName, string targetRoleName)
		{
			this._relationshipNamespaceName = relationshipNamespaceName;
			this._relationshipName = relationshipName;
			this._targetRoleName = targetRoleName;
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001C19 RID: 7193 RVA: 0x0005FAB9 File Offset: 0x0005DCB9
		public string RelationshipNamespaceName
		{
			get
			{
				return this._relationshipNamespaceName;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001C1A RID: 7194 RVA: 0x0005FAC1 File Offset: 0x0005DCC1
		public string RelationshipName
		{
			get
			{
				return this._relationshipName;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001C1B RID: 7195 RVA: 0x0005FAC9 File Offset: 0x0005DCC9
		public string TargetRoleName
		{
			get
			{
				return this._targetRoleName;
			}
		}

		// Token: 0x04000B98 RID: 2968
		private string _relationshipNamespaceName;

		// Token: 0x04000B99 RID: 2969
		private string _relationshipName;

		// Token: 0x04000B9A RID: 2970
		private string _targetRoleName;
	}
}
