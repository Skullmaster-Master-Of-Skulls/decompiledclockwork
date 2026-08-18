using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.ServiceProcess;
using System.Web.Compilation;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.Util;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Build.Utilities;
using Microsoft.CSharp;

namespace System.Web.Configuration
{
	// Token: 0x020006A4 RID: 1700
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class BrowserCapabilitiesCodeGenerator
	{
		// Token: 0x060051AD RID: 20909 RVA: 0x00118F7C File Offset: 0x0011717C
		static BrowserCapabilitiesCodeGenerator()
		{
			BrowserCapabilitiesCodeGenerator._browsersDirectory = HttpRuntime.ClrInstallDirectoryInternal + "\\config\\browsers";
			BrowserCapabilitiesCodeGenerator._publicKeyTokenFile = BrowserCapabilitiesCodeGenerator._browsersDirectory + "\\" + BrowserCapabilitiesCodeGenerator._publicKeyTokenFileName;
		}

		// Token: 0x060051AE RID: 20910 RVA: 0x00118FD4 File Offset: 0x001171D4
		public BrowserCapabilitiesCodeGenerator()
		{
			this._headers = new CaseInsensitiveStringSet();
		}

		// Token: 0x17001770 RID: 6000
		// (get) Token: 0x060051AF RID: 20911 RVA: 0x00119032 File Offset: 0x00117232
		internal BrowserTree BrowserTree
		{
			get
			{
				return this._browserTree;
			}
		}

		// Token: 0x17001771 RID: 6001
		// (get) Token: 0x060051B0 RID: 20912 RVA: 0x0011903A File Offset: 0x0011723A
		internal BrowserTree DefaultTree
		{
			get
			{
				return this._defaultTree;
			}
		}

		// Token: 0x17001772 RID: 6002
		// (get) Token: 0x060051B1 RID: 20913 RVA: 0x00119042 File Offset: 0x00117242
		internal ArrayList CustomTreeList
		{
			get
			{
				return this._customTreeList;
			}
		}

		// Token: 0x17001773 RID: 6003
		// (get) Token: 0x060051B2 RID: 20914 RVA: 0x0011904A File Offset: 0x0011724A
		internal ArrayList CustomTreeNames
		{
			get
			{
				return this._customTreeNames;
			}
		}

		// Token: 0x17001774 RID: 6004
		// (get) Token: 0x060051B3 RID: 20915 RVA: 0x00119054 File Offset: 0x00117254
		internal static string BrowserCapAssemblyPublicKeyToken
		{
			get
			{
				if (BrowserCapabilitiesCodeGenerator._publicKeyTokenLoaded)
				{
					return BrowserCapabilitiesCodeGenerator._publicKeyToken;
				}
				object staticLock = BrowserCapabilitiesCodeGenerator._staticLock;
				string publicKeyToken;
				lock (staticLock)
				{
					if (BrowserCapabilitiesCodeGenerator._publicKeyTokenLoaded)
					{
						publicKeyToken = BrowserCapabilitiesCodeGenerator._publicKeyToken;
					}
					else
					{
						string filename;
						if (MultiTargetingUtil.IsTargetFramework40OrAbove)
						{
							filename = BrowserCapabilitiesCodeGenerator._publicKeyTokenFile;
						}
						else
						{
							string fileName = "config\\browsers\\" + BrowserCapabilitiesCodeGenerator._publicKeyTokenFileName;
							filename = ToolLocationHelper.GetPathToDotNetFrameworkFile(fileName, TargetDotNetFrameworkVersion.Version20);
						}
						BrowserCapabilitiesCodeGenerator._publicKeyToken = BrowserCapabilitiesCodeGenerator.LoadPublicKeyTokenFromFile(filename);
						BrowserCapabilitiesCodeGenerator._publicKeyTokenLoaded = true;
						publicKeyToken = BrowserCapabilitiesCodeGenerator._publicKeyToken;
					}
				}
				return publicKeyToken;
			}
		}

		// Token: 0x17001775 RID: 6005
		// (get) Token: 0x060051B4 RID: 20916 RVA: 0x000097B7 File Offset: 0x000079B7
		internal virtual bool GenerateOverrides
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001776 RID: 6006
		// (get) Token: 0x060051B5 RID: 20917 RVA: 0x001190EC File Offset: 0x001172EC
		internal virtual string TypeName
		{
			get
			{
				return "BrowserCapabilitiesFactory";
			}
		}

		// Token: 0x060051B6 RID: 20918 RVA: 0x001190F3 File Offset: 0x001172F3
		internal void AddFile(string filePath)
		{
			if (this._browserFileList == null)
			{
				this._browserFileList = new ArrayList();
			}
			this._browserFileList.Add(filePath);
		}

		// Token: 0x060051B7 RID: 20919 RVA: 0x00119115 File Offset: 0x00117315
		internal void AddCustomFile(string filePath)
		{
			if (this._customBrowserFileLists == null)
			{
				this._customBrowserFileLists = new ArrayList();
			}
			this._customBrowserFileLists.Add(filePath);
		}

		// Token: 0x060051B8 RID: 20920 RVA: 0x00119138 File Offset: 0x00117338
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public virtual void Create()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(BrowserCapabilitiesCodeGenerator._browsersDirectory);
			FileInfo[] files = directoryInfo.GetFiles("*.browser");
			if (files == null || files.Length == 0)
			{
				return;
			}
			foreach (FileInfo fileInfo in files)
			{
				this.AddFile(fileInfo.FullName);
			}
			this.ProcessBrowserFiles();
			this.ProcessCustomBrowserFiles();
			this.Uninstall();
			this.GenerateAssembly();
			this.RestartW3SVCIfNecessary();
		}

		// Token: 0x060051B9 RID: 20921 RVA: 0x001191A8 File Offset: 0x001173A8
		internal bool UninstallInternal()
		{
			if (File.Exists(BrowserCapabilitiesCodeGenerator._publicKeyTokenFile))
			{
				File.Delete(BrowserCapabilitiesCodeGenerator._publicKeyTokenFile);
			}
			GacUtil gacUtil = new GacUtil();
			return gacUtil.GacUnInstall("ASP.BrowserCapsFactory, Version=4.0.0.0, Culture=neutral");
		}

		// Token: 0x060051BA RID: 20922 RVA: 0x001191E3 File Offset: 0x001173E3
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public bool Uninstall()
		{
			this.RestartW3SVCIfNecessary();
			if (!this.UninstallInternal())
			{
				return false;
			}
			this.RestartW3SVCIfNecessary();
			return true;
		}

