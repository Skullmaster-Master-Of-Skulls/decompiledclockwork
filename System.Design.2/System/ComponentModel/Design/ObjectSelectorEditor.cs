using System;
using System.Design;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.ComponentModel.Design
{
	// Token: 0x020001BC RID: 444
	public abstract class ObjectSelectorEditor : UITypeEditor
	{
		// Token: 0x06001021 RID: 4129 RVA: 0x00003939 File Offset: 0x00001B39
		public ObjectSelectorEditor()
		{
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0005B5A4 File Offset: 0x000597A4
		public ObjectSelectorEditor(bool subObjectSelector)
		{
			this.SubObjectSelector = subObjectSelector;
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0005B5B4 File Offset: 0x000597B4
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null)
				{
					if (this.selector == null)
					{
						this.selector = new ObjectSelectorEditor.Selector(this);
						DesignerUtils.ApplyTreeViewThemeStyles(this.selector);
					}
					this.prevValue = value;
					this.currValue = value;
					this.FillTreeWithData(this.selector, context, provider);
					this.selector.Start(windowsFormsEditorService, value);
					windowsFormsEditorService.DropDownControl(this.selector);
					this.selector.Stop();
					if (this.prevValue != this.currValue)
					{
						value = this.currValue;
					}
				}
			}
			return value;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0003DFAE File Offset: 0x0003C1AE
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0005B656 File Offset: 0x00059856
		public bool EqualsToValue(object value)
		{
			return value == this.currValue;
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0005B664 File Offset: 0x00059864
		protected virtual void FillTreeWithData(ObjectSelectorEditor.Selector selector, ITypeDescriptorContext context, IServiceProvider provider)
		{
			selector.Clear();
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0005B66C File Offset: 0x0005986C
		public virtual void SetValue(object value)
		{
			this.currValue = value;
		}

		// Token: 0x0400095D RID: 2397
		public bool SubObjectSelector;

		// Token: 0x0400095E RID: 2398
		protected object prevValue;

		// Token: 0x0400095F RID: 2399
		protected object currValue;

		// Token: 0x04000960 RID: 2400
		private ObjectSelectorEditor.Selector selector;

		// Token: 0x02000495 RID: 1173
		public class Selector : TreeView
		{
			// Token: 0x06002B49 RID: 11081 RVA: 0x00103074 File Offset: 0x00101274
			public Selector(ObjectSelectorEditor editor)
			{
				this.CreateHandle();
				this.editor = editor;
				base.BorderStyle = BorderStyle.None;
				base.FullRowSelect = !editor.SubObjectSelector;
				base.Scrollable = true;
				base.CheckBoxes = false;
				base.ShowPlusMinus = editor.SubObjectSelector;
				base.ShowLines = editor.SubObjectSelector;
				base.ShowRootLines = editor.SubObjectSelector;
				base.AfterSelect += this.OnAfterSelect;
			}

			// Token: 0x06002B4A RID: 11082 RVA: 0x001030F0 File Offset: 0x001012F0
			public ObjectSelectorEditor.SelectorNode AddNode(string label, object value, ObjectSelectorEditor.SelectorNode parent)
			{
				ObjectSelectorEditor.SelectorNode selectorNode = new ObjectSelectorEditor.SelectorNode(label, value);
				if (parent != null)
				{
					parent.Nodes.Add(selectorNode);
				}
				else
				{
					base.Nodes.Add(selectorNode);
				}
				return selectorNode;
			}

			// Token: 0x06002B4B RID: 11083 RVA: 0x00103128 File Offset: 0x00101328
			private bool ChooseSelectedNodeIfEqual()
			{
				if (this.editor != null && this.edSvc != null)
				{
					this.editor.SetValue(((ObjectSelectorEditor.SelectorNode)base.SelectedNode).value);
					if (this.editor.EqualsToValue(((ObjectSelectorEditor.SelectorNode)base.SelectedNode).value))
					{
						this.edSvc.CloseDropDown();
						return true;
					}
				}
				return false;
			}

			// Token: 0x06002B4C RID: 11084 RVA: 0x0010318B File Offset: 0x0010138B
			public void Clear()
			{
				this.clickSeen = false;
				base.Nodes.Clear();
			}

			// Token: 0x06002B4D RID: 11085 RVA: 0x0010319F File Offset: 0x0010139F
			protected void OnAfterSelect(object sender, TreeViewEventArgs e)
			{
				if (this.clickSeen)
				{
					this.ChooseSelectedNodeIfEqual();
					this.clickSeen = false;
				}
			}

			// Token: 0x06002B4E RID: 11086 RVA: 0x001031B8 File Offset: 0x001013B8
			protected override void OnKeyDown(KeyEventArgs e)
			{
				Keys keyCode = e.KeyCode;
				if (keyCode != Keys.Return)
				{
					if (keyCode == Keys.Escape)
					{
						this.editor.SetValue(this.editor.prevValue);
						e.Handled = true;
						this.edSvc.CloseDropDown();
					}
				}
				else if (this.ChooseSelectedNodeIfEqual())
				{
					e.Handled = true;
				}
				base.OnKeyDown(e);
			}

			// Token: 0x06002B4F RID: 11087 RVA: 0x00103218 File Offset: 0x00101418
			protected override void OnKeyPress(KeyPressEventArgs e)
			{
				char keyChar = e.KeyChar;
				if (keyChar == '\r')
				{
					e.Handled = true;
				}
				base.OnKeyPress(e);
			}

			// Token: 0x06002B50 RID: 11088 RVA: 0x0010323F File Offset: 0x0010143F
			protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
			{
				if (e.Node == base.SelectedNode)
				{
					this.ChooseSelectedNodeIfEqual();
				}
				base.OnNodeMouseClick(e);
			}

			// Token: 0x06002B51 RID: 11089 RVA: 0x00103260 File Offset: 0x00101460
			public bool SetSelection(object value, TreeNodeCollection nodes)
			{
				TreeNode[] array;
				if (nodes == null)
				{
					array = new TreeNode[base.Nodes.Count];
					base.Nodes.CopyTo(array, 0);
				}
				else
				{
					array = new TreeNode[nodes.Count];
					nodes.CopyTo(array, 0);
				}
				int num = array.Length;
				if (num == 0)
				{
					return false;
				}
				for (int i = 0; i < num; i++)
				{
					if (((ObjectSelectorEditor.SelectorNode)array[i]).value == value)
					{
						base.SelectedNode = array[i];
						return true;
					}
					if (array[i].Nodes != null && array[i].Nodes.Count != 0)
					{
						array[i].Expand();
						if (this.SetSelection(value, array[i].Nodes))
						{
							return true;
						}
						array[i].Collapse();
					}
				}
				return false;
			}

			// Token: 0x06002B52 RID: 11090 RVA: 0x00103311 File Offset: 0x00101511
			public void Start(IWindowsFormsEditorService edSvc, object value)
			{
				this.edSvc = edSvc;
				this.clickSeen = false;
				this.SetSelection(value, base.Nodes);
			}

			// Token: 0x06002B53 RID: 11091 RVA: 0x0010332F File Offset: 0x0010152F
			public void Stop()
			{
				this.edSvc = null;
			}

			// Token: 0x06002B54 RID: 11092 RVA: 0x00103338 File Offset: 0x00101538
			protected override void WndProc(ref Message m)
			{
				int msg = m.Msg;
				if (msg != 135)
				{
					if (msg != 512)
					{
						if (msg == 8270)
						{
							NativeMethods.NMTREEVIEW nmtreeview = (NativeMethods.NMTREEVIEW)Marshal.PtrToStructure(m.LParam, typeof(NativeMethods.NMTREEVIEW));
							if (nmtreeview.nmhdr.code == -2)
							{
								this.clickSeen = true;
							}
						}
					}
					else if (this.clickSeen)
					{
						this.clickSeen = false;
					}
					base.WndProc(ref m);
					return;
				}
				m.Result = (IntPtr)((long)m.Result | 4L);
			}

			// Token: 0x04001E18 RID: 7704
			private ObjectSelectorEditor editor;

			// Token: 0x04001E19 RID: 7705
			private IWindowsFormsEditorService edSvc;

			// Token: 0x04001E1A RID: 7706
			public bool clickSeen;
		}

		// Token: 0x02000496 RID: 1174
		public class SelectorNode : TreeNode
		{
			// Token: 0x06002B55 RID: 11093 RVA: 0x001033C9 File Offset: 0x001015C9
			public SelectorNode(string label, object value) : base(label)
			{
				this.value = value;
			}

			// Token: 0x04001E1B RID: 7707
			public object value;
		}
	}
}
