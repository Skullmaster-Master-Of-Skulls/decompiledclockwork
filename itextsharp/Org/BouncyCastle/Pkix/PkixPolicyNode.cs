using System;
using System.Collections;
using System.Text;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x02000544 RID: 1348
	public class PkixPolicyNode
	{
		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06002E51 RID: 11857 RVA: 0x0011E7BD File Offset: 0x0011D7BD
		public virtual int Depth
		{
			get
			{
				return this.mDepth;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06002E52 RID: 11858 RVA: 0x0011E7C5 File Offset: 0x0011D7C5
		public virtual IEnumerable Children
		{
			get
			{
				return new EnumerableProxy(this.mChildren);
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06002E53 RID: 11859 RVA: 0x0011E7D2 File Offset: 0x0011D7D2
		// (set) Token: 0x06002E54 RID: 11860 RVA: 0x0011E7DA File Offset: 0x0011D7DA
		public virtual bool IsCritical
		{
			get
			{
				return this.mCritical;
			}
			set
			{
				this.mCritical = value;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06002E55 RID: 11861 RVA: 0x0011E7E3 File Offset: 0x0011D7E3
		public virtual ISet PolicyQualifiers
		{
			get
			{
				return new HashSet(this.mPolicyQualifiers);
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06002E56 RID: 11862 RVA: 0x0011E7F0 File Offset: 0x0011D7F0
		public virtual string ValidPolicy
		{
			get
			{
				return this.mValidPolicy;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06002E57 RID: 11863 RVA: 0x0011E7F8 File Offset: 0x0011D7F8
		public virtual bool HasChildren
		{
			get
			{
				return this.mChildren.Count != 0;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06002E58 RID: 11864 RVA: 0x0011E80B File Offset: 0x0011D80B
		// (set) Token: 0x06002E59 RID: 11865 RVA: 0x0011E818 File Offset: 0x0011D818
		public virtual ISet ExpectedPolicies
		{
			get
			{
				return new HashSet(this.mExpectedPolicies);
			}
			set
			{
				this.mExpectedPolicies = new HashSet(value);
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06002E5A RID: 11866 RVA: 0x0011E826 File Offset: 0x0011D826
		// (set) Token: 0x06002E5B RID: 11867 RVA: 0x0011E82E File Offset: 0x0011D82E
		public virtual PkixPolicyNode Parent
		{
			get
			{
				return this.mParent;
			}
			set
			{
				this.mParent = value;
			}
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x0011E838 File Offset: 0x0011D838
		public PkixPolicyNode(IList children, int depth, ISet expectedPolicies, PkixPolicyNode parent, ISet policyQualifiers, string validPolicy, bool critical)
		{
			ArrayList arrayList = new ArrayList();
			if (children != null)
			{
				arrayList.AddRange(children);
			}
			this.mChildren = arrayList;
			this.mDepth = depth;
			this.mExpectedPolicies = expectedPolicies;
			this.mParent = parent;
			this.mPolicyQualifiers = policyQualifiers;
			this.mValidPolicy = validPolicy;
			this.mCritical = critical;
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x0011E890 File Offset: 0x0011D890
		public virtual void AddChild(PkixPolicyNode child)
		{
			child.Parent = this;
			this.mChildren.Add(child);
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x0011E8A6 File Offset: 0x0011D8A6
		public virtual void RemoveChild(PkixPolicyNode child)
		{
			this.mChildren.Remove(child);
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x0011E8B4 File Offset: 0x0011D8B4
		public override string ToString()
		{
			return this.ToString("");
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x0011E8C4 File Offset: 0x0011D8C4
		public virtual string ToString(string indent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(indent);
			stringBuilder.Append(this.mValidPolicy);
			stringBuilder.Append(" {");
			stringBuilder.Append(Platform.NewLine);
			foreach (object obj in this.mChildren)
			{
				PkixPolicyNode pkixPolicyNode = (PkixPolicyNode)obj;
				stringBuilder.Append(pkixPolicyNode.ToString(indent + "    "));
			}
			stringBuilder.Append(indent);
			stringBuilder.Append("}");
			stringBuilder.Append(Platform.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x0011E988 File Offset: 0x0011D988
		public virtual object Clone()
		{
			return this.Copy();
		}

		// Token: 0x06002E62 RID: 11874 RVA: 0x0011E990 File Offset: 0x0011D990
		public virtual PkixPolicyNode Copy()
		{
			PkixPolicyNode pkixPolicyNode = new PkixPolicyNode(new ArrayList(), this.mDepth, new HashSet(this.mExpectedPolicies), null, new HashSet(this.mPolicyQualifiers), this.mValidPolicy, this.mCritical);
			foreach (object obj in this.mChildren)
			{
				PkixPolicyNode pkixPolicyNode2 = (PkixPolicyNode)obj;
				PkixPolicyNode pkixPolicyNode3 = pkixPolicyNode2.Copy();
				pkixPolicyNode3.Parent = pkixPolicyNode;
				pkixPolicyNode.AddChild(pkixPolicyNode3);
			}
			return pkixPolicyNode;
		}

		// Token: 0x04002003 RID: 8195
		protected IList mChildren;

		// Token: 0x04002004 RID: 8196
		protected int mDepth;

		// Token: 0x04002005 RID: 8197
		protected ISet mExpectedPolicies;

		// Token: 0x04002006 RID: 8198
		protected PkixPolicyNode mParent;

		// Token: 0x04002007 RID: 8199
		protected ISet mPolicyQualifiers;

		// Token: 0x04002008 RID: 8200
		protected string mValidPolicy;

		// Token: 0x04002009 RID: 8201
		protected bool mCritical;
	}
}
