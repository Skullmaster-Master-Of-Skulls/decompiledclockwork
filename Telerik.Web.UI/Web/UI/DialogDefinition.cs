using System;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001030 RID: 4144
	public class DialogDefinition
	{
		// Token: 0x17003385 RID: 13189
		// (get) Token: 0x0600A33B RID: 41787 RVA: 0x00245387 File Offset: 0x00243587
		[ScriptIgnore]
		public string VirtualPath
		{
			get
			{
				return (string)this.Parameters["Telerik.DialogDefinition.VirtualPath"];
			}
		}

		// Token: 0x17003386 RID: 13190
		// (get) Token: 0x0600A33C RID: 41788 RVA: 0x0024539E File Offset: 0x0024359E
		[ScriptIgnore]
		public Type DialogType
		{
			get
			{
				return Type.GetType((string)this.Parameters["Telerik.DialogDefinition.DialogTypeName"]);
			}
		}

		// Token: 0x17003387 RID: 13191
		// (get) Token: 0x0600A33D RID: 41789 RVA: 0x002453BA File Offset: 0x002435BA
		[ScriptIgnore]
		public DialogParameters Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x17003388 RID: 13192
		// (get) Token: 0x0600A33E RID: 41790 RVA: 0x002453C2 File Offset: 0x002435C2
		public string SerializedParameters
		{
			get
			{
				return this.Parameters.Serialize();
			}
		}

		// Token: 0x17003389 RID: 13193
		// (get) Token: 0x0600A33F RID: 41791 RVA: 0x002453CF File Offset: 0x002435CF
		// (set) Token: 0x0600A340 RID: 41792 RVA: 0x002453D7 File Offset: 0x002435D7
		public Unit Width
		{
			get
			{
				return this._width;
			}
			set
			{
				this._width = value;
			}
		}

		// Token: 0x1700338A RID: 13194
		// (get) Token: 0x0600A341 RID: 41793 RVA: 0x002453E0 File Offset: 0x002435E0
		// (set) Token: 0x0600A342 RID: 41794 RVA: 0x002453E8 File Offset: 0x002435E8
		public Unit Height
		{
			get
			{
				return this._height;
			}
			set
			{
				this._height = value;
			}
		}

		// Token: 0x1700338B RID: 13195
		// (get) Token: 0x0600A343 RID: 41795 RVA: 0x002453F1 File Offset: 0x002435F1
		// (set) Token: 0x0600A344 RID: 41796 RVA: 0x002453F9 File Offset: 0x002435F9
		[DefaultValue("")]
		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}

		// Token: 0x1700338C RID: 13196
		// (get) Token: 0x0600A345 RID: 41797 RVA: 0x00245402 File Offset: 0x00243602
		// (set) Token: 0x0600A346 RID: 41798 RVA: 0x0024540A File Offset: 0x0024360A
		[DefaultValue("")]
		public string ClientCallbackFunction
		{
			get
			{
				return this._clientCallbackFunction;
			}
			set
			{
				this._clientCallbackFunction = value;
			}
		}

		// Token: 0x1700338D RID: 13197
		// (get) Token: 0x0600A347 RID: 41799 RVA: 0x00245413 File Offset: 0x00243613
		// (set) Token: 0x0600A348 RID: 41800 RVA: 0x0024541B File Offset: 0x0024361B
		[DefaultValue(WindowBehaviors.Close | WindowBehaviors.Move)]
		public WindowBehaviors Behaviors
		{
			get
			{
				return this._behaviors;
			}
			set
			{
				this._behaviors = value;
			}
		}

		// Token: 0x1700338E RID: 13198
		// (get) Token: 0x0600A349 RID: 41801 RVA: 0x00245424 File Offset: 0x00243624
		// (set) Token: 0x0600A34A RID: 41802 RVA: 0x0024542C File Offset: 0x0024362C
		[DefaultValue(true)]
		public bool Modal
		{
			get
			{
				return this._modal;
			}
			set
			{
				this._modal = value;
			}
		}

		// Token: 0x1700338F RID: 13199
		// (get) Token: 0x0600A34B RID: 41803 RVA: 0x00245435 File Offset: 0x00243635
		// (set) Token: 0x0600A34C RID: 41804 RVA: 0x0024543D File Offset: 0x0024363D
		[DefaultValue(false)]
		public bool VisibleStatusbar
		{
			get
			{
				return this._visibleStatusbar;
			}
			set
			{
				this._visibleStatusbar = value;
			}
		}

		// Token: 0x17003390 RID: 13200
		// (get) Token: 0x0600A34D RID: 41805 RVA: 0x00245446 File Offset: 0x00243646
		// (set) Token: 0x0600A34E RID: 41806 RVA: 0x0024544E File Offset: 0x0024364E
		[DefaultValue(true)]
		public bool VisibleTitlebar
		{
			get
			{
				return this._visibleTitlebar;
			}
			set
			{
				this._visibleTitlebar = value;
			}
		}

		// Token: 0x17003391 RID: 13201
		// (get) Token: 0x0600A34F RID: 41807 RVA: 0x00245457 File Offset: 0x00243657
		// (set) Token: 0x0600A350 RID: 41808 RVA: 0x0024545F File Offset: 0x0024365F
		[DefaultValue(false)]
		public bool ReloadOnShow
		{
			get
			{
				return this._reloadOnShow;
			}
			set
			{
				this._reloadOnShow = value;
			}
		}

		// Token: 0x0600A351 RID: 41809 RVA: 0x00245468 File Offset: 0x00243668
		internal DialogDefinition(DialogParameters parameters)
		{
			this._parameters = parameters;
		}

		// Token: 0x0600A352 RID: 41810 RVA: 0x002454A4 File Offset: 0x002436A4
		public DialogDefinition(string virtualPath, DialogParameters parameters)
		{
			this._parameters = parameters;
			this.Parameters["Telerik.DialogDefinition.VirtualPath"] = virtualPath;
		}

		// Token: 0x0600A353 RID: 41811 RVA: 0x002454FC File Offset: 0x002436FC
		public DialogDefinition(Type dialogType, DialogParameters parameters)
		{
			this._parameters = parameters;
			this.Parameters["Telerik.DialogDefinition.DialogTypeName"] = dialogType.AssemblyQualifiedName;
		}

		// Token: 0x04002D5F RID: 11615
		internal const string DialogTypeNameKey = "Telerik.DialogDefinition.DialogTypeName";

		// Token: 0x04002D60 RID: 11616
		internal const string VirtualPathKey = "Telerik.DialogDefinition.VirtualPath";

		// Token: 0x04002D61 RID: 11617
		private Unit _width = Unit.Empty;

		// Token: 0x04002D62 RID: 11618
		private Unit _height = Unit.Empty;

		// Token: 0x04002D63 RID: 11619
		private string _title;

		// Token: 0x04002D64 RID: 11620
		private string _clientCallbackFunction;

		// Token: 0x04002D65 RID: 11621
		private WindowBehaviors _behaviors = WindowBehaviors.Close | WindowBehaviors.Move;

		// Token: 0x04002D66 RID: 11622
		private bool _modal = true;

		// Token: 0x04002D67 RID: 11623
		private bool _visibleStatusbar;

		// Token: 0x04002D68 RID: 11624
		private bool _visibleTitlebar = true;

		// Token: 0x04002D69 RID: 11625
		private bool _reloadOnShow;

		// Token: 0x04002D6A RID: 11626
		private readonly DialogParameters _parameters;
	}
}
