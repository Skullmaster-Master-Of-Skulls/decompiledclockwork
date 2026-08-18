using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace System.Web.Mvc
{
	// Token: 0x02000194 RID: 404
	internal class ViewTypeParserFilter : PageParserFilter
	{
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0001E3E9 File Offset: 0x0001C5E9
		public override bool AllowCode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0001E3EC File Offset: 0x0001C5EC
		public override int NumberOfControlsAllowed
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0001E3EF File Offset: 0x0001C5EF
		public override int NumberOfDirectDependenciesAllowed
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0001E3F2 File Offset: 0x0001C5F2
		public override int TotalNumberOfDependenciesAllowed
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0001E3F8 File Offset: 0x0001C5F8
		public override void PreprocessDirective(string directiveName, IDictionary attributes)
		{
			base.PreprocessDirective(directiveName, attributes);
			Type type;
			if (ViewTypeParserFilter._directiveBaseTypeMappings.TryGetValue(directiveName, out type))
			{
				string text = attributes["inherits"] as string;
				if (text != null && text.IndexOfAny(new char[]
				{
					'<',
					'('
				}) > 0)
				{
					attributes["inherits"] = type.FullName;
					this._inherits = text;
				}
			}
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0001E464 File Offset: 0x0001C664
		public override void ParseComplete(ControlBuilder rootBuilder)
		{
			base.ParseComplete(rootBuilder);
			IMvcControlBuilder mvcControlBuilder = rootBuilder as IMvcControlBuilder;
			if (mvcControlBuilder != null)
			{
				mvcControlBuilder.Inherits = this._inherits;
			}
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0001E48E File Offset: 0x0001C68E
		public override bool AllowBaseType(Type baseType)
		{
			return true;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0001E491 File Offset: 0x0001C691
		public override bool AllowControl(Type controlType, ControlBuilder builder)
		{
			return true;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0001E494 File Offset: 0x0001C694
		public override bool AllowVirtualReference(string referenceVirtualPath, VirtualReferenceType referenceType)
		{
			return true;
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0001E497 File Offset: 0x0001C697
		public override bool AllowServerSideInclude(string includeVirtualPath)
		{
			return true;
		}

		// Token: 0x0400030B RID: 779
		private static Dictionary<string, Type> _directiveBaseTypeMappings = new Dictionary<string, Type>
		{
			{
				"page",
				typeof(ViewPage)
			},
			{
				"control",
				typeof(ViewUserControl)
			},
			{
				"master",
				typeof(ViewMasterPage)
			}
		};

		// Token: 0x0400030C RID: 780
		private string _inherits;
	}
}
