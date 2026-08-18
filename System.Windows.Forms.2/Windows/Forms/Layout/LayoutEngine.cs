using System;
using System.Drawing;

namespace System.Windows.Forms.Layout
{
	// Token: 0x020004CD RID: 1229
	public abstract class LayoutEngine
	{
		// Token: 0x060050A3 RID: 20643 RVA: 0x0014F968 File Offset: 0x0014DB68
		internal IArrangedElement CastToArrangedElement(object obj)
		{
			IArrangedElement result = obj as IArrangedElement;
			if (obj == null)
			{
				throw new NotSupportedException(SR.GetString("LayoutEngineUnsupportedType", new object[]
				{
					obj.GetType()
				}));
			}
			return result;
		}

		// Token: 0x060050A4 RID: 20644 RVA: 0x00030759 File Offset: 0x0002E959
		internal virtual Size GetPreferredSize(IArrangedElement container, Size proposedConstraints)
		{
			return Size.Empty;
		}

		// Token: 0x060050A5 RID: 20645 RVA: 0x0014F99F File Offset: 0x0014DB9F
		public virtual void InitLayout(object child, BoundsSpecified specified)
		{
			this.InitLayoutCore(this.CastToArrangedElement(child), specified);
		}

		// Token: 0x060050A6 RID: 20646 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void InitLayoutCore(IArrangedElement element, BoundsSpecified bounds)
		{
		}

		// Token: 0x060050A7 RID: 20647 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void ProcessSuspendedLayoutEventArgs(IArrangedElement container, LayoutEventArgs args)
		{
		}

		// Token: 0x060050A8 RID: 20648 RVA: 0x0014F9B0 File Offset: 0x0014DBB0
		public virtual bool Layout(object container, LayoutEventArgs layoutEventArgs)
		{
			return this.LayoutCore(this.CastToArrangedElement(container), layoutEventArgs);
		}

		// Token: 0x060050A9 RID: 20649 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal virtual bool LayoutCore(IArrangedElement container, LayoutEventArgs layoutEventArgs)
		{
			return false;
		}
	}
}
