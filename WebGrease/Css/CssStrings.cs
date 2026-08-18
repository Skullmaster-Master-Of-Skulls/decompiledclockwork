using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace WebGrease.Css
{
	// Token: 0x02000183 RID: 387
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	public class CssStrings
	{
		// Token: 0x0600143F RID: 5183 RVA: 0x00077062 File Offset: 0x00075262
		internal CssStrings()
		{
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x0007706C File Offset: 0x0007526C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(CssStrings.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("WebGrease.Css.CssStrings", typeof(CssStrings).Assembly);
					CssStrings.resourceMan = resourceManager;
				}
				return CssStrings.resourceMan;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x000770AB File Offset: 0x000752AB
		// (set) Token: 0x06001442 RID: 5186 RVA: 0x000770B2 File Offset: 0x000752B2
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static CultureInfo Culture
		{
			get
			{
				return CssStrings.resourceCulture;
			}
			set
			{
				CssStrings.resourceCulture = value;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x000770BA File Offset: 0x000752BA
		public static string CssLowercaseValidationError
		{
			get
			{
				return CssStrings.ResourceManager.GetString("CssLowercaseValidationError", CssStrings.resourceCulture);
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x000770D0 File Offset: 0x000752D0
		public static string CssLowercaseValidationParentNodeError
		{
			get
			{
				return CssStrings.ResourceManager.GetString("CssLowercaseValidationParentNodeError", CssStrings.resourceCulture);
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x000770E6 File Offset: 0x000752E6
		public static string CssSelectorHackError
		{
			get
			{
				return CssStrings.ResourceManager.GetString("CssSelectorHackError", CssStrings.resourceCulture);
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x000770FC File Offset: 0x000752FC
		public static string DuplicateBackgroundFormatError
		{
			get
			{
				return CssStrings.ResourceManager.GetString("DuplicateBackgroundFormatError", CssStrings.resourceCulture);
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x00077112 File Offset: 0x00075312
		public static string DuplicateImageReferenceWithDifferentRulesError
		{
			get
			{
				return CssStrings.ResourceManager.GetString("DuplicateImageReferenceWithDifferentRulesError", CssStrings.resourceCulture);
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x00077128 File Offset: 0x00075328
		public static string ExpectedAstNode
		{
			get
			{
				return CssStrings.ResourceManager.GetString("ExpectedAstNode", CssStrings.resourceCulture);
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x0007713E File Offset: 0x0007533E
		public static string ExpectedEnum
		{
			get
			{
				return CssStrings.ResourceManager.GetString("ExpectedEnum", CssStrings.resourceCulture);
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x00077154 File Offset: 0x00075354
		public static string ExpectedIdentifierOrString
		{
			get
			{
				return CssStrings.ResourceManager.GetString("ExpectedIdentifierOrString", CssStrings.resourceCulture);
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x0007716A File Offset: 0x0007536A
		public static string ExpectedOperator
		{
			get
			{
				return CssStrings.ResourceManager.GetString("ExpectedOperator", CssStrings.resourceCulture);
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x00077180 File Offset: 0x00075380
		public static string ExpectedSimpleSelector
		{
			get
			{
				return CssStrings.ResourceManager.GetString("ExpectedSimpleSelector", CssStrings.resourceCulture);
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x00077196 File Offset: 0x00075396
		public static string ExpectedSingleValue
		{
			get
			{
				return CssStrings.ResourceManager.GetString("ExpectedSingleValue", CssStrings.resourceCulture);
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x000771AC File Offset: 0x000753AC
		public static string ExpectedValue
		{
			get
			{
				return CssStrings.ResourceManager.GetString("ExpectedValue", CssStrings.resourceCulture);
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x000771C2 File Offset: 0x000753C2
		public static string FileNotFoundError
		{
			get
			{
				return CssStrings.ResourceManager.GetString("FileNotFoundError", CssStrings.resourceCulture);
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x000771D8 File Offset: 0x000753D8
		public static string InnerExceptionFile
		{
			get
			{
				return CssStrings.ResourceManager.GetString("InnerExceptionFile", CssStrings.resourceCulture);
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x000771EE File Offset: 0x000753EE
		public static string InnerExceptionSelector
		{
			get
			{
				return CssStrings.ResourceManager.GetString("InnerExceptionSelector", CssStrings.resourceCulture);
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x00077204 File Offset: 0x00075404
		public static string InvalidDimensionsError
		{
			get
			{
				return CssStrings.ResourceManager.GetString("InvalidDimensionsError", CssStrings.resourceCulture);
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x0007721A File Offset: 0x0007541A
		public static string OriginalFileElementEmptyError
		{
			get
			{
				return CssStrings.ResourceManager.GetString("OriginalFileElementEmptyError", CssStrings.resourceCulture);
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x00077230 File Offset: 0x00075430
		public static string RepeatedPropertyNameError
		{
			get
			{
				return CssStrings.ResourceManager.GetString("RepeatedPropertyNameError", CssStrings.resourceCulture);
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x00077246 File Offset: 0x00075446
		public static string TooManyLengthsError
		{
			get
			{
				return CssStrings.ResourceManager.GetString("TooManyLengthsError", CssStrings.resourceCulture);
			}
		}

		// Token: 0x04000ACD RID: 2765
		private static ResourceManager resourceMan;

		// Token: 0x04000ACE RID: 2766
		private static CultureInfo resourceCulture;
	}
}
