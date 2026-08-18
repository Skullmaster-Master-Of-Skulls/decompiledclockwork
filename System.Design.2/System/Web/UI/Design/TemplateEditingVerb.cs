using System;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x02000070 RID: 112
	[Obsolete("Use of this type is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class TemplateEditingVerb : DesignerVerb, IDisposable
	{
		// Token: 0x0600037C RID: 892 RVA: 0x00011CBD File Offset: 0x0000FEBD
		public TemplateEditingVerb(string text, int index, TemplatedControlDesigner designer) : this(text, index, designer.TemplateEditingVerbHandler)
		{
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00011CCD File Offset: 0x0000FECD
		public TemplateEditingVerb(string text, int index) : this(text, index, TemplateEditingVerb.dummyEventHandler)
		{
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00011CDC File Offset: 0x0000FEDC
		private TemplateEditingVerb(string text, int index, EventHandler handler) : base(text, handler)
		{
			this.index = index;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00011CED File Offset: 0x0000FEED
		// (set) Token: 0x06000380 RID: 896 RVA: 0x00011CF5 File Offset: 0x0000FEF5
		internal ITemplateEditingFrame EditingFrame
		{
			get
			{
				return this.editingFrame;
			}
			set
			{
				this.editingFrame = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00011CFE File Offset: 0x0000FEFE
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00011D06 File Offset: 0x0000FF06
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00011D18 File Offset: 0x0000FF18
		~TemplateEditingVerb()
		{
			this.Dispose(false);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00011D48 File Offset: 0x0000FF48
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.editingFrame != null)
			{
				this.editingFrame.Dispose();
				this.editingFrame = null;
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00003937 File Offset: 0x00001B37
		private static void OnDummyEventHandler(object sender, EventArgs e)
		{
		}

		// Token: 0x0400018C RID: 396
		private static readonly EventHandler dummyEventHandler = new EventHandler(TemplateEditingVerb.OnDummyEventHandler);

		// Token: 0x0400018D RID: 397
		private ITemplateEditingFrame editingFrame;

		// Token: 0x0400018E RID: 398
		private int index;
	}
}
