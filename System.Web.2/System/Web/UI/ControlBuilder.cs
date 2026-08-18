using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Compilation;
using System.Web.Instrumentation;
using System.Web.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000260 RID: 608
	public class ControlBuilder
	{
		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001C92 RID: 7314 RVA: 0x0005AD20 File Offset: 0x00058F20
		public virtual Type BindingContainerType
		{
			get
			{
				if (this.NamingContainerBuilder == null)
				{
					return typeof(Control);
				}
				Type controlType = this.NamingContainerBuilder.ControlType;
				if (typeof(INonBindingContainer).IsAssignableFrom(controlType))
				{
					return this.NamingContainerBuilder.BindingContainerType;
				}
				return this.NamingContainerBuilder.ControlType;
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06001C93 RID: 7315 RVA: 0x0005AD78 File Offset: 0x00058F78
		public virtual ControlBuilder BindingContainerBuilder
		{
			get
			{
				if (this.NamingContainerBuilder != null)
				{
					Type controlType = this.NamingContainerBuilder.ControlType;
					if (typeof(INonBindingContainer).IsAssignableFrom(controlType))
					{
						return this.NamingContainerBuilder.BindingContainerBuilder;
					}
				}
				return this.NamingContainerBuilder;
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06001C94 RID: 7316 RVA: 0x0005ADC0 File Offset: 0x00058FC0
		public virtual string ItemType
		{
			get
			{
				ControlBuilder bindingContainerBuilder = this.BindingContainerBuilder;
				if (bindingContainerBuilder != null && bindingContainerBuilder.BindingContainerBuilder != null)
				{
					return (from object propertyEntry in bindingContainerBuilder.BindingContainerBuilder.SimplePropertyEntriesInternal
					let simplePropertyEntry = propertyEntry as SimplePropertyEntry
					where simplePropertyEntry != null && simplePropertyEntry.Name.Equals(ControlBuilder.ItemTypeProperty, StringComparison.OrdinalIgnoreCase)
					select (string)simplePropertyEntry.Value).FirstOrDefault<string>();
				}
				return null;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06001C95 RID: 7317 RVA: 0x0005AE68 File Offset: 0x00059068
		internal ICollection EventEntries
		{
			get
			{
				if (this._eventEntries == null)
				{
					return EmptyCollection.Instance;
				}
				return this._eventEntries;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06001C96 RID: 7318 RVA: 0x0005AE7E File Offset: 0x0005907E
		private ArrayList EventEntriesInternal
		{
			get
			{
				if (this._eventEntries == null)
				{
					this._eventEntries = new ArrayList();
				}
				return this._eventEntries;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001C97 RID: 7319 RVA: 0x0005AE99 File Offset: 0x00059099
		internal ICollection SimplePropertyEntries
		{
			get
			{
				if (this._simplePropertyEntries == null)
				{
					return EmptyCollection.Instance;
				}
				return this._simplePropertyEntries;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001C98 RID: 7320 RVA: 0x0005AEAF File Offset: 0x000590AF
		internal ArrayList SimplePropertyEntriesInternal
		{
			get
			{
				if (this._simplePropertyEntries == null)
				{
					this._simplePropertyEntries = new ArrayList();
				}
				return this._simplePropertyEntries;
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06001C99 RID: 7321 RVA: 0x0005AECA File Offset: 0x000590CA
		public ICollection ComplexPropertyEntries
		{
			get
			{
				if (this._complexPropertyEntries == null)
				{
					return EmptyCollection.Instance;
				}
				return this._complexPropertyEntries;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06001C9A RID: 7322 RVA: 0x0005AEE0 File Offset: 0x000590E0
		private ArrayList ComplexPropertyEntriesInternal
		{
			get
			{
				if (this._complexPropertyEntries == null)
				{
					this._complexPropertyEntries = new ArrayList();
				}
				return this._complexPropertyEntries;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06001C9B RID: 7323 RVA: 0x0005AEFB File Offset: 0x000590FB
		public ICollection TemplatePropertyEntries
		{
			get
			{
				if (this._templatePropertyEntries == null)
				{
					return EmptyCollection.Instance;
				}
				return this._templatePropertyEntries;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06001C9C RID: 7324 RVA: 0x0005AF11 File Offset: 0x00059111
		private ArrayList TemplatePropertyEntriesInternal
		{
			get
			{
				if (this._templatePropertyEntries == null)
				{
					this._templatePropertyEntries = new ArrayList();
				}
				return this._templatePropertyEntries;
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06001C9D RID: 7325 RVA: 0x0005AF2C File Offset: 0x0005912C
		internal ICollection BoundPropertyEntries
		{
			get
			{
				if (this._boundPropertyEntries == null)
				{
					return EmptyCollection.Instance;
				}
				return this._boundPropertyEntries;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x0005AF42 File Offset: 0x00059142
		private ArrayList BoundPropertyEntriesInternal
		{
			get
			{
				if (this._boundPropertyEntries == null)
				{
					this._boundPropertyEntries = new ArrayList();
				}
				return this._boundPropertyEntries;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06001C9F RID: 7327 RVA: 0x0005AF5D File Offset: 0x0005915D
		internal bool HasFilteredBoundEntries
		{
			get
			{
				return this.flags[512];
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x0005AF6F File Offset: 0x0005916F
		internal bool IsNoCompile
		{
			get
			{
				return this.flags[1];
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x0005AF7D File Offset: 0x0005917D
		// (set) Token: 0x06001CA2 RID: 7330 RVA: 0x0005AF85 File Offset: 0x00059185
		internal string SkinID
		{
			get
			{
				return this._skinID;
			}
			set
			{
				this._skinID = value;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x0005AF8E File Offset: 0x0005918E
		internal IDictionary AdditionalState
		{
			get
			{
				if (this._additionalState == null)
				{
					this._additionalState = new Dictionary<object, object>();
				}
				return this._additionalState;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x0005AFA9 File Offset: 0x000591A9
		public Type ControlType
		{
			get
			{
				return this._controlType;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06001CA5 RID: 7333 RVA: 0x0005AFB1 File Offset: 0x000591B1
		public IFilterResolutionService CurrentFilterResolutionService
		{
			get
			{
				if (this.ServiceProvider != null)
				{
					return (IFilterResolutionService)this.ServiceProvider.GetService(typeof(IFilterResolutionService));
				}
				return this.TemplateControl;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06001CA6 RID: 7334 RVA: 0x0005AFA9 File Offset: 0x000591A9
		public virtual Type DeclareType
		{
			get
			{
				return this._controlType;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x0005AFDC File Offset: 0x000591DC
		private IDesignerHost DesignerHost
		{
			get
			{
				if (this.InDesigner && this.ParseTimeData != null)
				{
					TemplateParser parser = this.ParseTimeData.Parser;
					if (parser != null)
					{
						return parser.DesignerHost;
					}
				}
				return null;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x0005B010 File Offset: 0x00059210
		private ControlBuilder DefaultPropertyBuilder
		{
			get
			{
				return this.ParseTimeData.DefaultPropertyBuilder;
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x0005B01D File Offset: 0x0005921D
		public IThemeResolutionService ThemeResolutionService
		{
			get
			{
				if (this.ServiceProvider != null)
				{
					return (IThemeResolutionService)this.ServiceProvider.GetService(typeof(IThemeResolutionService));
				}
				return this.TemplateControl as IThemeResolutionService;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06001CAA RID: 7338 RVA: 0x0005B04D File Offset: 0x0005924D
		private EventDescriptorCollection EventDescriptors
		{
			get
			{
				if (this.ParseTimeData.EventDescriptors == null)
				{
					this.ParseTimeData.EventDescriptors = TargetFrameworkUtil.GetEvents(this._controlType);
				}
				return this.ParseTimeData.EventDescriptors;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06001CAB RID: 7339 RVA: 0x0005B07D File Offset: 0x0005927D
		// (set) Token: 0x06001CAC RID: 7340 RVA: 0x0005B08A File Offset: 0x0005928A
		internal string Filter
		{
			get
			{
				return this.ParseTimeData.Filter;
			}
			set
			{
				this.ParseTimeData.Filter = value;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06001CAD RID: 7341 RVA: 0x0005B098 File Offset: 0x00059298
		protected bool FChildrenAsProperties
		{
			get
			{
				return this.ParseTimeData.ChildrenAsProperties;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06001CAE RID: 7342 RVA: 0x0005B0A5 File Offset: 0x000592A5
		protected bool FIsNonParserAccessor
		{
			get
			{
				return this.ParseTimeData.IsNonParserAccessor;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06001CAF RID: 7343 RVA: 0x0005B0B2 File Offset: 0x000592B2
		public virtual bool HasAspCode
		{
			get
			{
				return this.ParseTimeData.HasAspCode;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x0005B0BF File Offset: 0x000592BF
		// (set) Token: 0x06001CB1 RID: 7345 RVA: 0x0005B0CC File Offset: 0x000592CC
		public string ID
		{
			get
			{
				return this.ParseTimeData.ID;
			}
			set
			{
				this.ParseTimeData.ID = value;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x0005B0DA File Offset: 0x000592DA
		// (set) Token: 0x06001CB3 RID: 7347 RVA: 0x0005B0E7 File Offset: 0x000592E7
		internal bool IsGeneratedID
		{
			get
			{
				return this.ParseTimeData.IsGeneratedID;
			}
			set
			{
				this.ParseTimeData.IsGeneratedID = value;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06001CB4 RID: 7348 RVA: 0x0005B0F5 File Offset: 0x000592F5
		private bool IgnoreControlProperty
		{
			get
			{
				return this.ParseTimeData.IgnoreControlProperties;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06001CB5 RID: 7349 RVA: 0x0005B102 File Offset: 0x00059302
		protected bool InDesigner
		{
			get
			{
				return !this.IsNoCompile && this.Parser != null && this.Parser.FInDesigner;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x0005B123 File Offset: 0x00059323
		protected bool InPageTheme
		{
			get
			{
				return this.Parser is PageThemeParser;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06001CB7 RID: 7351 RVA: 0x0005B133 File Offset: 0x00059333
		internal bool IsControlSkin
		{
			get
			{
				return this.ParentBuilder is FileLevelPageThemeBuilder;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x0005B143 File Offset: 0x00059343
		private bool IsHtmlControl
		{
			get
			{
				return this.ParseTimeData.IsHtmlControl;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06001CB9 RID: 7353 RVA: 0x0005B150 File Offset: 0x00059350
		// (set) Token: 0x06001CBA RID: 7354 RVA: 0x0005B15D File Offset: 0x0005935D
		internal int Line
		{
			get
			{
				return this.ParseTimeData.Line;
			}
			set
			{
				this.ParseTimeData.Line = value;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06001CBB RID: 7355 RVA: 0x0005B16B File Offset: 0x0005936B
		public bool Localize
		{
			get
			{
				return this.ParseTimeData == null || this.ParseTimeData.Localize;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x0005B184 File Offset: 0x00059384
		private ControlBuilder NamingContainerBuilder
		{
			get
			{
				if (this.ParseTimeData.NamingContainerSearched)
				{
					return this.ParseTimeData.NamingContainerBuilder;
				}
				if (this.ParentBuilder == null || this.ParentBuilder.ControlType == null)
				{
					this.ParseTimeData.NamingContainerBuilder = null;
				}
				else if (typeof(INamingContainer).IsAssignableFrom(this.ParentBuilder.ControlType))
				{
					this.ParseTimeData.NamingContainerBuilder = this.ParentBuilder;
				}
				else
				{
					this.ParseTimeData.NamingContainerBuilder = this.ParentBuilder.NamingContainerBuilder;
				}
				this.ParseTimeData.NamingContainerSearched = true;
				return this.ParseTimeData.NamingContainerBuilder;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06001CBD RID: 7357 RVA: 0x0005B22F File Offset: 0x0005942F
		public Type NamingContainerType
		{
			get
			{
				if (this.NamingContainerBuilder == null)
				{
					return typeof(Control);
				}
				return this.NamingContainerBuilder.ControlType;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x0005B24F File Offset: 0x0005944F
		internal CompilationMode CompilationMode
		{
			get
			{
				return this.Parser.CompilationMode;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06001CBF RID: 7359 RVA: 0x0005B25C File Offset: 0x0005945C
		internal ControlBuilder ParentBuilder
		{
			get
			{
				return this.ParseTimeData.ParentBuilder;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x0005B269 File Offset: 0x00059469
		protected internal TemplateParser Parser
		{
			get
			{
				return this.ParseTimeData.Parser;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x0005B276 File Offset: 0x00059476
		private ControlBuilder.ControlBuilderParseTimeData ParseTimeData
		{
			get
			{
				if (this._parseTimeData == null)
				{
					if (this.IsNoCompile)
					{
						throw new InvalidOperationException(SR.GetString("ControlBuilder_ParseTimeDataNotAvailable"));
					}
					this._parseTimeData = new ControlBuilder.ControlBuilderParseTimeData();
				}
				return this._parseTimeData;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x0005B2A9 File Offset: 0x000594A9
		private PropertyDescriptorCollection PropertyDescriptors
		{
			get
			{
				if (this.ParseTimeData.PropertyDescriptors == null)
				{
					this.ParseTimeData.PropertyDescriptors = TargetFrameworkUtil.GetProperties(this._controlType);
				}
				return this.ParseTimeData.PropertyDescriptors;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06001CC3 RID: 7363 RVA: 0x0005B2D9 File Offset: 0x000594D9
		private StringSet PropertyEntries
		{
			get
			{
				if (this.ParseTimeData.PropertyEntries == null)
				{
					this.ParseTimeData.PropertyEntries = new CaseInsensitiveStringSet();
				}
				return this.ParseTimeData.PropertyEntries;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x0005B303 File Offset: 0x00059503
		public ArrayList SubBuilders
		{
			get
			{
				if (this._subBuilders == null)
				{
					this._subBuilders = new ArrayList();
				}
				return this._subBuilders;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x0005B31E File Offset: 0x0005951E
		public IServiceProvider ServiceProvider
		{
			get
			{
				return this._serviceProvider;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x0005B326 File Offset: 0x00059526
		private bool SupportsAttributes
		{
			get
			{
				return this.ParseTimeData.SupportsAttributes;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x0005B333 File Offset: 0x00059533
		public string TagName
		{
			get
			{
				return this._tagName;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x0005B33B File Offset: 0x0005953B
		// (set) Token: 0x06001CC9 RID: 7369 RVA: 0x0005B348 File Offset: 0x00059548
		internal VirtualPath VirtualPath
		{
			get
			{
				return this.ParseTimeData.VirtualPath;
			}
			set
			{
				this.ParseTimeData.VirtualPath = value;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06001CCA RID: 7370 RVA: 0x0005B356 File Offset: 0x00059556
		public string PageVirtualPath
		{
			get
			{
				return VirtualPath.GetVirtualPathString(this.VirtualPath);
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001CCB RID: 7371 RVA: 0x0005B364 File Offset: 0x00059564
		internal TemplateControl TemplateControl
		{
			get
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext == null)
				{
					return null;
				}
				return httpContext.TemplateControl;
			}
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x0005B384 File Offset: 0x00059584
		private void AddBoundProperty(string filter, string name, string expressionPrefix, string expression, ExpressionBuilder expressionBuilder, object parsedExpressionData, string fieldName, string formatString, bool twoWayBound, bool encode, int line = 0, int column = 0)
		{
			this.AddBoundProperty(filter, name, expressionPrefix, expression, expressionBuilder, parsedExpressionData, false, fieldName, formatString, twoWayBound, encode, line, column);
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x0005B3B0 File Offset: 0x000595B0
		private void AddBoundProperty(string filter, string name, string expressionPrefix, string expression, ExpressionBuilder expressionBuilder, object parsedExpressionData, bool generated, string fieldName, string formatString, bool twoWayBound, bool encode, int line = 0, int column = 0)
		{
			string id = this.ParseTimeData.ID;
			IDesignerHost designerHost = this.DesignerHost;
			if (string.IsNullOrEmpty(expressionPrefix))
			{
				if (string.IsNullOrEmpty(id))
				{
					if (this.CompilationMode == CompilationMode.Never)
					{
						throw new HttpException(SR.GetString("NoCompileBinding_requires_ID", new object[]
						{
							this._controlType.Name,
							fieldName
						}));
					}
					if (twoWayBound)
					{
						throw new HttpException(SR.GetString("TwoWayBinding_requires_ID", new object[]
						{
							this._controlType.Name,
							fieldName
						}));
					}
				}
				if (!this.flags[8192] && TargetFrameworkUtil.GetEvent(this.ControlType, "DataBinding") == null)
				{
					throw new InvalidOperationException(SR.GetString("ControlBuilder_DatabindingRequiresEvent", new object[]
					{
						this._controlType.FullName
					}));
				}
			}
			else if (expressionBuilder == null)
			{
				expressionBuilder = ExpressionBuilder.GetExpressionBuilder(expressionPrefix, this.VirtualPath, designerHost);
			}
			BoundPropertyEntry boundPropertyEntry = new BoundPropertyEntry();
			boundPropertyEntry.Filter = filter;
			boundPropertyEntry.Expression = expression;
			boundPropertyEntry.ExpressionBuilder = expressionBuilder;
			boundPropertyEntry.ExpressionPrefix = expressionPrefix;
			boundPropertyEntry.Generated = generated;
			boundPropertyEntry.FieldName = fieldName;
			boundPropertyEntry.FormatString = formatString;
			boundPropertyEntry.ControlType = this._controlType;
			boundPropertyEntry.ControlID = id;
			boundPropertyEntry.TwoWayBound = twoWayBound;
			boundPropertyEntry.ParsedExpressionData = parsedExpressionData;
			boundPropertyEntry.IsEncoded = encode;
			boundPropertyEntry.Line = line;
			boundPropertyEntry.Column = column;
			this.FillUpBoundPropertyEntry(boundPropertyEntry, name);
			foreach (object obj in this.BoundPropertyEntriesInternal)
			{
				BoundPropertyEntry boundPropertyEntry2 = (BoundPropertyEntry)obj;
				if (string.Equals(boundPropertyEntry2.Name, boundPropertyEntry.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(boundPropertyEntry2.Filter, boundPropertyEntry.Filter, StringComparison.OrdinalIgnoreCase))
				{
					string text = boundPropertyEntry.Name;
					if (!string.IsNullOrEmpty(boundPropertyEntry.Filter))
					{
						text = boundPropertyEntry.Filter + ":" + text;
					}
					throw new InvalidOperationException(SR.GetString("ControlBuilder_CannotHaveMultipleBoundEntries", new object[]
					{
						text,
						this.ControlType
					}));
				}
			}
			this.AddBoundProperty(boundPropertyEntry);
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x0005B5F0 File Offset: 0x000597F0
		private void AddBoundProperty(BoundPropertyEntry entry)
		{
			this.AddEntry(this.BoundPropertyEntriesInternal, entry);
			if (entry.TwoWayBound)
			{
				this.flags[1024] = true;
			}
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x0005B618 File Offset: 0x00059818
		private void AttachTypeDescriptionProvider(object obj)
		{
			if (this.InDesigner && obj != null && this._serviceProvider != null)
			{
				TypeDescriptionProviderService typeDescriptionProviderService = this._serviceProvider.GetService(typeof(TypeDescriptionProviderService)) as TypeDescriptionProviderService;
				if (typeDescriptionProviderService != null)
				{
					TypeDescriptor.AddProvider(typeDescriptionProviderService.GetProvider(obj), obj);
				}
			}
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x0005B664 File Offset: 0x00059864
		private void FillUpBoundPropertyEntry(BoundPropertyEntry entry, string name)
		{
			string name2;
			MemberInfo memberInfo = PropertyMapper.GetMemberInfo(this._controlType, name, out name2);
			entry.Name = name2;
			if (memberInfo != null)
			{
				if (memberInfo is PropertyInfo)
				{
					PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
					if (propertyInfo.GetSetMethod() == null)
					{
						if (!this.SupportsAttributes)
						{
							throw new HttpException(SR.GetString("Property_readonly", new object[]
							{
								name
							}));
						}
						if (entry.TwoWayBound)
						{
							entry.ReadOnlyProperty = true;
						}
						else
						{
							entry.UseSetAttribute = true;
						}
					}
					else
					{
						entry.PropertyInfo = propertyInfo;
						entry.Type = propertyInfo.PropertyType;
					}
				}
				else
				{
					entry.Type = ((FieldInfo)memberInfo).FieldType;
				}
			}
			else
			{
				if (!this.SupportsAttributes)
				{
					throw new HttpException(SR.GetString("Type_doesnt_have_property", new object[]
					{
						this._controlType.FullName,
						name
					}));
				}
				if (entry.TwoWayBound)
				{
					throw new InvalidOperationException(SR.GetString("ControlBuilder_TwoWayBindingNonProperty", new object[]
					{
						name,
						this.ControlType.Name
					}));
				}
				entry.Name = name;
				entry.UseSetAttribute = true;
			}
			if (entry.ParsedExpressionData == null)
			{
				entry.ParseExpression(new ExpressionBuilderContext(this.VirtualPath));
			}
			if (!this.Parser.IgnoreParseErrors && entry.ParsedExpressionData == null && Util.IsWhiteSpaceString(entry.Expression))
			{
				throw new HttpException(SR.GetString("Empty_expression"));
			}
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x0005B7D8 File Offset: 0x000599D8
		private void AddCollectionItem(ControlBuilder builder)
		{
			ComplexPropertyEntry complexPropertyEntry = new ComplexPropertyEntry(true);
			complexPropertyEntry.Builder = builder;
			complexPropertyEntry.Filter = string.Empty;
			this.AddEntry(this.ComplexPropertyEntriesInternal, complexPropertyEntry);
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x0005B80C File Offset: 0x00059A0C
		private void AddComplexProperty(string filter, string name, ControlBuilder builder)
		{
			string empty = string.Empty;
			MemberInfo memberInfo = PropertyMapper.GetMemberInfo(this._controlType, name, out empty);
			ComplexPropertyEntry complexPropertyEntry = new ComplexPropertyEntry();
			complexPropertyEntry.Builder = builder;
			complexPropertyEntry.Filter = filter;
			complexPropertyEntry.Name = empty;
			if (memberInfo != null)
			{
				Type type;
				if (memberInfo is PropertyInfo)
				{
					PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
					complexPropertyEntry.PropertyInfo = propertyInfo;
					if (propertyInfo.GetSetMethod() == null)
					{
						complexPropertyEntry.ReadOnly = true;
					}
					this.ValidatePersistable(propertyInfo, false, false, false, filter);
					type = propertyInfo.PropertyType;
				}
				else
				{
					type = ((FieldInfo)memberInfo).FieldType;
				}
				complexPropertyEntry.Type = type;
				this.AddEntry(this.ComplexPropertyEntriesInternal, complexPropertyEntry);
				return;
			}
			throw new HttpException(SR.GetString("Type_doesnt_have_property", new object[]
			{
				this._controlType.FullName,
				name
			}));
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x0005B8E4 File Offset: 0x00059AE4
		private void AddEntry(ArrayList entries, PropertyEntry entry)
		{
			if (string.Equals(entry.Name, "ID", StringComparison.OrdinalIgnoreCase) && this.flags[8192] && !(entry is SimplePropertyEntry))
			{
				throw new HttpException(SR.GetString("ControlBuilder_IDMustUseAttribute"));
			}
			entry.Index = entries.Count;
			entries.Add(entry);
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x0005B944 File Offset: 0x00059B44
		private void AddProperty(string filter, string name, string value, bool mainDirectiveMode)
		{
			if (this.IgnoreControlProperty && !name.Equals(ControlBuilder.ItemTypeProperty, StringComparison.OrdinalIgnoreCase))
			{
				return;
			}
			string empty = string.Empty;
			MemberInfo memberInfo = null;
			if (this._controlType != null)
			{
				if (string.Equals(name, "SkinID", StringComparison.OrdinalIgnoreCase) && this.flags[8192])
				{
					if (!string.IsNullOrEmpty(filter))
					{
						throw new InvalidOperationException(SR.GetString("Illegal_Device", new object[]
						{
							"SkinID"
						}));
					}
					this.SkinID = value;
					return;
				}
				else
				{
					memberInfo = PropertyMapper.GetMemberInfo(this._controlType, name, out empty);
				}
			}
			if (memberInfo != null)
			{
				SimplePropertyEntry simplePropertyEntry = new SimplePropertyEntry();
				simplePropertyEntry.Filter = filter;
				simplePropertyEntry.Name = empty;
				simplePropertyEntry.PersistedValue = value;
				Type type;
				if (memberInfo is PropertyInfo)
				{
					PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
					simplePropertyEntry.PropertyInfo = propertyInfo;
					if (propertyInfo.GetSetMethod() == null)
					{
						if (!this.SupportsAttributes)
						{
							throw new HttpException(SR.GetString("Property_readonly", new object[]
							{
								name
							}));
						}
						simplePropertyEntry.UseSetAttribute = true;
						simplePropertyEntry.Name = name;
					}
					this.ValidatePersistable(propertyInfo, simplePropertyEntry.UseSetAttribute, mainDirectiveMode, true, filter);
					type = propertyInfo.PropertyType;
				}
				else
				{
					type = ((FieldInfo)memberInfo).FieldType;
				}
				simplePropertyEntry.Type = type;
				if (simplePropertyEntry.UseSetAttribute)
				{
					simplePropertyEntry.Value = value;
				}
				else
				{
					object obj = PropertyConverter.ObjectFromString(type, memberInfo, value);
					DesignTimePageThemeParser designTimePageThemeParser = this.Parser as DesignTimePageThemeParser;
					if (designTimePageThemeParser != null)
					{
						object[] customAttributes = memberInfo.GetCustomAttributes(typeof(UrlPropertyAttribute), true);
						if (customAttributes.Length != 0)
						{
							string text = obj.ToString();
							if (UrlPath.IsRelativeUrl(text) && !UrlPath.IsAppRelativePath(text))
							{
								obj = designTimePageThemeParser.ThemePhysicalPath + text;
							}
						}
					}
					simplePropertyEntry.Value = obj;
					if (type.IsEnum)
					{
						if (obj == null)
						{
							throw new HttpException(SR.GetString("Invalid_enum_value", new object[]
							{
								value,
								name,
								simplePropertyEntry.Type.FullName
							}));
						}
						simplePropertyEntry.PersistedValue = Enum.Format(type, obj, "G");
					}
					else if (type == typeof(bool) && obj == null)
					{
						simplePropertyEntry.Value = true;
					}
				}
				this.AddEntry(this.SimplePropertyEntriesInternal, simplePropertyEntry);
				return;
			}
			bool flag = false;
			if (StringUtil.StringStartsWithIgnoreCase(name, "on"))
			{
				string text2 = name.Substring(2);
				EventDescriptor eventDescriptor = this.EventDescriptors.Find(text2, true);
				if (eventDescriptor != null)
				{
					if (this.InPageTheme)
					{
						throw new HttpException(SR.GetString("Property_theme_disabled", new object[]
						{
							text2,
							this.ControlType.FullName
						}));
					}
					if (value != null)
					{
						value = value.Trim();
					}
					if (string.IsNullOrEmpty(value))
					{
						throw new HttpException(SR.GetString("Event_handler_cant_be_empty", new object[]
						{
							name
						}));
					}
					if (filter.Length > 0)
					{
						throw new HttpException(SR.GetString("Events_cant_be_filtered", new object[]
						{
							filter,
							name
						}));
					}
					flag = true;
					if (!this.Parser.PageParserFilterProcessedEventHookupAttribute(this.ID, eventDescriptor.Name, value))
					{
						this.Parser.OnFoundEventHandler(name);
						EventEntry eventEntry = new EventEntry();
						eventEntry.Name = eventDescriptor.Name;
						eventEntry.HandlerType = eventDescriptor.EventType;
						eventEntry.HandlerMethodName = value;
						this.EventEntriesInternal.Add(eventEntry);
					}
				}
			}
			if (!flag)
			{
				if (!this.SupportsAttributes && filter != ControlBuilder.DesignerFilter)
				{
					if (this._controlType != null)
					{
						throw new HttpException(SR.GetString("Type_doesnt_have_property", new object[]
						{
							this._controlType.FullName,
							name
						}));
					}
					throw new HttpException(SR.GetString("Property_doesnt_have_property", new object[]
					{
						this.TagName,
						name
					}));
				}
				else
				{
					SimplePropertyEntry simplePropertyEntry2 = new SimplePropertyEntry();
					simplePropertyEntry2.Filter = filter;
					simplePropertyEntry2.Name = name;
					simplePropertyEntry2.PersistedValue = value;
					simplePropertyEntry2.UseSetAttribute = true;
					simplePropertyEntry2.Value = value;
					this.AddEntry(this.SimplePropertyEntriesInternal, simplePropertyEntry2);
				}
			}
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0005BD50 File Offset: 0x00059F50
		private void AddTemplateProperty(string filter, string name, TemplateBuilder builder)
		{
			string empty = string.Empty;
			MemberInfo memberInfo = PropertyMapper.GetMemberInfo(this._controlType, name, out empty);
			bool bindableTemplate = builder is BindableTemplateBuilder;
			TemplatePropertyEntry templatePropertyEntry = new TemplatePropertyEntry(bindableTemplate);
			templatePropertyEntry.Builder = builder;
			templatePropertyEntry.Filter = filter;
			templatePropertyEntry.Name = empty;
			Type type = null;
			if (memberInfo != null)
			{
				if (memberInfo is PropertyInfo)
				{
					PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
					templatePropertyEntry.PropertyInfo = propertyInfo;
					this.ValidatePersistable(propertyInfo, false, false, false, filter);
					TemplateContainerAttribute templateContainerAttribute = (TemplateContainerAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(TemplateContainerAttribute), false);
					if (templateContainerAttribute != null)
					{
						if (!typeof(INamingContainer).IsAssignableFrom(templateContainerAttribute.ContainerType))
						{
							throw new HttpException(SR.GetString("Invalid_template_container", new object[]
							{
								name,
								templateContainerAttribute.ContainerType.FullName
							}));
						}
						builder.SetControlType(templateContainerAttribute.ContainerType);
					}
					templatePropertyEntry.Type = propertyInfo.PropertyType;
				}
				else
				{
					type = ((FieldInfo)memberInfo).FieldType;
				}
				templatePropertyEntry.Type = type;
				this.AddEntry(this.TemplatePropertyEntriesInternal, templatePropertyEntry);
				return;
			}
			throw new HttpException(SR.GetString("Type_doesnt_have_property", new object[]
			{
				this._controlType.FullName,
				name
			}));
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x0005BE94 File Offset: 0x0005A094
		internal void AddSubBuilder(object o)
		{
			this.SubBuilders.Add(o);
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06001CD7 RID: 7383 RVA: 0x0005BEA3 File Offset: 0x0005A0A3
		internal bool HasTwoWayBoundProperties
		{
			get
			{
				return this.flags[1024];
			}
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x000097B7 File Offset: 0x000079B7
		public virtual bool AllowWhitespaceLiterals()
		{
			return true;
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x0005BEB8 File Offset: 0x0005A0B8
		public virtual void AppendLiteralString(string s)
		{
			if (s == null)
			{
				return;
			}
			if (this.FIsNonParserAccessor || this.FChildrenAsProperties)
			{
				if (this.DefaultPropertyBuilder != null)
				{
					this.DefaultPropertyBuilder.AppendLiteralString(s);
					return;
				}
				s = s.Trim();
				if (this.FChildrenAsProperties && s.StartsWith("<", StringComparison.OrdinalIgnoreCase))
				{
					throw new HttpException(SR.GetString("Literal_content_not_match_property", new object[]
					{
						this._controlType.FullName,
						s
					}));
				}
				if (s.Length != 0)
				{
					throw new HttpException(SR.GetString("Literal_content_not_allowed", new object[]
					{
						this._controlType.FullName,
						s
					}));
				}
				return;
			}
			else
			{
				if (!this.AllowWhitespaceLiterals() && Util.IsWhiteSpaceString(s))
				{
					return;
				}
				if (this.HtmlDecodeLiterals())
				{
					s = HttpUtility.HtmlDecode(s);
				}
				DataBoundLiteralControlBuilder dataBoundLiteralControlBuilder = null;
				if (!PageInstrumentationService.IsEnabled)
				{
					object lastBuilder = this.GetLastBuilder();
					dataBoundLiteralControlBuilder = (lastBuilder as DataBoundLiteralControlBuilder);
				}
				if (dataBoundLiteralControlBuilder != null)
				{
					dataBoundLiteralControlBuilder.AddLiteralString(s);
					return;
				}
				this.AddSubBuilder(s);
				return;
			}
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x0005BFB4 File Offset: 0x0005A1B4
		public virtual void AppendSubBuilder(ControlBuilder subBuilder)
		{
			subBuilder.OnAppendToParentBuilder(this);
			if (this.FChildrenAsProperties)
			{
				if (subBuilder is CodeBlockBuilder)
				{
					throw new HttpException(SR.GetString("Code_not_supported_on_not_controls"));
				}
				if (this.DefaultPropertyBuilder != null)
				{
					this.DefaultPropertyBuilder.AppendSubBuilder(subBuilder);
					return;
				}
				string tagName = subBuilder.TagName;
				if (subBuilder is TemplateBuilder)
				{
					TemplateBuilder templateBuilder = (TemplateBuilder)subBuilder;
					this.AddTemplateProperty(templateBuilder.Filter, tagName, templateBuilder);
					return;
				}
				if (subBuilder is CollectionBuilder)
				{
					if (subBuilder.SubBuilders != null && subBuilder.SubBuilders.Count > 0)
					{
						foreach (object obj in subBuilder.SubBuilders)
						{
							ControlBuilder builder = (ControlBuilder)obj;
							subBuilder.AddCollectionItem(builder);
						}
						subBuilder.SubBuilders.Clear();
						this.AddComplexProperty(subBuilder.Filter, tagName, subBuilder);
						return;
					}
				}
				else if (subBuilder is StringPropertyBuilder)
				{
					string value = ((StringPropertyBuilder)subBuilder).Text.Trim();
					if (!string.IsNullOrEmpty(value))
					{
						this.AddComplexProperty(subBuilder.Filter, tagName, subBuilder);
						return;
					}
				}
				else
				{
					this.AddComplexProperty(subBuilder.Filter, tagName, subBuilder);
				}
				return;
			}
			else
			{
				CodeBlockBuilder codeBlockBuilder = subBuilder as CodeBlockBuilder;
				if (codeBlockBuilder != null)
				{
					if (this.ControlType != null && !this.flags[8192])
					{
						throw new HttpException(SR.GetString("Code_not_supported_on_not_controls"));
					}
					if (codeBlockBuilder.BlockType == CodeBlockType.DataBinding)
					{
						if (ControlBuilder.bindExpressionRegex.Match(codeBlockBuilder.Content, 0).Success || ControlBuilder.bindItemExpressionRegex.Match(codeBlockBuilder.Content, 0).Success)
						{
							ControlBuilder controlBuilder = this;
							while (controlBuilder != null && !(controlBuilder is TemplateBuilder))
							{
								controlBuilder = controlBuilder.ParentBuilder;
							}
							if (controlBuilder != null && controlBuilder.ParentBuilder != null && controlBuilder is TemplateBuilder)
							{
								throw new HttpException(SR.GetString("DataBoundLiterals_cant_bind"));
							}
						}
						if (this.InDesigner)
						{
							IDictionary dictionary = new ParsedAttributeCollection();
							dictionary.Add("Text", "<%#" + codeBlockBuilder.Content + "%>");
							subBuilder = ControlBuilder.CreateBuilderFromType(this.Parser, this, typeof(DesignerDataBoundLiteralControl), null, null, dictionary, codeBlockBuilder.Line, codeBlockBuilder.PageVirtualPath);
						}
						else
						{
							object lastBuilder = this.GetLastBuilder();
							DataBoundLiteralControlBuilder dataBoundLiteralControlBuilder = lastBuilder as DataBoundLiteralControlBuilder;
							bool flag = false;
							if (dataBoundLiteralControlBuilder == null)
							{
								dataBoundLiteralControlBuilder = new DataBoundLiteralControlBuilder();
								dataBoundLiteralControlBuilder.Init(this.Parser, this, typeof(DataBoundLiteralControl), null, null, null);
								dataBoundLiteralControlBuilder.Line = codeBlockBuilder.Line;
								dataBoundLiteralControlBuilder.VirtualPath = codeBlockBuilder.VirtualPath;
								flag = true;
								if (!PageInstrumentationService.IsEnabled)
								{
									string text = lastBuilder as string;
									if (text != null)
									{
										this.SubBuilders.RemoveAt(this.SubBuilders.Count - 1);
										dataBoundLiteralControlBuilder.AddLiteralString(text);
									}
								}
							}
							dataBoundLiteralControlBuilder.AddDataBindingExpression(codeBlockBuilder);
							if (!flag)
							{
								return;
							}
							subBuilder = dataBoundLiteralControlBuilder;
						}
					}
					else
					{
						this.ParseTimeData.HasAspCode = true;
					}
				}
				if (this.FIsNonParserAccessor)
				{
					throw new HttpException(SR.GetString("Children_not_supported_on_not_controls"));
				}
				this.AddSubBuilder(subBuilder);
				return;
			}
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x0005C2B8 File Offset: 0x0005A4B8
		internal virtual void BuildChildren(object parentObj)
		{
			if (this._subBuilders != null)
			{
				IEnumerator enumerator = this._subBuilders.GetEnumerator();
				int num = 0;
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					object obj2;
					if (obj is string)
					{
						obj2 = new LiteralControl((string)obj);
						goto IL_16A;
					}
					if (!(obj is CodeBlockBuilder))
					{
						ControlBuilder controlBuilder = (ControlBuilder)obj;
						controlBuilder.SetServiceProvider(this.ServiceProvider);
						try
						{
							obj2 = controlBuilder.BuildObject(this.flags[32768]);
							if (!this.InDesigner)
							{
								UserControl userControl = obj2 as UserControl;
								if (userControl != null)
								{
									Control control = parentObj as Control;
									userControl.InitializeAsUserControl(control.Page);
								}
							}
						}
						finally
						{
							controlBuilder.SetServiceProvider(null);
						}
						goto IL_16A;
					}
					if (this.InDesigner)
					{
						CodeBlockBuilder codeBlockBuilder = (CodeBlockBuilder)obj;
						string text;
						switch (codeBlockBuilder.BlockType)
						{
						case CodeBlockType.Code:
							text = "<%" + codeBlockBuilder.Content + "%>";
							break;
						case CodeBlockType.Expression:
							text = "<%=" + codeBlockBuilder.Content + "%>";
							break;
						case CodeBlockType.DataBinding:
							text = "<%#" + (codeBlockBuilder.IsEncoded ? ":" : "") + codeBlockBuilder.Content + "%>";
							break;
						case CodeBlockType.EncodedExpression:
							text = "<%:" + codeBlockBuilder.Content + "%>";
							break;
						default:
							text = null;
							break;
						}
						obj2 = new LiteralControl(text);
						goto IL_16A;
					}
					IL_176:
					num++;
					continue;
					IL_16A:
					((IParserAccessor)parentObj).AddParsedSubObject(obj2);
					goto IL_176;
				}
			}
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x0005C45C File Offset: 0x0005A65C
		public virtual object BuildObject()
		{
			return this.BuildObjectInternal();
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x0005C464 File Offset: 0x0005A664
		internal object BuildObject(bool shouldApplyTheme)
		{
			if (this.flags[32768] != shouldApplyTheme)
			{
				this.flags[32768] = shouldApplyTheme;
			}
			return this.BuildObject();
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x0005C490 File Offset: 0x0005A690
		internal object BuildObjectInternal()
		{
			if (!this.flags[2])
			{
				ConstructorNeedsTagAttribute constructorNeedsTagAttribute = (ConstructorNeedsTagAttribute)TargetFrameworkUtil.GetAttributes(this.ControlType)[typeof(ConstructorNeedsTagAttribute)];
				if (constructorNeedsTagAttribute != null && constructorNeedsTagAttribute.NeedsTag)
				{
					this.flags[4] = true;
				}
				this.flags[2] = true;
			}
			object obj;
			if (this.flags[4])
			{
				object[] args = new object[]
				{
					this.TagName
				};
				obj = HttpRuntime.CreatePublicInstance(this._controlType, args);
			}
			else
			{
				obj = HttpRuntime.FastCreatePublicInstance(this._controlType);
			}
			if (this.flags[32768])
			{
				obj = this.GetThemedObject(obj);
			}
			this.AttachTypeDescriptionProvider(obj);
			RenderTraceListener.CurrentListeners.ShareTraceData(this, obj);
			this.InitObject(obj);
			return obj;
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void CloseControl()
		{
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x0005C560 File Offset: 0x0005A760
		internal static ParsedAttributeCollection ConvertDictionaryToParsedAttributeCollection(IDictionary attribs)
		{
			if (attribs is ParsedAttributeCollection)
			{
				return (ParsedAttributeCollection)attribs;
			}
			ParsedAttributeCollection parsedAttributeCollection = new ParsedAttributeCollection();
			foreach (object obj in attribs)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				parsedAttributeCollection.AddFilteredAttribute(string.Empty, dictionaryEntry.Key.ToString(), dictionaryEntry.Value.ToString());
			}
			return parsedAttributeCollection;
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x0005C5E8 File Offset: 0x0005A7E8
		internal ControlBuilder CreateChildBuilder(string filter, string tagName, IDictionary attribs, TemplateParser parser, ControlBuilder parentBuilder, string id, int line, VirtualPath virtualPath, ref Type childType, bool defaultProperty)
		{
			ControlBuilder controlBuilder;
			if (this.FChildrenAsProperties)
			{
				if (this.DefaultPropertyBuilder != null)
				{
					PropertyInfo property = TargetFrameworkUtil.GetProperty(this._controlType, tagName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, null, false);
					if (property != null)
					{
						controlBuilder = this.GetChildPropertyBuilder(tagName, attribs, ref childType, parser, false);
						if (this.DefaultPropertyBuilder.SubBuilders.Count > 0)
						{
							object[] customAttributes = TargetFrameworkUtil.GetCustomAttributes(this.ControlType, typeof(ParseChildrenAttribute), true);
							ParseChildrenAttribute parseChildrenAttribute = (ParseChildrenAttribute)customAttributes[0];
							throw new HttpException(SR.GetString("Cant_use_default_items_and_filtered_collection", new object[]
							{
								this._controlType.FullName,
								parseChildrenAttribute.DefaultProperty
							}));
						}
						this.ParseTimeData.DefaultPropertyBuilder = null;
					}
					else
					{
						controlBuilder = this.DefaultPropertyBuilder.CreateChildBuilder(filter, tagName, attribs, parser, parentBuilder, id, line, virtualPath, ref childType, false);
					}
				}
				else
				{
					controlBuilder = this.GetChildPropertyBuilder(tagName, attribs, ref childType, parser, defaultProperty);
				}
			}
			else
			{
				string tagName2 = Util.CreateFilteredName(filter, tagName);
				childType = this.GetChildControlType(tagName2, attribs);
				if (childType == null)
				{
					return null;
				}
				controlBuilder = ControlBuilder.CreateBuilderFromType(parser, parentBuilder, childType, tagName2, id, attribs, line, this.PageVirtualPath);
			}
			if (controlBuilder == null)
			{
				return null;
			}
			controlBuilder.Filter = filter;
			controlBuilder.SetParentBuilder((parentBuilder != null) ? parentBuilder : this);
			return controlBuilder;
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x0005C72C File Offset: 0x0005A92C
		public static ControlBuilder CreateBuilderFromType(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string id, IDictionary attribs, int line, string sourceFileName)
		{
			ControlBuilder controlBuilder = ControlBuilder.CreateBuilderFromType(type);
			controlBuilder.Line = line;
			controlBuilder.VirtualPath = VirtualPath.CreateAllowNull(sourceFileName);
			controlBuilder.Init(parser, parentBuilder, type, tagName, id, attribs);
			return controlBuilder;
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x0005C764 File Offset: 0x0005A964
		private static ControlBuilder CreateBuilderFromType(Type type)
		{
			if (ControlBuilder.s_controlBuilderFactoryCache == null)
			{
				ControlBuilder.s_controlBuilderFactoryGenerator = new FactoryGenerator();
				ControlBuilder.s_controlBuilderFactoryCache = Hashtable.Synchronized(new Hashtable());
				ControlBuilder.s_controlBuilderFactoryCache[typeof(Content)] = new ContentBuilderInternalFactory();
				ControlBuilder.s_controlBuilderFactoryCache[typeof(ContentPlaceHolder)] = new ContentPlaceHolderBuilderFactory();
			}
			IWebObjectFactory webObjectFactory = (IWebObjectFactory)ControlBuilder.s_controlBuilderFactoryCache[type];
			if (webObjectFactory == null)
			{
				ControlBuilderAttribute controlBuilderAttribute = ControlBuilder.GetControlBuilderAttribute(type);
				if (controlBuilderAttribute != null)
				{
					Util.CheckAssignableType(typeof(ControlBuilder), controlBuilderAttribute.BuilderType);
					if (controlBuilderAttribute.BuilderType.IsPublic)
					{
						webObjectFactory = ControlBuilder.s_controlBuilderFactoryGenerator.CreateFactory(controlBuilderAttribute.BuilderType);
					}
					else
					{
						webObjectFactory = new ControlBuilder.ReflectionBasedControlBuilderFactory(controlBuilderAttribute.BuilderType);
					}
				}
				else
				{
					webObjectFactory = ControlBuilder.s_defaultControlBuilderFactory;
				}
				ControlBuilder.s_controlBuilderFactoryCache[type] = webObjectFactory;
			}
			return (ControlBuilder)webObjectFactory.CreateInstance();
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x0005C844 File Offset: 0x0005AA44
		private static ControlBuilderAttribute GetControlBuilderAttribute(Type controlType)
		{
			ControlBuilderAttribute result = null;
			object[] customAttributes = TargetFrameworkUtil.GetCustomAttributes(controlType, typeof(ControlBuilderAttribute), true);
			if (customAttributes != null && customAttributes.Length != 0)
			{
				result = (ControlBuilderAttribute)customAttributes[0];
			}
			return result;
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x0005C878 File Offset: 0x0005AA78
		private ControlBuilder GetChildPropertyBuilder(string tagName, IDictionary attribs, ref Type childType, TemplateParser templateParser, bool defaultProperty)
		{
			PropertyInfo property = TargetFrameworkUtil.GetProperty(this._controlType, tagName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, null, false);
			if (property == null)
			{
				throw new HttpException(SR.GetString("Type_doesnt_have_property", new object[]
				{
					this._controlType.FullName,
					tagName
				}));
			}
			childType = property.PropertyType;
			ControlBuilder controlBuilder = null;
			if (typeof(ICollection).IsAssignableFrom(childType))
			{
				IgnoreUnknownContentAttribute ignoreUnknownContentAttribute = (IgnoreUnknownContentAttribute)Attribute.GetCustomAttribute(property, typeof(IgnoreUnknownContentAttribute), true);
				controlBuilder = new CollectionBuilder(ignoreUnknownContentAttribute != null);
			}
			else if (typeof(ITemplate).IsAssignableFrom(childType))
			{
				bool flag = false;
				object[] customAttributes = property.GetCustomAttributes(typeof(TemplateContainerAttribute), false);
				if (customAttributes != null && customAttributes.Length != 0)
				{
					flag = (((TemplateContainerAttribute)customAttributes[0]).BindingDirection == BindingDirection.TwoWay);
				}
				bool allowMultipleInstances = Util.IsMultiInstanceTemplateProperty(property);
				if (flag)
				{
					controlBuilder = new BindableTemplateBuilder();
				}
				else
				{
					controlBuilder = new TemplateBuilder();
				}
				if (controlBuilder is TemplateBuilder)
				{
					((TemplateBuilder)controlBuilder).AllowMultipleInstances = allowMultipleInstances;
					if (this.InDesigner)
					{
						((TemplateBuilder)controlBuilder).SetDesignerHost(templateParser.DesignerHost);
					}
				}
			}
			else if (childType == typeof(string))
			{
				PersistenceModeAttribute persistenceModeAttribute = (PersistenceModeAttribute)Attribute.GetCustomAttribute(property, typeof(PersistenceModeAttribute), true);
				if ((persistenceModeAttribute == null || persistenceModeAttribute.Mode == PersistenceMode.Attribute) && !defaultProperty)
				{
					throw new HttpException(SR.GetString("ControlBuilder_CannotHaveComplexString", new object[]
					{
						this._controlType.FullName,
						tagName
					}));
				}
				controlBuilder = new StringPropertyBuilder();
			}
			if (controlBuilder != null)
			{
				controlBuilder.Line = this.Line;
				controlBuilder.VirtualPath = this.VirtualPath;
				controlBuilder.Init(this.Parser, this, null, tagName, null, attribs);
				return controlBuilder;
			}
			return ControlBuilder.CreateBuilderFromType(this.Parser, this, childType, tagName, null, attribs, this.Line, this.PageVirtualPath);
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual Type GetChildControlType(string tagName, IDictionary attribs)
		{
			return null;
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x0005CA58 File Offset: 0x0005AC58
		internal ICollection GetFilteredPropertyEntrySet(ICollection entries)
		{
			IDictionary dictionary = new HybridDictionary(true);
			IFilterResolutionService currentFilterResolutionService = this.CurrentFilterResolutionService;
			if (currentFilterResolutionService != null)
			{
				using (IEnumerator enumerator = entries.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						PropertyEntry propertyEntry = (PropertyEntry)obj;
						if (!dictionary.Contains(propertyEntry.Name))
						{
							string filter = propertyEntry.Filter;
							if (string.IsNullOrEmpty(filter) || currentFilterResolutionService.EvaluateFilter(filter))
							{
								dictionary[propertyEntry.Name] = propertyEntry;
							}
						}
					}
					goto IL_CF;
				}
			}
			foreach (object obj2 in entries)
			{
				PropertyEntry propertyEntry2 = (PropertyEntry)obj2;
				if (string.IsNullOrEmpty(propertyEntry2.Filter))
				{
					dictionary[propertyEntry2.Name] = propertyEntry2;
				}
			}
			IL_CF:
			return dictionary.Values;
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x0005CB58 File Offset: 0x0005AD58
		private bool HasFilteredEntries(ICollection entries)
		{
			foreach (object obj in entries)
			{
				PropertyEntry propertyEntry = (PropertyEntry)obj;
				if (propertyEntry.Filter.Length > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x0005CBBC File Offset: 0x0005ADBC
		internal object GetLastBuilder()
		{
			if (this.SubBuilders.Count == 0)
			{
				return null;
			}
			return this.SubBuilders[this.SubBuilders.Count - 1];
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x0005CBE5 File Offset: 0x0005ADE5
		public ObjectPersistData GetObjectPersistData()
		{
			return new ObjectPersistData(this, this.Parser.RootBuilder.BuiltObjects);
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x000097B7 File Offset: 0x000079B7
		public virtual bool HasBody()
		{
			return true;
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool HtmlDecodeLiterals()
		{
			return false;
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x0005CC00 File Offset: 0x0005AE00
		public virtual void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string id, IDictionary attribs)
		{
			if (parser != null && parser.ControlBuilderInterceptor != null)
			{
				parser.ControlBuilderInterceptor.PreControlBuilderInit(this, parser, parentBuilder, type, tagName, id, attribs, this.AdditionalState);
			}
			this.ParseTimeData.Parser = parser;
			this.ParseTimeData.ParentBuilder = parentBuilder;
			if (parser != null)
			{
				this.ParseTimeData.IgnoreControlProperties = parser.IgnoreControlProperties;
			}
			this._tagName = tagName;
			if (type != null)
			{
				this._controlType = type;
				this.flags[8192] = typeof(Control).IsAssignableFrom(this._controlType);
				this.ID = id;
				ParseChildrenAttribute parseChildrenAttribute = ControlBuilder.GetParseChildrenAttribute(type);
				if (!typeof(IParserAccessor).IsAssignableFrom(type))
				{
					this.ParseTimeData.IsNonParserAccessor = true;
					this.ParseTimeData.ChildrenAsProperties = true;
				}
				else if (parseChildrenAttribute != null)
				{
					this.ParseTimeData.ChildrenAsProperties = parseChildrenAttribute.ChildrenAsProperties;
				}
				if (this.FChildrenAsProperties && parseChildrenAttribute != null && parseChildrenAttribute.DefaultProperty.Length != 0)
				{
					Type type2 = null;
					this.ParseTimeData.DefaultPropertyBuilder = this.CreateChildBuilder(string.Empty, parseChildrenAttribute.DefaultProperty, null, parser, null, null, this.Line, this.VirtualPath, ref type2, true);
				}
				this.ParseTimeData.IsHtmlControl = typeof(HtmlControl).IsAssignableFrom(this._controlType);
				this.ParseTimeData.SupportsAttributes = typeof(IAttributeAccessor).IsAssignableFrom(this._controlType);
			}
			else
			{
				this.flags[8192] = false;
			}
			if (attribs != null)
			{
				this.PreprocessAttributes(ControlBuilder.ConvertDictionaryToParsedAttributeCollection(attribs));
			}
			if (this.InPageTheme)
			{
				ControlBuilder currentSkinBuilder = ((PageThemeParser)parser).CurrentSkinBuilder;
				if (currentSkinBuilder != null && currentSkinBuilder.ControlType == this.ControlType && string.Equals(currentSkinBuilder.SkinID, this.SkinID, StringComparison.OrdinalIgnoreCase))
				{
					throw new InvalidOperationException(SR.GetString("Cannot_set_recursive_skin", new object[]
					{
						currentSkinBuilder.ControlType.Name
					}));
				}
			}
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x0005CE00 File Offset: 0x0005B000
		private static ParseChildrenAttribute GetParseChildrenAttribute(Type controlType)
		{
			ParseChildrenAttribute parseChildrenAttribute = (ParseChildrenAttribute)ControlBuilder.s_parseChildrenAttributeCache[controlType];
			if (parseChildrenAttribute == null)
			{
				object[] customAttributes = TargetFrameworkUtil.GetCustomAttributes(controlType, typeof(ParseChildrenAttribute), true);
				if (customAttributes != null && customAttributes.Length != 0)
				{
					parseChildrenAttribute = (ParseChildrenAttribute)customAttributes[0];
				}
				if (parseChildrenAttribute == null)
				{
					parseChildrenAttribute = ControlBuilder.s_markerParseChildrenAttribute;
				}
				object syncRoot = ControlBuilder.s_parseChildrenAttributeCache.SyncRoot;
				lock (syncRoot)
				{
					ControlBuilder.s_parseChildrenAttributeCache[controlType] = parseChildrenAttribute;
				}
			}
			if (parseChildrenAttribute == ControlBuilder.s_markerParseChildrenAttribute)
			{
				return null;
			}
			return parseChildrenAttribute;
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x0005CE94 File Offset: 0x0005B094
		private void DoInitObjectOptimizations(object obj)
		{
			this.flags[16] = typeof(ICollection).IsAssignableFrom(this.ControlType);
			this.flags[32] = typeof(IParserAccessor).IsAssignableFrom(obj.GetType());
			if (this._simplePropertyEntries != null)
			{
				this.flags[64] = this.HasFilteredEntries(this._simplePropertyEntries);
			}
			if (this._complexPropertyEntries != null)
			{
				this.flags[128] = this.HasFilteredEntries(this._complexPropertyEntries);
			}
			if (this._templatePropertyEntries != null)
			{
				this.flags[256] = this.HasFilteredEntries(this._templatePropertyEntries);
			}
			if (this._boundPropertyEntries != null)
			{
				this.flags[512] = this.HasFilteredEntries(this._boundPropertyEntries);
			}
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x0005CF74 File Offset: 0x0005B174
		internal virtual object GetThemedObject(object obj)
		{
			Control control = obj as Control;
			if (control == null)
			{
				return obj;
			}
			IThemeResolutionService themeResolutionService = this.ThemeResolutionService;
			if (themeResolutionService != null)
			{
				if (!string.IsNullOrEmpty(this.SkinID))
				{
					control.SkinID = this.SkinID;
				}
				ThemeProvider stylesheetThemeProvider = themeResolutionService.GetStylesheetThemeProvider();
				SkinBuilder skinBuilder = null;
				if (stylesheetThemeProvider != null)
				{
					skinBuilder = stylesheetThemeProvider.GetSkinBuilder(control);
					if (skinBuilder != null)
					{
						try
						{
							skinBuilder.SetServiceProvider(this.ServiceProvider);
							return skinBuilder.ApplyTheme();
						}
						finally
						{
							skinBuilder.SetServiceProvider(null);
						}
						return control;
					}
				}
			}
			return control;
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x0005CFFC File Offset: 0x0005B1FC
		internal virtual void InitObject(object obj)
		{
			this.EnsureEntriesSorted();
			if (!this.flags[8])
			{
				this.DoInitObjectOptimizations(obj);
				this.flags[8] = true;
			}
			Control control = obj as Control;
			if (control != null)
			{
				if (this.InDesigner)
				{
					control.SetDesignMode();
				}
				if (this.SkinID != null)
				{
					control.SkinID = this.SkinID;
				}
				if (!this.InDesigner && this.TemplateControl != null)
				{
					control.ApplyStyleSheetSkin(this.TemplateControl.Page);
				}
			}
			this.InitSimpleProperties(obj);
			if (this.flags[16])
			{
				this.InitCollectionsComplexProperties(obj);
			}
			else
			{
				this.InitComplexProperties(obj);
			}
			if (this.InDesigner)
			{
				if (control != null)
				{
					if (this.Parser.DesignTimeDataBindHandler != null)
					{
						control.DataBinding += this.Parser.DesignTimeDataBindHandler;
					}
					control.SetControlBuilder(this);
				}
				this.Parser.RootBuilder.BuiltObjects[obj] = this;
			}
			this.InitBoundProperties(obj);
			if (this.flags[32])
			{
				this.BuildChildren(obj);
			}
			this.InitTemplateProperties(obj);
			if (control != null)
			{
				this.BindFieldToControl(control);
			}
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x0005D11C File Offset: 0x0005B31C
		private void InitSimpleProperties(object obj)
		{
			if (this._simplePropertyEntries == null)
			{
				return;
			}
			ICollection collection;
			if (this.flags[64])
			{
				collection = this.GetFilteredPropertyEntrySet(this.SimplePropertyEntries);
			}
			else
			{
				collection = this.SimplePropertyEntries;
			}
			foreach (object obj2 in collection)
			{
				SimplePropertyEntry entry = (SimplePropertyEntry)obj2;
				this.SetSimpleProperty(entry, obj);
			}
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x0005D1A0 File Offset: 0x0005B3A0
		internal void SetSimpleProperty(SimplePropertyEntry entry, object obj)
		{
			if (entry.UseSetAttribute)
			{
				((IAttributeAccessor)obj).SetAttribute(entry.Name, entry.Value.ToString());
				return;
			}
			try
			{
				PropertyMapper.SetMappedPropertyValue(obj, entry.Name, entry.Value, this.InDesigner);
			}
			catch (Exception innerException)
			{
				throw new HttpException(SR.GetString("Cannot_set_property", new object[]
				{
					entry.PersistedValue,
					entry.Name
				}), innerException);
			}
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x0005D228 File Offset: 0x0005B428
		private void InitCollectionsComplexProperties(object obj)
		{
			if (this._complexPropertyEntries == null)
			{
				return;
			}
			foreach (object obj2 in this.ComplexPropertyEntries)
			{
				ComplexPropertyEntry complexPropertyEntry = (ComplexPropertyEntry)obj2;
				try
				{
					ControlBuilder builder = complexPropertyEntry.Builder;
					builder.SetServiceProvider(this.ServiceProvider);
					object obj3;
					try
					{
						obj3 = builder.BuildObject(this.flags[32768]);
					}
					finally
					{
						builder.SetServiceProvider(null);
					}
					object[] parameters = new object[]
					{
						obj3
					};
					MethodInfo method = this.ControlType.GetMethod("Add", BindingFlags.Instance | BindingFlags.Public, null, new Type[]
					{
						obj3.GetType()
					}, null);
					if (method == null)
					{
						throw new InvalidOperationException(SR.GetString("ControlBuilder_CollectionHasNoAddMethod", new object[]
						{
							this.TagName
						}));
					}
					Util.InvokeMethod(method, obj, parameters);
				}
				catch (Exception ex)
				{
					throw new HttpException(SR.GetString("Cannot_add_value_not_collection", new object[]
					{
						this.TagName,
						ex.Message
					}), ex);
				}
			}
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x0005D370 File Offset: 0x0005B570
		private void InitComplexProperties(object obj)
		{
			if (this._complexPropertyEntries == null)
			{
				return;
			}
			ICollection collection;
			if (this.flags[128])
			{
				collection = this.GetFilteredPropertyEntrySet(this.ComplexPropertyEntries);
			}
			else
			{
				collection = this.ComplexPropertyEntries;
			}
			foreach (object obj2 in collection)
			{
				ComplexPropertyEntry complexPropertyEntry = (ComplexPropertyEntry)obj2;
				if (complexPropertyEntry.ReadOnly)
				{
					try
					{
						object property = FastPropertyAccessor.GetProperty(obj, complexPropertyEntry.Name, this.InDesigner);
						complexPropertyEntry.Builder.SetServiceProvider(this.ServiceProvider);
						try
						{
							if (complexPropertyEntry.Builder.flags[32768] != this.flags[32768])
							{
								complexPropertyEntry.Builder.flags[32768] = this.flags[32768];
							}
							complexPropertyEntry.Builder.InitObject(property);
						}
						finally
						{
							complexPropertyEntry.Builder.SetServiceProvider(null);
						}
						continue;
					}
					catch (Exception ex)
					{
						throw new HttpException(SR.GetString("Cannot_init", new object[]
						{
							complexPropertyEntry.Name,
							ex.Message
						}), ex);
					}
				}
				try
				{
					ControlBuilder builder = complexPropertyEntry.Builder;
					object val = null;
					builder.SetServiceProvider(this.ServiceProvider);
					try
					{
						val = builder.BuildObject(this.flags[32768]);
					}
					finally
					{
						builder.SetServiceProvider(null);
					}
					FastPropertyAccessor.SetProperty(obj, complexPropertyEntry.Name, val, this.InDesigner);
				}
				catch (Exception innerException)
				{
					throw new HttpException(SR.GetString("Cannot_set_property", new object[]
					{
						this.TagName,
						complexPropertyEntry.Name
					}), innerException);
				}
			}
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x0005D5AC File Offset: 0x0005B7AC
		private void InitBoundProperties(object obj)
		{
			if (this._boundPropertyEntries == null)
			{
				return;
			}
			DataBindingCollection dataBindingCollection = null;
			IAttributeAccessor attributeAccessor = null;
			ICollection collection;
			if (this.flags[512])
			{
				collection = this.GetFilteredPropertyEntrySet(this.BoundPropertyEntries);
			}
			else
			{
				collection = this.BoundPropertyEntries;
			}
			foreach (object obj2 in collection)
			{
				BoundPropertyEntry boundPropertyEntry = (BoundPropertyEntry)obj2;
				if (!boundPropertyEntry.TwoWayBound || !(this is BindableTemplateBuilder) || !this.InDesigner)
				{
					this.InitBoundProperty(obj, boundPropertyEntry, ref dataBindingCollection, ref attributeAccessor);
				}
			}
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x0005D658 File Offset: 0x0005B858
		private void InitBoundProperty(object obj, BoundPropertyEntry entry, ref DataBindingCollection dataBindings, ref IAttributeAccessor attributeAccessor)
		{
			string text = (entry.ExpressionPrefix == null) ? string.Empty : entry.ExpressionPrefix.Trim();
			if (this.InDesigner)
			{
				if (string.IsNullOrEmpty(text))
				{
					if (dataBindings == null && obj is IDataBindingsAccessor)
					{
						dataBindings = ((IDataBindingsAccessor)obj).DataBindings;
					}
					dataBindings.Add(new DataBinding(entry.Name, entry.Type, entry.Expression.Trim()));
					return;
				}
				if (obj is IExpressionsAccessor)
				{
					string expression = (entry.Expression == null) ? string.Empty : entry.Expression.Trim();
					((IExpressionsAccessor)obj).Expressions.Add(new ExpressionBinding(entry.Name, entry.Type, text, expression, entry.Generated, entry.ParsedExpressionData));
					return;
				}
			}
			else
			{
				if (!string.IsNullOrEmpty(text))
				{
					ExpressionBuilder expressionBuilder = entry.ExpressionBuilder;
					if (!expressionBuilder.SupportsEvaluate)
					{
						return;
					}
					string name = entry.Name;
					ExpressionBuilderContext context;
					if (this.TemplateControl != null)
					{
						context = new ExpressionBuilderContext(this.TemplateControl);
					}
					else
					{
						context = new ExpressionBuilderContext(this.VirtualPath);
					}
					object obj2 = expressionBuilder.EvaluateExpression(obj, entry, entry.ParsedExpressionData, context);
					if (entry.UseSetAttribute)
					{
						if (attributeAccessor == null)
						{
							attributeAccessor = (IAttributeAccessor)obj;
						}
						attributeAccessor.SetAttribute(name, obj2.ToString());
						return;
					}
					try
					{
						PropertyMapper.SetMappedPropertyValue(obj, name, obj2, this.InDesigner);
						return;
					}
					catch (Exception innerException)
					{
						throw new HttpException(SR.GetString("Cannot_set_property", new object[]
						{
							entry.ExpressionPrefix + ":" + entry.Expression,
							name
						}), innerException);
					}
				}
				((Control)obj).DataBinding += this.DataBindingMethod;
			}
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x0005D818 File Offset: 0x0005BA18
		private void DataBindingMethod(object sender, EventArgs e)
		{
			bool flag = this is BindableTemplateBuilder;
			bool flag2 = this is TemplateBuilder;
			bool flag3 = true;
			Control control = null;
			ICollection collection;
			if (!this.flags[512])
			{
				collection = this.BoundPropertyEntries;
			}
			else
			{
				ServiceContainer serviceContainer = new ServiceContainer();
				serviceContainer.AddService(typeof(IFilterResolutionService), this.TemplateControl);
				try
				{
					this.SetServiceProvider(serviceContainer);
					collection = this.GetFilteredPropertyEntrySet(this.BoundPropertyEntries);
				}
				finally
				{
					this.SetServiceProvider(null);
				}
			}
			foreach (object obj in collection)
			{
				BoundPropertyEntry boundPropertyEntry = (BoundPropertyEntry)obj;
				if ((!boundPropertyEntry.TwoWayBound || (!flag && !boundPropertyEntry.ReadOnlyProperty)) && (boundPropertyEntry.TwoWayBound || !flag2) && boundPropertyEntry.IsDataBindingEntry)
				{
					if (flag3)
					{
						flag3 = false;
						if (this._bindingContainerDescriptor == null)
						{
							this._bindingContainerDescriptor = TargetFrameworkUtil.GetProperties(typeof(Control))["BindingContainer"];
						}
						object value = this._bindingContainerDescriptor.GetValue(sender);
						control = (value as Control);
						if (control.Page.GetDataItem() == null)
						{
							break;
						}
					}
					object obj2 = control.TemplateControl.Eval(boundPropertyEntry.FieldName, boundPropertyEntry.FormatString);
					string mappedName;
					MemberInfo memberInfo = PropertyMapper.GetMemberInfo(boundPropertyEntry.ControlType, boundPropertyEntry.Name, out mappedName);
					if (!boundPropertyEntry.Type.IsValueType || obj2 != null)
					{
						object value2 = obj2;
						if (boundPropertyEntry.Type == typeof(string))
						{
							value2 = Convert.ToString(obj2, CultureInfo.CurrentCulture);
						}
						else if (obj2 != null && !boundPropertyEntry.Type.IsAssignableFrom(obj2.GetType()))
						{
							value2 = PropertyConverter.ObjectFromString(boundPropertyEntry.Type, memberInfo, Convert.ToString(obj2, CultureInfo.CurrentCulture));
						}
						PropertyMapper.SetMappedPropertyValue(sender, mappedName, value2, this.InDesigner);
					}
				}
			}
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x0005DA48 File Offset: 0x0005BC48
		private void InitTemplateProperties(object obj)
		{
			if (this._templatePropertyEntries == null)
			{
				return;
			}
			object[] array = new object[1];
			ICollection collection;
			if (this.flags[256])
			{
				collection = this.GetFilteredPropertyEntrySet(this.TemplatePropertyEntries);
			}
			else
			{
				collection = this.TemplatePropertyEntries;
			}
			foreach (object obj2 in collection)
			{
				TemplatePropertyEntry templatePropertyEntry = (TemplatePropertyEntry)obj2;
				try
				{
					ControlBuilder builder = templatePropertyEntry.Builder;
					builder.SetServiceProvider(this.ServiceProvider);
					try
					{
						array[0] = builder.BuildObject(this.flags[32768]);
					}
					finally
					{
						builder.SetServiceProvider(null);
					}
					MethodInfo setMethod = templatePropertyEntry.PropertyInfo.GetSetMethod();
					Util.InvokeMethod(setMethod, obj, array);
				}
				catch (Exception innerException)
				{
					throw new HttpException(SR.GetString("Cannot_set_property", new object[]
					{
						this.TagName,
						templatePropertyEntry.Name
					}), innerException);
				}
			}
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x0005DB70 File Offset: 0x0005BD70
		private void BindFieldToControl(Control control)
		{
			if (this.flags[2048] && !this.flags[4096])
			{
				return;
			}
			this.flags[2048] = true;
			TemplateControl templateControl = this.TemplateControl;
			if (templateControl == null)
			{
				return;
			}
			Type type = this.TemplateControl.GetType();
			if (!this.flags[4096])
			{
				if (this.InDesigner)
				{
					return;
				}
				if (control.ID == null)
				{
					return;
				}
				if (type.Assembly == typeof(HttpRuntime).Assembly)
				{
					return;
				}
			}
			FieldInfo field = TargetFrameworkUtil.GetField(templateControl.GetType(), control.ID, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (field == null || field.IsPrivate || !field.FieldType.IsAssignableFrom(control.GetType()))
			{
				return;
			}
			field.SetValue(templateControl, control);
			this.flags[4096] = true;
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool NeedsTagInnerText()
		{
			return false;
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x0005DC60 File Offset: 0x0005BE60
		public virtual void OnAppendToParentBuilder(ControlBuilder parentBuilder)
		{
			if (this.DefaultPropertyBuilder != null)
			{
				ControlBuilder defaultPropertyBuilder = this.DefaultPropertyBuilder;
				this.ParseTimeData.DefaultPropertyBuilder = null;
				this.AppendSubBuilder(defaultPropertyBuilder);
			}
			if (!(this is BindableTemplateBuilder))
			{
				ControlBuilder controlBuilder = this;
				while (controlBuilder != null && !(controlBuilder is BindableTemplateBuilder))
				{
					controlBuilder = controlBuilder.ParentBuilder;
				}
				if (controlBuilder != null && controlBuilder is BindableTemplateBuilder)
				{
					foreach (object obj in this.BoundPropertyEntries)
					{
						BoundPropertyEntry boundPropertyEntry = (BoundPropertyEntry)obj;
						if (boundPropertyEntry.TwoWayBound)
						{
							((BindableTemplateBuilder)controlBuilder).AddBoundProperty(boundPropertyEntry);
						}
					}
				}
			}
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x0005DD14 File Offset: 0x0005BF14
		internal virtual void PrepareNoCompilePageSupport()
		{
			this.flags[1] = true;
			this._parseTimeData = null;
			if (this._eventEntries != null && this._eventEntries.Count == 0)
			{
				this._eventEntries = null;
			}
			if (this._simplePropertyEntries != null && this._simplePropertyEntries.Count == 0)
			{
				this._simplePropertyEntries = null;
			}
			if (this._complexPropertyEntries != null)
			{
				if (this._complexPropertyEntries.Count == 0)
				{
					this._complexPropertyEntries = null;
				}
				else
				{
					foreach (object obj in this._complexPropertyEntries)
					{
						BuilderPropertyEntry builderPropertyEntry = (BuilderPropertyEntry)obj;
						if (builderPropertyEntry.Builder != null)
						{
							builderPropertyEntry.Builder.PrepareNoCompilePageSupport();
						}
					}
				}
			}
			if (this._templatePropertyEntries != null)
			{
				if (this._templatePropertyEntries.Count == 0)
				{
					this._templatePropertyEntries = null;
				}
				else
				{
					foreach (object obj2 in this._templatePropertyEntries)
					{
						BuilderPropertyEntry builderPropertyEntry2 = (BuilderPropertyEntry)obj2;
						if (builderPropertyEntry2.Builder != null)
						{
							builderPropertyEntry2.Builder.PrepareNoCompilePageSupport();
						}
					}
				}
			}
			if (this._boundPropertyEntries != null && this._boundPropertyEntries.Count == 0)
			{
				this._boundPropertyEntries = null;
			}
			if (this._subBuilders != null)
			{
				if (this._subBuilders.Count > 0)
				{
					using (IEnumerator enumerator3 = this._subBuilders.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							object obj3 = enumerator3.Current;
							ControlBuilder controlBuilder = obj3 as ControlBuilder;
							if (controlBuilder != null)
							{
								controlBuilder.PrepareNoCompilePageSupport();
							}
						}
						goto IL_19D;
					}
				}
				this._subBuilders = null;
			}
			IL_19D:
			this.EnsureEntriesSorted();
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x0005DEEC File Offset: 0x0005C0EC
		internal void PreprocessAttribute(string filter, string attribname, string attribvalue, bool mainDirectiveMode, int line = 0, int column = 0)
		{
			if (attribvalue == null)
			{
				attribvalue = string.Empty;
			}
			Match match;
			if ((match = ControlBuilder.databindRegex.Match(attribvalue, 0)).Success)
			{
				if (BuildManager.PrecompilingForUpdatableDeployment)
				{
					return;
				}
				Group group = match.Groups["code"];
				column += group.Index;
				string value = group.Value;
				bool success = match.Groups["encode"].Success;
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				if (!this.InDesigner)
				{
					if ((match = ControlBuilder.bindExpressionRegex.Match(value, 0)).Success)
					{
						flag = true;
						flag2 = true;
					}
					else if ((match = ControlBuilder.bindItemExpressionRegex.Match(value, 0)).Success)
					{
						flag = true;
						flag2 = true;
						flag3 = true;
					}
					else if ((this.CompilationMode == CompilationMode.Never || this.InPageTheme) && (match = ControlBuilder.evalExpressionRegex.Match(value, 0)).Success)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					if (!this.Parser.PageParserFilterProcessedDataBindingAttribute(this.ID, attribname, value))
					{
						this.Parser.EnsureCodeAllowed();
						this.AddBoundProperty(filter, attribname, string.Empty, value, null, null, string.Empty, string.Empty, false, success, line, column);
					}
					return;
				}
				string value2 = match.Groups["params"].Value;
				if (!flag3)
				{
					if (!(match = ControlBuilder.bindParametersRegex.Match(value2, 0)).Success)
					{
						throw new HttpException(SR.GetString("BadlyFormattedBind"));
					}
				}
				else if (!(match = ControlBuilder.bindItemParametersRegex.Match(value2, 0)).Success)
				{
					throw new HttpException(SR.GetString("BadlyFormattedBindItem"));
				}
				string value3 = match.Groups["fieldName"].Value;
				string text = string.Empty;
				Group group2 = match.Groups["formatString"];
				if (group2 != null)
				{
					text = group2.Value;
				}
				if (text.Length > 0 && !ControlBuilder.formatStringRegex.Match(text, 0).Success)
				{
					throw new HttpException(SR.GetString("BadlyFormattedBind"));
				}
				if (this.InPageTheme && !flag2)
				{
					this.AddBoundProperty(filter, attribname, string.Empty, value, null, null, string.Empty, string.Empty, false, success, line, column);
					return;
				}
				this.AddBoundProperty(filter, attribname, string.Empty, value, null, null, value3, text, flag2, success, line, column);
				return;
			}
			else
			{
				if (!(match = ControlBuilder.expressionBuilderRegex.Match(attribvalue, 0)).Success)
				{
					this.AddProperty(filter, attribname, attribvalue, mainDirectiveMode);
					return;
				}
				if (this.InPageTheme)
				{
					throw new HttpParseException(SR.GetString("ControlBuilder_ExpressionsNotAllowedInThemes"));
				}
				if (BuildManager.PrecompilingForUpdatableDeployment)
				{
					return;
				}
				string text2 = match.Groups["code"].Value.Trim();
				int num = text2.IndexOf(':');
				if (num == -1)
				{
					throw new HttpParseException(SR.GetString("InvalidExpressionSyntax", new object[]
					{
						attribvalue
					}));
				}
				string text3 = text2.Substring(0, num).Trim();
				string text4 = text2.Substring(num + 1).Trim();
				if (text3.Length == 0)
				{
					throw new HttpParseException(SR.GetString("MissingExpressionPrefix", new object[]
					{
						attribvalue
					}));
				}
				if (text4.Length == 0)
				{
					throw new HttpParseException(SR.GetString("MissingExpressionValue", new object[]
					{
						attribvalue
					}));
				}
				ExpressionBuilder expressionBuilder = null;
				if (this.CompilationMode == CompilationMode.Never)
				{
					expressionBuilder = ExpressionBuilder.GetExpressionBuilder(text3, this.Parser.CurrentVirtualPath);
					if (expressionBuilder != null && !expressionBuilder.SupportsEvaluate)
					{
						throw new InvalidOperationException(SR.GetString("Cannot_evaluate_expression", new object[]
						{
							text3 + ":" + text4
						}));
					}
				}
				this.AddBoundProperty(filter, attribname, text3, text4, expressionBuilder, null, string.Empty, string.Empty, false, false, 0, 0);
				return;
			}
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x0005E29C File Offset: 0x0005C49C
		private bool IsValidForImplicitLocalization()
		{
			if (this.flags[8192])
			{
				return true;
			}
			if (this.ParentBuilder == null)
			{
				return false;
			}
			if (this.ParentBuilder.DefaultPropertyBuilder != null)
			{
				return typeof(ICollection).IsAssignableFrom(this.ParentBuilder.DefaultPropertyBuilder.ControlType);
			}
			return typeof(ICollection).IsAssignableFrom(this.ParentBuilder.ControlType);
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x0005E310 File Offset: 0x0005C510
		internal void ProcessImplicitResources(ParsedAttributeCollection attribs)
		{
			string text = (string)((IDictionary)attribs)["meta:localize"];
			if (text != null)
			{
				if (!this.IsValidForImplicitLocalization())
				{
					throw new InvalidOperationException(SR.GetString("meta_localize_notallowed", new object[]
					{
						this.TagName
					}));
				}
				bool localize;
				if (!bool.TryParse(text, out localize))
				{
					throw new HttpException(SR.GetString("ControlBuilder_InvalidLocalizeValue", new object[]
					{
						text
					}));
				}
				this.ParseTimeData.Localize = localize;
			}
			else
			{
				this.ParseTimeData.Localize = true;
			}
			string text2 = (string)((IDictionary)attribs)["meta:resourcekey"];
			attribs.ClearFilter("meta");
			if (text2 == null)
			{
				return;
			}
			if (!this.IsValidForImplicitLocalization())
			{
				throw new InvalidOperationException(SR.GetString("meta_reskey_notallowed", new object[]
				{
					this.TagName
				}));
			}
			if (!CodeGenerator.IsValidLanguageIndependentIdentifier(text2))
			{
				throw new HttpException(SR.GetString("Invalid_resourcekey", new object[]
				{
					text2
				}));
			}
			if (!this.ParseTimeData.Localize)
			{
				throw new HttpException(SR.GetString("meta_localize_error"));
			}
			this.ParseTimeData.ResourceKeyPrefix = text2;
			IImplicitResourceProvider implicitResourceProvider;
			if (this.Parser.FInDesigner && this.Parser.DesignerHost != null)
			{
				implicitResourceProvider = (IImplicitResourceProvider)this.Parser.DesignerHost.GetService(typeof(IImplicitResourceProvider));
			}
			else
			{
				implicitResourceProvider = this.Parser.GetImplicitResourceProvider();
			}
			ICollection collection = null;
			if (implicitResourceProvider != null)
			{
				collection = implicitResourceProvider.GetImplicitResourceKeys(text2);
			}
			if (collection != null)
			{
				IDesignerHost designerHost = this.DesignerHost;
				ExpressionBuilder expressionBuilder = ExpressionBuilder.GetExpressionBuilder("resources", this.Parser.CurrentVirtualPath, designerHost);
				bool flag = typeof(ResourceExpressionBuilder) == expressionBuilder.GetType();
				foreach (object obj in collection)
				{
					ImplicitResourceKey implicitResourceKey = (ImplicitResourceKey)obj;
					string text3 = text2 + "." + implicitResourceKey.Property;
					if (implicitResourceKey.Filter.Length > 0)
					{
						text3 = implicitResourceKey.Filter + ":" + text3;
					}
					string name = implicitResourceKey.Property.Replace('.', '-');
					object parsedExpressionData = null;
					string expression;
					if (flag)
					{
						parsedExpressionData = ResourceExpressionBuilder.ParseExpression(text3);
						expression = string.Empty;
					}
					else
					{
						expression = text3;
					}
					this.AddBoundProperty(implicitResourceKey.Filter, name, "resources", expression, expressionBuilder, parsedExpressionData, true, string.Empty, string.Empty, false, false, 0, 0);
				}
			}
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x0005E59C File Offset: 0x0005C79C
		private void PreprocessAttributes(ParsedAttributeCollection attribs)
		{
			this.ProcessImplicitResources(attribs);
			bool inClientBuildManager = BuildManagerHost.InClientBuildManager;
			IDictionary<string, Pair> dictionary = null;
			if (inClientBuildManager)
			{
				dictionary = attribs.AttributeValuePositionsDictionary;
			}
			foreach (object obj in attribs.GetFilteredAttributeDictionaries())
			{
				FilteredAttributeDictionary filteredAttributeDictionary = (FilteredAttributeDictionary)obj;
				string filter = filteredAttributeDictionary.Filter;
				foreach (object obj2 in ((IEnumerable)filteredAttributeDictionary))
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
					string text = dictionaryEntry.Key.ToString();
					string attribvalue = dictionaryEntry.Value.ToString();
					int column = 0;
					int line = 0;
					if (inClientBuildManager && dictionary.ContainsKey(text))
					{
						line = (int)dictionary[text].First;
						column = (int)dictionary[text].Second;
					}
					this.PreprocessAttribute(filter, text, attribvalue, false, line, column);
				}
			}
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x0005E6C4 File Offset: 0x0005C8C4
		public void SetServiceProvider(IServiceProvider serviceProvider)
		{
			this._serviceProvider = serviceProvider;
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x0005E6CD File Offset: 0x0005C8CD
		internal void EnsureEntriesSorted()
		{
			if (!this.flags[16384])
			{
				this.flags[16384] = true;
				this.SortEntries();
			}
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x0005E6F8 File Offset: 0x0005C8F8
		internal virtual void SortEntries()
		{
			if (this is CollectionBuilder)
			{
				return;
			}
			ControlBuilder.FilteredPropertyEntryComparer filteredPropertyEntryComparer = null;
			this.ProcessAndSortPropertyEntries(this._boundPropertyEntries, ref filteredPropertyEntryComparer);
			this.ProcessAndSortPropertyEntries(this._complexPropertyEntries, ref filteredPropertyEntryComparer);
			this.ProcessAndSortPropertyEntries(this._simplePropertyEntries, ref filteredPropertyEntryComparer);
			this.ProcessAndSortPropertyEntries(this._templatePropertyEntries, ref filteredPropertyEntryComparer);
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x0005E748 File Offset: 0x0005C948
		internal void ProcessAndSortPropertyEntries(ArrayList propertyEntries, ref ControlBuilder.FilteredPropertyEntryComparer comparer)
		{
			if (propertyEntries != null && propertyEntries.Count > 1)
			{
				HybridDictionary hybridDictionary = new HybridDictionary(propertyEntries.Count, true);
				int order = 0;
				foreach (object obj in propertyEntries)
				{
					PropertyEntry propertyEntry = (PropertyEntry)obj;
					object obj2 = hybridDictionary[propertyEntry.Name];
					if (obj2 != null)
					{
						propertyEntry.Order = (int)obj2;
					}
					else
					{
						propertyEntry.Order = order;
						hybridDictionary.Add(propertyEntry.Name, order++);
					}
				}
				if (comparer == null)
				{
					comparer = new ControlBuilder.FilteredPropertyEntryComparer(this.CurrentFilterResolutionService);
				}
				propertyEntries.Sort(comparer);
			}
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x0005E810 File Offset: 0x0005CA10
		internal void SetControlType(Type controlType)
		{
			this._controlType = controlType;
			if (this._controlType != null)
			{
				this.flags[8192] = typeof(Control).IsAssignableFrom(this._controlType);
				return;
			}
			this.flags[8192] = false;
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x0005E86C File Offset: 0x0005CA6C
		internal virtual void SetParentBuilder(ControlBuilder parentBuilder)
		{
			this.ParseTimeData.ParentBuilder = parentBuilder;
			if (this.ParseTimeData.FirstNonThemableProperty != null && parentBuilder is FileLevelPageThemeBuilder)
			{
				throw new InvalidOperationException(SR.GetString("Property_theme_disabled", new object[]
				{
					this.ParseTimeData.FirstNonThemableProperty.Name,
					this.ControlType.FullName
				}));
			}
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x0005E8D1 File Offset: 0x0005CAD1
		public string GetResourceKey()
		{
			return this.ParseTimeData.ResourceKeyPrefix;
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x0005E8E0 File Offset: 0x0005CAE0
		public void SetResourceKey(string resourceKey)
		{
			SimplePropertyEntry simplePropertyEntry = new SimplePropertyEntry();
			simplePropertyEntry.Filter = "meta";
			simplePropertyEntry.Name = "resourcekey";
			simplePropertyEntry.Value = resourceKey;
			simplePropertyEntry.PersistedValue = resourceKey;
			simplePropertyEntry.UseSetAttribute = true;
			simplePropertyEntry.Type = typeof(string);
			this.AddEntry(this.SimplePropertyEntriesInternal, simplePropertyEntry);
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void SetTagInnerText(string text)
		{
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void ProcessGeneratedCode(CodeCompileUnit codeCompileUnit, CodeTypeDeclaration baseType, CodeTypeDeclaration derivedType, CodeMemberMethod buildMethod, CodeMemberMethod dataBindingMethod)
		{
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x0005E93C File Offset: 0x0005CB3C
		private void ValidatePersistable(PropertyInfo propInfo, bool usingSetAttribute, bool mainDirectiveMode, bool simplePropertyEntry, string filter)
		{
			bool flag = propInfo.DeclaringType.IsAssignableFrom(this._controlType);
			PropertyDescriptorCollection propertyDescriptorCollection;
			if (flag)
			{
				propertyDescriptorCollection = this.PropertyDescriptors;
			}
			else
			{
				propertyDescriptorCollection = TargetFrameworkUtil.GetProperties(propInfo.DeclaringType);
			}
			PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[propInfo.Name];
			if (propertyDescriptor != null)
			{
				if (flag)
				{
					if (this.IsHtmlControl)
					{
						if (propertyDescriptor.Attributes.Contains(HtmlControlPersistableAttribute.No))
						{
							throw new HttpException(SR.GetString("Property_Not_Persistable", new object[]
							{
								propertyDescriptor.Name
							}));
						}
					}
					else if (!usingSetAttribute && !mainDirectiveMode && propertyDescriptor.Attributes.Contains(DesignerSerializationVisibilityAttribute.Hidden))
					{
						throw new HttpException(SR.GetString("Property_Not_Persistable", new object[]
						{
							propertyDescriptor.Name
						}));
					}
				}
				if (!FilterableAttribute.IsPropertyFilterable(propertyDescriptor) && !string.IsNullOrEmpty(filter))
				{
					throw new InvalidOperationException(SR.GetString("Illegal_Device", new object[]
					{
						propertyDescriptor.Name
					}));
				}
				if (this.InPageTheme && this.ParseTimeData.FirstNonThemableProperty == null && (!simplePropertyEntry || !usingSetAttribute))
				{
					ThemeableAttribute themeableAttribute = (ThemeableAttribute)propertyDescriptor.Attributes[typeof(ThemeableAttribute)];
					if (themeableAttribute != null && !themeableAttribute.Themeable)
					{
						if (this.ParentBuilder != null)
						{
							if (this.ParentBuilder is FileLevelPageThemeBuilder)
							{
								throw new InvalidOperationException(SR.GetString("Property_theme_disabled", new object[]
								{
									propertyDescriptor.Name,
									this.ControlType.FullName
								}));
							}
						}
						else
						{
							this.ParseTimeData.FirstNonThemableProperty = propertyDescriptor;
						}
					}
				}
			}
		}

		// Token: 0x0400190F RID: 6415
		public static readonly string DesignerFilter = "__designer";

		// Token: 0x04001910 RID: 6416
		private static readonly string ItemTypeProperty = "ItemType";

		// Token: 0x04001911 RID: 6417
		private static readonly Regex databindRegex = new DataBindRegex();

		// Token: 0x04001912 RID: 6418
		internal static readonly Regex expressionBuilderRegex = new ExpressionBuilderRegex();

		// Token: 0x04001913 RID: 6419
		private static readonly Regex bindExpressionRegex = new BindExpressionRegex();

		// Token: 0x04001914 RID: 6420
		private static readonly Regex bindParametersRegex = new BindParametersRegex();

		// Token: 0x04001915 RID: 6421
		private static readonly Regex bindItemExpressionRegex = new BindItemExpressionRegex();

		// Token: 0x04001916 RID: 6422
		private static readonly Regex bindItemParametersRegex = new BindItemParametersRegex();

		// Token: 0x04001917 RID: 6423
		private static readonly Regex evalExpressionRegex = new EvalExpressionRegex();

		// Token: 0x04001918 RID: 6424
		private static readonly Regex formatStringRegex = new FormatStringRegex();

		// Token: 0x04001919 RID: 6425
		private Type _controlType;

		// Token: 0x0400191A RID: 6426
		private string _tagName;

		// Token: 0x0400191B RID: 6427
		private string _skinID;

		// Token: 0x0400191C RID: 6428
		private ArrayList _subBuilders;

		// Token: 0x0400191D RID: 6429
		private ControlBuilder.ControlBuilderParseTimeData _parseTimeData;

		// Token: 0x0400191E RID: 6430
		private IServiceProvider _serviceProvider;

		// Token: 0x0400191F RID: 6431
		private ArrayList _eventEntries;

		// Token: 0x04001920 RID: 6432
		private ArrayList _simplePropertyEntries;

		// Token: 0x04001921 RID: 6433
		private ArrayList _complexPropertyEntries;

		// Token: 0x04001922 RID: 6434
		private ArrayList _templatePropertyEntries;

		// Token: 0x04001923 RID: 6435
		private ArrayList _boundPropertyEntries;

		// Token: 0x04001924 RID: 6436
		private IDictionary _additionalState;

		// Token: 0x04001925 RID: 6437
		private PropertyDescriptor _bindingContainerDescriptor;

		// Token: 0x04001926 RID: 6438
		private const int parseComplete = 1;

		// Token: 0x04001927 RID: 6439
		private const int needsTagAttributeComputed = 2;

		// Token: 0x04001928 RID: 6440
		private const int needsTagAttribute = 4;

		// Token: 0x04001929 RID: 6441
		private const int doneInitObjectOptimizations = 8;

		// Token: 0x0400192A RID: 6442
		private const int isICollection = 16;

		// Token: 0x0400192B RID: 6443
		private const int isIParserAccessor = 32;

		// Token: 0x0400192C RID: 6444
		private const int hasFilteredSimpleProps = 64;

		// Token: 0x0400192D RID: 6445
		private const int hasFilteredComplexProps = 128;

		// Token: 0x0400192E RID: 6446
		private const int hasFilteredTemplateProps = 256;

		// Token: 0x0400192F RID: 6447
		private const int hasFilteredBoundProps = 512;

		// Token: 0x04001930 RID: 6448
		private const int hasTwoWayBoundProps = 1024;

		// Token: 0x04001931 RID: 6449
		private const int triedFieldToControlBinding = 2048;

		// Token: 0x04001932 RID: 6450
		private const int hasFieldToControlBinding = 4096;

		// Token: 0x04001933 RID: 6451
		private const int controlTypeIsControl = 8192;

		// Token: 0x04001934 RID: 6452
		private const int entriesSorted = 16384;

		// Token: 0x04001935 RID: 6453
		private const int applyTheme = 32768;

		// Token: 0x04001936 RID: 6454
		private SimpleBitVector32 flags;

		// Token: 0x04001937 RID: 6455
		private static FactoryGenerator s_controlBuilderFactoryGenerator;

		// Token: 0x04001938 RID: 6456
		private static Hashtable s_controlBuilderFactoryCache;

		// Token: 0x04001939 RID: 6457
		private static ParseChildrenAttribute s_markerParseChildrenAttribute = new ParseChildrenAttribute();

		// Token: 0x0400193A RID: 6458
		private static Hashtable s_parseChildrenAttributeCache = new Hashtable();

		// Token: 0x0400193B RID: 6459
		private static IWebObjectFactory s_defaultControlBuilderFactory = new ControlBuilder.DefaultControlBuilderFactory();

		// Token: 0x0200095F RID: 2399
		private class DefaultControlBuilderFactory : IWebObjectFactory
		{
			// Token: 0x060069D1 RID: 27089 RVA: 0x001788BA File Offset: 0x00176ABA
			object IWebObjectFactory.CreateInstance()
			{
				return new ControlBuilder();
			}
		}

		// Token: 0x02000960 RID: 2400
		private class ReflectionBasedControlBuilderFactory : IWebObjectFactory
		{
			// Token: 0x060069D3 RID: 27091 RVA: 0x001788C1 File Offset: 0x00176AC1
			internal ReflectionBasedControlBuilderFactory(Type builderType)
			{
				this._builderType = builderType;
			}

			// Token: 0x060069D4 RID: 27092 RVA: 0x001788D0 File Offset: 0x00176AD0
			object IWebObjectFactory.CreateInstance()
			{
				return (ControlBuilder)HttpRuntime.CreateNonPublicInstance(this._builderType);
			}

			// Token: 0x04003820 RID: 14368
			private Type _builderType;
		}

		// Token: 0x02000961 RID: 2401
		private sealed class ControlBuilderParseTimeData
		{
			// Token: 0x17001D2A RID: 7466
			// (get) Token: 0x060069D5 RID: 27093 RVA: 0x001788E2 File Offset: 0x00176AE2
			// (set) Token: 0x060069D6 RID: 27094 RVA: 0x001788F0 File Offset: 0x00176AF0
			internal bool ChildrenAsProperties
			{
				get
				{
					return this.flags[1];
				}
				set
				{
					this.flags[1] = value;
				}
			}

			// Token: 0x17001D2B RID: 7467
			// (get) Token: 0x060069D7 RID: 27095 RVA: 0x001788FF File Offset: 0x00176AFF
			// (set) Token: 0x060069D8 RID: 27096 RVA: 0x0017890D File Offset: 0x00176B0D
			internal bool HasAspCode
			{
				get
				{
					return this.flags[2];
				}
				set
				{
					this.flags[2] = value;
				}
			}

			// Token: 0x17001D2C RID: 7468
			// (get) Token: 0x060069D9 RID: 27097 RVA: 0x0017891C File Offset: 0x00176B1C
			// (set) Token: 0x060069DA RID: 27098 RVA: 0x0017892A File Offset: 0x00176B2A
			internal bool IsHtmlControl
			{
				get
				{
					return this.flags[4];
				}
				set
				{
					this.flags[4] = value;
				}
			}

			// Token: 0x17001D2D RID: 7469
			// (get) Token: 0x060069DB RID: 27099 RVA: 0x00178939 File Offset: 0x00176B39
			// (set) Token: 0x060069DC RID: 27100 RVA: 0x0017894B File Offset: 0x00176B4B
			internal bool IgnoreControlProperties
			{
				get
				{
					return this.flags[256];
				}
				set
				{
					this.flags[256] = value;
				}
			}

			// Token: 0x17001D2E RID: 7470
			// (get) Token: 0x060069DD RID: 27101 RVA: 0x0017895E File Offset: 0x00176B5E
			// (set) Token: 0x060069DE RID: 27102 RVA: 0x0017896C File Offset: 0x00176B6C
			internal bool IsNonParserAccessor
			{
				get
				{
					return this.flags[8];
				}
				set
				{
					this.flags[8] = value;
				}
			}

			// Token: 0x17001D2F RID: 7471
			// (get) Token: 0x060069DF RID: 27103 RVA: 0x0017897B File Offset: 0x00176B7B
			// (set) Token: 0x060069E0 RID: 27104 RVA: 0x0017898A File Offset: 0x00176B8A
			internal bool IsGeneratedID
			{
				get
				{
					return this.flags[64];
				}
				set
				{
					this.flags[64] = value;
				}
			}

			// Token: 0x17001D30 RID: 7472
			// (get) Token: 0x060069E1 RID: 27105 RVA: 0x0017899A File Offset: 0x00176B9A
			// (set) Token: 0x060069E2 RID: 27106 RVA: 0x001789AC File Offset: 0x00176BAC
			internal bool Localize
			{
				get
				{
					return this.flags[128];
				}
				set
				{
					this.flags[128] = value;
				}
			}

			// Token: 0x17001D31 RID: 7473
			// (get) Token: 0x060069E3 RID: 27107 RVA: 0x001789BF File Offset: 0x00176BBF
			// (set) Token: 0x060069E4 RID: 27108 RVA: 0x001789CE File Offset: 0x00176BCE
			internal bool NamingContainerSearched
			{
				get
				{
					return this.flags[16];
				}
				set
				{
					this.flags[16] = value;
				}
			}

			// Token: 0x17001D32 RID: 7474
			// (get) Token: 0x060069E5 RID: 27109 RVA: 0x001789DE File Offset: 0x00176BDE
			// (set) Token: 0x060069E6 RID: 27110 RVA: 0x001789ED File Offset: 0x00176BED
			internal bool SupportsAttributes
			{
				get
				{
					return this.flags[32];
				}
				set
				{
					this.flags[32] = value;
				}
			}

			// Token: 0x04003821 RID: 14369
			private const int childrenAsProperties = 1;

			// Token: 0x04003822 RID: 14370
			private const int hasAspCode = 2;

			// Token: 0x04003823 RID: 14371
			private const int isHtmlControl = 4;

			// Token: 0x04003824 RID: 14372
			private const int isNonParserAccessor = 8;

			// Token: 0x04003825 RID: 14373
			private const int namingContainerSearched = 16;

			// Token: 0x04003826 RID: 14374
			private const int supportsAttributes = 32;

			// Token: 0x04003827 RID: 14375
			private const int isGeneratedID = 64;

			// Token: 0x04003828 RID: 14376
			private const int localize = 128;

			// Token: 0x04003829 RID: 14377
			private const int ignoreControlProperties = 256;

			// Token: 0x0400382A RID: 14378
			private SimpleBitVector32 flags;

			// Token: 0x0400382B RID: 14379
			internal ControlBuilder DefaultPropertyBuilder;

			// Token: 0x0400382C RID: 14380
			internal EventDescriptorCollection EventDescriptors;

			// Token: 0x0400382D RID: 14381
			internal string Filter;

			// Token: 0x0400382E RID: 14382
			internal string ID;

			// Token: 0x0400382F RID: 14383
			internal int Line;

			// Token: 0x04003830 RID: 14384
			internal ControlBuilder NamingContainerBuilder;

			// Token: 0x04003831 RID: 14385
			internal ControlBuilder ParentBuilder;

			// Token: 0x04003832 RID: 14386
			internal TemplateParser Parser;

			// Token: 0x04003833 RID: 14387
			internal PropertyDescriptorCollection PropertyDescriptors;

			// Token: 0x04003834 RID: 14388
			internal StringSet PropertyEntries;

			// Token: 0x04003835 RID: 14389
			internal VirtualPath VirtualPath;

			// Token: 0x04003836 RID: 14390
			internal PropertyDescriptor FirstNonThemableProperty;

			// Token: 0x04003837 RID: 14391
			internal string ResourceKeyPrefix;
		}

		// Token: 0x02000962 RID: 2402
		internal sealed class FilteredPropertyEntryComparer : IComparer
		{
			// Token: 0x060069E8 RID: 27112 RVA: 0x001789FD File Offset: 0x00176BFD
			public FilteredPropertyEntryComparer(IFilterResolutionService filterResolutionService)
			{
				this._filterResolutionService = filterResolutionService;
			}

			// Token: 0x060069E9 RID: 27113 RVA: 0x00178A0C File Offset: 0x00176C0C
			int IComparer.Compare(object o1, object o2)
			{
				if (o1 == o2)
				{
					return 0;
				}
				if (o1 == null)
				{
					return 1;
				}
				if (o2 == null)
				{
					return -1;
				}
				PropertyEntry propertyEntry = (PropertyEntry)o1;
				PropertyEntry propertyEntry2 = (PropertyEntry)o2;
				int num = propertyEntry.Order - propertyEntry2.Order;
				if (num == 0)
				{
					if (this._filterResolutionService == null)
					{
						if (string.IsNullOrEmpty(propertyEntry.Filter))
						{
							if (propertyEntry2.Filter != null && propertyEntry2.Filter.Length > 0)
							{
								num = 1;
							}
							else
							{
								num = 0;
							}
						}
						else if (string.IsNullOrEmpty(propertyEntry2.Filter))
						{
							num = -1;
						}
						else
						{
							num = 0;
						}
					}
					else
					{
						string filter = (propertyEntry.Filter.Length == 0) ? "Default" : propertyEntry.Filter;
						string filter2 = (propertyEntry2.Filter.Length == 0) ? "Default" : propertyEntry2.Filter;
						num = this._filterResolutionService.CompareFilters(filter, filter2);
					}
					if (num == 0)
					{
						return propertyEntry.Index - propertyEntry2.Index;
					}
				}
				return num;
			}

			// Token: 0x04003838 RID: 14392
			private IFilterResolutionService _filterResolutionService;
		}
	}
}
