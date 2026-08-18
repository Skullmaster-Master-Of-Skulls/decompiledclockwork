using System;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000536 RID: 1334
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class EdmRelationshipNavigationPropertyAttribute : EdmPropertyAttribute
	{
		// Token: 0x060032E0 RID: 13024 RVA: 0x000F06D1 File Offset: 0x000EE8D1
		public EdmRelationshipNavigationPropertyAttribute(string relationshipNamespaceName, string relationshipName, string targetRoleName)
		{
			this._relationshipNamespaceName = relationshipNamespaceName;
			this._relationshipName = relationshipName;
			this._targetRoleName = targetRoleName;
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x060032E1 RID: 13025 RVA: 0x000F06EE File Offset: 0x000EE8EE
		public string RelationshipNamespaceName
		{
			get
			{
				return this._relationshipNamespaceName;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x060032E2 RID: 13026 RVA: 0x000F06F6 File Offset: 0x000EE8F6
		public string RelationshipName
		{
			get
			{
				return this._relationshipName;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060032E3 RID: 13027 RVA: 0x000F06FE File Offset: 0x000EE8FE
		public string TargetRoleName
		{
			get
			{
				return this._targetRoleName;
			}
		}

		// Token: 0x0400136D RID: 4973
		private readonly string _relationshipNamespaceName;

		// Token: 0x0400136E RID: 4974
		private readonly string _relationshipName;

		// Token: 0x0400136F RID: 4975
		private readonly string _targetRoleName;
	}
}
