using System;
using System.Collections.Generic;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.ViewModels
{
	// Token: 0x02000D4A RID: 3402
	internal class GroupNode : IGroup
	{
		// Token: 0x06007E92 RID: 32402 RVA: 0x001CFB30 File Offset: 0x001CDD30
		public GroupNode(IGroup group, GroupNode parent, GroupType type, int aggregateIndex)
		{
			this.Group = group;
			this.Type = type;
			this.AggregateIndex = aggregateIndex;
			this.Parent = parent;
			object name = group.Name;
			this.Name = name;
			this.caption = Convert.ToString(name, CultureInfo.InvariantCulture);
		}

		// Token: 0x06007E93 RID: 32403 RVA: 0x001CFB7F File Offset: 0x001CDD7F
		public GroupNode(IGroup group, GroupNode parent, GroupType type, int aggregateIndex, string customName)
		{
			this.Group = group;
			this.Type = type;
			this.AggregateIndex = aggregateIndex;
			this.Parent = parent;
			this.Name = customName;
			this.caption = Convert.ToString(customName, CultureInfo.InvariantCulture);
		}

		// Token: 0x17002857 RID: 10327
		// (get) Token: 0x06007E94 RID: 32404 RVA: 0x001CFBBE File Offset: 0x001CDDBE
		// (set) Token: 0x06007E95 RID: 32405 RVA: 0x001CFBC6 File Offset: 0x001CDDC6
		public int AggregateIndex { get; internal set; }

		// Token: 0x17002858 RID: 10328
		// (get) Token: 0x06007E96 RID: 32406 RVA: 0x001CFBCF File Offset: 0x001CDDCF
		// (set) Token: 0x06007E97 RID: 32407 RVA: 0x001CFBD7 File Offset: 0x001CDDD7
		public GroupType Type { get; private set; }

		// Token: 0x17002859 RID: 10329
		// (get) Token: 0x06007E98 RID: 32408 RVA: 0x001CFBE0 File Offset: 0x001CDDE0
		// (set) Token: 0x06007E99 RID: 32409 RVA: 0x001CFBE8 File Offset: 0x001CDDE8
		public IGroup Group { get; private set; }

		// Token: 0x1700285A RID: 10330
		// (get) Token: 0x06007E9A RID: 32410 RVA: 0x001CFBF1 File Offset: 0x001CDDF1
		// (set) Token: 0x06007E9B RID: 32411 RVA: 0x001CFBF9 File Offset: 0x001CDDF9
		public IGroup Parent { get; private set; }

		// Token: 0x1700285B RID: 10331
		// (get) Token: 0x06007E9C RID: 32412 RVA: 0x001CFC02 File Offset: 0x001CDE02
		// (set) Token: 0x06007E9D RID: 32413 RVA: 0x001CFC0A File Offset: 0x001CDE0A
		public object Name { get; private set; }

		// Token: 0x1700285C RID: 10332
		// (get) Token: 0x06007E9E RID: 32414 RVA: 0x001CFC13 File Offset: 0x001CDE13
		public IReadOnlyList<IGroup> Groups
		{
			get
			{
				if (!this.HasGroups)
				{
					return new ReadOnlyList<GroupNode, IGroup>(new List<GroupNode>());
				}
				return this.readOnlyGroups;
			}
		}

		// Token: 0x1700285D RID: 10333
		// (get) Token: 0x06007E9F RID: 32415 RVA: 0x001CFC2E File Offset: 0x001CDE2E
		public bool HasGroups
		{
			get
			{
				return this.readOnlyGroups != null && this.readOnlyGroups.Count > 0;
			}
		}

		// Token: 0x1700285E RID: 10334
		// (get) Token: 0x06007EA0 RID: 32416 RVA: 0x001CFC48 File Offset: 0x001CDE48
		public int Level
		{
			get
			{
				return IGroupExtensions.GetLevel(this);
			}
		}

		// Token: 0x1700285F RID: 10335
		// (get) Token: 0x06007EA1 RID: 32417 RVA: 0x001CFC50 File Offset: 0x001CDE50
		internal IList<GroupNode> InternalGroups
		{
			get
			{
				if (this.internalGroups == null)
				{
					this.internalGroups = new List<GroupNode>();
					this.readOnlyGroups = new ReadOnlyList<GroupNode, IGroup>(this.internalGroups);
				}
				return this.internalGroups;
			}
		}

		// Token: 0x06007EA2 RID: 32418 RVA: 0x001CFC7C File Offset: 0x001CDE7C
		public override string ToString()
		{
			return this.caption;
		}

		// Token: 0x040022D6 RID: 8918
		private IList<GroupNode> internalGroups;

		// Token: 0x040022D7 RID: 8919
		private ReadOnlyList<GroupNode, IGroup> readOnlyGroups;

		// Token: 0x040022D8 RID: 8920
		private string caption;
	}
}
