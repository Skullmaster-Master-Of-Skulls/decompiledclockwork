using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Configuration;

namespace System.Web.UI
{
	// Token: 0x02000308 RID: 776
	internal class MainTagNameToTypeMapper
	{
		// Token: 0x060023CA RID: 9162 RVA: 0x00074AAC File Offset: 0x00072CAC
		internal MainTagNameToTypeMapper(BaseTemplateParser parser)
		{
			this._parser = parser;
			if (parser != null)
			{
				PagesSection pagesConfig = parser.PagesConfig;
				if (pagesConfig != null)
				{
					this._tagNamespaceRegisterEntries = pagesConfig.TagNamespaceRegisterEntriesInternal;
					if (this._tagNamespaceRegisterEntries != null)
					{
						this._tagNamespaceRegisterEntries = (TagNamespaceRegisterEntryTable)this._tagNamespaceRegisterEntries.Clone();
					}
					this._userControlRegisterEntries = pagesConfig.UserControlRegisterEntriesInternal;
					if (this._userControlRegisterEntries != null)
					{
						this._userControlRegisterEntries = (Hashtable)this._userControlRegisterEntries.Clone();
					}
				}
				if (parser.FInDesigner && this._tagNamespaceRegisterEntries == null)
				{
					this._tagNamespaceRegisterEntries = new TagNamespaceRegisterEntryTable();
					foreach (object obj in PagesSection.DefaultTagNamespaceRegisterEntries)
					{
						TagNamespaceRegisterEntry tagNamespaceRegisterEntry = (TagNamespaceRegisterEntry)obj;
						this._tagNamespaceRegisterEntries[tagNamespaceRegisterEntry.TagPrefix] = new ArrayList(new object[]
						{
							tagNamespaceRegisterEntry
						});
					}
				}
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x060023CB RID: 9163 RVA: 0x00074BB4 File Offset: 0x00072DB4
		internal ICollection UserControlRegisterEntries
		{
			get
			{
				if (this._userControlRegisterEntries != null)
				{
					return this._userControlRegisterEntries.Values;
				}
				return null;
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x060023CC RID: 9164 RVA: 0x00074BCB File Offset: 0x00072DCB
		internal List<TagNamespaceRegisterEntry> TagRegisterEntries
		{
			get
			{
				if (this._tagRegisterEntries == null)
				{
					this._tagRegisterEntries = new List<TagNamespaceRegisterEntry>();
				}
				return this._tagRegisterEntries;
			}
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x00074BE8 File Offset: 0x00072DE8
		internal void ProcessTagNamespaceRegistration(TagNamespaceRegisterEntry nsRegisterEntry)
		{
			string tagPrefix = nsRegisterEntry.TagPrefix;
			ArrayList arrayList = null;
			if (this._tagNamespaceRegisterEntries != null)
			{
				arrayList = (ArrayList)this._tagNamespaceRegisterEntries[tagPrefix];
			}
			if (arrayList != null && (this._prefixedMappers == null || this._prefixedMappers[tagPrefix] == null))
			{
				this.ProcessTagNamespaceRegistration(arrayList);
			}
			this.ProcessTagNamespaceRegistrationCore(nsRegisterEntry);
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x00074C40 File Offset: 0x00072E40
		private void ProcessTagNamespaceRegistration(ArrayList nsRegisterEntries)
		{
			foreach (object obj in nsRegisterEntries)
			{
				TagNamespaceRegisterEntry tagNamespaceRegisterEntry = (TagNamespaceRegisterEntry)obj;
				try
				{
					this.ProcessTagNamespaceRegistrationCore(tagNamespaceRegisterEntry);
				}
				catch (Exception ex)
				{
					throw new HttpParseException(ex.Message, ex, tagNamespaceRegisterEntry.VirtualPath, null, tagNamespaceRegisterEntry.Line);
				}
			}
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x00074CC0 File Offset: 0x00072EC0
		private void ProcessTagNamespaceRegistrationCore(TagNamespaceRegisterEntry nsRegisterEntry)
		{
			Assembly assembly = null;
			if (!string.IsNullOrEmpty(nsRegisterEntry.AssemblyName))
			{
				assembly = this._parser.AddAssemblyDependency(nsRegisterEntry.AssemblyName);
			}
			if (!string.IsNullOrEmpty(nsRegisterEntry.Namespace))
			{
				this._parser.AddImportEntry(nsRegisterEntry.Namespace);
			}
			NamespaceTagNameToTypeMapper mapper = new NamespaceTagNameToTypeMapper(nsRegisterEntry, assembly, this._parser);
			if (this._prefixedMappers == null)
			{
				this._prefixedMappers = new Hashtable(StringComparer.OrdinalIgnoreCase);
			}
			TagPrefixTagNameToTypeMapper tagPrefixTagNameToTypeMapper = (TagPrefixTagNameToTypeMapper)this._prefixedMappers[nsRegisterEntry.TagPrefix];
			if (tagPrefixTagNameToTypeMapper == null)
			{
				tagPrefixTagNameToTypeMapper = new TagPrefixTagNameToTypeMapper(nsRegisterEntry.TagPrefix);
				this._prefixedMappers[nsRegisterEntry.TagPrefix] = tagPrefixTagNameToTypeMapper;
			}
			tagPrefixTagNameToTypeMapper.AddNamespaceMapper(mapper);
			this.TagRegisterEntries.Add(nsRegisterEntry);
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x00074D80 File Offset: 0x00072F80
		internal void ProcessUserControlRegistration(UserControlRegisterEntry ucRegisterEntry)
		{
			Type type;
			if (this._parser.FInDesigner)
			{
				type = this._parser.GetDesignTimeUserControlType(ucRegisterEntry.TagPrefix, ucRegisterEntry.TagName);
			}
			else
			{
				type = this._parser.GetUserControlType(ucRegisterEntry.UserControlSource.VirtualPathString);
			}
			if (type == null)
			{
				return;
			}
			if (this._userControlRegisterEntries == null)
			{
				this._userControlRegisterEntries = new Hashtable();
			}
			this._userControlRegisterEntries[ucRegisterEntry.TagPrefix + ":" + ucRegisterEntry.TagName] = ucRegisterEntry;
			this.RegisterTag(ucRegisterEntry.TagPrefix + ":" + ucRegisterEntry.TagName, type);
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x00074E2C File Offset: 0x0007302C
		private bool TryUserControlRegisterDirectives(string tagName)
		{
			if (this._userControlRegisterEntries == null)
			{
				return false;
			}
			UserControlRegisterEntry userControlRegisterEntry = (UserControlRegisterEntry)this._userControlRegisterEntries[tagName];
			if (userControlRegisterEntry == null)
			{
				return false;
			}
			if (userControlRegisterEntry.ComesFromConfig)
			{
				VirtualPath parent = userControlRegisterEntry.UserControlSource.Parent;
				if (parent == this._parser.BaseVirtualDir)
				{
					throw new HttpException(SR.GetString("Invalid_use_of_config_uc", new object[]
					{
						this._parser.CurrentVirtualPath,
						userControlRegisterEntry.UserControlSource
					}));
				}
			}
			try
			{
				this.ProcessUserControlRegistration(userControlRegisterEntry);
			}
			catch (Exception ex)
			{
				throw new HttpParseException(ex.Message, ex, userControlRegisterEntry.VirtualPath, null, userControlRegisterEntry.Line);
			}
			return true;
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x00074EE4 File Offset: 0x000730E4
		private bool TryNamespaceRegisterDirectives(string prefix)
		{
			if (this._tagNamespaceRegisterEntries == null)
			{
				return false;
			}
			ArrayList arrayList = (ArrayList)this._tagNamespaceRegisterEntries[prefix];
			if (arrayList == null)
			{
				return false;
			}
			this.ProcessTagNamespaceRegistration(arrayList);
			return true;
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x00074F1C File Offset: 0x0007311C
		internal void RegisterTag(string tagName, Type type)
		{
			if (this._mappedTags == null)
			{
				this._mappedTags = new Hashtable(StringComparer.OrdinalIgnoreCase);
			}
			try
			{
				this._mappedTags.Add(tagName, type);
			}
			catch (ArgumentException)
			{
				throw new HttpException(SR.GetString("Duplicate_registered_tag", new object[]
				{
					tagName
				}));
			}
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x00074F7C File Offset: 0x0007317C
		internal Type GetControlType(string tagName, IDictionary attribs, bool fAllowHtmlTags)
		{
			Type type = this.GetControlType2(tagName, attribs, fAllowHtmlTags);
			if (type != null && this._parser != null && !this._parser.FInDesigner)
			{
				Hashtable tagTypeMappingInternal = this._parser.PagesConfig.TagMapping.TagTypeMappingInternal;
				if (tagTypeMappingInternal != null)
				{
					Type type2 = (Type)tagTypeMappingInternal[type];
					if (type2 != null)
					{
						type = type2;
					}
				}
			}
			return type;
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x00074FE4 File Offset: 0x000731E4
		private Type GetControlType2(string tagName, IDictionary attribs, bool fAllowHtmlTags)
		{
			if (this._mappedTags != null)
			{
				Type type = (Type)this._mappedTags[tagName];
				if (type == null && this.TryUserControlRegisterDirectives(tagName))
				{
					type = (Type)this._mappedTags[tagName];
				}
				if (type != null)
				{
					if (this._parser != null && this._parser._pageParserFilter != null && this._parser._pageParserFilter.GetNoCompileUserControlType() == type)
					{
						UserControlRegisterEntry userControlRegisterEntry = (UserControlRegisterEntry)this._userControlRegisterEntries[tagName];
						attribs["virtualpath"] = userControlRegisterEntry.UserControlSource;
					}
					return type;
				}
			}
			int num = tagName.IndexOf(':');
			if (num >= 0)
			{
				if (num == tagName.Length - 1)
				{
					return null;
				}
				string text = tagName.Substring(0, num);
				tagName = tagName.Substring(num + 1);
				ITagNameToTypeMapper tagNameToTypeMapper = null;
				if (this._prefixedMappers != null)
				{
					tagNameToTypeMapper = (ITagNameToTypeMapper)this._prefixedMappers[text];
				}
				if (tagNameToTypeMapper == null && this.TryNamespaceRegisterDirectives(text) && this._prefixedMappers != null)
				{
					tagNameToTypeMapper = (ITagNameToTypeMapper)this._prefixedMappers[text];
				}
				if (tagNameToTypeMapper == null)
				{
					return null;
				}
				return tagNameToTypeMapper.GetControlType(tagName, attribs);
			}
			else
			{
				if (fAllowHtmlTags)
				{
					return this._htmlMapper.GetControlType(tagName, attribs);
				}
				return null;
			}
		}

		// Token: 0x04001CD0 RID: 7376
		private BaseTemplateParser _parser;

		// Token: 0x04001CD1 RID: 7377
		private IDictionary _prefixedMappers;

		// Token: 0x04001CD2 RID: 7378
		private IDictionary _mappedTags;

		// Token: 0x04001CD3 RID: 7379
		private ITagNameToTypeMapper _htmlMapper = new HtmlTagNameToTypeMapper();

		// Token: 0x04001CD4 RID: 7380
		private Hashtable _userControlRegisterEntries;

		// Token: 0x04001CD5 RID: 7381
		private List<TagNamespaceRegisterEntry> _tagRegisterEntries;

		// Token: 0x04001CD6 RID: 7382
		private TagNamespaceRegisterEntryTable _tagNamespaceRegisterEntries;
	}
}
