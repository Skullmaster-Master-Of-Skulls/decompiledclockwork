using System;
using System.Collections;
using Telerik.Web.Apoc.Fo;

namespace Telerik.Web.Apoc.Extensions
{
	// Token: 0x02001399 RID: 5017
	internal class Outline : ExtensionObj
	{
		// Token: 0x0600D0F5 RID: 53493 RVA: 0x002E3FDB File Offset: 0x002E21DB
		public new static FObj.Maker GetMaker()
		{
			return new Outline.Maker();
		}

		// Token: 0x0600D0F6 RID: 53494 RVA: 0x002E3FE4 File Offset: 0x002E21E4
		public Outline(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this._internalDestination = this.properties.GetProperty("internal-destination").GetString();
			this._externalDestination = this.properties.GetProperty("external-destination").GetString();
			string.IsNullOrEmpty(this._externalDestination);
			if (string.IsNullOrEmpty(this._internalDestination))
			{
				ApocDriver.ActiveDriver.FireApocWarning("fox:outline requires an internal-destination.");
			}
			for (FONode parent2 = base.getParent(); parent2 != null; parent2 = parent2.getParent())
			{
				Outline outline = parent2 as Outline;
				if (outline != null)
				{
					this._parentOutline = outline;
					return;
				}
			}
		}

		// Token: 0x0600D0F7 RID: 53495 RVA: 0x002E4088 File Offset: 0x002E2288
		protected internal override void AddChild(FONode obj)
		{
			Label label = obj as Label;
			if (label != null)
			{
				this._label = label;
			}
			else if (obj is Outline)
			{
				this._outlines.Add(obj);
			}
			base.AddChild(obj);
		}

		// Token: 0x170042DA RID: 17114
		// (get) Token: 0x0600D0F8 RID: 53496 RVA: 0x002E40C4 File Offset: 0x002E22C4
		// (set) Token: 0x0600D0F9 RID: 53497 RVA: 0x002E40CC File Offset: 0x002E22CC
		public object RendererObject
		{
			get
			{
				return this._rendererObject;
			}
			set
			{
				this._rendererObject = value;
			}
		}

		// Token: 0x170042DB RID: 17115
		// (get) Token: 0x0600D0FA RID: 53498 RVA: 0x002E40D5 File Offset: 0x002E22D5
		public Outline ParentOutline
		{
			get
			{
				return this._parentOutline;
			}
		}

		// Token: 0x170042DC RID: 17116
		// (get) Token: 0x0600D0FB RID: 53499 RVA: 0x002E40DD File Offset: 0x002E22DD
		public Label Label
		{
			get
			{
				if (this._label != null)
				{
					return this._label;
				}
				return new Label(this, this.properties);
			}
		}

		// Token: 0x170042DD RID: 17117
		// (get) Token: 0x0600D0FC RID: 53500 RVA: 0x002E40FA File Offset: 0x002E22FA
		public ArrayList Outlines
		{
			get
			{
				return this._outlines;
			}
		}

		// Token: 0x170042DE RID: 17118
		// (get) Token: 0x0600D0FD RID: 53501 RVA: 0x002E4102 File Offset: 0x002E2302
		public string InternalDestination
		{
			get
			{
				return this._internalDestination;
			}
		}

		// Token: 0x04003815 RID: 14357
		private Label _label;

		// Token: 0x04003816 RID: 14358
		private ArrayList _outlines = new ArrayList();

		// Token: 0x04003817 RID: 14359
		private string _internalDestination;

		// Token: 0x04003818 RID: 14360
		private string _externalDestination;

		// Token: 0x04003819 RID: 14361
		private Outline _parentOutline;

		// Token: 0x0400381A RID: 14362
		private object _rendererObject;

		// Token: 0x0200139A RID: 5018
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D0FE RID: 53502 RVA: 0x002E410A File Offset: 0x002E230A
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new Outline(parent, propertyList);
			}
		}
	}
}
