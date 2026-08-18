using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200039F RID: 927
	internal class ContentBuilderInternal : TemplateBuilder
	{
		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06002C50 RID: 11344 RVA: 0x000908B9 File Offset: 0x0008EAB9
		public override Type BindingContainerType
		{
			get
			{
				return typeof(Control);
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06002C51 RID: 11345 RVA: 0x000908C5 File Offset: 0x0008EAC5
		internal string ContentPlaceHolderFilter
		{
			get
			{
				return this._contentPlaceHolderFilter;
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06002C52 RID: 11346 RVA: 0x000908CD File Offset: 0x0008EACD
		internal string ContentPlaceHolder
		{
			get
			{
				return this._contentPlaceHolder;
			}
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x000908D5 File Offset: 0x0008EAD5
		public override object BuildObject()
		{
			if (base.InDesigner)
			{
				return base.BuildObjectInternal();
			}
			return base.BuildObject();
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x000908EC File Offset: 0x0008EAEC
		public override void InstantiateIn(Control container)
		{
			base.InstantiateIn(container);
			HttpContext httpContext = HttpContext.Current;
			if (httpContext != null)
			{
				TemplateControl templateControl = httpContext.TemplateControl;
				if (templateControl != null && templateControl.NoCompile)
				{
					foreach (object obj in container.Controls)
					{
						Control control = (Control)obj;
						control.TemplateControl = templateControl;
					}
				}
			}
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x0009096C File Offset: 0x0008EB6C
		public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string ID, IDictionary attribs)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			ParsedAttributeCollection parsedAttributeCollection = ControlBuilder.ConvertDictionaryToParsedAttributeCollection(attribs);
			foreach (object obj in parsedAttributeCollection.GetFilteredAttributeDictionaries())
			{
				FilteredAttributeDictionary filteredAttributeDictionary = (FilteredAttributeDictionary)obj;
				string filter = filteredAttributeDictionary.Filter;
				foreach (object obj2 in ((IEnumerable)filteredAttributeDictionary))
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
					string text = (string)dictionaryEntry.Key;
					if (StringUtil.EqualsIgnoreCase(text, "ContentPlaceHolderID"))
					{
						if (this._contentPlaceHolder != null)
						{
							throw new HttpException(SR.GetString("Content_only_one_contentPlaceHolderID_allowed"));
						}
						this._contentPlaceHolder = dictionaryEntry.Value.ToString();
						this._contentPlaceHolderFilter = filter;
					}
					else if (ContentBuilderInternal.attributesToPreserve.Contains(text, StringComparer.OrdinalIgnoreCase))
					{
						dictionary[text] = dictionaryEntry.Value.ToString();
					}
				}
			}
			if (!parser.FInDesigner)
			{
				if (this._contentPlaceHolder == null)
				{
					throw new HttpException(SR.GetString("Control_Missing_Attribute", new object[]
					{
						"ContentPlaceHolderID",
						type.Name
					}));
				}
				attribs.Clear();
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					attribs[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			base.Init(parser, parentBuilder, type, tagName, ID, attribs);
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x00090B38 File Offset: 0x0008ED38
		internal override void SetParentBuilder(ControlBuilder parentBuilder)
		{
			if (!base.InDesigner && !(parentBuilder is FileLevelPageControlBuilder))
			{
				throw new HttpException(SR.GetString("Content_allowed_in_top_level_only"));
			}
			base.SetParentBuilder(parentBuilder);
		}

		// Token: 0x04001F30 RID: 7984
		private const string _contentPlaceHolderIDPropName = "ContentPlaceHolderID";

		// Token: 0x04001F31 RID: 7985
		private static string[] attributesToPreserve = new string[]
		{
			"ClientIDMode",
			"ViewStateMode"
		};

		// Token: 0x04001F32 RID: 7986
		private string _contentPlaceHolder;

		// Token: 0x04001F33 RID: 7987
		private string _contentPlaceHolderFilter;
	}
}
