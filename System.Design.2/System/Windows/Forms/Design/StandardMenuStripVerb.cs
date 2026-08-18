using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Design;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000336 RID: 822
	internal class StandardMenuStripVerb
	{
		// Token: 0x06002070 RID: 8304 RVA: 0x000C4828 File Offset: 0x000C2A28
		internal StandardMenuStripVerb(string text, ToolStripDesigner designer)
		{
			this._designer = designer;
			this._provider = designer.Component.Site;
			this._host = (IDesignerHost)this._provider.GetService(typeof(IDesignerHost));
			this.componentChangeSvc = (IComponentChangeService)this._provider.GetService(typeof(IComponentChangeService));
			if (text == null)
			{
				text = SR.GetString("ToolStripDesignerStandardItemsVerb");
			}
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x000C48A4 File Offset: 0x000C2AA4
		public void InsertItems()
		{
			DesignerActionUIService designerActionUIService = (DesignerActionUIService)this._host.GetService(typeof(DesignerActionUIService));
			if (designerActionUIService != null)
			{
				designerActionUIService.HideUI(this._designer.Component);
			}
			Cursor value = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				if (this._designer.Component is MenuStrip)
				{
					this.CreateStandardMenuStrip(this._host, (MenuStrip)this._designer.Component);
				}
				else
				{
					this.CreateStandardToolStrip(this._host, (ToolStrip)this._designer.Component);
				}
			}
			finally
			{
				Cursor.Current = value;
			}
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x000C4958 File Offset: 0x000C2B58
		private void CreateStandardMenuStrip(IDesignerHost host, MenuStrip tool)
		{
			string[][] array = new string[][]
			{
				new string[]
				{
					SR.GetString("StandardMenuFile"),
					SR.GetString("StandardMenuNew"),
					SR.GetString("StandardMenuOpen"),
					"-",
					SR.GetString("StandardMenuSave"),
					SR.GetString("StandardMenuSaveAs"),
					"-",
					SR.GetString("StandardMenuPrint"),
					SR.GetString("StandardMenuPrintPreview"),
					"-",
					SR.GetString("StandardMenuExit")
				},
				new string[]
				{
					SR.GetString("StandardMenuEdit"),
					SR.GetString("StandardMenuUndo"),
					SR.GetString("StandardMenuRedo"),
					"-",
					SR.GetString("StandardMenuCut"),
					SR.GetString("StandardMenuCopy"),
					SR.GetString("StandardMenuPaste"),
					"-",
					SR.GetString("StandardMenuSelectAll")
				},
				new string[]
				{
					SR.GetString("StandardMenuTools"),
					SR.GetString("StandardMenuCustomize"),
					SR.GetString("StandardMenuOptions")
				},
				new string[]
				{
					SR.GetString("StandardMenuHelp"),
					SR.GetString("StandardMenuContents"),
					SR.GetString("StandardMenuIndex"),
					SR.GetString("StandardMenuSearch"),
					"-",
					SR.GetString("StandardMenuAbout")
				}
			};
			string[][] array2 = new string[][]
			{
				new string[]
				{
					"",
					"new",
					"open",
					"-",
					"save",
					"",
					"-",
					"print",
					"printPreview",
					"-",
					""
				},
				new string[]
				{
					"",
					"",
					"",
					"-",
					"cut",
					"copy",
					"paste",
					"-",
					""
				},
				new string[]
				{
					"",
					"",
					""
				},
				new string[]
				{
					"",
					"",
					"",
					"",
					"-",
					""
				}
			};
			Keys[][] array3 = new Keys[4][];
			int num = 0;
			Keys[] array4 = new Keys[11];
			RuntimeHelpers.InitializeArray(array4, fieldof(<PrivateImplementationDetails>.759AE3623284452337C686D23B082BBBF8925990EE41D225EFBF998CFDFF3976).FieldHandle);
			array3[num] = array4;
			int num2 = 1;
			Keys[] array5 = new Keys[9];
			RuntimeHelpers.InitializeArray(array5, fieldof(<PrivateImplementationDetails>.C48D71A48A89051A601DA7E0D8A745030D9D8D8C28AECCE50F642A1C4B11B621).FieldHandle);
			array3[num2] = array5;
			array3[2] = new Keys[3];
			array3[3] = new Keys[6];
			Keys[][] array6 = array3;
			if (host == null)
			{
				return;
			}
			tool.SuspendLayout();
			ToolStripDesigner._autoAddNewItems = false;
			DesignerTransaction designerTransaction = this._host.CreateTransaction(SR.GetString("StandardMenuCreateDesc"));
			try
			{
				INameCreationService nameCreationService = (INameCreationService)this._provider.GetService(typeof(INameCreationService));
				string text = "standardMainMenuStrip";
				string text2 = text;
				int num3 = 1;
				if (host != null)
				{
					while (this._host.Container.Components[text2] != null)
					{
						text2 = text + num3++.ToString(CultureInfo.InvariantCulture);
					}
				}
				for (int i = 0; i < array.Length; i++)
				{
					string[] array7 = array[i];
					ToolStripMenuItem toolStripMenuItem = null;
					for (int j = 0; j < array7.Length; j++)
					{
						string text3 = array7[j];
						text2 = this.NameFromText(text3, typeof(ToolStripMenuItem), nameCreationService, true);
						ToolStripItem toolStripItem = null;
						if (text2.Contains("Separator"))
						{
							toolStripItem = (ToolStripSeparator)this._host.CreateComponent(typeof(ToolStripSeparator), text2);
							IDesigner designer = this._host.GetDesigner(toolStripItem);
							if (designer is ComponentDesigner)
							{
								((ComponentDesigner)designer).InitializeNewComponent(null);
							}
							toolStripItem.Text = text3;
						}
						else
						{
							toolStripItem = (ToolStripMenuItem)this._host.CreateComponent(typeof(ToolStripMenuItem), text2);
							IDesigner designer2 = this._host.GetDesigner(toolStripItem);
							if (designer2 is ComponentDesigner)
							{
								((ComponentDesigner)designer2).InitializeNewComponent(null);
							}
							toolStripItem.Text = text3;
							Keys keys = array6[i][j];
							if (toolStripItem is ToolStripMenuItem && keys != Keys.None && !ToolStripManager.IsShortcutDefined(keys) && ToolStripManager.IsValidShortcut(keys))
							{
								((ToolStripMenuItem)toolStripItem).ShortcutKeys = keys;
							}
							Bitmap bitmap = null;
							try
							{
								bitmap = this.GetImage(array2[i][j]);
							}
							catch
							{
							}
							if (bitmap != null)
							{
								PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(toolStripItem)["Image"];
								if (propertyDescriptor != null)
								{
									propertyDescriptor.SetValue(toolStripItem, bitmap);
								}
								toolStripItem.ImageTransparentColor = Color.Magenta;
							}
						}
						if (j == 0)
						{
							toolStripMenuItem = (ToolStripMenuItem)toolStripItem;
							toolStripMenuItem.DropDown.SuspendLayout();
						}
						else
						{
							toolStripMenuItem.DropDownItems.Add(toolStripItem);
						}
						if (j == array7.Length - 1)
						{
							MemberDescriptor member = TypeDescriptor.GetProperties(toolStripMenuItem)["DropDownItems"];
							this.componentChangeSvc.OnComponentChanging(toolStripMenuItem, member);
							this.componentChangeSvc.OnComponentChanged(toolStripMenuItem, member, null, null);
						}
					}
					toolStripMenuItem.DropDown.ResumeLayout(false);
					tool.Items.Add(toolStripMenuItem);
					if (i == array.Length - 1)
					{
						MemberDescriptor member2 = TypeDescriptor.GetProperties(tool)["Items"];
						this.componentChangeSvc.OnComponentChanging(tool, member2);
						this.componentChangeSvc.OnComponentChanged(tool, member2, null, null);
					}
				}
			}
			catch (Exception ex)
			{
				if (ex is InvalidOperationException)
				{
					IUIService iuiservice = (IUIService)this._provider.GetService(typeof(IUIService));
					iuiservice.ShowError(ex.Message);
				}
				if (designerTransaction != null)
				{
					designerTransaction.Cancel();
					designerTransaction = null;
				}
			}
			finally
			{
				ToolStripDesigner._autoAddNewItems = true;
				if (designerTransaction != null)
				{
					designerTransaction.Commit();
					designerTransaction = null;
				}
				tool.ResumeLayout();
				ISelectionService selectionService = (ISelectionService)this._provider.GetService(typeof(ISelectionService));
				if (selectionService != null)
				{
					selectionService.SetSelectedComponents(new object[]
					{
						this._designer.Component
					});
				}
				DesignerActionUIService designerActionUIService = (DesignerActionUIService)this._provider.GetService(typeof(DesignerActionUIService));
				if (designerActionUIService != null)
				{
					designerActionUIService.Refresh(this._designer.Component);
				}
				SelectionManager selectionManager = (SelectionManager)this._provider.GetService(typeof(SelectionManager));
				selectionManager.Refresh();
			}
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x000C5058 File Offset: 0x000C3258
		private void CreateStandardToolStrip(IDesignerHost host, ToolStrip tool)
		{
			string[] array = new string[]
			{
				SR.GetString("StandardMenuNew"),
				SR.GetString("StandardMenuOpen"),
				SR.GetString("StandardMenuSave"),
				SR.GetString("StandardMenuPrint"),
				"-",
				SR.GetString("StandardToolCut"),
				SR.GetString("StandardMenuCopy"),
				SR.GetString("StandardMenuPaste"),
				"-",
				SR.GetString("StandardToolHelp")
			};
			string[] array2 = new string[]
			{
				"new",
				"open",
				"save",
				"print",
				"-",
				"cut",
				"copy",
				"paste",
				"-",
				"help"
			};
			if (host == null)
			{
				return;
			}
			tool.SuspendLayout();
			ToolStripDesigner._autoAddNewItems = false;
			DesignerTransaction designerTransaction = this._host.CreateTransaction(SR.GetString("StandardMenuCreateDesc"));
			try
			{
				INameCreationService nameCreationService = (INameCreationService)this._provider.GetService(typeof(INameCreationService));
				string text = "standardMainToolStrip";
				string text2 = text;
				int num = 1;
				if (host != null)
				{
					while (this._host.Container.Components[text2] != null)
					{
						text2 = text + num++.ToString(CultureInfo.InvariantCulture);
					}
				}
				int num2 = 0;
				foreach (string text3 in array)
				{
					text2 = this.NameFromText(text3, typeof(ToolStripButton), nameCreationService, true);
					ToolStripItem toolStripItem = null;
					if (text2.Contains("Separator"))
					{
						toolStripItem = (ToolStripSeparator)this._host.CreateComponent(typeof(ToolStripSeparator), text2);
						IDesigner designer = this._host.GetDesigner(toolStripItem);
						if (designer is ComponentDesigner)
						{
							((ComponentDesigner)designer).InitializeNewComponent(null);
						}
					}
					else
					{
						toolStripItem = (ToolStripButton)this._host.CreateComponent(typeof(ToolStripButton), text2);
						IDesigner designer2 = this._host.GetDesigner(toolStripItem);
						if (designer2 is ComponentDesigner)
						{
							((ComponentDesigner)designer2).InitializeNewComponent(null);
						}
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(toolStripItem)["DisplayStyle"];
						if (propertyDescriptor != null)
						{
							propertyDescriptor.SetValue(toolStripItem, ToolStripItemDisplayStyle.Image);
						}
						PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(toolStripItem)["Text"];
						if (propertyDescriptor2 != null)
						{
							propertyDescriptor2.SetValue(toolStripItem, text3);
						}
						Bitmap bitmap = null;
						try
						{
							bitmap = this.GetImage(array2[num2]);
						}
						catch
						{
						}
						if (bitmap != null)
						{
							PropertyDescriptor propertyDescriptor3 = TypeDescriptor.GetProperties(toolStripItem)["Image"];
							if (propertyDescriptor3 != null)
							{
								propertyDescriptor3.SetValue(toolStripItem, bitmap);
							}
							toolStripItem.ImageTransparentColor = Color.Magenta;
						}
					}
					tool.Items.Add(toolStripItem);
					num2++;
				}
				MemberDescriptor member = TypeDescriptor.GetProperties(tool)["Items"];
				this.componentChangeSvc.OnComponentChanging(tool, member);
				this.componentChangeSvc.OnComponentChanged(tool, member, null, null);
			}
			catch (Exception ex)
			{
				if (ex is InvalidOperationException)
				{
					IUIService iuiservice = (IUIService)this._provider.GetService(typeof(IUIService));
					iuiservice.ShowError(ex.Message);
				}
				if (designerTransaction != null)
				{
					designerTransaction.Cancel();
					designerTransaction = null;
				}
			}
			finally
			{
				ToolStripDesigner._autoAddNewItems = true;
				if (designerTransaction != null)
				{
					designerTransaction.Commit();
					designerTransaction = null;
				}
				tool.ResumeLayout();
				ISelectionService selectionService = (ISelectionService)this._provider.GetService(typeof(ISelectionService));
				if (selectionService != null)
				{
					selectionService.SetSelectedComponents(new object[]
					{
						this._designer.Component
					});
				}
				DesignerActionUIService designerActionUIService = (DesignerActionUIService)this._provider.GetService(typeof(DesignerActionUIService));
				if (designerActionUIService != null)
				{
					designerActionUIService.Refresh(this._designer.Component);
				}
				SelectionManager selectionManager = (SelectionManager)this._provider.GetService(typeof(SelectionManager));
				selectionManager.Refresh();
			}
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x000C54C0 File Offset: 0x000C36C0
		private Bitmap GetImage(string name)
		{
			Bitmap result = null;
			if (name.StartsWith("new"))
			{
				result = new Bitmap(typeof(ToolStripMenuItem), "new.bmp");
			}
			else if (name.StartsWith("open"))
			{
				result = new Bitmap(typeof(ToolStripMenuItem), "open.bmp");
			}
			else if (name.StartsWith("save"))
			{
				result = new Bitmap(typeof(ToolStripMenuItem), "save.bmp");
			}
			else if (name.StartsWith("printPreview"))
			{
				result = new Bitmap(typeof(ToolStripMenuItem), "printPreview.bmp");
			}
			else if (name.StartsWith("print"))
			{
				result = new Bitmap(typeof(ToolStripMenuItem), "print.bmp");
			}
			else if (name.StartsWith("cut"))
			{
				result = new Bitmap(typeof(ToolStripMenuItem), "cut.bmp");
			}
			else if (name.StartsWith("copy"))
			{
				result = new Bitmap(typeof(ToolStripMenuItem), "copy.bmp");
			}
			else if (name.StartsWith("paste"))
			{
				result = new Bitmap(typeof(ToolStripMenuItem), "paste.bmp");
			}
			else if (name.StartsWith("help"))
			{
				result = new Bitmap(typeof(ToolStripMenuItem), "help.bmp");
			}
			return result;
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x000C5624 File Offset: 0x000C3824
		private string NameFromText(string text, Type itemType, INameCreationService nameCreationService, bool adjustCapitalization)
		{
			string text2;
			if (text == "-")
			{
				text2 = "toolStripSeparator";
			}
			else
			{
				string name = itemType.Name;
				StringBuilder stringBuilder = new StringBuilder(text.Length + name.Length);
				bool flag = false;
				foreach (char c in text)
				{
					if (char.IsLetterOrDigit(c))
					{
						if (!flag)
						{
							c = char.ToLower(c, CultureInfo.CurrentCulture);
							flag = true;
						}
						stringBuilder.Append(c);
					}
				}
				stringBuilder.Append(name);
				text2 = stringBuilder.ToString();
				if (adjustCapitalization)
				{
					string text3 = ToolStripDesigner.NameFromText(null, typeof(ToolStripMenuItem), this._designer.Component.Site);
					if (!string.IsNullOrEmpty(text3) && char.IsUpper(text3[0]))
					{
						text2 = char.ToUpper(text2[0], CultureInfo.InvariantCulture).ToString() + text2.Substring(1);
					}
				}
			}
			if (this._host.Container.Components[text2] != null)
			{
				string text4 = text2;
				int num = 1;
				while (!nameCreationService.IsValidName(text4))
				{
					text4 = text2 + num.ToString(CultureInfo.InvariantCulture);
					num++;
				}
				return text4;
			}
			if (!nameCreationService.IsValidName(text2))
			{
				return nameCreationService.CreateName(this._host.Container, itemType);
			}
			return text2;
		}

		// Token: 0x040018ED RID: 6381
		private ToolStripDesigner _designer;

		// Token: 0x040018EE RID: 6382
		private IDesignerHost _host;

		// Token: 0x040018EF RID: 6383
		private IComponentChangeService componentChangeSvc;

		// Token: 0x040018F0 RID: 6384
		private IServiceProvider _provider;
	}
}
