using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x0200049E RID: 1182
	[ComVisible(true)]
	[Serializable]
	public abstract class CodeGroup
	{
		// Token: 0x06002ED4 RID: 11988 RVA: 0x0009E24E File Offset: 0x0009D24E
		internal CodeGroup()
		{
			this.m_membershipCondition = null;
			this.m_children = null;
			this.m_policy = null;
			this.m_element = null;
			this.m_parentLevel = null;
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x0009E27C File Offset: 0x0009D27C
		internal CodeGroup(IMembershipCondition membershipCondition, PermissionSet permSet)
		{
			this.m_membershipCondition = membershipCondition;
			this.m_policy = new PolicyStatement();
			this.m_policy.SetPermissionSetNoCopy(permSet);
			this.m_children = ArrayList.Synchronized(new ArrayList());
			this.m_element = null;
			this.m_parentLevel = null;
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x0009E2CC File Offset: 0x0009D2CC
		protected CodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy)
		{
			if (membershipCondition == null)
			{
				throw new ArgumentNullException("membershipCondition");
			}
			if (policy == null)
			{
				this.m_policy = null;
			}
			else
			{
				this.m_policy = policy.Copy();
			}
			this.m_membershipCondition = membershipCondition.Copy();
			this.m_children = ArrayList.Synchronized(new ArrayList());
			this.m_element = null;
			this.m_parentLevel = null;
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x0009E330 File Offset: 0x0009D330
		public void AddChild(CodeGroup group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (this.m_children == null)
			{
				this.ParseChildren();
			}
			lock (this)
			{
				this.m_children.Add(group.Copy());
			}
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x0009E38C File Offset: 0x0009D38C
		internal void AddChildInternal(CodeGroup group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (this.m_children == null)
			{
				this.ParseChildren();
			}
			lock (this)
			{
				this.m_children.Add(group);
			}
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x0009E3E4 File Offset: 0x0009D3E4
		public void RemoveChild(CodeGroup group)
		{
			if (group == null)
			{
				return;
			}
			if (this.m_children == null)
			{
				this.ParseChildren();
			}
			lock (this)
			{
				int num = this.m_children.IndexOf(group);
				if (num != -1)
				{
					this.m_children.RemoveAt(num);
				}
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06002EDA RID: 11994 RVA: 0x0009E444 File Offset: 0x0009D444
		// (set) Token: 0x06002EDB RID: 11995 RVA: 0x0009E4C4 File Offset: 0x0009D4C4
		public IList Children
		{
			get
			{
				if (this.m_children == null)
				{
					this.ParseChildren();
				}
				IList result;
				lock (this)
				{
					IList list = new ArrayList(this.m_children.Count);
					foreach (object obj in this.m_children)
					{
						list.Add(((CodeGroup)obj).Copy());
					}
					result = list;
				}
				return result;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Children");
				}
				ArrayList arrayList = ArrayList.Synchronized(new ArrayList(value.Count));
				foreach (object obj in value)
				{
					CodeGroup codeGroup = obj as CodeGroup;
					if (codeGroup == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_CodeGroupChildrenMustBeCodeGroups"));
					}
					arrayList.Add(codeGroup.Copy());
				}
				this.m_children = arrayList;
			}
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x0009E534 File Offset: 0x0009D534
		internal IList GetChildrenInternal()
		{
			if (this.m_children == null)
			{
				this.ParseChildren();
			}
			return this.m_children;
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06002EDD RID: 11997 RVA: 0x0009E54A File Offset: 0x0009D54A
		// (set) Token: 0x06002EDE RID: 11998 RVA: 0x0009E56D File Offset: 0x0009D56D
		public IMembershipCondition MembershipCondition
		{
			get
			{
				if (this.m_membershipCondition == null && this.m_element != null)
				{
					this.ParseMembershipCondition();
				}
				return this.m_membershipCondition.Copy();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("MembershipCondition");
				}
				this.m_membershipCondition = value.Copy();
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06002EDF RID: 11999 RVA: 0x0009E589 File Offset: 0x0009D589
		// (set) Token: 0x06002EE0 RID: 12000 RVA: 0x0009E5B6 File Offset: 0x0009D5B6
		public PolicyStatement PolicyStatement
		{
			get
			{
				if (this.m_policy == null && this.m_element != null)
				{
					this.ParsePolicy();
				}
				if (this.m_policy != null)
				{
					return this.m_policy.Copy();
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.m_policy = value.Copy();
					return;
				}
				this.m_policy = null;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06002EE1 RID: 12001 RVA: 0x0009E5CF File Offset: 0x0009D5CF
		// (set) Token: 0x06002EE2 RID: 12002 RVA: 0x0009E5D7 File Offset: 0x0009D5D7
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06002EE3 RID: 12003 RVA: 0x0009E5E0 File Offset: 0x0009D5E0
		// (set) Token: 0x06002EE4 RID: 12004 RVA: 0x0009E5E8 File Offset: 0x0009D5E8
		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		// Token: 0x06002EE5 RID: 12005
		public abstract PolicyStatement Resolve(Evidence evidence);

		// Token: 0x06002EE6 RID: 12006
		public abstract CodeGroup ResolveMatchingCodeGroups(Evidence evidence);

		// Token: 0x06002EE7 RID: 12007
		public abstract CodeGroup Copy();

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06002EE8 RID: 12008 RVA: 0x0009E5F4 File Offset: 0x0009D5F4
		public virtual string PermissionSetName
		{
			get
			{
				if (this.m_policy == null && this.m_element != null)
				{
					this.ParsePolicy();
				}
				if (this.m_policy == null)
				{
					return null;
				}
				NamedPermissionSet namedPermissionSet = this.m_policy.GetPermissionSetNoCopy() as NamedPermissionSet;
				if (namedPermissionSet != null)
				{
					return namedPermissionSet.Name;
				}
				return null;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06002EE9 RID: 12009 RVA: 0x0009E63D File Offset: 0x0009D63D
		public virtual string AttributeString
		{
			get
			{
				if (this.m_policy == null && this.m_element != null)
				{
					this.ParsePolicy();
				}
				if (this.m_policy != null)
				{
					return this.m_policy.AttributeString;
				}
				return null;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06002EEA RID: 12010
		public abstract string MergeLogic { get; }

		// Token: 0x06002EEB RID: 12011 RVA: 0x0009E66A File Offset: 0x0009D66A
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x0009E673 File Offset: 0x0009D673
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x0009E67D File Offset: 0x0009D67D
		public SecurityElement ToXml(PolicyLevel level)
		{
			return this.ToXml(level, this.GetTypeName());
		}

		// Token: 0x06002EEE RID: 12014 RVA: 0x0009E68C File Offset: 0x0009D68C
		internal virtual string GetTypeName()
		{
			return base.GetType().FullName;
		}

		// Token: 0x06002EEF RID: 12015 RVA: 0x0009E69C File Offset: 0x0009D69C
		internal SecurityElement ToXml(PolicyLevel level, string policyClassName)
		{
			if (this.m_membershipCondition == null && this.m_element != null)
			{
				this.ParseMembershipCondition();
			}
			if (this.m_children == null)
			{
				this.ParseChildren();
			}
			if (this.m_policy == null && this.m_element != null)
			{
				this.ParsePolicy();
			}
			SecurityElement securityElement = new SecurityElement("CodeGroup");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), policyClassName);
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(this.m_membershipCondition.ToXml(level));
			if (this.m_policy != null)
			{
				PermissionSet permissionSetNoCopy = this.m_policy.GetPermissionSetNoCopy();
				NamedPermissionSet namedPermissionSet = permissionSetNoCopy as NamedPermissionSet;
				if (namedPermissionSet != null && level != null && level.GetNamedPermissionSetInternal(namedPermissionSet.Name) != null)
				{
					securityElement.AddAttribute("PermissionSetName", namedPermissionSet.Name);
				}
				else if (!permissionSetNoCopy.IsEmpty())
				{
					securityElement.AddChild(permissionSetNoCopy.ToXml());
				}
				if (this.m_policy.Attributes != PolicyStatementAttribute.Nothing)
				{
					securityElement.AddAttribute("Attributes", XMLUtil.BitFieldEnumToString(typeof(PolicyStatementAttribute), this.m_policy.Attributes));
				}
			}
			if (this.m_children.Count > 0)
			{
				lock (this)
				{
					foreach (object obj in this.m_children)
					{
						securityElement.AddChild(((CodeGroup)obj).ToXml(level));
					}
				}
			}
			if (this.m_name != null)
			{
				securityElement.AddAttribute("Name", SecurityElement.Escape(this.m_name));
			}
			if (this.m_description != null)
			{
				securityElement.AddAttribute("Description", SecurityElement.Escape(this.m_description));
			}
			this.CreateXml(securityElement, level);
			return securityElement;
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x0009E850 File Offset: 0x0009D850
		protected virtual void CreateXml(SecurityElement element, PolicyLevel level)
		{
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x0009E854 File Offset: 0x0009D854
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			lock (this)
			{
				this.m_element = e;
				this.m_parentLevel = level;
				this.m_children = null;
				this.m_membershipCondition = null;
				this.m_policy = null;
				this.m_name = e.Attribute("Name");
				this.m_description = e.Attribute("Description");
				this.ParseXml(e, level);
			}
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x0009E8E0 File Offset: 0x0009D8E0
		protected virtual void ParseXml(SecurityElement e, PolicyLevel level)
		{
		}

		// Token: 0x06002EF3 RID: 12019 RVA: 0x0009E8E4 File Offset: 0x0009D8E4
		private bool ParseMembershipCondition(bool safeLoad)
		{
			bool result;
			lock (this)
			{
				IMembershipCondition membershipCondition = null;
				SecurityElement securityElement = this.m_element.SearchForChildByTag("IMembershipCondition");
				if (securityElement == null)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidXMLElement"), new object[]
					{
						"IMembershipCondition",
						base.GetType().FullName
					}));
				}
				try
				{
					membershipCondition = XMLUtil.CreateMembershipCondition(securityElement);
					if (membershipCondition == null)
					{
						return false;
					}
				}
				catch (Exception innerException)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"), innerException);
				}
				membershipCondition.FromXml(securityElement, this.m_parentLevel);
				this.m_membershipCondition = membershipCondition;
				result = true;
			}
			return result;
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x0009E9B0 File Offset: 0x0009D9B0
		private void ParseMembershipCondition()
		{
			this.ParseMembershipCondition(false);
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x0009E9BC File Offset: 0x0009D9BC
		internal void ParseChildren()
		{
			lock (this)
			{
				ArrayList arrayList = ArrayList.Synchronized(new ArrayList());
				if (this.m_element != null && this.m_element.InternalChildren != null)
				{
					this.m_element.Children = (ArrayList)this.m_element.InternalChildren.Clone();
					ArrayList arrayList2 = ArrayList.Synchronized(new ArrayList());
					Evidence evidence = new Evidence();
					int count = this.m_element.InternalChildren.Count;
					int i = 0;
					while (i < count)
					{
						SecurityElement securityElement = (SecurityElement)this.m_element.Children[i];
						if (securityElement.Tag.Equals("CodeGroup"))
						{
							CodeGroup codeGroup = XMLUtil.CreateCodeGroup(securityElement);
							if (codeGroup != null)
							{
								codeGroup.FromXml(securityElement, this.m_parentLevel);
								if (this.ParseMembershipCondition(true))
								{
									codeGroup.Resolve(evidence);
									codeGroup.MembershipCondition.Check(evidence);
									arrayList.Add(codeGroup);
									i++;
								}
								else
								{
									this.m_element.InternalChildren.RemoveAt(i);
									count = this.m_element.InternalChildren.Count;
									arrayList2.Add(new CodeGroupPositionMarker(i, arrayList.Count, securityElement));
								}
							}
							else
							{
								this.m_element.InternalChildren.RemoveAt(i);
								count = this.m_element.InternalChildren.Count;
								arrayList2.Add(new CodeGroupPositionMarker(i, arrayList.Count, securityElement));
							}
						}
						else
						{
							i++;
						}
					}
					foreach (object obj in arrayList2)
					{
						CodeGroupPositionMarker codeGroupPositionMarker = (CodeGroupPositionMarker)obj;
						CodeGroup codeGroup2 = XMLUtil.CreateCodeGroup(codeGroupPositionMarker.element);
						if (codeGroup2 == null)
						{
							throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_FailedCodeGroup"), new object[]
							{
								codeGroupPositionMarker.element.Attribute("class")
							}));
						}
						codeGroup2.FromXml(codeGroupPositionMarker.element, this.m_parentLevel);
						codeGroup2.Resolve(evidence);
						codeGroup2.MembershipCondition.Check(evidence);
						arrayList.Insert(codeGroupPositionMarker.groupIndex, codeGroup2);
						this.m_element.InternalChildren.Insert(codeGroupPositionMarker.elementIndex, codeGroupPositionMarker.element);
					}
				}
				this.m_children = arrayList;
			}
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x0009EC3C File Offset: 0x0009DC3C
		private void ParsePolicy()
		{
			for (;;)
			{
				PolicyStatement policyStatement = new PolicyStatement();
				bool flag = false;
				SecurityElement securityElement = new SecurityElement("PolicyStatement");
				securityElement.AddAttribute("version", "1");
				SecurityElement element = this.m_element;
				lock (this)
				{
					if (this.m_element != null)
					{
						string text = this.m_element.Attribute("PermissionSetName");
						if (text != null)
						{
							securityElement.AddAttribute("PermissionSetName", text);
							flag = true;
						}
						else
						{
							SecurityElement securityElement2 = this.m_element.SearchForChildByTag("PermissionSet");
							if (securityElement2 != null)
							{
								securityElement.AddChild(securityElement2);
								flag = true;
							}
							else
							{
								securityElement.AddChild(new PermissionSet(false).ToXml());
								flag = true;
							}
						}
						string text2 = this.m_element.Attribute("Attributes");
						if (text2 != null)
						{
							securityElement.AddAttribute("Attributes", text2);
							flag = true;
						}
					}
				}
				if (flag)
				{
					policyStatement.FromXml(securityElement, this.m_parentLevel);
				}
				else
				{
					policyStatement.PermissionSet = null;
				}
				lock (this)
				{
					if (element == this.m_element && this.m_policy == null)
					{
						this.m_policy = policyStatement;
					}
					else if (this.m_policy == null)
					{
						continue;
					}
				}
				break;
			}
			if (this.m_policy != null && this.m_children != null)
			{
				IMembershipCondition membershipCondition = this.m_membershipCondition;
			}
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x0009ED98 File Offset: 0x0009DD98
		public override bool Equals(object o)
		{
			CodeGroup codeGroup = o as CodeGroup;
			if (codeGroup != null && base.GetType().Equals(codeGroup.GetType()) && object.Equals(this.m_name, codeGroup.m_name) && object.Equals(this.m_description, codeGroup.m_description))
			{
				if (this.m_membershipCondition == null && this.m_element != null)
				{
					this.ParseMembershipCondition();
				}
				if (codeGroup.m_membershipCondition == null && codeGroup.m_element != null)
				{
					codeGroup.ParseMembershipCondition();
				}
				if (object.Equals(this.m_membershipCondition, codeGroup.m_membershipCondition))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x0009EE2C File Offset: 0x0009DE2C
		public bool Equals(CodeGroup cg, bool compareChildren)
		{
			if (!this.Equals(cg))
			{
				return false;
			}
			if (compareChildren)
			{
				if (this.m_children == null)
				{
					this.ParseChildren();
				}
				if (cg.m_children == null)
				{
					cg.ParseChildren();
				}
				ArrayList arrayList = new ArrayList(this.m_children);
				ArrayList arrayList2 = new ArrayList(cg.m_children);
				if (arrayList.Count != arrayList2.Count)
				{
					return false;
				}
				for (int i = 0; i < arrayList.Count; i++)
				{
					if (!((CodeGroup)arrayList[i]).Equals((CodeGroup)arrayList2[i], true))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x0009EEC0 File Offset: 0x0009DEC0
		public override int GetHashCode()
		{
			if (this.m_membershipCondition == null && this.m_element != null)
			{
				this.ParseMembershipCondition();
			}
			if (this.m_name != null || this.m_membershipCondition != null)
			{
				return ((this.m_name == null) ? 0 : this.m_name.GetHashCode()) + ((this.m_membershipCondition == null) ? 0 : this.m_membershipCondition.GetHashCode());
			}
			return base.GetType().GetHashCode();
		}

		// Token: 0x040017EB RID: 6123
		private IMembershipCondition m_membershipCondition;

		// Token: 0x040017EC RID: 6124
		private IList m_children;

		// Token: 0x040017ED RID: 6125
		private PolicyStatement m_policy;

		// Token: 0x040017EE RID: 6126
		private SecurityElement m_element;

		// Token: 0x040017EF RID: 6127
		private PolicyLevel m_parentLevel;

		// Token: 0x040017F0 RID: 6128
		private string m_name;

		// Token: 0x040017F1 RID: 6129
		private string m_description;
	}
}
