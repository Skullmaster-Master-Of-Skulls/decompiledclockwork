using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design
{
	// Token: 0x0200006B RID: 107
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public abstract class TemplatedControlDesigner : ControlDesigner
	{
		// Token: 0x0600031F RID: 799 RVA: 0x00010876 File Offset: 0x0000EA76
		public TemplatedControlDesigner()
		{
			this.enableTemplateEditing = true;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00010885 File Offset: 0x0000EA85
		[Obsolete("Use of this property is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		public ITemplateEditingFrame ActiveTemplateEditingFrame
		{
			get
			{
				if (this._currentTemplateGroup != null)
				{
					return this._currentTemplateGroup.Frame;
				}
				return null;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0001089C File Offset: 0x0000EA9C
		public bool CanEnterTemplateMode
		{
			get
			{
				return this.enableTemplateEditing;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000322 RID: 802 RVA: 0x000108A4 File Offset: 0x0000EAA4
		protected override bool DataBindingsEnabled
		{
			get
			{
				return (!this.InTemplateModeInternal || !this.HidePropertiesInTemplateMode) && base.DataBindingsEnabled;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000323 RID: 803 RVA: 0x000108BE File Offset: 0x0000EABE
		[Obsolete("The recommended alternative is System.Web.UI.Design.ControlDesigner.InTemplateMode. http://go.microsoft.com/fwlink/?linkid=14202")]
		public new bool InTemplateMode
		{
			get
			{
				return this._currentTemplateGroup != null;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000324 RID: 804 RVA: 0x000108C9 File Offset: 0x0000EAC9
		internal bool InTemplateModeInternal
		{
			get
			{
				return this.InTemplateMode;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000325 RID: 805 RVA: 0x000108D1 File Offset: 0x0000EAD1
		internal EventHandler TemplateEditingVerbHandler
		{
			get
			{
				return new EventHandler(this.OnTemplateEditingVerbInvoked);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000326 RID: 806 RVA: 0x000108E0 File Offset: 0x0000EAE0
		public override TemplateGroupCollection TemplateGroups
		{
			get
			{
				TemplateGroupCollection templateGroups = base.TemplateGroups;
				this.TemplateGroupTable.Clear();
				TemplatedControlDesigner.TemplateEditingVerbCollection templateEditingVerbsInternal = this.GetTemplateEditingVerbsInternal();
				foreach (object obj in ((IEnumerable)templateEditingVerbsInternal))
				{
					TemplateEditingVerb templateEditingVerb = (TemplateEditingVerb)obj;
					if (templateEditingVerb.Enabled && templateEditingVerb.Visible)
					{
						ITemplateEditingFrame templateEditingFrame = this.CreateTemplateEditingFrame(templateEditingVerb);
						templateEditingFrame.Verb = templateEditingVerb;
						TemplateGroup templateGroup = new TemplatedControlDesigner.TemplatedControlDesignerTemplateGroup(templateEditingVerb, templateEditingFrame);
						bool flag = templateEditingFrame.TemplateStyles != null;
						for (int i = 0; i < templateEditingFrame.TemplateNames.Length; i++)
						{
							Style style = flag ? templateEditingFrame.TemplateStyles[i] : null;
							templateGroup.AddTemplateDefinition(new TemplatedControlDesigner.TemplatedControlDesignerTemplateDefinition(templateEditingFrame.TemplateNames[i], style, this, templateEditingFrame)
							{
								SupportsDataBinding = true
							});
						}
						templateGroups.Add(templateGroup);
						this.TemplateGroupTable[templateEditingFrame] = templateGroup;
					}
				}
				return templateGroups;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000327 RID: 807 RVA: 0x000109F8 File Offset: 0x0000EBF8
		private IDictionary TemplateGroupTable
		{
			get
			{
				if (this._templateGroupTable == null)
				{
					this._templateGroupTable = new HybridDictionary();
				}
				return this._templateGroupTable;
			}
		}

		// Token: 0x06000328 RID: 808
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		protected abstract ITemplateEditingFrame CreateTemplateEditingFrame(TemplateEditingVerb verb);

		// Token: 0x06000329 RID: 809 RVA: 0x00010A13 File Offset: 0x0000EC13
		private void EnableTemplateEditing(bool enable)
		{
			this.enableTemplateEditing = enable;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00010A1C File Offset: 0x0000EC1C
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		public void EnterTemplateMode(ITemplateEditingFrame newTemplateEditingFrame)
		{
			if (this.ActiveTemplateEditingFrame == newTemplateEditingFrame)
			{
				return;
			}
			if (this.BehaviorInternal != null)
			{
				IControlDesignerBehavior controlDesignerBehavior = (IControlDesignerBehavior)this.BehaviorInternal;
				try
				{
					bool flag = false;
					if (this.InTemplateModeInternal)
					{
						flag = true;
						this.ExitTemplateModeInternal(flag, false, true);
					}
					else if (controlDesignerBehavior != null)
					{
						controlDesignerBehavior.DesignTimeHtml = string.Empty;
					}
					this._currentTemplateGroup = (TemplatedControlDesigner.TemplatedControlDesignerTemplateGroup)this.TemplateGroupTable[newTemplateEditingFrame];
					if (this._currentTemplateGroup == null)
					{
						this._currentTemplateGroup = new TemplatedControlDesigner.TemplatedControlDesignerTemplateGroup(null, newTemplateEditingFrame);
					}
					if (!flag)
					{
						this.RaiseTemplateModeChanged();
					}
					this.ActiveTemplateEditingFrame.Open();
					base.IsDirtyInternal = true;
					TypeDescriptor.Refresh(base.Component);
				}
				catch
				{
				}
				IWebFormsDocumentService webFormsDocumentService = (IWebFormsDocumentService)this.GetService(typeof(IWebFormsDocumentService));
				if (webFormsDocumentService != null)
				{
					webFormsDocumentService.UpdateSelection();
				}
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00010AF8 File Offset: 0x0000ECF8
		private void EnterTemplateModeInternal(ITemplateEditingFrame newTemplateEditingFrame)
		{
			this.EnterTemplateMode(newTemplateEditingFrame);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00010B04 File Offset: 0x0000ED04
		private void ExitNestedTemplates(bool fSave)
		{
			try
			{
				IComponent viewControl = base.ViewControl;
				IDesignerHost designerHost = (IDesignerHost)viewControl.Site.GetService(typeof(IDesignerHost));
				ControlCollection controls = ((Control)viewControl).Controls;
				for (int i = 0; i < controls.Count; i++)
				{
					IDesigner designer = designerHost.GetDesigner(controls[i]);
					if (designer is TemplatedControlDesigner)
					{
						TemplatedControlDesigner templatedControlDesigner = (TemplatedControlDesigner)designer;
						if (templatedControlDesigner.InTemplateModeInternal)
						{
							templatedControlDesigner.ExitTemplateModeInternal(false, true, fSave);
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00010B9C File Offset: 0x0000ED9C
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		public void ExitTemplateMode(bool fSwitchingTemplates, bool fNested, bool fSave)
		{
			try
			{
				this.ExitNestedTemplates(fSave);
				this.ActiveTemplateEditingFrame.Close(fSave);
				if (!fSwitchingTemplates)
				{
					this._currentTemplateGroup = null;
					this.RaiseTemplateModeChanged();
					if (!fNested)
					{
						this.UpdateDesignTimeHtml();
						TypeDescriptor.Refresh(base.Component);
						IWebFormsDocumentService webFormsDocumentService = (IWebFormsDocumentService)this.GetService(typeof(IWebFormsDocumentService));
						if (webFormsDocumentService != null)
						{
							webFormsDocumentService.UpdateSelection();
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00010C14 File Offset: 0x0000EE14
		private void ExitTemplateModeInternal(bool fSwitchingTemplates, bool fNested, bool fSave)
		{
			this.ExitTemplateMode(fSwitchingTemplates, fNested, fSave);
		}

		// Token: 0x0600032F RID: 815
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		protected abstract TemplateEditingVerb[] GetCachedTemplateEditingVerbs();

		// Token: 0x06000330 RID: 816 RVA: 0x00010C20 File Offset: 0x0000EE20
		internal override string GetPersistInnerHtmlInternal()
		{
			if (this.InTemplateModeInternal)
			{
				this.SaveActiveTemplateEditingFrame();
			}
			string persistInnerHtmlInternal = base.GetPersistInnerHtmlInternal();
			if (this.InTemplateModeInternal)
			{
				base.IsDirtyInternal = true;
			}
			return persistInnerHtmlInternal;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00003930 File Offset: 0x00001B30
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		public virtual string GetTemplateContainerDataItemProperty(string templateName)
		{
			return string.Empty;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00003598 File Offset: 0x00001798
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		public virtual IEnumerable GetTemplateContainerDataSource(string templateName)
		{
			return null;
		}

		// Token: 0x06000333 RID: 819
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		public abstract string GetTemplateContent(ITemplateEditingFrame editingFrame, string templateName, out bool allowEditing);

		// Token: 0x06000334 RID: 820 RVA: 0x00010C54 File Offset: 0x0000EE54
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		public TemplateEditingVerb[] GetTemplateEditingVerbs()
		{
			if ((ITemplateEditingService)this.GetService(typeof(ITemplateEditingService)) == null)
			{
				return null;
			}
			TemplatedControlDesigner.TemplateEditingVerbCollection templateEditingVerbsInternal = this.GetTemplateEditingVerbsInternal();
			TemplateEditingVerb[] array = new TemplateEditingVerb[templateEditingVerbsInternal.Count];
			((ICollection)templateEditingVerbsInternal).CopyTo(array, 0);
			return array;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00010C98 File Offset: 0x0000EE98
		private TemplatedControlDesigner.TemplateEditingVerbCollection GetTemplateEditingVerbsInternal()
		{
			TemplatedControlDesigner.TemplateEditingVerbCollection templateEditingVerbCollection = new TemplatedControlDesigner.TemplateEditingVerbCollection();
			TemplateEditingVerb[] cachedTemplateEditingVerbs = this.GetCachedTemplateEditingVerbs();
			if (cachedTemplateEditingVerbs != null && cachedTemplateEditingVerbs.Length != 0)
			{
				for (int i = 0; i < cachedTemplateEditingVerbs.Length; i++)
				{
					if (this._currentTemplateGroup != null && this._currentTemplateGroup.Verb == cachedTemplateEditingVerbs[i])
					{
						cachedTemplateEditingVerbs[i].Checked = true;
					}
					else
					{
						cachedTemplateEditingVerbs[i].Checked = false;
					}
					templateEditingVerbCollection.Add(cachedTemplateEditingVerbs[i]);
				}
			}
			return templateEditingVerbCollection;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00010CFE File Offset: 0x0000EEFE
		protected ITemplate GetTemplateFromText(string text)
		{
			return this.GetTemplateFromText(text, null);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00010D08 File Offset: 0x0000EF08
		internal ITemplate GetTemplateFromText(string text, ITemplate currentTemplate)
		{
			if (text == null || text.Length == 0)
			{
				throw new ArgumentNullException("text");
			}
			IDesignerHost designerHost = (IDesignerHost)base.Component.Site.GetService(typeof(IDesignerHost));
			try
			{
				ITemplate template = ControlParser.ParseTemplate(designerHost, text);
				if (template != null)
				{
					return template;
				}
			}
			catch
			{
			}
			return currentTemplate;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00010D74 File Offset: 0x0000EF74
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		public virtual Type GetTemplatePropertyParentType(string templateName)
		{
			return base.Component.GetType();
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00010D81 File Offset: 0x0000EF81
		protected string GetTextFromTemplate(ITemplate template)
		{
			if (template == null)
			{
				throw new ArgumentNullException("template");
			}
			if (template is TemplateBuilder)
			{
				return ((TemplateBuilder)template).Text;
			}
			return string.Empty;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00010DAA File Offset: 0x0000EFAA
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			if (base.View != null)
			{
				base.View.ViewEvent += this.OnViewEvent;
				base.View.SetFlags(ViewFlags.TemplateEditing, true);
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00010DDF File Offset: 0x0000EFDF
		[Obsolete("The recommended alternative is ControlDesigner.Tag. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected override void OnBehaviorAttached()
		{
			if (this.InTemplateModeInternal)
			{
				this.ActiveTemplateEditingFrame.Close(false);
				this.ActiveTemplateEditingFrame.Dispose();
				this._currentTemplateGroup = null;
				TypeDescriptor.Refresh(base.Component);
			}
			base.OnBehaviorAttached();
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00010E18 File Offset: 0x0000F018
		public override void OnComponentChanged(object sender, ComponentChangedEventArgs ce)
		{
			base.OnComponentChanged(sender, ce);
			if (this.InTemplateModeInternal && ce.Member != null && ce.NewValue != null && ce.Member.Name.Equals("ID"))
			{
				this.ActiveTemplateEditingFrame.UpdateControlName(ce.NewValue.ToString());
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00010E74 File Offset: 0x0000F074
		public override void OnSetParent()
		{
			Control control = (Control)base.Component;
			bool enable = false;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			ITemplateEditingService templateEditingService = (ITemplateEditingService)designerHost.GetService(typeof(ITemplateEditingService));
			if (templateEditingService != null)
			{
				enable = true;
				Control parent = control.Parent;
				Control page = control.Page;
				while (parent != null && parent != page)
				{
					IDesigner designer = designerHost.GetDesigner(parent);
					TemplatedControlDesigner templatedControlDesigner = designer as TemplatedControlDesigner;
					if (templatedControlDesigner != null)
					{
						enable = templateEditingService.SupportsNestedTemplateEditing;
						break;
					}
					parent = parent.Parent;
				}
			}
			this.EnableTemplateEditing(enable);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00010F0C File Offset: 0x0000F10C
		private void OnTemplateEditingVerbInvoked(object sender, EventArgs e)
		{
			TemplateEditingVerb templateEditingVerb = (TemplateEditingVerb)sender;
			if (templateEditingVerb.EditingFrame == null)
			{
				templateEditingVerb.EditingFrame = this.CreateTemplateEditingFrame(templateEditingVerb);
			}
			if (templateEditingVerb.EditingFrame != null)
			{
				templateEditingVerb.EditingFrame.Verb = templateEditingVerb;
				this.EnterTemplateModeInternal(templateEditingVerb.EditingFrame);
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void OnTemplateModeChanged()
		{
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00010F58 File Offset: 0x0000F158
		internal void OnTemplateModeChangedInternal(TemplateModeChangedEventArgs e)
		{
			TemplateGroup newTemplateGroup = e.NewTemplateGroup;
			if (newTemplateGroup != null)
			{
				if (this._currentTemplateGroup != newTemplateGroup)
				{
					this.EnterTemplateModeInternal(((TemplatedControlDesigner.TemplatedControlDesignerTemplateGroup)newTemplateGroup).Frame);
					return;
				}
			}
			else
			{
				this.ExitTemplateModeInternal(false, false, true);
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00010F93 File Offset: 0x0000F193
		private void OnViewEvent(object sender, ViewEventArgs e)
		{
			if (e.EventType == ViewEvent.TemplateModeChanged)
			{
				this.OnTemplateModeChangedInternal((TemplateModeChangedEventArgs)e.EventArgs);
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00010FB3 File Offset: 0x0000F1B3
		private void RaiseTemplateModeChanged()
		{
			if (this.BehaviorInternal != null)
			{
				((IControlDesignerBehavior)this.BehaviorInternal).OnTemplateModeChanged();
			}
			this.OnTemplateModeChanged();
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00010FD3 File Offset: 0x0000F1D3
		protected void SaveActiveTemplateEditingFrame()
		{
			this.ActiveTemplateEditingFrame.Save();
		}

		// Token: 0x06000344 RID: 836
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		public abstract void SetTemplateContent(ITemplateEditingFrame editingFrame, string templateName, string templateContent);

		// Token: 0x06000345 RID: 837 RVA: 0x00010FE0 File Offset: 0x0000F1E0
		public override void UpdateDesignTimeHtml()
		{
			if (!this.InTemplateModeInternal)
			{
				base.UpdateDesignTimeHtml();
			}
		}

		// Token: 0x0400016B RID: 363
		private bool enableTemplateEditing;

		// Token: 0x0400016C RID: 364
		private TemplatedControlDesigner.TemplatedControlDesignerTemplateGroup _currentTemplateGroup;

		// Token: 0x0400016D RID: 365
		private IDictionary _templateGroupTable;

		// Token: 0x020003B6 RID: 950
		private class TemplatedControlDesignerTemplateDefinition : TemplateDefinition
		{
			// Token: 0x06002616 RID: 9750 RVA: 0x000EC808 File Offset: 0x000EAA08
			public TemplatedControlDesignerTemplateDefinition(string name, Style style, TemplatedControlDesigner parent, ITemplateEditingFrame frame) : base(parent, name, parent.Component, name, style)
			{
				this._parent = parent;
				this._frame = frame;
				base.Properties[typeof(Control)] = (Control)this._parent.Component;
			}

			// Token: 0x17000805 RID: 2053
			// (get) Token: 0x06002617 RID: 9751 RVA: 0x000EC85C File Offset: 0x000EAA5C
			public override bool AllowEditing
			{
				get
				{
					bool result;
					this._parent.GetTemplateContent(this._frame, base.Name, out result);
					return result;
				}
			}

			// Token: 0x17000806 RID: 2054
			// (get) Token: 0x06002618 RID: 9752 RVA: 0x000EC884 File Offset: 0x000EAA84
			// (set) Token: 0x06002619 RID: 9753 RVA: 0x000EC8AA File Offset: 0x000EAAAA
			public override string Content
			{
				get
				{
					bool flag;
					return this._parent.GetTemplateContent(this._frame, base.Name, out flag);
				}
				set
				{
					this._parent.SetTemplateContent(this._frame, base.Name, value);
					this._parent.Tag.SetDirty(true);
					this._parent.UpdateDesignTimeHtml();
				}
			}

			// Token: 0x04001BB3 RID: 7091
			private TemplatedControlDesigner _parent;

			// Token: 0x04001BB4 RID: 7092
			private ITemplateEditingFrame _frame;
		}

		// Token: 0x020003B7 RID: 951
		private class TemplatedControlDesignerTemplateGroup : TemplateGroup
		{
			// Token: 0x0600261A RID: 9754 RVA: 0x000EC8E0 File Offset: 0x000EAAE0
			public TemplatedControlDesignerTemplateGroup(TemplateEditingVerb verb, ITemplateEditingFrame frame) : base(verb.Text, frame.ControlStyle)
			{
				this._frame = frame;
				this._verb = verb;
			}

			// Token: 0x17000807 RID: 2055
			// (get) Token: 0x0600261B RID: 9755 RVA: 0x000EC902 File Offset: 0x000EAB02
			public ITemplateEditingFrame Frame
			{
				get
				{
					return this._frame;
				}
			}

			// Token: 0x17000808 RID: 2056
			// (get) Token: 0x0600261C RID: 9756 RVA: 0x000EC90A File Offset: 0x000EAB0A
			public TemplateEditingVerb Verb
			{
				get
				{
					return this._verb;
				}
			}

			// Token: 0x04001BB5 RID: 7093
			private ITemplateEditingFrame _frame;

			// Token: 0x04001BB6 RID: 7094
			private TemplateEditingVerb _verb;
		}

		// Token: 0x020003B8 RID: 952
		private class TemplateEditingVerbCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x0600261D RID: 9757 RVA: 0x0000362F File Offset: 0x0000182F
			public TemplateEditingVerbCollection()
			{
			}

			// Token: 0x0600261E RID: 9758 RVA: 0x000EC914 File Offset: 0x000EAB14
			internal TemplateEditingVerbCollection(TemplateEditingVerb[] verbs)
			{
				for (int i = 0; i < verbs.Length; i++)
				{
					this.Add(verbs[i]);
				}
			}

			// Token: 0x17000809 RID: 2057
			// (get) Token: 0x0600261F RID: 9759 RVA: 0x000EC93F File Offset: 0x000EAB3F
			public int Count
			{
				get
				{
					return this.InternalList.Count;
				}
			}

			// Token: 0x1700080A RID: 2058
			// (get) Token: 0x06002620 RID: 9760 RVA: 0x000EC94C File Offset: 0x000EAB4C
			private ArrayList InternalList
			{
				get
				{
					if (this._list == null)
					{
						this._list = new ArrayList();
					}
					return this._list;
				}
			}

			// Token: 0x1700080B RID: 2059
			public TemplateEditingVerb this[int index]
			{
				get
				{
					return (TemplateEditingVerb)this.InternalList[index];
				}
				set
				{
					this.InternalList[index] = value;
				}
			}

			// Token: 0x06002623 RID: 9763 RVA: 0x000EC989 File Offset: 0x000EAB89
			public int Add(TemplateEditingVerb verb)
			{
				return this.InternalList.Add(verb);
			}

			// Token: 0x06002624 RID: 9764 RVA: 0x000EC997 File Offset: 0x000EAB97
			public void Clear()
			{
				this.InternalList.Clear();
			}

			// Token: 0x06002625 RID: 9765 RVA: 0x000EC9A4 File Offset: 0x000EABA4
			public bool Contains(TemplateEditingVerb verb)
			{
				return this.InternalList.Contains(verb);
			}

			// Token: 0x06002626 RID: 9766 RVA: 0x000EC9B2 File Offset: 0x000EABB2
			public int IndexOf(TemplateEditingVerb verb)
			{
				return this.InternalList.IndexOf(verb);
			}

			// Token: 0x06002627 RID: 9767 RVA: 0x000EC9C0 File Offset: 0x000EABC0
			public void Insert(int index, TemplateEditingVerb verb)
			{
				this.InternalList.Insert(index, verb);
			}

			// Token: 0x06002628 RID: 9768 RVA: 0x000EC9CF File Offset: 0x000EABCF
			public void Remove(TemplateEditingVerb verb)
			{
				this.InternalList.Remove(verb);
			}

			// Token: 0x06002629 RID: 9769 RVA: 0x000EC9DD File Offset: 0x000EABDD
			public void RemoveAt(int index)
			{
				this.InternalList.RemoveAt(index);
			}

			// Token: 0x1700080C RID: 2060
			// (get) Token: 0x0600262A RID: 9770 RVA: 0x000EC9EB File Offset: 0x000EABEB
			int ICollection.Count
			{
				get
				{
					return this.Count;
				}
			}

			// Token: 0x1700080D RID: 2061
			// (get) Token: 0x0600262B RID: 9771 RVA: 0x000EC9F3 File Offset: 0x000EABF3
			bool IList.IsFixedSize
			{
				get
				{
					return this.InternalList.IsFixedSize;
				}
			}

			// Token: 0x1700080E RID: 2062
			// (get) Token: 0x0600262C RID: 9772 RVA: 0x000ECA00 File Offset: 0x000EAC00
			bool IList.IsReadOnly
			{
				get
				{
					return this.InternalList.IsReadOnly;
				}
			}

			// Token: 0x1700080F RID: 2063
			// (get) Token: 0x0600262D RID: 9773 RVA: 0x000ECA0D File Offset: 0x000EAC0D
			bool ICollection.IsSynchronized
			{
				get
				{
					return this.InternalList.IsSynchronized;
				}
			}

			// Token: 0x17000810 RID: 2064
			// (get) Token: 0x0600262E RID: 9774 RVA: 0x000ECA1A File Offset: 0x000EAC1A
			object ICollection.SyncRoot
			{
				get
				{
					return this.InternalList.SyncRoot;
				}
			}

			// Token: 0x17000811 RID: 2065
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					if (!(value is TemplateEditingVerb))
					{
						throw new ArgumentException();
					}
					this[index] = (TemplateEditingVerb)value;
				}
			}

			// Token: 0x06002631 RID: 9777 RVA: 0x000ECA4D File Offset: 0x000EAC4D
			int IList.Add(object o)
			{
				if (!(o is TemplateEditingVerb))
				{
					throw new ArgumentException();
				}
				return this.Add((TemplateEditingVerb)o);
			}

			// Token: 0x06002632 RID: 9778 RVA: 0x000ECA69 File Offset: 0x000EAC69
			void IList.Clear()
			{
				this.Clear();
			}

			// Token: 0x06002633 RID: 9779 RVA: 0x000ECA71 File Offset: 0x000EAC71
			bool IList.Contains(object o)
			{
				if (!(o is TemplateEditingVerb))
				{
					throw new ArgumentException();
				}
				return this.Contains((TemplateEditingVerb)o);
			}

			// Token: 0x06002634 RID: 9780 RVA: 0x000ECA8D File Offset: 0x000EAC8D
			void ICollection.CopyTo(Array array, int index)
			{
				this.InternalList.CopyTo(array, index);
			}

			// Token: 0x06002635 RID: 9781 RVA: 0x000ECA9C File Offset: 0x000EAC9C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.InternalList.GetEnumerator();
			}

			// Token: 0x06002636 RID: 9782 RVA: 0x000ECAA9 File Offset: 0x000EACA9
			int IList.IndexOf(object o)
			{
				if (!(o is TemplateEditingVerb))
				{
					throw new ArgumentException();
				}
				return this.IndexOf((TemplateEditingVerb)o);
			}

			// Token: 0x06002637 RID: 9783 RVA: 0x000ECAC5 File Offset: 0x000EACC5
			void IList.Insert(int index, object o)
			{
				if (!(o is TemplateEditingVerb))
				{
					throw new ArgumentException();
				}
				this.Insert(index, (TemplateEditingVerb)o);
			}

			// Token: 0x06002638 RID: 9784 RVA: 0x000ECAE2 File Offset: 0x000EACE2
			void IList.Remove(object o)
			{
				if (!(o is TemplateEditingVerb))
				{
					throw new ArgumentException();
				}
				this.Remove((TemplateEditingVerb)o);
			}

			// Token: 0x06002639 RID: 9785 RVA: 0x000ECAFE File Offset: 0x000EACFE
			void IList.RemoveAt(int index)
			{
				this.RemoveAt(index);
			}

			// Token: 0x04001BB7 RID: 7095
			private ArrayList _list;
		}
	}
}
