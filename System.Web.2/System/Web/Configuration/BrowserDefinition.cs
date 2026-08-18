using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.Adapters;
using System.Xml;

namespace System.Web.Configuration
{
	// Token: 0x020006A9 RID: 1705
	internal class BrowserDefinition
	{
		// Token: 0x060052A7 RID: 21159 RVA: 0x00122D70 File Offset: 0x00120F70
		internal static string MakeValidTypeNameFromString(string s)
		{
			if (s == null)
			{
				return s;
			}
			s = s.ToLower(CultureInfo.InvariantCulture);
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while (i < s.Length)
			{
				if (i != 0)
				{
					goto IL_64;
				}
				if (char.IsDigit(s[0]))
				{
					stringBuilder.Append("N");
					goto IL_64;
				}
				if (!char.IsLetter(s[0]))
				{
					goto IL_64;
				}
				stringBuilder.Append(s.Substring(0, 1).ToUpper(CultureInfo.InvariantCulture));
				IL_96:
				i++;
				continue;
				IL_64:
				if (char.IsLetterOrDigit(s[i]) || s[i] == '_')
				{
					stringBuilder.Append(s[i]);
					goto IL_96;
				}
				stringBuilder.Append('A');
				goto IL_96;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060052A8 RID: 21160 RVA: 0x00122E29 File Offset: 0x00121029
		internal BrowserDefinition(XmlNode node) : this(node, false)
		{
		}

		// Token: 0x060052A9 RID: 21161 RVA: 0x00122E34 File Offset: 0x00121034
		internal BrowserDefinition(XmlNode node, bool isDefaultBrowser)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			this._capabilities = new NameValueCollection();
			this._idHeaderChecks = new ArrayList();
			this._idCapabilityChecks = new ArrayList();
			this._captureHeaderChecks = new ArrayList();
			this._captureCapabilityChecks = new ArrayList();
			this._adapters = new AdapterDictionary();
			this._browsers = new BrowserDefinitionCollection();
			this._gateways = new BrowserDefinitionCollection();
			this._refBrowsers = new BrowserDefinitionCollection();
			this._refGateways = new BrowserDefinitionCollection();
			this._node = node;
			this._isDefaultBrowser = isDefaultBrowser;
			string text = null;
			HandlerBase.GetAndRemoveNonEmptyStringAttribute(node, "id", ref this._id);
			HandlerBase.GetAndRemoveNonEmptyStringAttribute(node, "refID", ref text);
			if (text != null && this._id != null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Browser_mutually_exclusive_attributes", new object[]
				{
					"id",
					"refID"
				}), node);
			}
			if (this._id != null)
			{
				if (!CodeGenerator.IsValidLanguageIndependentIdentifier(this._id))
				{
					throw new ConfigurationErrorsException(SR.GetString("Browser_InvalidID", new object[]
					{
						"id",
						this._id
					}), node);
				}
			}
			else if (text == null)
			{
				if (this is GatewayDefinition)
				{
					throw new ConfigurationErrorsException(SR.GetString("Browser_attributes_required", new object[]
					{
						"gateway",
						"refID",
						"id"
					}), node);
				}
				throw new ConfigurationErrorsException(SR.GetString("Browser_attributes_required", new object[]
				{
					"browser",
					"refID",
					"id"
				}), node);
			}
			else
			{
				if (!CodeGenerator.IsValidLanguageIndependentIdentifier(text))
				{
					throw new ConfigurationErrorsException(SR.GetString("Browser_InvalidID", new object[]
					{
						"refID",
						text
					}), node);
				}
				this._parentID = text;
				this._isRefID = true;
				this._id = text;
				if (this is GatewayDefinition)
				{
					this._name = "refgatewayid$";
				}
				else
				{
					this._name = "refbrowserid$";
				}
				string text2 = null;
				HandlerBase.GetAndRemoveNonEmptyStringAttribute(node, "parentID", ref text2);
				if (text2 != null && text2.Length != 0)
				{
					throw new ConfigurationErrorsException(SR.GetString("Browser_mutually_exclusive_attributes", new object[]
					{
						"parentID",
						"refID"
					}), node);
				}
			}
			this._name = BrowserDefinition.MakeValidTypeNameFromString(this._id + this._name);
			if (!this._isRefID)
			{
				if (!"Default".Equals(this._id))
				{
					HandlerBase.GetAndRemoveNonEmptyStringAttribute(node, "parentID", ref this._parentID);
				}
				else
				{
					HandlerBase.GetAndRemoveNonEmptyStringAttribute(node, "parentID", ref this._parentID);
					if (this._parentID != null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Browser_parentID_applied_to_default"), node);
					}
				}
			}
			this._parentName = BrowserDefinition.MakeValidTypeNameFromString(this._parentID);
			if (this._id.IndexOf(" ", StringComparison.Ordinal) != -1)
			{
				throw new ConfigurationErrorsException(SR.GetString("Space_attribute", new object[]
				{
					"id " + this._id
				}), node);
			}
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					string name = xmlNode.Name;
					if (!(name == "identification"))
					{
						if (!(name == "capture"))
						{
							if (!(name == "capabilities"))
							{
								if (!(name == "controlAdapters"))
								{
									if (!(name == "sampleHeaders"))
									{
										throw new ConfigurationErrorsException(SR.GetString("Browser_invalid_element", new object[]
										{
											xmlNode.Name
										}), node);
									}
								}
								else
								{
									this.ProcessControlAdaptersNode(xmlNode);
								}
							}
							else
							{
								this.ProcessCapabilitiesNode(xmlNode);
							}
						}
						else
						{
							this.ProcessCaptureNode(xmlNode, BrowserCapsElementType.Capture);
						}
					}
					else
					{
						if (this._isRefID)
						{
							throw new ConfigurationErrorsException(SR.GetString("Browser_refid_prohibits_identification"), node);
						}
						this.ProcessIdentificationNode(xmlNode, BrowserCapsElementType.Identification);
					}
				}
			}
		}

		// Token: 0x17001779 RID: 6009
		// (get) Token: 0x060052AA RID: 21162 RVA: 0x00123240 File Offset: 0x00121440
		public bool IsDefaultBrowser
		{
			get
			{
				return this._isDefaultBrowser;
			}
		}

		// Token: 0x1700177A RID: 6010
		// (get) Token: 0x060052AB RID: 21163 RVA: 0x00123248 File Offset: 0x00121448
		public BrowserDefinitionCollection Browsers
		{
			get
			{
				return this._browsers;
			}
		}

		// Token: 0x1700177B RID: 6011
		// (get) Token: 0x060052AC RID: 21164 RVA: 0x00123250 File Offset: 0x00121450
		public BrowserDefinitionCollection RefBrowsers
		{
			get
			{
				return this._refBrowsers;
			}
		}

		// Token: 0x1700177C RID: 6012
		// (get) Token: 0x060052AD RID: 21165 RVA: 0x00123258 File Offset: 0x00121458
		public BrowserDefinitionCollection RefGateways
		{
			get
			{
				return this._refGateways;
			}
		}

		// Token: 0x1700177D RID: 6013
		// (get) Token: 0x060052AE RID: 21166 RVA: 0x00123260 File Offset: 0x00121460
		public BrowserDefinitionCollection Gateways
		{
			get
			{
				return this._gateways;
			}
		}

		// Token: 0x1700177E RID: 6014
		// (get) Token: 0x060052AF RID: 21167 RVA: 0x00123268 File Offset: 0x00121468
		public string ID
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x1700177F RID: 6015
		// (get) Token: 0x060052B0 RID: 21168 RVA: 0x00123270 File Offset: 0x00121470
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17001780 RID: 6016
		// (get) Token: 0x060052B1 RID: 21169 RVA: 0x00123278 File Offset: 0x00121478
		public string ParentName
		{
			get
			{
				return this._parentName;
			}
		}

		// Token: 0x17001781 RID: 6017
		// (get) Token: 0x060052B2 RID: 21170 RVA: 0x00123280 File Offset: 0x00121480
		// (set) Token: 0x060052B3 RID: 21171 RVA: 0x00123288 File Offset: 0x00121488
		internal bool IsDeviceNode
		{
			get
			{
				return this._isDeviceNode;
			}
			set
			{
				this._isDeviceNode = value;
			}
		}

		// Token: 0x17001782 RID: 6018
		// (get) Token: 0x060052B4 RID: 21172 RVA: 0x00123291 File Offset: 0x00121491
		// (set) Token: 0x060052B5 RID: 21173 RVA: 0x00123299 File Offset: 0x00121499
		internal int Depth
		{
			get
			{
				return this._depth;
			}
			set
			{
				this._depth = value;
			}
		}

		// Token: 0x17001783 RID: 6019
		// (get) Token: 0x060052B6 RID: 21174 RVA: 0x001232A2 File Offset: 0x001214A2
		public string ParentID
		{
			get
			{
				return this._parentID;
			}
		}

		// Token: 0x17001784 RID: 6020
		// (get) Token: 0x060052B7 RID: 21175 RVA: 0x001232AA File Offset: 0x001214AA
		internal bool IsRefID
		{
			get
			{
				return this._isRefID;
			}
		}

		// Token: 0x17001785 RID: 6021
		// (get) Token: 0x060052B8 RID: 21176 RVA: 0x001232B2 File Offset: 0x001214B2
		public NameValueCollection Capabilities
		{
			get
			{
				return this._capabilities;
			}
		}

		// Token: 0x17001786 RID: 6022
		// (get) Token: 0x060052B9 RID: 21177 RVA: 0x001232BA File Offset: 0x001214BA
		public ArrayList IdHeaderChecks
		{
			get
			{
				return this._idHeaderChecks;
			}
		}

		// Token: 0x17001787 RID: 6023
		// (get) Token: 0x060052BA RID: 21178 RVA: 0x001232C2 File Offset: 0x001214C2
		public ArrayList CaptureHeaderChecks
		{
			get
			{
				return this._captureHeaderChecks;
			}
		}

		// Token: 0x17001788 RID: 6024
		// (get) Token: 0x060052BB RID: 21179 RVA: 0x001232CA File Offset: 0x001214CA
		public ArrayList IdCapabilityChecks
		{
			get
			{
				return this._idCapabilityChecks;
			}
		}

		// Token: 0x17001789 RID: 6025
		// (get) Token: 0x060052BC RID: 21180 RVA: 0x001232D2 File Offset: 0x001214D2
		public ArrayList CaptureCapabilityChecks
		{
			get
			{
				return this._captureCapabilityChecks;
			}
		}

		// Token: 0x1700178A RID: 6026
		// (get) Token: 0x060052BD RID: 21181 RVA: 0x001232DA File Offset: 0x001214DA
		public AdapterDictionary Adapters
		{
			get
			{
				return this._adapters;
			}
		}

		// Token: 0x1700178B RID: 6027
		// (get) Token: 0x060052BE RID: 21182 RVA: 0x001232E2 File Offset: 0x001214E2
		internal XmlNode XmlNode
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x1700178C RID: 6028
		// (get) Token: 0x060052BF RID: 21183 RVA: 0x001232EA File Offset: 0x001214EA
		public string HtmlTextWriterString
		{
			get
			{
				return this._htmlTextWriterString;
			}
		}

		// Token: 0x060052C0 RID: 21184 RVA: 0x001232F4 File Offset: 0x001214F4
		private void DisallowNonMatchAttribute(XmlNode node)
		{
			string text = null;
			HandlerBase.GetAndRemoveStringAttribute(node, "nonMatch", ref text);
			if (text != null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Browser_mutually_exclusive_attributes", new object[]
				{
					"match",
					"nonMatch"
				}), node);
			}
		}

		// Token: 0x060052C1 RID: 21185 RVA: 0x0012333B File Offset: 0x0012153B
		private void HandleMissingMatchAndNonMatchError(XmlNode node)
		{
			throw new ConfigurationErrorsException(SR.GetString("Missing_required_attributes", new object[]
			{
				"match",
				"nonMatch",
				node.Name
			}), node);
		}

		// Token: 0x060052C2 RID: 21186 RVA: 0x0012336C File Offset: 0x0012156C
		internal void ProcessIdentificationNode(XmlNode node, BrowserCapsElementType elementType)
		{
			string text = null;
			string header = null;
			bool flag = true;
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				text = string.Empty;
				bool flag2 = false;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					string name = xmlNode.Name;
					if (!(name == "userAgent"))
					{
						if (!(name == "header"))
						{
							if (!(name == "capability"))
							{
								throw new ConfigurationErrorsException(SR.GetString("Config_invalid_element", new object[]
								{
									xmlNode.ToString()
								}), xmlNode);
							}
							flag = false;
							HandlerBase.GetAndRemoveRequiredNonEmptyStringAttribute(xmlNode, "name", ref header);
							HandlerBase.GetAndRemoveNonEmptyStringAttribute(xmlNode, "match", ref text);
							if (string.IsNullOrEmpty(text))
							{
								HandlerBase.GetAndRemoveNonEmptyStringAttribute(xmlNode, "nonMatch", ref text);
								if (string.IsNullOrEmpty(text))
								{
									this.HandleMissingMatchAndNonMatchError(xmlNode);
								}
								flag2 = true;
							}
							this._idCapabilityChecks.Add(new CheckPair(header, text, flag2));
							if (!flag2)
							{
								this.DisallowNonMatchAttribute(xmlNode);
							}
						}
						else
						{
							flag = false;
							HandlerBase.GetAndRemoveRequiredNonEmptyStringAttribute(xmlNode, "name", ref header);
							HandlerBase.GetAndRemoveNonEmptyStringAttribute(xmlNode, "match", ref text);
							if (string.IsNullOrEmpty(text))
							{
								HandlerBase.GetAndRemoveNonEmptyStringAttribute(xmlNode, "nonMatch", ref text);
								if (string.IsNullOrEmpty(text))
								{
									this.HandleMissingMatchAndNonMatchError(xmlNode);
								}
								flag2 = true;
							}
							this._idHeaderChecks.Add(new CheckPair(header, text, flag2));
							if (!flag2)
							{
								this.DisallowNonMatchAttribute(xmlNode);
							}
						}
					}
					else
					{
						flag = false;
						HandlerBase.GetAndRemoveNonEmptyStringAttribute(xmlNode, "match", ref text);
						if (string.IsNullOrEmpty(text))
						{
							HandlerBase.GetAndRemoveNonEmptyStringAttribute(xmlNode, "nonMatch", ref text);
							if (string.IsNullOrEmpty(text))
							{
								this.HandleMissingMatchAndNonMatchError(xmlNode);
							}
							flag2 = true;
						}
						this._idHeaderChecks.Add(new CheckPair("User-Agent", text, flag2));
						if (!flag2)
						{
							this.DisallowNonMatchAttribute(xmlNode);
						}
					}
				}
			}
			if (flag)
			{
				throw new ConfigurationErrorsException(SR.GetString("Browser_empty_identification"), node);
			}
		}

		// Token: 0x060052C3 RID: 21187 RVA: 0x001235A4 File Offset: 0x001217A4
		internal void ProcessCaptureNode(XmlNode node, BrowserCapsElementType elementType)
		{
			string match = null;
			string header = null;
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					string name = xmlNode.Name;
					if (!(name == "userAgent"))
					{
						if (!(name == "header"))
						{
							if (!(name == "capability"))
							{
								throw new ConfigurationErrorsException(SR.GetString("Config_invalid_element", new object[]
								{
									xmlNode.ToString()
								}), xmlNode);
							}
							HandlerBase.GetAndRemoveRequiredNonEmptyStringAttribute(xmlNode, "name", ref header);
							HandlerBase.GetAndRemoveRequiredNonEmptyStringAttribute(xmlNode, "match", ref match);
							this._captureCapabilityChecks.Add(new CheckPair(header, match));
						}
						else
						{
							HandlerBase.GetAndRemoveRequiredNonEmptyStringAttribute(xmlNode, "name", ref header);
							HandlerBase.GetAndRemoveRequiredNonEmptyStringAttribute(xmlNode, "match", ref match);
							this._captureHeaderChecks.Add(new CheckPair(header, match));
						}
					}
					else
					{
						HandlerBase.GetAndRemoveRequiredNonEmptyStringAttribute(xmlNode, "match", ref match);
						this._captureHeaderChecks.Add(new CheckPair("User-Agent", match));
					}
				}
			}
		}

		// Token: 0x060052C4 RID: 21188 RVA: 0x00123700 File Offset: 0x00121900
		internal void ProcessCapabilitiesNode(XmlNode node)
		{
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					if (xmlNode.Name != "capability")
					{
						throw new ConfigurationErrorsException(SR.GetString("Config_base_unrecognized_element"), xmlNode);
					}
					string name = null;
					string value = null;
					HandlerBase.GetAndRemoveRequiredNonEmptyStringAttribute(xmlNode, "name", ref name);
					HandlerBase.GetAndRemoveRequiredStringAttribute(xmlNode, "value", ref value);
					this._capabilities[name] = value;
				}
			}
		}

		// Token: 0x060052C5 RID: 21189 RVA: 0x001237B0 File Offset: 0x001219B0
		internal void ProcessControlAdaptersNode(XmlNode node)
		{
			HandlerBase.GetAndRemoveStringAttribute(node, "markupTextWriterType", ref this._htmlTextWriterString);
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					if (xmlNode.Name != "adapter")
					{
						throw new ConfigurationErrorsException(SR.GetString("Config_base_unrecognized_element"), xmlNode);
					}
					XmlAttributeCollection attributes = xmlNode.Attributes;
					string text = null;
					string text2 = null;
					HandlerBase.GetAndRemoveRequiredNonEmptyStringAttribute(xmlNode, "controlType", ref text);
					HandlerBase.GetAndRemoveRequiredStringAttribute(xmlNode, "adapterType", ref text2);
					Type type = BrowserDefinition.CheckType(text, typeof(Control), xmlNode);
					text = type.AssemblyQualifiedName;
					if (!string.IsNullOrEmpty(text2))
					{
						BrowserDefinition.CheckType(text2, typeof(ControlAdapter), xmlNode);
					}
					this._adapters[text] = text2;
				}
			}
		}

		// Token: 0x060052C6 RID: 21190 RVA: 0x001238BC File Offset: 0x00121ABC
		private static Type CheckType(string typeName, Type baseType, XmlNode child)
		{
			Type type = ConfigUtil.GetType(typeName, child, true);
			if (!baseType.IsAssignableFrom(type))
			{
				throw new ConfigurationErrorsException(SR.GetString("Type_doesnt_inherit_from_type", new object[]
				{
					typeName,
					baseType.FullName
				}), child);
			}
			if (!HttpRuntime.IsTypeAllowedInConfig(type))
			{
				throw new ConfigurationErrorsException(SR.GetString("Type_from_untrusted_assembly", new object[]
				{
					typeName
				}), child);
			}
			return type;
		}

		// Token: 0x060052C7 RID: 21191 RVA: 0x00123924 File Offset: 0x00121B24
		internal void MergeWithDefinition(BrowserDefinition definition)
		{
			foreach (object obj in definition.Capabilities.Keys)
			{
				string name = (string)obj;
				this._capabilities[name] = definition.Capabilities[name];
			}
			foreach (object obj2 in definition.Adapters.Keys)
			{
				string key = (string)obj2;
				this._adapters[key] = definition.Adapters[key];
			}
			this._htmlTextWriterString = definition.HtmlTextWriterString;
		}

		// Token: 0x04002B5B RID: 11099
		private ArrayList _idHeaderChecks;

		// Token: 0x04002B5C RID: 11100
		private ArrayList _idCapabilityChecks;

		// Token: 0x04002B5D RID: 11101
		private ArrayList _captureHeaderChecks;

		// Token: 0x04002B5E RID: 11102
		private ArrayList _captureCapabilityChecks;

		// Token: 0x04002B5F RID: 11103
		private AdapterDictionary _adapters;

		// Token: 0x04002B60 RID: 11104
		private string _id;

		// Token: 0x04002B61 RID: 11105
		private string _parentID;

		// Token: 0x04002B62 RID: 11106
		private string _name;

		// Token: 0x04002B63 RID: 11107
		private string _parentName;

		// Token: 0x04002B64 RID: 11108
		private NameValueCollection _capabilities;

		// Token: 0x04002B65 RID: 11109
		private BrowserDefinitionCollection _browsers;

		// Token: 0x04002B66 RID: 11110
		private BrowserDefinitionCollection _gateways;

		// Token: 0x04002B67 RID: 11111
		private BrowserDefinitionCollection _refBrowsers;

		// Token: 0x04002B68 RID: 11112
		private BrowserDefinitionCollection _refGateways;

		// Token: 0x04002B69 RID: 11113
		private XmlNode _node;

		// Token: 0x04002B6A RID: 11114
		private bool _isRefID;

		// Token: 0x04002B6B RID: 11115
		private bool _isDeviceNode;

		// Token: 0x04002B6C RID: 11116
		private bool _isDefaultBrowser;

		// Token: 0x04002B6D RID: 11117
		private string _htmlTextWriterString;

		// Token: 0x04002B6E RID: 11118
		private int _depth;
	}
}
