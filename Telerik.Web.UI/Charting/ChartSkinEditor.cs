using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Telerik.Charting.Styles;
using Telerik.Charting.Styles.Skins;

namespace Telerik.Charting
{
	// Token: 0x0200174F RID: 5967
	internal class ChartSkinEditor : UITypeEditor, IDisposable
	{
		// Token: 0x0600E8D2 RID: 59602 RVA: 0x003447F0 File Offset: 0x003429F0
		public ChartSkinEditor()
		{
			this.columnsListing = new ListBox();
			ChartSkinsCollection chartSkinsCollection = new ChartSkinsCollection();
			foreach (string item in chartSkinsCollection.GetNames())
			{
				this.columnsListing.Items.Add(item);
			}
			this.columnsListing.Size = this.columnsListing.PreferredSize;
			this.columnsListing.Height = this.columnsListing.PreferredHeight * 2 / 3;
			this.columnsListing.SelectedIndexChanged += this.columnsListing_SelectedIndexChanged;
		}

		// Token: 0x170046C2 RID: 18114
		// (get) Token: 0x0600E8D3 RID: 59603 RVA: 0x003448AC File Offset: 0x00342AAC
		public override bool IsDropDownResizable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600E8D4 RID: 59604 RVA: 0x003448AF File Offset: 0x00342AAF
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x0600E8D5 RID: 59605 RVA: 0x003448B4 File Offset: 0x00342AB4
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			object obj = null;
			if (provider != null)
			{
				this.editorService = (provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService);
			}
			if (this.editorService != null)
			{
				this.editorService.DropDownControl(this.columnsListing);
				if (this.columnsListing.SelectedItem != null)
				{
					obj = this.columnsListing.SelectedItem.ToString();
				}
			}
			if (obj != null && obj != value)
			{
				value = obj;
				if (!ChartSkin.IsEmpty(obj.ToString()))
				{
					IComponent component = ChartSkinEditor.GetComponent(provider);
					component.GetType().GetMethod("ClearSkin", BindingFlags.Instance | BindingFlags.Public).Invoke(component, null);
				}
			}
			return value;
		}

		// Token: 0x0600E8D6 RID: 59606 RVA: 0x00344953 File Offset: 0x00342B53
		public void columnsListing_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.editorService != null)
			{
				this.editorService.CloseDropDown();
			}
		}

		// Token: 0x0600E8D7 RID: 59607 RVA: 0x00344968 File Offset: 0x00342B68
		internal static IComponent GetComponent(IServiceProvider serviceProvider)
		{
			ISelectionService selectionService = (ISelectionService)serviceProvider.GetService(typeof(ISelectionService));
			return (IComponent)selectionService.PrimarySelection;
		}

		// Token: 0x0600E8D8 RID: 59608 RVA: 0x00344996 File Offset: 0x00342B96
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600E8D9 RID: 59609 RVA: 0x003449A5 File Offset: 0x00342BA5
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.columnsListing.Dispose();
			}
		}

		// Token: 0x040042F6 RID: 17142
		private IWindowsFormsEditorService editorService;

		// Token: 0x040042F7 RID: 17143
		public ListBox columnsListing;
	}
}
