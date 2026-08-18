using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.ServiceModel.Description;
using System.Xml;
using System.Xml.Linq;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000030 RID: 48
	public class FindCriteria
	{
		// Token: 0x06000288 RID: 648 RVA: 0x00007E51 File Offset: 0x00006051
		public FindCriteria()
		{
			this.Initialize(null, DiscoveryDefaults.ScopeMatchBy);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00007E68 File Offset: 0x00006068
		public FindCriteria(Type contractType)
		{
			if (contractType == null)
			{
				throw FxTrace.Exception.ArgumentNull("contractType");
			}
			this.Initialize(new ContractTypeNameCollection
			{
				FindCriteria.GetContractTypeName(contractType)
			}, DiscoveryDefaults.ScopeMatchBy);
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00007EB2 File Offset: 0x000060B2
		public Collection<XmlQualifiedName> ContractTypeNames
		{
			get
			{
				if (this.contractTypeNames == null)
				{
					this.contractTypeNames = new ContractTypeNameCollection();
				}
				return this.contractTypeNames;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00007ECD File Offset: 0x000060CD
		public Collection<XElement> Extensions
		{
			get
			{
				if (this.extensions == null)
				{
					this.extensions = new NonNullItemCollection<XElement>();
				}
				return this.extensions;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00007EE8 File Offset: 0x000060E8
		// (set) Token: 0x0600028D RID: 653 RVA: 0x00007EF0 File Offset: 0x000060F0
		public Uri ScopeMatchBy
		{
			get
			{
				return this.scopeMatchBy;
			}
			set
			{
				if (value == null)
				{
					throw FxTrace.Exception.ArgumentNull("value");
				}
				this.scopeMatchBy = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00007F12 File Offset: 0x00006112
		public Collection<Uri> Scopes
		{
			get
			{
				if (this.scopes == null)
				{
					this.scopes = new ScopeCollection();
				}
				return this.scopes;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00007F2D File Offset: 0x0000612D
		// (set) Token: 0x06000290 RID: 656 RVA: 0x00007F35 File Offset: 0x00006135
		public int MaxResults
		{
			get
			{
				return this.maxResults;
			}
			set
			{
				if (value <= 0)
				{
					throw FxTrace.Exception.ArgumentOutOfRange("value", value, SR.DiscoveryFindMaxResultsLessThanZero);
				}
				this.maxResults = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00007F5D File Offset: 0x0000615D
		// (set) Token: 0x06000292 RID: 658 RVA: 0x00007F65 File Offset: 0x00006165
		public TimeSpan Duration
		{
			get
			{
				return this.duration;
			}
			set
			{
				if (value.CompareTo(TimeSpan.Zero) <= 0)
				{
					throw FxTrace.Exception.ArgumentOutOfRange("duration", value, SR.DiscoveryFindDurationLessThanZero);
				}
				this.duration = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00007F98 File Offset: 0x00006198
		internal Collection<Uri> InternalScopes
		{
			get
			{
				return this.scopes;
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00007FA0 File Offset: 0x000061A0
		public static FindCriteria CreateMetadataExchangeEndpointCriteria()
		{
			return new FindCriteria
			{
				ContractTypeNames = 
				{
					EndpointDiscoveryMetadata.MetadataContractName
				}
			};
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00007FC4 File Offset: 0x000061C4
		public static FindCriteria CreateMetadataExchangeEndpointCriteria(Type contractType)
		{
			FindCriteria findCriteria = FindCriteria.CreateMetadataExchangeEndpointCriteria();
			findCriteria.Scopes.Add(FindCriteria.GetContractTypeNameScope(FindCriteria.GetContractTypeName(contractType)));
			return findCriteria;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00007FF0 File Offset: 0x000061F0
		public static FindCriteria CreateMetadataExchangeEndpointCriteria(IEnumerable<XmlQualifiedName> contractTypeNames)
		{
			if (contractTypeNames == null)
			{
				throw FxTrace.Exception.ArgumentNull("contractTypeNames");
			}
			FindCriteria findCriteria = FindCriteria.CreateMetadataExchangeEndpointCriteria();
			foreach (XmlQualifiedName xmlQualifiedName in contractTypeNames)
			{
				if (xmlQualifiedName == null)
				{
					throw FxTrace.Exception.ArgumentNull("item");
				}
				findCriteria.Scopes.Add(FindCriteria.GetContractTypeNameScope(xmlQualifiedName));
			}
			return findCriteria;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00008078 File Offset: 0x00006278
		public bool IsMatch(EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			if (endpointDiscoveryMetadata == null)
			{
				throw FxTrace.Exception.ArgumentNull("endpointDiscoveryMetadata");
			}
			return this.IsMatch(endpointDiscoveryMetadata, ScopeCompiler.CompileMatchCriteria(this.scopes, this.scopeMatchBy));
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000080A5 File Offset: 0x000062A5
		internal bool IsMatch(EndpointDiscoveryMetadata endpointDiscoveryMetadata, CompiledScopeCriteria[] compiledScopeMatchCriterias)
		{
			return FindCriteria.MatchTypes(endpointDiscoveryMetadata, this.contractTypeNames) && FindCriteria.MatchScopes(endpointDiscoveryMetadata, compiledScopeMatchCriterias, this.scopeMatchBy);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000080C4 File Offset: 0x000062C4
		private static bool MatchTypes(EndpointDiscoveryMetadata endpointDiscoveryMetadata, Collection<XmlQualifiedName> contractTypeNames)
		{
			if (contractTypeNames == null || contractTypeNames.Count == 0)
			{
				return true;
			}
			if (endpointDiscoveryMetadata.InternalContractTypeNames == null || endpointDiscoveryMetadata.InternalContractTypeNames.Count == 0)
			{
				return false;
			}
			foreach (XmlQualifiedName item in contractTypeNames)
			{
				if (!endpointDiscoveryMetadata.InternalContractTypeNames.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00008140 File Offset: 0x00006340
		private static bool MatchScopes(EndpointDiscoveryMetadata endpointDiscoveryMetadata, CompiledScopeCriteria[] compiledScopeMatchCriterias, Uri scopeMatchBy)
		{
			if (compiledScopeMatchCriterias == null)
			{
				return scopeMatchBy != FindCriteria.ScopeMatchByNone || endpointDiscoveryMetadata.Scopes.Count == 0;
			}
			if (scopeMatchBy == FindCriteria.ScopeMatchByNone)
			{
				return false;
			}
			string[] array;
			if (endpointDiscoveryMetadata.IsOpen)
			{
				array = endpointDiscoveryMetadata.CompiledScopes;
			}
			else
			{
				array = ScopeCompiler.Compile(endpointDiscoveryMetadata.Scopes);
			}
			if (array == null)
			{
				return false;
			}
			for (int i = 0; i < compiledScopeMatchCriterias.Length; i++)
			{
				if (!ScopeCompiler.IsMatch(compiledScopeMatchCriterias[i], array))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000081BC File Offset: 0x000063BC
		internal void ReadFrom(DiscoveryVersion discoveryVersion, XmlReader reader)
		{
			if (discoveryVersion == null)
			{
				throw FxTrace.Exception.ArgumentNull("discoveryVersion");
			}
			if (reader == null)
			{
				throw FxTrace.Exception.ArgumentNull("reader");
			}
			this.contractTypeNames = null;
			this.scopes = null;
			this.scopeMatchBy = DiscoveryDefaults.ScopeMatchBy;
			this.extensions = null;
			this.duration = TimeSpan.MaxValue;
			this.maxResults = int.MaxValue;
			reader.MoveToContent();
			if (reader.IsEmptyElement)
			{
				reader.Read();
				return;
			}
			int depth = reader.Depth;
			reader.ReadStartElement();
			if (reader.IsStartElement("Types", discoveryVersion.Namespace))
			{
				this.contractTypeNames = new ContractTypeNameCollection();
				SerializationUtility.ReadContractTypeNames(this.contractTypeNames, reader);
			}
			if (reader.IsStartElement("Scopes", discoveryVersion.Namespace))
			{
				this.scopes = new ScopeCollection();
				Uri uri = SerializationUtility.ReadScopes(this.scopes, reader);
				if (uri != null)
				{
					this.scopeMatchBy = discoveryVersion.Implementation.ToVersionIndependentScopeMatchBy(uri);
				}
			}
			for (;;)
			{
				reader.MoveToContent();
				if (reader.NodeType == XmlNodeType.EndElement && reader.Depth == depth)
				{
					break;
				}
				if (reader.IsStartElement("MaxResults", "http://schemas.microsoft.com/ws/2008/06/discovery"))
				{
					this.maxResults = SerializationUtility.ReadMaxResults(reader);
				}
				else if (reader.IsStartElement("Duration", "http://schemas.microsoft.com/ws/2008/06/discovery"))
				{
					this.duration = SerializationUtility.ReadDuration(reader);
				}
				else if (reader.IsStartElement())
				{
					XElement item = XNode.ReadFrom(reader) as XElement;
					this.Extensions.Add(item);
				}
				else
				{
					reader.Read();
				}
			}
			reader.ReadEndElement();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00008348 File Offset: 0x00006548
		internal void WriteTo(DiscoveryVersion discoveryVersion, XmlWriter writer)
		{
			if (discoveryVersion == null)
			{
				throw FxTrace.Exception.ArgumentNull("discoveryVersion");
			}
			if (writer == null)
			{
				throw FxTrace.Exception.ArgumentNull("writer");
			}
			SerializationUtility.WriteContractTypeNames(discoveryVersion, this.contractTypeNames, writer);
			SerializationUtility.WriteScopes(discoveryVersion, this.scopes, this.scopeMatchBy, writer);
			if (this.maxResults != 2147483647)
			{
				writer.WriteElementString("MaxResults", "http://schemas.microsoft.com/ws/2008/06/discovery", this.maxResults.ToString(CultureInfo.InvariantCulture));
			}
			if (this.duration != TimeSpan.MaxValue)
			{
				writer.WriteElementString("Duration", "http://schemas.microsoft.com/ws/2008/06/discovery", XmlConvert.ToString(this.Duration));
			}
			if (this.extensions != null)
			{
				foreach (XElement xelement in this.Extensions)
				{
					xelement.WriteTo(writer);
				}
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00008440 File Offset: 0x00006640
		internal static XmlQualifiedName GetContractTypeName(Type contractType)
		{
			if (contractType == null)
			{
				throw FxTrace.Exception.ArgumentNull("contractType");
			}
			ContractDescription contract = ContractDescription.GetContract(contractType);
			return new XmlQualifiedName(contract.Name, contract.Namespace);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000847E File Offset: 0x0000667E
		internal static Uri GetContractTypeNameScope(XmlQualifiedName contractTypeName)
		{
			return new Uri(string.Format(CultureInfo.InvariantCulture, "urn:{0}", new object[]
			{
				contractTypeName.ToString()
			}));
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000084A4 File Offset: 0x000066A4
		internal FindCriteria Clone()
		{
			FindCriteria findCriteria = new FindCriteria();
			foreach (Uri item in this.Scopes)
			{
				findCriteria.Scopes.Add(item);
			}
			foreach (XmlQualifiedName xmlQualifiedName in this.ContractTypeNames)
			{
				findCriteria.ContractTypeNames.Add(new XmlQualifiedName(xmlQualifiedName.Name, xmlQualifiedName.Namespace));
			}
			foreach (XElement other in this.Extensions)
			{
				findCriteria.Extensions.Add(new XElement(other));
			}
			findCriteria.ScopeMatchBy = this.ScopeMatchBy;
			findCriteria.Duration = this.Duration;
			findCriteria.MaxResults = this.MaxResults;
			return findCriteria;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000085C4 File Offset: 0x000067C4
		private void Initialize(ContractTypeNameCollection contractTypeNames, Uri scopeMatchBy)
		{
			this.contractTypeNames = contractTypeNames;
			this.scopeMatchBy = scopeMatchBy;
			this.maxResults = int.MaxValue;
			this.duration = DiscoveryDefaults.DiscoveryOperationDuration;
		}

		// Token: 0x04000094 RID: 148
		public static readonly Uri ScopeMatchByExact = new Uri("http://schemas.microsoft.com/ws/2008/06/discovery/strcmp0");

		// Token: 0x04000095 RID: 149
		public static readonly Uri ScopeMatchByLdap = new Uri("http://schemas.microsoft.com/ws/2008/06/discovery/ldap");

		// Token: 0x04000096 RID: 150
		public static readonly Uri ScopeMatchByPrefix = new Uri("http://schemas.microsoft.com/ws/2008/06/discovery/rfc");

		// Token: 0x04000097 RID: 151
		public static readonly Uri ScopeMatchByUuid = new Uri("http://schemas.microsoft.com/ws/2008/06/discovery/uuid");

		// Token: 0x04000098 RID: 152
		public static readonly Uri ScopeMatchByNone = new Uri("http://schemas.microsoft.com/ws/2008/06/discovery/none");

		// Token: 0x04000099 RID: 153
		private ContractTypeNameCollection contractTypeNames;

		// Token: 0x0400009A RID: 154
		private NonNullItemCollection<XElement> extensions;

		// Token: 0x0400009B RID: 155
		private Uri scopeMatchBy;

		// Token: 0x0400009C RID: 156
		private ScopeCollection scopes;

		// Token: 0x0400009D RID: 157
		private int maxResults;

		// Token: 0x0400009E RID: 158
		private TimeSpan duration;
	}
}
