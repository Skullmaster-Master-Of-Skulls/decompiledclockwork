using System;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x02000014 RID: 20
	internal class GeneratedContractType
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00002050 File Offset: 0x00000250
		public GeneratedContractType()
		{
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003B87 File Offset: 0x00001D87
		public GeneratedContractType(string targetNamespace, string portName, string contractType, string configurationName)
		{
			this.m_TargetNamespace = targetNamespace;
			this.m_Name = portName;
			this.m_ContractType = contractType;
			this.m_ConfigurationName = configurationName;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003BAC File Offset: 0x00001DAC
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00003BB4 File Offset: 0x00001DB4
		[XmlAttribute]
		public string TargetNamespace
		{
			get
			{
				return this.m_TargetNamespace;
			}
			set
			{
				this.m_TargetNamespace = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003BBD File Offset: 0x00001DBD
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00003BC5 File Offset: 0x00001DC5
		[XmlAttribute]
		public string Name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003BCE File Offset: 0x00001DCE
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00003BD6 File Offset: 0x00001DD6
		[XmlAttribute]
		public string ContractType
		{
			get
			{
				return this.m_ContractType;
			}
			set
			{
				this.m_ContractType = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003BDF File Offset: 0x00001DDF
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00003BE7 File Offset: 0x00001DE7
		[XmlAttribute]
		public string ConfigurationName
		{
			get
			{
				return this.m_ConfigurationName;
			}
			set
			{
				this.m_ConfigurationName = value;
			}
		}

		// Token: 0x04000045 RID: 69
		private string m_TargetNamespace;

		// Token: 0x04000046 RID: 70
		private string m_Name;

		// Token: 0x04000047 RID: 71
		private string m_ContractType;

		// Token: 0x04000048 RID: 72
		private string m_ConfigurationName;
	}
}
