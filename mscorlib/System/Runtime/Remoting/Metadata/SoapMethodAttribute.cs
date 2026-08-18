using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x0200074A RID: 1866
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class SoapMethodAttribute : SoapAttribute
	{
		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x060042A4 RID: 17060 RVA: 0x000E2933 File Offset: 0x000E1933
		internal bool SoapActionExplicitySet
		{
			get
			{
				return this._bSoapActionExplicitySet;
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x060042A5 RID: 17061 RVA: 0x000E293B File Offset: 0x000E193B
		// (set) Token: 0x060042A6 RID: 17062 RVA: 0x000E2971 File Offset: 0x000E1971
		public string SoapAction
		{
			get
			{
				if (this._SoapAction == null)
				{
					this._SoapAction = this.XmlTypeNamespaceOfDeclaringType + "#" + ((MemberInfo)this.ReflectInfo).Name;
				}
				return this._SoapAction;
			}
			set
			{
				this._SoapAction = value;
				this._bSoapActionExplicitySet = true;
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x060042A7 RID: 17063 RVA: 0x000E2981 File Offset: 0x000E1981
		// (set) Token: 0x060042A8 RID: 17064 RVA: 0x000E2984 File Offset: 0x000E1984
		public override bool UseAttribute
		{
			get
			{
				return false;
			}
			set
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Attribute_UseAttributeNotsettable"));
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x060042A9 RID: 17065 RVA: 0x000E2995 File Offset: 0x000E1995
		// (set) Token: 0x060042AA RID: 17066 RVA: 0x000E29B1 File Offset: 0x000E19B1
		public override string XmlNamespace
		{
			get
			{
				if (this.ProtXmlNamespace == null)
				{
					this.ProtXmlNamespace = this.XmlTypeNamespaceOfDeclaringType;
				}
				return this.ProtXmlNamespace;
			}
			set
			{
				this.ProtXmlNamespace = value;
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x060042AB RID: 17067 RVA: 0x000E29BA File Offset: 0x000E19BA
		// (set) Token: 0x060042AC RID: 17068 RVA: 0x000E29F2 File Offset: 0x000E19F2
		public string ResponseXmlElementName
		{
			get
			{
				if (this._responseXmlElementName == null && this.ReflectInfo != null)
				{
					this._responseXmlElementName = ((MemberInfo)this.ReflectInfo).Name + "Response";
				}
				return this._responseXmlElementName;
			}
			set
			{
				this._responseXmlElementName = value;
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x060042AD RID: 17069 RVA: 0x000E29FB File Offset: 0x000E19FB
		// (set) Token: 0x060042AE RID: 17070 RVA: 0x000E2A17 File Offset: 0x000E1A17
		public string ResponseXmlNamespace
		{
			get
			{
				if (this._responseXmlNamespace == null)
				{
					this._responseXmlNamespace = this.XmlNamespace;
				}
				return this._responseXmlNamespace;
			}
			set
			{
				this._responseXmlNamespace = value;
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x060042AF RID: 17071 RVA: 0x000E2A20 File Offset: 0x000E1A20
		// (set) Token: 0x060042B0 RID: 17072 RVA: 0x000E2A3B File Offset: 0x000E1A3B
		public string ReturnXmlElementName
		{
			get
			{
				if (this._returnXmlElementName == null)
				{
					this._returnXmlElementName = "return";
				}
				return this._returnXmlElementName;
			}
			set
			{
				this._returnXmlElementName = value;
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x060042B1 RID: 17073 RVA: 0x000E2A44 File Offset: 0x000E1A44
		private string XmlTypeNamespaceOfDeclaringType
		{
			get
			{
				if (this.ReflectInfo != null)
				{
					Type declaringType = ((MemberInfo)this.ReflectInfo).DeclaringType;
					return XmlNamespaceEncoder.GetXmlNamespaceForType(declaringType, null);
				}
				return null;
			}
		}

		// Token: 0x0400217D RID: 8573
		private string _SoapAction;

		// Token: 0x0400217E RID: 8574
		private string _responseXmlElementName;

		// Token: 0x0400217F RID: 8575
		private string _responseXmlNamespace;

		// Token: 0x04002180 RID: 8576
		private string _returnXmlElementName;

		// Token: 0x04002181 RID: 8577
		private bool _bSoapActionExplicitySet;
	}
}
