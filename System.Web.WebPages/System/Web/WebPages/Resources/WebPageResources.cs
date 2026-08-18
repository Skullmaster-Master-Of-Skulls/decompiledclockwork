using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace System.Web.WebPages.Resources
{
	// Token: 0x02000092 RID: 146
	[CompilerGenerated]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	internal class WebPageResources
	{
		// Token: 0x060004A2 RID: 1186 RVA: 0x0000E378 File Offset: 0x0000C578
		internal WebPageResources()
		{
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000E380 File Offset: 0x0000C580
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(WebPageResources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("System.Web.WebPages.Resources.WebPageResources", typeof(WebPageResources).Assembly);
					WebPageResources.resourceMan = resourceManager;
				}
				return WebPageResources.resourceMan;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0000E3BF File Offset: 0x0000C5BF
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x0000E3C6 File Offset: 0x0000C5C6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return WebPageResources.resourceCulture;
			}
			set
			{
				WebPageResources.resourceCulture = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0000E3CE File Offset: 0x0000C5CE
		internal static string AntiForgeryToken_AdditionalDataCheckFailed
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("AntiForgeryToken_AdditionalDataCheckFailed", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0000E3E4 File Offset: 0x0000C5E4
		internal static string AntiForgeryToken_ClaimUidMismatch
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("AntiForgeryToken_ClaimUidMismatch", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000E3FA File Offset: 0x0000C5FA
		internal static string AntiForgeryToken_CookieMissing
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("AntiForgeryToken_CookieMissing", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000E410 File Offset: 0x0000C610
		internal static string AntiForgeryToken_DeserializationFailed
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("AntiForgeryToken_DeserializationFailed", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0000E426 File Offset: 0x0000C626
		internal static string AntiForgeryToken_FormFieldMissing
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("AntiForgeryToken_FormFieldMissing", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000E43C File Offset: 0x0000C63C
		internal static string AntiForgeryToken_SecurityTokenMismatch
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("AntiForgeryToken_SecurityTokenMismatch", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000E452 File Offset: 0x0000C652
		internal static string AntiForgeryToken_TokensSwapped
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("AntiForgeryToken_TokensSwapped", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000E468 File Offset: 0x0000C668
		internal static string AntiForgeryToken_UsernameMismatch
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("AntiForgeryToken_UsernameMismatch", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0000E47E File Offset: 0x0000C67E
		internal static string AntiForgeryWorker_RequireSSL
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("AntiForgeryWorker_RequireSSL", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0000E494 File Offset: 0x0000C694
		internal static string ApplicationPart_ModuleAlreadyRegistered
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("ApplicationPart_ModuleAlreadyRegistered", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0000E4AA File Offset: 0x0000C6AA
		internal static string ApplicationPart_ModuleAlreadyRegisteredForVirtualPath
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("ApplicationPart_ModuleAlreadyRegisteredForVirtualPath", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0000E4C0 File Offset: 0x0000C6C0
		internal static string ApplicationPart_ModuleCannotBeFound
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("ApplicationPart_ModuleCannotBeFound", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000E4D6 File Offset: 0x0000C6D6
		internal static string ApplicationPart_ModuleNotRegistered
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("ApplicationPart_ModuleNotRegistered", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0000E4EC File Offset: 0x0000C6EC
		internal static string ApplicationPart_ResourceNotFound
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("ApplicationPart_ResourceNotFound", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0000E502 File Offset: 0x0000C702
		internal static string ClaimUidExtractor_ClaimNotPresent
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("ClaimUidExtractor_ClaimNotPresent", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000E518 File Offset: 0x0000C718
		internal static string ClaimUidExtractor_DefaultClaimsNotPresent
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("ClaimUidExtractor_DefaultClaimsNotPresent", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0000E52E File Offset: 0x0000C72E
		internal static string DynamicDictionary_InvalidNumberOfIndexes
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("DynamicDictionary_InvalidNumberOfIndexes", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000E544 File Offset: 0x0000C744
		internal static string DynamicHttpApplicationState_UseOnlyStringOrIntToGet
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("DynamicHttpApplicationState_UseOnlyStringOrIntToGet", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0000E55A File Offset: 0x0000C75A
		internal static string DynamicHttpApplicationState_UseOnlyStringToSet
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("DynamicHttpApplicationState_UseOnlyStringToSet", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0000E570 File Offset: 0x0000C770
		internal static string HtmlHelper_ConversionThrew
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("HtmlHelper_ConversionThrew", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x0000E586 File Offset: 0x0000C786
		internal static string HtmlHelper_NoConverterExists
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("HtmlHelper_NoConverterExists", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0000E59C File Offset: 0x0000C79C
		internal static string HttpContextUnavailable
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("HttpContextUnavailable", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x0000E5B2 File Offset: 0x0000C7B2
		internal static string SessionState_InvalidValue
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("SessionState_InvalidValue", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0000E5C8 File Offset: 0x0000C7C8
		internal static string SessionState_TooManyValues
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("SessionState_TooManyValues", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0000E5DE File Offset: 0x0000C7DE
		internal static string StateStorage_RequestScopeNotAvailable
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("StateStorage_RequestScopeNotAvailable", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000E5F4 File Offset: 0x0000C7F4
		internal static string StateStorage_ScopeIsReadOnly
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("StateStorage_ScopeIsReadOnly", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0000E60A File Offset: 0x0000C80A
		internal static string StateStorage_StorageScopesCannotBeCreated
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("StateStorage_StorageScopesCannotBeCreated", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0000E620 File Offset: 0x0000C820
		internal static string TokenValidator_AuthenticatedUserWithoutUsername
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("TokenValidator_AuthenticatedUserWithoutUsername", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0000E636 File Offset: 0x0000C836
		internal static string UnobtrusiveJavascript_ValidationParameterCannotBeEmpty
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("UnobtrusiveJavascript_ValidationParameterCannotBeEmpty", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0000E64C File Offset: 0x0000C84C
		internal static string UnobtrusiveJavascript_ValidationParameterMustBeLegal
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("UnobtrusiveJavascript_ValidationParameterMustBeLegal", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0000E662 File Offset: 0x0000C862
		internal static string UnobtrusiveJavascript_ValidationTypeCannotBeEmpty
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("UnobtrusiveJavascript_ValidationTypeCannotBeEmpty", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000E678 File Offset: 0x0000C878
		internal static string UnobtrusiveJavascript_ValidationTypeMustBeLegal
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("UnobtrusiveJavascript_ValidationTypeMustBeLegal", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0000E68E File Offset: 0x0000C88E
		internal static string UnobtrusiveJavascript_ValidationTypeMustBeUnique
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("UnobtrusiveJavascript_ValidationTypeMustBeUnique", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0000E6A4 File Offset: 0x0000C8A4
		internal static string UrlData_ReadOnly
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("UrlData_ReadOnly", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0000E6BA File Offset: 0x0000C8BA
		internal static string ValidationDefault_DataType
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("ValidationDefault_DataType", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000E6D0 File Offset: 0x0000C8D0
		internal static string ValidationDefault_EqualsTo
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("ValidationDefault_EqualsTo", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0000E6E6 File Offset: 0x0000C8E6
		internal static string ValidationDefault_FloatRange
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("ValidationDefault_FloatRange", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000E6FC File Offset: 0x0000C8FC
		internal static string ValidationDefault_IntegerRange
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("ValidationDefault_IntegerRange", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0000E712 File Offset: 0x0000C912
		internal static string ValidationDefault_Regex
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("ValidationDefault_Regex", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0000E728 File Offset: 0x0000C928
		internal static string ValidationDefault_Required
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("ValidationDefault_Required", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0000E73E File Offset: 0x0000C93E
		internal static string ValidationDefault_StringLength
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("ValidationDefault_StringLength", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000E754 File Offset: 0x0000C954
		internal static string ValidationDefault_StringLengthRange
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("ValidationDefault_StringLengthRange", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0000E76A File Offset: 0x0000C96A
		internal static string WebPage_CannotRequestDirectly
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("WebPage_CannotRequestDirectly", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0000E780 File Offset: 0x0000C980
		internal static string WebPage_FileNotSupported
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("WebPage_FileNotSupported", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000E796 File Offset: 0x0000C996
		internal static string WebPage_InvalidPageType
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("WebPage_InvalidPageType", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0000E7AC File Offset: 0x0000C9AC
		internal static string WebPage_LayoutPageNotFound
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("WebPage_LayoutPageNotFound", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0000E7C2 File Offset: 0x0000C9C2
		internal static string WebPage_RenderBodyAlreadyCalled
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("WebPage_RenderBodyAlreadyCalled", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0000E7D8 File Offset: 0x0000C9D8
		internal static string WebPage_RenderBodyNotCalled
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("WebPage_RenderBodyNotCalled", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0000E7EE File Offset: 0x0000C9EE
		internal static string WebPage_SectionAleadyDefined
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("WebPage_SectionAleadyDefined", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0000E804 File Offset: 0x0000CA04
		internal static string WebPage_SectionAleadyRendered
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("WebPage_SectionAleadyRendered", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0000E81A File Offset: 0x0000CA1A
		internal static string WebPage_SectionNotDefined
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("WebPage_SectionNotDefined", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0000E830 File Offset: 0x0000CA30
		internal static string WebPage_SectionsNotRendered
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("WebPage_SectionsNotRendered", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0000E846 File Offset: 0x0000CA46
		internal static string WebPageRoute_UnderscoreBlocked
		{
			get
			{
				return WebPageResources.ResourceManager.GetString("WebPageRoute_UnderscoreBlocked", WebPageResources.resourceCulture);
			}
		}

		// Token: 0x0400013F RID: 319
		private static ResourceManager resourceMan;

		// Token: 0x04000140 RID: 320
		private static CultureInfo resourceCulture;
	}
}
