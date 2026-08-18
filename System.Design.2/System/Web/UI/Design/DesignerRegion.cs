using System;
using System.Drawing;

namespace System.Web.UI.Design
{
	// Token: 0x02000034 RID: 52
	public class DesignerRegion : DesignerObject
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x0000CCE3 File Offset: 0x0000AEE3
		public DesignerRegion(ControlDesigner designer, string name) : this(designer, name, false)
		{
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000CCEE File Offset: 0x0000AEEE
		public DesignerRegion(ControlDesigner designer, string name, bool selectable) : base(designer, name)
		{
			this._selectable = selectable;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000CCFF File Offset: 0x0000AEFF
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x0000CD15 File Offset: 0x0000AF15
		public virtual string Description
		{
			get
			{
				if (this._description == null)
				{
					return string.Empty;
				}
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000CD1E File Offset: 0x0000AF1E
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x0000CD34 File Offset: 0x0000AF34
		public virtual string DisplayName
		{
			get
			{
				if (this._displayName == null)
				{
					return string.Empty;
				}
				return this._displayName;
			}
			set
			{
				this._displayName = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000CD3D File Offset: 0x0000AF3D
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x0000CD45 File Offset: 0x0000AF45
		public bool EnsureSize
		{
			get
			{
				return this._ensureSize;
			}
			set
			{
				this._ensureSize = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000CD4E File Offset: 0x0000AF4E
		// (set) Token: 0x060001BA RID: 442 RVA: 0x0000CD56 File Offset: 0x0000AF56
		public virtual bool Highlight
		{
			get
			{
				return this._highlight;
			}
			set
			{
				this._highlight = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000CD5F File Offset: 0x0000AF5F
		// (set) Token: 0x060001BC RID: 444 RVA: 0x0000CD67 File Offset: 0x0000AF67
		public virtual bool Selectable
		{
			get
			{
				return this._selectable;
			}
			set
			{
				this._selectable = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000CD70 File Offset: 0x0000AF70
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000CD78 File Offset: 0x0000AF78
		public virtual bool Selected
		{
			get
			{
				return this._selected;
			}
			set
			{
				this._selected = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000CD81 File Offset: 0x0000AF81
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x0000CD89 File Offset: 0x0000AF89
		public object UserData
		{
			get
			{
				return this._userData;
			}
			set
			{
				this._userData = value;
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public Rectangle GetBounds()
		{
			return base.Designer.View.GetBounds(this);
		}

		// Token: 0x04000123 RID: 291
		public static readonly string DesignerRegionAttributeName = "_designerRegion";

		// Token: 0x04000124 RID: 292
		private string _displayName;

		// Token: 0x04000125 RID: 293
		private string _description;

		// Token: 0x04000126 RID: 294
		private object _userData;

		// Token: 0x04000127 RID: 295
		private bool _selectable;

		// Token: 0x04000128 RID: 296
		private bool _selected;

		// Token: 0x04000129 RID: 297
		private bool _highlight;

		// Token: 0x0400012A RID: 298
		private bool _ensureSize;
	}
}
