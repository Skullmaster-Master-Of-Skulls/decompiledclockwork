using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200008B RID: 139
	internal class GroupAggregateVarInfoManager
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x00033C97 File Offset: 0x00031E97
		internal IEnumerable<GroupAggregateVarInfo> GroupAggregateVarInfos
		{
			get
			{
				return this._groupAggregateVarInfos;
			}
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00033C9F File Offset: 0x00031E9F
		internal void Add(Var var, GroupAggregateVarInfo groupAggregateVarInfo, Node computationTemplate, bool isUnnested)
		{
			this._groupAggregateVarRelatedVarToInfo.Add(var, new GroupAggregateVarRefInfo(groupAggregateVarInfo, computationTemplate, isUnnested));
			this._groupAggregateVarInfos.Add(groupAggregateVarInfo);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00033CC4 File Offset: 0x00031EC4
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

		// Token: 0x060009A1 RID: 2465 RVA: 0x00033D35 File Offset: 0x00031F35
		internal bool TryGetReferencedGroupAggregateVarInfo(Var var, out GroupAggregateVarRefInfo groupAggregateVarRefInfo)
		{
			return this._groupAggregateVarRelatedVarToInfo.TryGetValue(var, out groupAggregateVarRefInfo);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00033D44 File Offset: 0x00031F44
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

		// Token: 0x04000895 RID: 2197
		private readonly Dictionary<Var, GroupAggregateVarRefInfo> _groupAggregateVarRelatedVarToInfo = new Dictionary<Var, GroupAggregateVarRefInfo>();

		// Token: 0x04000896 RID: 2198
		private Dictionary<Var, Dictionary<EdmMember, GroupAggregateVarRefInfo>> _groupAggregateVarRelatedVarPropertyToInfo;

		// Token: 0x04000897 RID: 2199
		private HashSet<GroupAggregateVarInfo> _groupAggregateVarInfos = new HashSet<GroupAggregateVarInfo>();
	}
}
