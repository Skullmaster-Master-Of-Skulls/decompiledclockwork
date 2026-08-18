using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x02001055 RID: 4181
	[ClientScriptResource("Telerik.Web.UI.Widgets.CodeFormatter", "Telerik.Web.UI.Common.Core.js")]
	[ToolboxItem(false)]
	[RequiredScript(typeof(jQuery))]
	public class FormatCodeBlockDialog : UserControlBase, IClientParameterConsumer
	{
		// Token: 0x17003638 RID: 13880
		// (get) Token: 0x0600A8F5 RID: 43253 RVA: 0x0024B5E5 File Offset: 0x002497E5
		public override string DialogName
		{
			get
			{
				return "FormatCodeBlock";
			}
		}

		// Token: 0x0600A8F6 RID: 43254 RVA: 0x0024B5EC File Offset: 0x002497EC
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			return new List<ScriptReference>(base.GetScriptReferences())
			{
				new ScriptReference("Telerik.Web.UI.Editor.DialogControls.FormatCodeBlock.Languages.js", typeof(FormatCodeBlockDialog).Assembly.FullName)
			};
		}
	}
}
