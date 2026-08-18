using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000537 RID: 1335
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class EdmRelationshipAttribute : Attribute
	{
		// Token: 0x060032E4 RID: 13028 RVA: 0x000F0708 File Offset: 0x000EE908
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

		// Token: 0x060032E5 RID: 13029 RVA: 0x000F0758 File Offset: 0x000EE958
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

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060032E6 RID: 13030 RVA: 0x000F07B0 File Offset: 0x000EE9B0
		public string RelationshipNamespaceName
		{
			get
			{
				return this._relationshipNamespaceName;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x060032E7 RID: 13031 RVA: 0x000F07B8 File Offset: 0x000EE9B8
		public string RelationshipName
		{
			get
			{
				return this._relationshipName;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x060032E8 RID: 13032 RVA: 0x000F07C0 File Offset: 0x000EE9C0
		public string Role1Name
		{
			get
			{
				return this._role1Name;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x060032E9 RID: 13033 RVA: 0x000F07C8 File Offset: 0x000EE9C8
		public RelationshipMultiplicity Role1Multiplicity
		{
			get
			{
				return this._role1Multiplicity;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x060032EA RID: 13034 RVA: 0x000F07D0 File Offset: 0x000EE9D0
		public Type Role1Type
		{
			get
			{
				return this._role1Type;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060032EB RID: 13035 RVA: 0x000F07D8 File Offset: 0x000EE9D8
		public string Role2Name
		{
			get
			{
				return this._role2Name;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x060032EC RID: 13036 RVA: 0x000F07E0 File Offset: 0x000EE9E0
		public RelationshipMultiplicity Role2Multiplicity
		{
			get
			{
				return this._role2Multiplicity;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060032ED RID: 13037 RVA: 0x000F07E8 File Offset: 0x000EE9E8
		public Type Role2Type
		{
			get
			{
				return this._role2Type;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060032EE RID: 13038 RVA: 0x000F07F0 File Offset: 0x000EE9F0
		public bool IsForeignKey
		{
			get
			{
				return this._isForeignKey;
			}
		}

		// Token: 0x04001370 RID: 4976
		private readonly string _relationshipNamespaceName;

		// Token: 0x04001371 RID: 4977
		private readonly string _relationshipName;

		// Token: 0x04001372 RID: 4978
		private readonly string _role1Name;

		// Token: 0x04001373 RID: 4979
		private readonly string _role2Name;

		// Token: 0x04001374 RID: 4980
		private readonly RelationshipMultiplicity _role1Multiplicity;

		// Token: 0x04001375 RID: 4981
		private readonly RelationshipMultiplicity _role2Multiplicity;

		// Token: 0x04001376 RID: 4982
		private readonly Type _role1Type;

		// Token: 0x04001377 RID: 4983
		private readonly Type _role2Type;

		// Token: 0x04001378 RID: 4984
		private readonly bool _isForeignKey;
	}
}
