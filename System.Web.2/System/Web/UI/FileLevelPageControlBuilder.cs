using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace System.Web.UI
{
	// Token: 0x020002D2 RID: 722
	public class FileLevelPageControlBuilder : RootBuilder
	{
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x0600207A RID: 8314 RVA: 0x0006822E File Offset: 0x0006642E
		internal ICollection ContentBuilderEntries
		{
			get
			{
				return this._contentBuilderEntries;
			}
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x00068238 File Offset: 0x00066438
		public override void AppendLiteralString(string text)
		{
			if (this._firstLiteralText == null && !Util.IsWhiteSpaceString(text))
			{
				int num = Util.FirstNonWhiteSpaceIndex(text);
				if (num < 0)
				{
					num = 0;
				}
				this._firstLiteralLineNumber = base.Parser._lineNumber - Util.LineCount(text, num, text.Length);
				this._firstLiteralText = text;
				if (this._containsContentPage)
				{
					throw new HttpException(SR.GetString("Only_Content_supported_on_content_page"));
				}
			}
			base.AppendLiteralString(text);
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x000682A8 File Offset: 0x000664A8
		public override void AppendSubBuilder(ControlBuilder subBuilder)
		{
			if (subBuilder is ContentBuilderInternal)
			{
				ContentBuilderInternal contentBuilderInternal = (ContentBuilderInternal)subBuilder;
				this._containsContentPage = true;
				if (this._contentBuilderEntries == null)
				{
					this._contentBuilderEntries = new ArrayList();
				}
				if (this._firstLiteralText != null)
				{
					throw new HttpParseException(SR.GetString("Only_Content_supported_on_content_page"), null, base.Parser.CurrentVirtualPath, this._firstLiteralText, this._firstLiteralLineNumber);
				}
				if (this._firstControlBuilder != null)
				{
					base.Parser._lineNumber = this._firstControlBuilder.Line;
					throw new HttpException(SR.GetString("Only_Content_supported_on_content_page"));
				}
				TemplatePropertyEntry templatePropertyEntry = new TemplatePropertyEntry();
				templatePropertyEntry.Filter = contentBuilderInternal.ContentPlaceHolderFilter;
				templatePropertyEntry.Name = contentBuilderInternal.ContentPlaceHolder;
				templatePropertyEntry.Builder = contentBuilderInternal;
				this._contentBuilderEntries.Add(templatePropertyEntry);
			}
			else if (this._firstControlBuilder == null)
			{
				if (this._containsContentPage)
				{
					throw new HttpException(SR.GetString("Only_Content_supported_on_content_page"));
				}
				this._firstControlBuilder = subBuilder;
			}
			base.AppendSubBuilder(subBuilder);
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x000683A4 File Offset: 0x000665A4
		internal override void InitObject(object obj)
		{
			base.InitObject(obj);
			if (this._contentBuilderEntries == null)
			{
				return;
			}
			ICollection filteredPropertyEntrySet = base.GetFilteredPropertyEntrySet(this._contentBuilderEntries);
			foreach (object obj2 in filteredPropertyEntrySet)
			{
				TemplatePropertyEntry templatePropertyEntry = (TemplatePropertyEntry)obj2;
				ContentBuilderInternal contentBuilderInternal = (ContentBuilderInternal)templatePropertyEntry.Builder;
				try
				{
					contentBuilderInternal.SetServiceProvider(base.ServiceProvider);
					this.AddContentTemplate(obj, contentBuilderInternal.ContentPlaceHolder, contentBuilderInternal.BuildObject() as ITemplate);
				}
				finally
				{
					contentBuilderInternal.SetServiceProvider(null);
				}
			}
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x00068458 File Offset: 0x00066658
		internal virtual void AddContentTemplate(object obj, string templateName, ITemplate template)
		{
			Page page = (Page)obj;
			page.AddContentTemplate(templateName, template);
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x00068474 File Offset: 0x00066674
		internal override void SortEntries()
		{
			base.SortEntries();
			ControlBuilder.FilteredPropertyEntryComparer filteredPropertyEntryComparer = null;
			base.ProcessAndSortPropertyEntries(this._contentBuilderEntries, ref filteredPropertyEntryComparer);
		}

		// Token: 0x04001B32 RID: 6962
		private ArrayList _contentBuilderEntries;

		// Token: 0x04001B33 RID: 6963
		private ControlBuilder _firstControlBuilder;

		// Token: 0x04001B34 RID: 6964
		private int _firstLiteralLineNumber;

		// Token: 0x04001B35 RID: 6965
		private bool _containsContentPage;

		// Token: 0x04001B36 RID: 6966
		private string _firstLiteralText;
	}
}
