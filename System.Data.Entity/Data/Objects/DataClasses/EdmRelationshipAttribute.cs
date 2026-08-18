using System;
using System.Data.Metadata.Edm;

namespace System.Data.Objects.DataClasses
{
	// Token: 0x02000186 RID: 390
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class EdmRelationshipAttribute : Attribute
	{
		// Token: 0x06001C1C RID: 7196 RVA: 0x0005FAD4 File Offset: 0x0005DCD4
		public EdmRelationshipAttribute(string relationshipNamespaceName, string relationshipName, string role1Name, RelationshipMultiplicity role1Multiplicity, Type role1Type, string role2Name, RelationshipMultiplicity role2Multiplicity, Type role2Type)
		{
			this._relationshipNamespaceName = relationshipNamespaceName;
			this._relationshipName = relationshipName;
			this._role1Name = role1Name;
			this._role1Multiplicity = role1Multiplicity;
			this._role1Type = role1Type;
			this._role2Name = role2Name;
			this._role2Multiplicity = role2Multiplicity;
			this._role2Type = role2Type;
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x0005FB24 File Offset: 0x0005DD24
		public EdmRelationshipAttribute(string relationshipNamespaceName, string relationshipName, string role1Name, RelationshipMultiplicity role1Multiplicity, Type role1Type, string role2Name, RelationshipMultiplicity role2Multiplicity, Type role2Type, bool isForeignKey)
		{
			this._relationshipNamespaceName = relationshipNamespaceName;
			this._relationshipName = relationshipName;
			this._role1Name = role1Name;
			this._role1Multiplicity = role1Multiplicity;
			this._role1Type = role1Type;
			this._role2Name = role2Name;
			this._role2Multiplicity = role2Multiplicity;
			this._role2Type = role2Type;
			this._isForeignKey = isForeignKey;
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001C1E RID: 7198 RVA: 0x0005FB7C File Offset: 0x0005DD7C
		public string RelationshipNamespaceName
		{
			get
			{
				return this._relationshipNamespaceName;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x0005FB84 File Offset: 0x0005DD84
		public string RelationshipName
		{
			get
			{
				return this._relationshipName;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x0005FB8C File Offset: 0x0005DD8C
		public string Role1Name
		{
			get
			{
				return this._role1Name;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x0005FB94 File Offset: 0x0005DD94
		public RelationshipMultiplicity Role1Multiplicity
		{
			get
			{
				return this._role1Multiplicity;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001C22 RID: 7202 RVA: 0x0005FB9C File Offset: 0x0005DD9C
		public Type Role1Type
		{
			get
			{
				return this._role1Type;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x0005FBA4 File Offset: 0x0005DDA4
		public string Role2Name
		{
			get
			{
				return this._role2Name;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x0005FBAC File Offset: 0x0005DDAC
		public RelationshipMultiplicity Role2Multiplicity
		{
			get
			{
				return this._role2Multiplicity;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x0005FBB4 File Offset: 0x0005DDB4
		public Type Role2Type
		{
			get
			{
				return this._role2Type;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001C26 RID: 7206 RVA: 0x0005FBBC File Offset: 0x0005DDBC
		public bool IsForeignKey
		{
			get
			{
				return this._isForeignKey;
			}
		}

		// Token: 0x04000B9B RID: 2971
		private readonly string _relationshipNamespaceName;

		// Token: 0x04000B9C RID: 2972
		private readonly string _relationshipName;

		// Token: 0x04000B9D RID: 2973
		private readonly string _role1Name;

		// Token: 0x04000B9E RID: 2974
		private readonly string _role2Name;

		// Token: 0x04000B9F RID: 2975
		private readonly RelationshipMultiplicity _role1Multiplicity;

		// Token: 0x04000BA0 RID: 2976
		private readonly RelationshipMultiplicity _role2Multiplicity;

		// Token: 0x04000BA1 RID: 2977
		private readonly Type _role1Type;

		// Token: 0x04000BA2 RID: 2978
		private readonly Type _role2Type;

		// Token: 0x04000BA3 RID: 2979
		private readonly bool _isForeignKey;
	}
}
