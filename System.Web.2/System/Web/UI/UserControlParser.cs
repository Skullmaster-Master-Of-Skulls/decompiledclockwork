using System;
using System.Collections;
using System.Web.Caching;

namespace System.Web.UI
{
	// Token: 0x02000321 RID: 801
	internal class UserControlParser : TemplateControlParser
	{
		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x0007AB16 File Offset: 0x00078D16
		internal bool FSharedPartialCaching
		{
			get
			{
				return this._fSharedPartialCaching;
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x0600253C RID: 9532 RVA: 0x0007AB1E File Offset: 0x00078D1E
		internal string Provider
		{
			get
			{
				return this._provider;
			}
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x0007AB26 File Offset: 0x00078D26
		internal override void ProcessConfigSettings()
		{
			base.ProcessConfigSettings();
			this.ApplyBaseType();
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x0007AB34 File Offset: 0x00078D34
		internal virtual void ApplyBaseType()
		{
			if (PageParser.DefaultUserControlBaseType != null)
			{
				base.BaseType = PageParser.DefaultUserControlBaseType;
				return;
			}
			if (base.PagesConfig != null && base.PagesConfig.UserControlBaseTypeInternal != null)
			{
				base.BaseType = base.PagesConfig.UserControlBaseTypeInternal;
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x0600253F RID: 9535 RVA: 0x0007AB86 File Offset: 0x00078D86
		internal override Type DefaultBaseType
		{
			get
			{
				return typeof(UserControl);
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06002540 RID: 9536 RVA: 0x00054FD7 File Offset: 0x000531D7
		internal override string DefaultDirectiveName
		{
			get
			{
				return "control";
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06002541 RID: 9537 RVA: 0x0007AB92 File Offset: 0x00078D92
		internal override Type DefaultFileLevelBuilderType
		{
			get
			{
				return typeof(FileLevelUserControlBuilder);
			}
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x0007AB9E File Offset: 0x00078D9E
		internal override RootBuilder CreateDefaultFileLevelBuilder()
		{
			return new FileLevelUserControlBuilder();
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x0007ABA8 File Offset: 0x00078DA8
		internal override void ProcessOutputCacheDirective(string directiveName, IDictionary directive)
		{
			Util.GetAndRemoveBooleanAttribute(directive, "shared", ref this._fSharedPartialCaching);
			this._provider = Util.GetAndRemoveNonEmptyAttribute(directive, "providerName");
			if (this._provider == "AspNetInternalProvider")
			{
				this._provider = null;
			}
			OutputCache.ThrowIfProviderNotFound(this._provider);
			string andRemoveNonEmptyAttribute = Util.GetAndRemoveNonEmptyAttribute(directive, "sqldependency");
			if (andRemoveNonEmptyAttribute != null)
			{
				SqlCacheDependency.ValidateOutputCacheDependencyString(andRemoveNonEmptyAttribute, false);
				base.OutputCacheParameters.SqlDependency = andRemoveNonEmptyAttribute;
			}
			base.ProcessOutputCacheDirective(directiveName, directive);
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06002544 RID: 9540 RVA: 0x0007AC26 File Offset: 0x00078E26
		internal override bool FVaryByParamsRequiredOnOutputCache
		{
			get
			{
				return base.OutputCacheParameters.VaryByControl == null;
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06002545 RID: 9541 RVA: 0x0007AC36 File Offset: 0x00078E36
		internal override string UnknownOutputCacheAttributeError
		{
			get
			{
				return "Attr_not_supported_in_ucdirective";
			}
		}

		// Token: 0x04001D73 RID: 7539
		private bool _fSharedPartialCaching;

		// Token: 0x04001D74 RID: 7540
		private string _provider;

		// Token: 0x04001D75 RID: 7541
		internal const string defaultDirectiveName = "control";
	}
}
