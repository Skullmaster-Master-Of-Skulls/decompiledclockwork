using System;
using System.Collections;
using System.IO;

namespace System.Web.UI
{
	// Token: 0x0200046C RID: 1132
	internal class TagPrefixTagNameToTypeMapper : ITagNameToTypeMapper
	{
		// Token: 0x06003594 RID: 13716 RVA: 0x000E75E8 File Offset: 0x000E65E8
		internal TagPrefixTagNameToTypeMapper(string tagPrefix)
		{
			this._tagPrefix = tagPrefix;
			this._mappers = new ArrayList();
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x000E7602 File Offset: 0x000E6602
		internal void AddNamespaceMapper(NamespaceTagNameToTypeMapper mapper)
		{
			this._mappers.Add(mapper);
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x000E7614 File Offset: 0x000E6614
		Type ITagNameToTypeMapper.GetControlType(string tagName, IDictionary attribs)
		{
			Type type = null;
			Exception ex = null;
			foreach (object obj in this._mappers)
			{
				NamespaceTagNameToTypeMapper namespaceTagNameToTypeMapper = (NamespaceTagNameToTypeMapper)obj;
				Type controlType = ((ITagNameToTypeMapper)namespaceTagNameToTypeMapper).GetControlType(tagName, attribs);
				if (controlType != null)
				{
					if (type == null)
					{
						type = controlType;
					}
					else if (type != controlType)
					{
						throw new HttpParseException(SR.GetString("Ambiguous_server_tag", new object[]
						{
							this._tagPrefix + ":" + tagName
						}), null, namespaceTagNameToTypeMapper.RegisterEntry.VirtualPath, null, namespaceTagNameToTypeMapper.RegisterEntry.Line);
					}
				}
			}
			if (type == null)
			{
				try
				{
					foreach (object obj2 in this._mappers)
					{
						NamespaceTagNameToTypeMapper namespaceTagNameToTypeMapper2 = (NamespaceTagNameToTypeMapper)obj2;
						namespaceTagNameToTypeMapper2.GetControlType(tagName, attribs, true);
					}
				}
				catch (FileNotFoundException ex2)
				{
					ex = ex2;
				}
				catch (FileLoadException ex3)
				{
					ex = ex3;
				}
				catch (BadImageFormatException ex4)
				{
					ex = ex4;
				}
			}
			if (ex != null)
			{
				throw new HttpException(SR.GetString("ControlAdapters_TypeNotFound", new object[]
				{
					this._tagPrefix + ":" + tagName
				}) + " " + ex.Message, ex);
			}
			if (type == null)
			{
				throw new HttpException(SR.GetString("Unknown_server_tag", new object[]
				{
					this._tagPrefix + ":" + tagName
				}));
			}
			return type;
		}

		// Token: 0x04002537 RID: 9527
		private string _tagPrefix;

		// Token: 0x04002538 RID: 9528
		private ArrayList _mappers;
	}
}
