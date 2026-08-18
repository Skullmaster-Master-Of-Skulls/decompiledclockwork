using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace System.Web.Http.Owin.Properties
{
	// Token: 0x0200001B RID: 27
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class OwinResources
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x000053F4 File Offset: 0x000035F4
		internal OwinResources()
		{
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000053FC File Offset: 0x000035FC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(OwinResources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("System.Web.Http.Owin.Properties.OwinResources", typeof(OwinResources).Assembly);
					OwinResources.resourceMan = resourceManager;
				}
				return OwinResources.resourceMan;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000543B File Offset: 0x0000363B
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00005442 File Offset: 0x00003642
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return OwinResources.resourceCulture;
			}
			set
			{
				OwinResources.resourceCulture = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000544A File Offset: 0x0000364A
		internal static string HttpAuthenticationChallengeContext_RequestMustNotBeNull
		{
			get
			{
				return OwinResources.ResourceManager.GetString("HttpAuthenticationChallengeContext_RequestMustNotBeNull", OwinResources.resourceCulture);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00005460 File Offset: 0x00003660
		internal static string HttpAuthenticationContext_RequestMustNotBeNull
		{
			get
			{
				return OwinResources.ResourceManager.GetString("HttpAuthenticationContext_RequestMustNotBeNull", OwinResources.resourceCulture);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00005476 File Offset: 0x00003676
		internal static string IAuthenticationManagerNotAvailable
		{
			get
			{
				return OwinResources.ResourceManager.GetString("IAuthenticationManagerNotAvailable", OwinResources.resourceCulture);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000548C File Offset: 0x0000368C
		internal static string OwinContext_NullRequest
		{
			get
			{
				return OwinResources.ResourceManager.GetString("OwinContext_NullRequest", OwinResources.resourceCulture);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000054A2 File Offset: 0x000036A2
		internal static string OwinContext_NullResponse
		{
			get
			{
				return OwinResources.ResourceManager.GetString("OwinContext_NullResponse", OwinResources.resourceCulture);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000054B8 File Offset: 0x000036B8
		internal static string Request_RequestContextMustNotBeNull
		{
			get
			{
				return OwinResources.ResourceManager.GetString("Request_RequestContextMustNotBeNull", OwinResources.resourceCulture);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000054CE File Offset: 0x000036CE
		internal static string SendAsync_ReturnedNull
		{
			get
			{
				return OwinResources.ResourceManager.GetString("SendAsync_ReturnedNull", OwinResources.resourceCulture);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000054E4 File Offset: 0x000036E4
		internal static string TypePropertyMustNotBeNull
		{
			get
			{
				return OwinResources.ResourceManager.GetString("TypePropertyMustNotBeNull", OwinResources.resourceCulture);
			}
		}

		// Token: 0x04000032 RID: 50
		private static ResourceManager resourceMan;

		// Token: 0x04000033 RID: 51
		private static CultureInfo resourceCulture;
	}
}
