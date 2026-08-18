using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x02000748 RID: 1864
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
	public sealed class SoapTypeAttribute : SoapAttribute
	{
		// Token: 0x06004292 RID: 17042 RVA: 0x000E277F File Offset: 0x000E177F
		internal bool IsInteropXmlElement()
		{
			return (this._explicitlySet & (SoapTypeAttribute.ExplicitlySet.XmlElementName | SoapTypeAttribute.ExplicitlySet.XmlNamespace)) != SoapTypeAttribute.ExplicitlySet.None;
		}

		// Token: 0x06004293 RID: 17043 RVA: 0x000E278F File Offset: 0x000E178F
		internal bool IsInteropXmlType()
		{
			return (this._explicitlySet & (SoapTypeAttribute.ExplicitlySet.XmlTypeName | SoapTypeAttribute.ExplicitlySet.XmlTypeNamespace)) != SoapTypeAttribute.ExplicitlySet.None;
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06004294 RID: 17044 RVA: 0x000E27A0 File Offset: 0x000E17A0
		// (set) Token: 0x06004295 RID: 17045 RVA: 0x000E27A8 File Offset: 0x000E17A8
		public SoapOption SoapOptions
		{
			get
			{
				return this._SoapOptions;
			}
			set
			{
				this._SoapOptions = value;
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06004296 RID: 17046 RVA: 0x000E27B1 File Offset: 0x000E17B1
		// (set) Token: 0x06004297 RID: 17047 RVA: 0x000E27DF File Offset: 0x000E17DF
		public string XmlElementName
		{
			get
			{
				if (this._XmlElementName == null && this.ReflectInfo != null)
				{
					this._XmlElementName = SoapTypeAttribute.GetTypeName((Type)this.ReflectInfo);
				}
				return this._XmlElementName;
			}
			set
			{
				this._XmlElementName = value;
				this._explicitlySet |= SoapTypeAttribute.ExplicitlySet.XmlElementName;
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06004298 RID: 17048 RVA: 0x000E27F6 File Offset: 0x000E17F6
		// (set) Token: 0x06004299 RID: 17049 RVA: 0x000E281A File Offset: 0x000E181A
		public override string XmlNamespace
		{
			get
			{
				if (this.ProtXmlNamespace == null && this.ReflectInfo != null)
				{
					this.ProtXmlNamespace = this.XmlTypeNamespace;
				}
				return this.ProtXmlNamespace;
			}
			set
			{
				this.ProtXmlNamespace = value;
				this._explicitlySet |= SoapTypeAttribute.ExplicitlySet.XmlNamespace;
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x0600429A RID: 17050 RVA: 0x000E2831 File Offset: 0x000E1831
		// (set) Token: 0x0600429B RID: 17051 RVA: 0x000E285F File Offset: 0x000E185F
		public string XmlTypeName
		{
			get
			{
				if (this._XmlTypeName == null && this.ReflectInfo != null)
				{
					this._XmlTypeName = SoapTypeAttribute.GetTypeName((Type)this.ReflectInfo);
				}
				return this._XmlTypeName;
			}
			set
			{
				this._XmlTypeName = value;
				this._explicitlySet |= SoapTypeAttribute.ExplicitlySet.XmlTypeName;
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x0600429C RID: 17052 RVA: 0x000E2876 File Offset: 0x000E1876
		// (set) Token: 0x0600429D RID: 17053 RVA: 0x000E28A5 File Offset: 0x000E18A5
		public string XmlTypeNamespace
		{
			get
			{
				if (this._XmlTypeNamespace == null && this.ReflectInfo != null)
				{
					this._XmlTypeNamespace = XmlNamespaceEncoder.GetXmlNamespaceForTypeNamespace((Type)this.ReflectInfo, null);
				}
				return this._XmlTypeNamespace;
			}
			set
			{
				this._XmlTypeNamespace = value;
				this._explicitlySet |= SoapTypeAttribute.ExplicitlySet.XmlTypeNamespace;
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x0600429E RID: 17054 RVA: 0x000E28BC File Offset: 0x000E18BC
		// (set) Token: 0x0600429F RID: 17055 RVA: 0x000E28C4 File Offset: 0x000E18C4
		public XmlFieldOrderOption XmlFieldOrder
		{
			get
			{
				return this._XmlFieldOrder;
			}
			set
			{
				this._XmlFieldOrder = value;
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x060042A0 RID: 17056 RVA: 0x000E28CD File Offset: 0x000E18CD
		// (set) Token: 0x060042A1 RID: 17057 RVA: 0x000E28D0 File Offset: 0x000E18D0
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

		// Token: 0x060042A2 RID: 17058 RVA: 0x000E28E4 File Offset: 0x000E18E4
		private static string GetTypeName(Type t)
		{
			if (!t.IsNested)
			{
				return t.Name;
			}
			string fullName = t.FullName;
			string @namespace = t.Namespace;
			if (@namespace == null || @namespace.Length == 0)
			{
				return fullName;
			}
			return fullName.Substring(@namespace.Length + 1);
		}

		// Token: 0x04002171 RID: 8561
		private SoapTypeAttribute.ExplicitlySet _explicitlySet;

		// Token: 0x04002172 RID: 8562
		private SoapOption _SoapOptions;

		// Token: 0x04002173 RID: 8563
		private string _XmlElementName;

		// Token: 0x04002174 RID: 8564
		private string _XmlTypeName;

		// Token: 0x04002175 RID: 8565
		private string _XmlTypeNamespace;

		// Token: 0x04002176 RID: 8566
		private XmlFieldOrderOption _XmlFieldOrder;

		// Token: 0x02000749 RID: 1865
		[Flags]
		[Serializable]
		private enum ExplicitlySet
		{
			// Token: 0x04002178 RID: 8568
			None = 0,
			// Token: 0x04002179 RID: 8569
			XmlElementName = 1,
			// Token: 0x0400217A RID: 8570
			XmlNamespace = 2,
			// Token: 0x0400217B RID: 8571
			XmlTypeName = 4,
			// Token: 0x0400217C RID: 8572
			XmlTypeNamespace = 8
		}
	}
}
