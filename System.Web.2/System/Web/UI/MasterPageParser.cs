using System;
using System.Collections;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x020002C6 RID: 710
	internal sealed class MasterPageParser : UserControlParser
	{
		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06002007 RID: 8199 RVA: 0x00065EE0 File Offset: 0x000640E0
		internal override Type DefaultBaseType
		{
			get
			{
				return typeof(MasterPage);
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06002008 RID: 8200 RVA: 0x00054FDE File Offset: 0x000531DE
		internal override string DefaultDirectiveName
		{
			get
			{
				return "master";
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06002009 RID: 8201 RVA: 0x00065EEC File Offset: 0x000640EC
		internal override Type DefaultFileLevelBuilderType
		{
			get
			{
				return typeof(FileLevelMasterPageControlBuilder);
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x0600200A RID: 8202 RVA: 0x00065EF8 File Offset: 0x000640F8
		internal Type MasterPageType
		{
			get
			{
				return this._masterPageType;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x0600200B RID: 8203 RVA: 0x00065F00 File Offset: 0x00064100
		internal CaseInsensitiveStringSet PlaceHolderList
		{
			get
			{
				if (this._placeHolderList == null)
				{
					this._placeHolderList = new CaseInsensitiveStringSet();
				}
				return this._placeHolderList;
			}
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x00006164 File Offset: 0x00004364
		internal override void ApplyBaseType()
		{
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x00065F1B File Offset: 0x0006411B
		internal override RootBuilder CreateDefaultFileLevelBuilder()
		{
			return new FileLevelMasterPageControlBuilder();
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x00065F24 File Offset: 0x00064124
		internal override void ProcessDirective(string directiveName, IDictionary directive)
		{
			if (StringUtil.EqualsIgnoreCase(directiveName, "masterType"))
			{
				if (this._masterPageType != null)
				{
					base.ProcessError(SR.GetString("Only_one_directive_allowed", new object[]
					{
						directiveName
					}));
					return;
				}
				this._masterPageType = base.GetDirectiveType(directive, directiveName);
				Util.CheckAssignableType(typeof(MasterPage), this._masterPageType);
				return;
			}
			else
			{
				if (StringUtil.EqualsIgnoreCase(directiveName, "outputcache"))
				{
					base.ProcessError(SR.GetString("Directive_not_allowed", new object[]
					{
						directiveName
					}));
					return;
				}
				base.ProcessDirective(directiveName, directive);
				return;
			}
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x00065FBC File Offset: 0x000641BC
		internal override bool ProcessMainDirectiveAttribute(string deviceName, string name, string value, IDictionary parseData)
		{
			if (!(name == "masterpagefile"))
			{
				if (!(name == "outputcaching"))
				{
					return base.ProcessMainDirectiveAttribute(deviceName, name, value, parseData);
				}
				base.ProcessError(SR.GetString("Attr_not_supported_in_directive", new object[]
				{
					name,
					this.DefaultDirectiveName
				}));
				return false;
			}
			else
			{
				if (base.IsExpressionBuilderValue(value))
				{
					return false;
				}
				if (value.Length > 0)
				{
					Type referencedType = base.GetReferencedType(value);
					Util.CheckAssignableType(typeof(MasterPage), referencedType);
				}
				return false;
			}
		}

		// Token: 0x04001ACA RID: 6858
		internal new const string defaultDirectiveName = "master";

		// Token: 0x04001ACB RID: 6859
		private Type _masterPageType;

		// Token: 0x04001ACC RID: 6860
		private CaseInsensitiveStringSet _placeHolderList;
	}
}
