using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.Charting.Styles
{
	// Token: 0x0200176F RID: 5999
	internal partial class CustomShapeEditorForm : Form
	{
		// Token: 0x170046F7 RID: 18167
		// (get) Token: 0x0600EA13 RID: 59923 RVA: 0x00355071 File Offset: 0x00353271
		public RadShapeEditorControl EditorControl
		{
			get
			{
				return this.radShapeEditorControl1;
			}
		}

		// Token: 0x0600EA14 RID: 59924 RVA: 0x00355079 File Offset: 0x00353279
		public CustomShapeEditorForm()
		{
			this.InitializeComponent();
			this.radShapeEditorControl1.propertyGrid = this.propertyGrid1;
		}
	}
}