		// Token: 0x060051BB RID: 20923 RVA: 0x001191FC File Offset: 0x001173FC
		private void RestartW3SVCIfNecessary()
		{
			try
			{
				ServiceController[] services = ServiceController.GetServices();
				ServiceController serviceController = services.SingleOrDefault((ServiceController s) => string.Equals(s.ServiceName, "W3SVC", StringComparison.OrdinalIgnoreCase));
				if (serviceController != null)
				{
					ServiceControllerStatus status = serviceController.Status;
					if (!status.Equals(ServiceControllerStatus.Stopped) && !status.Equals(ServiceControllerStatus.StopPending) && !status.Equals(ServiceControllerStatus.StartPending))
					{
						serviceController.Stop();
						serviceController.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 5, 0));
						serviceController.Start();
						if (status.Equals(ServiceControllerStatus.Paused) || status.Equals(ServiceControllerStatus.PausePending))
						{
							serviceController.Pause();
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException(SR.GetString("Browser_W3SVC_Failure_Helper_Text", new object[]
				{
					ex
				}));
			}
		}

		// Token: 0x060051BC RID: 20924 RVA: 0x001192FC File Offset: 0x001174FC
		internal void ProcessBrowserFiles()
		{
			this.ProcessBrowserFiles(false, string.Empty);
		}

		// Token: 0x060051BD RID: 20925 RVA: 0x0011930C File Offset: 0x0011750C
		private string NoPathFileName(string fullPath)
		{
			int num = fullPath.LastIndexOf("\\", StringComparison.Ordinal);
			if (num > -1)
			{
				return fullPath.Substring(num + 1);
			}
			return fullPath;
		}

		// Token: 0x060051BE RID: 20926 RVA: 0x00119338 File Offset: 0x00117538
		internal virtual void ProcessBrowserNode(XmlNode node, BrowserTree browserTree)
		{
			BrowserDefinition browserDefinition;
			if (node.Name == "gateway")
			{
				browserDefinition = new GatewayDefinition(node);
			}
			else if (node.Name == "browser")
			{
				browserDefinition = new BrowserDefinition(node);
			}
			else
			{
				browserDefinition = new BrowserDefinition(node, true);
			}
			BrowserDefinition browserDefinition2 = (BrowserDefinition)browserTree[browserDefinition.Name];
			if (browserDefinition2 == null)
			{
				browserTree[browserDefinition.Name] = browserDefinition;
				return;
			}
			if (browserDefinition.IsRefID)
			{
				browserDefinition2.MergeWithDefinition(browserDefinition);
				return;
			}
			throw new ConfigurationErrorsException(SR.GetString("Duplicate_browser_id", new object[]
			{
				browserDefinition.ID
			}), node);
		}

		// Token: 0x060051BF RID: 20927 RVA: 0x001193D7 File Offset: 0x001175D7
		private void NormalizeAndValidateTree(BrowserTree browserTree, bool isDefaultBrowser)
		{
			this.NormalizeAndValidateTree(browserTree, isDefaultBrowser, false);
		}

		// Token: 0x060051C0 RID: 20928 RVA: 0x001193E4 File Offset: 0x001175E4
		private void NormalizeAndValidateTree(BrowserTree browserTree, bool isDefaultBrowser, bool isCustomBrowser)
		{
			foreach (object obj in browserTree)
			{
				BrowserDefinition browserDefinition = (BrowserDefinition)((DictionaryEntry)obj).Value;
				string parentName = browserDefinition.ParentName;
				BrowserDefinition browserDefinition2 = null;
				if (!this.IsRootNode(browserDefinition.Name))
				{
					if (parentName.Length > 0)
					{
						browserDefinition2 = (BrowserDefinition)browserTree[parentName];
					}
					if (browserDefinition2 != null)
					{
						if (browserDefinition.IsRefID)
						{
							if (browserDefinition is GatewayDefinition)
							{
								browserDefinition2.RefGateways.Add(browserDefinition);
							}
							else
							{
								browserDefinition2.RefBrowsers.Add(browserDefinition);
							}
						}
						else if (browserDefinition is GatewayDefinition)
						{
							browserDefinition2.Gateways.Add(browserDefinition);
						}
						else
						{
							browserDefinition2.Browsers.Add(browserDefinition);
						}
					}
					else
					{
						if (isCustomBrowser)
						{
							throw new ConfigurationErrorsException(SR.GetString("Browser_parentID_Not_Found", new object[]
							{
								browserDefinition.ParentID
							}), browserDefinition.XmlNode);
						}
						this.HandleUnRecognizedParentElement(browserDefinition, isDefaultBrowser);
					}
				}
			}
			foreach (object obj2 in browserTree)
			{
				BrowserDefinition browserDefinition3 = (BrowserDefinition)((DictionaryEntry)obj2).Value;
				Hashtable hashtable = new Hashtable();
				BrowserDefinition browserDefinition4 = browserDefinition3;
				string name = browserDefinition4.Name;
				while (!this.IsRootNode(name))
				{
					if (hashtable[name] != null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Browser_Circular_Reference", new object[]
						{
							name
						}), browserDefinition4.XmlNode);
					}
					hashtable[name] = name;
					browserDefinition4 = (BrowserDefinition)browserTree[browserDefinition4.ParentName];
					if (browserDefinition4 == null)
					{
						break;
					}
					name = browserDefinition4.Name;
				}
			}
		}

		// Token: 0x060051C1 RID: 20929 RVA: 0x001195D8 File Offset: 0x001177D8
		private void SetCustomTreeRoots(BrowserTree browserTree, int index)
		{
			foreach (object obj in browserTree)
			{
				BrowserDefinition browserDefinition = (BrowserDefinition)((DictionaryEntry)obj).Value;
				if (browserDefinition.ParentName == null)
				{
					this._customTreeNames[index] = browserDefinition.Name;
					break;
				}
			}
		}

		// Token: 0x060051C2 RID: 20930 RVA: 0x00119650 File Offset: 0x00117850
		private bool IsRootNode(string nodeName)
		{
			if (string.Compare(nodeName, "Default", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			foreach (object obj in this._customTreeNames)
			{
				string strB = (string)obj;
				if (string.Compare(nodeName, strB, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060051C3 RID: 20931 RVA: 0x001196C4 File Offset: 0x001178C4
		protected void ProcessBrowserFiles(bool useVirtualPath, string virtualDir)
		{
			this._browserTree = new BrowserTree();
			this._defaultTree = new BrowserTree();
			this._customTreeNames = new ArrayList();
			if (this._browserFileList == null)
			{
				this._browserFileList = new ArrayList();
			}
			this._browserFileList.Sort();
			string text = null;
			string text2 = null;
			string text3 = null;
			foreach (object obj in this._browserFileList)
			{
				string text4 = (string)obj;
				if (text4.EndsWith("ie.browser", StringComparison.OrdinalIgnoreCase))
				{
					text2 = text4;
				}
				else if (text4.EndsWith("mozilla.browser", StringComparison.OrdinalIgnoreCase))
				{
					text = text4;
				}
				else if (text4.EndsWith("opera.browser", StringComparison.OrdinalIgnoreCase))
				{
					text3 = text4;
					break;
				}
			}
			if (text2 != null)
			{
				this._browserFileList.Remove(text2);
				this._browserFileList.Insert(0, text2);
			}
			if (text != null)
			{
				this._browserFileList.Remove(text);
				this._browserFileList.Insert(1, text);
			}
			if (text3 != null)
			{
				this._browserFileList.Remove(text3);
				this._browserFileList.Insert(2, text3);
			}
			foreach (object obj2 in this._browserFileList)
			{
				string text5 = (string)obj2;
				XmlDocument xmlDocument = new ConfigXmlDocument();
				try
				{
					xmlDocument.Load(text5);
					XmlNode documentElement = xmlDocument.DocumentElement;
					if (documentElement.Name != "browsers")
					{
						if (useVirtualPath)
						{
							throw new HttpParseException(SR.GetString("Invalid_browser_root"), null, virtualDir + "/" + this.NoPathFileName(text5), null, 1);
						}
						throw new HttpParseException(SR.GetString("Invalid_browser_root"), null, text5, null, 1);
					}
					else
					{
						foreach (object obj3 in documentElement.ChildNodes)
						{
							XmlNode xmlNode = (XmlNode)obj3;
							if (xmlNode.NodeType == XmlNodeType.Element)
							{
								if (xmlNode.Name == "browser" || xmlNode.Name == "gateway")
								{
									this.ProcessBrowserNode(xmlNode, this._browserTree);
								}
								else if (xmlNode.Name == "defaultBrowser")
								{
									this.ProcessBrowserNode(xmlNode, this._defaultTree);
								}
								else
								{
									HandlerBase.ThrowUnrecognizedElement(xmlNode);
								}
							}
						}
					}
				}
				catch (XmlException ex)
				{
					if (useVirtualPath)
					{
						throw new HttpParseException(ex.Message, null, virtualDir + "/" + this.NoPathFileName(text5), null, ex.LineNumber);
					}
					throw new HttpParseException(ex.Message, null, text5, null, ex.LineNumber);
				}
				catch (XmlSchemaException ex2)
				{
					if (useVirtualPath)
					{
						throw new HttpParseException(ex2.Message, null, virtualDir + "/" + this.NoPathFileName(text5), null, ex2.LineNumber);
					}
					throw new HttpParseException(ex2.Message, null, text5, null, ex2.LineNumber);
				}
			}
			this.NormalizeAndValidateTree(this._browserTree, false);
			this.NormalizeAndValidateTree(this._defaultTree, true);
			BrowserDefinition browserDefinition = (BrowserDefinition)this._browserTree["Default"];
			if (browserDefinition != null)
			{
				this.AddBrowserToCollectionRecursive(browserDefinition, 0);
			}
		}

		// Token: 0x060051C4 RID: 20932 RVA: 0x00119A84 File Offset: 0x00117C84
		internal void ProcessCustomBrowserFiles()
		{
			this.ProcessCustomBrowserFiles(false, string.Empty);
		}

		// Token: 0x060051C5 RID: 20933 RVA: 0x00119A94 File Offset: 0x00117C94
		internal void ProcessCustomBrowserFiles(bool useVirtualPath, string virtualDir)
		{
			DirectoryInfo[] array = null;
			this._customTreeList = new ArrayList();
			this._customBrowserFileLists = new ArrayList();
			this._customBrowserDefinitionCollections = new ArrayList();
			DirectoryInfo directoryInfo;
			if (!useVirtualPath)
			{
				directoryInfo = new DirectoryInfo(BrowserCapabilitiesCodeGenerator._browsersDirectory);
			}
			else
			{
				directoryInfo = new DirectoryInfo(HostingEnvironment.MapPathInternal(virtualDir));
			}
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			int num = 0;
			int num2 = directories.Length;
			array = new DirectoryInfo[num2];
			for (int i = 0; i < num2; i++)
			{
				if ((directories[i].Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
				{
					array[num] = directories[i];
					num++;
				}
			}
			Array.Resize<DirectoryInfo>(ref array, num);
			for (int j = 0; j < array.Length; j++)
			{
				FileInfo[] filesNotHidden = BrowserCapabilitiesCodeGenerator.GetFilesNotHidden(array[j], directoryInfo);
				if (filesNotHidden != null && filesNotHidden.Length != 0)
				{
					BrowserTree value = new BrowserTree();
					this._customTreeList.Add(value);
					this._customTreeNames.Add(array[j].Name);
					ArrayList arrayList = new ArrayList();
					foreach (FileInfo fileInfo in filesNotHidden)
					{
						arrayList.Add(fileInfo.FullName);
					}
					this._customBrowserFileLists.Add(arrayList);
				}
			}
			for (int l = 0; l < this._customBrowserFileLists.Count; l++)
			{
				ArrayList arrayList2 = (ArrayList)this._customBrowserFileLists[l];
				foreach (object obj in arrayList2)
				{
					string text = (string)obj;
					XmlDocument xmlDocument = new ConfigXmlDocument();
					try
					{
						xmlDocument.Load(text);
						XmlNode documentElement = xmlDocument.DocumentElement;
						if (documentElement.Name != "browsers")
						{
							if (useVirtualPath)
							{
								throw new HttpParseException(SR.GetString("Invalid_browser_root"), null, virtualDir + "/" + this.NoPathFileName(text), null, 1);
							}
							throw new HttpParseException(SR.GetString("Invalid_browser_root"), null, text, null, 1);
						}
						else
						{
							foreach (object obj2 in documentElement.ChildNodes)
							{
								XmlNode xmlNode = (XmlNode)obj2;
								if (xmlNode.NodeType == XmlNodeType.Element)
								{
									if (xmlNode.Name == "browser" || xmlNode.Name == "gateway")
									{
										this.ProcessBrowserNode(xmlNode, (BrowserTree)this._customTreeList[l]);
									}
									else
									{
										HandlerBase.ThrowUnrecognizedElement(xmlNode);
									}
								}
							}
						}
					}
					catch (XmlException ex)
					{
						if (useVirtualPath)
						{
							throw new HttpParseException(ex.Message, null, virtualDir + "/" + this.NoPathFileName(text), null, ex.LineNumber);
						}
						throw new HttpParseException(ex.Message, null, text, null, ex.LineNumber);
					}
					catch (XmlSchemaException ex2)
					{
						if (useVirtualPath)
						{
							throw new HttpParseException(ex2.Message, null, virtualDir + "/" + this.NoPathFileName(text), null, ex2.LineNumber);
						}
						throw new HttpParseException(ex2.Message, null, text, null, ex2.LineNumber);
					}
				}
				this.SetCustomTreeRoots((BrowserTree)this._customTreeList[l], l);
				this.NormalizeAndValidateTree((BrowserTree)this._customTreeList[l], false, true);
				this._customBrowserDefinitionCollections.Add(new BrowserDefinitionCollection());
				this.AddCustomBrowserToCollectionRecursive((BrowserDefinition)((BrowserTree)this._customTreeList[l])[this._customTreeNames[l]], 0, l);
			}
		}

		// Token: 0x060051C6 RID: 20934 RVA: 0x00119EA4 File Offset: 0x001180A4
		internal void AddCustomBrowserToCollectionRecursive(BrowserDefinition bd, int depth, int index)
		{
			if (this._customBrowserDefinitionCollections[index] == null)
			{
				this._customBrowserDefinitionCollections[index] = new BrowserDefinitionCollection();
			}
			bd.Depth = depth;
			bd.IsDeviceNode = true;
			((BrowserDefinitionCollection)this._customBrowserDefinitionCollections[index]).Add(bd);
			foreach (object obj in bd.Browsers)
			{
				BrowserDefinition bd2 = (BrowserDefinition)obj;
				this.AddCustomBrowserToCollectionRecursive(bd2, depth + 1, index);
			}
		}

		// Token: 0x060051C7 RID: 20935 RVA: 0x00119F48 File Offset: 0x00118148
		internal void AddBrowserToCollectionRecursive(BrowserDefinition bd, int depth)
		{
			if (this._browserDefinitionCollection == null)
			{
				this._browserDefinitionCollection = new BrowserDefinitionCollection();
			}
			bd.Depth = depth;
			bd.IsDeviceNode = true;
			this._browserDefinitionCollection.Add(bd);
			foreach (object obj in bd.Browsers)
			{
				BrowserDefinition bd2 = (BrowserDefinition)obj;
				this.AddBrowserToCollectionRecursive(bd2, depth + 1);
			}
		}

		// Token: 0x060051C8 RID: 20936 RVA: 0x00119FD4 File Offset: 0x001181D4
		internal virtual void HandleUnRecognizedParentElement(BrowserDefinition bd, bool isDefault)
		{
			throw new ConfigurationErrorsException(SR.GetString("Browser_parentID_Not_Found", new object[]
			{
				bd.ParentID
			}), bd.XmlNode);
		}

		// Token: 0x060051C9 RID: 20937 RVA: 0x00119FFC File Offset: 0x001181FC
		private static FileInfo[] GetFilesNotHidden(DirectoryInfo rootDirectory, DirectoryInfo browserDirInfo)
		{
			ArrayList arrayList = new ArrayList();
			DirectoryInfo[] directories = rootDirectory.GetDirectories("*", SearchOption.AllDirectories);
			FileInfo[] files = rootDirectory.GetFiles("*.browser", SearchOption.TopDirectoryOnly);
			arrayList.AddRange(files);
			for (int i = 0; i < directories.Length; i++)
			{
				if (!BrowserCapabilitiesCodeGenerator.HasHiddenParent(directories[i], browserDirInfo))
				{
					files = directories[i].GetFiles("*.browser", SearchOption.TopDirectoryOnly);
					arrayList.AddRange(files);
				}
			}
			return (FileInfo[])arrayList.ToArray(typeof(FileInfo));
		}

		// Token: 0x060051CA RID: 20938 RVA: 0x0011A074 File Offset: 0x00118274
		private static bool HasHiddenParent(DirectoryInfo directory, DirectoryInfo browserDirInfo)
		{
			while (!string.Equals(directory.Parent.Name, browserDirInfo.Name))
			{
				if ((directory.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
				{
					return true;
				}
				directory = directory.Parent;
			}
			return false;
		}

		// Token: 0x060051CB RID: 20939 RVA: 0x0011A0A8 File Offset: 0x001182A8
		private void GenerateAssembly()
		{
			BrowserDefinition bd = (BrowserDefinition)this._browserTree["Default"];
			BrowserDefinition browserDefinition = (BrowserDefinition)this._defaultTree["Default"];
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < this._customTreeNames.Count; i++)
			{
				arrayList.Add((BrowserDefinition)((BrowserTree)this._customTreeList[i])[this._customTreeNames[i]]);
			}
			CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider();
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
			CodeAttributeDeclaration value = new CodeAttributeDeclaration("System.Reflection.AssemblyKeyFile", new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(new CodePrimitiveExpression(BrowserCapabilitiesCodeGenerator._strongNameKeyFileName))
			});
			CodeAttributeDeclaration value2 = new CodeAttributeDeclaration("System.Security.AllowPartiallyTrustedCallers");
			codeCompileUnit.AssemblyCustomAttributes.Add(value2);
			codeCompileUnit.AssemblyCustomAttributes.Add(value);
			value = new CodeAttributeDeclaration("System.Reflection.AssemblyVersion", new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(new CodePrimitiveExpression("4.0.0.0"))
			});
			codeCompileUnit.AssemblyCustomAttributes.Add(value);
			CodeNamespace codeNamespace = new CodeNamespace("ASP");
			codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
			codeNamespace.Imports.Add(new CodeNamespaceImport("System.Web"));
			codeNamespace.Imports.Add(new CodeNamespaceImport("System.Web.Configuration"));
			codeNamespace.Imports.Add(new CodeNamespaceImport("System.Reflection"));
			codeCompileUnit.Namespaces.Add(codeNamespace);
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration("BrowserCapabilitiesFactory");
			codeTypeDeclaration.Attributes = MemberAttributes.Private;
			codeTypeDeclaration.IsClass = true;
			codeTypeDeclaration.Name = this.TypeName;
			codeTypeDeclaration.BaseTypes.Add(new CodeTypeReference("System.Web.Configuration.BrowserCapabilitiesFactoryBase"));
			codeNamespace.Types.Add(codeTypeDeclaration);
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Attributes = (MemberAttributes)24580;
			codeMemberMethod.ReturnType = new CodeTypeReference(typeof(void));
			codeMemberMethod.Name = "ConfigureBrowserCapabilities";
			CodeParameterDeclarationExpression value3 = new CodeParameterDeclarationExpression(typeof(NameValueCollection), "headers");
			codeMemberMethod.Parameters.Add(value3);
			value3 = new CodeParameterDeclarationExpression(typeof(HttpBrowserCapabilities), "browserCaps");
			codeMemberMethod.Parameters.Add(value3);
			codeTypeDeclaration.Members.Add(codeMemberMethod);
			this.GenerateSingleProcessCall(bd, codeMemberMethod);
			for (int j = 0; j < arrayList.Count; j++)
			{
				this.GenerateSingleProcessCall((BrowserDefinition)arrayList[j], codeMemberMethod);
			}
			CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
			codeConditionStatement.Condition = new CodeBinaryOperatorExpression(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "IsBrowserUnknown", new CodeExpression[0])
			{
				Parameters = 
				{
					this._browserCapsRefExpr
				}
			}, CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(false));
			codeConditionStatement.TrueStatements.Add(new CodeMethodReturnStatement());
			codeMemberMethod.Statements.Add(codeConditionStatement);
			if (browserDefinition != null)
			{
				this.GenerateSingleProcessCall(browserDefinition, codeMemberMethod, "Default");
			}
			for (int k = 0; k < arrayList.Count; k++)
			{
				foreach (object obj in ((BrowserTree)this._customTreeList[k]))
				{
					BrowserDefinition bd2 = ((DictionaryEntry)obj).Value as BrowserDefinition;
					this.GenerateProcessMethod(bd2, codeTypeDeclaration);
				}
			}
			foreach (object obj2 in this._browserTree)
			{
				BrowserDefinition bd3 = ((DictionaryEntry)obj2).Value as BrowserDefinition;
				this.GenerateProcessMethod(bd3, codeTypeDeclaration);
			}
			foreach (object obj3 in this._defaultTree)
			{
				BrowserDefinition bd4 = ((DictionaryEntry)obj3).Value as BrowserDefinition;
				this.GenerateProcessMethod(bd4, codeTypeDeclaration, "Default");
			}
			this.GenerateOverrideMatchedHeaders(codeTypeDeclaration);
			this.GenerateOverrideBrowserElements(codeTypeDeclaration);
			TextWriter textWriter = new StreamWriter(new FileStream(BrowserCapabilitiesCodeGenerator._browsersDirectory + "\\BrowserCapsFactory.cs", FileMode.Create));
			try
			{
				csharpCodeProvider.GenerateCodeFromCompileUnit(codeCompileUnit, textWriter, null);
			}
			finally
			{
				if (textWriter != null)
				{
					textWriter.Close();
				}
			}
			CompilationSection compilationAppConfig = MTConfigUtil.GetCompilationAppConfig();
			bool debug = compilationAppConfig.Debug;
			string text = BrowserCapabilitiesCodeGenerator._browsersDirectory + "\\" + BrowserCapabilitiesCodeGenerator._strongNameKeyFileName;
			StrongNameUtility.GenerateStrongNameFile(text);
			string[] assemblyNames = new string[]
			{
				"System.dll",
				"System.Web.dll"
			};
			CompilerParameters compilerParameters = new CompilerParameters(assemblyNames, "ASP.BrowserCapsFactory", debug);
			compilerParameters.GenerateInMemory = false;
			compilerParameters.OutputAssembly = BrowserCapabilitiesCodeGenerator._browsersDirectory + "\\ASP.BrowserCapsFactory.dll";
			CompilerResults compilerResults = null;
			try
			{
				compilerResults = csharpCodeProvider.CompileAssemblyFromFile(compilerParameters, new string[]
				{
					BrowserCapabilitiesCodeGenerator._browsersDirectory + "\\BrowserCapsFactory.cs"
				});
			}
			finally
			{
				if (File.Exists(text))
				{
					File.Delete(text);
				}
			}
			if (compilerResults.NativeCompilerReturnValue != 0 || compilerResults.Errors.HasErrors)
			{
				foreach (object obj4 in compilerResults.Errors)
				{
					CompilerError compilerError = (CompilerError)obj4;
					if (!compilerError.IsWarning)
					{
						throw new HttpCompileException(compilerError.ErrorText);
					}
				}
				throw new HttpCompileException(SR.GetString("Browser_compile_error"));
			}
			Assembly compiledAssembly = compilerResults.CompiledAssembly;
			GacUtil gacUtil = new GacUtil();
			gacUtil.GacInstall(compiledAssembly.Location);
			this.SavePublicKeyTokenFile(BrowserCapabilitiesCodeGenerator._publicKeyTokenFile, compiledAssembly.GetName().GetPublicKeyToken());
		}

		// Token: 0x060051CC RID: 20940 RVA: 0x0011A6D8 File Offset: 0x001188D8
		private void SavePublicKeyTokenFile(string filename, byte[] publicKeyToken)
		{
			using (FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write))
			{
				using (StreamWriter streamWriter = new StreamWriter(fileStream))
				{
					foreach (byte b in publicKeyToken)
					{
						streamWriter.Write("{0:X2}", b);
					}
				}
			}
		}

		// Token: 0x060051CD RID: 20941 RVA: 0x0011A750 File Offset: 0x00118950
		private static string LoadPublicKeyTokenFromFile(string filename)
		{
			IStackWalk stackWalk = InternalSecurityPermissions.FileReadAccess(filename);
			stackWalk.Assert();
			if (!File.Exists(filename))
			{
				return null;
			}
			string result;
			try
			{
				using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
				{
					using (StreamReader streamReader = new StreamReader(fileStream))
					{
						result = streamReader.ReadLine();
					}
				}
			}
			catch (IOException)
			{
				if (HttpRuntime.HasFilePermission(filename))
				{
					throw;
				}
				result = null;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x060051CE RID: 20942 RVA: 0x0011A7EC File Offset: 0x001189EC
		internal void GenerateOverrideBrowserElements(CodeTypeDeclaration typeDeclaration)
		{
			if (this._browserDefinitionCollection == null)
			{
				return;
			}
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Name = "PopulateBrowserElements";
			codeMemberMethod.Attributes = (MemberAttributes)12292;
			codeMemberMethod.ReturnType = new CodeTypeReference(typeof(void));
			CodeParameterDeclarationExpression value = new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(IDictionary)), "dictionary");
			codeMemberMethod.Parameters.Add(value);
			typeDeclaration.Members.Add(codeMemberMethod);
			CodeMethodReferenceExpression method = new CodeMethodReferenceExpression(new CodeBaseReferenceExpression(), "PopulateBrowserElements");
			CodeMethodInvokeExpression value2 = new CodeMethodInvokeExpression(method, new CodeExpression[]
			{
				this._dictionaryRefExpr
			});
			codeMemberMethod.Statements.Add(value2);
			foreach (object obj in this._browserDefinitionCollection)
			{
				BrowserDefinition browserDefinition = (BrowserDefinition)obj;
				if (browserDefinition.IsDeviceNode)
				{
					CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
					codeAssignStatement.Left = new CodeIndexerExpression(this._dictionaryRefExpr, new CodeExpression[]
					{
						new CodePrimitiveExpression(browserDefinition.ID)
					});
					codeAssignStatement.Right = new CodeObjectCreateExpression(typeof(Triplet), new CodeExpression[]
					{
						new CodePrimitiveExpression(browserDefinition.ParentName),
						new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(string)), "Empty"),
						new CodePrimitiveExpression(browserDefinition.Depth)
					});
					codeMemberMethod.Statements.Add(codeAssignStatement);
				}
			}
			for (int i = 0; i < this._customTreeNames.Count; i++)
			{
				foreach (object obj2 in ((BrowserDefinitionCollection)this._customBrowserDefinitionCollections[i]))
				{
					BrowserDefinition browserDefinition2 = (BrowserDefinition)obj2;
					if (browserDefinition2.IsDeviceNode)
					{
						CodeAssignStatement codeAssignStatement2 = new CodeAssignStatement();
						codeAssignStatement2.Left = new CodeIndexerExpression(this._dictionaryRefExpr, new CodeExpression[]
						{
							new CodePrimitiveExpression(browserDefinition2.ID)
						});
						codeAssignStatement2.Right = new CodeObjectCreateExpression(typeof(Triplet), new CodeExpression[]
						{
							new CodePrimitiveExpression(browserDefinition2.ParentName),
							new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(string)), "Empty"),
							new CodePrimitiveExpression(browserDefinition2.Depth)
						});
						codeMemberMethod.Statements.Add(codeAssignStatement2);
					}
				}
			}
		}

		// Token: 0x060051CF RID: 20943 RVA: 0x0011AAB0 File Offset: 0x00118CB0
		internal void GenerateOverrideMatchedHeaders(CodeTypeDeclaration typeDeclaration)
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Name = "PopulateMatchedHeaders";
			codeMemberMethod.Attributes = (MemberAttributes)12292;
			codeMemberMethod.ReturnType = new CodeTypeReference(typeof(void));
			CodeParameterDeclarationExpression value = new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(IDictionary)), "dictionary");
			codeMemberMethod.Parameters.Add(value);
			typeDeclaration.Members.Add(codeMemberMethod);
			CodeMethodReferenceExpression method = new CodeMethodReferenceExpression(new CodeBaseReferenceExpression(), "PopulateMatchedHeaders");
			CodeMethodInvokeExpression value2 = new CodeMethodInvokeExpression(method, new CodeExpression[]
			{
				this._dictionaryRefExpr
			});
			codeMemberMethod.Statements.Add(value2);
			foreach (object obj in ((IEnumerable)this._headers))
			{
				string value3 = (string)obj;
				CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
				codeAssignStatement.Left = new CodeIndexerExpression(this._dictionaryRefExpr, new CodeExpression[]
				{
					new CodePrimitiveExpression(value3)
				});
				codeAssignStatement.Right = new CodePrimitiveExpression(null);
				codeMemberMethod.Statements.Add(codeAssignStatement);
			}
		}

		// Token: 0x060051D0 RID: 20944 RVA: 0x0011ABE8 File Offset: 0x00118DE8
		internal void GenerateProcessMethod(BrowserDefinition bd, CodeTypeDeclaration ctd)
		{
			this.GenerateProcessMethod(bd, ctd, string.Empty);
		}

		// Token: 0x060051D1 RID: 20945 RVA: 0x0011ABF8 File Offset: 0x00118DF8
		internal void GenerateProcessMethod(BrowserDefinition bd, CodeTypeDeclaration ctd, string prefix)
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Name = prefix + bd.Name + "Process";
			codeMemberMethod.ReturnType = new CodeTypeReference(typeof(bool));
			codeMemberMethod.Attributes = MemberAttributes.Private;
			CodeParameterDeclarationExpression value = new CodeParameterDeclarationExpression(typeof(NameValueCollection), "headers");
			codeMemberMethod.Parameters.Add(value);
			value = new CodeParameterDeclarationExpression(typeof(HttpBrowserCapabilities), "browserCaps");
			codeMemberMethod.Parameters.Add(value);
			bool flag = false;
			this.GenerateIdentificationCode(bd, codeMemberMethod, ref flag);
			this.GenerateCapturesCode(bd, codeMemberMethod, ref flag);
			this.GenerateSetCapabilitiesCode(bd, codeMemberMethod, ref flag);
			this.GenerateSetAdaptersCode(bd, codeMemberMethod);
			if (bd.IsDeviceNode)
			{
				CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("browserCaps"), "AddBrowser", new CodeExpression[0]);
				codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(bd.ID));
				codeMemberMethod.Statements.Add(codeMethodInvokeExpression);
			}
			foreach (object obj in bd.RefGateways)
			{
				BrowserDefinition bd2 = (BrowserDefinition)obj;
				this.AddComment("ref gateways, parent=" + bd.ID, codeMemberMethod);
				this.GenerateSingleProcessCall(bd2, codeMemberMethod);
			}
			if (this.GenerateOverrides && prefix.Length == 0)
			{
				string methodName = prefix + bd.Name + "ProcessGateways";
				this.GenerateChildProcessMethod(methodName, ctd, false);
				this.GenerateChildProcessInvokeExpression(methodName, codeMemberMethod, false);
			}
			foreach (object obj2 in bd.Gateways)
			{
				BrowserDefinition bd3 = (BrowserDefinition)obj2;
				this.AddComment("gateway, parent=" + bd.ID, codeMemberMethod);
				this.GenerateSingleProcessCall(bd3, codeMemberMethod);
			}
			if (this.GenerateOverrides)
			{
				CodeVariableDeclarationStatement value2 = new CodeVariableDeclarationStatement(typeof(bool), "ignoreApplicationBrowsers", new CodePrimitiveExpression(bd.Browsers.Count != 0));
				codeMemberMethod.Statements.Add(value2);
			}
			if (bd.Browsers.Count > 0)
			{
				CodeStatementCollection codeStatementCollection = codeMemberMethod.Statements;
				this.AddComment("browser, parent=" + bd.ID, codeMemberMethod);
				foreach (object obj3 in bd.Browsers)
				{
					BrowserDefinition bd4 = (BrowserDefinition)obj3;
					codeStatementCollection = this.GenerateTrackedSingleProcessCall(codeStatementCollection, bd4, codeMemberMethod, prefix);
				}
				if (this.GenerateOverrides)
				{
					codeStatementCollection.Add(new CodeAssignStatement
					{
						Left = new CodeVariableReferenceExpression("ignoreApplicationBrowsers"),
						Right = new CodePrimitiveExpression(false)
					});
				}
			}
			foreach (object obj4 in bd.RefBrowsers)
			{
				BrowserDefinition browserDefinition = (BrowserDefinition)obj4;
				this.AddComment("ref browsers, parent=" + bd.ID, codeMemberMethod);
				if (browserDefinition.IsDefaultBrowser)
				{
					this.GenerateSingleProcessCall(browserDefinition, codeMemberMethod, "Default");
				}
				else
				{
					this.GenerateSingleProcessCall(browserDefinition, codeMemberMethod);
				}
			}
			if (this.GenerateOverrides)
			{
				string methodName2 = prefix + bd.Name + "ProcessBrowsers";
				this.GenerateChildProcessMethod(methodName2, ctd, true);
				this.GenerateChildProcessInvokeExpression(methodName2, codeMemberMethod, true);
			}
			CodeMethodReturnStatement value3 = new CodeMethodReturnStatement(new CodePrimitiveExpression(true));
			codeMemberMethod.Statements.Add(value3);
			ctd.Members.Add(codeMemberMethod);
		}

		// Token: 0x060051D2 RID: 20946 RVA: 0x0011AFE8 File Offset: 0x001191E8
		private void GenerateChildProcessInvokeExpression(string methodName, CodeMemberMethod cmm, bool generateTracker)
		{
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), methodName, new CodeExpression[0]);
			if (generateTracker)
			{
				codeMethodInvokeExpression.Parameters.Add(new CodeVariableReferenceExpression("ignoreApplicationBrowsers"));
			}
			codeMethodInvokeExpression.Parameters.Add(new CodeVariableReferenceExpression("headers"));
			codeMethodInvokeExpression.Parameters.Add(new CodeVariableReferenceExpression("browserCaps"));
			cmm.Statements.Add(codeMethodInvokeExpression);
		}

		// Token: 0x060051D3 RID: 20947 RVA: 0x0011B05C File Offset: 0x0011925C
		private void GenerateChildProcessMethod(string methodName, CodeTypeDeclaration ctd, bool generateTracker)
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Name = methodName;
			codeMemberMethod.ReturnType = new CodeTypeReference(typeof(void));
			codeMemberMethod.Attributes = MemberAttributes.Family;
			CodeParameterDeclarationExpression value;
			if (generateTracker)
			{
				value = new CodeParameterDeclarationExpression(typeof(bool), "ignoreApplicationBrowsers");
				codeMemberMethod.Parameters.Add(value);
			}
			value = new CodeParameterDeclarationExpression(typeof(NameValueCollection), "headers");
			codeMemberMethod.Parameters.Add(value);
			value = new CodeParameterDeclarationExpression(typeof(HttpBrowserCapabilities), "browserCaps");
			codeMemberMethod.Parameters.Add(value);
			ctd.Members.Add(codeMemberMethod);
		}

		// Token: 0x060051D4 RID: 20948 RVA: 0x0011B110 File Offset: 0x00119310
		private void GenerateRegexWorkerIfNecessary(CodeMemberMethod cmm, ref bool regexWorkerGenerated)
		{
			if (regexWorkerGenerated)
			{
				return;
			}
			regexWorkerGenerated = true;
			cmm.Statements.Add(new CodeVariableDeclarationStatement("RegexWorker", "regexWorker"));
			cmm.Statements.Add(new CodeAssignStatement(this._regexWorkerRefExpr, new CodeObjectCreateExpression("RegexWorker", new CodeExpression[]
			{
				this._browserCapsRefExpr
			})));
		}

		// Token: 0x060051D5 RID: 20949 RVA: 0x0011B170 File Offset: 0x00119370
		private void ReturnIfHeaderValueEmpty(CodeMemberMethod cmm, CodeVariableReferenceExpression varExpr)
		{
			CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
			CodeMethodReferenceExpression method = new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(string)), "IsNullOrEmpty");
			CodeMethodInvokeExpression condition = new CodeMethodInvokeExpression(method, new CodeExpression[]
			{
				varExpr
			});
			codeConditionStatement.Condition = condition;
			codeConditionStatement.TrueStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(false)));
			cmm.Statements.Add(codeConditionStatement);
		}

		// Token: 0x060051D6 RID: 20950 RVA: 0x0011B1E0 File Offset: 0x001193E0
		private void GenerateIdentificationCode(BrowserDefinition bd, CodeMemberMethod cmm, ref bool regexWorkerGenerated)
		{
			cmm.Statements.Add(new CodeVariableDeclarationStatement(typeof(IDictionary), "dictionary"));
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement(this._dictionaryRefExpr, new CodePropertyReferenceExpression(this._browserCapsRefExpr, "Capabilities"));
			cmm.Statements.Add(codeAssignStatement);
			bool flag = false;
			CodeVariableReferenceExpression codeVariableReferenceExpression = null;
			CodeVariableReferenceExpression codeVariableReferenceExpression2 = null;
			if (bd.IdHeaderChecks.Count > 0)
			{
				this.AddComment("Identification: check header matches", cmm);
				for (int i = 0; i < bd.IdHeaderChecks.Count; i++)
				{
					string matchString = ((CheckPair)bd.IdHeaderChecks[i]).MatchString;
					if (!matchString.Equals(".*"))
					{
						if (codeVariableReferenceExpression2 == null)
						{
							codeVariableReferenceExpression2 = this.GenerateVarReference(cmm, typeof(string), "headerValue");
						}
						CodeAssignStatement codeAssignStatement2 = new CodeAssignStatement();
						cmm.Statements.Add(codeAssignStatement2);
						codeAssignStatement2.Left = codeVariableReferenceExpression2;
						if (((CheckPair)bd.IdHeaderChecks[i]).Header.Equals("User-Agent"))
						{
							this._headers.Add(string.Empty);
							codeAssignStatement2.Right = new CodeCastExpression(typeof(string), new CodeIndexerExpression(new CodeVariableReferenceExpression("browserCaps"), new CodeExpression[]
							{
								new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(string)), "Empty")
							}));
						}
						else
						{
							string header = ((CheckPair)bd.IdHeaderChecks[i]).Header;
							this._headers.Add(header);
							codeAssignStatement2.Right = new CodeCastExpression(typeof(string), new CodeIndexerExpression(this._headersRefExpr, new CodeExpression[]
							{
								new CodePrimitiveExpression(header)
							}));
							flag = true;
						}
						if (matchString.Equals("."))
						{
							this.ReturnIfHeaderValueEmpty(cmm, codeVariableReferenceExpression2);
						}
						else
						{
							if (codeVariableReferenceExpression == null)
							{
								codeVariableReferenceExpression = this.GenerateVarReference(cmm, typeof(bool), "result");
							}
							this.GenerateRegexWorkerIfNecessary(cmm, ref regexWorkerGenerated);
							CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(this._regexWorkerRefExpr, "ProcessRegex", new CodeExpression[0]);
							codeMethodInvokeExpression.Parameters.Add(codeVariableReferenceExpression2);
							codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(matchString));
							codeAssignStatement = new CodeAssignStatement();
							codeAssignStatement.Left = codeVariableReferenceExpression;
							codeAssignStatement.Right = codeMethodInvokeExpression;
							cmm.Statements.Add(codeAssignStatement);
							CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
							if (((CheckPair)bd.IdHeaderChecks[i]).NonMatch)
							{
								codeConditionStatement.Condition = new CodeBinaryOperatorExpression(codeVariableReferenceExpression, CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(true));
							}
							else
							{
								codeConditionStatement.Condition = new CodeBinaryOperatorExpression(codeVariableReferenceExpression, CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(false));
							}
							codeConditionStatement.TrueStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(false)));
							cmm.Statements.Add(codeConditionStatement);
						}
					}
				}
			}
			if (bd.IdCapabilityChecks.Count > 0)
			{
				this.AddComment("Identification: check capability matches", cmm);
				for (int j = 0; j < bd.IdCapabilityChecks.Count; j++)
				{
					string matchString2 = ((CheckPair)bd.IdCapabilityChecks[j]).MatchString;
					if (!matchString2.Equals(".*"))
					{
						if (codeVariableReferenceExpression2 == null)
						{
							codeVariableReferenceExpression2 = this.GenerateVarReference(cmm, typeof(string), "headerValue");
						}
						CodeAssignStatement codeAssignStatement3 = new CodeAssignStatement();
						cmm.Statements.Add(codeAssignStatement3);
						codeAssignStatement3.Left = codeVariableReferenceExpression2;
						codeAssignStatement3.Right = new CodeCastExpression(typeof(string), new CodeIndexerExpression(this._dictionaryRefExpr, new CodeExpression[]
						{
							new CodePrimitiveExpression(((CheckPair)bd.IdCapabilityChecks[j]).Header)
						}));
						if (!matchString2.Equals("."))
						{
							if (codeVariableReferenceExpression == null)
							{
								codeVariableReferenceExpression = this.GenerateVarReference(cmm, typeof(bool), "result");
							}
							this.GenerateRegexWorkerIfNecessary(cmm, ref regexWorkerGenerated);
							CodeMethodInvokeExpression codeMethodInvokeExpression2 = new CodeMethodInvokeExpression(this._regexWorkerRefExpr, "ProcessRegex", new CodeExpression[0]);
							codeMethodInvokeExpression2.Parameters.Add(codeVariableReferenceExpression2);
							codeMethodInvokeExpression2.Parameters.Add(new CodePrimitiveExpression(matchString2));
							codeAssignStatement = new CodeAssignStatement();
							codeAssignStatement.Left = codeVariableReferenceExpression;
							codeAssignStatement.Right = codeMethodInvokeExpression2;
							cmm.Statements.Add(codeAssignStatement);
							CodeConditionStatement codeConditionStatement2 = new CodeConditionStatement();
							if (((CheckPair)bd.IdCapabilityChecks[j]).NonMatch)
							{
								codeConditionStatement2.Condition = new CodeBinaryOperatorExpression(codeVariableReferenceExpression, CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(true));
							}
							else
							{
								codeConditionStatement2.Condition = new CodeBinaryOperatorExpression(codeVariableReferenceExpression, CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(false));
							}
							codeConditionStatement2.TrueStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(false)));
							cmm.Statements.Add(codeConditionStatement2);
						}
					}
				}
			}
			if (flag)
			{
				CodeMethodInvokeExpression value = new CodeMethodInvokeExpression(this._browserCapsRefExpr, "DisableOptimizedCacheKey", new CodeExpression[0]);
				cmm.Statements.Add(value);
			}
		}

		// Token: 0x060051D7 RID: 20951 RVA: 0x0011B6E2 File Offset: 0x001198E2
		private CodeVariableReferenceExpression GenerateVarReference(CodeMemberMethod cmm, Type varType, string varName)
		{
			cmm.Statements.Add(new CodeVariableDeclarationStatement(varType, varName));
			return new CodeVariableReferenceExpression(varName);
		}

		// Token: 0x060051D8 RID: 20952 RVA: 0x0011B700 File Offset: 0x00119900
		private void GenerateCapturesCode(BrowserDefinition bd, CodeMemberMethod cmm, ref bool regexWorkerGenerated)
		{
			if (bd.CaptureHeaderChecks.Count == 0 && bd.CaptureCapabilityChecks.Count == 0)
			{
				return;
			}
			if (bd.CaptureHeaderChecks.Count > 0)
			{
				this.AddComment("Capture: header values", cmm);
				for (int i = 0; i < bd.CaptureHeaderChecks.Count; i++)
				{
					string matchString = ((CheckPair)bd.CaptureHeaderChecks[i]).MatchString;
					if (!matchString.Equals(".*"))
					{
						this.GenerateRegexWorkerIfNecessary(cmm, ref regexWorkerGenerated);
						CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(this._regexWorkerRefExpr, "ProcessRegex", new CodeExpression[0]);
						if (((CheckPair)bd.CaptureHeaderChecks[i]).Header.Equals("User-Agent"))
						{
							this._headers.Add(string.Empty);
							codeMethodInvokeExpression.Parameters.Add(new CodeCastExpression(typeof(string), new CodeIndexerExpression(new CodeVariableReferenceExpression("browserCaps"), new CodeExpression[]
							{
								new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(string)), "Empty")
							})));
						}
						else
						{
							string header = ((CheckPair)bd.CaptureHeaderChecks[i]).Header;
							this._headers.Add(header);
							codeMethodInvokeExpression.Parameters.Add(new CodeCastExpression(typeof(string), new CodeIndexerExpression(this._headersRefExpr, new CodeExpression[]
							{
								new CodePrimitiveExpression(header)
							})));
						}
						codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(matchString));
						cmm.Statements.Add(codeMethodInvokeExpression);
					}
				}
			}
			if (bd.CaptureCapabilityChecks.Count > 0)
			{
				this.AddComment("Capture: capability values", cmm);
				for (int j = 0; j < bd.CaptureCapabilityChecks.Count; j++)
				{
					string matchString2 = ((CheckPair)bd.CaptureCapabilityChecks[j]).MatchString;
					if (!matchString2.Equals(".*"))
					{
						this.GenerateRegexWorkerIfNecessary(cmm, ref regexWorkerGenerated);
						CodeMethodInvokeExpression codeMethodInvokeExpression2 = new CodeMethodInvokeExpression(this._regexWorkerRefExpr, "ProcessRegex", new CodeExpression[0]);
						codeMethodInvokeExpression2.Parameters.Add(new CodeCastExpression(typeof(string), new CodeIndexerExpression(this._dictionaryRefExpr, new CodeExpression[]
						{
							new CodePrimitiveExpression(((CheckPair)bd.CaptureCapabilityChecks[j]).Header)
						})));
						codeMethodInvokeExpression2.Parameters.Add(new CodePrimitiveExpression(matchString2));
						cmm.Statements.Add(codeMethodInvokeExpression2);
					}
				}
			}
		}

		// Token: 0x060051D9 RID: 20953 RVA: 0x0011B994 File Offset: 0x00119B94
		private void GenerateSetCapabilitiesCode(BrowserDefinition bd, CodeMemberMethod cmm, ref bool regexWorkerGenerated)
		{
			NameValueCollection capabilities = bd.Capabilities;
			this.AddComment("Capabilities: set capabilities", cmm);
			foreach (object obj in capabilities.Keys)
			{
				string text = (string)obj;
				string text2 = capabilities[text];
				CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
				codeAssignStatement.Left = new CodeIndexerExpression(this._dictionaryRefExpr, new CodeExpression[]
				{
					new CodePrimitiveExpression(text)
				});
				CodePrimitiveExpression codePrimitiveExpression = new CodePrimitiveExpression(text2);
				if (RegexWorker.RefPat.Match(text2).Success)
				{
					this.GenerateRegexWorkerIfNecessary(cmm, ref regexWorkerGenerated);
					codeAssignStatement.Right = new CodeIndexerExpression(this._regexWorkerRefExpr, new CodeExpression[]
					{
						codePrimitiveExpression
					});
				}
				else
				{
					codeAssignStatement.Right = codePrimitiveExpression;
				}
				cmm.Statements.Add(codeAssignStatement);
			}
		}

		// Token: 0x060051DA RID: 20954 RVA: 0x0011BA88 File Offset: 0x00119C88
		internal void GenerateSetAdaptersCode(BrowserDefinition bd, CodeMemberMethod cmm)
		{
			foreach (object obj in bd.Adapters)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string value = (string)dictionaryEntry.Key;
				string value2 = (string)dictionaryEntry.Value;
				CodePropertyReferenceExpression targetObject = new CodePropertyReferenceExpression(this._browserCapsRefExpr, "Adapters");
				CodeIndexerExpression left = new CodeIndexerExpression(targetObject, new CodeExpression[]
				{
					new CodePrimitiveExpression(value)
				});
				CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
				codeAssignStatement.Left = left;
				codeAssignStatement.Right = new CodePrimitiveExpression(value2);
				cmm.Statements.Add(codeAssignStatement);
			}
			if (bd.HtmlTextWriterString != null)
			{
				CodeAssignStatement codeAssignStatement2 = new CodeAssignStatement();
				codeAssignStatement2.Left = new CodePropertyReferenceExpression(this._browserCapsRefExpr, "HtmlTextWriter");
				codeAssignStatement2.Right = new CodePrimitiveExpression(bd.HtmlTextWriterString);
				cmm.Statements.Add(codeAssignStatement2);
			}
		}

		// Token: 0x060051DB RID: 20955 RVA: 0x0011BB98 File Offset: 0x00119D98
		internal void AddComment(string comment, CodeMemberMethod cmm)
		{
			cmm.Statements.Add(new CodeCommentStatement(comment));
		}

		// Token: 0x060051DC RID: 20956 RVA: 0x0011BBAC File Offset: 0x00119DAC
		internal CodeStatementCollection GenerateTrackedSingleProcessCall(CodeStatementCollection stmts, BrowserDefinition bd, CodeMemberMethod cmm)
		{
			return this.GenerateTrackedSingleProcessCall(stmts, bd, cmm, string.Empty);
		}

		// Token: 0x060051DD RID: 20957 RVA: 0x0011BBBC File Offset: 0x00119DBC
		internal CodeStatementCollection GenerateTrackedSingleProcessCall(CodeStatementCollection stmts, BrowserDefinition bd, CodeMemberMethod cmm, string prefix)
		{
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), prefix + bd.Name + "Process", new CodeExpression[0]);
			codeMethodInvokeExpression.Parameters.Add(new CodeVariableReferenceExpression("headers"));
			codeMethodInvokeExpression.Parameters.Add(new CodeVariableReferenceExpression("browserCaps"));
			CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
			codeConditionStatement.Condition = codeMethodInvokeExpression;
			stmts.Add(codeConditionStatement);
			return codeConditionStatement.FalseStatements;
		}

		// Token: 0x060051DE RID: 20958 RVA: 0x0011BC33 File Offset: 0x00119E33
		internal void GenerateSingleProcessCall(BrowserDefinition bd, CodeMemberMethod cmm)
		{
			this.GenerateSingleProcessCall(bd, cmm, string.Empty);
		}

		// Token: 0x060051DF RID: 20959 RVA: 0x0011BC44 File Offset: 0x00119E44
		internal void GenerateSingleProcessCall(BrowserDefinition bd, CodeMemberMethod cmm, string prefix)
		{
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), prefix + bd.Name + "Process", new CodeExpression[0]);
			codeMethodInvokeExpression.Parameters.Add(new CodeVariableReferenceExpression("headers"));
			codeMethodInvokeExpression.Parameters.Add(new CodeVariableReferenceExpression("browserCaps"));
			cmm.Statements.Add(codeMethodInvokeExpression);
		}

		// Token: 0x04002B32 RID: 11058
		private static readonly string _browsersDirectory;

		// Token: 0x04002B33 RID: 11059
		private static readonly string _publicKeyTokenFile;

		// Token: 0x04002B34 RID: 11060
		private static object _staticLock = new object();

		// Token: 0x04002B35 RID: 11061
		private BrowserTree _browserTree;

		// Token: 0x04002B36 RID: 11062
		private BrowserTree _defaultTree;

		// Token: 0x04002B37 RID: 11063
		private BrowserDefinitionCollection _browserDefinitionCollection;

		// Token: 0x04002B38 RID: 11064
		internal const string browserCapsVariable = "browserCaps";

		// Token: 0x04002B39 RID: 11065
		internal const string IgnoreApplicationBrowserVariableName = "ignoreApplicationBrowsers";

		// Token: 0x04002B3A RID: 11066
		private const string _factoryTypeName = "BrowserCapabilitiesFactory";

		// Token: 0x04002B3B RID: 11067
		private const string _headerDictionaryVarName = "_headerDictionary";

		// Token: 0x04002B3C RID: 11068
		private const string _disableOptimizedCacheKeyMethodName = "DisableOptimizedCacheKey";

		// Token: 0x04002B3D RID: 11069
		private const string _matchedHeadersMethodName = "PopulateMatchedHeaders";

		// Token: 0x04002B3E RID: 11070
		private const string _browserElementsMethodName = "PopulateBrowserElements";

		// Token: 0x04002B3F RID: 11071
		private const string _dictionaryRefName = "dictionary";

		// Token: 0x04002B40 RID: 11072
		private const string _regexWorkerRefName = "regexWorker";

		// Token: 0x04002B41 RID: 11073
		private const string _headersRefName = "headers";

		// Token: 0x04002B42 RID: 11074
		private const string _resultVarName = "result";

		// Token: 0x04002B43 RID: 11075
		private const string _processRegexMethod = "ProcessRegex";

		// Token: 0x04002B44 RID: 11076
		private static readonly string _strongNameKeyFileName = "browserCaps.snk";

		// Token: 0x04002B45 RID: 11077
		private static readonly string _publicKeyTokenFileName = "browserCaps.token";

		// Token: 0x04002B46 RID: 11078
		private static bool _publicKeyTokenLoaded;

		// Token: 0x04002B47 RID: 11079
		private static string _publicKeyToken;

		// Token: 0x04002B48 RID: 11080
		private CodeVariableReferenceExpression _dictionaryRefExpr = new CodeVariableReferenceExpression("dictionary");

		// Token: 0x04002B49 RID: 11081
		private CodeVariableReferenceExpression _regexWorkerRefExpr = new CodeVariableReferenceExpression("regexWorker");

		// Token: 0x04002B4A RID: 11082
		private CodeVariableReferenceExpression _headersRefExpr = new CodeVariableReferenceExpression("headers");

		// Token: 0x04002B4B RID: 11083
		private CodeVariableReferenceExpression _browserCapsRefExpr = new CodeVariableReferenceExpression("browserCaps");

		// Token: 0x04002B4C RID: 11084
		private ArrayList _browserFileList;

		// Token: 0x04002B4D RID: 11085
		private ArrayList _customBrowserFileLists;

		// Token: 0x04002B4E RID: 11086
		private ArrayList _customTreeList;

		// Token: 0x04002B4F RID: 11087
		private ArrayList _customTreeNames;

		// Token: 0x04002B50 RID: 11088
		private ArrayList _customBrowserDefinitionCollections;

		// Token: 0x04002B51 RID: 11089
		private CaseInsensitiveStringSet _headers;
	}
}
