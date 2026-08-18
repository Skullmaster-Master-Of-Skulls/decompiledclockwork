using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;
using System.Text;

namespace System.Security.Policy
{
	// Token: 0x020004B4 RID: 1204
	[ComVisible(true)]
	[Serializable]
	public sealed class PolicyStatement : ISecurityEncodable, ISecurityPolicyEncodable
	{
		// Token: 0x06002FD8 RID: 12248 RVA: 0x000A3F3C File Offset: 0x000A2F3C
		internal PolicyStatement()
		{
			this.m_permSet = null;
			this.m_attributes = PolicyStatementAttribute.Nothing;
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x000A3F52 File Offset: 0x000A2F52
		public PolicyStatement(PermissionSet permSet) : this(permSet, PolicyStatementAttribute.Nothing)
		{
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x000A3F5C File Offset: 0x000A2F5C
		public PolicyStatement(PermissionSet permSet, PolicyStatementAttribute attributes)
		{
			if (permSet == null)
			{
				this.m_permSet = new PermissionSet(false);
			}
			else
			{
				this.m_permSet = permSet.Copy();
			}
			if (PolicyStatement.ValidProperties(attributes))
			{
				this.m_attributes = attributes;
			}
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x000A3F90 File Offset: 0x000A2F90
		private PolicyStatement(PermissionSet permSet, PolicyStatementAttribute attributes, bool copy)
		{
			if (permSet != null)
			{
				if (copy)
				{
					this.m_permSet = permSet.Copy();
				}
				else
				{
					this.m_permSet = permSet;
				}
			}
			else
			{
				this.m_permSet = new PermissionSet(false);
			}
			this.m_attributes = attributes;
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06002FDC RID: 12252 RVA: 0x000A3FC8 File Offset: 0x000A2FC8
		// (set) Token: 0x06002FDD RID: 12253 RVA: 0x000A4004 File Offset: 0x000A3004
		public PermissionSet PermissionSet
		{
			get
			{
				PermissionSet result;
				lock (this)
				{
					result = this.m_permSet.Copy();
				}
				return result;
			}
			set
			{
				lock (this)
				{
					if (value == null)
					{
						this.m_permSet = new PermissionSet(false);
					}
					else
					{
						this.m_permSet = value.Copy();
					}
				}
			}
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x000A4050 File Offset: 0x000A3050
		internal void SetPermissionSetNoCopy(PermissionSet permSet)
		{
			this.m_permSet = permSet;
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x000A405C File Offset: 0x000A305C
		internal PermissionSet GetPermissionSetNoCopy()
		{
			PermissionSet permSet;
			lock (this)
			{
				permSet = this.m_permSet;
			}
			return permSet;
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06002FE0 RID: 12256 RVA: 0x000A4094 File Offset: 0x000A3094
		// (set) Token: 0x06002FE1 RID: 12257 RVA: 0x000A409C File Offset: 0x000A309C
		public PolicyStatementAttribute Attributes
		{
			get
			{
				return this.m_attributes;
			}
			set
			{
				if (PolicyStatement.ValidProperties(value))
				{
					this.m_attributes = value;
				}
			}
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x000A40B0 File Offset: 0x000A30B0
		public PolicyStatement Copy()
		{
			PolicyStatement policyStatement = new PolicyStatement(this.m_permSet, this.Attributes, true);
			if (this.HasDependentEvidence)
			{
				policyStatement.m_dependentEvidence = new List<IDelayEvaluatedEvidence>(this.m_dependentEvidence);
			}
			return policyStatement;
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06002FE3 RID: 12259 RVA: 0x000A40EC File Offset: 0x000A30EC
		public string AttributeString
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = true;
				if (this.GetFlag(1))
				{
					stringBuilder.Append("Exclusive");
					flag = false;
				}
				if (this.GetFlag(2))
				{
					if (!flag)
					{
						stringBuilder.Append(" ");
					}
					stringBuilder.Append("LevelFinal");
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x000A4142 File Offset: 0x000A3142
		private static bool ValidProperties(PolicyStatementAttribute attributes)
		{
			if ((attributes & ~(PolicyStatementAttribute.Exclusive | PolicyStatementAttribute.LevelFinal)) == PolicyStatementAttribute.Nothing)
			{
				return true;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"));
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x000A415B File Offset: 0x000A315B
		private bool GetFlag(int flag)
		{
			return (flag & (int)this.m_attributes) != 0;
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06002FE6 RID: 12262 RVA: 0x000A416B File Offset: 0x000A316B
		internal IEnumerable<IDelayEvaluatedEvidence> DependentEvidence
		{
			get
			{
				return this.m_dependentEvidence.AsReadOnly();
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06002FE7 RID: 12263 RVA: 0x000A4178 File Offset: 0x000A3178
		internal bool HasDependentEvidence
		{
			get
			{
				return this.m_dependentEvidence != null && this.m_dependentEvidence.Count > 0;
			}
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x000A4192 File Offset: 0x000A3192
		internal void AddDependentEvidence(IDelayEvaluatedEvidence dependentEvidence)
		{
			if (this.m_dependentEvidence == null)
			{
				this.m_dependentEvidence = new List<IDelayEvaluatedEvidence>();
			}
			this.m_dependentEvidence.Add(dependentEvidence);
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x000A41B4 File Offset: 0x000A31B4
		internal void InplaceUnion(PolicyStatement childPolicy)
		{
			if ((this.Attributes & childPolicy.Attributes & PolicyStatementAttribute.Exclusive) == PolicyStatementAttribute.Exclusive)
			{
				throw new PolicyException(Environment.GetResourceString("Policy_MultipleExclusive"));
			}
			if (childPolicy.HasDependentEvidence)
			{
				bool flag = this.m_permSet.IsSubsetOf(childPolicy.GetPermissionSetNoCopy()) && !childPolicy.GetPermissionSetNoCopy().IsSubsetOf(this.m_permSet);
				if (this.HasDependentEvidence || flag)
				{
					if (this.m_dependentEvidence == null)
					{
						this.m_dependentEvidence = new List<IDelayEvaluatedEvidence>();
					}
					this.m_dependentEvidence.AddRange(childPolicy.DependentEvidence);
				}
			}
			if ((childPolicy.Attributes & PolicyStatementAttribute.Exclusive) == PolicyStatementAttribute.Exclusive)
			{
				this.m_permSet = childPolicy.PermissionSet;
				this.Attributes = childPolicy.Attributes;
				return;
			}
			this.m_permSet.InplaceUnion(childPolicy.GetPermissionSetNoCopy());
			this.Attributes |= childPolicy.Attributes;
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x000A428D File Offset: 0x000A328D
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000A4296 File Offset: 0x000A3296
		public void FromXml(SecurityElement et)
		{
			this.FromXml(et, null);
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000A42A0 File Offset: 0x000A32A0
		public SecurityElement ToXml(PolicyLevel level)
		{
			return this.ToXml(level, false);
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000A42AC File Offset: 0x000A32AC
		internal SecurityElement ToXml(PolicyLevel level, bool useInternal)
		{
			SecurityElement securityElement = new SecurityElement("PolicyStatement");
			securityElement.AddAttribute("version", "1");
			if (this.m_attributes != PolicyStatementAttribute.Nothing)
			{
				securityElement.AddAttribute("Attributes", XMLUtil.BitFieldEnumToString(typeof(PolicyStatementAttribute), this.m_attributes));
			}
			lock (this)
			{
				if (this.m_permSet != null)
				{
					if (this.m_permSet is NamedPermissionSet)
					{
						NamedPermissionSet namedPermissionSet = (NamedPermissionSet)this.m_permSet;
						if (level != null && level.GetNamedPermissionSet(namedPermissionSet.Name) != null)
						{
							securityElement.AddAttribute("PermissionSetName", namedPermissionSet.Name);
						}
						else if (useInternal)
						{
							securityElement.AddChild(namedPermissionSet.InternalToXml());
						}
						else
						{
							securityElement.AddChild(namedPermissionSet.ToXml());
						}
					}
					else if (useInternal)
					{
						securityElement.AddChild(this.m_permSet.InternalToXml());
					}
					else
					{
						securityElement.AddChild(this.m_permSet.ToXml());
					}
				}
			}
			return securityElement;
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x000A43B4 File Offset: 0x000A33B4
		public void FromXml(SecurityElement et, PolicyLevel level)
		{
			this.FromXml(et, level, false);
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000A43C0 File Offset: 0x000A33C0
		internal void FromXml(SecurityElement et, PolicyLevel level, bool allowInternalOnly)
		{
			if (et == null)
			{
				throw new ArgumentNullException("et");
			}
			if (!et.Tag.Equals("PolicyStatement"))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidXMLElement"), new object[]
				{
					"PolicyStatement",
					base.GetType().FullName
				}));
			}
			this.m_attributes = PolicyStatementAttribute.Nothing;
			string text = et.Attribute("Attributes");
			if (text != null)
			{
				this.m_attributes = (PolicyStatementAttribute)Enum.Parse(typeof(PolicyStatementAttribute), text);
			}
			lock (this)
			{
				this.m_permSet = null;
				if (level != null)
				{
					string text2 = et.Attribute("PermissionSetName");
					if (text2 != null)
					{
						this.m_permSet = level.GetNamedPermissionSetInternal(text2);
						if (this.m_permSet == null)
						{
							this.m_permSet = new PermissionSet(PermissionState.None);
						}
					}
				}
				if (this.m_permSet == null)
				{
					SecurityElement securityElement = et.SearchForChildByTag("PermissionSet");
					if (securityElement != null)
					{
						string text3 = securityElement.Attribute("class");
						if (text3 != null && (text3.Equals("NamedPermissionSet") || text3.Equals("System.Security.NamedPermissionSet")))
						{
							this.m_permSet = new NamedPermissionSet("DefaultName", PermissionState.None);
						}
						else
						{
							this.m_permSet = new PermissionSet(PermissionState.None);
						}
						try
						{
							this.m_permSet.FromXml(securityElement, allowInternalOnly, true);
							goto IL_152;
						}
						catch
						{
							goto IL_152;
						}
					}
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
				}
				IL_152:
				if (this.m_permSet == null)
				{
					this.m_permSet = new PermissionSet(PermissionState.None);
				}
			}
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x000A455C File Offset: 0x000A355C
		internal void FromXml(SecurityDocument doc, int position, PolicyLevel level, bool allowInternalOnly)
		{
			if (doc == null)
			{
				throw new ArgumentNullException("doc");
			}
			if (!doc.GetTagForElement(position).Equals("PolicyStatement"))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidXMLElement"), new object[]
				{
					"PolicyStatement",
					base.GetType().FullName
				}));
			}
			this.m_attributes = PolicyStatementAttribute.Nothing;
			string attributeForElement = doc.GetAttributeForElement(position, "Attributes");
			if (attributeForElement != null)
			{
				this.m_attributes = (PolicyStatementAttribute)Enum.Parse(typeof(PolicyStatementAttribute), attributeForElement);
			}
			lock (this)
			{
				this.m_permSet = null;
				if (level != null)
				{
					string attributeForElement2 = doc.GetAttributeForElement(position, "PermissionSetName");
					if (attributeForElement2 != null)
					{
						this.m_permSet = level.GetNamedPermissionSetInternal(attributeForElement2);
						if (this.m_permSet == null)
						{
							this.m_permSet = new PermissionSet(PermissionState.None);
						}
					}
				}
				if (this.m_permSet == null)
				{
					ArrayList childrenPositionForElement = doc.GetChildrenPositionForElement(position);
					int num = -1;
					for (int i = 0; i < childrenPositionForElement.Count; i++)
					{
						if (doc.GetTagForElement((int)childrenPositionForElement[i]).Equals("PermissionSet"))
						{
							num = (int)childrenPositionForElement[i];
						}
					}
					if (num == -1)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
					}
					string attributeForElement3 = doc.GetAttributeForElement(num, "class");
					if (attributeForElement3 != null && (attributeForElement3.Equals("NamedPermissionSet") || attributeForElement3.Equals("System.Security.NamedPermissionSet")))
					{
						this.m_permSet = new NamedPermissionSet("DefaultName", PermissionState.None);
					}
					else
					{
						this.m_permSet = new PermissionSet(PermissionState.None);
					}
					this.m_permSet.FromXml(doc, num, allowInternalOnly);
				}
				if (this.m_permSet == null)
				{
					this.m_permSet = new PermissionSet(PermissionState.None);
				}
			}
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x000A4740 File Offset: 0x000A3740
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			PolicyStatement policyStatement = obj as PolicyStatement;
			return policyStatement != null && this.m_attributes == policyStatement.m_attributes && object.Equals(this.m_permSet, policyStatement.m_permSet);
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x000A4780 File Offset: 0x000A3780
		[ComVisible(false)]
		public override int GetHashCode()
		{
			int num = (int)this.m_attributes;
			if (this.m_permSet != null)
			{
				num ^= this.m_permSet.GetHashCode();
			}
			return num;
		}

		// Token: 0x04001855 RID: 6229
		internal PermissionSet m_permSet;

		// Token: 0x04001856 RID: 6230
		[NonSerialized]
		private List<IDelayEvaluatedEvidence> m_dependentEvidence;

		// Token: 0x04001857 RID: 6231
		internal PolicyStatementAttribute m_attributes;
	}
}
