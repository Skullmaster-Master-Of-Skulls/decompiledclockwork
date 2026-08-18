using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000672 RID: 1650
	internal class GroupAggregateVarInfoManager
	{
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06004065 RID: 16485 RVA: 0x001278F7 File Offset: 0x00125AF7
		internal IEnumerable<GroupAggregateVarInfo> GroupAggregateVarInfos
		{
			get
			{
				return this._groupAggregateVarInfos;
			}
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x001278FF File Offset: 0x00125AFF
		internal void Add(Var var, GroupAggregateVarInfo groupAggregateVarInfo, Node computationTemplate, bool isUnnested)
		{
			this._groupAggregateVarRelatedVarToInfo.Add(var, new GroupAggregateVarRefInfo(groupAggregateVarInfo, computationTemplate, isUnnested));
			this._groupAggregateVarInfos.Add(groupAggregateVarInfo);
		}

		// Token: 0x06004067 RID: 16487 RVA: 0x00127924 File Offset: 0x00125B24
		internal void Add(Var var, GroupAggregateVarInfo groupAggregateVarInfo, Node computationTemplate, bool isUnnested, EdmMember property)
		{
			if (property == null)
			{
				this.Add(var, groupAggregateVarInfo, computationTemplate, isUnnested);
				return;
			}
			if (this._groupAggregateVarRelatedVarPropertyToInfo == null)
			{
				this._groupAggregateVarRelatedVarPropertyToInfo = new Dictionary<Var, Dictionary<EdmMember, GroupAggregateVarRefInfo>>();
			}
			Dictionary<EdmMember, GroupAggregateVarRefInfo> dictionary;
			if (!this._groupAggregateVarRelatedVarPropertyToInfo.TryGetValue(var, out dictionary))
			{
				dictionary = new Dictionary<EdmMember, GroupAggregateVarRefInfo>();
				this._groupAggregateVarRelatedVarPropertyToInfo.Add(var, dictionary);
			}
			dictionary.Add(property, new GroupAggregateVarRefInfo(groupAggregateVarInfo, computationTemplate, isUnnested));
			this._groupAggregateVarInfos.Add(groupAggregateVarInfo);
		}

		// Token: 0x06004068 RID: 16488 RVA: 0x00127995 File Offset: 0x00125B95
		internal bool TryGetReferencedGroupAggregateVarInfo(Var var, out GroupAggregateVarRefInfo groupAggregateVarRefInfo)
		{
			return this._groupAggregateVarRelatedVarToInfo.TryGetValue(var, out groupAggregateVarRefInfo);
		}

		// Token: 0x06004069 RID: 16489 RVA: 0x001279A4 File Offset: 0x00125BA4
		internal bool TryGetReferencedGroupAggregateVarInfo(Var var, EdmMember property, out GroupAggregateVarRefInfo groupAggregateVarRefInfo)
		{
			if (property == null)
			{
				return this.TryGetReferencedGroupAggregateVarInfo(var, out groupAggregateVarRefInfo);
			}
			Dictionary<EdmMember, GroupAggregateVarRefInfo> dictionary;
			if (this._groupAggregateVarRelatedVarPropertyToInfo == null || !this._groupAggregateVarRelatedVarPropertyToInfo.TryGetValue(var, out dictionary))
			{
				groupAggregateVarRefInfo = null;
				return false;
			}
			return dictionary.TryGetValue(property, out groupAggregateVarRefInfo);
		}

		// Token: 0x04001803 RID: 6147
		private readonly Dictionary<Var, GroupAggregateVarRefInfo> _groupAggregateVarRelatedVarToInfo = new Dictionary<Var, GroupAggregateVarRefInfo>();

		// Token: 0x04001804 RID: 6148
		private Dictionary<Var, Dictionary<EdmMember, GroupAggregateVarRefInfo>> _groupAggregateVarRelatedVarPropertyToInfo;

		// Token: 0x04001805 RID: 6149
		private readonly HashSet<GroupAggregateVarInfo> _groupAggregateVarInfos = new HashSet<GroupAggregateVarInfo>();
	}
}
