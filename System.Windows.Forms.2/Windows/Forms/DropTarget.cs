using System;
using System.Runtime.InteropServices.ComTypes;

namespace System.Windows.Forms
{
	// Token: 0x02000249 RID: 585
	internal class DropTarget : UnsafeNativeMethods.IOleDropTarget
	{
		// Token: 0x06002512 RID: 9490 RVA: 0x000AD624 File Offset: 0x000AB824
		public DropTarget(IDropTarget owner)
		{
			this.owner = owner;
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x000AD634 File Offset: 0x000AB834
		private DragEventArgs CreateDragEventArgs(object pDataObj, int grfKeyState, NativeMethods.POINTL pt, int pdwEffect)
		{
			IDataObject data;
			if (pDataObj == null)
			{
				data = this.lastDataObject;
			}
			else if (pDataObj is IDataObject)
			{
				data = (IDataObject)pDataObj;
			}
			else
			{
				if (!(pDataObj is IDataObject))
				{
					return null;
				}
				data = new DataObject(pDataObj);
			}
			DragEventArgs result = new DragEventArgs(data, grfKeyState, pt.x, pt.y, (DragDropEffects)pdwEffect, this.lastEffect);
			this.lastDataObject = data;
			return result;
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x000AD698 File Offset: 0x000AB898
		int UnsafeNativeMethods.IOleDropTarget.OleDragEnter(object pDataObj, int grfKeyState, UnsafeNativeMethods.POINTSTRUCT pt, ref int pdwEffect)
		{
			DragEventArgs dragEventArgs = this.CreateDragEventArgs(pDataObj, grfKeyState, new NativeMethods.POINTL
			{
				x = pt.x,
				y = pt.y
			}, pdwEffect);
			if (dragEventArgs != null)
			{
				this.owner.OnDragEnter(dragEventArgs);
				pdwEffect = (int)dragEventArgs.Effect;
				this.lastEffect = dragEventArgs.Effect;
			}
			else
			{
				pdwEffect = 0;
			}
			return 0;
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x000AD6FC File Offset: 0x000AB8FC
		int UnsafeNativeMethods.IOleDropTarget.OleDragOver(int grfKeyState, UnsafeNativeMethods.POINTSTRUCT pt, ref int pdwEffect)
		{
			DragEventArgs dragEventArgs = this.CreateDragEventArgs(null, grfKeyState, new NativeMethods.POINTL
			{
				x = pt.x,
				y = pt.y
			}, pdwEffect);
			this.owner.OnDragOver(dragEventArgs);
			pdwEffect = (int)dragEventArgs.Effect;
			this.lastEffect = dragEventArgs.Effect;
			return 0;
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x000AD754 File Offset: 0x000AB954
		int UnsafeNativeMethods.IOleDropTarget.OleDragLeave()
		{
			this.owner.OnDragLeave(EventArgs.Empty);
			return 0;
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x000AD768 File Offset: 0x000AB968
		int UnsafeNativeMethods.IOleDropTarget.OleDrop(object pDataObj, int grfKeyState, UnsafeNativeMethods.POINTSTRUCT pt, ref int pdwEffect)
		{
			DragEventArgs dragEventArgs = this.CreateDragEventArgs(pDataObj, grfKeyState, new NativeMethods.POINTL
			{
				x = pt.x,
				y = pt.y
			}, pdwEffect);
			if (dragEventArgs != null)
			{
				this.owner.OnDragDrop(dragEventArgs);
				pdwEffect = (int)dragEventArgs.Effect;
			}
			else
			{
				pdwEffect = 0;
			}
			this.lastEffect = DragDropEffects.None;
			this.lastDataObject = null;
			return 0;
		}

		// Token: 0x04000F6A RID: 3946
		private IDataObject lastDataObject;

		// Token: 0x04000F6B RID: 3947
		private DragDropEffects lastEffect;

		// Token: 0x04000F6C RID: 3948
		private IDropTarget owner;
	}
}
