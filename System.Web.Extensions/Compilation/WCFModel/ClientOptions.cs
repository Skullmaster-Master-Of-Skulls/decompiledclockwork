using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x0200000D RID: 13
	internal class ClientOptions
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003604 File Offset: 0x00001804
		// (set) Token: 0x06000075 RID: 117 RVA: 0x0000360C File Offset: 0x0000180C
		[XmlElement]
		public bool GenerateAsynchronousMethods
		{
			get
			{
				return this.m_GenerateAsynchronousMethods;
			}
			set
			{
				this.m_GenerateAsynchronousMethods = value;
				if (this.GenerateAsynchronousMethods)
				{
					this.GenerateTaskBasedAsynchronousMethod = false;
				}
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003624 File Offset: 0x00001824
		// (set) Token: 0x06000077 RID: 119 RVA: 0x0000362C File Offset: 0x0000182C
		[XmlElement]
		public bool GenerateTaskBasedAsynchronousMethod
		{
			get
			{
				return this.m_GenerateTaskBasedAsynchronousMethod;
			}
			set
			{
				this.m_GenerateTaskBasedAsynchronousMethod = value;
				this.m_GenerateTaskBasedAsynchronousMethodSpecified = value;
				if (this.GenerateTaskBasedAsynchronousMethod)
				{
					this.GenerateAsynchronousMethods = false;
				}
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000364B File Offset: 0x0000184B
		[XmlIgnore]
		public bool GenerateTaskBasedAsynchronousMethodSpecified
		{
			get
			{
				return this.m_GenerateTaskBasedAsynchronousMethodSpecified;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003653 File Offset: 0x00001853
		// (set) Token: 0x0600007A RID: 122 RVA: 0x0000365B File Offset: 0x0000185B
		[XmlElement]
		public bool EnableDataBinding
		{
			get
			{
				return this.m_EnableDataBinding;
			}
			set
			{
				this.m_EnableDataBinding = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003664 File Offset: 0x00001864
		[XmlArray(ElementName = "ExcludedTypes")]
		[XmlArrayItem("ExcludedType", typeof(ReferencedType))]
		public List<ReferencedType> ExcludedTypeList
		{
			get
			{
				if (this.m_ExcludedTypeList == null)
				{
					this.m_ExcludedTypeList = new List<ReferencedType>();
				}
				return this.m_ExcludedTypeList;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000367F File Offset: 0x0000187F
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00003687 File Offset: 0x00001887
		[XmlElement]
		public bool ImportXmlTypes
		{
			get
			{
				return this.m_ImportXmlTypes;
			}
			set
			{
				this.m_ImportXmlTypes = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003690 File Offset: 0x00001890
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003698 File Offset: 0x00001898
		[XmlElement]
		public bool GenerateInternalTypes
		{
			get
			{
				return this.m_GenerateInternalTypes;
			}
			set
			{
				this.m_GenerateInternalTypes = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000036A1 File Offset: 0x000018A1
		// (set) Token: 0x06000081 RID: 129 RVA: 0x000036A9 File Offset: 0x000018A9
		[XmlElement]
		public bool GenerateMessageContracts
		{
			get
			{
				return this.m_GenerateMessageContracts;
			}
			set
			{
				this.m_GenerateMessageContracts = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000036B2 File Offset: 0x000018B2
		[XmlArray(ElementName = "NamespaceMappings")]
		[XmlArrayItem("NamespaceMapping", typeof(NamespaceMapping))]
		public List<NamespaceMapping> NamespaceMappingList
		{
			get
			{
				if (this.m_NamespaceMappingList == null)
				{
					this.m_NamespaceMappingList = new List<NamespaceMapping>();
				}
				return this.m_NamespaceMappingList;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000036CD File Offset: 0x000018CD
		[XmlArray(ElementName = "CollectionMappings")]
		[XmlArrayItem("CollectionMapping", typeof(ReferencedCollectionType))]
		public List<ReferencedCollectionType> CollectionMappingList
		{
			get
			{
				if (this.m_CollectionMappingList == null)
				{
					this.m_CollectionMappingList = new List<ReferencedCollectionType>();
				}
				return this.m_CollectionMappingList;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000036E8 File Offset: 0x000018E8
		// (set) Token: 0x06000085 RID: 133 RVA: 0x000036F0 File Offset: 0x000018F0
		[XmlElement]
		public bool GenerateSerializableTypes
		{
			get
			{
				return this.m_GenerateSerializableTypes;
			}
			set
			{
				this.m_GenerateSerializableTypes = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000036F9 File Offset: 0x000018F9
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00003701 File Offset: 0x00001901
		[XmlElement]
		public ClientOptions.ProxySerializerType Serializer
		{
			get
			{
				return this.m_Serializer;
			}
			set
			{
				this.m_Serializer = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000370A File Offset: 0x0000190A
		// (set) Token: 0x06000089 RID: 137 RVA: 0x0000371C File Offset: 0x0000191C
		[XmlElement]
		public bool UseSerializerForFaults
		{
			get
			{
				return this.m_UseSerializerForFaultsSpecified && this.m_UseSerializerForFaults;
			}
			set
			{
				this.m_UseSerializerForFaultsSpecified = true;
				this.m_UseSerializerForFaults = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000372C File Offset: 0x0000192C
		[XmlIgnore]
		public bool UseSerializerForFaultsSpecified
		{
			get
			{
				return this.m_UseSerializerForFaultsSpecified;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003734 File Offset: 0x00001934
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003746 File Offset: 0x00001946
		[XmlElement]
		public bool Wrapped
		{
			get
			{
				return this.m_WrappedSpecified && this.m_Wrapped;
			}
			set
			{
				this.m_WrappedSpecified = true;
				this.m_Wrapped = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003756 File Offset: 0x00001956
		[XmlIgnore]
		public bool WrappedSpecified
		{
			get
			{
				return this.m_WrappedSpecified;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000375E File Offset: 0x0000195E
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00003766 File Offset: 0x00001966
		[XmlElement]
		public bool ReferenceAllAssemblies
		{
			get
			{
				return this.m_ReferenceAllAssemblies;
			}
			set
			{
				this.m_ReferenceAllAssemblies = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000376F File Offset: 0x0000196F
		[XmlArray(ElementName = "ReferencedAssemblies")]
		[XmlArrayItem("ReferencedAssembly", typeof(ReferencedAssembly))]
		public List<ReferencedAssembly> ReferencedAssemblyList
		{
			get
			{
				if (this.m_ReferencedAssemblyList == null)
				{
					this.m_ReferencedAssemblyList = new List<ReferencedAssembly>();
				}
				return this.m_ReferencedAssemblyList;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000378A File Offset: 0x0000198A
		[XmlArray(ElementName = "ReferencedDataContractTypes")]
		[XmlArrayItem("ReferencedDataContractType", typeof(ReferencedType))]
		public List<ReferencedType> ReferencedDataContractTypeList
		{
			get
			{
				if (this.m_ReferencedDataContractTypeList == null)
				{
					this.m_ReferencedDataContractTypeList = new List<ReferencedType>();
				}
				return this.m_ReferencedDataContractTypeList;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000037A5 File Offset: 0x000019A5
		[XmlArray(ElementName = "ServiceContractMappings")]
		[XmlArrayItem("ServiceContractMapping", typeof(ContractMapping))]
		public List<ContractMapping> ServiceContractMappingList
		{
			get
			{
				if (this.m_ServiceContractMappingList == null)
				{
					this.m_ServiceContractMappingList = new List<ContractMapping>();
				}
				return this.m_ServiceContractMappingList;
			}
		}

		// Token: 0x0400001F RID: 31
		private bool m_GenerateAsynchronousMethods;

		// Token: 0x04000020 RID: 32
		private bool m_GenerateTaskBasedAsynchronousMethod;

		// Token: 0x04000021 RID: 33
		private bool m_GenerateTaskBasedAsynchronousMethodSpecified;

		// Token: 0x04000022 RID: 34
		private bool m_EnableDataBinding;

		// Token: 0x04000023 RID: 35
		private List<ReferencedType> m_ExcludedTypeList;

		// Token: 0x04000024 RID: 36
		private bool m_ImportXmlTypes;

		// Token: 0x04000025 RID: 37
		private bool m_GenerateInternalTypes;

		// Token: 0x04000026 RID: 38
		private bool m_GenerateMessageContracts;

		// Token: 0x04000027 RID: 39
		private List<NamespaceMapping> m_NamespaceMappingList;

		// Token: 0x04000028 RID: 40
		private List<ReferencedCollectionType> m_CollectionMappingList;

		// Token: 0x04000029 RID: 41
		private bool m_GenerateSerializableTypes;

		// Token: 0x0400002A RID: 42
		private ClientOptions.ProxySerializerType m_Serializer;

		// Token: 0x0400002B RID: 43
		private bool m_ReferenceAllAssemblies;

		// Token: 0x0400002C RID: 44
		private List<ReferencedAssembly> m_ReferencedAssemblyList;

		// Token: 0x0400002D RID: 45
		private List<ReferencedType> m_ReferencedDataContractTypeList;

		// Token: 0x0400002E RID: 46
		private List<ContractMapping> m_ServiceContractMappingList;

		// Token: 0x0400002F RID: 47
		private bool m_UseSerializerForFaults;

		// Token: 0x04000030 RID: 48
		private bool m_UseSerializerForFaultsSpecified;

		// Token: 0x04000031 RID: 49
		private bool m_Wrapped;

		// Token: 0x04000032 RID: 50
		private bool m_WrappedSpecified;

		// Token: 0x02000129 RID: 297
		public enum ProxySerializerType
		{
			// Token: 0x04000455 RID: 1109
			[XmlEnum(Name = "Auto")]
			Auto,
			// Token: 0x04000456 RID: 1110
			[XmlEnum(Name = "DataContractSerializer")]
			DataContractSerializer,
			// Token: 0x04000457 RID: 1111
			[XmlEnum(Name = "XmlSerializer")]
			XmlSerializer
		}
	}
}
