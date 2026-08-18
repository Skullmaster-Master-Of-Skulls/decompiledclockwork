using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017B4 RID: 6068
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class Position : StateManagedObject, ICloneable
	{
		// Token: 0x1700477A RID: 18298
		// (get) Token: 0x0600EC39 RID: 60473 RVA: 0x0035E18B File Offset: 0x0035C38B
		internal bool IsTop
		{
			get
			{
				return this.Auto && (this.AlignedPosition == AlignedPositions.Top || this.AlignedPosition == AlignedPositions.TopLeft || this.AlignedPosition == AlignedPositions.TopRight || this.AlignedPosition == AlignedPositions.None);
			}
		}

		// Token: 0x1700477B RID: 18299
		// (get) Token: 0x0600EC3A RID: 60474 RVA: 0x0035E1BD File Offset: 0x0035C3BD
		internal bool IsBottom
		{
			get
			{
				return this.Auto && (this.AlignedPosition == AlignedPositions.Bottom || this.AlignedPosition == AlignedPositions.BottomLeft || this.AlignedPosition == AlignedPositions.BottomRight);
			}
		}

		// Token: 0x1700477C RID: 18300
		// (get) Token: 0x0600EC3B RID: 60475 RVA: 0x0035E1F2 File Offset: 0x0035C3F2
		internal bool IsLeft
		{
			get
			{
				return this.Auto && (this.AlignedPosition == AlignedPositions.Left || this.AlignedPosition == AlignedPositions.BottomLeft || this.AlignedPosition == AlignedPositions.TopLeft);
			}
		}

		// Token: 0x1700477D RID: 18301
		// (get) Token: 0x0600EC3C RID: 60476 RVA: 0x0035E220 File Offset: 0x0035C420
		internal bool IsRight
		{
			get
			{
				return this.Auto && (this.AlignedPosition == AlignedPositions.Right || this.AlignedPosition == AlignedPositions.BottomRight || this.AlignedPosition == AlignedPositions.TopRight);
			}
		}

		// Token: 0x1700477E RID: 18302
		// (get) Token: 0x0600EC3D RID: 60477 RVA: 0x0035E24E File Offset: 0x0035C44E
		internal bool IsNone
		{
			get
			{
				return this.AlignedPosition == AlignedPositions.None;
			}
		}

		// Token: 0x1700477F RID: 18303
		// (get) Token: 0x0600EC3E RID: 60478 RVA: 0x0035E259 File Offset: 0x0035C459
		// (set) Token: 0x0600EC3F RID: 60479 RVA: 0x0035E26B File Offset: 0x0035C46B
		internal Position Copy
		{
			get
			{
				if (this.positionCopy == null)
				{
					return this;
				}
				return this.positionCopy;
			}
			set
			{
				this.positionCopy = (Position)value.Clone();
			}
		}

		// Token: 0x17004780 RID: 18304
		// (get) Token: 0x0600EC40 RID: 60480 RVA: 0x0035E27E File Offset: 0x0035C47E
		// (set) Token: 0x0600EC41 RID: 60481 RVA: 0x0035E29F File Offset: 0x0035C49F
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public virtual bool Auto
		{
			get
			{
				return (bool)(base.ViewState["Auto"] ?? true);
			}
			set
			{
				base.ViewState["Auto"] = value;
			}
		}

		// Token: 0x17004781 RID: 18305
		// (get) Token: 0x0600EC42 RID: 60482 RVA: 0x0035E2B7 File Offset: 0x0035C4B7
		// (set) Token: 0x0600EC43 RID: 60483 RVA: 0x0035E2D8 File Offset: 0x0035C4D8
		[DefaultValue(typeof(AlignedPositions), "None")]
		[NotifyParentProperty(true)]
		[Editor("System.Drawing.Design.ContentAlignmentEditor, System.Drawing.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SkinnableProperty]
		public virtual AlignedPositions AlignedPosition
		{
			get
			{
				return (AlignedPositions)(base.ViewState["AlignedPosition"] ?? AlignedPositions.None);
			}
			set
			{
				base.ViewState["AlignedPosition"] = value;
			}
		}

		// Token: 0x17004782 RID: 18306
		// (get) Token: 0x0600EC44 RID: 60484 RVA: 0x0035E2F0 File Offset: 0x0035C4F0
		// (set) Token: 0x0600EC45 RID: 60485 RVA: 0x0035E315 File Offset: 0x0035C515
		[NotifyParentProperty(true)]
		public virtual float X
		{
			get
			{
				return (float)(base.ViewState["X"] ?? 0f);
			}
			set
			{
				base.ViewState["X"] = value;
				this.ResetGlobal();
			}
		}

		// Token: 0x0600EC46 RID: 60486 RVA: 0x0035E333 File Offset: 0x0035C533
		protected virtual bool ShouldSerializeX()
		{
			return !this.Auto;
		}

		// Token: 0x0600EC47 RID: 60487 RVA: 0x0035E33E File Offset: 0x0035C53E
		protected virtual void ResetX()
		{
			this.X = 0f;
		}

		// Token: 0x17004783 RID: 18307
		// (get) Token: 0x0600EC48 RID: 60488 RVA: 0x0035E34B File Offset: 0x0035C54B
		// (set) Token: 0x0600EC49 RID: 60489 RVA: 0x0035E370 File Offset: 0x0035C570
		[NotifyParentProperty(true)]
		public virtual float Y
		{
			get
			{
				return (float)(base.ViewState["Y"] ?? 0f);
			}
			set
			{
				base.ViewState["Y"] = value;
				this.ResetGlobal();
			}
		}

		// Token: 0x0600EC4A RID: 60490 RVA: 0x0035E38E File Offset: 0x0035C58E
		protected virtual bool ShouldSerializeY()
		{
			return !this.Auto;
		}

		// Token: 0x0600EC4B RID: 60491 RVA: 0x0035E399 File Offset: 0x0035C599
		protected virtual void ResetY()
		{
			this.Y = 0f;
		}

		// Token: 0x17004784 RID: 18308
		internal object this[StyleProperties name]
		{
			get
			{
				if (name == StyleProperties.Auto)
				{
					return this.Auto;
				}
				switch (name)
				{
				case StyleProperties.AlignedPosition:
					return this.AlignedPosition;
				case StyleProperties.X:
					return this.X;
				case StyleProperties.Y:
					return this.Y;
				default:
					return null;
				}
			}
		}

		// Token: 0x17004785 RID: 18309
		// (get) Token: 0x0600EC4D RID: 60493 RVA: 0x0035E403 File Offset: 0x0035C603
		// (set) Token: 0x0600EC4E RID: 60494 RVA: 0x0035E40B File Offset: 0x0035C60B
		internal float GlobalX
		{
			get
			{
				return this.positionGlobalX;
			}
			set
			{
				this.positionGlobalX = value;
			}
		}

		// Token: 0x17004786 RID: 18310
		// (get) Token: 0x0600EC4F RID: 60495 RVA: 0x0035E414 File Offset: 0x0035C614
		// (set) Token: 0x0600EC50 RID: 60496 RVA: 0x0035E41C File Offset: 0x0035C61C
		internal float GlobalY
		{
			get
			{
				return this.positionGlobalY;
			}
			set
			{
				this.positionGlobalY = value;
			}
		}

		// Token: 0x17004787 RID: 18311
		// (get) Token: 0x0600EC51 RID: 60497 RVA: 0x0035E425 File Offset: 0x0035C625
		internal bool IsSetGlobal
		{
			get
			{
				return !float.IsNaN(this.positionGlobalX) || !float.IsNaN(this.positionGlobalY);
			}
		}

		// Token: 0x0600EC52 RID: 60498 RVA: 0x0035E444 File Offset: 0x0035C644
		public Position(object container) : this()
		{
			this.positionContainerObject = container;
		}

		// Token: 0x0600EC53 RID: 60499 RVA: 0x0035E453 File Offset: 0x0035C653
		public Position()
		{
			this.ResetGlobal();
			this.requireCalculation = true;
		}

		// Token: 0x0600EC54 RID: 60500 RVA: 0x0035E468 File Offset: 0x0035C668
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Position(float x, float y) : this()
		{
			this.X = x;
			this.Y = y;
		}

		// Token: 0x0600EC55 RID: 60501 RVA: 0x0035E47E File Offset: 0x0035C67E
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Position(AlignedPositions position) : this()
		{
			this.AlignedPosition = position;
		}

		// Token: 0x0600EC56 RID: 60502 RVA: 0x0035E48D File Offset: 0x0035C68D
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Position(AlignedPositions position, float x, float y) : this(position)
		{
			this.X = x;
			this.Y = y;
		}

		// Token: 0x0600EC57 RID: 60503 RVA: 0x0035E4A4 File Offset: 0x0035C6A4
		internal virtual void Reset()
		{
			this.Auto = true;
			this.AlignedPosition = AlignedPositions.None;
			this.X = 0f;
			this.Y = 0f;
			this.requireCalculation = true;
		}

		// Token: 0x0600EC58 RID: 60504 RVA: 0x0035E4D1 File Offset: 0x0035C6D1
		internal void ResetGlobal()
		{
			this.positionGlobalX = float.NaN;
			this.positionGlobalY = float.NaN;
		}

		// Token: 0x0600EC59 RID: 60505 RVA: 0x0035E4EC File Offset: 0x0035C6EC
		internal void SetPositionForAutoLayout()
		{
			if (this.AlignedPosition == AlignedPositions.TopLeft)
			{
				this.AlignedPosition = AlignedPositions.Top;
				return;
			}
			if (this.AlignedPosition == AlignedPositions.TopRight)
			{
				this.AlignedPosition = AlignedPositions.Right;
				return;
			}
			if (this.AlignedPosition == AlignedPositions.BottomLeft)
			{
				this.AlignedPosition = AlignedPositions.Bottom;
				return;
			}
			if (this.AlignedPosition == AlignedPositions.BottomRight)
			{
				this.AlignedPosition = AlignedPositions.Bottom;
				return;
			}
			if (this.AlignedPosition == AlignedPositions.Center)
			{
				this.AlignedPosition = AlignedPositions.Bottom;
			}
		}

		// Token: 0x0600EC5A RID: 60506 RVA: 0x0035E564 File Offset: 0x0035C764
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			Position position = obj as Position;
			if (position != null)
			{
				return position.Auto == this.Auto && position.AlignedPosition == this.AlignedPosition && position.X.Equals(this.X) && position.Y.Equals(this.Y);
			}
			return base.Equals(obj);
		}

		// Token: 0x0600EC5B RID: 60507 RVA: 0x0035E5D0 File Offset: 0x0035C7D0
		public override int GetHashCode()
		{
			return this.Auto.GetHashCode() ^ this.AlignedPosition.GetHashCode() ^ this.X.GetHashCode() ^ this.Y.GetHashCode();
		}

		// Token: 0x0600EC5C RID: 60508 RVA: 0x0035E61C File Offset: 0x0035C81C
		public virtual object Clone()
		{
			Position position = (Position)base.MemberwiseClone();
			position.ViewState = base.CloneState();
			position.positionContainerObject = null;
			return position;
		}

		// Token: 0x0400442A RID: 17450
		private float positionGlobalX;

		// Token: 0x0400442B RID: 17451
		private float positionGlobalY;

		// Token: 0x0400442C RID: 17452
		internal bool requireCalculation;

		// Token: 0x0400442D RID: 17453
		private Position positionCopy;

		// Token: 0x0400442E RID: 17454
		internal object positionContainerObject;
	}
}
