using System;
using System.Collections.Generic;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000D37 RID: 3383
	internal class Group : IGroup, IEquatable<Group>
	{
		// Token: 0x06007DB9 RID: 32185 RVA: 0x001CBF85 File Offset: 0x001CA185
		internal Group(object name)
		{
			this.Name = (name ?? NullValue.Instance);
			this.caption = Convert.ToString(this.Name, CultureInfo.InvariantCulture);
		}

		// Token: 0x1700281C RID: 10268
		// (get) Token: 0x06007DBA RID: 32186 RVA: 0x001CBFB4 File Offset: 0x001CA1B4
		internal int Hash
		{
			get
			{
				if (this.hash == null)
				{
					this.hash = new int?(((this.Parent != null) ? this.Parent.GetHashCode() : Group.RandomHashCode) * 104743 + this.Name.GetHashCode() * 104759);
				}
				return this.hash.Value;
			}
		}

		// Token: 0x1700281D RID: 10269
		// (get) Token: 0x06007DBB RID: 32187 RVA: 0x001CC016 File Offset: 0x001CA216
		public GroupType Type
		{
			get
			{
				return GroupType.Subheading;
			}
		}

		// Token: 0x1700281E RID: 10270
		// (get) Token: 0x06007DBC RID: 32188 RVA: 0x001CC019 File Offset: 0x001CA219
		public IGroup Parent
		{
			get
			{
				return this.InternalParent;
			}
		}

		// Token: 0x1700281F RID: 10271
		// (get) Token: 0x06007DBD RID: 32189 RVA: 0x001CC021 File Offset: 0x001CA221
		// (set) Token: 0x06007DBE RID: 32190 RVA: 0x001CC029 File Offset: 0x001CA229
		public object Name { get; private set; }

		// Token: 0x17002820 RID: 10272
		// (get) Token: 0x06007DBF RID: 32191 RVA: 0x001CC032 File Offset: 0x001CA232
		public IReadOnlyList<IGroup> Groups
		{
			get
			{
				this.InitializeReadOnlyGroups();
				return this.readOnlyGroups;
			}
		}

		// Token: 0x17002821 RID: 10273
		// (get) Token: 0x06007DC0 RID: 32192 RVA: 0x001CC040 File Offset: 0x001CA240
		public bool HasGroups
		{
			get
			{
				return this.groupList != null && this.groupList.Count > 0;
			}
		}

		// Token: 0x17002822 RID: 10274
		// (get) Token: 0x06007DC1 RID: 32193 RVA: 0x001CC05A File Offset: 0x001CA25A
		public bool HasParent
		{
			get
			{
				return this.Parent != null;
			}
		}

		// Token: 0x17002823 RID: 10275
		// (get) Token: 0x06007DC2 RID: 32194 RVA: 0x001CC068 File Offset: 0x001CA268
		public int Level
		{
			get
			{
				int num = 0;
				for (IGroup parent = this.Parent; parent != null; parent = parent.Parent)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x17002824 RID: 10276
		// (get) Token: 0x06007DC3 RID: 32195 RVA: 0x001CC08F File Offset: 0x001CA28F
		internal CalculatedItem CalculatedItem
		{
			get
			{
				return this.Name as CalculatedItem;
			}
		}

		// Token: 0x17002825 RID: 10277
		// (get) Token: 0x06007DC4 RID: 32196 RVA: 0x001CC09C File Offset: 0x001CA29C
		// (set) Token: 0x06007DC5 RID: 32197 RVA: 0x001CC0A4 File Offset: 0x001CA2A4
		internal Group InternalParent { get; set; }

		// Token: 0x17002826 RID: 10278
		// (get) Token: 0x06007DC6 RID: 32198 RVA: 0x001CC0AD File Offset: 0x001CA2AD
		internal IList<Group> InternalGroups
		{
			get
			{
				this.InitializeReadOnlyGroups();
				return this.groupList;
			}
		}

		// Token: 0x17002827 RID: 10279
		// (get) Token: 0x06007DC7 RID: 32199 RVA: 0x001CC0BB File Offset: 0x001CA2BB
		private List<Group> GroupList
		{
			get
			{
				if (this.groupList == null)
				{
					this.groupList = new List<Group>();
				}
				return this.groupList;
			}
		}

		// Token: 0x17002828 RID: 10280
		// (get) Token: 0x06007DC8 RID: 32200 RVA: 0x001CC0D6 File Offset: 0x001CA2D6
		private Dictionary<object, Group> GroupsByName
		{
			get
			{
				if (this.groupsByName == null)
				{
					this.groupsByName = new Dictionary<object, Group>();
				}
				return this.groupsByName;
			}
		}

		// Token: 0x06007DC9 RID: 32201 RVA: 0x001CC0F1 File Offset: 0x001CA2F1
		public override int GetHashCode()
		{
			return this.Hash;
		}

		// Token: 0x06007DCA RID: 32202 RVA: 0x001CC0FC File Offset: 0x001CA2FC
		public override bool Equals(object obj)
		{
			Group group = obj as Group;
			return group != null && this.Equals(group);
		}

		// Token: 0x06007DCB RID: 32203 RVA: 0x001CC11C File Offset: 0x001CA31C
		public bool Equals(Group other)
		{
			if (this == other)
			{
				return true;
			}
			if (other == null)
			{
				return false;
			}
			if (!object.Equals(this.Name, other.Name))
			{
				return false;
			}
			if (this.Parent == null)
			{
				return other.Parent == null;
			}
			return this.Parent.Equals(other.Parent);
		}

		// Token: 0x06007DCC RID: 32204 RVA: 0x001CC16C File Offset: 0x001CA36C
		public override string ToString()
		{
			return this.caption;
		}

		// Token: 0x06007DCD RID: 32205 RVA: 0x001CC174 File Offset: 0x001CA374
		internal void SortSubGroups(IComparer<Group> comparer)
		{
			if (this.HasGroups)
			{
				this.GroupList.Sort(comparer);
			}
		}

		// Token: 0x06007DCE RID: 32206 RVA: 0x001CC18C File Offset: 0x001CA38C
		internal Group CreateGroupByName(object groupName)
		{
			if (groupName == null)
			{
				groupName = NullValue.Instance;
			}
			Group group;
			if (!this.GroupsByName.TryGetValue(groupName, out group))
			{
				group = new Group(groupName);
				this.AddGroup(group);
			}
			return group;
		}

		// Token: 0x06007DCF RID: 32207 RVA: 0x001CC1C2 File Offset: 0x001CA3C2
		internal void AddGroup(Group group)
		{
			if (group.Parent != null)
			{
				throw new InvalidOperationException("Group is already a child of another Group.");
			}
			group.InternalParent = this;
			this.GroupsByName.Add(group.Name, group);
			this.GroupList.Add(group);
		}

		// Token: 0x06007DD0 RID: 32208 RVA: 0x001CC1FC File Offset: 0x001CA3FC
		internal void RemoveGroupAt(int i)
		{
			Group group = this.GroupList[i];
			this.GroupList.RemoveAt(i);
			this.GroupsByName.Remove(group.Name);
		}

		// Token: 0x06007DD1 RID: 32209 RVA: 0x001CC234 File Offset: 0x001CA434
		internal IGroup GetGroupByName(object groupName)
		{
			object key = (groupName == null) ? NullValue.Instance : groupName;
			Group result;
			this.GroupsByName.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x06007DD2 RID: 32210 RVA: 0x001CC25D File Offset: 0x001CA45D
		private void InitializeReadOnlyGroups()
		{
			if (this.readOnlyGroups == null && this.groupList != null)
			{
				this.readOnlyGroups = new ReadOnlyList<Group, IGroup>(this.groupList);
			}
		}

		// Token: 0x04002290 RID: 8848
		private static readonly int RandomHashCode = new object().GetHashCode();

		// Token: 0x04002291 RID: 8849
		private List<Group> groupList;

		// Token: 0x04002292 RID: 8850
		private IReadOnlyList<IGroup> readOnlyGroups;

		// Token: 0x04002293 RID: 8851
		private Dictionary<object, Group> groupsByName;

		// Token: 0x04002294 RID: 8852
		private int? hash;

		// Token: 0x04002295 RID: 8853
		private string caption;
	}
}
