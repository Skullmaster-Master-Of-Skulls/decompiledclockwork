using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web.WebPages.Html;

namespace System.Web.WebPages
{
	// Token: 0x02000089 RID: 137
	public class WebPageContext
	{
		// Token: 0x06000435 RID: 1077 RVA: 0x0000D5D7 File Offset: 0x0000B7D7
		public WebPageContext() : this(null, null, null)
		{
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000D5E2 File Offset: 0x0000B7E2
		public WebPageContext(HttpContextBase context, WebPageRenderingBase page, object model)
		{
			this.HttpContext = context;
			this.Page = page;
			this.Model = model;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000D600 File Offset: 0x0000B800
		public static WebPageContext Current
		{
			get
			{
				HttpContext httpContext = System.Web.HttpContext.Current;
				if (httpContext == null)
				{
					return null;
				}
				HttpContextWrapper httpContext2 = new HttpContextWrapper(httpContext);
				ITemplateFile currentTemplate = TemplateStack.GetCurrentTemplate(httpContext2);
				WebPageRenderingBase webPageRenderingBase = currentTemplate as WebPageRenderingBase;
				if (webPageRenderingBase != null)
				{
					return webPageRenderingBase.PageContext;
				}
				return null;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000D638 File Offset: 0x0000B838
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x0000D640 File Offset: 0x0000B840
		internal HttpContextBase HttpContext { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000D649 File Offset: 0x0000B849
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x0000D651 File Offset: 0x0000B851
		public object Model { get; internal set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000D65A File Offset: 0x0000B85A
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x0000D675 File Offset: 0x0000B875
		internal ModelStateDictionary ModelState
		{
			get
			{
				if (this._modelStateDictionary == null)
				{
					this._modelStateDictionary = new ModelStateDictionary();
				}
				return this._modelStateDictionary;
			}
			private set
			{
				this._modelStateDictionary = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000D67E File Offset: 0x0000B87E
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x0000D6A5 File Offset: 0x0000B8A5
		internal ValidationHelper Validation
		{
			get
			{
				if (this._validation == null)
				{
					this._validation = new ValidationHelper(this.HttpContext, this.ModelState);
				}
				return this._validation;
			}
			private set
			{
				this._validation = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000D6AE File Offset: 0x0000B8AE
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x0000D6B6 File Offset: 0x0000B8B6
		internal Action<TextWriter> BodyAction { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000D6BF File Offset: 0x0000B8BF
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x0000D6DA File Offset: 0x0000B8DA
		internal Stack<TextWriter> OutputStack
		{
			get
			{
				if (this._outputStack == null)
				{
					this._outputStack = new Stack<TextWriter>();
				}
				return this._outputStack;
			}
			set
			{
				this._outputStack = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000D6E3 File Offset: 0x0000B8E3
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x0000D6EB File Offset: 0x0000B8EB
		public WebPageRenderingBase Page { get; internal set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000D6F4 File Offset: 0x0000B8F4
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x0000D70F File Offset: 0x0000B90F
		[Dynamic(new bool[]
		{
			false,
			false,
			true
		})]
		public IDictionary<object, dynamic> PageData
		{
			[return: Dynamic(new bool[]
			{
				false,
				false,
				true
			})]
			get
			{
				if (this._pageData == null)
				{
					this._pageData = new PageDataDictionary<object>();
				}
				return this._pageData;
			}
			[param: Dynamic(new bool[]
			{
				false,
				false,
				true
			})]
			internal set
			{
				this._pageData = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000D718 File Offset: 0x0000B918
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x0000D733 File Offset: 0x0000B933
		internal Stack<Dictionary<string, SectionWriter>> SectionWritersStack
		{
			get
			{
				if (this._sectionWritersStack == null)
				{
					this._sectionWritersStack = new Stack<Dictionary<string, SectionWriter>>();
				}
				return this._sectionWritersStack;
			}
			set
			{
				this._sectionWritersStack = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0000D73C File Offset: 0x0000B93C
		internal HashSet<string> SourceFiles
		{
			get
			{
				HashSet<string> hashSet = this.HttpContext.Items[WebPageContext._sourceFileKey] as HashSet<string>;
				if (hashSet == null)
				{
					hashSet = new HashSet<string>();
					this.HttpContext.Items[WebPageContext._sourceFileKey] = hashSet;
				}
				return hashSet;
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000D784 File Offset: 0x0000B984
		internal static WebPageContext CreateNestedPageContext<TModel>(WebPageContext parentContext, IDictionary<object, dynamic> pageData, TModel model, bool isLayoutPage)
		{
			WebPageContext webPageContext = new WebPageContext
			{
				HttpContext = parentContext.HttpContext,
				OutputStack = parentContext.OutputStack,
				Validation = parentContext.Validation,
				PageData = pageData,
				Model = model,
				ModelState = parentContext.ModelState
			};
			if (isLayoutPage)
			{
				webPageContext.BodyAction = parentContext.BodyAction;
				webPageContext.SectionWritersStack = parentContext.SectionWritersStack;
			}
			return webPageContext;
		}

		// Token: 0x0400012F RID: 303
		private static readonly object _sourceFileKey = new object();

		// Token: 0x04000130 RID: 304
		private Stack<TextWriter> _outputStack;

		// Token: 0x04000131 RID: 305
		private Stack<Dictionary<string, SectionWriter>> _sectionWritersStack;

		// Token: 0x04000132 RID: 306
		[Dynamic(new bool[]
		{
			false,
			false,
			true
		})]
		private IDictionary<object, dynamic> _pageData;

		// Token: 0x04000133 RID: 307
		private ValidationHelper _validation;

		// Token: 0x04000134 RID: 308
		private ModelStateDictionary _modelStateDictionary;
	}
}
