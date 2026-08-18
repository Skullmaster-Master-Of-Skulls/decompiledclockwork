using System;
using System.Collections;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Hosting;
using System.Web.UI.Design;

namespace System.Web.UI
{
	// Token: 0x02000306 RID: 774
	internal class NamespaceTagNameToTypeMapper : ITagNameToTypeMapper
	{
		// Token: 0x060023C3 RID: 9155 RVA: 0x000746FB File Offset: 0x000728FB
		internal NamespaceTagNameToTypeMapper(TagNamespaceRegisterEntry nsRegisterEntry, Assembly assembly, TemplateParser parser)
		{
			this._nsRegisterEntry = nsRegisterEntry;
			this._assembly = assembly;
			this._parser = parser;
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x060023C4 RID: 9156 RVA: 0x00074718 File Offset: 0x00072918
		public TagNamespaceRegisterEntry RegisterEntry
		{
			get
			{
				return this._nsRegisterEntry;
			}
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x00074720 File Offset: 0x00072920
		Type ITagNameToTypeMapper.GetControlType(string tagName, IDictionary attribs)
		{
			return this.GetControlType(tagName, attribs, false);
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x0007472C File Offset: 0x0007292C
		internal Type GetControlType(string tagName, IDictionary attribs, bool throwOnError)
		{
			string @namespace = this._nsRegisterEntry.Namespace;
			string text;
			if (string.IsNullOrEmpty(@namespace))
			{
				text = tagName;
			}
			else
			{
				text = @namespace + "." + tagName;
			}
			if (this._assembly != null)
			{
				Type result = null;
				if (throwOnError)
				{
					try
					{
						return this._assembly.GetType(text, true, true);
					}
					catch (FileNotFoundException)
					{
						throw;
					}
					catch (FileLoadException)
					{
						throw;
					}
					catch (BadImageFormatException)
					{
						throw;
					}
					catch
					{
						return result;
					}
				}
				result = this._assembly.GetType(text, false, true);
				return result;
			}
			if (this._parser.FInDesigner && this._parser.DesignerHost != null)
			{
				if (this._parser.DesignerHost.RootComponent != null)
				{
					WebFormsRootDesigner webFormsRootDesigner = this._parser.DesignerHost.GetDesigner(this._parser.DesignerHost.RootComponent) as WebFormsRootDesigner;
					if (webFormsRootDesigner != null)
					{
						WebFormsReferenceManager referenceManager = webFormsRootDesigner.ReferenceManager;
						if (referenceManager != null)
						{
							Type type = referenceManager.GetType(this._nsRegisterEntry.TagPrefix, tagName);
							if (type != null)
							{
								return type;
							}
						}
					}
				}
				ITypeResolutionService typeResolutionService = (ITypeResolutionService)this._parser.DesignerHost.GetService(typeof(ITypeResolutionService));
				if (typeResolutionService != null)
				{
					Type type2 = typeResolutionService.GetType(text, false, true);
					if (type2 != null)
					{
						return type2;
					}
				}
			}
			if (!HostingEnvironment.IsHosted)
			{
				return null;
			}
			return BuildManager.GetTypeFromCodeAssembly(text, true);
		}

		// Token: 0x04001CCB RID: 7371
		private TagNamespaceRegisterEntry _nsRegisterEntry;

		// Token: 0x04001CCC RID: 7372
		private Assembly _assembly;

		// Token: 0x04001CCD RID: 7373
		private TemplateParser _parser;
	}
}
